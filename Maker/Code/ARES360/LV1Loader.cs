using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Entity.Action;
using ARES360.Event;
using ARES360.Input;
using ARES360.Profile;
using ARES360.Screen;
using ARES360.UI;
using ARES360Loader;
using FlatRedBall;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ARES360
{
	public static class LV1Loader
	{
		private static BlastDoor ambush1Door;

		private static BlastDoor ambush2Door;

		private static BlastDoor miniBossDoor;

		private static BlastDoor bossDoor;

		private static OneWayPlatform ambush3Platform;

		private static Sprite mCrackFloor;

		private static UpgradeChip mRepairChip;

		private static UpgradeChip mBlastChip;

		private static float mRepairTutorTime;

		private static float mSpecailTutorTime;

		private static bool mSecondRollTutor;

		private static bool mMidAirJumpTutor;

		private static Dictionary<int, int> mReward;

		public static void LoadEnemy()
		{
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Goliath);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.FlyingPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Turret);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronCrawler);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronWalkerBoss);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShocker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.FlyingPod, 8);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Turret, 8);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronCrawler, 8);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronWalkerBoss, 1);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Goliath, 1);
		}

		public static void Destroy()
		{
			SpriteManager.RemoveSpriteOneWay(mCrackFloor);
			ambush1Door = null;
			ambush2Door = null;
			miniBossDoor = null;
			bossDoor = null;
			ambush3Platform = null;
			mCrackFloor = null;
			mRepairChip = null;
			mBlastChip = null;
			mReward = null;
		}

		public static void LoadObject(out List<GameObject>[] objectInRoom)
		{
			mCrackFloor = new Sprite();
			mCrackFloor.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/lvl01_9", "PlayWorld");
			mCrackFloor.Position = new Vector3(80f, 120.1f, -1f);
			mCrackFloor.RotationX = 1.5708f;
			mCrackFloor.ScaleX = 28f;
			mCrackFloor.ScaleY = 28f;
			mRepairChip = new UpgradeChip(5, Vector3.Zero, true);
			mBlastChip = new UpgradeChip(6, Vector3.Zero, true);
			mReward = new Dictionary<int, int>(3);
			objectInRoom = new List<GameObject>[21];
			objectInRoom[0] = new List<GameObject>();
			objectInRoom[1] = new List<GameObject>(1);
			objectInRoom[1].Add(new BlastDoor(new Vector3(476f, 140f, 0f), true, 1));
			objectInRoom[2] = new List<GameObject>(1);
			ambush1Door = new BlastDoor(new Vector3(596f, 140f, 0f), true, 1);
			objectInRoom[2].Add(ambush1Door);
			objectInRoom[3] = new List<GameObject>();
			objectInRoom[3].Add(new BlastDoor(new Vector3(812f, 140f, 0f), true, 1));
			objectInRoom[4] = new List<GameObject>();
			objectInRoom[5] = new List<GameObject>(3);
			ambush2Door = new BlastDoor(new Vector3(1132f, 44f, 0f), false, 0);
			objectInRoom[5].Add(ambush2Door);
			objectInRoom[5].Add(new PlayerBlocker(new Vector3(1012f, 44f, 0f), 3f, 12f));
			objectInRoom[5].Add(new StaticAttack(new Vector3(1012f, 44f, 0f), 4f, 12f, 10, false, true, true));
			objectInRoom[6] = new List<GameObject>(1);
			CrackedBox crackedBox = new CrackedBox(new Vector3(928f, 40f, 0f), 2, false);
			crackedBox.HiddenGameObject = World.DataCube[2];
			objectInRoom[6].Add(crackedBox);
			objectInRoom[7] = new List<GameObject>(3);
			objectInRoom[7].Add(new BlastDoor(new Vector3(1524f, 140f, 0f), true, 1));
			ambush3Platform = new OneWayPlatform(new Vector2(16f, 4f), new Vector3(1584f, 124f, 0f), new Vector3(1584f, 24f, 0f), 250f, true, "Content/World/m_lv_platform4x1", Vector3.Zero);
			ambush3Platform.Disabled = true;
			objectInRoom[7].Add(ambush3Platform);
			ModelObject modelObject = new ModelObject(new Vector3(1552f, 124f, 0f), Vector3.Zero, 16f, 4f, false, "Content/World/m_lv_platform4x1", Vector3.Zero, Vector3.One);
			ambush3Platform.ActivateRadius = 52f;
			ambush3Platform.AddLoadedObject(modelObject);
			objectInRoom[7].Add(modelObject);
			objectInRoom[8] = new List<GameObject>(2);
			objectInRoom[8].Add(new BlastDoor(new Vector3(1612f, 28f, 0f), true, 1));
			miniBossDoor = new BlastDoor(new Vector3(1748f, 60f, 0f), true, 1);
			objectInRoom[8].Add(miniBossDoor);
			objectInRoom[9] = new List<GameObject>(1);
			objectInRoom[9].Add(new BlastDoor(new Vector3(1700f, 172f, 0f), true, 1));
			objectInRoom[10] = new List<GameObject>();
			objectInRoom[11] = new List<GameObject>(2);
			objectInRoom[11].Add(new BlastDoor(new Vector3(2340f, 28f, 0f), true, 1));
			objectInRoom[11].Add(new BlastDoor(new Vector3(2524f, 28f, 0f), true, 1));
			objectInRoom[12] = new List<GameObject>(1);
			bossDoor = new BlastDoor(new Vector3(2708f, 28f, 0f), true, 1);
			objectInRoom[12].Add(bossDoor);
			objectInRoom[13] = new List<GameObject>(1);
			objectInRoom[13].Add(new OneWayPlatform(new Vector2(16f, 4f), new Vector3(2288f, 156f, 0f), new Vector3(2288f, 12f, 0f), 20f, false, "Content/World/m_lv1_elevator_4", new Vector3(0f, 4f, -12f)));
			objectInRoom[14] = new List<GameObject>();
			objectInRoom[15] = new List<GameObject>(1);
			objectInRoom[15].Add(new GasTank(3, new Vector3(1716f, 236f, 0f)));
			objectInRoom[16] = new List<GameObject>();
			objectInRoom[17] = new List<GameObject>();
			objectInRoom[18] = new List<GameObject>();
			objectInRoom[19] = new List<GameObject>();
			objectInRoom[20] = new List<GameObject>();
		}

		public static void LoadDialog(out Dialog[] dialogList)
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				dialogList = new Dialog[3];
				dialogList[0] = new Dialog(new Chat[2]
				{
					new Chat(51, 1, new string[2]
					{
						"还是没有卡森博士和其他人的消息，",
						"反倒是你北边有很多杂乱的信号。"
					}),
					new Chat(61, 1, new string[1]
					{
						"看来这里除了你还有其它家伙……"
					})
				});
				dialogList[1] = new Dialog(new Chat[7]
				{
					new Chat(52, 1, new string[2]
					{
						"哇！",
						"那是什么？！"
					}),
					new Chat(10, 2, new string[2]
					{
						"名称：WKR-7750-3LTT",
						"机能：移动修理"
					}, "Ares_Scan"),
					new Chat(10, 2, new string[2]
					{
						"名称：STR-8391-5KIL",
						"机能：移动修理"
					}),
					new Chat(10, 2, new string[2]
					{
						"从扫描结果看，它是由多种单位重新组装而成",
						"的机器人。"
					}),
					new Chat(60, 1, new string[1]
					{
						"机器人可不会咆哮！"
					}),
					new Chat(14, 2, new string[2]
					{
						"你说的对。用最大概率算法得到的结论是，有97.34%",
						"的可能性是灾创病毒入侵了机器人的基础功能。"
					}, "Ares_Request"),
					new Chat(71, 1, new string[2]
					{
						"看来情况不是很乐观……",
						"万事小心，阿瑞斯。"
					})
				});
				dialogList[2] = new Dialog(new Chat[4]
				{
					new Chat(55, 1, new string[1]
					{
						"干的不错！"
					}),
					new Chat(61, 1, new string[1]
					{
						"看来灾创病毒的危害远比我们想象的要严重……"
					}),
					new Chat(65, 1, new string[2]
					{
						"我正在分析你装甲传回来的战斗遥测数据",
						"不过这需要一些时间。"
					}),
					new Chat(65, 1, new string[1]
					{
						"在这期间，请继续寻找卡森博士的下落。"
					})
				});
			}
			else
			{
				dialogList = new Dialog[3];
				dialogList[0] = new Dialog(new Chat[2]
				{
					new Chat(61, 1, new string[1]
					{
						"塔鲁斯，我们时间不多了。"
					}),
					new Chat(61, 1, new string[2]
					{
						"得赶快到约定的地点。",
						"我会对样品和数据进行调查。"
					})
				});
				dialogList[1] = new Dialog(new Chat[4]
				{
					new Chat(20, 2, new string[2]
					{
						"那不是正常的机器人。",
						"瓦尔基莉，你有那东西的情报吗？"
					}, "Tarus_Confirm"),
					new Chat(62, 1, new string[2]
					{
						"很明显是灾创病毒篡改了它的程序。",
						"不过具体情况还需要更多数据来分析。"
					}),
					new Chat(22, 2, new string[2]
					{
						"有必要的话，我会把空间站里所有机器人",
						"的数据都带给你的。"
					}),
					new Chat(22, 2, new string[1]
					{
						"塔鲁斯通话完毕。"
					}, "Tarus_Argue")
				});
				dialogList[2] = new Dialog(new Chat[2]
				{
					new Chat(65, 1, new string[2]
					{
						"把它收好，塔鲁斯。",
						"这份数据非常珍贵。"
					}),
					new Chat(65, 1, new string[2]
					{
						"我已经帮你找到前往回收厂的捷径了。",
						"去搭前面的升降机吧。"
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
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.PodShocker);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(-44f, 5f, 0f);
			ambushGroup.Enemies[0].FaceDirection = 1;
			ambushGroup.Enemies[0].Actions = new EnemyAction[5]
			{
				new WaitAction(0.4f),
				new WalkAction(1.4f, 1),
				new WaitAction(1f),
				new WalkAction(1.4f, 1),
				new WaitAction(1.5f)
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.PodShocker);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(44f, 5f, 0f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Actions = new EnemyAction[5]
			{
				new WaitAction(0.4f),
				new WalkAction(1.4f, -1),
				new WaitAction(1f),
				new WalkAction(1.4f, -1),
				new WaitAction(1.5f)
			};
			ambushSpawnerList[0].Groups[0] = ambushGroup;
			ambushSpawnerList[1] = new AmbushSpawner(1);
			ambushSpawnerList[1].OnStart = Ambush2Start;
			ambushSpawnerList[1].OnCancel = Ambush2Cancel;
			ambushSpawnerList[1].OnFinish = Ambush2Finish;
			ambushGroup = new AmbushGroup(2);
			ambushGroup.SpawnGap = 0f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.FlyingPod);
			ambushGroup.Enemies[0].SpawnPosition = Vector3.Zero;
			ambushGroup.Enemies[0].FaceDirection = -1;
			ambushGroup.Enemies[0].Actions = new EnemyAction[2]
			{
				new MoveToAction(1f, new Vector3(-24f, -32f, 0f), new Vector3(-0.8f, -0.8f, 0f)),
				new TurnToAction(1)
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.FlyingPod);
			ambushGroup.Enemies[1].SpawnPosition = Vector3.Zero;
			ambushGroup.Enemies[1].FaceDirection = 1;
			ambushGroup.Enemies[1].Actions = new EnemyAction[2]
			{
				new MoveToAction(1f, new Vector3(24f, -32f, 0f), new Vector3(-0.8f, -0.8f, 0f)),
				new TurnToAction(-1)
			};
			ambushSpawnerList[1].Groups[0] = ambushGroup;
			ambushSpawnerList[2] = new AmbushSpawner(1);
			ambushSpawnerList[2].OnFinish = Ambush3Finish;
			ambushGroup = new AmbushGroup(4);
			ambushGroup.SpawnGap = 0f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.FlyingPod);
			ambushGroup.Enemies[0].SpawnPosition = Vector3.Zero;
			ambushGroup.Enemies[0].FaceDirection = -1;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(-16f, -40f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.FlyingPod);
			ambushGroup.Enemies[1].SpawnPosition = Vector3.Zero;
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(-24f, -32f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.FlyingPod);
			ambushGroup.Enemies[2].SpawnPosition = Vector3.Zero;
			ambushGroup.Enemies[2].FaceDirection = -1;
			ambushGroup.Enemies[2].Actions = new EnemyAction[1]
			{
				new MoveToAction(1f, new Vector3(-8f, -48f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[3] = new AmbushEnemy(EnemyType.PodShocker);
			ambushGroup.Enemies[3].SpawnPosition = Vector3.Zero;
			ambushGroup.Enemies[3].FaceDirection = -1;
			ambushGroup.Enemies[3].Actions = new EnemyAction[2]
			{
				new WaitAction(1f),
				new WalkAction(1f, -1)
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
			ambush1Door.Activated = true;
			mReward[1] = 25;
			mReward[5] = 6;
			MaterialDropper.Instance.Start(528f, 544f, 176f, 176f, 0.1f, mReward, Vector3.Zero);
		}

		private static void Ambush2Start()
		{
			if (ProfileManager.Current.ArmorLevel > 0)
			{
				EnemyManager.CancelAmbush();
			}
			else
			{
				ambush2Door.Activated = false;
			}
		}

		private static void Ambush2Cancel()
		{
			ambush2Door.Activated = true;
		}

		private static void Ambush2Finish()
		{
			ambush2Door.Activated = true;
			mReward[1] = 25;
			mReward[5] = 6;
			MaterialDropper.Instance.Start(1064f, 1080f, 88f, 88f, 0.1f, mReward, Vector3.Zero);
		}

		private static void Ambush3Finish()
		{
			ambush3Platform.Disabled = false;
			OnceEmitParticlePool.Emit(new Vector3(1536f, 124f, 0f), "explosion");
			OnceEmitParticlePool.Emit(new Vector3(1600f, 124f, 0f), "explosion");
		}

		public static void JunkyardOpening1()
		{
			EnemyManager.IsActive = false;
			World.LoadRoomByID(3, false);
			World.LoadRoomByID(18, false);
			World.Camera.Mode = CameraMode.Cinematic;
			World.Camera.Position = new Vector3(968f, 170f, 140f);
			World.Camera.Velocity.X = -5f;
			Director.WaitForSeconds(1.72f, JunkyardOpening1_1, false);
		}

		public static void JunkyardOpening1_1()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				BGMManager.Play(0);
			}
			Director.WaitForSeconds(1.28f, JunkyardOpening1_2, false);
		}

		public static void JunkyardOpening1_2()
		{
			Narrator.Instance.Show("米诺斯空间站\n六小时后", new Vector3(8f, -5f, -40f));
			Director.WaitForSeconds(5.5f, JunkyardOpening2, false);
		}

		public static void JunkyardOpening2()
		{
			Director.FadeOut(0.25f);
			Director.WaitForSeconds(0.25f, JunkyardOpening4, false);
		}

		public static void JunkyardOpening4()
		{
			if (Player.Instance.Type == PlayerType.Tarus)
			{
				BGMManager.Play(2);
			}
			World.UnLoadRoomByID(3, false);
			World.UnLoadRoomByID(18, false);
			World.Play(0, false, false, false, false);
			World.Camera.Position = new Vector3(80f, 150f, 80f);
			World.Camera.Velocity.X = 0f;
			World.Camera.Velocity.Z = 5f;
			Director.FadeIn(0.1f);
			Director.WaitForSeconds(1f, JunkyardOpening5, false);
		}

		public static void JunkyardOpening5()
		{
			Player.Instance.Spawn(new Vector3(80f, 196f, 0f));
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.ControlEnabled = false;
			Player.Instance.AffectedByGravity = false;
			Player.Instance.Velocity = Vector3.Zero;
			Player.Instance.Acceleration = Vector3.Zero;
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(80f, 128f, 0f),
				"fall"
			}, 0f, 0.5f, JunkyardOpening6);
		}

		public static void JunkyardOpening6()
		{
			SpriteManager.AddSprite(mCrackFloor);
			mCrackFloor.Alpha = 0f;
			mCrackFloor.AlphaRate = 4f;
			OnceEmitParticlePool.Emit(new Vector3(80f, 120.2f, 0f), "ares_landing_impact");
			SFXManager.PlaySound("crash");
			World.Camera.Velocity.Z = 0f;
			World.Camera.Shake(3, 0.25f);
			Player.Instance.Position = new Vector3(80f, 128f, 0f);
			Director.PlayerTweener.Tween(Player.Instance, new int[1]
			{
				2
			}, new object[1]
			{
				"crouch_idle"
			}, 0f, 1f, JunkyardOpening7);
		}

		public static void JunkyardOpening7()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				((Mars)Player.Instance).StateMachine.ChangeState(MarsCrouch.Instance);
			}
			else if (Player.Instance.Type == PlayerType.Tarus)
			{
				((Tarus)Player.Instance).StateMachine.ChangeState(TarusCrouch.Instance);
			}
			SFXManager.PlaySound("remind");
			World.Camera.Mode = CameraMode.Cinematic;
			World.ShowDialogByID(0, AfterDialog0);
		}

		public static void AfterDialog0()
		{
			EnemyManager.IsActive = true;
			Player.Instance.StateMachineEnabled = true;
			Player.Instance.ControlEnabled = true;
			Player.Instance.AffectedByGravity = true;
			World.Camera.Mode = CameraMode.SideScrolling;
			World.SaveCheckPoint(new Vector3(80f, 140f, 0f), 0, false, false, false, false, false, false);
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
			DataCube.Acquire(2);
			if (!ProfileManager.Current.TutorialBasic)
			{
				Director.WaitForSeconds(0.5f, BasicTutor, false);
			}
		}

		public static void BasicTutor()
		{
			switch (GamePad.GetPreferredDeviceFamily())
			{
			case 2:
				HintUI.Instance.ShowHint(176, MoveChecker);
				break;
			case 3:
				HintUI.Instance.ShowHint(177, MoveChecker);
				break;
			default:
				HintUI.Instance.ShowHint(178, MoveChecker);
				break;
			}
			HintUI.Instance.ShowHint(173, null);
		}

		private static void MoveChecker()
		{
			if (Player.Instance.Position.X > 128f && Player.Instance.Position.Y < 136f)
			{
				HintUI.Instance.HideHint();
				HintUI.Instance.HideHint();
				Director.WaitForSeconds(0.2f, RollDashTutor, false);
			}
		}

		private static void RollDashTutor()
		{
			mSecondRollTutor = false;
			mMidAirJumpTutor = false;
			if (Player.Instance.Type == PlayerType.Mars)
			{
				HintUI.Instance.ShowHint(179, RollDashChecker);
			}
			else
			{
				HintUI.Instance.ShowHint(180, RollDashChecker);
			}
		}

		private static void RollDashChecker()
		{
			if (Player.Instance.Position.X > 168f)
			{
				HintUI.Instance.HideHint();
				Director.WaitForSeconds(0.2f, AttackTutor, false);
			}
		}

		private static void AttackTutor()
		{
			switch (GamePad.GetPreferredDeviceFamily())
			{
			case 2:
				HintUI.Instance.ShowHint(158, AttackChecker);
				break;
			case 3:
				HintUI.Instance.ShowHint(159, AttackChecker);
				break;
			default:
				HintUI.Instance.ShowHint(160, AttackChecker);
				break;
			}
		}

		private static void AttackChecker()
		{
			if (!mSecondRollTutor && Player.Instance.Position.X > 280f)
			{
				if (Player.Instance.Type == PlayerType.Mars)
				{
					HintUI.Instance.ShowHint(179, RollDashChecker2);
				}
				else
				{
					HintUI.Instance.ShowHint(180, RollDashChecker2);
				}
			}
			else if (!mMidAirJumpTutor && Player.Instance.Position.X > 360f)
			{
				HintUI.Instance.ShowHint(175, MidAirJumpChecker);
			}
			else if (Player.Instance.Position.X > 600f)
			{
				ProfileManager.Current.TutorialBasic = true;
				HintUI.Instance.HideHint();
			}
		}

		private static void RollDashChecker2()
		{
			if (Player.Instance.Position.X > 336f)
			{
				mSecondRollTutor = true;
				HintUI.Instance.HideHint();
			}
		}

		private static void MidAirJumpChecker()
		{
			if (Player.Instance.Position.X > 456f)
			{
				mMidAirJumpTutor = true;
				HintUI.Instance.HideHint();
			}
		}

		public static void SpecialTutor()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				HintUI.Instance.ShowHint(194, SpecialChecker);
			}
			else
			{
				HintUI.Instance.ShowHint(195, SpecialChecker);
			}
			HintUI.Instance.ShowHint(196, null);
			mSpecailTutorTime = 10f;
		}

		private static void SpecialChecker()
		{
			mSpecailTutorTime -= TimeManager.SecondDifference;
			if (GamePad.GetKeyDown(2048) || mSpecailTutorTime <= 0f)
			{
				HintUI.Instance.HideHint();
				HintUI.Instance.HideHint();
			}
		}

		public static void MiniBossZytron1()
		{
			miniBossDoor.Activated = false;
			Player.Instance.ControlEnabled = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Player.Instance.ChangeToUnStuckStage();
			Player.Instance.ForceIdle();
			Director.WaitForSeconds(0.5f, MiniBossZytron2, false);
		}

		public static void MiniBossZytron2()
		{
			World.Camera.TargetingTransform(new Vector3(1704f, 32f, 80f), 2.5f, true);
			Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.ZytronWalkerBoss, new Vector3(1752f, 32f, 0f), -1);
			Director.ControlledEnemy.IsAIEnabled = false;
			Director.ControlledEnemy.SpriteRig.SetPose("walk", 0.0);
			Director.ControlledEnemy.SpriteRig.Cycle = true;
			Director.ControlledEnemy.IsAllowRemovingByEnemyGarbageCollection = false;
			EnemyTweener enemyTweener = Director.EnemyTweener;
			Enemy controlledEnemy = Director.ControlledEnemy;
			int[] propertyIndexes = new int[1];
			enemyTweener.Tween(controlledEnemy, propertyIndexes, new object[1]
			{
				new Vector3(1728f, 32f, 0f)
			}, 0f, 2.5f, MiniBossZytron3);
			SFXManager.PlayLoopSound("zytron_walk", 2.88f, 0.72f);
		}

		public static void MiniBossZytron3()
		{
			SFXManager.PlaySound("miniboss_scream");
			World.Camera.Mode = CameraMode.Cinematic;
			Director.ControlledEnemy.SpriteRig.SetPose("taunt", 0.0);
			Director.WaitForSeconds(0.4f, MiniBossZytron4, false);
		}

		public static void MiniBossZytron4()
		{
			World.Camera.Shake(2, 0.8f);
			Director.WaitForSeconds(0.7f, MiniBossZytron4B, false);
		}

		public static void MiniBossZytron4B()
		{
			Director.EnemyTweener.Tween(Director.ControlledEnemy, new int[1]
			{
				1
			}, new object[1]
			{
				"idle"
			}, 0f, 1.8f, MiniBossZytron5);
		}

		public static void MiniBossZytron5()
		{
			World.Camera.EnterRoom(World.GetRoom(8), true, false, 0.5f);
			World.Camera.TransformToSideScrollingMode(0.3f, 102f);
			Player.Instance.ControlEnabled = true;
			Director.ControlledEnemy.IsAIEnabled = true;
		}

		public static void RepairChip1()
		{
			miniBossDoor.Activated = true;
			if (ProfileManager.Current.RepairLevel == 0)
			{
				BGMManager.FadeOff();
				Director.WaitForSeconds(1f, RepairChip2, false);
			}
			else
			{
				Player.Instance.ControlEnabled = true;
			}
		}

		public static void RepairChip2()
		{
			Vector3 position = Player.Instance.Position;
			position.Y += 26f;
			mRepairChip.Spawn(position);
			mRepairChip.Velocity.Y = -4f;
			SFXManager.PlayLoopSound("charge", 4f, 0.75f);
			Director.WaitForSeconds(5f, RepairChip3, false);
			TimedEmitParticlePool.Summon("chip_unlock", mRepairChip, 3f);
		}

		public static void RepairChip3()
		{
			ProfileManager.BeginSave(null, 1, false);
			StatusUI.Instance.RefreshSkillAvailability(0, true);
			SFXManager.PlaySound("remind");
			World.ShowDialogByID(1, RepairChip4);
		}

		public static void RepairChip4()
		{
			StatusUI.Instance.RefreshSkillAvailability(0, true);
			UnlockUI.Instance.Show(5);
			Director.WaitForSeconds(7.3f, RepairChipTutor, false);
		}

		public static void RepairChipTutor()
		{
			HintUI.Instance.ShowHint(184, RepairChecker);
			HintUI.Instance.ShowHint(185, null);
			mRepairTutorTime = 10f;
			Player.Instance.ControlEnabled = true;
			BGMManager.Play((Player.Instance.Type != 0) ? 2 : 0);
			World.Camera.Mode = CameraMode.SideScrolling;
		}

		private static void RepairChecker()
		{
			mRepairTutorTime -= TimeManager.SecondDifference;
			if (GamePad.GetKeyDown(4096) || mRepairTutorTime <= 0f)
			{
				HintUI.Instance.HideHint();
				HintUI.Instance.HideHint();
			}
		}

		public static void BossGoliath1()
		{
			BGMManager.FadeOff();
			bossDoor.Activated = false;
			Director.ControlledEnemy = EnemyManager.AcquireEnemy(EnemyType.Goliath, new Vector3(2616f, 88f, 0f), -1, false);
			Director.ControlledEnemy.IsAllowRemovingByEnemyGarbageCollection = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Player.Instance.ControlEnabled = false;
			Player.Instance.ForceIdle();
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(0.5f, BossGoliath2, false);
		}

		public static void BossGoliath2()
		{
			World.Camera.TargetingTransform(new Vector3(2616f, 56f, 145.5f), 2.1f, true);
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
			PlayerTweener playerTweener = Director.PlayerTweener;
			Player instance = Player.Instance;
			int[] propertyIndexes = new int[1];
			playerTweener.Tween(instance, propertyIndexes, new object[1]
			{
				new Vector3(2616f, 24f, 0f)
			}, 0f, 2f, BossGoliath3);
			SFXManager.PlayLoopSound("run", 2.4f, 0.4f);
		}

		public static void BossGoliath3()
		{
			Apollo.Instance.Dismiss();
			Player.Instance.SpriteRig.SetPose("stand_idle", 0.0);
			Player.Instance.ControlEnabled = true;
			Player.Instance.StateMachineEnabled = true;
			World.Camera.EnterRoom(World.GetRoom(12), false, false, 0.1f);
			(Director.ControlledEnemy as Goliath).ActivateTurrets();
			BossUI.Instance.__setBoss__(Director.ControlledEnemy);
			Director.ControlledEnemy = null;
			BGMManager.Play(1);
		}

		public static void Ending1()
		{
			bossDoor.Activated = true;
			if (ProfileManager.Current.BlastLevel == 0)
			{
				Director.WaitForSeconds(5f, BlastChip1, false);
			}
			else
			{
				Director.WaitForSeconds(5f, Ending2, false);
			}
		}

		public static void EndingDialog()
		{
			Player.Instance.ControlEnabled = false;
			World.Camera.Mode = CameraMode.Cinematic;
			SFXManager.PlaySound("remind");
			World.ShowDialogByID(2, Ending2);
		}

		public static void Ending2()
		{
			bossDoor.Activated = true;
			Player.Instance.FaceDirection = 1;
			Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
			PlayerTweener playerTweener = Director.PlayerTweener;
			Player instance = Player.Instance;
			int[] propertyIndexes = new int[1];
			playerTweener.Tween(instance, propertyIndexes, new object[1]
			{
				new Vector3(2752f, 24f, 0f)
			}, 0f, (2752f - Player.Instance.Position.X) / 40f + 1f, Ending3);
			SFXManager.PlayLoopSound("run", 999f, 0.4f);
		}

		public static void Ending3()
		{
			SFXManager.StopLoopSound();
			(ScreenManager.CurrentScreen as PlayScreen).CompleteLevel(false);
		}

		public static void BlastChip1()
		{
			World.Camera.Mode = CameraMode.Cinematic;
			Vector3 position = Player.Instance.Position;
			position.Y += 26f;
			mBlastChip.Spawn(position);
			mBlastChip.Velocity.Y = -4f;
			SFXManager.PlayLoopSound("charge", 4f, 0.75f);
			Director.WaitForSeconds(5f, BlastChip2, false);
			TimedEmitParticlePool.Summon("chip_unlock", mBlastChip, 3f);
		}

		public static void BlastChip2()
		{
			ProfileManager.BeginSave(null, 1, false);
			StatusUI.Instance.RefreshSkillAvailability(1, true);
			UnlockUI.Instance.Show(6);
			Director.WaitForSeconds(7.3f, EndingDialog, false);
		}
	}
}
