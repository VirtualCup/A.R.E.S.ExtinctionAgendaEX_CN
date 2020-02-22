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
using FlatRedBall.Graphics;
using FlatRedBall.ManagedSpriteGroups;
using FlatRedBall.Math.Geometry;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace ARES360
{
	public static class LV7Loader
	{
		private static AxisAlignedRectangle mDoor;

		private static AxisAlignedRectangle mDoor2;

		private static Sprite mDoorSprite;

		private static Sprite mDoorSprite2;

		private static SpringPlatform redPlatform1;

		private static SpringPlatform redPlatform2;

		private static SpringPlatform redPlatform3;

		private static PyramidUnlocker magentaUnlocker;

		private static DoorLocker magentaLocker;

		private static BlastDoor magentaDoor;

		private static PyramidUnlocker redUnlocker;

		private static PyramidUnlocker greenUnlocker;

		private static PyramidUnlocker blueUnlocker;

		private static PyramidUnlocker yellowUnlocker;

		private static BlastDoor redDoor;

		private static DoorLocker redDoorLocker;

		private static CoreDoorLocker redLocker;

		private static CoreDoorLocker greenLocker;

		private static CoreDoorLocker blueLocker;

		private static CoreDoorLocker yellowLocker;

		private static ModelObject greenHex1;

		private static ModelObject greenHex2;

		private static ModelObject greenHex3;

		private static ModelObject greenHex4;

		private static OneWayPlatform mCoreLift;

		private static ModelObject mInnerCoreLift;

		private static ARES360.Entity.TheCore mTheCore;

		private static CharacterBlocker mBlocker;

		private static Sprite mZytronPic;

		private static SpriteRig mZytronAvatar;

		private static TimedEmitParticle[] mZytronGas;

		private static Vector3[] mSavedZytronAvatarBodySpriteRelPosition;

		private static SpriteObject mCoreC;

		private static SpriteObject mCoreCW;

		private static ModelObject[] mCoreCCList;

		private static ModelObject[] mCoreCWList;

		private static bool mIsZytronGasDestroyed;

		public static bool[] HasUnlockedMinoscore;

		private static bool mHasCompletedMinosSurvival = false;

		public static void LoadEnemy()
		{
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Bomber);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.BomberHive);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.FlyingPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShooter);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.PodShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.SentryPod);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.Shocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronCrawler);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronShocker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.ZytronWalker);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.CoreTurret);
			EnemyManager.LoadEnemyPrototypeForType(EnemyType.TheCore);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Bomber, 12);
			EnemyManager.CreateAndStoreEnemy(EnemyType.BomberHive, 1);
			EnemyManager.CreateAndStoreEnemy(EnemyType.FlyingPod, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShooter, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.PodShocker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.SentryPod, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.Shocker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronCrawler, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronShocker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.ZytronWalker, 10);
			EnemyManager.CreateAndStoreEnemy(EnemyType.CoreTurret, 4);
			EnemyManager.CreateAndStoreEnemy(EnemyType.TheCore, 1);
		}

		public static void LoadObject(out List<GameObject>[] objectInRoom)
		{
			World.CurrentWorld.RoomList[3].ModelTransform.Add(World.CurrentWorld.RoomList[8].ModelTransform[8]);
			bool[] array = HasUnlockedMinoscore = new bool[5];
			objectInRoom = new List<GameObject>[24];
			for (int i = 0; i < 24; i++)
			{
				objectInRoom[i] = new List<GameObject>();
			}
			redPlatform1 = new SpringPlatform(new Vector3(824f, 808f, -2f));
			redPlatform2 = new SpringPlatform(new Vector3(792f, 840f, -2f));
			redPlatform3 = new SpringPlatform(new Vector3(840f, 872f, -2f));
			Sprite sprite = new Sprite();
			sprite.Animate = false;
			sprite.BlendOperation = BlendOperation.Add;
			sprite.ColorOperation = ColorOperation.ColorTextureAlpha;
			sprite.ScaleX = 30.7393856f;
			sprite.ScaleY = 30.7393856f;
			sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/lvl05_3", "PlayWorld");
			sprite.TextureAddressMode = TextureAddressMode.Clamp;
			sprite.Blue = 0f;
			sprite.Green = 0.392157f;
			sprite.Red = 1f;
			mCoreC = new SpriteObject(sprite);
			mCoreC.X = 1072.43237f;
			mCoreC.Y = 683.648438f;
			mCoreC.Z = 13.06154f;
			objectInRoom[3].Add(mCoreC);
			sprite = new Sprite();
			sprite.Animate = false;
			sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/lvl05_2", "PlayWorld");
			sprite.BlendOperation = BlendOperation.Add;
			sprite.ColorOperation = ColorOperation.ColorTextureAlpha;
			sprite.ScaleX = 24.4265213f;
			sprite.ScaleY = 24.4265213f;
			sprite.Blue = 0f;
			sprite.Green = 1f;
			sprite.Red = 1f;
			mCoreCW = new SpriteObject(sprite);
			mCoreCW.X = 1072.43237f;
			mCoreCW.Y = 683.648438f;
			objectInRoom[3].Add(mCoreCW);
			mCoreCCList = new ModelObject[19];
			mCoreCCList[0] = new ModelObject(new Vector3(1072f, 1584f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCCList[0]);
			mCoreCCList[1] = new ModelObject(new Vector3(1072f, 944.000061f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCCList[1]);
			mCoreCCList[2] = new ModelObject(new Vector3(1072f, 1104f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[18].Add(mCoreCCList[2]);
			mCoreCCList[3] = new ModelObject(new Vector3(1072f, 1264f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCCList[3]);
			mCoreCCList[4] = new ModelObject(new Vector3(1072f, 1368.46008f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_1", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCCList[4]);
			mCoreCCList[5] = new ModelObject(new Vector3(1072f, 799.9327f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_1", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCCList[5]);
			mCoreCCList[6] = new ModelObject(new Vector3(1072f, 783.999939f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCCList[6]);
			mCoreCCList[7] = new ModelObject(new Vector3(1072f, 890.9179f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_1", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCCList[7]);
			mCoreCCList[8] = new ModelObject(new Vector3(1072f, 1151.06323f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_1", Vector3.Zero, Vector3.One);
			objectInRoom[18].Add(mCoreCCList[8]);
			mCoreCCList[9] = new ModelObject(new Vector3(1072f, 1196.66553f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_2", Vector3.Zero, Vector3.One);
			objectInRoom[18].Add(mCoreCCList[9]);
			mCoreCCList[10] = new ModelObject(new Vector3(1072f, 1299.6449f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_2", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCCList[10]);
			mCoreCCList[11] = new ModelObject(new Vector3(1072f, 845.1525f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_2", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCCList[11]);
			mCoreCCList[12] = new ModelObject(new Vector3(1072f, 1107.46973f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_2", Vector3.Zero, Vector3.One);
			objectInRoom[18].Add(mCoreCCList[12]);
			mCoreCCList[13] = new ModelObject(new Vector3(1072f, 624f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCCList[13]);
			mCoreCCList[14] = new ModelObject(new Vector3(1072f, 384f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_base_2", Vector3.Zero, Vector3.One);
			objectInRoom[16].Add(mCoreCCList[14]);
			mCoreCCList[15] = new ModelObject(new Vector3(1072f, 464f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[16].Add(mCoreCCList[15]);
			mCoreCCList[16] = new ModelObject(new Vector3(1072f, 1424f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCCList[16]);
			mCoreCCList[17] = new ModelObject(new Vector3(1072f, 304f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_pillar", Vector3.Zero, Vector3.One);
			objectInRoom[16].Add(mCoreCCList[17]);
			mCoreCCList[18] = new ModelObject(new Vector3(1072f, 624f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_base_2", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCCList[18]);
			ModelObject[] array2 = mCoreCCList;
			foreach (ModelObject modelObject in array2)
			{
				modelObject.AddColliderWhenRegister = false;
			}
			mCoreCWList = new ModelObject[14];
			mCoreCWList[0] = new ModelObject(new Vector3(1072f, 1619.594f, -120f), new Vector3(0f, 0f, 3.14159274f), 1f, 1f, false, "Content/World/m_lv5_core_base_2", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCWList[0]);
			mCoreCWList[1] = new ModelObject(new Vector3(1072f, 624f, -120f), new Vector3(0f, 0f, 3.14159274f), 1f, 1f, false, "Content/World/m_lv5_core_base_2", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCWList[1]);
			mCoreCWList[2] = new ModelObject(new Vector3(1072f, 1474.608f, -120f), new Vector3(0f, 2.69333458f, 0f), 1f, 1f, false, "Content/World/m_lv5_core_swing_3", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCWList[2]);
			mCoreCWList[3] = new ModelObject(new Vector3(1072f, 1262.65454f, -120f), new Vector3(0f, 5.3304944f, 0f), 1f, 1f, false, "Content/World/m_lv5_core_swing_4", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCWList[3]);
			mCoreCWList[4] = new ModelObject(new Vector3(1072f, 746.088867f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_4", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCWList[4]);
			mCoreCWList[5] = new ModelObject(new Vector3(1072f, 946.152954f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_4", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCWList[5]);
			mCoreCWList[6] = new ModelObject(new Vector3(1072f, 926.9474f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_4", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCWList[6]);
			mCoreCWList[7] = new ModelObject(new Vector3(1072f, 711.8325f, -120f), new Vector3(0f, 5.22003746f, 0f), 1f, 1f, false, "Content/World/m_lv5_core_swing_4", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCWList[7]);
			mCoreCWList[8] = new ModelObject(new Vector3(1072f, 1518.30615f, -120f), new Vector3(0f, 5.385723f, 0f), 1f, 1f, false, "Content/World/m_lv5_core_swing_3", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCWList[8]);
			mCoreCWList[9] = new ModelObject(new Vector3(1072f, 1239.41846f, -120f), new Vector3(0f, 5.3304944f, 0f), 1f, 1f, false, "Content/World/m_lv5_core_swing_4", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCWList[9]);
			mCoreCWList[10] = new ModelObject(new Vector3(1072f, 991.8721f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_3", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCWList[10]);
			mCoreCWList[11] = new ModelObject(new Vector3(1072f, 1048.841f, -120f), new Vector3(0f, 3.202351f, 0f), 1f, 1f, false, "Content/World/m_lv5_core_swing_3", Vector3.Zero, Vector3.One);
			objectInRoom[18].Add(mCoreCWList[11]);
			mCoreCWList[12] = new ModelObject(new Vector3(1072f, 963.6465f, -120f), new Vector3(0f, 5.3304944f, 0f), 1f, 1f, false, "Content/World/m_lv5_core_swing_4", Vector3.Zero, Vector3.One);
			objectInRoom[3].Add(mCoreCWList[12]);
			mCoreCWList[13] = new ModelObject(new Vector3(1072f, 1426.17139f, -120f), Vector3.Zero, 1f, 1f, false, "Content/World/m_lv5_core_swing_3", Vector3.Zero, Vector3.One);
			objectInRoom[12].Add(mCoreCWList[13]);
			ModelObject[] array3 = mCoreCWList;
			foreach (ModelObject modelObject2 in array3)
			{
				modelObject2.AddColliderWhenRegister = false;
			}
			if (HasUnlockedMinoscore[1] && HasUnlockedMinoscore[2] && HasUnlockedMinoscore[3] && HasUnlockedMinoscore[4])
			{
				mCoreC.RotationZVelocity = -0.5f;
				mCoreCW.RotationZVelocity = 0.5f;
				ModelObject[] array4 = mCoreCCList;
				foreach (ModelObject modelObject3 in array4)
				{
					modelObject3.RotationYVelocity = 0.2f;
				}
				ModelObject[] array5 = mCoreCWList;
				foreach (ModelObject modelObject4 in array5)
				{
					modelObject4.RotationYVelocity = -0.2f;
				}
			}
			magentaDoor = new BlastDoor(new Vector3(964f, 636f, 0f), true, 1);
			objectInRoom[2].Add(magentaDoor);
			if (HasUnlockedMinoscore[0])
			{
				magentaDoor.Activated = true;
			}
			else
			{
				magentaDoor.Activated = false;
				magentaUnlocker = new PyramidUnlocker(new Vector3(584f, 688f, 0f), PyramidColor.Magenta);
				magentaUnlocker.OnTouch = CoreMagentaUnlock;
				objectInRoom[2].Add(magentaUnlocker);
				magentaLocker = new DoorLocker(new Vector3(952f, 636f, 8f));
				magentaLocker.Sprite.Red = 1f;
				magentaLocker.Sprite.Blue = 1f;
				objectInRoom[2].Add(magentaLocker);
			}
			redDoor = new BlastDoor(new Vector3(964f, 908f, 0f), false, 0);
			objectInRoom[6].Add(redDoor);
			if (HasUnlockedMinoscore[1])
			{
				mHasCompletedMinosSurvival = true;
				objectInRoom[5].Add(redPlatform1);
				objectInRoom[5].Add(redPlatform2);
				objectInRoom[5].Add(redPlatform3);
				redDoor.Activated = true;
			}
			else
			{
				mHasCompletedMinosSurvival = false;
				redUnlocker = new PyramidUnlocker(new Vector3(776f, 920f, 0f), PyramidColor.Red);
				redUnlocker.OnTouch = CoreRedUnlock;
				objectInRoom[6].Add(redUnlocker);
				redDoor.Activated = false;
				redDoorLocker = new DoorLocker(new Vector3(972f, 908f, 8f));
				redDoorLocker.Sprite.Red = 1f;
				objectInRoom[6].Add(redDoorLocker);
				mDoor = new AxisAlignedRectangle(4f, 12f);
				mDoor.Position = new Vector3(976f, 828f, 0f);
				mDoor2 = new AxisAlignedRectangle(16f, 4f);
				mDoor2.Position = new Vector3(824f, 904f, 0f);
				mDoorSprite = new Sprite();
				mDoorSprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/lvl02_22", "PlayWorld");
				mDoorSprite.ScaleX = 16f;
				mDoorSprite.ScaleY = 16f;
				mDoorSprite.Alpha = 0.5f;
				mDoorSprite.RotationY = 1.57079637f;
				mDoorSprite.Position = new Vector3(974f, 828f, 0f);
				mDoorSprite2 = new Sprite();
				mDoorSprite2.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/lvl02_22", "PlayWorld");
				mDoorSprite2.ScaleX = 20f;
				mDoorSprite2.ScaleY = 16f;
				mDoorSprite2.Alpha = 0.5f;
				mDoorSprite2.RotationX = 1.57079637f;
				mDoorSprite2.Position = new Vector3(824f, 902f, 0f);
			}
			if (!HasUnlockedMinoscore[2])
			{
				greenUnlocker = new PyramidUnlocker(new Vector3(936f, 1144f, 0f), PyramidColor.Green);
				greenUnlocker.OnTouch = CoreGreenUnlock;
				objectInRoom[11].Add(greenUnlocker);
				greenHex1 = new HexPlatform(new Vector3(808f, 1132f, 9.2376f));
				objectInRoom[11].Add(greenHex1);
				greenHex2 = new HexPlatform(new Vector3(840f, 1132f, -9.2376f));
				objectInRoom[11].Add(greenHex2);
				greenHex3 = new HexPlatform(new Vector3(872f, 1132f, 9.2376f));
				objectInRoom[11].Add(greenHex3);
				greenHex4 = new HexPlatform(new Vector3(904f, 1132f, -9.2376f));
				objectInRoom[11].Add(greenHex4);
			}
			if (!HasUnlockedMinoscore[3])
			{
				blueUnlocker = new PyramidUnlocker(new Vector3(2240f, 1056f, 0f), PyramidColor.Blue);
				blueUnlocker.OnTouch = CoreBlueUnlock;
				objectInRoom[23].Add(blueUnlocker);
			}
			if (!HasUnlockedMinoscore[4])
			{
				yellowUnlocker = new PyramidUnlocker(new Vector3(1240f, 1480f, 0f), PyramidColor.Yellow);
				yellowUnlocker.OnTouch = CoreYellowUnlock;
				objectInRoom[13].Add(yellowUnlocker);
			}
			Vector3 position = new Vector3(1072f, 620f, 0f);
			if (!HasUnlockedMinoscore[1])
			{
				redLocker = new CoreDoorLocker(position);
				redLocker.Sprite.Red = 1f;
				redLocker.Axis = new Vector2(-0.83f, -5.48f);
				redLocker.SpinFactor = 1f;
				objectInRoom[3].Add(redLocker);
			}
			if (!HasUnlockedMinoscore[2])
			{
				greenLocker = new CoreDoorLocker(position);
				greenLocker.Sprite.Green = 1f;
				greenLocker.Axis = new Vector2(-4.18f, -0.75f);
				greenLocker.SpinFactor = -1f;
				objectInRoom[3].Add(greenLocker);
			}
			if (!HasUnlockedMinoscore[3])
			{
				blueLocker = new CoreDoorLocker(position);
				blueLocker.Sprite.Blue = 1f;
				blueLocker.Sprite.Green = 1f;
				blueLocker.Axis = new Vector2(-5.21f, -1.5f);
				blueLocker.SpinFactor = 1f;
				objectInRoom[3].Add(blueLocker);
			}
			if (!HasUnlockedMinoscore[4])
			{
				yellowLocker = new CoreDoorLocker(position);
				yellowLocker.Sprite.Red = 1f;
				yellowLocker.Sprite.Green = 1f;
				yellowLocker.Axis = new Vector2(-5.41f, -4.78f);
				yellowLocker.SpinFactor = -1f;
				objectInRoom[3].Add(yellowLocker);
			}
			objectInRoom[0].Add(new BlastDoor(new Vector3(212f, 908f, 0f), true, 1));
			objectInRoom[2].Add(new BlastDoor(new Vector3(492f, 636f, 0f), true, 1));
			objectInRoom[23].Add(new GasTank(4, new Vector3(2056f, 1152f, 0f)));
			objectInRoom[3].Add(new SpringPlatform(new Vector3(1152f, 712f, 0f)));
			for (int n = 0; n < 5; n++)
			{
				objectInRoom[3].Add(new SpringPlatform(new Vector3((float)(1120 - n * 32), (float)(744 + n * 16), 0f)));
			}
			objectInRoom[3].Add(new SpringPlatform(new Vector3(992f, 888f, 0f)));
			for (int num = 0; num < 5; num++)
			{
				objectInRoom[3].Add(new SpringPlatform(new Vector3((float)(1024 + num * 32), (float)(920 + num * 16), 0f)));
			}
			for (int num2 = 0; num2 < 5; num2++)
			{
				objectInRoom[3].Add(new SpringPlatform(new Vector3((float)(1120 - num2 * 32), (float)(1016 + num2 * 16), 0f)));
			}
			for (int num3 = 0; num3 < 5; num3++)
			{
				objectInRoom[18].Add(new SpringPlatform(new Vector3((float)(1024 + num3 * 32), (float)(1112 + num3 * 16), 0f)));
			}
			for (int num4 = 0; num4 < 5; num4++)
			{
				objectInRoom[18].Add(new SpringPlatform(new Vector3((float)(1120 - num4 * 32), (float)(1208 + num4 * 16), 0f)));
			}
			for (int num5 = 0; num5 < 5; num5++)
			{
				objectInRoom[12].Add(new SpringPlatform(new Vector3((float)(888 - num5 * 32), (float)(1304 + num5 * 16), 0f)));
			}
			for (int num6 = 0; num6 < 5; num6++)
			{
				objectInRoom[12].Add(new SpringPlatform(new Vector3((float)(792 + num6 * 32), (float)(1400 + num6 * 16), 0f)));
			}
			objectInRoom[12].Add(new SpringPlatform(new Vector3(968f, 1448f, 0f)));
			objectInRoom[12].Add(new SpringPlatform(new Vector3(1016f, 1464f, 0f)));
			objectInRoom[12].Add(new SpringPlatform(new Vector3(1104f, 1464f, 0f)));
			objectInRoom[12].Add(new SpringPlatform(new Vector3(1152f, 1464f, 0f)));
			objectInRoom[9].Add(new Conveyor(24, new Vector3(640f, 1004f, -2f), 32f, -1));
			objectInRoom[9].Add(new Conveyor(24, new Vector3(352f, 1052f, -2f), 32f, -1));
			objectInRoom[20].Add(new Conveyor(16, new Vector3(488f, 1172f, -2f), 32f, 1));
			objectInRoom[11].Add(new Conveyor(12, new Vector3(600f, 1156f, -2f), 32f, -1));
			objectInRoom[11].Add(new Conveyor(8, new Vector3(728f, 1156f, -2f), 32f, 1));
			AxisAlignedRectangle axisAlignedRectangle = null;
			CrushWall crushWall = new CrushWall(new Vector3(176f, 988f, -2f), new Vector3(200f, 988f, -2f));
			objectInRoom[9].Add(crushWall);
			CrushWall crushWall2 = new CrushWall(new Vector3(288f, 988f, -2f), new Vector3(264f, 988f, -2f));
			objectInRoom[9].Add(crushWall2);
			CrushWall crushWall3 = new CrushWall(new Vector3(176f, 1020f, -2f), new Vector3(200f, 1020f, -2f));
			objectInRoom[9].Add(crushWall3);
			CrushWall crushWall4 = new CrushWall(new Vector3(288f, 1020f, -2f), new Vector3(264f, 1020f, -2f));
			objectInRoom[9].Add(crushWall4);
			crushWall.CrushingFlag = true;
			crushWall2.CrushingFlag = false;
			crushWall3.CrushingFlag = true;
			crushWall4.CrushingFlag = false;
			crushWall.TriggerArea = new AxisAlignedRectangle(24f, 4f, new Vector3(232f, 980f, 0f));
			crushWall.SetNextWall(crushWall2, 0f);
			crushWall2.SetNextWall(crushWall3, 0.7f);
			crushWall3.SetNextWall(crushWall4, 0f);
			CrushWall crushWall5 = new CrushWall(new Vector3(72f, 1056f, -2f), new Vector3(96f, 1056f, -2f));
			objectInRoom[10].Add(crushWall5);
			CrushWall crushWall6 = new CrushWall(new Vector3(72f, 1088f, -2f), new Vector3(96f, 1088f, -2f));
			objectInRoom[10].Add(crushWall6);
			CrushWall crushWall7 = new CrushWall(new Vector3(184f, 1088f, -2f), new Vector3(160f, 1088f, -2f));
			objectInRoom[10].Add(crushWall7);
			CrushWall crushWall8 = new CrushWall(new Vector3(72f, 1120f, -2f), new Vector3(96f, 1120f, -2f));
			objectInRoom[10].Add(crushWall8);
			CrushWall crushWall9 = new CrushWall(new Vector3(184f, 1120f, -2f), new Vector3(160f, 1120f, -2f));
			objectInRoom[10].Add(crushWall9);
			crushWall5.CrushingFlag = true;
			crushWall6.CrushingFlag = true;
			crushWall8.CrushingFlag = true;
			crushWall7.CrushingFlag = false;
			crushWall9.CrushingFlag = false;
			crushWall5.TriggerArea = new AxisAlignedRectangle(4f, 16f, new Vector3(136f, 1056f, 0f));
			crushWall5.SetNextWall(crushWall6, 0.7f);
			crushWall6.SetNextWall(crushWall7, 0f);
			crushWall7.SetNextWall(crushWall8, 0.7f);
			crushWall8.SetNextWall(crushWall9, 0f);
			axisAlignedRectangle = (crushWall9.WorkingArea = (crushWall8.WorkingArea = (crushWall7.WorkingArea = (crushWall6.WorkingArea = (crushWall5.WorkingArea = new AxisAlignedRectangle(112f, 128f, new Vector3(176f, 1104f, 0f)))))));
			axisAlignedRectangle = null;
			CrushWall crushWall10 = new CrushWall(new Vector3(344f, 1160f, -2f), new Vector3(344f, 1200f, -2f), 16f, 32f);
			crushWall10.SpawnRotation = new Vector3(0f, 0f, 1.57079637f);
			objectInRoom[20].Add(crushWall10);
			CrushWall crushWall11 = new CrushWall(new Vector3(376f, 1160f, -2f), new Vector3(376f, 1200f, -2f), 16f, 32f);
			crushWall11.SpawnRotation = new Vector3(0f, 0f, 1.57079637f);
			objectInRoom[20].Add(crushWall11);
			CrushWall crushWall12 = new CrushWall(new Vector3(408f, 1160f, -2f), new Vector3(408f, 1200f, -2f), 16f, 32f);
			crushWall12.SpawnRotation = new Vector3(0f, 0f, 1.57079637f);
			objectInRoom[20].Add(crushWall12);
			crushWall10.CrushingFlag = false;
			crushWall11.CrushingFlag = false;
			crushWall12.CrushingFlag = false;
			crushWall10.TriggerArea = new AxisAlignedRectangle(4f, 20f, new Vector3(336f, 1212f, 0f));
			crushWall10.SetNextWall(crushWall11, 0.5f);
			crushWall11.SetNextWall(crushWall12, 0.5f);
			axisAlignedRectangle = (crushWall12.WorkingArea = (crushWall11.WorkingArea = (crushWall10.WorkingArea = new AxisAlignedRectangle(200f, 48f, new Vector3(356f, 1184f, 0f)))));
			axisAlignedRectangle = null;
			objectInRoom[20].Add(new CrushChecker(new Vector3(376f, 1236f, 0f), 48f, 4f, true));
			AxisAlignedRectangle workingArea = new AxisAlignedRectangle(200f, 60f, new Vector3(532f, 1060f, 0f));
			objectInRoom[9].Add(new Piston(new Vector3(528f, 1068f, -2f), new Vector3(528f, 1028f, -2f), 0f, workingArea, true));
			objectInRoom[9].Add(new CrushChecker(new Vector3(528f, 1020f, 0f), 16f, 4f, false));
			objectInRoom[9].Add(new Piston(new Vector3(496f, 1084f, -2f), new Vector3(496f, 1044f, -2f), 1f, workingArea, true));
			objectInRoom[9].Add(new CrushChecker(new Vector3(496f, 1036f, 0f), 16f, 4f, false));
			objectInRoom[9].Add(new Piston(new Vector3(464f, 1100f, -2f), new Vector3(464f, 1060f, -2f), 0f, workingArea, true));
			objectInRoom[9].Add(new CrushChecker(new Vector3(464f, 1052f, 0f), 16f, 4f, false));
			workingArea = new AxisAlignedRectangle(120f, 48f, new Vector3(312f, 1184f, 0f));
			objectInRoom[20].Add(new Piston(new Vector3(280f, 1156f, -2f), new Vector3(280f, 1196f, -2f), 0f, workingArea, false));
			objectInRoom[20].Add(new CrushChecker(new Vector3(280f, 1204f, 0f), 16f, 4f, true));
			objectInRoom[20].Add(new Piston(new Vector3(312f, 1172f, -2f), new Vector3(312f, 1212f, -2f), 1f, workingArea, false));
			objectInRoom[20].Add(new CrushChecker(new Vector3(312f, 1220f, 0f), 16f, 4f, true));
			workingArea = new AxisAlignedRectangle(164f, 48f, new Vector3(600f, 1184f, 0f));
			objectInRoom[11].Add(new Piston(new Vector3(600f, 1196f, -2f), new Vector3(600f, 1164f, -2f), 0f, workingArea, true));
			workingArea = new AxisAlignedRectangle(164f, 48f, new Vector3(776f, 1184f, 0f));
			objectInRoom[11].Add(new Piston(new Vector3(776f, 1155.5f, -2f), new Vector3(776f, 1196f, -2f), 0f, workingArea, false));
			objectInRoom[11].Add(new CrushChecker(new Vector3(776f, 1204f, 0f), 16f, 4f, true));
			workingArea = new AxisAlignedRectangle(156f, 40f, new Vector3(344f, 1080f, 0f));
			objectInRoom[9].Add(new ZAxisPiston(new Vector3(380f, 1068f, -32f), workingArea, 0.75f));
			objectInRoom[9].Add(new ZAxisPiston(new Vector3(308f, 1068f, -32f), workingArea, 0f));
			workingArea = new AxisAlignedRectangle(156f, 40f, new Vector3(532f, 1188f, 0f));
			objectInRoom[20].Add(new ZAxisPiston(new Vector3(532f, 1188f, -32f), workingArea, 0f));
			objectInRoom[4].Add(new StaticAttack(new Vector3(1221f, 692f, 0f), 2f, 12f, 10, false, true, true));
			objectInRoom[14].Add(new StaticAttack(new Vector3(1568f, 1224f, 0f), 1f, 16f, 75, false, true, false));
			objectInRoom[21].Add(new StaticAttack(new Vector3(1784f, 1196f, 0f), 128f, 1f, 75, false, true, false));
			objectInRoom[22].Add(new StaticAttack(new Vector3(1976f, 1292f, 0f), 1f, 20f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(2208f, 1040f, 0f), 16f, 1f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(2008f, 1084f, 0f), 1f, 12f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(2024f, 1084f, 0f), 1f, 12f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(2016f, 1084f, 0f), 1f, 12f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(1880f, 1108f, 0f), 1f, 12f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(1896f, 1068f, 0f), 1f, 20f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(1800f, 1076f, 0f), 1f, 28f, 75, false, true, false));
			objectInRoom[23].Add(new StaticAttack(new Vector3(1856f, 1056f, 0f), 128f, 1f, 75, false, true, false));
			objectInRoom[15].Add(new StaticAttack(new Vector3(1672f, 1068f, 0f), 1f, 20f, 75, false, true, false));
			objectInRoom[15].Add(new StaticAttack(new Vector3(1640f, 1068f, 0f), 1f, 20f, 75, false, true, false));
			LaserTurret laserTurret = new LaserTurret(new Vector3(1404f, 1208f, 0f), 2);
			objectInRoom[14].Add(laserTurret);
			objectInRoom[14].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(1484f, 1248f, 0f), -2);
			objectInRoom[14].Add(laserTurret);
			objectInRoom[14].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(1644f, 1224f, 0f), 2);
			objectInRoom[21].Add(laserTurret);
			objectInRoom[21].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(1644f, 1264f, 0f), -2);
			objectInRoom[21].Add(laserTurret);
			objectInRoom[21].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(1924f, 1232f, 0f), 2);
			objectInRoom[22].Add(laserTurret);
			objectInRoom[22].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(1948f, 1256f, 0f), 2);
			objectInRoom[22].Add(laserTurret);
			objectInRoom[22].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(2084f, 1232f, 0f), 2);
			objectInRoom[22].Add(laserTurret);
			objectInRoom[22].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(1996f, 1120f, 0f), -2);
			laserTurret.RotationY = 3.14159274f;
			objectInRoom[23].Add(laserTurret);
			objectInRoom[23].Add(laserTurret.Muzzle);
			laserTurret = new LaserTurret(new Vector3(2180f, 1080f, 0f), 2);
			objectInRoom[23].Add(laserTurret);
			objectInRoom[23].Add(laserTurret.Muzzle);
			mCoreLift = new OneWayPlatform(new Vector2(32f, 8f), new Vector3(1072f, 616f, 0f), new Vector3(1072f, 136f, 0f), 50f, false, "Content/World/m_lv5_coreElevator", new Vector3(0f, 8f, -2f));
			mCoreLift.StartOnRegister = false;
			mCoreLift.Model.RelativeRotationY = -1.57079637f;
			mCoreLift.Disabled = true;
			mInnerCoreLift = new ModelObject(Vector3.Zero, Vector3.Zero, 1f, 0.5f, false, "Content/World/m_lv5_coreElevator_inner", Vector3.Zero, new Vector3(1f, 0.5f, 1f));
			mInnerCoreLift.AttachTo(mCoreLift, false);
			mInnerCoreLift.RelativePosition.Y = 7.5f;
			mInnerCoreLift.RelativePosition.Z = -2.5f;
			mInnerCoreLift.RelativeRotationY = -1.57079637f;
			mBlocker = new CharacterBlocker(40f, 8f, new Vector3(1072f, 296f, 0f));
			RoundTripPlatform roundTripPlatform = new RoundTripPlatform(new Vector2(24f, 4f), new Vector3(1696f, 1212f, 0f), new Vector3(1856f, 1212f, 0f), 40f, 0.5f, "Content/World/m_LV5_blue_hex");
			roundTripPlatform.Model.ScaleX = 3f;
			roundTripPlatform.Model.ScaleY = 0.5f;
			roundTripPlatform.Model.ScaleZ = 3f;
			objectInRoom[21].Add(roundTripPlatform);
			RoundTripPlatform roundTripPlatform2 = new RoundTripPlatform(new Vector2(24f, 4f), new Vector3(1952f, 1068f, 0f), new Vector3(1760f, 1068f, 0f), 40f, 1f, "Content/World/m_LV5_blue_hex");
			roundTripPlatform2.Model.ScaleX = 3f;
			roundTripPlatform2.Model.ScaleY = 0.5f;
			roundTripPlatform2.Model.ScaleZ = 3f;
			objectInRoom[23].Add(roundTripPlatform2);
			BlastDoor blastDoor = new BlastDoor(new Vector3(1268f, 988f, 0f), false, 0);
			RailSwitch railSwitch = new RailSwitch(new Vector3(1192f, 1072f, 0f), Vector3.Up, true, blastDoor);
			railSwitch.Tough = true;
			objectInRoom[7].Add(blastDoor);
			objectInRoom[7].Add(railSwitch);
			mZytronAvatar = new SpriteZytron().Load("PlayWorld");
			mSavedZytronAvatarBodySpriteRelPosition = new Vector3[mZytronAvatar.BodySprites.Count];
			for (int num7 = 0; num7 < mZytronAvatar.BodySprites.Count; num7++)
			{
				mSavedZytronAvatarBodySpriteRelPosition[num7] = mZytronAvatar.BodySprites[num7].RelativePosition;
			}
			mZytronPic = new Sprite();
			mZytronPic.Texture = FlatRedBallServices.Load<Texture2D>("Content/World/zytron_pic", "PlayWorld");
			mZytronPic.ScaleX = 20f;
			mZytronPic.ScaleY = 20f;
			mZytronGas = new TimedEmitParticle[mZytronAvatar.BodySprites.Count];
			mIsZytronGasDestroyed = false;
		}

		public static void AddNonRoomedObjects()
		{
			ProcessManager.AddProcess(mCoreLift);
			ProcessManager.AddProcess(mInnerCoreLift);
		}

		public static void RemoveNonRoomedObjects()
		{
			ProcessManager.RemoveProcess(mCoreLift);
			ProcessManager.RemoveProcess(mInnerCoreLift);
		}

		public static void LoadDialog(out Dialog[] dialogList)
		{
			if (Player.Instance.Type == PlayerType.Mars)
			{
				dialogList = new Dialog[1];
				dialogList[0] = new Dialog(new Chat[5]
				{
					new Chat(50, 1, new string[3]
					{
						"阿瑞斯，那就是米诺斯加农炮。",
						"要是让它完成充能，后果将不堪设想！",
						"你要想办法摧毁它。"
					}),
					new Chat(40, 1, new string[1]
					{
						"我们是不会停止的，因为我们已经做出了决定。"
					}, "Tarus_Infected"),
					new Chat(16, 2, new string[2]
					{
						"这个决定不是你们能做出的！",
						"谁都没有权利来摧毁整个星球！"
					}),
					new Chat(40, 1, new string[3]
					{
						"但我们有权利来决定对错。",
						"我们不想摧毁你，阿瑞斯。",
						"但你要是坚持妨碍我们的话，我们会把你抹消掉。"
					}),
					new Chat(18, 2, new string[2]
					{
						"这不是你第一次威胁我了。",
						"但我会让你明白，这会是你的最后一次。"
					}, "Ares_Request")
				});
			}
			else
			{
				dialogList = new Dialog[1];
				dialogList[0] = new Dialog(new Chat[1]
				{
					new Chat(27, 1, new string[1]
					{
						"啊啊，这一定就是米诺斯加农炮。"
					}, "Tarus_Confirm")
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
			ambushGroup = new AmbushGroup(12);
			ambushGroup.SpareLife = 25;
			ambushGroup.SpawnGap = 1f;
			ambushGroup.Enemies[0] = new AmbushEnemy(EnemyType.Shocker);
			ambushGroup.Enemies[0].SpawnPosition = new Vector3(-40f, 6f, 0f);
			ambushGroup.Enemies[0].FaceDirection = 1;
			ambushGroup.Enemies[1] = new AmbushEnemy(EnemyType.Shocker);
			ambushGroup.Enemies[1].SpawnPosition = new Vector3(40f, 6f, 0f);
			ambushGroup.Enemies[1].FaceDirection = -1;
			ambushGroup.Enemies[2] = new AmbushEnemy(EnemyType.Shocker);
			ambushGroup.Enemies[2].SpawnPosition = new Vector3(-96f, 16f, 0f);
			ambushGroup.Enemies[2].FaceDirection = 1;
			ambushGroup.Enemies[3] = new AmbushEnemy(EnemyType.Shocker);
			ambushGroup.Enemies[3].SpawnPosition = new Vector3(96f, 16f, 0f);
			ambushGroup.Enemies[3].FaceDirection = -1;
			ambushGroup.Enemies[4] = new AmbushEnemy(EnemyType.ZytronWalker);
			ambushGroup.Enemies[4].SpawnPosition = new Vector3(-144f, 4f, 0f);
			ambushGroup.Enemies[4].FaceDirection = 1;
			ambushGroup.Enemies[4].Actions = new EnemyAction[2]
			{
				new WalkAction(2.2f, 1),
				new WaitAction(0.5f)
			};
			ambushGroup.Enemies[5] = new AmbushEnemy(EnemyType.ZytronWalker);
			ambushGroup.Enemies[5].SpawnPosition = new Vector3(144f, 4f, 0f);
			ambushGroup.Enemies[5].FaceDirection = -1;
			ambushGroup.Enemies[5].Actions = new EnemyAction[2]
			{
				new WalkAction(2.2f, -1),
				new WaitAction(0.5f)
			};
			ambushGroup.Enemies[6] = new AmbushEnemy(EnemyType.PodShooter);
			ambushGroup.Enemies[6].SpawnPosition = new Vector3(-144f, 4f, 0f);
			ambushGroup.Enemies[6].FaceDirection = 1;
			ambushGroup.Enemies[6].Actions = new EnemyAction[2]
			{
				new WalkAction(1.6f, 1),
				new WaitAction(1f)
			};
			ambushGroup.Enemies[7] = new AmbushEnemy(EnemyType.PodShooter);
			ambushGroup.Enemies[7].SpawnPosition = new Vector3(144f, 4f, 0f);
			ambushGroup.Enemies[7].FaceDirection = -1;
			ambushGroup.Enemies[7].Actions = new EnemyAction[2]
			{
				new WalkAction(1.6f, -1),
				new WaitAction(1f)
			};
			ambushGroup.Enemies[8] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[8].SpawnPosition = new Vector3(-52f, 24f, 0f);
			ambushGroup.Enemies[8].FaceDirection = 1;
			ambushGroup.Enemies[8].Actions = new EnemyAction[3]
			{
				new MoveToAction(2f, new Vector3(-52f, 0f, 0f), new Vector3(-0.5f, -0.5f, 0f)),
				new TurnToAction(1),
				new MoveToAction(0.6f, new Vector3(24f, 0f, 0f), Vector3.Zero)
			};
			ambushGroup.Enemies[9] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[9].SpawnPosition = new Vector3(52f, 24f, 0f);
			ambushGroup.Enemies[9].FaceDirection = -1;
			ambushGroup.Enemies[9].Actions = new EnemyAction[3]
			{
				new MoveToAction(1.5f, new Vector3(52f, 0f, 0f), new Vector3(-0.5f, -0.5f, 0f)),
				new TurnToAction(-1),
				new MoveToAction(0.6f, new Vector3(-24f, 0f, 0f), Vector3.Zero)
			};
			ambushGroup.Enemies[10] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[10].SpawnPosition = new Vector3(-52f, 24f, 0f);
			ambushGroup.Enemies[10].FaceDirection = 1;
			ambushGroup.Enemies[10].Actions = new EnemyAction[3]
			{
				new MoveToAction(1f, new Vector3(-52f, 12f, 0f), new Vector3(-0.5f, -0.5f, 0f)),
				new TurnToAction(1),
				new MoveToAction(0.6f, new Vector3(24f, 12f, 0f), Vector3.Zero)
			};
			ambushGroup.Enemies[11] = new AmbushEnemy(EnemyType.Bomber);
			ambushGroup.Enemies[11].SpawnPosition = new Vector3(52f, 24f, 0f);
			ambushGroup.Enemies[11].FaceDirection = -1;
			ambushGroup.Enemies[11].Actions = new EnemyAction[3]
			{
				new MoveToAction(0.5f, new Vector3(52f, 12f, 0f), new Vector3(-0.5f, -0.5f, 0f)),
				new TurnToAction(-1),
				new MoveToAction(0.6f, new Vector3(-24f, 12f, 0f), Vector3.Zero)
			};
			ambushSpawnerList[0].Groups[0] = ambushGroup;
		}

		private static void Ambush1Start()
		{
			if (mHasCompletedMinosSurvival)
			{
				EnemyManager.CancelAmbush();
			}
			else
			{
				CollisionManager.AddLevelCollider(mDoor);
				CollisionManager.AddLevelCollider(mDoor2);
				SpriteManager.AddSprite(mDoorSprite);
				SpriteManager.AddSprite(mDoorSprite2);
			}
		}

		private static void Ambush1Cancel()
		{
			CollisionManager.RemoveLevelCollider(mDoor);
			CollisionManager.RemoveLevelCollider(mDoor2);
			SpriteManager.RemoveSpriteOneWay(mDoorSprite);
			SpriteManager.RemoveSpriteOneWay(mDoorSprite2);
		}

		private static void Ambush1Finish()
		{
			CollisionManager.RemoveLevelCollider(mDoor);
			CollisionManager.RemoveLevelCollider(mDoor2);
			SpriteManager.RemoveSprite(mDoorSprite);
			SpriteManager.RemoveSprite(mDoorSprite2);
			MinosSurvivalEnd();
		}

		public static void CoreOpening1()
		{
			World.Camera.Position = new Vector3(280f, 924f, 102f);
			World.Play(0, true);
			Player.Instance.Spawn(new Vector3(184f, 904f, 0f));
			Player.Instance.StateMachineEnabled = false;
			Player.Instance.ControlEnabled = false;
			Player.Instance.SpriteRig.SetPose("run_idle", 0.0);
			Director.WaitForSeconds(1f, CoreOpening2, false);
		}

		private static void CoreOpening2()
		{
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				0,
				2
			}, new object[2]
			{
				new Vector3(264f, 904f, 0f),
				"run_idle"
			}, 0f, 1.6f, CoreOpening3);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void CoreOpening3()
		{
			Player.Instance.StateMachineEnabled = true;
			Player.Instance.ControlEnabled = true;
			Player.Instance.ChangeToUnStuckStage();
			SFXManager.StopLoopSound();
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
		}

		private static void CoreOpening4()
		{
			StatusUI.Instance.Show();
			MaterialUI.Instance.Show();
		}

		public static void MinoscoreSurvival()
		{
		}

		public static void MinosSurvivalEnd()
		{
			mHasCompletedMinosSurvival = true;
			Player.Instance.ControlEnabled = false;
			Director.WaitForSeconds(1f, MinosSurvivalEnd2, false);
		}

		public static void MinosSurvivalEnd2()
		{
			ProcessManager.AddProcess(redPlatform1);
			World.AddObjectToRoom(redPlatform1, 5);
			SFXManager.PlaySound("PlatformAppear");
			OnceEmitParticlePool.Emit(redPlatform1.SpawnPosition, "recycle");
			Director.WaitForSeconds(0.5f, MinosSurvivalEnd3, false);
		}

		public static void MinosSurvivalEnd3()
		{
			ProcessManager.AddProcess(redPlatform2);
			World.AddObjectToRoom(redPlatform2, 5);
			SFXManager.PlaySound("PlatformAppear");
			OnceEmitParticlePool.Emit(redPlatform2.SpawnPosition, "recycle");
			Director.WaitForSeconds(0.5f, MinosSurvivalEnd4, false);
		}

		public static void MinosSurvivalEnd4()
		{
			ProcessManager.AddProcess(redPlatform3);
			World.AddObjectToRoom(redPlatform3, 5);
			SFXManager.PlaySound("PlatformAppear");
			OnceEmitParticlePool.Emit(redPlatform3.SpawnPosition, "recycle");
			Player.Instance.ControlEnabled = true;
		}

		public static void ControlRoomOpening()
		{
			if (!HasUnlockedMinoscore[0])
			{
				magentaDoor.Activated = false;
				Player.Instance.ControlEnabled = false;
				Director.WaitForSeconds(0.7f, ControlRoomOpening2, false);
			}
		}

		public static void ControlRoomOpening2()
		{
			World.Camera.TargetingTransform(new Vector3(912f, 644f, 102f), 1f, true);
			Director.WaitForSeconds(3f, ControlRoomOpening3, false);
		}

		public static void ControlRoomOpening3()
		{
			World.Camera.TargetingTransform(new Vector3(584f, 712f, 116f), 0.6f, true);
			Director.WaitForSeconds(2.6f, ControlRoomOpening4, false);
		}

		public static void ControlRoomOpening4()
		{
			World.Camera.TransformToSideScrollingMode(0.5f);
			Player.Instance.ControlEnabled = true;
		}

		public static void CoreMagentaUnlock()
		{
			HasUnlockedMinoscore[0] = true;
			magentaUnlocker.Unlock();
			Player.Instance.ControlEnabled = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1.5f, CoreMagentaUnlock2, false);
		}

		public static void CoreMagentaUnlock2()
		{
			World.Camera.TargetingTransform(new Vector3(904f, 648f, 150f), 0.5f, true);
			Director.WaitForSeconds(0.7f, CoreMagentaUnlock3, false);
		}

		public static void CoreMagentaUnlock3()
		{
			magentaLocker.Unlock();
			Director.WaitForSeconds(2f, CoreMagentaUnlock4, false);
		}

		public static void CoreMagentaUnlock4()
		{
			World.Camera.TransformToSideScrollingMode(0.5f);
			Director.WaitForSeconds(0.7f, CoreMagentaUnlock5, false);
		}

		public static void CoreMagentaUnlock5()
		{
			magentaDoor.Activated = true;
			Player.Instance.ControlEnabled = true;
			World.Camera.Mode = CameraMode.SideScrolling;
		}

		public static bool CheckAllUnlock()
		{
			for (int i = 1; i < 5; i++)
			{
				if (!HasUnlockedMinoscore[i])
				{
					return false;
				}
			}
			mCoreC.RotationZVelocity = -0.5f;
			mCoreCW.RotationZVelocity = 0.5f;
			World.Camera.Shake(1, 2f);
			SFXManager.PlaySound("shake");
			for (int num = mCoreCCList.Length - 1; num >= 0; num--)
			{
				mCoreCCList[num].RotationYVelocity = 0.2f;
			}
			for (int num2 = mCoreCWList.Length - 1; num2 >= 0; num2--)
			{
				mCoreCWList[num2].RotationYVelocity = -0.2f;
			}
			return true;
		}

		public static void CoreRedUnlock()
		{
			HasUnlockedMinoscore[1] = true;
			redUnlocker.Unlock();
			Player.Instance.ControlEnabled = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1.5f, CoreRedUnlock2, false);
		}

		public static void CoreRedUnlock2()
		{
			Director.FadeCameraTo(new Vector3(1072f, 648f, 150f), 1f, CoreRedUnlock3);
		}

		public static void CoreRedUnlock3()
		{
			if (redLocker != null)
			{
				redLocker.Unlock();
			}
			if (redDoorLocker != null)
			{
				redDoorLocker.Unlock();
			}
			if (CheckAllUnlock())
			{
				Director.WaitForSeconds(4f, CoreRedUnlock4, false);
			}
			else
			{
				Director.WaitForSeconds(2f, CoreRedUnlock4, false);
			}
		}

		public static void CoreRedUnlock4()
		{
			Director.FadeCameraToSideScrollingMode(1f, CoreRedUnlock5);
		}

		public static void CoreRedUnlock5()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			Player.Instance.ControlEnabled = true;
			redDoor.Activated = true;
			World.SaveCheckPoint(new Vector3(780f, 936f, 0f));
			ProfileManager.BeginSave(null, 1, false);
		}

		public static void CoreGreenUnlock()
		{
			HasUnlockedMinoscore[2] = true;
			greenUnlocker.Unlock();
			World.LoadRoomByID(3, true);
			Player.Instance.ControlEnabled = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1.5f, CoreGreenUnlock2, false);
		}

		public static void CoreGreenUnlock2()
		{
			Director.FadeCameraTo(new Vector3(1072f, 648f, 150f), 1f, CoreGreenUnlock3);
		}

		public static void CoreGreenUnlock3()
		{
			if (greenLocker != null)
			{
				greenLocker.Unlock();
			}
			if (CheckAllUnlock())
			{
				Director.WaitForSeconds(4f, CoreGreenUnlock4, false);
			}
			else
			{
				Director.WaitForSeconds(2f, CoreGreenUnlock4, false);
			}
		}

		public static void CoreGreenUnlock4()
		{
			Director.FadeCameraToSideScrollingMode(1f, CoreGreenUnlock5);
		}

		public static void CoreGreenUnlock5()
		{
			greenHex1.Destroy();
			greenHex2.Destroy();
			greenHex3.Destroy();
			greenHex4.Destroy();
			World.Camera.Mode = CameraMode.SideScrolling;
			Player.Instance.ControlEnabled = true;
			World.SaveCheckPoint(new Vector3(936f, 1152f, 0f));
			ProfileManager.BeginSave(null, 1, false);
			World.UnLoadRoomByID(3, true);
		}

		public static void CoreBlueUnlock()
		{
			HasUnlockedMinoscore[3] = true;
			blueUnlocker.Unlock();
			World.LoadRoomByID(3, true);
			Player.Instance.ControlEnabled = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1.5f, CoreBlueUnlock2, false);
		}

		public static void CoreBlueUnlock2()
		{
			Director.FadeCameraTo(new Vector3(1072f, 648f, 150f), 1f, CoreBlueUnlock3);
		}

		public static void CoreBlueUnlock3()
		{
			if (blueLocker != null)
			{
				blueLocker.Unlock();
			}
			if (CheckAllUnlock())
			{
				Director.WaitForSeconds(4f, CoreBlueUnlock4, false);
			}
			else
			{
				Director.WaitForSeconds(2f, CoreBlueUnlock4, false);
			}
		}

		public static void CoreBlueUnlock4()
		{
			Director.FadeCameraToSideScrollingMode(1f, CoreBlueUnlock5);
		}

		public static void CoreBlueUnlock5()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			Player.Instance.ControlEnabled = true;
			World.UnLoadRoomByID(3, true);
			World.SaveCheckPoint(new Vector3(2240f, 1064f, 0f));
			ProfileManager.BeginSave(null, 1, false);
		}

		public static void CoreYellowUnlock()
		{
			HasUnlockedMinoscore[4] = true;
			yellowUnlocker.Unlock();
			World.LoadRoomByID(3, true);
			Player.Instance.ControlEnabled = false;
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(1.5f, CoreYellowUnlock2, false);
		}

		public static void CoreYellowUnlock2()
		{
			Director.FadeCameraTo(new Vector3(1072f, 648f, 150f), 1f, CoreYellowUnlock3);
		}

		public static void CoreYellowUnlock3()
		{
			if (yellowLocker != null)
			{
				yellowLocker.Unlock();
			}
			if (CheckAllUnlock())
			{
				Director.WaitForSeconds(4f, CoreYellowUnlock4, false);
			}
			else
			{
				Director.WaitForSeconds(2f, CoreYellowUnlock4, false);
			}
		}

		public static void CoreYellowUnlock4()
		{
			Director.FadeCameraToSideScrollingMode(1f, CoreYellowUnlock5);
		}

		public static void CoreYellowUnlock5()
		{
			World.Camera.Mode = CameraMode.SideScrolling;
			Player.Instance.ControlEnabled = true;
			World.UnLoadRoomByID(3, false);
			World.SaveCheckPoint(new Vector3(1240f, 1488f, 0f));
			ProfileManager.BeginSave(null, 1, false);
		}

		public static void CoreAccess()
		{
			for (int i = 1; i < 5; i++)
			{
				if (!HasUnlockedMinoscore[i])
				{
					return;
				}
			}
			ProcessManager.DoNotPause = true;
			Player.Instance.ControlEnabled = false;
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(0.9f, CoreAccess2, false);
		}

		private static void CoreAccess2()
		{
			World.Camera.Shake(1, 2f);
			SFXManager.PlaySound("New_HeavyElevatorStart");
			mCoreLift.Disabled = false;
			mCoreLift.SetSpeed(50f);
			mCoreLift.Start();
			Director.WaitForSeconds(1f, CoreAccess3, false);
		}

		private static void CoreAccess3()
		{
			Player.Instance.StateMachineEnabled = false;
		}

		public static void BossTheCore1()
		{
			Apollo.Instance.Dismiss();
			ProcessManager.DoNotPause = false;
			Player.Instance.ControlEnabled = false;
			Vector3 position = new Vector3(1072f, 192f, -60f);
			mTheCore = (EnemyManager.AcquireEnemy(EnemyType.TheCore, position, -1, false) as ARES360.Entity.TheCore);
			mTheCore.AcquireCoreTurretsAndWeapons();
			Player.Instance.ChangeToUnStuckStage();
			Director.WaitForSeconds(1f, BossTheCore1_0, false);
		}

		private static void BossTheCore1_0()
		{
			Player.Instance.StateMachineEnabled = false;
			float num = 1072f - Player.Instance.X;
			if (Player.Instance.FaceDirection == -1 && num > 0f)
			{
				Player.Instance.FaceDirection = 1;
			}
			else if (Player.Instance.FaceDirection == 1 && num < 0f)
			{
				Player.Instance.FaceDirection = -1;
			}
			Director.PlayerTweener.Tween(Player.Instance, new int[2]
			{
				1,
				2
			}, new object[2]
			{
				(Player.Instance.FaceDirection == 1) ? 40f : (-40f),
				"run_idle"
			}, 0f, Math.Abs(num) / 40f, BossTheCore1_1);
			SFXManager.PlayLoopSound("run", 99f, 0.4f);
		}

		private static void BossTheCore1_1()
		{
			Player.Instance.SpriteRig.SetPose("stand_idle", 0.0);
			Player.Instance.FaceDirection = 1;
			Player.Instance.StateMachineEnabled = true;
			SFXManager.StopLoopSound();
			BGMManager.FadeOff();
		}

		public static void BossTheCore_ReachCore1()
		{
			mCoreLift.YAcceleration = 50f;
			Director.WaitForSeconds(1f, BossTheCore_ReachCore2, false);
		}

		private static void BossTheCore_ReachCore2()
		{
			SFXManager.PlaySound("New_SlowCrusherStop");
			World.Camera.Shake(1, 0.5f);
			mCoreLift.YAcceleration = 0f;
			Vector3 destination = new Vector3(1072f, 136f, -2f);
			mCoreLift.Model.Detach();
			mCoreLift.Reset(mCoreLift.Position, destination, 30f);
			mTheCore.Opening();
			Director.WaitForSeconds(7.5f, BossTheCore_ReachCore3, false);
		}

		private static void BossTheCore_ReachCore3()
		{
			ProcessManager.AddProcess(mBlocker);
			Player.Instance.ControlEnabled = true;
			BossTheCore1_2();
		}

		private static void BossTheCore1_2()
		{
			Player.Instance.ControlEnabled = false;
			SpriteRigManager.AddSpriteRig(mZytronAvatar);
			mZytronAvatar.BodySprites.Visible = false;
			mZytronAvatar.Root.Position = new Vector3(1100f, 144f, 0f);
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
			mZytronGas = new TimedEmitParticle[mZytronAvatar.BodySprites.Count];
			for (int j = 0; j < mZytronAvatar.BodySprites.Count; j++)
			{
				mZytronGas[j] = TimedEmitParticlePool.Summon("zytron", mZytronAvatar.BodySprites[j], -1f);
			}
			SFXManager.PlaySound("gas");
			World.Camera.Shake(1, 1f);
			Director.WaitForSeconds(1.5f, BossTheCore2, false);
		}

		private static void BossTheCore2()
		{
			mZytronPic.AlphaRate = 0f;
			World.Camera.Mode = CameraMode.SideScrolling;
			mTheCore.Activate();
			World.ShowDialogByID(0, BossTheCore3);
			BGMManager.Play(2);
		}

		private static void BossTheCore3()
		{
			World.Camera.Mode = CameraMode.Cinematic;
			Director.WaitForSeconds(0.8f, BossTheCore4, false);
		}

		private static void BossTheCore4()
		{
			mZytronPic.AlphaRate = -1f;
			foreach (Sprite bodySprite in mZytronAvatar.BodySprites)
			{
				bodySprite.RelativeXAcceleration = (float)((bodySprite.X > mZytronAvatar.Root.X) ? (-10) : 10);
			}
			mZytronAvatar.Root.YAcceleration = 50f;
			SFXManager.PlaySound("gas");
			BGMManager.FadeOff();
			Director.WaitForSeconds(2f, BossTheCore4_2, false);
		}

		private static void BossTheCore4_2()
		{
			BGMManager.Play(1);
			Director.WaitForSeconds(2f, BossTheCore5, false);
		}

		private static void BossTheCore5()
		{
			TimedEmitParticle[] array = mZytronGas;
			foreach (TimedEmitParticle timedEmitParticle in array)
			{
				timedEmitParticle.Destroy();
			}
			mZytronAvatar.Destroy();
			SpriteManager.RemoveSpriteOneWay(mZytronPic);
			mTheCore.Start();
			Player.Instance.ControlEnabled = true;
		}

		public static void CoreEnding()
		{
			Player.Instance.ControlEnabled = false;
			Director.WaitForSeconds(1f, CoreEnding1_2, false);
		}

		private static void CoreEnding1_2()
		{
			Director.WaitForSeconds(1f, CoreEnding1_4, false);
		}

		private static void CoreEnding1_4()
		{
			Player.Instance.ControlEnabled = false;
			mTheCore.End();
			Director.WaitForSeconds(1f, CoreEnding2, false);
		}

		private static void CoreEnding2()
		{
			Player.Instance.StateMachineEnabled = false;
			SpriteRigManager.AddSpriteRig(mZytronAvatar);
			mZytronAvatar.BodySprites.Visible = false;
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
			mZytronPic.Alpha = 0f;
			mZytronPic.AlphaRate = 0.4f;
			SpriteManager.AddSprite(mZytronPic);
			SFXManager.PlaySound("gas");
			for (int j = 0; j < mZytronAvatar.BodySprites.Count; j++)
			{
				mZytronGas[j] = TimedEmitParticlePool.Summon("zytron", mZytronAvatar.BodySprites[j], -1f);
			}
			if (Player.Instance.FaceDirection == -1)
			{
				mZytronAvatar.Root.Position = Player.Instance.Position + new Vector3(-28f, 0f, 0f);
				mZytronAvatar.Root.RotationY = 3.14159274f;
				mZytronPic.RotationY = 3.14159274f;
				mZytronPic.Position = mZytronAvatar.Root.Position + new Vector3(-0.7f, 14.5f, 0.1f);
			}
			else
			{
				mZytronAvatar.Root.Position = Player.Instance.Position + new Vector3(28f, 0f, 0f);
				mZytronPic.Position = mZytronAvatar.Root.Position + new Vector3(0.7f, 14.5f, 0.1f);
			}
			Director.WaitForSeconds(1.5f, CoreEnding3, false);
		}

		private static void CoreEnding3()
		{
			mZytronPic.AlphaRate = 0f;
			Player.Instance.ControlEnabled = false;
			Director.WaitForSeconds(3f, CoreEnding4, false);
		}

		private static void CoreEnding4()
		{
			Player.Instance.ControlEnabled = false;
			Director.WaitForSeconds(0.5f, CoreEnding5, false);
		}

		private static void CoreEnding5()
		{
			mTheCore.GotoCinematic();
			Director.WaitForSeconds(1.5f, CoreEnding6, false);
		}

		private static void CoreEnding6()
		{
			Director.WaitForSeconds(0.5f, CoreEnding7, false);
		}

		private static void CoreEnding7()
		{
			PlayScreen.Instance.CompleteLevel(true);
		}

		private static void DestroyZytronGasAvatar()
		{
			TimedEmitParticle[] array = mZytronGas;
			for (int i = 0; i < array.Length; i++)
			{
				array[i]?.Destroy();
			}
			mZytronAvatar.Destroy();
			SpriteManager.RemoveSprite(mZytronPic);
		}

		public static void Destroy()
		{
			HasUnlockedMinoscore = null;
			mTheCore = null;
			redPlatform1 = null;
			redPlatform2 = null;
			redPlatform3 = null;
			mCoreC = null;
			mCoreCW = null;
			mCoreCCList = null;
			mCoreCWList = null;
			magentaDoor = null;
			magentaLocker = null;
			magentaUnlocker = null;
			redDoor = null;
			redDoorLocker = null;
			redLocker = null;
			redUnlocker = null;
			mDoor = null;
			mDoor2 = null;
			mDoorSprite = null;
			mDoorSprite2 = null;
			greenHex1 = null;
			greenHex2 = null;
			greenHex3 = null;
			greenHex4 = null;
			greenLocker = null;
			greenUnlocker = null;
			blueLocker = null;
			blueUnlocker = null;
			yellowLocker = null;
			yellowUnlocker = null;
			mCoreLift = null;
			mInnerCoreLift = null;
			mBlocker = null;
			if (!mIsZytronGasDestroyed)
			{
				DestroyZytronGasAvatar();
			}
			mZytronAvatar = null;
			mZytronPic = null;
			mZytronGas = null;
			mSavedZytronAvatarBodySpriteRelPosition = null;
		}
	}
}
