using ARES360.Audio;
using ARES360.Input;
using ARES360.Profile;
using ARES360.Screen;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;

namespace ARES360.UI
{
	public class PauseMenuUI : PositionedObject, IProcessable
	{
		public enum Page
		{
			MAIN,
			RESUME,
			ABORT_MISSION,
			LEADERBOARD,
			ACHIEVEMENT,
			HELP_AND_OPTION,
			EXIT
		}

		public class ClickableMenuItem : ClickablePositionedObject
		{
			public Sprite sprite;

			public Text text;

			public int index;
		}

		private const float PAN_SPEED = 400f;

		private const float DISTANCE_FROM_CAMERA = -70f;

		private const float MENU_ITEM_VERTICAL_SPACE = 4.26f;

		private const int MENU_RESUME = 0;

		private const int MENU_ABORT_MISSION = 1;

		private const int MENU_LEADERBOARD = 2;

		private const int MENU_ACHIEVEMENT = 3;

		private const int MENU_HELP_AND_OPTION = 4;

		private const int MENU_EXIT = 5;

		private bool mConfirmExit;

		private Page mPage;

		private Page mRequestNextPage;

		private bool mIsJustChangePage;

		private bool mIsTransitionBetweenPage;

		private bool mIsTransitionEnd;

		private PositionedObject mOtherPageRootObject;

		private Sprite mMainPanel;

		private List<ClickableMenuItem> mMenuItems;

		private int mMenuItemCount;

		private Texture2D mMenuItemTextureNormal;

		private Texture2D mMenuItemTextureHighlight;

		private int mMenuItemCursor;

		private Sprite mInformationBackground;

		private Text mChapterNameText;

		private Text mDifficultyText;

		private Text mTotalScoreText;

		private Text mGamerTagText;

		private int mExitStep;

		private float mDelayBeforeExit;

		private static PauseMenuUI mInstance;

