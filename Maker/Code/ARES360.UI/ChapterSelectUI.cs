using ARES360.Audio;
using ARES360.Input;
using ARES360.Profile;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARES360.UI
{
	public class ChapterSelectUI : PositionedObject, IProcessable
	{
		private const float CHAPTER_THUMBNAIL_GAP = 10.167f;

		private const float UNSELECTED_CHAPTER_THUMBNAIL_ALPHA = 0.5f;

		private const int CHAPTER_COUNT = 7;

		private static ChapterSelectUI mInstance;

		private PlayerProfile mSelectedProfile;

		private string[] mChapterNames;

		private string[] mChapterNumbers;

		private Sprite mMainPanelSprite;

		private Sprite[] mChapterIconSprites;

		private Texture2D[] mChapterIconTextures;

		private Texture2D mLockedChapterIconTexture;

		private Sprite mChapterThumbnailSprite;

		private Texture2D[] mChapterThumbnailTextures;

		private Texture2D mLockedChapterThumbnailTexture;

		private Sprite mRankSprite;

		private Sprite[] mAcquiredChipSprites;

		private Sprite mUnderlineSprite;

		private Clickable[] mClickables;

		private Text mChapterLabelText;

		private Text mChapterNumberText;

		private Text mChapterNameText;

		private Text mRankLabelText;

		private Text mScoreLabelText;

		private Text mScoreText;

		private Text mChapterDetailText;

		private bool[] mChapterUnlockedFlags;

		private int mSelectedChapter;

		private int mPreviousSelectedChapter;

		public VoidEventHandler OnOk;

		public VoidEventHandler OnBack;

		public static ChapterSelectUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new ChapterSelectUI();
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

		public bool IsActivated
		{
			get;
			set;
		}

		private ChapterSelectUI()
		{
			mChapterUnlockedFlags = new bool[7];
			for (int i = 0; i < 7; i++)
			{
				mChapterUnlockedFlags[i] = false;
			}
			mChapterNumbers = new string[7]
			{
				"1",
				"2",
				"3",
				"4",
				"5",
				"6",
				"7"
			};
			IsActivated = false;
		}

		private Text CreateText()
		{
			Text text = new Text(GUIHelper.CaptionFont);
			text.Scale = 1.2f;
			text.Spacing = 1.2f;
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.VerticalAlignment = VerticalAlignment.Top;
			text.ColorOperation = ColorOperation.Texture;
			return text;
		}

		public void Load()
		{
			mChapterNames = new string[7];
			for (int i = 1; i <= 7; i++)
			{
				mChapterNames[i - 1] = GUIHelper.LevelInfo[i].Name.Split('\n')[1];
			}
			mLockedChapterIconTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_00", "MenuScreen");
			mLockedChapterThumbnailTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_00", "MenuScreen");
			mChapterIconSprites = new Sprite[7];
			mChapterIconTextures = new Texture2D[7];
			mChapterThumbnailTextures = new Texture2D[7];
			mChapterIconTextures[0] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_01", "MenuScreen");
			mChapterIconTextures[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_02", "MenuScreen");
			mChapterIconTextures[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_03", "MenuScreen");
			mChapterIconTextures[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_04", "MenuScreen");
			mChapterIconTextures[4] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_05", "MenuScreen");
			mChapterIconTextures[5] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_06", "MenuScreen");
			mChapterIconTextures[6] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_icon_07", "MenuScreen");
			mChapterThumbnailTextures[0] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_01", "MenuScreen");
			mChapterThumbnailTextures[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_02", "MenuScreen");
			mChapterThumbnailTextures[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_03", "MenuScreen");
			mChapterThumbnailTextures[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_04", "MenuScreen");
			mChapterThumbnailTextures[4] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_05", "MenuScreen");
			mChapterThumbnailTextures[5] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_06", "MenuScreen");
			mChapterThumbnailTextures[6] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_07", "MenuScreen");
			for (int j = 0; j < 7; j++)
			{
				mChapterIconSprites[j] = new Sprite();
				mChapterIconSprites[j].ScaleX = 5.5f;
				mChapterIconSprites[j].ScaleY = 3f;
				mChapterIconSprites[j].Alpha = 0.8f;
				mChapterIconSprites[j].ColorOperation = ColorOperation.Texture;
				mChapterIconSprites[j].AttachTo(this, false);
				if (j == 0)
				{
					mChapterIconSprites[j].RelativePosition = new Vector3(-30.5f, -12f, 0.2f);
				}
				else
				{
					mChapterIconSprites[j].RelativePosition = mChapterIconSprites[j - 1].RelativePosition + Vector3.Right * 10.167f;
				}
			}
			mClickables = new Clickable[7];
			for (int k = 0; k < 7; k++)
			{
				mClickables[k] = new ClickableSprite
				{
					tag = k,
					sprite = mChapterIconSprites[k],
					OnMouseEnter = new Clickable.ClickableHoverHandler(OnMouseEnterChapterIcon),
					OnMouseClick = new Clickable.ClickableHandler(OnMouseClickChapterIcon)
				};
			}
			mChapterThumbnailSprite = new Sprite();
			mChapterThumbnailSprite.Texture = mChapterThumbnailTextures[mSelectedChapter];
			mChapterThumbnailSprite.ScaleX = 24f;
			mChapterThumbnailSprite.ScaleY = 14f;
			mChapterThumbnailSprite.RelativePosition = new Vector3(-13f, 4.6f, 0.1f);
			mChapterThumbnailSprite.AttachTo(this, false);
			mMainPanelSprite = new Sprite();
			mMainPanelSprite.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/chapter_main_panel"), "MenuScreen");
			mMainPanelSprite.ScaleX = 37.5f;
			mMainPanelSprite.ScaleY = 19.5f;
			mMainPanelSprite.RelativePosition = new Vector3(0f, 0f, 0.1f);
			mMainPanelSprite.AttachTo(this, false);
			mAcquiredChipSprites = new Sprite[3];
			for (int l = 0; l < 3; l++)
			{
				mAcquiredChipSprites[l] = new Sprite();
				mAcquiredChipSprites[l].ScaleX = 3f;
				mAcquiredChipSprites[l].ScaleY = 3f;
				mAcquiredChipSprites[l].Texture = null;
				mAcquiredChipSprites[l].Visible = false;
				mAcquiredChipSprites[l].AttachTo(this, false);
			}
			mAcquiredChipSprites[0].RelativePosition = new Vector3(14.7f, 8.2f, 0.11f);
			mAcquiredChipSprites[1].RelativePosition = new Vector3(22.6f, 8.2f, 0.11f);
			mAcquiredChipSprites[2].RelativePosition = new Vector3(30.5f, 8.2f, 0.11f);
			mUnderlineSprite = GlobalSheet.CreateSprite(141);
			mUnderlineSprite.ScaleX = 6.8f;
			mUnderlineSprite.ScaleY = 1.75f;
			mUnderlineSprite.RelativePosition = new Vector3(-30.5f, -15.25f, 0.1f);
			mUnderlineSprite.AttachTo(this, false);
			mRankSprite = LocalizedSheet.CreateSprite(29);
			mRankSprite.AttachTo(this, false);
			mRankSprite.ScaleY = 2.1f;
			Vector2 textureScale = LocalizedSheet.GetTextureScale(29);
			mRankSprite.ScaleX = mRankSprite.ScaleY / textureScale.Y * textureScale.X;
			mRankSprite.RelativePosition = new Vector3(4.45f, -2.8f, 0.2f);
			mChapterLabelText = CreateText();
			mChapterLabelText.RelativePosition = new Vector3(-34f, 15f, 0.2f);
			mChapterLabelText.DisplayText = "章 节";
			mChapterLabelText.AttachTo(this, false);
			mChapterNumberText = CreateText();
			mChapterNumberText.RelativePosition = new Vector3(-29f, 14.9f, 0.2f);
			mChapterNumberText.DisplayText = "-";
			mChapterNumberText.AttachTo(this, false);
			mRankLabelText = CreateText();
			mRankLabelText.RelativePosition = new Vector3(-5f, -3f, 0.2f);
			mRankLabelText.DisplayText = "等 级：";
			mRankLabelText.AttachTo(this, false);
			mScoreLabelText = CreateText();
			mScoreLabelText.RelativePosition = new Vector3(-5f, -5.2f, 0.2f);
			mScoreLabelText.DisplayText = "分 数：";
			mScoreLabelText.AttachTo(this, false);
			mScoreText = CreateText();
			mScoreText.RelativePosition = new Vector3(2.5f, -5.2f, 0.2f);
			mScoreText.DisplayText = "-";
			mScoreText.AttachTo(this, false);
			mChapterNameText = new Text(GUIHelper.CaptionFont);
			mChapterNameText.Scale = 1.7f;
			mChapterNameText.Spacing = 1.7f;
			mChapterNameText.HorizontalAlignment = HorizontalAlignment.Left;
			mChapterNameText.VerticalAlignment = VerticalAlignment.Top;
			mChapterNameText.ColorOperation = ColorOperation.Texture;
			mChapterNameText.DisplayText = "-";
			mChapterNameText.RelativePosition = new Vector3(-34f, 12.3f, 0.2f);
			mChapterNameText.AttachTo(this, false);
			mChapterDetailText = new Text(GUIHelper.SpeechFont);
			mChapterDetailText.Scale = 0.8f;
			mChapterDetailText.Spacing = 0.8f;
			mChapterDetailText.HorizontalAlignment = HorizontalAlignment.Left;
			mChapterDetailText.VerticalAlignment = VerticalAlignment.Top;
			mChapterDetailText.ColorOperation = ColorOperation.Texture;
			mChapterDetailText.DisplayText = "robots in your path. Point me a location if";
			mChapterDetailText.RelativePosition = new Vector3(11.5f, 1.5f, 0.2f);
			mChapterDetailText.AttachTo(this, false);
		}

		public void Unload()
		{
			mChapterNames = null;
			mLockedChapterIconTexture = null;
			mLockedChapterThumbnailTexture = null;
			if (mChapterIconSprites != null)
			{
				for (int i = 0; i < 7; i++)
				{
					mChapterIconSprites[i].Detach();
				}
			}
			mChapterIconSprites = null;
			mChapterIconTextures = null;
			mChapterThumbnailTextures = null;
			mChapterThumbnailSprite.Detach();
			mMainPanelSprite.Detach();
			for (int j = 0; j < 3; j++)
			{
				mAcquiredChipSprites[j].Detach();
			}
			mAcquiredChipSprites = null;
			mUnderlineSprite.Detach();
			mChapterLabelText.Detach();
			mChapterNumberText.Detach();
			mRankLabelText.Detach();
			mScoreLabelText.Detach();
			mRankSprite.Detach();
			mScoreText.Detach();
			mChapterNameText.Detach();
			mChapterDetailText.Detach();
			mUnderlineSprite = null;
			mChapterLabelText = null;
			mChapterNumberText = null;
			mRankLabelText = null;
			mScoreLabelText = null;
			mRankSprite = null;
			mScoreText = null;
			mChapterNameText = null;
			mChapterDetailText = null;
		}

		public void Show(PlayerProfile profile)
		{
			mSelectedProfile = profile;
			mSelectedChapter = profile.CurrentLevel - 1;
			for (int i = 0; i < mSelectedProfile.UnlockedLevel; i++)
			{
				mChapterUnlockedFlags[i] = true;
			}
			for (int j = mSelectedProfile.UnlockedLevel; j < 7; j++)
			{
				mChapterUnlockedFlags[j] = false;
			}
			if (!mChapterUnlockedFlags[mSelectedChapter])
			{
				mSelectedChapter = 0;
			}
			ProcessManager.AddProcess(this);
		}

		public void Hide()
		{
			ProcessManager.RemoveProcess(this);
		}

		public void OnRegister()
		{
			SpriteManager.AddToLayer(mMainPanelSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mChapterThumbnailSprite, GUIHelper.UILayer);
			for (int i = 0; i < 7; i++)
			{
				mChapterIconSprites[i].Alpha = 0.5f;
				mChapterIconSprites[i].Texture = (mChapterUnlockedFlags[i] ? mChapterIconTextures[i] : mLockedChapterIconTexture);
				SpriteManager.AddToLayer(mChapterIconSprites[i], GUIHelper.UILayer);
			}
			mChapterIconSprites[mSelectedChapter].Alpha = 1f;
			for (int j = 0; j < 3; j++)
			{
				mAcquiredChipSprites[j].AttachTo(this, false);
				SpriteManager.AddToLayer(mAcquiredChipSprites[j], GUIHelper.UILayer);
			}
			SpriteManager.AddToLayer(mUnderlineSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mRankSprite, GUIHelper.UILayer);
			TextManager.AddToLayer(mChapterLabelText, GUIHelper.UILayer);
			TextManager.AddToLayer(mChapterNumberText, GUIHelper.UILayer);
			TextManager.AddToLayer(mChapterNameText, GUIHelper.UILayer);
			TextManager.AddToLayer(mRankLabelText, GUIHelper.UILayer);
			TextManager.AddToLayer(mScoreLabelText, GUIHelper.UILayer);
			TextManager.AddToLayer(mScoreText, GUIHelper.UILayer);
			TextManager.AddToLayer(mChapterDetailText, GUIHelper.UILayer);
			UpdateVisualComponentAlongSelectedChapter();
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		public void OnRemove()
		{
			for (int i = 0; i < 7; i++)
			{
				SpriteManager.RemoveSpriteOneWay(mChapterIconSprites[i]);
			}
			for (int j = 0; j < 3; j++)
			{
				SpriteManager.RemoveSpriteOneWay(mAcquiredChipSprites[j]);
			}
			SpriteManager.RemoveSpriteOneWay(mMainPanelSprite);
			SpriteManager.RemoveSpriteOneWay(mChapterThumbnailSprite);
			SpriteManager.RemoveSpriteOneWay(mUnderlineSprite);
			SpriteManager.RemoveSpriteOneWay(mRankSprite);
			TextManager.RemoveTextOneWay(mChapterLabelText);
			TextManager.RemoveTextOneWay(mChapterNumberText);
			TextManager.RemoveTextOneWay(mChapterNameText);
			TextManager.RemoveTextOneWay(mRankLabelText);
			TextManager.RemoveTextOneWay(mScoreLabelText);
			TextManager.RemoveTextOneWay(mScoreText);
			TextManager.RemoveTextOneWay(mChapterDetailText);
		}

		private void UpdateVisualComponentAlongSelectedChapter()
		{
			mChapterIconSprites[mPreviousSelectedChapter].Alpha = 0.5f;
			mChapterIconSprites[mSelectedChapter].Alpha = 1f;
			mChapterThumbnailSprite.Texture = mChapterThumbnailTextures[mSelectedChapter];
			if (mChapterUnlockedFlags[mSelectedChapter])
			{
				mChapterThumbnailSprite.Texture = mChapterThumbnailTextures[mSelectedChapter];
			}
			else
			{
				mChapterThumbnailSprite.Texture = mLockedChapterThumbnailTexture;
			}
			mUnderlineSprite.RelativePosition.X = mChapterIconSprites[mSelectedChapter].RelativePosition.X;
			int key = 29 + World.GetRank(mSelectedProfile.Score[mSelectedChapter]);
			Vector2 textureScale = LocalizedSheet.GetTextureScale(key);
			mRankSprite.TextureBound = LocalizedSheet.GetTextureBound(key);
			mRankSprite.ScaleX = mRankSprite.ScaleY / textureScale.Y * textureScale.X;
			mChapterNumberText.DisplayText = mChapterNumbers[mSelectedChapter];
			mChapterNameText.DisplayText = mChapterNames[mSelectedChapter];
			mChapterDetailText.DisplayText = GUIHelper.LevelInfo[mSelectedChapter + 1].Detail2;
			if (mChapterUnlockedFlags[mSelectedChapter])
			{
				mScoreText.DisplayText = mSelectedProfile.Score[mSelectedChapter].ToString();
			}
			else
			{
				mScoreText.DisplayText = "-";
			}
			LevelMenuInfo levelMenuInfo = GUIHelper.LevelInfo[mSelectedChapter + 1];
			for (int i = 0; i < levelMenuInfo.ChipGeometry.Length; i++)
			{
				mAcquiredChipSprites[i].Visible = false;
				if (mSelectedProfile.AcquiredChip[mSelectedChapter + 1].Contains(i))
				{
					switch (levelMenuInfo.ChipGeometry[i].Key)
					{
					case 0:
					case 1:
					case 2:
					case 3:
						mAcquiredChipSprites[i].Texture = GameUI.Instance.ChipTexture[0];
						break;
					case 4:
					case 5:
					case 6:
					case 7:
						mAcquiredChipSprites[i].Texture = GameUI.Instance.ChipTexture[1];
						break;
					case 8:
					case 9:
					case 10:
					case 11:
						mAcquiredChipSprites[i].Texture = GameUI.Instance.ChipTexture[2];
						break;
					}
					mAcquiredChipSprites[i].Visible = true;
				}
			}
		}

		private void OnMouseEnterChapterIcon(Clickable sender)
		{
			int tag = sender.tag;
			if (mChapterUnlockedFlags[tag])
			{
				SFXManager.PlaySound("cursor");
				mPreviousSelectedChapter = mSelectedChapter;
				mSelectedChapter = tag;
				UpdateVisualComponentAlongSelectedChapter();
			}
		}

		private bool OnMouseClickChapterIcon(Clickable sender)
		{
			int tag = sender.tag;
			if (mChapterUnlockedFlags[tag])
			{
				if (mSelectedChapter != tag)
				{
					SFXManager.PlaySound("cursor");
					mPreviousSelectedChapter = mSelectedChapter;
					mSelectedChapter = tag;
					UpdateVisualComponentAlongSelectedChapter();
					return true;
				}
				ExecuteSelectedChapter();
				return true;
			}
			SFXManager.PlaySound("error");
			return true;
		}

		private void ExecuteSelectedChapter()
		{
			if (mChapterUnlockedFlags[mSelectedChapter])
			{
				mSelectedProfile.CurrentLevel = (byte)(mSelectedChapter + 1);
				if (OnOk != null)
				{
					OnOk();
					SFXManager.PlaySound("New_StartGame");
				}
			}
		}

		public void Update()
		{
			if (IsActivated)
			{
				bool flag = false;
				int num = mSelectedChapter;
				if (GamePad.GetMenuRepeatKeyDown(1) || GamePad.keyboard.GetMouseScrollUp())
				{
					num--;
					flag = true;
				}
				else if (GamePad.GetMenuRepeatKeyDown(2) || GamePad.keyboard.GetMouseScrollDown())
				{
					num++;
					flag = true;
				}
				if (flag)
				{
					if (num < 0)
					{
						num = 6;
					}
					else if (num >= 7)
					{
						num = 0;
					}
					if (mChapterUnlockedFlags[num])
					{
						SFXManager.PlaySound("cursor");
						mPreviousSelectedChapter = mSelectedChapter;
						mSelectedChapter = num;
					}
					UpdateVisualComponentAlongSelectedChapter();
				}
				if (GamePad.GetMenuKeyDown(524288))
				{
					ExecuteSelectedChapter();
				}
				else if (GamePad.GetMenuKeyDown(1048576))
				{
					if (OnBack != null)
					{
						OnBack();
						SFXManager.PlaySound("ok");
					}
					return;
				}
				GamePad.UpdateClickables(mClickables);
			}
		}

		public void PausedUpdate()
		{
		}
	}
}
