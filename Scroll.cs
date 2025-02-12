// CirclePrefect, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// CirclePrefect.Wpf.Surface.Scroll
using System;
using System.Drawing;
using CirclePrefect.Native;
using CirclePrefect.Wpf.Surface;

public class Scroll
{
	public float value;

	public System.Drawing.Rectangle parent;

	public const int Width = 12;

	public const int Height = 32;

	private bool clicked;

	private bool flag;

	private float x => parent.Right - 12;

	private float y => (float)parent.Top + (float)parent.Height * value;

	public int X => (int)x;

	public int Y => (int)y;

	public System.Drawing.Rectangle hitbox => new System.Drawing.Rectangle(X, Y, 12, 32);

	internal static void DirectKbInteract(Scroll bar)
	{
		bar.parent.Contains((int)DirectTerminal.MainMouse.X, (int)DirectTerminal.MainMouse.Y);
	}

	internal static void DirectMouseInteract(Scroll bar)
	{
		if (DirectTerminal.MouseLeft && bar.hitbox.Contains((int)DirectTerminal.MainMouse.X, (int)DirectTerminal.MainMouse.Y))
		{
			bar.clicked = true;
		}
		bar.flag = DirectTerminal.MouseLeft;
		if (!DirectTerminal.MouseLeft)
		{
			bar.clicked = false;
		}
		if (bar.clicked && bar.flag)
		{
			bar.value = Math.Max(0f, Math.Min(new Vector2(DirectTerminal.MainMouse.X, DirectTerminal.MainMouse.Y - (float)bar.parent.Top - 16f).Y / (float)bar.parent.Height, 1f));
		}
	}

	internal static void KbInteract(Scroll bar)
	{
		bar.parent.Contains((int)DrawTerminal.MainMouse.X, (int)DrawTerminal.MainMouse.Y);
	}

	internal static void MouseInteract(Scroll bar)
	{
		if (DrawTerminal.MouseLeft && bar.hitbox.Contains((int)DrawTerminal.MainMouse.X, (int)DrawTerminal.MainMouse.Y))
		{
			bar.clicked = true;
		}
		bar.flag = DrawTerminal.MouseLeft;
		if (!DrawTerminal.MouseLeft)
		{
			bar.clicked = false;
		}
		if (bar.clicked && bar.flag)
		{
			bar.value = Math.Max(0f, Math.Min(new Vector2(DrawTerminal.MainMouse.X, DrawTerminal.MainMouse.Y - (float)bar.parent.Top - 16f).Y / (float)bar.parent.Height, 1f));
		}
	}
}
