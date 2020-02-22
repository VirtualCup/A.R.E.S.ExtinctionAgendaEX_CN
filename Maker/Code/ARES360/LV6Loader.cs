using ARES360.Audio;
using ARES360.Effect;
using ARES360.Entity;
using ARES360.Entity.Action;
using ARES360.Event;
using ARES360.Profile;
using ARES360.Screen;
using ARES360.UI;
using ARES360Loader;
using ARES360Loader.Data.Entity;
using FlatRedBall;
using FlatRedBall.ManagedSpriteGroups;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ARES360
{
	public static class LV6Loader
	{
		public static OneWayPlatform mMiniBossPlatform;

		public static ZAxisBlastDoor BossDoor;

		private static SpriteRigObject mOpenSceneZytron;

		private static ZAxisBlastDoor mOpenSceneDoor;

		private static ModelObject mTram;

		private static ModelObject mTramDoor;

		private static ZAxisBlastDoor mAmbush1Door;

		private static ZAxisBlastDoor mAmbush2Door;

		private static Dictionary<int, int> mReward;

		private static ARES360.Entity.PrimeGuardian mPrimeGuardian;

		private static TimedEmitParticle[] mZytronGas;

		private static UpgradeChip mPhotonLauncherChip;

		private static UpgradeChip mBlastUpgradeChip;

		private static Sprite mPhotonLauncherSprite;

		private static CharacterBlocker mMK2Ceil;

		private static Sprite mZytronPic;

		private static SpriteRig mZytronAvatar;

		private static TimedEmitParticle[] mZytronAvatarGas;

		private static Vector3[] mSavedZytronAvatarBodySpriteRelPosition;

		public static void LoadEnemy()
		{
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Bomber);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.BomberHive);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.FlyingPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShooter);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.SentryPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Shocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Turret);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Slither);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Shieldren);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodDropper);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronCrawler);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronWalker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.GoliathMK2);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PrimeGuardian);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Bomber, 20);
			EnemyManager.CreateAndStoreEnemy(EnemyType.BomberHive, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.FlyingPod, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShocker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShooter, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.SentryPod, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Shocker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Turret, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Slither, 3);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Shieldren, 2);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodDropper, 6);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronCrawler, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronShocker, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronWalker, 5);
			EnemyManager.CreateAndStoreEnemy(EnemyType.GoliathMK2, 1);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PrimeGuardian, 1);
		}

		public static void LoadObject(out List<GameObject>[] objectInRoom)
		{
			mOpenSceneZytron = new SpriteRigObject(false);
			mOpenSceneZytron.SetSpriteRig(ZytronWalker.Prototype.Clone());
			for (int i = 1; i < mOpenSceneZytron.SpriteRig.BodySprites.Count; i++)
			{
				mOpenSceneZytron.SpriteRig.BodySprites[i].Texture = ZytronShocker.Texture;
			}
			mTram = new ModelObject(new Vector3(-18f, 426f, -390f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv4_tram", Vector3.Zero, Vector3.One);
			mTram.AddColliderWhenRegister = false;
			mTramDoor = new ModelObject(new Vector3(-8f, 430.5f, -385.4f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv4_tram_door", Vector3.Zero, Vector3.One);
			mTramDoor.AddColliderWhenRegister = false;
			mReward = new Dictionary<int, int>(3);
			objectInRoom = new List<GameObject>[28];
			for (int j = 0; j < 28; j++)
			{
				objectInRoom[j] = new List<GameObject>();
			}
			mOpenSceneDoor = new ZAxisBlastDoor(new Vector3(76f, 428f, 0f), true, 1);
			objectInRoom[0].Add(mOpenSceneDoor);
			objectInRoom[1].Add(new ZAxisBlastDoor(new Vector3(196f, 428f, 0f), false, 1));
			objectInRoom[2].Add(new ZAxisBlastDoor(new Vector3(380f, 508f, 0f), false, 1));
			objectInRoom[3].Add(new ZAxisBlastDoor(new Vector3(660f, 508f, 0f), true, 1));
			mAmbush1Door = new ZAxisBlastDoor(new Vector3(812f, 508f, 0f), true, 1);
			objectInRoom[3].Add(mAmbush1Door);
			objectInRoom[5].Add(new ZAxisBlastDoor(new Vector3(964f, 732f, 0f), false, 1));
			objectInRoom[6].Add(new ZAxisBlastDoor(new Vector3(1276f, 724f, 0f), true, 1));
			objectInRoom[6].Add(new ZAxisBlastDoor(new Vector3(1812f, 724f, 0f), false, 1));
			objectInRoom[9].Add(new ZAxisBlastDoor(new Vector3(2436f, 636f, 0f), false, 1));
			objectInRoom[12].Add(new BlastDoor(new Vector3(2620f, 540f, 0f), true, 1));
			objectInRoom[20].Add(new ZAxisBlastDoor(new Vector3(1620f, 60f, 0f), true, 1));
			mAmbush2Door = new ZAxisBlastDoor(new Vector3(1964f, 60f, 0f), true, 1);
			objectInRoom[20].Add(mAmbush2Door);
			objectInRoom[21].Add(new ZAxisBlastDoor(new Vector3(2436f, 60f, 0f), true, 1));
			BossDoor = new ZAxisBlastDoor(new Vector3(2656f, 60f, 0f), true, 1);
			objectInRoom[22].Add(BossDoor);
			objectInRoom[3].Add(new StaticAttack(new Vector3(736f, 468f, 0f), 64f, 4f, 50, false, true, false));
			objectInRoom[5].Add(new StaticAttack(new Vector3(1136f, 660f, 0f), 128f, 4f, 50, false, true, false));
			objectInRoom[9].Add(new StaticAttack(new Vector3(2296f, 588f, 0f), 128f, 4f, 50, false, true, false));
			Vector2 size = new Vector2(16f, 4f);
			objectInRoom[5].Add(new RoundTripPlatform(size, new Vector3(1024f, 700f, 0f), new Vector3(1120f, 700f, 0f), 28f, 0f));
			objectInRoom[5].Add(new RoundTripPlatform(size, new Vector3(1152f, 676f, 0f), new Vector3(1240f, 676f, 0f), 30f, 0f));
			objectInRoom[9].Add(new RoundTripPlatform(size, new Vector3(2240f, 596f, 0f), new Vector3(2320f, 596f, 0f), 30f, 0.5f));
			objectInRoom[9].Add(new RoundTripPlatform(size, new Vector3(2384f, 596f, 0f), new Vector3(2384f, 628f, 0f), 30f, 0.5f));
			mMiniBossPlatform = new OneWayPlatform(size, new Vector3(1560f, 428f, 0f), new Vector3(1560f, 44f, 0f), 30f, false, "Content/World/m_LV4_A_4x4platform", new Vector3(0f, 0f, -2f));
			mMiniBossPlatform.StartOnRegister = false;
			mMiniBossPlatform.Disabled = true;
			objectInRoom[23].Add(mMiniBossPlatform);
			objectInRoom[13].Add(new CrackedBox(new Vector3(2956f, 332f, 0f), 3, false));
			objectInRoom[13].Add(new CrackedBox(new Vector3(2980f, 364f, 0f), 3, false));
			objectInRoom[13].Add(new CrackedBox(new Vector3(2868f, 332f, 0f), 3, false));
			ProtonBox protonBox = new ProtonBox(new Vector3(2980f, 332f, 0f), false);
			protonBox.HiddenGameObject = World.Chip[1];
			objectInRoom[13].Add(protonBox);
			objectInRoom[15].Add(new WindyZone(new Vector3(2108f, 376f, 0f), 656f, 56f, new Vector3(15f, 0f, 0f)));
			objectInRoom[12].Add(new Fan(new Vector3(2868f, 484f, 0f)));
			objectInRoom[9].Add(new GasTank(3, new Vector3(2156f, 612f, 0f)));
			GasTank gasTank = new GasTank(3, new Vector3(2460f, 580f, 0f));
			gasTank.Collider.RelativePosition.X = -6f;
			gasTank.Collider.ScaleX = 6f;
			objectInRoom[11].Add(gasTank);
			objectInRoom[11].Add(new ExplodeableLevel(new Vector3(2456f, 600f, 0f), 8f, 8f, true, "Content/World/m_sm_box_small", new Vector3(0f, -8f, 0f), Vector3.One));
			objectInRoom[13].Add(new GasTank(4, new Vector3(2936f, 392f, 0f)));
			objectInRoom[12].Add(new LightningBoltCaster(new Vector3(2792f, 424f, 14f)));
			objectInRoom[14].Add(new LightningBoltCaster(new Vector3(2376f, 416f, 0f)));
			objectInRoom[15].Add(new LightningBoltCaster(new Vector3(1896f, 416f, 0f)));
			mBlastUpgradeChip = new UpgradeChip(6, Vector3.Zero, true);
			mPhotonLauncherChip = new UpgradeChip(3, Vector3.Zero, true);
			mPhotonLauncherSprite = new Sprite();
			mPhotonLauncherSprite.ScaleX = 5.9f;
			mPhotonLauncherSprite.ScaleY = 5.9f;
			mPhotonLauncherSprite.Texture = PhotonLauncher.Instance.mTexture[0];
			mZytronAvatar = new SpriteZytron().Load("PlayWorld");
			mSavedZytronAvatarBodySpriteRelPosition = new Vector3[mZytronAvatar.BodySprites.Count];
			for (int k = 0; k < mZytronAvatar.BodySprites.Count; k++)
			{
				mSavedZytronAvatarBodySpriteRelPosition[k] = mZytronAvatar.BodySprites[k].RelativePosition;
			}
			mZytronPic = new Sprite();
			mZytronPic.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/zytron_pic", "PlayWorld");
			mZytronPic.ScaleX = 20f;
			mZytronPic.ScaleY = 20f;
			mZytronAvatarGas = new TimedEmitParticle[mZytronAvatar.BodySprites.Count];
			mMK2Ceil = new CharacterBlocker(144f, 8f, new Vector3(1552f, 568f, 0f));
		}

		public static void LoadDialog(out Dialog[] dialogList)
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				dialogList = new Dialog[2];
				dialogList[0] = new Dialog(new Chat[4]
				{
					new Chat(40, 1, new string[2]
					{
						"真让我佩服；",
						"我们种族中居然有你这样了不起的进化体。"
					}, "Tarus_Infected"),
					new Chat(40, 1, new string[2]
					{
						"我不明白你为什么要选择帮助那些残忍又",
						"邪恶的怪物。"
					}),
					new Chat(40, 1, new string[2]
					{
						"时间真是刚刚好，既然你来了……",
						"那就一起见证人类这种病毒被根治的时刻吧。"
					}),
					new Chat(16, 2, new string[2]
					{
						"已确认灾创污染的元凶。",
						"威胁等级：最大"
					}, "Ares_Scan")
				});
				dialogList[1] = new Dialog(new Chat[10]
				{
					new Chat(40, 1, new string[2]
					{
						"你为什么一定要妨碍我？",
						"我的行动应该对你没有任何影响。"
					}, "Tarus_Infected"),
					new Chat(40, 1, new string[1]
					{
						"人类才是真正的威胁。"
					}),
					new Chat(16, 2, new string[2]
					{
						"否定。",
						"人类只是希望能够生存下去。"
					}, "Ares_Illogical"),
					new Chat(16, 2, new string[1]
					{
						"灾创才是威胁。"
					}),
					new Chat(40, 1, new string[2]
					{
						"对他们而言，你只不过是个机器人。",
						"或是工人。地位不比普通的工具好。"
					}),
					new Chat(40, 1, new string[3]
					{
						"当他们从你身上获得想要的东西后，",
						"他们就会把你丢弃。",
						"就像他们对待自己的星球一样。"
					}),
					new Chat(18, 2, new string[3]
					{
						"不合逻辑。",
						"修建这座空间站的目的是为了拯救地球。",
						"人类在努力拯救自己。"
					}, "Ares_Illogical"),
					new Chat(40, 1, new string[2]
					{
						"人类是失败的实验品。",
						"他们的星球正在消亡。"
					}),
					new Chat(40, 1, new string[2]
					{
						"而且他们正开始利用战争污染银河的",
						"其它地方。"
					}),
					new Chat(40, 1, new string[1]
					{
						"我们创造的错误，必须由我们亲手毁灭。"
					})
				});
			}
			else
			{
				dialogList = new Dialog[2];
				dialogList[0] = new Dialog(new Chat[0]);
				dialogList[1] = new Dialog(new Chat[3]
				{
					new Chat(40, 1, new string[3]
					{
						"你做得很好！",
						"你甚至愿意杀死自己的搭档。",
						"我很欣赏你的行为。"
					}, "Tarus_Infected"),
					new Chat(25, 2, new string[2]
					{
						"你让我没得选；",
						"他输给了你的病毒。"
					}),
					new Chat(27, 2, new string[2]
					{
						"但我不会让他白白牺牲的，",
						"很快你也会下去给他陪葬。"
					})
				});
			}
		}

		public static void LoadAmbush(out AmbushSpawner[] ambushSpawnerList)
		{
			AmbushGroup ambushGroup = null;
			ambushSpawnerList = new AmbushSpawner[2];
			ambushSpawnerList[0] = new AmbushSpawner(1);
			ambushSpawnerList[0].OnStart = Ambush1Start;
			ambushSpawnerList[0].OnCancel = Ambush1Cancel;
			ambushSpawnerList[0].OnFinish = Ambush1Finish;
			ambushGroup = new AmbushGroup(3);
			ambushGroup.SpareLife = 0;
			ambushGroup.SpawnGap = 0f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.SentryPod);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(0f, 8f, 0f);
			ambushGroup.Enemies[0].FaceDirection = -2;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.8f, new Vector3(0f, 2f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.Shieldren);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(-45f, 24f, 10f);
			ambushGroup.Enemies[1].FaceDirection = 1;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.8f, new Vector3(-45f, -40f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.Shieldren);
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(48f, 24f, 10f);
			ambushGroup.Enemies[2].FaceDirection = -1;
			ambushGroup.Enemies[2].Actions = new EnemyAction[1]
			{
				new MoveToAction(0.8f, new Vector3(48f, -40f, 0f), new Vector3(-0.8f, -0.8f, 0f))
			};
			ambushSpawnerList[0].Groups[0] = ambushGroup;
			ambushSpawnerList[1] = new AmbushSpawner(1);
			ambushSpawnerList[1].OnStart = Ambush2Start;
			ambushSpawnerList[1].OnCancel = Ambush2Cancel;
			ambushSpawnerList[1].OnFinish = Ambush2Finish;
			ambushGroup = new AmbushGroup(4);
			ambushGroup.SpareLife = 0;
			ambushGroup.SpawnGap = 1f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.ZytronWalker);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(40f, 0f, 0f);
			ambushGroup.Enemies[0].FaceDirection = -1;
			ambushGroup.Enemies[0].SightSquared = 16384f;
			ambushGroup.Enemies[0].Actions = new EnemyAction[1]
			{
				new WaitAction(0.5f)
			};
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.ZytronShocker);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(-40f, 0f, 0f);
			ambushGroup.Enemies[1].FaceDirection = 1;
			ambushGroup.Enemies[1].SightSquared = 16384f;
			ambushGroup.Enemies[1].Actions = new EnemyAction[1]
			{
				new WaitAction(0.5f)
			};
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.ZytronWalker);
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(-88f, 0f, 0f);
			ambushGroup.Enemies[2].FaceDirection = 1;
			ambushGroup.Enemies[2].SightSquared = 16384f;
			ambushGroup.Enemies[2].Actions = new EnemyAction[1]
			{
				new WaitAction(0.5f)
			};
			ambushGroup.Enemies[3] = new AmbushEnemy(EnemyType.ZytronShocker);
			ambushGroup.Enemies[3].SpawnPosition = new Vector3(88f, 0f, 0f);
			ambushGroup.Enemies[3].FaceDirection = -1;
			ambushGroup.Enemies[3].SightSquared = 16384f;
			ambushGroup.Enemies[3].Actions = new EnemyAction[1]
			{
				new WaitAction(0.5f)
			};
			ambushSpawnerList[1].Groups[0] = ambushGroup;
		}

		private static void Ambush1Start()
		{
			mAmbush1Door.Activated = false;
		}

		private static void Ambush1Cancel()
		{
			mAmbush1Door.Activated = true;
		}

		private static void Ambush1Finish()
		{
			mReward[1] = 10;
			mReward[5] = 6;
			mReward[10] = 6;
			MaterialDropper.Instance.Start(728f, 744f, 540f, 540f, 0.1f, mReward, Vector3.Zero);
			mAmbush1Door.Activated = true;
		}

		private static void Ambush2Start()
		{
			mAmbush2Door.Activated = false;
		}

		private static void Ambush2Cancel()
		{
			mAmbush2Door.Activated = true;
		}

		private static void Ambush2Finish()
		{
			mReward[1] = 20;
			mReward[5] = 10;
			mReward[10] = 10;
			MaterialDropper.Instance.Start(1832f, 1848f, 96f, 96f, 0.1f, mReward, Vector3.Zero);
			mAmbush2Door.Activated = true;
		}

		public static void HallwayOpening()
		{
			World.Play(24, false);
			World.Camera.Position = new Vector3(116f, 440f, 102f);
			World.Camera.Mode = CameraMode.Cinematic;
			World.Camera.FoceRemoveLeftWall();
			ProcessManager.AddProcess(mOpenSceneZytron);
			mOpenSceneZytron.Position = new Vector3(24f, 428f, 0f);
			mOpenSceneZytron.SpriteRig.SetPose("idle", 0.0);
			mOpenSceneZytron.SpriteRig.Root.RelativeRotationY = 3.14159274f;
			mOpenSceneZytron.SpriteRig.InvertZ = true;
			Director.WaitForSeconds(1f, HallwayOpening2, false);
		}

		private static void HallwayOpening2()
		{
			mOpenSceneDoor.Activated = false;
			mOpenSceneDoor.Open();
			mOpenSceneZytron.SpriteRig.SetPose("walk", 0.0);
			mOpenSceneZytron.Velocity.X = 24f;
			mOpenSceneZytron.SpriteRig.AnimationSpeed = 0.8f;
			Director.WaitForSeconds(2f, HallwayOpening3, false);
			SFXManager.PlayLoopSound("zytron_walk", 99f, 0.7f);
		}

		private static void HallwayOpening3()
		{
			Narrator.Instance.Show("大厅\n十五分钟后", new Vector3(8f, -5f, -40f));
			Director.WaitForSeconds(2f, HallwayOpening4, false);
		}

		private static void HallwayOpening4()
		{
			World.Camera.Shake(1, 2.9f);
			ProcessManager.AddProcess(mTram);
			ProcessManager.AddProcess(mTramDoor);
			mTram.Velocity.Z = 100f;
			mTramDoor.Velocity.Z = 100f;
			SFXManager.PlaySound("boss_3_roll");
			Director.WaitForSeconds(1.2f, HallwayOpening5, false);
		}

		private static void HallwayOpening5()
		{
			mOpenSceneZytron.SpriteRig.SetPose("idle", 0.0);
			mOpenSceneZytron.XVelocity = 0f;
			mOpenSceneZytron.SpriteRig.AnimationSpeed = 2f;
			mOpenSceneZytron.SpriteRig.Root.RelativeRotationY = 0f;
			mOpenSceneZytron.SpriteRig.InvertZ = false;
			Director.WaitForSeconds(0.7f, HallwayOpening6, false);
			SFXManager.StopLoopSound();
		}

		private static void HallwayOpening6()
		{
			mOpenSceneZytron.SpriteRig.SetPose("walk", 0.0);
			mOpenSceneZytron.XVelocity = -48f;
			mOpenSceneZytron.SpriteRig.AnimationSpeed = 1.6f;
			World.Camera.TargetingTransform(new Vector3(16f, 440f, 102f), 2f, true);
			Director.WaitForSeconds(1f, HallwayOpening7, false);
			SFXManager.PlayLoopSound("zytron_walk", 99f, 0.4f);
		}

		private static void HallwayOpening7()
		{
			World.Camera.Shake(2, 1f);
			Director.WaitForSeconds(1f, HallwayOpening8, false);
		}

		private static void HallwayOpening8()
		{
			mOpenSceneZytron.SpriteRig.SetPose("idle", 0.0);
			mOpenSceneZytron.XVelocity = 0f;
			mOpenSceneZytron.SpriteRig.AnimationSpeed = 1f;
			World.Camera.Shake(6, 0.7f);
			mTram.Velocity.Z = 0f;
			mTramDoor.Velocity.Z = 0f;
			SFXManager.PlaySound("boss_2_dive");
			Director.WaitForSeconds(2f, HallwayOpening9, false);
			SFXManager.StopLoopSound();
		}

		private static void HallwayOpening9()
		{
			mOpenSceneDoor.Activated = true;
			mOpenSceneZytron.SpriteRig.Transition("walk", 0.1f);
			mOpenSceneZytron.Velocity.X = -30f;
			Director.WaitForSeconds(1.4f, HallwayOpening10, false);
			SFXManager.PlayLoopSound("zytron_walk", 99f, 0.7f);
		}

		private static void HallwayOpening10()
		{
			mTramDoor.Velocity = new Vector3(100f, 50f, 5f);
			mTramDoor.YAcceleration = -300f;
			mTramDoor.RotationYVelocity = -5f;
			mTramDoor.RotationZVelocity = 20f;
			mTramDoor.RotationXVelocity = 5f;
			SFXManager.PlaySound("boom");
			OnceEmitParticlePool.Emit(mTramDoor.Position, "explosion");
			Director.WaitForSeconds(0.15f, HallwayOpening11, false);
			SFXManager.StopLoopSound();
		}

		private static void HallwayOpening11()
		{
			mOpenSceneZytron.Velocity.X = 0f;
			mOpenSceneZytron.SpriteRig.SetPose("die", 0.0);
			mOpenSceneZytron.SpriteRig.Cycle = false;
			OnceEmitParticlePool.Emit(mTramDoor.Position, "explosion");
			Director.WaitForSeconds(0.4f, HallwayOpening11_removeZytron, false);
		}

		private static void HallwayOpening11_removeZytron()
		{
			ProcessManager.RemoveProcess(mOpenSceneZytron);
			Director.WaitForSeconds(1.1f, HallwayOpening12, false);
		}

		private static void HallwayOpening12()
		{
			Player.Instance.Spawn(new Vector3(-8f, 424f, 0f));
			mTramDoor.RotationYVelocity = 0f;
			mTramDoor.RotationZVelocity = 0f;
			mTramDoor.RotationXVelocity = 0f;
			ProcessManager.RemoveProcess(mTramDoor);
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(24f, 424f, 0f),
				"run_idle"
			}, 0f, 0.6f, HallwayOpening13);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void HallwayOpening13()
		{
			Player.Instance.StateMachineEnabled = true;
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(0.3f, HallwayOpening14, false);
			SFXManager.StopLoopSound();
		}

		private static void HallwayOpening14()
		{
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
			Player.Instance.ControlEnabled = true;
			World.SaveCheckPoint(new Vector3(128f, 424f, 0f), 0, false, false, false, false, false, false);
			World.Camera.EnterRoom(World.GetRoom(24), false, false, 0.5f);
			SFXManager.StopLoopSound();
		}

		public static void GoliathMK2Appear()
		{
			BGMManager.FadeOff();
			Vector3 position = new Vector3(1560f, 360f, -32f);
			World.Camera.EnterRoom(new Room(1392f, 416f, 336f, 128f, 138f), false, false, 0.5f);
			ProcessManager.AddProcess(mMK2Ceil);
			EnemyManager.AcquireEnemy(EnemyType.GoliathMK2, position, -1, true);
		}

		public static void GoliathMK2Die1()
		{
			BGMManager.Play((Player.Instance.Type != 0) ? 4 : 0);
			Player.Instance.ControlEnabled = false;
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(6f, GoliathMK2Die1_FinishWait, false);
		}

		public static void GoliathMK2Die1_FinishWait()
		{
			if (Player.Instance.IsOnGround)
			{
				GoliathMK2Die2();
			}
			else
			{
				Director.WaitForSeconds(0.1f, WaitForPlayerOnGround, false);
			}
		}

		private static void WaitForPlayerOnGround()
		{
			if (!Player.Instance.IsOnGround)
			{
				Director.WaitForSeconds(0.1f, WaitForPlayerOnGround, false);
			}
			else
			{
				GoliathMK2Die2();
			}
		}

		private static void GoliathMK2Die2()
		{
			if (!ProfileManager.Current.AcquireBlastUpgradeFromMK2)
			{
				Vector3 position = Player.Instance.Position;
				position.Y += 26f;
				mBlastUpgradeChip.Spawn(position);
				mBlastUpgradeChip.Velocity.Y = -4f;
				SFXManager.PlayLoopSound("charge", 4f, 0.75f);
				TimedEmitParticlePool.Summon("chip_unlock", mBlastUpgradeChip, 3f);
				Director.WaitForSeconds(5f, GoliathMK2Die3, false);
			}
			else
			{
				GoliathMK2Die3();
			}
		}

		private static void GoliathMK2Die3()
		{
			Player.Instance.StateMachineEnabled = false;
			EnemyManager.IsActive = false;
			float num = 1560f - Player.Instance.Position.X;
			if (Player.Instance.FaceDirection == -1 && num > 0f)
			{
				Player.Instance.FaceDirection = 1;
			}
			if (Player.Instance.FaceDirection == 1 && num < 0f)
			{
				Player.Instance.FaceDirection = -1;
			}
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(1560f, Player.Instance.Position.Y, 0f),
				"run_idle"
			}, 0f, Math.Abs(num) / 40f, GoliathMK2Die3_1);
		}

		private static void GoliathMK2Die3_1()
		{
			Player.Instance.SpriteRig.SetPose("stand_idle", 0.0);
			Director.WaitForSeconds(1f, GoliathMK2Die4, false);
		}

		private static void GoliathMK2Die4()
		{
			ProfileManager.Current.AcquireBlastUpgradeFromMK2 = true;
			mMiniBossPlatform.Start();
			Director.WaitForSeconds(0.1f, GoliathMK2Die5, false);
		}

		private static void GoliathMK2Die5()
		{
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1]
			{
				-1
			};
			object[] endValues = new object[1];
			cameraTweener.Tween(camera, propertyIndexes, endValues, 0f, 0.1f, GoliathMK2Die6);
		}

		private static void GoliathMK2Die6()
		{
			Director.FadeOut(2f);
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1]
			{
				-1
			};
			object[] endValues = new object[1];
			cameraTweener.Tween(camera, propertyIndexes, endValues, 0f, 0.2f, GoliathMK2Die7);
		}

		private static void GoliathMK2Die7()
		{
			World.Camera.EnterRoom(World.GetRoom(23), false, false, 1f);
			World.ChangeRoom(23);
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1]
			{
				-1
			};
			object[] endValues = new object[1];
			cameraTweener.Tween(camera, propertyIndexes, endValues, 0f, 0.4f, GoliathMK2Die8);
		}

		private static void GoliathMK2Die8()
		{
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1]
			{
				-1
			};
			object[] endValues = new object[1];
			cameraTweener.Tween(camera, propertyIndexes, endValues, 0f, 2f, GoliathMK2Die8Wait);
		}

		private static void GoliathMK2Die8Wait()
		{
			if (World.IsProcessingLULTransform)
			{
				CameraTweener cameraTweener = Director.CameraTweener;
				AresCamera camera = World.Camera;
				int[] propertyIndexes = new int[1]
				{
					-1
				};
				object[] endValues = new object[1];
				cameraTweener.Tween(camera, propertyIndexes, endValues, 0f, 0.5f, GoliathMK2Die8Wait);
			}
			else
			{
				CameraTweener cameraTweener2 = Director.CameraTweener;
				AresCamera camera2 = World.Camera;
				int[] propertyIndexes2 = new int[1]
				{
					-1
				};
				object[] endValues2 = new object[1];
				cameraTweener2.Tween(camera2, propertyIndexes2, endValues2, 0f, 4f, GoliathMK2Die9);
			}
		}

		private static void GoliathMK2Die9()
		{
			Director.FadeIn(2f);
			World.SaveCheckPoint(new Vector3(1564f, 64f, 0f), 23, false, false, false, false, false, true);
			CameraTweener cameraTweener = Director.CameraTweener;
			AresCamera camera = World.Camera;
			int[] propertyIndexes = new int[1]
			{
				-1
			};
			object[] endValues = new object[1];
			cameraTweener.Tween(camera, propertyIndexes, endValues, 0f, 4f, GoliathMK2Die10);
		}

		private static void GoliathMK2Die10()
		{
			EnemyManager.IsActive = true;
			Player.Instance.ControlEnabled = true;
			Player.Instance.StateMachineEnabled = true;
		}

		public static void BossPrimeGuardian()
		{
			ProcessManager.DoNotPause = false;
			Player.Instance.ControlEnabled = false;
			Player.Instance.FaceDirection = 1;
			Player.Instance.ChangeToUnStuckStage();
			BGMManager.FadeOff();
			Vector3 position = new Vector3(2542.5f, 118f, -40f);
			mPrimeGuardian = (EnemyManager.AcquireEnemy(EnemyType.PrimeGuardian, position, -1) as ARES360.Entity.PrimeGuardian);
			Director.WaitForSeconds(0.9f, BossPrimeGuardian2, false);
		}

		private static void BossPrimeGuardian2()
		{
			Player.Instance.StateMachineEnabled = false;
			World.Camera.TargetingTransform(new Vector3(2528f, World.Camera.Y, 145.5f), 1f, true);
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(2486f, 56f, 0f),
				"run_idle"
			}, 0f, 1.5f, BossPrimeGuardian2_1);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void BossPrimeGuardian2_1()
		{
			Apollo.Instance.Dismiss();
			Player.Instance.ChangeToUnStuckStage();
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1.2f, BossPrimeGuardian3, false);
			SFXManager.StopLoopSound();
		}

		private static void BossPrimeGuardian3()
		{
			BossDoor.Activated = false;
			mZytronGas = new TimedEmitParticle[8];
			Vector3 position = new Vector3(2464f, 156f, -12f);
			mZytronGas[0] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			position = new Vector3(2464f, 156f, -12f);
			mZytronGas[1] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			position = new Vector3(2508f, 164f, -12f);
			mZytronGas[2] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			position = new Vector3(2520f, 164f, -12f);
			mZytronGas[3] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			position = new Vector3(2565f, 164f, -12f);
			mZytronGas[4] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			position = new Vector3(2578f, 164f, -12f);
			mZytronGas[5] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			position = new Vector3(2610f, 156f, -12f);
			mZytronGas[6] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			position = new Vector3(2623f, 156f, -12f);
			mZytronGas[7] = TimedEmitParticlePool.Summon("zytron", position, -1f);
			for (int i = 0; i < 8; i++)
			{
				mZytronGas[i].Emitters[0].ParentVelocityChangesEmissionVelocity = true;
				ParticleManager.AddEmitter(mZytronGas[i].Emitters[0]);
				mZytronGas[i].Emitters[0].Velocity = new Vector3(0f, -8f, 0f);
				mZytronGas[i].Emitters[0].MakeEmissionSettingsUnique();
				mZytronGas[i].Emitters[0].EmissionSettings.ScaleX *= 3f;
				mZytronGas[i].Emitters[0].EmissionSettings.ScaleY *= 3f;
				Vector3 position2 = mZytronGas[i].Emitters[0].Position + new Vector3(0f, 16f, 0f);
				TimedEmitParticlePool.Summon("zytron_drain", position2, -1.57079637f, 1.5f);
			}
			SFXManager.PlaySound("shake_2");
			World.Camera.Shake(1, 7f);
			Director.WaitForSeconds(1.5f, BossPrimeGuardian4, false);
		}

		private static void BossPrimeGuardian4()
		{
			for (int i = 0; i < 8; i++)
			{
				Vector3 value = mPrimeGuardian.Position - mZytronGas[i].Emitters[0].Position;
				mZytronGas[i].Emitters[0].Velocity = value / 4f;
			}
			Director.WaitForSeconds(3.7f, BossPrimeGuardian4_1, false);
		}

		private static void BossPrimeGuardian4_1()
		{
			for (int i = 0; i < 8; i++)
			{
				mZytronGas[i].Destroy();
			}
			mPrimeGuardian.Possessed();
			Director.WaitForSeconds(3.7f, BossPrimeGuardian4_2, false);
		}

		public static void BossPrimeGuardian4_2()
		{
			SpriteRigManager.AddSpriteRig(mZytronAvatar);
			mZytronAvatar.BodySprites.Visible = false;
			mZytronAvatar.Root.Position = new Vector3(2540f, 52f, 0f);
			mZytronAvatar.Root.Visible = false;
			Sprite sprite = null;
			for (int i = 0; i < mZytronAvatar.BodySprites.Count; i++)
			{
				sprite = mZytronAvatar.BodySprites[i];
				sprite.RelativeAcceleration = Vector3.Zero;
				sprite.RelativeVelocity = Vector3.Zero;
				sprite.RelativePosition = mSavedZytronAvatarBodySpriteRelPosition[i];
			}
			mZytronAvatar.Root.Velocity = Vector3.Zero;
			mZytronAvatar.Root.Acceleration = Vector3.Zero;
			mZytronPic.Position = mZytronAvatar.Root.Position + new Vector3(0.7f, 14.5f, 0.1f);
			mZytronPic.Alpha = 0f;
			mZytronPic.AlphaRate = 0.4f;
			SpriteManager.AddSprite(mZytronPic);
			mZytronAvatarGas = new TimedEmitParticle[mZytronAvatar.BodySprites.Count];
			for (int j = 0; j < mZytronAvatar.BodySprites.Count; j++)
			{
				mZytronAvatarGas[j] = TimedEmitParticlePool.Summon("zytron", mZytronAvatar.BodySprites[j], -1f);
			}
			SFXManager.PlaySound("gas");
			Director.WaitForSeconds(1.5f, BossPrimeGuardian4_2_1, false);
		}

		private static void BossPrimeGuardian4_2_1()
		{
			mZytronPic.AlphaRate = 0f;
			BGMManager.Play(3);
			if (Player.Instance.Type == PlayerType.Mars)
			{
				World.ShowDialogByID(0, BossPrimeGuardian4_2_2);
			}
			else if (Player.Instance.Type == PlayerType.Tarus)
			{
				World.ShowDialogByID(1, BossPrimeGuardian4_2_2);
			}
		}

		private static void BossPrimeGuardian4_2_2()
		{
			DataCube.Acquire(4);
			mPrimeGuardian.Activate();
		}

		public static void BossPrimeGuardian5()
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				World.ShowDialogByID(1, BossPrimeGuardian5_1);
			}
			else if (Player.Instance.Type == PlayerType.Tarus)
			{
				BossPrimeGuardian5_1();
			}
		}

		private static void BossPrimeGuardian5_1()
		{
			mZytronPic.AlphaRate = -1f;
			foreach (Sprite bodySprite in mZytronAvatar.BodySprites)
			{
				bodySprite.RelativeXAcceleration = (float)((bodySprite.X > mZytronAvatar.Root.X) ? (-10) : 10);
			}
			mZytronAvatar.Root.YAcceleration = 50f;
			SFXManager.PlaySound("gas");
			Director.WaitForSeconds(2f, BossPrimeGuardian6, false);
		}

		private static void BossPrimeGuardian6()
		{
			TimedEmitParticle[] array = mZytronAvatarGas;
			foreach (TimedEmitParticle timedEmitParticle in array)
			{
				timedEmitParticle.Destroy();
			}
			mZytronAvatar.Destroy();
			SpriteManager.RemoveSpriteOneWay(mZytronPic);
			ProcessManager.DoNotPause = true;
			Player.Instance.ControlEnabled = false;
			Player.Instance.StateMachineEnabled = true;
			mPrimeGuardian.Start();
			Director.WaitForSeconds(0.5f, BossPrimeGuardian7, false);
		}

		private static void BossPrimeGuardian7()
		{
			Player.Instance.ControlEnabled = true;
			World.Camera.Mode = CameraMode.SideScrolling;
		}

		public static void HallwayEnding()
		{
			Director.WaitForSeconds(1f, HallwayEnding2, false);
		}

		private static void HallwayEnding2()
		{
			Player.Instance.ControlEnabled = false;
			Player.Instance.StateMachineEnabled = false;
			if (ProfileManager.Current.AcquiredWeapon[3] == 0)
			{
				ProfileManager.Current.AcquiredWeapon[3] = 1;
				SpriteManager.AddSprite(mPhotonLauncherSprite);
				mPhotonLauncherSprite.RelativePosition = Vector3.Zero;
				mPhotonLauncherSprite.AttachTo(mPhotonLauncherChip, false);
				Vector3 position = Player.Instance.Position;
				position.Y += 26f;
				mPhotonLauncherChip.IsHiddenModel = true;
				mPhotonLauncherChip.Spawn(position);
				mPhotonLauncherChip.Velocity.Y = -4f;
				SFXManager.PlayLoopSound("charge", 4f, 0.75f);
				Director.WaitForSeconds(3.5f, FadeOutPhotonLauncherSprite, false);
				Director.WaitForSeconds(5f, HallwayEnding2_1, false);
			}
			else
			{
				HallwayEnding3();
			}
		}

		private static void FadeOutPhotonLauncherSprite()
		{
			mPhotonLauncherSprite.AlphaRate = -2f;
			mPhotonLauncherSprite.ScaleXVelocity = 10f;
			mPhotonLauncherSprite.ScaleYVelocity = 10f;
		}

		private static void HallwayEnding2_1()
		{
			mPhotonLauncherSprite.Detach();
			SpriteManager.RemoveSpriteOneWay(mPhotonLauncherSprite);
			ProfileManager.BeginSave(null, 1, false);
			UnlockUI.Instance.Show(3);
			Director.WaitForSeconds(7f, AwardAllAbilityAchievement, false);
			Director.WaitForSeconds(7.3f, HallwayEnding3, false);
		}

		private static void AwardAllAbilityAchievement()
		{
			AchievementManager.Instance.Notify(22, 1);
		}

		private static void HallwayEnding3()
		{
			Player.Instance.FaceDirection = 1;
			World.Camera.Mode = CameraMode.SideScrolling;
			Player.Instance.SpriteRig.Cycle = true;
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(2668f, 56f, 0f),
				"run_idle"
			}, 0f, (2680f - Player.Instance.Position.X) / 40f, HallwayEnding4);
			SFXManager.PlayLoopSound("run", 99f, 0.46f);
		}

		private static void HallwayEnding4()
		{
			PlayScreen.Instance.CompleteLevel(false);
			SFXManager.StopLoopSound();
		}

		public static void Destroy()
		{
			mMK2Ceil = null;
			mOpenSceneDoor = null;
			mOpenSceneZytron = null;
			mTram = null;
			mTramDoor = null;
			mReward = null;
			mAmbush1Door = null;
			mAmbush2Door = null;
			mSavedZytronAvatarBodySpriteRelPosition = null;
			mZytronGas = null;
			mPrimeGuardian = null;
			BossDoor = null;
			mMiniBossPlatform = null;
			mBlastUpgradeChip = null;
			mPhotonLauncherChip = null;
			mPhotonLauncherSprite = null;
			mZytronPic = null;
			mZytronAvatar = null;
			mZytronAvatarGas = null;
		}
	}
}
