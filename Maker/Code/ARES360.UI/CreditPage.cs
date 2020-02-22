using ARES360.Input;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ARES360.UI
{
	public class CreditPage : PositionedObject
	{
		private const int TOTAL_LABELS = 12;

		private const float LINE_HEIGHT = 2.2f;

		private const float PAGE_HEIGHT = 25f;

		private static CreditPage mInstance;

		private string[] mLines;

		private List<Text> mLabels;

		private float mOffset;

		public static CreditPage Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new CreditPage();
				}
				return mInstance;
			}
		}

		private CreditPage()
		{
		}

		private bool IsTitle(int index)
		{
			if (index <= 0)
			{
				return true;
			}
			if (mLines[index] == null)
			{
				return false;
			}
			if (mLines[index - 1] == null)
			{
				return true;
			}
			return false;
		}

		public void Load()
		{
			mLines = new string[138]
			{
				"游戏主管",
				"Nenin Ananbanchachai",
				null,
				"开发主管",
				"Chakkapun Singto-ngam",
				null,
				"美术主管",
				"Somjade Chuntavorn",
				null,
				"高级制作人",
				"Adam McClard",
				null,
				"主游戏设计",
				"Nenin Ananbanchachai",
				"Adam McClard",
				null,
				"游戏设计",
				"Somjade Chuntavorn",
				"Chakkapun Singto-ngam",
				"Siruit Busayapoka",
				"Adam McClard",
				null,
				"主程序",
				"Chakkapun Singto-ngam",
				null,
				"程序",
				"Nenin Ananbanchachai",
				"Chanyut Leecharoen",
				"Sittipon Simasanti",
				"Siruit Busayapoka",
				"Putt Sakdhnagool",
				null,
				null,
				"引擎程序",
				"Chanyut Leecharoen",
				null,
				"技术程序",
				"Sittipon Simasanti",
				null,
				"主美术",
				"Somjade Chuntavorn",
				"Sylvain Magne",
				null,
				"美术",
				"Aeksiam Wuttirak",
				null,
				"动画",
				"Somjade Chuntavorn",
				"Aeksiam Wuttirak",
				null,
				"电影 ",
				"Sylvain Magne",
				null,
				"故事",
				"Adam McClard",
				"Gavin Frankle",
				null,
				"原作",
				"Chakkapun Singto-ngam",
				null,
				"助理制作人",
				"David E. Klein",
				null,
				"市场和公关",
				"David E. Klein",
				null,
				"商务拓展",
				"David E. Klein",
				null,
				"开发团队",
				"Extend Studio",
				"Extend Interactive Co., Ltd.",
				"Origo Games Pte., Ltd.",
				null,
				"音乐和音效",
				"Hyperduck SoundWorks",
				"Christopher Geehan",
				"Daniel Byrne-Mccullough",
				null,
				null,
				"追加音乐",
				"Charlie Parra del Riego",
				null,
				"品控领队",
				"Nenin Ananbanchachai",
				null,
				"品控测试员",
				"Chakkapun Singto-ngam",
				"Somjade Chuntavorn",
				"Siruit Busayapoka",
				"Chanyut Leecharoen",
				"Sittipon Simasanti",
				"Adam McClard",
				null,
				"特别鸣谢",
				"Charlie Parra del Riego",
				"Victor Chelaru and FlatRedBall Team",
				"Putt Sakdhnagool",
				"Ari Patrick",
				"Sirawat Pitaksarit",
				"David E. Klein ",
				"Silvaine Magne",
				null,
				null,
				"英语本地化",
				"Aksys Games Localization Inc.",
				null,
				"以下为Aksys员工",
				null,
				"执行制作人",
				"Akibo Shieh",
				null,
				"首席财务官",
				"Michiko Shieh",
				null,
				"总经理",
				"Yoshikazu Morizuka",
				null,
				"许可证管理员",
				"Yuko Abe",
				null,
				"产品负责人",
				"Frank \"Bo\" Dewindt II",
				null,
				"合作编辑",
				"Michael Engler",
				null,
				"品控测试",
				"Digital Hearts USA Inc.",
				null,
				null,
				"市场",
				"Russel Iriye",
				null,
				null,
				null,
				null,
				"感谢你的游玩！"
			};
			Reset();
			mLabels = new List<Text>(12);
			for (int num = 11; num >= 0; num--)
			{
				Text text = new Text(GUIHelper.SpeechFont);
				text.Scale = 1f;
				text.Spacing = 1f;
				text.AdjustPositionForPixelPerfectDrawing = false;
				text.HorizontalAlignment = HorizontalAlignment.Center;
				text.VerticalAlignment = VerticalAlignment.Center;
				text.ColorOperation = ColorOperation.Texture;
				text.DisplayText = string.Empty;
				text.AttachTo(this, false);
				mLabels.Add(text);
			}
		}

		public void Unload()
		{
			for (int num = 11; num >= 0; num--)
			{
				mLabels[num].Detach();
			}
			mLabels = null;
			mLines = null;
		}

		public void Update()
		{
			UpdateLabels();
		}

		public void Show()
		{
			for (int num = mLabels.Count - 1; num >= 0; num--)
			{
				TextManager.AddToLayer(mLabels[num], GUIHelper.UILayer);
			}
			UpdateLabels();
		}

		private void UpdateLabels()
		{
			float num = TimeManager.SecondDifference * 3f;
			if (GamePad.GetMenuKey(524288))
			{
				num *= 5f;
			}
			mOffset += num;
			int num2 = 0;
			int num3 = mLines.Length;
			float num4 = mOffset;
			float num5 = -25f;
			float num6 = 0f;
			for (int i = 0; i < num3; i++)
			{
				bool flag = IsTitle(i);
				num4 -= 2.2f;
				if (num5 <= num4 && num4 <= num6)
				{
					string text = mLines[i];
					if (text != null)
					{
						Text text2 = mLabels[num2];
						text2.Visible = true;
						if (flag)
						{
							text2.Font = GUIHelper.CaptionFont;
						}
						else
						{
							text2.Font = GUIHelper.SpeechFont;
						}
						text2.DisplayText = text;
						float num7 = num4 - num5;
						float num8 = num6 - num4;
						if (num8 < num7)
						{
							num7 = num8;
						}
						if (num7 < 1f)
						{
							text2.Alpha = num7 * num7;
						}
						else
						{
							text2.Alpha = 1f;
						}
						text2.RelativePosition = new Vector3(0f, num4 + 10f, 1f);
						num2++;
					}
				}
				if (flag)
				{
					num4 -= 0.2f;
				}
			}
			if (num2 == 0 && mOffset > 0f)
			{
				Reset();
			}
			for (int j = num2; j < 12; j++)
			{
				mLabels[j].Visible = false;
			}
		}

		public void Reset()
		{
			mOffset = -25f;
		}

		public void Hide()
		{
			for (int num = mLabels.Count - 1; num >= 0; num--)
			{
				TextManager.RemoveTextOneWay(mLabels[num]);
			}
		}
	}
}
