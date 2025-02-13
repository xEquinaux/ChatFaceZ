using System;
using System.Collections.Generic;

namespace ChatFaceZ
{
	public class ScrollLogic
	{
		static int visibleAreaHeight = 8; // Height of the visible window in lines
		static int lineHeight = 45; // Height of a single text line
		static int scrollPosition = 0;

		public static int yCoord(int count)
		{
			int totalTextHeight = count * lineHeight;
			if (totalTextHeight > visibleAreaHeight * lineHeight)
			{
				return scrollPosition = totalTextHeight - (visibleAreaHeight * lineHeight);
			}
			else
			{
				return scrollPosition = 0;
			}
		}
	}
}