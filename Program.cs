using System;
using REWD;
using REWD.FoundationR;
using Color = System.Drawing.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using tUserInterface.ModUI;
using System.Globalization;
using System.Text;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;
using TestConsole;
using System.Windows.Shapes;
using ChatFaceZ;
using ColorConverter = System.Drawing.ColorConverter;
using craigomatic.sample;

namespace Foundation_GameTemplate
{
	public class Program
	{
		static int StartX => 0;
		static int StartY => 0;
		internal static int Width => 800;
		internal static int Height => 600;
		static int BitsPerPixel => 32;
		static string Title = "Foundation_GameTemplate";
	}
	public class Main : Foundation
	{
		public static Main Instance;
		bool init;
		public IList<string> message = new List<string>();
		public IList<User> user = new List<User>();
		public int whoAmI;
		int num = 0;
		int retry = 10;
		double maxWidth = 300;
		//Scroll scroll = new Scroll(new Rectangle(0, 0, Program.Width, Program.Height));
		int remove = 100;
		float yOffset = 0;

		internal Main(int sx, int sy, int w, int y, string title, int bpp) : base(sx, sy, w, y, title, bpp)
		{
		}

		public override void RegisterHooks()
		{
			Foundation.UpdateEvent += Update;
			Foundation.ResizeEvent += Resize;
			Foundation.InputEvent += Input;
			Foundation.DrawEvent += Draw;
			Foundation.InitializeEvent += Initialize;
			Foundation.LoadResourcesEvent += LoadResources;
			Foundation.MainMenuEvent += MainMenu;
			Foundation.PreDrawEvent += PreDraw;
			Foundation.CameraEvent += Camera;
		}

		protected override void Camera(CameraArgs e)
		{
		}

		protected override void PreDraw(PreDrawArgs e)
		{
		}

		protected override void MainMenu(DrawingArgs e)
		{
		}

		protected override void LoadResources()
		{
		}

		protected override void Initialize(InitializeArgs e)
		{
			Instance = this;
			new TestConsole.Bot();
		}

		protected override void Draw(DrawingArgs e)
		{
			string font = "Arial";
			string[] array = new string[this.message.Count];
			this.message.CopyTo(array, 0);

			e.rewBatch.Draw(REW.Create((int)Program.Width, Program.Height, Color.Black, Ext.GetFormat(4)), 0, 0);

			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Replace(user[i].username + ": ", "");

				List<string> wrappedText = WrapText(array[i], maxWidth, "Arial", 16f);
				bool once = false;
				int whoAmI = 0;
				foreach (var line in wrappedText)
				{
					if (!once)
					{
						once = true;
						var _color = (Color)new ColorConverter().ConvertFromString(user[i].uColor);
						if (_color == Color.White)
						{
							_color = Color.Purple;
						}
						e.rewBatch.Draw(REW.Create((int)(maxWidth * 1.2f), 20, Color.FromArgb(30, 30, 30), Ext.GetFormat(4)), 62, (int)yOffset + 8 - ScrollLogic.yCoord(message.Count));
						e.rewBatch.DrawString(font, user[i].username, 50, (int)yOffset - ScrollLogic.yCoord(message.Count), (int)maxWidth * 2, Program.Height, _color, 12f);
						craigomatic.sample.AvatarGenerator.Generate(e.rewBatch, user[i].username, user[i].channel, 10, (int)yOffset + 8 - ScrollLogic.yCoord(message.Count), user[i].color, 40, 12);
						yOffset += 20f;
					}
					e.rewBatch.DrawString(font, line, 50, (int)(yOffset) - ScrollLogic.yCoord(message.Count), (int)maxWidth * 2, Program.Height);
					yOffset += 25f;
				}
			}
			yOffset = 0f;
		}

		protected override void Input(InputArgs e)
		{
		}

		protected override void Update(UpdateArgs e)
		{
			return;
			//maxWidth = 200;
			Task.WaitAll(Task.Delay(1000));

			string text = ++num + " There was an oddity today while working with some integers in the code. " + num;
			string word = " There was an oddity today while working with some integers in the code.";
			string bigword = " Therewasanodditytodaywhileworkingwithsomeintegersinthecode.";

			//set_Avatar("Test Console", "default", text);
			//set_Avatar("Test Console 4", "default", bigword);

			if (--remove <= 20)
			{
				for (int i = 0; i < 10; i++)
				{
					message.RemoveAt(i);
					user.RemoveAt(i);
				}
				remove = 20;
			}

			if (--retry <= 0)
			{
				//message.Clear();
				retry = 10;
			}
		}

