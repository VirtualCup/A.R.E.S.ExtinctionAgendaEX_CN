using ARES360.Entity;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ARES360.UI
{
	public class AcquireItemUI : PositionedObject, IProcessable
	{
		private const string HEADER_DATACUBE = "数据方块已取得";

		private const string HEADER_UPGRADE_CHIP = "升级项目已解锁";

		private const float DISTANCE_FROM_CAMERA = -60f;

		private const byte ITEM_DATA_CUBE = 1;

		private const byte ITEM_UPGRADE_CHIP = 2;

		private const byte ITEM_ACHIEVEMENT = 3;

		private Queue<int> mQueue;

		private byte mItemType;

		private int mDataCubeType;

		private int mUpgradeChipType;

		private float mCountdownTimer1;

		private float mLastTime;

		private bool mIsShown;

		private Sprite mPanelSprite;

		private Sprite mItemSprite;

		private Text mHeaderText;

		private Text mItemNameText;

		private Texture2D mDataCubeTexture;

		private Texture2D[] mUpgradeChipTextures;

		private float[] savedProps;

		private static AcquireItemUI mInstance;

		public static AcquireItemUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new AcquireItemUI();
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

		private AcquireItemUI()
		{
			mIsShown = false;
			savedProps = new float[5];
		}

		public void Load()
		{
			RelativePosition = new Vector3(36f, 0f, -60f);
			mQueue = new Queue<int>();
			mDataCubeTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/data_icon", "Global");
			mUpgradeChipTextures = GameUI.Instance.ChipTexture;
			mPanelSprite = GlobalSheet.CreateSprite(0);
			mPanelSprite.ScaleX = 9.5f;
			mPanelSprite.ScaleY = 4.5f;
			mPanelSprite.AttachTo(this, false);
			mItemSprite = new Sprite();
			mItemSprite.ScaleX = 2.4f;
			mItemSprite.ScaleY = 2.4f;
			mHeaderText = new Text(GUIHelper.CaptionFont);
			mHeaderText.Scale = 0.6f;
			mHeaderText.Spacing = 0.6f;
			mHeaderText.HorizontalAlignment = HorizontalAlignment.Center;
			mHeaderText.VerticalAlignment = VerticalAlignment.Center;
			mHeaderText.ColorOperation = ColorOperation.Texture;
			mHeaderText.ParentRotationChangesPosition = false;
			mHeaderText.ParentRotationChangesRotation = false;
			mHeaderText.Visible = false;
			mItemNameText = new Text(GUIHelper.CaptionFont);
			mItemNameText.Scale = 0.55f;
			mItemNameText.Spacing = 0.55f;
			mItemNameText.HorizontalAlignment = HorizontalAlignment.Center;
			mItemNameText.VerticalAlignment = VerticalAlignment.Center;
			mItemNameText.ColorOperation = ColorOperation.Texture;
			mItemNameText.Visible = false;
		}

		public void ShowDataCube(int dataCube)
		{
			mQueue.Enqueue((dataCube << 4) | 1);
			ResumeItemQueue();
		}

		public void ShowUpgradeChip(int upgradeChip)
		{
			mQueue.Enqueue((upgradeChip << 4) | 2);
			ResumeItemQueue();
		}

		public void ShowAchievement(int achievementId)
		{
			mQueue.Enqueue((achievementId << 4) | 3);
			ResumeItemQueue();
		}

		private void _Show()
		{
			if (!mIsShown)
			{
				AttachTo(World.Camera, false);
			}
			mIsShown = true;
			mCountdownTimer1 = 5f;
			mItemNameText.RelativePosition = new Vector3(-2.53f, -2.14f, 0f);
			base.RelativeRotationX = 0f;
			base.RelativeRotationXVelocity = 31.4159279f;
			RelativePosition.X = 71f;
			base.RelativeXVelocity = -43.75f;
			if (mItemType == 1)
			{
				mHeaderText.DisplayText = "数据方块已取得";
				int num = mDataCubeType;
				mItemSprite.LeftTextureCoordinate = (float)(num % 8) * 0.125f;
				mItemSprite.RightTextureCoordinate = 0.125f + (float)(num % 8) * 0.125f;
				mItemSprite.TopTextureCoordinate = (float)(num / 8) * 0.125f;
				mItemSprite.BottomTextureCoordinate = 0.125f + (float)(num / 8) * 0.125f;
				mItemSprite.Texture = mDataCubeTexture;
				mItemSprite.ScaleX = 3.6f;
				mItemSprite.ScaleY = 3.6f;
				mItemNameText.DisplayText = GUIHelper.DataCubeInfo[num].Name;
			}
			else if (mItemType == 2)
			{
				mHeaderText.DisplayText = "升级项目已解锁";
				mItemSprite.LeftTextureCoordinate = 0f;
				mItemSprite.RightTextureCoordinate = 1f;
				mItemSprite.TopTextureCoordinate = 0f;
				mItemSprite.BottomTextureCoordinate = 1f;
				mItemSprite.ScaleX = 2.4f;
				mItemSprite.ScaleY = 2.4f;
				switch (mUpgradeChipType)
				{
				case 0:
				case 1:
				case 2:
				case 3:
					mItemSprite.Texture = mUpgradeChipTextures[0];
					break;
				case 4:
				case 5:
				case 6:
				case 7:
					mItemSprite.Texture = mUpgradeChipTextures[1];
					break;
				case 8:
				case 9:
				case 10:
				case 11:
					mItemSprite.Texture = mUpgradeChipTextures[2];
					break;
				}
				mItemNameText.DisplayText = Upgrade.GetUpgradeCaptionName(Player.Instance.Type, mUpgradeChipType);
			}
			mItemSprite.Visible = false;
			ProcessManager.AddProcess(this);
		}

		public void ResumeItemQueue()
		{
			if (!IsShowing() && !AcquireAchievementUI.Instance.IsShowing() && mQueue.Count > 0)
			{
				int num = mQueue.Dequeue();
				int num2 = num & 0xF;
				int achievementId = num >> 4;
				switch (num2)
				{
				case 1:
					mItemType = 1;
					mDataCubeType = achievementId;
					_Show();
					break;
				case 2:
					mItemType = 2;
					mUpgradeChipType = achievementId;
					_Show();
					break;
				case 3:
					AcquireAchievementUI.Instance._ShowAchievement(achievementId);
					break;
				}
			}
		}

		public bool IsShowing()
		{
			return mIsShown;
		}

		private void _Hide()
		{
			Detach();
			mIsShown = false;
			ProcessManager.RemoveProcess(this);
		}

		public void OnRegister()
		{
			SpriteManager.AddPositionedObject(this);
			mHeaderText.Visible = true;
			mItemNameText.Visible = true;
			mItemSprite.Visible = true;
			mPanelSprite.Visible = true;
			SpriteManager.AddToLayer(mPanelSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mItemSprite, GUIHelper.UILayer);
			TextManager.AddToLayer(mHeaderText, GUIHelper.UILayer);
			TextManager.AddToLayer(mItemNameText, GUIHelper.UILayer);
		}

		public void OnPause()
		{
			savedProps[0] = base.RelativeXVelocity;
			base.RelativeXVelocity = 0f;
			savedProps[1] = base.RelativeRotationXVelocity;
			base.RelativeRotationXVelocity = 0f;
			savedProps[2] = mHeaderText.AlphaRate;
			mHeaderText.AlphaRate = 0f;
			mHeaderText.Visible = false;
			mItemNameText.Visible = false;
			mItemSprite.Visible = false;
			mPanelSprite.Visible = false;
		}

		public void OnResume()
		{
			base.RelativeXVelocity = savedProps[0];
			base.RelativeRotationXVelocity = savedProps[1];
			mHeaderText.AlphaRate = savedProps[2];
			mHeaderText.Visible = true;
			mItemNameText.Visible = true;
			mItemSprite.Visible = true;
			mPanelSprite.Visible = true;
		}

		public void OnRemove()
		{
			Detach();
			mIsShown = false;
			SpriteManager.RemovePositionedObject(this);
			SpriteManager.RemoveSpriteOneWay(mPanelSprite);
			SpriteManager.RemoveSpriteOneWay(mItemSprite);
			TextManager.RemoveTextOneWay(mHeaderText);
			TextManager.RemoveTextOneWay(mItemNameText);
			ResumeItemQueue();
		}

		public void Update()
		{
			mLastTime = mCountdownTimer1;
			mCountdownTimer1 -= TimeManager.SecondDifference;
			if (mCountdownTimer1 <= 0.5f)
			{
				if (mLastTime > 0.5f)
				{
					mCountdownTimer1 = 0f;
					RelativePosition.X = 70f;
					base.RelativeXVelocity = 0f;
					mHeaderText.Visible = false;
					mItemNameText.Visible = false;
					mItemSprite.Detach();
					mItemNameText.Detach();
					mHeaderText.Detach();
					_Hide();
				}
			}
			else if (mCountdownTimer1 <= 1.45f)
			{
				if (mLastTime > 1.45f)
				{
					base.RelativeXVelocity = 30f;
					mHeaderText.AlphaRate = 0f;
					mItemSprite.AlphaRate = 0f;
				}
			}
			else if (mCountdownTimer1 <= 1.65f)
			{
				if (mLastTime > 1.65f)
				{
					mHeaderText.AlphaRate = -5f;
				}
			}
			else if (mCountdownTimer1 <= 2.4f)
			{
				if (mLastTime > 2.4f)
				{
					mHeaderText.AlphaRate = 5f;
				}
			}
			else if (mCountdownTimer1 <= 2.6f)
			{
				if (mLastTime > 2.6f)
				{
					mHeaderText.AlphaRate = -5f;
				}
			}
			else if (mCountdownTimer1 <= 3.3f)
			{
				if (mLastTime > 3.3f)
				{
					mHeaderText.AlphaRate = 5f;
				}
			}
			else if (mCountdownTimer1 <= 3.5f)
			{
				if (mLastTime > 3.5f)
				{
					mHeaderText.AlphaRate = -5f;
				}
			}
			else if (mCountdownTimer1 <= 4f)
			{
				if (mLastTime > 4f)
				{
					mHeaderText.Alpha = 1f;
					mHeaderText.AlphaRate = 0f;
				}
			}
			else if (mCountdownTimer1 <= 4.2f && mLastTime > 4.2f)
			{
				base.RelativeRotationXVelocity = 0f;
				base.RelativeRotationX = 0f;
				base.RelativeXVelocity = 0f;
				mHeaderText.Visible = true;
				mHeaderText.RelativePosition = new Vector3(-2.8f, 4.7f, 0f);
				mHeaderText.Alpha = 0f;
				mHeaderText.AlphaRate = 5f;
				mHeaderText.AttachTo(this, false);
				mItemNameText.Visible = true;
				mItemNameText.RelativePosition = new Vector3(-2.53f, -2.14f, 0.1f);
				mItemNameText.AttachTo(this, false);
				mItemSprite.RelativePosition = new Vector3(-2.45f, 0.88f, 0.1f);
				mItemSprite.Visible = true;
				mItemSprite.AttachTo(this, false);
			}
		}

		public void PausedUpdate()
		{
		}
	}
}
