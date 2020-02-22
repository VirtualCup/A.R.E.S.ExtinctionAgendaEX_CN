using ARES360.Audio;
using ARES360.Entity;
using ARES360.Input;
using ARES360.Profile;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace ARES360.UI
{
	public class GameSettingsPage : PositionedObject
	{
		public class Resolution
		{
			public int width;

			public int height;

			public byte ratio;
		}

		public class Row : ClickablePositionedObject
		{
			public int index;

			public Text label;

			private bool mAddedToLayers;

			public Vector3 RelativePosition
			{
				get
				{
					return positionedObject.RelativePosition;
				}
				set
				{
					positionedObject.RelativePosition = value;
				}
			}

			protected Text CreateText()
			{
				Text text = new Text(GUIHelper.CaptionFont);
				text.Scale = 1.1f;
				text.Spacing = 1.1f;
				text.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Left;
				text.VerticalAlignment = VerticalAlignment.Center;
				text.ColorOperation = ColorOperation.Texture;
				return text;
			}

			public virtual void Load()
			{
				positionedObject = new PositionedObject();
				label = CreateText();
				label.RelativePosition = new Vector3(-27.5f, 0f, 0f);
				label.AttachTo(positionedObject, false);
			}

			public virtual bool TryUpdate()
			{
				return false;
			}

			public void AttachTo(PositionedObject parent, bool changeRelative)
			{
				positionedObject.AttachTo(parent, changeRelative);
			}

			public virtual void DetachAndUnload()
			{
				label.Detach();
				label = null;
				positionedObject.Detach();
				positionedObject = null;
			}

			public void AddToLayers(Layer layer)
			{
				if (!mAddedToLayers)
				{
					mAddedToLayers = true;
					OnAddToLayers(layer);
				}
			}

			public void RemoveFromLayers()
			{
				if (mAddedToLayers)
				{
					mAddedToLayers = false;
					OnRemoveFromLayers();
				}
			}

			protected virtual void OnAddToLayers(Layer layer)
			{
				TextManager.AddToLayer(label, layer);
			}

			protected virtual void OnRemoveFromLayers()
			{
				TextManager.RemoveTextOneWay(label);
			}
		}

		public class OptionsRow : Row
		{
			public delegate bool UpdateHandler(OptionsRow row, int newIndex);

			public ClickableSprite leftArrow;

			public ClickableSprite rightArrow;

			public Text optionLabel;

			private int mSelectedIndex;

			public int TotalChoices;

			public UpdateHandler OnTryUpdateValue;

			public override bool Update(Vector3 mousePoint, bool mouseDown, bool mouseMove)
			{
				return leftArrow.Update(mousePoint, mouseDown, mouseMove) | rightArrow.Update(mousePoint, mouseDown, mouseMove) | base.Update(mousePoint, mouseDown, mouseMove);
			}

			public override bool TryUpdate()
			{
				if (GamePad.GetMenuRepeatKeyDown(1))
				{
					if (mSelectedIndex > 0 && OnTryUpdateValue(this, mSelectedIndex - 1))
					{
						SFXManager.PlaySound("ok");
						return true;
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(2) && mSelectedIndex + 1 < TotalChoices && OnTryUpdateValue(this, mSelectedIndex + 1))
				{
					SFXManager.PlaySound("ok");
					return true;
				}
				return false;
			}

			public void SetValue(int index, string displayText)
			{
				mSelectedIndex = index;
				leftArrow.sprite.Alpha = ((index > 0) ? 1f : 0.5f);
				rightArrow.sprite.Alpha = ((index + 1 < TotalChoices) ? 1f : 0.5f);
				optionLabel.DisplayText = displayText;
			}

			private bool OnMouseClickArrow(Clickable sender)
			{
				int num = mSelectedIndex + sender.tag;
				if (0 > num || num >= TotalChoices)
				{
					SFXManager.PlaySound("error");
					return true;
				}
				if (OnTryUpdateValue(this, num))
				{
					SFXManager.PlaySound("ok");
					return true;
				}
				return true;
			}

			public override void Load()
			{
				base.Load();
				leftArrow = new ClickableSprite
				{
					tag = -1,
					paddingLeft = 1f,
					paddingRight = 1f,
					paddingTop = 1f,
					paddingBottom = 1f,
					OnMouseClick = new ClickableHandler(OnMouseClickArrow)
				};
				leftArrow.sprite = GlobalSheet.CreateScaledSprite(134);
				leftArrow.sprite.ColorOperation = ColorOperation.Subtract;
				leftArrow.sprite.Red = 1f;
				leftArrow.sprite.Green = 0f;
				leftArrow.sprite.Blue = 0f;
				leftArrow.sprite.RelativePosition = new Vector3(-3f, 0f, 0f);
				leftArrow.sprite.AttachTo(positionedObject, false);
				rightArrow = new ClickableSprite
				{
					tag = 1,
					paddingLeft = 1f,
					paddingRight = 1f,
					paddingTop = 1f,
					paddingBottom = 1f,
					OnMouseClick = new ClickableHandler(OnMouseClickArrow)
				};
				rightArrow.sprite = GlobalSheet.CreateScaledSprite(137);
				rightArrow.sprite.ColorOperation = ColorOperation.Subtract;
				rightArrow.sprite.Red = 1f;
				rightArrow.sprite.Green = 0f;
				rightArrow.sprite.Blue = 0f;
				rightArrow.sprite.RelativePosition = new Vector3(18f, 0f, 0f);
				rightArrow.sprite.AttachTo(positionedObject, false);
				optionLabel = CreateText();
				optionLabel.Scale = 1.1f;
				optionLabel.Spacing = 1.1f;
				optionLabel.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Center;
				optionLabel.RelativePosition = new Vector3(7.5f, 0f, 0f);
				optionLabel.ColorOperation = ColorOperation.Subtract;
				optionLabel.SetColor(1f, 0f, 0f);
				optionLabel.AttachTo(positionedObject, false);
			}

			public override void DetachAndUnload()
			{
				leftArrow.sprite.Detach();
				leftArrow = null;
				rightArrow.sprite.Detach();
				rightArrow = null;
				optionLabel.Detach();
				optionLabel = null;
				base.DetachAndUnload();
			}

			protected override void OnAddToLayers(Layer layer)
			{
				base.OnAddToLayers(layer);
				SpriteManager.AddToLayer(leftArrow.sprite, layer);
				SpriteManager.AddToLayer(rightArrow.sprite, layer);
				TextManager.AddToLayer(optionLabel, layer);
			}

			protected override void OnRemoveFromLayers()
			{
				SpriteManager.RemoveSpriteOneWay(leftArrow.sprite);
				SpriteManager.RemoveSpriteOneWay(rightArrow.sprite);
				TextManager.RemoveTextOneWay(optionLabel);
				base.OnRemoveFromLayers();
			}
		}

		public class OnOffRow : Row
		{
			public delegate bool UpdateHandler(OnOffRow row, bool newValue);

			public ClickableSprite radioOn;

			public Text radioOnLabel;

			public ClickableSprite radioOff;

			public Text radioOffLabel;

			public UpdateHandler OnTryUpdateValue;

			private bool mValue;

			public override bool Update(Vector3 mousePoint, bool mouseDown, bool mouseMove)
			{
				return radioOn.Update(mousePoint, mouseDown, mouseMove) | radioOff.Update(mousePoint, mouseDown, mouseMove) | base.Update(mousePoint, mouseDown, mouseMove);
			}

			public override bool TryUpdate()
			{
				if (GamePad.GetMenuRepeatKeyDown(1))
				{
					if (!mValue && OnTryUpdateValue(this, true))
					{
						SFXManager.PlaySound("ok");
						return true;
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(2) && mValue && OnTryUpdateValue(this, false))
				{
					SFXManager.PlaySound("ok");
					return true;
				}
				return false;
			}

			public override void Load()
			{
				base.Load();
				radioOn = new ClickableSprite
				{
					paddingRight = 6f,
					OnMouseClick = (ClickableHandler)delegate
					{
						if (!mValue && OnTryUpdateValue(this, true))
						{
							SFXManager.PlaySound("ok");
							return true;
						}
						return true;
					}
				};
				radioOn.sprite = GlobalSheet.CreateScaledSprite(136);
				radioOn.sprite.RelativePosition = new Vector3(-2.5f, 0f, 0f);
				radioOn.sprite.AttachTo(positionedObject, false);
				radioOnLabel = CreateText();
				radioOnLabel.RelativePosition = new Vector3(-0.5f, 0f, 0f);
				radioOnLabel.ColorOperation = ColorOperation.Subtract;
				radioOnLabel.SetColor(1f, 0f, 0f);
				radioOnLabel.AttachTo(positionedObject, false);
				radioOff = new ClickableSprite
				{
					paddingRight = 6f,
					OnMouseClick = (ClickableHandler)delegate
					{
						if (mValue && OnTryUpdateValue(this, false))
						{
							SFXManager.PlaySound("ok");
							return true;
						}
						return true;
					}
				};
				radioOff.sprite = GlobalSheet.CreateScaledSprite(136);
				radioOff.sprite.RelativePosition = new Vector3(5.5f, 0f, 0f);
				radioOff.sprite.AttachTo(positionedObject, false);
				radioOffLabel = CreateText();
				radioOffLabel.RelativePosition = new Vector3(7.1f, 0f, 0f);
				radioOffLabel.ColorOperation = ColorOperation.Subtract;
				radioOffLabel.SetColor(1f, 0f, 0f);
				radioOffLabel.AttachTo(positionedObject, false);
			}

			public void SetValue(bool value)
			{
				mValue = value;
				if (value)
				{
					radioOn.sprite.TextureBound = GlobalSheet.GetTextureBound(135);
					radioOff.sprite.TextureBound = GlobalSheet.GetTextureBound(136);
					radioOnLabel.Alpha = 1f;
					radioOffLabel.Alpha = 0.3f;
				}
				else
				{
					radioOn.sprite.TextureBound = GlobalSheet.GetTextureBound(136);
					radioOff.sprite.TextureBound = GlobalSheet.GetTextureBound(135);
					radioOnLabel.Alpha = 0.3f;
					radioOffLabel.Alpha = 1f;
				}
			}

			public override void DetachAndUnload()
			{
				radioOn.sprite.Detach();
				radioOn = null;
				radioOff.sprite.Detach();
				radioOff = null;
				radioOnLabel.Detach();
				radioOnLabel = null;
				radioOffLabel.Detach();
				radioOffLabel = null;
				base.DetachAndUnload();
			}

			protected override void OnAddToLayers(Layer layer)
			{
				base.OnAddToLayers(layer);
				SpriteManager.AddToLayer(radioOn.sprite, layer);
				SpriteManager.AddToLayer(radioOff.sprite, layer);
				TextManager.AddToLayer(radioOnLabel, layer);
				TextManager.AddToLayer(radioOffLabel, layer);
			}

			protected override void OnRemoveFromLayers()
			{
				SpriteManager.RemoveSpriteOneWay(radioOn.sprite);
				SpriteManager.RemoveSpriteOneWay(radioOff.sprite);
				TextManager.RemoveTextOneWay(radioOnLabel);
				TextManager.RemoveTextOneWay(radioOffLabel);
				base.OnRemoveFromLayers();
			}
		}

		public class DifficultyRow : Row
		{
			public delegate bool UpdateHandler(DifficultyRow row, GameCombatDifficulty newValue);

			public ClickableText casualText;

			public ClickableText normalText;

			public ClickableText hardcoreText;

			public Sprite underline;

			private GameCombatDifficulty mValue;

			public UpdateHandler OnTryUpdateValue;

			public override bool Update(Vector3 mousePoint, bool mouseDown, bool mouseMove)
			{
				return casualText.Update(mousePoint, mouseDown, mouseMove) | normalText.Update(mousePoint, mouseDown, mouseMove) | hardcoreText.Update(mousePoint, mouseDown, mouseMove) | base.Update(mousePoint, mouseDown, mouseMove);
			}

			public override bool TryUpdate()
			{
				if (GamePad.GetMenuRepeatKeyDown(1))
				{
					GameCombatDifficulty newValue = mValue;
					switch (mValue)
					{
					case GameCombatDifficulty.Casual:
						return false;
					case GameCombatDifficulty.Normal:
						newValue = GameCombatDifficulty.Casual;
						break;
					case GameCombatDifficulty.Hardcore:
						newValue = GameCombatDifficulty.Normal;
						break;
					}
					if (OnTryUpdateValue(this, newValue))
					{
						SFXManager.PlaySound("ok");
						return true;
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(2))
				{
					GameCombatDifficulty newValue2 = mValue;
					switch (mValue)
					{
					case GameCombatDifficulty.Casual:
						newValue2 = GameCombatDifficulty.Normal;
						break;
					case GameCombatDifficulty.Normal:
						newValue2 = GameCombatDifficulty.Hardcore;
						break;
					case GameCombatDifficulty.Hardcore:
						return false;
					}
					if (OnTryUpdateValue(this, newValue2))
					{
						SFXManager.PlaySound("ok");
						return true;
					}
				}
				return false;
			}

			public void SetValue(GameCombatDifficulty difficulty)
			{
				mValue = difficulty;
				Text text = (mValue == GameCombatDifficulty.Normal) ? normalText.text : ((mValue != GameCombatDifficulty.Hardcore) ? casualText.text : hardcoreText.text);
				float width = TextManager.GetWidth(text);
				underline.RelativePosition = new Vector3(text.RelativeX + width / 2f, -1.64999962f, 0f);
				underline.ScaleX = width * 0.7f;
			}

			private bool OnMouseClickText(Clickable sender)
			{
				GameCombatDifficulty tag = (GameCombatDifficulty)sender.tag;
				if (tag == mValue)
				{
					return true;
				}
				if (OnTryUpdateValue(this, tag))
				{
					SFXManager.PlaySound("ok");
					return true;
				}
				return true;
			}

			public override void Load()
			{
				base.Load();
				casualText = new ClickableText
				{
					tag = 0,
					boundary = new Vector4(0f, -1.5f, 10f, 1.5f),
					OnMouseClick = new ClickableHandler(OnMouseClickText)
				};
				casualText.text = CreateText();
				casualText.text.ColorOperation = ColorOperation.Subtract;
				casualText.text.SetColor(1f, 0f, 0f);
				casualText.text.RelativePosition = new Vector3(-3f, 0f, 0f);
				casualText.text.AttachTo(positionedObject, false);
				normalText = new ClickableText
				{
					tag = 1,
					boundary = new Vector4(0f, -1.5f, 10f, 1.5f),
					OnMouseClick = new ClickableHandler(OnMouseClickText)
				};
				normalText.text = CreateText();
				normalText.text.ColorOperation = ColorOperation.Subtract;
				normalText.text.SetColor(1f, 0f, 0f);
				normalText.text.RelativePosition = new Vector3(7f, 0f, 0f);
				normalText.text.AttachTo(positionedObject, false);
				hardcoreText = new ClickableText
				{
					tag = 2,
					boundary = new Vector4(0f, -1.5f, 10f, 1.5f),
					OnMouseClick = new ClickableHandler(OnMouseClickText)
				};
				hardcoreText.text = CreateText();
				hardcoreText.text.ColorOperation = ColorOperation.Subtract;
				hardcoreText.text.SetColor(1f, 0f, 0f);
				hardcoreText.text.RelativePosition = new Vector3(17f, 0f, 0f);
				hardcoreText.text.AttachTo(positionedObject, false);
				underline = GlobalSheet.CreateScaledSprite(141);
				underline.AttachTo(positionedObject, false);
			}

			public override void DetachAndUnload()
			{
				casualText.text.Detach();
				casualText = null;
				normalText.text.Detach();
				normalText = null;
				hardcoreText.text.Detach();
				hardcoreText = null;
				underline.Detach();
				underline = null;
				base.DetachAndUnload();
			}

			protected override void OnAddToLayers(Layer layer)
			{
				base.OnAddToLayers(layer);
				TextManager.AddToLayer(casualText.text, layer);
				TextManager.AddToLayer(normalText.text, layer);
				TextManager.AddToLayer(hardcoreText.text, layer);
				SpriteManager.AddToLayer(underline, layer);
			}

			protected override void OnRemoveFromLayers()
			{
				TextManager.RemoveTextOneWay(casualText.text);
				TextManager.RemoveTextOneWay(normalText.text);
				TextManager.RemoveTextOneWay(hardcoreText.text);
				SpriteManager.RemoveSpriteOneWay(underline);
				base.OnRemoveFromLayers();
			}
		}

		public class SliderRow : Row
		{
			public delegate bool UpdateHandler(SliderRow row, int newValue);

			public Sprite bar;

			public Sprite cursor;

			public Text valueLabel;

			private int mValue;

			public UpdateHandler OnTryUpdateValue;

			public override void Load()
			{
				base.Load();
				bar = GlobalSheet.CreateSprite(139);
				bar.ScaleX = 12f;
				bar.ScaleY = 0.25f;
				bar.RelativePosition = new Vector3(8f, 0f, 0f);
				bar.AttachTo(positionedObject, false);
				cursor = GlobalSheet.CreateScaledSprite(140);
				cursor.RelativePosition = new Vector3(8f, 0f, 0f);
				cursor.AttachTo(positionedObject, false);
				valueLabel = CreateText();
				valueLabel.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Left;
				valueLabel.RelativePosition = new Vector3(22f, 0f, 0f);
				valueLabel.ColorOperation = ColorOperation.Subtract;
				valueLabel.SetColor(1f, 0f, 0f);
				valueLabel.Scale = 0.9f;
				valueLabel.Spacing = 0.9f;
				valueLabel.AttachTo(positionedObject, false);
			}

			public override bool TryUpdate()
			{
				int repeatCount = GamePad.GetRepeatCount();
				if (GamePad.GetMenuRepeatKeyDown(1))
				{
					if (mValue > 0 && OnTryUpdateValue(this, mValue - 1))
					{
						SFXManager.PlaySound("cursor");
						return true;
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(2))
				{
					if (mValue < 100 && OnTryUpdateValue(this, mValue + 1))
					{
						SFXManager.PlaySound("cursor");
						return true;
					}
				}
				else if (repeatCount > 1 && GamePad.GetMenuKey(1))
				{
					if (mValue > 0 && OnTryUpdateValue(this, mValue - 1))
					{
						return true;
					}
				}
				else if (repeatCount > 1 && GamePad.GetMenuKey(2) && mValue < 100 && OnTryUpdateValue(this, mValue + 1))
				{
					return true;
				}
				if (GamePad.keyboard.GetMouseButton(1))
				{
					Vector3 mouseWorldPoint = GamePad.GetMouseWorldPoint(cursor.Z, SpriteManager.Camera);
					float num = mouseWorldPoint.Y - cursor.Y;
					if (-1.5f <= num && num <= 1.5f)
					{
						float num2 = mouseWorldPoint.X - cursor.X;
						if (num2 < -0.1f)
						{
							if (mValue > 0 && OnTryUpdateValue(this, mValue - 1))
							{
								return true;
							}
						}
						else if (num2 > 0.1f && mValue < 100 && OnTryUpdateValue(this, mValue + 1))
						{
							return true;
						}
					}
				}
				return false;
			}

			public void SetValue(int value)
			{
				mValue = value;
				valueLabel.DisplayText = value.ToString();
				float num = (float)value * bar.ScaleX / 50f;
				cursor.RelativePosition = new Vector3(bar.RelativeX - bar.ScaleX + num, 0f, 0f);
			}

			public override void DetachAndUnload()
			{
				bar.Detach();
				bar = null;
				cursor.Detach();
				cursor = null;
				valueLabel.Detach();
				valueLabel = null;
				base.DetachAndUnload();
			}

			protected override void OnAddToLayers(Layer layer)
			{
				base.OnAddToLayers(layer);
				SpriteManager.AddToLayer(bar, layer);
				SpriteManager.AddToLayer(cursor, layer);
				TextManager.AddToLayer(valueLabel, layer);
			}

			protected override void OnRemoveFromLayers()
			{
				SpriteManager.RemoveSpriteOneWay(bar);
				SpriteManager.RemoveSpriteOneWay(cursor);
				TextManager.RemoveTextOneWay(valueLabel);
				base.OnRemoveFromLayers();
			}
		}

		public const int SETTINGS_NONE = 0;

		public const int SETTINGS_RESOLUTION = 1;

		public const int SETTINGS_WINDOW_MODE = 2;

		public const int SETTINGS_MUSIC_VOL = 4;

		public const int SETTINGS_FX_VOL = 8;

		public const int SETTINGS_VIBRATE = 16;

		public const int SETTINGS_JUMP_MODE = 32;

		public const int SETTINGS_APOLLO = 64;

		public const int SETTINGS_COMBAT_DIFF = 128;

		private const int STATE_IDLE = 0;

		private const int STATE_NEED_APPLY = 1;

		private const int STATE_CONFIRM_APPLY = 2;

		private const int STATE_ASK_APPLY = 3;

		private const int RATIO_NONE = 0;

		private const int RATIO_4_3 = 1;

		private const int RATIO_16_9 = 2;

		private const int RATIO_16_10 = 3;

		private OptionsRow mResolutionRow;

		private OptionsRow mWindowModeRow;

		private SliderRow mMusicRow;

		private SliderRow mFXRow;

		private OnOffRow mVibrateRow;

		private OnOffRow mJumpModeRow;

		private OnOffRow mDroneRow;

		private DifficultyRow mDifficultyRow;

		private Text mDifficultyText;

		private Sprite mDifficultyBackground;

		private Sprite mSelectedRowSprite;

		private int mProfileResolution;

		private int mSelectedWindowMode;

		private int mState;

		private static GameSettingsPage mInstance;

		public int dirty;

		private int mSelectedRow;

		private List<Row> mRows;

		private List<Resolution> mResolutions;

		private int mSelectedResolution;

		private int mCurrentResolutionWidth;

		private int mCurrentResolutionHeight;

		public static GameSettingsPage Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new GameSettingsPage();
				}
				return mInstance;
			}
		}

		public bool IsChangingSettings
		{
			get
			{
				if (mSelectedRow < 0)
				{
					return mState != 0;
				}
				return true;
			}
		}

		public void OnExit()
		{
			ProfileManager.ApplyConfigs();
		}

		private static string GetLocalizedCombatDifficultyDescription(GameCombatDifficulty difficulty)
		{
			switch (difficulty)
			{
			case GameCombatDifficulty.Casual:
				return "在休闲难度下，所有头目的伤害都会减半，且敌人不会使用护盾。\n但在这种模式下，你将无法获得最高的章节评价。";
			case GameCombatDifficulty.Hardcore:
				return "在硬核难度下，你的分数可以突破极限。\n但是敌人会强多一倍，并且修理技能的冷却时间会变长。";
			default:
				return "在普通难度下，所有游戏成就你都能解锁。\n但是你的得分会有上限。";
			}
		}

		public void Load()
		{
			mSelectedRowSprite = GlobalSheet.CreateScaledSprite(138);
			mSelectedRowSprite.AttachTo(this, false);
			mDifficultyText = new Text(GUIHelper.SpeechFont);
			mDifficultyText.NewLineDistance = 1.8f;
			mDifficultyText.Scale = 1f;
			mDifficultyText.Spacing = 1f;
			mDifficultyText.HorizontalAlignment = FlatRedBall.Graphics.HorizontalAlignment.Center;
			mDifficultyText.VerticalAlignment = VerticalAlignment.Top;
			mDifficultyText.ColorOperation = ColorOperation.Texture;
			mDifficultyText.DisplayText = string.Empty;
			mDifficultyText.AttachTo(this, false);
			mDifficultyText.RelativePosition = new Vector3(0f, -12f, 1f);
			mRows = new List<Row>();
			mResolutionRow = new OptionsRow();
			mResolutionRow.tag = 1;
			mResolutionRow.Load();
			mResolutionRow.label.DisplayText = "分辨率";
			mResolutionRow.OnTryUpdateValue = delegate(OptionsRow row, int newIndex)
			{
				mSelectedResolution = newIndex;
				Resolution res = mResolutions[newIndex];
				row.SetValue(newIndex, GetLocalizedResolution(res));
				dirty |= row.tag;
				ReloadNeedApply();
				return true;
			};
			mRows.Add(mResolutionRow);
			mWindowModeRow = new OptionsRow();
			mWindowModeRow.tag = 2;
			mWindowModeRow.Load();
			mWindowModeRow.label.DisplayText = "窗口模式";
			mWindowModeRow.TotalChoices = 3;
			mWindowModeRow.OnTryUpdateValue = delegate(OptionsRow row, int newIndex)
			{
				mSelectedWindowMode = newIndex;
				row.SetValue(newIndex, GetLocalizedWindowMode(newIndex));
				dirty |= row.tag;
				ReloadNeedApply();
				return true;
			};
			mRows.Add(mWindowModeRow);
			mMusicRow = new SliderRow();
			mMusicRow.tag = 4;
			mMusicRow.Load();
			mMusicRow.label.DisplayText = "音乐音量";
			mMusicRow.OnTryUpdateValue = delegate(SliderRow row, int newValue)
			{
				ProfileManager.Configs.MusicVolume = newValue;
				BGMManager.Volume = ProfileManager.Configs.RawMusicVolume;
				row.SetValue(newValue);
				dirty |= row.tag;
				return true;
			};
			mRows.Add(mMusicRow);
			mFXRow = new SliderRow();
			mFXRow.tag = 8;
			mFXRow.Load();
			mFXRow.label.DisplayText = "音效音量";
			mFXRow.OnTryUpdateValue = delegate(SliderRow row, int newValue)
			{
				ProfileManager.Configs.FXVolume = newValue;
				SFXManager.Volume = ProfileManager.Configs.RawFXVolume;
				row.SetValue(newValue);
				dirty |= row.tag;
				return true;
			};
			mRows.Add(mFXRow);
			mVibrateRow = new OnOffRow();
			mVibrateRow.tag = 16;
			mVibrateRow.Load();
			mVibrateRow.radioOnLabel.DisplayText = "开启";
			mVibrateRow.radioOffLabel.DisplayText = "关闭";
			mVibrateRow.label.DisplayText = "震动";
			mVibrateRow.OnTryUpdateValue = delegate(OnOffRow row, bool newValue)
			{
				ProfileManager.Configs.IsVibrateEnable = newValue;
				GamePad.EnabledVibration = newValue;
				row.SetValue(newValue);
				dirty |= row.tag;
				return true;
			};
			mRows.Add(mVibrateRow);
			mJumpModeRow = new OnOffRow();
			mJumpModeRow.tag = 32;
			mJumpModeRow.Load();
			mJumpModeRow.radioOnLabel.DisplayText = "开启";
			mJumpModeRow.radioOffLabel.DisplayText = "关闭";
			mJumpModeRow.label.DisplayText = "固定跳跃高度";
			mJumpModeRow.OnTryUpdateValue = delegate(OnOffRow row, bool newValue)
			{
				ProfileManager.Configs.JumpMode = ((!newValue) ? 1 : 0);
				row.SetValue(newValue);
				dirty |= row.tag;
				return true;
			};
			mRows.Add(mJumpModeRow);
			mDroneRow = new OnOffRow();
			mDroneRow.tag = 64;
			mDroneRow.Load();
			mDroneRow.radioOnLabel.DisplayText = "开启";
			mDroneRow.radioOffLabel.DisplayText = "关闭";
			mDroneRow.label.DisplayText = "阿波罗无人机";
			mDroneRow.OnTryUpdateValue = delegate(OnOffRow row, bool newValue)
			{
				if (ProfileManager.Profile.UseApollo != newValue && Player.Instance != null)
				{
					if (newValue)
					{
						Apollo.Instance.Call(Player.Instance.Position);
					}
					else
					{
						Apollo.Instance.Dismiss();
					}
				}
				ProfileManager.Profile.UseApollo = newValue;
				row.SetValue(newValue);
				dirty |= row.tag;
				return true;
			};
			if (Pref.HasApolloDLC)
			{
				mRows.Add(mDroneRow);
			}
			mDifficultyRow = new DifficultyRow();
			mDifficultyRow.tag = 128;
			mDifficultyRow.Load();
			mDifficultyRow.label.DisplayText = "战斗难度";
			mDifficultyRow.casualText.text.DisplayText = Pref.COMBAT_DIFFICULTY_TEXT_CASUAL;
			mDifficultyRow.normalText.text.DisplayText = Pref.COMBAT_DIFFICULTY_TEXT_NORMAL;
			mDifficultyRow.hardcoreText.text.DisplayText = Pref.COMBAT_DIFFICULTY_TEXT_HARDCORE;
			mDifficultyRow.OnTryUpdateValue = delegate(DifficultyRow row, GameCombatDifficulty newValue)
			{
				ProfileManager.CombatDifficulty = newValue;
				mDifficultyText.DisplayText = GetLocalizedCombatDifficultyDescription(newValue);
				row.SetValue(newValue);
				dirty |= row.tag;
				return true;
			};
			mRows.Add(mDifficultyRow);
			int count = mRows.Count;
			float num = 10f;
			if (!Pref.HasApolloDLC)
			{
				num -= 1.425f;
			}
			for (int i = 0; i < count; i++)
			{
				Row row2 = mRows[i];
				row2.index = i;
				row2.boundary = new Vector4(-28f, -1.5f, 28f, 1.5f);
				row2.RelativePosition = new Vector3(0f, num, 0.2f);
				row2.AttachTo(this, false);
				row2.OnMouseEnter = delegate
				{
					if (mSelectedRow != row2.index)
					{
						SFXManager.PlaySound("cursor");
						SelectRow(row2.index);
					}
				};
				row2.OnMouseClick = delegate
				{
					if (mSelectedRow != row2.index)
					{
						SFXManager.PlaySound("cursor");
						SelectRow(row2.index);
						return true;
					}
					return true;
				};
				num -= 2.85f;
			}
		}

		private void ReloadNeedApply()
		{
			if (mSelectedResolution != mProfileResolution || mSelectedWindowMode != ProfileManager.Configs.WindowMode)
			{
				if (mState != 1)
				{
					mState = 1;
					ControlHint.Instance.Clear().AddHint(8388608, "应用").AddHint(1048576, "返回")
						.ShowHints(FlatRedBall.Graphics.HorizontalAlignment.Right)
						.FadeIn();
				}
			}
			else if (mState != 0)
			{
				mState = 0;
				ControlHint.Instance.Clear().AddHint(1048576, "返回").ShowHints(FlatRedBall.Graphics.HorizontalAlignment.Right)
					.FadeIn();
			}
		}

		public void Unload()
		{
			int count = mRows.Count;
			for (int i = 0; i < count; i++)
			{
				mRows[i].DetachAndUnload();
			}
			mRows = null;
			mResolutions = null;
			mDifficultyText.Detach();
			mDifficultyText = null;
			mSelectedRowSprite.Detach();
			mSelectedRowSprite = null;
			mResolutionRow = null;
			mWindowModeRow = null;
			mMusicRow = null;
			mFXRow = null;
			mVibrateRow = null;
			mJumpModeRow = null;
			mDroneRow = null;
			mDifficultyRow = null;
		}

		public void AddAllComponentsToManager()
		{
			TextManager.AddText(mDifficultyText, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mSelectedRowSprite, GUIHelper.UILayer);
			int count = mRows.Count;
			for (int i = 0; i < count; i++)
			{
				mRows[i].AddToLayers(GUIHelper.UILayer);
			}
		}

		public void RemoveAllComponentsFromManager()
		{
			TextManager.RemoveTextOneWay(mDifficultyText);
			SpriteManager.RemoveSpriteOneWay(mSelectedRowSprite);
			int count = mRows.Count;
			for (int i = 0; i < count; i++)
			{
				mRows[i].RemoveFromLayers();
			}
		}

		public void Show()
		{
			Show(0);
		}

		public void Show(int cursor)
		{
			dirty = 0;
			if (ProfileManager.Current == null)
			{
				switch (ProfileManager.Profile.LastPlayedCharacter)
				{
				case PlayerType.Tarus:
					ProfileManager.Current = ProfileManager.Tarus;
					break;
				default:
					ProfileManager.Current = ProfileManager.Mars;
					break;
				}
			}
			LoadResolutions();
			mProfileResolution = mSelectedResolution;
			mSelectedWindowMode = ProfileManager.Configs.WindowMode;
			mResolutionRow.TotalChoices = mResolutions.Count;
			mResolutionRow.SetValue(mSelectedResolution, GetLocalizedResolution(mResolutions[mSelectedResolution]));
			mWindowModeRow.SetValue(mSelectedWindowMode, GetLocalizedWindowMode(mSelectedWindowMode));
			mMusicRow.SetValue(ProfileManager.Configs.MusicVolume);
			mFXRow.SetValue(ProfileManager.Configs.FXVolume);
			mVibrateRow.SetValue(ProfileManager.Configs.IsVibrateEnable);
			mJumpModeRow.SetValue(ProfileManager.Configs.JumpMode == 0);
			mDroneRow.SetValue(ProfileManager.Profile.UseApollo);
			mDifficultyRow.SetValue(ProfileManager.CombatDifficulty);
			mDifficultyText.DisplayText = GetLocalizedCombatDifficultyDescription(ProfileManager.CombatDifficulty);
			mState = 0;
			ControlHint.Instance.Clear().AddHint(1048576, "返回").ShowHints(FlatRedBall.Graphics.HorizontalAlignment.Right);
			switch (cursor)
			{
			case 64:
				SelectRow(mDroneRow.index);
				break;
			case 128:
				SelectRow(mDifficultyRow.index);
				break;
			case 8:
				SelectRow(mFXRow.index);
				break;
			case 4:
				SelectRow(mMusicRow.index);
				break;
			case 1:
				SelectRow(mResolutionRow.index);
				break;
			case 16:
				SelectRow(mVibrateRow.index);
				break;
			case 32:
				SelectRow(mJumpModeRow.index);
				break;
			case 2:
				SelectRow(mWindowModeRow.index);
				break;
			default:
				SelectRow(-1);
				break;
			}
		}

		public bool CanExit()
		{
			if (PopUpUI.Instance.IsRunning)
			{
				return false;
			}
			if (mState == 0)
			{
				return true;
			}
			return false;
		}

		private void SelectRow(int row)
		{
			mSelectedRow = row;
			if (row >= 0)
			{
				Vector3 relativePosition = mRows[mSelectedRow].positionedObject.RelativePosition;
				relativePosition.Z = 0f;
				mSelectedRowSprite.RelativePosition = relativePosition;
				mSelectedRowSprite.Visible = true;
			}
			else
			{
				mSelectedRowSprite.Visible = false;
			}
		}

		public void Update()
		{
			if (mState == 0)
			{
				if (GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
				{
					if (mSelectedRow >= 0)
					{
						SelectRow(mSelectedRow - 1);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
				{
					if (mSelectedRow + 1 < mRows.Count)
					{
						SelectRow(mSelectedRow + 1);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (mSelectedRow < 0 || !mRows[mSelectedRow].TryUpdate())
				{
					GamePad.UpdateClickables(mRows);
				}
			}
			else if (mState == 1)
			{
				if (GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
				{
					if (mSelectedRow >= 0)
					{
						SelectRow(mSelectedRow - 1);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
				{
					if (mSelectedRow + 1 < mRows.Count)
					{
						SelectRow(mSelectedRow + 1);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.GetMenuKeyDown(8388608))
				{
					SFXManager.PlaySound("ok");
					mState = 2;
					ExecuteApplyVideoSettings();
				}
				else if (GamePad.GetMenuKeyDown(1048576))
				{
					SFXManager.PlaySound("ok");
					mState = 3;
					AskToApplyVideoSettings();
				}
				else
				{
					if (mSelectedRow >= 0)
					{
						if (mRows[mSelectedRow].TryUpdate())
						{
							return;
						}
					}
					else if (GamePad.GetMenuRepeatKeyDown(2))
					{
						SFXManager.PlaySound("ok");
						mState = 3;
						AskToApplyVideoSettings();
						return;
					}
					GamePad.UpdateClickables(mRows);
				}
			}
		}

		public bool ShouldSwitchToTab(int tab)
		{
			if (mState == 1)
			{
				SFXManager.PlaySound("ok");
				mState = 3;
				AskToApplyVideoSettings();
				return false;
			}
			return true;
		}

		private void ExecuteApplyVideoSettings()
		{
			try
			{
				_ApplyVideoSettingsToFlatRedBall();
			}
			catch (Exception)
			{
				RevertVideoSettings();
				_ApplyVideoSettingsToFlatRedBall();
				ReloadNeedApply();
				return;
			}
			PopUpUI.Instance.ShowCountDown("确认设置", "是否要保留现在的设置？\n会在{0}秒后还原。", 15f, delegate(PopUpUI.PopUpUIResult result)
			{
				if (result == PopUpUI.PopUpUIResult.Ok)
				{
					ProfileManager.Configs.WindowMode = mSelectedWindowMode;
					mProfileResolution = mSelectedResolution;
					Resolution resolution = mResolutions[mProfileResolution];
					ProfileManager.Configs.ScreenWidth = resolution.width;
					ProfileManager.Configs.ScreenHeight = resolution.height;
					ReloadNeedApply();
				}
				else
				{
					RevertVideoSettings();
					_ApplyVideoSettingsToFlatRedBall();
					ReloadNeedApply();
				}
			});
		}

		private void AskToApplyVideoSettings()
		{
			PopUpUI.Instance.Show("应用设置", "是否应用这些设置？", delegate(PopUpUI.PopUpUIResult result)
			{
				if (result == PopUpUI.PopUpUIResult.Ok)
				{
					ExecuteApplyVideoSettings();
				}
				else
				{
					RevertVideoSettings();
					ReloadNeedApply();
				}
			});
		}

		public static Form GetForm()
		{
			return (Form)Control.FromHandle(FlatRedBallServices.Game.Window.Handle);
		}

		private void _ApplyVideoSettingsToFlatRedBall()
		{
			Resolution resolution = mResolutions[mSelectedResolution];
			Pref.Width = resolution.width;
			Pref.Height = resolution.height;
			Pref.FullScreen = (mSelectedWindowMode == 0);
			Form form = GetForm();
			FormBorderStyle formBorderStyle = form.FormBorderStyle;
			switch (mSelectedWindowMode)
			{
			case 2:
				Pref.FullScreen = false;
				formBorderStyle = FormBorderStyle.None;
				break;
			case 0:
				Pref.FullScreen = true;
				formBorderStyle = FormBorderStyle.None;
				break;
			case 1:
				Pref.FullScreen = false;
				formBorderStyle = FormBorderStyle.Fixed3D;
				break;
			}
			Thread.Sleep(300);
			FlatRedBallServices.SuspendEngine();
			Thread.Sleep(300);
			FlatRedBallServices.GraphicsOptions.SetResolution(Pref.Width, Pref.Height, Pref.FullScreen);
			Thread.Sleep(300);
			if (mSelectedWindowMode == 1 || mSelectedWindowMode == 2)
			{
				FlatRedBallServices.GraphicsOptions.SetResolution(Pref.Width, Pref.Height, Pref.FullScreen);
			}
			Thread.Sleep(300);
			SpriteManager.Camera.DestinationRectangle = new Rectangle(0, 0, FlatRedBallServices.GraphicsDevice.Viewport.Width, FlatRedBallServices.GraphicsDevice.Viewport.Height);
			Thread.Sleep(300);
			form.FormBorderStyle = formBorderStyle;
			if (formBorderStyle == FormBorderStyle.None)
			{
				form.Width = Pref.Width;
				form.Height = Pref.Height;
				form.Left = ProfileManager.Configs.ScreenLeft;
				form.Top = ProfileManager.Configs.ScreenTop;
			}
			Thread.Sleep(300);
			FlatRedBallServices.UnsuspendEngine();
			BossUI.Instance.AdjustPosition();
			ComboUI.Instance.AdjustPosition();
			MaterialUI.Instance.AdjustPosition();
			StatusUI.Instance.AdjustPosition();
			HintUI.Instance.AdjustPosition();
		}

		private void RevertVideoSettings()
		{
			mSelectedResolution = mProfileResolution;
			mSelectedWindowMode = ProfileManager.Configs.WindowMode;
			Resolution res = mResolutions[mSelectedResolution];
			mResolutionRow.SetValue(mSelectedResolution, GetLocalizedResolution(res));
			mWindowModeRow.SetValue(mSelectedWindowMode, GetLocalizedWindowMode(mSelectedWindowMode));
		}

		public string GetLocalizedWindowMode(int mode)
		{
			switch (mode)
			{
			case 2:
				return "无边框";
			case 1:
				return "窗口";
			default:
				return "全屏";
			}
		}

		private string GetLocalizedResolution(Resolution res)
		{
			switch (res.ratio)
			{
			case 0:
				return $"{res.width}x{res.height}";
			case 1:
				return $"{res.width}x{res.height} (4:3)";
			case 2:
				return $"{res.width}x{res.height} (16:9)";
			case 3:
				return $"{res.width}x{res.height} (16:10)";
			default:
				return null;
			}
		}

		private byte GuessResolutionRatio(int width, int height)
		{
			float num = (float)width;
			float num2 = (float)height;
			float num3 = 16f * num2 / num;
			if (11.9f <= num3 && num3 <= 12.1f)
			{
				return 1;
			}
			if (9.9f <= num3 && num3 <= 10.1f)
			{
				return 3;
			}
			if (8.9f <= num3 && num3 <= 9.1f)
			{
				return 2;
			}
			return 0;
		}

		private bool TryAddResolution(int width, int height, byte ratio)
		{
			if (width > Pref.NativeScreenWidth)
			{
				return false;
			}
			if (height > Pref.NativeScreenHeight)
			{
				return false;
			}
			if (mSelectedResolution == -1)
			{
				if (width == mCurrentResolutionWidth && height == mCurrentResolutionHeight)
				{
					mSelectedResolution = mResolutions.Count;
				}
				else if (width == mCurrentResolutionWidth && height > mCurrentResolutionHeight)
				{
					mSelectedResolution = mResolutions.Count;
					mResolutions.Add(new Resolution
					{
						width = mCurrentResolutionWidth,
						height = mCurrentResolutionHeight,
						ratio = GuessResolutionRatio(mCurrentResolutionWidth, mCurrentResolutionHeight)
					});
				}
				else if (width > mCurrentResolutionWidth)
				{
					mSelectedResolution = mResolutions.Count;
					mResolutions.Add(new Resolution
					{
						width = mCurrentResolutionWidth,
						height = mCurrentResolutionHeight,
						ratio = GuessResolutionRatio(mCurrentResolutionWidth, mCurrentResolutionHeight)
					});
				}
			}
			mResolutions.Add(new Resolution
			{
				width = width,
				height = height,
				ratio = ratio
			});
			return true;
		}

		private void LoadResolutions()
		{
			mCurrentResolutionWidth = ProfileManager.Configs.ScreenWidth;
			mCurrentResolutionHeight = ProfileManager.Configs.ScreenHeight;
			if (mCurrentResolutionWidth == 0 || mCurrentResolutionHeight == 0)
			{
				mCurrentResolutionWidth = Pref.Width;
				mCurrentResolutionHeight = Pref.Height;
			}
			mSelectedResolution = -1;
			mResolutions = new List<Resolution>();
			TryAddResolution(1280, 720, 2);
			TryAddResolution(1280, 800, 3);
			TryAddResolution(1366, 768, 2);
			TryAddResolution(1440, 900, 3);
			TryAddResolution(1600, 900, 2);
			TryAddResolution(1680, 945, 2);
			TryAddResolution(1680, 1050, 3);
			TryAddResolution(1920, 1080, 2);
			TryAddResolution(1920, 1200, 3);
			TryAddResolution(2560, 1440, 2);
			TryAddResolution(2560, 1600, 3);
			if (mSelectedResolution < 0)
			{
				mSelectedResolution = mResolutions.Count;
				mResolutions.Add(new Resolution
				{
					width = mCurrentResolutionWidth,
					height = mCurrentResolutionHeight,
					ratio = GuessResolutionRatio(mCurrentResolutionWidth, mCurrentResolutionHeight)
				});
			}
		}
	}
}
