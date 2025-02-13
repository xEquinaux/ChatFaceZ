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
      private List<string> _BackgroundColours;

      public AvatarGenerator()
      {
          _BackgroundColours = new List<string> { "B26126", "FFF7F2", "FFE8D8", "74ADB2", "D8FCFF" };
      }

      public Bitmap Generate(string firstName, string lastName, int dimesions = 192, int fontSize = 48)
      {
          var avatarString = string.Format("{0}{1}", firstName[0], lastName[0]).ToUpper();

          var randomIndex = new Random().Next(0, _BackgroundColours.Count - 1);
          var bgColour = _BackgroundColours[randomIndex];

          var bmp = new Bitmap(dimesions, dimesions);
          var sf = new StringFormat();
          sf.Alignment = StringAlignment.Center;
          sf.LineAlignment = StringAlignment.Center;

          var font = new Font("Arial", fontSize, FontStyle.Bold, GraphicsUnit.Pixel);
          var graphics = Graphics.FromImage(bmp);

          graphics.Clear((Color)new ColorConverter().ConvertFromString("#" + bgColour));
          graphics.SmoothingMode = SmoothingMode.AntiAlias;
          graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
          graphics.DrawString(avatarString, font, new SolidBrush(Color.WhiteSmoke), new RectangleF(0, 0, dimesions, dimesions), sf);
          graphics.Flush();

          return bmp;
      }
  }
}