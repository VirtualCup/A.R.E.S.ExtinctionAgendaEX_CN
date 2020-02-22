using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.Screen;
using ARES360.UI;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Graphics.Model;
using FlatRedBall.Graphics.PostProcessing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Threading;
using System.Windows.Forms;

namespace ARES360
{
	public class Ares : Game
	{
		private GraphicsDeviceManager mGraphics;

		private static Gamer mSignedOutGamer;

		private bool mIsInitializeCompleted;

		private static float mWaitForLiveSignInTimeOut;

		public Ares()
		{
			Pref.NativeScreenWidth = SystemInformation.PrimaryMonitorSize.Width;
			Pref.NativeScreenHeight = SystemInformation.PrimaryMonitorSize.Height;
			ProfileManager.LoadConfigsFromStorage();
			Pref.Steamworks = false;
			Pref.HasApolloDLC = false;
			if (SteamAPI.Init())
			{
				Pref.Steamworks = true;
				Pref.HasApolloDLC = SteamApps.BIsSubscribedApp(new AppId_t(92300u));
				string gamerKey = $"steam-{SteamUser.GetSteamID().m_SteamID}";
				ProfileManager.Configs.GamerKey = gamerKey;
			}
			ProfileManager.SaveConfigsToStorage();
			Pref.FullScreen = (ProfileManager.Configs.WindowMode == 0);
			int num = ProfileManager.Configs.ScreenWidth;
			int num2 = ProfileManager.Configs.ScreenHeight;
			if (num == 0 || num2 == 0)
			{
				num = Pref.NativeScreenWidth;
				num2 = Pref.NativeScreenHeight;
				float num3 = (float)num2 / (float)num * 16f;
				if (!(8.5f <= num3) || !(num3 <= 9.5f))
				{
					if (num3 > 9f)
					{
						num2 = num * 9 / 16;
					}
					else
					{
						num = num2 * 16 / 9;
					}
				}
				if (num == 0 || num2 == 0)
				{
					num = 1920;
					num2 = 1080;
				}
				if (num > Pref.NativeScreenWidth)
				{
					num = Pref.NativeScreenWidth;
				}
				if (num2 > Pref.NativeScreenHeight)
				{
					num2 = Pref.NativeScreenHeight;
				}
			}
			Pref.Width = num;
			Pref.Height = num2;
			mGraphics = new GraphicsDeviceManager(this);
			mGraphics.PreferredBackBufferWidth = Pref.Width;
			mGraphics.PreferredBackBufferHeight = Pref.Height;
			mGraphics.IsFullScreen = Pref.FullScreen;
			ProfileManager.UpdateIsTrialModeFlag();
			base.Exiting += Ares_Exiting;
		}

		public static Form GetForm()
		{
			return (Form)Control.FromHandle(FlatRedBallServices.Game.Window.Handle);
		}

		protected override void BeginRun()
		{
			base.BeginRun();
			Form form = GetForm();
			form.Text = "A.R.E.S.EX";
			if (ProfileManager.Configs.WindowMode == 2)
			{
				form.FormBorderStyle = FormBorderStyle.None;
				form.Width = Pref.Width;
				form.Height = Pref.Height;
				form.Left = ProfileManager.Configs.ScreenLeft;
				form.Top = ProfileManager.Configs.ScreenTop;
			}
		}

		protected override void OnExiting(object sender, EventArgs args)
		{
			if (Pref.Steamworks)
			{
				SteamAPI.Shutdown();
			}
			GamePad.Shutdown();
			base.OnExiting(sender, args);
		}

		protected override void Initialize()
		{
			ManageResolutionSupport();
			FlatRedBallServices.InitializeFlatRedBall(this, mGraphics);
			TextManager.FilterTexts = true;
			base.Initialize();
			InitializeARES();
		}

		private void InitializeARES()
		{
			Thread.CurrentThread.Priority = ThreadPriority.Highest;
			mIsInitializeCompleted = false;
			DirectInputPad.Initialize(this);
			MouseUI.Initialize(mGraphics.GraphicsDevice);
			GamePad.Initialize();
			InputBinder.Load();
			BGMManager.Initialize();
			ModelManager.Initialize();
			ProcessManager.Initialize();
			CollisionManager.Initialize();
			EventManager.Initialize();
			EnemyManager.Initialize();
			ProfileManager.Initialize();
			AchievementManager.Instance.Initialize();
			World.Initialize();
			OnceEmitParticlePool.Initialize();
			TimedEmitParticlePool.Initialize();
			VideoScreen.Instance.Initialize(this);
			SplashScreen.Instance.Load();
			ScreenManager.Start(SplashScreen.Instance);
			SteamLeaderboardManager.Instance.Initialize();
			mIsInitializeCompleted = true;
		}

