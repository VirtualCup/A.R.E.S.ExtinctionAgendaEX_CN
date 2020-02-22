using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Entity.Action;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.UI;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace ARES360
{
	public static class LV2Loader
	{
		private static Sprite[] mPods;

		private static int mPodIndex;

		private static OneWayPlatform mOpenScenePlatform;

		private static GasTank mTutorGas;

		private static BlastDoor ambush1Door;

		private static BlastDoor ambush2Door;

		private static BlastDoor ambush3Door;

		private static MaterialBoxExtra ambush2Reward;

		private static MaterialBoxExtra ambush3Reward;

		private static BlastDoor miniBossDoor;

		private static BlastDoor bossDoor;

		private static Dictionary<int, int> mReward;

		private static Sprite mLaserSMG;

		private static float mLaserSMGTutorTime;

		private static UpgradeChip mBoostChip;

		public static ExplodeableLevel[] BossStageBlocks;

		public static Room mBossRoom;

		private static Sprite _laserSMG;

		public static void LoadEnemy()
		{
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.FlyingPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Turret);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronCrawler);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronWalker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronGunner);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Carrion);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShocker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.FlyingPod, 6);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Turret, 9);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronCrawler, 6);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronWalker, 2);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronGunner, 1);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Carrion, 1);
		}

		public static void Destroy()
		{
			mReward = null;
			ambush2Reward = null;
			ambush3Reward = null;
			mPods = null;
			mPodIndex = 0;
			mBoostChip = null;
			ambush1Door = null;
			ambush2Door = null;
			ambush3Door = null;
			mLaserSMG = null;
			mOpenScenePlatform = null;
			mTutorGas = null;
			miniBossDoor = null;
			bossDoor = null;
			BossStageBlocks = null;
			mBossRoom = null;
			_laserSMG = null;
		}

		public static void LoadObject(out List<GameObject>[] objectInRoom)
		{
			mReward = new Dictionary<int, int>(3);
			ambush2Reward = new MaterialBoxExtra(new Vector3(1280f, 204f, -40f), 3, false);
			ambush3Reward = new MaterialBoxExtra(new Vector3(2448f, 92f, -40f), 3, false);
			mPods = new Sprite[16];
			for (int i = 0; i < 16; i++)
			{
				mPods[i] = new Sprite();
				mPods[i].Texture = PodShocker.Prototype.BodySprites[0].Texture;
				mPods[i].LeftTextureCoordinate = 0.4978903f;
				mPods[i].RightTextureCoordinate = 0.98593533f;
				mPods[i].TopTextureCoordinate = 0.00281292549f;
				mPods[i].BottomTextureCoordinate = 0.609001338f;
				mPods[i].ScaleX = 3.47f;
				mPods[i].ScaleY = 4.31f;
			}
			mPodIndex = 0;
			mBoostChip = new UpgradeChip(9, Vector3.Zero, true);
			BossStageBlocks = new ExplodeableLevel[11];
			objectInRoom = new List<GameObject>[34];
			objectInRoom[0] = new List<GameObject>(1);
			mOpenScenePlatform = new OneWayPlatform(new Vector2(16f, 4f), new Vector3(40f, 772f, 0f), new Vector3(40f, 628f, 0f), 16f, false, "Content/World/m_lv2_elevator_4", new Vector3(0f, 4f, -12f));
			objectInRoom[0].Add(mOpenScenePlatform);
			objectInRoom[1] = new List<GameObject>(2);
			objectInRoom[1].Add(new CrackedBox(new Vector3(232f, 632f, 0f), 2, false));
			mTutorGas = new GasTank(3, new Vector3(148f, 644f, 0f));
			objectInRoom[1].Add(mTutorGas);
			objectInRoom[2] = new List<GameObject>();
			objectInRoom[2].Add(new CrackedBox(new Vector3(344f, 632f, 0f), 2, false));
			objectInRoom[2].Add(new CrackedBox(new Vector3(360f, 632f, 0f), 2, false));
			objectInRoom[2].Add(new CrackedBox(new Vector3(380f, 636f, 0f), 3, false));
			objectInRoom[2].Add(new BlastDoor(new Vector3(284f, 644f, 0f), true, 1));
			ambush1Door = new BlastDoor(new Vector3(404f, 644f, 0f), true, 1);
			objectInRoom[2].Add(ambush1Door);
			objectInRoom[3] = new List<GameObject>();
			objectInRoom[3].Add(new CrackedBox(new Vector3(424f, 672f, 0f), 2, false));
			objectInRoom[3].Add(new CrackedBox(new Vector3(528f, 640f, 0f), 2, false));
			objectInRoom[3].Add(new CrackedBox(new Vector3(576f, 672f, 0f), 2, false));
			objectInRoom[3].Add(new CrackedBox(new Vector3(592f, 640f, 0f), 2, false));
			objectInRoom[3].Add(new BlastDoor(new Vector3(716f, 644f, 0f), true, 1));
			objectInRoom[4] = new List<GameObject>(4);
			objectInRoom[4].Add(new CrackedBox(new Vector3(892f, 596f, 0f), 3, false));
			objectInRoom[4].Add(new BlastDoor(new Vector3(1028f, 660f, 0f), false, 0));
			objectInRoom[4].Add(new BlastDoor(new Vector3(1028f, 572f, 0f), false, 0));
			ModelObject modelObject = new ModelObject(new Vector3(876f, 629f, -132f), Vector3.Zero, 1f, 1f, false, "Content/World/m_generator", Vector3.Zero, new Vector3(1.6f, 1.6f, 1.6f));
			modelObject.AddColliderWhenRegister = false;
			modelObject.Model.RelativeRotationYVelocity = 0.2f;
			objectInRoom[4].Add(modelObject);
			objectInRoom[5] = new List<GameObject>(1);
			objectInRoom[5].Add(new Fan(new Vector3(716f, 572f, 0f)));
			objectInRoom[6] = new List<GameObject>();
			objectInRoom[7] = new List<GameObject>();
			objectInRoom[8] = new List<GameObject>();
			objectInRoom[8].Add(new CrackedBox(new Vector3(900f, 204f, 0f), 3, false));
			BackgroundConveyer backgroundConveyer = new BackgroundConveyer(28, 3f, 2f, new Vector3(1030f, 186f, -66f), new Vector3(1028f, 194f, -66f), 220f, false, false, true, null, PodBodyDisappear);
			objectInRoom[8].Add(backgroundConveyer);
			objectInRoom[8].Add(new Crane(new Vector3(1064f, 234f, -120f), new Vector3(1064f, 234f, -66f), 32f, 0.5f, DrawPodBody, DropPodBody, StartDrawPodBody, StartDropPodBody, backgroundConveyer));
			objectInRoom[9] = new List<GameObject>();
			CrackedBox crackedBox = new CrackedBox(new Vector3(766f, 250f, 0f), 2, false);
			crackedBox.HiddenGameObject = World.Chip[1];
			crackedBox.ObjectToApplyGravityWhenDestroyed = World.Chip[1];
			objectInRoom[9].Add(crackedBox);
			objectInRoom[9].Add(new Conveyor(10, new Vector3(792f, 228f, 0f), 30f, 1));
			objectInRoom[10] = new List<GameObject>();
			objectInRoom[10].Add(new BlastDoor(new Vector3(1220f, 204f, 0f), true, 1));
			ambush2Door = new BlastDoor(new Vector3(1340f, 204f, 0f), true, 1);
			objectInRoom[10].Add(ambush2Door);
			objectInRoom[10].Add(new BackgroundConveyer(4, 3f, 2f, new Vector3(1280f, 186f, -34f), Vector3.Zero, 0f, false, true, true, null, null));
			objectInRoom[11] = new List<GameObject>();
			objectInRoom[11].Add(new BlastDoor(new Vector3(1588f, 204f, 0f), true, 1));
			objectInRoom[12] = new List<GameObject>();
			objectInRoom[12].Add(new BlastDoor(new Vector3(1644f, 380f, 0f), true, 1));
			objectInRoom[12].Add(new OneWayPlatform(new Vector2(16f, 4f), new Vector3(1616f, 188f, 0f), new Vector3(1616f, 364f, 0f), 20f, false, "Content/World/m_lv2_elevator_4", new Vector3(0f, 4f, -12f)));
			objectInRoom[13] = new List<GameObject>();
			objectInRoom[14] = new List<GameObject>();
			objectInRoom[14].Add(new BlastDoor(new Vector3(1924f, 380f, 0f), true, 1));
			miniBossDoor = new BlastDoor(new Vector3(2076f, 380f, 0f), true, 1);
			objectInRoom[14].Add(miniBossDoor);
			objectInRoom[15] = new List<GameObject>();
			objectInRoom[15].Add(new ExplosiveBox(new Vector3(2180f, 380f, 0f), 3, false));
			objectInRoom[15].Add(new ExplosiveBox(new Vector3(2228f, 404f, 0f), 3, false));
			objectInRoom[15].Add(new ExplosiveBox(new Vector3(2276f, 428f, 0f), 3, false));
			objectInRoom[15].Add(new BlastDoor(new Vector3(2388f, 380f, 0f), true, 1));
			objectInRoom[16] = new List<GameObject>();
			objectInRoom[16].Add(new BlastDoor(new Vector3(2388f, 204f, 0f), true, -1));
			objectInRoom[16].Add(new OneWayPlatform(new Vector2(16f, 4f), new Vector3(2416f, 364f, 0f), new Vector3(2416f, 188f, 0f), 20f, false, "Content/World/m_lv2_elevator_4", new Vector3(0f, 4f, -12f)));
			objectInRoom[17] = new List<GameObject>();
			objectInRoom[17].Add(new CrackedBox(new Vector3(2266f, 196f, 0f), 3, false));
			objectInRoom[17].Add(new CrackedBox(new Vector3(2242f, 196f, 0f), 3, false));
			objectInRoom[17].Add(new ExplosiveBox(new Vector3(2242f, 220f, 0f), 3, true));
			objectInRoom[17].Add(new ExplosiveBox(new Vector3(2080f, 196f, 0f), 3, false));
			objectInRoom[17].Add(new ExplosiveBox(new Vector3(2056f, 196f, 0f), 3, false));
			objectInRoom[17].Add(new ExplosiveBox(new Vector3(2048f, 220f, 0f), 3, false));
			objectInRoom[17].Add(new ExplosiveBox(new Vector3(2048f, 244f, 0f), 3, false));
			objectInRoom[17].Add(new ModelObject(new Vector3(2072f, 220f, 0f), Vector3.Zero, 12f, 12f, true, "Content/World/m_sm_lv2_box", new Vector3(0f, -12f, -2f), new Vector3(1.5f, 1.5f, 1.5f)));
			objectInRoom[17].Add(new CrushChecker(new Vector3(2334f, 176f, 0f), 12f, 8f, false, false));
			objectInRoom[17].Add(new CrushChecker(new Vector3(2242f, 176f, 0f), 12f, 8f, false, false));
			objectInRoom[17].Add(new CrushChecker(new Vector3(1968f, 192f, 0f), 8f, 8f, false, false));
			objectInRoom[17].Add(new ExplosiveBoxDropper(new Vector3(2160f, 224f, 0f), 272f, 40f, new Vector3(2334f, 268f, 0f), 2f));
			objectInRoom[17].Add(new ExplosiveBoxDropper(new Vector3(2160f, 224f, 0f), 272f, 40f, new Vector3(2188f, 268f, 0f), 1.5f));
			objectInRoom[17].Add(new ExplosiveBoxDropper(new Vector3(2160f, 224f, 0f), 272f, 40f, new Vector3(1976f, 268f, 0f), 1.5f));
			objectInRoom[17].Add(new Conveyor(6, new Vector3(2204f, 188f, 0f), 30f, 1));
			objectInRoom[17].Add(new CharacterBlocker(12f, 8f, new Vector3(2334f, 264f, 0f)));
			objectInRoom[17].Add(new CharacterBlocker(12f, 8f, new Vector3(2188f, 264f, 0f)));
			objectInRoom[17].Add(new CharacterBlocker(12f, 8f, new Vector3(1976f, 264f, 0f)));
			objectInRoom[18] = new List<GameObject>();
			objectInRoom[18].Add(new CrackedBox(new Vector3(1800f, 148f, 0f), 3, false));
			objectInRoom[18].Add(new BlastDoor(new Vector3(1884f, 204f, 0f), true, -1));
			objectInRoom[18].Add(new Conveyor(4, new Vector3(1744f, 212f, 0f), 30f, -1));
			objectInRoom[18].Add(new Conveyor(5, new Vector3(1820f, 220f, 0f), 30f, 1));
			objectInRoom[19] = new List<GameObject>();
			objectInRoom[19].Add(new Conveyor(8, new Vector3(2008f, 92f, 0f), 30f, -1));
			objectInRoom[19].Add(new Conveyor(8, new Vector3(2104f, 92f, 0f), 30f, 1));
			objectInRoom[19].Add(new Conveyor(12, new Vector3(2200f, 116f, 0f), 30f, 1));
			objectInRoom[19].Add(new Conveyor(8, new Vector3(2280f, 84f, 0f), 30f, -1));
			objectInRoom[19].Add(new StaticAttack(new Vector3(2152f, 48f, 0f), 192f, 8f, 200, true, true, false));
			backgroundConveyer = new BackgroundConveyer(14, 3f, 2f, new Vector3(2308f, 72f, -66f), new Vector3(2320f, 80f, -66f), 100f, false, false, true, null, PodBodyDisappear);
			objectInRoom[19].Add(backgroundConveyer);
			objectInRoom[19].Add(new Crane(new Vector3(2320f, 120f, -120f), new Vector3(2320f, 120f, -66f), 32f, 0.5f, DrawPodBody, DropPodBody, StartDrawPodBody, StartDropPodBody, backgroundConveyer));
			objectInRoom[20] = new List<GameObject>();
			objectInRoom[20].Add(new Conveyor(4, new Vector3(2416f, 68f, 0f), 30f, 1));
			objectInRoom[20].Add(new Conveyor(4, new Vector3(2480f, 68f, 0f), 30f, -1));
			objectInRoom[20].Add(new BlastDoor(new Vector3(2388f, 92f, 0f), true, 1));
			ambush3Door = new BlastDoor(new Vector3(2508f, 92f, 0f), true, 1);
			objectInRoom[20].Add(ambush3Door);
			objectInRoom[20].Add(new BackgroundConveyer(4, 3f, 2f, new Vector3(2448f, 72f, -34f), Vector3.Zero, 0f, false, true, true, null, null));
			objectInRoom[21] = new List<GameObject>();
			objectInRoom[21].Add(new Conveyor(8, new Vector3(2624f, 92f, 0f), 30f, 1));
			objectInRoom[21].Add(new Conveyor(8, new Vector3(2552f, 124f, 0f), 30f, 1));
			objectInRoom[21].Add(new Conveyor(12, new Vector3(2600f, 284f, 0f), 30f, 1));
			objectInRoom[21].Add(new Conveyor(12, new Vector3(2600f, 396f, 0f), 30f, -1));
			objectInRoom[21].Add(new Conveyor(4, new Vector3(2636f, 220f, 0f), 30f, -1));
			objectInRoom[21].Add(new Conveyor(4, new Vector3(2672f, 252f, 0f), 30f, -1));
			objectInRoom[21].Add(new Conveyor(8, new Vector3(2584f, 428f, 0f), 30f, 1));
			objectInRoom[21].Add(new Conveyor(4, new Vector3(2536f, 364f, 0f), 30f, 1));
			objectInRoom[21].Add(new Conveyor(17, new Vector3(2668f, 180f, 0f), 30f, 1));
			objectInRoom[21].Add(new CrushChecker(new Vector3(2700f, 192f, 0f), 4f, 16f));
			objectInRoom[21].Add(new Conveyor(2, new Vector3(2512f, 148f, 0f), 30f, 1));
			objectInRoom[21].Add(new CrackedBoxDropper(new Vector3(2616f, 288f, 0f), 200f, 300f, new Vector3(2604f, 516f, 0f), 5f));
			objectInRoom[21].Add(new CrackedBoxDropper(new Vector3(2508f, 164f, 0f), 300f, 300f, new Vector3(2508f, 164f, 0f), 5f));
			objectInRoom[21].Add(new CharacterBlocker(4f, 12f, new Vector3(2516f, 164f, 0f)));
			objectInRoom[21].Add(new CharacterBlocker(4f, 12f, new Vector3(2700f, 196f, 0f)));
			objectInRoom[21].Add(new BlastDoor(new Vector3(2508f, 492f, 0f), true, -1));
			objectInRoom[22] = new List<GameObject>();
			objectInRoom[23] = new List<GameObject>();
			modelObject = new ModelObject(new Vector3(2440f, 504f, 6f), Vector3.Zero, 8f, 8f, false, "Content/World/m_SM_LV2_box", new Vector3(0f, -8f, 0f), Vector3.One);
			objectInRoom[23].Add(modelObject);
			objectInRoom[24] = new List<GameObject>();
			objectInRoom[24].Add(new BlastDoor(new Vector3(2764f, 556f, 0f), true, 1));
			modelObject = new ModelObject(new Vector3(2836f, 524f, 0f), Vector3.Zero, 12f, 12f, false, "Content/World/m_SM_LV2_box", new Vector3(0f, -12f, 0f), new Vector3(1.5f, 1.5f, 1.5f));
			objectInRoom[24].Add(modelObject);
			modelObject = new ModelObject(new Vector3(2860f, 524f, 0f), Vector3.Zero, 12f, 12f, false, "Content/World/m_SM_LV2_box", new Vector3(0f, -12f, 0f), new Vector3(1.5f, 1.5f, 1.5f));
			objectInRoom[24].Add(modelObject);
			objectInRoom[25] = new List<GameObject>();
			objectInRoom[26] = new List<GameObject>();
			objectInRoom[26].Add(new BlastDoor(new Vector3(2884f, 444f, 0f), false, 0));
			objectInRoom[26].Add(new MaterialBox(new Vector3(2811f, 472f, 0f), 2, false));
			CrackedBox crackedBox2 = new CrackedBox(new Vector3(2837f, 472f, 0f), 2, false);
			crackedBox2.ObjectToApplyGravityWhenDestroyed = World.DataCube[2];
			crackedBox2.HiddenGameObject = World.DataCube[2];
			objectInRoom[26].Add(crackedBox2);
			objectInRoom[27] = new List<GameObject>();
			objectInRoom[27].Add(new BlastDoor(new Vector3(3172f, 412f, 0f), true, 1));
			bossDoor = new BlastDoor(new Vector3(3776f, 412f, 0f), true, 1);
			objectInRoom[27].Add(bossDoor);
			BossStageBlocks[0] = new ExplodeableLevel(new Vector3(3216f, 364f, 0f), 32f, 36f, false, "Content/World/m_sm_platform_8x3", new Vector3(0f, 24f, -2f), Vector3.One);
			BossStageBlocks[1] = new ExplodeableLevel(new Vector3(3268f, 388f, 0f), 20f, 12f, false, "Content/World/m_sm_platform_5x3", new Vector3(0f, 0f, -2f), Vector3.One);
			BossStageBlocks[2] = new ExplodeableLevel(new Vector3(3320f, 364f, 0f), 32f, 36f, false, "Content/World/m_sm_platform_8x3", new Vector3(0f, 24f, -2f), Vector3.One);
			BossStageBlocks[3] = new ExplodeableLevel(new Vector3(3372f, 388f, 0f), 20f, 12f, false, "Content/World/m_sm_platform_5x3", new Vector3(0f, 0f, -2f), Vector3.One);
			BossStageBlocks[4] = new ExplodeableLevel(new Vector3(3424f, 364f, 0f), 32f, 36f, false, "Content/World/m_sm_platform_8x3", new Vector3(0f, 24f, -2f), Vector3.One);
			BossStageBlocks[5] = new ExplodeableLevel(new Vector3(3476f, 388f, 0f), 20f, 12f, false, "Content/World/m_sm_platform_5x3", new Vector3(0f, 0f, -2f), Vector3.One);
			BossStageBlocks[6] = new ExplodeableLevel(new Vector3(3528f, 364f, 0f), 32f, 36f, false, "Content/World/m_sm_platform_8x3", new Vector3(0f, 24f, -2f), Vector3.One);
			BossStageBlocks[7] = new ExplodeableLevel(new Vector3(3580f, 388f, 0f), 20f, 12f, false, "Content/World/m_sm_platform_5x3", new Vector3(0f, 0f, -2f), Vector3.One);
			BossStageBlocks[8] = new ExplodeableLevel(new Vector3(3632f, 364f, 0f), 32f, 36f, false, "Content/World/m_sm_platform_8x3", new Vector3(0f, 24f, -2f), Vector3.One);
			BossStageBlocks[9] = new ExplodeableLevel(new Vector3(3684f, 388f, 0f), 20f, 12f, false, "Content/World/m_sm_platform_5x3", new Vector3(0f, 0f, -2f), Vector3.One);
			BossStageBlocks[10] = new ExplodeableLevel(new Vector3(3736f, 364f, 0f), 32f, 36f, false, "Content/World/m_sm_platform_8x3", new Vector3(0f, 24f, -2f), Vector3.One);
			objectInRoom[27].Add(BossStageBlocks[0]);
			objectInRoom[27].Add(BossStageBlocks[1]);
			objectInRoom[27].Add(BossStageBlocks[2]);
			objectInRoom[27].Add(BossStageBlocks[3]);
			objectInRoom[27].Add(BossStageBlocks[4]);
			objectInRoom[27].Add(BossStageBlocks[5]);
			objectInRoom[27].Add(BossStageBlocks[6]);
			objectInRoom[27].Add(BossStageBlocks[7]);
			objectInRoom[27].Add(BossStageBlocks[8]);
			objectInRoom[27].Add(BossStageBlocks[9]);
			objectInRoom[27].Add(BossStageBlocks[10]);
			modelObject = new ModelObject(new Vector3(3338f, 504f, -248f), new Vector3(3.14159274f, 0f, 0f), 1f, 1f, false, "Content/World/m_generator", Vector3.Zero, new Vector3(2f, 2f, 2f));
			modelObject.AddColliderWhenRegister = false;
			modelObject.Model.RelativeRotationYVelocity = 0.2f;
			objectInRoom[27].Add(modelObject);
			modelObject = new ModelObject(new Vector3(3680f, 504f, -248f), new Vector3(3.14159274f, 0f, 0f), 1f, 1f, false, "Content/World/m_generator", Vector3.Zero, new Vector3(2f, 2f, 2f));
			modelObject.AddColliderWhenRegister = false;
			modelObject.Model.RelativeRotationYVelocity = 0.2f;
			objectInRoom[27].Add(modelObject);
			modelObject = new ModelObject(new Vector3(3512f, 516f, -284f), new Vector3(3.14159274f, 0f, 0f), 1f, 1f, false, "Content/World/m_generator", Vector3.Zero, new Vector3(3f, 3f, 3f));
			modelObject.AddColliderWhenRegister = false;
			modelObject.Model.RelativeRotationYVelocity = -0.2f;
			objectInRoom[27].Add(modelObject);
			objectInRoom[28] = new List<GameObject>(1);
			objectInRoom[28].Add(new Fan(new Vector3(1220f, 300f, 0f)));
			objectInRoom[29] = new List<GameObject>(5);
			objectInRoom[29].Add(new Conveyor(4, new Vector3(2752f, 68f, 0f), 30f, 1));
			objectInRoom[29].Add(new Conveyor(4, new Vector3(2728f, 100f, 0f), 30f, 1));
			objectInRoom[29].Add(new Conveyor(3, new Vector3(2780f, 116f, 0f), 30f, -1));
			objectInRoom[29].Add(new Conveyor(5, new Vector3(2764f, 148f, 0f), 30f, -1));
			objectInRoom[29].Add(new GasTank(4, new Vector3(2704f, 88f, 0f)));
			objectInRoom[30] = new List<GameObject>(1);
			objectInRoom[30].Add(new GasTank(4, new Vector3(1696f, 152f, 0f)));
			objectInRoom[31] = new List<GameObject>(1);
			objectInRoom[31].Add(new BlastDoor(new Vector3(1884f, 92f, 0f), true, 1));
			objectInRoom[32] = new List<GameObject>(5);
			objectInRoom[32].Add(new ExplosiveBoxDropper(new Vector3(3028f, 560f, 0f), 132f, 48f, new Vector3(2916f, 604f, 0f), 2f));
			objectInRoom[32].Add(new ExplosiveBoxDropper(new Vector3(3028f, 560f, 0f), 132f, 48f, new Vector3(3140f, 604f, 0f), 2f));
			objectInRoom[32].Add(new ExplosiveBox(new Vector3(3028f, 556f, 0f), 3, false));
			objectInRoom[32].Add(new ExplosiveBox(new Vector3(3000f, 596f, 0f), 3, false));
			objectInRoom[32].Add(new ExplosiveBox(new Vector3(3056f, 596f, 0f), 3, false));
			objectInRoom[33] = new List<GameObject>();
			mBossRoom = new Room(3228f, 328f, 500f, 248f, 100f);
			World.CurrentWorld.RoomList[26].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[35]);
			World.CurrentWorld.RoomList[27].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[35]);
			World.CurrentWorld.RoomList[26].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[34]);
			World.CurrentWorld.RoomList[27].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[34]);
			World.CurrentWorld.RoomList[26].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[33]);
			World.CurrentWorld.RoomList[27].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[33]);
			World.CurrentWorld.RoomList[26].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[36]);
			World.CurrentWorld.RoomList[27].ModelTransform.Add(World.CurrentWorld.RoomList[32].ModelTransform[36]);
		}

		public static void LoadDialog(out Dialog[] dialogList)
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				dialogList = new Dialog[2];
				dialogList[0] = new Dialog(new Chat[11]
				{
					new Chat(10, 2, new string[2]
					{
						"已定位灾创病毒坐标。",
						"请求：对我所在的地点进行数据扫描。"
					}, "Ares_Request"),
					new Chat(61, 1, new string[2]
					{
						"那东西不是灾创。",
						"那些绿色气体是连接过程中生成的副产物。"
					}),
					new Chat(62, 1, new string[1]
					{
						"但为了安全起见，你还是别靠近为好。"
					}),
					new Chat(61, 1, new string[2]
					{
						"除非你想被那些拥有意识的生物机械病毒控制，",
						"变成没有自我只懂得破坏的狂暴机器人。"
					}),
					new Chat(70, 1, new string[2]
					{
						"那你就去靠近它，触碰它，吸入它。",
						"甚至把它当烟抽，反正与我无关。"
					}),
					new Chat(10, 2, new string[1]
					{
						"瓦尔基莉，你太紧张了。"
					}, "Ares_Scan"),
					new Chat(56, 1, new string[3]
					{
						"我太紧张？！",
						"亚扎人把我们派到满是疯子机器人的空间站",
						"来执行这种不要命的任务。"
					}),
					new Chat(56, 1, new string[1]
					{
						"我当然得紧张！"
					}),
					new Chat(14, 2, new string[1]
					{
						"别担心。我不会失败的。"
					}, "Ares_Request"),
					new Chat(59, 1, new string[2]
					{
						"我知道你对自己有信心，阿瑞斯。",
						"一定要活着，好吗？"
					}),
					new Chat(12, 2, new string[1]
					{
						"（我一定不会失败的。）"
					})
				});
				dialogList[1] = new Dialog(new Chat[11]
				{
					new Chat(52, 1, new string[2]
					{
						"阿瑞斯，快停下！",
						"那距离已经超过了你的最大跳跃距离。"
					}),
					new Chat(14, 2, new string[2]
					{
						"你说得没错。但我没发现有其它路可以走。",
						"我必须试一下。"
					}),
					new Chat(53, 1, new string[1]
					{
						"别急，我有更简单的办法让你过去。"
					}),
					new Chat(50, 1, new string[2]
					{
						"// 已确认合适的上传路径",
						"// 原理图下载中"
					}, "remind"),
					new Chat(50, 1, new string[2]
					{
						"// 力量分发器重新配置中",
						"// 推进部件激活中"
					}),
					new Chat(50, 1, new string[1]
					{
						"// …… …… …… …… ……"
					}),
					new Chat(65, 1, new string[1]
					{
						"// 部件已上线"
					}, "success"),
					new Chat(58, 1, new string[2]
					{
						"这样应该就行了。这项改进能让你在空中瞬间改",
						"变轨道。"
					}),
					new Chat(10, 2, new string[2]
					{
						"谢谢你，瓦尔基莉。有了新增的向量预测功能，",
						"成功率应该会提高不少。"
					}),
					new Chat(65, 1, new string[2]
					{
						"谢谢你？阿瑞斯，要不是我知道怎么做更好，",
						"我一定会让你自己去升级的。"
					}),
					new Chat(10, 2, new string[1]
					{
						"…… ……"
					})
				});
			}
			else
			{
				dialogList = new Dialog[2];
				dialogList[0] = new Dialog(new Chat[9]
				{
					new Chat(20, 2, new string[1]
					{
						"我收到讯号了，瓦尔基莉。请讲。"
					}),
					new Chat(62, 1, new string[1]
					{
						"这事有点意思……"
					}),
					new Chat(51, 1, new string[2]
					{
						"在对这里的机器进行分析后，我发现灾创病毒是通过",
						"主机线路传染开的，是由内而外的一次病毒感染。"
					}),
					new Chat(51, 1, new string[1]
					{
						"那些绿色气体是连接过程中生成的副产物。"
					}),
					new Chat(62, 1, new string[2]
					{
						"机器感染病毒的时间越久，灾创对他们",
						"的控制力就越强。"
					}),
					new Chat(51, 1, new string[1]
					{
						"看来这台机器是最近才被感染的。"
					}),
					new Chat(22, 2, new string[2]
					{
						"它来的方向应该有不少这样的机器。",
						"阿瑞斯没问题吧？"
					}),
					new Chat(65, 1, new string[2]
					{
						"是有些小阻碍，但没有事情能难住他。",
						"只不过他现在还没找到卡森博士。"
					}),
					new Chat(20, 2, new string[2]
					{
						"我现在就赶过去。",
						"塔鲁斯通话完毕。"
					}, "Tarus_Argue")
				});
				dialogList[1] = new Dialog(new Chat[11]
				{
					new Chat(22, 2, new string[1]
					{
						"看来我得找其它路过去。"
					}),
					new Chat(53, 1, new string[1]
					{
						"或者你能跳过去。"
					}),
					new Chat(20, 2, new string[1]
					{
						"谢谢你看得起我，但我肯定做不到。"
					}),
					new Chat(62, 1, new string[3]
					{
						"哼！机器人也要有梦想！",
						"我做了一些东西……",
						"收好，可能会有点疼。"
					}),
					new Chat(50, 1, new string[2]
					{
						"// 已确认合适的上传路径",
						"// 原理图下载中"
					}, "remind"),
					new Chat(50, 1, new string[2]
					{
						"// 力量分发器重新配置中",
						"// 推进部件激活中"
					}),
					new Chat(50, 1, new string[1]
					{
						"// …… …… …… …… ……"
					}),
					new Chat(65, 1, new string[1]
					{
						"// 部件已上线"
					}, "success"),
					new Chat(58, 1, new string[2]
					{
						"这样应该就行了。这项改进能让你在空中瞬间改",
						"变轨道。"
					}),
					new Chat(20, 2, new string[2]
					{
						"你的智慧锦囊里还有其它点子吗？",
						"比如帮我变出把大手枪？"
					}, "Tarus_Talk"),
					new Chat(55, 1, new string[2]
					{
						"很遗憾，这些东西你就凑合着用吧。",
						"好了，去吧。试试它的功能！"
					})
				});
			}
		}

		public static void LoadAmbush(out AmbushSpawner[] ambushSpawnerList)
		{
			ambushSpawnerList = new AmbushSpawner[3];
			ambushSpawnerList[0] = new AmbushSpawner(1);
			ambushSpawnerList[0].OnStart = Ambush1Start;
			ambushSpawnerList[0].OnCancel = Ambush1Cancel;
			ambushSpawnerList[0].OnFinish = Ambush1Finish;
			AmbushGroup ambushGroup = new AmbushGroup(2);
			ambushGroup.SpareLife = 0;
			ambushGroup.SpawnGap = 1f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.FlyingPod);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(-44f, 16f, 0f);
			ambushGroup.Enemies[0].FaceDirection = 1;
			ambushGroup.Enemies[0].Actions = new EnemyAction[3]
			{
				new MoveToAction(3f, new Vector3(12f, 16f, 0f), new Vector3(-0.5f, -0.5f, 0f)),
				new TurnToAction(-1),
				new MoveToAction(1.5f, new Vector3(-10f, -6f, 0f), Vector3.Zero)
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.PodShocker);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(44f, 16f, 0f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Actions = new EnemyAction[3]
			{
				new JumpOutAction(-1, new Vector3(0f, 7f, 0f), new Vector3(25f, 0f, 0f)),
				new WalkAction(2.8f, -1),
				new WaitAction(0.5f)
			};
			ambushSpawnerList[0].Groups[0] = ambushGroup;
			ambushSpawnerList[1] = new AmbushSpawner(1);
			ambushSpawnerList[1].OnStart = Ambush2Start;
			ambushSpawnerList[1].OnCancel = Ambush2Cancel;
			ambushSpawnerList[1].OnFinish = Ambush2Finish;
			ambushGroup = new AmbushGroup(1);
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.PodShocker);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(0f, -20f, -80f);
			ambushGroup.Enemies[0].FaceDirection = -1;
			ambushGroup.Enemies[0].Life = 3;
			ambushGroup.Enemies[0].GroupSize = 3;
			ambushGroup.Enemies[0].Actions = new EnemyAction[2]
			{
				new JumpToAction(0.5f, new Vector3(0f, -18f, -24f), new Vector3(13f, 0f, 0f), -300f),
				new MoveToAction(0.8f, new Vector3(0f, -18f, 0f), Vector3.Zero, new Vector3(0f, 1f, 1f), Vector3.Zero)
			};
			ambushSpawnerList[1].Groups[0] = ambushGroup;
			ambushSpawnerList[2] = new AmbushSpawner(1);
			ambushSpawnerList[2].OnStart = Ambush3Start;
			ambushSpawnerList[2].OnCancel = Ambush3Cancel;
			ambushSpawnerList[2].OnFinish = Ambush3Finish;
			ambushGroup = new AmbushGroup(2);
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.ZytronWalker);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(0f, -16f, -80f);
			ambushGroup.Enemies[0].FaceDirection = -1;
			ambushGroup.Enemies[0].Life = 1;
			ambushGroup.Enemies[0].GroupSize = 1;
			ambushGroup.Enemies[0].Actions = new EnemyAction[3]
			{
				new JumpToAction(0.5f, new Vector3(0f, -14f, -24f), new Vector3(13f, 0f, 0f), -300f),
				new MoveToAction(0.8f, new Vector3(0f, -14f, 0f), Vector3.Zero, new Vector3(0f, 1f, 1f), Vector3.Zero),
				new WaitAction(0.2f)
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.PodShocker);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(0f, -20f, -80f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Life = 3;
			ambushGroup.Enemies[1].GroupSize = 2;
			ambushGroup.Enemies[1].Actions = new EnemyAction[3]
			{
				new JumpToAction(0.5f, new Vector3(0f, -18f, -24f), new Vector3(13f, 0f, 0f), -300f),
				new MoveToAction(0.8f, new Vector3(0f, -18f, 0f), Vector3.Zero, new Vector3(0f, 1f, 1f), Vector3.Zero),
				new WaitAction(0.2f)
			};
			ambushSpawnerList[2].Groups[0] = ambushGroup;
		}

		private static void Ambush1Start()
		{
			ambush1Door.Activated = false;
		}

		private static void Ambush1Cancel()
		{
			if (ambush1Door != null)
			{
				ambush1Door.Activated = true;
			}
		}

		private static void Ambush1Finish()
		{
			mReward[1] = 20;
			mReward[5] = 10;
			MaterialDropper.Instance.Start(300f, 300f, 678f, 686f, 0.1f, mReward, new Vector3(60f, 0f, 0f));
			ambush1Door.Activated = true;
		}

		private static void Ambush2Start()
		{
			ambush2Door.Activated = false;
		}

		private static void Ambush2Cancel()
		{
			ambush2Door.Activated = true;
		}

		private static void Ambush2Finish()
		{
			ambush2Reward.Destroyed = false;
			ProcessManager.AddProcess(ambush2Reward);
			ambush2Door.Activated = true;
		}

		private static void Ambush3Start()
		{
			ambush3Door.Activated = false;
		}

		private static void Ambush3Cancel()
		{
			ambush3Door.Activated = true;
		}

		private static void Ambush3Finish()
		{
			ambush3Reward.Destroyed = false;
			ProcessManager.AddProcess(ambush3Reward);
			ambush3Door.Activated = true;
		}

		private static PositionedObject PodBodyAppear()
		{
			Sprite sprite = mPods[mPodIndex];
			SpriteManager.AddSprite(sprite);
			mPodIndex++;
			mPodIndex %= 16;
			return sprite;
		}

		private static void PodBodyDisappear(PositionedObject pod)
		{
			SpriteManager.RemoveSpriteOneWay(pod as Sprite);
		}

		private static void StartDrawPodBody(Crane crane)
		{
			for (int i = 0; i < 2; i++)
			{
				SpriteManager.AddSprite(mPods[mPodIndex]);
				crane.DrewObject[i] = mPods[mPodIndex];
				mPodIndex++;
				mPodIndex %= 16;
				crane.DrewObject[i].AttachTo(crane.Model, false);
			}
			crane.DrewObject[0].RelativePosition = new Vector3(-16f, -32f, 0f);
			crane.DrewObject[1].RelativePosition = new Vector3(16f, -32f, 0f);
			crane.DrewObject[0].RelativeVelocity = new Vector3(10f, 50f, 0f);
			crane.DrewObject[1].RelativeVelocity = new Vector3(-10f, 50f, 0f);
			crane.DrewObject[0].RelativeRotationZ = -0.5f;
			crane.DrewObject[1].RelativeRotationZ = 0.5f;
			crane.DrewObject[0].RelativeRotationZVelocity = 1.2f;
			crane.DrewObject[1].RelativeRotationZVelocity = -1.2f;
		}

		private static void DrawPodBody(Crane crane)
		{
			if (crane.DrewObject[1].RelativeRotationZ >= 3f)
			{
				crane.DrewObject[0].RelativeRotationZ = 0f;
				crane.DrewObject[1].RelativeRotationZ = 0f;
				crane.DrewObject[0].RelativeRotationZVelocity = 0f;
				crane.DrewObject[1].RelativeRotationZVelocity = 0f;
			}
			if (crane.DrewObject[0].RelativeX >= -13f)
			{
				crane.DrewObject[0].RelativeX = -13f;
				crane.DrewObject[1].RelativeX = 13f;
				crane.DrewObject[0].RelativeXVelocity = 0f;
				crane.DrewObject[1].RelativeXVelocity = 0f;
			}
			if (crane.DrewObject[0].RelativeY > -12f)
			{
				crane.DrewObject[0].RelativeY = -12f;
				crane.DrewObject[1].RelativeY = -12f;
				crane.DrewObject[0].RelativeYVelocity = 0f;
				crane.DrewObject[1].RelativeYVelocity = 0f;
			}
		}

		private static void StartDropPodBody(Crane crane)
		{
			crane.HitReceiver = false;
			for (int i = 0; i < 2; i++)
			{
				crane.DrewObject[i].RelativeYAcceleration = -700f;
			}
		}

		private static void DropPodBody(Crane crane)
		{
			if (crane.DrewObject[0].Y <= crane.Receiver.StartPosition.Y + 8f && !crane.HitReceiver)
			{
				crane.DrewObject[0].RelativeYVelocity = 0f;
				crane.DrewObject[1].RelativeYVelocity = 0f;
				crane.DrewObject[0].RelativeYAcceleration = 0f;
				crane.DrewObject[1].RelativeYAcceleration = 0f;
				crane.DrewObject[0].Detach();
				crane.DrewObject[1].Detach();
				crane.DrewObject[0].Y = crane.Receiver.StartPosition.Y + 8f;
				crane.DrewObject[1].Y = crane.Receiver.StartPosition.Y + 8f;
				crane.Receiver.AddShiftedObject(crane.DrewObject[0]);
				crane.Receiver.AddShiftedObject(crane.DrewObject[1]);
				crane.HitReceiver = true;
			}
		}

		public static void RecycleplantOpening1()
		{
			World.Play(0, false);
			World.Camera.Mode = CameraMode.Cinematic;
			World.Camera.Position = new Vector3(40f, 754f, 70f);
			Player.Instance.Spawn(new Vector3(40f, 780f, 0f));
			Player.Instance.ControlEnabled = false;
			Player.Instance.StateMachineEnabled = false;
			Director.WaitForSeconds(2.5f, RecycleplantOpening1_2, false);
		}

		public static void RecycleplantOpening1_2()
		{
			Narrator.Instance.Show("回收厂\n米诺斯空间站内部", new Vector3(8f, -5f, -40f));
			World.Camera.Velocity.Y = -16f;
			Director.WaitForSeconds(6.5f, RecycleplantOpening2, false);
		}

		public static void RecycleplantOpening2()
		{
			World.Camera.Velocity.Y = 0f;
			World.Camera.EnterRoom(World.GetRoom(0), false, false, 0.5f);
			Director.WaitForSeconds(0.6f, RecycleplantOpening3, false);
		}

		public static void RecycleplantOpening3()
		{
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
			Player.Instance.ControlEnabled = true;
			Player.Instance.StateMachineEnabled = true;
			World.SaveCheckPoint(new Vector3(40f, 648f, 0f), 0, false, false, false, false, false, false);
			mOpenScenePlatform.SetSpawnPosition(new Vector3(40f, 628f, 0f));
			mOpenScenePlatform.Disabled = true;
		}

		public static void BlastTutor()
		{
			if (!ProfileManager.Current.TutorialBlast)
			{
				if (Player.Instance.Type == PlayerType.Mars)
				{
					HintUI.Instance.ShowHint(161, BlastChecker);
				}
				else
				{
					HintUI.Instance.ShowHint(162, BlastChecker);
				}
			}
		}

		private static void BlastChecker()
		{
			if (mTutorGas.Destroyed)
			{
				ProfileManager.Current.TutorialBlast = true;
				HintUI.Instance.HideHint();
			}
		}

		public static void MiniBossZytronGunner()
		{
			miniBossDoor.Activated = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Player.Instance.FaceDirection = 1;
			Player.Instance.ControlEnabled = false;
			Player.Instance.ChangeToUnStuckStage();
			Player.Instance.ForceIdle();
			Director.WaitForSeconds(0.5f, MiniBossZytronGunner2, false);
		}

		private static void MiniBossZytronGunner2()
		{
			World.Camera.TargetingTransform(new Vector3(2000f, 396f, 102f), 2.5f, true);
			Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.ZytronGunner, new Vector3(2048f, 368f, 0f), -1);
			Director.ControlledEnemy.IsAIEnabled = false;
			Director.ControlledEnemy.SpriteRig.SetPose("walk", 0.0);
			Director.ControlledEnemy.IsAllowRemovingByEnemyGarbageCollection = false;
			EnemyTweener enemyTweener = Director.EnemyTweener;
			Enemy controlledEnemy = Director.ControlledEnemy;
			int[] propertyIndexes = new int[1];
			enemyTweener.Tween(controlledEnemy, propertyIndexes, new object[1]
			{
				new Vector3(2024f, 368f, 0f)
			}, 0f, 2.5f, MiniBossZytronGunner3);
			SFXManager.PlayLoopSound("zytron_walk", 2.88f, 0.72f);
		}

		private static void MiniBossZytronGunner3()
		{
			SFXManager.PlaySound("miniboss_scream");
			World.Camera.EnterRoom(World.GetRoom(14), true, false, 0.5f);
			World.ChangeRoom(14);
			Director.ControlledEnemy.SpriteRig.SetPose("taunt", 0.0);
			Director.WaitForSeconds(0.4f, MiniBossZytronGunner4, false);
		}

		private static void MiniBossZytronGunner4()
		{
			World.Camera.Shake(2, 0.8f);
			Director.WaitForSeconds(0.7f, MiniBossZytronGunner4_1, false);
		}

		private static void MiniBossZytronGunner4_1()
		{
			Director.ControlledEnemy.SpriteRig.SetPose("idle", 0.0);
			Director.WaitForSeconds(1.8f, MiniBossZytronGunner5, false);
		}

		private static void MiniBossZytronGunner5()
		{
			(Director.ControlledEnemy as ZytronGunner).Activate();
			World.Camera.TransformToSideScrollingMode(0.6f, 116f);
			Player.Instance.ControlEnabled = true;
			Player.Instance.StateMachineEnabled = true;
		}

		public static void UnlockMiniBossDoor()
		{
			miniBossDoor.Activated = true;
		}

		public static void AcquireLaserSMG0(Sprite laserSMG)
		{
			if (ProfileManager.Current.AcquiredWeapon[1] == 0)
			{
				Player.Instance.ControlEnabled = false;
				_laserSMG = laserSMG;
				SFXManager.PlaySound("remind");
				World.ShowDialogByID(0, AcquireLaserSMG1);
				World.Camera.Mode = CameraMode.Cinematic;
			}
		}

		public static void AcquireLaserSMG1()
		{
			mLaserSMG = _laserSMG;
			float num = mLaserSMG.Position.X - Player.Instance.Position.X;
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
			PlayerTweener playerTweener = Director.PlayerTweener;
			Player instance = Player.Instance;
			int[] propertyIndexes = new int[1];
			playerTweener.Tween(instance, propertyIndexes, new object[1]
			{
				new Vector3(mLaserSMG.X + (float)((num > 0f) ? (-4) : 4), Player.Instance.Position.Y, 0f)
			}, 0f, Math.Abs(num) / 40f, AcquireLaserSMG2);
		}

		public static void AcquireLaserSMG2()
		{
			mLaserSMG.ScaleXVelocity = 20f;
			mLaserSMG.ScaleYVelocity = 20f;
			mLaserSMG.AlphaRate = -4f;
			OnceEmitParticlePool.Emit(Player.Instance.Position, "material_picked");
			SFXManager.StopLoopSound();
			if (Player.Instance.Type == PlayerType.Mars)
			{
				(Player.Instance as Mars).StateMachine.ChangeState(MarsCrouch.Instance);
			}
			else
			{
				(Player.Instance as Tarus).StateMachine.ChangeState(TarusCrouch.Instance);
			}
			Director.WaitForSeconds(0.7f, AcquireLaserSMG3, false);
		}

		public static void AcquireLaserSMG3()
		{
			ProfileManager.Current.AcquiredWeapon[1] = 1;
			ProfileManager.Current.AcqiuredUpgrade[1] = 1;
			ProfileManager.BeginSave(null, 1, false);
			UnlockUI.Instance.Show(1);
			Director.WaitForSeconds(7.3f, AcquireLaserSMG4, false);
		}

		public static void AcquireLaserSMG4()
		{
			UnlockMiniBossDoor();
			HintUI.Instance.ShowHint(174, LaserSMGChecker);
			mLaserSMGTutorTime = 10f;
			Player.Instance.ControlEnabled = true;
			Player.Instance.StateMachineEnabled = true;
			World.Camera.Mode = CameraMode.SideScrolling;
			BGMManager.Play((Player.Instance.Type != 0) ? 2 : 0);
		}

		private static void LaserSMGChecker()
		{
			mLaserSMGTutorTime -= TimeManager.SecondDifference;
			if (mLaserSMGTutorTime <= 0f || ProfileManager.Current.EquipedWeapon == WeaponType.LaserSMG)
			{
				HintUI.Instance.HideHint();
			}
		}

		public static void BossCarrion_00()
		{
			bossDoor.Activated = false;
			Player.Instance.ControlEnabled = false;
			Player.Instance.FaceDirection = 1;
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(0.6f, BossCarrion_01, false);
		}

		private static void BossCarrion_01()
		{
			World.Camera.Mode = CameraMode.Cinematic;
			BGMManager.FadeOff();
			Player.Instance.StateMachineEnabled = false;
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(3320f, 400f, 0f),
				"run_idle"
			}, 0f, 3f, BossCarrion_02);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void BossCarrion_02()
		{
			SFXManager.PlaySound("miniboss_roar");
			World.Camera.Shake(2, 2f);
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(3392f, 400f, 0f),
				"run_idle"
			}, 0f, 1f, BossCarrion_02_01);
		}

		private static void BossCarrion_02_01()
		{
			Apollo.Instance.Dismiss();
			BGMManager.Play(1);
			BossStageBlocks[0].Destroy();
			BossStageBlocks[1].Destroy();
			Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.Carrion, new Vector3(3864f, 400f, 0f), -1);
			Director.ControlledEnemy.SpriteRig.Root.Position = new Vector3(3864f, 400f, 0f);
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(3424f, 400f, 0f),
				"run_idle"
			}, 0f, 1f, BossCarrion_03);
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1];
			cameraTweener.Tween(camera, propertyIndexes, new object[1]
			{
				3528f
			}, 0f, 1.8f, BossCarrion_04);
		}

		private static void BossCarrion_03()
		{
			Player.Instance.StateMachineEnabled = true;
			if (Player.Instance is Mars)
			{
				(Player.Instance as Mars).StateMachine.ChangeState(MarsStand.Instance);
			}
			else if (Player.Instance is Tarus)
			{
				(Player.Instance as Tarus).StateMachine.ChangeState(TarusStand.Instance);
			}
			SFXManager.StopLoopSound();
		}

		private static void BossCarrion_04()
		{
			BossStageBlocks[5].Destroy();
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1];
			cameraTweener.Tween(camera, propertyIndexes, new object[1]
			{
				3632f
			}, 0f, 1.5f, BossCarrion_05);
		}

		private static void BossCarrion_05()
		{
			BossStageBlocks[7].Destroy();
			BossStageBlocks[10].Destroy();
			BossStageBlocks[9].Destroy();
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1];
			cameraTweener.Tween(camera, propertyIndexes, new object[1]
			{
				3424f
			}, 0f, 1f, BossCarrion_06);
		}

		private static void BossCarrion_06()
		{
			AxisAlignedRectangle playerBoundingRectangle = (Director.ControlledEnemy as Carrion).Core.PlayerBoundingRectangle;
			playerBoundingRectangle.AttachTo(Player.Instance, false);
			ShapeManager.AddAxisAlignedRectangle(playerBoundingRectangle);
			ShapeManager.AddAxisAlignedRectangle((Director.ControlledEnemy as Carrion).Core.CarrionBoundingRectangle);
			World.Camera.AddDynamicBoundingFocusedRectangle(playerBoundingRectangle);
			World.Camera.AddDynamicBoundingFocusedRectangle((Director.ControlledEnemy as Carrion).Core.CarrionBoundingRectangle);
			World.Camera.DynamicBoundingModeYLimit = 370f;
			Player.Instance.ControlEnabled = true;
			World.Camera.EnterRoom(mBossRoom, true, true, 0f);
		}

		public static void UnlockBossDoor()
		{
			bossDoor.Activated = true;
		}

		public static void BoostChip1()
		{
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(2.5f, ShowBoostDialog, false);
		}

		public static void ShowBoostDialog()
		{
			World.ShowDialogByID(1, BoostChip2);
		}

		public static void BoostChip2()
		{
			Player.Instance.ControlEnabled = false;
			Vector3 position = Player.Instance.Position;
			position.Y += 26f;
			mBoostChip.Spawn(position);
			mBoostChip.Velocity.Y = -4f;
			SFXManager.PlayLoopSound("charge", 4f, 0.75f);
			TimedEmitParticlePool.Summon("chip_unlock", mBoostChip, 3f);
			Director.WaitForSeconds(5f, BoostChip3, false);
		}

		public static void BoostChip3()
		{
			ProfileManager.BeginSave(null, 1, false);
			UnlockUI.Instance.Show(9);
			Director.WaitForSeconds(7.3f, BoostChipTutor, false);
		}

		public static void BoostChipTutor()
		{
			UnlockBossDoor();
			if (Player.Instance.Type == PlayerType.Mars)
			{
				switch (GamePad.GetPreferredDeviceFamily())
				{
				case 2:
				case 3:
					HintUI.Instance.ShowHint(168, BoostChecker);
					break;
				default:
					HintUI.Instance.ShowHint(169, BoostChecker);
					break;
				}
			}
			else
			{
				HintUI.Instance.ShowHint(170, BoostChecker);
			}
			Player.Instance.ControlEnabled = true;
			World.Camera.Mode = CameraMode.SideScrolling;
		}

		private static void BoostChecker()
		{
			if (Player.Instance.Position.X > 3728f)
			{
				HintUI.Instance.HideHint();
			}
		}
	}
}
