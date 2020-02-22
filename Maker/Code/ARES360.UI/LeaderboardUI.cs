using ARES360.Audio;
using ARES360.Entity;
using ARES360.Input;
using ARES360.Profile;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Steamworks;
using System;
using System.Collections.Generic;

namespace ARES360.UI
{
	public class LeaderboardUI : PositionedObject, IProcessable
	{
		private const int MAX_DISPLAYED_ENTRIES = 8;

		private const float GAP_BETWEEN_ENTRIES = 2.96875f;

		private const string CONTENT_MNG_NAME = "全球";

		private const string ARES_MODE_TEXT = "阿瑞斯";

		private const string TARUS_MODE_TEXT = "塔鲁斯";

		private const string MY_SCORE_TEXT = "我的分数";

		private const string FILTER_FRIENDS_TEXT = "好友";

		private const string FILTER_OVERALL_TEXT = "全球";

		public const int STATE_IDLE = 0;

		private uint mPreloadingStep;

		private bool mCurrentGamerIsOnAresLeaderboard;

		private bool mCurrentGamerIsOnTarusLeaderboard;

		private int mSelectedFilter;

		private bool mIsRequireFirstReload;

		private bool mIsPivotToCurrentSignedInGamer;

		private PlayerType mSelectedCharacter;

		private int mLeaderboardEntryCursor;

		private Vector3 mFirstEntryPosition;

		private Texture2D mLeaderboardEntryNormalTexture;

		private Texture2D mLeaderboardEntryHighlightTexture;

		private Sprite mLeaderboardPanelSprite;

		private ClickableLeaderboardEntry[] mLeaderboardEntrySprites;

		private Text[] mRankTexts;

		private Text[] mGamerTagTexts;

		private Text[] mScoreTexts;

		private Text mDateText;

		private Text mFilterText;

		private Text mCharacterText;

		private int mState;

		public VoidEventHandler OnBackButtonPressed;

		private static LeaderboardUI mInstance;

		private int mEntriesInCurrentPage;

		private bool mHasNextPage;

		private bool mHasPreviousPage;

		private bool mNeedHideLoadingUI;

		private bool mNeedShowControlHint;

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

		public static LeaderboardUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new LeaderboardUI();
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

		private LeaderboardUI()
		{
			mRankTexts = new Text[8];
			mGamerTagTexts = new Text[8];
			mScoreTexts = new Text[8];
			SteamLeaderboardManager.Instance.pageSize = 8;
			mFirstEntryPosition = new Vector3(0.475f, 6.4f, 0.1f);
			mSelectedFilter = 1;
			mSelectedCharacter = PlayerType.Mars;
			IsRemoveProcessWhenPressBack = true;
			mPreloadingStep = 0u;
			mIsPivotToCurrentSignedInGamer = false;
			mCurrentGamerIsOnAresLeaderboard = true;
			mCurrentGamerIsOnTarusLeaderboard = true;
		}

