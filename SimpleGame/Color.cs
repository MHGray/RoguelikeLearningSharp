using System;

namespace SimpleGame
{
	class Color
	{
		public static Color normal = new Color(ConsoleColor.White,ConsoleColor.Black);
		public static Color player = new Color(ConsoleColor.Yellow, ConsoleColor.Black);
		public static Color wall = new Color(ConsoleColor.Gray, ConsoleColor.Black);
		public static Color floor = new Color(ConsoleColor.DarkGray, ConsoleColor.Black);
        public static Color goblin = new Color(ConsoleColor.Green, ConsoleColor.Black);
        public static Color turret = new Color(ConsoleColor.Yellow, ConsoleColor.Black);
        public static Color human = new Color(ConsoleColor.White, ConsoleColor.DarkYellow);

		private ConsoleColor _color { get; set; }
		private ConsoleColor _bgColor { get; set; }


		public ConsoleColor Front {
			get { return _color; }
			set { _color = value; }
		}
		public ConsoleColor Back {
			get { return _bgColor; }
			set { _bgColor = value; }
		}

		public Color(ConsoleColor color, ConsoleColor bgColor)
		{
			_color = color;
			_bgColor = bgColor;
		}
	}

}
