using ARES360.Data;
using ARES360.Entity;
using ARES360.Input;

namespace ARES360.UI
{
	public static class Upgrade
	{
		public static void LoadUpgradePurchaseDetailRichText(RichText r, PlayerType playerType, int upgradeType, int level)
		{
			switch (playerType)
			{
			case PlayerType.Mars:
				switch (upgradeType)
				{
				case 0:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 72);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 74);
						break;
					}
					break;
				case 1:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 20);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 22);
						break;
					}
					break;
				case 2:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 64);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 66);
						break;
					}
					break;
				case 3:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 28);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 30);
						break;
					}
					break;
				case 4:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 53);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 55);
						break;
					}
					break;
				case 5:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 41);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 43);
						break;
					}
					break;
				case 6:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 1);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 3);
						break;
					}
					break;
				case 7:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 47);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 49);
						break;
					}
					break;
				case 8:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 14);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 16);
						break;
					}
					break;
				case 9:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 7);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 9);
						break;
					}
					break;
				case 10:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 36);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 38);
						break;
					}
					break;
				case 11:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 59);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 61);
						break;
					}
					break;
				}
				break;
			case PlayerType.Tarus:
				switch (upgradeType)
				{
				case 0:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 151);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 153);
						break;
					}
					break;
				case 1:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 99);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 101);
						break;
					}
					break;
				case 2:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 143);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 145);
						break;
					}
					break;
				case 3:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 107);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 109);
						break;
					}
					break;
				case 4:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 132);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 134);
						break;
					}
					break;
				case 5:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 120);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 122);
						break;
					}
					break;
				case 6:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 80);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 82);
						break;
					}
					break;
				case 7:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 126);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 128);
						break;
					}
					break;
				case 8:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 93);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 95);
						break;
					}
					break;
				case 9:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 86);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 88);
						break;
					}
					break;
				case 10:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 115);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 117);
						break;
					}
					break;
				case 11:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 138);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 140);
						break;
					}
					break;
				}
				break;
			}
		}

		public static void LoadUpgradeDescriptionRichText(RichText r, PlayerType playerType, int upgradeType, int level)
		{
			switch (playerType)
			{
			case PlayerType.Mars:
				switch (upgradeType)
				{
				case 0:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 71);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 73);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 75);
						break;
					}
					break;
				case 1:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 19);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 21);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 23);
						break;
					}
					break;
				case 2:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 63);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 65);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 67);
						break;
					}
					break;
				case 3:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 27);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 29);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 31);
						break;
					}
					break;
				case 4:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 52);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 54);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 56);
						break;
					}
					break;
				case 5:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 40);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 42);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 44);
						break;
					}
					break;
				case 6:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 0);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 2);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 4);
						break;
					}
					break;
				case 7:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 46);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 48);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 50);
						break;
					}
					break;
				case 8:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 13);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 15);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 17);
						break;
					}
					break;
				case 9:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 6);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 8);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 10);
						break;
					}
					break;
				case 10:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 35);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 37);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 39);
						break;
					}
					break;
				case 11:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 58);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 60);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 62);
						break;
					}
					break;
				}
				break;
			case PlayerType.Tarus:
				switch (upgradeType)
				{
				case 0:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 150);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 152);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 154);
						break;
					}
					break;
				case 1:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 98);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 100);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 102);
						break;
					}
					break;
				case 2:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 142);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 144);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 146);
						break;
					}
					break;
				case 3:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 106);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 108);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 110);
						break;
					}
					break;
				case 4:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 131);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 133);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 135);
						break;
					}
					break;
				case 5:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 119);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 121);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 123);
						break;
					}
					break;
				case 6:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 79);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 81);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 83);
						break;
					}
					break;
				case 7:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 125);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 127);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 129);
						break;
					}
					break;
				case 8:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 92);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 94);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 96);
						break;
					}
					break;
				case 9:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 85);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 87);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 89);
						break;
					}
					break;
				case 10:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 114);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 116);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 118);
						break;
					}
					break;
				case 11:
					switch (level)
					{
					case 1:
						LocalizedRichText.LoadRichText(r, 137);
						break;
					case 2:
						LocalizedRichText.LoadRichText(r, 139);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 141);
						break;
					}
					break;
				}
				break;
			}
		}

		public static void LoadUpgradeUsageRichText(RichText r, PlayerType playerType, int upgradeType, int level)
		{
			switch (playerType)
			{
			case PlayerType.Mars:
				switch (upgradeType)
				{
				case 4:
					LocalizedRichText.LoadRichText(r, 57);
					break;
				case 5:
					LocalizedRichText.LoadRichText(r, 45);
					break;
				case 6:
					LocalizedRichText.LoadRichText(r, 5);
					break;
				case 7:
					LocalizedRichText.LoadRichText(r, 51);
					break;
				case 8:
					LocalizedRichText.LoadRichText(r, 18);
					break;
				case 9:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 11);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 11);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 12);
						break;
					}
					break;
				case 0:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 76);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 77);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 78);
						break;
					}
					break;
				case 1:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 24);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 25);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 26);
						break;
					}
					break;
				case 2:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 68);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 69);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 70);
						break;
					}
					break;
				case 3:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 32);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 33);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 34);
						break;
					}
					break;
				}
				break;
			case PlayerType.Tarus:
				switch (upgradeType)
				{
				case 4:
					LocalizedRichText.LoadRichText(r, 136);
					break;
				case 5:
					LocalizedRichText.LoadRichText(r, 124);
					break;
				case 6:
					LocalizedRichText.LoadRichText(r, 84);
					break;
				case 7:
					LocalizedRichText.LoadRichText(r, 130);
					break;
				case 8:
					LocalizedRichText.LoadRichText(r, 97);
					break;
				case 9:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 90);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 90);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 91);
						break;
					}
					break;
				case 0:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 155);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 156);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 157);
						break;
					}
					break;
				case 1:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 103);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 104);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 105);
						break;
					}
					break;
				case 2:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 147);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 148);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 149);
						break;
					}
					break;
				case 3:
					switch (GamePad.GetPreferredDeviceFamily())
					{
					case 2:
						LocalizedRichText.LoadRichText(r, 111);
						break;
					case 3:
						LocalizedRichText.LoadRichText(r, 112);
						break;
					default:
						LocalizedRichText.LoadRichText(r, 113);
						break;
					}
					break;
				}
				break;
			}
		}

		public static string GetUpgradeCaptionName(PlayerType playerType, int upgradeType)
		{
			switch (playerType)
			{
			case PlayerType.Mars:
				switch (upgradeType)
				{
				case 0:
					return "灾创爆破枪";
				case 1:
					return "激光反应器";
				case 2:
					return "电波放射器";
				case 3:
					return "光子发射器";
				case 4:
					return "烈日打击";
				case 5:
					return "修理";
				case 6:
					return "炽热铁拳";
				case 7:
					return "电浆冲击";
				case 8:
					return "冲刺";
				case 9:
					return "空中推进";
				case 10:
					return "额外防护";
				case 11:
					return "原型装甲";
				}
				break;
			case PlayerType.Tarus:
				switch (upgradeType)
				{
				case 0:
					return "灾创爆破枪";
				case 1:
					return "激光反应器";
				case 2:
					return "电波发射器";
				case 3:
					return "光子发射器";
				case 4:
					return "能量灌注";
				case 5:
					return "修理";
				case 6:
					return "能量冲击";
				case 7:
					return "封闭打击";
				case 8:
					return "翻滚";
				case 9:
					return "悬浮推进";
				case 10:
					return "额外防护";
				case 11:
					return "融合形态";
				}
				break;
			}
			return null;
		}

		public static int GetUpgradeCost(PlayerType playerType, int upgradeType, int level)
		{
			switch (playerType)
			{
			case PlayerType.Mars:
				switch (upgradeType)
				{
				case 0:
					if (level != 1)
					{
						return 1500;
					}
					return 750;
				case 1:
					if (level != 1)
					{
						return 2750;
					}
					return 1200;
				case 2:
					if (level != 1)
					{
						return 2750;
					}
					return 1200;
				case 3:
					if (level != 1)
					{
						return 3500;
					}
					return 1500;
				case 4:
					if (level != 1)
					{
						return 1500;
					}
					return 500;
				case 5:
					if (level != 1)
					{
						return 1200;
					}
					return 350;
				case 6:
					if (level != 1)
					{
						return 1200;
					}
					return 350;
				case 7:
					if (level != 1)
					{
						return 1200;
					}
					return 350;
				case 8:
					if (level != 1)
					{
						return 500;
					}
					return 500;
				case 9:
					if (level != 1)
					{
						return 5000;
					}
					return 1500;
				case 10:
					if (level != 1)
					{
						return 1600;
					}
					return 800;
				case 11:
					if (level != 1)
					{
						return 1500;
					}
					return 500;
				default:
					return 0;
				}
			case PlayerType.Tarus:
				switch (upgradeType)
				{
				case 0:
					if (level != 1)
					{
						return 1500;
					}
					return 750;
				case 1:
					if (level != 1)
					{
						return 2750;
					}
					return 1200;
				case 2:
					if (level != 1)
					{
						return 2750;
					}
					return 1200;
				case 3:
					if (level != 1)
					{
						return 3500;
					}
					return 1500;
				case 4:
					if (level != 1)
					{
						return 1500;
					}
					return 500;
				case 5:
					if (level != 1)
					{
						return 1200;
					}
					return 350;
				case 6:
					if (level != 1)
					{
						return 1200;
					}
					return 350;
				case 7:
					if (level != 1)
					{
						return 1200;
					}
					return 350;
				case 8:
					if (level != 1)
					{
						return 500;
					}
					return 500;
				case 9:
					if (level != 1)
					{
						return 5000;
					}
					return 1500;
				case 10:
					if (level != 1)
					{
						return 1800;
					}
					return 800;
				case 11:
					if (level != 1)
					{
						return 1500;
					}
					return 500;
				default:
					return 0;
				}
			default:
				return 0;
			}
		}

		public static string GetUpgradeSpeechName(PlayerType playerType, int upgradeType)
		{
			switch (playerType)
			{
			case PlayerType.Mars:
				switch (upgradeType)
				{
				case 0:
					return "灾创爆破枪";
				case 1:
					return "激光反应器";
				case 2:
					return "电波发射器";
				case 3:
					return "光子发射器";
				case 4:
					return "烈日打击";
				case 5:
					return "修理";
				case 6:
					return "炽热铁拳";
				case 7:
					return "电浆冲击";
				case 8:
					return "冲刺";
				case 9:
					return "空中推进";
				case 10:
					return "额外防护";
				case 11:
					return "原型装甲";
				}
				break;
			case PlayerType.Tarus:
				switch (upgradeType)
				{
				case 0:
					return "灾创爆破枪";
				case 1:
					return "激光反应器";
				case 2:
					return "电波发射器";
				case 3:
					return "光子发射器";
				case 4:
					return "能量灌注";
				case 5:
					return "修理";
				case 6:
					return "能量冲击";
				case 7:
					return "封闭打击";
				case 8:
					return "翻滚";
				case 9:
					return "悬浮推进";
				case 10:
					return "额外防护";
				case 11:
					return "融合形态";
				}
				break;
			}
			return null;
		}
	}
}
