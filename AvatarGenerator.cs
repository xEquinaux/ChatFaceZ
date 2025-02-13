using REWD.FoundationR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;

//https://gist.github.com/craigomatic/c5d2363820aaa818dee1
namespace craigomatic.sample
{
	public class AvatarGenerator
	{
		public static List<string> _BackgroundColours = new List<string> { "B26126", "FFF7F2", "FFE8D8", "74ADB2", "D8FCFF" };

		public AvatarGenerator()
		{
		}

		public static void Generate(RewBatch rew, string firstName, string lastName, int x, int y, string color, int dimensions = 192, int fontSize = 48)
		{
			var avatarString = string.Format("{0}{1}", firstName[0], lastName[0]).ToUpper();

			rew.Draw(REW.Create(
				dimensions, 
				dimensions, 
				(Color)new ColorConverter().ConvertFromString("#" + color), 
				Ext.GetFormat(4)), x, y);
			rew.DrawString("Arial", avatarString, x - 4, y + 2, dimensions, dimensions, Color.Black, fontSize);
		}
	}
}