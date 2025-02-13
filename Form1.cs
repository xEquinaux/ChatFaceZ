using Foundation_GameTemplate;

namespace ChatFaceZ
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			new Main(0, 0, 800, 600, "Demo", 32);
		}
	}
}
