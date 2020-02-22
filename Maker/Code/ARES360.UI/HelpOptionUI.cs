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
	public class HelpOptionUI : PositionedObject, IProcessable
	{
		public enum Page
		{
			None,
			Settings,
			Controls,
			HowToPlay,
			Credits,
			Exit
		}

		private const int EXITREASON_BACK_PRESSED = 0;

		private const int EXITREASON_OK_PRESSED = 1;

		internal const string CONTENT_MNG_NAME = "Global";

		private const float DISTANCE_FROM_CAMERA = -60f;

		private const int CLICKABLE_COUNT = 4;

		private const int TAB_COUNT = 4;

		private const int TAB_INDEX_SETTINGS = 0;

		private const int TAB_INDEX_CONTROLS = 1;

		private const int TAB_INDEX_HOW2PLAY = 2;

		private const int TAB_INDEX_CREDITS = 3;

		private int mCurrentTabIndex;

		private Page mCurrentPage;

		private Page mRequestNextPage;

		private bool mIsJustChangePage;

		private bool mIsTransitionBetweenPage;

		private bool mIsTransitionEnd;

		private Page mPreviuosPage;

		private Dictionary<int, string> mControlHintDict;

		private Texture2D mTabHeaderNormalTexture;

		private Texture2D mTabHeaderHighlightTexture;

		private Texture2D mTabHeaderIntermediateTexture;

		private Sprite mHeaderSprite;

		private Sprite mMainPanel;

		private Sprite[] mTabHeaders;

		private Text[] mTabHeaderTexts;

		private Clickable[] mClickables;

		private int mExitReason;

		public VoidEventHandler OnBackButtonPressed;

		public VoidEventHandler OnOkButtonPressed;

		private static HelpOptionUI mInstance;

		public bool IsActivated
		{
			get;
			set;
		}

		public bool IsImmediatelyRemoveProcessWhenExit
		{
			get;
			set;
		}

		public bool IsShowBetweenSelectCharacterAndEnterPlayScreen
		{
			get;
			set;
		}

		public static HelpOptionUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new HelpOptionUI();
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

		private HelpOptionUI()
		{
			mCurrentPage = Page.Settings;
			mControlHintDict = new Dictionary<int, string>();
			mHeaderSprite = new Sprite();
			mMainPanel = new Sprite();
			mTabHeaders = new Sprite[4];
			mTabHeaderTexts = new Text[4];
			mClickables = new Clickable[4];
			IsImmediatelyRemoveProcessWhenExit = true;
			IsShowBetweenSelectCharacterAndEnterPlayScreen = false;
		}

		public void Load()
		{
			mTabHeaderHighlightTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/help_opt_tabheader_intermediate", "Global");
			mTabHeaderIntermediateTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/help_opt_tabheader_intermediate", "Global");
			mTabHeaderNormalTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/help_opt_tabheader_normal", "Global");
			mHeaderSprite.Texture = LocalizedSheet.Texture;
			mHeaderSprite.TextureBound = LocalizedSheet.GetTextureBound(12);
			mHeaderSprite.ScaleX = 12.875f;
			mHeaderSprite.ScaleY = 1f;
			mHeaderSprite.RelativePosition = new Vector3(18.5f, 21f, 0f);
			mHeaderSprite.AttachTo(this, false);
			mMainPanel.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/menu_common_panel", "Global");
			mMainPanel.ScaleX = 35f;
			mMainPanel.ScaleY = 19.25f;
			mMainPanel.RelativePosition = new Vector3(0f, 0f, 0f);
			mMainPanel.AttachTo(this, false);
			Vector3 vector = new Vector3(-24.25f, 15.5f, 0f);
			float num = 15.2f;
			for (int i = 0; i < 4; i++)
			{
				mTabHeaders[i] = new Sprite();
				mTabHeaders[i].ScaleX = 8f;
				mTabHeaders[i].ScaleY = 3.25f;
				mTabHeaders[i].Texture = mTabHeaderNormalTexture;
				mTabHeaders[i].RelativePosition.X = vector.X + (float)i * num;
				mTabHeaders[i].RelativePosition.Y = vector.Y;
				mTabHeaders[i].RelativePosition.Z = 0f;
				mTabHeaders[i].Alpha = 0.8f;
				mTabHeaders[i].AttachTo(this, false);
				mTabHeaderTexts[i] = new Text(GUIHelper.CaptionFont);
				mTabHeaderTexts[i].ColorOperation = ColorOperation.Subtract;
				mTabHeaderTexts[i].SetColor(0f, 0f, 0f);
				mTabHeaderTexts[i].Scale = 1f;
				mTabHeaderTexts[i].Spacing = 1f;
				mTabHeaderTexts[i].HorizontalAlignment = HorizontalAlignment.Center;
				mTabHeaderTexts[i].VerticalAlignment = VerticalAlignment.Center;
				mTabHeaderTexts[i].RelativePosition = new Vector3(0f, 0f, 0.1f);
				mTabHeaderTexts[i].AttachTo(mTabHeaders[i], false);
				mClickables[i] = new ClickableSprite
				{
					tag = i,
					sprite = mTabHeaders[i],
					OnMouseEnter = (Clickable.ClickableHoverHandler)delegate(Clickable sender)
					{
						mTabHeaderTexts[sender.tag].SetColor(1f, 0f, 0f);
						mTabHeaders[sender.tag].Alpha = 1f;
					},
					OnMouseExit = (Clickable.ClickableHoverHandler)delegate(Clickable sender)
					{
						mTabHeaderTexts[sender.tag].SetColor(0f, 0f, 0f);
						mTabHeaders[sender.tag].Alpha = 0.8f;
					},
					OnMouseClick = (Clickable.ClickableHandler)delegate(Clickable sender)
					{
						if (SelectTab(sender.tag))
						{
							SFXManager.PlaySound("cursor");
						}
						return true;
					}
				};
			}
			mTabHeaderTexts[0].DisplayText = "设置";
			mTabHeaderTexts[1].DisplayText = "控制";
			mTabHeaderTexts[2].DisplayText = "玩法";
			mTabHeaderTexts[3].DisplayText = "制作组";
			GameSettingsPage.Instance.Load();
			HowToPlayPage.Instance.Load();
			CreditPage.Instance.Load();
			GamePadPage.Instance.Load();
		}

		public void Unload()
		{
			mTabHeaderHighlightTexture = null;
			mTabHeaderIntermediateTexture = null;
			mTabHeaderNormalTexture = null;
			mHeaderSprite.Texture = null;
			mMainPanel.Texture = null;
			for (int i = 0; i < 4; i++)
			{
				mTabHeaders[i] = null;
				mTabHeaderTexts[i] = null;
			}
			GameSettingsPage.Instance.Unload();
			GamePadPage.Instance.Unload();
			HowToPlayPage.Instance.Unload();
			CreditPage.Instance.Unload();
		}

		private void SelectRelativeTab(sbyte direction)
		{
			int num = 0;
			switch (direction)
			{
			case -2:
			case 1:
				num = 1;
				break;
			case -1:
			case 2:
				num = -1;
				break;
			}
			int num2 = mCurrentTabIndex + num;
			switch (num2)
			{
			case -1:
				num2 = 3;
				break;
			case 4:
				num2 = 0;
				break;
			}
			SelectTab(num2);
		}

		private bool SelectTab(int n)
		{
			Page page = mCurrentPage;
			if (page != Page.Settings || GameSettingsPage.Instance.ShouldSwitchToTab(n))
			{
				mTabHeaders[mCurrentTabIndex].Texture = mTabHeaderNormalTexture;
				mCurrentTabIndex = n;
				mTabHeaders[mCurrentTabIndex].Texture = mTabHeaderHighlightTexture;
				switch (mCurrentTabIndex)
				{
				case 0:
					ChangePage(Page.Settings);
					return true;
				case 1:
					ChangePage(Page.Controls);
					return true;
				case 2:
					ChangePage(Page.HowToPlay);
					return true;
				case 3:
					ChangePage(Page.Credits);
					return true;
				default:
					return false;
				}
			}
			return false;
		}

		private void ResetTabCursorByPage(Page p)
		{
			int num = 0;
			switch (p)
			{
			case Page.Settings:
				num = 0;
				break;
			case Page.Controls:
				num = 1;
				break;
			case Page.HowToPlay:
				num = 2;
				break;
			case Page.Credits:
				num = 3;
				break;
			}
			if (num >= 0 && num < 4)
			{
				for (int i = 0; i < 4; i++)
				{
					if (mTabHeaders[i].Texture == mTabHeaderHighlightTexture)
					{
						mTabHeaders[i].Texture = mTabHeaderNormalTexture;
					}
				}
				mTabHeaders[num].Texture = mTabHeaderHighlightTexture;
			}
			mCurrentTabIndex = num;
		}

		public void Show()
		{
			Show(mPreviuosPage);
		}

		public void Show(Page beginPage)
		{
			MouseUI.SetCursor(1);
			CreditPage.Instance.Reset();
			mPreviuosPage = beginPage;
			ProcessManager.AddProcess(this);
		}

		public void Hide()
		{
			ControlHint.Instance.HideHints();
			ProcessManager.RemoveProcess(this);
		}

		private void ChangePage(Page NextPage)
		{
			if (mCurrentPage != NextPage)
			{
				mRequestNextPage = NextPage;
				mIsTransitionBetweenPage = true;
				mIsTransitionEnd = false;
				mIsJustChangePage = true;
			}
		}

		private void UpdatePageTransition(Page currentPage, Page nextPage)
		{
			switch (currentPage)
			{
			case Page.Settings:
				GameSettingsPage.Instance.OnExit();
				GameSettingsPage.Instance.Detach();
				GameSettingsPage.Instance.RemoveAllComponentsFromManager();
				SpriteManager.RemovePositionedObject(GameSettingsPage.Instance);
				if (GameSettingsPage.Instance.dirty > 0)
				{
					ProfileManager.BeginSave(null, 3, true);
				}
				break;
			case Page.Controls:
				GamePadPage.Instance.Detach();
				GamePadPage.Instance.Hide();
				SpriteManager.RemovePositionedObject(GamePadPage.Instance);
				break;
			case Page.HowToPlay:
				HowToPlayPage.Instance.Detach();
				HowToPlayPage.Instance.Hide();
				SpriteManager.RemovePositionedObject(HowToPlayPage.Instance);
				break;
			case Page.Credits:
				CreditPage.Instance.Detach();
				CreditPage.Instance.Hide();
				SpriteManager.RemovePositionedObject(CreditPage.Instance);
				break;
			}
			switch (nextPage)
			{
			case Page.Settings:
				if (IsShowBetweenSelectCharacterAndEnterPlayScreen)
				{
					GameSettingsPage.Instance.Show(128);
				}
				else
				{
					GameSettingsPage.Instance.Show();
				}
				GameSettingsPage.Instance.RelativePosition = new Vector3(0f, 0f, 0.1f);
				GameSettingsPage.Instance.AttachTo(this, false);
				GameSettingsPage.Instance.AddAllComponentsToManager();
				SpriteManager.AddPositionedObject(GameSettingsPage.Instance);
				ControlHint.Instance.Clear();
				if (IsShowBetweenSelectCharacterAndEnterPlayScreen)
				{
					ControlHint.Instance.AddHint(524288, "开始");
				}
				ControlHint.Instance.AddHint(1048576, "返回");
				ControlHint.Instance.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
				break;
			case Page.Controls:
				GamePadPage.Instance.InitialComponents();
				GamePadPage.Instance.RelativePosition = new Vector3(0f, 0f, 0.1f);
				GamePadPage.Instance.AttachTo(this, false);
				GamePadPage.Instance.Show();
				SpriteManager.AddPositionedObject(GamePadPage.Instance);
				ControlHint.Instance.Clear();
				if (IsShowBetweenSelectCharacterAndEnterPlayScreen)
				{
					ControlHint.Instance.AddHint(524288, "开始");
				}
				ControlHint.Instance.AddHint(1048576, "返回");
				ControlHint.Instance.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
				break;
			case Page.HowToPlay:
				HowToPlayPage.Instance.RelativePosition = new Vector3(0f, 0f, 0.1f);
				HowToPlayPage.Instance.AttachTo(this, false);
				HowToPlayPage.Instance.Show();
				SpriteManager.AddPositionedObject(HowToPlayPage.Instance);
				ControlHint.Instance.Clear();
				ControlHint.Instance.AddHint(2097152, "前一页");
				ControlHint.Instance.AddHint(4194304, "后一页");
				if (IsShowBetweenSelectCharacterAndEnterPlayScreen)
				{
					ControlHint.Instance.AddHint(524288, "开始");
				}
				ControlHint.Instance.AddHint(1048576, "返回");
				ControlHint.Instance.ShowHints(HorizontalAlignment.Right, SpriteManager.TopLayer);
				break;
			case Page.Credits:
				CreditPage.Instance.RelativePosition = new Vector3(0f, 0f, 0.1f);
				CreditPage.Instance.AttachTo(this, false);
				CreditPage.Instance.Show();
				SpriteManager.AddPositionedObject(CreditPage.Instance);
				ControlHint.Instance.Clear();
				if (IsShowBetweenSelectCharacterAndEnterPlayScreen)
				{
					ControlHint.Instance.AddHint(524288, "开始");
				}
				ControlHint.Instance.AddHint(1048576, "返回");
				ControlHint.Instance.ShowHints(HorizontalAlignment.Right);
				break;
			}
			mIsTransitionEnd = true;
		}

		public void OnRegister()
		{
			SpriteManager.AddPositionedObject(this);
			AttachTo(World.Camera, false);
			SpriteManager.AddToLayer(mHeaderSprite, GUIHelper.UILayer);
			SpriteManager.AddToLayer(mMainPanel, GUIHelper.UILayer);
			for (int i = 0; i < 4; i++)
			{
				SpriteManager.AddToLayer(mTabHeaders[i], GUIHelper.UILayer);
				TextManager.AddToLayer(mTabHeaderTexts[i], GUIHelper.UILayer);
			}
			if (mPreviuosPage == Page.None)
			{
				mPreviuosPage = Page.Settings;
			}
			mCurrentPage = Page.None;
			ChangePage(mPreviuosPage);
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		public void OnRemove()
		{
			Detach();
			SpriteManager.RemovePositionedObject(this);
			SpriteManager.RemoveSpriteOneWay(mHeaderSprite);
			SpriteManager.RemoveSpriteOneWay(mMainPanel);
			for (int i = 0; i < 4; i++)
			{
				SpriteManager.RemoveSpriteOneWay(mTabHeaders[i]);
				TextManager.RemoveTextOneWay(mTabHeaderTexts[i]);
			}
			UpdatePageTransition(mCurrentPage, Page.Exit);
		}

		public void Update()
		{
			if (IsActivated)
			{
				if (mIsTransitionBetweenPage)
				{
					UpdatePageTransition(mCurrentPage, mRequestNextPage);
					if (mIsTransitionEnd)
					{
						mCurrentPage = mRequestNextPage;
						mIsTransitionBetweenPage = false;
						ResetTabCursorByPage(mCurrentPage);
					}
					mIsJustChangePage = false;
				}
				else
				{
					switch (mCurrentPage)
					{
					case Page.Settings:
						GameSettingsPage.Instance.Update();
						if (GameSettingsPage.Instance.IsChangingSettings)
						{
							if (mTabHeaders[mCurrentTabIndex].Alpha > 0.8f)
							{
								mTabHeaders[mCurrentTabIndex].Alpha = 0.8f;
							}
						}
						else
						{
							if (mTabHeaders[mCurrentTabIndex].Alpha < 1f)
							{
								mTabHeaders[mCurrentTabIndex].Alpha = 1f;
							}
							if (GamePad.GetMenuRepeatKeyDown(1))
							{
								SelectRelativeTab(-1);
								SFXManager.PlaySound("cursor");
							}
							else if (GamePad.GetMenuRepeatKeyDown(2))
							{
								SelectRelativeTab(1);
								SFXManager.PlaySound("cursor");
							}
						}
						if (GameSettingsPage.Instance.CanExit() && GamePad.GetMenuKeyDown(1048576))
						{
							mExitReason = 0;
							mPreviuosPage = mCurrentPage;
							ChangePage(Page.Exit);
							SFXManager.PlaySound("New_MenuBack");
						}
						break;
					case Page.Controls:
						GamePadPage.Instance.Update();
						if (GamePadPage.Instance.IsChangingSettings)
						{
							if (mTabHeaders[mCurrentTabIndex].Alpha > 0.8f)
							{
								mTabHeaders[mCurrentTabIndex].Alpha = 0.8f;
							}
						}
						else
						{
							if (mTabHeaders[mCurrentTabIndex].Alpha < 1f)
							{
								mTabHeaders[mCurrentTabIndex].Alpha = 1f;
							}
							if (GamePad.GetMenuRepeatKeyDown(1))
							{
								SelectRelativeTab(-1);
								SFXManager.PlaySound("cursor");
							}
							else if (GamePad.GetMenuRepeatKeyDown(2))
							{
								SelectRelativeTab(1);
								SFXManager.PlaySound("cursor");
							}
						}
						if (!GamePadPage.Instance.CanExit())
						{
							return;
						}
						if (GamePad.GetMenuKeyDown(1048576))
						{
							mExitReason = 0;
							mPreviuosPage = mCurrentPage;
							ChangePage(Page.Exit);
							SFXManager.PlaySound("New_MenuBack");
						}
						break;
					case Page.HowToPlay:
						HowToPlayPage.Instance.Update();
						if (GamePad.GetMenuRepeatKeyDown(1))
						{
							SelectRelativeTab(-1);
							SFXManager.PlaySound("cursor");
						}
						else if (GamePad.GetMenuRepeatKeyDown(2))
						{
							SelectRelativeTab(1);
							SFXManager.PlaySound("cursor");
						}
						else if (GamePad.GetMenuKeyDown(1048576))
						{
							mExitReason = 0;
							mPreviuosPage = mCurrentPage;
							ChangePage(Page.Exit);
							SFXManager.PlaySound("New_MenuBack");
						}
						break;
					case Page.Credits:
						CreditPage.Instance.Update();
						if (GamePad.GetMenuRepeatKeyDown(1))
						{
							SelectRelativeTab(-1);
							SFXManager.PlaySound("cursor");
						}
						else if (GamePad.GetMenuRepeatKeyDown(2))
						{
							SelectRelativeTab(1);
							SFXManager.PlaySound("cursor");
						}
						else if (GamePad.GetMenuKeyDown(1048576))
						{
							mExitReason = 0;
							mPreviuosPage = mCurrentPage;
							ChangePage(Page.Exit);
							SFXManager.PlaySound("New_MenuBack");
						}
						break;
					case Page.Exit:
						if (IsImmediatelyRemoveProcessWhenExit)
						{
							ProcessManager.RemoveProcess(this);
						}
						if (mExitReason == 0)
						{
							if (OnBackButtonPressed != null)
							{
								OnBackButtonPressed();
							}
						}
						else if (mExitReason == 1 && OnOkButtonPressed != null)
						{
							OnOkButtonPressed();
						}
						break;
					}
					if (IsShowBetweenSelectCharacterAndEnterPlayScreen && !PopUpUI.Instance.IsRunning && GamePad.GetMenuKeyDown(524288))
					{
						SFXManager.PlaySound("ok");
						mExitReason = 1;
						mPreviuosPage = mCurrentPage;
						ChangePage(Page.Exit);
					}
					GamePad.UpdateClickables(mClickables);
				}
			}
		}

		public void PausedUpdate()
		{
			Update();
		}
	}
}
