using ARES360.Audio;
using ARES360.Entity;
using ARES360.Event;
using ARES360.Input;
using ARES360.Movie;
using ARES360.Profile;
using ARES360.UI;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;

namespace ARES360.Screen
{
	public class VideoScreen : Screen
	{
		private const byte STATE_LOADING = 0;

		private const byte STATE_LOADED = 1;

		private const byte STATE_PLAYING = 2;

		private const byte STATE_SKIPPING = 3;

		private const byte STATE_SKIPPED = 4;

		private const byte STATE_ENDED = 10;

		private const byte STATE_LOADING_NEXT_SCREEN = 11;

		private const byte STATE_TRANSITING = 20;

		private const byte STATE_SIGNED_OUT = 30;

		private const float SKIP_TIME = 3f;

		public Screen NextScreen;

		private byte mState;

		private static VideoScreen mInstance;

		private ContentManager mContent;

		private MovieBatch mMovieBatch;

		private bool mIsMovieFinished;

		private bool mHasSkip;

		private bool mGamerJustSignout;

		public string VideoName;

		public string AudioName;

		private Game mGame;

		private float mTimer;

		private TimeSpan mEndTime;

		public static VideoScreen Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new VideoScreen();
				}
				return mInstance;
			}
		}

		public VideoScreen()
		{
			mMovieBatch = new MovieBatch();
			mMovieBatch.OnFinish = OnMovieFinish;
			mHasSkip = false;
		}

		public new void Load()
		{
			mState = 0;
			mHasSkip = false;
			mIsMovieFinished = false;
			mContent = new ContentManager(mGame.Services);
			Video video = mContent.Load<Video>(VideoName);
			mMovieBatch.Z = -100f;
			mMovieBatch.Load(video);
			if (AudioName != null)
			{
				BGMManager.AddVideoSoundtrack(AudioName);
				mEndTime = video.Duration.Subtract(new TimeSpan(0, 0, 2));
			}
			if (NextScreen == CreditScreen.Instance)
			{
				NextScreen.Load();
			}
			base.ActivityFinished = false;
			base.LoadingDone = true;
			mState = 1;
		}

		private void OnMovieFinish()
		{
			mIsMovieFinished = true;
		}

		public void Initialize(Game game)
		{
			mGame = game;
		}

		public override void Initialize()
		{
			BGMManager.FadeOff();
			SpriteManager.AddDrawableBatch(mMovieBatch);
			mState = 2;
			Director.FadeIn(1f);
			mGamerJustSignout = false;
		}

		private void Unload()
		{
			mMovieBatch.Unload();
			if (mContent != null)
			{
				mContent.Unload();
				mContent = null;
			}
			GC.Collect();
			if (AudioName != null)
			{
				BGMManager.Play(0);
				AudioName = null;
			}
		}

		public override void Destroy()
		{
			ControlHint.Instance.HideHints();
			SpriteManager.RemoveDrawableBatch(mMovieBatch);
			base.ActivityFinished = false;
			base.LoadingDone = false;
			Unload();
		}

		public override void Update()
		{
			if (AudioName != null && mMovieBatch.Video != null && mMovieBatch.Player != null && mMovieBatch.Player.PlayPosition >= mEndTime)
			{
				BGMManager.Play(0);
				AudioName = null;
			}
			if (mState == 2)
			{
				if (mGamerJustSignout)
				{
					mState = 30;
				}
				else if (mIsMovieFinished)
				{
					mHasSkip = false;
					mState = 10;
				}
				else if (GamePad.AnyKeyDown())
				{
					ControlHint.Instance.Clear().AddHint(1048576, "跳过").ShowHints(HorizontalAlignment.Right)
						.FadeIn();
					mState = 3;
					mTimer = 0f;
				}
			}
			else if (mState == 3)
			{
				if (mGamerJustSignout)
				{
					mState = 30;
				}
				else if (mIsMovieFinished)
				{
					mTimer = 0f;
					mState = 10;
				}
				else if (GamePad.GetMenuKeyDown(1048576))
				{
					mTimer = 0f;
					mState = 4;
					Director.FadeOut(0.2f);
					ControlHint.Instance.FadeOut();
				}
				else
				{
					mTimer += TimeManager.SecondDifference;
					if (mTimer >= 3f)
					{
						ControlHint.Instance.FadeOut();
						mState = 2;
					}
				}
			}
			else if (mState == 4)
			{
				mTimer += TimeManager.SecondDifference;
				if (mIsMovieFinished)
				{
					mHasSkip = true;
					mState = 10;
				}
				else if (mTimer >= 0.3f)
				{
					mMovieBatch.Stop();
				}
			}
			else if (mState == 30)
			{
				mTimer = 0f;
				mState = 4;
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
				if (NextScreen.LoadingDone)
				{
					if (!mHasSkip)
					{
						if (ProfileManager.Current.PlayerType == PlayerType.Tarus)
						{
							Director.FadeOutWhite(0.2f);
						}
						else
						{
							Director.FadeOut(0.2f);
						}
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
					Unload();
					if (NextScreen.LoadingDone)
					{
						mTimer = 0f;
						mState = 20;
					}
					else
					{
						Screen nextScreen = NextScreen;
						LoadNextScreen(nextScreen.Load);
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
