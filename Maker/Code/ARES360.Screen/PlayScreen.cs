using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.UI;
using ARES360Loader.Data.World;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Graphics.PostProcessing;
using System;
using System.Threading;

namespace ARES360.Screen
{
	public class PlayScreen : Screen
	{
		public enum AbortReason
		{
			Normal,
			GamerSignedOut,
			EndTrial,
			ExitTrial
		}

		private enum State
		{
			Load,
			Play,
			ChangeWorld,
			ResetWorld,
			Abort,
			AbortBySignedOut,
			AbortByEndTrial,
			AbortForVideo,
			SaveBeforeAbort,
			ManageIfSaveFail,
			Score
		}

		private static PlayScreen mInstance;

		private State mState;

		private float mFadeTime;

		private float mAbortFadeOutTime;

		private AbortReason mCurrentAbortReason;

		private int mSaveBeforeAbortStep;

		private byte mLevelToChanged;

		public static PlayScreen Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new PlayScreen();
				}
				return mInstance;
			}
		}

		private PlayScreen()
		{
		}

		public override void Load()
		{
			base.LoadingDone = false;
			mState = State.Load;
			LoadWorld();
			base.LoadingDone = true;
			ScoreUI.Instance.Load();
		}

		private void LoadWorld()
		{
			base.LoadingDone = false;
			switch (World.CurrentLevel)
			{
			case 1:
				World.Load(new Junkyard());
				break;
			case 2:
				World.Load(new Recycleplant());
				break;
			case 3:
				World.Load(new Sewer());
				break;
			case 4:
				World.Load(new Livingquarter());
				break;
			case 5:
				World.Load(new Tram());
				break;
			case 6:
				World.Load(new Hallway());
				break;
			case 7:
				World.Load(new Minoscore());
				break;
			}
			ParticleLoader.LoadPlayWorld(World.CurrentLevel);
			Thread.Sleep(1);
			Renderer.LockGraphicsDeviceWhileDrawing = 0;
			SFXManager.UnmuteGameVolumes();
			base.LoadingDone = true;
			EnemyManager.IsActive = true;
		}

		public override void Initialize()
		{
			World.Camera.ClearDynamicBoundingFocusedRectangles();
			World.Camera.RotationZ = 0f;
			ProcessManager.AddProcess(Narrator.Instance);
			Director.SetCurrentLevel(ProfileManager.Current.CurrentLevel);
			World.SummonParticle();
			if (!World.HasCheckPoint)
			{
				byte currentLevel = World.CurrentLevel;
				if (World.CurrentLevel != 2 && World.CurrentLevel != 3 && World.CurrentLevel != 4 && World.CurrentLevel != 5 && World.CurrentLevel != 6)
				{
					byte currentLevel2 = World.CurrentLevel;
				}
			}
			if (World.CurrentLevel == 5)
			{
				LV5Loader.AddLoopedBackgrounds();
			}
			else if (World.CurrentLevel == 7)
			{
				LV7Loader.AddNonRoomedObjects();
			}
			if (!World.HasCheckPoint)
			{
				switch (World.CurrentLevel)
				{
				case 1:
					Director.JunkyardOpenScene();
					break;
				case 2:
					Director.RecycleplantOpenScene();
					break;
				case 3:
					Director.SewerOpenScene();
					break;
				case 4:
					Director.LivingQuarterOpenScene();
					break;
				case 5:
					Director.TramOpenScene();
					break;
				case 6:
					Director.HallwayOpenScene();
					break;
				case 7:
					Director.MinosCoreOpening();
					break;
				}
			}
			else
			{
				World.PlayCheckPoint();
			}
			World.HitsByBoss = 0;
			if (mState == State.Load || mState == State.ChangeWorld)
			{
				World.LevelPlayTime = 0f;
				World.ResetScore();
			}
			else
			{
				SFXManager.StopLoopSound();
			}
			Director.FadeIn(1f);
			base.ActivityFinished = false;
			base.LoadingDone = false;
			string gamerPresence = string.Empty;
			if (ProfileManager.Current.PlayerType != 0)
			{
				switch (World.CurrentLevel)
				{
				case 1:
					gamerPresence = Pref.GP_TARUS_LV1;
					break;
				case 2:
					gamerPresence = Pref.GP_TARUS_LV2;
					break;
				case 3:
					gamerPresence = Pref.GP_TARUS_LV3;
					break;
				case 4:
					gamerPresence = Pref.GP_TARUS_LV4;
					break;
				case 5:
					gamerPresence = Pref.GP_TARUS_LV5;
					break;
				case 6:
					gamerPresence = Pref.GP_TARUS_LV6;
					break;
				case 7:
					gamerPresence = Pref.GP_TARUS_LV7;
					break;
				}
			}
			else
			{
				switch (World.CurrentLevel)
				{
				case 1:
					gamerPresence = Pref.GP_ARES_LV1;
					break;
				case 2:
					gamerPresence = Pref.GP_ARES_LV2;
					break;
				case 3:
					gamerPresence = Pref.GP_ARES_LV3;
					break;
				case 4:
					gamerPresence = Pref.GP_ARES_LV4;
					break;
				case 5:
					gamerPresence = Pref.GP_ARES_LV5;
					break;
				case 6:
					gamerPresence = Pref.GP_ARES_LV6;
					break;
				case 7:
					gamerPresence = Pref.GP_ARES_LV7;
					break;
				}
			}
			ProfileManager.SetGamerPresence(gamerPresence);
			gamerPresence = null;
		}

		public void ChangeLevel(byte level)
		{
			mLevelToChanged = level;
			SaveBeforeChangeLevel();
		}

		private void SaveBeforeChangeLevel()
		{
			ProfileManager.BeginSave(ChangeLevelSaveFinish, 7, false);
		}

		private void ChangeLevelSaveFinish(IAsyncResult result)
		{
			if (result.IsCompleted)
			{
				_DoChangeLevel();
			}
			else
			{
				mState = State.ManageIfSaveFail;
				ProfileManager.ManageSaveFail(SaveBeforeChangeLevel, _DoChangeLevel);
			}
		}

		private void _DoChangeLevel()
		{
			byte b = mLevelToChanged;
			ProfileManager.Current.CurrentLevel = b;
			if (ProfileManager.Current.UnlockedLevel < b)
			{
				ProfileManager.Current.UnlockedLevel = b;
			}
			Director.FadeOutIn(1.2f, 0.5f);
			mState = State.ChangeWorld;
			mFadeTime = 1.1f;
			Player.Instance.ControlEnabled = false;
			BGMManager.FadeOff();
		}

		public void CompleteLevel(bool dramaSound)
		{
			BGMManager.TurnOff();
			mState = State.Score;
			Player.Instance.ChangeToUnStuckStage();
			Player.Instance.ControlEnabled = false;
			ScoreUI.Instance.OnFinish = OnScoreFinish;
			ScoreUI.Instance.ShowScore(dramaSound);
			if (!dramaSound)
			{
				SFXManager.BGMVolume = BGMManager.Volume;
				SFXManager.PlaySound("win");
			}
		}

		public void OnScoreFinish()
		{
			if (ProfileManager.IsTrialMode)
			{
				Instance.Abort(AbortReason.EndTrial);
			}
			else
			{
				int num = World.CurrentLevel + 1;
				if (num > 7)
				{
					num = 1;
				}
				ProfileManager.Current.CurrentLevel = (byte)num;
				if (ProfileManager.Current.UnlockedLevel < num)
				{
					ProfileManager.Current.UnlockedLevel = (byte)num;
				}
				ProfileManager.BeginSave(null, 1, false);
				if (Pref.Steamworks)
				{
					int experience = ProfileManager.Current.Experience;
					SteamLeaderboardManager.Instance.WriteScore(ProfileManager.Current.PlayerType, experience);
				}
				if (World.CurrentLevel == 4)
				{
					ProfileManager.Current.SetHasShownCutscene(5, true);
					ScreenManager.NextScreen = VideoScreen.Instance;
					VideoScreen.Instance.NextScreen = Instance;
					VideoScreen.Instance.VideoName = "Content/Video/CARSON";
					VideoScreen.Instance.AudioName = null;
					ProfileManager.Current.CurrentLevel = (byte)num;
					ProfileManager.BeginSave(null, 1, false);
					mState = State.AbortForVideo;
					Director.FadeOutIn(1.2f, 0.5f);
					mFadeTime = 1.1f;
					Player.Instance.ControlEnabled = false;
					BGMManager.FadeOff();
				}
				else if (World.CurrentLevel == 7)
				{
					ScreenManager.NextScreen = VideoScreen.Instance;
					VideoScreen.Instance.NextScreen = CreditScreen.Instance;
					CreditScreen.Instance.NextScreen = MenuScreen.Instance;
					if (ProfileManager.Current.PlayerType == PlayerType.Mars)
					{
						VideoScreen.Instance.VideoName = "Content/Video/END_Ares";
						VideoScreen.Instance.AudioName = "end_ares";
					}
					else
					{
						VideoScreen.Instance.VideoName = "Content/Video/END_Tarus";
						VideoScreen.Instance.AudioName = "end_tarus";
					}
					mState = State.AbortForVideo;
					Director.FadeOutIn(1.2f, 0.5f);
					mFadeTime = 1.1f;
					Player.Instance.ControlEnabled = false;
					BGMManager.FadeOff();
				}
				else
				{
					ChangeLevel((byte)num);
				}
			}
		}

		public void LoadCheckPoint()
		{
			Player.Instance.Invincible = true;
			FinishingButtonUI.Instance.Hide();
			Director.FadeOut(1f);
			BGMManager.FadeOff();
			mState = State.ResetWorld;
			mFadeTime = 2.5f;
		}

		private void ChangeStateToSaveBeforeAbort()
		{
			BGMManager.FadeOff();
			mState = State.SaveBeforeAbort;
			mSaveBeforeAbortStep = 0;
		}

		public void Abort(AbortReason reason)
		{
			EnemyManager.IsActive = false;
			mFadeTime = 1.1f;
			mCurrentAbortReason = reason;
			ChangeStateToSaveBeforeAbort();
		}

		private void LoadMainMenu()
		{
			LoadNextScreen(MenuScreen.Instance.LoadMainMenu);
			ScreenManager.NextScreen = MenuScreen.Instance;
			LoadingUI.Instance.Show(true, 1);
		}

		private void LoadVideo()
		{
			LoadNextScreen(VideoScreen.Instance.Load);
			ScreenManager.NextScreen = VideoScreen.Instance;
			LoadingUI.Instance.Show(true, 1);
		}

		private void LoadStartScreen()
		{
			StartScreen instance = StartScreen.Instance;
			LoadNextScreen(((Screen)instance).Load);
			ScreenManager.NextScreen = StartScreen.Instance;
			LoadingUI.Instance.Show(true, 1);
		}

		private void LoadUnlockFullVersionScreen()
		{
			AdvertiseScreen instance = AdvertiseScreen.Instance;
			LoadNextScreen(((Screen)instance).Load);
			ScreenManager.NextScreen = AdvertiseScreen.Instance;
			LoadingUI.Instance.Show(true, 1);
			if (mCurrentAbortReason == AbortReason.ExitTrial)
			{
				AdvertiseScreen.Instance.NextScreenWhenBack = null;
			}
			else
			{
				AdvertiseScreen.Instance.NextScreenWhenBack = MenuScreen.Instance;
			}
		}

		public override void Update()
		{
			switch (mState)
			{
			case State.Score:
				break;
			case State.Load:
				mState = State.Play;
				break;
			case State.ChangeWorld:
				if (mFadeTime > 0f)
				{
					mFadeTime -= TimeManager.SecondDifference;
					if (mFadeTime <= 0f)
					{
						base.LoadingDone = false;
						ProcessManager.ClearProcess();
						ARESPostProcessingManager.Deactivate();
						ProcessManager.ApplyAddRemoveAction();
						SFXManager.StopLoopSound();
						World.Destroy();
						World.CurrentLevel = ProfileManager.Current.CurrentLevel;
						LoadingUI.Instance.Show(true, 1);
						LoadNextScreen(LoadWorld);
					}
				}
				else if (base.LoadingDone)
				{
					Director.StopAll();
					Initialize();
					ControlHint.Instance.HideHints();
					mState = State.Load;
					Director.FadeOutIn(0.4f, 0.5f);
					Director.WaitForSeconds(0.5f, LoadingUI.Instance.Hide, false);
				}
				break;
			case State.ResetWorld:
				if (mFadeTime > 0f)
				{
					mFadeTime -= TimeManager.SecondDifference;
					if (mFadeTime <= 0f)
					{
						Director.StopAll();
						ARESPostProcessingManager.Deactivate();
						EnemyManager.CancelAmbush();
						ProcessManager.ClearProcess();
						World.UnloadAllRoom();
						ProcessManager.ApplyAddRemoveAction();
						Initialize();
						mState = State.Load;
					}
				}
				break;
			case State.SaveBeforeAbort:
				if (mSaveBeforeAbortStep == 0)
				{
					if (mCurrentAbortReason == AbortReason.EndTrial || mCurrentAbortReason == AbortReason.ExitTrial)
					{
						mState = State.AbortByEndTrial;
					}
					else if (mCurrentAbortReason == AbortReason.GamerSignedOut)
					{
						mState = State.AbortBySignedOut;
					}
					else
					{
						mState = State.Abort;
					}
				}
				else if (mSaveBeforeAbortStep == 1)
				{
					break;
				}
				break;
			case State.ManageIfSaveFail:
				mState = State.Abort;
				break;
			case State.Abort:
			case State.AbortBySignedOut:
			case State.AbortByEndTrial:
			case State.AbortForVideo:
				if (mFadeTime > 0f)
				{
					mFadeTime -= TimeManager.SecondDifference;
					if (mFadeTime <= 0f)
					{
						Director.StopAll();
						ProcessManager.ClearProcess();
						ARESPostProcessingManager.Deactivate();
						SFXManager.StopLoopSound();
						World.Destroy();
						ProcessManager.ApplyAddRemoveAction();
						ProcessManager.Resume();
						Director.FadeIn(0.5f);
						mAbortFadeOutTime = -1f;
						if (mState == State.Abort)
						{
							LoadMainMenu();
						}
						else if (mState == State.AbortByEndTrial)
						{
							if (ProfileManager.IsTrialMode)
							{
								LoadUnlockFullVersionScreen();
							}
							else
							{
								LoadStartScreen();
							}
						}
						else if (mState == State.AbortBySignedOut)
						{
							LoadStartScreen();
						}
						else if (mState == State.AbortForVideo)
						{
							LoadVideo();
							World.CurrentLevel = ProfileManager.Current.CurrentLevel;
						}
					}
				}
				else if (mAbortFadeOutTime <= -1f)
				{
					bool flag = false;
					if (mState == State.Abort)
					{
						if (MenuScreen.Instance.LoadingDone)
						{
							flag = true;
						}
					}
					else if (mState == State.AbortBySignedOut)
					{
						if (StartScreen.Instance.LoadingDone)
						{
							GamePad.Enabled = true;
							flag = true;
						}
					}
					else if (mState == State.AbortByEndTrial)
					{
						if (ProfileManager.IsTrialMode)
						{
							if (AdvertiseScreen.Instance.LoadingDone)
							{
								flag = true;
							}
						}
						else if (StartScreen.Instance.LoadingDone)
						{
							flag = true;
						}
					}
					else if (mState == State.AbortForVideo && VideoScreen.Instance.LoadingDone)
					{
						flag = true;
					}
					if (flag)
					{
						LoadingUI.Instance.Hide();
						mAbortFadeOutTime = 0.5f;
						Director.FadeOut(mAbortFadeOutTime);
					}
				}
				else if (mAbortFadeOutTime > 0f)
				{
					mAbortFadeOutTime -= TimeManager.SecondDifference;
					if (mAbortFadeOutTime <= 0f)
					{
						base.ActivityFinished = true;
					}
				}
				break;
			case State.Play:
			{
				ProfileManager.Current.PlayTime += TimeManager.SecondDifference;
				if (!ProcessManager.Paused && Player.Instance.HP > 0)
				{
					MouseUI.SetCursor(2);
					if (GamePad.GetKeyDown(262144))
					{
						MouseUI.SetCursor(1);
						ProcessManager.Pause(PauseMenuUI.Instance.Show);
						break;
					}
					if (GamePad.GetKeyDown(131072))
					{
						MouseUI.SetCursor(1);
						ProcessManager.Pause(GameUI.Instance.Show);
						break;
					}
					if (World.Camera.Mode != 0 && Player.Instance.ControlEnabled)
					{
						World.LevelPlayTime += TimeManager.SecondDifference;
					}
				}
				byte currentLevel = World.CurrentLevel;
				if (currentLevel == 2 && Player.Instance.Position.X > 3796f)
				{
					CompleteLevel(false);
				}
				break;
			}
			}
		}

		private void SaveBeforeAbortFinish(IAsyncResult ar)
		{
			if (ar.IsCompleted)
			{
				mState = State.Abort;
			}
			else
			{
				if (StorageDeviceManager.SelectedStorageDevice == null || !StorageDeviceManager.SelectedStorageDevice.IsConnected)
				{
					GuideMessageBoxWrapper.Instance.Show("无法保存", "选择的存储设备无法使用。\n请选择其它存储设备或在不存档的情况下继续游戏。", PopUpUIAskForStorageUnavailable, 2, "选择存档", "继续", string.Empty);
				}
				mState = State.ManageIfSaveFail;
			}
		}

		public override void Destroy()
		{
			base.LoadingDone = false;
			ScoreUI.Instance.Unload();
		}

		public override void OnCurrentGamerSignedOut()
		{
			GamePad.Enabled = false;
			Abort(AbortReason.GamerSignedOut);
		}

		private void PopUpUIAskForStorageUnavailable(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				SaveFailStorageMissing_SelectStorageDevice();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Cancel:
				SaveFailStorageMissing_ContinueWithoutSaving();
				break;
			}
		}

		private void SaveFailStorageMissing_SelectStorageDevice()
		{
			StorageDeviceManager.PromptUserToSelectStorageDevice(FinishSelectStorageDevice);
		}

		private void FinishSelectStorageDevice(FinishSelectStorageDeviceEventArgument arg)
		{
			if (arg.SelectionType == StorageDeviceManager.SelectionType.Auto)
			{
				GuideMessageBoxWrapper.Instance.Show("确认选择", "只有一台存储设备能用。\n请确认你的选择，你可以在不存档的情况下继续游戏或选择其它存储设备。", PopUpUIAskForAutoSelectStorageDevice, 3, "确认", "继续", "选择其它存储设备");
			}
			else
			{
				CheckIsThereExistingSave();
			}
		}

		private void PopUpUIAskForAutoSelectStorageDevice(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				CheckIsThereExistingSave();
				break;
			case PopUpUI.PopUpUIResult.Cancel:
				SaveFailStorageMissing_ContinueWithoutSaving();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Other:
				SaveFailStorageMissing_SelectStorageDevice();
				break;
			}
		}

		private void CheckIsThereExistingSave()
		{
			if (StorageDeviceManager.SelectedStorageDevice != null && StorageDeviceManager.SelectedStorageDevice.IsConnected)
			{
				if (StorageDeviceManager.IsDirectoryForGamerExist())
				{
					GuideMessageBoxWrapper.Instance.Show("覆盖存档", "在当前设备中发现了存档文件。\n是否要覆盖此存档吗？", PopUpUIAskForOverwriteSave, 2, "覆盖", "选择其它存储设备", string.Empty);
				}
				else
				{
					ChangeStateToSaveBeforeAbort();
				}
			}
			else
			{
				ChangeStateToSaveBeforeAbort();
			}
		}

		private void PopUpUIAskForOverwriteSave(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				ChangeStateToSaveBeforeAbort();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Cancel:
				StorageDeviceManager.Reset();
				StorageDeviceManager.Initialize();
				StorageDeviceManager.PromptUserToSelectStorageDevice(FinishSelectStorageDevice);
				break;
			}
		}

		private void SaveFailStorageMissing_ContinueWithoutSaving()
		{
			if (mCurrentAbortReason == AbortReason.Normal)
			{
				mState = State.Abort;
			}
			else if (mCurrentAbortReason == AbortReason.GamerSignedOut)
			{
				mState = State.AbortBySignedOut;
			}
			else if (mCurrentAbortReason == AbortReason.EndTrial)
			{
				mState = State.AbortByEndTrial;
			}
		}
	}
}
