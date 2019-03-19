using System;

namespace SimpleGame
{
	public class Player : Actor
	{
        private int _hp = 0;
        private int _hpMax = 0;
        private string _str = "1d6";
        private string _con = "1d2";

        public new int HP {
            get { return _hp; }
            set {
                _hp = value;
                Game.stats.Draw();
            }
        }
        public new int HPMax {
            get { return _hpMax; }
            set {
                _hpMax = value;
                Game.stats.Draw();
            }
        }
        public new string Con {
            get { return _con; }
            set {
                _con = value;
                Game.stats.Draw();
            }
        }
        public new string Str {
            get { return _str; }
            set {
                _str = value;
                Game.stats.Draw();
            }
        }

        public Player()
        {
            Symbol = '@';
            Color = Color.player;
            _hp = 8;
            _hpMax = 8;
        }

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
                    HP--;
					break;
				case ConsoleKey.Enter:
                    Game.dice.Roll("1d6");
					break;
				case ConsoleKey.Escape:
					break;
				case ConsoleKey.Spacebar:

                    int rand = Game.rng.Next(0,4);

                    switch (rand)
                    {
                        case 0:
                            Game.messages.Write("I am writing messages woo and now I'm writing reaaaaaaaaaaaaally long messagessa tooo. My goodness how long they are");
                            break;
                        case 1:
                            Game.messages.Write("here is a short line");
                            break;
                        case 2:
                            Game.messages.Write("here is a diferent line");
                            break;
                        case 3:
                            Game.messages.Write(Str);
                            break;
                        default:
                            break;
                    }
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
						int x = Game.rng.Next(Game.map.Width);
						int y = Game.rng.Next(Game.map.Height);
						int width = Game.rng.Next(20) + 2;
						int height = Game.rng.Next(10) + 2;
						if (x + width > Game.map.Width)
						{
							x = Game.map.Width - width - 1;
						}
						if (y + height > Game.map.Height)
						{
							y = Game.map.Height- height - 1;
						}

						Game.map.Artist.DrawRect('#', x, y, width, height, Color.normal);
					}
					break;
				case ConsoleKey.B:
					break;
				case ConsoleKey.C:
                    Game.map.Artist.Clear();
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
            return new Point(Pos.X, Pos.Y);
        }

		public void Move(string dir)
		{
			//Clear current position in artist
			Game.map.DrawTile(Pos.X, Pos.Y);

            int moveX = Pos.X;
            int moveY = Pos.Y;

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
			}else if(moveX > Game.map.Width - 1)
			{
				moveX = Game.map.Width - 1;
			}
			if (moveY < 0)
			{
				moveY = 0;
			}
			else if (moveY > Game.map.Height - 1)
			{
				moveY = Game.map.Height - 1;
			}

            //Check to make sure position is walkable
            if (Game.map.IsTileWalkable(moveX, moveY))
            {
                Pos.X = moveX;
                Pos.Y = moveY;
            }
		}

        public void SetPos(Point pos)
        {
            Pos.X = pos.X;
            Pos.Y = pos.Y;
        }

		public void Draw()
		{
			Game.map.Artist.DrawSymbol(Symbol, Pos.X, Pos.Y, Color);
		}
	}
}
