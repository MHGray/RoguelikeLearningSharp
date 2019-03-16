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
                    Game.artist.Clear();
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

        public Point GetPos()
        {
            return new Point(_x, _y);
        }

		public void Move(string dir)
		{
			//Clear current position in artist
			Game.map.DrawTile(_x, _y);

            int moveX = _x;
            int moveY = _y;

			switch (dir)
			{
				case "u":
					moveY--;
					break;
				case "d":
					moveY++;
					break;
				case "l":
					moveX--;
					break;
				case "r":
					moveX++;
					break;
				case "ul":
					moveY--;
					moveX--;
					break;
				case "ur":
					moveY--;
					moveX++;
					break;
				case "dl":
					moveY++;
					moveX--;
					break;
				case "dr":
					moveY++;
					moveX++;
					break;
				case "wait":
					break;
				default:
					break;
			}

            //Check against edge of screen
			if(moveX < 0)
			{
				moveX = 0;
			}else if(moveX > Game.WIDTH - 1)
			{
				moveX = Game.WIDTH - 1;
			}
			if (moveY < 0)
			{
				moveY = 0;
			}
			else if (moveY > Game.HEIGHT - 1)
			{
				moveY = Game.HEIGHT - 1;
			}

            //Check to make sure position is walkable
            if (Game.map.IsTileWalkable(moveX, moveY))
            {
                _x = moveX;
                _y = moveY;
            }
		}

        public void SetPos(Point pos)
        {
            _x = pos.X;
            _y = pos.Y;
        }

		public void Draw()
		{
			Game.artist.DrawSymbol(_symbol, _x, _y, _color);
		}
	}
}
