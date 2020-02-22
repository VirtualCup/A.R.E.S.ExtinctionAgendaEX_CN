using ARES360.Audio;
using ARES360.Input;
using ARES360.Profile;
using ARES360Loader;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace ARES360.UI
{
	public class GamePadPage : PositionedObject
	{
		public class Row : Clickable
		{
			public Text controlLabel;

			public Text primaryValue;

			public Text secondaryValue;

			public ClickablePositionedObject primaryClickable;

			public ClickablePositionedObject secondaryClickable;

			private bool mPrimaryValueEnabled = true;

			private bool mSecondaryValueEnabled = true;

			private Vector3 mRelativePosition;

			private bool mAddedToLayers;

			public override float Z => controlLabel.Z;

			public bool PrimaryValueEnabled
			{
				get
				{
					return mPrimaryValueEnabled;
				}
				set
				{
					mPrimaryValueEnabled = value;
					primaryValue.Alpha = (value ? 1f : 0.5f);
				}
			}

			public bool SecondaryValueEnabled
			{
				get
				{
					return mSecondaryValueEnabled;
				}
				set
				{
					mSecondaryValueEnabled = value;
					secondaryValue.Alpha = (value ? 1f : 0.5f);
				}
			}

			public Vector3 RelativePosition
			{
				get
				{
					return mRelativePosition;
				}
				set
				{
					mRelativePosition = value;
					primaryValue.RelativePosition = value;
					controlLabel.RelativePosition = value + new Vector3(-27.5f, 0f, 0f);
					secondaryValue.RelativePosition = value + new Vector3(20f, 0f, 0f);
				}
			}

			public override bool ContainsPoint(Vector3 worldPoint)
			{
				float x = controlLabel.X;
				float y = controlLabel.Y;
				float num = x - 2f;
				if (worldPoint.X < num)
				{
					return false;
				}
				float num2 = x + 55f;
				if (worldPoint.X > num2)
				{
					return false;
				}
				float num3 = y - 1f;
				if (worldPoint.Y < num3)
				{
					return false;
				}
				float num4 = y + 1f;
				if (worldPoint.Y > num4)
				{
					return false;
				}
				return true;
			}

			public override bool Update(Vector3 mousePoint, bool mouseDown, bool mouseMove)
			{
				bool flag = secondaryClickable.Update(mousePoint, mouseDown, mouseMove);
				flag |= primaryClickable.Update(mousePoint, mouseDown, mouseMove);
				return flag | base.Update(mousePoint, mouseDown, mouseMove);
			}

			public bool IsColumnEnabled(int index)
			{
				switch (index)
				{
				case 0:
					return mPrimaryValueEnabled;
				case 1:
					return mSecondaryValueEnabled;
				default:
					return false;
				}
			}

			public Text GetColumn(int index)
			{
				switch (index)
				{
				case 0:
					return primaryValue;
				case 1:
					return secondaryValue;
				default:
					return null;
				}
			}

			public void Unload()
			{
				controlLabel.Detach();
				primaryValue.Detach();
				secondaryValue.Detach();
				controlLabel = null;
				primaryValue = null;
				secondaryValue = null;
			}

			public void Load()
			{
				controlLabel = new Text(GUIHelper.CaptionFont);
				controlLabel.Scale = 1.1f;
				controlLabel.Spacing = 1.1f;
				controlLabel.HorizontalAlignment = HorizontalAlignment.Left;
				controlLabel.VerticalAlignment = VerticalAlignment.Center;
				controlLabel.ColorOperation = ColorOperation.Texture;
				primaryValue = new Text(GUIHelper.CaptionFont);
				primaryValue.Scale = 1.1f;
				primaryValue.Spacing = 1.1f;
				primaryValue.HorizontalAlignment = HorizontalAlignment.Center;
				primaryValue.VerticalAlignment = VerticalAlignment.Center;
				primaryValue.ColorOperation = ColorOperation.Subtract;
				primaryValue.SetColor(1f, 0f, 0f);
				secondaryValue = new Text(GUIHelper.CaptionFont);
				secondaryValue.Scale = 1.1f;
				secondaryValue.Spacing = 1.1f;
				secondaryValue.HorizontalAlignment = HorizontalAlignment.Center;
				secondaryValue.VerticalAlignment = VerticalAlignment.Center;
				secondaryValue.ColorOperation = ColorOperation.Texture;
				secondaryValue.ColorOperation = ColorOperation.Subtract;
				secondaryValue.SetColor(1f, 0f, 0f);
				primaryClickable = new ClickablePositionedObject
				{
					positionedObject = primaryValue,
					boundary = new Vector4(-10f, -1.5f, 10f, 1.5f)
				};
				secondaryClickable = new ClickablePositionedObject
				{
					positionedObject = secondaryValue,
					boundary = new Vector4(-10f, -1.5f, 10f, 1.5f)
				};
			}

			public void AttachTo(PositionedObject parent, bool changedRelative)
			{
				controlLabel.AttachTo(parent, changedRelative);
				primaryValue.AttachTo(parent, changedRelative);
				secondaryValue.AttachTo(parent, changedRelative);
			}

			public void SetVisible(bool value)
			{
				controlLabel.Visible = value;
				primaryValue.Visible = value;
				secondaryValue.Visible = value;
			}

			public void AddToLayers()
			{
				if (!mAddedToLayers)
				{
					mAddedToLayers = true;
					TextManager.AddToLayer(controlLabel, GUIHelper.UILayer);
					TextManager.AddToLayer(primaryValue, GUIHelper.UILayer);
					TextManager.AddToLayer(secondaryValue, GUIHelper.UILayer);
				}
			}

			public void RemoveFromLayers()
			{
				if (mAddedToLayers)
				{
					mAddedToLayers = false;
					TextManager.RemoveTextOneWay(controlLabel);
					TextManager.RemoveTextOneWay(primaryValue);
					TextManager.RemoveTextOneWay(secondaryValue);
				}
			}
		}

		public const int STATE_NONE = 0;

		public const int STATE_DEVICE_AND_PRESET = 1;

		public const int STATE_MAPPING = 2;

		public const int STATE_ASSIGN_KEY = 4;

		public const int STATE_CONFIRM_REVERT = 128;

		public const int STATE_UNTIL_KEY_UP = 256;

		private const int MAPPING_ROWS_PER_PAGE = 6;

		public const int MOVE_UP = 701;

		public const int MOVE_DOWN = 702;

		public const int MOVE_LEFT = 703;

		public const int MOVE_RIGHT = 704;

		public const int FIRE_UP = 705;

		public const int FIRE_DOWN = 706;

		public const int FIRE_LEFT = 707;

		public const int FIRE_RIGHT = 708;

		public const int MOVEMENT = 709;

		public const int AIM_AND_FIRE = 710;

		public const int AIM = 711;

		private Sprite mSelectedRowSprite;

		private Sprite mSelectedCellSprite;

		private Sprite mLeftArrowSprite;

		private Sprite mRightArrowSprite;

		private Sprite mHeaderSprite;

		private Sprite mLeftLineSprite;

		private Sprite mRightLineSprite;

		private Text mInputDeviceLabel;

		private Text mInputDeviceValue;

		private Sprite[] mAllSprites;

		private Text[] mAllTexts;

		private List<Row> mVisibleMappingRows;

		private List<Row> mUnusedMappingRows;

		private Dictionary<int, string> mControllerHint;

		private int mState;

		private int mSelectedRow;

		private int mSelectedColumn;

		private Row mSelectedMappingRow;

		private int mSelectedDevice = 1;

		private static GamePadPage mInstance;

		private Clickable[] mClickables;

		public static GamePadPage Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new GamePadPage();
				}
				return mInstance;
			}
		}

		public bool IsChangingSettings
		{
			get;
			private set;
		}

		public bool CanExit()
		{
			if ((mState & 0x100) > 0)
			{
				return false;
			}
			if ((mState & 0x80) > 0)
			{
				return false;
			}
			if (mState == 4)
			{
				return false;
			}
			return true;
		}

		public GamePadPage()
		{
			mControllerHint = new Dictionary<int, string>();
		}

		private Text AddText(string displayText, Vector3 relativePosition)
		{
			Text text = new Text(GUIHelper.CaptionFont);
			text.Scale = 1.1f;
			text.Spacing = 1.1f;
			text.HorizontalAlignment = HorizontalAlignment.Left;
			text.VerticalAlignment = VerticalAlignment.Center;
			text.ColorOperation = ColorOperation.Texture;
			text.DisplayText = displayText;
			text.AttachTo(this, false);
			text.RelativePosition = relativePosition;
			return text;
		}

		private void ReloadSelectedDevice()
		{
			if (mSelectedDevice == 1)
			{
				mInputDeviceValue.DisplayText = "键盘";
				mLeftArrowSprite.Alpha = 0.5f;
				mRightArrowSprite.Alpha = ((GamePad.GetDeviceFamily() != 1) ? 1f : 0.5f);
			}
			else
			{
				mInputDeviceValue.DisplayText = "手柄";
				mLeftArrowSprite.Alpha = 1f;
				mRightArrowSprite.Alpha = 0.5f;
			}
		}

		private bool ToggleSelectedDevice()
		{
			if (mSelectedDevice != 1)
			{
				SetSelectedDevice(1);
				return true;
			}
			int deviceFamily = GamePad.GetDeviceFamily();
			if (deviceFamily != 1)
			{
				SetSelectedDevice(deviceFamily);
				return true;
			}
			return false;
		}

		private void AssignSelectedCell()
		{
			Row row = FindMappingRow(mSelectedRow);
			if (row != null && row.IsColumnEnabled(mSelectedColumn))
			{
				if (mSelectedDevice == 1)
				{
					GamePad.keyboard.ResetDetectButtons();
				}
				else if (mSelectedDevice == 3)
				{
					GamePad.directInput.ResetDetectButtons();
				}
				else if (mSelectedDevice == 2)
				{
					GamePad.xinput.ResetDetectButtons();
				}
				mSelectedMappingRow = row;
				Text column = row.GetColumn(mSelectedColumn);
				column.DisplayText = "- 请按键 -";
				SetState(260);
			}
		}

		private void RevertSelectedDevice()
		{
			SetState(mState | 0x80);
			if (mSelectedDevice == 2)
			{
				PopUpUI.Instance.Show("还原手柄", "是否还原为默认的手柄布局？", PopUpUIAskForRevertSelectedDevice);
			}
			else if (mSelectedDevice == 3)
			{
				PopUpUI.Instance.Show("还原手柄", "是否还原为默认的手柄布局？", PopUpUIAskForRevertSelectedDevice);
			}
			else
			{
				PopUpUI.Instance.Show("还原键盘", "是否还原为默认的键盘布局？", PopUpUIAskForRevertSelectedDevice);
			}
		}

		private void PopUpUIAskForRevertSelectedDevice(PopUpUI.PopUpUIResult result)
		{
			if (result == PopUpUI.PopUpUIResult.Ok)
			{
				if (mSelectedDevice == 2)
				{
					GamePad.xinput.ResetData();
					GamePad.xinput.dirty = true;
				}
				else if (mSelectedDevice == 3)
				{
					GamePad.directInput.ResetData();
					GamePad.directInput.dirty = true;
				}
				else
				{
					GamePad.keyboard.ResetData();
					GamePad.keyboard.dirty = true;
				}
				SetState(mState & -129);
				ReloadVisibleMappingRows();
			}
			else
			{
				SetState(mState & -129);
			}
		}

		private void SetSelectedDevice(int device)
		{
			mSelectedDevice = device;
			GamePad.SetPreferredDeviceFamily(device);
			ReloadSelectedDevice();
			LayoutMappingRows();
			ReloadVisibleMappingRows();
		}

		public void Load()
		{
			mInputDeviceLabel = AddText("输入设备", new Vector3(-27.5f, 10f, 0.1f));
			mInputDeviceValue = AddText("键盘", new Vector3(7.5f, 10f, 0.1f));
			mInputDeviceValue.ColorOperation = ColorOperation.Subtract;
			mInputDeviceValue.SetColor(1f, 0f, 0f);
			mInputDeviceValue.HorizontalAlignment = HorizontalAlignment.Center;
			mAllTexts = new Text[2]
			{
				mInputDeviceLabel,
				mInputDeviceValue
			};
			mHeaderSprite = GamePadSheet.CreateScaledSprite(0);
			mHeaderSprite.RelativePosition = new Vector3(0f, 6.5f, 0f);
			mHeaderSprite.AttachTo(this, false);
			mLeftArrowSprite = GamePadSheet.CreateScaledSprite(1);
			mLeftArrowSprite.ColorOperation = ColorOperation.Subtract;
			mLeftArrowSprite.Red = 1f;
			mLeftArrowSprite.Green = 0f;
			mLeftArrowSprite.Blue = 0f;
			mLeftArrowSprite.RelativePosition = new Vector3(-3f, 10f, 0.1f);
			mLeftArrowSprite.AttachTo(this, false);
			mRightArrowSprite = GamePadSheet.CreateScaledSprite(3);
			mRightArrowSprite.ColorOperation = ColorOperation.Subtract;
			mRightArrowSprite.Red = 1f;
			mRightArrowSprite.Green = 0f;
			mRightArrowSprite.Blue = 0f;
			mRightArrowSprite.RelativePosition = new Vector3(18f, 10f, 0.1f);
			mRightArrowSprite.AttachTo(this, false);
			mLeftLineSprite = GamePadSheet.CreateScaledSprite(2);
			mLeftLineSprite.RelativePosition = new Vector3(-11f, -6f, 0f);
			mLeftLineSprite.AttachTo(this, false);
			mRightLineSprite = GamePadSheet.CreateScaledSprite(2);
			mRightLineSprite.RelativePosition = new Vector3(11f, -6f, 0f);
			mRightLineSprite.AttachTo(this, false);
			mSelectedRowSprite = GamePadSheet.CreateScaledSprite(5);
			mSelectedRowSprite.RelativePosition = new Vector3(0f, 10f, 0f);
			mSelectedRowSprite.AttachTo(this, false);
			mSelectedCellSprite = GamePadSheet.CreateScaledSprite(4);
			mSelectedCellSprite.RelativePosition = new Vector3(0f, 0f, 0f);
			mSelectedCellSprite.AttachTo(this, false);
			mAllSprites = new Sprite[7]
			{
				mHeaderSprite,
				mLeftArrowSprite,
				mRightArrowSprite,
				mSelectedRowSprite,
				mSelectedCellSprite,
				mLeftLineSprite,
				mRightLineSprite
			};
			mClickables = new Clickable[1]
			{
				new ClickablePositionedObject
				{
					positionedObject = mInputDeviceLabel,
					boundary = new Vector4(-1f, -2f, 55f, 2f),
					OnMouseEnter = (Clickable.ClickableHoverHandler)delegate
					{
						SetState(1);
						SFXManager.PlaySound("cursor");
					},
					OnMouseClick = (Clickable.ClickableHandler)delegate
					{
						if (mState != 1)
						{
							SetSelectedDevice(1);
							SFXManager.PlaySound("cursor");
							return true;
						}
						ToggleSelectedDevice();
						SFXManager.PlaySound("cursor");
						return true;
					}
				}
			};
			mVisibleMappingRows = new List<Row>();
			mUnusedMappingRows = new List<Row>();
		}

		public void Unload()
		{
			for (int i = 0; i < mAllTexts.Length; i++)
			{
				mAllTexts[i].Detach();
			}
			for (int j = 0; j < mAllSprites.Length; j++)
			{
				mAllSprites[j].Detach();
			}
			for (int k = 0; k < mVisibleMappingRows.Count; k++)
			{
				Row row = mVisibleMappingRows[k];
				row.Unload();
			}
			for (int l = 0; l < mUnusedMappingRows.Count; l++)
			{
				Row row2 = mUnusedMappingRows[l];
				row2.Unload();
			}
			mHeaderSprite = null;
			mLeftArrowSprite = null;
			mRightArrowSprite = null;
			mSelectedRowSprite = null;
			mSelectedCellSprite = null;
			mInputDeviceLabel = null;
			mInputDeviceValue = null;
			mVisibleMappingRows = null;
			mUnusedMappingRows = null;
			mAllTexts = null;
			mAllSprites = null;
		}

		public void InitialComponents()
		{
			InitialComponents(0, 0);
		}

		public void InitialComponents(int section, int row)
		{
			SetState(section, row, 0);
		}

		public void Show()
		{
			mSelectedDevice = GamePad.GetPreferredDeviceFamily();
			SetSelectedDevice(mSelectedDevice);
			for (int i = 0; i < mAllTexts.Length; i++)
			{
				TextManager.AddToLayer(mAllTexts[i], GUIHelper.UILayer);
			}
			for (int j = 0; j < mAllSprites.Length; j++)
			{
				SpriteManager.AddToLayer(mAllSprites[j], GUIHelper.UILayer);
			}
			for (int k = 0; k < mVisibleMappingRows.Count; k++)
			{
				Row row = mVisibleMappingRows[k];
				row.AddToLayers();
			}
			for (int l = 0; l < mUnusedMappingRows.Count; l++)
			{
				Row row2 = mUnusedMappingRows[l];
				row2.AddToLayers();
			}
			ControlHint.Instance.Clear().AddHint(1048576, "返回").ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
		}

		public void Hide()
		{
			GamePad.Commit();
			for (int i = 0; i < mAllTexts.Length; i++)
			{
				TextManager.RemoveTextOneWay(mAllTexts[i]);
			}
			for (int j = 0; j < mAllSprites.Length; j++)
			{
				SpriteManager.RemoveSpriteOneWay(mAllSprites[j]);
			}
			for (int k = 0; k < mVisibleMappingRows.Count; k++)
			{
				Row row = mVisibleMappingRows[k];
				row.RemoveFromLayers();
			}
			for (int l = 0; l < mUnusedMappingRows.Count; l++)
			{
				Row row2 = mUnusedMappingRows[l];
				row2.RemoveFromLayers();
			}
		}

		private void SetState(int state)
		{
			SetState(state, mSelectedRow, mSelectedColumn);
		}

		private void SetState(int state, int row, int column)
		{
			int num = mState;
			mState = state;
			mSelectedRow = row;
			mSelectedColumn = column;
			if (mState == 0)
			{
				IsChangingSettings = false;
				mSelectedRowSprite.Visible = false;
				mSelectedCellSprite.Visible = false;
				if (mState != num)
				{
					ControlHint.Instance.Clear().AddHint(1048576, "返回").ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
				}
			}
			else if (mState == 1)
			{
				IsChangingSettings = true;
				mSelectedRowSprite.RelativePosition = new Vector3(0f, mInputDeviceLabel.RelativeY, 0f);
				mSelectedCellSprite.Visible = false;
				mSelectedRowSprite.Visible = true;
				if (mState != num)
				{
					ControlHint.Instance.Clear().AddHint(16777216, "全部还原").AddHint(1048576, "返回")
						.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
				}
			}
			else if (mState == 2)
			{
				IsChangingSettings = true;
				int count = mVisibleMappingRows.Count;
				int num2 = 0;
				int num3 = mSelectedRow;
				bool flag = true;
				if (count > 0)
				{
					num3 = mVisibleMappingRows[0].tag;
					num2 = mSelectedRow - num3;
					if (num2 < 0)
					{
						flag = true;
						num3 = mSelectedRow;
					}
					else if (num2 >= 6)
					{
						flag = true;
						num3 = mSelectedRow - 6 + 1;
					}
					else
					{
						flag = false;
					}
				}
				if (flag)
				{
					int num4 = GetTotalMappingRows() - 6 + 1;
					if (num3 < 0)
					{
						num3 = 0;
					}
					if (num3 > num4)
					{
						num3 = num4;
					}
					LayoutMappingRows(num3);
				}
				num2 = mSelectedRow - num3;
				Row row2 = mVisibleMappingRows[num2];
				float y = row2.RelativePosition.Y;
				float num5 = 0f;
				num5 = ((mSelectedColumn != 0) ? row2.secondaryValue.RelativeX : row2.primaryValue.RelativeX);
				mSelectedRowSprite.RelativePosition = new Vector3(0f, y, 0f);
				mSelectedCellSprite.RelativePosition = new Vector3(num5, y, 0.1f);
				mSelectedRowSprite.Visible = true;
				mSelectedCellSprite.Visible = true;
				if (mState != num)
				{
					ControlHint.Instance.Clear().AddHint(16777216, "全部还原").AddHint(524288, "配置")
						.AddHint(1048576, "返回")
						.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
				}
			}
			else if (mState == 4 && mState != num)
			{
				ControlHint.Instance.Clear().AddHint(1048576, "返回").ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
			}
		}

		public Row FindMappingRow(int rowIndex)
		{
			int count = mVisibleMappingRows.Count;
			for (int i = 0; i < count; i++)
			{
				Row row = mVisibleMappingRows[i];
				if (row.tag == rowIndex)
				{
					return row;
				}
			}
			return null;
		}

		private void DespawnMappingRow(Row row)
		{
			row.SetVisible(false);
			mUnusedMappingRows.Add(row);
		}

		private Row SpawnMappingRow(int rowIndex)
		{
			int count = mUnusedMappingRows.Count;
			Row row;
			if (count > 0)
			{
				row = mUnusedMappingRows[count - 1];
				mUnusedMappingRows.RemoveAt(count - 1);
			}
			else
			{
				row = new Row();
				row.Load();
				row.AttachTo(this, false);
				row.AddToLayers();
				row.OnMouseEnter = OnMouseEnterRow;
				row.primaryClickable.OnMouseEnter = OnMouseEnterPrimaryCell;
				row.secondaryClickable.OnMouseEnter = OnMouseEnterSecondaryCell;
				row.primaryClickable.OnMouseClick = OnMouseClickPrimaryCell;
				row.secondaryClickable.OnMouseClick = OnMouseClickSecondaryCell;
			}
			row.primaryClickable.tag = rowIndex;
			row.secondaryClickable.tag = rowIndex;
			row.tag = rowIndex;
			ReloadMappingRow(row);
			row.SetVisible(true);
			return row;
		}

		private void OnMouseEnterRow(Clickable sender)
		{
			if (mState == 2 || mState == 0 || mState == 1)
			{
				int tag = sender.tag;
				if (tag != mSelectedRow)
				{
					SFXManager.PlaySound("cursor");
				}
				SetState(2, tag, mSelectedColumn);
			}
		}

		private void OnMouseEnterPrimaryCell(Clickable sender)
		{
			if (mState == 2 || mState == 0 || mState == 1)
			{
				int tag = sender.tag;
				if (tag != mSelectedRow)
				{
					SFXManager.PlaySound("cursor");
				}
				SetState(2, tag, 0);
			}
		}

		private void OnMouseEnterSecondaryCell(Clickable sender)
		{
			if (mState == 2 || mState == 0 || mState == 1)
			{
				int tag = sender.tag;
				SetState(2, tag, 1);
				SFXManager.PlaySound("cursor");
			}
		}

		private bool OnMouseClickPrimaryCell(Clickable sender)
		{
			if (mState == 2 || mState == 0 || mState == 1)
			{
				int tag = sender.tag;
				if (mState != 2 || mSelectedRow != tag)
				{
					SetState(2, tag, 0);
					SFXManager.PlaySound("cursor");
					return true;
				}
				SFXManager.PlaySound("ok");
				SetState(2, tag, 0);
				AssignSelectedCell();
			}
			return true;
		}

		private bool OnMouseClickSecondaryCell(Clickable sender)
		{
			if (mState == 2 || mState == 0 || mState == 1)
			{
				int tag = sender.tag;
				if (mState != 2 || mSelectedRow != tag)
				{
					SetState(2, tag, 1);
					SFXManager.PlaySound("cursor");
					return true;
				}
				SFXManager.PlaySound("ok");
				SetState(2, tag, 1);
				AssignSelectedCell();
			}
			return true;
		}

		private void LayoutMappingRows()
		{
			if (mVisibleMappingRows != null && mVisibleMappingRows.Count > 0)
			{
				int num = mVisibleMappingRows[0].tag;
				int num2 = num + 6 - 1;
				int totalMappingRows = GetTotalMappingRows();
				if (num2 >= totalMappingRows)
				{
					num2 = totalMappingRows - 1;
					num = num2 - 6 + 1;
				}
				if (num < 0)
				{
					num = 0;
				}
				if (mSelectedRow < num)
				{
					mSelectedRow = num;
				}
				if (mSelectedRow > num2)
				{
					mSelectedRow = num2;
				}
				LayoutMappingRows(num);
			}
			else
			{
				LayoutMappingRows(0);
			}
		}

		private void LayoutMappingRows(int startRow)
		{
			int num = startRow + 6 - 1;
			int totalMappingRows = GetTotalMappingRows();
			if (num >= totalMappingRows)
			{
				num = totalMappingRows - 1;
			}
			for (int num2 = mVisibleMappingRows.Count - 1; num2 >= 0; num2--)
			{
				Row row = mVisibleMappingRows[num2];
				if (row.tag < startRow || row.tag > num)
				{
					DespawnMappingRow(row);
					mVisibleMappingRows.RemoveAt(num2);
					row.SetVisible(false);
				}
			}
			int num3 = mVisibleMappingRows.Count;
			if (num3 > 0)
			{
				int tag = mVisibleMappingRows[0].tag;
				for (int num4 = tag - 1; num4 >= startRow; num4--)
				{
					Row item = SpawnMappingRow(num4);
					mVisibleMappingRows.Insert(0, item);
					num3++;
				}
				int tag2 = mVisibleMappingRows[num3 - 1].tag;
				for (int i = tag2 + 1; i <= num; i++)
				{
					Row item2 = SpawnMappingRow(i);
					mVisibleMappingRows.Add(item2);
					num3++;
				}
			}
			else
			{
				for (int j = startRow; j <= num; j++)
				{
					Row item3 = SpawnMappingRow(j);
					mVisibleMappingRows.Add(item3);
					num3++;
				}
			}
			float num5 = 1f;
			for (int k = 0; k < num3; k++)
			{
				Row row2 = mVisibleMappingRows[k];
				row2.RelativePosition = new Vector3(0f, num5, 0.2f);
				num5 -= 3f;
			}
		}

		private void ReloadVisibleMappingRows()
		{
			int count = mVisibleMappingRows.Count;
			for (int i = 0; i < count; i++)
			{
				Row row = mVisibleMappingRows[i];
				ReloadMappingRow(row);
			}
		}

		private void ReloadMappingRow(Row row)
		{
			if (mSelectedDevice == 2)
			{
				ReloadMappingXInputRow(row);
			}
			else if (mSelectedDevice == 3)
			{
				ReloadMappingDirectInputRow(row);
			}
			else
			{
				ReloadMappingKeyboardRow(row);
			}
		}

		private bool TryAssignMappingButton(Row row, int column)
		{
			if (mSelectedDevice == 2)
			{
				return TrySetMappingXInputButton(row, column);
			}
			if (mSelectedDevice == 3)
			{
				return TrySetMappingDirectInputButton(row, column);
			}
			return TrySetMappingKeyboardButton(row, column);
		}

		private void ReloadMappingKeyboardRow(Row row)
		{
			int tag = row.tag;
			int keyboardControlKey = GetKeyboardControlKey(tag);
			string controlKeyLocalizedName = GetControlKeyLocalizedName(keyboardControlKey);
			switch (keyboardControlKey)
			{
			case 711:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = "鼠标";
				row.secondaryValue.DisplayText = "-";
				row.PrimaryValueEnabled = false;
				row.SecondaryValueEnabled = false;
				break;
			case 1024:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = $"{GamePad.keyboard.GetButtonName(512, 0)}+{GamePad.keyboard.GetAxisBlueprint(2).GetName(false, 0)}";
				row.secondaryValue.DisplayText = GamePad.keyboard.GetButtonName(keyboardControlKey, 0);
				row.PrimaryValueEnabled = false;
				row.SecondaryValueEnabled = true;
				break;
			case 703:
			{
				KeyboardPad.Axis axisBlueprint4 = GamePad.keyboard.GetAxisBlueprint(1);
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = axisBlueprint4.GetName(true, 0);
				row.secondaryValue.DisplayText = axisBlueprint4.GetName(true, 1);
				row.PrimaryValueEnabled = true;
				row.SecondaryValueEnabled = true;
				break;
			}
			case 704:
			{
				KeyboardPad.Axis axisBlueprint3 = GamePad.keyboard.GetAxisBlueprint(1);
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = axisBlueprint3.GetName(false, 0);
				row.secondaryValue.DisplayText = axisBlueprint3.GetName(false, 1);
				row.PrimaryValueEnabled = true;
				row.SecondaryValueEnabled = true;
				break;
			}
			case 701:
			{
				KeyboardPad.Axis axisBlueprint2 = GamePad.keyboard.GetAxisBlueprint(2);
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = axisBlueprint2.GetName(false, 0);
				row.secondaryValue.DisplayText = axisBlueprint2.GetName(false, 1);
				row.PrimaryValueEnabled = true;
				row.SecondaryValueEnabled = true;
				break;
			}
			case 702:
			{
				KeyboardPad.Axis axisBlueprint = GamePad.keyboard.GetAxisBlueprint(2);
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = axisBlueprint.GetName(true, 0);
				row.secondaryValue.DisplayText = axisBlueprint.GetName(true, 1);
				row.PrimaryValueEnabled = true;
				row.SecondaryValueEnabled = true;
				break;
			}
			case 131072:
			case 262144:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.keyboard.GetButtonName(keyboardControlKey, 0);
				row.secondaryValue.DisplayText = GamePad.keyboard.GetButtonName(keyboardControlKey, 1);
				row.PrimaryValueEnabled = false;
				row.SecondaryValueEnabled = false;
				break;
			default:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.keyboard.GetButtonName(keyboardControlKey, 0);
				row.secondaryValue.DisplayText = GamePad.keyboard.GetButtonName(keyboardControlKey, 1);
				row.PrimaryValueEnabled = true;
				row.SecondaryValueEnabled = true;
				break;
			}
		}

		private bool TrySetMappingKeyboardButton(Row row, int column)
		{
			KeyboardPad.Button button;
			byte b = GamePad.keyboard.TryDetectButton(out button);
			switch (b)
			{
			case 0:
				return false;
			case 4:
				return false;
			case 1:
				SFXManager.PlaySound("ok");
				return true;
			default:
			{
				int tag = row.tag;
				int keyboardControlKey = GetKeyboardControlKey(tag);
				switch (b)
				{
				case 2:
					SFXManager.PlaySound("ok");
					SetMappingKeyboardButton(keyboardControlKey, column, new KeyboardPad.Button());
					return true;
				case 3:
				{
					SFXManager.PlaySound("ok");
					KeyboardPad.Button mappingKeyboardButton = GetMappingKeyboardButton(keyboardControlKey, column);
					ReplaceMappingKeyboardButton(button, mappingKeyboardButton);
					SetMappingKeyboardButton(keyboardControlKey, column, button);
					return true;
				}
				default:
					return true;
				}
			}
			}
		}

		private KeyboardPad.Button GetMappingKeyboardButton(int controlKey, int column)
		{
			switch (controlKey)
			{
			case 1024:
				return GamePad.keyboard.GetButtonBlueprint(1024, 0);
			case 703:
				return GamePad.keyboard.GetAxisBlueprint(1, true, column);
			case 704:
				return GamePad.keyboard.GetAxisBlueprint(1, false, column);
			case 701:
				return GamePad.keyboard.GetAxisBlueprint(2, false, column);
			case 702:
				return GamePad.keyboard.GetAxisBlueprint(2, true, column);
			default:
				return GamePad.keyboard.GetButtonBlueprint(controlKey, column);
			}
		}

		private void SetMappingKeyboardButton(int controlKey, int column, KeyboardPad.Button button)
		{
			switch (controlKey)
			{
			case 1024:
				GamePad.keyboard.SetButtonBlueprint(1024, 0, button);
				break;
			case 703:
				GamePad.keyboard.SetAxisBlueprint(1, true, column, button);
				break;
			case 704:
				GamePad.keyboard.SetAxisBlueprint(1, false, column, button);
				break;
			case 701:
				GamePad.keyboard.SetAxisBlueprint(2, false, column, button);
				break;
			case 702:
			{
				GamePad.keyboard.SetAxisBlueprint(2, true, column, button);
				KeyboardPad.ButtonAxis buttonAxis = GamePad.keyboard.GetAxisBlueprint(2) as KeyboardPad.ButtonAxis;
				if (buttonAxis != null)
				{
					GamePad.keyboard.SetButtonBlueprints(16, buttonAxis.negativeButtons);
				}
				break;
			}
			default:
				GamePad.keyboard.SetButtonBlueprint(controlKey, column, button);
				break;
			}
		}

		private void ReloadMappingXInputRow(Row row)
		{
			int tag = row.tag;
			int xInputControlKey = GetXInputControlKey(tag);
			switch (xInputControlKey)
			{
			case 709:
				row.controlLabel.DisplayText = "移动";
				row.primaryValue.DisplayText = "左摇杆";
				row.PrimaryValueEnabled = false;
				row.secondaryValue.DisplayText = "-";
				row.SecondaryValueEnabled = false;
				break;
			case 710:
				row.controlLabel.DisplayText = "瞄准和射击";
				row.primaryValue.DisplayText = "右摇杆";
				row.PrimaryValueEnabled = false;
				row.secondaryValue.DisplayText = "-";
				row.SecondaryValueEnabled = false;
				break;
			case 131072:
			case 262144:
			{
				string controlKeyLocalizedName2 = GetControlKeyLocalizedName(xInputControlKey);
				row.controlLabel.DisplayText = controlKeyLocalizedName2;
				row.primaryValue.DisplayText = GamePad.xinput.GetButtonName(xInputControlKey);
				row.PrimaryValueEnabled = false;
				row.secondaryValue.DisplayText = "-";
				row.SecondaryValueEnabled = false;
				break;
			}
			default:
			{
				string controlKeyLocalizedName = GetControlKeyLocalizedName(xInputControlKey);
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.xinput.GetButtonName(xInputControlKey);
				row.PrimaryValueEnabled = true;
				row.secondaryValue.DisplayText = "-";
				row.SecondaryValueEnabled = false;
				break;
			}
			}
		}

		private bool TrySetMappingXInputButton(Row row, int column)
		{
			KeyboardPad.Button button;
			byte b = GamePad.keyboard.TryDetectButton(out button);
			switch (b)
			{
			case 4:
				return false;
			case 3:
				return false;
			case 1:
				SFXManager.PlaySound("ok");
				return true;
			default:
			{
				int tag = row.tag;
				int xInputControlKey = GetXInputControlKey(tag);
				if (b == 2)
				{
					SFXManager.PlaySound("ok");
					GamePad.xinput.SetButtonBlueprint(xInputControlKey, new XInputPad.Button());
					return true;
				}
				XInputPad.Button button2;
				if (GamePad.xinput.TryDetectButton(out button2))
				{
					SFXManager.PlaySound("ok");
					XInputPad.Button buttonBlueprint = GamePad.xinput.GetButtonBlueprint(xInputControlKey);
					ReplaceMappingXInputButton(button2, buttonBlueprint);
					GamePad.xinput.SetButtonBlueprint(xInputControlKey, button2);
					return true;
				}
				return false;
			}
			}
		}

		private void ReloadMappingDirectInputRow(Row row)
		{
			int tag = row.tag;
			int directInputControlKey = GetDirectInputControlKey(tag);
			string controlKeyLocalizedName = GetControlKeyLocalizedName(directInputControlKey);
			switch (directInputControlKey)
			{
			case 703:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(1, true);
				row.PrimaryValueEnabled = true;
				break;
			case 704:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(1, false);
				row.PrimaryValueEnabled = true;
				break;
			case 701:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(2, false);
				row.PrimaryValueEnabled = true;
				break;
			case 702:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(2, true);
				row.PrimaryValueEnabled = true;
				break;
			case 707:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(3, true);
				row.PrimaryValueEnabled = true;
				break;
			case 708:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(3, false);
				row.PrimaryValueEnabled = true;
				break;
			case 705:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(4, false);
				row.PrimaryValueEnabled = true;
				break;
			case 706:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetAxisName(4, true);
				row.PrimaryValueEnabled = true;
				break;
			default:
				row.controlLabel.DisplayText = controlKeyLocalizedName;
				row.primaryValue.DisplayText = GamePad.directInput.GetButtonName(directInputControlKey);
				row.PrimaryValueEnabled = true;
				break;
			}
			row.secondaryValue.DisplayText = "-";
			row.SecondaryValueEnabled = false;
		}

		private bool TrySetMappingDirectInputButton(Row row, int column)
		{
			KeyboardPad.Button button;
			byte b = GamePad.keyboard.TryDetectButton(out button);
			switch (b)
			{
			case 4:
				return false;
			case 3:
				return false;
			case 1:
				SFXManager.PlaySound("ok");
				return true;
			default:
			{
				int tag = row.tag;
				int directInputControlKey = GetDirectInputControlKey(tag);
				if (b == 2)
				{
					SFXManager.PlaySound("ok");
					SetMappingDirectInputButton(directInputControlKey, column, new DirectInputPad.Button());
					return true;
				}
				DirectInputPad.Button button2;
				if (GamePad.directInput.TryDetectButton(out button2))
				{
					SFXManager.PlaySound("ok");
					DirectInputPad.Button mappingDirectInputButton = GetMappingDirectInputButton(directInputControlKey, column);
					ReplaceMappingDirectInputButton(button2, mappingDirectInputButton);
					SetMappingDirectInputButton(directInputControlKey, column, button2);
					return true;
				}
				return false;
			}
			}
		}

		private DirectInputPad.Button GetMappingDirectInputButton(int controlKey, int buttonIndex)
		{
			switch (controlKey)
			{
			case 703:
				return GamePad.directInput.GetAxisBlueprint(1, true);
			case 704:
				return GamePad.directInput.GetAxisBlueprint(1, false);
			case 701:
				return GamePad.directInput.GetAxisBlueprint(2, false);
			case 702:
				return GamePad.directInput.GetAxisBlueprint(2, true);
			case 707:
				return GamePad.directInput.GetAxisBlueprint(3, true);
			case 708:
				return GamePad.directInput.GetAxisBlueprint(3, false);
			case 705:
				return GamePad.directInput.GetAxisBlueprint(4, false);
			case 706:
				return GamePad.directInput.GetAxisBlueprint(4, true);
			default:
				return GamePad.directInput.GetButtonBlueprint(controlKey);
			}
		}

		private bool SetMappingDirectInputButton(int controlKey, int buttonIndex, DirectInputPad.Button button)
		{
			switch (controlKey)
			{
			case 703:
				return GamePad.directInput.SetAxisBlueprint(1, true, button);
			case 704:
				return GamePad.directInput.SetAxisBlueprint(1, false, button);
			case 701:
				return GamePad.directInput.SetAxisBlueprint(2, false, button);
			case 702:
				if (GamePad.directInput.SetAxisBlueprint(2, true, button))
				{
					GamePad.directInput.SetButtonBlueprint(16, button);
					return true;
				}
				return false;
			case 707:
				return GamePad.directInput.SetAxisBlueprint(3, true, button);
			case 708:
				return GamePad.directInput.SetAxisBlueprint(3, false, button);
			case 705:
				return GamePad.directInput.SetAxisBlueprint(4, false, button);
			case 706:
				return GamePad.directInput.SetAxisBlueprint(4, true, button);
			default:
				return GamePad.directInput.SetButtonBlueprint(controlKey, button);
			}
		}

		private string GetControlKeyLocalizedName(int controlKey)
		{
			switch (controlKey)
			{
			case 711:
				return "瞄准";
			case 701:
				return "上";
			case 702:
				return "下";
			case 703:
				return "左";
			case 704:
				return "右";
			case 705:
				return "向上射击";
			case 706:
				return "向下射击";
			case 707:
				return "向左射击";
			case 708:
				return "向右射击";
			case 64:
				return "射击";
			case 32:
				return "跳跃";
			case 128:
				return "特殊技 1";
			case 256:
				return "特殊技 2";
			case 2048:
				return "终结技";
			case 4096:
				return "修理工具";
			case 1024:
				if (ProfileManager.Profile.LastPlayedCharacter != 0)
				{
					return "悬浮";
				}
				return "空气推进";
			case 512:
				return "冲刺";
			case 8192:
				return "武器 1";
			case 16384:
				return "武器 2";
			case 32768:
				return "武器 3";
			case 65536:
				return "武器 4";
			case 131072:
				return "游戏菜单";
			case 262144:
				return "系统菜单";
			default:
				return null;
			}
		}

		private int GetKeyboardControlKey(int index)
		{
			switch (index)
			{
			case 0:
				return 711;
			case 1:
				return 701;
			case 2:
				return 702;
			case 3:
				return 703;
			case 4:
				return 704;
			case 5:
				return 64;
			case 6:
				return 32;
			case 7:
				return 128;
			case 8:
				return 256;
			case 9:
				return 2048;
			case 10:
				return 4096;
			case 11:
				return 1024;
			case 12:
				return 512;
			case 13:
				return 8192;
			case 14:
				return 16384;
			case 15:
				return 32768;
			case 16:
				return 65536;
			case 17:
				return 131072;
			case 18:
				return 262144;
			default:
				return 0;
			}
		}

		private void ReplaceMappingKeyboardButton(KeyboardPad.Button oldButton, KeyboardPad.Button newButton)
		{
			if (newButton == null)
			{
				newButton = new KeyboardPad.Button();
			}
			GamePad.keyboard.ReplaceAxisBlueprint(1, oldButton, newButton);
			GamePad.keyboard.ReplaceAxisBlueprint(2, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(16, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(64, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(32, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(128, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(256, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(2048, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(4096, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(1024, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(512, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(8192, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(16384, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(32768, oldButton, newButton);
			GamePad.keyboard.ReplaceButtonBlueprint(65536, oldButton, newButton);
		}

		private int GetXInputControlKey(int index)
		{
			switch (index)
			{
			case 0:
				return 709;
			case 1:
				return 710;
			case 2:
				return 64;
			case 3:
				return 32;
			case 4:
				return 128;
			case 5:
				return 256;
			case 6:
				return 2048;
			case 7:
				return 4096;
			case 8:
				return 1024;
			case 9:
				return 512;
			case 10:
				return 8192;
			case 11:
				return 16384;
			case 12:
				return 32768;
			case 13:
				return 65536;
			case 14:
				return 131072;
			case 15:
				return 262144;
			default:
				return 0;
			}
		}

		private void ReplaceMappingXInputButton(XInputPad.Button oldButton, XInputPad.Button newButton)
		{
			if (newButton == null)
			{
				newButton = new XInputPad.Button();
			}
			GamePad.xinput.ReplaceButtonBlueprint(64, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(32, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(128, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(256, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(2048, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(4096, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(1024, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(512, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(8192, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(16384, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(32768, oldButton, newButton);
			GamePad.xinput.ReplaceButtonBlueprint(65536, oldButton, newButton);
		}

		private int GetDirectInputControlKey(int index)
		{
			switch (index)
			{
			case 0:
				return 701;
			case 1:
				return 702;
			case 2:
				return 703;
			case 3:
				return 704;
			case 4:
				return 32;
			case 5:
				return 128;
			case 6:
				return 256;
			case 7:
				return 64;
			case 8:
				return 705;
			case 9:
				return 706;
			case 10:
				return 707;
			case 11:
				return 708;
			case 12:
				return 2048;
			case 13:
				return 4096;
			case 14:
				return 1024;
			case 15:
				return 512;
			case 16:
				return 8192;
			case 17:
				return 16384;
			case 18:
				return 32768;
			case 19:
				return 65536;
			case 20:
				return 131072;
			case 21:
				return 262144;
			default:
				return 0;
			}
		}

		private void ReplaceMappingDirectInputButton(DirectInputPad.Button oldButton, DirectInputPad.Button newButton)
		{
			if (newButton == null)
			{
				newButton = new DirectInputPad.Button();
			}
			GamePad.directInput.ReplaceAxisBlueprint(1, oldButton, newButton);
			GamePad.directInput.ReplaceAxisBlueprint(2, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(32, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(128, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(256, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(64, oldButton, newButton);
			GamePad.directInput.ReplaceAxisBlueprint(3, oldButton, newButton);
			GamePad.directInput.ReplaceAxisBlueprint(4, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(2048, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(4096, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(1024, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(512, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(8192, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(16384, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(32768, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(65536, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(131072, oldButton, newButton);
			GamePad.directInput.ReplaceButtonBlueprint(262144, oldButton, newButton);
		}

		private int GetTotalMappingRows()
		{
			if (mSelectedDevice == 1)
			{
				return 19;
			}
			if (mSelectedDevice == 2)
			{
				return 16;
			}
			if (mSelectedDevice == 3)
			{
				return 22;
			}
			return 0;
		}

		public void Update()
		{
			if (mState == 0 || mState == 1 || mState == 2)
			{
				if (mSelectedDevice == 1)
				{
					int deviceFamily = GamePad.GetDeviceFamily();
					if (GamePad.TryDetectJoystick(false))
					{
						int deviceFamily2 = GamePad.GetDeviceFamily();
						if (deviceFamily != deviceFamily2)
						{
							ReloadSelectedDevice();
							SetState(mState | 0x100);
							return;
						}
					}
				}
				else if (GamePad.TryDetectJoystick(true))
				{
					int deviceFamily3 = GamePad.GetDeviceFamily();
					if (mSelectedDevice != deviceFamily3)
					{
						SetSelectedDevice(deviceFamily3);
						SetState(mState | 0x100);
						return;
					}
				}
			}
			KeyboardPad.Button button;
			if (mState == 0)
			{
				if (GamePad.GetMenuRepeatKeyDown(8))
				{
					SetState(1);
					SFXManager.PlaySound("cursor");
				}
				else if (GamePad.keyboard.GetMouseScrollDown())
				{
					SetState(2);
					SFXManager.PlaySound("cursor");
				}
				else
				{
					GamePad.UpdateClickables(mClickables);
					GamePad.UpdateClickables(mVisibleMappingRows);
				}
			}
			else if (mState == 1)
			{
				if (GamePad.GetMenuRepeatKeyDown(4))
				{
					SetState(0);
					SFXManager.PlaySound("cursor");
				}
				else if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
				{
					SetState(2);
					SFXManager.PlaySound("cursor");
				}
				else
				{
					if (GamePad.GetMenuRepeatKeyDown(1))
					{
						if (mSelectedDevice != 1)
						{
							SetSelectedDevice(1);
							SFXManager.PlaySound("cursor");
							return;
						}
					}
					else if (GamePad.GetMenuRepeatKeyDown(2))
					{
						if (mSelectedDevice == 1)
						{
							int deviceFamily4 = GamePad.GetDeviceFamily();
							if (deviceFamily4 != 1)
							{
								SetSelectedDevice(deviceFamily4);
								SFXManager.PlaySound("cursor");
								return;
							}
						}
					}
					else if (GamePad.GetMenuKeyDown(524288))
					{
						if (ToggleSelectedDevice())
						{
							SFXManager.PlaySound("cursor");
							return;
						}
					}
					else if (GamePad.GetMenuKeyDown(16777216))
					{
						RevertSelectedDevice();
						return;
					}
					GamePad.UpdateClickables(mClickables);
					GamePad.UpdateClickables(mVisibleMappingRows);
				}
			}
			else if (mState == 2)
			{
				if (GamePad.GetMenuRepeatKeyDown(4))
				{
					if (mSelectedRow == 0)
					{
						SetState(1);
						SFXManager.PlaySound("cursor");
					}
					else
					{
						SetState(2, mSelectedRow - 1, mSelectedColumn);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.keyboard.GetMouseScrollUp())
				{
					if (mSelectedRow != 0)
					{
						SetState(2, mSelectedRow - 1, mSelectedColumn);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
				{
					if (mSelectedRow + 1 < GetTotalMappingRows())
					{
						SetState(2, mSelectedRow + 1, mSelectedColumn);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(1))
				{
					if (mSelectedColumn > 0)
					{
						SetState(2, mSelectedRow, mSelectedColumn - 1);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.GetMenuRepeatKeyDown(2))
				{
					if (mSelectedColumn <= 0)
					{
						SetState(2, mSelectedRow, mSelectedColumn + 1);
						SFXManager.PlaySound("cursor");
					}
				}
				else if (GamePad.GetMenuKeyDown(524288))
				{
					SFXManager.PlaySound("ok");
					AssignSelectedCell();
				}
				else if (GamePad.GetMenuKeyDown(16777216))
				{
					SFXManager.PlaySound("ok");
					RevertSelectedDevice();
				}
				else
				{
					GamePad.UpdateClickables(mClickables);
					GamePad.UpdateClickables(mVisibleMappingRows);
				}
			}
			else if (mState == 4)
			{
				Text column = mSelectedMappingRow.GetColumn(mSelectedColumn);
				if (column.Alpha >= 0.9f)
				{
					column.AlphaRate = -3.5f;
				}
				else if (column.Alpha <= 0.1f)
				{
					column.AlphaRate = 3.5f;
				}
				if (TryAssignMappingButton(mSelectedMappingRow, mSelectedColumn))
				{
					SFXManager.PlaySound("ok");
					SetState(258);
					column.AlphaRate = 0f;
					column.Alpha = 1f;
					ReloadVisibleMappingRows();
				}
			}
			else if ((mState & 0x100) > 0 && !GamePad.GetMenuKey(4) && !GamePad.GetMenuKey(8) && !GamePad.GetMenuKey(1) && !GamePad.GetMenuKey(2) && !GamePad.GetMenuKey(524288) && !GamePad.GetMenuKey(1048576) && !GamePad.GetMenuKey(8388608) && !GamePad.GetMenuKey(16777216) && !GamePad.keyboard.GetMouseButton(1) && GamePad.keyboard.TryDetectButton(out button) == 0)
			{
				SetState(mState & -257);
			}
		}
	}
}