		protected new bool Resize()
		{
			return false;
		}

		private int yCoord(int height, int i, int count, int boundsHeight, float value = 1f)
		{
			return height * i - (int)(value * (float)(boundsHeight + count * height));
		}

		public void set_Avatar(string username, string channel, string text, string uColor)
		{
			var user = this.user.FirstOrDefault(t => t.username == username);
			if (user != default)
			{
				this.user.Add(user);
			}
			else
			{
				var randomIndex = new Random().Next(0, craigomatic.sample.AvatarGenerator._BackgroundColours.Count - 1);
				var bgColour = craigomatic.sample.AvatarGenerator._BackgroundColours[randomIndex];
				this.user.Add(
					new User()
					{
						username = username,
						color = AvatarGenerator.RandomLight(),
						channel = channel,
						uColor = uColor
					});
			}
			this.message.Add($"{username}: {text}");
			this.whoAmI++;
		}

		private List<string> WrapText(string text, double maxWidth, float emSize)
		{
			List<string> line = new List<string>();
			int num = 0;
			int num2 = 0;
			int num3 = 0;

			string add = "";
			string[] word = text.Split(' ');
			foreach (string w in word)
			{
				string[] array2 = null;
				if (w.Length * emSize > maxWidth)
				{
					if ((array2 = SplitWord(w, maxWidth, emSize).ToArray()).Length > 1)
					{
						for (int i = 0; i < array2.Length; i++)
						{
							num2 += array2[i].Length;
							add += array2[i] + "_WRAP_";
							num3 += array2[i].Length;
						}
					}
				}
				else
				{
					num2 += w.Length;
					add += w + " ";
					num3 += w.Length;
				}
				if (num2 * emSize > maxWidth)
				{
					string[] array = add.Split("_WRAP_");
					for (int i = 0; i < array?.Length; i++)
					{
						if (!string.IsNullOrWhiteSpace(array[i]))
						{
							line.Add(array[i]);
						}
					}
					num2 = 0;
					add = "";
				}
			}
			if (!string.IsNullOrWhiteSpace(add))
			{ 
				line.Add(add);
			}

			return line;
		}

		private List<string> SplitWord(string text, double maxWidth, double emSize)
		{
			List<string> line = new List<string>();
			int num2 = 0;

			string add = "";
			foreach (char w in text)
			{
				num2++;
				add += w;
				if (num2 * emSize >= maxWidth)
				{
					line.Add(add);
					num2 = 0;
					add = "";
				}
			}
			line.Add(add);

			return line;
		}

		private double MeasureTextWidth(string text)
		{
			return 20;
		}

		[Obsolete]
		private List<string> WrapText(string text, double pixels, string fontFamily, float emSize)
		{
			string[] originalLines = text.Split(new string[] { " " },
				StringSplitOptions.None);

			List<string> line = new List<string>();
			int num = 0;
			int num2 = 0;
			int num3 = 0;

			string add = "";
			string[] word = text.Split(' ');
			foreach (string w in word)
			{
				if (w.Length * emSize > pixels)
				{
					return WrapText(text, pixels * 2, emSize);
				}
			}

			List<string> wrappedLines = new List<string>();

			StringBuilder actualLine = new StringBuilder();
			double actualWidth = 0;

			foreach (var item in originalLines)
			{
				FormattedText formatted = new FormattedText(item,
					CultureInfo.CurrentCulture,
					System.Windows.FlowDirection.LeftToRight,
					new Typeface(fontFamily), emSize, Brushes.Black);

				actualLine.Append(item + " ");
				actualWidth += formatted.Width;

				if (actualWidth > pixels)
				{
					wrappedLines.Add(actualLine.ToString());
					actualLine.Clear();
					actualWidth = 0;
				}
			}

			if (actualLine.Length > 0)
				wrappedLines.Add(actualLine.ToString());

			return wrappedLines;
		}
	}
	public class User
	{
		public string username;
		public string message;
		public Color color;
		public string channel;
		public string uColor;
		public float yOffset;
		public REW avatar;
	}
}
