using ARES360.Audio;
using ARES360.Entity;
using ARES360.Input;
using ARES360.Profile;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ARES360.UI
{
	public class ScoreUI : IProcessable
	{
		private const float BACKGROUND_ALPHA = 0.8f;

		private const float DISTANCE_FROM_CAMERA = -75f;

		private const float ROWS_OFFSET = 5.5f;

		private const float ROWS_HEIGHT = -4f;

		private const byte STATE_FINISHED = 100;

		private const byte STATE_SHOW_BONUS = 60;

		private const byte STATE_FILL_EXPERIENCE_BAR = 70;

		private static ScoreUI mInstance = null;

		public VoidEventHandler OnFinish;

		private Sprite mCorner;

		private Sprite mHeaderBackground;

		private Sprite mBodyBackground;

		private Sprite mTitle;

		private Sprite mSubTitle;

		private Sprite mBlackOverlay;

		private Sprite mScoreTitle;

		private Text mScoreLabel;

		private Sprite mRank;

		private PositionedObject mRoot;

		private ScoreRow[] mRows;

		private Vector3 mLevelUpPoint;

		private Vector3 mBarBackgroundPoint;

		private Vector3 mNewPoint;

		private Vector3 mCharacterNamePoint;

		private Vector3 mChapterTitlePoint;

		private Vector3 mChapterLabelPoint;

		private Vector3 mHiScoreTitlePoint;

		private Vector3 mHiScoreLabelPoint;

		private Vector3 mBonusLabelPoint;

		private Vector3 mLevelLabelPoint;

		private Vector3 mExperienceLabelPoint;

		private Text mLevelLabel;

		private Sprite mLevelUp;

		private Text mExperienceLabel;

		private Sprite mBarBackground;

		private Sprite mBar;

		private Sprite mBarLeft;

		private Text mChapterTitle;

		private Text mHiScoreTitle;

		private Text mChapterLabel;

		private Text mHiScoreLabel;

		private Text mNewHiScoreLabel;

		private Sprite mNew;

		private Text mBonusLabel;

		private Sprite mCharacterName;

		private Sprite mCharacterRankBackground;

		private int mCharacterRank;

		private float mExperience;

		private float mNewExperience;

		private int mScore;

		private Vector3 mCornerPoint;

		private Vector3 mHeaderBackgroundPoint;

		private Vector3 mBodyBackgroundPoint;

		private Vector3 mTitlePoint;

		private Vector3 mSubTitlePoint;

		private bool mHighScoreSoundPlayed;

		private bool mDramaSoundEnabled;

		private bool mIsLoaded;

		private static Vector3 RANK_POINT = new Vector3(36f, -8f, 0f);

		private int hiScore;

		private int newHiScore;

		private bool skip;

		private bool mIsInManagers;

		private byte mIsInitialized;

		private float mTimer;

		private byte mState;

		private float mLevelUpTimer = 2f;

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

		public static ScoreUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new ScoreUI();
				}
				return mInstance;
			}
		}

		public ScoreUI()
		{
			mIsInitialized = 0;
			mIsLoaded = false;
		}

		public void Load()
		{
			if (!mIsLoaded)
			{
				mRows = new ScoreRow[6]
				{
					new ScoreRow(),
					new ScoreRow(),
					new ScoreRow(),
					new ScoreRow(),
					new ScoreRow(),
					new ScoreRow()
				};
				for (int num = mRows.Length - 1; num >= 0; num--)
				{
					mRows[num].Load();
				}
				mCorner = GlobalSheet.CreateSprite(131);
				mHeaderBackground = GlobalSheet.CreateSprite(129);
				mBodyBackground = GlobalSheet.CreateSprite(130);
				mTitle = LocalizedSheet.CreateSprite(39);
				mSubTitle = LocalizedSheet.CreateSprite(41);
				mBlackOverlay = GlobalSheet.CreateSprite(8);
				mScoreTitle = LocalizedSheet.CreateSprite(45);
				mScoreLabel = new Text(GUIHelper.SpeechFont);
				mRank = LocalizedSheet.CreateSprite(23);
				mRoot = new PositionedObject();
				mCharacterRankBackground = GlobalSheet.CreateSprite(132);
				mLevelUp = LocalizedSheet.CreateSprite(42);
				mBarBackground = GlobalSheet.CreateSprite(106);
				mBar = GlobalSheet.CreateSprite(105);
				mBarLeft = GlobalSheet.CreateSprite(105);
				mNew = LocalizedSheet.CreateSprite(43);
				mCharacterName = LocalizedSheet.CreateSprite(40);
				mChapterTitle = new Text(GUIHelper.CaptionFont);
				mChapterLabel = new Text(GUIHelper.CaptionFont);
				mHiScoreTitle = new Text(GUIHelper.CaptionFont);
				mHiScoreLabel = new Text(GUIHelper.CaptionFont);
				mNewHiScoreLabel = new Text(GUIHelper.CaptionFont);
				mBonusLabel = new Text(GUIHelper.CaptionFont);
				mLevelLabel = new Text(GUIHelper.CaptionFont);
				mExperienceLabel = new Text(GUIHelper.CaptionFont);
				mIsLoaded = true;
			}
		}

		public void Unload()
		{
			if (mIsLoaded)
			{
				mIsLoaded = false;
				OnRemove();
				mCorner = null;
				mHeaderBackground = null;
				mBodyBackground = null;
				mTitle = null;
				mSubTitle = null;
				mBlackOverlay = null;
				mRows = null;
				mScoreTitle = null;
				mScoreLabel = null;
				mRank = null;
				mRoot = null;
				mCharacterRankBackground = null;
				mLevelUp = null;
				mBarBackground = null;
				mBar = null;
				mBarLeft = null;
				mNew = null;
				mCharacterName = null;
				mChapterTitle = null;
				mChapterLabel = null;
				mHiScoreTitle = null;
				mHiScoreLabel = null;
				mNewHiScoreLabel = null;
				mBonusLabel = null;
				mLevelLabel = null;
				mExperienceLabel = null;
			}
		}

		public void OnRegister()
		{
			mIsInitialized = 0;
		}

		private int GetCharacterRank(int experience)
		{
			int num = 0;
			GetCharacterRankScore(num);
			int characterRankScore = GetCharacterRankScore(num + 1);
			while (experience >= characterRankScore)
			{
				if (num >= 6)
				{
					return 6;
				}
				num++;
				characterRankScore = GetCharacterRankScore(num + 1);
			}
			return num;
		}

		private int GetCharacterRankScore(int rank)
		{
			return PlayerProfile.GetCharacterRankScore(rank);
		}

		private string GetCharacterRankBonus(int rank)
		{
			if (rank == 6)
			{
				return string.Empty;
			}
			return "奖励：提高10点生命";
		}

		private bool OnInitialize()
		{
			if (mIsInitialized == 0)
			{
				mIsInitialized++;
				mHighScoreSoundPlayed = false;
				return false;
			}
			if (mIsInitialized == 1)
			{
				mRoot.RelativeZ = -75f;
				mRoot.AttachTo(World.Camera, false);
				SpriteManager.AddPositionedObject(mRoot);
				mBlackOverlay.ScaleX = 150f;
				mBlackOverlay.ScaleY = 150f;
				mBlackOverlay.RelativePosition = new Vector3(0f, 0f, -10f);
				mBlackOverlay.Alpha = 0f;
				mBlackOverlay.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mBlackOverlay, GUIHelper.UILayer);
				mIsInitialized++;
				return false;
			}
			if (mIsInitialized == 2)
			{
				World.Camera.GetXViewVolByDistance(75f);
				float yViewVolByDistance = World.Camera.GetYViewVolByDistance(75f);
				mHeaderBackground.Alpha = 0f;
				mHeaderBackground.ScaleX = 60f;
				mHeaderBackground.ScaleY = 5.34375f;
				mHeaderBackground.RelativePosition = new Vector3(0f, yViewVolByDistance - 13.2f, 0f);
				mHeaderBackground.AttachTo(mRoot, false);
				mHeaderBackground.TextureFilter = TextureFilter.Linear;
				SpriteManager.AddToLayer(mHeaderBackground, GUIHelper.UILayer);
				mCharacterRankBackground.Visible = false;
				mCharacterRankBackground.ScaleX = 60f;
				mCharacterRankBackground.ScaleY = 16.25f;
				mCharacterRankBackground.RelativePosition = new Vector3(0f, -5f, -0.1f);
				mCharacterRankBackground.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mCharacterRankBackground, GUIHelper.UILayer);
				mBodyBackground.Alpha = 0f;
				mBodyBackground.ScaleX = 60f;
				mBodyBackground.ScaleY = 16.25f;
				mBodyBackground.RelativePosition = new Vector3(0f, -5f, -0.2f);
				mBodyBackground.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mBodyBackground, GUIHelper.UILayer);
				mIsInitialized++;
				return false;
			}
			if (mIsInitialized == 3)
			{
				float xViewVolByDistance = World.Camera.GetXViewVolByDistance(75f);
				float yViewVolByDistance2 = World.Camera.GetYViewVolByDistance(75f);
				mCorner.Alpha = 0f;
				mCorner.ScaleX = 17.4375f;
				mCorner.ScaleY = 8.4375f;
				mCorner.RelativePosition = new Vector3(0f - xViewVolByDistance + mCorner.ScaleX, yViewVolByDistance2 - mCorner.ScaleY, 0f);
				mCorner.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mCorner, GUIHelper.UILayer);
				mTitle.Alpha = 0f;
				mTitle.ScaleX = 30.9375f;
				mTitle.ScaleY = 2.69999981f;
				mTitle.RelativePosition = new Vector3(-0.22f * xViewVolByDistance, mHeaderBackground.RelativePosition.Y + 0.5f, 0f);
				mTitle.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mTitle, GUIHelper.UILayer);
				mSubTitle.Alpha = 0f;
				mSubTitle.ScaleX = 9f;
				mSubTitle.ScaleY = 0.45f;
				mSubTitle.RelativePosition = new Vector3(mTitle.ScaleX + mTitle.RelativePosition.X - mSubTitle.ScaleX - 3f, mHeaderBackground.RelativePosition.Y - 3f, 0f);
				mSubTitle.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mSubTitle, GUIHelper.UILayer);
				mIsInitialized++;
				return false;
			}
			if (mIsInitialized < 10)
			{
				mIsInitialized = 10;
				return false;
			}
			if (mIsInitialized < 10 + mRows.Length)
			{
				int num = mIsInitialized - 10;
				ScoreRow scoreRow = mRows[num];
				scoreRow.Point = new Vector3(0f, 5.5f + (float)num * -4f, 0f);
				scoreRow.RelativePosition = scoreRow.Point;
				scoreRow.AttachTo(mRoot, false);
				scoreRow.OnRegister();
				scoreRow.TopicLabel.RelativePosition = new Vector3(-42f, 0f, 0f);
				scoreRow.TopicLabel.Scale = 1.3f;
				scoreRow.TopicLabel.Spacing = 1.3f;
				scoreRow.TopicLabel.ColorOperation = ColorOperation.Texture;
				scoreRow.TopicLabel.Visible = false;
				scoreRow.ScoreLabel.Scale = 1.2f;
				scoreRow.ScoreLabel.Spacing = 1.2f;
				scoreRow.ScoreLabel.RelativePosition = new Vector3(-2f, 0f, 0f);
				scoreRow.ScoreLabel.ColorOperation = ColorOperation.Texture;
				scoreRow.ScoreLabel.Visible = false;
				scoreRow.Perfect.RelativePosition = new Vector3(15f, 0f, 0f);
				scoreRow.Perfect.ScaleX = 6.25f;
				scoreRow.Perfect.ScaleY = 1.25f;
				scoreRow.Perfect.Visible = false;
				scoreRow.Medal.RelativePosition = new Vector3(7f, 0f, 0f);
				scoreRow.Medal.ScaleX = 0f;
				scoreRow.Medal.ScaleY = 1.5f;
				scoreRow.Medal.Visible = false;
				scoreRow.Visible = false;
				SpriteManager.AddPositionedObject(scoreRow);
				mIsInitialized++;
				return false;
			}
			if (mIsInitialized < 30)
			{
				mIsInitialized = 30;
				mScoreTitle.Alpha = 0f;
				mScoreTitle.ScaleX = 7.65f;
				mScoreTitle.ScaleY = 2.7f;
				mScoreTitle.RelativePosition = new Vector3(36f, 8.5f, 0f);
				mScoreTitle.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mScoreTitle, GUIHelper.UILayer);
				mScoreLabel.RelativePosition = new Vector3(36f, 5f, 0f);
				mScoreLabel.Scale = 2f;
				mScoreLabel.Spacing = 2f;
				mScoreLabel.ColorOperation = ColorOperation.Texture;
				mScoreLabel.VerticalAlignment = VerticalAlignment.Center;
				mScoreLabel.HorizontalAlignment = HorizontalAlignment.Center;
				mScoreLabel.AttachTo(mRoot, false);
				TextManager.AddToLayer(mScoreLabel, GUIHelper.UILayer);
				mRank.Visible = false;
				mRank.ScaleX = 9f;
				mRank.ScaleY = 9f;
				mRank.RelativePosition = RANK_POINT;
				mRank.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mRank, GUIHelper.UILayer);
				if (mDramaSoundEnabled)
				{
					SFXManager.PlaySound("New_MVImpact");
				}
				mIsInitialized++;
				return false;
			}
			if (mIsInitialized == 31)
			{
				mLevelUp.ScaleX = 5.75f;
				mLevelUp.ScaleY = 0.75f;
				mLevelUp.Alpha = 0f;
				mLevelUp.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mLevelUp, GUIHelper.UILayer);
				mBarBackground.ScaleX = 22.625f;
				mBarBackground.ScaleY = 1.375f;
				mBarBackground.Alpha = 0f;
				mBarBackground.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mBarBackground, GUIHelper.UILayer);
				mBar.ScaleX = 0f;
				mBar.ScaleY = 1.375f;
				mBar.Alpha = 0f;
				mBar.AttachTo(mBarBackground, false);
				SpriteManager.AddToLayer(mBar, GUIHelper.UILayer);
				mBarLeft.ScaleX = 0f;
				mBarLeft.ScaleY = 1.375f;
				mBarLeft.Alpha = 0f;
				mBarLeft.AttachTo(mBarBackground, false);
				SpriteManager.AddToLayer(mBarLeft, GUIHelper.UILayer);
				mNew.ScaleX = 2.75f;
				mNew.ScaleY = 0.75f;
				mNew.Alpha = 0f;
				mNew.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mNew, GUIHelper.UILayer);
				mCharacterName.ScaleX = 11.5625f;
				mCharacterName.ScaleY = 3.75f;
				mCharacterName.Alpha = 0f;
				mCharacterName.AttachTo(mRoot, false);
				SpriteManager.AddToLayer(mCharacterName, GUIHelper.UILayer);
				mChapterTitle.Alpha = 0f;
				mChapterTitle.Spacing = 1f;
				mChapterTitle.Scale = 1f;
				mChapterTitle.VerticalAlignment = VerticalAlignment.Center;
				mChapterTitle.HorizontalAlignment = HorizontalAlignment.Left;
				mChapterTitle.ColorOperation = ColorOperation.Texture;
				mChapterTitle.DisplayText = "章节";
				mChapterTitle.AttachTo(mRoot, false);
				TextManager.AddToLayer(mChapterTitle, GUIHelper.UILayer);
				mChapterLabel.Alpha = 0f;
				mChapterLabel.Spacing = 1f;
				mChapterLabel.Scale = 1f;
				mChapterLabel.VerticalAlignment = VerticalAlignment.Center;
				mChapterLabel.ColorOperation = ColorOperation.Texture;
				mChapterLabel.DisplayText = string.Empty;
				mChapterLabel.AttachTo(mRoot, false);
				TextManager.AddToLayer(mChapterLabel, GUIHelper.UILayer);
				mHiScoreTitle.Alpha = 0f;
				mHiScoreTitle.Spacing = 1f;
				mHiScoreTitle.Scale = 1f;
				mHiScoreTitle.VerticalAlignment = VerticalAlignment.Center;
				mHiScoreTitle.HorizontalAlignment = HorizontalAlignment.Left;
				mHiScoreTitle.ColorOperation = ColorOperation.Texture;
				mHiScoreTitle.DisplayText = "最高分";
				mHiScoreTitle.AttachTo(mRoot, false);
				TextManager.AddToLayer(mHiScoreTitle, GUIHelper.UILayer);
				mHiScoreLabel.Alpha = 0f;
				mHiScoreLabel.Spacing = 1f;
				mHiScoreLabel.Scale = 1f;
				mHiScoreLabel.VerticalAlignment = VerticalAlignment.Center;
				mHiScoreLabel.ColorOperation = ColorOperation.Texture;
				mHiScoreLabel.DisplayText = string.Empty;
				mHiScoreLabel.AttachTo(mRoot, false);
				TextManager.AddToLayer(mHiScoreLabel, GUIHelper.UILayer);
				mNewHiScoreLabel.Alpha = 0f;
				mNewHiScoreLabel.Spacing = 1f;
				mNewHiScoreLabel.Scale = 1f;
				mNewHiScoreLabel.VerticalAlignment = VerticalAlignment.Center;
				mNewHiScoreLabel.ColorOperation = ColorOperation.Texture;
				mNewHiScoreLabel.DisplayText = string.Empty;
				mNewHiScoreLabel.AttachTo(mRoot, false);
				TextManager.AddToLayer(mNewHiScoreLabel, GUIHelper.UILayer);
				mBonusLabel.Alpha = 0f;
				mBonusLabel.Spacing = 1f;
				mBonusLabel.Scale = 1f;
				mBonusLabel.VerticalAlignment = VerticalAlignment.Center;
				mBonusLabel.HorizontalAlignment = HorizontalAlignment.Left;
				mBonusLabel.ColorOperation = ColorOperation.Texture;
				mBonusLabel.DisplayText = string.Empty;
				mBonusLabel.AttachTo(mRoot, false);
				TextManager.AddToLayer(mBonusLabel, GUIHelper.UILayer);
				mLevelLabel.Alpha = 0f;
				mLevelLabel.Spacing = 1.2f;
				mLevelLabel.Scale = 1.2f;
				mLevelLabel.VerticalAlignment = VerticalAlignment.Center;
				mLevelLabel.HorizontalAlignment = HorizontalAlignment.Left;
				mLevelLabel.ColorOperation = ColorOperation.Texture;
				mLevelLabel.DisplayText = string.Empty;
				mLevelLabel.AttachTo(mRoot, false);
				TextManager.AddToLayer(mLevelLabel, GUIHelper.UILayer);
				mExperienceLabel.Alpha = 0f;
				mExperienceLabel.Spacing = 1f;
				mExperienceLabel.Scale = 1f;
				mExperienceLabel.VerticalAlignment = VerticalAlignment.Center;
				mExperienceLabel.ColorOperation = ColorOperation.Texture;
				mExperienceLabel.DisplayText = string.Empty;
				mExperienceLabel.AttachTo(mRoot, false);
				TextManager.AddToLayer(mExperienceLabel, GUIHelper.UILayer);
				mLevelUpPoint = new Vector3(41.16668f, 3.5f, 0f);
				mBarBackgroundPoint = new Vector3(24.6668f, -2f, 0f);
				mNewPoint = new Vector3(44.66714f, -9f, 0f);
				mCharacterNamePoint = new Vector3(11f, 5f, 0f);
				mChapterTitlePoint = new Vector3(8.5f, -7f, 0f);
				mChapterLabelPoint = new Vector3(39.5f, -7f, 0f);
				mHiScoreTitlePoint = new Vector3(8.5f, -9f, 0f);
				mHiScoreLabelPoint = new Vector3(39.5f, -9f, 0f);
				mBonusLabelPoint = new Vector3(10.5f, -17.33335f, 0f);
				mLevelLabelPoint = new Vector3(2.166671f, 0.6666668f, 0f);
				mExperienceLabelPoint = new Vector3(46.83342f, 1.333336f, 0f);
				mLevelUp.RelativePosition = mLevelUpPoint;
				mBarBackground.RelativePosition = mBarBackgroundPoint;
				mNew.RelativePosition = mNewPoint;
				mCharacterName.RelativePosition = mCharacterNamePoint;
				mChapterTitle.RelativePosition = mChapterTitlePoint;
				mChapterLabel.RelativePosition = mChapterLabelPoint;
				mHiScoreTitle.RelativePosition = mHiScoreTitlePoint;
				mHiScoreLabel.RelativePosition = mHiScoreLabelPoint;
				mNewHiScoreLabel.RelativePosition = mHiScoreLabelPoint;
				mBonusLabel.RelativePosition = mBonusLabelPoint;
				mLevelLabel.RelativePosition = mLevelLabelPoint;
				mExperienceLabel.RelativePosition = mExperienceLabelPoint;
				mIsInitialized++;
				return false;
			}
			mIsInManagers = true;
			CalculateScores();
			return true;
		}

		private void UpdateExperienceBar()
		{
			int num = mCharacterRank;
			if (num == 6)
			{
				num--;
			}
			int characterRankScore = GetCharacterRankScore(num);
			int characterRankScore2 = GetCharacterRankScore(num + 1);
			float num2 = mExperience;
			num2 -= (float)characterRankScore;
			float num3 = (float)(characterRankScore2 - characterRankScore);
			mExperienceLabel.DisplayText = $"{(int)mExperience} / {characterRankScore2}";
			float num4 = 1f;
			if (num3 > 0f)
			{
				num4 = Clamp01(num2 / num3);
			}
			float num5 = 0.25f;
			float num6 = mBarBackground.ScaleX - num5;
			float num7 = 0.02f;
			Vector4 textureBound = GlobalSheet.GetTextureBound(105);
			float x = textureBound.X;
			float y = textureBound.Y;
			float z = textureBound.Z;
			float w = textureBound.W;
			float num8 = z - x;
			float num9 = w - y;
			float num10 = num4;
			if (num10 < num7)
			{
				mBar.Visible = false;
			}
			else
			{
				mBar.Visible = true;
				num10 = num7;
			}
			float num11 = mBar.ScaleY / num9 * num8;
			mBarLeft.Visible = true;
			mBarLeft.ScaleX = num6 * num10;
			mBarLeft.RelativeX = 0f - num6 + mBarLeft.ScaleX + num5;
			mBarLeft.RightTextureCoordinate = x + num8 * mBarLeft.ScaleX / num11;
			if (mBar.Visible)
			{
				mBar.ScaleX = num6 * num4 - mBarLeft.ScaleX;
				mBar.RelativeX = 0f - num6 + mBar.ScaleX + num5 + mBarLeft.ScaleX;
				mBar.LeftTextureCoordinate = z - num8 * Clamp01(mBar.ScaleX / num11);
			}
			mBar.RelativeZ = 0.1f;
			mBarLeft.RelativeZ = 0.1f;
		}

		public void OnRemove()
		{
			mIsInitialized = 0;
			if (mIsInManagers)
			{
				mIsInManagers = false;
				for (int num = mRows.Length - 1; num >= 0; num--)
				{
					mRows[num].OnRemove();
					SpriteManager.RemovePositionedObject(mRows[num]);
				}
				mCorner.Detach();
				mHeaderBackground.Detach();
				mBodyBackground.Detach();
				mTitle.Detach();
				mSubTitle.Detach();
				mCharacterRankBackground.Detach();
				mLevelUp.Detach();
				mBarBackground.Detach();
				mBar.Detach();
				mBarLeft.Detach();
				mNew.Detach();
				mCharacterName.Detach();
				mChapterTitle.Detach();
				mChapterLabel.Detach();
				mHiScoreTitle.Detach();
				mHiScoreLabel.Detach();
				mNewHiScoreLabel.Detach();
				mBonusLabel.Detach();
				mLevelLabel.Detach();
				mExperienceLabel.Detach();
				mScoreTitle.Detach();
				mScoreLabel.Detach();
				mRank.Detach();
				mBlackOverlay.Detach();
				mRoot.Detach();
				SpriteManager.RemoveSpriteOneWay(mCorner);
				SpriteManager.RemoveSpriteOneWay(mHeaderBackground);
				SpriteManager.RemoveSpriteOneWay(mBodyBackground);
				SpriteManager.RemoveSpriteOneWay(mTitle);
				SpriteManager.RemoveSpriteOneWay(mSubTitle);
				SpriteManager.RemoveSpriteOneWay(mLevelUp);
				SpriteManager.RemoveSpriteOneWay(mBarBackground);
				SpriteManager.RemoveSpriteOneWay(mBar);
				SpriteManager.RemoveSpriteOneWay(mBarLeft);
				SpriteManager.RemoveSpriteOneWay(mNew);
				SpriteManager.RemoveSpriteOneWay(mCharacterName);
				TextManager.RemoveTextOneWay(mChapterTitle);
				TextManager.RemoveTextOneWay(mChapterLabel);
				TextManager.RemoveTextOneWay(mHiScoreTitle);
				TextManager.RemoveTextOneWay(mHiScoreLabel);
				TextManager.RemoveTextOneWay(mNewHiScoreLabel);
				TextManager.RemoveTextOneWay(mBonusLabel);
				TextManager.RemoveTextOneWay(mLevelLabel);
				TextManager.RemoveTextOneWay(mExperienceLabel);
				SpriteManager.RemoveSpriteOneWay(mCharacterRankBackground);
				SpriteManager.RemoveSpriteOneWay(mScoreTitle);
				TextManager.RemoveTextOneWay(mScoreLabel);
				SpriteManager.RemoveSpriteOneWay(mRank);
				SpriteManager.RemoveSpriteOneWay(mBlackOverlay);
				SpriteManager.RemovePositionedObject(mRoot);
			}
		}

		private void UpdateRowScore(ScoreRow r, double rating)
		{
			if (r.DisplayScore > r.Score)
			{
				r.DisplayScoreStep = -Math.Abs(r.DisplayScoreStep);
			}
			else
			{
				r.DisplayScoreStep = Math.Abs(r.DisplayScoreStep);
			}
			if (rating >= 1.0)
			{
				r.Perfect.TextureBound = LocalizedSheet.GetTextureBound(44);
				r.Perfect.Texture = LocalizedSheet.Texture;
				r.Medal.TextureBound = GlobalSheet.GetTextureBound(116);
			}
			else if (rating >= 0.85)
			{
				r.Perfect.TextureBound = GlobalSheet.GetTextureBound(9);
				r.Perfect.Texture = GlobalSheet.Texture;
				r.Medal.TextureBound = GlobalSheet.GetTextureBound(116);
			}
			else if (rating >= 0.75)
			{
				r.Perfect.TextureBound = GlobalSheet.GetTextureBound(9);
				r.Perfect.Texture = GlobalSheet.Texture;
				r.Medal.TextureBound = GlobalSheet.GetTextureBound(117);
			}
			else if (rating >= 0.5)
			{
				r.Perfect.TextureBound = GlobalSheet.GetTextureBound(9);
				r.Perfect.Texture = GlobalSheet.Texture;
				r.Medal.TextureBound = GlobalSheet.GetTextureBound(115);
			}
			else
			{
				r.Perfect.TextureBound = GlobalSheet.GetTextureBound(9);
				r.Perfect.Texture = GlobalSheet.Texture;
				r.Medal.TextureBound = GlobalSheet.GetTextureBound(9);
			}
		}

		private void CalculateScores()
		{
			mRows[0].TopicLabel.DisplayText = "难度设置：";
			mRows[0].Score = World.GetDifficultyScore();
			mRows[0].DisplayScore = 0;
			mRows[0].DisplayScoreStep = 99;
			mRows[0].ScoreFormat = 3;
			if (World.LowestCombatDifficulty == GameCombatDifficulty.Hardcore)
			{
				mRows[0].ScoreLabel.DisplayText = "硬核";
			}
			else if (World.LowestCombatDifficulty == GameCombatDifficulty.Normal)
			{
				mRows[0].ScoreLabel.DisplayText = "普通";
			}
			else
			{
				mRows[0].ScoreLabel.DisplayText = "休闲";
			}
			UpdateRowScore(mRows[0], World.GetDifficultyScoreRating());
			mRows[1].TopicLabel.DisplayText = "通关时间：";
			float num = 0f;
			float levelPlayTime = World.LevelPlayTime;
			num = ((levelPlayTime < World.LevelBestTime) ? World.LevelBestTime : ((!(levelPlayTime < World.LevelParTime)) ? (1f + levelPlayTime * 1.5f) : World.LevelParTime));
			float num2 = (float)World.GetTimeScore() / (levelPlayTime - num);
			float scoreOffset = (0f - num) * num2;
			mRows[1].DisplayScore = (int)num;
			mRows[1].Score = (int)levelPlayTime;
			mRows[1].DisplayScoreStep = 18;
			mRows[1].ScoreFormat = 1;
			mRows[1].ScoreFactor = num2;
			mRows[1].ScoreOffset = scoreOffset;
			UpdateRowScore(mRows[1], World.GetTimeScoreRating());
			mRows[2].TopicLabel.DisplayText = "伤害输出：";
			num = 0f;
			levelPlayTime = (float)World.DamagesByPlayer;
			if (levelPlayTime == num)
			{
				num = (float)World.LevelRequiredDealtDamages;
			}
			num2 = (float)World.GetCombatScore() / (levelPlayTime - num);
			scoreOffset = (0f - num) * num2;
			mRows[2].DisplayScore = (int)num;
			mRows[2].Score = (int)levelPlayTime;
			int num3 = (int)levelPlayTime / 15;
			num3 -= num3 % 10;
			num3--;
			if (num3 < 99)
			{
				num3 = 99;
			}
			mRows[2].DisplayScoreStep = num3;
			mRows[2].ScoreFormat = 0;
			mRows[2].ScoreFactor = 1f;
			UpdateRowScore(mRows[2], World.GetCombatScoreRating());
			mRows[3].TopicLabel.DisplayText = "被头目击中的次数：";
			num = 0f;
			levelPlayTime = (float)World.HitsByBoss;
			num = ((!(levelPlayTime < (float)World.AllowedHitsByBoss)) ? (1f + levelPlayTime * 1.5f) : ((float)World.AllowedHitsByBoss));
			num2 = (float)World.GetBossScore() / (levelPlayTime - num);
			scoreOffset = (0f - num) * num2;
			mRows[3].DisplayScore = (int)num;
			mRows[3].Score = (int)levelPlayTime;
			mRows[3].DisplayScoreStep = 1;
			mRows[3].ScoreFormat = 0;
			mRows[3].ScoreFactor = num2;
			mRows[3].ScoreOffset = scoreOffset;
			UpdateRowScore(mRows[3], World.GetBossScoreRating());
			mRows[4].TopicLabel.DisplayText = "获得的升级晶片：";
			float num4 = (float)World.GetLevelTotalChips();
			int levelTotalAcquiredChips = World.GetLevelTotalAcquiredChips();
			mRows[4].DisplayScore = 0;
			mRows[4].Score = levelTotalAcquiredChips;
			mRows[4].MaxScore = (int)num4;
			mRows[4].DisplayScoreStep = 9;
			mRows[4].ScoreFormat = 2;
			mRows[4].ScoreFactor = (float)((num4 > 0f) ? ((int)Math.Ceiling((double)((float)World.GetChipsScore() / num4))) : 0);
			UpdateRowScore(mRows[4], World.GetChipsScoreRating());
			if ((float)levelTotalAcquiredChips >= num4)
			{
				mRows[4].Perfect.TextureBound = LocalizedSheet.GetTextureBound(44);
				mRows[4].Perfect.Texture = LocalizedSheet.Texture;
				mRows[4].Medal.TextureBound = GlobalSheet.GetTextureBound(116);
			}
			else if (levelTotalAcquiredChips >= 2)
			{
				mRows[4].Perfect.TextureBound = GlobalSheet.GetTextureBound(9);
				mRows[4].Perfect.Texture = GlobalSheet.Texture;
				mRows[4].Medal.TextureBound = GlobalSheet.GetTextureBound(117);
			}
			else if (levelTotalAcquiredChips >= 1)
			{
				mRows[4].Perfect.TextureBound = GlobalSheet.GetTextureBound(9);
				mRows[4].Perfect.Texture = GlobalSheet.Texture;
				mRows[4].Medal.TextureBound = GlobalSheet.GetTextureBound(115);
			}
			else
			{
				mRows[4].Perfect.TextureBound = GlobalSheet.GetTextureBound(9);
				mRows[4].Perfect.Texture = GlobalSheet.Texture;
				mRows[4].Medal.TextureBound = GlobalSheet.GetTextureBound(9);
			}
			mRows[5].TopicLabel.DisplayText = "最高连击次数：";
			float num5 = (float)World.GetMaxComboScore();
			mRows[5].DisplayScore = 0;
			mRows[5].Score = World.MaxCombos;
			mRows[5].DisplayScoreStep = 9;
			mRows[5].ScoreFormat = 0;
			mRows[5].ScoreFactor = (float)((World.MaxCombos > 0) ? ((int)Math.Ceiling((double)(num5 / (float)World.MaxCombos))) : 0);
			UpdateRowScore(mRows[5], World.GetMaxComboScoreRating());
			if (World.TotalRepairs == 0)
			{
				AchievementManager.Instance.MarkNeverUseRepairKitForLevel(World.CurrentLevel - 1);
			}
			num = 0f;
			levelPlayTime = (float)World.TotalRepairs;
			num = ((!(levelPlayTime < 4f)) ? (1f + levelPlayTime * 1.5f) : 4f);
			num2 = (float)World.GetRepairScore() / (levelPlayTime - num);
			scoreOffset = (0f - num) * num2;
			if (Player.Instance != null && Player.Instance.Type == PlayerType.Mars)
			{
				mCharacterName.TextureBound = LocalizedSheet.GetTextureBound(40);
				mCharacterRankBackground.TextureBound = GlobalSheet.GetTextureBound(132);
			}
			else
			{
				mCharacterName.TextureBound = LocalizedSheet.GetTextureBound(46);
				mCharacterRankBackground.TextureBound = GlobalSheet.GetTextureBound(133);
			}
			int num6 = World.CurrentLevel;
			if (num6 < 1)
			{
				num6 = 1;
			}
			if (num6 >= 7)
			{
				num6 = 7;
			}
			switch (num6)
			{
			case 2:
				mChapterLabel.DisplayText = "重新启动";
				break;
			case 3:
				mChapterLabel.DisplayText = "疯狂机械";
				break;
			case 4:
				mChapterLabel.DisplayText = "落难少女";
				break;
			case 5:
				mChapterLabel.DisplayText = "地狱通道";
				break;
			case 6:
				mChapterLabel.DisplayText = "英雄末路";
				break;
			case 7:
				mChapterLabel.DisplayText = "灭绝计划";
				break;
			default:
				num6 = 1;
				mChapterLabel.DisplayText = "机械叛乱";
				break;
			}
			if (ProfileManager.Current != null)
			{
				int totalScore = World.GetTotalScore();
				hiScore = ProfileManager.Current.Score[num6 - 1];
				newHiScore = hiScore;
				if (totalScore > hiScore)
				{
					newHiScore = totalScore;
					ProfileManager.Current.Score[num6 - 1] = (short)totalScore;
				}
				mExperience = (float)ProfileManager.Current.Experience;
				int num7 = 0;
				for (int num8 = ProfileManager.Current.Score.Length - 1; num8 >= 0; num8--)
				{
					num7 += ProfileManager.Current.Score[num8];
				}
				mNewExperience = (float)num7;
				ProfileManager.Current.Experience = num7;
				ProfileManager.Current.Rank = (byte)GetCharacterRank(num7);
			}
			else
			{
				hiScore = 3000;
				newHiScore = 4000;
				mExperience = 500f;
				mNewExperience = 10000f;
			}
			mHiScoreLabel.DisplayText = hiScore.ToString();
			mNewHiScoreLabel.DisplayText = newHiScore.ToString();
			if (hiScore == newHiScore)
			{
				mNew.Visible = false;
				mNewHiScoreLabel.Visible = false;
			}
			else
			{
				mNew.Visible = true;
				mNewHiScoreLabel.Visible = true;
			}
			mCharacterRank = GetCharacterRank((int)mExperience);
			mLevelLabel.DisplayText = $"等 级 {mCharacterRank}";
			UpdateExperienceBar();
			mScore = World.GetTotalScore();
			int rank = World.GetRank();
			int key = 23 + rank;
			Vector2 textureScale = LocalizedSheet.GetTextureScale(key);
			mRank.RelativePosition = RANK_POINT;
			mRank.TextureBound = LocalizedSheet.GetTextureBound(key);
			mRank.ScaleX = mRank.ScaleY / textureScale.Y * textureScale.X;
			mExperienceLabel.HorizontalAlignment = HorizontalAlignment.Right;
			mChapterLabel.HorizontalAlignment = HorizontalAlignment.Right;
			mHiScoreLabel.HorizontalAlignment = HorizontalAlignment.Right;
			mNewHiScoreLabel.HorizontalAlignment = HorizontalAlignment.Right;
		}

		public void ShowScore(bool dramaSound)
		{
			mDramaSoundEnabled = dramaSound;
			ProcessManager.AddProcess(this);
		}

		public void OnResume()
		{
		}

		public void OnPause()
		{
		}

		public void PausedUpdate()
		{
		}

		private float Clamp01(float time)
		{
			if (!(time > 1f))
			{
				if (!(time < 0f))
				{
					return time;
				}
				return 0f;
			}
			return 1f;
		}

		private float EasingDown(float time)
		{
			float num = 1f - time;
			return num * num;
		}

		public void Update()
		{
			if (mIsInitialized < 100)
			{
				if (mIsLoaded && OnInitialize())
				{
					mIsInitialized = 100;
					mState = 1;
				}
			}
			else if (mState != 0)
			{
				if (mState == 1)
				{
					mTimer = 0f;
					mCornerPoint = mCorner.RelativePosition;
					mHeaderBackgroundPoint = mHeaderBackground.RelativePosition;
					mBodyBackgroundPoint = mBodyBackground.RelativePosition;
					mTitlePoint = mTitle.RelativePosition;
					mSubTitlePoint = mSubTitle.RelativePosition;
					mState++;
				}
				else if (mState == 2)
				{
					UpdateScore();
					mTimer += TimeManager.SecondDifference;
					float num = Clamp01(mTimer * 2f);
					float num2 = Clamp01(mTimer * 3f);
					float num3 = Clamp01(mTimer * 4f);
					float num4 = EasingDown(num);
					float num5 = EasingDown(num2);
					mCorner.Alpha = num2;
					mCorner.RelativeX = mCornerPoint.X + 10f * num5;
					mHeaderBackground.Alpha = num3;
					mBodyBackground.Alpha = num3 * 0.7f;
					mTitle.Alpha = num;
					mTitle.RelativeX = mTitlePoint.X - 5f * num4;
					mSubTitle.Alpha = num;
					mSubTitle.RelativeX = mSubTitlePoint.X + 3f * num4;
					mBlackOverlay.Alpha = num * 0.8f;
					mScoreTitle.Alpha = num;
					mScoreLabel.Alpha = num;
					if (num >= 1f)
					{
						mState++;
					}
				}
				else if (mState == 3)
				{
					mTimer = 0f;
					mState = 10;
				}
				else if (mState <= 9 + mRows.Length)
				{
					UpdateScore();
					mTimer += TimeManager.SecondDifference;
					if (mTimer >= 0.1f)
					{
						ScoreRow scoreRow = mRows[mState - 10];
						scoreRow.Medal.Alpha = 1f;
						scoreRow.Perfect.Alpha = 1f;
						scoreRow.ScoreLabel.Alpha = 1f;
						scoreRow.TopicLabel.Alpha = 1f;
						scoreRow.FlyIn();
						mTimer = 0f;
						mState++;
					}
				}
				else if (mState < 20)
				{
					mState = 20;
				}
				else if (mState == 20)
				{
					UpdateScore();
					bool flag = true;
					for (int num6 = mRows.Length - 1; num6 >= 0; num6--)
					{
						ScoreRow scoreRow2 = mRows[num6];
						if (!scoreRow2.IsIdling)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						mState++;
					}
				}
				else if (mState == 21)
				{
					mScoreLabel.DisplayText = mScore.ToString();
					mTimer = 0f;
					mState++;
				}
				else if (mState == 22)
				{
					mTimer += TimeManager.SecondDifference;
					float num7 = Clamp01(mTimer * 3f);
					float num8 = EasingDown(num7);
					mRank.Visible = true;
					mRank.Alpha = num7;
					mRank.RelativePosition = RANK_POINT + 10f * num8 * Vector3.Backward;
					if (num7 >= 1f)
					{
						mState++;
					}
				}
				else if (mState == 23)
				{
					ControlHint.Instance.Clear().AddHint(524288, "继续").ShowHints(HorizontalAlignment.Right)
						.FadeIn();
					mState++;
				}
				else if (mState == 24)
				{
					if (GamePad.GetMenuKeyDown(524288))
					{
						SFXManager.PlaySound("ok");
						mState = 30;
					}
				}
				else if (mState == 30)
				{
					mTimer = 0f;
					mState++;
				}
				else if (mState == 31)
				{
					mTimer += TimeManager.SecondDifference;
					float time = Clamp01(mTimer * 5f);
					float num9 = EasingDown(time);
					mScoreTitle.Alpha = num9;
					mScoreLabel.Alpha = num9;
					mRank.Alpha = num9;
					for (int num10 = mRows.Length - 1; num10 >= 0; num10--)
					{
						ScoreRow scoreRow3 = mRows[num10];
						scoreRow3.RelativePosition = scoreRow3.Point * num9;
						scoreRow3.TopicLabel.Alpha = num9;
						scoreRow3.Medal.Alpha = num9;
						scoreRow3.Perfect.Alpha = num9;
						scoreRow3.ScoreLabel.Alpha = num9;
					}
					if (num9 <= 0f)
					{
						mState = 40;
					}
				}
				else if (mState == 40)
				{
					mCharacterRankBackground.Alpha = 0f;
					mTimer = 0f;
					mState++;
				}
				else if (mState == 41)
				{
					mTimer += TimeManager.SecondDifference;
					float num11 = mTimer * 5f;
					EasingDown(num11);
					mCharacterRankBackground.Visible = true;
					mCharacterRankBackground.Alpha = num11;
					if (num11 >= 1f)
					{
						mState++;
					}
				}
				else if (mState == 42)
				{
					mTimer = 0f;
					mState++;
				}
				else if (mState == 43)
				{
					float num12 = 0f;
					float num13 = 0f;
					mTimer += TimeManager.SecondDifference * 3f;
					num12 = Clamp01(mTimer);
					num13 = 3f * EasingDown(num12);
					mTimer += TimeManager.SecondDifference;
					mCharacterName.Alpha = num12;
					mCharacterName.RelativeY = mCharacterNamePoint.Y + num13;
					num12 = Clamp01(mTimer - 0.2f);
					num13 = 3f * EasingDown(num12);
					mLevelLabel.Alpha = num12;
					mLevelLabel.RelativeY = mLevelLabelPoint.Y + num13;
					mExperienceLabel.Alpha = num12;
					mExperienceLabel.RelativeY = mExperienceLabelPoint.Y + num13;
					num12 = Clamp01(mTimer - 0.4f);
					num13 = 3f * EasingDown(num12);
					mBar.Alpha = num12;
					mBarLeft.Alpha = num12;
					mBarBackground.Alpha = num12;
					mBarBackground.RelativeY = mBarBackgroundPoint.Y + num13;
					num12 = Clamp01(mTimer - 0.6f);
					num13 = 3f * EasingDown(num12);
					mChapterTitle.Alpha = num12;
					mChapterTitle.RelativeY = mChapterTitlePoint.Y + num13;
					mChapterLabel.Alpha = num12;
					mChapterLabel.RelativeY = mChapterLabelPoint.Y + num13;
					num12 = Clamp01(mTimer - 0.8f);
					num13 = 3f * EasingDown(num12);
					mHiScoreTitle.Alpha = num12;
					mHiScoreTitle.RelativeY = mHiScoreTitlePoint.Y + num13;
					mHiScoreLabel.Alpha = num12;
					mHiScoreLabel.RelativeY = mHiScoreLabelPoint.Y + num13;
					if (num12 >= 1f)
					{
						mTimer = 0f;
						mState++;
					}
				}
				else if (mState == 44)
				{
					mTimer += TimeManager.SecondDifference;
					if (mTimer > 0.3f)
					{
						mTimer = 0f;
						mState = 50;
					}
				}
				else if (mState == 50)
				{
					if (!mNewHiScoreLabel.Visible)
					{
						mTimer = 0f;
						mState++;
					}
					else
					{
						mTimer += TimeManager.SecondDifference;
						float num14 = Clamp01(mTimer * 4f);
						float num15 = EasingDown(num14);
						mHiScoreLabel.Alpha = num15;
						mNewHiScoreLabel.Alpha = num14;
						mNewHiScoreLabel.Spacing = 1f + 3f * num15;
						mNewHiScoreLabel.Scale = 1f + 3f * num15;
						mNewHiScoreLabel.RelativeX = mHiScoreLabelPoint.X + 10f * num15;
						if (num14 >= 1f)
						{
							mTimer = 0f;
							mState++;
						}
					}
				}
				else if (mState == 51)
				{
					mTimer += TimeManager.SecondDifference;
					float num16 = Clamp01(mTimer * 4f);
					float num17 = EasingDown(num16);
					mNew.Alpha = num16;
					mNew.ScaleX = 2.75f * (1f + 3f * num17);
					mNew.ScaleY = 0.75f * (1f + 3f * num17);
					if (num16 > 0.2f && !mHighScoreSoundPlayed)
					{
						SFXManager.PlaySound("New_HighScore");
						mHighScoreSoundPlayed = true;
					}
					if (num16 >= 1f)
					{
						mTimer = 0f;
						skip = false;
						mState = 70;
					}
				}
				else if (mState == 70)
				{
					SFXManager.PlaySound("New_ExpCount");
					UpdateLevelUp();
					if (mExperience < mNewExperience)
					{
						float num18 = (mNewExperience - mExperience) * 0.6f;
						if (num18 < 1000f)
						{
							num18 = 1000f;
						}
						if (skip)
						{
							num18 *= 10f;
						}
						int characterRankScore = GetCharacterRankScore(mCharacterRank + 1);
						GetCharacterRankScore(mCharacterRank);
						mExperience += TimeManager.SecondDifference * num18;
						if (mExperience >= mNewExperience)
						{
							mExperience = mNewExperience;
							mState = 100;
						}
						UpdateExperienceBar();
						if (mExperience >= (float)characterRankScore && mCharacterRank < 6)
						{
							mState = 60;
						}
					}
					else
					{
						mState = 100;
					}
				}
				else if (mState == 60)
				{
					SFXManager.PlaySound("New_LevelUp");
					mCharacterRank++;
					if (mCharacterRank == 4)
					{
						AchievementManager.Instance.Notify(3, 1);
					}
					else if (mCharacterRank == 5)
					{
						AchievementManager.Instance.Notify(4, 1);
					}
					else if (mCharacterRank == 6)
					{
						AchievementManager.Instance.Notify(5, 1);
					}
					mLevelUpTimer = 0f;
					mLevelLabel.DisplayText = $"等 级 {mCharacterRank}";
					mBonusLabel.DisplayText = GetCharacterRankBonus(mCharacterRank);
					mState = 70;
				}
				else if (mState == 100)
				{
					if (skip)
					{
						ControlHint.Instance.FadeOut();
					}
					if (!UpdateLevelUp())
					{
						if (skip)
						{
							mState++;
						}
						else if (GamePad.GetMenuKeyDown(524288))
						{
							mState++;
							SFXManager.PlaySound("ok");
						}
					}
				}
				else if (mState == 101)
				{
					ControlHint.Instance.FadeOut();
					mState++;
				}
				else if (OnFinish != null)
				{
					OnFinish();
					OnFinish = null;
				}
			}
		}

		private bool UpdateLevelUp()
		{
			if (GamePad.GetMenuKeyDown(524288))
			{
				skip = true;
				SFXManager.PlaySound("ok");
			}
			if (mLevelUpTimer >= 2f)
			{
				return false;
			}
			float num = Clamp01(mLevelUpTimer * 4f);
			float num2 = EasingDown(num);
			mLevelLabel.RelativeX = mLevelLabelPoint.X - 10f * num2;
			mLevelLabel.Alpha = num;
			mLevelLabel.Spacing = 1f + 2f * num2;
			mLevelLabel.Scale = 1f + 2f * num2;
			if (mLevelUp.Alpha < num)
			{
				mLevelUp.Alpha = num;
			}
			mLevelUpTimer += TimeManager.SecondDifference;
			float num3 = Clamp01(mLevelUpTimer * 3f - 0.5f);
			float num4 = EasingDown(num3);
			mBonusLabel.Alpha = num3;
			mBonusLabel.RelativeY = mBonusLabelPoint.Y - 3f * num4;
			return true;
		}

		private void UpdateScore()
		{
			int num = 0;
			for (int num2 = mRows.Length - 1; num2 >= 0; num2--)
			{
				ScoreRow scoreRow = mRows[num2];
				scoreRow.Update();
				int num3 = (int)((float)scoreRow.DisplayScore * scoreRow.ScoreFactor + scoreRow.ScoreOffset);
				num += num3;
			}
			if (num >= mScore)
			{
				num = mScore;
			}
			if (num <= 0)
			{
				mScoreLabel.DisplayText = string.Empty;
			}
			else
			{
				if (num < 1)
				{
					num = 1;
				}
				mScoreLabel.DisplayText = num.ToString();
			}
		}
	}
}
