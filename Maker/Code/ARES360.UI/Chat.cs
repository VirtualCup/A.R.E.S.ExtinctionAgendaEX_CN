using FlatRedBall;
using Microsoft.Xna.Framework.Graphics;

namespace ARES360.UI
{
	public class Chat
	{
		public const byte MARS = 10;

		public const byte TARUS = 20;

		public const byte JULIA = 30;

		public const byte ZYTRON = 40;

		public const byte VALKYL = 50;

		public const byte MARS_NORMAL = 10;

		public const byte MARS_ARMSCROSSED = 11;

		public const byte MARS_BACK = 12;

		public const byte MARS_CONTAMINATED = 13;

		public const byte MARS_DETERMINED = 14;

		public const byte MARS_GUN = 15;

		public const byte MARS_SURPRISED = 17;

		public const byte MARS2_DETERMINED = 16;

		public const byte MARS2_GUN = 18;

		public const byte MARS2_NORMAL = 19;

		public const byte TARUS_NORMAL = 20;

		public const byte TARUS_BACK = 21;

		public const byte TARUS_FIST = 22;

		public const byte TARUS_FINGER = 23;

		public const byte TARUS_ZYTRON = 25;

		public const byte TARUS_ZYTRON_BACK = 26;

		public const byte TARUS_ZYTRON_FIST = 27;

		public const byte TARUS_GLOW = 28;

		public const byte JULIA_WORRIED = 30;

		public const byte JULIA_SERIOUS = 31;

		public const byte JULIA_SERIOUS2 = 32;

		public const byte ZYTRON_NORMAL = 40;

		public const byte VALKYL_NORMAL = 50;

		public const byte VALKYL_NORMAL2 = 51;

		public const byte VALKYL_ALARMED = 52;

		public const byte VALKYL_AMUSED = 53;

		public const byte VALKYL_ANGRY = 54;

		public const byte VALKYL_EXCITED = 55;

		public const byte VALKYL_OUTRAGED = 56;

		public const byte VALKYL_OVERWHELMED = 57;

		public const byte VALKYL_PROUD = 58;

		public const byte VALKYL_SAD = 59;

		public const byte VALKYL_SCARED = 60;

		public const byte VALKYL_SERIOUS = 61;

		public const byte VALKYL_SERIOUS2 = 62;

		public const byte VALKYL_SHOCKED = 63;

		public const byte VALKYL_SMILEY = 64;

		public const byte VALKYL_SMILEY2 = 65;

		public const byte VALKYL_SURPRISED = 66;

		public const byte VALKYL_SYMPATHETIC = 67;

		public const byte VALKYL_THOUGHTFUL = 68;

		public const byte VALKYL_UNIMPRESSED = 69;

		public const byte VALKYL_UNIMPRESSED2 = 70;

		public const byte VALKYL_WORRIED = 71;

		public const byte ANCHOR_AUTO = 0;

		public const byte ANCHOR_LEFT = 2;

		public const byte ANCHOR_RIGHT = 1;

		public const byte TRANSITION_AUTO = 0;

		public static string MARS_NAME = "阿瑞斯";

		public static string TARUS_NAME = "塔鲁斯";

		public static string JULIA_NAME = "朱莉娅";

		public static string ZYTRON_NAME = "灾创";

		public static string VALKYL_NAME = "瓦尔基莉";

		public byte Character;

		public byte Anchor;

		public byte Transition;

		private byte mExpression;

		public string SoundName;

		public string[] Lines;

		public byte Expression
		{
			get
			{
				return mExpression;
			}
			set
			{
				mExpression = value;
				switch ((int)value / 10)
				{
				case 1:
					Character = 10;
					break;
				case 2:
					Character = 20;
					break;
				case 3:
					Character = 30;
					break;
				case 4:
					Character = 40;
					break;
				default:
					Character = 50;
					break;
				}
			}
		}

		public string Text
		{
			set
			{
				Lines = value.Split('\n');
			}
		}

