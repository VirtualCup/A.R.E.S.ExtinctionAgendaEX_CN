using ARES360.Audio;
using ARES360.Input;
using FlatRedBall;
using FlatRedBall.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ARES360.UI
{
	public class PopUpUI : IProcessable
	{
		public enum PopUpUIResult
		{
			None,
			Ok,
			Cancel,
			Other
		}

		public delegate void PopUpUICallback(PopUpUIResult result);

		private static PopUpUI mInstance;

		private PopUpUICallback mCallback;

		private Sprite mPanel;

		private Sprite[] mFrame;

		private Text mHeader;

		private Text mMessage;

		private float mTimer;

		private string mMessageTemplate;

		private PopUpUIResult mResult;

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

		public static PopUpUI Instance
		{
			get
			{
				if (mInstance == null)
				{
					mInstance = new PopUpUI();
				}
				return mInstance;
			}
		}

		public void Load()
		{
			mPanel = new Sprite();
			mPanel.Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/popup_panel", "Global");
			mPanel.ScaleX = 16f;
			mPanel.ScaleY = 5.5f;
			mPanel.RelativePosition = new Vector3(0f, 0f, -60f);
			mFrame = new Sprite[2];
			mFrame[0] = new Sprite();
			mFrame[0].Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/popup_frame", "Global");
			mFrame[0].ScaleX = 16f;
			mFrame[0].ScaleY = 4f;
			mFrame[0].RelativePosition = new Vector3(-31.3f, -8.3f, 0f);
			mFrame[0].AttachTo(mPanel, false);
			mFrame[1] = new Sprite();
			mFrame[1].Texture = FlatRedBallServices.Load<Texture2D>("Content/UI/popup_frame", "Global");
			mFrame[1].ScaleX = 16f;
			mFrame[1].ScaleY = 4f;
			mFrame[1].RelativePosition = new Vector3(31.3f, 8.3f, 0f);
			mFrame[1].RelativeRotationZ = 3.14159274f;
			mFrame[1].AttachTo(mPanel, false);
			mHeader = new Text(GUIHelper.CaptionFont);
			mHeader.Scale = 1.2f;
			mHeader.Spacing = 1.2f;
			mHeader.HorizontalAlignment = HorizontalAlignment.Left;
			mHeader.VerticalAlignment = VerticalAlignment.Center;
			mHeader.ColorOperation = ColorOperation.Texture;
			mHeader.BlendOperation = BlendOperation.Add;
			mHeader.RelativePosition = new Vector3(-14.8f, 5.6f, 0f);
			mHeader.AttachTo(mPanel, false);
			mMessage = new Text(GUIHelper.SpeechFont);
			mMessage.Scale = 1f;
			mMessage.Spacing = 1f;
			mMessage.NewLineDistance = 2.3f;
			mMessage.HorizontalAlignment = HorizontalAlignment.Center;
			mMessage.VerticalAlignment = VerticalAlignment.Center;
			mMessage.ColorOperation = ColorOperation.Texture;
			mMessage.RelativePosition = new Vector3(0f, 0.7f, 0.01f);
			mMessage.AttachTo(mPanel, false);
		}

		public void Show(string header, string message, PopUpUICallback callback)
		{
			mTimer = 0f;
			mHeader.DisplayText = header;
			mMessage.DisplayText = message;
			mCallback = callback;
			mResult = PopUpUIResult.None;
			ControlHint.Instance.Clear().AddHint(524288, "确认").AddHint(1048576, "取消")
				.ShowHints(HorizontalAlignment.Center, SpriteManager.TopLayer);
			ProcessManager.AddProcess(this);
		}

		public void ShowCountDown(string header, string message, float timer, PopUpUICallback callback)
		{
			mMessageTemplate = message;
			mHeader.DisplayText = header;
			mMessage.DisplayText = string.Format(mMessageTemplate, (int)mTimer);
			mCallback = callback;
			mTimer = timer;
			mResult = PopUpUIResult.None;
			ControlHint.Instance.Clear().AddHint(524288, "确认").AddHint(1048576, "取消")
				.ShowHints(HorizontalAlignment.Center, SpriteManager.TopLayer);
			ProcessManager.AddProcess(this);
		}

		public void OnRegister()
		{
			SpriteManager.AddToLayer(mPanel, SpriteManager.TopLayer);
			mPanel.AttachTo(World.Camera, false);
			SpriteManager.AddToLayer(GUIHelper.TopMask, SpriteManager.TopLayer);
			SpriteManager.AddToLayer(mFrame[0], SpriteManager.TopLayer);
			SpriteManager.AddToLayer(mFrame[1], SpriteManager.TopLayer);
			TextManager.AddToLayer(mHeader, SpriteManager.TopLayer);
			TextManager.AddToLayer(mMessage, SpriteManager.TopLayer);
		}

		public void OnPause()
		{
		}

		public void OnResume()
		{
		}

		public void OnRemove()
		{
			mPanel.Detach();
			SpriteManager.RemoveSpriteOneWay(mPanel);
			SpriteManager.RemoveSpriteOneWay(GUIHelper.TopMask);
			SpriteManager.RemoveSpriteOneWay(mFrame[0]);
			SpriteManager.RemoveSpriteOneWay(mFrame[1]);
			TextManager.RemoveTextOneWay(mHeader);
			TextManager.RemoveTextOneWay(mMessage);
		}

		public void Update()
		{
			bool flag = false;
			if (mResult != 0)
			{
				flag = true;
			}
			else if (GamePad.GetMenuKeyDown(524288))
			{
				mResult = PopUpUIResult.Ok;
				SFXManager.PlaySound("ok");
			}
			else if (GamePad.GetMenuKeyDown(1048576))
			{
				mResult = PopUpUIResult.Cancel;
				SFXManager.PlaySound("New_MenuBack");
			}
			else if (mTimer > 0f)
			{
				mTimer -= TimeManager.SecondDifference;
				mMessage.DisplayText = string.Format(mMessageTemplate, (int)mTimer);
				if (mTimer < 0f)
				{
					flag = true;
					mResult = PopUpUIResult.Cancel;
				}
			}
			if (flag)
			{
				ControlHint.Instance.HideHints();
				ProcessManager.RemoveProcess(this);
				if (mCallback != null)
				{
					PopUpUICallback popUpUICallback = mCallback;
					mCallback = null;
					popUpUICallback(mResult);
				}
			}
		}

		public void PausedUpdate()
		{
			Update();
		}
	}
}