		public void Load()
		{
			mLeaderboardEntryNormalTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/leaderboard_entry_normal", "Global");
			mLeaderboardEntryHighlightTexture = FlatRedBallServices.Load<Texture2D>("Content/UI/leaderboard_entry_highlight", "Global");
			mLeaderboardPanelSprite = new Sprite();
			mLeaderboardPanelSprite.ScaleX = 48f;
			mLeaderboardPanelSprite.ScaleY = 24f;
			mLeaderboardPanelSprite.Texture = FlatRedBallServices.Load<Texture2D>(Localized.GetContentPath("UI/leaderboard_panel"), "Global");
			mLeaderboardPanelSprite.AttachTo(this, false);
			mLeaderboardEntrySprites = new ClickableLeaderboardEntry[8];
			for (int i = 0; i < 8; i++)
			{
				Sprite sprite = new Sprite();
				sprite.ScaleX = 32.5f;
				sprite.ScaleY = 1.25f;
				sprite.Texture = mLeaderboardEntryNormalTexture;
				sprite.RelativePosition.X = mFirstEntryPosition.X;
				sprite.RelativePosition.Y = mFirstEntryPosition.Y - 2.96875f * (float)i;
				sprite.RelativePosition.Z = mFirstEntryPosition.Z;
				sprite.AttachTo(mLeaderboardPanelSprite, false);
				mLeaderboardEntrySprites[i] = new ClickableLeaderboardEntry
				{
					tag = i,
					sprite = sprite,
					OnMouseEnter = (Clickable.ClickableHoverHandler)delegate(Clickable sender)
					{
						MoveLeaderboardCursorTo(sender.tag);
						SFXManager.PlaySound("cursor");
					},
					OnMouseClick = (Clickable.ClickableHandler)delegate(Clickable sender)
					{
						if (mLeaderboardEntryCursor != sender.tag)
						{
							MoveLeaderboardCursorTo(sender.tag);
							SFXManager.PlaySound("cursor");
							return true;
						}
						ExecuteSelectedGamer();
						return true;
					}
				};
			}
			mDateText = new Text(GUIHelper.CaptionFont);
			mDateText.HorizontalAlignment = HorizontalAlignment.Left;
			mDateText.ColorOperation = ColorOperation.Texture;
			mDateText.Scale = 0.8f;
			mDateText.Spacing = 0.8f;
			mDateText.RelativePosition = new Vector3(-27f, 16.35f, 0.1f);
			mDateText.DisplayText = DateTime.Now.ToString("MM-dd-yyyy");
			mDateText.AttachTo(mLeaderboardPanelSprite, false);
			mFilterText = new Text(GUIHelper.CaptionFont);
			mFilterText.HorizontalAlignment = HorizontalAlignment.Left;
			mFilterText.ColorOperation = ColorOperation.Texture;
			mFilterText.Scale = 0.8f;
			mFilterText.Spacing = 0.8f;
			mFilterText.RelativePosition = new Vector3(24.25f, 16.35f, 0.1f);
			mFilterText.DisplayText = "friends";
			mFilterText.AttachTo(mLeaderboardPanelSprite, false);
			mCharacterText = new Text(GUIHelper.CaptionFont);
			mCharacterText.HorizontalAlignment = HorizontalAlignment.Center;
			mCharacterText.ColorOperation = ColorOperation.Texture;
			mCharacterText.Scale = 1.3f;
			mCharacterText.Spacing = 1.3f;
			mCharacterText.RelativePosition = new Vector3(0f, 15f, 0.1f);
			mCharacterText.DisplayText = "ares";
			mCharacterText.AttachTo(mLeaderboardPanelSprite, false);
			for (int j = 0; j < 8; j++)
			{
				mRankTexts[j] = new Text(GUIHelper.SpeechFont);
				mRankTexts[j].HorizontalAlignment = HorizontalAlignment.Right;
				mRankTexts[j].ColorOperation = ColorOperation.Texture;
				mRankTexts[j].Scale = 1f;
				mRankTexts[j].Spacing = 1f;
				mRankTexts[j].RelativePosition = new Vector3(-17f, 0f, 0.1f);
				mRankTexts[j].DisplayText = "-";
				mRankTexts[j].AttachTo(mLeaderboardPanelSprite, false);
				mScoreTexts[j] = new Text(GUIHelper.SpeechFont);
				mScoreTexts[j].HorizontalAlignment = HorizontalAlignment.Right;
				mScoreTexts[j].ColorOperation = ColorOperation.Texture;
				mScoreTexts[j].Scale = 1f;
				mScoreTexts[j].Spacing = 1f;
				mScoreTexts[j].RelativePosition = new Vector3(31f, 0f, 0.1f);
				mScoreTexts[j].DisplayText = "-";
				mScoreTexts[j].AttachTo(mLeaderboardPanelSprite, false);
				mGamerTagTexts[j] = new Text(GUIHelper.SpeechFont);
				mGamerTagTexts[j].HorizontalAlignment = HorizontalAlignment.Center;
				mGamerTagTexts[j].ColorOperation = ColorOperation.Texture;
				mGamerTagTexts[j].Scale = 1f;
				mGamerTagTexts[j].Spacing = 1f;
				mGamerTagTexts[j].RelativePosition = new Vector3(0f, 0f, 0.1f);
				mGamerTagTexts[j].DisplayText = "-";
				mGamerTagTexts[j].AttachTo(mLeaderboardPanelSprite, false);
				mRankTexts[j].AttachTo(mLeaderboardEntrySprites[j].sprite, false);
				mScoreTexts[j].AttachTo(mLeaderboardEntrySprites[j].sprite, false);
				mGamerTagTexts[j].AttachTo(mLeaderboardEntrySprites[j].sprite, false);
			}
		}