		public static string GetCharacterName(byte character)
		{
			switch (character)
			{
			case 10:
				return MARS_NAME;
			case 20:
				return TARUS_NAME;
			case 30:
				return JULIA_NAME;
			case 40:
				return ZYTRON_NAME;
			case 50:
				return VALKYL_NAME;
			default:
				return null;
			}
		}

		public static Sprite CreateExpressionSprite(byte expression)
		{
			Sprite sprite = new Sprite();
			switch (expression)
			{
			case 10:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars_normal", "PlayWorld");
				break;
			case 11:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars_armscrossed", "PlayWorld");
				break;
			case 12:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars_back", "PlayWorld");
				break;
			case 13:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars2_contaminated", "PlayWorld");
				break;
			case 14:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars_determined", "PlayWorld");
				break;
			case 15:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars_gun", "PlayWorld");
				break;
			case 17:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars_surprised", "PlayWorld");
				break;
			case 18:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars2_gun", "PlayWorld");
				break;
			case 16:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars2_determined", "PlayWorld");
				break;
			case 19:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/mars2_normal", "PlayWorld");
				break;
			case 20:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_normal", "PlayWorld");
				break;
			case 21:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_back", "PlayWorld");
				break;
			case 22:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_fist", "PlayWorld");
				break;
			case 23:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_finger", "PlayWorld");
				break;
			case 25:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_zytron", "PlayWorld");
				break;
			case 26:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_zytron_back", "PlayWorld");
				break;
			case 27:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_zytron_fist", "PlayWorld");
				break;
			case 28:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/tarus_glow", "PlayWorld");
				break;
			case 30:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/julia_worried", "PlayWorld");
				break;
			case 31:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/julia_serious", "PlayWorld");
				break;
			case 32:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/julia_serious2", "PlayWorld");
				break;
			case 40:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/zytron_normal", "PlayWorld");
				break;
			case 50:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_normal", "PlayWorld");
				break;
			case 51:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_normal2", "PlayWorld");
				break;
			case 52:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_alarmed", "PlayWorld");
				break;
			case 53:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_amused", "PlayWorld");
				break;
			case 54:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_angry", "PlayWorld");
				break;
			case 55:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_excited", "PlayWorld");
				break;
			case 56:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_outraged", "PlayWorld");
				break;
			case 57:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_overwhelmed", "PlayWorld");
				break;
			case 58:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_proud", "PlayWorld");
				break;
			case 59:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_sad", "PlayWorld");
				break;
			case 60:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_scared", "PlayWorld");
				break;
			case 61:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_serious", "PlayWorld");
				break;
			case 62:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_serious2", "PlayWorld");
				break;
			case 63:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_shocked", "PlayWorld");
				break;
			case 64:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_smiley", "PlayWorld");
				break;
			case 65:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_smiley2", "PlayWorld");
				break;
			case 66:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_surprised", "PlayWorld");
				break;
			case 67:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_sympathetic", "PlayWorld");
				break;
			case 68:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_thoughtful", "PlayWorld");
				break;
			case 69:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_unimpressed", "PlayWorld");
				break;
			case 70:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_unimpressed2", "PlayWorld");
				break;
			case 71:
				sprite.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/Character/valkyl_worried", "PlayWorld");
				break;
			}
			return sprite;
		}

		public Chat(byte expression, byte anchor, string[] lines, string soundName)
		{
			Expression = expression;
			Anchor = anchor;
			Lines = lines;
			SoundName = soundName;
		}

		public Chat(byte expression, byte anchor, string[] lines)
			: this(expression, anchor, lines, string.Empty)
		{
		}

		public Chat(byte expression, string[] lines)
			: this(expression, 0, lines, string.Empty)
		{
		}

		public Chat(byte expression, string[] lines, string soundName)
			: this(expression, 0, lines, soundName)
		{
		}
	}
}
