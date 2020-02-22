using ARES360.Event;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARES360.UI
{
	public class LoadingUI : IProcessable
	{
		private const string CONTENT_MNG_NAME = "LoadingUI";

		public const byte NONE = 0;

		public const byte BACKGROUND = 1;

		public const byte OVERLAY = 2;

		private const float OVERLAY_OPACITY = 0.7f;

		private const int TOTAL_TIP_TEXTS = 15;

		private static LoadingUI mInstance;

		private float mShowTime;

		private float mActionTime;

		private float mLastActionTime;

		private float mActionTime02;

		private float mLastActionTime02;

		private bool mShowBackground;

		private Sprite mBlackOverlay;

		private bool mShowBlackOverlay;

		private Sprite mOuterRing;

		private Sprite mInnerRing;

		private Sprite mCircle;

		private PositionedObject mCirclePairPivot;

		private Sprite mCirclePair;

		private HorizontalAlignment mLoadingIconHAlign;

		private Texture2D[] mldBgTextures = new Texture2D[4];

		private Texture2D[] mldDescTextures = new Texture2D[4];

		private Texture2D[,] mldPartDescTextures = new Texture2D[4, 3];

		private PositionedObject mLDRootObject;

		private Sprite mCurrentLdBgSprite;

		private Sprite mCurrentLdDescSprite;

		private Sprite[] mCurrentLdPartDescSprites;

		private Sprite[] mCurrentLdPartPinPointSprites;

		private Text mTipText;

		private string[] mTipTextStrings = new string[15]
		{
			"任何时候你都能在选项菜单中改变游戏难度。",
			"如果你是在关卡中改变难度，\n那最后的章节评价会以最低难度为基准进行结算。",
			"激光反应器拥有最高的射击频率。\n你能用它来达成更高的连击数。",
			"电属性攻击对炸弹型和电击型敌人非常有效。",
			"收集数据方块以获得额外的信息。",
			"收集升级晶片以解锁更强力的技能。",
			"在休闲模式下，你能获得的最高评价是‘S’。\n而在普通模式下，你能获得的最高评价是‘SS’。",
			"在普通模式下，你能获得的最高分数是10000。\n而在硬核模式下，你能获得的最高分数没有上限。",
			"你可以重玩之前的章节来获得升级能力时所需要的材料。",
			"电波波发射器射出的电波能贯穿任何障碍物。",
			"你能在游戏菜单界面查看当前的生命和等级。",
			"角色的等级是根据各个章节的最高分进行计算的。\n每次升级都能提高你的最大生命值。",
			"米诺斯是这个空间站的名称，\n它会利用地球的垃圾来生产有效能源。",
			"朱莉娅·卡森博士是米诺斯空间站的首席行星工程师。",
			"深空再加工属于行星级循环利用技术，\n它能把积存的垃圾转化为有效能源。"
		};

		private float mHideTimeout;

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

		public bool LoadingDone
		{
			get;
			private set;
		}

		public static LoadingUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new LoadingUI();
				}
				return mInstance;
			}
		}

		private LoadingUI()
		{
			LoadingDone = false;
		}

		public void Load()
		{
			LoadingDone = false;
			mOuterRing = LoadingSheet.CreateSprite(1);
			mOuterRing.ScaleX = 10.875f;
			mOuterRing.ScaleY = 10.875f;
			mInnerRing = LoadingSheet.CreateSprite(2);
			mInnerRing.ScaleX = 10.875f;
			mInnerRing.ScaleY = 10.875f;
			mCircle = LoadingSheet.CreateSprite(3);
			mCircle.ScaleX = 1.1875f;
			mCircle.ScaleY = 1.1875f;
			mCirclePair = LoadingSheet.CreateSprite(4);
			mCirclePair.ScaleX = 1.78125f;
			mCirclePair.ScaleY = 0.71875f;
			mCirclePairPivot = new PositionedObject();
			mBlackOverlay = GlobalSheet.CreateSprite(8);
			mBlackOverlay.ScaleX = 177f;
			mBlackOverlay.ScaleY = 100f;
			mldBgTextures[0] = FlatRedBallServices.Load<Texture2D>("Content/UI/ld1_bg", "LoadingUI");
			mldBgTextures[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/ld2_bg", "LoadingUI");
			mldBgTextures[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/ld3_bg", "LoadingUI");
			mldBgTextures[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/ld4_bg", "LoadingUI");
			mldDescTextures[0] = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld1_desc"), "LoadingUI");
			mldDescTextures[1] = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld2_desc"), "LoadingUI");
			mldDescTextures[2] = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld3_desc"), "LoadingUI");
			mldDescTextures[3] = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld4_desc"), "LoadingUI");
			Texture2D[,] array = mldPartDescTextures;
			Texture2D texture2D = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld1_p1_desc"), "LoadingUI");
			array[0, 0] = texture2D;
			Texture2D[,] array2 = mldPartDescTextures;
			Texture2D texture2D2 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld1_p2_desc"), "LoadingUI");
			array2[0, 1] = texture2D2;
			Texture2D[,] array3 = mldPartDescTextures;
			Texture2D texture2D3 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld1_p3_desc"), "LoadingUI");
			array3[0, 2] = texture2D3;
			Texture2D[,] array4 = mldPartDescTextures;
			Texture2D texture2D4 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld2_p1_desc"), "LoadingUI");
			array4[1, 0] = texture2D4;
			Texture2D[,] array5 = mldPartDescTextures;
			Texture2D texture2D5 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld2_p2_desc"), "LoadingUI");
			array5[1, 1] = texture2D5;
			Texture2D[,] array6 = mldPartDescTextures;
			Texture2D texture2D6 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld2_p3_desc"), "LoadingUI");
			array6[1, 2] = texture2D6;
			Texture2D[,] array7 = mldPartDescTextures;
			Texture2D texture2D7 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld3_p1_desc"), "LoadingUI");
			array7[2, 0] = texture2D7;
			Texture2D[,] array8 = mldPartDescTextures;
			Texture2D texture2D8 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld3_p2_desc"), "LoadingUI");
			array8[2, 1] = texture2D8;
			Texture2D[,] array9 = mldPartDescTextures;
			Texture2D texture2D9 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld3_p3_desc"), "LoadingUI");
			array9[2, 2] = texture2D9;
			Texture2D[,] array10 = mldPartDescTextures;
			Texture2D texture2D10 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld4_p1_desc"), "LoadingUI");
			array10[3, 0] = texture2D10;
			Texture2D[,] array11 = mldPartDescTextures;
			Texture2D texture2D11 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld4_p2_desc"), "LoadingUI");
			array11[3, 1] = texture2D11;
			Texture2D[,] array12 = mldPartDescTextures;
			Texture2D texture2D12 = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/ld4_p3_desc"), "LoadingUI");
			array12[3, 2] = texture2D12;
			mLDRootObject = new PositionedObject();
			mCurrentLdBgSprite = new Sprite();
			mCurrentLdDescSprite = new Sprite();
			mCurrentLdPartDescSprites = new Sprite[3]
			{
				new Sprite(),
				new Sprite(),
				new Sprite()
			};
			mCurrentLdPartPinPointSprites = new Sprite[3]
			{
				LoadingSheet.CreateSprite(0),
				LoadingSheet.CreateSprite(0),
				LoadingSheet.CreateSprite(0)
			};
			mTipText = new Text(GUIHelper.SpeechFont);
			mTipText.ColorOperation = ColorOperation.Texture;
			mTipText.VerticalAlignment = VerticalAlignment.Top;
			mTipText.HorizontalAlignment = HorizontalAlignment.Center;
			mTipText.Scale = 1f;
			mTipText.Spacing = 1f;
			mTipText.NewLineDistance = 2f;
			LoadingDone = true;
		}

		private void ShowLoadingScene(int n)
		{
			mLDRootObject.RelativePosition = new Vector3(0f, 0f, -64.8f);
			mLDRootObject.AttachTo(World.Camera, false);
			mCurrentLdBgSprite.Alpha = 1f;
			mCurrentLdBgSprite.AlphaRate = 0f;
			mCurrentLdBgSprite.Texture = mldBgTextures[n];
			mCurrentLdBgSprite.ScaleX = 47.8125f;
			mCurrentLdBgSprite.ScaleY = 27.0625f;
			mCurrentLdBgSprite.RelativePosition = new Vector3(0f, 0f, 0f);
			mCurrentLdBgSprite.AttachTo(mLDRootObject, false);
			mCurrentLdDescSprite.Alpha = 0.8f;
			mCurrentLdDescSprite.AlphaRate = 0f;
			mCurrentLdDescSprite.Texture = mldDescTextures[n];
			mCurrentLdDescSprite.ScaleX = 16.4375f;
			mCurrentLdDescSprite.ScaleY = 9.75f;
			mCurrentLdDescSprite.RelativePosition = new Vector3(-30f, 17f, 0f);
			mCurrentLdDescSprite.AttachTo(mLDRootObject, false);
			for (int i = 0; i < 3; i++)
			{
				mCurrentLdPartDescSprites[i].Alpha = 0f;
				mCurrentLdPartDescSprites[i].AttachTo(mLDRootObject, false);
				mCurrentLdPartDescSprites[i].Texture = mldPartDescTextures[n, i];
				mCurrentLdPartPinPointSprites[i].Alpha = 0f;
				mCurrentLdPartPinPointSprites[i].AttachTo(mLDRootObject, false);
				mCurrentLdPartPinPointSprites[i].ScaleX = 1.90625f;
				mCurrentLdPartPinPointSprites[i].ScaleY = 1.90625f;
			}
			switch (n)
			{
			case 0:
				mCurrentLdPartDescSprites[0].RelativePosition = new Vector3(22.4f, -6.5f, 0f);
				mCurrentLdPartDescSprites[0].ScaleX = 22.9375f;
				mCurrentLdPartDescSprites[0].ScaleY = 12f;
				mCurrentLdPartDescSprites[1].RelativePosition = new Vector3(8.7f, 15.2f, 0f);
				mCurrentLdPartDescSprites[1].ScaleX = 20.5f;
				mCurrentLdPartDescSprites[1].ScaleY = 10.1875f;
				mCurrentLdPartDescSprites[2].RelativePosition = new Vector3(-31f, -3.3f, 0f);
				mCurrentLdPartDescSprites[2].ScaleX = 14.8125f;
				mCurrentLdPartDescSprites[2].ScaleY = 12.9375f;
				mCurrentLdPartPinPointSprites[0].RelativePosition = new Vector3(0f, 1.85f, 0f);
				mCurrentLdPartPinPointSprites[1].RelativePosition = new Vector3(29.2f, 16.5f, 0f);
				mCurrentLdPartPinPointSprites[2].RelativePosition = new Vector3(-17f, -16.2f, 0f);
				break;
			case 1:
				mCurrentLdPartDescSprites[0].RelativePosition = new Vector3(3.9f, 7f, 0f);
				mCurrentLdPartDescSprites[0].ScaleX = 17.625f;
				mCurrentLdPartDescSprites[0].ScaleY = 14.6875f;
				mCurrentLdPartDescSprites[1].RelativePosition = new Vector3(32.7f, 0f, 0f);
				mCurrentLdPartDescSprites[1].ScaleX = 10.8125f;
				mCurrentLdPartDescSprites[1].ScaleY = 19.375f;
				mCurrentLdPartDescSprites[2].RelativePosition = new Vector3(-14.4f, -4.55f, 0f);
				mCurrentLdPartDescSprites[2].ScaleX = 30.25f;
				mCurrentLdPartDescSprites[2].ScaleY = 12.8125f;
				mCurrentLdPartPinPointSprites[0].RelativePosition = new Vector3(21.5f, 19f, 0f);
				mCurrentLdPartPinPointSprites[1].RelativePosition = new Vector3(39.25f, 18.75f, 0f);
				mCurrentLdPartPinPointSprites[2].RelativePosition = new Vector3(15.8f, -12.3f, 0f);
				break;
			case 2:
				mCurrentLdPartDescSprites[0].RelativePosition = new Vector3(-20.5f, 1.25f, 0f);
				mCurrentLdPartDescSprites[0].ScaleX = 24.1875f;
				mCurrentLdPartDescSprites[0].ScaleY = 18.375f;
				mCurrentLdPartDescSprites[1].RelativePosition = new Vector3(27.8f, 6.5f, 0f);
				mCurrentLdPartDescSprites[1].ScaleX = 15.0625f;
				mCurrentLdPartDescSprites[1].ScaleY = 17.3125f;
				mCurrentLdPartDescSprites[2].RelativePosition = new Vector3(17.1f, -4.78f, 0f);
				mCurrentLdPartDescSprites[2].ScaleX = 14.5f;
				mCurrentLdPartDescSprites[2].ScaleY = 13.8125f;
				mCurrentLdPartPinPointSprites[0].RelativePosition = new Vector3(2.8f, 20f, 0f);
				mCurrentLdPartPinPointSprites[1].RelativePosition = new Vector3(42f, -11f, 0f);
				mCurrentLdPartPinPointSprites[2].RelativePosition = new Vector3(10.5f, 8.25f, 0f);
				break;
			case 3:
				mCurrentLdPartDescSprites[0].RelativePosition = new Vector3(-19.8f, -4.75f, 0f);
				mCurrentLdPartDescSprites[0].ScaleX = 25.8125f;
				mCurrentLdPartDescSprites[0].ScaleY = 13.375f;
				mCurrentLdPartDescSprites[1].RelativePosition = new Vector3(25.5f, 14.4f, 0f);
				mCurrentLdPartDescSprites[1].ScaleX = 14.25f;
				mCurrentLdPartDescSprites[1].ScaleY = 9.875f;
				mCurrentLdPartDescSprites[2].RelativePosition = new Vector3(25.8f, -8.6f, 0f);
				mCurrentLdPartDescSprites[2].ScaleX = 17.875f;
				mCurrentLdPartDescSprites[2].ScaleY = 10.9375f;
				mCurrentLdPartPinPointSprites[0].RelativePosition = new Vector3(5.5f, 6.1f, 0f);
				mCurrentLdPartPinPointSprites[1].RelativePosition = new Vector3(39.5f, 19.5f, 0f);
				mCurrentLdPartPinPointSprites[2].RelativePosition = new Vector3(42.75f, 2.4f, 0f);
				break;
			}
			mTipText.AttachTo(mLDRootObject, false);
			mTipText.RelativePosition = new Vector3(0f, -20f, 0.1f);
			mTipText.DisplayText = mTipTextStrings[FlatRedBallServices.Random.Next(15)];
			mTipText.Alpha = 0f;
			SpriteManager.AddPositionedObject(mLDRootObject);
			SpriteManager.AddToLayer(mCurrentLdBgSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mCurrentLdDescSprite, GUIHelper.UILayer);
			for (int j = 0; j < 3; j++)
			{
				SpriteManager.AddToLayer(mCurrentLdPartDescSprites[j], GUIHelper.UILayer);
				SpriteManager.AddToLayer(mCurrentLdPartPinPointSprites[j], GUIHelper.UILayer);
			}
			if (mTipText.Font == null && GUIHelper.SpeechFont != null)
			{
				mTipText.Font = GUIHelper.SpeechFont;
			}
			if (mTipText.Font != null)
			{
				TextManager.AddToLayer(mTipText, GUIHelper.UILayer);
			}
		}

		private void HideLoadingScene()
		{
			mActionTime02 = -1f;
			mCurrentLdBgSprite.AlphaRate = -2f;
			mCurrentLdDescSprite.AlphaRate = -2f;
			for (int i = 0; i < 3; i++)
			{
				mCurrentLdPartDescSprites[i].AlphaRate = -8f;
				mCurrentLdPartPinPointSprites[i].AlphaRate = -8f;
			}
			mTipText.AlphaRate = -2f;
		}

		private void RemoveLoadingScene()
		{
			mLDRootObject.Detach();
			mCurrentLdBgSprite.Detach();
			mCurrentLdDescSprite.Detach();
			SpriteManager.RemovePositionedObject(mLDRootObject);
			SpriteManager.RemoveSpriteOneWay(mCurrentLdBgSprite);
			SpriteManager.RemoveSpriteOneWay(mCurrentLdDescSprite);
			for (int i = 0; i < 3; i++)
			{
				mCurrentLdPartDescSprites[i].Detach();
				mCurrentLdPartPinPointSprites[i].Detach();
				SpriteManager.RemoveSpriteOneWay(mCurrentLdPartDescSprites[i]);
				SpriteManager.RemoveSpriteOneWay(mCurrentLdPartPinPointSprites[i]);
			}
			mTipText.Detach();
			TextManager.RemoveText(mTipText);
		}

		public void Hide(float sec)
		{
			mShowTime = sec;
		}

		public void Show(bool isLoad)
		{
			Show(isLoad, 0);
		}

		public void Show(bool isLoad, byte options)
		{
			HorizontalAlignment loadingIconHAlign = (options == 0) ? HorizontalAlignment.Right : HorizontalAlignment.Center;
			Show(isLoad, options, loadingIconHAlign);
		}

		public void Show(bool isLoad, byte options, HorizontalAlignment loadingIconHAlign)
		{
			mShowBackground = (options == 1);
			mShowBlackOverlay = (options == 2);
			mLoadingIconHAlign = loadingIconHAlign;
			if (mTipText.Font == null && GUIHelper.SpeechFont != null)
			{
				mTipText.Font = GUIHelper.SpeechFont;
			}
			if (isLoad)
			{
				mShowTime = -1f;
			}
			else
			{
				mShowTime = 0.5f;
			}
			ProcessManager.AddProcess(this);
		}

		public void Hide()
		{
			if (IsRunning)
			{
				if (Director.BlackOverlay.Alpha >= 0.8f)
				{
					Destroy();
				}
				else
				{
					if (mShowBlackOverlay)
					{
						mBlackOverlay.AlphaRate = -2f;
					}
					if (mShowBackground)
					{
						HideLoadingScene();
					}
					mActionTime = 0f;
					mHideTimeout = 0.5f;
					mOuterRing.AlphaRate = -4f;
					mInnerRing.AlphaRate = -4f;
					mCircle.AlphaRate = -4f;
					mCirclePair.AlphaRate = -4f;
				}
			}
		}

		public void OnRegister()
		{
			if (mShowBackground)
			{
				int n = FlatRedBallServices.Random.Next(4);
				ShowLoadingScene(n);
			}
			if (mShowBlackOverlay)
			{
				SpriteManager.AddToLayer(mBlackOverlay, SpriteManager.TopLayer);
				mBlackOverlay.Alpha = 0f;
				mBlackOverlay.AlphaRate = 2f;
				mBlackOverlay.RelativePosition = new Vector3(0f, 0f, -160f);
				mBlackOverlay.AttachTo(World.Camera, false);
			}
			mInnerRing.AttachTo(World.Camera, false);
			mInnerRing.Alpha = 0f;
			mInnerRing.AlphaRate = 2f;
			float num = 0f;
			float num2 = 0f;
			if (mLoadingIconHAlign == HorizontalAlignment.Right)
			{
				num2 = 0.8f * World.Camera.GetXViewVolByDistance(159f);
				num = -0.7f * World.Camera.GetYViewVolByDistance(159f);
			}
			else
			{
				num2 = 0f;
				num = -0.6f * World.Camera.GetYViewVolByDistance(159f);
			}
			mInnerRing.RelativePosition = new Vector3(num2, num, -159f);
			if (!mShowBackground)
			{
				SpriteManager.AddToLayer(mOuterRing, SpriteManager.TopLayer);
			}
			mOuterRing.AttachTo(mInnerRing, false);
			mOuterRing.Alpha = 0.1f;
			mOuterRing.AlphaRate = 2f;
			mOuterRing.RelativePosition = new Vector3(0f, 0f, -0.01f);
			if (!mShowBackground)
			{
				SpriteManager.AddToLayer(mCircle, SpriteManager.TopLayer);
			}
			mCircle.AttachTo(mInnerRing, false);
			mCircle.Alpha = 0.1f;
			mCircle.AlphaRate = 2f;
			mCircle.RelativePosition = new Vector3(0f, 0f, 0.01f);
			if (!mShowBackground)
			{
				SpriteManager.AddPositionedObject(mCirclePairPivot);
			}
			mCirclePairPivot.AttachTo(mInnerRing, false);
			mCirclePairPivot.RelativePosition = Vector3.Zero;
			if (!mShowBackground)
			{
				SpriteManager.AddToLayer(mCirclePair, SpriteManager.TopLayer);
			}
			mCirclePair.AttachTo(mCirclePairPivot, false);
			mCirclePair.Alpha = 0f;
			mCirclePair.AlphaRate = 2f;
			mCirclePair.RelativePosition = new Vector3(0f, -0.9f, 0.01f);
			mActionTime = 1f;
			mOuterRing.RelativeRotationZ = 0f;
			mOuterRing.RelativeRotationZVelocity = -2f;
			mCircle.RelativeVelocity.Y = -0.5f;
			mCircle.RelativePosition.Y = 0f;
			mCirclePairPivot.RelativeRotationZ = 5.6f;
			mCirclePairPivot.RelativeRotationZVelocity = 0f;
			mActionTime02 = 4.1f;
		}

		public void OnPause()
		{
			Hide();
		}

		public void OnResume()
		{
		}

		public void OnRemove()
		{
			mBlackOverlay.Detach();
			SpriteManager.RemoveSpriteOneWay(mBlackOverlay);
			if (mShowBackground)
			{
				RemoveLoadingScene();
			}
			mCircle.Detach();
			mCirclePair.Detach();
			mCirclePairPivot.Detach();
			mInnerRing.Detach();
			mOuterRing.Detach();
			SpriteManager.RemoveSpriteOneWay(mCircle);
			SpriteManager.RemoveSpriteOneWay(mCirclePair);
			SpriteManager.RemovePositionedObject(mCirclePairPivot);
			SpriteManager.RemoveSpriteOneWay(mInnerRing);
			SpriteManager.RemoveSpriteOneWay(mOuterRing);
		}

		public void Update()
		{
			if (mBlackOverlay.AlphaRate > 0f && mBlackOverlay.Alpha >= 0.7f)
			{
				mBlackOverlay.Alpha = 0.7f;
				mBlackOverlay.AlphaRate = 0f;
			}
			if (mShowTime > 0f)
			{
				mShowTime -= TimeManager.SecondDifference;
				if (mShowTime <= 0f)
				{
					mShowTime = 0f;
					Hide();
				}
			}
			else if (mShowBackground)
			{
				if (mCurrentLdBgSprite.Alpha == 0f)
				{
					ProcessManager.RemoveProcess(this);
					return;
				}
			}
			else if (mOuterRing.Alpha == 0f)
			{
				ProcessManager.RemoveProcess(this);
				return;
			}
			if (mShowBackground)
			{
				if (mActionTime02 < 3.5f)
				{
					for (int i = 0; i < 3; i++)
					{
						if (mCurrentLdPartPinPointSprites[i].Alpha > 0.9f)
						{
							mCurrentLdPartPinPointSprites[i].AlphaRate = -1f;
						}
						else if (mCurrentLdPartPinPointSprites[i].Alpha < 0.1f)
						{
							mCurrentLdPartPinPointSprites[i].AlphaRate = 1f;
						}
						if (mCurrentLdPartDescSprites[i].Alpha > 0.9f)
						{
							mCurrentLdPartDescSprites[i].AlphaRate = -2f;
						}
						else if (mCurrentLdPartDescSprites[i].Alpha < 0.85f)
						{
							mCurrentLdPartDescSprites[i].AlphaRate = 2f;
						}
					}
				}
				else if (mActionTime02 > 0f)
				{
					mLastActionTime02 = mActionTime02;
					mActionTime02 -= TimeManager.SecondDifference;
					if (mActionTime02 <= 0f)
					{
						mActionTime02 = 0f;
					}
					else if (mActionTime02 <= 3.5f)
					{
						if (mLastActionTime02 > 3.5f)
						{
							mCurrentLdPartDescSprites[2].AlphaRate = 8f;
						}
					}
					else if (mActionTime02 <= 3.6f)
					{
						if (mLastActionTime02 > 3.6f)
						{
							mCurrentLdPartDescSprites[1].AlphaRate = 8f;
						}
					}
					else if (mActionTime02 <= 3.7f)
					{
						if (mLastActionTime02 > 3.7f)
						{
							mCurrentLdPartDescSprites[0].AlphaRate = 8f;
						}
					}
					else if (mActionTime02 <= 3.8f)
					{
						if (mLastActionTime02 > 3.8f)
						{
							mCurrentLdPartPinPointSprites[2].AlphaRate = 6f;
							mTipText.AlphaRate = 0f;
							mTipText.Alpha = 1f;
						}
					}
					else if (mActionTime02 <= 3.9f)
					{
						if (mLastActionTime02 > 3.9f)
						{
							mCurrentLdPartPinPointSprites[1].AlphaRate = 6f;
						}
					}
					else if (mActionTime02 <= 4f && mLastActionTime02 > 4f)
					{
						mCurrentLdPartPinPointSprites[0].AlphaRate = 6f;
						mTipText.AlphaRate = 2f;
					}
				}
			}
			mLastActionTime = mActionTime;
			mActionTime -= TimeManager.SecondDifference;
			if (mActionTime <= 0f)
			{
				mActionTime = 1f;
				mOuterRing.RelativeRotationZVelocity = -0.7252f;
				mCircle.RelativeVelocity.Y = -0.5f;
				mCircle.RelativePosition.Y = 0f;
			}
			else if (mActionTime <= 0.3f)
			{
				if (mLastActionTime > 0.3f)
				{
					mCirclePairPivot.RelativeRotationZVelocity = 0.653f;
					mCirclePairPivot.RelativeRotationZ = 5.078f;
					mCircle.RelativeVelocity.Y = 0.5f;
					mCircle.RelativePosition.Y = -0.2f;
				}
			}
			else if (mActionTime <= 0.36f)
			{
				if (mLastActionTime > 0.36f)
				{
					mOuterRing.RelativeRotationZVelocity = -0.7252f;
					mOuterRing.RelativeRotationZ = 5.742f;
				}
			}
			else if (mActionTime <= 0.4f)
			{
				if (mLastActionTime > 0.4f)
				{
					mCirclePairPivot.RelativeRotationZVelocity = 52.22f;
				}
			}
			else if (mActionTime <= 0.5f)
			{
				if (mLastActionTime > 0.5f)
				{
					mCircle.RelativePosition.Y = -1f;
					mCircle.RelativeVelocity.Y = 4f;
					mCirclePairPivot.RelativeRotationZVelocity = 5.222f;
					mCirclePairPivot.RelativeRotationZ = 5.6f;
				}
			}
			else if (mActionTime <= 0.64f)
			{
				if (mLastActionTime > 0.64f)
				{
					mOuterRing.RelativeRotationZVelocity = -20.5f;
					mOuterRing.RelativeRotationZ = 0f;
				}
			}
			else if (mActionTime <= 0.7f && mLastActionTime > 0.7f)
			{
				mCircle.RelativeVelocity.Y = -4f;
				mCircle.RelativePosition.Y = -0.2f;
			}
		}

		public void Destroy()
		{
			ProcessManager.RemoveProcess(this);
		}

		public void PausedUpdate()
		{
			Update();
		}
	}
}
