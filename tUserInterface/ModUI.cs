using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Color = System.Drawing.Color;
using ChatFaceZ;
using REWD.FoundationR;

namespace ChatFaceZ.tUserInterface
{
	public sealed class UiHelper
	{
		public static Rectangle CenterBox(int screenWidth, int screenHeight, int rectWidth, int rectHeight)
		{
			return new Rectangle(screenWidth / 2 - rectWidth / 2, screenHeight / 2 - rectHeight / 2, rectWidth, rectHeight);
		}
	}
	public class ListBox
	{
		public ListBox(Rectangle bounds, Scroll scroll, string[] content, REW[] icon = null, Color[] textColor = null)
		{
			hitbox = bounds;
			this.content = content;
			this.scroll = scroll;
			this.icon = icon;
			this.textColor = textColor;
			selectedPage = this;
		}
		public ListBox(Rectangle bounds, Scroll scroll, Button[] item)
		{
			hitbox = bounds;
			this.scroll = scroll;
			this.item = item;
		}
		public Button[] AddButton(string buttonText, Color color)
		{
			var _item = item.ToList();
			_item.Add(new Button("", default, color) { text2 = buttonText, innactiveDrawText = true });
			return _item.ToArray();
		}
		public bool active = true;
		public Rectangle hitbox;
		public Scroll scroll;
		public Color bgColor = Color.White;
		public string[] content;
		public REW[] icon;
		public Color[] textColor;
		public Button[] item;
		public Button[] tab;
		public ListBox[] page;
		private ListBox selectedPage;
		public int offX, offY;
		public void Update(bool canDrag = true)
		{
			if (!active)
				return;
			scroll.parent = hitbox;
			if (canDrag)
			{
				if (hitbox.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y) && !scroll.hitbox.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y) && !scroll.clicked)
				{
					hitbox = Element.Drag(hitbox);
				}
			}
			Scroll.KbInteract(scroll, Main.MouseScreen);
			Scroll.MouseInteract(scroll, Main.MouseScreen, Main.MouseLeft);
			Scroll.ScrollInteract(scroll, Main.MouseScreen);
		}
		public void Draw(RewBatch rew, string font, REW backgroundTex, int xOffset = 0, int yOffset = 0, int height = 42)
		{
			if (!active)
				return;
			rew.Draw(backgroundTex, hitbox.X, hitbox.Y);
			for (int n = 0; n < item.Length; n++)
			{
				float y = hitbox.Y + n * height - item.Length * height * scroll.value;
				if (y >= hitbox.Top && y <= hitbox.Bottom - height)
				{
					Rectangle box = new Rectangle(hitbox.X, (int)y, 32, 32);
					if (icon != null && icon.Length == item.Length)
					{
						rew.Draw(icon[n], hitbox.X, box.Y + yOffset);
					}
					if (textColor == null || textColor.Length != item.Length)
					{
						rew.DrawString(font, item[n].text, hitbox.X + xOffset, box.Y + yOffset, hitbox.Width, hitbox.Height, Color.White, 12f);
					}
					else rew.DrawString(font, item[n].text, hitbox.X + xOffset, box.Y + yOffset, hitbox.Width, hitbox.Height, Color.White, 12f);
				}
			}
		}
	}
	public class Scroll
	{
		public Scroll(Rectangle parent)
		{
			this.parent = parent;
		}
		public float value;
		private float x => parent.Right - Width;
		private float y => parent.Top + parent.Height * value;
		public int X => (int)x;
		public int Y => (int)y;
		public Rectangle parent;
		public Rectangle hitbox => new Rectangle(X, Y, Width, Height);
		public const int Width = 12;
		public const int Height = 32;
		public bool clicked;
		private bool flag;
		static int oldValue = 0;
		public static void DirectMouseInteract(Scroll bar, Vector2 mouseScreen, bool mouseLeft)
		{
			if (mouseLeft && bar.hitbox.Contains((int)mouseScreen.X, (int)mouseScreen.Y))
				bar.clicked = true;
			bar.flag = mouseLeft;
			if (!mouseLeft)
				bar.clicked = false;
			if (bar.clicked && bar.flag)
			{
				Vector2 mouse = new Vector2(mouseScreen.X, mouseScreen.Y - bar.parent.Top - Height / 2);
				bar.value = Math.Max(0f, Math.Min(mouse.Y / bar.parent.Height, 1f));
			}
		}
		public static void KbInteract(Scroll bar, Vector2 mouseScreen)
		{
			if (bar.parent.Contains((int)mouseScreen.X, (int)mouseScreen.Y))
			{
				if (Keyboard.GetState().IsKeyDown(Keys.Down))
				{
					if (bar.value * (bar.parent.Height - Height) < bar.parent.Height - Height)
					{
						bar.value += 0.04f;
					}
				}
				if (Keyboard.GetState().IsKeyDown(Keys.Up))
				{
					if (bar.value > 0f)
					{
						bar.value -= 0.04f;
					}
					else bar.value = 0f;
				}
			}
		}
		public static void MouseInteract(Scroll bar, Vector2 mouseScreen, bool mouseLeft)
		{
			if (mouseLeft && bar.hitbox.Contains((int)mouseScreen.X, (int)mouseScreen.Y))
				bar.clicked = true;
			bar.flag = mouseLeft;
			if (!mouseLeft)
				bar.clicked = false;
			if (bar.clicked && bar.flag)
			{
				Vector2 mouse = new Vector2(mouseScreen.X, mouseScreen.Y - bar.parent.Top - Height / 2);
				bar.value = Math.Max(0f, Math.Min(mouse.Y / bar.parent.Height, 1f));
			}
		}
		public static void ScrollInteract(Scroll bar, Vector2 mouseScreen)
		{
			if (bar.parent.Contains((int)mouseScreen.X, (int)mouseScreen.Y))
			{
				if (Mouse.GetState().ScrollWheelValue < oldValue)
				{
					bar.value = Math.Min(1f, bar.value + 0.1f);
				}
				else if (Mouse.GetState().ScrollWheelValue > oldValue)
				{
					bar.value = Math.Max(0f, bar.value - 0.1f);
				}
				oldValue = Mouse.GetState().ScrollWheelValue;
			}
		}
		public void Draw(RewBatch rew, REW texture, Color color)
		{
			rew.Draw(texture, hitbox.X, hitbox.Y);
		}
	}
	public class TextBox
	{
		public bool active;
		public string text = "";
		public Color color => active ? Color.FromArgb((byte)(255 * 0.67f), color2) : Color.FromArgb((byte)(255 * 0.33f), color2);
		private Color color2 = Color.DodgerBlue;
		public Rectangle box;
		private KeyboardState oldState;
		private KeyboardState keyState => Keyboard.GetState();
		public static Texture2D magicPixel;
		public static void Initialize(Texture2D magicPixel)
		{
			TextBox.magicPixel = magicPixel;
		}
		public TextBox(Rectangle box, Color color)
		{
			this.box = box;
			this.color2 = color;
		}
		public bool LeftClick()
		{
			return box.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y) && Main.MouseLeft;
		}
		public bool HoverOver()
		{
			return box.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y);
		}
		public void UpdateInput()
		{
			if (active)
			{
				foreach (Keys key in keyState.GetPressedKeys())
				{
					if (oldState.IsKeyUp(key))
					{
						if (key == Keys.F3)
							return;
						if (key == Keys.Back)
						{
							if (text.Length > 0)
								text = text.Remove(text.Length - 1);
							oldState = keyState;
							return;
						}
						else if (key == Keys.Space)
							text += " ";
						else if (key == Keys.OemPeriod)
							text += ".";
						else if (text.Length < 24 && key != Keys.OemPeriod)
						{
							string n = key.ToString().ToLower();
							if (n.StartsWith("d") && n.Length == 2)
								n = n.Substring(1);
							text += n;
						}
					}
				}
				oldState = keyState;
			}
		}
		public void DrawText(RewBatch rew, REW bgTexture, string font, bool drawMagicPixel = false)
		{
			if (!active)
				return;
			rew.Draw(bgTexture, box.X, box.Y);
			rew.DrawString(font, text, box.X + 2, box.Y + 1, bgTexture.Width, bgTexture.Height, Color.White, 12f);
		}
	}
	public class Button
	{
		public bool active = true;
		public bool innactiveDrawText = false;
		public bool drawMagicPixel = false;
		public string text = "";
		public string text2 = "";
		public int reserved;
		private int tick = 0;
		private Color color2 = Color.DodgerBlue;
		private static REW magicPixel;
		public int offX = 0;
		public int offY = 0;
		public static void Initialize(REW magicPixel)
		{
			Button.magicPixel = magicPixel;
		}
		private Rectangle boundCorrect => new Rectangle(box.X - box.Width + offX, box.Y - box.Height * 2 + offY, box.Width, box.Height);
		public Color color(bool select = true)
		{
			if (select)
				return boundCorrect.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y) ? Color.FromArgb((byte)(255 * 0.67f), color2) : Color.FromArgb((byte)(255 * 0.33f), color2);
			else
			{
				return Color.FromArgb((byte)(255 * 0.67f), Color.White);
			}
		}
		public Rectangle box;
		public REW texture;
		public bool LeftClick()
		{
			return active && box.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y) && Main.MouseLeft;
		}
		public bool LeftClick(Rectangle hitbox)
		{
			return active && hitbox.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y) && Main.MouseLeft;
		}
		public bool HoverOver()
		{
			return boundCorrect.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y);
		}
		public bool HoverOver(Rectangle bound)
		{
			return bound.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y);
		}
		public Button(string text, Rectangle box, Color color)
		{
			this.color2 = color;
			if (texture == null)
				this.texture = Button.magicPixel;
			this.text = text;
			this.box = box;
		}
		public Button(string text, Rectangle box, REW texture = null)
		{
			this.texture = texture;
			if (texture == null)
				this.texture = magicPixel;
			this.text = text;
			this.box = box;
		}
		public void Draw(RewBatch rew, REW texture, bool select = true)
		{
			if (!active)
				return;
			rew.Draw(texture, box.X, box.Y);
		}
		public void Draw(RewBatch rew, REW texture, string font, bool select = true)
		{
			if (!active)
				return;
			rew.Draw(texture, box.X, box.Y);
			rew.DrawString(font, text, box.X + 2 + offX, box.Y + 2 + offY, texture.Width, texture.Height, color(select), 12f);
		}
	}
	public class InputBox
	{
		public bool active;
		public string text = "";
		public Color color
		{
			get { return active ? Color.FromArgb((byte)(255 * 0.67f), Color.DodgerBlue) : Color.FromArgb((byte)(255 * 0.33f), Color.DodgerBlue); }
		}
		public Rectangle box;
		private KeyboardState oldState;
		private KeyboardState keyState
		{
			get { return Keyboard.GetState(); }
		}
		public InputBox(Rectangle box)
		{
			this.box = box;
		}
		public bool LeftClick()
		{
			return box.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y) && Main.MouseLeft;
		}
		public bool HoverOver()
		{
			return box.Contains((int)Main.MouseScreen.X, (int)Main.MouseScreen.Y);
		}
		public void UpdateInput()
		{
			if (active)
			{
				foreach (Keys key in keyState.GetPressedKeys())
				{
					if (oldState.IsKeyUp(key))
					{
						if (key == Keys.F3)
							return;
						if (key == Keys.Back)
						{
							if (text.Length > 0)
								text = text.Remove(text.Length - 1);
							oldState = keyState;
							return;
						}
						else if (key == Keys.Space)
							text += " ";
						else if (key == Keys.OemPeriod)
							text += ".";
						else if (text.Length < 24 && (key.ToString().StartsWith('D') || key.ToString().Length == 1))
						{
							string n = key.ToString().ToLower();
							if (n.StartsWith("d") && n.Length == 2)
								n = n.Substring(1);
							text += n;
						}
					}
				}
				oldState = keyState;
			}
		}
		public void DrawText(RewBatch rew, REW background, string font)
		{
			if (background != null && font != null)
			rew.Draw(background, box.X, box.Y);
			rew.DrawString(font, text, box.X + 2, box.Y + 1, background.Width, background.Height, Color.White, 12f);
		}
	}
}