		public void Unload()
		{
			mLeaderboardEntryNormalTexture = null;
			mLeaderboardEntryHighlightTexture = null;
			mLeaderboardPanelSprite.Detach();
			mLeaderboardPanelSprite = null;
			for (int i = 0; i < 8; i++)
			{
				mLeaderboardEntrySprites[i].sprite.Detach();
			}
			mLeaderboardEntrySprites = null;
			mDateText.Detach();
			mDateText = null;
			mFilterText.Detach();
			mFilterText = null;
			mCharacterText.Detach();
			mCharacterText = null;
			for (int j = 0; j < 8; j++)
			{
				mRankTexts[j].Detach();
				mScoreTexts[j].Detach();
				mGamerTagTexts[j].Detach();
			}
		}

		private bool MoveLeaderboardCursor(sbyte direction)
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
			int num2 = mLeaderboardEntryCursor + num;
			if (num2 < 0)
			{
				if (mHasPreviousPage)
				{
					LoadPreviousPage();
					return true;
				}
				return false;
			}
			if (num2 >= 8)
			{
				if (mHasNextPage)
				{
					LoadNextPage();
					return true;
				}
				return false;
			}
			mLeaderboardEntrySprites[mLeaderboardEntryCursor].sprite.Texture = mLeaderboardEntryNormalTexture;
			mLeaderboardEntryCursor = num2;
			mLeaderboardEntrySprites[mLeaderboardEntryCursor].sprite.Texture = mLeaderboardEntryHighlightTexture;
			return true;
		}

		private bool MoveLeaderboardCursorTo(int n)
		{
			mLeaderboardEntrySprites[mLeaderboardEntryCursor].sprite.Texture = mLeaderboardEntryNormalTexture;
			mLeaderboardEntryCursor = n;
			mLeaderboardEntrySprites[mLeaderboardEntryCursor].sprite.Texture = mLeaderboardEntryHighlightTexture;
			return true;
		}

		private void ResetSelectedEntry(int n)
		{
			if (n >= 0 && n < 8)
			{
				mLeaderboardEntryCursor = n;
				for (int i = 0; i < 8; i++)
				{
					if (mLeaderboardEntrySprites[i].sprite.Texture == mLeaderboardEntryHighlightTexture)
					{
						mLeaderboardEntrySprites[i].sprite.Texture = mLeaderboardEntryNormalTexture;
					}
				}
				mLeaderboardEntrySprites[n].sprite.Texture = mLeaderboardEntryHighlightTexture;
			}
		}

