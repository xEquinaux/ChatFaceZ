// CirclePrefect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// CirclePrefect.Wpf.Surface.DirectTerminal
using System.Collections.Generic;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using CirclePrefect.Foundation;
using CirclePrefect.Foundation.Ext;
using CirclePrefect.Native;
using CirclePrefect.Wpf.Surface;

public class DirectTerminal
{
	public Window window;

	public static Game gui;

	public static Vector2 MainMouse;

	public bool active;

	public float emSize = 10f;

	internal static List<string> console = new List<string>();

	internal static List<string> cmd = new List<string>();

	private Scroll scroll = new Scroll();

	private System.Drawing.Rectangle bounds;

	internal static string Input = "";

	private string caret = "_";

	private static string Command = "";

	private int index;

	public static bool MouseLeft => CirclePrefect.Foundation.Ext.Cursor.IsLeftPressed();

	public static bool MouseRight => CirclePrefect.Foundation.Ext.Cursor.IsRightPressed();

	public void Create(int x, int y, int width, int height, Game gui)
	{
		DirectTerminal.gui = gui;
		bounds = new System.Drawing.Rectangle(x, y, width, height);
		scroll.parent = bounds;
	}

	public void Draw(Game.DrawingArgs e)
	{
		e.graphics.FillRectangle(new SolidBrush(Color.FromArgb(110, 100, 100)), bounds);
		Font font = new Font(FontFamily.GenericMonospace.Name, emSize);
		SolidBrush bg = new SolidBrush(Color.FromArgb(50, Color.Black));
		for (int i = 0; i < console.Count; i++)
		{
			int height = 14;
			SolidBrush fg = new SolidBrush(Color.FromArgb(225, 225, 225));
			System.Drawing.Point point = new System.Drawing.Point(10, bounds.Y + yCoord(height, i));
			if (point.Y >= bounds.Y && point.Y <= bounds.Y + bounds.Height)
			{
				DrawString(console[i], font, bg, fg, point.X, point.Y, e.graphics);
				DrawString(Input + caret, font, bg, fg, 10, bounds.Y + yCoord(height, console.Count), e.graphics);
			}
		}
		e.graphics.FillRectangle(Brushes.Gray, new RectangleF(scroll.X, scroll.Y, 16f, 24f));
	}

	private int yCoord(int height, int i)
	{
		return height * i - (int)(scroll.value * (float)(bounds.Height + console.Count * height));
	}

	public static void DrawString(string text, Font font, Brush bg, Brush fg, int x, int y, Graphics graphics)
	{
		for (int i = -1; i <= 1; i++)
		{
			for (int j = -1; j <= 1; j++)
			{
				graphics.DrawString(text, font, bg, x + i, y + j);
			}
		}
		graphics.DrawString(text, font, fg, x, y);
	}

	public void Update(Game.UpdateArgs e)
	{
		Window window = this.window;
		System.Drawing.Point position = System.Windows.Forms.Cursor.Position;
		MainMouse = new Vector2(position.X - (int)window.Left - 8, position.Y - (int)window.Top - 30);
		if (bounds.Contains((int)MainMouse.X, (int)MainMouse.Y))
		{
			active = true;
		}
		else if (CirclePrefect.Foundation.Ext.Cursor.IsLeftPressed())
		{
			active = false;
		}
		Scroll.DirectKbInteract(scroll);
		Scroll.DirectMouseInteract(scroll);
	}

	public void terminal_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
	{
		if (!active)
		{
			return;
		}
		if (e.Key == Key.Escape)
		{
			active = false;
			return;
		}
		if (e.Key == Key.Back && Input.Length > 0)
		{
			Input = Input.Substring(0, Input.Length - 1);
		}
		Input += KeyChar.Convert(e.Key).ToLower();
		if (e.Key == Key.Up && index > 0)
		{
			index--;
			Input = cmd[index];
		}
		if (e.Key == Key.Down)
		{
			if (index < cmd.Count - 1)
			{
				index++;
				Input = cmd[index];
			}
			else
			{
				Input = "";
			}
		}
		if (e.Key == Key.Return)
		{
			HandleCommands(Input);
		}
	}

	private void HandleCommands(string command)
	{
		if (string.IsNullOrWhiteSpace(command))
		{
			Input = "";
			return;
		}
		Input = "";
		console.Add(command);
		cmd.Add(command);
		index = cmd.Count;
		Command = command;
	}

	public void WriteLine(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
		{
			Input = "";
			return;
		}
		Input = "";
		int num = 0;
		for (int i = 0; i < value.Length; i++)
		{
			if ((float)(i - num) * (emSize * 0.9f) >= (float)bounds.Width - emSize)
			{
				value = value.Insert(i, "|");
				num = i;
			}
		}
		if (!value.Contains("|"))
		{
			console.Add(value);
		}
		else
		{
			string[] array = value.Split('|');
			foreach (string item in array)
			{
				console.Add(item);
			}
		}
		index = cmd.Count;
	}

	public static string GetCommand()
	{
		string result = Command.Clone().ToString();
		Command = "";
		return result;
	}

	public static string ReadLine()
	{
		if (string.IsNullOrWhiteSpace(Command))
		{
			return "";
		}
		string result = Command.Clone().ToString();
		Command = "";
		return result;
	}
}
