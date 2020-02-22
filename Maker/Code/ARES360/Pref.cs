using Microsoft.Xna.Framework;
using System;
using System.IO;

namespace ARES360
{
	public static class Pref
	{
		public const string VERSION = "v.0.10";

		public const short MaxProcessCount = 512;

		public const float CameraYOffset = 12f;

		public const int FallbackWidth = 1920;

		public const int FallbackHeight = 1080;

		public const byte MaxParticleType = 128;

		public const byte ParticlePoolCap = 128;

		public const byte MaxManagedParticle = 128;

		public const short MaxLevelCollider = 512;

		public const short MaxGameObject = 512;

		public const byte MaxAttackCollider = 128;

		public const byte MaxEnemyPart = 128;

		public const float PlayerDamageTime = 0.25f;

		public const float PlayerBlinkLastingTime = 1.5f;

		public const float PlayerBlinkTime = 0.05f;

		public const float PlayerKnockBackTime = 0.2f;

		public const byte MaxTrigger = 128;

		public const byte MaxEnemy = 20;

		public const float EnemySpawnRangeOffset = 56f;

		public const float EnemySpawnRingWidth = 16f;

		public const float EnemyDespawnRange = 72f;

		public const float MiniBossExtendedBlinkTime = 0.1f;

		public const float BossExtendedBlinkTime = 0.2f;

		public const byte MaxTurretBullet = 4;

		public const byte MaxSentryBullet = 12;

		public const byte MaxPodShot = 5;

		public const byte MaxDroppedMaterial = 32;

		public const byte MaxSpawnEnemiesPerFrame = 2;

		public const byte MaxProcessingLULTransformPerFrame = 32;

		public const float AnalogTriggerSignalMagnitude = 0.3f;

		public const float AnalogStickSignalMagnitude = 0.45f;

		public const float AnalogStickSignalSquaredMagnitude = 0.2025f;

		public const byte MaxControlNumber = 32;

		public const byte MaxInputPerControl = 4;

		public const bool UseVibration = true;

		public const int MaxTransformCountPerFrame = 50;

		public const float BlastDoorOpenRange = 50f;

		public const float BlastDoorCloseRange = 12f;

		public const float BlastDoorOpenVelocity = 40f;

		public const float ElevatorMoveSpeed = 48f;

		public const float BreakableLevelBlinkTime = 0.05f;

		public const int BoxHP = 40;

		public const int ExplosiveBoxDamageToPlayer = 20;

		public const int ExplosiveBoxDamageToEnemy = 100;

		public const int BoxDropDamage = 20;

		public const byte BoxDropperPoolCap = 7;

		public const byte MaxConveyorObject = 16;

		public const float JunkDropRate = 1f;

		public const float JunkDropLength = 0.5f;

		public const float JunkBaseSpeed = 105f;

		public const float JunkSpeedRange = 10f;

		public const float FireAnimationLastingTime = 0.5f;

		public const byte MaxMarsBulletCount = 20;

		public const byte MaxTarusBulletCount = 5;

		public const float GlobalGravity = -500f;

		public const float PlayerMoveSpeed = 50f;

		public const float PlayerJumpStartVelocity = 163.299316f;

		public const float PlayerSecondJumpStartVelocity = 130.63945f;

		public const float GrenadeChargeTime = 2f;

		public const float DashLastingTime = 0.45f;

		public const float AddingDashSpeed = 50f;

		public const byte MaxSplashHitObject = 8;

		public const byte MaxWaveCount = 8;

		public const byte MaxMarsSoul = 12;

		public const byte MaxSoulHitPerEnemy = 3;

		public const byte DataCubeNumber = 32;

		public const byte MaxDialogLines = 4;

		public const ulong TITLE_ID = 1480659949uL;

		public const byte MAX_CHARACTER_RANK = 6;

		public const byte RANK_D = 0;

		public const byte RANK_C = 1;

		public const byte RANK_B = 2;

		public const byte RANK_A = 3;

		public const byte RANK_S = 4;

		public const byte RANK_SS = 5;

		public const sbyte DIRECTION_LEFT = -1;

		public const sbyte DIRECTION_RIGHT = 1;

		public const sbyte DIRECTION_DOWN = -2;

		public const sbyte DIRECTION_UP = 2;

