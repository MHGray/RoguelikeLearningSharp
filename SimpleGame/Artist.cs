using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleGame
{
	class Artist
	{
		private readonly int _width;
		private readonly int _height;
        private readonly int _widthOff = 0;
        private readonly int _heightOff = 0;
		private List<Pixel> _pixels = new List<Pixel>();

		#region Constructor		
		/// <summary>
		/// Constructs an artist with console width and height
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public Artist(int width, int height) {
			Console.CursorVisible = false;
			_width = width;
			_height = height;

			for (int j = 0; j < _height; j++)
			{
				for (int i = 0; i < _width; i++)
				{
					_pixels.Add(new Pixel(i, j, ' ',Color.normal));
				}
			}
		}

        public Artist(int width, int height, int widthOff, int heightOff)
        {
            Console.CursorVisible = false;
            _width = width;
            _height = height;
            _widthOff = widthOff;
            _heightOff = heightOff;

            for (int j = 0; j < _height; j++)
            {
                for (int i = 0; i < _width; i++)
                {
                    _pixels.Add(new Pixel(i, j, ' ', Color.normal));
                }
            }
        }
        #endregion

        /// <summary>
        /// Completely clears the screen. Inefficient and causes screen flickering
        /// if it is done a lot.
        /// </summary>
        public void Clear()
		{
			Console.SetCursorPosition(0, 0);
			//Console.WindowWidth = Game.WIDTH;
			//Console.WindowHeight = Game.HEIGHT+2;

			string complete = "";

			for (int j = 0; j < _height; j++)
			{
				for (int i = 0; i < _width; i++)
				{
					complete += ' ';
				}
                complete += '\n';
			}

			Console.Write(complete);
			Console.SetWindowPosition(0, 0);
		}


		public void DrawRectFill(char symbol, int x, int y, int width, int height, Color color)
		{
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					DrawSymbol(symbol, i+x, j+y, color);
				}
			}
		}

		public void DrawRect(char symbol, int x, int y, int width, int height, Color color)
		{
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					if(i == 0 || j ==0 || i==width-1 || j == height - 1)
					{
						DrawSymbol(symbol, i + x, j + y, color);
					}
				}
			}
		}

		public void DrawSymbol(char symbol, int x, int y,Color color)
		{
			Pixel pix = _pixels.Find(p => p.X == x && p.Y == y);
			pix.Symbol = symbol;
			pix.Color = color;
		}

		private void DrawPixel(Pixel pixel)
		{
			SetColor(pixel.Color);
            Console.SetCursorPosition(pixel.X + _widthOff, pixel.Y + _heightOff);
			Console.Write(pixel.Symbol);
		}

		public void Draw()
		{
			Console.CursorVisible = false;
			foreach(Pixel pixel in _pixels.Where(p => p.HasChanged))
			{
				DrawPixel(pixel);
			}
			foreach(Pixel pixel in _pixels.Where(p => p.HasChanged))
			{
				pixel.HasChanged = false;
			}
			Console.SetCursorPosition(0, _height + 1);
			Console.SetWindowPosition(0, 0);
		}

		private void SetColor(Color color) {
			Console.ForegroundColor = color.Front;
			Console.BackgroundColor = color.Back;
		}

        public void WriteLine(string str, int x, int y)
        {
            for (int i = 0; i < str.Length; i++)
            {
                DrawSymbol(str[i], 0 + i, y, Color.normal);
            }
        }

		private class Pixel
		{
			private int _x;
			private int _y;
			private Color _color;
			private bool _hasChanged = false;
			private char _symbol;

			public bool HasChanged { get => _hasChanged; set => _hasChanged = value; }
			public int Y { get => _y; set => _y = value; }
			public int X { get => _x; set => _x = value; }
			internal Color Color { get => _color; set { _color = value; HasChanged = true; } }
			public char Symbol { get => _symbol; set { _symbol = value; HasChanged = true; } }

			public Pixel(int x,int y, char symbol, Color color){
				X = x;
				Y = y;
				Color = color;
				Symbol = symbol;
			}

			public void Set(int x, int y, Color color)
			{
				X = x;
				Y = y;
				Color = color;
				HasChanged = true;
			}
		}
	}

	
}