		public static PauseMenuUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new PauseMenuUI();
				}
				return mInstance;
			}
		}

		public bool IsAdding
		{
			get;
			set;
		}

		public bool IsRemoving
		{
			get;
			set;
		}

		public bool IsRunning
		{
			get;
			set;
		}

		private PauseMenuUI()
		{
			mMenuItemCursor = 0;
			mMenuItems = new List<ClickableMenuItem>();
			mPage = Page.MAIN;
		}

		private void AddMenuItem(int key, string text)
		{
			int index = mMenuItemCount;
			Sprite sprite = new Sprite();
			sprite = new Sprite();
			sprite.Texture = mMenuItemTextureNormal;
			sprite.ScaleX = 14.5f;
			sprite.ScaleY = 3.75f;
			sprite.AttachTo(mMainPanel, false);
			Text text2 = new Text(GUIHelper.CaptionFont);
			text2.HorizontalAlignment = HorizontalAlignment.Left;
			text2.ColorOperation = ColorOperation.Texture;
			text2.Scale = 1f;
			text2.Spacing = 1f;
			text2.DisplayText = text;
			text2.RelativePosition = new Vector3(-10.8f, 0f, 0.1f);
			text2.AttachTo(sprite, false);
			mMenuItems.Add(new ClickableMenuItem
			{
				tag = key,
				sprite = sprite,
				positionedObject = sprite,
				text = text2,
				index = index,
				boundary = new Vector4(-7.25f, -1.8f, 7.25f, 1.8f),
				OnMouseEnter = new Clickable.ClickableHoverHandler(OnMouseEnterMenuItem),
				OnMouseClick = new Clickable.ClickableHandler(OnMouseClickMenuItem)
			});
			mMenuItemCount++;
		}

		private void OnMouseEnterMenuItem(Clickable sender)
		{
			ClickableMenuItem clickableMenuItem = sender as ClickableMenuItem;
			SetMenuCursor(clickableMenuItem.index);
			SFXManager.PlaySound("cursor");
		}

		private bool OnMouseClickMenuItem(Clickable sender)
		{
			ClickableMenuItem clickableMenuItem = sender as ClickableMenuItem;
			if (mMenuItemCursor != clickableMenuItem.index)
			{
				SetMenuCursor(clickableMenuItem.index);
				SFXManager.PlaySound("cursor");
				return true;
			}
			ExecuteSelectedMenuItem();
			return true;
		}

		public void Load()
		{
			mMenuItemTextureHighlight = FlatRedBallServices.Load<Texture2D>("Content/UI/pause_option_item_highlight", "Global");
			mMenuItemTextureNormal = FlatRedBallServices.Load<Texture2D>("Content/UI/pause_option_item_normal", "Global");
			mMainPanel = new Sprite();
			mMainPanel.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/pause_option_panel"), "Global");
			mMainPanel.ScaleX = 24.125f;
			mMainPanel.ScaleY = 19f;
			mMainPanel.RelativePosition = new Vector3(-25f, 0f, 0f);
			mMainPanel.AttachTo(this, false);
			mMenuItems.Clear();
			mMenuItemCount = 0;
			AddMenuItem(0, "继续游戏");
			AddMenuItem(1, "中止任务");
			if (Pref.Steamworks)
			{
				AddMenuItem(2, "排行榜");
			}
			AddMenuItem(3, "成就");
			AddMenuItem(4, "帮助和选项");
			AddMenuItem(5, "退出游戏");
			mMenuItems[mMenuItemCursor].sprite.Texture = mMenuItemTextureHighlight;
			mInformationBackground = new Sprite();
			mInformationBackground.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/pause_option_detail_bg"), "Global");
			mInformationBackground.RelativePosition = new Vector3(30f, 5f, 0f);
			mInformationBackground.ScaleX = 24.6875f;
			mInformationBackground.ScaleY = 24.78125f;
			mInformationBackground.AttachTo(this, false);
			Text[] array = new Text[4];
			for (int i = 0; i < 4; i++)
			{
				array[i] = new Text(GUIHelper.CaptionFont);
				array[i].HorizontalAlignment = HorizontalAlignment.Right;
				array[i].VerticalAlignment = VerticalAlignment.Center;
				array[i].ColorOperation = ColorOperation.Texture;
				array[i].Scale = 0.8f;
				array[i].Spacing = 0.8f;
				array[i].DisplayText = string.Empty;
				array[i].AttachTo(this, false);
			}
			mChapterNameText = array[0];
			mDifficultyText = array[1];
			mTotalScoreText = array[2];
			mGamerTagText = array[3];
			array = null;
			mChapterNameText.Scale = 1.2f;
			mChapterNameText.Spacing = 1.2f;
			mChapterNameText.HorizontalAlignment = HorizontalAlignment.Left;
			mChapterNameText.RelativePosition = new Vector3(18.75f, 7.25f, 0f);
			mDifficultyText.RelativePosition = new Vector3(40.25f, -7.5f, 0.1f);
			mTotalScoreText.RelativePosition = new Vector3(40.25f, -12.2f, 0.1f);
			mGamerTagText.RelativePosition = new Vector3(40.25f, -16.25f, 0.1f);
		}

		private void MoveMenuCursor(sbyte direction)
		{
			int num = 0;
			switch (direction)
			{
			case -2:
			case 1:
				num = 1;
				break;
			case -1:
			case 2:
				num = -1;
				break;
			}
			int num2 = mMenuItemCursor + num;
			if (num2 >= 0 && num2 < mMenuItemCount)
			{
				mMenuItems[mMenuItemCursor].sprite.Texture = mMenuItemTextureNormal;
				mMenuItemCursor = num2;
				mMenuItems[mMenuItemCursor].sprite.Texture = mMenuItemTextureHighlight;
			}
		}

		private void SetMenuCursor(int index)
		{
			if (index >= 0 && index < mMenuItemCount)
			{
				mMenuItems[mMenuItemCursor].sprite.Texture = mMenuItemTextureNormal;
				mMenuItemCursor = index;
				mMenuItems[mMenuItemCursor].sprite.Texture = mMenuItemTextureHighlight;
			}
		}

		private void ResetMenuItemCursorTo(int n)
		{
			if (n >= 0 && n < mMenuItemCount)
			{
				for (int i = 0; i < mMenuItemCount; i++)
				{
					if (mMenuItems[i].sprite.Texture == mMenuItemTextureHighlight)
					{
						mMenuItems[i].sprite.Texture = mMenuItemTextureNormal;
					}
				}
				mMenuItems[n].sprite.Texture = mMenuItemTextureHighlight;
			}
			mMenuItemCursor = n;
		}

		public void Show()
		{
			MouseUI.SetCursor(1);
			RelativePosition = new Vector3(0f, 0f, -70f);
			SFXManager.MuteGameVolumes();
			SFXManager.PlaySound("open_recycle_menu");
			ProcessManager.AddProcess(this);
		}

		public void Hide()
		{
			if (World.LowestCombatDifficulty > ProfileManager.CombatDifficulty)
			{
				World.LowestCombatDifficulty = ProfileManager.CombatDifficulty;
			}
			if (World.LowestBossCombatDifficulty > ProfileManager.CombatDifficulty)
			{
				World.LowestBossCombatDifficulty = ProfileManager.CombatDifficulty;
			}
			SFXManager.UnmuteGameVolumes();
			ControlHint.Instance.HideHints();
			ProcessManager.RemoveProcess(this);
			ProcessManager.Resume();
		}

		public void OnRegister()
		{
			mChapterNameText.DisplayText = GUIHelper.LevelInfo[World.CurrentLevel].ShortName;
			mTotalScoreText.DisplayText = ProfileManager.Current.Experience.ToString();
			if (Pref.Steamworks)
			{
				mGamerTagText.DisplayText = SteamFriends.GetPersonaName();
			}
			else
			{
				mGamerTagText.DisplayText = "Extend Studio";
			}
			switch (ProfileManager.Current.CombatDifficulty)
			{
			case GameCombatDifficulty.Casual:
				mDifficultyText.DisplayText = Pref.COMBAT_DIFFICULTY_TEXT_CASUAL;
				break;
			case GameCombatDifficulty.Normal:
				mDifficultyText.DisplayText = Pref.COMBAT_DIFFICULTY_TEXT_NORMAL;
				break;
			case GameCombatDifficulty.Hardcore:
				mDifficultyText.DisplayText = Pref.COMBAT_DIFFICULTY_TEXT_HARDCORE;
				break;
			}
			SpriteManager.AddToLayer(GUIHelper.UIMask, GUIHelper.UILayer);
			SpriteManager.AddPositionedObject(this);
			AttachTo(World.Camera, false);
			SpriteManager.AddToLayer(mMainPanel, GUIHelper.UILayer);
			for (int i = 0; i < mMenuItemCount; i++)
			{
				ClickableMenuItem clickableMenuItem = mMenuItems[i];
				SpriteManager.AddToLayer(clickableMenuItem.sprite, GUIHelper.UILayer);
				TextManager.AddToLayer(clickableMenuItem.text, GUIHelper.UILayer);
			}
			SpriteManager.AddToLayer(mInformationBackground, GUIHelper.UILayer);
			TextManager.AddToLayer(mChapterNameText, GUIHelper.UILayer);
			TextManager.AddToLayer(mDifficultyText, GUIHelper.UILayer);
			TextManager.AddToLayer(mTotalScoreText, GUIHelper.UILayer);
			TextManager.AddToLayer(mGamerTagText, GUIHelper.UILayer);
			mMenuItems[0].sprite.RelativePosition = new Vector3(-8.8f, 7.3f, 0f);
			for (int j = 1; j < mMenuItemCount; j++)
			{
				Sprite sprite = mMenuItems[j].sprite;
				sprite.RelativePosition = mMenuItems[j - 1].sprite.RelativePosition;
				sprite.RelativePosition.Y -= 4.26f;
			}
			mPage = Page.MAIN;
			ResetMenuItemCursorTo(0);
			RelativeVelocity = Vector3.Zero;
			ManageControlHintForMainPage();
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		public void OnRemove()
		{
			ControlHint.Instance.HideHints();
			SpriteManager.RemoveSpriteOneWay(GUIHelper.UIMask);
			Detach();
			SpriteManager.RemovePositionedObject(this);
			SpriteManager.RemoveSpriteOneWay(mMainPanel);
			for (int i = 0; i < mMenuItemCount; i++)
			{
				ClickableMenuItem clickableMenuItem = mMenuItems[i];
				SpriteManager.RemoveSpriteOneWay(clickableMenuItem.sprite);
				TextManager.RemoveTextOneWay(clickableMenuItem.text);
			}
			SpriteManager.RemoveSpriteOneWay(mInformationBackground);
			TextManager.RemoveTextOneWay(mChapterNameText);
			TextManager.RemoveTextOneWay(mDifficultyText);
			TextManager.RemoveTextOneWay(mTotalScoreText);
			TextManager.RemoveTextOneWay(mGamerTagText);
		}

		private void ChangePage(Page nextPage)
		{
			if (mPage != nextPage)
			{
				mRequestNextPage = nextPage;
				mIsTransitionBetweenPage = true;
				mIsTransitionEnd = false;
				mIsJustChangePage = true;
			}
		}

		private void ManageControlHintForMainPage()
		{
			ControlHint.Instance.Clear().AddHint(524288, "选择").ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
		}

		private void UpdatePageTransition(Page currentPage, Page nextPage)
		{
			switch (currentPage)
			{
			case Page.MAIN:
				if (nextPage == Page.LEADERBOARD)
				{
					if (mIsJustChangePage)
					{
						RelativeVelocity.X = -400f;
						LeaderboardUI.Instance.RelativePosition = new Vector3(100f, 0f, -70f);
						LeaderboardUI.Instance.RelativeVelocity.X = -400f;
						LeaderboardUI.Instance.IsActivated = false;
						LeaderboardUI.Instance.IsRemoveProcessWhenPressBack = false;
						LeaderboardUI.Instance.AttachTo(World.Camera, false);
						LeaderboardUI.Instance.Show();
					}
					if (LeaderboardUI.Instance.RelativePosition.X <= 0f)
					{
						LeaderboardUI.Instance.RelativeVelocity = Vector3.Zero;
						LeaderboardUI.Instance.IsActivated = true;
						LeaderboardUI.Instance.RelativePosition = new Vector3(0f, 0f, -70f);
						LeaderboardUI.Instance.OnBackButtonPressed = LeaderboardUIDidBackButtonPressed;
						mIsTransitionEnd = true;
					}
				}
				switch (nextPage)
				{
				case Page.ACHIEVEMENT:
					if (mIsJustChangePage)
					{
						RelativeVelocity.X = -400f;
						AchievementUI.Instance.RelativePosition = new Vector3(100f, 0f, -70f);
						AchievementUI.Instance.RelativeVelocity.X = -400f;
						AchievementUI.Instance.IsActivated = false;
						AchievementUI.Instance.IsRemoveProcessWhenPressBack = false;
						AchievementUI.Instance.AttachTo(World.Camera, false);
						AchievementUI.Instance.Show();
					}
					if (AchievementUI.Instance.RelativePosition.X <= 0f)
					{
						AchievementUI.Instance.RelativeVelocity = Vector3.Zero;
						AchievementUI.Instance.IsActivated = true;
						AchievementUI.Instance.RelativePosition = new Vector3(0f, 0f, -70f);
						AchievementUI.Instance.OnBackButtonPressed = AchievementUIDidBackButtonPressed;
						mIsTransitionEnd = true;
					}
					break;
				case Page.HELP_AND_OPTION:
					if (mIsJustChangePage)
					{
						RelativeVelocity.X = -400f;
						HelpOptionUI.Instance.RelativePosition = new Vector3(100f, 0f, -70f);
						HelpOptionUI.Instance.RelativeVelocity.X = -400f;
						HelpOptionUI.Instance.IsActivated = false;
						HelpOptionUI.Instance.IsImmediatelyRemoveProcessWhenExit = false;
						HelpOptionUI.Instance.AttachTo(World.Camera, false);
						HelpOptionUI.Instance.Show();
					}
					if (HelpOptionUI.Instance.RelativePosition.X <= 0f)
					{
						HelpOptionUI.Instance.RelativeVelocity = Vector3.Zero;
						HelpOptionUI.Instance.IsActivated = true;
						HelpOptionUI.Instance.RelativePosition = new Vector3(0f, 0f, -70f);
						HelpOptionUI.Instance.OnBackButtonPressed = HelpOptionUIDidBackButtonPressed;
						mIsTransitionEnd = true;
					}
					break;
				case Page.ABORT_MISSION:
					mIsTransitionEnd = true;
					break;
				case Page.EXIT:
					mExitStep = 0;
					mIsTransitionEnd = true;
					break;
				}
				break;
			case Page.LEADERBOARD:
				if (nextPage == Page.MAIN)
				{
					if (mIsJustChangePage)
					{
						LeaderboardUI.Instance.IsActivated = false;
						LeaderboardUI.Instance.RelativeVelocity.X = 400f;
						LeaderboardUI.Instance.OnBackButtonPressed = null;
						RelativePosition = new Vector3(-100f, 0f, -70f);
						RelativeVelocity.X = 400f;
					}
					if (RelativePosition.X >= 0f)
					{
						LeaderboardUI.Instance.Detach();
						LeaderboardUI.Instance.Hide();
						RelativeVelocity = Vector3.Zero;
						RelativePosition = new Vector3(0f, 0f, -70f);
						mIsTransitionEnd = true;
					}
				}
				break;
			case Page.ACHIEVEMENT:
				if (nextPage == Page.MAIN)
				{
					if (mIsJustChangePage)
					{
						AchievementUI.Instance.IsActivated = false;
						AchievementUI.Instance.RelativeVelocity.X = 400f;
						AchievementUI.Instance.OnBackButtonPressed = null;
						RelativePosition = new Vector3(-100f, 0f, -70f);
						RelativeVelocity.X = 400f;
					}
					if (RelativePosition.X >= 0f)
					{
						AchievementUI.Instance.Detach();
						AchievementUI.Instance.Hide();
						RelativeVelocity = Vector3.Zero;
						RelativePosition = new Vector3(0f, 0f, -70f);
						mIsTransitionEnd = true;
					}
				}
				break;
			case Page.HELP_AND_OPTION:
				if (nextPage == Page.MAIN)
				{
					if (mIsJustChangePage)
					{
						HelpOptionUI.Instance.IsActivated = false;
						HelpOptionUI.Instance.RelativeVelocity.X = 400f;
						HelpOptionUI.Instance.OnBackButtonPressed = null;
						RelativePosition = new Vector3(-100f, 0f, -70f);
						RelativeVelocity.X = 400f;
					}
					if (RelativePosition.X >= 0f)
					{
						HelpOptionUI.Instance.RelativeVelocity.X = 0f;
						HelpOptionUI.Instance.Detach();
						HelpOptionUI.Instance.Hide();
						RelativeVelocity = Vector3.Zero;
						RelativePosition = new Vector3(0f, 0f, -70f);
						mIsTransitionEnd = true;
					}
				}
				break;
			default:
				mIsTransitionEnd = true;
				break;
			}
			if (nextPage == Page.MAIN)
			{
				ManageControlHintForMainPage();
			}
		}

		private void LeaderboardUIDidBackButtonPressed()
		{
			ChangePage(Page.MAIN);
		}

		private void AchievementUIDidBackButtonPressed()
		{
			ChangePage(Page.MAIN);
		}

		private void HelpOptionUIDidBackButtonPressed()
		{
			ChangePage(Page.MAIN);
		}

		private void ExecuteSelectedMenuItem()
		{
			switch (mMenuItems[mMenuItemCursor].tag)
			{
			case 0:
				Hide();
				break;
			case 1:
				SFXManager.PlaySound("ok");
				mConfirmExit = false;
				PopUpUI.Instance.Show("退出游戏", "是否要退出游戏？\n当前进度将会丢失。", PopUpUIAskForAbortMission);
				ChangePage(Page.ABORT_MISSION);
				break;
			case 2:
				SFXManager.PlaySound("ok");
				if (Pref.Steamworks)
				{
					ChangePage(Page.LEADERBOARD);
				}
				else
				{
					GuideMessageBoxWrapper.Instance.Show("警告", "你必须登陆Steam才能查看排行榜。", null, 1, "知道", string.Empty, string.Empty);
				}
				break;
			case 3:
				SFXManager.PlaySound("ok");
				ChangePage(Page.ACHIEVEMENT);
				break;
			case 4:
				SFXManager.PlaySound("ok");
				ChangePage(Page.HELP_AND_OPTION);
				break;
			case 5:
				mConfirmExit = false;
				SFXManager.PlaySound("ok");
				PopUpUI.Instance.Show("退出游戏", "是否要退出游戏？\n当前进度将会丢失。", PopUpUIAskForAbortMission);
				ChangePage(Page.EXIT);
				break;
			}
		}

		public void Update()
		{
			if (!mIsTransitionBetweenPage)
			{
				switch (mPage)
				{
				case Page.MAIN:
					if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
					{
						MoveMenuCursor(-2);
						SFXManager.PlaySound("cursor");
					}
					else if (GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
					{
						MoveMenuCursor(2);
						SFXManager.PlaySound("cursor");
					}
					else if (GamePad.GetMenuKeyDown(524288))
					{
						ExecuteSelectedMenuItem();
					}
					else if (GamePad.GetMenuKeyDown(1048576) || GamePad.GetMenuKeyDown(262144))
					{
						Hide();
					}
					else
					{
						GamePad.UpdateClickables(mMenuItems);
					}
					break;
				case Page.ABORT_MISSION:
					if (PopUpUI.Instance.IsRemoving)
					{
						if (mConfirmExit)
						{
							SFXManager.PlaySound("ok");
							Hide();
							PlayScreen.Instance.Abort(PlayScreen.AbortReason.Normal);
						}
						else
						{
							SFXManager.PlaySound("New_MenuBack");
							ChangePage(Page.MAIN);
						}
					}
					break;
				case Page.EXIT:
					if (mExitStep == 0)
					{
						if (PopUpUI.Instance.IsRemoving)
						{
							if (mConfirmExit)
							{
								SFXManager.PlaySound("ok");
								mExitStep = 1;
							}
							else
							{
								SFXManager.PlaySound("New_MenuBack");
								ChangePage(Page.MAIN);
							}
						}
					}
					else if (mExitStep == 1)
					{
						if (!ProfileManager.IsTrialMode)
						{
							mExitStep = 2;
							ProfileManager.BeginSave(SaveBeforeExitFinish, 7, true);
						}
					}
					else if (mExitStep != 2 && mExitStep == 3 && mDelayBeforeExit > 0f)
					{
						mDelayBeforeExit -= TimeManager.SecondDifference;
						if (mDelayBeforeExit <= 0f)
						{
							Hide();
							FlatRedBallServices.Game.Exit();
						}
					}
					break;
				}
			}
			else
			{
				UpdatePageTransition(mPage, mRequestNextPage);
				if (mIsTransitionEnd)
				{
					mPage = mRequestNextPage;
					mIsTransitionBetweenPage = false;
				}
				mIsJustChangePage = false;
			}
		}

		public void PausedUpdate()
		{
			Update();
		}

		private void PopUpUIAskForAbortMission(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				mConfirmExit = true;
				break;
			case PopUpUI.PopUpUIResult.Cancel:
				mConfirmExit = false;
				break;
			}
		}

		private void SaveBeforeExitFinish(IAsyncResult ar)
		{
			if (ar.IsCompleted)
			{
				mExitStep = 3;
				mDelayBeforeExit = 1f;
			}
			else if (StorageDeviceManager.SelectedStorageDevice == null || !StorageDeviceManager.SelectedStorageDevice.IsConnected)
			{
				GuideMessageBoxWrapper.Instance.Show("无法存档", "当前的存储设备无法使用。\n请选择其它存储设备或在不存档的情况下继续游戏。", PopUpUIAskForStorageUnavailable, 3, "选择设备", "继续", "返回");
			}
		}

		private void PopUpUIAskForStorageUnavailable(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				SaveFailStorageMissing_SelectStorageDevice();
				break;
			case PopUpUI.PopUpUIResult.Cancel:
				Hide();
				FlatRedBallServices.Game.Exit();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Other:
				ChangePage(Page.MAIN);
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
				GuideMessageBoxWrapper.Instance.Show("确认选择", "只有一台存储设备能用。\n请确认你的选择或在不存档的情况下继续游戏。", PopUpUIAskForAutoSelectStorageDevice, 3, "确认", "继续", "返回");
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
				FlatRedBallServices.Game.Exit();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Other:
				StorageDeviceManager.Reset();
				StorageDeviceManager.Initialize();
				ChangePage(Page.MAIN);
				break;
			}
		}

		private void CheckIsThereExistingSave()
		{
			if (StorageDeviceManager.SelectedStorageDevice != null && StorageDeviceManager.SelectedStorageDevice.IsConnected)
			{
				if (StorageDeviceManager.IsDirectoryForGamerExist())
				{
					GuideMessageBoxWrapper.Instance.Show("覆盖存档", "在当前设备中发现了存档文件。\n是否要覆盖此存档？", PopUpUIAskForOverwriteSave, 2, "覆盖", "选择其它存储设备", string.Empty);
				}
				else
				{
					mExitStep = 1;
				}
			}
			else
			{
				mExitStep = 1;
			}
		}

		private void PopUpUIAskForOverwriteSave(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				mExitStep = 1;
				break;
			case PopUpUI.PopUpUIResult.Cancel:
				StorageDeviceManager.Reset();
				StorageDeviceManager.Initialize();
				StorageDeviceManager.PromptUserToSelectStorageDevice(FinishSelectStorageDevice);
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Other:
				StorageDeviceManager.Reset();
				StorageDeviceManager.Initialize();
				ChangePage(Page.MAIN);
				break;
			}
		}
	}
}
