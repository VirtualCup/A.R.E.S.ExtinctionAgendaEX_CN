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
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ARES360
{
	public static class LV4Loader
	{
		public static ExplodeableLevel[] EnviPod;

		private static BlastDoor ambush1Door;

		private static BlastDoor ambush2Door;

		private static BlastDoor ambush3Door;

		private static Dictionary<int, int> mReward;

		public static BlastDoor MiniBossDoor;

		public static BlastDoor BossDoor24;

		public static SentinelBackgroundPlatformSpawner SentinelBackgroundPlatform;

		public static void LoadEnemy()
		{
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.FlyingPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Turret);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronCrawler);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.BomberHive);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Bomber);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShooter);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronWalker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Slither);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Leviathan);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Sentinel);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShocker, 4);
			EnemyManager.CreateAndStoreEnemy(EnemyType.FlyingPod, 6);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Turret, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronCrawler, 4);
			EnemyManager.CreateAndStoreEnemy(EnemyType.BomberHive, 6);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Bomber, 30);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShooter, 3);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Slither, 3);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronWalker, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Leviathan, 1);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Sentinel, 1);
		}

		public static void Destroy()
		{
			EnviPod = null;
			ambush1Door = null;
			ambush2Door = null;
			ambush3Door = null;
			mReward = null;
			MiniBossDoor = null;
			BossDoor24 = null;
			SentinelBackgroundPlatform = null;
		}

		public static void LoadObject(out List<GameObject>[] objectInRoom)
		{
			mReward = new Dictionary<int, int>(3);
			objectInRoom = new List<GameObject>[41];
			for (int i = 0; i < 41; i++)
			{
				objectInRoom[i] = new List<GameObject>();
			}
			objectInRoom[2].Add(new PlatformSpawner(new Vector3(664f, 648f, 0f), new Vector3(664f, 280f, 0f), 2f));
			objectInRoom[2].Add(new PlatformSpawner(new Vector3(704f, 288f, 0f), new Vector3(704f, 664f, 0f), 2f));
			objectInRoom[2].Add(new PlatformSpawner(new Vector3(744f, 680f, 0f), new Vector3(744f, 280f, 0f), 2f));
			objectInRoom[37].Add(new BlastDoor(new Vector3(12f, 596f, 0f), true, 1));
			objectInRoom[37].Add(new BlastDoor(new Vector3(164f, 596f, 0f), true, 1));
			objectInRoom[37].Add(new Fan(new Vector3(108f, 596f, 0f)));
			ambush1Door = new BlastDoor(new Vector3(284f, 596f, 0f), true, 1);
			objectInRoom[0].Add(ambush1Door);
			objectInRoom[6].Add(new BlastDoor(new Vector3(948f, 340f, 0f), true, 1));
			objectInRoom[6].Add(new BlastDoor(new Vector3(1196f, 340f, 0f), true, 1));
			objectInRoom[8].Add(new BlastDoor(new Vector3(1284f, 340f, 0f), true, 1));
			objectInRoom[12].Add(new BlastDoor(new Vector3(1436f, 148f, 0f), true, 1));
			ambush2Door = new BlastDoor(new Vector3(1556f, 148f, 0f), true, 1);
			objectInRoom[12].Add(ambush2Door);
			objectInRoom[14].Add(new BlastDoor(new Vector3(1076f, 532f, 0f), true, -1));
			objectInRoom[16].Add(new BlastDoor(new Vector3(988f, 532f, 0f), true, -1));
			objectInRoom[18].Add(new BlastDoor(new Vector3(1092f, 836f, 0f), true, 1));
			MiniBossDoor = new BlastDoor(new Vector3(1228f, 724f, 0f), true, 1);
			objectInRoom[20].Add(MiniBossDoor);
			objectInRoom[21].Add(new BlastDoor(new Vector3(1700f, 724f, 0f), true, 1));
			objectInRoom[22].Add(new BlastDoor(new Vector3(1820f, 724f, 0f), true, 1));
			objectInRoom[25].Add(new BlastDoor(new Vector3(1844f, 548f, 0f), true, 1));
			objectInRoom[26].Add(new BlastDoor(new Vector3(2148f, 548f, 0f), true, 1));
			ambush3Door = new BlastDoor(new Vector3(2268f, 548f, 0f), true, 1);
			objectInRoom[26].Add(ambush3Door);
			objectInRoom[28].Add(new BlastDoor(new Vector3(2388f, 548f, 0f), true, 1));
			objectInRoom[28].Add(new BlastDoor(new Vector3(2388f, 548f, 0f), true, 1));
			objectInRoom[28].Add(new BlastDoor(new Vector3(2636f, 340f, 0f), true, 1));
			objectInRoom[32].Add(new BlastDoor(new Vector3(2692f, 180f, 0f), true, -1));
			objectInRoom[35].Add(new BlastDoor(new Vector3(1988f, 116f, 0f), true, -1));
			BossDoor24 = new BlastDoor(new Vector3(1772f, 116f, 0f), true, -1);
			objectInRoom[35].Add(BossDoor24);
			objectInRoom[13].Add(new OneWayPlatform(new Vector2(32f, 4f), new Vector3(1600f, 132f, -2f), new Vector3(1600f, 516f, -2f), 30f, false, "Content/World/m_LV3_elevator_8", new Vector3(0f, 4f, 0f)));
			EnviPod = new ExplodeableLevel[20];
			EnviPod[0] = new ExplodeableLevel(new Vector3(952f, 460f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[1] = new ExplodeableLevel(new Vector3(928f, 492f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[2] = new ExplodeableLevel(new Vector3(896f, 524f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[3] = new ExplodeableLevel(new Vector3(960f, 548f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[4] = new ExplodeableLevel(new Vector3(896f, 564f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[5] = new ExplodeableLevel(new Vector3(936f, 580f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[6] = new ExplodeableLevel(new Vector3(896f, 604f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[7] = new ExplodeableLevel(new Vector3(960f, 620f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[8] = new ExplodeableLevel(new Vector3(928f, 652f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[9] = new ExplodeableLevel(new Vector3(896f, 684f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[10] = new ExplodeableLevel(new Vector3(960f, 684f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[11] = new ExplodeableLevel(new Vector3(944f, 716f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[12] = new ExplodeableLevel(new Vector3(912f, 740f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[13] = new ExplodeableLevel(new Vector3(960f, 764f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[14] = new ExplodeableLevel(new Vector3(896f, 796f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[15] = new ExplodeableLevel(new Vector3(936f, 820f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[16] = new ExplodeableLevel(new Vector3(960f, 836f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[17] = new ExplodeableLevel(new Vector3(896f, 860f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[18] = new ExplodeableLevel(new Vector3(1168f, 788f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			EnviPod[19] = new ExplodeableLevel(new Vector3(1200f, 788f, 0f), 16f, 4f, false, "Content/World/m_plant_platform", new Vector3(0f, -15f, -2f), Vector3.One);
			objectInRoom[16].Add(EnviPod[0]);
			objectInRoom[16].Add(EnviPod[1]);
			objectInRoom[16].Add(EnviPod[2]);
			objectInRoom[16].Add(EnviPod[3]);
			objectInRoom[16].Add(EnviPod[4]);
			objectInRoom[16].Add(EnviPod[5]);
			objectInRoom[16].Add(EnviPod[6]);
			objectInRoom[16].Add(EnviPod[7]);
			objectInRoom[16].Add(EnviPod[8]);
			objectInRoom[16].Add(EnviPod[9]);
			objectInRoom[16].Add(EnviPod[10]);
			objectInRoom[16].Add(EnviPod[11]);
			objectInRoom[16].Add(EnviPod[12]);
			objectInRoom[16].Add(EnviPod[13]);
			objectInRoom[16].Add(EnviPod[14]);
			objectInRoom[16].Add(EnviPod[15]);
			objectInRoom[16].Add(EnviPod[16]);
			objectInRoom[16].Add(EnviPod[17]);
			objectInRoom[19].Add(EnviPod[18]);
			objectInRoom[19].Add(EnviPod[19]);
			objectInRoom[4].Add(new MaterialBox(new Vector3(476f, 340f, 0f), 3, false));
			objectInRoom[4].Add(new CrackedBox(new Vector3(496f, 336f, 0f), 2, false));
			objectInRoom[5].Add(new CrackedBox(new Vector3(816f, 352f, 0f), 2, false));
			objectInRoom[5].Add(new CrackedBox(new Vector3(836f, 340f, 0f), 3, false));
			objectInRoom[5].Add(new CrackedBox(new Vector3(928f, 368f, 0f), 2, false));
			objectInRoom[21].Add(new CrackedBox(new Vector3(1336f, 744f, 0f), 2, false));
			objectInRoom[21].Add(new CrackedBox(new Vector3(1460f, 724f, 0f), 3, false));
			objectInRoom[21].Add(new CrackedBox(new Vector3(1448f, 744f, 0f), 2, false));
			objectInRoom[21].Add(new CrackedBox(new Vector3(1556f, 724f, 0f), 3, false));
			objectInRoom[21].Add(new CrackedBox(new Vector3(1580f, 748f, 0f), 3, false));
			objectInRoom[21].Add(new CrackedBox(new Vector3(1652f, 724f, 0f), 3, false));
			objectInRoom[14].Add(new CrackedBox(new Vector3(1536f, 616f, 0f), 2, false));
			objectInRoom[28].Add(new CrackedBox(new Vector3(2612f, 436f, 0f), 3, false));
			objectInRoom[31].Add(new CrackedBox(new Vector3(2888f, 176f, 0f), 2, false));
			objectInRoom[31].Add(new CrackedBox(new Vector3(2908f, 180f, 0f), 3, false));
			objectInRoom[32].Add(new CrackedBox(new Vector3(2380f, 116f, 0f), 3, false));
			objectInRoom[32].Add(new CrackedBox(new Vector3(2300f, 100f, 0f), 3, false));
			objectInRoom[33].Add(new CrackedBox(new Vector3(2280f, 96f, 0f), 2, false));
			objectInRoom[8].Add(new ExplosiveBox(new Vector3(1412f, 364f, 0f), 3, false));
			objectInRoom[8].Add(new ExplosiveBox(new Vector3(1412f, 388f, 0f), 3, false));
			objectInRoom[8].Add(new CrackedBox(new Vector3(1412f, 412f, 0f), 3, false));
			objectInRoom[24].Add(new CrackedBox(new Vector3(1772f, 548f, 0f), 3, false));
			objectInRoom[24].Add(new ExplosiveBox(new Vector3(1796f, 548f, 0f), 3, false));
			objectInRoom[24].Add(new CrackedBox(new Vector3(1748f, 572f, 0f), 3, false));
			objectInRoom[24].Add(new CrackedBox(new Vector3(1772f, 572f, 0f), 3, false));
			objectInRoom[24].Add(new CrackedBox(new Vector3(1796f, 572f, 0f), 3, false));
			objectInRoom[24].Add(new ExplosiveBox(new Vector3(1748f, 596f, 0f), 3, false));
			objectInRoom[24].Add(new ExplodeableLevel(new Vector3(1772f, 596f, 0f), 12f, 12f, true, "Content/World/m_sm_box_small", new Vector3(0f, -12f, 0f), new Vector3(1.5f, 1.5f, 0.75f)));
			objectInRoom[24].Add(new ExplosiveBox(new Vector3(1796f, 596f, 0f), 3, false));
			objectInRoom[24].Add(new CrackedBox(new Vector3(1748f, 620f, 0f), 3, false));
			objectInRoom[24].Add(new ExplosiveBox(new Vector3(1772f, 620f, 0f), 3, false));
			objectInRoom[24].Add(new CrackedBox(new Vector3(1796f, 620f, 0f), 3, false));
			objectInRoom[24].Add(new ExplosiveBox(new Vector3(1748f, 644f, 0f), 3, false));
			objectInRoom[24].Add(new CrackedBox(new Vector3(1772f, 644f, 0f), 3, false));
			objectInRoom[24].Add(new ExplosiveBox(new Vector3(1748f, 668f, 0f), 3, false));
			objectInRoom[24].Add(new PlayerBlocker(new Vector3(1880f, 652f, 0f), 9f, 12f));
			objectInRoom[24].Add(new StaticAttack(new Vector3(1880f, 648f, 0f), 10f, 16f, 10, false, true, true));
			SentinelBackgroundPlatformSpawner item = new SentinelBackgroundPlatformSpawner(new Vector3(1832f, 32f, -48f), new Vector3(1832f, 264f, -48f), 1.2f, 3f, 32f);
			SentinelBackgroundPlatformSpawner sentinelBackgroundPlatformSpawner = new SentinelBackgroundPlatformSpawner(new Vector3(1880f, 264f, -44f), new Vector3(1880f, 32f, -44f), 2.6f, 2f, 24f);
			SentinelBackgroundPlatformSpawner item2 = new SentinelBackgroundPlatformSpawner(new Vector3(1928f, 32f, -48f), new Vector3(1928f, 264f, -48f), 1.2f, 3f, 32f);
			SentinelBackgroundPlatform = sentinelBackgroundPlatformSpawner;
			objectInRoom[35].Add(item);
			objectInRoom[35].Add(sentinelBackgroundPlatformSpawner);
			objectInRoom[35].Add(item2);
			objectInRoom[30].Add(new MaterialBoxExtra(new Vector3(2824f, 344f, 0f), 2, false));
			objectInRoom[30].Add(new MaterialBoxExtra(new Vector3(2824f, 240f, 0f), 2, false));
			objectInRoom[30].Add(new MaterialBox(new Vector3(2712f, 216f, 0f), 2, false));
			objectInRoom[30].Add(new MaterialBox(new Vector3(2752f, 264f, 0f), 2, false));
			objectInRoom[30].Add(new Fan(new Vector3(2796f, 364f, 0f)));
			objectInRoom[30].Add(new Fan(new Vector3(2804f, 276f, 0f)));
			objectInRoom[30].Add(new Fan(new Vector3(2788f, 244f, 0f)));
			objectInRoom[30].Add(new Fan(new Vector3(2724f, 236f, 0f)));
			objectInRoom[4].Add(new Fan(new Vector3(540f, 348f, 0f)));
			objectInRoom[32].Add(new GasTank(3, new Vector3(2488f, 116f, 0f)));
			objectInRoom[28].Add(new GasTank(3, new Vector3(2384f, 340f, 0f)));
			objectInRoom[30].Add(new GasTank(4, new Vector3(2800f, 184f, 0f)));
			objectInRoom[30].Add(new GasTank(3, new Vector3(2820f, 308f, 0f)));
			objectInRoom[22].Add(new ArmorUpgradeCapsule());
			BlastDoor blastDoor = new BlastDoor(new Vector3(1164f, 140f, 0f), false, 0);
			RailSwitch railSwitch = new RailSwitch(new Vector3(1176f, 192f, 0f), Vector3.Up, true, blastDoor);
			railSwitch.Tough = true;
			objectInRoom[10].Add(blastDoor);
			objectInRoom[10].Add(railSwitch);
			objectInRoom[26].Add(new ModelObject(new Vector3(2208f, 597f, 4f), new Vector3(3.14159274f, 0f, 0f), 16f, 4f, false, "Content/World/m_sm_vent", new Vector3(0f, -4f, 4f), Vector3.One));
		}

		public static void LoadDialog(out Dialog[] dialogList)
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				dialogList = new Dialog[5];
				dialogList[0] = new Dialog(new Chat[6]
				{
					new Chat(30, 1, new string[1]
					{
						"&#%$FFSG$##$%^&^BVXBGF#%^"
					}, "noise"),
					new Chat(66, 1, new string[1]
					{
						"呼！抱歉！呵呵。马上进行破译。"
					}),
					new Chat(30, 1, new string[2]
					{
						"我是卡森博士。",
						"我和其它生存者都被困在军工厂里。"
					}),
					new Chat(30, 1, new string[3]
					{
						"我不知道外面有没有人，",
						"但你要是收到信息的话，请帮助我们。",
						"我是卡森博士。我和其它生存…………"
					}),
					new Chat(52, 1, new string[2]
					{
						"阿瑞斯，我不知道这条信息在空间站里循环",
						"了多久，但你现在应该去军工厂看看！"
					}),
					new Chat(71, 1, new string[1]
					{
						"希望我们不会太迟……"
					})
				});
				dialogList[1] = new Dialog(new Chat[5]
				{
					new Chat(63, 1, new string[2]
					{
						"我的天，我的上帝，我的妈呀……",
						"那台机器人对你动了手脚，阿瑞斯！"
					}),
					new Chat(63, 1, new string[2]
					{
						"最可怕的是，它正在分子层面分解你的",
						"装甲！"
					}),
					new Chat(10, 2, new string[1]
					{
						"询问：推荐的行动方案？"
					}, "Ares_Request"),
					new Chat(71, 1, new string[2]
					{
						"离你不远的地方有座机器人维修舱。",
						"我打算远程激活它。"
					}),
					new Chat(52, 1, new string[3]
					{
						"……",
						"你还在等什么？！",
						"快跑过去啊！"
					})
				});
				dialogList[2] = new Dialog(new Chat[1]
				{
					new Chat(55, 1, new string[2]
					{
						"呼！差点没命！",
						"快点进去吧！"
					})
				});
				dialogList[3] = new Dialog(new Chat[10]
				{
					new Chat(55, 1, new string[1]
					{
						"好了，没事了！"
					}),
					new Chat(55, 1, new string[2]
					{
						"我顺便帮你改良了装甲的兼容性，",
						"你得感谢我在舱内数据库发现的原理图。"
					}),
					new Chat(55, 1, new string[1]
					{
						"机动性和整体战斗力应该有不小的提升。"
					}),
					new Chat(19, 2, new string[1]
					{
						"我没感觉有什么不同。"
					}, "Ares_Illogical"),
					new Chat(58, 1, new string[2]
					{
						"哦，我很乐意这么做，阿瑞斯。",
						"不用谢！不用谢！"
					}),
					new Chat(58, 1, new string[3]
					{
						"除了我以外，还有谁既能够获取军事网络",
						"的访问权限，又能对错综复杂的程序进行",
						"重新编译，"
					}),
					new Chat(58, 1, new string[1]
					{
						"还能对你的架构进行编码改良呢。"
					}),
					new Chat(16, 2, new string[2]
					{
						"别小瞧自己，瓦尔基莉。",
						"没受过高等培训的人是做不到你那种程度的。"
					}),
					new Chat(70, 1, new string[3]
					{
						"*叹气*",
						"真是对牛弹琴，也许把你的脑子喂给纳米",
						"瘟疫会更好。"
					}),
					new Chat(55, 1, new string[1]
					{
						"算了，你还是做回你的任务吧。"
					})
				});
				dialogList[4] = new Dialog(new Chat[4]
				{
					new Chat(18, 2, new string[1]
					{
						"感谢：援助很有必要。"
					}, "Ares_Request"),
					new Chat(25, 1, new string[1]
					{
						"我都让你小心点了！"
					}),
					new Chat(25, 1, new string[2]
					{
						"给你，这是我从那些受感染的机器中",
						"打捞出来的。"
					}),
					new Chat(25, 1, new string[1]
					{
						"应该比你手上那把豌豆射手要好用。"
					})
				});
			}
			else
			{
				dialogList = new Dialog[5];
				dialogList[0] = new Dialog(new Chat[8]
				{
					new Chat(55, 1, new string[2]
					{
						"卡森博士还活着，塔鲁斯！",
						"她真的还活着！"
					}),
					new Chat(55, 1, new string[2]
					{
						"虽然她在躲进军工厂时被大批安保机器人包围，",
						"不过她现在已经安全了。"
					}),
					new Chat(55, 1, new string[1]
					{
						"阿瑞斯正赶过去，不过他需要支援。"
					}),
					new Chat(21, 2, new string[2]
					{
						"错误必须由我来修正……",
						"……石板上的污渍必须被擦除……"
					}, "Tarus_Infected"),
					new Chat(71, 1, new string[1]
					{
						"塔鲁斯？"
					}),
					new Chat(71, 1, new string[2]
					{
						"……哔……啪……",
						"你能听到我说话吗？"
					}),
					new Chat(20, 2, new string[2]
					{
						"……",
						"抱歉，瓦尔基莉……军工厂是吧。"
					}, "Tarus_Talk"),
					new Chat(20, 2, new string[2]
					{
						"我知道了。",
						"我很快就会过去。"
					}, "Tarus_Confirm")
				});
				dialogList[1] = new Dialog(new Chat[0]);
				dialogList[2] = new Dialog(new Chat[1]
				{
					new Chat(20, 2, new string[1]
					{
						"啊啊……啊啊啊……"
					})
				});
				dialogList[3] = new Dialog(new Chat[8]
				{
					new Chat(25, 2, new string[2]
					{
						"// 应急系统已激活",
						"// 开始重启系统"
					}, "Tarus_Argue"),
					new Chat(25, 2, new string[3]
					{
						"// ……",
						"// …… …… ……",
						"// 诊断扫描已完成"
					}, "Tarus_Confirm"),
					new Chat(25, 2, new string[3]
					{
						"// …… ……",
						"// 装甲完整性检查完毕",
						"// 主副系统已上线"
					}, "Tarus_Confirm"),
					new Chat(25, 2, new string[1]
					{
						"我感觉……我感觉……"
					}),
					new Chat(27, 2, new string[1]
					{
						"全身都充满力量！！！"
					}, "Tarus_Infected"),
					new Chat(25, 2, new string[1]
					{
						"我的回路从没这么畅通过！"
					}),
					new Chat(25, 2, new string[2]
					{
						"这是怎么回事？！",
						"武器和动力系统都在超水平运作。"
					}),
					new Chat(27, 2, new string[2]
					{
						"不管这是什么，那些被灾创感染的叛徒都",
						"逃不出我的手心！"
					}, "Tarus_Talk")
				});
				dialogList[4] = new Dialog(new Chat[5]
				{
					new Chat(26, 2, new string[1]
					{
						"我不需要你的帮助！"
					}),
					new Chat(19, 1, new string[2]
					{
						"我们是搭档。",
						"相互帮助是理所当然的。"
					}),
					new Chat(19, 1, new string[1]
					{
						"发现：回收的武器与当前的机载系统不兼容。"
					}, "Ares_Request"),
					new Chat(19, 1, new string[1]
					{
						"图像表明它与塔鲁斯有92.3%的兼容性。"
					}),
					new Chat(25, 2, new string[2]
					{
						"我不需要你的武器。",
						"不过既然你坚持，我就收下吧。"
					}, "Tarus_Argue")
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
			AmbushGroup ambushGroup = new AmbushGroup(4);
			ambushGroup.SpareLife = 16;
			ambushGroup.SpawnGap = 0.4f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[0].SightSquared = 10000f;
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(-50f, 0f, 0f);
			ambushGroup.Enemies[0].FaceDirection = 1;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.5f, new Vector3(16f, 0f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[1].SightSquared = 10000f;
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(-50f, 50f, 0f);
			ambushGroup.Enemies[1].FaceDirection = 1;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.5f, new Vector3(-24f, 16f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[2].SightSquared = 10000f;
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(50f, 0f, 0f);
			ambushGroup.Enemies[2].FaceDirection = -1;
			ambushGroup.Enemies[2].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.5f, new Vector3(-16f, 0f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushGroup.Enemies[3] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[3].SightSquared = 10000f;
			ambushGroup.Enemies[3].SpawnPosition = new Vector3(50f, 50f, 0f);
			ambushGroup.Enemies[3].FaceDirection = -1;
			ambushGroup.Enemies[3].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.5f, new Vector3(24f, -16f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushSpawnerList[0].Groups[0] = ambushGroup;
			ambushSpawnerList[1] = new AmbushSpawner(1);
			ambushSpawnerList[1].OnStart = Ambush2Start;
			ambushSpawnerList[1].OnCancel = Ambush2Cancel;
			ambushSpawnerList[1].OnFinish = Ambush2Finish;
			ambushGroup = new AmbushGroup(3);
			ambushGroup.SpareLife = 10;
			ambushGroup.SpawnGap = 0.7f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[0].SightSquared = 10000f;
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(-48f, 48f, 0f);
			ambushGroup.Enemies[0].FaceDirection = 1;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(-32f, 0f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[1].SightSquared = 10000f;
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(-32f, -80f, 0f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(0f, -64f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[2].SightSquared = 10000f;
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(48f, 48f, 0f);
			ambushGroup.Enemies[2].FaceDirection = -1;
			ambushGroup.Enemies[2].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(16f, -16f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushSpawnerList[1].Groups[0] = ambushGroup;
			ambushSpawnerList[2] = new AmbushSpawner(1);
			ambushSpawnerList[2].OnStart = Ambush3Start;
			ambushSpawnerList[2].OnCancel = Ambush3Cancel;
			ambushSpawnerList[2].OnFinish = Ambush3Finish;
			ambushGroup = new AmbushGroup(3);
			ambushGroup.SpawnGap = 0.4f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[0].SightSquared = 10000f;
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(-50f, 16f, 0f);
			ambushGroup.Enemies[0].FaceDirection = 1;
			ambushGroup.Enemies[0].Life = 6;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.5f, new Vector3(-24f, 16f, 0f), new Vector3(-0.5f, -0.5f, 0f))
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.ZytronWalker);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(0f, 44f, 0f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Life = 2;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(0f, -12f, 0f), new Vector3(0f, -4f, 0f))
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[2].SightSquared = 10000f;
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(50f, 16f, 0f);
			ambushGroup.Enemies[2].FaceDirection = -1;
			ambushGroup.Enemies[2].Life = 6;
			ambushGroup.Enemies[2].Actions = new EnemyAction[2]
			{
				new MoveToAction(0.5f, new Vector3(24f, 16f, 0f), new Vector3(-0.5f, -0.5f, 0f)),
				new WaitAction(1f)
			};
			ambushSpawnerList[2].Groups[0] = ambushGroup;
		}

		private static void Ambush1Start()
		{
			ambush1Door.Activated = false;
		}

		private static void Ambush1Cancel()
		{
			ambush1Door.Activated = true;
		}

		private static void Ambush1Finish()
		{
			mReward[1] = 20;
			mReward[5] = 6;
			mReward[10] = 5;
			MaterialDropper.Instance.Start(180f, 180f, 626f, 634f, 0.1f, mReward, new Vector3(60f, 0f, 0f));
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
			mReward[1] = 20;
			mReward[5] = 6;
			mReward[10] = 5;
			MaterialDropper.Instance.Start(1488f, 1504f, 200f, 200f, 0.1f, mReward, Vector3.Zero);
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
			mReward[1] = 20;
			mReward[5] = 6;
			mReward[10] = 5;
			MaterialDropper.Instance.Start(2164f, 2164f, 580f, 588f, 0.1f, mReward, new Vector3(60f, 0f, 0f));
			ambush3Door.Activated = true;
		}

		public static void LivingQuarterOpening1()
		{
			World.Camera.Mode = CameraMode.Cinematic;
			World.Camera.Position = new Vector3(424f, 588f, 116f);
			World.Play(0, false);
			Director.WaitForSeconds(0.5f, LivingQuarterOpening2, false);
		}

		private static void LivingQuarterOpening2()
		{
			Narrator.Instance.Show("生活区\n米诺斯空间站内部", new Vector3(8f, -5f, -40f));
			World.Camera.TargetingTransform(new Vector3(360f, 588f, 116f), 6.25f, true);
			Director.WaitForSeconds(6f, LivingQuarterOpening3, false);
		}

		private static void LivingQuarterOpening3()
		{
			Director.FadeOut(0.25f);
			Director.WaitForSeconds(0.25f, LivingQuarterOpening4, false);
		}

		private static void LivingQuarterOpening4()
		{
			World.Camera.Position = new Vector3(104f, 600f, 87f);
			Director.FadeIn(0.25f);
			World.Camera.TargetingTransform(new Vector3(64f, 600f, 87f), 1.25f, true);
			Player.Instance.Spawn(new Vector3(0f, 592f, 0f));
			Director.WaitForSeconds(0.1f, LivingQuarterOpening4_1, false);
		}

		private static void LivingQuarterOpening4_1()
		{
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(64f, 592f, 0f),
				"run_idle"
			}, 0f, 1f, LivingQuarterOpening5);
		}

		private static void LivingQuarterOpening5()
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
			Director.WaitForSeconds(1f, ShowStartDialog, false);
		}

		private static void ShowStartDialog()
		{
			SFXManager.PlaySound("remind");
			World.ShowDialogByID(0, LivingQuarterOpening6);
		}

		private static void LivingQuarterOpening6()
		{
			World.Camera.EnterRoom(World.GetRoom(37), true, false, 0.5f);
			World.SaveCheckPoint(new Vector3(64f, 592f, 0f), 37, true, false, false, false, false, false);
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
			Player.Instance.ControlEnabled = true;
		}

		private static void LivingQuarterOpening7()
		{
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
			Player.Instance.ControlEnabled = true;
		}

		public static void ShockTutor()
		{
			if (!ProfileManager.Current.TutorialFan)
			{
				if (Player.Instance.Type == PlayerType.Mars)
				{
					HintUI.Instance.ShowHint(191, ShockChecker);
					HintUI.Instance.ShowHint(190, null);
				}
				else
				{
					HintUI.Instance.ShowHint(193, ShockChecker);
					HintUI.Instance.ShowHint(192, null);
				}
			}
		}

		private static void ShockChecker()
		{
			if (Player.Instance.Position.X > 112f)
			{
				ProfileManager.Current.TutorialFan = true;
				HintUI.Instance.HideHint();
				HintUI.Instance.HideHint();
				ProfileManager.BeginSave(null, 1, false);
			}
		}

		public static void ShieldTutor()
		{
			if (!ProfileManager.Current.TutorialShield)
			{
				if (Player.Instance.Type == PlayerType.Mars)
				{
					HintUI.Instance.ShowHint(188, ShieldChecker);
				}
				else
				{
					HintUI.Instance.ShowHint(189, ShieldChecker);
				}
			}
		}

		private static void ShieldChecker()
		{
			if (Player.Instance.Position.X > 1196f)
			{
				HintUI.Instance.HideHint();
				ProfileManager.Current.TutorialShield = true;
				ProfileManager.BeginSave(null, 1, false);
			}
		}

		public static void MiniBossLeviathan1()
		{
			if (Player.Instance.Position.X < 992f)
			{
				Player.Instance.Position.X = 992f;
			}
			Player.Instance.ControlEnabled = false;
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.FaceDirection = -1;
			Player.Instance.ChangeToUnStuckStage();
			BGMManager.FadeOff();
			Director.WaitForSeconds(0.8f, MiniBossLeviathan2, false);
		}

		private static void MiniBossLeviathan2()
		{
			EnemyManager.AcquireEnemy(EnemyType.Leviathan, new Vector3(928f, 384f, 0f), -1);
		}

		public static void MiniBossLeviathanFight()
		{
			if (BGMManager.mNowPlaying == 0)
			{
				BGMManager.FadeOff();
			}
			for (int i = 0; i < EnemyManager.mEnemiesInScene.Count; i++)
			{
				if (EnemyManager.mEnemiesInScene[i].Type == EnemyType.Leviathan)
				{
					return;
				}
			}
			EnemyManager.AcquireEnemy(EnemyType.Leviathan, new Vector3(928f, 384f, 0f), -1);
		}

		public static void ArmorDamaged()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				if (ProfileManager.Current.ArmorLevel == 0)
				{
					SFXManager.PlaySound("remind");
					World.ShowDialogByID(1, AfterArmorDamagedDialog);
					World.Camera.Mode = CameraMode.Cinematic;
				}
				else
				{
					AfterArmorDamagedDialog();
				}
			}
			else
			{
				AfterArmorDamagedDialog();
			}
		}

		public static void AfterArmorDamagedDialog()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			Player.Instance.ControlEnabled = true;
			MiniBossDoor.Activated = true;
		}

		public static void BeforeChangeArmorDialog()
		{
			if (ProfileManager.Current.ArmorLevel == 0)
			{
				if (Player.Instance.Type == PlayerType.Mars)
				{
					SFXManager.PlaySound("remind");
				}
				World.ShowDialogByID(2, EndBeforeChangeArmorDialog);
				World.Camera.Mode = CameraMode.Cinematic;
			}
			else
			{
				EndBeforeChangeArmorDialog();
			}
		}

		public static void EndBeforeChangeArmorDialog()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			Player.Instance.ControlEnabled = true;
		}

		public static void PrototypeSuit1()
		{
			if (ProfileManager.Current.ArmorLevel < 1)
			{
				World.Camera.Mode = CameraMode.Cinematic;
				ProfileManager.Current.ArmorLevel = 1;
				ProfileManager.Current.AcqiuredUpgrade[11] = 1;
				UnlockUI.Instance.Show(11);
				Director.WaitForSeconds(7f, AwardSuitAchievement, false);
				Director.WaitForSeconds(7.3f, AfterChangeArmorDialog, false);
			}
			else
			{
				Director.WaitForSeconds(0.3f, AfterChangeArmorDialogAndPrototypeSuit2Replay, false);
			}
		}

		private static void AwardSuitAchievement()
		{
			AchievementManager.Instance.Notify(9, 1);
		}

		public static void AfterChangeArmorDialog()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				SFXManager.PlaySound("remind");
			}
			World.ShowDialogByID(3, PrototypeSuit2);
		}

		private static void PrototypeSuit2()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			BGMManager.Play(0);
			HintUI.Instance.ShowHint(181, SuitChecker);
			HintUI.Instance.ShowHint(182, null);
			World.SaveCheckPoint(new Vector3(1760f, 740f, 0f));
			Player.Instance.ControlEnabled = true;
		}

		public static void AfterChangeArmorDialogAndPrototypeSuit2Replay()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			BGMManager.Play(0);
			World.SaveCheckPoint(new Vector3(1760f, 740f, 0f));
			Player.Instance.ControlEnabled = true;
		}

		private static void SuitChecker()
		{
			if (Player.Instance.Position.X > 1900f)
			{
				HintUI.Instance.HideHint();
				HintUI.Instance.HideHint();
			}
		}

		public static void RisingForm1()
		{
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(0.5f, RisingForm2, false);
		}

		private static void RisingForm2()
		{
			Player.Instance.SpriteRig.Transition("crouch_idle", 0.3f);
			Director.WaitForSeconds(2f, RisingForm3, false);
		}

		private static void RisingForm3()
		{
			SFXManager.PlaySound("New_TarusSpa");
			TimedEmitParticlePool.Summon("tarus_gas_transform", Player.Instance.Position, 4f);
			Director.WaitForSeconds(5f, RisingForm4, false);
		}

		private static void RisingForm4()
		{
			OnceEmitParticlePool.Emit(Player.Instance.Position + new Vector3(0f, 0f, 3f), "armor_upgrade_final");
			Player.Instance.UpgradeArmor();
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(4f, RisingForm5, false);
		}

		private static void RisingForm5()
		{
			ProfileManager.Current.ArmorLevel = 1;
			ProfileManager.Current.AcqiuredUpgrade[11] = 1;
			UnlockUI.Instance.Show(11);
			Director.WaitForSeconds(7f, AwardFormAchievement, false);
			Director.WaitForSeconds(7.3f, AfterChangeArmorDialogTarus, false);
		}

		private static void AwardFormAchievement()
		{
			AchievementManager.Instance.Notify(12, 1);
		}

		public static void AfterChangeArmorDialogTarus()
		{
			World.ShowDialogByID(3, RisingForm6);
		}

		private static void RisingForm6()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			BGMManager.Play(3);
			HintUI.Instance.ShowHint(171, SuitChecker);
			HintUI.Instance.ShowHint(172, null);
			World.SaveCheckPoint(new Vector3(1760f, 740f, 0f));
			Player.Instance.ControlEnabled = true;
		}

		public static void BossSentinel1()
		{
			Player.Instance.ControlEnabled = false;
			Player.Instance.FaceDirection = -1;
			Player.Instance.ChangeToUnStuckStage();
			World.Camera.Mode = CameraMode.Cinematic;
			BGMManager.FadeOff();
			Director.WaitForSeconds(0.6f, BossSentinel2, false);
		}

		private static void BossSentinel2()
		{
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.ControlEnabled = false;
			World.Camera.TargetingTransform(new Vector3(1880f, World.Camera.Y, 102f), 2f, true);
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(1896f, 112f, 0f),
				"run_idle"
			}, 0f, 2f, BossSentinel3);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void BossSentinel3()
		{
			Apollo.Instance.Dismiss();
			World.Camera.ClearDynamicBoundingFocusedRectangles();
			EnemyManager.AcquireEnemy(EnemyType.Sentinel, new Vector3(1832f, 120f, 0f), 1, false);
			new SpriteList();
			if (Player.Instance.Type == PlayerType.Mars)
			{
				(Player.Instance as Mars).StateMachine.ChangeState(MarsStand.Instance);
			}
			else if (Player.Instance.Type == PlayerType.Tarus)
			{
				(Player.Instance as Tarus).StateMachine.ChangeState(TarusStand.Instance);
			}
			SFXManager.StopLoopSound();
			Director.LightIn();
		}

		public static void AfterBossDeadDialog()
		{
			if (ProfileManager.Current.AcquiredWeapon[2] == 0)
			{
				ProfileManager.Current.AcquiredWeapon[2] = 1;
				ProfileManager.Current.AcqiuredUpgrade[2] = 1;
				ProfileManager.BeginSave(null, 1, false);
				UnlockUI.Instance.Show(2);
				Director.WaitForSeconds(7.3f, AfterBossDeadDialog2, false);
			}
			else
			{
				AfterBossDeadDialog2();
			}
		}

		public static void AfterBossDeadDialog2()
		{
			PlayScreen.Instance.CompleteLevel(false);
		}
	}
}
