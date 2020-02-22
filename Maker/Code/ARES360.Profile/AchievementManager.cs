using ARES360.UI;
using Steamworks;

namespace ARES360.Profile
{
	public class AchievementManager
	{
		private static AchievementManager mInstance;

		public static AchievementManager Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new AchievementManager();
				}
				return mInstance;
			}
		}

		public static string GetAchievementSteamKey(int achivementId)
		{
			switch (achivementId)
			{
			case 0:
				return "ARES_ENDING";
			case 1:
				return "TARUS_ENDING";
			case 2:
				return "COMPLETE_WITHOUT_REPAIR";
			case 3:
				return "LEVEL_4";
			case 4:
				return "LEVEL_5";
			case 5:
				return "LEVEL_6";
			case 6:
				return "BEAT_GOLIATH";
			case 7:
				return "BEAT_CARRION_ARES_TARUS";
			case 8:
				return "BEAT_ZYPHERPOD_ARES";
			case 9:
				return "ARMOR_2_ARES";
			case 10:
				return "BEAT_TARUS";
			case 11:
				return "BEAT_ZYTRON_RANGERS_TARUS";
			case 12:
				return "ARMOR_2_TARUS";
			case 13:
				return "BEAT_ARES";
			case 14:
				return "PERFECT_GOLIATH";
			case 15:
				return "PERFECT_CARRION";
			case 16:
				return "PERFECT_ZYPHERPOD";
			case 17:
				return "PERFECT_SENTINEL";
			case 18:
				return "PERFECT_ARES";
			case 19:
				return "PERFECT_TARUS";
			case 20:
				return "PERFECT_PRIMEGUARDIAN";
			case 21:
				return "PERFECT_MINOS";
			case 22:
				return "ALL_ABILITIES";
			case 23:
				return "ALL_DATACUBES";
			case 24:
				return "ALL_UPGRADECHIPS";
			case 25:
				return "BREAK_SHIELD";
			case 26:
				return "99_COMBO";
			case 27:
				return "LEVEL3_WEAPON";
			case 28:
				return "LEVEL3_EVERYTHING";
			case 29:
				return "ULTIMATE_ATTACK_ARES";
			case 30:
				return "PERFECT_HARD_BOSS";
			default:
				return null;
			}
		}

		public static string GetLocalizedCaptionName(int achievementId)
		{
			switch (achievementId)
			{
			case 0:
				return "主要任务";
			case 1:
				return "另一个故事";
			case 2:
				return "无限能量！";
			case 3:
				return "我就是毁灭者！";
			case 4:
				return "看好！这才是我真正的实力！";
			case 5:
				return "宇宙战士";
			case 6:
				return "这才是第一个……";
			case 7:
				return "……早知道就一起行动！";
			case 8:
				return "破坏外壳";
			case 9:
				return "武装圣徒";
			case 10:
				return "次级任务";
			case 11:
				return "灾创的士兵也不是很强";
			case 12:
				return "我觉得自己很强，非常强！";
			case 13:
				return "只能这么做";
			case 14:
				return "别再回来了！";
			case 15:
				return "……也不是很长";
			case 16:
				return "就像抓虫子一样！";
			case 17:
				return "简直像为我量身定做的";
			case 18:
				return "太失败了……";
			case 19:
				return "拥抱黑暗";
			case 20:
				return "……也不是很大";
			case 21:
				return "哦，人类！";
			case 22:
				return "准备行动";
			case 23:
				return "书虫";
			case 24:
				return "我拥有全部力量！";
			case 25:
				return "护盾破环者";
			case 26:
				return "九九连招表";
			case 27:
				return "致命武器！";
			case 28:
				return "暴力演绎";
			case 29:
				return "能量爆破";
			case 30:
				return "不可能！";
			default:
				return null;
			}
		}

		public static string GetLocalizedObjective(int achievementId)
		{
			switch (achievementId)
			{
			case 0:
				return "用阿瑞斯通关游戏";
			case 1:
				return "用塔鲁斯通关游戏";
			case 2:
				return "不用修理技能通关每个章节";
			case 3:
				return "角色等级达到四级";
			case 4:
				return "角色等级达到五级";
			case 5:
				return "角色等级达到六级";
			case 6:
				return "击败歌利亚";
			case 7:
				return "分别用阿瑞斯和塔鲁斯击败卡里恩";
			case 8:
				return "用阿瑞斯击败重灾化机器人";
			case 9:
				return "找到机器人维修舱";
			case 10:
				return "用阿瑞斯击败第5章头目";
			case 11:
				return "用塔鲁斯击败灾创步兵，灾创步甲兵和灾创火箭兵";
			case 12:
				return "变得更强！";
			case 13:
				return "用塔鲁斯击败第5章头目";
			case 14:
				return "完美击败歌利亚";
			case 15:
				return "完美击败卡里恩";
			case 16:
				return "完美击败重灾化机器人";
			case 17:
				return "完美击败哨卫者";
			case 18:
				return "用塔鲁斯完美击败第5章头目";
			case 19:
				return "用阿瑞斯完美击败第5章头目";
			case 20:
				return "完美击败精英守护者";
			case 21:
				return "完美击败米诺斯加农炮";
			case 22:
				return "获得所有能力和武器";
			case 23:
				return "集齐全部数据方块";
			case 24:
				return "集齐全部升级晶片";
			case 25:
				return "用电浆冲击或封闭打击破坏敌人的护盾";
			case 26:
				return "达成99连击";
			case 27:
				return "将一把武器升至满级";
			case 28:
				return "把所有武器和能力升至满级";
			case 29:
				return "使用烈日打击一次性摧毁五个以上的敌人";
			case 30:
				return "在硬核难度下完美击败所有头目";
			default:
				return null;
			}
		}

		public static int GetAchievementSheetKey(int achievementId)
		{
			switch (achievementId)
			{
			case 0:
				return 6;
			case 1:
				return 62;
			case 2:
				return 26;
			case 3:
				return 32;
			case 4:
				return 34;
			case 5:
				return 36;
			case 6:
				return 16;
			case 7:
				return 14;
			case 8:
				return 20;
			case 9:
				return 8;
			case 10:
				return 18;
			case 11:
				return 18;
			case 12:
				return 10;
			case 13:
				return 12;
			case 14:
				return 46;
			case 15:
				return 44;
			case 16:
				return 58;
			case 17:
				return 54;
			case 18:
				return 42;
			case 19:
				return 56;
			case 20:
				return 52;
			case 21:
				return 50;
			case 22:
				return 0;
			case 23:
				return 2;
			case 24:
				return 4;
			case 25:
				return 24;
			case 26:
				return 38;
			case 27:
				return 30;
			case 28:
				return 28;
			case 29:
				return 64;
			case 30:
				return 48;
			default:
				return 0;
			}
		}

		public static int GetAchievementMaxCount(int achievementID)
		{
			return 1;
		}

		public void Initialize()
		{
		}

		public void RestoreAchievements()
		{
			if (Pref.Steamworks)
			{
				bool flag = false;
				for (int i = 0; i < 31; i++)
				{
					if (ProfileManager.Profile.HasAwardedAchievement(i))
					{
						flag = true;
						string achievementSteamKey = GetAchievementSteamKey(i);
						SteamUserStats.SetAchievement(achievementSteamKey);
					}
				}
				if (flag)
				{
					SteamUserStats.StoreStats();
				}
			}
		}

		public void Notify(int achievementID, int amount)
		{
			int achievementCurrentCount = ProfileManager.Profile.GetAchievementCurrentCount(achievementID);
			int achievementMaxCount = GetAchievementMaxCount(achievementID);
			achievementCurrentCount += amount;
			if (achievementCurrentCount < achievementMaxCount)
			{
				ProfileManager.Profile.SetAchievementCurrentCount(achievementID, achievementCurrentCount);
				ProfileManager.BeginSave(null, 4, false);
			}
			else
			{
				achievementCurrentCount = achievementMaxCount;
				if (!ProfileManager.Profile.HasAwardedAchievement(achievementID))
				{
					ProfileManager.Profile.SetAchievementCurrentCount(achievementID, achievementCurrentCount);
					ProfileManager.Profile.SetHasAwardedAchievement(achievementID, true);
					ProfileManager.BeginSave(null, 4, false);
					if (!ProfileManager.IsTrialMode)
					{
						if (Pref.Steamworks)
						{
							string achievementSteamKey = GetAchievementSteamKey(achievementID);
							SteamUserStats.SetAchievement(achievementSteamKey);
							SteamUserStats.StoreStats();
						}
						else
						{
							AcquireItemUI.Instance.ShowAchievement(achievementID);
						}
					}
				}
			}
		}

		private void PopUpUIAskForUnlockFullVersion(PopUpUI.PopUpUIResult result)
		{
		}

		public void Update()
		{
		}

		public void MarkHasPerfectHardBossFight(int bossId)
		{
			if (bossId > 0)
			{
				ProfileManager.Profile.SetHasPerfectHardBossFight(bossId, true);
				for (int i = 1; i <= 8; i++)
				{
					if (!ProfileManager.Profile.HasPerfectHardBossFight(i))
					{
						return;
					}
				}
				Notify(30, 1);
			}
		}

		public void MarkNeverUseRepairKitForLevel(int level)
		{
			if (level >= 0)
			{
				ProfileManager.Profile.SetHasNeverUsedRepairKitInLevel(level, true);
				for (int i = 0; i < 7; i++)
				{
					if (!ProfileManager.Profile.HasNeverUsedRepairKitInLevel(i))
					{
						return;
					}
				}
				Notify(2, 1);
			}
		}
	}
}
