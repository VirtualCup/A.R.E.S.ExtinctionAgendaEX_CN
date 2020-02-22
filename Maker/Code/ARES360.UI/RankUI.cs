using ARES360.Entity;
using ARES360.Profile;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;

namespace ARES360.UI
{
	public class RankUI : IProcessable
	{
		private const float DISTANCE_FROM_CAMERA = 70f;

		private PositionedObject mRoot;

		private static RankUI mInstance;

		private Sprite mRankBackground;

		private Sprite mRankCharacter;

		private Sprite mRankCircleA;

		private Sprite mRankCircleB;

		private Sprite mRank;

		private Text mRankCharacterLabel;

		private Text mRankLabel;

		private bool mIsLoaded;

		public static RankUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new RankUI();
				}
				return mInstance;
			}
		}

		public bool IsAdding
		{
			get;
			set;
		}

		public bool IsRemoving
		{
			get;
			set;
		}

		public bool IsRunning
		{
			get;
			set;
		}

		public void Load()
		{
			mRoot = new PositionedObject();
			mRank = LocalizedSheet.CreateSprite(13);
			mRankBackground = MenuSheet.CreateSprite(7);
			mRankCharacter = MenuSheet.CreateSprite(8);
			mRankCircleA = MenuSheet.CreateSprite(0);
			mRankCircleB = MenuSheet.CreateSprite(1);
			mRankCharacterLabel = new Text(GUIHelper.CaptionFont);
			mRankLabel = new Text(GUIHelper.CaptionFont);
			mIsLoaded = true;
		}

		public void Unload()
		{
			mIsLoaded = false;
			mRoot = null;
			mRank = null;
			mRankBackground = null;
			mRankCharacter = null;
			mRankCircleA = null;
			mRankCircleB = null;
			mRankCharacterLabel = null;
			mRankLabel = null;
		}

		public void Show()
		{
			if (mIsLoaded)
			{
				if (Player.Instance != null && Player.Instance.Type == PlayerType.Tarus)
				{
					mRankCharacterLabel.DisplayText = "塔鲁斯";
					if (ProfileManager.Current.ArmorLevel <= 0)
					{
						mRankCharacter.TextureBound = MenuSheet.GetTextureBound(10);
					}
					else
					{
						mRankCharacter.TextureBound = MenuSheet.GetTextureBound(11);
					}
				}
				else
				{
					mRankCharacterLabel.DisplayText = "阿瑞斯";
					if (ProfileManager.Current.ArmorLevel <= 0)
					{
						mRankCharacter.TextureBound = MenuSheet.GetTextureBound(8);
					}
					else
					{
						mRankCharacter.TextureBound = MenuSheet.GetTextureBound(9);
					}
				}
				mRankLabel.DisplayText = "玩 家 等 级 ：";
				mRank.Texture = LocalizedSheet.Texture;
				int key = 13 + ProfileManager.Current.Rank;
				Vector2 textureScale = LocalizedSheet.GetTextureScale(key);
				mRank.TextureBound = LocalizedSheet.GetTextureBound(key);
				mRank.ScaleY = 1.5f;
				mRank.ScaleX = mRank.ScaleY / textureScale.Y * textureScale.X;
				ProcessManager.AddProcess(this);
			}
		}

		public void Hide()
		{
			ProcessManager.RemoveProcess(this);
		}

		public void OnPause()
		{
		}

		public void OnRegister()
		{
			float xViewVolByDistance = World.Camera.GetXViewVolByDistance(70f);
			float yViewVolByDistance = World.Camera.GetYViewVolByDistance(70f);
			mRoot.RelativePosition = new Vector3(0f - xViewVolByDistance, 0f - yViewVolByDistance - 0.5f, -70f);
			mRoot.AttachTo(World.Camera, false);
			SpriteManager.AddPositionedObject(mRoot);
			mRankBackground.ScaleX = 16f;
			mRankBackground.ScaleY = 9f;
			mRankBackground.RelativePosition = new Vector3(mRankBackground.ScaleX, mRankBackground.ScaleY, 0f);
			mRankBackground.AttachTo(mRoot, false);
			SpriteManager.AddToLayer(mRankBackground, SpriteManager.TopLayer);
			mRankCircleA.ScaleX = 7.75f;
			mRankCircleA.ScaleY = 7.75f;
			mRankCircleA.RelativePosition = new Vector3(5.5f, 7f, 0f);
			mRankCircleA.RelativeRotationZVelocity = 2f;
			mRankCircleA.AttachTo(mRoot, false);
			SpriteManager.AddToLayer(mRankCircleA, SpriteManager.TopLayer);
			mRankCircleB.ScaleX = 7.75f;
			mRankCircleB.ScaleY = 7.75f;
			mRankCircleB.RelativePosition = mRankCircleA.RelativePosition;
			mRankCircleB.RelativeRotationZVelocity = -2f;
			mRankCircleB.AttachTo(mRoot, false);
			SpriteManager.AddToLayer(mRankCircleB, SpriteManager.TopLayer);
			mRankCharacter.ScaleX = 9f;
			mRankCharacter.ScaleY = 10f;
			mRankCharacter.RelativeX = mRankCharacter.ScaleX;
			mRankCharacter.RelativeY = mRankCharacter.ScaleY;
			mRankCharacter.AttachTo(mRoot, false);
			SpriteManager.AddToLayer(mRankCharacter, SpriteManager.TopLayer);
			mRankLabel.HorizontalAlignment = HorizontalAlignment.Left;
			mRankLabel.VerticalAlignment = VerticalAlignment.Top;
			mRankLabel.Scale = 0.5f;
			mRankLabel.Spacing = 0.5f;
			mRankLabel.NewLineDistance = 2.2f;
			mRankLabel.RelativePosition = new Vector3(16.5f, 8.5f, 0.2f);
			mRankLabel.ColorOperation = ColorOperation.Texture;
			mRankLabel.AttachTo(mRoot, false);
			TextManager.AddToLayer(mRankLabel, SpriteManager.TopLayer);
			mRankCharacterLabel.HorizontalAlignment = HorizontalAlignment.Center;
			mRankCharacterLabel.VerticalAlignment = VerticalAlignment.Top;
			mRankCharacterLabel.Scale = 0.875f;
			mRankCharacterLabel.Spacing = 0.875f;
			mRankCharacterLabel.RelativePosition = new Vector3(11f, 5.8f, 0.2f);
			mRankCharacterLabel.NewLineDistance = 2.2f;
			mRankCharacterLabel.ColorOperation = ColorOperation.Texture;
			mRankCharacterLabel.AttachTo(mRoot, false);
			TextManager.AddToLayer(mRankCharacterLabel, SpriteManager.TopLayer);
			mRank.RelativePosition = new Vector3(19.85f, 5.5f, 0f);
			mRank.AttachTo(mRoot, false);
			mRank.ScaleY = 1.5f;
			SpriteManager.AddToLayer(mRank, SpriteManager.TopLayer);
		}

		public void OnRemove()
		{
			mRankBackground.Detach();
			mRoot.Detach();
			mRankCircleA.Detach();
			mRankCircleB.Detach();
			mRankCharacter.Detach();
			mRankLabel.Detach();
			mRankCharacterLabel.Detach();
			mRank.Detach();
			SpriteManager.RemoveSpriteOneWay(mRankCircleA);
			SpriteManager.RemoveSpriteOneWay(mRankCircleB);
			SpriteManager.RemoveSpriteOneWay(mRankCharacter);
			SpriteManager.RemoveSpriteOneWay(mRankBackground);
			SpriteManager.RemoveSpriteOneWay(mRank);
			TextManager.RemoveTextOneWay(mRankCharacterLabel);
			TextManager.RemoveTextOneWay(mRankLabel);
			SpriteManager.RemovePositionedObject(mRoot);
		}

		public void OnResume()
		{
		}

		public void PausedUpdate()
		{
		}

		public void Update()
		{
		}
	}
}