		public void Show()
		{
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

		public void SyncData()
		{
			int totalPageEntries = SteamLeaderboardManager.Instance.GetTotalPageEntries();
			for (int i = 0; i < totalPageEntries; i++)
			{
				SteamLeaderboardManager.LeaderboardPageEntry pageEntry = SteamLeaderboardManager.Instance.GetPageEntry(i);
				if (pageEntry.rank > 0)
				{
					mRankTexts[i].DisplayText = pageEntry.rank.ToString();
				}
				else
				{
					mRankTexts[i].DisplayText = "-";
				}
				mGamerTagTexts[i].DisplayText = pageEntry.gamerName;
				mScoreTexts[i].DisplayText = pageEntry.score.ToString();
			}
			for (int j = totalPageEntries; j < 8; j++)
			{
				mRankTexts[j].DisplayText = "-";
				mGamerTagTexts[j].DisplayText = "-";
				mScoreTexts[j].DisplayText = "-";
			}
		}

		private void LoadPreviousPage()
		{
			mHasNextPage = false;
			mHasPreviousPage = false;
			SteamLeaderboardManager.Instance.LoadPreviousPage();
			if (mSelectedFilter != 2 && mSelectedFilter != 3)
			{
				LoadingUI.Instance.Show(true, 0, HorizontalAlignment.Center);
			}
		}

		private void LoadNextPage()
		{
			mHasNextPage = false;
			mHasPreviousPage = false;
			SteamLeaderboardManager.Instance.LoadNextPage();
			if (mSelectedFilter != 2 && mSelectedFilter != 3)
			{
				LoadingUI.Instance.Show(true, 0, HorizontalAlignment.Center);
			}
		}

		public void OnRegister()
		{
			mNeedHideLoadingUI = false;
			mNeedShowControlHint = false;
			SpriteManager.AddPositionedObject(this);
			SpriteManager.AddToLayer(mLeaderboardPanelSprite, GUIHelper.UILayer);
			for (int i = 0; i < 8; i++)
			{
				SpriteManager.AddToLayer(mLeaderboardEntrySprites[i].sprite, GUIHelper.UILayer);
				TextManager.AddToLayer(mRankTexts[i], GUIHelper.UILayer);
				TextManager.AddToLayer(mScoreTexts[i], GUIHelper.UILayer);
				TextManager.AddToLayer(mGamerTagTexts[i], GUIHelper.UILayer);
			}
			ResetSelectedEntry(0);
			TextManager.AddToLayer(mDateText, GUIHelper.UILayer);
			TextManager.AddToLayer(mFilterText, GUIHelper.UILayer);
			TextManager.AddToLayer(mCharacterText, GUIHelper.UILayer);
			SteamLeaderboardManager.Instance.OnQueryStart = OnSteamLeaderboardQueryStart;
			SteamLeaderboardManager.Instance.OnQueryEnd = OnSteamLeaderboardQueryEnd;
			ClearEntries();
			LoadEntries(ProfileManager.Profile.LastPlayedCharacter, 1);
			ReloadSelectedCharacter();
			ReloadSelectedFilter();
			ShowControlHint();
			mIsRequireFirstReload = true;
			mCurrentGamerIsOnAresLeaderboard = true;
			mCurrentGamerIsOnTarusLeaderboard = true;
			mPreloadingStep = 0u;
		}

		public void OnRemove()
		{
			Detach();
			SteamLeaderboardManager.Instance.OnQueryStart = null;
			SteamLeaderboardManager.Instance.OnQueryEnd = null;
			SpriteManager.RemovePositionedObject(this);
			SpriteManager.RemoveSpriteOneWay(mLeaderboardPanelSprite);
			for (int i = 0; i < 8; i++)
			{
				SpriteManager.RemoveSpriteOneWay(mLeaderboardEntrySprites[i].sprite);
				TextManager.RemoveTextOneWay(mRankTexts[i]);
				TextManager.RemoveTextOneWay(mScoreTexts[i]);
				TextManager.RemoveTextOneWay(mGamerTagTexts[i]);
			}
			TextManager.RemoveTextOneWay(mDateText);
			TextManager.RemoveTextOneWay(mFilterText);
			TextManager.RemoveTextOneWay(mCharacterText);
			ControlHint.Instance.HideHints();
		}

		private void OnSteamLeaderboardQueryStart()
		{
		}

		private void OnSteamLeaderboardQueryEnd()
		{
			LoadingUI.Instance.Hide();
			mHasNextPage = SteamLeaderboardManager.Instance.HasNextPage();
			mHasPreviousPage = SteamLeaderboardManager.Instance.HasPreviousPage();
			mEntriesInCurrentPage = SteamLeaderboardManager.Instance.GetTotalPageEntries();
			int pageFilter = SteamLeaderboardManager.Instance.GetPageFilter();
			if (mSelectedFilter == 1 && pageFilter == 0)
			{
				mSelectedFilter = pageFilter;
				if (mSelectedCharacter == PlayerType.Mars)
				{
					mCurrentGamerIsOnAresLeaderboard = false;
				}
				else if (mSelectedCharacter == PlayerType.Tarus)
				{
					mCurrentGamerIsOnTarusLeaderboard = false;
				}
			}
			ReloadSelectedFilter();
			ShowControlHint();
			int i;
			for (i = 0; i < mEntriesInCurrentPage; i++)
			{
				SteamLeaderboardManager.LeaderboardPageEntry pageEntry = SteamLeaderboardManager.Instance.GetPageEntry(i);
				mRankTexts[i].DisplayText = pageEntry.rank.ToString();
				mScoreTexts[i].DisplayText = pageEntry.score.ToString();
				mGamerTagTexts[i].DisplayText = pageEntry.gamerName;
				mLeaderboardEntrySprites[i].steamId = pageEntry.steamId;
			}
			for (; i < 8; i++)
			{
				mRankTexts[i].DisplayText = "-";
				mScoreTexts[i].DisplayText = "-";
				mGamerTagTexts[i].DisplayText = "-";
			}
		}

		private void LoadEntries(PlayerType playerType, int filter)
		{
			mSelectedCharacter = playerType;
			mSelectedFilter = filter;
			LoadingUI.Instance.Show(true, 0, HorizontalAlignment.Center);
			SteamLeaderboardManager.Instance.StartQuery((playerType != PlayerType.Tarus) ? 1 : 2, filter);
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		private void ExecuteSelectedGamer()
		{
			if (Pref.Steamworks)
			{
				if (mLeaderboardEntryCursor < mEntriesInCurrentPage)
				{
					CSteamID steamId = mLeaderboardEntrySprites[mLeaderboardEntryCursor].steamId;
					SteamFriends.ActivateGameOverlayToUser("steamid", steamId);
					SFXManager.PlaySound("ok");
				}
				else
				{
					SFXManager.PlaySound("error");
				}
			}
		}

		private void ReloadSelectedCharacter()
		{
			switch (mSelectedCharacter)
			{
			case PlayerType.Tarus:
				mCharacterText.DisplayText = "塔鲁斯";
				break;
			default:
				mCharacterText.DisplayText = "阿瑞斯";
				break;
			}
		}

		private void ClearEntries()
		{
			mHasNextPage = false;
			mHasPreviousPage = false;
			mEntriesInCurrentPage = 0;
			for (int i = 0; i < 8; i++)
			{
				mRankTexts[i].DisplayText = "-";
				mScoreTexts[i].DisplayText = "-";
				mGamerTagTexts[i].DisplayText = "-";
			}
		}

		private void ReloadSelectedFilter()
		{
			switch (mSelectedFilter)
			{
			case 2:
			case 3:
				mFilterText.DisplayText = "好友";
				break;
			default:
				mFilterText.DisplayText = "全球";
				break;
			}
		}

		public void Update()
		{
			if (IsActivated)
			{
				if (mNeedHideLoadingUI)
				{
					LoadingUI.Instance.Hide();
					mNeedHideLoadingUI = false;
				}
				if (mNeedShowControlHint)
				{
					ShowControlHint();
					mNeedShowControlHint = false;
				}
				if (mState == 0)
				{
					if (GamePad.GetMenuRepeatKeyDown(8) || GamePad.keyboard.GetMouseScrollDown())
					{
						if (MoveLeaderboardCursor(-2))
						{
							SFXManager.PlaySound("cursor");
						}
					}
					else if (GamePad.GetMenuRepeatKeyDown(4) || GamePad.keyboard.GetMouseScrollUp())
					{
						if (MoveLeaderboardCursor(2))
						{
							SFXManager.PlaySound("cursor");
						}
					}
					else if (GamePad.GetMenuKeyDown(2097152))
					{
						if (mSelectedCharacter == PlayerType.Tarus)
						{
							SFXManager.PlaySound("ok");
							mSelectedCharacter = PlayerType.Mars;
							ReloadSelectedCharacter();
							ClearEntries();
							LoadEntries(mSelectedCharacter, mSelectedFilter);
							ShowControlHint();
						}
					}
					else if (GamePad.GetMenuKeyDown(4194304))
					{
						if (mSelectedCharacter == PlayerType.Mars)
						{
							SFXManager.PlaySound("ok");
							mSelectedCharacter = PlayerType.Tarus;
							ReloadSelectedCharacter();
							ClearEntries();
							LoadEntries(mSelectedCharacter, mSelectedFilter);
							ShowControlHint();
						}
					}
					else if (GamePad.GetMenuKeyDown(524288))
					{
						if (mLeaderboardEntryCursor < mEntriesInCurrentPage)
						{
							SFXManager.PlaySound("ok");
							ExecuteSelectedGamer();
						}
					}
					else if (GamePad.GetMenuKeyDown(16777216))
					{
						SFXManager.PlaySound("ok");
						ClearEntries();
						switch (mSelectedFilter)
						{
						case 0:
							LoadEntries(mSelectedCharacter, 2);
							break;
						case 1:
							LoadEntries(mSelectedCharacter, 3);
							break;
						case 2:
							LoadEntries(mSelectedCharacter, 0);
							break;
						case 3:
							LoadEntries(mSelectedCharacter, 1);
							break;
						}
						ReloadSelectedFilter();
						ShowControlHint();
					}
					else if (GamePad.GetMenuKeyDown(8388608))
					{
						SFXManager.PlaySound("ok");
						ClearEntries();
						switch (mSelectedFilter)
						{
						case 0:
							LoadEntries(mSelectedCharacter, 1);
							break;
						case 1:
							LoadEntries(mSelectedCharacter, 0);
							break;
						case 2:
							LoadEntries(mSelectedCharacter, 3);
							break;
						case 3:
							LoadEntries(mSelectedCharacter, 2);
							break;
						}
						ReloadSelectedFilter();
						ShowControlHint();
					}
					else if (GamePad.GetMenuKeyDown(1048576))
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
					else
					{
						GamePad.UpdateClickables((IList<ClickableSprite>)mLeaderboardEntrySprites);
					}
				}
			}
		}

		private void ShowControlHint()
		{
			ControlHint.Instance.Clear();
			if (mSelectedCharacter == PlayerType.Tarus)
			{
				ControlHint.Instance.AddHint(2097152, "阿瑞斯");
			}
			else if (mSelectedCharacter == PlayerType.Mars)
			{
				ControlHint.Instance.AddHint(4194304, "塔鲁斯");
			}
			ControlHint.Instance.AddHint(8388608, "我的分数");
			ControlHint.Instance.AddHint(16777216, "变更过滤项");
			ControlHint.Instance.AddHint(524288, "玩家页面");
			ControlHint.Instance.AddHint(1048576, "返回");
			ControlHint.Instance.ShowHints(HorizontalAlignment.Right);
			if ((mSelectedFilter == 0 || mSelectedFilter == 1) && ((mSelectedCharacter == PlayerType.Mars && !mCurrentGamerIsOnAresLeaderboard) || (mSelectedCharacter == PlayerType.Tarus && !mCurrentGamerIsOnTarusLeaderboard)))
			{
				ControlHint.Instance.SetHintActive(8388608, false);
			}
			else
			{
				ControlHint.Instance.SetHintActive(8388608, true);
			}
		}

		public void PausedUpdate()
		{
			Update();
		}
	}
}
