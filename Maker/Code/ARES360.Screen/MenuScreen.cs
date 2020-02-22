using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.UI;
using ARES360Loader;
using ARES360Loader.Data.Effect;
using ARES360Loader.Data.World;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ARES360.Screen
{
	public class MenuScreen : Screen
	{
		private enum Page
		{
			None,
			Main,
			SelectCharacter,
			HelpAndOptions,
			Leaderboard,
			Achievements,
			UnlockFullVersion,
			Marketplace,
			Exit,
			SelectChapter,
			EnterPlayScreen,
			BackToStartScreen
		}

		private class ClickableMenuItem : ClickableText
		{
			public int index;
		}

		private const float DISTANCE_FROM_CAMERA = -70f;

		private const int MAINMENU_ITEM_STARTGAME = 1;

		private const int MAINMENU_ITEM_HELP_OPTIONS = 2;

		private const int MAINMENU_ITEM_LEADERBOARD = 3;

		private const int MAINMENU_ITEM_ACHIEVEMENT = 4;

		private const int MAINMENU_ITEM_EXIT = 5;

		private const int MAINMENU_ITEM_UNLOCK_FULLVERSION = 6;

		private const int MAINMENU_ITEM_START_POS_X = 14;

		private const int MAINMENU_ITEM_START_POS_Y = 1;

		private const int MAINMENU_CURSOR_MOVE_Y_STEP_DISTANCE = 3;

		private const float MAINMENU_CURSOR_MOVE_Y_TIME = 0.05f;

		private const float MAINMENU_CURSOR_MOVE_Y_VELOCITY = 60f;

		private const int SELECTCHAR_SELECT_MARS_INDEX = 0;

		private const int SELECTCHAR_SELECT_TARUS_INDEX = 1;

		private const int SELECTCHAR_SELECT_NONE_INDEX = -1;

		private const float PAN_SPEED = 400f;

		private static MenuScreen mInstance;

		private Page mPage;

		private Page mRequestNextPage;

		private Page mPreviousPage;

		private bool mIsJustChangePage;

		private bool mIsTransitionBetweenPages;

		private bool mIsTransitionEnd;

		private int mExitStep;

		private Sprite mBlackCoverSprite;

		private bool mIsBackgroundSceneWorldLoaded;

		private TimedEmitParticle mBackgroundSceneParticle;

		private float mCountdownTimer1;

		private float mLastCountdownTimer1;

		private Dictionary<int, string> mControllerHint;

		private Stack<Page> mPageNavigateStack;

		private Mars mMars;

		private Tarus mTarus;

		private float mDelayBeforeExit;

		private int mMainMenuCursor;

		private int mMainMenuItemCount;

		private float mMainMenuCursorMovingTimeLeft;

		private PositionedObject mMainMenuRootObject;

		private Sprite mMainMenuCursorSprite;

		private Sprite mMainMenuARESLogoSprite;

		private Sprite mDarkBackgroundSprite;

		private List<ClickableMenuItem> mMainMenuItems;

		private int mCurrentSelectedCharacterIndex;

		private PlayerProfile mCurrentSelectPlayerProfile;

		private PositionedObject mSelectCharRootObject;

		private Sprite mSelectCharHeaderSprite;

		private Sprite mSelectCharARESLogoSprite;

		private Sprite mSelectCharAresSprite;

		private Sprite mSelectCharTarusSprite;

		private Sprite mSelectCharAresSelectedFrameSprite;

		private Sprite mSelectCharTarusSelectedFrameSprite;

		private Sprite mSelectCharAresSelectedOuterFrameSprite;

		private Sprite mSelectCharTarusSelectedOuterFrameSprite;

		private Sprite mSelectCharBackgroundPanelSprite;

		private CharacterDetailPanel mCharacterDetailPanel;

		private ClickableText mSelectCharResetProgress;

		private bool IsGoToPlayScreenAfterEnterToSelectCharacter;

		private Clickable[] mClickables;

		private bool mBackToStartScreenWhileGoingToPlayScreen;

		public bool LoadingBackgroundWorldDone
		{
			get;
			private set;
		}

		public static MenuScreen Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new MenuScreen();
				}
				return mInstance;
			}
		}

		private MenuScreen()
		{
			mPageNavigateStack = new Stack<Page>(5);
			mIsBackgroundSceneWorldLoaded = false;
		}

		public override void Load()
		{
			base.LoadingDone = false;
			GUIHelper.UIMask = new Sprite();
			GUIHelper.UIMask.ScaleX = 128f;
			GUIHelper.UIMask.ScaleY = 72f;
			GUIHelper.UIMask.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/black", "Global");
			GUIHelper.UIMask.RelativePosition.Z = -120f;
			GUIHelper.UIMask.AttachTo(World.Camera, false);
			GUIHelper.UIMask.Alpha = 0.7f;
			GUIHelper.TopMask = new Sprite();
			GUIHelper.TopMask.ScaleX = 80f;
			GUIHelper.TopMask.ScaleY = 45f;
			GUIHelper.TopMask.Texture = GUIHelper.UIMask.Texture;
			GUIHelper.TopMask.RelativePosition.Z = -90f;
			GUIHelper.TopMask.AttachTo(World.Camera, false);
			GUIHelper.TopMask.Alpha = 0.7f;
			GUIHelper.UpgradeInfo = new Dictionary<int, Dictionary<int, UpgradeMenuInfo>>(2);
			GUIHelper.UpgradeInfo[0] = new Dictionary<int, UpgradeMenuInfo>(12);
			GUIHelper.UpgradeInfo[0][0] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][0].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon11", "Global");
			GUIHelper.UpgradeInfo[0][0].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon12", "Global");
			GUIHelper.UpgradeInfo[0][0].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon13", "Global");
			GUIHelper.UpgradeInfo[0][1] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][1].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon21", "Global");
			GUIHelper.UpgradeInfo[0][1].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon22", "Global");
			GUIHelper.UpgradeInfo[0][1].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon23", "Global");
			GUIHelper.UpgradeInfo[0][2] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][2].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon31", "Global");
			GUIHelper.UpgradeInfo[0][2].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon32", "Global");
			GUIHelper.UpgradeInfo[0][2].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon33", "Global");
			GUIHelper.UpgradeInfo[0][3] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][3].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon41", "Global");
			GUIHelper.UpgradeInfo[0][3].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon42", "Global");
			GUIHelper.UpgradeInfo[0][3].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon43", "Global");
			GUIHelper.UpgradeInfo[0][4] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][4].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon51", "Global");
			GUIHelper.UpgradeInfo[0][4].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon51", "Global");
			GUIHelper.UpgradeInfo[0][4].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon51", "Global");
			GUIHelper.UpgradeInfo[0][5] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][5].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon61", "Global");
			GUIHelper.UpgradeInfo[0][5].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon61", "Global");
			GUIHelper.UpgradeInfo[0][5].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon61", "Global");
			GUIHelper.UpgradeInfo[0][6] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][6].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon71", "Global");
			GUIHelper.UpgradeInfo[0][6].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon71", "Global");
			GUIHelper.UpgradeInfo[0][6].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon71", "Global");
			GUIHelper.UpgradeInfo[0][7] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][7].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon81", "Global");
			GUIHelper.UpgradeInfo[0][7].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon81", "Global");
			GUIHelper.UpgradeInfo[0][7].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon81", "Global");
			GUIHelper.UpgradeInfo[0][8] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][8].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon91", "Global");
			GUIHelper.UpgradeInfo[0][8].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon91", "Global");
			GUIHelper.UpgradeInfo[0][8].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon91", "Global");
			GUIHelper.UpgradeInfo[0][9] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][9].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon101", "Global");
			GUIHelper.UpgradeInfo[0][9].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon101", "Global");
			GUIHelper.UpgradeInfo[0][9].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon101", "Global");
			GUIHelper.UpgradeInfo[0][10] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][10].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon111", "Global");
			GUIHelper.UpgradeInfo[0][10].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon111", "Global");
			GUIHelper.UpgradeInfo[0][10].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon111", "Global");
			GUIHelper.UpgradeInfo[0][11] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[0][11].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon121", "Global");
			GUIHelper.UpgradeInfo[0][11].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon121", "Global");
			GUIHelper.UpgradeInfo[0][11].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon121", "Global");
			GUIHelper.UpgradeInfo[1] = new Dictionary<int, UpgradeMenuInfo>(12);
			GUIHelper.UpgradeInfo[1][0] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][0].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon11", "Global");
			GUIHelper.UpgradeInfo[1][0].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon12", "Global");
			GUIHelper.UpgradeInfo[1][0].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon13", "Global");
			GUIHelper.UpgradeInfo[1][1] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][1].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon21", "Global");
			GUIHelper.UpgradeInfo[1][1].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon22", "Global");
			GUIHelper.UpgradeInfo[1][1].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon23", "Global");
			GUIHelper.UpgradeInfo[1][2] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][2].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon31", "Global");
			GUIHelper.UpgradeInfo[1][2].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon32", "Global");
			GUIHelper.UpgradeInfo[1][2].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon33", "Global");
			GUIHelper.UpgradeInfo[1][3] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][3].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon41", "Global");
			GUIHelper.UpgradeInfo[1][3].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon42", "Global");
			GUIHelper.UpgradeInfo[1][3].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon43", "Global");
			GUIHelper.UpgradeInfo[1][4] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][4].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon52", "Global");
			GUIHelper.UpgradeInfo[1][4].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon52", "Global");
			GUIHelper.UpgradeInfo[1][4].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon52", "Global");
			GUIHelper.UpgradeInfo[1][5] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][5].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon61", "Global");
			GUIHelper.UpgradeInfo[1][5].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon61", "Global");
			GUIHelper.UpgradeInfo[1][5].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon61", "Global");
			GUIHelper.UpgradeInfo[1][6] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][6].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon72", "Global");
			GUIHelper.UpgradeInfo[1][6].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon72", "Global");
			GUIHelper.UpgradeInfo[1][6].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon72", "Global");
			GUIHelper.UpgradeInfo[1][7] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][7].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon82", "Global");
			GUIHelper.UpgradeInfo[1][7].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon82", "Global");
			GUIHelper.UpgradeInfo[1][7].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon82", "Global");
			GUIHelper.UpgradeInfo[1][8] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][8].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon92", "Global");
			GUIHelper.UpgradeInfo[1][8].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon92", "Global");
			GUIHelper.UpgradeInfo[1][8].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon92", "Global");
			GUIHelper.UpgradeInfo[1][9] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][9].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon102", "Global");
			GUIHelper.UpgradeInfo[1][9].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon102", "Global");
			GUIHelper.UpgradeInfo[1][9].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon102", "Global");
			GUIHelper.UpgradeInfo[1][10] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][10].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon111", "Global");
			GUIHelper.UpgradeInfo[1][10].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon111", "Global");
			GUIHelper.UpgradeInfo[1][10].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon111", "Global");
			GUIHelper.UpgradeInfo[1][11] = new UpgradeMenuInfo();
			GUIHelper.UpgradeInfo[1][11].Icon[1] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon122", "Global");
			GUIHelper.UpgradeInfo[1][11].Icon[2] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon122", "Global");
			GUIHelper.UpgradeInfo[1][11].Icon[3] = FlatRedBallServices.Load<Texture2D>("Content/UI/upgrade_icon122", "Global");
			GUIHelper.LevelInfo = new Dictionary<int, LevelMenuInfo>(7);
			GUIHelper.LevelInfo[1] = new LevelMenuInfo(3, 3, 0);
			GUIHelper.LevelInfo[1].Name = "章 节 1 \n机械叛乱";
			GUIHelper.LevelInfo[1].ShortName = "机械叛乱";
			GUIHelper.LevelInfo[1].Detail = "阿瑞斯和塔鲁斯已成功进入米诺斯空间站。现在他们要\n找到弄乱空间站的始作俑者，并用强大的火力消灭它！";
			GUIHelper.LevelInfo[1].Detail2 = "阿瑞斯和塔鲁斯已成功进入米诺斯空间站。现\n在他们要找到弄乱空间站的始作俑者，并用强\n大的火力消灭它！";
			GUIHelper.LevelInfo[1].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_01", "Global");
			GUIHelper.LevelInfo[1].ChipGeometry[0] = new KeyValuePair<int, GeometryInfo>(1, new GeometryInfo(780f, 220f, 0f));
			GUIHelper.LevelInfo[1].ChipGeometry[1] = new KeyValuePair<int, GeometryInfo>(5, new GeometryInfo(2124f, 384f, 0f));
			GUIHelper.LevelInfo[1].ChipGeometry[2] = new KeyValuePair<int, GeometryInfo>(11, new GeometryInfo(780f, 44f, 0f));
			GUIHelper.LevelInfo[1].DataGeometry[0] = new KeyValuePair<int, GeometryInfo>(5, new GeometryInfo(2288f, 220f, 0f));
			GUIHelper.LevelInfo[1].DataGeometry[1] = new KeyValuePair<int, GeometryInfo>(6, new GeometryInfo(1632f, 268f, 0f));
			GUIHelper.LevelInfo[1].DataGeometry[2] = new KeyValuePair<int, GeometryInfo>(7, new GeometryInfo(928f, 40f, 0f, true));
			GUIHelper.LevelInfo[2] = new LevelMenuInfo(3, 4, 3);
			GUIHelper.LevelInfo[2].Name = "章 节 2 \n重新启动";
			GUIHelper.LevelInfo[2].ShortName = "重新启动";
			GUIHelper.LevelInfo[2].Detail = "在打败歌利亚后，两位战士要攻进米诺斯回收厂的中心\n地带，他们将面临更加危险的敌人。";
			GUIHelper.LevelInfo[2].Detail2 = "在打败歌利亚后，两位战士要攻进米诺斯回收\n厂的中心地带，他们将面临更加危险的敌人。";
			GUIHelper.LevelInfo[2].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_02", "Global");
			GUIHelper.LevelInfo[2].ChipGeometry[0] = new KeyValuePair<int, GeometryInfo>(0, new GeometryInfo(968f, 428f, 0f));
			GUIHelper.LevelInfo[2].ChipGeometry[1] = new KeyValuePair<int, GeometryInfo>(8, new GeometryInfo(768f, 256f, 0f, true));
			GUIHelper.LevelInfo[2].ChipGeometry[2] = new KeyValuePair<int, GeometryInfo>(6, new GeometryInfo(2772f, 204f, 0f));
			GUIHelper.LevelInfo[2].DataGeometry[0] = new KeyValuePair<int, GeometryInfo>(8, new GeometryInfo(1748f, 100f, 0f));
			GUIHelper.LevelInfo[2].DataGeometry[1] = new KeyValuePair<int, GeometryInfo>(9, new GeometryInfo(428f, 604f, 0f));
			GUIHelper.LevelInfo[2].DataGeometry[2] = new KeyValuePair<int, GeometryInfo>(10, new GeometryInfo(2836f, 476f, 0f, true));
			GUIHelper.LevelInfo[2].DataGeometry[3] = new KeyValuePair<int, GeometryInfo>(11, new GeometryInfo(1312f, 308f, 0f));
			GUIHelper.LevelInfo[2].MaterialLocation[0] = new Vector3(1200f, 640f, 0f);
			GUIHelper.LevelInfo[2].MaterialLocation[1] = new Vector3(1200f, 240f, 0f);
			GUIHelper.LevelInfo[2].MaterialLocation[2] = new Vector3(2464f, 208f, 0f);
			GUIHelper.LevelInfo[3] = new LevelMenuInfo(3, 4, 3);
			GUIHelper.LevelInfo[3].Name = "章 节 3 \n疯狂机械";
			GUIHelper.LevelInfo[3].ShortName = "疯狂机械";
			GUIHelper.LevelInfo[3].Detail = "为了找到卡森博士的下落，两名战士决定深入空间站的\n底层。";
			GUIHelper.LevelInfo[3].Detail2 = "为了找到卡森博士的下落，两名战士决定深入\n空间站的底层。";
			GUIHelper.LevelInfo[3].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_03", "Global");
			GUIHelper.LevelInfo[3].ChipGeometry[0] = new KeyValuePair<int, GeometryInfo>(1, new GeometryInfo(636f, 404f, 0f));
			GUIHelper.LevelInfo[3].ChipGeometry[1] = new KeyValuePair<int, GeometryInfo>(5, new GeometryInfo(804f, 208f, 0f));
			GUIHelper.LevelInfo[3].ChipGeometry[2] = new KeyValuePair<int, GeometryInfo>(9, new GeometryInfo(1536f, 304f, 0f));
			GUIHelper.LevelInfo[3].DataGeometry[0] = new KeyValuePair<int, GeometryInfo>(12, new GeometryInfo(828f, 316f, 0f));
			GUIHelper.LevelInfo[3].DataGeometry[1] = new KeyValuePair<int, GeometryInfo>(13, new GeometryInfo(1492f, 356f, 0f));
			GUIHelper.LevelInfo[3].DataGeometry[2] = new KeyValuePair<int, GeometryInfo>(14, new GeometryInfo(2552f, 582f, 0f));
			GUIHelper.LevelInfo[3].DataGeometry[3] = new KeyValuePair<int, GeometryInfo>(15, new GeometryInfo(2472f, 840f, 0f));
			GUIHelper.LevelInfo[3].MaterialLocation[0] = new Vector3(1056f, 280f, 0f);
			GUIHelper.LevelInfo[3].MaterialLocation[1] = new Vector3(1616f, 688f, 0f);
			GUIHelper.LevelInfo[3].MaterialLocation[2] = new Vector3(2656f, 848f, 0f);
			GUIHelper.LevelInfo[4] = new LevelMenuInfo(3, 4, 3);
			GUIHelper.LevelInfo[4].Name = "章 节 4 \n落难少女";
			GUIHelper.LevelInfo[4].ShortName = "落难少女";
			GUIHelper.LevelInfo[4].Detail = "很快就能找到卡森博士了。但危险已悄然而至。";
			GUIHelper.LevelInfo[4].Detail2 = "很快就能找到卡森博士了。但危险已悄然而至。";
			GUIHelper.LevelInfo[4].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_04", "Global");
			GUIHelper.LevelInfo[4].ChipGeometry[0] = new KeyValuePair<int, GeometryInfo>(0, new GeometryInfo(1104f, 260f, 0f));
			GUIHelper.LevelInfo[4].ChipGeometry[1] = new KeyValuePair<int, GeometryInfo>(4, new GeometryInfo(892f, 876f, 0f));
			GUIHelper.LevelInfo[4].ChipGeometry[2] = new KeyValuePair<int, GeometryInfo>(7, new GeometryInfo(472f, 368f, 0f));
			GUIHelper.LevelInfo[4].DataGeometry[0] = new KeyValuePair<int, GeometryInfo>(17, new GeometryInfo(1460f, 724f, 0f));
			GUIHelper.LevelInfo[4].DataGeometry[1] = new KeyValuePair<int, GeometryInfo>(18, new GeometryInfo(2252f, 340f, 0f));
			GUIHelper.LevelInfo[4].DataGeometry[2] = new KeyValuePair<int, GeometryInfo>(16, new GeometryInfo(1092f, 392f, 0f));
			GUIHelper.LevelInfo[4].DataGeometry[3] = new KeyValuePair<int, GeometryInfo>(19, new GeometryInfo(2888f, 192f, 0f));
			GUIHelper.LevelInfo[4].MaterialLocation[0] = new Vector3(808f, 560f, 0f);
			GUIHelper.LevelInfo[4].MaterialLocation[1] = new Vector3(1464f, 432f, 0f);
			GUIHelper.LevelInfo[4].MaterialLocation[2] = new Vector3(2416f, 576f, 0f);
			GUIHelper.LevelInfo[5] = new LevelMenuInfo(3, 4, 3);
			GUIHelper.LevelInfo[5].Name = "章 节 5 \n地狱通道";
			GUIHelper.LevelInfo[5].ShortName = "地狱通道";
			GUIHelper.LevelInfo[5].Detail = "卡森博士已往安全的地方逃跑。是时候铲除米诺斯加农\n炮这个威胁了。";
			GUIHelper.LevelInfo[5].Detail2 = "卡森博士已往安全的地方逃跑。是时候铲除米\n诺斯加农炮这个威胁了。";
			GUIHelper.LevelInfo[5].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_05", "Global");
			GUIHelper.LevelInfo[5].ChipGeometry[0] = new KeyValuePair<int, GeometryInfo>(2, new GeometryInfo(808f, 312f, 0f));
			GUIHelper.LevelInfo[5].ChipGeometry[1] = new KeyValuePair<int, GeometryInfo>(9, new GeometryInfo(1562f, 432f, 0f, true));
			GUIHelper.LevelInfo[5].ChipGeometry[2] = new KeyValuePair<int, GeometryInfo>(10, new GeometryInfo(7732f, 436f, 0f, true));
			GUIHelper.LevelInfo[5].DataGeometry[0] = new KeyValuePair<int, GeometryInfo>(20, new GeometryInfo(120f, 152f, 0f));
			GUIHelper.LevelInfo[5].DataGeometry[1] = new KeyValuePair<int, GeometryInfo>(21, new GeometryInfo(4424f, 432f, 0f, true));
			GUIHelper.LevelInfo[5].DataGeometry[2] = new KeyValuePair<int, GeometryInfo>(22, new GeometryInfo(1184f, 680f, 0f));
			GUIHelper.LevelInfo[5].DataGeometry[3] = new KeyValuePair<int, GeometryInfo>(23, new GeometryInfo(6720f, 456f, 0f));
			GUIHelper.LevelInfo[5].MaterialLocation[0] = new Vector3(728f, 224f, 0f);
			GUIHelper.LevelInfo[5].MaterialLocation[1] = new Vector3(2604f, 432f, 0f);
			GUIHelper.LevelInfo[5].MaterialLocation[2] = new Vector3(9000f, 432f, 0f);
			GUIHelper.LevelInfo[6] = new LevelMenuInfo(3, 4, 3);
			GUIHelper.LevelInfo[6].Name = "章 节 6 \n英雄末路";
			GUIHelper.LevelInfo[6].ShortName = "英雄末路";
			GUIHelper.LevelInfo[6].Detail = "你没有选择的余地，你只能消灭你的搭档。现在只有你\n和瓦尔基莉能阻止灾创的邪恶计划了。";
			GUIHelper.LevelInfo[6].Detail2 = "你没有选择的余地，你只能消灭你的搭档。现\n在只有你和瓦尔基莉能阻止灾创的邪恶计划了。";
			GUIHelper.LevelInfo[6].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_06", "Global");
			GUIHelper.LevelInfo[6].ChipGeometry[0] = new KeyValuePair<int, GeometryInfo>(2, new GeometryInfo(2112f, 600f, 0f));
			GUIHelper.LevelInfo[6].ChipGeometry[1] = new KeyValuePair<int, GeometryInfo>(3, new GeometryInfo(2980f, 332f, 0f, true));
			GUIHelper.LevelInfo[6].ChipGeometry[2] = new KeyValuePair<int, GeometryInfo>(8, new GeometryInfo(1404f, 348f, 0f));
			GUIHelper.LevelInfo[6].DataGeometry[0] = new KeyValuePair<int, GeometryInfo>(24, new GeometryInfo(1120f, 684f, 0f));
			GUIHelper.LevelInfo[6].DataGeometry[1] = new KeyValuePair<int, GeometryInfo>(28, new GeometryInfo(2984f, 520f, 0f));
			GUIHelper.LevelInfo[6].DataGeometry[2] = new KeyValuePair<int, GeometryInfo>(26, new GeometryInfo(2492f, 572f, 0f));
			GUIHelper.LevelInfo[6].DataGeometry[3] = new KeyValuePair<int, GeometryInfo>(27, new GeometryInfo(1312f, 604f, 0f));
			GUIHelper.LevelInfo[6].MaterialLocation[0] = new Vector3(2640f, 464f, 0f);
			GUIHelper.LevelInfo[6].MaterialLocation[1] = new Vector3(1960f, 440f, 0f);
			GUIHelper.LevelInfo[6].MaterialLocation[2] = new Vector3(2056f, 112f, 0f);
			GUIHelper.LevelInfo[7] = new LevelMenuInfo(3, 4, 3);
			GUIHelper.LevelInfo[7].Name = "章 节 7 \n灭绝计划";
			GUIHelper.LevelInfo[7].ShortName = "灭绝计划";
			GUIHelper.LevelInfo[7].Detail = "米诺斯加农炮正在为最后的发射进行充能。这是决定生\n死的时刻。如果你不去阻止它，那地球将永远地消失。";
			GUIHelper.LevelInfo[7].Detail2 = "米诺斯加农炮正在为最后的发射进行充能。这\n是决定生死的时刻。如果你不去阻止它，那地\n球将永远地消失。";
			GUIHelper.LevelInfo[7].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/chapter_main_thumb_07", "Global");
			GUIHelper.LevelInfo[7].ChipGeometry[0] = new KeyValuePair<int, GeometryInfo>(3, new GeometryInfo(964f, 1020f, 0f));
			GUIHelper.LevelInfo[7].ChipGeometry[1] = new KeyValuePair<int, GeometryInfo>(11, new GeometryInfo(1204f, 692f, 0f));
			GUIHelper.LevelInfo[7].ChipGeometry[2] = new KeyValuePair<int, GeometryInfo>(10, new GeometryInfo(1352f, 996f, 0f));
			GUIHelper.LevelInfo[7].DataGeometry[0] = new KeyValuePair<int, GeometryInfo>(25, new GeometryInfo(904f, 888f, 0f));
			GUIHelper.LevelInfo[7].DataGeometry[1] = new KeyValuePair<int, GeometryInfo>(29, new GeometryInfo(172f, 1196f, 0f));
			GUIHelper.LevelInfo[7].DataGeometry[2] = new KeyValuePair<int, GeometryInfo>(30, new GeometryInfo(2028f, 1152f, 0f));
			GUIHelper.LevelInfo[7].DataGeometry[3] = new KeyValuePair<int, GeometryInfo>(31, new GeometryInfo(1244f, 1520f, 0f));
			GUIHelper.LevelInfo[7].MaterialLocation[0] = new Vector3(936f, 696f, 0f);
			GUIHelper.LevelInfo[7].MaterialLocation[1] = new Vector3(232f, 984f, 0f);
			GUIHelper.LevelInfo[7].MaterialLocation[2] = new Vector3(744f, 1288f, 0f);
			GUIHelper.DataCubeInfo = new DataMenuInfo[32];
			GUIHelper.DataCubeInfo[0] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[0].Name = "阿瑞斯";
			GUIHelper.DataCubeInfo[0].Detail = "由亚扎人开发的最新型机械战士，这次任务是\n他抗灾创纳米科技与自适应战斗能力的首次实\n战测试。";
			GUIHelper.DataCubeInfo[0].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb01", "Global");
			GUIHelper.DataCubeInfo[1] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[1].Name = "塔鲁斯";
			GUIHelper.DataCubeInfo[1].Detail = "由亚扎人开发的战斗型机器人，被委以教导年\n轻且缺乏经验的阿瑞斯的任务。对重武器和近\n战尤为擅长。";
			GUIHelper.DataCubeInfo[1].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb02", "Global");
			GUIHelper.DataCubeInfo[2] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[2].Name = "瓦尔基莉";
			GUIHelper.DataCubeInfo[2].Detail = "搭载了高级人工智能和精确激光武器的军事支\n援单位。任务期间，她除了会在空中进行侦察\n外，还会提供火力支援。";
			GUIHelper.DataCubeInfo[2].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb03", "Global");
			GUIHelper.DataCubeInfo[3] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[3].Name = "朱莉娅·卡森博士";
			GUIHelper.DataCubeInfo[3].Detail = "亚扎首席行星工程师兼米诺斯空间站设计者，\n她是灾害先遣调查队的唯一幸存者。";
			GUIHelper.DataCubeInfo[3].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb04", "Global");
			GUIHelper.DataCubeInfo[4] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[4].Name = "灾创";
			GUIHelper.DataCubeInfo[4].Detail = "神秘的气态生命体，事故的元凶。它拥有影响\n和控制机械的能力。";
			GUIHelper.DataCubeInfo[4].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb05", "Global");
			GUIHelper.DataCubeInfo[5] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[5].Name = "修理机器人";
			GUIHelper.DataCubeInfo[5].Detail = "便宜又可靠的服务型机器人，通常会搭载多个\n多关节机械臂以及一个特殊机械臂，任何工具\n都能安装上去。";
			GUIHelper.DataCubeInfo[5].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb06", "Global");
			GUIHelper.DataCubeInfo[6] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[6].Name = "维修无人机";
			GUIHelper.DataCubeInfo[6].Detail = "小巧的自动型机器人，它能飞到修理机器人无\n法到达的区域。";
			GUIHelper.DataCubeInfo[6].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb07", "Global");
			GUIHelper.DataCubeInfo[7] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[7].Name = "能量炮塔";
			GUIHelper.DataCubeInfo[7].Detail = "安装在墙上和天花板上的小型安保炮塔，它的\n主要功能是维护安全。只要发现入侵者，它就\n会对目标进行持续射击。";
			GUIHelper.DataCubeInfo[7].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb08", "Global");
			GUIHelper.DataCubeInfo[8] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[8].Name = "灾创步兵";
			GUIHelper.DataCubeInfo[8].Detail = "由灾创利用空间站垃圾组装成的机器人战士。\n它不仅拥有强悍的实力，其纵向跳跃能力也颇\n为惊人。";
			GUIHelper.DataCubeInfo[8].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb09", "Global");
			GUIHelper.DataCubeInfo[9] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[9].Name = "歌利亚";
			GUIHelper.DataCubeInfo[9].Detail = "由亚扎人秘密研发的高科技飞空型突击机械巨\n人。";
			GUIHelper.DataCubeInfo[9].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb10", "Global");
			GUIHelper.DataCubeInfo[10] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[10].Name = "灾创爬行者";
			GUIHelper.DataCubeInfo[10].Detail = "由灾创利用空间站垃圾组装成的豹型机器人。\n特征是体型小，动作迅速，还会使用致命的滚\n动攻击。";
			GUIHelper.DataCubeInfo[10].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb11", "Global");
			GUIHelper.DataCubeInfo[11] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[11].Name = "灾创步甲兵";
			GUIHelper.DataCubeInfo[11].Detail = "由灾创利用空间站垃圾组装成的升级版灾创步\n兵。它配备了激光冲锋枪。";
			GUIHelper.DataCubeInfo[11].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb12", "Global");
			GUIHelper.DataCubeInfo[12] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[12].Name = "卡里恩";
			GUIHelper.DataCubeInfo[12].Detail = "由亚扎人秘密研发的蛇型机械巨人，平时潜藏\n在空间站的垃圾焚烧厂里，拥有制造其它机器\n的能力。";
			GUIHelper.DataCubeInfo[12].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb13", "Global");
			GUIHelper.DataCubeInfo[13] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[13].Name = "滑行者";
			GUIHelper.DataCubeInfo[13].Detail = "用于分解不可回收垃圾的机器人，通常活动在\n空间站底层。";
			GUIHelper.DataCubeInfo[13].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb14", "Global");
			GUIHelper.DataCubeInfo[14] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[14].Name = "灾创火箭兵";
			GUIHelper.DataCubeInfo[14].Detail = "为了让它的行动变得更加灵活，它们给灾创步\n兵配备了类似喷气背包一样的东西。";
			GUIHelper.DataCubeInfo[14].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb15", "Global");
			GUIHelper.DataCubeInfo[15] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[15].Name = "重灾化机器人";
			GUIHelper.DataCubeInfo[15].Detail = "在系统出现了重大异常后，修理机器人开始乱\n用垃圾来武装自己，现在它变得非常危险。";
			GUIHelper.DataCubeInfo[15].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb16", "Global");
			GUIHelper.DataCubeInfo[16] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[16].Name = "灾创步兵（轻甲）";
			GUIHelper.DataCubeInfo[16].Detail = "只装备了轻型护甲的灾创步兵，是灾创利用各\n种垃圾拼凑出来的。";
			GUIHelper.DataCubeInfo[16].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb17", "Global");
			GUIHelper.DataCubeInfo[17] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[17].Name = "炸弹蜂";
			GUIHelper.DataCubeInfo[17].Detail = "由亚扎人研发的飞空型安保机器人，特点是机\n体内携带有了炸弹。只要发现入侵者，它就会\n以全速冲过去。";
			GUIHelper.DataCubeInfo[17].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb18", "Global");
			GUIHelper.DataCubeInfo[18] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[18].Name = "炸弹蜂巢";
			GUIHelper.DataCubeInfo[18].Detail = "由亚扎人研发的设备，能在同一时间快速生成\n六只炸弹蜂。";
			GUIHelper.DataCubeInfo[18].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb19", "Global");
			GUIHelper.DataCubeInfo[19] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[19].Name = "利维坦";
			GUIHelper.DataCubeInfo[19].Detail = "由亚扎人秘密研发的巨型机器人，特点是搭载\n的化学武器，以及致命的真空漩涡。";
			GUIHelper.DataCubeInfo[19].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb20", "Global");
			GUIHelper.DataCubeInfo[20] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[20].Name = "哨卫者";
			GUIHelper.DataCubeInfo[20].Detail = "由亚扎人秘密研发的特殊突击机器人，特点是\n能以高速进行灵活地移动。它的尾巴以及离子\n武器也是非常危险的存在。";
			GUIHelper.DataCubeInfo[20].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb21", "Global");
			GUIHelper.DataCubeInfo[21] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[21].Name = "抛物机器人";
			GUIHelper.DataCubeInfo[21].Detail = "在米诺斯空间站发现的另一种功能型机器人。\n它的主要用途是运输物品，偶尔也会运载烈性\n炸药。";
			GUIHelper.DataCubeInfo[21].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb22", "Global");
			GUIHelper.DataCubeInfo[22] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[22].Name = "骸子";
			GUIHelper.DataCubeInfo[22].Detail = "被灾创毒气所感染的不可移动机器人，原本普\n通的炮塔被换成了螺钉枪。";
			GUIHelper.DataCubeInfo[22].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb23", "Global");
			GUIHelper.DataCubeInfo[23] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[23].Name = "欧克拉斯";
			GUIHelper.DataCubeInfo[23].Detail = "拥有四条机械臂的超大型电击球体。为了完成\n各种大型作业，它配备了能量护盾以及激光切\n割器。";
			GUIHelper.DataCubeInfo[23].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb24", "Global");
			GUIHelper.DataCubeInfo[24] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[24].Name = "灾创守护者";
			GUIHelper.DataCubeInfo[24].Detail = "用高级部件认真强化过的灾创步兵。拥有向敌\n人释放电浆球的能力。";
			GUIHelper.DataCubeInfo[24].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb25", "Global");
			GUIHelper.DataCubeInfo[25] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[25].Name = "歌利亚MK2";
			GUIHelper.DataCubeInfo[25].Detail = "被灾创改良过的歌利亚，搭载了极为强力的能\n量武器。";
			GUIHelper.DataCubeInfo[25].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb26", "Global");
			GUIHelper.DataCubeInfo[26] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[26].Name = "安保机器人";
			GUIHelper.DataCubeInfo[26].Detail = "由亚扎人研发的移动型安保机器人，不仅能在\n墙上行走，还能同时往三个方向进行射击。";
			GUIHelper.DataCubeInfo[26].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb27", "Global");
			GUIHelper.DataCubeInfo[27] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[27].Name = "突击机器人";
			GUIHelper.DataCubeInfo[27].Detail = "由亚扎人研发的军用机器人，虽然和看起来很\n像工程机器人，实际上却要危险得多。特点是\n能向敌人发射致死的能量波。";
			GUIHelper.DataCubeInfo[27].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb28", "Global");
			GUIHelper.DataCubeInfo[28] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[28].Name = "电击球体";
			GUIHelper.DataCubeInfo[28].Detail = "由亚扎人研发的浮空型机器人。虽然速度不是\n很快，但它的磁场能困住敌人，并对敌人造成\n大量的伤害。";
			GUIHelper.DataCubeInfo[28].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb29", "Global");
			GUIHelper.DataCubeInfo[29] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[29].Name = "精英守卫者";
			GUIHelper.DataCubeInfo[29].Detail = "由灾创利用空间站高级垃圾制造出来的超大型\n机器人。它不仅实力强大，还拥有多种形态，\n对远程攻击尤为擅长。";
			GUIHelper.DataCubeInfo[29].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb30", "Global");
			GUIHelper.DataCubeInfo[30] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[30].Name = "安保加农炮";
			GUIHelper.DataCubeInfo[30].Detail = "由亚扎人研发的巨型激光加农炮。特点是会发\n射能造成巨大伤害的高密度光束，不过它在发\n射前需要一些时间来进行充能。";
			GUIHelper.DataCubeInfo[30].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb31", "Global");
			GUIHelper.DataCubeInfo[31] = new DataMenuInfo();
			GUIHelper.DataCubeInfo[31].Name = "米诺斯核心";
			GUIHelper.DataCubeInfo[31].Detail = "由亚扎人秘密研发的巨大加农炮，还没完全竣\n工。它会回收并利用空间站里的垃圾来持续改\n良自己。";
			GUIHelper.DataCubeInfo[31].Thumb = FlatRedBallServices.Load<Texture2D>("Content/UI/data_thumb32", "Global");
			Mars.Load();
			Tarus.Load();
			Apollo.Load();
			ZytronBlaster.Instance.Load();
			LaserSMG.Instance.Load();
			WaveEmitter.Instance.Load();
			PhotonLauncher.Instance.Load();
			MaterialPool.Initialize();
			ProfileManager.Load();
			mMars = new Mars();
			mTarus = new Tarus();
			LeaderboardUI.Instance.Load();
			AchievementUI.Instance.Load();
			HelpOptionUI.Instance.Load();
			base.LoadingDone = true;
			PauseMenuUI.Instance.Load();
			UpgradeChip.Load();
			ControlHint.Instance.Load();
			StatusUI.Instance.Load();
			MaterialUI.Instance.Load();
			DialogUI.Instance.Load();
			ScoreUI.Instance.Load();
			CheckpointUI.Instance.Load();
			ComboUI.Instance.Load();
			WeaponUI.Instance.Load();
			GameUI.Instance.Load();
			RankUI.Instance.Load();
			PopUpUI.Instance.Load();
			UnlockUI.Instance.Load();
			HintUI.Instance.Load();
			BossUI.Instance.Load();
			MaterialDropper.Instance.Load();
			DataCube.Load();
			AcquireAchievementUI.Instance.Load();
			AcquireItemUI.Instance.Load();
		}

		private void UnloadMainMenuScreenContent()
		{
			mControllerHint = null;
			mBlackCoverSprite = null;
			mDarkBackgroundSprite.Detach();
			mMainMenuCursorSprite.Detach();
			mMainMenuARESLogoSprite.Detach();
			for (int i = 0; i < mMainMenuItemCount; i++)
			{
				mMainMenuItems[i].text.Detach();
			}
			mMainMenuRootObject.Detach();
			mMainMenuItems = null;
			mDarkBackgroundSprite = null;
			mMainMenuCursorSprite = null;
			mMainMenuARESLogoSprite = null;
			mMainMenuRootObject = null;
			mSelectCharHeaderSprite.Detach();
			mSelectCharARESLogoSprite.Detach();
			mSelectCharAresSprite.Detach();
			mSelectCharTarusSprite.Detach();
			mSelectCharAresSelectedFrameSprite.Detach();
			mSelectCharTarusSelectedFrameSprite.Detach();
			mSelectCharAresSelectedOuterFrameSprite.Detach();
			mSelectCharTarusSelectedOuterFrameSprite.Detach();
			mSelectCharBackgroundPanelSprite.Detach();
			mSelectCharResetProgress.text.Detach();
			mSelectCharHeaderSprite = null;
			mSelectCharARESLogoSprite = null;
			mSelectCharAresSprite = null;
			mSelectCharTarusSprite = null;
			mSelectCharAresSelectedFrameSprite = null;
			mSelectCharTarusSelectedFrameSprite = null;
			mSelectCharAresSelectedOuterFrameSprite = null;
			mSelectCharTarusSelectedOuterFrameSprite = null;
			mSelectCharRootObject = null;
			mSelectCharBackgroundPanelSprite = null;
			mCharacterDetailPanel.OnRemove();
			mCharacterDetailPanel.UnloadVisualComponents();
			mCharacterDetailPanel = null;
			mSelectCharResetProgress = null;
			mClickables = null;
			UnloadBackgroundWorld();
		}

		private void AddMainMenuItem(int tag, string text)
		{
			mMainMenuItems.Add(new ClickableMenuItem
			{
				tag = tag,
				index = mMainMenuItemCount,
				text = CreateText(text),
				boundary = new Vector4(-65f, -1.5f, 37f, 1.5f),
				OnMouseEnter = new Clickable.ClickableHoverHandler(OnMouseEnterMainMenuItem),
				OnMouseClick = new Clickable.ClickableHandler(OnMouseClickMainMenuItem)
			});
			mMainMenuItemCount++;
		}

		private void OnMouseEnterMainMenuItem(Clickable sender)
		{
			ClickableMenuItem clickableMenuItem = sender as ClickableMenuItem;
			if (SelectMainMenuItem(clickableMenuItem.index))
			{
				SFXManager.PlaySound("cursor");
			}
		}

		private bool OnMouseClickMainMenuItem(Clickable sender)
		{
			ClickableMenuItem clickableMenuItem = sender as ClickableMenuItem;
			if (SelectMainMenuItem(clickableMenuItem.index))
			{
				SFXManager.PlaySound("cursor");
				return true;
			}
			ExecuteSelectedMainMenuItem();
			SFXManager.PlaySound("ok");
			return true;
		}

		private void LoadMainMenuScreenContent()
		{
			mControllerHint = new Dictionary<int, string>(5);
			mBlackCoverSprite = GlobalSheet.CreateSprite(8);
			mDarkBackgroundSprite = new Sprite();
			mDarkBackgroundSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/menu_dim", "MenuScreen");
			mMainMenuRootObject = new PositionedObject();
			mMainMenuCursorSprite = new Sprite();
			mMainMenuARESLogoSprite = new Sprite();
			mMainMenuCursorSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/mainmenu_cursor", "MenuScreen");
			mMainMenuARESLogoSprite.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/areslogo_medium"), "MenuScreen");
			mMainMenuItems = new List<ClickableMenuItem>();
			mMainMenuItemCount = 0;
			AddMainMenuItem(1, "开始游戏");
			AddMainMenuItem(2, "帮助和选项");
			if (Pref.Steamworks)
			{
				AddMainMenuItem(3, "排行榜");
			}
			AddMainMenuItem(4, "成就");
			if (ProfileManager.IsTrialMode)
			{
				AddMainMenuItem(6, "unlock full game");
			}
			AddMainMenuItem(5, "退出游戏");
			mMainMenuItems[0].boundary.W = 10f;
			mMainMenuItems[mMainMenuItemCount - 1].boundary.Y = -10f;
			mSelectCharRootObject = new PositionedObject();
			mSelectCharHeaderSprite = LocalizedSheet.CreateSprite(35);
			mSelectCharARESLogoSprite = new Sprite();
			mSelectCharARESLogoSprite.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/areslogo_medium"), "MenuScreen");
			mSelectCharAresSprite = new Sprite();
			if (ProfileManager.Mars != null && ProfileManager.Mars.ArmorLevel > 0)
			{
				mSelectCharAresSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_ares_large_upgraded", "MenuScreen");
			}
			else
			{
				mSelectCharAresSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_ares_large", "MenuScreen");
			}
			mSelectCharTarusSprite = new Sprite();
			if (ProfileManager.Tarus != null && ProfileManager.Tarus.ArmorLevel > 0)
			{
				mSelectCharTarusSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_tarus_large_upgraded", "MenuScreen");
			}
			else
			{
				mSelectCharTarusSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_tarus_large", "MenuScreen");
			}
			mSelectCharAresSelectedFrameSprite = new Sprite();
			mSelectCharAresSelectedFrameSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_ares_frame", "MenuScreen");
			mSelectCharTarusSelectedFrameSprite = new Sprite();
			mSelectCharTarusSelectedFrameSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_tarus_frame", "MenuScreen");
			mSelectCharAresSelectedOuterFrameSprite = new Sprite();
			mSelectCharAresSelectedOuterFrameSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_frame", "MenuScreen");
			mSelectCharTarusSelectedOuterFrameSprite = new Sprite();
			mSelectCharTarusSelectedOuterFrameSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_frame", "MenuScreen");
			mSelectCharBackgroundPanelSprite = GlobalSheet.CreateSprite(8);
			mCharacterDetailPanel = new CharacterDetailPanel();
			mCharacterDetailPanel.LoadVisualComponents();
			mSelectCharResetProgress = new ClickableText
			{
				boundary = new Vector4(-1f, -1.5f, 16f, 1.5f),
				text = new Text(GUIHelper.SpeechFont)
				{
					HorizontalAlignment = HorizontalAlignment.Left,
					ColorOperation = ColorOperation.Subtract,
					DisplayText = "重置进度",
					Red = 0f,
					Green = 0f,
					Blue = 0f,
					Scale = 1.5f,
					Spacing = 1.5f
				},
				OnMouseEnter = (Clickable.ClickableHoverHandler)delegate
				{
					mSelectCharResetProgress.text.SetColor(1f, 0f, 0f);
				},
				OnMouseExit = (Clickable.ClickableHoverHandler)delegate
				{
					mSelectCharResetProgress.text.SetColor(0f, 0f, 0f);
				},
				OnMouseClick = (Clickable.ClickableHandler)delegate
				{
					SFXManager.PlaySound("ok");
					mSelectCharResetProgress.text.Visible = false;
					PopUpUI.Instance.Show("重置进度", "是否要重置进度？\n阿瑞斯和塔鲁斯的档案都会被删除。", delegate(PopUpUI.PopUpUIResult result)
					{
						if (result == PopUpUI.PopUpUIResult.Ok)
						{
							SFXManager.PlaySound("ok");
							ProfileManager.DeleteProfiles();
							ProfileManager.BeginSave(null, 7, false);
							mSelectCharTarusSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_tarus_large", "MenuScreen");
							mSelectCharAresSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_ares_large", "MenuScreen");
						}
						else
						{
							SFXManager.PlaySound("error");
						}
						mCharacterDetailPanel.SetVisualComponentsToCurrentProfile();
						mSelectCharResetProgress.text.Visible = true;
						ManageControlHintForSelectCharacter();
					});
					return true;
				}
			};
			mClickables = new Clickable[3]
			{
				mSelectCharResetProgress,
				new ClickableSelectCharSprite
				{
					sprite = mSelectCharAresSprite,
					topRight = 0.3981788f,
					bottomRight = 0.969342f,
					OnMouseEnter = (Clickable.ClickableHoverHandler)delegate
					{
						if (mCurrentSelectPlayerProfile != ProfileManager.Mars)
						{
							SelectCharacter(0);
							SFXManager.PlaySound("cursor");
						}
					},
					OnMouseClick = (Clickable.ClickableHandler)delegate
					{
						if (mCurrentSelectPlayerProfile != ProfileManager.Mars)
						{
							SelectCharacter(0);
							SFXManager.PlaySound("cursor");
							return true;
						}
						ExecuteSelectedCharacter();
						return true;
					}
				},
				new ClickableSelectCharSprite
				{
					sprite = mSelectCharTarusSprite,
					topLeft = -0.969342f,
					bottomLeft = -0.3981788f,
					OnMouseEnter = (Clickable.ClickableHoverHandler)delegate
					{
						if (mCurrentSelectPlayerProfile != ProfileManager.Tarus)
						{
							SelectCharacter(1);
							SFXManager.PlaySound("cursor");
						}
					},
					OnMouseClick = (Clickable.ClickableHandler)delegate
					{
						if (mCurrentSelectPlayerProfile != ProfileManager.Tarus)
						{
							SelectCharacter(1);
							SFXManager.PlaySound("cursor");
							return true;
						}
						ExecuteSelectedCharacter();
						return true;
					}
				}
			};
			LoadBackgroundWorldIfRequired();
			BGMManager.LoadMenu();
		}

		public bool IsBackgroundWorldLoadingRequire(int index)
		{
			return !mIsBackgroundSceneWorldLoaded;
		}

		public void LoadBackgroundWorldIfRequired()
		{
			int lastPlayedLevel = ProfileManager.LastPlayedLevel;
			if (!mIsBackgroundSceneWorldLoaded)
			{
				WorldLoader worldLoader = null;
				switch (lastPlayedLevel)
				{
				case 2:
					worldLoader = new RecyclePlantStartScreen();
					break;
				case 3:
					worldLoader = new SewerStartScreen();
					break;
				case 4:
					worldLoader = new LivingQuarterStartScreen();
					break;
				case 5:
					worldLoader = new TrainStartScreen();
					break;
				case 6:
					worldLoader = new HallwayStartScreen();
					break;
				case 7:
					worldLoader = new MinosCoreStartScreen();
					break;
				default:
					worldLoader = new JunkyardStartScreen();
					break;
				}
				World.CurrentLevel = 0;
				World.LoadWorldForMenuScreen(worldLoader);
				mIsBackgroundSceneWorldLoaded = true;
				switch (lastPlayedLevel)
				{
				case 1:
					TimedEmitParticlePool.LoadParticle("junkyard_startscreen", new junkyard_startscreen(), 1);
					break;
				case 2:
					TimedEmitParticlePool.LoadParticle("recycleplant_startscreen", new recycleplant_startscreen(), 1);
					break;
				case 3:
					TimedEmitParticlePool.LoadParticle("sewer_startscreen", new sewer_startscreen(), 1);
					break;
				case 4:
					TimedEmitParticlePool.LoadParticle("livingquarter_startscreen", new livingquarter_startscreen(), 1);
					break;
				case 5:
					TimedEmitParticlePool.LoadParticle("train_startscreen", new train_startscreen(), 1);
					break;
				case 6:
					TimedEmitParticlePool.LoadParticle("hallway_startscreen", new hallway_startscreen(), 1);
					break;
				case 7:
					TimedEmitParticlePool.LoadParticle("minoscore_startscreen", new minoscore_startscreen(), 1);
					break;
				}
			}
		}

		public void UnloadBackgroundWorld()
		{
			World.UnLoadRoomByID(0, false);
			if (mBackgroundSceneParticle != null)
			{
				ProcessManager.RemoveProcess(mBackgroundSceneParticle);
				mBackgroundSceneParticle = null;
			}
			World.Destroy();
			mIsBackgroundSceneWorldLoaded = false;
		}

		public override void Initialize()
		{
			base.ActivityFinished = false;
			base.LoadingDone = false;
			mMainMenuCursor = 0;
			mPage = Page.None;
			mRequestNextPage = Page.None;
			mMainMenuCursorSprite.ScaleX = 45f;
			mMainMenuCursorSprite.ScaleY = 1.2f;
			mMainMenuCursorSprite.BlendOperation = BlendOperation.Add;
			mMainMenuARESLogoSprite.ScaleX = 20f;
			mMainMenuARESLogoSprite.ScaleY = 5.5f;
			mMainMenuRootObject.AttachTo(World.Camera, false);
			mMainMenuCursorSprite.AttachTo(mMainMenuRootObject, false);
			mMainMenuARESLogoSprite.AttachTo(mMainMenuRootObject, false);
			for (int i = 0; i < mMainMenuItemCount; i++)
			{
				mMainMenuItems[i].text.AttachTo(mMainMenuRootObject, false);
			}
			mCurrentSelectedCharacterIndex = -1;
			mSelectCharHeaderSprite.ScaleX = 17f;
			mSelectCharHeaderSprite.ScaleY = 4.875f;
			mSelectCharARESLogoSprite.ScaleX = 10f;
			mSelectCharARESLogoSprite.ScaleY = 2.75f;
			mSelectCharAresSprite.ScaleX = 29.25f;
			mSelectCharAresSprite.ScaleY = 25.45f;
			mSelectCharTarusSprite.ScaleX = 29.25f;
			mSelectCharTarusSprite.ScaleY = 25.45f;
			mSelectCharAresSelectedFrameSprite.ScaleX = 14.625f;
			mSelectCharAresSelectedFrameSprite.ScaleY = 25.45f;
			mSelectCharTarusSelectedFrameSprite.ScaleX = 14.625f;
			mSelectCharTarusSelectedFrameSprite.ScaleY = 25.45f;
			mSelectCharAresSelectedOuterFrameSprite.ScaleX = 14.625f;
			mSelectCharAresSelectedOuterFrameSprite.ScaleY = 25.45f;
			mSelectCharTarusSelectedOuterFrameSprite.ScaleX = 14.625f;
			mSelectCharTarusSelectedOuterFrameSprite.ScaleY = 25.45f;
			mSelectCharTarusSelectedOuterFrameSprite.FlipHorizontal = true;
			mSelectCharBackgroundPanelSprite.ScaleX = 54f;
			mSelectCharBackgroundPanelSprite.ScaleY = 24f;
			mSelectCharHeaderSprite.AttachTo(mSelectCharRootObject, false);
			mSelectCharARESLogoSprite.AttachTo(mSelectCharRootObject, false);
			mSelectCharAresSprite.AttachTo(mSelectCharRootObject, false);
			mSelectCharTarusSprite.AttachTo(mSelectCharRootObject, false);
			mSelectCharAresSelectedFrameSprite.AttachTo(mSelectCharAresSprite, false);
			mSelectCharTarusSelectedFrameSprite.AttachTo(mSelectCharTarusSprite, false);
			mSelectCharAresSelectedOuterFrameSprite.AttachTo(mSelectCharAresSelectedFrameSprite, false);
			mSelectCharTarusSelectedOuterFrameSprite.AttachTo(mSelectCharTarusSelectedFrameSprite, false);
			mSelectCharBackgroundPanelSprite.AttachTo(mSelectCharRootObject, false);
			mSelectCharResetProgress.text.AttachTo(World.Camera, false);
			mBlackCoverSprite.ScaleX = 128f;
			mBlackCoverSprite.ScaleY = 72f;
			mBlackCoverSprite.RelativePosition.Z = -1f;
			mBlackCoverSprite.Alpha = 0f;
			mDarkBackgroundSprite.ScaleX = 80f;
			mDarkBackgroundSprite.ScaleY = 45f;
			mDarkBackgroundSprite.RelativePosition.Z = -108f;
			mDarkBackgroundSprite.AttachTo(World.Camera, false);
			SpriteManager.AddToLayer(mDarkBackgroundSprite, GUIHelper.UILayer);
			ManageBackgroundScene();
			Director.FadeIn(1f);
			ProfileManager.SetGamerPresence(Pref.GP_MENU);
			if (!BGMManager.isPlaying)
			{
				BGMManager.Play(0);
			}
		}

		private void ManageBackgroundScene()
		{
			int lastPlayedLevel = ProfileManager.LastPlayedLevel;
			World.LoadRoomByID(0, false);
			switch (lastPlayedLevel)
			{
			default:
				World.Camera.Position = new Vector3(907f, 149f, 131f);
				mBackgroundSceneParticle = TimedEmitParticlePool.Summon("junkyard_startscreen", Vector3.Zero, -1f);
				break;
			case 2:
				World.Camera.Position = new Vector3(1734f, 402f, 98f);
				mBackgroundSceneParticle = TimedEmitParticlePool.Summon("recycleplant_startscreen", Vector3.Zero, -1f);
				break;
			case 3:
				World.Camera.Position = new Vector3(653f, 168f, 155f);
				mBackgroundSceneParticle = TimedEmitParticlePool.Summon("sewer_startscreen", Vector3.Zero, -1f);
				break;
			case 4:
				World.Camera.Position = new Vector3(510f, 618f, 122f);
				mBackgroundSceneParticle = TimedEmitParticlePool.Summon("livingquarter_startscreen", Vector3.Zero, -1f);
				break;
			case 5:
				World.Camera.Position = new Vector3(2502f, 449f, 117f);
				mBackgroundSceneParticle = TimedEmitParticlePool.Summon("train_startscreen", Vector3.Zero, -1f);
				break;
			case 6:
				World.Camera.Position = new Vector3(1604f, 739f, 136f);
				mBackgroundSceneParticle = TimedEmitParticlePool.Summon("hallway_startscreen", Vector3.Zero, -1f);
				break;
			case 7:
				World.Camera.Position = new Vector3(1100f, 195f, 118f);
				mBackgroundSceneParticle = TimedEmitParticlePool.Summon("minoscore_startscreen", Vector3.Zero, -1f);
				break;
			}
		}

		public override void Update()
		{
			if (!mIsTransitionBetweenPages)
			{
				switch (mPage)
				{
				case Page.HelpAndOptions:
				case Page.Leaderboard:
				case Page.Achievements:
				case Page.SelectChapter:
					break;
				case Page.None:
					mPageNavigateStack.Clear();
					ChangePage(Page.Main);
					break;
				case Page.Main:
					if (mMainMenuCursorSprite.RelativeVelocity.Y != 0f)
					{
						UpdateMainMenuCursorSprite();
					}
					else
					{
						if (PopUpUI.Instance.IsRunning)
						{
							break;
						}
						if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
						{
							if (MoveMainMenuCursor(-2))
							{
								SFXManager.PlaySound("cursor");
							}
							break;
						}
						if (GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
						{
							if (MoveMainMenuCursor(2))
							{
								SFXManager.PlaySound("cursor");
							}
							break;
						}
						if (GamePad.GetMenuKeyDown(1048576))
						{
							SFXManager.PlaySound("New_MenuBack");
							ChangePage(Page.BackToStartScreen);
							break;
						}
						if (GamePad.GetMenuKeyDown(524288))
						{
							SFXManager.PlaySound("ok");
							ExecuteSelectedMainMenuItem();
							break;
						}
					}
					GamePad.UpdateClickables(mMainMenuItems);
					break;
				case Page.SelectCharacter:
					if (!PopUpUI.Instance.IsRunning)
					{
						if (IsGoToPlayScreenAfterEnterToSelectCharacter)
						{
							IsGoToPlayScreenAfterEnterToSelectCharacter = false;
							ChangePage(Page.EnterPlayScreen);
						}
						else if (GamePad.GetMenuRepeatKeyDown(1) || GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
						{
							if (mCurrentSelectPlayerProfile != ProfileManager.Mars)
							{
								SFXManager.PlaySound("cursor");
							}
							SelectCharacter(0);
						}
						else if (GamePad.GetMenuRepeatKeyDown(2) || GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
						{
							if (mCurrentSelectPlayerProfile != ProfileManager.Tarus)
							{
								SFXManager.PlaySound("cursor");
							}
							SelectCharacter(1);
						}
						else if (GamePad.GetMenuKeyDown(8388608))
						{
							if (mCurrentSelectPlayerProfile.UnlockedLevel > 0)
							{
								SFXManager.PlaySound("ok");
								ChangePage(Page.SelectChapter);
							}
							else
							{
								SFXManager.PlaySound("error");
							}
						}
						else if (GamePad.GetMenuKeyDown(16777216))
						{
							SFXManager.PlaySound("ok");
							ChangePage(Page.HelpAndOptions);
						}
						else if (GamePad.GetMenuKeyDown(524288))
						{
							ExecuteSelectedCharacter();
						}
						else if (GamePad.GetMenuKeyDown(1048576))
						{
							SFXManager.PlaySound("New_MenuBack");
							ChangePage(Page.Main);
						}
						else
						{
							GamePad.UpdateClickables(mClickables);
						}
					}
					break;
				case Page.EnterPlayScreen:
					if (ScreenManager.NextScreen.LoadingDone)
					{
						if (mCountdownTimer1 == -1f)
						{
							mCountdownTimer1 = 0.51f;
						}
						else if (mCountdownTimer1 > 0f)
						{
							mLastCountdownTimer1 = mCountdownTimer1;
							mCountdownTimer1 -= TimeManager.SecondDifference;
							if (mBackToStartScreenWhileGoingToPlayScreen)
							{
								ChangePage(Page.BackToStartScreen);
								mBackToStartScreenWhileGoingToPlayScreen = false;
							}
							if (mCountdownTimer1 <= 0f)
							{
								if (mLastCountdownTimer1 > 0f)
								{
									ChangePage(Page.None);
									base.ActivityFinished = true;
								}
							}
							else if (mCountdownTimer1 <= 0.25f)
							{
								if (mLastCountdownTimer1 > 0.25f)
								{
									LoadingUI.Instance.Hide();
								}
							}
							else if (mCountdownTimer1 <= 0.5f && mLastCountdownTimer1 > 0.5f)
							{
								Director.FadeOut(0.25f);
							}
						}
					}
					else
					{
						mCountdownTimer1 = -1f;
					}
					break;
				case Page.BackToStartScreen:
					if (StartScreen.Instance.LoadingDone && Director.BlackOverlay.Alpha > 0.9f)
					{
						ChangePage(Page.None);
						base.ActivityFinished = true;
					}
					break;
				case Page.UnlockFullVersion:
					if (AdvertiseScreen.Instance.LoadingDone && Director.BlackOverlay.Alpha > 0.9f)
					{
						ChangePage(Page.None);
						base.ActivityFinished = true;
					}
					break;
				case Page.Marketplace:
					if (!Guide.IsVisible)
					{
						ProfileManager.UpdateIsTrialModeFlag();
						if (!ProfileManager.IsTrialMode)
						{
							ChangePage(Page.BackToStartScreen);
						}
						else
						{
							ChangePage(Page.Main);
						}
					}
					break;
				case Page.Exit:
					if (mExitStep == 0)
					{
						if (ProfileManager.IsTrialMode)
						{
							AdvertiseScreen.Instance.NextScreenWhenBack = null;
							ChangePage(Page.UnlockFullVersion);
						}
						else
						{
							mExitStep = 1;
						}
					}
					else if (mExitStep == 1)
					{
						mExitStep = 2;
						ProfileManager.BeginSave(SaveBeforeExitFinish, 7, true);
					}
					else if (mExitStep != 2 && mExitStep == 3 && mDelayBeforeExit > 0f)
					{
						mDelayBeforeExit -= TimeManager.SecondDifference;
						if (mDelayBeforeExit <= 0f)
						{
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
					if (mPreviousPage == mRequestNextPage && mPageNavigateStack.Count > 1)
					{
						mPageNavigateStack.Pop();
					}
					else
					{
						mPageNavigateStack.Push(mRequestNextPage);
					}
					mPreviousPage = mPage;
					mPage = mRequestNextPage;
					mIsTransitionBetweenPages = false;
				}
				mIsJustChangePage = false;
			}
		}

		private void PopUpUIAskForExitGame(PopUpUI.PopUpUIResult result)
		{
			if (result == PopUpUI.PopUpUIResult.Ok)
			{
				SFXManager.PlaySound("ok");
				ChangePage(Page.Exit);
			}
			else
			{
				SFXManager.PlaySound("New_MenuBack");
				ControlHint.Instance.Clear().AddHint(524288, "选择").AddHint(1048576, "返回")
					.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
			}
		}

		private void SaveBeforeExitFinish(IAsyncResult ar)
		{
			if (ar.IsCompleted)
			{
				mDelayBeforeExit = 1f;
				mExitStep = 3;
			}
			else if (StorageDeviceManager.SelectedStorageDevice == null || !StorageDeviceManager.SelectedStorageDevice.IsConnected)
			{
				GuideMessageBoxWrapper.Instance.Show("无法保存", "选择的存储设备无法使用。\n请选择其它存储设备或在不存档的情况下继续游戏。", PopUpUIAskForStorageUnavailable, 3, "选择存档", "继续", "返回");
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
				FlatRedBallServices.Game.Exit();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Other:
				ChangePage(Page.None);
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
				if (StorageDeviceManager.SelectedStorageDevice != null && StorageDeviceManager.SelectedStorageDevice.IsConnected)
				{
					GuideMessageBoxWrapper.Instance.Show("确认选择", "只有一台存储设备能用。\n请确认你的选择或在不存档的情况下继续游戏。", PopUpUIAskForAutoSelectStorageDevice, 3, "确认", "继续", "返回");
				}
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
				ChangePage(Page.None);
				break;
			}
		}

		private void CheckIsThereExistingSave()
		{
			if (StorageDeviceManager.SelectedStorageDevice != null && StorageDeviceManager.SelectedStorageDevice.IsConnected)
			{
				if (StorageDeviceManager.IsDirectoryForGamerExist())
				{
					GuideMessageBoxWrapper.Instance.Show("覆盖存档", "在当前设备中发现了存档文件。\n是否要覆盖此存档吗？", PopUpUIAskForOverwriteSave, 2, "覆盖", "选择其它存储设备", string.Empty);
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
				ChangePage(Page.None);
				break;
			}
		}

		private void ChangePage(Page nextState)
		{
			if (mPage != nextState)
			{
				mRequestNextPage = nextState;
				mIsTransitionBetweenPages = true;
				mIsTransitionEnd = false;
				mIsJustChangePage = true;
			}
		}

		private void RemovePagesInNavigationStack()
		{
			while (mPageNavigateStack.Count > 0)
			{
				switch (mPageNavigateStack.Pop())
				{
				case Page.Main:
					RemoveMainMenuComponentsFromManager();
					break;
				case Page.SelectCharacter:
					RemoveSelectCharacterFromManager();
					break;
				case Page.HelpAndOptions:
					SpriteManager.RemoveSprite(mBlackCoverSprite);
					HelpOptionUI.Instance.OnBackButtonPressed = null;
					HelpOptionUI.Instance.OnBackButtonPressed = null;
					HelpOptionUI.Instance.Hide();
					break;
				case Page.SelectChapter:
					SpriteManager.RemoveSprite(mBlackCoverSprite);
					ProcessManager.RemoveProcess(ChapterSelectUI.Instance);
					break;
				case Page.Leaderboard:
					LeaderboardUI.Instance.Hide();
					break;
				case Page.Achievements:
					AchievementUI.Instance.Hide();
					break;
				}
			}
		}

		private void GotoPlayScreen()
		{
			SpriteManager.RemoveSprite(mDarkBackgroundSprite);
			mMainMenuCursor = 2;
			UnloadMainMenu();
			if (mCurrentSelectedCharacterIndex == 0)
			{
				mMars.Play();
			}
			else if (mCurrentSelectedCharacterIndex == 1)
			{
				mTarus.Play();
			}
			World.CurrentLevel = ProfileManager.Current.CurrentLevel;
			if (ProfileManager.Current.CurrentLevel > ProfileManager.Current.UnlockedLevel)
			{
				ProfileManager.Current.UnlockedLevel = ProfileManager.Current.CurrentLevel;
			}
			LoadPlayScreen();
		}

		private void GotoStartScreen()
		{
			SpriteManager.RemoveSprite(mDarkBackgroundSprite);
			UnloadBackgroundWorld();
			RemovePagesInNavigationStack();
			StartScreen instance = StartScreen.Instance;
			LoadNextScreen(((Screen)instance).Load);
			ScreenManager.NextScreen = StartScreen.Instance;
		}

		private void GotoUnlockFullVersionScreen()
		{
			SpriteManager.RemoveSprite(mDarkBackgroundSprite);
			UnloadBackgroundWorld();
			RemovePagesInNavigationStack();
			AdvertiseScreen instance = AdvertiseScreen.Instance;
			LoadNextScreen(((Screen)instance).Load);
			ScreenManager.NextScreen = AdvertiseScreen.Instance;
		}

		private void UpdatePageTransition(Page currentPage, Page newPage)
		{
			if (mIsJustChangePage && mSelectCharResetProgress != null)
			{
				if (newPage == Page.SelectCharacter)
				{
					mSelectCharResetProgress.text.Visible = true;
				}
				else
				{
					mSelectCharResetProgress.text.Visible = false;
				}
			}
			switch (currentPage)
			{
			case Page.None:
				if (newPage == Page.Main)
				{
					AddMainMenuComponentsToManager();
					EndTransitionToMainState();
				}
				mIsTransitionEnd = true;
				break;
			case Page.Main:
				if (mIsJustChangePage && newPage != Page.BackToStartScreen)
				{
					RemoveMainMenuComponentsFromManager();
				}
				switch (newPage)
				{
				case Page.SelectCharacter:
					if (mIsJustChangePage)
					{
						AddSelectCharacterComponentsToManager();
						ManageControlHintForSelectCharacter();
						mIsTransitionEnd = true;
					}
					break;
				case Page.Leaderboard:
					if (mIsJustChangePage)
					{
						LeaderboardUI.Instance.RelativePosition = new Vector3(100f, 0f, -70f);
						LeaderboardUI.Instance.RelativeVelocity.X = -400f;
						LeaderboardUI.Instance.IsActivated = false;
						LeaderboardUI.Instance.IsRemoveProcessWhenPressBack = false;
						LeaderboardUI.Instance.AttachTo(World.Camera, false);
						LeaderboardUI.Instance.Show();
					}
					if (LeaderboardUI.Instance.RelativePosition.X <= 0f)
					{
						LeaderboardUI.Instance.RelativeVelocity.X = 0f;
						LeaderboardUI.Instance.RelativePosition = new Vector3(0f, 0f, -70f);
						LeaderboardUI.Instance.OnBackButtonPressed = LeaderboardUIDidBackButtonPressed;
						LeaderboardUI.Instance.IsActivated = true;
						mIsTransitionEnd = true;
					}
					break;
				case Page.Achievements:
					if (mIsJustChangePage)
					{
						AchievementUI.Instance.RelativePosition = new Vector3(100f, 0f, -70f);
						AchievementUI.Instance.RelativeVelocity.X = -400f;
						AchievementUI.Instance.IsActivated = false;
						AchievementUI.Instance.IsRemoveProcessWhenPressBack = false;
						AchievementUI.Instance.AttachTo(World.Camera, false);
						AchievementUI.Instance.Show();
					}
					if (AchievementUI.Instance.RelativePosition.X <= 0f)
					{
						AchievementUI.Instance.RelativeVelocity.X = 0f;
						AchievementUI.Instance.RelativePosition = new Vector3(0f, 0f, -70f);
						AchievementUI.Instance.OnBackButtonPressed = AchievementUIDidBackButtonPressed;
						AchievementUI.Instance.IsActivated = true;
						mIsTransitionEnd = true;
					}
					break;
				case Page.HelpAndOptions:
					if (mIsJustChangePage)
					{
						HelpOptionUI.Instance.RelativePosition = new Vector3(100f, 0f, -70f);
						HelpOptionUI.Instance.RelativeVelocity.X = -400f;
						HelpOptionUI.Instance.IsActivated = false;
						HelpOptionUI.Instance.IsImmediatelyRemoveProcessWhenExit = false;
						HelpOptionUI.Instance.Show();
						SpriteManager.RemoveSprite(mBlackCoverSprite);
						mBlackCoverSprite.Position = World.Camera.Position + new Vector3(0f, 0f, -71f);
						mBlackCoverSprite.RelativePosition.Z = 0.1f;
						mBlackCoverSprite.RelativePosition.Y = 0f;
						mBlackCoverSprite.Alpha = 0.6f;
						mBlackCoverSprite.AlphaRate = 0f;
						SpriteManager.AddToLayer(mBlackCoverSprite, GUIHelper.UILayer);
					}
					if (HelpOptionUI.Instance.RelativePosition.X <= 0f)
					{
						HelpOptionUI.Instance.RelativeVelocity.X = 0f;
						HelpOptionUI.Instance.RelativePosition = new Vector3(0f, 0f, -70f);
						HelpOptionUI.Instance.OnBackButtonPressed = HelpOptionUIDidBackButtonPressed;
						HelpOptionUI.Instance.IsActivated = true;
						mIsTransitionEnd = true;
					}
					break;
				case Page.Marketplace:
					mIsTransitionEnd = true;
					break;
				case Page.Exit:
					mExitStep = 0;
					mIsTransitionEnd = true;
					break;
				}
				break;
			case Page.SelectCharacter:
				if (newPage == Page.Main && mIsJustChangePage)
				{
					RemoveSelectCharacterFromManager();
					AddMainMenuComponentsToManager();
					EndTransitionToMainState();
					mIsTransitionEnd = true;
				}
				switch (newPage)
				{
				case Page.EnterPlayScreen:
					if (mIsJustChangePage)
					{
						Director.FadeOut(0.25f);
						BGMManager.FadeOff();
						mCountdownTimer1 = 0.5f;
					}
					if (mCountdownTimer1 > 0f)
					{
						mLastCountdownTimer1 = mCountdownTimer1;
						mCountdownTimer1 -= TimeManager.SecondDifference;
						if (mCountdownTimer1 <= 0f)
						{
							if (mLastCountdownTimer1 > 0f)
							{
								mCountdownTimer1 = -1f;
								mIsTransitionEnd = true;
							}
						}
						else if (mCountdownTimer1 <= 0.1f)
						{
							if (mLastCountdownTimer1 > 0.1f)
							{
								LoadingUI.Instance.Show(true, 1);
							}
						}
						else if (mCountdownTimer1 <= 0.25f && mLastCountdownTimer1 > 0.25f)
						{
							Director.FadeIn(0.25f);
							RemoveSelectCharacterFromManager();
							GotoPlayScreen();
						}
					}
					break;
				case Page.SelectChapter:
					SpriteManager.RemoveSprite(mBlackCoverSprite);
					mBlackCoverSprite.RelativePosition.Z = 0.1f;
					mBlackCoverSprite.RelativePosition.Y = 0f;
					mBlackCoverSprite.Alpha = 0.9f;
					mBlackCoverSprite.AlphaRate = 0f;
					mBlackCoverSprite.AttachTo(ChapterSelectUI.Instance, false);
					SpriteManager.AddToLayer(mBlackCoverSprite, GUIHelper.UILayer);
					ChapterSelectUI.Instance.RelativePosition = new Vector3(0f, 4f, -65f);
					ChapterSelectUI.Instance.AttachTo(World.Camera, false);
					ChapterSelectUI.Instance.IsActivated = true;
					ChapterSelectUI.Instance.OnOk = SelectChapterUIDidOk;
					ChapterSelectUI.Instance.OnBack = SelectChapterUIDidBack;
					ChapterSelectUI.Instance.Show(mCurrentSelectPlayerProfile);
					SpriteManager.AddPositionedObject(ChapterSelectUI.Instance);
					ControlHint.Instance.Clear().AddHint(524288, "开始").AddHint(1048576, "返回")
						.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
					mIsTransitionEnd = true;
					break;
				case Page.HelpAndOptions:
					mBlackCoverSprite.RelativePosition.Z = -0.1f;
					mBlackCoverSprite.RelativePosition.Y = 0f;
					mBlackCoverSprite.Alpha = 0.9f;
					mBlackCoverSprite.AttachTo(HelpOptionUI.Instance, false);
					SpriteManager.AddToLayer(mBlackCoverSprite, GUIHelper.UILayer);
					mSelectCharARESLogoSprite.Visible = false;
					HelpOptionUI.Instance.RelativePosition = new Vector3(0f, 0f, -65f);
					HelpOptionUI.Instance.IsActivated = true;
					HelpOptionUI.Instance.IsImmediatelyRemoveProcessWhenExit = false;
					HelpOptionUI.Instance.OnBackButtonPressed = HelpOptionUIDidBackButtonPressed;
					if (mCurrentSelectPlayerProfile.UnlockedLevel == 0)
					{
						HelpOptionUI.Instance.IsShowBetweenSelectCharacterAndEnterPlayScreen = true;
						HelpOptionUI.Instance.OnOkButtonPressed = HelpOptionUIOnOkButtonPressed;
						HelpOptionUI.Instance.Show(HelpOptionUI.Page.Settings);
					}
					else
					{
						HelpOptionUI.Instance.Show(HelpOptionUI.Page.Settings);
					}
					mIsTransitionEnd = true;
					break;
				}
				break;
			case Page.Leaderboard:
				if (newPage == Page.Main)
				{
					if (mIsJustChangePage)
					{
						mCountdownTimer1 = 0.3f;
						LeaderboardUI.Instance.IsActivated = false;
						LeaderboardUI.Instance.RelativeVelocity.X = 400f;
						LeaderboardUI.Instance.OnBackButtonPressed = null;
					}
					if (mCountdownTimer1 <= 0f)
					{
						LeaderboardUI.Instance.Detach();
						LeaderboardUI.Instance.RelativeVelocity = Vector3.Zero;
						LeaderboardUI.Instance.Hide();
						AddMainMenuComponentsToManager();
						EndTransitionToMainState();
						mIsTransitionEnd = true;
					}
					mCountdownTimer1 -= TimeManager.SecondDifference;
				}
				break;
			case Page.Achievements:
				if (newPage == Page.Main)
				{
					if (mIsJustChangePage)
					{
						mCountdownTimer1 = 0.3f;
						AchievementUI.Instance.IsActivated = false;
						AchievementUI.Instance.RelativeVelocity.X = 400f;
						AchievementUI.Instance.OnBackButtonPressed = null;
					}
					if (mCountdownTimer1 <= 0f)
					{
						AchievementUI.Instance.Detach();
						AchievementUI.Instance.RelativeVelocity = Vector3.Zero;
						AchievementUI.Instance.Hide();
						AddMainMenuComponentsToManager();
						EndTransitionToMainState();
						mIsTransitionEnd = true;
					}
					mCountdownTimer1 -= TimeManager.SecondDifference;
				}
				break;
			case Page.HelpAndOptions:
				if (newPage == Page.Main)
				{
					if (mIsJustChangePage)
					{
						mCountdownTimer1 = 0.3f;
						HelpOptionUI.Instance.IsActivated = false;
						HelpOptionUI.Instance.RelativeVelocity.X = 400f;
						HelpOptionUI.Instance.OnBackButtonPressed = HelpOptionUIDidBackButtonPressed;
					}
					if (mCountdownTimer1 <= 0f)
					{
						HelpOptionUI.Instance.Detach();
						HelpOptionUI.Instance.RelativeVelocity = Vector3.Zero;
						HelpOptionUI.Instance.Hide();
						SpriteManager.RemoveSprite(mBlackCoverSprite);
						AddMainMenuComponentsToManager();
						EndTransitionToMainState();
						mIsTransitionEnd = true;
					}
					mCountdownTimer1 -= TimeManager.SecondDifference;
				}
				if (newPage == Page.SelectCharacter)
				{
					SpriteManager.RemoveSprite(mBlackCoverSprite);
					HelpOptionUI.Instance.IsActivated = false;
					HelpOptionUI.Instance.IsImmediatelyRemoveProcessWhenExit = false;
					HelpOptionUI.Instance.OnBackButtonPressed = HelpOptionUIDidBackButtonPressed;
					if (HelpOptionUI.Instance.IsShowBetweenSelectCharacterAndEnterPlayScreen)
					{
						HelpOptionUI.Instance.IsShowBetweenSelectCharacterAndEnterPlayScreen = false;
						HelpOptionUI.Instance.OnBackButtonPressed = HelpOptionUIOnOkButtonPressed;
					}
					HelpOptionUI.Instance.Hide();
					mSelectCharARESLogoSprite.Visible = true;
					ManageControlHintForSelectCharacter();
					mCharacterDetailPanel.SetVisualComponentsToCurrentProfile();
					mIsTransitionEnd = true;
				}
				break;
			case Page.SelectChapter:
				if (newPage == Page.SelectCharacter && mIsJustChangePage)
				{
					SpriteManager.RemoveSprite(mBlackCoverSprite);
					ChapterSelectUI.Instance.IsActivated = false;
					ChapterSelectUI.Instance.Hide();
					SpriteManager.RemovePositionedObject(ChapterSelectUI.Instance);
					ManageControlHintForSelectCharacter();
					mIsTransitionEnd = true;
				}
				if (newPage == Page.EnterPlayScreen)
				{
					if (mIsJustChangePage)
					{
						SpriteManager.RemoveSprite(mBlackCoverSprite);
						ChapterSelectUI.Instance.IsActivated = false;
						ChapterSelectUI.Instance.Hide();
						SpriteManager.RemovePositionedObject(ChapterSelectUI.Instance);
						Director.FadeOut(0.25f);
						BGMManager.FadeOff();
						mCountdownTimer1 = 0.5f;
					}
					if (mCountdownTimer1 > 0f)
					{
						mLastCountdownTimer1 = mCountdownTimer1;
						mCountdownTimer1 -= TimeManager.SecondDifference;
						if (mCountdownTimer1 <= 0f)
						{
							if (mLastCountdownTimer1 > 0f)
							{
								mCountdownTimer1 = -1f;
								mIsTransitionEnd = true;
							}
						}
						else if (mCountdownTimer1 <= 0.1f)
						{
							if (mLastCountdownTimer1 > 0.1f)
							{
								LoadingUI.Instance.Show(true, 1);
							}
						}
						else if (mCountdownTimer1 <= 0.25f && mLastCountdownTimer1 > 0.25f)
						{
							Director.FadeIn(0.25f);
							RemoveSelectCharacterFromManager();
							GotoPlayScreen();
						}
					}
				}
				break;
			case Page.Marketplace:
				if (newPage == Page.Main)
				{
					if (mIsJustChangePage)
					{
						mCountdownTimer1 = 0.2f;
					}
					if (mCountdownTimer1 <= 0f)
					{
						AddMainMenuComponentsToManager();
						EndTransitionToMainState();
						mIsTransitionEnd = true;
					}
					mCountdownTimer1 -= TimeManager.SecondDifference;
				}
				break;
			default:
				mIsTransitionEnd = true;
				break;
			}
			switch (newPage)
			{
			case Page.BackToStartScreen:
				mIsTransitionEnd = false;
				if (mIsJustChangePage)
				{
					mCountdownTimer1 = 0.7f;
					LoadingUI.Instance.Hide();
					Director.FadeOut(0.5f);
				}
				if (mCountdownTimer1 > 0f)
				{
					mCountdownTimer1 -= TimeManager.SecondDifference;
					if (mCountdownTimer1 <= 0f)
					{
						if (currentPage == Page.Main)
						{
							RemoveMainMenuComponentsFromManager();
						}
						GotoStartScreen();
						mIsTransitionEnd = true;
					}
				}
				break;
			case Page.UnlockFullVersion:
				mIsTransitionEnd = false;
				if (mIsJustChangePage)
				{
					mCountdownTimer1 = 0.7f;
					Director.FadeOut(0.5f);
				}
				if (mCountdownTimer1 > 0f)
				{
					mCountdownTimer1 -= TimeManager.SecondDifference;
					if (mCountdownTimer1 <= 0f)
					{
						GotoUnlockFullVersionScreen();
						mIsTransitionEnd = true;
					}
				}
				break;
			}
		}

		private void SelectChapterUIDidBack()
		{
			ChangePage(Page.SelectCharacter);
		}

		private void SelectChapterUIDidOk()
		{
			ChangePage(Page.EnterPlayScreen);
		}

		private void LeaderboardUIDidBackButtonPressed()
		{
			ChangePage(Page.Main);
		}

		private void AchievementUIDidBackButtonPressed()
		{
			ChangePage(Page.Main);
		}

		private void HelpOptionUIDidBackButtonPressed()
		{
			if (mPreviousPage == Page.Main)
			{
				ChangePage(Page.Main);
			}
			else if (mPreviousPage == Page.SelectCharacter)
			{
				ChangePage(Page.SelectCharacter);
			}
		}

		private void HelpOptionUIOnOkButtonPressed()
		{
			if (mPreviousPage == Page.SelectCharacter)
			{
				IsGoToPlayScreenAfterEnterToSelectCharacter = true;
				ChangePage(Page.SelectCharacter);
			}
		}

		private void AddMainMenuComponentsToManager()
		{
			mMainMenuCursorSprite.RelativePosition = new Vector3(0f, 0f, -0.1f);
			mMainMenuARESLogoSprite.RelativePosition = new Vector3(20f, 13.5f, 0f);
			for (int i = 0; i < mMainMenuItemCount; i++)
			{
				Text text = mMainMenuItems[i].text;
				text.RelativePosition = new Vector3(14f, (float)(1 - 3 * i), 0f);
				TextManager.AddToLayer(text, GUIHelper.UILayer);
			}
			SpriteManager.AddPositionedObject(mMainMenuRootObject);
			SpriteManager.AddToLayer(mMainMenuCursorSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mMainMenuARESLogoSprite, GUIHelper.UILayer);
		}

		private void RemoveMainMenuComponentsFromManager()
		{
			SpriteManager.RemoveSpriteOneWay(mMainMenuCursorSprite);
			SpriteManager.RemoveSpriteOneWay(mMainMenuARESLogoSprite);
			for (int i = 0; i < mMainMenuItemCount; i++)
			{
				Text text = mMainMenuItems[i].text;
				TextManager.RemoveTextOneWay(text);
			}
			if (mMainMenuRootObject != null)
			{
				SpriteManager.RemovePositionedObject(mMainMenuRootObject);
			}
			ControlHint.Instance.HideHints();
		}

		private void EndTransitionToMainState()
		{
			mMainMenuRootObject.RelativePosition = new Vector3(0f, 0f, -70f);
			UpdateMainMenuCursorSprite();
			ControlHint.Instance.Clear().AddHint(524288, "选择").AddHint(1048576, "返回")
				.ShowHints(HorizontalAlignment.Right);
		}

		private void ExecuteSelectedMainMenuItem()
		{
			switch (mMainMenuItems[mMainMenuCursor].tag)
			{
			case 1:
				ChangePage(Page.SelectCharacter);
				break;
			case 3:
				if (Pref.Steamworks)
				{
					ChangePage(Page.Leaderboard);
				}
				else
				{
					GuideMessageBoxWrapper.Instance.Show("警告", "你必须登陆Steam才能查看排行榜。", null, 1, "知道", string.Empty, string.Empty);
				}
				break;
			case 4:
				ChangePage(Page.Achievements);
				break;
			case 2:
				ChangePage(Page.HelpAndOptions);
				break;
			case 5:
				PopUpUI.Instance.Show("退出游戏", "是否要退出游戏？\n当前进度将会丢失。", PopUpUIAskForExitGame);
				break;
			}
		}

		private bool MoveMainMenuCursor(sbyte direction)
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
			int num2 = mMainMenuCursor + num;
			if (num2 >= 0 && num2 < mMainMenuItemCount)
			{
				mMainMenuCursorMovingTimeLeft = 0.05f;
				mMainMenuCursorSprite.RelativeVelocity.Y = (float)(-num) * 60f;
				mMainMenuCursor = num2;
				return true;
			}
			return false;
		}

		private bool SelectMainMenuItem(int index)
		{
			int num = index - mMainMenuCursor;
			if (num != 0)
			{
				mMainMenuCursorMovingTimeLeft = 0.05f;
				mMainMenuCursorSprite.RelativeVelocity.Y = (float)(-num) * 60f;
				mMainMenuCursor = index;
				return true;
			}
			return false;
		}

		private void UpdateMainMenuCursorSprite()
		{
			if (mMainMenuCursorMovingTimeLeft <= 0f)
			{
				mMainMenuCursorMovingTimeLeft = -5f;
				mMainMenuCursorSprite.RelativeVelocity.Y = 0f;
				mMainMenuCursorSprite.RelativePosition.Y = (float)(1 - mMainMenuCursor * 3);
			}
			else
			{
				mMainMenuCursorMovingTimeLeft -= TimeManager.LastSecondDifference;
			}
		}

		private void AddSelectCharacterComponentsToManager()
		{
			mSelectCharRootObject.RelativePosition = new Vector3(0f, 2f, -66f);
			mSelectCharHeaderSprite.RelativePosition = new Vector3(0f, 15f, 0.02f);
			mSelectCharARESLogoSprite.RelativePosition = new Vector3(0f, -19f, 0.02f);
			mSelectCharAresSprite.RelativePosition = new Vector3(-20f, 0f, 0f);
			mSelectCharTarusSprite.RelativePosition = new Vector3(20f, 0f, 0f);
			mSelectCharBackgroundPanelSprite.RelativePosition = new Vector3(0f, 0f, -0.02f);
			mSelectCharAresSelectedFrameSprite.RelativePosition = new Vector3(14.625f, 0f, 0.01f);
			mSelectCharTarusSelectedFrameSprite.RelativePosition = new Vector3(-14.625f, 0f, 0.01f);
			mSelectCharAresSelectedOuterFrameSprite.RelativePosition = new Vector3(-29.25f, 0f, 0f);
			mSelectCharTarusSelectedOuterFrameSprite.RelativePosition = new Vector3(29.25f, 0f, 0f);
			SpriteManager.AddToLayer(mSelectCharHeaderSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectCharARESLogoSprite, SpriteManager.TopLayer);
			SpriteManager.AddToLayer(mSelectCharAresSelectedFrameSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectCharTarusSelectedFrameSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectCharAresSelectedOuterFrameSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectCharTarusSelectedOuterFrameSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectCharAresSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectCharTarusSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectCharBackgroundPanelSprite, GUIHelper.UILayer);
			float y = 4.5f - World.Camera.GetYViewVolByDistance(85f);
			float xViewVolByDistance = World.Camera.GetXViewVolByDistance(85f);
			mSelectCharResetProgress.text.RelativePosition = new Vector3(0f - xViewVolByDistance + 6f, y, -85f);
			TextManager.AddToLayer(mSelectCharResetProgress.text, SpriteManager.TopLayer);
			mCharacterDetailPanel.AttachTo(mSelectCharRootObject, false);
			mCharacterDetailPanel.RelativePosition = new Vector3(-27.1f, -10.7833f, 0.03f);
			ProcessManager.AddProcess(mCharacterDetailPanel);
			mSelectCharRootObject.AttachTo(World.Camera, false);
			if (ProfileManager.Current == null)
			{
				SelectCharacter(0);
			}
			else if (ProfileManager.Current.PlayerType == PlayerType.Mars)
			{
				SelectCharacter(0);
			}
			else if (ProfileManager.Current.PlayerType == PlayerType.Tarus)
			{
				SelectCharacter(1);
			}
		}

		private void RemoveSelectCharacterFromManager()
		{
			if (mSelectCharRootObject != null)
			{
				mSelectCharRootObject.Detach();
				SpriteManager.RemovePositionedObject(mSelectCharRootObject);
			}
			TextManager.RemoveTextOneWay(mSelectCharResetProgress.text);
			SpriteManager.RemoveSpriteOneWay(mSelectCharHeaderSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharARESLogoSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharAresSelectedFrameSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharTarusSelectedFrameSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharAresSelectedOuterFrameSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharTarusSelectedOuterFrameSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharAresSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharTarusSprite);
			SpriteManager.RemoveSpriteOneWay(mSelectCharBackgroundPanelSprite);
			if (mCharacterDetailPanel != null)
			{
				ProcessManager.RemoveProcess(mCharacterDetailPanel);
			}
			ControlHint.Instance.HideHints();
		}

		private void ExecuteSelectedCharacter()
		{
			if (mCurrentSelectPlayerProfile.UnlockedLevel <= 0)
			{
				SFXManager.PlaySound("ok");
				ChangePage(Page.HelpAndOptions);
			}
			else
			{
				SFXManager.PlaySound("New_StartGame");
				ChangePage(Page.EnterPlayScreen);
			}
		}

		private void SelectCharacter(int characterIndex)
		{
			mCurrentSelectedCharacterIndex = characterIndex;
			if (mCurrentSelectedCharacterIndex == 0)
			{
				mSelectCharAresSprite.Alpha = 1f;
				mSelectCharAresSelectedFrameSprite.Visible = true;
				mSelectCharAresSelectedOuterFrameSprite.Visible = true;
				mSelectCharTarusSprite.Alpha = 0.25f;
				mSelectCharTarusSelectedFrameSprite.Visible = false;
				mSelectCharTarusSelectedOuterFrameSprite.Visible = false;
				mCurrentSelectPlayerProfile = ProfileManager.Mars;
				mCharacterDetailPanel.RelativePosition.X = -27.1f;
			}
			else if (mCurrentSelectedCharacterIndex == 1)
			{
				mSelectCharTarusSprite.Alpha = 1f;
				mSelectCharTarusSelectedFrameSprite.Visible = true;
				mSelectCharTarusSelectedOuterFrameSprite.Visible = true;
				mSelectCharAresSprite.Alpha = 0.25f;
				mSelectCharAresSelectedFrameSprite.Visible = false;
				mSelectCharAresSelectedOuterFrameSprite.Visible = false;
				mCurrentSelectPlayerProfile = ProfileManager.Tarus;
				mCharacterDetailPanel.RelativePosition.X = 27.1f;
			}
			ProfileManager.Current = mCurrentSelectPlayerProfile;
			mCharacterDetailPanel.SetProfile(mCurrentSelectPlayerProfile);
			ManageControlHintForSelectCharacter();
		}

		private void ManageControlHintForSelectCharacter()
		{
			ControlHint.Instance.Clear().AddHint(16777216, "设置").AddHint(8388608, "章节")
				.AddHint(524288, (mCurrentSelectPlayerProfile.UnlockedLevel <= 0) ? "选择" : "继续")
				.AddHint(1048576, "返回")
				.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
			if (mCurrentSelectPlayerProfile.UnlockedLevel <= 0)
			{
				ControlHint.Instance.SetHintActive(8388608, false);
			}
			else
			{
				ControlHint.Instance.SetHintActive(8388608, true);
			}
		}

		private void LoadPlayScreen()
		{
			if (World.CurrentLevel == 1 && !ProfileManager.Current.HasShownCutscene(1))
			{
				ProfileManager.Current.SetHasShownCutscene(1, true);
				ScreenManager.NextScreen = VideoScreen.Instance;
				VideoScreen.Instance.NextScreen = PlayScreen.Instance;
				VideoScreen.Instance.VideoName = "Content/Video/INTRO";
				VideoScreen.Instance.AudioName = null;
				LoadNextScreen(VideoScreen.Instance.Load);
			}
			else if (World.CurrentLevel == 5 && !ProfileManager.Current.HasShownCutscene(5))
			{
				ProfileManager.Current.SetHasShownCutscene(5, true);
				ScreenManager.NextScreen = VideoScreen.Instance;
				VideoScreen.Instance.NextScreen = PlayScreen.Instance;
				VideoScreen.Instance.VideoName = "Content/Video/CARSON";
				VideoScreen.Instance.AudioName = null;
				LoadNextScreen(VideoScreen.Instance.Load);
			}
			else
			{
				ScreenManager.NextScreen = PlayScreen.Instance;
				PlayScreen instance = PlayScreen.Instance;
				LoadNextScreen(((Screen)instance).Load);
			}
		}

		public void LoadMainMenu()
		{
			ChapterSelectUI.Instance.Load();
			LoadMainMenuScreenContent();
			base.LoadingDone = true;
		}

		public void UnloadMainMenu()
		{
			ChapterSelectUI.Instance.Unload();
			UnloadMainMenuScreenContent();
			FlatRedBallServices.Unload("MenuScreen");
			BGMManager.Unload();
			base.LoadingDone = false;
		}

		public override void Destroy()
		{
		}

		public override void OnCurrentGamerSignedOut()
		{
			if (mPage == Page.EnterPlayScreen)
			{
				mBackToStartScreenWhileGoingToPlayScreen = true;
			}
			else
			{
				ChangePage(Page.BackToStartScreen);
			}
		}

		private Text CreateText(string displayText)
		{
			Text text = new Text(GUIHelper.CaptionFont);
			text.Scale = 1f;
			text.Spacing = 1f;
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.VerticalAlignment = VerticalAlignment.Center;
			text.ColorOperation = ColorOperation.Texture;
			text.DisplayText = displayText;
			return text;
		}
	}
}
