using ARES360.Audio;
using ARES360.Entity;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.UI;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

namespace ARES360.Screen
{
	public class StartScreen : Screen
	{
		private enum State
		{
			__ChangeState__,
			RequireSignedProfile,
			Normal,
			PlayAnimation,
			SelectStorageDevice,
			GotoMenuScreen,
			FadeOut
		}

		private const float DISTANCE_FROM_CAMERA = -89f;

		private const float TIME_EACH_SPACESHIP = 20f;

		private const string CONTENT_MNG_NAME = "start_screen";

		private const int TOTAL_STONES_SPRITES = 10;

		private static StartScreen mInstance;

		private State mState;

		private State mRequestChangedToState;

		private Sprite mARESLogoSprite;

		private Sprite mSpaceBackgroundSprite;

		private Sprite mBaseSprite;

		private Sprite mBaseLightSprite;

		private Sprite[] mStonesSprites;

		private Sprite mStartTab;

		private Sprite mSpaceShip;

		private Text mStartText;

		private Text mPressButtonText;

		private Text mCopyRightText;

		private PlayerIndex mWhoPressStart;

		private bool mIsWaitingForStorageDeviceSelectionFinish;

		private int mAskForSignInCount;

		private float mCountdownTimer01;

		private float mLastTime01;

		private float mCountdownTimer02;

		private float mLastTime02;

		private float mFadeTime;

		private float mLastFadeTime;

		private int mAnimationSet;

		private bool mIsMenuScreenLoadingDone;

		private Clickable[] mClickables;

