using ARES360.Entity;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.UI;
using ARES360Loader;
using ARES360Loader.Data.World;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;

namespace ARES360.Screen
{
	public class CreditScreen : Screen
	{
		private const float BOTTOM_Y = -1380f;

		private const float SKIP_TIME = 3f;

		private const byte STATE_PLAYING = 1;

		private const byte STATE_SKIPPING = 5;

		private const byte STATE_SKIPPED = 6;

		private const byte STATE_ENDED = 10;

		private const byte STATE_SIGNED_OUT = 30;

		private const byte STATE_LOADING_NEXT_SCREEN = 11;

		private const byte STATE_TRANSITING = 20;

		private const float SCROLL_SPEED = -6.642857f;

		private WorldLoader mBackgroundWorld;

		public Screen NextScreen;

		private byte mState;

		private bool mGamerJustSignout;

		private float mTimer;

		private static CreditScreen mInstance;

		private string ContentManagerKey;

		private bool mHasSkip;

		public static CreditScreen Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new CreditScreen();
				}
				return mInstance;
			}
		}

		public override void Initialize()
		{
			World.LoadRoomByID(0, false);
			World.Camera.Position = new Vector3(8f, -660f, 80f);
			World.Camera.YVelocity = -6.642857f;
			mState = 1;
			if (ProfileManager.Current != null)
			{
				ProfileManager.Current.CurrentLevel = 1;
			}
			mGamerJustSignout = false;
			base.ActivityFinished = false;
		}

		private void Unload()
		{
			World.UnloadAllRoom();
			World.Destroy();
			if (ContentManagerKey != null)
			{
				FlatRedBallServices.Unload(ContentManagerKey);
				ContentManagerKey = null;
			}
			mBackgroundWorld.Destroy();
			mBackgroundWorld = null;
		}

		public override void Destroy()
		{
			World.UnloadAllRoom();
			base.LoadingDone = false;
		}

		public override void Load()
		{
			base.LoadingDone = false;
			mState = 1;
			mHasSkip = false;
			mBackgroundWorld = new Credit();
			World.LoadWorldForMenuScreen(mBackgroundWorld);
			ContentManagerKey = World.ContentManagerKey;
			World.ContentManagerKey = null;
			base.LoadingDone = true;
		}

		public override void Update()
		{
			if (mState == 1)
			{
				if (World.Camera.Y <= -1380f)
				{
					World.Camera.YVelocity = 0f;
					World.Camera.Y = -1380f;
					mHasSkip = false;
					mState = 10;
				}
				else if (GamePad.AnyKeyDown())
				{
					ControlHint.Instance.Clear().AddHint(1048576, "跳过").ShowHints(HorizontalAlignment.Right)
						.FadeIn();
					mTimer = 0f;
					mState = 5;
				}
				if (mGamerJustSignout)
				{
					mState = 30;
				}
			}
			else if (mState == 5)
			{
				if (mGamerJustSignout)
				{
					mState = 30;
				}
				else if (World.Camera.Y <= -1380f)
				{
					World.Camera.YVelocity = 0f;
					World.Camera.Y = -1380f;
					mHasSkip = false;
					mState = 10;
				}
				else if (GamePad.GetMenuKeyDown(1048576))
				{
					mTimer = 0f;
					mState = 6;
					Director.FadeOut(0.2f);
					ControlHint.Instance.FadeOut();
				}
				else
				{
					mTimer += TimeManager.SecondDifference;
					if (mTimer >= 3f)
					{
						ControlHint.Instance.FadeOut();
						mState = 1;
					}
				}
			}
			else if (mState == 6)
			{
				mTimer += TimeManager.SecondDifference;
				if (mTimer >= 0.3f)
				{
					mHasSkip = true;
					mState = 10;
				}
			}
			else if (mState == 30)
			{
				mTimer = 0f;
				mState = 6;
				Director.FadeOut(0.2f);
				ControlHint.Instance.FadeOut();
				if (NextScreen != StartScreen.Instance)
				{
					NextScreen = StartScreen.Instance;
					StartScreen instance = StartScreen.Instance;
					LoadNextScreen(((Screen)instance).Load);
				}
				mGamerJustSignout = false;
			}
			else if (mState == 10)
			{
				World.Camera.YVelocity = 0f;
				if (NextScreen.LoadingDone)
				{
					if (!mHasSkip)
					{
						Director.FadeOut(0.2f);
					}
				}
				else if (mHasSkip)
				{
					Director.FadeIn(0.2f);
					LoadingUI.Instance.Show(true, 1);
				}
				else
				{
					LoadingUI.Instance.Show(true, 0);
				}
				mTimer = 0f;
				mState = 11;
			}
			else if (mState == 11)
			{
				mTimer += TimeManager.SecondDifference;
				if (mTimer >= 0.5f)
				{
					World.Camera.YVelocity = 0f;
					if (NextScreen.LoadingDone)
					{
						mTimer = 0f;
						mState = 20;
					}
					else
					{
						if (NextScreen == MenuScreen.Instance)
						{
							LoadNextScreen(MenuScreen.Instance.LoadMainMenu);
						}
						else
						{
							Screen nextScreen = NextScreen;
							LoadNextScreen(nextScreen.Load);
						}
						mTimer = 0f;
						mState++;
					}
				}
			}
			else if (mState == 12)
			{
				if (NextScreen.LoadingDone)
				{
					Director.FadeOut(0.2f);
					mTimer = 0f;
					mState = 20;
				}
			}
			else if (mState == 20)
			{
				mTimer += TimeManager.SecondDifference;
				if (mTimer >= 0.3f)
				{
					LoadingUI.Instance.Destroy();
					Director.FadeIn(1f);
					ScreenManager.NextScreen = NextScreen;
					base.ActivityFinished = true;
					mState++;
					if (ProfileManager.Current.PlayerType == PlayerType.Mars)
					{
						AchievementManager.Instance.Notify(0, 1);
					}
					else if (ProfileManager.Current.PlayerType == PlayerType.Tarus)
					{
						AchievementManager.Instance.Notify(1, 1);
					}
				}
			}
		}

		public override void OnCurrentGamerSignedOut()
		{
			base.OnCurrentGamerSignedOut();
			mGamerJustSignout = true;
		}
	}
}
