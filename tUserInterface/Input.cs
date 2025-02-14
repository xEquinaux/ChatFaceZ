using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Markup; // Or use whatever point class you like for the implicit cast operator

namespace ChatFaceZ
{
	/// <summary>
	/// Struct representing a point.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	public struct POINT
	{
		public int X;
		public int Y;

		public static implicit operator Point(POINT point)
		{
			return new Point(point.X, point.Y);
		}
	}

	//https://stackoverflow.com/questions/1316681/getting-mouse-position-in-c-sharp
	public class getInput
	{
		/// <summary>
		/// Retrieves the cursor's position, in screen coordinates.
		/// </summary>
		/// <see>See MSDN documentation for further information.</see>
		[DllImport("user32.dll")]
		static extern bool GetCursorPos(out POINT lpPoint);

		public static Point GetCursorPosition()
		{
			POINT lpPoint;
			GetCursorPos(out lpPoint);
			// NOTE: If you need error handling
			// bool success = GetCursorPos(out lpPoint);
			// if (!success)

			return lpPoint;
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern IntPtr FindWindow(string strClassName, string strWindowName);

		[DllImport("user32.dll")]
		public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

		public struct Rect {
		   public int Left { get; set; }
		   public int Top { get; set; }
		   public int Right { get; set; }
		   public int Bottom { get; set; }
		}

		public static System.Drawing.PointF getOrigin(string windowName)
		{
			Rect r = default;
			GetWindowRect(FindWindow("", windowName), ref r);
			return new System.Drawing.Point((int)r.Left, (int)r.Top);
		}
	}
}