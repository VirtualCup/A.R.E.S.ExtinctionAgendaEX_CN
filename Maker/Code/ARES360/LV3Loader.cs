using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.Screen;
using ARES360.UI;
using ARES360Loader;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ARES360
{
	public static class LV3Loader
	{
		public static JunkDropper BossJunkL;

		public static JunkDropper BossJunkR;

		private static UpgradeChip mShockChip;

		private static UpgradeChip mProtectionChip;

		public static BlastDoor BossDoor;

		public static BlastDoor MiniBossDoor;

		public static OneWayPlatform MiniBossElevator;

		public static void LoadEnemy()
		{
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.FlyingPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Turret);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronCrawler);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Bomber);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronWalker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronBooster);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Slither);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZypherPod);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShocker, 8);
			EnemyManager.CreateAndStoreEnemy(EnemyType.FlyingPod, 8);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Turret, 4);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronCrawler, 4);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Bomber, 7);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronWalker, 2);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Slither, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronBooster, 1);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZypherPod, 1);
		}

		public static void Destroy()
		{
			mShockChip = null;
			mProtectionChip = null;
			BossJunkL = null;
			BossJunkR = null;
			BossDoor = null;
			MiniBossDoor = null;
		}

		public static void LoadObject(out List<GameObject>[] objectInRoom)
		{
			mShockChip = new UpgradeChip(7, Vector3.Zero, true);
			mProtectionChip = new UpgradeChip(10, Vector3.Zero, true);
			objectInRoom = new List<GameObject>[28];
			objectInRoom[0] = new List<GameObject>();
			objectInRoom[1] = new List<GameObject>();
			objectInRoom[2] = new List<GameObject>(1);
			objectInRoom[2].Add(new BlastDoor(new Vector3(660f, 108f, 0f), false, 0, "Content/World/lv_sewer_gate"));
			objectInRoom[3] = new List<GameObject>(1);
			objectInRoom[3].Add(new BlastDoor(new Vector3(780f, 420f, 0f), true, 1, "Content/World/lv_sewer_gate"));
			objectInRoom[4] = new List<GameObject>();
			objectInRoom[5] = new List<GameObject>();
			objectInRoom[6] = new List<GameObject>();
			objectInRoom[7] = new List<GameObject>();
			objectInRoom[7].Add(new BlastDoor(new Vector3(996f, 108f, 0f), true, 1));
			BlastDoor blastDoor = new BlastDoor(new Vector3(1116f, 108f, 0f), true, 1);
			objectInRoom[7].Add(blastDoor);
			objectInRoom[7].Add(new RailSwitch(new Vector3(1104f, 128f, 0f), Vector3.Right, false, blastDoor));
			objectInRoom[8] = new List<GameObject>(2);
			blastDoor = new BlastDoor(new Vector3(1396f, 108f, 0f), false, 0);
			objectInRoom[8].Add(blastDoor);
			objectInRoom[8].Add(new RailSwitch(new Vector3(1386f, 136f, 0f), Vector3.Right, true, blastDoor));
			objectInRoom[9] = new List<GameObject>(2);
			blastDoor = new BlastDoor(new Vector3(1396f, 180f, 0f), false, 0);
			objectInRoom[9].Add(blastDoor);
			objectInRoom[9].Add(new RailSwitch(new Vector3(1568f, 200f, 0f), Vector3.Up, false, blastDoor));
			objectInRoom[10] = new List<GameObject>(3);
			objectInRoom[10].Add(new JunkDropper(new Vector3(1488f, 216f, 0f), 2.5f));
			blastDoor = new BlastDoor(new Vector3(1492f, 308f, 0f), false, 0);
			RailSwitch railSwitch = new RailSwitch(new Vector3(1568f, 344f, 0f), Vector3.Right, false, blastDoor);
			railSwitch.Tough = true;
			objectInRoom[10].Add(blastDoor);
			objectInRoom[10].Add(railSwitch);
			objectInRoom[11] = new List<GameObject>(2);
			objectInRoom[11].Add(new JunkDropper(new Vector3(1272f, 248f, 0f), 2.5f));
			objectInRoom[11].Add(new JunkDropper(new Vector3(1336f, 248f, 0f), 2.5f));
			objectInRoom[12] = new List<GameObject>(4);
			objectInRoom[12].Add(new JunkDropper(new Vector3(1024f, 344f, 0f), 2.5f));
			objectInRoom[12].Add(new JunkDropper(new Vector3(1088f, 344f, 0f), 2.5f));
			blastDoor = new BlastDoor(new Vector3(1148f, 308f, 0f), true, 1);
			objectInRoom[12].Add(blastDoor);
			objectInRoom[12].Add(new RailSwitch(new Vector3(944f, 256f, 0f), Vector3.Left, false, blastDoor));
			objectInRoom[13] = new List<GameObject>();
			objectInRoom[13].Add(new BlastDoor(new Vector3(1276f, 364f, 0f), true, 1, "Content/World/lv_sewer_gate"));
			objectInRoom[14] = new List<GameObject>(1);
			MiniBossElevator = new OneWayPlatform(new Vector2(16f, 4f), new Vector3(1304f, 348f, 0f), new Vector3(1304f, 556f, 0f), 26f, false, "Content/World/lv_sewer_elevator", new Vector3(0f, 4f, 0f));
			MiniBossElevator.Disabled = true;
			objectInRoom[14].Add(MiniBossElevator);
			objectInRoom[15] = new List<GameObject>(1);
			MiniBossDoor = new BlastDoor(new Vector3(1428f, 612f, 0f), true, 1, "Content/World/lv_sewer_gate");
			objectInRoom[15].Add(MiniBossDoor);
			objectInRoom[16] = new List<GameObject>(2);
			objectInRoom[16].Add(new ExplosiveBox(new Vector3(1590f, 610f, 0f), 3, false));
			objectInRoom[16].Add(new BlastDoor(new Vector3(1932f, 612f, 0f), true, 1, "Content/World/lv_sewer_gate"));
			objectInRoom[17] = new List<GameObject>(7);
			objectInRoom[17].Add(new JunkDropper(new Vector3(2056f, 696f, 0f), 2f));
			objectInRoom[17].Add(new JunkDropper(new Vector3(2120f, 712f, 0f), 2f));
			objectInRoom[17].Add(new JunkDropper(new Vector3(2184f, 728f, 0f), 2f));
			objectInRoom[17].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(2088f, 620f, 0f), new Vector3(2088f, 580f, 0f), 18f, 0f, "Content/World/lv_sewer_elevator", new Vector3(0f, 4f, 0f)));
			objectInRoom[17].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(2152f, 596f, 0f), new Vector3(2152f, 636f, 0f), 18f, 0f, "Content/World/lv_sewer_elevator", new Vector3(0f, 4f, 0f)));
			blastDoor = new BlastDoor(new Vector3(2412f, 676f, 0f), true, 1, "Content/World/lv_sewer_gate");
			objectInRoom[17].Add(blastDoor);
			objectInRoom[17].Add(new RailSwitch(new Vector3(2400f, 728f, 0f), Vector3.Up, false, blastDoor));
			objectInRoom[18] = new List<GameObject>(7);
			blastDoor = new BlastDoor(new Vector3(2564f, 660f, 0f), false, 0, "Content/World/lv_sewer_gate");
			objectInRoom[18].Add(blastDoor);
			objectInRoom[18].Add(new RailSwitch(new Vector3(2616f, 696f, 0f), Vector3.Right, false, blastDoor));
			blastDoor = new BlastDoor(new Vector3(2628f, 724f, 0f), false, 0, "Content/World/lv_sewer_gate");
			objectInRoom[18].Add(blastDoor);
			objectInRoom[18].Add(new RailSwitch(new Vector3(2616f, 680f, 0f), Vector3.Right, true, blastDoor));
			blastDoor = new BlastDoor(new Vector3(2444f, 948f, 0f), true, -1, "Content/World/lv_sewer_gate");
			objectInRoom[18].Add(blastDoor);
			objectInRoom[18].Add(new RailSwitch(new Vector3(2616f, 872f, 0f), Vector3.Right, false, blastDoor));
			objectInRoom[18].Add(new JunkDropper(new Vector3(2504f, 992f, 0f), 5f));
			objectInRoom[19] = new List<GameObject>(9);
			objectInRoom[19].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(2744f, 724f, 0f), new Vector3(2784f, 724f, 0f), 15f, 0f, "Content/World/lv_sewer_elevator", new Vector3(0f, 4f, 0f)));
			objectInRoom[19].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(2784f, 804f, 0f), new Vector3(2744f, 804f, 0f), 15f, 0f, "Content/World/lv_sewer_elevator", new Vector3(0f, 4f, 0f)));
			objectInRoom[19].Add(new RoundTripPlatform(new Vector2(16f, 4f), new Vector3(2856f, 788f, 0f), new Vector3(2856f, 740f, 0f), 18f, 0f, "Content/World/lv_sewer_elevator", new Vector3(0f, 4f, 0f)));
			blastDoor = new BlastDoor(new Vector3(2692f, 852f, 0f), false, 0, "Content/World/lv_sewer_gate");
			objectInRoom[19].Add(blastDoor);
			objectInRoom[19].Add(new RailSwitch(new Vector3(2896f, 856f, 0f), Vector3.Up, false, blastDoor));
			blastDoor = new BlastDoor(new Vector3(2628f, 948f, 0f), false, 0, "Content/World/lv_sewer_gate");
			objectInRoom[19].Add(blastDoor);
			objectInRoom[19].Add(new RailSwitch(new Vector3(2864f, 928f, 0f), Vector3.Right, true, blastDoor));
			objectInRoom[19].Add(new JunkDropper(new Vector3(2720f, 992f, 0f), 4.5f));
			objectInRoom[19].Add(new JunkDropper(new Vector3(2816f, 992f, 0f), 4.5f));
			objectInRoom[20] = new List<GameObject>(1);
			objectInRoom[20].Add(new OneWayPlatform(new Vector2(16f, 4f), new Vector3(2336f, 932f, 0f), new Vector3(2200f, 932f, 0f), 30f, false, "Content/World/lv_sewer_elevator", new Vector3(0f, 4f, 0f)));
			objectInRoom[21] = new List<GameObject>(2);
			objectInRoom[21].Add(new BlastDoor(new Vector3(2124f, 948f, 0f), true, -1, "Content/World/lv_sewer_gate"));
			objectInRoom[21].Add(new BlastDoor(new Vector3(1972f, 948f, 0f), true, -1, "Content/World/lv_sewer_gate"));
			objectInRoom[22] = new List<GameObject>(2);
			BossJunkL = new JunkDropper(new Vector3(1816f, 1048f, 0f), -1f);
			BossJunkR = new JunkDropper(new Vector3(1912f, 1048f, 0f), -1f);
			objectInRoom[22].Add(BossJunkL);
			objectInRoom[22].Add(BossJunkR);
			BossDoor = new BlastDoor(new Vector3(1756f, 948f, 0f), true, -1, "Content/World/lv_sewer_gate");
			objectInRoom[22].Add(BossDoor);
			objectInRoom[23] = new List<GameObject>();
			objectInRoom[24] = new List<GameObject>();
			objectInRoom[25] = new List<GameObject>(1);
			objectInRoom[25].Add(new StaticAttack(new Vector3(804f, 292f, 0f), 12f, 2f, 10, false, true, false, true));
			objectInRoom[26] = new List<GameObject>();
			objectInRoom[27] = new List<GameObject>();
		}

		public static void LoadDialog(out Dialog[] dialogList)
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				dialogList = new Dialog[2];
				dialogList[0] = new Dialog(new Chat[7]
				{
					new Chat(21, 1, new string[1]
					{
						"呃……啊……"
					}, "Tarus_Regret"),
					new Chat(10, 2, new string[2]
					{
						"我检测到你的机载系统有非常严重的",
						"损伤。"
					}, "Ares_Scan"),
					new Chat(23, 1, new string[2]
					{
						"……呃……啊……",
						"我、我没事。只是小擦伤而已。"
					}),
					new Chat(22, 1, new string[2]
					{
						"相比之下灾创病毒的影响远比我们",
						"想象的要严重。"
					}),
					new Chat(22, 1, new string[2]
					{
						"空间站里的机器人都被感染了……",
						"他们的数量实在太多了。"
					}),
					new Chat(10, 2, new string[1]
					{
						"询问：你需要帮助吗？"
					}, "Ares_Request"),
					new Chat(21, 1, new string[3]
					{
						"不需要。现在需要的是找到卡森博士。",
						"我会尽快与你会面的。",
						"塔鲁斯通话完毕。"
					}, "Tarus_Argue")
				});
				dialogList[1] = new Dialog(new Chat[10]
				{
					new Chat(61, 1, new string[1]
					{
						"装甲收集来的数据我已经分析完毕了。"
					}),
					new Chat(62, 1, new string[2]
					{
						"灾创并不是简单地覆盖主机程序；",
						"它还会重写程序，并且创造出新的行为逻辑。"
					}),
					new Chat(10, 2, new string[1]
					{
						"比如给维修无人机增加战斗功能。"
					}),
					new Chat(51, 1, new string[2]
					{
						"说得没错。",
						"所以你必须找到卡森博士，阿瑞斯。"
					}),
					new Chat(57, 1, new string[1]
					{
						"也许她能解释这一切。"
					}),
					new Chat(51, 1, new string[1]
					{
						"更重要的是，她能制止现在的问题。"
					}),
					new Chat(10, 2, new string[2]
					{
						"主要任务进行中。",
						"……"
					}, "Ares_Scan"),
					new Chat(11, 2, new string[1]
					{
						"阿瑞斯通话完毕？"
					}, "Ares_Illogical"),
					new Chat(55, 1, new string[1]
					{
						"“阿瑞斯通话完毕？”"
					}),
					new Chat(53, 1, new string[1]
					{
						"嗯……看来他跟塔鲁斯学坏了。"
					})
				});
			}
			else
			{
				dialogList = new Dialog[2];
				dialogList[0] = new Dialog(new Chat[9]
				{
					new Chat(63, 1, new string[2]
					{
						"你还好吗？我检测到你的载波信号出现",
						"了小幅度的波动。"
					}),
					new Chat(21, 2, new string[2]
					{
						"我没事。病毒的传染范围远比我们想象",
						"的要广。"
					}),
					new Chat(21, 2, new string[2]
					{
						"看来要完成任务，还得破坏不少空间站",
						"的卫兵才行。"
					}),
					new Chat(56, 1, new string[3]
					{
						"没事？！你看看自己，简直一团槽！",
						"我马上帮你进行远程诊断，",
						"检查下内部系统有没有损伤。"
					}),
					new Chat(21, 2, new string[2]
					{
						"不用！嗯，没有必要。",
						"你还有什么事情要跟我说吗？"
					}),
					new Chat(71, 1, new string[1]
					{
						"好吧，既然你都这么说了。"
					}),
					new Chat(71, 1, new string[3]
					{
						"我在空间站内网发现一段加密了的求",
						"救信号。",
						"我现在正尝试对它进行破译。"
					}),
					new Chat(23, 2, new string[2]
					{
						"你要是真有重要消息再联络我吧。",
						"塔鲁斯通话完毕。"
					}, "Tarus_Argue"),
					new Chat(70, 1, new string[2]
					{
						"天啊。",
						"他在搞什么？"
					})
				});
				dialogList[1] = new Dialog(new Chat[1]
				{
					new Chat(60, 1, new string[1]
					{
						"塔鲁斯！接信号，塔鲁斯！"
					})
				});
			}
		}

		public static void LoadAmbush(out AmbushSpawner[] ambushSpawnerList)
		{
			ambushSpawnerList = new AmbushSpawner[3];
		}

		public static void SewerOpening1()
		{
			World.Play(0, false);
			World.Camera.Mode = CameraMode.Cinematic;
			World.Camera.Position = new Vector3(128f, 232f, 80f);
			World.Camera.Velocity.Z = 5f;
			Director.WaitForSeconds(2.5f, SewerOpening2, false);
		}

		private static void SewerOpening2()
		{
			World.Camera.Velocity.Z = 0f;
			Player.Instance.Spawn(new Vector3(128f, 280f, 0f));
			Player.Instance.ControlEnabled = false;
			Player.Instance.StateMachineEnabled = true;
			Director.WaitForSeconds(1.5f, ShowBeginStageDialog, false);
		}

		private static void ShowBeginStageDialog()
		{
			SFXManager.PlaySound("remind");
			World.ShowDialogByID(0, SewerOpening3);
		}

		private static void SewerOpening3()
		{
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
			Player.Instance.ControlEnabled = true;
			World.Camera.EnterRoom(World.GetRoom(0), false, false, 0.5f);
			World.SaveCheckPoint(new Vector3(118f, 224f, 0f), 0, false, false, false, false, false, false);
		}

		public static void BoostChipTutor()
		{
			if (!ProfileManager.Current.TutorialBoost)
			{
				if (Player.Instance.Type == PlayerType.Mars)
				{
					HintUI.Instance.ShowHint(163, BoostChecker);
					int preferredDeviceFamily = GamePad.GetPreferredDeviceFamily();
					if (preferredDeviceFamily == 1)
					{
						HintUI.Instance.ShowHint(167, BoostChecker);
					}
					else
					{
						HintUI.Instance.ShowHint(166, BoostChecker);
					}
				}
				else if (Player.Instance.Type == PlayerType.Tarus)
				{
					int preferredDeviceFamily2 = GamePad.GetPreferredDeviceFamily();
					if (preferredDeviceFamily2 == 1)
					{
						HintUI.Instance.ShowHint(164, BoostChecker);
					}
					else
					{
						HintUI.Instance.ShowHint(165, BoostChecker);
					}
				}
			}
		}

		private static void BoostChecker()
		{
			if (Player.Instance.Position.X > 560f)
			{
				HintUI.Instance.HideHint();
				ProfileManager.Current.TutorialBoost = true;
				ProfileManager.BeginSave(null, 1, false);
			}
		}

		public static void Booster1()
		{
			Player.Instance.ControlEnabled = false;
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(0.5f, Booster2, false);
		}

		private static void Booster2()
		{
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.FaceDirection = 1;
			Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
			PlayerTweener playerTweener = Director.PlayerTweener;
			Player instance = Player.Instance;
			int[] propertyIndexes = new int[1];
			playerTweener.Tween(instance, propertyIndexes, new object[1]
			{
				new Vector3(1304f, Player.Instance.Position.Y, 0f)
			}, 0f, (1304f - Player.Instance.Position.X) / 50f, Booster3);
		}

		private static void Booster3()
		{
			Player.Instance.SpriteRig.SetPose("stand_idle", 0.0);
			Player.Instance.StateMachineEnabled = true;
			Director.WaitForSeconds(0.5f, Booster4, false);
		}

		private static void Booster4()
		{
			World.Camera.Shake(1, 1f);
			MiniBossElevator.Disabled = false;
			Director.WaitForSeconds(6f, Booster5, false);
		}

		private static void Booster5()
		{
			BGMManager.FadeOff();
			MiniBossDoor.Activated = false;
			Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.ZytronBooster, new Vector3(1384f, 696f, 0f), -1);
		}

		public static void FinishZytronBooster()
		{
			MiniBossDoor.Activated = true;
			BGMManager.Play(0);
			World.ChangeRoom(23);
			World.Camera.EnterRoom(World.GetRoom(23), true, false, 1f);
			Player.Instance.ControlEnabled = true;
			Director.ControlledEnemy = null;
		}

		public static void ExtraProtectionChip1AndFinishZytronBooster()
		{
			if (ProfileManager.Current.ProtectionLevel == 0)
			{
				BGMManager.FadeOff();
				Player.Instance.ControlEnabled = false;
				World.Camera.Mode = CameraMode.Cinematic;
				Director.WaitForSeconds(1f, ExtraProtectionChip2, false);
			}
		}

		private static void ExtraProtectionChip2()
		{
			Vector3 position = Player.Instance.Position;
			position.Y += 26f;
			mProtectionChip.Spawn(position);
			mProtectionChip.Velocity.Y = -4f;
			SFXManager.PlayLoopSound("charge", 4f, 0.75f);
			TimedEmitParticlePool.Summon("chip_unlock", mProtectionChip, 3f);
			Director.WaitForSeconds(5f, ExtraProtectionChip3, false);
		}

		private static void ExtraProtectionChip3()
		{
			ProfileManager.BeginSave(null, 1, false);
			SFXManager.PlaySound("remind");
			UnlockUI.Instance.Show(10);
			Player.Instance.IncreaseProtectionLevel();
			Director.WaitForSeconds(7.3f, ExtraProtectionChip4, false);
		}

		private static void ExtraProtectionChip4()
		{
			World.ShowDialogByID(1, FinishZytronBooster);
		}

		public static void BossZypherPod1()
		{
			BGMManager.FadeOff();
			BossDoor.Activated = false;
			Player.Instance.ControlEnabled = false;
			Player.Instance.FaceDirection = -1;
			Player.Instance.ChangeToUnStuckStage();
			World.Camera.TargetingTransform(new Vector3(1864f, World.Camera.Position.Y, World.Camera.Position.Z), 2f, true);
			Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.ZypherPod, new Vector3(1816f, 937f, 0f), -1);
			Director.WaitForSeconds(0.5f, BossZypherPod2, false);
		}

		private static void BossZypherPod2()
		{
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
			PlayerTweener playerTweener = Director.PlayerTweener;
			Player instance = Player.Instance;
			int[] propertyIndexes = new int[1];
			playerTweener.Tween(instance, propertyIndexes, new object[1]
			{
				new Vector3(1912f, Player.Instance.Position.Y, 0f)
			}, 0f, 1.2f, BossZypherPod3);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void BossZypherPod3()
		{
			Apollo.Instance.Dismiss();
			Director.ControlledEnemy.IsAIEnabled = true;
			Director.ControlledEnemy.SpriteRig.Transition("arm_swing_attack", 0.1f);
			Player.Instance.SpriteRig.SetPose("stand_idle", 0.0);
			SFXManager.StopLoopSound();
		}

		public static void ShockChip1()
		{
			BGMManager.FadeOff();
			Vector3 position = Player.Instance.Position;
			position.Y += 26f;
			mShockChip.Spawn(position);
			mShockChip.Velocity.Y = -4f;
			SFXManager.PlayLoopSound("charge", 4f, 0.75f);
			Director.WaitForSeconds(5f, ShockChip2, false);
			TimedEmitParticlePool.Summon("chip_unlock", mShockChip, 3f);
		}

		private static void ShockChip2()
		{
			ProfileManager.BeginSave(null, 1, false);
			StatusUI.Instance.RefreshSkillAvailability(2, true);
			UnlockUI.Instance.Show(7);
			Director.WaitForSeconds(7.3f, Ending1, false);
		}

		public static void Ending1()
		{
			BGMManager.FadeOff();
			BossDoor.Activated = true;
			if (World.Camera.Position.X > 1848f)
			{
				World.Camera.TargetingTransform(new Vector3(1848f, 964f, 145.5f), (World.Camera.Position.X - 1848f) / 30f, true);
			}
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.FaceDirection = -1;
			Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
			PlayerTweener playerTweener = Director.PlayerTweener;
			Player instance = Player.Instance;
			int[] propertyIndexes = new int[1];
			playerTweener.Tween(instance, propertyIndexes, new object[1]
			{
				new Vector3(1696f, Player.Instance.Position.Y, 0f)
			}, 0f, (Player.Instance.Position.X - 1696f) / 50f, Ending2);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void Ending2()
		{
			(ScreenManager.CurrentScreen as PlayScreen).CompleteLevel(false);
			SFXManager.StopLoopSound();
		}
	}
}
