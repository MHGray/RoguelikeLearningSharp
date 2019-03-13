using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace SimpleGame
{
	class Map
	{
		private List<Tile> _tiles = new List<Tile>();
		private List<Room> _rooms = new List<Room>();
		private int Width { get; set; }
		private int Height { get; set; }

		public Map(int width, int height)
		{
			Width = width;
			Height = height;
			for (int j = 0; j < height; j++)
			{
				for (int i = 0; i < width; i++)
				{
					_tiles.Add(new Tile(i, j));
				}
			}
		}

		public void Draw()
		{
			foreach (Tile tile in _tiles.Where(t => t.HasChanged))
			{
				tile.Draw();
			}
		}

		public void DrawTile(int x, int y)
		{
			Tile tile = _tiles.Find(t => t.X == x && t.Y == y);
			tile.HasChanged = true;
		}

		public void SetTile(int x, int y, char symbol, Color color, bool walkable, bool explored)
		{
			Tile tile = _tiles.Find(t => t.X == x && t.Y == y);
			tile.Symbol = symbol;
			tile.Color = color;
			tile.IsWalkable = walkable;
			tile.IsExplored = explored;
		}

		public void Test()
		{
					CreateRooms();
					foreach(Room room in _rooms)
					{
						room.Set();
					}
		}

		public void CreateRooms()
		{
			for (int i = 0; i < 10; i++)
			{
				int counter = 0;
				//Try 10 times to make a room that fits
				while(counter < 10)
				{
					int x = Game.rng.Next(Game.WIDTH);
					int y = Game.rng.Next(Game.HEIGHT);
					int width = Game.rng.Next(10) + 4;
					int height = Game.rng.Next(9) + 4;

					//Nudge room back on to the screen 
					//if it is too far over
					if (x + width > Game.WIDTH)
					{
						x = Game.WIDTH - width - 1;
					}
					if(y+ height> Game.HEIGHT)
					{
						y = Game.HEIGHT - height - 1;
					}

					//Create Test Room
					Room room = new Room(x,y,width,height);

					//If it isn't overlapping then it is good to go!
					if (!IsRoomOverlapping(room))
					{
						_rooms.Add(room);
						break;
					}
					counter++;
				}
				
			}
		}

		private bool IsRoomOverlapping(Room room)
		{

			if (Game.map._rooms.Count > 0)
			{
				foreach(Room real in _rooms)
				{
					if(room.X <= real.X + real.Width &&
						room.X + room.Width >= real.X &&
						room.Y <= real.Y + real.Height &&
						room.Y + room.Height >= real.Y )
					{
						return true;
					}
				}
			}
			return false;
		}

		class Room
		{
			public int X;
			public int Y;
			public int Width;
			public int Height;

			public Room(int x, int y, int width, int height)
			{
				X = x;
				Y = y;
				Width = width;
				Height = height;
				//Set();
			}

			public void Set()
			{
				for (int j = 0; j < Height; j++)
				{
					for (int i = 0; i < Width; i++)
					{
						if(j==0 || i == 0 || j==Height-1 || i == Width - 1)
						{
							Game.map.SetTile(i + X, j + Y, '#', Color.wall, false, false);
						}
						else
						{
							Game.map.SetTile(i + X, j + Y, '.', Color.floor, true, false);
						}
					}
				}
			}

		}

		class Hallway
		{
			private static int _num;
			private int _x;
			private int _y;
			

		}

		class Tile
		{
			private char _symbol;
			private Color _color;

			public int X { get; set; }
			public int Y { get; set; }
			public char Symbol {
				get => _symbol;
				set {
					_symbol = value;
					HasChanged = true;
				}
			}
			internal Color Color {
				get => _color;
				set {
					_color = value;
					HasChanged = true;
				} 
			}
			internal bool IsWalkable { get; set; }
			internal bool IsExplored { get; set; }
			internal bool HasChanged { get; set; }

			public Tile(int x, int y)
			{
				X = x;
				Y = y;
				Symbol = ' ';
				Color = Color.normal;
				IsWalkable = false;
				IsExplored = false;
				HasChanged = true;
			}

			public void Draw()
			{
				Game.artist.DrawSymbol(Symbol, X, Y, Color);
				HasChanged = false;
			}
		}

		class Marsh : Tile
		{
			
			

			public Marsh(int x, int y) : base(x,y)
			{
				Symbol = ' ';
				Color = Color.normal;
				IsWalkable = false;
				IsExplored = false;
				HasChanged = true;
			}
		}
	}
}
