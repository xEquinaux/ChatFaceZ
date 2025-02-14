using System;

namespace ChatFaceZ
{
	internal static class Launch
	{
		[STAThread]
		static void Main()
		{
			new Chat_tacular.Login().ShowDialog();
			new Main(0, 0, 800, 600, "Chat FaceZ", 32, true);
		}
	}
}