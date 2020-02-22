using ARES360.UI;

namespace ARES360.Data
{
	public class LocalizedRichTextEN
	{
		public static void LoadRichText(RichText r, int key)
		{
			switch (key)
			{
			case 0:
				r.Text("能引发致命的爆炸").Break(1).Text(" •  造成100点伤害")
					.Break(1)
					.Text(" •  充能时间10秒");
				break;
			case 1:
				r.Text("提高有效性").Break(1).Text(" •  造成100 » ")
					.Text("150", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  ")
					.Text("提高影响范围", 2)
					.Break(1)
					.Text(" •  充能时间10 » ")
					.Text("8", 2)
					.Text("秒");
				break;
			case 2:
				r.Text("能引发致命的爆炸").Break(1).Text(" •  造成150点伤害")
					.Break(1)
					.Text(" •  充能时间8秒");
				break;
			case 3:
				r.Text("提高有效性").Break(1).Text(" •  造成150 » ")
					.Text("200", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  ")
					.Text("提高影响范围", 2)
					.Break(1)
					.Text(" •  充能时间8 » ")
					.Text("6", 2)
					.Text("秒");
				break;
			case 4:
				r.Text("能引发致命的爆炸").Break(1).Text(" •  造成200点伤害")
					.Break(1)
					.Text(" •  充能时间6秒");
				break;
			case 5:
				r.Text("按下").Button(128).Text("使用");
				break;
			case 6:
				r.Text("能让你在空中进行短距离推进");
				break;
			case 7:
				r.Text("提高可用性").Break(1).Text(" •  ")
					.Text("能在空中进行2次推进", 2);
				break;
			case 8:
				r.Text("能让你在空中进行短距离推进").Break(1).Text(" •  能在空中进行2次推进");
				break;
			case 9:
				r.Text("提高可用性").Break(1).Text(" •  能在空中进行2 » ")
					.Text("3", 2)
					.Text("次推进");
				break;
			case 10:
				r.Text("能让你在空中进行短距离推进").Break(1).Text(" •  能在空中进行3次推进");
				break;
			case 11:
				r.Text("按住").Button(1024).Text("使用");
				break;
			case 12:
				r.Text("按住").Button(512).Text("+")
					.KeyboardAxisButton(2, false)
					.Text("使用");
				break;
			case 13:
				r.Text("向前冲刺，可以穿过狭窄的通道");
				break;
			case 14:
				r.Text("提高可用性").Break(1).Text(" •  ")
					.Text("能连续进行冲刺", 2);
				break;
			case 15:
				r.Text("向前冲刺，可以穿过狭窄的通道")
					.Break(1)
					.Text(" •  能连续进行冲刺");
				break;
			case 16:
				r.Text("提高可用性").Break(1).Text(" •  能连续进行冲刺")
					.Break(1)
					.Text(" •  ")
					.Text("能在冲刺过程中改变方向", 2);
				break;
			case 17:
				r.Text("向前冲刺，可以穿过狭窄的通道")
					.Break(1)
					.Text(" •  能连续进行冲刺")
					.Break(1)
					.Text(" •  能在冲刺过程中改变方向");
				break;
			case 18:
				r.Text("按下").Button(512).Text("使用");
				break;
			case 19:
				r.Text("全自动火枪").Break(1).Text(" •  造成13点伤害")
					.Break(1)
					.Text(" •  射速0.1秒");
				break;
			case 20:
				r.Text("提高伤害和射速").Break(1).Text(" •  造成13 » ")
					.Text("16", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速0.1 » ")
					.Text("0.075", 2)
					.Text("秒");
				break;
			case 21:
				r.Text("全自动火枪").Break(1).Text(" •  造成16点伤害")
					.Break(1)
					.Text(" •  射速0.075秒");
				break;
			case 22:
				r.Text("提高伤害和射速").Break(1).Text(" •  造成16 » ")
					.Text("19", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速0.075 » ")
					.Text("0.05", 2)
					.Text("秒");
				break;
			case 23:
				r.Text("全自动火枪").Break(1).Text(" •  造成19点伤害")
					.Break(1)
					.Text(" •  射速0.05秒");
				break;
			case 24:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 25:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 26:
				r.Text("按住").Button(64).Text("进行射击");
				break;
			case 27:
				r.Text("发射压缩光子球，受到冲击后会产生爆炸")
					.Break(1)
					.Text(" •  造成60点范围伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 28:
				r.Text("提高范围伤害").Break(1).Text(" •  造成60 » ")
					.Text("80", 2)
					.Text("点范围伤害");
				break;
			case 29:
				r.Text("发射压缩光子球，受到冲击后会产生爆炸")
					.Break(1)
					.Text(" •  造成80点范围伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 30:
				r.Text("提高范围伤害").Break(1).Text(" •  造成80 » ")
					.Text("100", 2)
					.Text("点范围伤害");
				break;
			case 31:
				r.Text("发射压缩光子球，受到冲击后会产生爆炸")
					.Break(1)
					.Text(" •  造成100点范围伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 32:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 33:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 34:
				r.Text("按住").Button(64).Text("进行射击");
				break;
			case 35:
				r.Text("提高装甲的防御力").Break(1).Text(" •  +20点生命上限");
				break;
			case 36:
				r.Text("提高生命上限和可用性").Break(1).Text(" •  +20 » ")
					.Text("40", 2)
					.Text("点生命上限")
					.Break(1)
					.Text(" •  ")
					.Text("在满能量槽时有且只有一次能够防止死亡", 2);
				break;
			case 37:
				r.Text("提高装甲的防御力").Break(1).Text(" •  +40点生命上限")
					.Break(1)
					.Text(" •  在满能量槽时有且只有一次能够防止死亡");
				break;
			case 38:
				r.Text("提高生命上限和可用性").Break(1).Text(" •  +40 » ")
					.Text("60", 2)
					.Text("点生命上限")
					.Break(1)
					.Text(" •  在满能量槽时有且只有一次能够防止死亡")
					.Break(1)
					.Text(" •  ")
					.Text("受到的爆炸伤害减半", 2);
				break;
			case 39:
				r.Text("提高装甲的防御力").Break(1).Text(" •  +60点生命上限")
					.Break(1)
					.Text(" •  在满能量槽时有且只有一次能够防止死亡")
					.Break(1)
					.Text(" •  受到的爆炸伤害减半");
				break;
			case 40:
				r.Text("消耗材料来恢复生命").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复50-70点生命")
					.Break(1)
					.Text(" •  充能时间22秒");
				break;
			case 41:
				r.Text("增加回复量，减少充能时间").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复50-70 » ")
					.Text("70-100", 2)
					.Text("点生命")
					.Break(1)
					.Text(" •  充能时间22 » ")
					.Text("20", 2)
					.Text("秒");
				break;
			case 42:
				r.Text("消耗材料来恢复生命").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复70-100点生命")
					.Break(1)
					.Text(" •  充能时间20秒");
				break;
			case 43:
				r.Text("增加回复量，减少充能时间").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复70-100 » ")
					.Text("100-150", 2)
					.Text("点生命")
					.Break(1)
					.Text(" •  充能时间20 » ")
					.Text("18", 2)
					.Text("秒");
				break;
			case 44:
				r.Text("消耗材料来恢复生命").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复100-150点生命")
					.Break(1)
					.Text(" •  充能时间18秒");
				break;
			case 45:
				r.Text("按下").Button(4096).Text("使用");
				break;
			case 46:
				r.Text("生成电磁护盾").Break(1).Text(" •  能让敌人眩晕2秒")
					.Break(1)
					.Text(" •  充能时间6秒")
					.Break(1)
					.Text(" •  对头目无效");
				break;
			case 47:
				r.Text("提高有效性").Break(1).Text(" •  能让敌人眩晕2 » ")
					.Text("3", 2)
					.Text("秒")
					.Break(1)
					.Text(" •  ")
					.Text("提高有效范围", 2)
					.Break(1)
					.Text(" •  充能时间6 » ")
					.Text("5", 2)
					.Text("秒");
				break;
			case 48:
				r.Text("生成电磁护盾").Break(1).Text(" •  能让敌人眩晕3秒")
					.Break(1)
					.Text(" •  充能时间5秒")
					.Break(1)
					.Text(" •  对头目无效");
				break;
			case 49:
				r.Text("提高有效性").Break(1).Text(" •  能让敌人眩晕3 » ")
					.Text("4", 2)
					.Text("秒")
					.Break(1)
					.Text(" •  ")
					.Text("提高有效范围", 2)
					.Break(1)
					.Text(" •  充能时间5 » ")
					.Text("4", 2)
					.Text("秒");
				break;
			case 50:
				r.Text("生成电磁护盾").Break(1).Text(" •  能让敌人眩晕4秒")
					.Break(1)
					.Text(" •  充能时间4秒")
					.Break(1)
					.Text(" •  对头目无效");
				break;
			case 51:
				r.Text("按下").Button(256).Text("使用");
				break;
			case 52:
				r.Text("用能量球攻击所有敌人").Break(1).Text(" •  8个能量球，每个造成80点伤害")
					.Break(1)
					.Text(" •  每个敌人最多受到2次攻击");
				break;
			case 53:
				r.Text("增加攻击次数").Break(1).Text(" •  8 » ")
					.Text("12", 2)
					.Text("个能量球，每个造成80点伤害")
					.Break(1)
					.Text(" •  每个敌人最多受到2 » ")
					.Text("3", 2)
					.Text("次攻击");
				break;
			case 54:
				r.Text("用能量球攻击所有敌人").Break(1).Text(" •  12个能量球，每个造成80点伤害")
					.Break(1)
					.Text(" •  每个敌人最多受到3次攻击");
				break;
			case 55:
				r.Text("增加吸取效果").Break(1).Text(" •  12个能量球，每个造成80点伤害")
					.Break(1)
					.Text(" •  每个敌人最多受到3次攻击")
					.Break(1)
					.Text(" •  ")
					.Text("20%的伤害会转化成自身的生命", 2);
				break;
			case 56:
				r.Text("用能量球打击所有敌人").Break(1).Text(" •  12个能量球，每个造成80点伤害")
					.Break(1)
					.Text(" •  每个敌人最多受到3次攻击")
					.Break(1)
					.Text(" •  20%的伤害会转化成自身的生命");
				break;
			case 57:
				r.Text("按下").Button(2048).Text("使用");
				break;
			case 58:
				r.Text("新H类装甲原型").Break(1).Text(" •  冲刺时无敌")
					.Break(1)
					.Text(" •  造成20点伤害");
				break;
			case 59:
				r.Text("提高可用性").Break(1).Text(" •  冲刺和")
					.Text("推进", 2)
					.Text("时无敌")
					.Break(1)
					.Text(" •  造成20点伤害");
				break;
			case 60:
				r.Text("新H类装甲原型").Break(1).Text(" •  冲刺和推进时无敌")
					.Break(1)
					.Text(" •  造成20点伤害");
				break;
			case 61:
				r.Text("提高伤害").Break(1).Text(" •  冲刺和推进时无敌")
					.Break(1)
					.Text(" •  造成20 » ")
					.Text("60", 2)
					.Text("点伤害");
				break;
			case 62:
				r.Text("新H类装甲原型").Break(1).Text(" •  冲刺和推进时无敌")
					.Break(1)
					.Text(" •  造成60点伤害");
				break;
			case 63:
				r.Text("放射反合成冲击波").Break(1).Text(" •  造成15点范围伤害")
					.Break(1)
					.Text(" •  冲击波持续0.35秒")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 64:
				r.Text("提高冲击波的伤害和持续时间").Break(1).Text(" •  造成15 » ")
					.Text("22", 2)
					.Text("点范围伤害")
					.Break(1)
					.Text(" •  冲击波持续0.35 » ")
					.Text("0.45", 2)
					.Text("秒")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 65:
				r.Text("放射反合成冲击波").Break(1).Text(" •  造成22点范围伤害")
					.Break(1)
					.Text(" •  冲击波持续0.45秒")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 66:
				r.Text("提高冲击波的伤害和持续时间").Break(1).Text(" •  造成22 » ")
					.Text("29", 2)
					.Text("点范围伤害")
					.Break(1)
					.Text(" •  冲击波持续0.45 » ")
					.Text("0.55", 2)
					.Text("秒")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 67:
				r.Text("放射反合成冲击波").Break(1).Text(" •  造成29点范围伤害")
					.Break(1)
					.Text(" •  冲击波持续0.55秒")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 68:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 69:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 70:
				r.Text("使用").Button(64).Text("进行射击");
				break;
			case 71:
				r.Text("单发能量手枪").Break(1).Text(" •  造成20点伤害")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 72:
				r.Text("提高伤害").Break(1).Text(" •  造成20 » ")
					.Text("30", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 73:
				r.Text("单发能量手枪").Break(1).Text(" •  造成30点伤害")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 74:
				r.Text("提高伤害").Break(1).Text(" •  造成30 » ")
					.Text("40", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 75:
				r.Text("单发能量手枪").Break(1).Text(" •  造成40点伤害")
					.Break(1)
					.Text(" •  射速0.2秒");
				break;
			case 76:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 77:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 78:
				r.Text("按住").Button(64).Text("进行射击");
				break;
			case 79:
				r.Text("大型爆炸射击").Break(1).Text(" •  造成100点范围伤害")
					.Break(1)
					.Text(" •  充能时间10秒");
				break;
			case 80:
				r.Text("提高有效性").Break(1).Text(" •  造成100 » ")
					.Text("150", 2)
					.Text("点范围伤害")
					.Break(1)
					.Text(" •  充能时间10 » ")
					.Text("8", 2)
					.Text("秒");
				break;
			case 81:
				r.Text("大型爆炸射击").Break(1).Text(" •  造成150点范围伤害")
					.Break(1)
					.Text(" •  充能时间8秒");
				break;
			case 82:
				r.Text("提高有效性").Break(1).Text(" •  造成150 » ")
					.Text("200", 2)
					.Text("点范围伤害")
					.Break(1)
					.Text(" •  充能时间8 » ")
					.Text("6", 2)
					.Text("秒");
				break;
			case 83:
				r.Text("大型爆炸射击").Break(1).Text(" •  造成200点范围伤害")
					.Break(1)
					.Text(" •  充能时间6秒");
				break;
			case 84:
				r.Text("按下").Button(128).Text("使用");
				break;
			case 85:
				r.Text("能让塔鲁斯悬浮在空中").Break(1).Text(" •  持续时间2秒");
				break;
			case 86:
				r.Text("提高可用性").Break(1).Text(" •  持续时间2秒")
					.Break(1)
					.Text(" •  ")
					.Text("悬浮期间的移动速度提高50%", 2);
				break;
			case 87:
				r.Text("能让塔鲁斯悬浮在空中").Break(1).Text(" •  持续时间2秒")
					.Break(1)
					.Text(" •  悬浮期间的移动速度提高50%");
				break;
			case 88:
				r.Text("提高可用性").Break(1).Text(" •  持续时间2 » ")
					.Text("4", 2)
					.Text("秒")
					.Break(1)
					.Text(" •  悬浮期间的移动速度提高50%");
				break;
			case 89:
				r.Text("能让塔鲁斯悬浮在空中").Break(1).Text(" •  持续时间4秒")
					.Break(1)
					.Text(" •  悬浮期间的移动速度提高50%");
				break;
			case 90:
				r.Text("按住").Button(1024).Text("使用");
				break;
			case 91:
				r.Text("按住").Button(1024).Text("使用");
				break;
			case 92:
				r.Text("向前翻滚，可以穿过狭窄的通道");
				break;
			case 93:
				r.Text("提高可用性").Break(1).Text(" •  ")
					.Text("能连续进行翻滚", 2);
				break;
			case 94:
				r.Text("向前翻滚，可以穿过狭窄的通道")
					.Break(1)
					.Text(" •  能连续进行翻滚");
				break;
			case 95:
				r.Text("提高可用性").Break(1).Text(" •  能连续进行翻滚")
					.Break(1)
					.Text(" •  ")
					.Text("能在翻滚过程中改变方向", 2);
				break;
			case 96:
				r.Text("向前翻滚，可以穿过狭窄的通道")
					.Break(1)
					.Text(" •  能连续进行翻滚")
					.Break(1)
					.Text(" •  能在翻滚过程中改变方向");
				break;
			case 97:
				r.Text("按下").Button(512).Text("使用");
				break;
			case 98:
				r.Text("生成一把刺眼的激光刀").Break(1).Text(" •  造成14点伤害")
					.Break(1)
					.Text(" •  攻速0.09秒");
				break;
			case 99:
				r.Text("提高伤害和攻速").Break(1).Text(" •  造成14 » ")
					.Text("17", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速0.09 » ")
					.Text("0.065", 2)
					.Text("秒");
				break;
			case 100:
				r.Text("生成一把刺眼的激光刀").Break(1).Text(" •  造成17点伤害")
					.Break(1)
					.Text(" •  攻速0.065秒");
				break;
			case 101:
				r.Text("提高伤害和攻速").Break(1).Text(" •  造成17 » ")
					.Text("20", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  攻速0.065 » ")
					.Text("0.04", 2)
					.Text("秒");
				break;
			case 102:
				r.Text("生成一把强力激光刀").Break(1).Text(" •  造成20点伤害")
					.Break(1)
					.Text(" •  攻速0.04秒");
				break;
			case 103:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 104:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 105:
				r.Text("按住").Button(64).Text("进行射击");
				break;
			case 106:
				r.Text("扔出一个粘人光子球，靠近它的所有敌人都").Break(1).Text("会受到伤害")
					.Break(1)
					.Text(" •  造成90点伤害")
					.Break(1)
					.Text(" •  射速1秒");
				break;
			case 107:
				r.Text("提高伤害").Break(1).Text(" •  造成90 » ")
					.Text("120", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速1秒");
				break;
			case 108:
				r.Text("扔出一个粘人光子球，靠近它的所有敌人都").Break(1).Text("会受到伤害")
					.Break(1)
					.Text(" •  造成120点伤害")
					.Break(1)
					.Text(" •  射速1秒");
				break;
			case 109:
				r.Text("提高伤害").Break(1).Text(" •  造成120 » ")
					.Text("150", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速1秒");
				break;
			case 110:
				r.Text("扔出一个粘人光子球，靠近它的所有敌人都").Break(1).Text("会受到伤害")
					.Break(1)
					.Text(" •  造成150点伤害")
					.Break(1)
					.Text(" •  射速1秒");
				break;
			case 111:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 112:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 113:
				r.Text("按住").Button(64).Text("进行射击");
				break;
			case 114:
				r.Text("提高装甲的防御力").Break(1).Text(" •  +20点生命上限");
				break;
			case 115:
				r.Text("提高生命上限和可用性").Break(1).Text(" •  +20 » ")
					.Text("40", 2)
					.Text("点生命上限")
					.Break(1)
					.Text(" •  ")
					.Text("在满能量槽时有且只有一次能够防止死亡", 2);
				break;
			case 116:
				r.Text("提高装甲的防御力").Break(1).Text(" •  +40点生命上限")
					.Break(1)
					.Text(" •  在满能量槽时有且只有一次能够防止死亡");
				break;
			case 117:
				r.Text("提高生命上限和可用性").Break(1).Text(" •  +40 » ")
					.Text("60", 2)
					.Text("点生命上限")
					.Break(1)
					.Text(" •  在满能量槽时有且只有一次能够防止死亡")
					.Break(1)
					.Text(" •  ")
					.Text("受到的爆炸伤害减半", 2);
				break;
			case 118:
				r.Text("提高装甲的防御力").Break(1).Text(" •  +60点生命上限")
					.Break(1)
					.Text(" •  在满能量槽时有且只有一次能够防止死亡")
					.Break(1)
					.Text(" •  受到的爆炸伤害减半");
				break;
			case 119:
				r.Text("消耗材料来恢复生命").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复50-70点生命")
					.Break(1)
					.Text(" •  充能时间22秒");
				break;
			case 120:
				r.Text("增加回复量，减少充能时间").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复50-70 » ")
					.Text("70-100", 2)
					.Text("点生命")
					.Break(1)
					.Text(" •  充能时间22 » ")
					.Text("20", 2)
					.Text("秒");
				break;
			case 121:
				r.Text("消耗材料来恢复生命").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复70-100点生命")
					.Break(1)
					.Text(" •  充能时间20秒");
				break;
			case 122:
				r.Text("增加回复量，减少充能时间").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复70-100 » ")
					.Text("100-150", 2)
					.Text("点生命")
					.Break(1)
					.Text(" •  充能时间20 » ")
					.Text("18", 2)
					.Text("秒");
				break;
			case 123:
				r.Text("消耗材料来恢复生命").Break(1).Text(" •  消耗100材料")
					.Break(1)
					.Text(" •  恢复100-150点生命")
					.Break(1)
					.Text(" •  充能时间18秒");
				break;
			case 124:
				r.Text("按下").Button(4096).Text("使用");
				break;
			case 125:
				r.Text("释放大型电磁脉冲球").Break(1).Text(" •  能让敌人眩晕2秒")
					.Break(1)
					.Text(" •  充能时间6秒")
					.Break(1)
					.Text(" •  对头目无效");
				break;
			case 126:
				r.Text("提高有效性").Break(1).Text(" •  能让敌人眩晕2 » ")
					.Text("3", 2)
					.Text("秒")
					.Break(1)
					.Text(" •  充能时间6 » ")
					.Text("5", 2)
					.Text("秒");
				break;
			case 127:
				r.Text("释放大型电磁脉冲球").Break(1).Text(" •  能让敌人眩晕3秒")
					.Break(1)
					.Text(" •  充能时间5秒")
					.Break(1)
					.Text(" •  对头目无效");
				break;
			case 128:
				r.Text("提高有效性").Break(1).Text(" •  能让敌人眩晕3 » ")
					.Text("4", 2)
					.Text("秒")
					.Break(1)
					.Text(" •  充能时间5 » ")
					.Text("4", 2)
					.Text("秒");
				break;
			case 129:
				r.Text("释放大型电磁脉冲球").Break(1).Text(" •  能让敌人眩晕4秒")
					.Break(1)
					.Text(" •  充能时间4秒")
					.Break(1)
					.Text(" •  对头目无效");
				break;
			case 130:
				r.Text("按下").Button(256).Text("使用");
				break;
			case 131:
				r.Text("生成一个能量场，吸收伤害的同时也会对接").Break(1).Text("触的敌人造成伤害")
					.Break(1)
					.Text(" •  每点能量能吸收1.25点伤害");
				break;
			case 132:
				r.Text("提高伤害吸收比例").Break(1).Text(" •  每点能量能吸收1.25 » ")
					.Text("1.875", 2)
					.Text("点伤害");
				break;
			case 133:
				r.Text("生成一个能量场，吸收伤害的同时也会对接").Break(1).Text("触的敌人造成伤害")
					.Break(1)
					.Text(" •  每点能量能吸收1.875点伤害");
				break;
			case 134:
				r.Text("提高伤害吸收比例").Break(1).Text(" •  每点能量能吸收1.875 » ")
					.Text("2.5", 2)
					.Text("点伤害");
				break;
			case 135:
				r.Text("生成一个能量场，吸收伤害的同时也会对接").Break(1).Text("触的敌人造成伤害")
					.Break(1)
					.Text(" •  每点能量能吸收2.5点伤害");
				break;
			case 136:
				r.Text("按下").Button(2048).Text("使用");
				break;
			case 137:
				r.Text("神秘又强大的能力").Break(1).Text(" •  塔鲁斯在翻滚时会处于无敌状态，并且会")
					.Break(1)
					.Text("    让接触到的敌人陷入1秒的眩晕状态");
				break;
			case 138:
				r.Text("提高可用性").Break(1).Text(" •  塔鲁斯在翻滚时会处于无敌状态，并且会")
					.Break(1)
					.Text("    让接触到的敌人陷入1秒的眩晕状态")
					.Break(1)
					.Text(" •  ")
					.Text("受到攻击时，悬浮推进不会被打断", 2);
				break;
			case 139:
				r.Text("神秘又强大的能力").Break(1).Text(" •  塔鲁斯在翻滚时会处于无敌状态，并且会")
					.Break(1)
					.Text("   让接触到的敌人陷入1秒的眩晕状态")
					.Break(1)
					.Text(" •  受到攻击时，悬浮推进不会被打断");
				break;
			case 140:
				r.Text("提高可用性").Break(1).Text(" •  塔鲁斯在翻滚时会处于无敌状态，并且会")
					.Break(1)
					.Text("    让接触到的敌人陷入1秒的眩晕状态")
					.Break(1)
					.Text(" •  ")
					.Text("受到攻击时，防止塔鲁斯陷入眩晕状态", 2);
				break;
			case 141:
				r.Text("神秘又强大的能力").Break(1).Text(" •  塔鲁斯在翻滚时会处于无敌状态，并且会")
					.Break(1)
					.Text("   让接触到的敌人陷入1秒的眩晕状态")
					.Break(1)
					.Text(" •  受到攻击时，防止塔鲁斯陷入眩晕状态");
				break;
			case 142:
				r.Text("放射大型冲击波").Break(1).Text(" •  造成40点伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 143:
				r.Text("提高伤害和射速").Break(1).Text(" •  造成40 » ")
					.Text("60", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 144:
				r.Text("放射大型冲击波").Break(1).Text(" •  造成60点伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 145:
				r.Text("提高伤害和射速").Break(1).Text(" •  造成60 » ")
					.Text("80", 2)
					.Text("点伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 146:
				r.Text("放射大型冲击波").Break(1).Text(" •  造成80点伤害")
					.Break(1)
					.Text(" •  射速0.5秒");
				break;
			case 147:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 148:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 149:
				r.Text("按住").Button(64).Text("进行射击");
				break;
			case 150:
				r.Text("短距离能量爆破射击").Break(1).Text(" •  造成30点小范围伤害")
					.Break(1)
					.Text(" •  射速0.3秒");
				break;
			case 151:
				r.Text("提高爆破伤害").Break(1).Text(" •  造成30 » ")
					.Text("40", 2)
					.Text("点小范围伤害")
					.Break(1)
					.Text(" •  射速0.3秒");
				break;
			case 152:
				r.Text("短距离能量爆破射击").Break(1).Text(" •  造成40点小范围伤害")
					.Break(1)
					.Text(" •  射速0.3秒");
				break;
			case 153:
				r.Text("提高爆破伤害").Break(1).Text(" •  造成40 » ")
					.Text("50", 2)
					.Text("点小范围伤害")
					.Break(1)
					.Text(" •  射速0.3秒");
				break;
			case 154:
				r.Text("短距离能量爆破射击").Break(1).Text(" •  造成50点小范围伤害")
					.Break(1)
					.Text(" •  射速0.3秒");
				break;
			case 155:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 156:
				r.Text("使用").Image(39).Text("或按住")
					.Button(64)
					.Text("进行射击");
				break;
			case 157:
				r.Text("按住").Button(64).Text("进行射击");
				break;
			case 158:
				r.Text("按住").Button(64).Text("或使用")
					.Image(39)
					.Text("进行")
					.Text("射击", 1);
				break;
			case 159:
				r.Text("按住").Button(64).Text("或使用")
					.Image(39)
					.Text("进行")
					.Text("射击", 1);
				break;
			case 160:
				r.Text("按住").Button(64).Text("进行")
					.Text("射击", 1);
				break;
			case 161:
				r.Text("按下").Button(128).Text("使用")
					.Text("炽热铁拳", 1)
					.Text("来摧毁燃气罐");
				break;
			case 162:
				r.Text("按住").Button(128).Text("使用")
					.Text("能量冲击", 1)
					.Text("来摧毁燃气罐");
				break;
			case 163:
				r.Text("在空中按下").Button(512).Text("使用")
					.Text("空中推进", 1);
				break;
			case 164:
				r.Text("按住").Button(1024).Text("使用")
					.Text("悬浮推进", 1);
				break;
			case 165:
				r.Text("按住").Button(1024).Text("使用")
					.Text("悬浮推进", 1);
				break;
			case 166:
				r.Text("按住").Button(1024).Text("使用")
					.Text("纵向")
					.Text("空中推进", 1);
				break;
			case 167:
				r.Text("按住").Button(512).Text("+")
					.KeyboardAxisButton(2, false)
					.Text("使用")
					.Text("纵向")
					.Text("空中推进", 1);
				break;
			case 168:
				r.Text("在空中按下").Button(512).Text("或")
					.Button(1024)
					.Text("使用")
					.Text("空中推进", 1);
				break;
			case 169:
				r.Text("在空中按下").Button(512).Text("使用")
					.Text("空中推进", 1);
				break;
			case 170:
				r.Text("按住").Button(1024).Text("使用")
					.Text("悬浮推进", 1);
				break;
			case 171:
				r.Text("融合形态", 1).Text("能让你在翻滚时变得无敌");
				break;
			case 172:
				r.Text("并且会让接触到的敌人陷入眩晕状态");
				break;
			case 173:
				r.Text("按下").Button(32).Text("进行")
					.Text("跳跃", 1);
				break;
			case 174:
				r.Text("按下").Button(16384).Text("装备")
					.Text("激光反应器", 1);
				break;
			case 175:
				r.Text("在空中按下").Button(32).Text("进行")
					.Text("二段跳", 1);
				break;
			case 176:
				r.Text("使用").Image(31).Text("进行")
					.Text("移动", 1);
				break;
			case 177:
				r.Text("使用").Image(31).Text("进行")
					.Text("移动", 1);
				break;
			case 178:
				r.Text("使用").KeyboardAxisButton(2, false).KeyboardAxisButton(1, true)
					.KeyboardAxisButton(2, true)
					.KeyboardAxisButton(1, false)
					.Text("进行")
					.Text("移动", 1);
				break;
			case 179:
				r.Text("按下").Button(512).Text("进行")
					.Text("冲刺", 1)
					.Text("，可以穿过狭窄的通道");
				break;
			case 180:
				r.Text("按下").Button(512).Text("进行")
					.Text("翻滚", 1)
					.Text("，可以穿过狭窄的通道");
				break;
			case 181:
				r.Text("原型装甲", 1).Text("能让你在冲刺时变得无敌");
				break;
			case 182:
				r.Text("并对接触到的敌人造成伤害");
				break;
			case 183:
				r.Text("按下").Button(131072).Text("打开")
					.Text("科技升级", 1)
					.Text("菜单");
				break;
			case 184:
				r.Text("修理", 1).Text("能消耗100材料来恢复你的生命");
				break;
			case 185:
				r.Text("按下").Button(4096).Text("使用");
				break;
			case 186:
				r.Text("按下").Button(512).Text("进行")
					.Text("冲刺", 1);
				break;
			case 187:
				r.Text("按下").Button(512).Text("进行")
					.Text("翻滚", 1);
				break;
			case 188:
				r.Text("按下").Button(256).Text("使用")
					.Text("电浆冲击", 1)
					.Text("来摧毁带有护盾的敌人");
				break;
			case 189:
				r.Text("按下").Button(256).Text("使用")
					.Text("封闭打击", 1)
					.Text("来摧毁带有护盾的敌人");
				break;
			case 190:
				r.Text("当它们短路时，使用").Text("冲刺", 1).Text("穿过它们");
				break;
			case 191:
				r.Text("按下").Button(256).Text("使用")
					.Text("电浆冲击", 1)
					.Text("来摧毁带电风扇");
				break;
			case 192:
				r.Text("当它们短路时，使用").Text("翻滚", 1).Text("穿过它们");
				break;
			case 193:
				r.Text("按下").Button(256).Text("使用")
					.Text("封闭打击", 1)
					.Text("来摧毁带电风扇");
				break;
			case 194:
				r.Text("烈日打击", 1).Text("充能完毕");
				break;
			case 195:
				r.Text("能量灌注", 1).Text("充能完毕");
				break;
			case 196:
				r.Text("按下").Button(2048).Text("使用");
				break;
			}
		}
	}
}