		public const int ACHIEVEMENT_COUNT = 31;

		public const string GAME_STORAGEDEVICE_CONTAINER_NAME = "A.R.E.S.";

		public const int SAVE_VERSION = 5;

		public const string CONFIGS_FILENAME = "configs.ini";

		public const string ACHIEVEMENT_FILENAME = "game.ach";

		public const int MAX_COMBO_HITS = 999;

		public const float COMBO_IDLE_TIME = 5f;

		public const bool LEVIATHAN_DEAD_ALWAYS_DAMAGE_MARS_ARMOR = true;

		public const bool LOAD_NEXT_SCREEN_DURING_CUT_SCENE = false;

		public const string DEFAULT_GAMER_KEY = "player";

		public const string KEYBOARD_FILENAME = "keyboard.configs";

		public const string XINPUT_FILENAME = "xinput.configs";

		public static Vector2 CameraStiffness = new Vector2(2000f, 1800f);

		public static Vector2 CameraDamping = new Vector2(10f, 10f);

		public static float CameraMass = 10f;

		public static float PlayerOfOutCamDeadTime = 0.8f;

		public static int Width = 1280;

		public static int Height = 720;

		public static bool FullScreen = false;

		public static float DynamicBoundingCameraYOffset = 12f;

		public static float DynamicBoundingCameraStiffness = 2000f;

		public static float DynamicBoundingCameraDamping = 50f;

		public static float MaxPlayerFallVelocity = -130f;

		public static bool IsFoundCarrion = false;

		public static float[] GMF_BOSS_DAMAGE_MULTIPLIER = new float[3]
		{
			0.5f,
			1f,
			1.5f
		};

		public static float[] GMF_BOSS_HP_MULTIPLIER = new float[3]
		{
			1f,
			1f,
			2f
		};

		public static float[] GMF_ENEMY_DAMAGE_MULTIPLIER = new float[3]
		{
			1f,
			1f,
			1.5f
		};

		public static float[] GMF_ENEMY_HP_MULTIPLIER = new float[3]
		{
			1f,
			1f,
			2f
		};

		public static float[] GMF_PLAYER_REPAIR_COOLDOWN_MULTIPLIER = new float[3]
		{
			0.5f,
			1f,
			2f
		};

		public static float WAIT_TIME_FOR_SIGNIN_AFTER_SIGNOUT = 2f;

		public static string COMBAT_DIFFICULTY_TEXT_CASUAL = "休闲";

		public static string COMBAT_DIFFICULTY_TEXT_NORMAL = "普通";

		public static string COMBAT_DIFFICULTY_TEXT_HARDCORE = "硬核";

		public static bool Steamworks = false;

		public static bool HasApolloDLC = false;

		public static int NativeScreenWidth;

		public static int NativeScreenHeight;

		public static string GP_MENU = "Menu";

		public static string GP_TARUS_LV1 = "tarus_lv1";

		public static string GP_TARUS_LV2 = "tarus_lv2";

		public static string GP_TARUS_LV3 = "tarus_lv3";

		public static string GP_TARUS_LV4 = "tarus_lv4";

		public static string GP_TARUS_LV5 = "tarus_lv5";

		public static string GP_TARUS_LV6 = "tarus_lv6";

		public static string GP_TARUS_LV7 = "tarus_lv7";

		public static string GP_ARES_LV1 = "ares_lv1";

		public static string GP_ARES_LV2 = "ares_lv2";

		public static string GP_ARES_LV3 = "ares_lv3";

		public static string GP_ARES_LV4 = "ares_lv4";

		public static string GP_ARES_LV5 = "ares_lv5";

		public static string GP_ARES_LV6 = "ares_lv6";

		public static string GP_ARES_LV7 = "ares_lv7";

		public static string GetProfileDirectory()
		{
			return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "ARES_EX");
		}

		public static string GetGamerFileName(string gamerKey)
		{
			if (gamerKey == null || gamerKey == "")
			{
				return string.Format("{0}/save.profile", "player");
			}
			return $"{gamerKey}/save.profile";
		}

		public static string GetDirectInputFileName(string deviceKey)
		{
			if (deviceKey == null)
			{
				return "direct-null.configs";
			}
			return $"direct-{deviceKey.ToLower()}.configs";
		}
	}
}
