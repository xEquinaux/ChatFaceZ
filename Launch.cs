namespace ChatFaceZ
{
	internal static class Launch
	{
		[STAThread]
		static void Main()
		{
			new Chat_tacular.Login().ShowDialog();
			Application.Run(new Form1());
		}
	}
}