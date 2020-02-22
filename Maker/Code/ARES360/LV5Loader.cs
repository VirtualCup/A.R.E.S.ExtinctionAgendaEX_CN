using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Entity.Action;
using ARES360.Event;
using ARES360.Profile;
using ARES360.Screen;
using ARES360.UI;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ARES360
{
	public static class LV5Loader
	{
		private const float TramSpeed = -180f;

		public static ExplodeableLevel[] ScrollPlatform;

		public static ExplodeableLevel[] ScrollBox;

		public static ModelObject[] OculusCage;

		private static LoopedBackground mLoopedRails;

		private static LoopedBackground mLoopedScenary;

		private static UpgradeChip mShockUpgradeChip;

		private static UpgradeChip mSpecialUpgradeChip;

		private static bool mTramHasStarted = false;

		private static Dictionary<int, int> mReward;

		public static void LoadEnemy()
		{
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.FlyingPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Turret);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronCrawler);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Bomber);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShooter);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronWalker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Shieldren);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodDropper);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Slither);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Oculus);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShocker, 2);
			EnemyManager.CreateAndStoreEnemy(EnemyType.FlyingPod, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Turret, 3);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronCrawler, 3);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Bomber, 8);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShooter, 3);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronWalker, 3);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodDropper, 35);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Shieldren, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Slither, 20);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Oculus, 1);
			if (Player.Instance.Type == PlayerType.Mars)
			{
				EnemyManager.CreateAndStoreEnemy(EnemyType.BadTarus, 1);
			}
			else
			{
				EnemyManager.CreateAndStoreEnemy(EnemyType.BadMars, 1);
			}
		}

		public static void AddLoopedBackgrounds()
		{
			ProcessManager.AddProcess(mLoopedRails);
			ProcessManager.AddProcess(mLoopedScenary);
			mLoopedRails.XAcceleration = 0f;
			mLoopedRails.XVelocity = 0f;
			mLoopedRails.MinXVelocity = 0f;
			mLoopedScenary.XAcceleration = 0f;
			mLoopedScenary.XVelocity = 0f;
			mLoopedScenary.MinXVelocity = 0f;
			Vector3 checkPoint = World.CheckPoint;
			if (checkPoint.X < 1250f)
			{
				UseRailsOnly();
			}
			else if (checkPoint.X < 5650f)
			{
				UseRailsOnly();
				mLoopedRails.XVelocity = -180f;
				mLoopedScenary.XVelocity = -180f;
				mLoopedRails.MinXVelocity = -180f;
				mLoopedScenary.MinXVelocity = -180f;
			}
			else if (checkPoint.X < 9450f)
			{
				UseRailsWithTunnel();
				mLoopedRails.XVelocity = -180f;
				mLoopedScenary.XVelocity = -180f;
				mLoopedRails.MinXVelocity = -180f;
				mLoopedScenary.MinXVelocity = -180f;
			}
			else
			{
				UseRailsOnly();
				mLoopedRails.XVelocity = -180f;
				mLoopedScenary.XVelocity = -180f;
				mLoopedRails.MinXVelocity = -180f;
				mLoopedScenary.MinXVelocity = -180f;
			}
			if (World.Camera.Mode == CameraMode.SideScrolling)
			{
				World.Camera.ForceSideScrollingDirection(World.Camera.SideScrollingDirection);
			}
		}

		private static void UseRailsWithTunnel()
		{
			mLoopedRails.StartRoomID = 32;
			mLoopedRails.TotalRooms = 4;
		}

		private static void UseRailsOnly()
		{
			mLoopedRails.StartRoomID = 26;
			mLoopedRails.TotalRooms = 4;
		}

		public static void LoadObject(out List<GameObject>[] objectInRoom)
		{
			objectInRoom = new List<GameObject>[40];
			objectInRoom[34] = new List<GameObject>();
			mReward = new Dictionary<int, int>(3);
			mTramHasStarted = false;
			Texture2D texture = FlatRedBallServices.Load<Texture2D>("Content/World/m_SM_LV2_box_destroyable_02", "PlayWorld");
			mLoopedRails = new LoopedBackground(1, new AxisAlignedRectangle(4500f, 128f, new Vector3(5684f, 536f, 0f)));
			mLoopedRails.StartOffset = new Vector3(0f, 392f, 0f);
			mLoopedRails.XVelocity = 0f;
			mLoopedRails.SightRadius = 200f;
			UseRailsOnly();
			mLoopedScenary = new LoopedBackground(3751, new AxisAlignedRectangle(4500f, 128f, new Vector3(5684f, 536f, 0f)));
			mLoopedScenary.StartOffset = new Vector3(0f, 370f, 0f);
			mLoopedScenary.SightRadius = 230f;
			mLoopedScenary.StartRoomID = 36;
			mLoopedScenary.TotalRooms = 3;
			mLoopedScenary.XVelocity = 0f;
			for (int i = 31; i <= 35; i++)
			{
				Room room = World.GetRoom(i);
				room.Level = new List<AxisAlignedRectangle>((i == 34) ? 1 : 2);
				AxisAlignedRectangle axisAlignedRectangle = new AxisAlignedRectangle();
				axisAlignedRectangle.ScaleX = room.Width / 2f;
				axisAlignedRectangle.ScaleY = 8f;
				axisAlignedRectangle.RelativePosition = new Vector3(room.Width / 2f, room.Height + 8f + 0.01f, 0f);
				room.Level.Add(axisAlignedRectangle);
				if (i == 34)
				{
					axisAlignedRectangle = new AxisAlignedRectangle(1f, 1f);
					axisAlignedRectangle.RelativePosition = new Vector3(78f, room.Height - 12f, 0f);
					axisAlignedRectangle.XVelocity = -1f;
					room.Level.Add(axisAlignedRectangle);
					CrushChecker crushChecker = new CrushChecker(Vector3.Zero, 12f, 12f, true);
					crushChecker.AttachTo(axisAlignedRectangle, false);
					crushChecker.RelativePosition = new Vector3(0f, 0f, 0f);
					objectInRoom[34].Add(crushChecker);
				}
			}
			objectInRoom[0] = new List<GameObject>();
			objectInRoom[1] = new List<GameObject>();
			objectInRoom[2] = new List<GameObject>();
			objectInRoom[3] = new List<GameObject>();
			objectInRoom[3].Add(new AutoScrollPlatform(new Vector3(800f, 128f, 0f), new Vector3(800f, 560f, 0f), 20f));
			ScrollPlatform = new ExplodeableLevel[12];
			ScrollPlatform[0] = new ExplodeableLevel(new Vector3(784f, 188f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[1] = new ExplodeableLevel(new Vector3(816f, 220f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[2] = new ExplodeableLevel(new Vector3(784f, 252f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[3] = new ExplodeableLevel(new Vector3(800f, 300f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[4] = new ExplodeableLevel(new Vector3(800f, 332f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[5] = new ExplodeableLevel(new Vector3(816f, 364f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[6] = new ExplodeableLevel(new Vector3(784f, 404f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[7] = new ExplodeableLevel(new Vector3(816f, 436f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[8] = new ExplodeableLevel(new Vector3(800f, 468f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[9] = new ExplodeableLevel(new Vector3(816f, 516f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[10] = new ExplodeableLevel(new Vector3(784f, 516f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[11] = new ExplodeableLevel(new Vector3(816f, 556f, 0f), 16f, 4f, false, "Content/World/m_lv_platform4x1", new Vector3(0f, 0f, -6f), Vector3.One);
			ScrollPlatform[3].ObjectToApplyGravityWhenDestroyed = World.Chip[0];
			for (int j = 0; j < 12; j++)
			{
				CrushChecker crushChecker2 = new CrushChecker(Vector3.Zero, 14f, 2f, true);
				crushChecker2.AttachTo(ScrollPlatform[j], false);
				crushChecker2.RelativePosition = new Vector3(0f, -2.1f, 0f);
				objectInRoom[3].Add(ScrollPlatform[j]);
				objectInRoom[3].Add(crushChecker2);
			}
			ScrollBox = new ExplodeableLevel[8];
			ScrollBox[0] = new ExplodeableLevel(new Vector3(824f, 232f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			ScrollBox[1] = new ExplodeableLevel(new Vector3(776f, 264f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			ScrollBox[2] = new ExplodeableLevel(new Vector3(792f, 312f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			ScrollBox[3] = new ExplodeableLevel(new Vector3(808f, 344f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			ScrollBox[4] = new ExplodeableLevel(new Vector3(776f, 416f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			ScrollBox[5] = new ExplodeableLevel(new Vector3(808f, 448f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			ScrollBox[6] = new ExplodeableLevel(new Vector3(792f, 480f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			ScrollBox[7] = new ExplodeableLevel(new Vector3(824f, 568f, 0f), 8f, 8f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			for (int k = 0; k < 8; k++)
			{
				CrushChecker crushChecker3 = new CrushChecker(Vector3.Zero, 6f, 2f, true);
				crushChecker3.AttachTo(ScrollBox[k], false);
				crushChecker3.RelativePosition = new Vector3(0f, -6.01f, 0f);
				objectInRoom[3].Add(ScrollBox[k]);
				objectInRoom[3].Add(crushChecker3);
			}
			objectInRoom[4] = new List<GameObject>();
			objectInRoom[5] = new List<GameObject>();
			GasTank gasTank = new GasTank(2, new Vector3(1184f, 664f, 0f));
			gasTank.ObjectToApplyGravityWhenDestroyed = World.DataCube[2];
			objectInRoom[5].Add(gasTank);
			objectInRoom[5].Add(new Conveyor(10, new Vector3(1160f, 644f, 0f), 30f, -1));
			objectInRoom[6] = new List<GameObject>();
			objectInRoom[6].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(952f, 540f, 0f), new Vector3(1000f, 540f, 0f), 20f, 0f, "Content/World/m_lv2_elevator_4", new Vector3(0f, 4f, -2f)));
			objectInRoom[6].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(912f, 532f, 0f), new Vector3(912f, 444f, 0f), 24f, 0f, "Content/World/m_lv2_elevator_4", new Vector3(0f, 4f, -2f)));
			objectInRoom[6].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(1048f, 436f, 0f), new Vector3(952f, 436f, 0f), 24f, 0f, "Content/World/m_lv2_elevator_4", new Vector3(0f, 4f, -2f)));
			ProtonBox protonBox = new ProtonBox(new Vector3(1564f, 436f, 0f), false);
			protonBox.HiddenGameObject = World.Chip[1];
			objectInRoom[7] = new List<GameObject>();
			objectInRoom[7].Add(protonBox);
			objectInRoom[8] = new List<GameObject>();
			objectInRoom[8].Add(new CrackedBox(new Vector3(1800f, 448f, 0f), 2, false, texture));
			objectInRoom[8].Add(new CrackedBox(new Vector3(1816f, 456f, 0f), 2, false, texture));
			objectInRoom[8].Add(new CrackedBox(new Vector3(1840f, 432f, 0f), 2, false, texture));
			objectInRoom[8].Add(new CrackedBox(new Vector3(1888f, 432f, 0f), 2, false, texture));
			objectInRoom[8].Add(new CrackedBox(new Vector3(1908f, 436f, 0f), 3, false, texture));
			objectInRoom[9] = new List<GameObject>();
			objectInRoom[9].Add(new CrackedBox(new Vector3(2512f, 448f, 0f), 2, true, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2512f, 432f, 0f), 2, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2528f, 448f, 0f), 2, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2548f, 436f, 0f), 3, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2568f, 448f, 0f), 2, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2584f, 448f, 0f), 2, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2632f, 456f, 0f), 2, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2652f, 436f, 0f), 3, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2672f, 448f, 0f), 2, true, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2672f, 432f, 0f), 2, false, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2688f, 448f, 0f), 2, true, texture));
			objectInRoom[9].Add(new CrackedBox(new Vector3(2688f, 432f, 0f), 2, false, texture));
			objectInRoom[10] = new List<GameObject>();
			objectInRoom[11] = new List<GameObject>();
			objectInRoom[11].Add(new ExplosiveBox(new Vector3(3300f, 436f, 0f), 3, false));
			objectInRoom[11].Add(new ExplosiveBox(new Vector3(3324f, 436f, 0f), 3, false));
			objectInRoom[11].Add(new ExplosiveBox(new Vector3(3348f, 436f, 0f), 3, false));
			objectInRoom[11].Add(new ExplosiveBox(new Vector3(3388f, 436f, 0f), 3, false));
			objectInRoom[11].Add(new ExplosiveBox(new Vector3(3412f, 436f, 0f), 3, false));
			objectInRoom[11].Add(new ExplosiveBox(new Vector3(3436f, 436f, 0f), 3, false));
			objectInRoom[12] = new List<GameObject>();
			objectInRoom[13] = new List<GameObject>();
			objectInRoom[14] = new List<GameObject>();
			objectInRoom[14].Add(new CrackedBox(new Vector3(4328f, 432f, 0f), 2, false, texture));
			objectInRoom[14].Add(new CrackedBox(new Vector3(4384f, 432f, 0f), 2, false, texture));
			objectInRoom[14].Add(new CrackedBox(new Vector3(4420f, 460f, 0f), 3, false, texture));
			objectInRoom[14].Add(new ExplodeableLevel(new Vector3(4328f, 448f, 0f), 8f, 8f, true, "Content/World/m_sm_box_small", new Vector3(0f, -8f, 0f), new Vector3(1f, 1f, 0.33f)));
			objectInRoom[14].Add(new ExplodeableLevel(new Vector3(4404f, 436f, 0f), 12f, 12f, false, "Content/World/m_sm_box_small", new Vector3(0f, -12f, 0f), new Vector3(1.5f, 1.5f, 0.5f)));
			objectInRoom[14].Add(new ExplodeableLevel(new Vector3(4444f, 436f, 0f), 12f, 12f, false, "Content/World/m_sm_box_small", new Vector3(0f, -12f, 0f), new Vector3(1.5f, 1.5f, 0.5f)));
			objectInRoom[14].Add(new ExplosiveBox(new Vector3(4444f, 460f, 0f), 3, false));
			objectInRoom[14].Add(new ExplosiveBox(new Vector3(4468f, 436f, 0f), 3, false));
			objectInRoom[15] = new List<GameObject>();
			objectInRoom[16] = new List<GameObject>(4);
			OculusCage = new ModelObject[4];
			string[] array = new string[2]
			{
				"Content/World/lv_traincar_boss_cage",
				"Content/World/lv_traincar_boss_cage_r"
			};
			for (int l = 0; l < 4; l++)
			{
				OculusCage[l] = new ModelObject(new Vector3(5416f, 424f, 0f), new Vector3(0f, 3.14159274f * (float)(l % 2), 0f), 24f, 24f, false, array[l / 2], Vector3.Zero, new Vector3(1f, 1f, 0.8f));
				OculusCage[l].AddColliderWhenRegister = false;
				objectInRoom[16].Add(OculusCage[l]);
			}
			objectInRoom[17] = new List<GameObject>();
			objectInRoom[18] = new List<GameObject>();
			objectInRoom[19] = new List<GameObject>();
			objectInRoom[19].Add(new CrackedBox(new Vector3(6720f, 456f, 0f), 2, true, texture));
			objectInRoom[19].Add(new CrackedBox(new Vector3(6708f, 436f, 0f), 3, false, texture));
			objectInRoom[19].Add(new CrackedBox(new Vector3(6732f, 436f, 0f), 3, false, texture));
			objectInRoom[19].Add(new ExplodeableLevel(new Vector3(6704f, 456f, 0f), 8f, 8f, true, "Content/World/m_sm_box_small", new Vector3(0f, -8f, 0f), new Vector3(1f, 1f, 0.33f)));
			objectInRoom[19].Add(new ExplodeableLevel(new Vector3(6736f, 456f, 0f), 8f, 8f, true, "Content/World/m_sm_box_small", new Vector3(0f, -8f, 0f), new Vector3(1f, 1f, 0.33f)));
			objectInRoom[20] = new List<GameObject>();
			objectInRoom[20].Add(new ExplosiveBox(new Vector3(7140f, 436f, 0f), 3, false));
			objectInRoom[20].Add(new ExplosiveBox(new Vector3(7164f, 436f, 0f), 3, false));
			objectInRoom[20].Add(new ExplosiveBox(new Vector3(7188f, 436f, 0f), 3, false));
			objectInRoom[20].Add(new ExplosiveBox(new Vector3(7228f, 436f, 0f), 3, false));
			objectInRoom[20].Add(new ExplosiveBox(new Vector3(7252f, 436f, 0f), 3, false));
			objectInRoom[20].Add(new ExplosiveBox(new Vector3(7276f, 436f, 0f), 3, false));
			objectInRoom[21] = new List<GameObject>();
			objectInRoom[21].Add(new CrackedBox(new Vector3(7636f, 436f, 0f), 3, false, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7676f, 436f, 0f), 3, false, texture));
			CrackedBox crackedBox = new CrackedBox(new Vector3(7732f, 436f, 0f), 3, false, texture);
			crackedBox.HiddenGameObject = World.Chip[2];
			objectInRoom[21].Add(crackedBox);
			objectInRoom[21].Add(new CrackedBox(new Vector3(7788f, 452f, 0f), 3, false, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7656f, 448f, 0f), 2, false, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7672f, 456f, 0f), 2, true, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7696f, 448f, 0f), 2, false, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7712f, 448f, 0f), 2, false, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7752f, 456f, 0f), 2, false, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7808f, 448f, 0f), 2, true, texture));
			objectInRoom[21].Add(new CrackedBox(new Vector3(7808f, 432f, 0f), 2, false, texture));
			objectInRoom[22] = new List<GameObject>();
			objectInRoom[22].Add(new ExplosiveBox(new Vector3(8462f, 452f, 0f), 3, false));
			objectInRoom[22].Add(new ExplosiveBox(new Vector3(8492f, 436f, 0f), 3, false));
			objectInRoom[22].Add(new ExplosiveBox(new Vector3(8516f, 436f, 0f), 3, false));
			objectInRoom[22].Add(new ExplosiveBox(new Vector3(8564f, 452f, 0f), 3, false));
			objectInRoom[23] = new List<GameObject>();
			objectInRoom[23].Add(new CrackedBox(new Vector3(8661f, 436f, 0f), 3, false, texture));
			objectInRoom[24] = new List<GameObject>();
			objectInRoom[25] = new List<GameObject>();
			objectInRoom[26] = new List<GameObject>();
			objectInRoom[27] = new List<GameObject>();
			objectInRoom[28] = new List<GameObject>();
			objectInRoom[29] = new List<GameObject>();
			objectInRoom[30] = new List<GameObject>();
			objectInRoom[31] = new List<GameObject>();
			objectInRoom[32] = new List<GameObject>();
			objectInRoom[33] = new List<GameObject>();
			objectInRoom[35] = new List<GameObject>();
			objectInRoom[36] = new List<GameObject>();
			objectInRoom[37] = new List<GameObject>();
			objectInRoom[38] = new List<GameObject>();
			objectInRoom[39] = new List<GameObject>();
			mShockUpgradeChip = new UpgradeChip(7, Vector3.Zero, true);
			mSpecialUpgradeChip = new UpgradeChip(4, Vector3.Zero, true);
		}

		public static void LoadDialog(out Dialog[] dialogList)
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				dialogList = new Dialog[7];
				dialogList[0] = new Dialog(new Chat[3]
				{
					new Chat(71, 1, new string[3]
					{
						"噼……区域……啪……受损严重。",
						"……要小心能量浪潮……噼啪……",
						"…………………………………………………………"
					}),
					new Chat(19, 2, new string[1]
					{
						"那些导管产生的干扰太多了。"
					}),
					new Chat(19, 2, new string[2]
					{
						"过滤算法不是很奏效。",
						"通信已离线。"
					}, "Ares_Illogical")
				});
				dialogList[1] = new Dialog(new Chat[0]);
				dialogList[2] = new Dialog(new Chat[3]
				{
					new Chat(71, 1, new string[2]
					{
						"阿瑞斯，我无法与塔鲁斯建立通信。",
						"有什么东西在妨碍我的信号。"
					}),
					new Chat(16, 2, new string[2]
					{
						"别太紧张，瓦尔基莉，",
						"我会想办法找到他的。"
					}),
					new Chat(70, 1, new string[1]
					{
						"（哼！迟早我也会让你紧张的……）"
					})
				});
				dialogList[3] = new Dialog(new Chat[7]
				{
					new Chat(18, 2, new string[2]
					{
						"检测到重度灾创污染……",
						"塔鲁斯？！"
					}, "Ares_Scan"),
					new Chat(26, 1, new string[2]
					{
						"阿瑞斯，你太迟了……",
						"这边。我们时间不多；快跟上我。"
					}),
					new Chat(16, 2, new string[3]
					{
						"瓦尔基莉，最优先请求：",
						"对我所在的地点进行全面扫描！",
						"塔鲁斯？！"
					}, "Ares_Request"),
					new Chat(19, 2, new string[1]
					{
						"怀疑：塔鲁斯已经被灾创感染了。"
					}),
					new Chat(19, 2, new string[3]
					{
						"…… …… ……",
						"瓦尔基莉？",
						"改道信号……失败。"
					}),
					new Chat(18, 2, new string[2]
					{
						"第二目标：",
						"消灭被灾创感染的机器人。"
					}, "Ares_Request"),
					new Chat(18, 2, new string[2]
					{
						"目标更新……失败。",
						"我……不想……这样做。"
					}, "Ares_Illogical")
				});
				dialogList[4] = new Dialog(new Chat[0]);
				dialogList[5] = new Dialog(new Chat[1]
				{
					new Chat(27, 1, new string[1]
					{
						"啊啊啊啊！"
					}, "Tarus_Infected")
				});
				dialogList[6] = new Dialog(new Chat[9]
				{
					new Chat(18, 2, new string[2]
					{
						"原谅我，塔鲁斯。",
						"我的程序还不够……完美。"
					}, "Ares_Illogical"),
					new Chat(71, 1, new string[2]
					{
						"阿瑞斯！塔鲁斯的信号消失了。",
						"我找不到他的踪迹。"
					}),
					new Chat(19, 2, new string[2]
					{
						"塔鲁斯死了。",
						"我很抱歉，瓦尔基莉。是我毁了他。"
					}),
					new Chat(63, 1, new string[2]
					{
						"什么？！",
						"你怎么能？！"
					}),
					new Chat(16, 2, new string[2]
					{
						"塔鲁斯已经完全陷入灾创的控制。",
						"用最大概率算法也找不到其它更好的选择。"
					}),
					new Chat(57, 1, new string[2]
					{
						"总会有更好的选择的，阿瑞斯！",
						"他是我们的朋友！"
					}),
					new Chat(31, 1, new string[2]
					{
						"你们俩最好把握下现状。",
						"需要我提醒你们世界还处在危险当中吗！"
					}),
					new Chat(18, 2, new string[1]
					{
						"询问：所有人质都安全撤离了吗？"
					}, "Ares_Request"),
					new Chat(32, 1, new string[3]
					{
						"多亏有你，我们都已经离开了，",
						"不过要是我们无法阻止加农炮的话，",
						"离不离开结局都一样！"
					})
				});
			}
			else
			{
				dialogList = new Dialog[7];
				dialogList[0] = new Dialog(new Chat[3]
				{
					new Chat(25, 2, new string[2]
					{
						"请接线，瓦尔基莉。",
						"瓦尔基莉，你能看到吗？"
					}),
					new Chat(25, 2, new string[1]
					{
						"这些能量浪潮一定是干扰信号。"
					}),
					new Chat(25, 2, new string[2]
					{
						"我不确定自己的装甲能否应对这些浪潮。",
						"最好还是避开。"
					})
				});
				dialogList[1] = new Dialog(new Chat[2]
				{
					new Chat(26, 2, new string[3]
					{
						"啊……啊……",
						"必须……",
						"联络……"
					}),
					new Chat(27, 2, new string[1]
					{
						"杀死……"
					}, "Tarus_Infected")
				});
				dialogList[2] = new Dialog(new Chat[0]);
				dialogList[3] = new Dialog(new Chat[1]
				{
					new Chat(25, 2, new string[2]
					{
						"阿瑞斯！",
						"你要去哪？"
					})
				});
				dialogList[4] = new Dialog(new Chat[1]
				{
					new Chat(27, 2, new string[2]
					{
						"啊……",
						"这是在搞什么呀？！"
					}, "Tarus_Argue")
				});
				dialogList[5] = new Dialog(new Chat[7]
				{
					new Chat(25, 2, new string[2]
					{
						"阿瑞斯，你的装甲！",
						"你肯定是被感染了！"
					}, "Tarus_Talk"),
					new Chat(13, 1, new string[2]
					{
						"目标分析完毕。",
						"弱点已确认。"
					}, "Ares_Scan"),
					new Chat(13, 1, new string[2]
					{
						"塔鲁斯型号。劣质机器人。",
						"可以进行歼灭。"
					}, "Ares_Request"),
					new Chat(25, 2, new string[2]
					{
						"瓦尔基莉！",
						"远程关掉阿瑞斯，快！"
					}),
					new Chat(25, 2, new string[3]
					{
						"……",
						"瓦尔基莉？！",
						"该死！"
					}),
					new Chat(13, 1, new string[1]
					{
						"激活：战斗子程序。"
					}, "Ares_Request"),
					new Chat(27, 2, new string[2]
					{
						"对不起，阿瑞斯。",
						"但这是唯一的选择……"
					}, "Tarus_Regret")
				});
				dialogList[6] = new Dialog(new Chat[1]
				{
					new Chat(26, 2, new string[2]
					{
						"阿瑞斯……我辜负了你。",
						"但我不会辜负人类。"
					})
				});
			}
		}

		public static void LoadAmbush(out AmbushSpawner[] ambushSpawnerList)
		{
			ambushSpawnerList = new AmbushSpawner[2];
			ambushSpawnerList[0] = new AmbushSpawner(1);
			ambushSpawnerList[0].OnStart = Ambush1Start;
			ambushSpawnerList[0].OnCancel = Ambush1Cancel;
			ambushSpawnerList[0].OnFinish = Ambush1Finish;
			AmbushGroup ambushGroup = new AmbushGroup(6);
			ambushGroup.SpareLife = 0;
			ambushGroup.SpawnGap = 1f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.PodDropper);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(-64f, 24f, 0f);
			ambushGroup.Enemies[0].FaceDirection = 1;
			ambushGroup.Enemies[0].Life = 3;
			ambushGroup.Enemies[0].GroupSize = 3;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(-64f, -8f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.PodDropper);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(64f, 24f, 0f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Life = 3;
			ambushGroup.Enemies[1].GroupSize = 3;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(64f, -8f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.PodDropper);
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(-32f, 24f, 0f);
			ambushGroup.Enemies[2].FaceDirection = 1;
			ambushGroup.Enemies[2].Life = 3;
			ambushGroup.Enemies[2].GroupSize = 3;
			ambushGroup.Enemies[2].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(-32f, -8f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[3] = new AmbushEnemy(EnemyType.PodDropper);
			ambushGroup.Enemies[3].SpawnPosition = new Vector3(32f, 24f, 0f);
			ambushGroup.Enemies[3].FaceDirection = -1;
			ambushGroup.Enemies[3].Life = 3;
			ambushGroup.Enemies[3].GroupSize = 3;
			ambushGroup.Enemies[3].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(32f, -8f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[4] = new AmbushEnemy(EnemyType.Slither);
			ambushGroup.Enemies[4].SpawnPosition = new Vector3(-64f, 24f, 0f);
			ambushGroup.Enemies[4].FaceDirection = 1;
			ambushGroup.Enemies[4].Life = 3;
			ambushGroup.Enemies[4].GroupSize = 3;
			ambushGroup.Enemies[4].Actions = new EnemyAction[3]
			{
				new MoveToAction(0.6f, new Vector3(-64f, -8f, 0f), new Vector3(-0.8f, -0.8f, 0f)),
				new WalkAction(2f, 1),
				new TurnToAction(1)
			};
			ambushGroup.Enemies[5] = new AmbushEnemy(EnemyType.Slither);
			ambushGroup.Enemies[5].SpawnPosition = new Vector3(64f, 24f, 0f);
			ambushGroup.Enemies[5].FaceDirection = -1;
			ambushGroup.Enemies[5].Life = 3;
			ambushGroup.Enemies[5].GroupSize = 3;
			ambushGroup.Enemies[5].Actions = new EnemyAction[3]
			{
				new MoveToAction(0.6f, new Vector3(64f, -8f, 0f), new Vector3(-0.8f, -0.8f, 0f)),
				new WalkAction(2f, -1),
				new TurnToAction(-1)
			};
			ambushSpawnerList[0].Groups[0] = ambushGroup;
			ambushSpawnerList[1] = new AmbushSpawner(1);
			ambushSpawnerList[1].OnStart = Ambush2Start;
			ambushSpawnerList[1].OnCancel = Ambush2Cancel;
			ambushSpawnerList[1].OnFinish = Ambush2Finish;
			ambushGroup = new AmbushGroup(7);
			ambushGroup.SpareLife = 7;
			ambushGroup.SpawnGap = 1.5f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(48f, 24f, 0f);
			ambushGroup.Enemies[0].FaceDirection = -1;
			ambushGroup.Enemies[0].Life = 1;
			ambushGroup.Enemies[0].GroupSize = 1;
			ambushGroup.Enemies[0].Shield = true;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.6f, new Vector3(48f, -36f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(32f, 24f, 0f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Life = 1;
			ambushGroup.Enemies[1].GroupSize = 1;
			ambushGroup.Enemies[1].Shield = true;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.6f, new Vector3(32f, -28f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(16f, 24f, 0f);
			ambushGroup.Enemies[2].FaceDirection = -1;
			ambushGroup.Enemies[2].Life = 1;
			ambushGroup.Enemies[2].GroupSize = 1;
			ambushGroup.Enemies[2].Shield = true;
			ambushGroup.Enemies[2].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.6f, new Vector3(16f, -20f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[3] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[3].SpawnPosition = new Vector3(0f, 24f, 0f);
			ambushGroup.Enemies[3].FaceDirection = -1;
			ambushGroup.Enemies[3].Life = 1;
			ambushGroup.Enemies[3].GroupSize = 3;
			ambushGroup.Enemies[3].Shield = true;
			ambushGroup.Enemies[3].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.6f, new Vector3(0f, -12f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[4] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[4].SpawnPosition = new Vector3(-16f, 24f, 0f);
			ambushGroup.Enemies[4].FaceDirection = 1;
			ambushGroup.Enemies[4].Life = 1;
			ambushGroup.Enemies[4].GroupSize = 1;
			ambushGroup.Enemies[4].Shield = true;
			ambushGroup.Enemies[4].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.6f, new Vector3(-16f, -20f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[5] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[5].SpawnPosition = new Vector3(-32f, 24f, 0f);
			ambushGroup.Enemies[5].FaceDirection = 1;
			ambushGroup.Enemies[5].Life = 1;
			ambushGroup.Enemies[5].GroupSize = 1;
			ambushGroup.Enemies[5].Shield = true;
			ambushGroup.Enemies[5].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.6f, new Vector3(-32f, -28f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[6] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[6].SpawnPosition = new Vector3(-48f, 24f, 0f);
			ambushGroup.Enemies[6].FaceDirection = 1;
			ambushGroup.Enemies[6].Life = 1;
			ambushGroup.Enemies[6].GroupSize = 1;
			ambushGroup.Enemies[6].Shield = true;
			ambushGroup.Enemies[6].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.6f, new Vector3(-48f, -36f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushSpawnerList[1].Groups[0] = ambushGroup;
		}

		private static void Ambush1Start()
		{
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Both);
		}

		private static void Ambush1Cancel()
		{
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Right);
		}

		private static void Ambush1Finish()
		{
			mReward[1] = 20;
			mReward[5] = 10;
			mReward[10] = 10;
			MaterialDropper.Instance.Start(2846f, 2866f, 504f, 504f, 0.1f, mReward, Vector3.Zero);
			World.ChangeRoom(11);
			World.Camera.EnterRoom(World.GetRoom(11), true, false, 0.5f);
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Right);
		}

		private static void Ambush2Start()
		{
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Both);
		}

		private static void Ambush2Cancel()
		{
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Right);
		}

		private static void Ambush2Finish()
		{
			mReward[1] = 20;
			mReward[5] = 10;
			mReward[10] = 10;
			MaterialDropper.Instance.Start(4126f, 4146f, 504f, 504f, 0.1f, mReward, Vector3.Zero);
			World.ChangeRoom(14);
			World.Camera.EnterRoom(World.GetRoom(14), true, false, 0.5f);
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Right);
		}

		public static void TramOpenScene1()
		{
			World.Camera.Position = new Vector3(304f, 176f, 116f);
			World.Play(0, true);
			Player.Instance.Spawn(new Vector3(304f, 240f, 0f));
			Player.Instance.ControlEnabled = false;
			Player.Instance.StateMachineEnabled = true;
			Director.WaitForSeconds(1.5f, TramOpenScene2, false);
		}

		private static void TramOpenScene2()
		{
			SFXManager.PlaySound("noise");
			if (Player.Instance.Type == PlayerType.Mars)
			{
				World.ShowDialogByID(0, TramOpenScene3);
			}
			else
			{
				World.ShowDialogByID(0, TramOpenScene2B);
			}
		}

		private static void TramOpenScene2B()
		{
			Player.Instance.SpriteRig.Transition("bleeding_crouch", 0.3f);
			Director.WaitForSeconds(0.5f, TramOpenScene2C, false);
		}

		private static void TramOpenScene2C()
		{
			World.ShowDialogByID(1, TramOpenScene3);
		}

		private static void TramOpenScene3()
		{
			if (Player.Instance.Type == PlayerType.Tarus)
			{
				((Tarus)Player.Instance).StateMachine.ChangeState(TarusCrouch.Instance);
			}
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
			Player.Instance.ControlEnabled = true;
			World.SaveCheckPoint(new Vector3(304f, 144f, 0f), 0, false, false, false, false, false, false);
			DataCube.Acquire(3);
		}

		public static void TramStart1()
		{
			World.Camera.Mode = CameraMode.Cinematic;
			Player.Instance.ControlEnabled = false;
			Player.Instance.ChangeToUnStuckStage();
			if (mTramHasStarted)
			{
				Director.WaitForSeconds(0.1f, TramStart3_2, false);
			}
			else
			{
				Director.WaitForSeconds(0.5f, TramStart2, false);
			}
		}

		private static void TramStart2()
		{
			World.Camera.Shake(1, 2f);
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Right);
			Director.WaitForSeconds(1f, TramStart3_1, false);
			SFXManager.PlaySound("New_TrainStart");
		}

		private static void TramStart3_1()
		{
			mTramHasStarted = true;
			mLoopedRails.XAcceleration = -20f;
			mLoopedRails.MinXVelocity = -180f;
			mLoopedScenary.XAcceleration = -20f;
			mLoopedScenary.MinXVelocity = -180f;
			Director.WaitForSeconds(0.1f, TramStart3_2, false);
		}

		private static void TramStart3_2()
		{
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Right);
			Player.Instance.ControlEnabled = true;
		}

		public static void TramMiniBoss()
		{
			Player.Instance.ControlEnabled = false;
			Player.Instance.ChangeToUnStuckStage();
			Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.Oculus, new Vector3(5416f, 444f, 0f), 0);
			Director.WaitForSeconds(0.5f, MiniBoss2, false);
			BGMManager.FadeOff();
		}

		private static void MiniBoss2()
		{
			World.ChangeRoom(17);
			World.Camera.EnterRoom(World.GetRoom(17), true, false, 1.5f);
			Director.WaitForSeconds(1.7f, MiniBoss3, false);
		}

		private static void MiniBoss3()
		{
			World.Camera.SideScrollingDirection = CameraSideScrollingDirection.Both;
		}

		public static void AcquirePlasmaShockAndFinishOculus()
		{
			BGMManager.FadeOff();
			Player.Instance.ControlEnabled = false;
			Player.Instance.ForceIdle();
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1f, AcquirePlasmaShock2, false);
		}

		private static void AcquirePlasmaShock2()
		{
			if (!ProfileManager.Current.AcquireShockUpgradeFromOculus)
			{
				Vector3 position = Player.Instance.Position;
				position.Y += 26f;
				mShockUpgradeChip.Spawn(position);
				mShockUpgradeChip.Velocity.Y = -4f;
				SFXManager.PlayLoopSound("charge", 4f, 0.75f);
				TimedEmitParticlePool.Summon("chip_unlock", mShockUpgradeChip, 3f);
				ProfileManager.Current.AcquireShockUpgradeFromOculus = true;
				Director.WaitForSeconds(5f, AcquirePlasmaShock3, false);
			}
			else
			{
				OculusDefeatDialog();
			}
		}

		private static void AcquirePlasmaShock3()
		{
			ProfileManager.BeginSave(null, 1, false);
			Director.WaitForSeconds(3f, OculusDefeatDialog, false);
		}

		private static void OculusDefeatDialog()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				World.ShowDialogByID(2, PlayBGM);
			}
			else
			{
				PlayBGM();
			}
		}

		private static void PlayBGM()
		{
			Player.Instance.ControlEnabled = true;
			BGMManager.Play(0);
		}

		public static void TramTunnelStart()
		{
			World.Camera.ForceSideScrollingDirection(CameraSideScrollingDirection.Right);
			UseRailsWithTunnel();
		}

		public static void TramTunnelEnd()
		{
			UseRailsOnly();
		}

		public static void TramBoss()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.BadTarus, new Vector3(9880f, 472f, 0f), 1);
			}
			else
			{
				Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.BadMars, new Vector3(9880f, 472f, 0f), -1);
			}
			Player.Instance.ControlEnabled = false;
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(0.5f, TramBoss2, false);
			BGMManager.FadeOff();
		}

		private static void TramBoss2()
		{
			if (!Player.Instance.IsOnGround)
			{
				Director.WaitForSeconds(0.5f, TramBoss2, false);
			}
			else
			{
				Player.Instance.StateMachineEnabled = false;
				Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
				World.Camera.TargetingTransform(new Vector3(9824f, 464f, 140f), 2.5f, true);
				PlayerTweener playerTweener = Director.PlayerTweener;
				Player instance = Player.Instance;
				int[] propertyIndexes = new int[1];
				playerTweener.Tween(instance, propertyIndexes, new object[1]
				{
					new Vector3(9768f, 464f, 0f)
				}, 0f, 2.4f, TramBoss3);
				SFXManager.PlayLoopSound("run", 2.8f, 0.4f);
			}
		}

		private static void TramBoss3()
		{
			Apollo.Instance.Dismiss();
			World.Camera.SideScrollingDirection = CameraSideScrollingDirection.Both;
			if (Player.Instance.Type == PlayerType.Mars)
			{
				Player.Instance.SpriteRig.SetPose("stand", 0.0);
			}
			else
			{
				Player.Instance.SpriteRig.SetPose("stand_idle", 0.0);
			}
			Director.WaitForSeconds(0.5f, TramBoss4, false);
		}

		private static void TramBoss4()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				BGMManager.Play(1);
			}
			World.ShowDialogByID(3, TramBoss5);
		}

		private static void TramBoss5()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				Player.Instance.SpriteRig.SetPose("stand_fire", 0.0);
				Player.Instance.SpriteRig.Cycle = false;
				Player.Instance.FireWeapon(0.1f);
				Director.WaitForSeconds(1f, TramBoss6, false);
			}
			else
			{
				((BadMars)Director.ControlledEnemy).OpenFire();
				Player.Instance.Invincible = true;
				Director.WaitForSeconds(1f, TramBoss5B, false);
			}
		}

		private static void TramBoss5B()
		{
			World.ShowDialogByID(4, TramBoss6);
		}

		private static void TramBoss6()
		{
			if (Player.Instance.Type == PlayerType.Tarus)
			{
				((BadMars)Director.ControlledEnemy).EmitGas();
				SFXManager.PlaySound("gas");
			}
			Director.ControlledEnemy.SetFaceDirection(-1);
			Director.WaitForSeconds(0.5f, TramBoss7, false);
		}

		private static void TramBoss7()
		{
			World.ShowDialogByID(5, TramBoss8);
			if (Player.Instance.Type == PlayerType.Mars)
			{
				World.Camera.Shake(1, 1f);
				BGMManager.FadeOff();
			}
			if (Player.Instance.Type == PlayerType.Tarus)
			{
				BGMManager.Play(1);
			}
		}

		private static void TramBoss8()
		{
			if (Player.Instance.Type == PlayerType.Tarus)
			{
				BGMManager.FadeOff();
			}
			Player.Instance.Invincible = false;
			Director.ControlledEnemy.SpriteRig.SetPose("run_idle", 0.0);
			Director.ControlledEnemy.IsAIEnabled = true;
			Director.ControlledEnemy = null;
		}

		private static void AcquireSolarStrikeChip()
		{
			BGMManager.FadeOff();
			Player.Instance.ControlEnabled = false;
			Player.Instance.ForceIdle();
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1.5f, AcquireSolarStrikeChip2, false);
		}

		private static void AcquireSolarStrikeChip2()
		{
			Vector3 position = Player.Instance.Position;
			position.Y += 26f;
			mSpecialUpgradeChip.Spawn(position);
			mSpecialUpgradeChip.Velocity.Y = -4f;
			SFXManager.PlayLoopSound("charge", 4f, 0.75f);
			TimedEmitParticlePool.Summon("chip_unlock", mSpecialUpgradeChip, 3f);
			Director.WaitForSeconds(5.5f, AcquireSolarStrikeChip3, false);
		}

		private static void AcquireSolarStrikeChip3()
		{
			ProfileManager.Current.AcquireSpecialUpgradeFromBad = true;
			ProfileManager.BeginSave(null, 1, false);
			Director.WaitForSeconds(3f, BossDefeatDialog, false);
		}

		private static void BossDefeatDialog()
		{
			World.ShowDialogByID(6, TramBoss9);
		}

		public static void BadTarusDefeat()
		{
			DataCube.Acquire(1);
			AchievementManager.Instance.Notify(10, 1);
			if (World.HitsByBoss == 0)
			{
				AchievementManager.Instance.Notify(19, 1);
			}
			if (World.HitsByBoss == 0 && World.LowestBossCombatDifficulty == GameCombatDifficulty.Hardcore)
			{
				AchievementManager.Instance.MarkHasPerfectHardBossFight(4);
			}
			if (ProfileManager.Current.AcquireSpecialUpgradeFromBad)
			{
				Director.WaitForSeconds(0.2f, BossDefeatDialog, false);
			}
			else
			{
				Director.WaitForSeconds(0.2f, AcquireSolarStrikeChip, false);
			}
		}

		public static void BadMarsDefeat()
		{
			DataCube.Acquire(0);
			AchievementManager.Instance.Notify(13, 1);
			if (World.HitsByBoss == 0)
			{
				AchievementManager.Instance.Notify(18, 1);
			}
			if (World.HitsByBoss == 0 && World.LowestBossCombatDifficulty == GameCombatDifficulty.Hardcore)
			{
				AchievementManager.Instance.MarkHasPerfectHardBossFight(5);
			}
			if (ProfileManager.Current.AcquireSpecialUpgradeFromBad)
			{
				Director.WaitForSeconds(0.2f, BossDefeatDialog, false);
			}
			else
			{
				Director.WaitForSeconds(0.2f, AcquireSolarStrikeChip, false);
			}
		}

		private static void TramBoss9()
		{
			PlayScreen.Instance.CompleteLevel(false);
		}

		public static void Destroy()
		{
			((MarsValkyl)MarsValkyl.Instance).BadTarus = null;
			World.Camera.SideScrollingDirection = CameraSideScrollingDirection.Both;
			ProcessManager.RemoveProcess(mLoopedRails);
			ProcessManager.RemoveProcess(mLoopedScenary);
			mLoopedRails = null;
			mLoopedScenary = null;
			ScrollPlatform = null;
			ScrollBox = null;
			OculusCage = null;
			mShockUpgradeChip = null;
			mSpecialUpgradeChip = null;
			mReward = null;
		}
	}
}
