using System;
using System.Drawing;
using System.IO;
using System.Numerics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Diagnostics;
using System.Windows.Forms;
using System.Windows.Media;
using FoundationR;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Foundation_GameTemplate
{
	public class Program
	{
		static int StartX => 0;
		static int StartY => 0;
		static int Width => 640;
		static int Height => 480;
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
				List<string> wrappedText = WrapText(message, 20);
				foreach (var line in wrappedText)
				{
					e.rewBatch.DrawString(font, line, 0, (int)yOffset, 300, 1000);
					yOffset += 20;
				}
			}
		}

		protected void Input(InputArgs e)
		{
		}

		protected void Update(UpdateArgs e)
		{
			Task.WaitAll(Task.Delay(1000));
			message.Add(++num + " Therewasanodditytodaywhileworkingwithsomeintegersinthecode. " + num);
			//There was an oddity today while working with some integers in the code.
			//Therewasanodditytodaywhileworkingwithsomeintegersinthecode.
			
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

		private List<string> WrapText(string text, double maxWidth)
		{
			List<string> line = new List<string>();
			int num = 0;
			int num2 = 0;
			int num3 = 0;

			string add = "";
			string[] word = text.Split(" ");
			foreach (string w in word)
			{
				string[] array2 = null;
				if (w.Length > maxWidth)
				{
					if ((array2 = SplitWord(w, 20, num2).ToArray()).Length > 1)
					{
						for (int i = 0; i < array2.Length; i++)
						{
							num2 += array2[i].Length;
							add += array2[i];
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
				if (num2 > maxWidth)
				{
					List<string> list = new List<string>();
					string complete = "";
					string trim = "";
					int num5 = 0;
					if (w.Length > maxWidth)
					{
						int num6 = 0;
						int num7 = 0;
						int num8 = 0;
						for (int i = (int)maxWidth * ++num6; i < w.Length; i += (int)maxWidth)
						{
							reduce:
							try
							{ 
								num8 = w.Substring(i, (int)maxWidth * num6 + num7).Length;
							}
							catch 
							{ 
								num7--; 
								goto reduce;
							}
							list.Add(w.Substring(i, num8));
						}
					}
					else
					{ 
						for (int i = 0; i < add.Length; i += (int)maxWidth)
						{
							list.Add(add.Substring(Math.Min(add.Length, i), Math.Min(add.Length - i, (int)maxWidth)));
						}
					}
					for (int i = 0; i < list?.Count; i++)
					{
						num5 += list[i].Length;
						complete += list[i];
						if (num5 > maxWidth)
						{
							line.Add(complete);
						}
					}
					num2 = 0;
					add = "";
				}
			}
			line.Add(text.Substring(Math.Min(text.Length, num3)));

			return line;
		}

		private List<string> SplitWord(string text, double maxWidth, double currentWidth)
		{
			List<string> line = new List<string>();
			int num2 = 0;
			
			string add = "";
			foreach (char w in text)
			{
				num2++;
				add += w;
				if (num2 >= maxWidth)
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
	}
}
