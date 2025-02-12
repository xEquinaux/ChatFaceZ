using System;
using FoundationR;
using Color = System.Drawing.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using tUserInterface.ModUI;
using System.Globalization;
using System.Text;
using System.Windows.Media;
using Brushes = System.Windows.Media.Brushes;

namespace Foundation_GameTemplate
{
	public class Program
	{
		static int StartX => 0;
		static int StartY => 0;
		internal static int Width => 640;
		internal static int Height => 480;
		static int BitsPerPixel => 32;
		static string Title = "Foundation_GameTemplate";
		public static Main m;
		static void Launch(string[] args)
		{
			new Main().Run(SurfaceType.WindowHandle_Loop, new FoundationR.Surface(StartX, StartY, Width, Height, Title, BitsPerPixel)); ;
		}
		public static void Run(bool noBorder = false)
		{
			new Main().Run(noBorder ? SurfaceType.WindowHandle_Loop_NoBorder : SurfaceType.WindowHandle_Loop, new FoundationR.Surface(StartX, StartY, Width, Height, Title, BitsPerPixel)); ;
		}
	}
	public class Main : Foundation
	{
		List<string> message = new List<string>();
		int num = 0;
		int retry = 10;
		double maxWidth = 100;
		Scroll scroll = new Scroll(new Rectangle(0, 0, Program.Width, Program.Height));

		internal Main()
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

		protected void Camera(CameraArgs e)
		{
		}

		protected void PreDraw(PreDrawArgs e)
		{
		}

		protected void MainMenu(DrawingArgs e)
		{
		}

		protected void LoadResources()
		{
		}

		protected void Initialize(InitializeArgs e)
		{
		}

		protected void Draw(DrawingArgs e)
		{
			string font = "Arial";
			float yOffset = 0;
			string[] array = new string[this.message.Count];
			this.message.CopyTo(array, 0);
			foreach (var message in array)
			{
				List<string> wrappedText = WrapText(message, maxWidth, "Arial", 16f);
				e.rewBatch.Draw(REW.Create(50, 20, Color.Red, Ext.GetFormat(4)), 0, (int)yOffset + 12);
				foreach (var line in wrappedText)
				{
					e.rewBatch.DrawString(font, line, 50, (int)yOffset, 400, 1000);
					yOffset += 20;
				}
			}
		}

		protected void Input(InputArgs e)
		{
		}

		protected void Update(UpdateArgs e)
		{
			maxWidth = 300;
			Task.WaitAll(Task.Delay(1000));
			message.Add(++num + " Therewasanodditytodaywhileworkingwithsomeintegersinthecode. " + num);

			//	There was an oddity today while working with some integers in the code.
			//	Therewasanodditytodaywhileworkingwithsomeintegersinthecode.

			if (--retry <= 0)
			{
				message.Clear();
				retry = 10;
			}
		}

		protected new bool Resize()
		{
			return false;
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
							add += array2[i] + " ";
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
					string[] array = add.Split(' ');
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
			line.Add(text.Substring(Math.Min(text.Length, num3 - 1)));

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
					return WrapText(text, pixels, emSize);
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
}
