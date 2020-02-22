using ARES360.Audio;
using ARES360.Input;
using ARES360.Profile;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace ARES360.UI
{
	public class AchievementUI : PositionedObject, IProcessable
	{
		private const int ICONS_PER_ROW = 8;

		private static AchievementUI mInstance;

		private Sprite mPanel;

		private Text mStatusLabel;

		private Text mObjectiveLabel;

		private Text mCollectedLabel;

		private Sprite mBigPicture;

		private Text mBigPictureLabel;

		private ClickableSprite[] mClickableIcons;

		private Sprite mSelectedIcon;

		private Text[] mTexts;

		private Sprite[] mSprites;

		private int mSelectedIndex;

		public VoidEventHandler OnBackButtonPressed;

		public static AchievementUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new AchievementUI();
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

		public bool IsActivated
		{
			get;
			set;
		}

		public bool IsRemoveProcessWhenPressBack
		{
			get;
			set;
		}

		private AchievementUI()
		{
		}

		public void Load()
		{
			mPanel = new Sprite();
			mPanel.ScaleX = 47.875f;
			mPanel.ScaleY = 22.875f;
			mPanel.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/achievement_panel"));
			mPanel.AttachTo(this, false);
			mBigPicture = AchievementSheet.CreateSprite(6);
			mBigPicture.ScaleX = 7f;
			mBigPicture.ScaleY = 7f;
			mBigPicture.RelativePosition = new Vector3(25.4f, 7.4f, 0.1f);
			mBigPicture.AttachTo(this, false);
			mBigPictureLabel = new Text(GUIHelper.CaptionFont);
			mBigPictureLabel.DisplayText = "the first of many yahoo google";
			mBigPictureLabel.ColorOperation = ColorOperation.Subtract;
			mBigPictureLabel.MaxWidth = 16f;
			mBigPictureLabel.MaxWidthBehavior = MaxWidthBehavior.Wrap;
			mBigPictureLabel.SetColor(1f, 0f, 0f);
			mBigPictureLabel.Scale = 0.8f;
			mBigPictureLabel.Spacing = 0.8f;
			mBigPictureLabel.HorizontalAlignment = HorizontalAlignment.Center;
			mBigPictureLabel.VerticalAlignment = VerticalAlignment.Top;
			mBigPictureLabel.RelativePosition = new Vector3(25.5f, -2f, 0.1f);
			mBigPictureLabel.AttachTo(this, false);
			mStatusLabel = new Text(GUIHelper.CaptionFont);
			mStatusLabel.DisplayText = "complete";
			mStatusLabel.HorizontalAlignment = HorizontalAlignment.Left;
			mStatusLabel.VerticalAlignment = VerticalAlignment.Center;
			mStatusLabel.ColorOperation = ColorOperation.Subtract;
			mStatusLabel.SetColor(0.9f, 0f, 0.8f);
			mStatusLabel.Scale = 0.8f;
			mStatusLabel.Spacing = 0.8f;
			mStatusLabel.RelativePosition = new Vector3(24.5f, -6.73f, 0.1f);
			mStatusLabel.AttachTo(this, false);
			mObjectiveLabel = new Text(GUIHelper.SpeechFont);
			mObjectiveLabel.DisplayText = "Beat goliath the first of many yahoo google";
			mObjectiveLabel.NewLineDistance = 1.8f;
			mObjectiveLabel.MaxWidth = 16f;
			mObjectiveLabel.Scale = 0.8f;
			mObjectiveLabel.Spacing = 0.8f;
			mObjectiveLabel.MaxWidthBehavior = MaxWidthBehavior.Wrap;
			mObjectiveLabel.HorizontalAlignment = HorizontalAlignment.Left;
			mObjectiveLabel.VerticalAlignment = VerticalAlignment.Top;
			mObjectiveLabel.ColorOperation = ColorOperation.Texture;
			mObjectiveLabel.RelativePosition = new Vector3(17.25f, -10f, 0.1f);
			mObjectiveLabel.AttachTo(this, false);
			mCollectedLabel = new Text(GUIHelper.CaptionFont);
			mCollectedLabel.DisplayText = "collected 5 / 31";
			mCollectedLabel.ColorOperation = ColorOperation.Subtract;
			mCollectedLabel.SetColor(1f, 0f, 0f);
			mCollectedLabel.Scale = 0.75f;
			mCollectedLabel.Spacing = 0.75f;
			mCollectedLabel.HorizontalAlignment = HorizontalAlignment.Left;
			mCollectedLabel.VerticalAlignment = VerticalAlignment.Center;
			mCollectedLabel.RelativePosition = new Vector3(-31.25f, -13.5f, 0.1f);
			mCollectedLabel.AttachTo(this, false);
			mSelectedIcon = AchievementSheet.CreateScaledSprite(61);
			mSelectedIcon.AttachTo(this, false);
			mTexts = new Text[4]
			{
				mBigPictureLabel,
				mStatusLabel,
				mObjectiveLabel,
				mCollectedLabel
			};
			mSprites = new Sprite[3]
			{
				mPanel,
				mSelectedIcon,
				mBigPicture
			};
			mClickableIcons = new ClickableSprite[31];
			float num = -28.7f;
			float num2 = 8.7f;
			float num3 = 5.7f;
			float num4 = -5.7f;
			mSelectedIcon.RelativePosition = new Vector3(num, num2, 0.1f);
			int num5 = 0;
			int num6 = 0;
			for (int i = 0; i < 31; i++)
			{
				float x = num + num3 * (float)num6;
				float y = num2 + num4 * (float)num5;
				Sprite sprite = AchievementSheet.CreateScaledSprite(60);
				Vector3 vector = sprite.RelativePosition = new Vector3(x, y, 0f);
				sprite.AttachTo(this, false);
				int achievementSheetKey = AchievementManager.GetAchievementSheetKey(i);
				Sprite sprite2 = AchievementSheet.CreateScaledSprite(achievementSheetKey + 1);
				sprite2.RelativePosition = new Vector3(0f, 0f, 0.2f);
				sprite2.AttachTo(sprite, false);
				mClickableIcons[i] = new ClickableSprite
				{
					sprite = sprite2,
					tag = i,
					OnMouseEnter = new Clickable.ClickableHoverHandler(OnMouseEnterIcon),
					OnMouseClick = new Clickable.ClickableHandler(OnMouseClickIcon)
				};
				num6++;
				if (num6 >= 8)
				{
					num5++;
					num6 = 0;
				}
			}
		}

		public void Unload()
		{
			for (int num = mTexts.Length - 1; num >= 0; num--)
			{
				mTexts[num].Detach();
			}
			for (int num2 = mSprites.Length - 1; num2 >= 0; num2--)
			{
				mSprites[num2].Detach();
			}
			mTexts = null;
			mSprites = null;
			mCollectedLabel = null;
			mObjectiveLabel = null;
			mStatusLabel = null;
			mBigPictureLabel = null;
			mBigPicture = null;
			mSelectedIcon = null;
			mPanel = null;
			for (int i = 0; i < 31; i++)
			{
				ClickableSprite clickableSprite = mClickableIcons[i];
				Sprite sprite = clickableSprite.sprite;
				PositionedObject parent = sprite.Parent;
				sprite.Detach();
				parent.Detach();
				parent = null;
				sprite = null;
			}
			mClickableIcons = null;
		}

		private void OnMouseEnterIcon(Clickable sender)
		{
			if (mSelectedIndex != sender.tag)
			{
				mSelectedIndex = sender.tag;
				ReloadSelectedAchievement();
				SFXManager.PlaySound("cursor");
			}
		}

		private bool OnMouseClickIcon(Clickable sender)
		{
			if (mSelectedIndex != sender.tag)
			{
				mSelectedIndex = sender.tag;
				ReloadSelectedAchievement();
				SFXManager.PlaySound("cursor");
			}
			return true;
		}

		private void ReloadAchievements()
		{
			int num = 0;
			for (int i = 0; i < 31; i++)
			{
				ClickableSprite clickableSprite = mClickableIcons[i];
				Sprite sprite = clickableSprite.sprite;
				if (ProfileManager.Profile.HasAwardedAchievement(i))
				{
					sprite.Alpha = 1f;
					num++;
				}
				else
				{
					sprite.Alpha = 0.25f;
				}
			}
			mCollectedLabel.DisplayText = $"已收集 {num} / {31}";
		}

		private void ReloadSelectedAchievement()
		{
			int num = mSelectedIndex;
			Vector3 relativePosition = mClickableIcons[num].sprite.Parent.RelativePosition;
			relativePosition.Z = 0.1f;
			mSelectedIcon.RelativePosition = relativePosition;
			int achievementSheetKey = AchievementManager.GetAchievementSheetKey(num);
			mBigPicture.TextureBound = AchievementSheet.GetTextureBound(achievementSheetKey);
			bool flag = ProfileManager.Profile.HasAwardedAchievement(num);
			mBigPicture.Alpha = (flag ? 1f : 0.5f);
			mBigPictureLabel.DisplayText = AchievementManager.GetLocalizedCaptionName(num);
			mObjectiveLabel.DisplayText = AchievementManager.GetLocalizedObjective(num);
			if (flag)
			{
				mStatusLabel.DisplayText = "已完成";
				mStatusLabel.SetColor(0.443137258f, 0.223529413f, 0.745098054f);
			}
			else
			{
				mStatusLabel.DisplayText = "未完成";
				mStatusLabel.SetColor(0f, 0.9f, 0.8f);
			}
		}

		public void Show()
		{
			ReloadAchievements();
			ReloadSelectedAchievement();
			ProcessManager.AddProcess(this);
		}

		public void Hide()
		{
			ControlHint.Instance.HideHints();
			if (LoadingUI.Instance.IsRunning)
			{
				LoadingUI.Instance.Hide();
			}
			ProcessManager.RemoveProcess(this);
		}

		public void OnRegister()
		{
			SpriteManager.AddPositionedObject(this);
			for (int num = mTexts.Length - 1; num >= 0; num--)
			{
				TextManager.AddToLayer(mTexts[num], GUIHelper.UILayer);
			}
			for (int num2 = mSprites.Length - 1; num2 >= 0; num2--)
			{
				SpriteManager.AddToLayer(mSprites[num2], GUIHelper.UILayer);
			}
			for (int i = 0; i < 31; i++)
			{
				Sprite sprite = mClickableIcons[i].sprite;
				PositionedObject parent = sprite.Parent;
				SpriteManager.AddToLayer(parent as Sprite, GUIHelper.UILayer);
				SpriteManager.AddToLayer(sprite, GUIHelper.UILayer);
			}
			ControlHint.Instance.Clear().AddHint(1048576, "返回").ShowHints(HorizontalAlignment.Right);
		}

		public void OnRemove()
		{
			Detach();
			SpriteManager.RemovePositionedObject(this);
			for (int num = mTexts.Length - 1; num >= 0; num--)
			{
				TextManager.RemoveTextOneWay(mTexts[num]);
			}
			for (int num2 = mSprites.Length - 1; num2 >= 0; num2--)
			{
				SpriteManager.RemoveSpriteOneWay(mSprites[num2]);
			}
			for (int i = 0; i < 31; i++)
			{
				Sprite sprite = mClickableIcons[i].sprite;
				PositionedObject parent = sprite.Parent;
				SpriteManager.RemoveSpriteOneWay(parent as Sprite);
				SpriteManager.RemoveSpriteOneWay(sprite);
			}
			ControlHint.Instance.HideHints();
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		private void UpdateState()
		{
			if (GamePad.GetMenuRepeatKeyDown(8))
			{
				if (mSelectedIndex + 8 < 31)
				{
					mSelectedIndex += 8;
					ReloadSelectedAchievement();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuRepeatKeyDown(4))
			{
				if (mSelectedIndex >= 8)
				{
					mSelectedIndex -= 8;
					ReloadSelectedAchievement();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuRepeatKeyDown(1))
			{
				int num = mSelectedIndex % 8;
				if (num > 0)
				{
					mSelectedIndex--;
					ReloadSelectedAchievement();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.GetMenuRepeatKeyDown(2))
			{
				int num2 = mSelectedIndex % 8;
				if (num2 + 1 < 8 && mSelectedIndex + 1 < 31)
				{
					mSelectedIndex++;
					ReloadSelectedAchievement();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.keyboard.GetMouseScrollUp())
			{
				if (mSelectedIndex > 0)
				{
					mSelectedIndex--;
					ReloadSelectedAchievement();
					SFXManager.PlaySound("cursor");
				}
			}
			else if (GamePad.keyboard.GetMouseScrollDown())
			{
				if (mSelectedIndex + 1 < 31)
				{
					mSelectedIndex++;
					ReloadSelectedAchievement();
					SFXManager.PlaySound("cursor");
				}
			}
			else
			{
				if (GamePad.GetMenuKeyDown(1048576))
				{
					SFXManager.PlaySound("New_MenuBack");
					if (IsRemoveProcessWhenPressBack)
					{
						Hide();
					}
					if (OnBackButtonPressed != null)
					{
						OnBackButtonPressed();
					}
				}
				GamePad.UpdateClickables((IList<ClickableSprite>)mClickableIcons);
			}
		}

		public void Update()
		{
			if (IsActivated)
			{
				UpdateState();
			}
		}

		public void PausedUpdate()
		{
			if (IsActivated)
			{
				UpdateState();
			}
		}
	}
}
