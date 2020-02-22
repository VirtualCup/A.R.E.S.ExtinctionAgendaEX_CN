using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ARES360.UI
{
	public static class GUIHelper
	{
		public static Layer UILayer;

		public static Layer WorldLayer;

		public static BitmapFont SpeechFont;

		public static BitmapFont CaptionFont;

		public static Dictionary<int, Dictionary<int, UpgradeMenuInfo>> UpgradeInfo;

		public static Dictionary<int, LevelMenuInfo> LevelInfo;

		public static Dictionary<int, KeyValuePair<int, Vector3>[]> ChipLocation;

		public static DataMenuInfo[] DataCubeInfo;

		public static Sprite UIMask;

		public static Sprite TopMask;

		public static void LoadFonts()
		{
			if (SpeechFont == null)
			{
				Texture2D texture1 = FlatRedBallServices.Load<Texture2D>("Content/UI/speech_cn", "Global");
				SpeechFont = new BitmapFont(texture1, Localized.GetContentPath("UI/speech_cn.fnt"), Vector4.Zero);
				Texture2D texture2 = FlatRedBallServices.Load<Texture2D>("Content/UI/caption_cn", "Global");
				CaptionFont = new BitmapFont(texture2, Localized.GetContentPath("UI/caption_cn.fnt"), Vector4.Zero);
			}
		}

		public static void SetChapterGrade(Text grade, int score)
		{
			if (score >= 10000)
			{
				grade.DisplayText = "ss";
				grade.Red = 0f;
				grade.Green = 1f;
				grade.Blue = 0f;
			}
			else if (score >= 9000)
			{
				grade.DisplayText = "s";
				grade.Red = 0f;
				grade.Green = 1f;
				grade.Blue = 0.5f;
			}
			else if (score >= 8000)
			{
				grade.DisplayText = "a";
				grade.Red = 0f;
				grade.Green = 1f;
				grade.Blue = 1f;
			}
			else if (score >= 6500)
			{
				grade.DisplayText = "b";
				grade.Red = 0f;
				grade.Green = 0.5f;
				grade.Blue = 1f;
			}
			else if (score >= 5000)
			{
				grade.DisplayText = "c";
				grade.Red = 0f;
				grade.Green = 0f;
				grade.Blue = 1f;
			}
			else if (score > 0)
			{
				grade.DisplayText = "d";
				grade.Red = 1f;
				grade.Green = 0f;
				grade.Blue = 1f;
			}
			else
			{
				grade.DisplayText = "-";
				grade.Red = 0f;
				grade.Green = 0f;
				grade.Blue = 0f;
			}
		}
	}
}
