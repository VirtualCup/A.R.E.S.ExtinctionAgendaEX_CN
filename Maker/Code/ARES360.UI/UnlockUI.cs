using ARES360.Audio;
using ARES360.Entity;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace ARES360.UI
{
	public class UnlockUI : IProcessable
	{
		private static UnlockUI mInstance;

		private Sprite mPanel;

		private Sprite mThumb;

		private Text mTitle;

		private Text mName;

		private StringBuilder mNameBuilder;

		private string[] mDisplayTitle;

		private string mDisplayName;

		private float mShowTime;

		private float mNameTime;

		private int mNameIndex;

		private Vector3 mSavedPanelRelVelocity;

		private Vector3 mSavedTitleRelVelocity;

		private float mSavedTitleAlphaRate;

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

		public static UnlockUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new UnlockUI();
				}
				return mInstance;
			}
		}

		public void Load()
		{
			mDisplayTitle = new string[3];
			mDisplayTitle[0] = "武 器 获 得";
			mDisplayTitle[1] = "能 力 解 锁";
			mDisplayTitle[2] = "装 甲 强 化";
			mNameBuilder = new StringBuilder();
			mPanel = new Sprite();
			mPanel.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/unlock_b", "Global");
			mPanel.ScaleX = 42f;
			mPanel.ScaleY = 16f;
			mThumb = new Sprite();
			mThumb.ScaleX = 12f;
			mThumb.ScaleY = 9.09f;
			mThumb.AttachTo(mPanel, false);
			mThumb.RelativePosition = new Vector3(0f, 1f, 0.1f);
			mTitle = new Text(GUIHelper.CaptionFont);
			mTitle.Scale = 1.6f;
			mTitle.Spacing = 1.6f;
			mTitle.ColorOperation = ColorOperation.Texture;
			mTitle.HorizontalAlignment = HorizontalAlignment.Center;
			mTitle.VerticalAlignment = VerticalAlignment.Center;
			mTitle.AttachTo(mPanel, false);
			mTitle.RelativePosition = new Vector3(0f, 13f, 0.1f);
			mName = new Text(GUIHelper.CaptionFont);
			mName.Scale = 1f;
			mName.Spacing = 1f;
			mName.ColorOperation = ColorOperation.Texture;
			mName.HorizontalAlignment = HorizontalAlignment.Center;
			mName.VerticalAlignment = VerticalAlignment.Center;
			mName.AttachTo(mPanel, false);
			mName.RelativePosition = new Vector3(0f, -8.2f, 0.1f);
		}

		public void Show(int type)
		{
			mDisplayName = Upgrade.GetUpgradeCaptionName(Player.Instance.Type, type);
			mThumb.Texture = GUIHelper.UpgradeInfo[(int)Player.Instance.Type][type].Icon[1];
			mTitle.DisplayText = mDisplayTitle[type / 4];
			mName.DisplayText = string.Empty;
			mNameBuilder = new StringBuilder();
			mShowTime = 6.8f;
			mNameTime = 0f;
			mNameIndex = 0;
			SFXManager.BGMVolume = BGMManager.Volume;
			SFXManager.PlaySound("unlock");
			ProcessManager.AddProcess(this);
		}

		public void OnRegister()
		{
			SpriteManager.AddToLayer(mPanel, GUIHelper.UILayer);
			mPanel.AttachTo(World.Camera, false);
			mPanel.RelativePosition = new Vector3(0f, -6f, -30f);
			mPanel.RelativeVelocity = new Vector3(0f, 8f, -60f);
			mPanel.Alpha = 0f;
			mPanel.AlphaRate = 2f;
			SpriteManager.AddToLayer(mThumb, GUIHelper.UILayer);
			mThumb.Alpha = 0f;
			mThumb.AlphaRate = 2f;
			TextManager.AddToLayer(mTitle, GUIHelper.UILayer);
			mTitle.Alpha = 0f;
			mTitle.RelativePosition = new Vector3(0f, 15f, 30.1f);
			TextManager.AddToLayer(mName, GUIHelper.UILayer);
		}

		public void OnPause()
		{
			mSavedPanelRelVelocity = mPanel.RelativeVelocity;
			mSavedTitleRelVelocity = mTitle.RelativeVelocity;
			mSavedTitleAlphaRate = mTitle.AlphaRate;
			mPanel.RelativeVelocity = Vector3.Zero;
			mTitle.RelativeVelocity = Vector3.Zero;
			mTitle.AlphaRate = 0f;
			mPanel.Visible = false;
			mTitle.Visible = false;
			mThumb.Visible = false;
			mName.Visible = false;
		}

		public void OnResume()
		{
			mPanel.RelativeVelocity = mSavedPanelRelVelocity;
			mTitle.RelativeVelocity = mSavedTitleRelVelocity;
			mTitle.AlphaRate = mSavedTitleAlphaRate;
			mPanel.Visible = true;
			mTitle.Visible = true;
			mThumb.Visible = true;
			mName.Visible = true;
		}

		public void OnRemove()
		{
			mPanel.Detach();
			SpriteManager.RemoveSpriteOneWay(mPanel);
			SpriteManager.RemoveSpriteOneWay(mThumb);
			TextManager.RemoveTextOneWay(mTitle);
			TextManager.RemoveTextOneWay(mName);
		}

		public void Update()
		{
			if (mPanel.RelativeVelocity.Z < 0f && mPanel.RelativePosition.Z <= -60f)
			{
				mPanel.RelativePosition = new Vector3(0f, -2f, -60f);
				mPanel.RelativeVelocity = Vector3.Zero;
				World.Camera.Shake(1, 0.2f, true);
				mTitle.AlphaRate = 2f;
				mTitle.RelativeVelocity = new Vector3(0f, -8f, -60f);
			}
			if (mTitle.RelativeVelocity.Z < 0f && mTitle.RelativePosition.Z <= 0.1f)
			{
				mTitle.RelativePosition = new Vector3(0f, 12f, 0.1f);
				mTitle.RelativeVelocity = Vector3.Zero;
				World.Camera.Shake(1, 0.2f, true);
				mNameTime = 0.05f;
			}
			if (mNameTime > 0f)
			{
				mNameTime -= TimeManager.SecondDifference;
				if (mNameTime <= 0f && mNameIndex < mDisplayName.Length)
				{
					mNameBuilder.Append(mDisplayName[mNameIndex]);
					mName.DisplayText = mNameBuilder.ToString();
					mNameTime = 0.1f;
					mNameIndex++;
					SFXManager.PlaySound("number");
				}
			}
			mShowTime -= TimeManager.SecondDifference;
			if (mShowTime <= 0f)
			{
				mPanel.RelativeVelocity.Y = -200f;
			}
			if (mPanel.RelativeVelocity.Y < 0f && mPanel.RelativePosition.Y < -100f)
			{
				mPanel.RelativeVelocity = Vector3.Zero;
				ProcessManager.RemoveProcess(this);
			}
		}

		public void PausedUpdate()
		{
		}
	}
}
