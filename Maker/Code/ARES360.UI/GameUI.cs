using ARES360.Audio;
using ARES360.Entity;
using ARES360.Input;
using ARES360.Profile;
using ARES360.Screen;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using FlatRedBall.Graphics.Model;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ARES360.UI
{
	public class GameUI : IProcessable
	{
		private enum Page
		{
			Upgrade,
			Database,
			Chapter
		}

		private enum State
		{
			Fading,
			Panning,
			Idle
		}

		public Texture2D[] ChipTexture;

		private static GameUI mInstance;

		private State mState;

		private Page mPage;

		private bool mActive;

		private float mPanTime;

		private Texture2D[] mUpgradeTexture;

		private Texture2D[] mSlotTexture;

		private Sprite mUpgradePanel;

		private Sprite[] mUpgradeItem;

		private Sprite[][] mUpgradeSlot;

		private Text[] mUpgradeName;

		private Sprite mUpgradeIcon;

		private Sprite[] mUpgradeCircle;

		private RichText mUpgradeUsage;

		private RichText mUpgradeDetail;

		private Sprite[] mUpgradeCost;

		private PositionedModel mMaterial;

		private Sprite mUpgradeCursor;

		private UpgradeMenuItem[] mUpgradeList;

		private int mUpgradeIndex;

		private Clickable[] mClickableUpgradeItems;

		private Sprite mChapterPanel;

		private Sprite[] mChapterItem;

		private Text[] mChapterName;

		private Sprite mChapterThumb;

		private Sprite[] mChipSlot;

		private Sprite[] mChip;

		private Text mRankTitle;

		private Text mRankGrade;

		private Text mChapterDetail;

		private int mChapterIndex;

		private Clickable[] mClickableChapterItems;

		private Texture2D mLockedThumb;

		private Sprite mDataPanel;

		private Sprite[] mDataIcon;

		private Text mDataName;

		private Text mDataDetail;

		private Sprite mDataThumb;

		private int mDataIndex;

		private Clickable[] mClickableDataItems;

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

		public static GameUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new GameUI();
				}
				return mInstance;
			}
		}

		public void Load()
		{
			mUpgradeTexture = new Texture2D[4];
			mUpgradeTexture[0] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_item_b", "Global");
			mUpgradeTexture[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_item_r", "Global");
			mUpgradeTexture[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_item_g", "Global");
			mUpgradeTexture[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_item_locked", "Global");
			mSlotTexture = new Texture2D[9];
			mSlotTexture[0] = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/upgrade_slot_b"), "Global");
			mSlotTexture[1] = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/upgrade_slot_r"), "Global");
			mSlotTexture[2] = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/upgrade_slot_g"), "Global");
			mSlotTexture[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_fill_b", "Global");
			mSlotTexture[4] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_fill_r", "Global");
			mSlotTexture[5] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_fill_g", "Global");
			mSlotTexture[6] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_lock_b", "Global");
			mSlotTexture[7] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_lock_r", "Global");
			mSlotTexture[8] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_lock_g", "Global");
			mUpgradePanel = new Sprite();
			mUpgradePanel.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/upgrade_panel"), "Global");
			mUpgradePanel.ScaleX = 36f;
			mUpgradePanel.ScaleY = 25f;
			mUpgradeItem = new Sprite[12];
			mUpgradeSlot = new Sprite[12][];
			mUpgradeName = new Text[12];
			mClickableUpgradeItems = new Clickable[12];
			for (int i = 0; i < 12; i++)
			{
				mUpgradeItem[i] = new Sprite();
				mUpgradeItem[i].Texture = mUpgradeTexture[i / 4];
				mUpgradeItem[i].ScaleX = 20f;
				mUpgradeItem[i].ScaleY = 3.5f;
				mUpgradeItem[i].RelativePosition = new Vector3(-15.75f, 13.5f - 3f * (float)i, 0.1f);
				mUpgradeItem[i].Red = 0.6f;
				mUpgradeItem[i].Green = 0.6f;
				mUpgradeItem[i].Blue = 0.6f;
				mUpgradeItem[i].AttachTo(mUpgradePanel, false);
				mUpgradeSlot[i] = new Sprite[3];
				for (int j = 0; j < 3; j++)
				{
					mUpgradeSlot[i][j] = new Sprite();
					mUpgradeSlot[i][j].ScaleX = 4f;
					mUpgradeSlot[i][j].ScaleY = 4f;
					mUpgradeSlot[i][j].RelativePosition = new Vector3(-10.875f + 5f * (float)j, 13.4f - 3f * (float)i, 0.11f);
					mUpgradeSlot[i][j].AttachTo(mUpgradePanel, false);
				}
				mUpgradeName[i] = new Text(GUIHelper.CaptionFont);
				mUpgradeName[i].HorizontalAlignment = HorizontalAlignment.Left;
				mUpgradeName[i].VerticalAlignment = VerticalAlignment.Center;
				mUpgradeName[i].ColorOperation = ColorOperation.Texture;
				mUpgradeName[i].Scale = 0.9f;
				mUpgradeName[i].Spacing = 0.9f;
				mUpgradeName[i].RelativePosition = new Vector3(-31.3f, 13.45f - 3f * (float)i, 0.11f);
				mUpgradeName[i].AttachTo(mUpgradePanel, false);
				mClickableUpgradeItems[i] = new ClickablePositionedObject
				{
					tag = i,
					positionedObject = mUpgradeItem[i],
					boundary = new Vector4(-20f, -1.5f, 20f, 1.5f),
					OnMouseClick = new Clickable.ClickableHandler(OnMouseClickUpgradeItem),
					OnMouseEnter = new Clickable.ClickableHoverHandler(OnMouseEnterUpgradeItem)
				};
			}
			mUpgradeCursor = new Sprite();
			mUpgradeCursor.ScaleX = 4f;
			mUpgradeCursor.ScaleY = 4f;
			mUpgradeCursor.BlendOperation = BlendOperation.Add;
			mUpgradeCursor.AttachTo(mUpgradePanel, false);
			mUpgradeCircle = new Sprite[2];
			mUpgradeCircle[0] = MenuSheet.CreateSprite(0);
			mUpgradeCircle[0].ScaleX = 11f;
			mUpgradeCircle[0].ScaleY = 11f;
			mUpgradeCircle[0].RelativePosition = new Vector3(18.5f, 2.8f, 0.2f);
			mUpgradeCircle[0].AttachTo(mUpgradePanel, false);
			mUpgradeCircle[0].RelativeRotationZVelocity = 2f;
			mUpgradeCircle[1] = MenuSheet.CreateSprite(1);
			mUpgradeCircle[1].ScaleX = 11f;
			mUpgradeCircle[1].ScaleY = 11f;
			mUpgradeCircle[1].RelativePosition = new Vector3(18.5f, 2.8f, 0.1f);
			mUpgradeCircle[1].AttachTo(mUpgradePanel, false);
			mUpgradeCircle[1].RelativeRotationZVelocity = -2f;
			mUpgradeIcon = new Sprite();
			mUpgradeIcon.ScaleX = 16.5f;
			mUpgradeIcon.ScaleY = 12.5f;
			mUpgradeIcon.RelativePosition = new Vector3(18.5f, 2.8f, 0.3f);
			mUpgradeIcon.AttachTo(mUpgradePanel, false);
			mUpgradeDetail = new RichText();
			mUpgradeDetail.buttonSize = 1.2f;
			mUpgradeDetail.lineHeight = 2.2f;
			mUpgradeDetail.fontSize = 1f;
			mUpgradeDetail.RelativePosition = new Vector3(5f, -11.9000006f, 0.1f);
			mUpgradeDetail.AttachTo(mUpgradePanel, false);
			mUpgradeUsage = new RichText();
			mUpgradeUsage.buttonSize = 1.2f;
			mUpgradeUsage.lineHeight = 2.2f;
			mUpgradeUsage.fontSize = 1f;
			mUpgradeUsage.RelativePosition = new Vector3(5f, -20.7f, 0.1f);
			mUpgradeUsage.AttachTo(mUpgradePanel, false);
			mUpgradeCost = new Sprite[4];
			for (int k = 0; k < 4; k++)
			{
				mUpgradeCost[k] = new Sprite();
				mUpgradeCost[k].ScaleX = 0.8f;
				mUpgradeCost[k].ScaleY = 1.2f;
				mUpgradeCost[k].RelativePosition = new Vector3(31.2f - 1.5f * (float)k, 17f, 0.1f);
				mUpgradeCost[k].AttachTo(mUpgradePanel, false);
			}
			mMaterial = new PositionedModel();
			mMaterial.XnaModel = ARES360.Entity.Material.Model[1];
			mMaterial.ScaleX = 0.5f;
			mMaterial.ScaleY = 0.5f;
			mMaterial.ScaleZ = 0.5f;
			mMaterial.RelativePosition = new Vector3(23.7f, 17.2f, 0f);
			mMaterial.AttachTo(mUpgradePanel, false);
			mMaterial.RelativeRotationYVelocity = 2f;
			mUpgradeList = new UpgradeMenuItem[12];
			mChapterPanel = new Sprite();
			mChapterPanel.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/chapter_panel"), "Global");
			mChapterPanel.ScaleX = 36f;
			mChapterPanel.ScaleY = 22.5f;
			mChapterItem = new Sprite[7];
			mChapterName = new Text[7];
			mClickableChapterItems = new Clickable[7];
			for (int l = 0; l < 7; l++)
			{
				mChapterItem[l] = new Sprite();
				mChapterItem[l].Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_item", "Global");
				mChapterItem[l].ScaleX = 16f;
				mChapterItem[l].ScaleY = 4f;
				mChapterItem[l].Red = 0.6f;
				mChapterItem[l].Green = 0.6f;
				mChapterItem[l].Blue = 0.6f;
				mChapterItem[l].AttachTo(mChapterPanel, false);
				mChapterItem[l].RelativePosition = new Vector3(-19f, 10.2f - (float)l * 4.6f, 0.1f);
				mChapterName[l] = new Text(GUIHelper.CaptionFont);
				mChapterName[l].Scale = 0.75f;
				mChapterName[l].Spacing = 0.75f;
				mChapterName[l].NewLineDistance = 1.6f;
				mChapterName[l].ColorOperation = ColorOperation.Texture;
				mChapterName[l].HorizontalAlignment = HorizontalAlignment.Left;
				mChapterName[l].VerticalAlignment = VerticalAlignment.Top;
				mChapterName[l].AttachTo(mChapterPanel, false);
				mChapterName[l].RelativePosition = new Vector3(-32f, 11.7f - (float)l * 4.6f, 0.2f);
				mClickableChapterItems[l] = new ClickablePositionedObject
				{
					tag = l,
					positionedObject = mChapterItem[l],
					boundary = new Vector4(-16f, -2.3f, 16f, 2.3f),
					OnMouseEnter = new Clickable.ClickableHoverHandler(OnMouseEnterChapterItem),
					OnMouseClick = new Clickable.ClickableHandler(OnMouseClickChapterItem)
				};
			}
			mChapterThumb = new Sprite();
			mChapterThumb.ScaleX = 18f;
			mChapterThumb.ScaleY = 10f;
			mChapterThumb.AttachTo(mChapterPanel, false);
			mChapterThumb.RelativePosition = new Vector3(15.35f, 2.6f, 0.1f);
			ChipTexture = new Texture2D[3];
			ChipTexture[0] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_chip1", "Global");
			ChipTexture[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_chip2", "Global");
			ChipTexture[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_chip3", "Global");
			mChipSlot = new Sprite[4];
			mChip = new Sprite[4];
			for (int m = 0; m < 4; m++)
			{
				mChipSlot[m] = new Sprite();
				mChipSlot[m].Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_chip0", "Global");
				mChipSlot[m].ScaleX = 3f;
				mChipSlot[m].ScaleY = 3f;
				mChipSlot[m].AttachTo(mChapterPanel, false);
				mChipSlot[m].RelativePosition = new Vector3(31.35f - (float)m * 3.3f, 12.95f, 0.1f);
				mChip[m] = new Sprite();
				mChip[m].ScaleX = 1.5f;
				mChip[m].ScaleY = 1.5f;
				mChip[m].AttachTo(mChapterPanel, false);
				mChip[m].RelativePosition = new Vector3(31.35f - (float)m * 3.3f, 12.95f, 0.2f);
			}
			mRankTitle = new Text(GUIHelper.CaptionFont);
			mRankTitle.Scale = 1f;
			mRankTitle.Spacing = 1f;
			mRankTitle.HorizontalAlignment = HorizontalAlignment.Left;
			mRankTitle.VerticalAlignment = VerticalAlignment.Center;
			mRankTitle.ColorOperation = ColorOperation.Texture;
			mRankTitle.DisplayText = "分数等级：";
			mRankTitle.AttachTo(mChapterPanel, false);
			mRankTitle.RelativePosition = new Vector3(20f, -4.5f, 0.2f);
			mRankGrade = new Text(GUIHelper.CaptionFont);
			mRankGrade.Scale = 1.5f;
			mRankGrade.Spacing = 1.5f;
			mRankGrade.HorizontalAlignment = HorizontalAlignment.Center;
			mRankGrade.VerticalAlignment = VerticalAlignment.Center;
			mRankGrade.ColorOperation = ColorOperation.Subtract;
			mRankGrade.AttachTo(mChapterPanel, false);
			mRankGrade.RelativePosition = new Vector3(30.1f, -4.5f, 0.2f);
			mChapterDetail = new Text(GUIHelper.SpeechFont);
			mChapterDetail.Scale = 1f;
			mChapterDetail.Spacing = 1f;
			mChapterDetail.HorizontalAlignment = HorizontalAlignment.Left;
			mChapterDetail.VerticalAlignment = VerticalAlignment.Top;
			mChapterDetail.NewLineDistance = 2.1f;
			mChapterDetail.ColorOperation = ColorOperation.Texture;
			mChapterDetail.AttachTo(mChapterPanel, false);
			mChapterDetail.RelativePosition = new Vector3(-1.7f, -9.4f, 0.1f);
			mDataPanel = new Sprite();
			mDataPanel.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/data_panel"), "Global");
			mDataPanel.ScaleX = 36f;
			mDataPanel.ScaleY = 24f;
			Texture2D texture = FlatRedBallServices.Load<Texture2D>("Content/UI/data_icon", "Global");
			mDataIcon = new Sprite[32];
			mClickableDataItems = new Clickable[32];
			for (int n = 0; n < 32; n++)
			{
				mDataIcon[n] = new Sprite();
				mDataIcon[n].Texture = texture;
				mDataIcon[n].ScaleX = 4f;
				mDataIcon[n].ScaleY = 4f;
				mDataIcon[n].AttachTo(mDataPanel, false);
				mDataIcon[n].RelativePosition = new Vector3(2.5f + (float)(n % 6) * 5.6f, 11.3f - (float)(n / 6) * 5.6f, 0.1f);
				mClickableDataItems[n] = new ClickablePositionedObject
				{
					tag = n,
					positionedObject = mDataIcon[n],
					boundary = new Vector4(-2.8f, -2.8f, 2.8f, 2.8f),
					OnMouseEnter = new Clickable.ClickableHoverHandler(OnMouseEnterDatabaseItem),
					OnMouseClick = new Clickable.ClickableHandler(OnMouseClickDatabaseItem)
				};
			}
			mDataName = new Text(GUIHelper.CaptionFont);
			mDataName.Scale = 1f;
			mDataName.Spacing = 1f;
			mDataName.HorizontalAlignment = HorizontalAlignment.Left;
			mDataName.VerticalAlignment = VerticalAlignment.Center;
			mDataName.ColorOperation = ColorOperation.Texture;
			mDataName.AttachTo(mDataPanel, false);
			mDataName.RelativePosition = new Vector3(-31f, -10.6f, 0.1f);
			mDataDetail = new Text(GUIHelper.SpeechFont);
			mDataDetail.Scale = 1f;
			mDataDetail.Spacing = 1f;
			mDataDetail.HorizontalAlignment = HorizontalAlignment.Left;
			mDataDetail.VerticalAlignment = VerticalAlignment.Top;
			mDataDetail.NewLineDistance = 2.1f;
			mDataDetail.ColorOperation = ColorOperation.Texture;
			mDataDetail.AttachTo(mDataPanel, false);
			mDataDetail.RelativePosition = new Vector3(-30.95f, -12f, 0.1f);
			mLockedThumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb00", "Global");
			mDataThumb = new Sprite();
			mDataThumb.ScaleX = 15f;
			mDataThumb.ScaleY = 12f;
			mDataThumb.AttachTo(mDataPanel, false);
			mDataThumb.RelativePosition = new Vector3(-18f, 4.75f, 0.1f);
		}

		public void Show()
		{
			MouseUI.SetCursor(1);
			SFXManager.MuteGameVolumes();
			mPage = Page.Upgrade;
			SetPageAndHint();
			MaterialUI.Instance.AutoHide = false;
			MaterialUI.Instance.Show();
			RankUI.Instance.Show();
			SFXManager.PlaySound("open_recycle_menu");
			ProcessManager.AddProcess(this);
		}

		private void MenuLeft()
		{
			mState = State.Panning;
			mPanTime = 0.25f;
			mChapterPanel.RelativeVelocity.X = 400f;
			mUpgradePanel.RelativeVelocity.X = 400f;
			mDataPanel.RelativeVelocity.X = 400f;
			ControlHint.Instance.HideHints();
			switch (mPage)
			{
			case Page.Chapter:
				mPage = Page.Database;
				break;
			case Page.Upgrade:
				mPage = Page.Chapter;
				break;
			case Page.Database:
				mPage = Page.Upgrade;
				break;
			}
			SFXManager.PlaySound("cursor");
		}

		private void MenuRight()
		{
			mState = State.Panning;
			mPanTime = 0.25f;
			mChapterPanel.RelativeVelocity.X = -400f;
			mUpgradePanel.RelativeVelocity.X = -400f;
			mDataPanel.RelativeVelocity.X = -400f;
			ControlHint.Instance.HideHints();
			switch (mPage)
			{
			case Page.Chapter:
				mPage = Page.Upgrade;
				break;
			case Page.Upgrade:
				mPage = Page.Database;
				break;
			case Page.Database:
				mPage = Page.Chapter;
				break;
			}
			SFXManager.PlaySound("cursor");
		}

		private void SetPageAndHint()
		{
			switch (mPage)
			{
			case Page.Upgrade:
				mChapterPanel.RelativePosition = new Vector3(-100f, 0.5f, -70f);
				mUpgradePanel.RelativePosition = new Vector3(0f, 3f, -70f);
				mDataPanel.RelativePosition = new Vector3(100f, 2f, -70f);
				ControlHint.Instance.Clear().AddHint(2097152, "章节").AddHint(4194304, "数据库")
					.AddHint(524288, "升级")
					.AddHint(1048576, "返回")
					.ShowHints(HorizontalAlignment.Right);
				break;
			case Page.Chapter:
				mDataPanel.RelativePosition = new Vector3(-100f, 2f, -70f);
				mChapterPanel.RelativePosition = new Vector3(0f, 0.5f, -70f);
				mUpgradePanel.RelativePosition = new Vector3(100f, 3f, -70f);
				ControlHint.Instance.Clear().AddHint(2097152, "数据库").AddHint(4194304, "升级")
					.AddHint(524288, "选择")
					.AddHint(1048576, "返回")
					.ShowHints(HorizontalAlignment.Right);
				break;
			case Page.Database:
				mUpgradePanel.RelativePosition = new Vector3(-100f, 3f, -70f);
				mDataPanel.RelativePosition = new Vector3(0f, 2f, -70f);
				mChapterPanel.RelativePosition = new Vector3(100f, 0.5f, -70f);
				ControlHint.Instance.Clear().AddHint(2097152, "升级").AddHint(4194304, "章节")
					.ShowHints(HorizontalAlignment.Right);
				break;
			}
		}

		private void SetUpgradeItem()
		{
			for (int i = 0; i < mUpgradeList.Length; i++)
			{
				int currentLevel = mUpgradeList[i].CurrentLevel;
				if (currentLevel > 0)
				{
					if (mUpgradeIndex == i)
					{
						UpgradeMenuInfo upgradeMenuInfo = GUIHelper.UpgradeInfo[(int)Player.Instance.Type][i];
						mUpgradeItem[i].Alpha = 1f;
						mUpgradeName[i].Alpha = 1f;
						if (mUpgradeList[i].CurrentLevel < mUpgradeList[i].UnlockedLevel)
						{
							mUpgradeIcon.Texture = upgradeMenuInfo.Icon[currentLevel + 1];
							mUpgradeDetail.Clear();
							Upgrade.LoadUpgradePurchaseDetailRichText(mUpgradeDetail, Player.Instance.Type, mUpgradeIndex, currentLevel);
							mUpgradeUsage.Clear();
							Upgrade.LoadUpgradeUsageRichText(mUpgradeUsage, Player.Instance.Type, mUpgradeIndex, currentLevel);
							mUpgradeCursor.Alpha = 0f;
							mUpgradeCursor.AlphaRate = 2f;
							mUpgradeCursor.Texture = mSlotTexture[i / 4];
							mUpgradeCursor.RelativePosition = mUpgradeSlot[i][currentLevel].RelativePosition;
							mMaterial.Visible = true;
							int upgradeCost = Upgrade.GetUpgradeCost(Player.Instance.Type, mUpgradeIndex, currentLevel);
							int num = 1000;
							int num2 = upgradeCost;
							for (int num3 = 3; num3 >= 0; num3--)
							{
								mUpgradeCost[num3].Visible = false;
								if (upgradeCost >= num)
								{
									mUpgradeCost[num3].Visible = true;
									mUpgradeCost[num3].Texture = LocalizedSheet.Texture;
									mUpgradeCost[num3].TextureBound = LocalizedSheet.GetTextureBound(13 + num2 / num);
									num2 %= num;
								}
								num /= 10;
							}
							if (upgradeCost <= ProfileManager.Profile.MaterialAmount)
							{
								ControlHint.Instance.SetHintActive(524288, true);
							}
							else
							{
								ControlHint.Instance.SetHintActive(524288, false);
							}
						}
						else
						{
							mUpgradeIcon.Texture = upgradeMenuInfo.Icon[currentLevel];
							mUpgradeDetail.Clear();
							Upgrade.LoadUpgradeDescriptionRichText(mUpgradeDetail, Player.Instance.Type, mUpgradeIndex, currentLevel);
							mUpgradeUsage.Clear();
							Upgrade.LoadUpgradeUsageRichText(mUpgradeUsage, Player.Instance.Type, mUpgradeIndex, currentLevel);
							mUpgradeCursor.Alpha = 0f;
							mUpgradeCursor.AlphaRate = 0f;
							mMaterial.Visible = false;
							for (int j = 0; j < 4; j++)
							{
								mUpgradeCost[j].Visible = false;
							}
							ControlHint.Instance.SetHintActive(524288, false);
						}
						for (int k = 0; k < 3; k++)
						{
							mUpgradeSlot[i][k].Alpha = 1f;
						}
					}
					else
					{
						mUpgradeItem[i].Alpha = 0.4f;
						mUpgradeName[i].Alpha = 0.7f;
						for (int l = 0; l < 3; l++)
						{
							mUpgradeSlot[i][l].Alpha = 0.5f;
							if (mUpgradeList[i].UnlockedLevel > l && mUpgradeList[i].CurrentLevel <= l)
							{
								mUpgradeSlot[i][l].Alpha = 0f;
							}
						}
					}
				}
				else
				{
					mUpgradeItem[i].Alpha = 0.5f;
					mUpgradeName[i].Alpha = 0.7f;
				}
			}
		}

		private void UpgradeCurrentIndex()
		{
			mActive = true;
			SetPageAndHint();
			int currentLevel = mUpgradeList[mUpgradeIndex].CurrentLevel;
			int upgradeCost = Upgrade.GetUpgradeCost(Player.Instance.Type, mUpgradeIndex, currentLevel);
			ProfileManager.Profile.MaterialAmount -= upgradeCost;
			SFXManager.PlaySound("success");
			mUpgradeSlot[mUpgradeIndex][mUpgradeList[mUpgradeIndex].CurrentLevel].Texture = mSlotTexture[mUpgradeIndex / 4 + 3];
			mUpgradeList[mUpgradeIndex].CurrentLevel++;
			SetUpgradeItem();
			switch (mUpgradeIndex)
			{
			case 0:
				UpgradeWeapon(WeaponType.ZytronBlaster);
				break;
			case 1:
				UpgradeWeapon(WeaponType.LaserSMG);
				break;
			case 2:
				UpgradeWeapon(WeaponType.WaveEmitter);
				break;
			case 3:
				UpgradeWeapon(WeaponType.PhotonLauncher);
				break;
			case 4:
				ProfileManager.Current.SpecialLevel++;
				break;
			case 5:
				ProfileManager.Current.RepairLevel++;
				break;
			case 6:
				ProfileManager.Current.BlastLevel++;
				break;
			case 7:
				ProfileManager.Current.ShockLevel++;
				break;
			case 8:
				ProfileManager.Current.DashLevel++;
				break;
			case 9:
				ProfileManager.Current.BoostLevel++;
				break;
			case 10:
				ProfileManager.Current.ProtectionLevel++;
				Player.Instance.IncreaseProtectionLevel();
				break;
			case 11:
				ProfileManager.Current.ArmorLevel++;
				break;
			}
			for (int i = 0; i < mUpgradeList.Length; i++)
			{
				if (mUpgradeList[i].CurrentLevel < 3)
				{
					return;
				}
			}
			AchievementManager.Instance.Notify(28, 1);
		}

		private void UpgradeWeapon(WeaponType type)
		{
			Dictionary<int, int> acquiredWeapon;
			int key;
			(acquiredWeapon = ProfileManager.Current.AcquiredWeapon)[key = (int)type] = acquiredWeapon[key] + 1;
			if (ProfileManager.Current.EquipedWeapon == type)
			{
				Player.Instance.RefreshEquippedWeapon();
			}
			if (ProfileManager.Current.AcquiredWeapon[(int)type] == 3)
			{
				AchievementManager.Instance.Notify(27, 1);
			}
		}

		private void SetChapterItem()
		{
			for (int i = 0; i < 7; i++)
			{
				if (i == mChapterIndex)
				{
					LevelMenuInfo levelMenuInfo = GUIHelper.LevelInfo[i + 1];
					mChapterItem[i].Alpha = 1f;
					mChapterName[i].Alpha = 1f;
					mChapterThumb.Texture = levelMenuInfo.Thumb;
					mChapterDetail.DisplayText = levelMenuInfo.Detail;
					for (int j = 0; j < 4; j++)
					{
						mChip[j].Visible = false;
						mChipSlot[j].Visible = false;
					}
					for (int k = 0; k < levelMenuInfo.ChipGeometry.Length; k++)
					{
						mChipSlot[k].Visible = true;
						if (ProfileManager.Current.AcquiredChip[i + 1].Contains(k))
						{
							switch (levelMenuInfo.ChipGeometry[k].Key)
							{
							case 0:
							case 1:
							case 2:
							case 3:
								mChip[k].Texture = ChipTexture[0];
								break;
							case 4:
							case 5:
							case 6:
							case 7:
								mChip[k].Texture = ChipTexture[1];
								break;
							case 8:
							case 9:
							case 10:
							case 11:
								mChip[k].Texture = ChipTexture[2];
								break;
							}
							mChip[k].Visible = true;
						}
					}
					GUIHelper.SetChapterGrade(mRankGrade, ProfileManager.Current.Score[i]);
					ControlHint.Instance.SetHintActive(524288, true);
				}
				else
				{
					mChapterItem[i].Alpha = 0.3f;
					mChapterName[i].Alpha = 0.4f;
				}
			}
		}

		private void PopUpUIAskForGotoOtherChapter(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				JumpToCurrentChapterIndex();
				break;
			case PopUpUI.PopUpUIResult.Cancel:
				OnCancel();
				break;
			}
		}

		private void PopUpUIAskForUpgradeItem(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				UpgradeCurrentIndex();
				break;
			case PopUpUI.PopUpUIResult.Cancel:
				OnCancel();
				break;
			}
		}

		private void JumpToCurrentChapterIndex()
		{
			SFXManager.PlaySound("success");
			(ScreenManager.CurrentScreen as PlayScreen).ChangeLevel((byte)(mChapterIndex + 1));
		}

		private void OnCancel()
		{
			mActive = true;
			SFXManager.PlaySound("New_MenuBack");
			SetPageAndHint();
		}

		private void SetDataItem()
		{
			for (int i = 0; i < 32; i++)
			{
				if (i == mDataIndex)
				{
					mDataIcon[i].Alpha = 1f;
					if (ProfileManager.Profile.AcquiredData.Contains(i))
					{
						mDataName.Visible = true;
						mDataDetail.Visible = true;
						mDataName.DisplayText = GUIHelper.DataCubeInfo[i].Name;
						mDataDetail.DisplayText = GUIHelper.DataCubeInfo[i].Detail;
						mDataThumb.Texture = GUIHelper.DataCubeInfo[i].Thumb;
					}
					else
					{
						mDataName.Visible = false;
						mDataDetail.Visible = false;
						mDataThumb.Texture = mLockedThumb;
					}
				}
				else
				{
					mDataIcon[i].Alpha = 0.4f;
				}
			}
		}

		public void OnRegister()
		{
			SpriteManager.AddToLayer(GUIHelper.UIMask, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mUpgradePanel, GUIHelper.UILayer);
			mUpgradePanel.AttachTo(World.Camera, false);
			for (int i = 0; i < mUpgradeList.Length; i++)
			{
				mUpgradeList[i].UnlockedLevel = ProfileManager.Current.AcqiuredUpgrade[i];
			}
			mUpgradeList[0].CurrentLevel = ProfileManager.Current.AcquiredWeapon[0];
			mUpgradeList[1].CurrentLevel = ProfileManager.Current.AcquiredWeapon[1];
			mUpgradeList[2].CurrentLevel = ProfileManager.Current.AcquiredWeapon[2];
			mUpgradeList[3].CurrentLevel = ProfileManager.Current.AcquiredWeapon[3];
			mUpgradeList[4].CurrentLevel = ProfileManager.Current.SpecialLevel;
			mUpgradeList[5].CurrentLevel = ProfileManager.Current.RepairLevel;
			mUpgradeList[6].CurrentLevel = ProfileManager.Current.BlastLevel;
			mUpgradeList[7].CurrentLevel = ProfileManager.Current.ShockLevel;
			mUpgradeList[8].CurrentLevel = ProfileManager.Current.DashLevel;
			mUpgradeList[9].CurrentLevel = ProfileManager.Current.BoostLevel;
			mUpgradeList[10].CurrentLevel = ProfileManager.Current.ProtectionLevel;
			mUpgradeList[11].CurrentLevel = ProfileManager.Current.ArmorLevel;
			for (int j = 0; j < mUpgradeList.Length; j++)
			{
				SpriteManager.AddToLayer(mUpgradeItem[j], GUIHelper.UILayer);
				TextManager.AddToLayer(mUpgradeName[j], GUIHelper.UILayer);
				if (mUpgradeList[j].CurrentLevel > 0)
				{
					for (int k = 0; k < 3; k++)
					{
						SpriteManager.AddToLayer(mUpgradeSlot[j][k], GUIHelper.UILayer);
						mUpgradeSlot[j][k].Texture = mSlotTexture[(mUpgradeList[j].UnlockedLevel > k) ? ((mUpgradeList[j].CurrentLevel > k) ? (j / 4 + 3) : (j / 4)) : (6 + j / 4 % 3)];
					}
					mUpgradeItem[j].ColorOperation = ColorOperation.Texture;
					mUpgradeName[j].DisplayText = Upgrade.GetUpgradeCaptionName(Player.Instance.Type, j);
					mUpgradeItem[j].Texture = mUpgradeTexture[j / 4];
				}
				else
				{
					mUpgradeItem[j].ColorOperation = ColorOperation.ColorTextureAlpha;
					mUpgradeName[j].DisplayText = "未解锁";
					mUpgradeItem[j].Texture = mUpgradeTexture[3];
				}
			}
			SpriteManager.AddToLayer(mUpgradeCursor, GUIHelper.UILayer);
			mUpgradeCursor.Alpha = 0f;
			mUpgradeCursor.AlphaRate = 0f;
			SpriteManager.AddToLayer(mUpgradeCircle[0], GUIHelper.UILayer);
			SpriteManager.AddToLayer(mUpgradeCircle[1], GUIHelper.UILayer);
			SpriteManager.AddToLayer(mUpgradeIcon, GUIHelper.UILayer);
			mUpgradeDetail.AddToLayer(GUIHelper.UILayer);
			mUpgradeUsage.AddToLayer(GUIHelper.UILayer);
			for (int l = 0; l < 4; l++)
			{
				SpriteManager.AddToLayer(mUpgradeCost[l], GUIHelper.UILayer);
			}
			ModelManager.AddToLayer(mMaterial, SpriteManager.TopLayer);
			SpriteManager.AddToLayer(mChapterPanel, GUIHelper.UILayer);
			mChapterPanel.AttachTo(World.Camera, false);
			for (int m = 0; m < 7; m++)
			{
				if (m < ProfileManager.Current.UnlockedLevel)
				{
					mChapterItem[m].ColorOperation = ColorOperation.Texture;
					mChapterName[m].DisplayText = GUIHelper.LevelInfo[m + 1].Name;
				}
				else
				{
					mChapterItem[m].ColorOperation = ColorOperation.ColorTextureAlpha;
					mChapterName[m].DisplayText = "未解锁";
				}
				SpriteManager.AddToLayer(mChapterItem[m], GUIHelper.UILayer);
				TextManager.AddToLayer(mChapterName[m], GUIHelper.UILayer);
			}
			SpriteManager.AddToLayer(mChapterThumb, GUIHelper.UILayer);
			for (int n = 0; n < 4; n++)
			{
				SpriteManager.AddToLayer(mChipSlot[n], GUIHelper.UILayer);
				SpriteManager.AddToLayer(mChip[n], GUIHelper.UILayer);
			}
			TextManager.AddToLayer(mRankTitle, GUIHelper.UILayer);
			TextManager.AddToLayer(mRankGrade, GUIHelper.UILayer);
			TextManager.AddToLayer(mChapterDetail, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mDataPanel, GUIHelper.UILayer);
			mDataPanel.AttachTo(World.Camera, false);
			for (int num = 0; num < 32; num++)
			{
				if (ProfileManager.Profile.AcquiredData.Contains(num))
				{
					mDataIcon[num].LeftTextureCoordinate = (float)(num % 8) * 0.125f;
					mDataIcon[num].RightTextureCoordinate = 0.125f + (float)(num % 8) * 0.125f;
					mDataIcon[num].TopTextureCoordinate = (float)(num / 8) * 0.125f;
					mDataIcon[num].BottomTextureCoordinate = 0.125f + (float)(num / 8) * 0.125f;
				}
				else
				{
					mDataIcon[num].LeftTextureCoordinate = 0.875f;
					mDataIcon[num].RightTextureCoordinate = 1f;
					mDataIcon[num].TopTextureCoordinate = 0.875f;
					mDataIcon[num].BottomTextureCoordinate = 1f;
				}
				SpriteManager.AddToLayer(mDataIcon[num], GUIHelper.UILayer);
			}
			TextManager.AddToLayer(mDataName, GUIHelper.UILayer);
			TextManager.AddToLayer(mDataDetail, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mDataThumb, GUIHelper.UILayer);
			mUpgradeIndex = 0;
			SetUpgradeItem();
			mChapterIndex = ProfileManager.Current.CurrentLevel - 1;
			SetChapterItem();
			mDataIndex = 0;
			SetDataItem();
			mActive = true;
			mState = State.Idle;
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		public void OnRemove()
		{
			SpriteManager.RemoveSpriteOneWay(GUIHelper.UIMask);
			mUpgradePanel.Detach();
			SpriteManager.RemoveSpriteOneWay(mUpgradePanel);
			for (int i = 0; i < mUpgradeList.Length; i++)
			{
				SpriteManager.RemoveSpriteOneWay(mUpgradeItem[i]);
				TextManager.RemoveTextOneWay(mUpgradeName[i]);
				if (mUpgradeList[i].CurrentLevel > 0)
				{
					for (int j = 0; j < 3; j++)
					{
						SpriteManager.RemoveSpriteOneWay(mUpgradeSlot[i][j]);
					}
				}
			}
			SpriteManager.RemoveSpriteOneWay(mUpgradeCursor);
			SpriteManager.RemoveSpriteOneWay(mUpgradeCircle[0]);
			SpriteManager.RemoveSpriteOneWay(mUpgradeCircle[1]);
			SpriteManager.RemoveSpriteOneWay(mUpgradeIcon);
			mUpgradeDetail.Clear();
			mUpgradeDetail.RemoveOneWay();
			mUpgradeUsage.Clear();
			mUpgradeUsage.RemoveOneWay();
			for (int k = 0; k < 4; k++)
			{
				SpriteManager.RemoveSpriteOneWay(mUpgradeCost[k]);
			}
			ModelManager.RemoveModel(mMaterial);
			mChapterPanel.Detach();
			SpriteManager.RemoveSpriteOneWay(mChapterPanel);
			for (int l = 0; l < 7; l++)
			{
				SpriteManager.RemoveSpriteOneWay(mChapterItem[l]);
				TextManager.RemoveTextOneWay(mChapterName[l]);
			}
			SpriteManager.RemoveSpriteOneWay(mChapterThumb);
			for (int m = 0; m < 4; m++)
			{
				SpriteManager.RemoveSpriteOneWay(mChipSlot[m]);
				SpriteManager.RemoveSpriteOneWay(mChip[m]);
			}
			TextManager.RemoveTextOneWay(mRankTitle);
			TextManager.RemoveTextOneWay(mRankGrade);
			TextManager.RemoveTextOneWay(mChapterDetail);
			mDataPanel.Detach();
			SpriteManager.RemoveSpriteOneWay(mDataPanel);
			for (int n = 0; n < 32; n++)
			{
				SpriteManager.RemoveSpriteOneWay(mDataIcon[n]);
			}
			TextManager.RemoveTextOneWay(mDataName);
			TextManager.RemoveTextOneWay(mDataDetail);
			SpriteManager.RemoveSpriteOneWay(mDataThumb);
			MaterialUI.Instance.AutoHide = true;
			ControlHint.Instance.HideHints();
		}

		public void Update()
		{
		}

		private bool OnMouseClickUpgradeItem(Clickable sender)
		{
			int tag = sender.tag;
			if (mUpgradeList[tag].CurrentLevel > 0)
			{
				if (mUpgradeIndex != tag)
				{
					mUpgradeIndex = tag;
					SetUpgradeItem();
					SFXManager.PlaySound("cursor");
					return true;
				}
				ExecuteSelectedUpgrade();
				return true;
			}
			SFXManager.PlaySound("error");
			return true;
		}

		private void OnMouseEnterUpgradeItem(Clickable sender)
		{
			int tag = sender.tag;
			if (mUpgradeList[tag].CurrentLevel > 0)
			{
				mUpgradeIndex = tag;
				SetUpgradeItem();
				SFXManager.PlaySound("cursor");
			}
		}

		private void ExecuteSelectedUpgrade()
		{
			if (mUpgradeList[mUpgradeIndex].CurrentLevel < mUpgradeList[mUpgradeIndex].UnlockedLevel)
			{
				int upgradeCost = Upgrade.GetUpgradeCost(Player.Instance.Type, mUpgradeIndex, mUpgradeList[mUpgradeIndex].CurrentLevel);
				if (upgradeCost <= ProfileManager.Profile.MaterialAmount)
				{
					mActive = false;
					SFXManager.PlaySound("ok");
					ControlHint.Instance.HideHints();
					PopUpUI.Instance.Show("升级确认", $"消耗{upgradeCost}材料对{Upgrade.GetUpgradeSpeechName(Player.Instance.Type, mUpgradeIndex)}进行升级", PopUpUIAskForUpgradeItem);
				}
				else
				{
					SFXManager.PlaySound("error");
				}
			}
			else
			{
				SFXManager.PlaySound("error");
			}
		}

		private void PausedUpdateIdleUpgrade()
		{
			if (mUpgradeCursor.AlphaRate > 0f && mUpgradeCursor.Alpha >= 1f)
			{
				mUpgradeCursor.AlphaRate = -2f;
			}
			else if (mUpgradeCursor.AlphaRate < 0f && mUpgradeCursor.Alpha <= 0f)
			{
				mUpgradeCursor.AlphaRate = 2f;
			}
			if (GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
			{
				int num = mUpgradeIndex - 1;
				while (true)
				{
					if (num < 0)
					{
						return;
					}
					if (mUpgradeList[num].CurrentLevel > 0)
					{
						break;
					}
					num--;
				}
				mUpgradeIndex = num;
				SetUpgradeItem();
				SFXManager.PlaySound("cursor");
			}
			else if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
			{
				int num = mUpgradeIndex + 1;
				while (true)
				{
					if (num >= mUpgradeList.Length)
					{
						return;
					}
					if (mUpgradeList[num].CurrentLevel > 0)
					{
						break;
					}
					num++;
				}
				mUpgradeIndex = num;
				SetUpgradeItem();
				SFXManager.PlaySound("cursor");
			}
			else if (GamePad.GetMenuKeyDown(524288))
			{
				ExecuteSelectedUpgrade();
			}
			else if (GamePad.GetMenuKeyDown(2097152))
			{
				MenuLeft();
			}
			else if (GamePad.GetMenuKeyDown(4194304))
			{
				MenuRight();
			}
			else if (GamePad.GetMenuKeyDown(1048576) || GamePad.GetMenuKeyDown(262144) || GamePad.GetMenuKeyDown(131072))
			{
				SFXManager.PlaySound("New_MenuBack");
				Hide();
			}
			else
			{
				GamePad.UpdateClickables(mClickableUpgradeItems);
			}
		}

		private bool OnMouseClickChapterItem(Clickable sender)
		{
			if (sender.tag < ProfileManager.Current.UnlockedLevel)
			{
				if (mChapterIndex != sender.tag)
				{
					mChapterIndex = sender.tag;
					SetChapterItem();
					SFXManager.PlaySound("cursor");
					return true;
				}
				ExecuteSelectedChapter();
			}
			return true;
		}

		private void OnMouseEnterChapterItem(Clickable sender)
		{
			if (sender.tag < ProfileManager.Current.UnlockedLevel)
			{
				mChapterIndex = sender.tag;
				SetChapterItem();
				SFXManager.PlaySound("cursor");
			}
		}

		private void ExecuteSelectedChapter()
		{
			if (mChapterIndex != ProfileManager.Current.CurrentLevel - 1)
			{
				SFXManager.PlaySound("ok");
				mActive = false;
				ControlHint.Instance.HideHints();
				PopUpUI.Instance.Show("中止任务", "放弃当前进度并跳至章节" + (mChapterIndex + 1) + "？", PopUpUIAskForGotoOtherChapter);
			}
			else
			{
				SFXManager.PlaySound("ok");
				mActive = false;
				ControlHint.Instance.HideHints();
				PopUpUI.Instance.Show("重开任务", "放弃当前进度并重开章节" + (mChapterIndex + 1) + "？", PopUpUIAskForGotoOtherChapter);
			}
		}

		private void PausedUpdateIdleChapter()
		{
			if (GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
			{
				if (mChapterIndex > 0)
				{
					mChapterIndex--;
					SetChapterItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
			{
				if (mChapterIndex < ProfileManager.Current.UnlockedLevel - 1)
				{
					mChapterIndex++;
					SetChapterItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuKeyDown(524288))
			{
				ExecuteSelectedChapter();
			}
			else if (GamePad.GetMenuKeyDown(2097152))
			{
				MenuLeft();
			}
			else if (GamePad.GetMenuKeyDown(4194304))
			{
				MenuRight();
			}
			else if (GamePad.GetMenuKeyDown(1048576) || GamePad.GetMenuKeyDown(262144) || GamePad.GetMenuKeyDown(131072))
			{
				SFXManager.PlaySound("New_MenuBack");
				Hide();
			}
			else
			{
				GamePad.UpdateClickables(mClickableChapterItems);
			}
		}

		private void OnMouseEnterDatabaseItem(Clickable sender)
		{
			mDataIndex = sender.tag;
			SetDataItem();
			SFXManager.PlaySound("cursor");
		}

		private bool OnMouseClickDatabaseItem(Clickable sender)
		{
			if (mDataIndex != sender.tag)
			{
				mDataIndex = sender.tag;
				SetDataItem();
				SFXManager.PlaySound("cursor");
			}
			return true;
		}

		private void PausedUpdateIdleDatabase()
		{
			if (GamePad.GetMenuRepeatKeyDown(4))
			{
				if (mDataIndex - 6 >= 0)
				{
					mDataIndex -= 6;
					SetDataItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuRepeatKeyDown(8))
			{
				if (mDataIndex + 6 < 32)
				{
					mDataIndex += 6;
					SetDataItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuRepeatKeyDown(1))
			{
				if (mDataIndex % 6 > 0)
				{
					mDataIndex--;
					SetDataItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuRepeatKeyDown(2))
			{
				if (mDataIndex % 6 < 5 && mDataIndex + 1 < 32)
				{
					mDataIndex++;
					SetDataItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.keyboard.GetMouseScrollUp())
			{
				if (mDataIndex > 0)
				{
					mDataIndex--;
					SetDataItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.keyboard.GetMouseScrollDown())
			{
				if (mDataIndex + 1 < 32)
				{
					mDataIndex++;
					SetDataItem();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuKeyDown(2097152))
			{
				MenuLeft();
			}
			else if (GamePad.GetMenuKeyDown(4194304))
			{
				MenuRight();
			}
			else if (GamePad.GetMenuKeyDown(1048576) || GamePad.GetMenuKeyDown(262144) || GamePad.GetMenuKeyDown(131072))
			{
				SFXManager.PlaySound("New_MenuBack");
				Hide();
			}
			else
			{
				GamePad.UpdateClickables(mClickableDataItems);
			}
		}

		public void PausedUpdate()
		{
			switch (mState)
			{
			case State.Panning:
				mPanTime -= TimeManager.SecondDifference;
				if (mPanTime <= 0f)
				{
					mChapterPanel.RelativeVelocity.X = 0f;
					mUpgradePanel.RelativeVelocity.X = 0f;
					mDataPanel.RelativeVelocity.X = 0f;
					SetPageAndHint();
					mState = State.Idle;
				}
				break;
			case State.Idle:
				if (mActive)
				{
					switch (mPage)
					{
					case Page.Upgrade:
						PausedUpdateIdleUpgrade();
						break;
					case Page.Chapter:
						PausedUpdateIdleChapter();
						break;
					case Page.Database:
						PausedUpdateIdleDatabase();
						break;
					}
				}
				break;
			}
		}

		public void Hide()
		{
			SFXManager.UnmuteGameVolumes();
			RankUI.Instance.Hide();
			ProcessManager.RemoveProcess(this);
			ProcessManager.Resume();
		}
	}
}
