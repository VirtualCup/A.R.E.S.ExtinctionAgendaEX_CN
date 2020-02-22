using ARES360.Entity;
using ARES360.Profile;
using ARES360.UI;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace ARES360.Screen
{
	public class CharacterDetailPanel : PositionedObject, IProcessable
	{
		private const int FIELD_COUNT = 5;

		private const int FIELD_INDEX_PLAYINGTIME = 0;

		private const int FIELD_INDEX_COMBAT_DIFFICULTY = 1;

		private const int FIELD_INDEX_CHAPTER = 2;

		private const int FIELD_INDEX_MATERIAL = 3;

		private const int FIELD_INDEX_SCORE = 4;

		private const float FIELD_START_POS_Y = 3f;

		private const float FIELDKEY_COLUMN_POS_X = -12f;

		private const float FIELDVALUE_COLUMN_POS_X = 2f;

		private const float FIELD_NEWLINE_SPACE = 2f;

		private PlayerProfile mCurrentPlayerProfile;

		private Sprite mCharacterHeaderSprite;

		private Sprite mMainPanel;

		private Sprite mRank;

		private Sprite mBadge;

		private Text mPlayerDataHeaderText;

		private Text[] mFieldKeyTexts;

		private Text[] mFieldValueTexts;

		private string[] mFieldKeys;

		private string[] mFieldValues;

		private bool IsLoaded;

		private bool mIsInManagers;

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

		public CharacterDetailPanel()
		{
			IsLoaded = false;
			mCurrentPlayerProfile = null;
			mFieldKeys = new string[5]
			{
				"通关时间：",
				"战斗难度：",
				"章节：",
				"材料：",
				"总得分："
			};
			mFieldValues = new string[5]
			{
				"03:45:12",
				"Normal",
				"2",
				"2000",
				"1550"
			};
		}

		public void LoadVisualComponents()
		{
			if (!IsLoaded)
			{
				mMainPanel = new Sprite();
				mMainPanel.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/character_detail_panel", "Global");
				mMainPanel.ScaleX = 15f;
				mMainPanel.ScaleY = 9f;
				mMainPanel.AttachTo(this, false);
				mBadge = new Sprite();
				mBadge.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/selectchar_badge", "Global");
				mBadge.ScaleX = 2.53125f;
				mBadge.ScaleY = 3.5625f;
				mBadge.AttachTo(this, false);
				mCharacterHeaderSprite = new Sprite();
				mCharacterHeaderSprite.ScaleX = 12f;
				mCharacterHeaderSprite.ScaleY = 3.625f;
				mCharacterHeaderSprite.RelativePosition = new Vector3(-4f, 12f, 0f);
				mCharacterHeaderSprite.AttachTo(mMainPanel, false);
				mPlayerDataHeaderText = new Text(GUIHelper.CaptionFont);
				mPlayerDataHeaderText.Scale = 1f;
				mPlayerDataHeaderText.Spacing = 1f;
				mPlayerDataHeaderText.ColorOperation = ColorOperation.Texture;
				mPlayerDataHeaderText.VerticalAlignment = VerticalAlignment.Center;
				mPlayerDataHeaderText.RelativePosition = new Vector3(-12f, 6.5f, 0f);
				mPlayerDataHeaderText.DisplayText = "玩家数据";
				mPlayerDataHeaderText.AttachTo(mMainPanel, false);
				Vector2 textureScale = LocalizedSheet.GetTextureScale(13);
				mRank = LocalizedSheet.CreateSprite(13);
				mRank.AttachTo(mMainPanel, false);
				mRank.ScaleY = 2f;
				mRank.ScaleX = mRank.ScaleY / textureScale.Y * textureScale.X;
				mFieldKeyTexts = new Text[5];
				mFieldValueTexts = new Text[5];
				for (int i = 0; i < 5; i++)
				{
					mFieldKeyTexts[i] = new Text(GUIHelper.SpeechFont);
					mFieldKeyTexts[i].Scale = 1f;
					mFieldKeyTexts[i].Spacing = 1f;
					mFieldKeyTexts[i].ColorOperation = ColorOperation.Texture;
					mFieldKeyTexts[i].VerticalAlignment = VerticalAlignment.Center;
					mFieldKeyTexts[i].HorizontalAlignment = HorizontalAlignment.Left;
					mFieldKeyTexts[i].DisplayText = mFieldKeys[i];
					mFieldValueTexts[i] = new Text(GUIHelper.SpeechFont);
					mFieldValueTexts[i].Scale = 1f;
					mFieldValueTexts[i].Spacing = 1f;
					mFieldValueTexts[i].ColorOperation = ColorOperation.Texture;
					mFieldValueTexts[i].VerticalAlignment = VerticalAlignment.Center;
					mFieldValueTexts[i].HorizontalAlignment = HorizontalAlignment.Left;
					mFieldValueTexts[i].DisplayText = mFieldValues[i];
					if (i > 0)
					{
						mFieldKeyTexts[i].RelativePosition = mFieldKeyTexts[i - 1].RelativePosition;
						mFieldKeyTexts[i].RelativePosition.Y -= 2f;
						mFieldValueTexts[i].RelativePosition = mFieldValueTexts[i - 1].RelativePosition;
						mFieldValueTexts[i].RelativePosition.Y -= 2f;
					}
					else
					{
						mFieldKeyTexts[i].RelativePosition = new Vector3(-12f, 3f, 0.02f);
						mFieldValueTexts[i].RelativePosition = new Vector3(2f, 3f, 0.02f);
					}
					mFieldKeyTexts[i].AttachTo(mMainPanel, false);
					mFieldValueTexts[i].AttachTo(mMainPanel, false);
				}
				IsLoaded = true;
			}
		}

		public void UnloadVisualComponents()
		{
			if (IsLoaded)
			{
				IsLoaded = false;
				mBadge.Detach();
				mRank.Detach();
				mMainPanel.Detach();
				mCharacterHeaderSprite.Detach();
				mPlayerDataHeaderText.Detach();
				for (int i = 0; i < 5; i++)
				{
					mFieldKeyTexts[i].Detach();
					mFieldValueTexts[i].Detach();
				}
				mRank = null;
				mMainPanel = null;
				mCharacterHeaderSprite = null;
				mPlayerDataHeaderText = null;
				mFieldKeyTexts = null;
				mFieldValueTexts = null;
			}
		}

		public void SetProfile(PlayerProfile profile)
		{
			if (mCurrentPlayerProfile != profile)
			{
				mCurrentPlayerProfile = profile;
				SetVisualComponentsToCurrentProfile();
			}
		}

		public void SetVisualComponentsToCurrentProfile()
		{
			PlayerProfile playerProfile = mCurrentPlayerProfile;
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)playerProfile.PlayTime);
			mFieldValueTexts[0].DisplayText = $"{timeSpan.Hours:00}:{timeSpan.Minutes:00}:{timeSpan.Seconds:00}";
			mFieldValueTexts[2].DisplayText = playerProfile.CurrentLevel.ToString();
			mFieldValueTexts[3].DisplayText = ProfileManager.Profile.MaterialAmount.ToString();
			mFieldValueTexts[4].DisplayText = $"{playerProfile.Experience}";
			int key = 13 + playerProfile.Rank;
			Vector2 textureScale = LocalizedSheet.GetTextureScale(key);
			mRank.TextureBound = LocalizedSheet.GetTextureBound(key);
			mRank.ScaleX = mRank.ScaleY / textureScale.Y * textureScale.X;
			switch (playerProfile.CombatDifficulty)
			{
			default:
				mFieldValueTexts[1].DisplayText = "普通";
				break;
			case GameCombatDifficulty.Casual:
				mFieldValueTexts[1].DisplayText = "休闲";
				break;
			case GameCombatDifficulty.Hardcore:
				mFieldValueTexts[1].DisplayText = "硬核";
				break;
			}
			if (mCurrentPlayerProfile.PlayerType == PlayerType.Mars)
			{
				mCharacterHeaderSprite.RelativeX = -4f;
				mCharacterHeaderSprite.TextureBound = LocalizedSheet.GetTextureBound(3);
				mCharacterHeaderSprite.Texture = LocalizedSheet.Texture;
				mMainPanel.FlipHorizontal = false;
				mPlayerDataHeaderText.RelativePosition = new Vector3(-12f, 6.5f, 0f);
				mRank.RelativePosition = new Vector3(0.16f, 12.05f, 0.1f);
				mBadge.Visible = (playerProfile.Score[6] > 0);
				mBadge.RelativePosition = new Vector3(14.633f, 5.549f, 0.1f);
			}
			else if (mCurrentPlayerProfile.PlayerType == PlayerType.Tarus)
			{
				mCharacterHeaderSprite.RelativeX = 4f;
				mCharacterHeaderSprite.TextureBound = LocalizedSheet.GetTextureBound(4);
				mCharacterHeaderSprite.Texture = LocalizedSheet.Texture;
				mMainPanel.FlipHorizontal = true;
				mPlayerDataHeaderText.RelativePosition = new Vector3(-0.7f, 6.5f, 0f);
				mRank.RelativePosition = new Vector3(-3.32f, 12.05f, 0.1f);
				mBadge.Visible = (playerProfile.Score[6] > 0);
				mBadge.RelativePosition = new Vector3(-14.633f, 5.549f, 0.1f);
			}
		}

		public void OnRegister()
		{
			SpriteManager.AddToLayer(mRank, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mBadge, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mCharacterHeaderSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mMainPanel, GUIHelper.UILayer);
			TextManager.AddToLayer(mPlayerDataHeaderText, GUIHelper.UILayer);
			for (int i = 0; i < 5; i++)
			{
				TextManager.AddToLayer(mFieldKeyTexts[i], GUIHelper.UILayer);
				TextManager.AddToLayer(mFieldValueTexts[i], GUIHelper.UILayer);
			}
			SpriteManager.AddPositionedObject(this);
			mIsInManagers = true;
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		public void OnRemove()
		{
			if (mIsInManagers)
			{
				mIsInManagers = false;
				SpriteManager.RemoveSpriteOneWay(mRank);
				SpriteManager.RemoveSpriteOneWay(mBadge);
				SpriteManager.RemoveSpriteOneWay(mCharacterHeaderSprite);
				SpriteManager.RemoveSpriteOneWay(mMainPanel);
				TextManager.RemoveTextOneWay(mPlayerDataHeaderText);
				for (int i = 0; i < 5; i++)
				{
					TextManager.RemoveTextOneWay(mFieldKeyTexts[i]);
					TextManager.RemoveTextOneWay(mFieldValueTexts[i]);
				}
				SpriteManager.RemovePositionedObject(this);
			}
		}

		public void Update()
		{
		}

		public void PausedUpdate()
		{
		}
	}
}