		public static StartScreen Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new StartScreen();
				}
				return mInstance;
			}
		}

		private StartScreen()
		{
		}

		public override void Load()
		{
			base.LoadingDone = false;
			GUIHelper.LoadFonts();
			mARESLogoSprite = new Sprite();
			mSpaceBackgroundSprite = new Sprite();
			mBaseSprite = new Sprite();
			mBaseLightSprite = new Sprite();
			mStonesSprites = new Sprite[10];
			mStartTab = new Sprite();
			mSpaceShip = new Sprite();
			mARESLogoSprite.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/areslogo_medium"), "start_screen");
			mARESLogoSprite.ScaleX = 23.25f;
			mARESLogoSprite.ScaleY = 6.5f;
			mARESLogoSprite.Position = new Vector3(0f, 8f, 22f);
			mStartTab.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/startscr_tab", "start_screen");
			mStartTab.ScaleX = 67f;
			mStartTab.ScaleY = 3.85f;
			mStartTab.Position = new Vector3(0f, -14f, 0f);
			mStartTab.Alpha = 1f;
			mSpaceBackgroundSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/startscr_space_bg", "start_screen");
			mSpaceBackgroundSprite.ScaleX = 66f;
			mSpaceBackgroundSprite.ScaleY = 37.125f;
			mSpaceBackgroundSprite.Position = new Vector3(0f, 0f, 0f);
			mBaseSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/startscr_base", "start_screen");
			mBaseSprite.ScaleX = 51f;
			mBaseSprite.ScaleY = 28.6f;
			mBaseSprite.Position = new Vector3(6.28f, 0.28f, 0f);
			mBaseLightSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/startscr_base_light", "start_screen");
			mBaseLightSprite.ScaleX = 51f;
			mBaseLightSprite.ScaleY = 28.6f;
			mBaseLightSprite.Position = new Vector3(0f, 0f, 0.1f);
			mBaseLightSprite.AttachTo(mBaseSprite, false);
			mBaseLightSprite.BlendOperation = BlendOperation.Add;
			Texture2D texture = FlatRedBallServices.Load<Texture2D>("Content/UI/startscr_stones", "start_screen");
			for (int i = 0; i < 10; i++)
			{
				mStonesSprites[i] = new Sprite();
				mStonesSprites[i].Texture = texture;
			}
			mStonesSprites[0].ScaleX = 69.75f;
			mStonesSprites[0].ScaleY = 39.375f;
			mStonesSprites[1].ScaleX = 69.75f;
			mStonesSprites[1].ScaleY = 39.375f;
			mStonesSprites[2].ScaleX = 69.75f;
			mStonesSprites[2].ScaleY = 39.375f;
			mStonesSprites[3].ScaleX = 66f;
			mStonesSprites[3].ScaleY = 37.125f;
			mStonesSprites[4].ScaleX = 66f;
			mStonesSprites[4].ScaleY = 37.125f;
			mStonesSprites[5].ScaleX = 66f;
			mStonesSprites[5].ScaleY = 37.125f;
			mStonesSprites[6].ScaleX = 26.4f;
			mStonesSprites[6].ScaleY = 14.85f;
			mStonesSprites[7].ScaleX = 26.4f;
			mStonesSprites[7].ScaleY = 14.85f;
			mStonesSprites[8].ScaleX = 26.4f;
			mStonesSprites[8].ScaleY = 14.85f;
			mStonesSprites[9].ScaleX = 26.4f;
			mStonesSprites[9].ScaleY = 14.85f;
			mSpaceShip.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/startscr_spaceship", "start_screen");
			mSpaceShip.ScaleX = 9.625f;
			mSpaceShip.ScaleY = 17.25f;
			mSpaceShip.Position = new Vector3(-17f, 1f, 90f);
			mStartText = new Text(GUIHelper.CaptionFont);
			mStartText.Scale = 1.4f;
			mStartText.Spacing = 1.4f;
			mStartText.HorizontalAlignment = HorizontalAlignment.Center;
			mStartText.VerticalAlignment = VerticalAlignment.Center;
			mStartText.ColorOperation = ColorOperation.Subtract;
			mStartText.SetColor(1f, 0f, 0f);
			mStartText.DisplayText = "按下任意键";
			mStartText.Position = new Vector3(0f, -14f, 0f);
			mStartText.Alpha = 1f;
			mPressButtonText = new Text(GUIHelper.CaptionFont);
			mPressButtonText.Scale = 1.4f;
			mPressButtonText.Spacing = 1.4f;
			mPressButtonText.HorizontalAlignment = HorizontalAlignment.Center;
			mPressButtonText.VerticalAlignment = VerticalAlignment.Center;
			mPressButtonText.ColorOperation = ColorOperation.Subtract;
			mPressButtonText.SetColor(0f, 0f, 0f);
			mPressButtonText.DisplayText = "";
			mPressButtonText.Position = new Vector3(0.6f, -14f, 0f);
			mPressButtonText.Alpha = 1f;
			mCopyRightText = new Text(GUIHelper.CaptionFont);
			mCopyRightText.Scale = 1f;
			mCopyRightText.Spacing = 1f;
			mCopyRightText.HorizontalAlignment = HorizontalAlignment.Center;
			mCopyRightText.VerticalAlignment = VerticalAlignment.Center;
			mCopyRightText.ColorOperation = ColorOperation.Texture;
			mCopyRightText.DisplayText = "";
			mCopyRightText.Position = new Vector3(0f, -31.558f, 0f);
			mClickables = new Clickable[1]
			{
				new ClickableSprite
				{
					sprite = mStartTab,
					OnMouseEnter = (Clickable.ClickableHoverHandler)delegate
					{
						mStartText.SetColor(1f, 0.1f, 0.1f);
						mPressButtonText.SetColor(0.1f, 0.1f, 0.1f);
					},
					OnMouseExit = (Clickable.ClickableHoverHandler)delegate
					{
						mStartText.SetColor(1f, 0f, 0f);
						mPressButtonText.SetColor(0f, 0f, 0f);
					},
					OnMouseClick = (Clickable.ClickableHandler)delegate
					{
						GamePad.CurrentPlayerIndex = PlayerIndex.One;
						ChangeState(State.PlayAnimation);
						SFXManager.PlaySound("success");
						return true;
					}
				}
			};
			BGMManager.LoadMenu();
			base.LoadingDone = true;
		}

		public override void Initialize()
		{
			StorageDeviceManager.Reset();
			ProfileManager.ApplyConfigs();
			World.Camera.Position = new Vector3(0f, 0f, 89f);
			base.LoadingDone = false;
			base.ActivityFinished = false;
			mCountdownTimer01 = 25f;
			mCountdownTimer02 = 0f;
			mFadeTime = 0f;
			for (int i = 0; i < 10; i++)
			{
				SpriteManager.AddToLayer(mStonesSprites[i], GUIHelper.WorldLayer);
			}
			for (int j = 0; j < 3; j++)
			{
				mStonesSprites[j].Position = new Vector3((float)(-156 + 156 * j), (float)(-4 + 4 * j), -20f);
				mStonesSprites[j].Velocity = new Vector3(7f, 0.2f, -0.1f) / 2f;
			}
			for (int k = 3; k < 6; k++)
			{
				int num = k - 3;
				mStonesSprites[k].Position = new Vector3((float)(148 - 148 * num), (float)(-12 + 12 * num), -20f);
				mStonesSprites[k].Velocity = new Vector3(-8.25f, 1.5f, -0.0625f) / 2f;
			}
			for (int l = 6; l < 10; l++)
			{
				int num2 = l - 6;
				mStonesSprites[l].Position = new Vector3((float)(-108 + 72 * num2), 2.5f - (float)(4 * num2), -4f);
				mStonesSprites[l].Velocity = new Vector3(4.125f, -0.5f, 0.0625f) / 2f;
			}
			mStartTab.Visible = true;
			mPressButtonText.Visible = true;
			mCopyRightText.Visible = true;
			mStartTab.Visible = true;
			mARESLogoSprite.Visible = true;
			TextManager.AddToLayer(mStartText, SpriteManager.TopLayer);
			TextManager.AddToLayer(mPressButtonText, SpriteManager.TopLayer);
			TextManager.AddToLayer(mCopyRightText, SpriteManager.TopLayer);
			SpriteManager.AddToLayer(mStartTab, SpriteManager.TopLayer);
			SpriteManager.AddToLayer(mARESLogoSprite, SpriteManager.TopLayer);
			SpriteManager.AddSprite(mSpaceBackgroundSprite);
			SpriteManager.AddToLayer(mBaseSprite, GUIHelper.WorldLayer);
			SpriteManager.AddToLayer(mBaseLightSprite, GUIHelper.WorldLayer);
			SpriteManager.AddToLayer(mSpaceShip, GUIHelper.WorldLayer);
			if (!BGMManager.isPlaying)
			{
				BGMManager.Play(0);
			}
			mState = State.Normal;
			ScreenManager.NextScreen = MenuScreen.Instance;
			Director.FadeIn(1f);
		}

		private void ChangeState(State nextState)
		{
			if (mState != nextState)
			{
				mRequestChangedToState = nextState;
				mState = State.__ChangeState__;
			}
		}

		public override void Update()
		{
			switch (mState)
			{
			case State.Normal:
			{
				mWhoPressStart = PlayerIndex.One;
				bool flag = false;
				if (!BGMManager.isPlaying)
				{
					BGMManager.Play(0);
				}
				if (GamePad.TryDetectDevice(true))
				{
					mWhoPressStart = PlayerIndex.One;
					flag = true;
				}
				else
				{
					GamePad.UpdateClickables(mClickables);
				}
				if (flag)
				{
					SFXManager.PlaySound("success");
					GamePad.CurrentPlayerIndex = mWhoPressStart;
					ChangeState(State.PlayAnimation);
				}
				ManageSpaceShipAnimation();
				break;
			}
			case State.RequireSignedProfile:
				if (!Guide.IsVisible)
				{
					if (mAskForSignInCount >= 1)
					{
						ChangeState(State.Normal);
					}
					else
					{
						Guide.ShowSignIn(1, false);
						mAskForSignInCount++;
					}
				}
				break;
			case State.PlayAnimation:
				mLastTime02 = mCountdownTimer02;
				mCountdownTimer02 -= TimeManager.SecondDifference;
				if (mCountdownTimer02 < 0f)
				{
					if (mLastTime02 >= 0f)
					{
						if (ProfileManager.IsTrialMode)
						{
							ChangeState(State.GotoMenuScreen);
						}
						else
						{
							LoadFromMyDoc();
							ChangeState(State.GotoMenuScreen);
						}
					}
				}
				else if (mCountdownTimer02 < 0.75f)
				{
					if (mLastTime02 >= 0.75f)
					{
						mStartTab.ScaleYVelocity = -6f;
						mStartTab.AlphaRate = -2f;
					}
				}
				else if (mCountdownTimer02 < 1.25f)
				{
					if (mLastTime02 >= 1.25f)
					{
						mStartText.AlphaRate = -2f;
						mPressButtonText.AlphaRate = -2f;
					}
				}
				else if (mCountdownTimer02 < 1.75f)
				{
					if (mLastTime02 >= 1.75f)
					{
						mStartText.AlphaRate = 2f;
					}
				}
				else if (mCountdownTimer02 < 2f && mLastTime02 >= 2f)
				{
					mStartText.AlphaRate = -4f;
				}
				break;
			case State.SelectStorageDevice:
				if (!mIsWaitingForStorageDeviceSelectionFinish)
				{
					break;
				}
				break;
			case State.GotoMenuScreen:
				if (mFadeTime > 0f)
				{
					mLastFadeTime = mFadeTime;
					mFadeTime -= TimeManager.SecondDifference;
					if (mFadeTime <= 0f)
					{
						mFadeTime = 0f;
					}
					else if (mFadeTime <= 0.2f)
					{
						if (mLastFadeTime > 0.2f)
						{
							Director.FadeIn(0.25f);
							LoadingUI.Instance.Show(true, 1);
						}
					}
					else if (mFadeTime <= 0.25f && mLastFadeTime > 0.25f)
					{
						if (mIsMenuScreenLoadingDone)
						{
							mFadeTime = 0.51f;
							Director.FadeOut(0.5f);
							ChangeState(State.FadeOut);
						}
						else
						{
							mStartTab.Visible = false;
							mPressButtonText.Visible = false;
							mCopyRightText.Visible = false;
							mStartTab.Visible = false;
							mARESLogoSprite.Visible = false;
						}
					}
				}
				else if (mIsMenuScreenLoadingDone)
				{
					mFadeTime = 1.1f;
					Director.FadeOut(0.5f);
					ChangeState(State.FadeOut);
				}
				break;
			case State.FadeOut:
				if (mFadeTime > 0f)
				{
					mLastFadeTime = mFadeTime;
					mFadeTime -= TimeManager.SecondDifference;
					if (mFadeTime <= 0f)
					{
						if (mLastFadeTime > 0f)
						{
							mFadeTime = 0f;
						}
					}
					else if (mFadeTime <= 0.5f)
					{
						if (mLastFadeTime > 0.5f)
						{
							Director.FadeIn(0.5f);
							base.ActivityFinished = true;
						}
					}
					else if (mFadeTime <= 0.6f && mLastFadeTime > 0.6f)
					{
						LoadingUI.Instance.Hide();
					}
				}
				break;
			case State.__ChangeState__:
				if (mRequestChangedToState == State.RequireSignedProfile)
				{
					mAskForSignInCount = 0;
				}
				else if (mRequestChangedToState == State.SelectStorageDevice)
				{
					mIsWaitingForStorageDeviceSelectionFinish = false;
				}
				else if (mRequestChangedToState == State.GotoMenuScreen)
				{
					int lastPlayedLevel = ProfileManager.LastPlayedLevel;
					mIsMenuScreenLoadingDone = false;
					LoadNextScreen(_LoadMenuScreen);
					mFadeTime = 0.5f;
					Director.FadeOut(0.25f);
				}
				else if (mRequestChangedToState == State.PlayAnimation)
				{
					mCountdownTimer02 = 2f;
				}
				mState = mRequestChangedToState;
				break;
			}
			for (int i = 0; i < 3; i++)
			{
				if (mStonesSprites[i].Position.X >= 156f)
				{
					mStonesSprites[i].Position = new Vector3(-156f, -4f, -20f);
				}
			}
			for (int j = 3; j < 6; j++)
			{
				if (mStonesSprites[j].Position.X <= -148f)
				{
					mStonesSprites[j].Position = new Vector3(148f, -12f, -20f);
				}
			}
			for (int k = 6; k < 10; k++)
			{
				if (mStonesSprites[k].Position.X >= 108f)
				{
					mStonesSprites[k].Position = new Vector3(-108f, 2.5f, -4f);
				}
			}
			if (mBaseLightSprite.Alpha >= 0.95f)
			{
				mBaseLightSprite.AlphaRate = -0.5f;
			}
			else if (mBaseLightSprite.Alpha <= 0.05f)
			{
				mBaseLightSprite.AlphaRate = 0.5f;
			}
		}

		private void ManageSpaceShipAnimation()
		{
			mLastTime01 = mCountdownTimer01;
			mCountdownTimer01 -= TimeManager.SecondDifference;
			if (mCountdownTimer01 < 0f)
			{
				if (mLastTime01 >= 0f)
				{
					mCountdownTimer01 = 20f;
					mAnimationSet = FlatRedBallServices.Random.Next(2);
				}
			}
			else if (mCountdownTimer01 < 14.5f)
			{
				if (mLastTime01 >= 14.5f)
				{
					mSpaceShip.AlphaRate = -4f;
				}
			}
			else if (mCountdownTimer01 < 19.3f)
			{
				if (mLastTime01 >= 19.3f)
				{
					if (mAnimationSet == 1)
					{
						mSpaceShip.XAcceleration = 30f;
					}
					else
					{
						mSpaceShip.XAcceleration = 50f;
					}
				}
			}
			else if (mCountdownTimer01 < 20f && mLastTime01 >= 20f)
			{
				mSpaceShip.Velocity = Vector3.Zero;
				mSpaceShip.Acceleration = Vector3.Zero;
				mSpaceShip.AlphaRate = 0f;
				mSpaceShip.Alpha = 1f;
				if (mAnimationSet == 1)
				{
					mSpaceShip.Position = new Vector3(-20f, 1f, 90f);
					mSpaceShip.ZVelocity = -600f;
					mSpaceShip.XVelocity = -10f;
					mSpaceShip.XAcceleration = -15f;
				}
				else
				{
					mSpaceShip.Position = new Vector3(-20f, 12f, 90f);
					mSpaceShip.ZVelocity = -800f;
					mSpaceShip.XVelocity = -20f;
				}
			}
		}

		private void _LoadMenuScreen()
		{
			mIsMenuScreenLoadingDone = false;
			MenuScreen.Instance.LoadMainMenu();
			mIsMenuScreenLoadingDone = true;
		}

		private void LoadFromMyDoc()
		{
			bool flag = true;
			if (!ProfileManager.LoadProfilesFromStorage())
			{
				flag = false;
			}
			if (flag)
			{
				ProfileManager.Current = null;
				if (ProfileManager.Mars.UnlockedLevel > 0 || ProfileManager.Tarus.UnlockedLevel > 0)
				{
					if (ProfileManager.Profile.LastPlayedCharacter == PlayerType.Mars)
					{
						ProfileManager.Current = ProfileManager.Mars;
					}
					else if (ProfileManager.Profile.LastPlayedCharacter == PlayerType.Tarus)
					{
						ProfileManager.Current = ProfileManager.Tarus;
					}
				}
			}
			AchievementManager.Instance.RestoreAchievements();
		}

		public override void Destroy()
		{
			SpriteManager.RemoveSprite(mARESLogoSprite);
			SpriteManager.RemoveSprite(mSpaceBackgroundSprite);
			SpriteManager.RemoveSprite(mBaseLightSprite);
			SpriteManager.RemoveSprite(mBaseSprite);
			for (int i = 0; i < 10; i++)
			{
				SpriteManager.RemoveSprite(mStonesSprites[i]);
			}
			SpriteManager.RemoveSprite(mStartTab);
			SpriteManager.RemoveSprite(mSpaceShip);
			TextManager.RemoveText(mStartText);
			TextManager.RemoveText(mPressButtonText);
			TextManager.RemoveText(mCopyRightText);
			FlatRedBallServices.Unload("start_screen");
			mARESLogoSprite = null;
			mSpaceBackgroundSprite = null;
			mBaseLightSprite = null;
			mBaseSprite = null;
			mStonesSprites = null;
			mStartTab = null;
			mClickables = null;
			mSpaceShip = null;
			mStartText = null;
			mPressButtonText = null;
			mCopyRightText = null;
		}

		public override void OnCurrentGamerSignedOut()
		{
			if (Gamer.SignedInGamers.Count == 0)
			{
				ChangeState(State.RequireSignedProfile);
			}
		}
	}
}
