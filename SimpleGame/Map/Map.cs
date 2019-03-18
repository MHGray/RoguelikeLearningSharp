using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleGame
{
    class Map
    {
        private List<Tile> _tiles = new List<Tile>();
        private List<Room> _rooms = new List<Room>();
        private List<Hallway> _hallways = new List<Hallway>();
        public int Width { get; set; }
        public int Height { get; set; }
        public Artist Artist;

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            Artist = new Artist(Width,Height);
            for (int j = 0; j < Height; j++)
            {
                for (int i = 0; i < Width; i++)
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
            Tile tile = _tiles.Find(t => t.Pos.X == x && t.Pos.Y == y);
            tile.HasChanged = true;
        }

        public void SetTile(int x, int y, char symbol, Color color, bool walkable, bool explored)
        {
            Tile tile = _tiles.Find(t => t.Pos.X == x && t.Pos.Y == y);
            if (tile == null)
            {
                return;
            }
            tile.Symbol = symbol;
            tile.Color = color;
            tile.IsWalkable = walkable;
            tile.IsExplored = explored;
        }

        public void SetTile(int x, int y, TileType type)
        {
            Tile tile = _tiles.Find(t => t.Pos.X == x && t.Pos.Y == y);
            if (tile == null)
            {
                return;
            }
            switch (type)
            {
                case TileType.Blank:
                    tile.Symbol = ' ';
                    tile.Color = Color.normal;
                    tile.IsWalkable = false;
                    break;
                case TileType.Wall:
                    tile.Symbol = '#';
                    tile.Color = Color.wall;
                    tile.IsWalkable = false;
                    break;
                case TileType.Floor:
                    tile.Symbol = '∙';
                    tile.Color = Color.floor;
                    tile.IsWalkable = true;
                    break;
                default:
                    break;
            }

        }

        public bool IsTileWalkable(int x, int y)
        {
            if (GetTile(x,y).IsWalkable)
            {
                return true;
            }
            return false;
        }

        public bool IsTileWalkable(Point p)
        {
            if (GetTile(p.X, p.Y).IsWalkable)
            {
                return true;
            }
            return false;
        }

        public Point GetWalkableTilePos()
        {
            List<Tile> tiles = _tiles.FindAll(t => t.IsWalkable);
            Tile selectedTile = tiles[Game.rng.Next(tiles.Count)];
            Point point = new Point(selectedTile.Pos.X, selectedTile.Pos.Y);
            return point;

        }

        Tile GetTile(Point p)
        {
            return _tiles.Find(t => t.Pos.X == p.X && t.Pos.Y == p.Y);
        }

        Tile GetTile(int x, int y)
        {
            return _tiles.Find(t => t.Pos.X == x && t.Pos.Y == y);
        }

        List<Tile> GetAdjTiles(int x, int y)
        {
            List<Tile> tiles = new List<Tile>();
            //Getting eight surrounding tiles
            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    Tile tileToCheck = GetTile(x + i, y + j);
                    if(tileToCheck != null)
                    {
                        tiles.Add(tileToCheck);
                    }
                }
            }
            return tiles;
        }

        public List<Point> GetAdjPoints(Point p)
        {
            List<Point> points = new List<Point>();

            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    if (i == 0 && j == 0)
                    {
                        continue;
                    }
                    Tile tileToCheck = GetTile(p.X + i, p.Y + j);
                    if (tileToCheck != null)
                    {
                        if(tileToCheck.IsWalkable)
                            points.Add(tileToCheck.Pos);
                    }
                }
            }

            return points;
        }

        public void Test()
        {
            CreateRooms();
            foreach (Room room in _rooms)
            {
                room.Set();
            }

            for (int i = 0; i < _rooms.Count - 1; i++)
            {
                int startX = Game.rng.Next(_rooms[i].X + 1, _rooms[i].X + _rooms[i].Width-1);
                int startY = Game.rng.Next(_rooms[i].Y + 1, _rooms[i].Y + _rooms[i].Height-1);
                int endX = Game.rng.Next(_rooms[i + 1].X + 1, _rooms[i + 1].X + _rooms[i + 1].Width-1);
                int endY = Game.rng.Next(_rooms[i + 1].Y + 1, _rooms[i + 1].Y + _rooms[i + 1].Height-1);
                Point start = new Point(startX, startY);
                Point end = new Point(endX, endY);
                _hallways.Add(new Hallway(start, end));
            }

        }

        /// <summary>
        /// Builds stand alone rooms of which none will overlap.
        /// </summary>
		public void CreateRooms()
        {
            for (int i = 0; i < 10; i++)
            {
                int counter = 0;
                //Try 10 times to make a room that fits
                while (counter < 10)
                {
                    int x = Game.rng.Next(Width);
                    int y = Game.rng.Next(Height);
                    int width = Game.rng.Next(10) + 4;
                    int height = Game.rng.Next(9) + 4;

                    //Nudge room back on to the screen 
                    //if it is too far over
                    if (x + width > Width)
                    {
                        x = Width - width - 1;
                    }
                    if (y + height > Height)
                    {
                        y = Height - height - 1;
                    }

                    //Create Test Room
                    Room room = new Room(x, y, width, height);

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

        /// <summary>
        /// Check if room overlaps with another room
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
		private bool IsRoomOverlapping(Room room)
        {

            if (Game.map._rooms.Count > 0)
            {
                foreach (Room real in _rooms)
                {
                    if (room.X <= real.X + real.Width &&
                        room.X + room.Width >= real.X &&
                        room.Y <= real.Y + real.Height &&
                        room.Y + room.Height >= real.Y)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Create a room and set the map tiles to be the correct things
        /// </summary>
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
                        if (j == 0 || i == 0 || j == Height - 1 || i == Width - 1)
                        {
                            Game.map.SetTile(i + X, j + Y, TileType.Wall);
                        }
                        else
                        {
                            Game.map.SetTile(i + X, j + Y, TileType.Floor);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// Connects two points and stores the start and end point
        /// </summary>
		class Hallway
        {
            private readonly int _xStart;
            private readonly int _yStart;
            private readonly int _xEnd;
            private readonly int _yEnd;
            private bool _vertFirst { get; set; }

            /// <summary>
            /// construct with points
            /// </summary>
            /// <param name="start">Point</param>
            /// <param name="end">Point</param>
			public Hallway(Point start, Point end)
            {
                _xStart = start.X;
                _yStart = start.Y;
                _xEnd = end.X;
                _yEnd = end.Y;
                Init();
            }

            /// <summary>
            /// Construct with 2 length array
            /// </summary>
            /// <param name="start">2 length int array</param>
            /// <param name="end">2 length int array</param>
            public Hallway(int[] start, int[] end)
            {
                _xStart = start[0];
                _yStart = start[1];
                _xEnd = end[0];
                _yEnd = end[1];
                Init();
            }


            private void Init()
            {
                _vertFirst = true;

                //Creates the Hallway
                if (_vertFirst)
                {
                    Tile tileToCheck;
                    //go up or down
                    if (_yEnd - _yStart >= 0)
                    {
                        //Go Up
                        for (int j = 0; j <= Math.Abs(_yEnd - _yStart); j++)
                        {
                            //Set Tile to floor
                            Game.map.SetTile(_xStart, _yStart + j, TileType.Floor);

                            //Set Neighbors to walls
                            tileToCheck = Game.map.GetTile(_xStart - 1, _yStart + j);
                            if (tileToCheck != null && tileToCheck.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart - 1, _yStart + j, TileType.Wall);
                            }
                            tileToCheck = Game.map.GetTile(_xStart + 1, _yStart + j);
                            if (tileToCheck != null && tileToCheck.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart + 1, _yStart + j, TileType.Wall);
                            }
                        }
                    }
                    else
                    {
                        //Go Down
                        for (int j = 0; j <= Math.Abs(_yEnd - _yStart); j++)
                        {
                            Game.map.SetTile(_xStart, _yStart - j, TileType.Floor);

                            //Set Neighbors to walls
                            tileToCheck = Game.map.GetTile(_xStart - 1, _yStart - j);
                            if (tileToCheck != null && tileToCheck.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart - 1, _yStart - j, TileType.Wall);
                            }
                            tileToCheck = Game.map.GetTile(_xStart + 1, _yStart - j);
                            if (tileToCheck != null && tileToCheck.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart + 1, _yStart - j, TileType.Wall);
                            }
                        }
                    }

                    //Set Corners of the hallway
                    //Get Adjacent tiles
                    List<Tile> adjTiles = Game.map.GetAdjTiles(_xStart, _yEnd);
                    adjTiles.ForEach(t => {
                        if(t.Symbol == ' ')
                        {
                            Game.map.SetTile(t.Pos.X, t.Pos.Y, TileType.Wall);
                        }
                    });

                    //go left or right
                    if (_xEnd - _xStart >= 0)
                    {
                        //go Right
                        for (int i = 0; i <= Math.Abs(_xEnd - _xStart); i++)
                        {
                            //Set tile to floor
                            Game.map.SetTile(_xStart + i, _yEnd, TileType.Floor);

                            //Set Neighbors to walls
                            tileToCheck = Game.map.GetTile(_xStart + i, _yEnd + 1);
                            if (tileToCheck != null && tileToCheck.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart + i, _yEnd + 1, TileType.Wall);
                            }
                            tileToCheck = Game.map.GetTile(_xStart + i, _yEnd - 1);
                            if (tileToCheck != null && tileToCheck.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart + i, _yEnd - 1, TileType.Wall);
                            }
                        }
                    }
                    else
                    {
                        //go Left
                        for (int i = 0; i <= Math.Abs(_xEnd - _xStart); i++)
                        {
                            //Set Tile to floor
                            Game.map.SetTile(_xStart - i, _yEnd, TileType.Floor);

                            //Set Neighbors to wall
                            Tile tileLeft = Game.map.GetTile(_xStart - i, _yEnd + 1);
                            if (tileLeft != null && tileLeft.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart - i, _yEnd + 1, TileType.Wall);
                            }
                            Tile tileRight = Game.map.GetTile(_xStart - i, _yEnd - 1);
                            if (tileRight != null && tileRight.Symbol == ' ')
                            {
                                Game.map.SetTile(_xStart - i, _yEnd - 1, TileType.Wall);
                            }

                        }
                    }
                }
                else
                {
                    //TODO: Start horizontally
                }
            }
        }
        
    }

}
