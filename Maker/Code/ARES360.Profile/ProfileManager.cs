using ARES360.Audio;
using ARES360.Entity;
using ARES360.Input;
using ARES360.UI;
using FlatRedBall;
using Microsoft.Xna.Framework.GamerServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace ARES360.Profile
{
	public static class ProfileManager
	{
		public const byte SAVE_OPTION_PROFILE = 1;

		public const byte SAVE_OPTION_CONFIGS = 2;

		public const byte SAVE_OPTION_ACHIEVEMENT = 4;

		public const byte SAVE_OPTION_ALL = 7;

		public static PlayerProfile Mars;

		public static PlayerProfile Tarus;

		public static GamerProfile Profile;

		public static Configs Configs;

		public static PlayerProfile Current;

		public static SignedInGamer CurrentSignedInGamer;

		private static bool mIsTrialMode;

		private static AsyncCallback mSavingCallback;

		private static Thread mSavingThread;

		private static byte mSaveOptions = 0;

		private static bool mIsRequestToSaveThisFrame = false;

		private static bool mIsShowLoadingUIForSaving = false;

		private static bool mSavingSuccessThisFrame = false;

		private static float mSaveTime = 0f;

		private static float mDelaySaveIconCountDownTimer = 0f;

		private static Action mStorageAvailableToSaveCallback;

		private static Action mContinueWithoutSavingCallback;

		public static bool IsTrialMode => false;

		public static int LastPlayedLevel
		{
			get
			{
				if (Current == null)
				{
					if (Profile.LastPlayedCharacter == PlayerType.Mars)
					{
						return Mars.CurrentLevel;
					}
					if (Profile.LastPlayedCharacter == PlayerType.Tarus)
					{
						return Tarus.CurrentLevel;
					}
					return 0;
				}
				return Current.CurrentLevel;
			}
		}

		public static GameCombatDifficulty CombatDifficulty
		{
			get
			{
				if (Current == null)
				{
					return GameCombatDifficulty.Normal;
				}
				return Current.CombatDifficulty;
			}
			set
			{
				if (Current == null)
				{
					Mars.CombatDifficulty = value;
					Tarus.CombatDifficulty = value;
				}
				else
				{
					Current.CombatDifficulty = value;
				}
			}
		}

		public static void ApplyConfigs()
		{
			SFXManager.Volume = Configs.RawFXVolume;
			BGMManager.Volume = Configs.RawMusicVolume;
			GamePad.EnabledVibration = Configs.IsVibrateEnable;
		}

		public static void Initialize()
		{
			mIsTrialMode = true;
		}

		public static void UpdateIsTrialModeFlag()
		{
		}

		public static void Load()
		{
			Profile = new GamerProfile();
			Mars = new PlayerProfile();
			Mars.PlayerType = PlayerType.Mars;
			Mars.AcquiredWeapon = new Dictionary<int, int>(4);
			Mars.AcqiuredUpgrade = new Dictionary<int, int>(12);
			Mars.AcquiredChip = new Dictionary<int, List<int>>(7);
			for (int i = 1; i <= 7; i++)
			{
				Mars.AcquiredChip[i] = new List<int>(3);
			}
			Mars.Score = new short[7];
			Tarus = new PlayerProfile();
			Tarus.PlayerType = PlayerType.Tarus;
			Tarus.AcquiredWeapon = new Dictionary<int, int>(4);
			Tarus.AcqiuredUpgrade = new Dictionary<int, int>(12);
			Tarus.AcquiredChip = new Dictionary<int, List<int>>(7);
			for (int j = 1; j <= 7; j++)
			{
				Tarus.AcquiredChip[j] = new List<int>(3);
			}
			Tarus.Score = new short[7];
			ResetProfiles();
		}

		private static void ResetMarsProfile()
		{
			Mars.ArmorLevel = 0;
			for (int i = 0; i < 7; i++)
			{
				Mars.Score[i] = 0;
			}
			Mars.CurrentLevel = 1;
			Mars.UnlockedLevel = 0;
			Mars.AcquiredWeapon[0] = 1;
			Mars.AcquiredWeapon[1] = 0;
			Mars.AcquiredWeapon[2] = 0;
			Mars.AcquiredWeapon[3] = 0;
			for (int j = 0; j < 12; j++)
			{
				Mars.AcqiuredUpgrade[j] = 0;
			}
			Mars.AcqiuredUpgrade[0] = 1;
			Mars.AcqiuredUpgrade[4] = 1;
			Mars.AcqiuredUpgrade[8] = 1;
			for (int k = 1; k <= 7; k++)
			{
				Mars.AcquiredChip[k].Clear();
			}
			Mars.EquipedWeapon = WeaponType.ZytronBlaster;
			Mars.SpecialLevel = 1;
			Mars.RepairLevel = 0;
			Mars.BlastLevel = 0;
			Mars.ShockLevel = 0;
			Mars.BoostLevel = 0;
			Mars.DashLevel = 1;
			Mars.ProtectionLevel = 0;
			Mars.TutorialBasic = false;
			Mars.TutorialSpecial = false;
			Mars.TutorialBlast = false;
			Mars.TutorialData = false;
			Mars.TutorialFan = false;
			Mars.TutorialBoost = false;
			Mars.TutorialShield = false;
			Mars.TutorialUpgrade = false;
			Mars.PlayTime = 0f;
			Mars.Rank = 1;
			Mars.CombatDifficulty = GameCombatDifficulty.Normal;
			Mars.Experience = 0;
			Mars.ResetHasShownCutscenes();
			Mars.AcquireBlastUpgradeFromMK2 = false;
			Mars.AcquireShockUpgradeFromOculus = false;
			Mars.AcquireSpecialUpgradeFromBad = false;
		}

		private static void ResetTarusProfile()
		{
			Tarus.ArmorLevel = 0;
			for (int i = 0; i < 7; i++)
			{
				Tarus.Score[i] = 0;
			}
			Tarus.CurrentLevel = 1;
			Tarus.UnlockedLevel = 0;
			Tarus.AcquiredWeapon[0] = 1;
			Tarus.AcquiredWeapon[1] = 0;
			Tarus.AcquiredWeapon[2] = 0;
			Tarus.AcquiredWeapon[3] = 0;
			for (int j = 0; j < 12; j++)
			{
				Tarus.AcqiuredUpgrade[j] = 0;
			}
			Tarus.AcqiuredUpgrade[0] = 1;
			Tarus.AcqiuredUpgrade[4] = 1;
			Tarus.AcqiuredUpgrade[8] = 1;
			for (int k = 1; k <= 7; k++)
			{
				Tarus.AcquiredChip[k].Clear();
			}
			Tarus.EquipedWeapon = WeaponType.ZytronBlaster;
			Tarus.SpecialLevel = 1;
			Tarus.RepairLevel = 0;
			Tarus.BlastLevel = 0;
			Tarus.ShockLevel = 0;
			Tarus.BoostLevel = 0;
			Tarus.DashLevel = 1;
			Tarus.ProtectionLevel = 0;
			Tarus.TutorialBasic = false;
			Tarus.TutorialSpecial = false;
			Tarus.TutorialBlast = false;
			Tarus.TutorialData = false;
			Tarus.TutorialFan = false;
			Tarus.TutorialBoost = false;
			Tarus.TutorialShield = false;
			Tarus.TutorialUpgrade = false;
			Tarus.PlayTime = 0f;
			Tarus.Rank = 1;
			Tarus.CombatDifficulty = GameCombatDifficulty.Normal;
			Tarus.Experience = 0;
			Tarus.ResetHasShownCutscenes();
			Tarus.AcquireBlastUpgradeFromMK2 = false;
			Tarus.AcquireShockUpgradeFromOculus = false;
			Tarus.AcquireSpecialUpgradeFromBad = false;
		}

		public static void ResetConfigs()
		{
			Configs.ResetData();
		}

		public static bool LoadConfigsFromStorage()
		{
			Console.WriteLine("profile:load configs");
			if (Configs == null)
			{
				Configs = new Configs();
			}
			string profileDirectory = Pref.GetProfileDirectory();
			string path = Path.Combine(profileDirectory, "configs.ini");
			bool flag = false;
			bool flag2 = true;
			if (File.Exists(path))
			{
				try
				{
					using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open, FileAccess.Read), Encoding.UTF8))
					{
						Configs.RestoreData(reader);
					}
				}
				catch (Exception)
				{
					flag2 = false;
					flag = true;
				}
			}
			else
			{
				flag = true;
			}
			if (!flag2 || flag)
			{
				ResetConfigs();
			}
			return flag2;
		}

		public static void DeleteProfiles()
		{
			Console.WriteLine("profile:delete profiles");
			ResetMarsProfile();
			ResetTarusProfile();
			Profile.ResetData();
		}

		public static void ResetProfiles()
		{
			Console.WriteLine("profile:reset profiles");
			ResetMarsProfile();
			ResetTarusProfile();
			Profile.ResetData();
		}

		public static void RegulateProfile(PlayerProfile p)
		{
			p.AcqiuredUpgrade[4] = Math.Max(p.AcqiuredUpgrade[4], p.SpecialLevel);
			p.AcqiuredUpgrade[5] = Math.Max(p.AcqiuredUpgrade[5], p.RepairLevel);
			p.AcqiuredUpgrade[6] = Math.Max(p.AcqiuredUpgrade[6], p.BlastLevel);
			p.AcqiuredUpgrade[7] = Math.Max(p.AcqiuredUpgrade[7], p.ShockLevel);
			p.AcqiuredUpgrade[9] = Math.Max(p.AcqiuredUpgrade[9], p.BoostLevel);
			p.AcqiuredUpgrade[8] = Math.Max(p.AcqiuredUpgrade[8], p.DashLevel);
			p.AcqiuredUpgrade[11] = Math.Max(p.AcqiuredUpgrade[11], p.ArmorLevel);
			p.AcqiuredUpgrade[0] = Math.Max(p.AcqiuredUpgrade[0], p.AcquiredWeapon[0]);
			p.AcqiuredUpgrade[1] = Math.Max(p.AcqiuredUpgrade[1], p.AcquiredWeapon[1]);
			p.AcqiuredUpgrade[2] = Math.Max(p.AcqiuredUpgrade[2], p.AcquiredWeapon[2]);
			p.AcqiuredUpgrade[3] = Math.Max(p.AcqiuredUpgrade[3], p.AcquiredWeapon[3]);
			int num = 0;
			if (p.UnlockedLevel > 3 || p.ProtectionLevel > 0)
			{
				num++;
			}
			if (p.AcquiredChip[5].Contains(2))
			{
				num++;
			}
			if (p.AcquiredChip[7].Contains(2))
			{
				num++;
			}
			p.AcqiuredUpgrade[10] = num;
		}

		public static bool LoadProfilesFromStorage()
		{
			Console.WriteLine("profile:load profile");
			bool flag = true;
			bool flag2 = false;
			if (!IsTrialMode)
			{
				string profileDirectory = Pref.GetProfileDirectory();
				string gamerKey = Configs.GamerKey;
				string path = Path.Combine(profileDirectory, Pref.GetGamerFileName(gamerKey));
				if (File.Exists(path))
				{
					try
					{
						using (BinaryReader binaryReader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read)))
						{
							uint num = binaryReader.ReadUInt32();
							if (num >= 1)
							{
								Mars.RestoreData(binaryReader, num);
								Tarus.RestoreData(binaryReader, num);
								Profile.RestoreData(binaryReader, num);
							}
							if (num >= 5)
							{
								Mars.TutorialUpgrade = binaryReader.ReadBoolean();
								Tarus.TutorialUpgrade = binaryReader.ReadBoolean();
							}
						}
					}
					catch (Exception)
					{
						flag2 = true;
						flag = false;
					}
				}
				else
				{
					flag2 = true;
				}
			}
			if (!flag || flag2)
			{
				ResetProfiles();
			}
			else
			{
				RegulateProfile(Mars);
				RegulateProfile(Tarus);
			}
			return flag;
		}

		private static void SaveProfileToStorage()
		{
			Console.WriteLine("profile:save profile");
			if (!IsTrialMode)
			{
				string profileDirectory = Pref.GetProfileDirectory();
				string gamerKey = Configs.GamerKey;
				string path = Path.Combine(profileDirectory, Pref.GetGamerFileName(gamerKey));
				string directoryName = Path.GetDirectoryName(path);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
				{
					binaryWriter.Write(5u);
					Mars.WriteData(binaryWriter);
					Tarus.WriteData(binaryWriter);
					Profile.WriteData(binaryWriter);
					binaryWriter.Write(Mars.TutorialUpgrade);
					binaryWriter.Write(Tarus.TutorialUpgrade);
				}
			}
		}

		public static void SaveConfigsToStorage()
		{
			Console.WriteLine("profile:save configs");
			string profileDirectory = Pref.GetProfileDirectory();
			if (!Directory.Exists(profileDirectory))
			{
				Directory.CreateDirectory(profileDirectory);
			}
			string path = Path.Combine(profileDirectory, "configs.ini");
			using (StreamWriter writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.ReadWrite), Encoding.UTF8))
			{
				Configs.WriteData(writer);
			}
		}

		public static void PlayMars()
		{
			Profile.LastPlayedCharacter = PlayerType.Mars;
			Current = Mars;
		}

		public static void PlayTarus()
		{
			Profile.LastPlayedCharacter = PlayerType.Tarus;
			Current = Tarus;
		}

		public static int GetEnemyHP(int baseHP)
		{
			return GetEnemyHP(baseHP, Current.CombatDifficulty);
		}

		public static int GetBossHP(int baseHP)
		{
			return GetBossHP(baseHP, Current.CombatDifficulty);
		}

		public static int GetEnemyDamage(int baseDamage)
		{
			return GetEnemyDamage(baseDamage, Current.CombatDifficulty);
		}

		public static int GetBossDamage(int baseDamage)
		{
			return GetBossDamage(baseDamage, Current.CombatDifficulty);
		}

		public static float GetRepairCooldownTime(float baseCooldown)
		{
			return GetRepairCooldownTime(baseCooldown, Current.CombatDifficulty);
		}

		public static int GetEnemyDamage(int baseDamage, GameCombatDifficulty difficulty)
		{
			int combatDifficultyIndex = GetCombatDifficultyIndex(difficulty);
			int num = (int)((float)baseDamage * Pref.GMF_ENEMY_DAMAGE_MULTIPLIER[combatDifficultyIndex]);
			if (difficulty == GameCombatDifficulty.Normal)
			{
				num += 5;
			}
			return num;
		}

		public static int GetEnemyHP(int baseHP, GameCombatDifficulty difficulty)
		{
			int combatDifficultyIndex = GetCombatDifficultyIndex(difficulty);
			return (int)((float)baseHP * Pref.GMF_ENEMY_HP_MULTIPLIER[combatDifficultyIndex]);
		}

		public static int GetBossDamage(int baseDamage, GameCombatDifficulty difficulty)
		{
			int combatDifficultyIndex = GetCombatDifficultyIndex(difficulty);
			return (int)((float)baseDamage * Pref.GMF_BOSS_DAMAGE_MULTIPLIER[combatDifficultyIndex]);
		}

		public static int GetBossHP(int baseHP, GameCombatDifficulty difficulty)
		{
			int combatDifficultyIndex = GetCombatDifficultyIndex(difficulty);
			return (int)((float)baseHP * Pref.GMF_BOSS_HP_MULTIPLIER[combatDifficultyIndex]);
		}

		public static float GetRepairCooldownTime(float baseCooldown, GameCombatDifficulty difficulty)
		{
			int combatDifficultyIndex = GetCombatDifficultyIndex(difficulty);
			return baseCooldown * Pref.GMF_PLAYER_REPAIR_COOLDOWN_MULTIPLIER[combatDifficultyIndex];
		}

		private static int GetCombatDifficultyIndex(GameCombatDifficulty difficulty)
		{
			switch (difficulty)
			{
			case GameCombatDifficulty.Casual:
				return 0;
			case GameCombatDifficulty.Hardcore:
				return 2;
			default:
				return 1;
			}
		}

		public static void SetGamerPresence(string presence)
		{
		}

		public static bool BeginSave(AsyncCallback callback, byte saveOptions, bool showLoadingUI)
		{
			mIsRequestToSaveThisFrame = true;
			mIsShowLoadingUIForSaving = showLoadingUI;
			mSavingCallback = callback;
			mSaveOptions = saveOptions;
			_SaveInBackground();
			return true;
		}

		private static void _SaveInBackground()
		{
			if (IsTrialMode)
			{
				mSavingSuccessThisFrame = true;
			}
			else
			{
				if ((mSaveOptions & 1) > 0 || (mSaveOptions & 4) > 0)
				{
					SaveProfileToStorage();
				}
				if ((mSaveOptions & 2) > 0)
				{
					SaveConfigsToStorage();
				}
				mSavingSuccessThisFrame = true;
				if (mSavingCallback != null)
				{
					ProfileSaveAsyncResult profileSaveAsyncResult = new ProfileSaveAsyncResult();
					profileSaveAsyncResult.IsCompleted = true;
					mSavingCallback(profileSaveAsyncResult);
					mSavingCallback = null;
				}
			}
		}

		public static void Update()
		{
			if (mSavingThread == null)
			{
				if (mIsRequestToSaveThisFrame)
				{
					mIsRequestToSaveThisFrame = false;
					if (mIsShowLoadingUIForSaving)
					{
						LoadingUI.Instance.Show(false, 0);
					}
					mSaveTime = 0f;
					mDelaySaveIconCountDownTimer = 0f;
				}
			}
			else
			{
				mSaveTime += TimeManager.SecondDifference;
				if (!mSavingThread.IsAlive)
				{
					mDelaySaveIconCountDownTimer = 3f - mSaveTime;
					mSavingThread = null;
				}
			}
			if (mDelaySaveIconCountDownTimer > 0f)
			{
				mDelaySaveIconCountDownTimer -= TimeManager.SecondDifference;
				if (mDelaySaveIconCountDownTimer <= 0f && mIsShowLoadingUIForSaving)
				{
					mIsShowLoadingUIForSaving = false;
				}
			}
		}

		public static bool OpenMarketplaceUI(out string errorMessage)
		{
			bool flag = false;
			string empty = string.Empty;
			errorMessage = string.Empty;
			return true;
		}

		public static void ManageSaveFail(Action storageAvailableToSaveCallback, Action continueWithoutSavingCallback)
		{
			mStorageAvailableToSaveCallback = storageAvailableToSaveCallback;
			mContinueWithoutSavingCallback = continueWithoutSavingCallback;
			if (StorageDeviceManager.SelectedStorageDevice == null || !StorageDeviceManager.SelectedStorageDevice.IsConnected)
			{
				GuideMessageBoxWrapper.Instance.Show("无法保存", "选择的存储设备无法使用。\n请选择其它存储设备或在不存档的情况下继续游戏。", PopUpUIAskForStorageUnavailable, 2, "选择存档", "继续", string.Empty);
			}
		}

		private static void PopUpUIAskForStorageUnavailable(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				SaveFailStorageMissing_SelectStorageDevice();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Cancel:
				mContinueWithoutSavingCallback();
				break;
			}
		}

		private static void SaveFailStorageMissing_SelectStorageDevice()
		{
			StorageDeviceManager.PromptUserToSelectStorageDevice(FinishSelectStorageDevice);
		}

		private static void FinishSelectStorageDevice(FinishSelectStorageDeviceEventArgument arg)
		{
			if (arg.SelectionType == StorageDeviceManager.SelectionType.Auto)
			{
				GuideMessageBoxWrapper.Instance.Show("确认选择", "只有一台存储设备能用。\n请确认你的选择，你可以在不存档的情况下继续游戏，也可以选择其它存储设备。", PopUpUIAskForAutoSelectStorageDevice, 3, "确认", "继续", "选择其它存储设备");
			}
			else
			{
				CheckIsThereExistingSave();
			}
		}

		private static void PopUpUIAskForAutoSelectStorageDevice(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				CheckIsThereExistingSave();
				break;
			case PopUpUI.PopUpUIResult.Cancel:
				mContinueWithoutSavingCallback();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Other:
				SaveFailStorageMissing_SelectStorageDevice();
				break;
			}
		}

		private static void CheckIsThereExistingSave()
		{
			if (StorageDeviceManager.SelectedStorageDevice != null && StorageDeviceManager.SelectedStorageDevice.IsConnected)
			{
				if (StorageDeviceManager.IsDirectoryForGamerExist())
				{
					GuideMessageBoxWrapper.Instance.Show("覆盖存档", "在当前设备中发现了存档文件。\n是否要覆盖此存档吗？", PopUpUIAskForOverwriteSave, 2, "覆盖", "选择其它存储设备", string.Empty);
				}
				else
				{
					mStorageAvailableToSaveCallback();
				}
			}
			else
			{
				mStorageAvailableToSaveCallback();
			}
		}

		private static void PopUpUIAskForOverwriteSave(PopUpUI.PopUpUIResult result)
		{
			switch (result)
			{
			case PopUpUI.PopUpUIResult.Ok:
				mStorageAvailableToSaveCallback();
				break;
			case PopUpUI.PopUpUIResult.None:
			case PopUpUI.PopUpUIResult.Cancel:
				StorageDeviceManager.Reset();
				StorageDeviceManager.Initialize();
				StorageDeviceManager.PromptUserToSelectStorageDevice(FinishSelectStorageDevice);
				break;
			}
		}
	}
}
