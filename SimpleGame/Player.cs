using System;

namespace SimpleGame
{
	public class Player
	{
		char _symbol = '@';
		Color _color = new Color(ConsoleColor.Yellow, ConsoleColor.Black);
		private int _x = 0;
		private int _y = 0;

		public void Update(ConsoleKeyInfo input)
		{
			switch (input.Key)
			{
				case ConsoleKey.NumPad0:
					break;
				case ConsoleKey.NumPad1:
					Move("dl");
					break;
				case ConsoleKey.NumPad2:
					Move("d");
					break;
				case ConsoleKey.NumPad3:
					Move("dr");
					break;
				case ConsoleKey.NumPad4:
					Move("l");
					break;
				case ConsoleKey.NumPad5:
					Move("wait");
					break;
				case ConsoleKey.NumPad6:
					Move("r");
					break;
				case ConsoleKey.NumPad7:
					Move("ul");
					break;
				case ConsoleKey.NumPad8:
					Move("u");
					break;
				case ConsoleKey.NumPad9:
					Move("ur");
					break;
				case ConsoleKey.Tab:
					break;
				case ConsoleKey.Enter:
					break;
				case ConsoleKey.Escape:
					break;
				case ConsoleKey.Spacebar:
					break;
				case ConsoleKey.LeftArrow:
					break;
				case ConsoleKey.UpArrow:
					break;
				case ConsoleKey.RightArrow:
					break;
				case ConsoleKey.DownArrow:
					break;
				case ConsoleKey.A:
					for(int i = 0; i < 10; i++)
					{
						int x = Game.rng.Next(Game.WIDTH);
						int y = Game.rng.Next(Game.HEIGHT);
						int width = Game.rng.Next(20) + 2;
						int height = Game.rng.Next(10) + 2;
						if (x + width > Game.WIDTH)
						{
							x = Game.WIDTH - width - 1;
						}
						if (y + height > Game.HEIGHT)
						{
							y = Game.HEIGHT - height - 1;
						}

						Game.artist.DrawRect('#', x, y, width, height, Color.normal);
					}
					break;
				case ConsoleKey.B:
					break;
				case ConsoleKey.C:
					break;
				case ConsoleKey.D:
					break;
				case ConsoleKey.E:
					break;
				case ConsoleKey.F:
					break;
				case ConsoleKey.G:
					break;
				case ConsoleKey.H:
					break;
				case ConsoleKey.I:
					break;
				case ConsoleKey.J:
					break;
				case ConsoleKey.K:
					break;
				case ConsoleKey.L:
					break;
				case ConsoleKey.M:
					break;
				case ConsoleKey.N:
					break;
				case ConsoleKey.O:
					break;
				case ConsoleKey.P:
					break;
				case ConsoleKey.Q:
					break;
				case ConsoleKey.R:
					break;
				case ConsoleKey.S:
					break;
				case ConsoleKey.T:
					break;
				case ConsoleKey.U:
					break;
				case ConsoleKey.V:
					break;
				case ConsoleKey.W:
					break;
				case ConsoleKey.X:
					break;
				case ConsoleKey.Y:
					break;
				case ConsoleKey.Z:
					break;
				case ConsoleKey.Multiply:
					break;
				case ConsoleKey.Add:
					break;
				case ConsoleKey.Subtract:
					break;
				case ConsoleKey.Decimal:
					break;
				case ConsoleKey.Divide:
					break;
				default:
					break;
			}
		}

		public void Move(string dir)
		{
			//Clear current position in artist
			Game.map.DrawTile(_x, _y);

			switch (dir)
			{
				case "u":
					_y--;
					break;
				case "d":
					_y++;
					break;
				case "l":
					_x--;
					break;
				case "r":
					_x++;
					break;
				case "ul":
					_y--;
					_x--;
					break;
				case "ur":
					_y--;
					_x++;
					break;
				case "dl":
					_y++;
					_x--;
					break;
				case "dr":
					_y++;
					_x++;
					break;
				case "wait":
					break;
				default:
					break;
			}
			if(_x < 0)
			{
				_x = 0;
			}else if(_x > Game.WIDTH - 1)
			{
				_x = Game.WIDTH - 1;
			}
			if (_y < 0)
			{
				_y = 0;
			}
			else if (_y > Game.HEIGHT - 1)
			{
				_y = Game.HEIGHT - 1;
			}
		}

		public void Draw()
		{
			Game.artist.DrawSymbol(_symbol, _x, _y, _color);
		}
	}
}