		private void ManageResolutionSupport()
		{
			Viewport viewport = mGraphics.GraphicsDevice.Viewport;
			if (Pref.Width > viewport.Width || Pref.Height > viewport.Height)
			{
				Pref.Width = viewport.Width;
				Pref.Height = viewport.Height;
				mGraphics.PreferredBackBufferWidth = Pref.Width;
				mGraphics.PreferredBackBufferHeight = Pref.Height;
			}
		}

		protected override void LoadContent()
		{
		}

		protected override void UnloadContent()
		{
		}

		protected override void Update(GameTime gameTime)
		{
			if (!mIsInitializeCompleted)
			{
				FlatRedBallServices.Update(gameTime);
				base.Update(gameTime);
			}
			else
			{
				if (Pref.Steamworks)
				{
					SteamAPI.RunCallbacks();
				}
				float secondDifference = TimeManager.SecondDifference;
				GamePad.Update(secondDifference);
				MouseUI.Update(secondDifference);
				ControlHint.Instance.Update();
				Director.Update();
				FlatRedBallServices.Update(gameTime);
				SFXManager.Update();
				BGMManager.Update();
				CollisionManager.UpdateObjectCollision();
				EventManager.Update();
				ScreenManager.Update();
				ProcessManager.Process();
				CollisionManager.UpdateAttackCollission();
				EnemyManager.Update();
				World.Camera.Update();
				if (World.IsProcessingLULTransform)
				{
					World.ProcessLULTransforms();
				}
				AchievementManager.Instance.Update();
				StorageDeviceManager.Update();
				ARESPostProcessingManager.Update();
				ProfileManager.Update();
				ManageSignInAndOut();
				GuideMessageBoxWrapper.Instance.Update();
				base.Update(gameTime);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			FlatRedBallServices.Draw();
			MouseUI.Draw();
			base.Draw(gameTime);
		}

		protected override void EndDraw()
		{
			base.EndDraw();
		}

		private void Ares_Exiting(object sender, EventArgs e)
		{
		}

		public static void GamerWasSignedIn(object sender, SignedInEventArgs e)
		{
			SignedInGamer gamer = e.Gamer;
			if (ProfileManager.CurrentSignedInGamer != null && ProfileManager.CurrentSignedInGamer.PlayerIndex == gamer.PlayerIndex && ProfileManager.CurrentSignedInGamer.Tag == gamer.Tag)
			{
				mWaitForLiveSignInTimeOut = 0f;
				ProfileManager.CurrentSignedInGamer = gamer;
			}
			ProfileManager.UpdateIsTrialModeFlag();
		}

		public static void GamerWasSignedOut(object sender, SignedOutEventArgs e)
		{
			mSignedOutGamer = e.Gamer;
			if (ProfileManager.CurrentSignedInGamer != null && ProfileManager.CurrentSignedInGamer.Gamertag == mSignedOutGamer.Gamertag)
			{
				mWaitForLiveSignInTimeOut = Pref.WAIT_TIME_FOR_SIGNIN_AFTER_SIGNOUT;
			}
		}

		private void ManageSignInAndOut()
		{
			if (mWaitForLiveSignInTimeOut > 0f)
			{
				mWaitForLiveSignInTimeOut -= TimeManager.SecondDifference;
				if (mWaitForLiveSignInTimeOut <= 0f)
				{
					mWaitForLiveSignInTimeOut = 0f;
					if (ScreenManager.CurrentScreen != StartScreen.Instance)
					{
						GuideMessageBoxWrapper.Instance.Show("玩家已登出", "你已登出档案。之后会回到标题画面。", MsgBoxAskForGamerSignout, 1, "知道", string.Empty, string.Empty);
					}
				}
			}
		}

		public static void MsgBoxAskForGamerSignout(PopUpUI.PopUpUIResult result)
		{
			ScreenManager.ManageGamerSignedOut();
			ProfileManager.CurrentSignedInGamer = null;
		}
	}
}
