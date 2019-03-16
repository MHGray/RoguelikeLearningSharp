namespace SimpleGame
{
    class Tile
    {
        private char _symbol;
        private Color _color;

        public int x;
        public int y;
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
            this.x = x;
            this.y = y;
            Symbol = ' ';
            Color = Color.normal;
            IsWalkable = false;
            IsExplored = false;
            HasChanged = true;
        }

        public Tile(int x, int y, TileType type)
        {
            this.x = x;
            this.y = y;
            switch (type)
            {
                case TileType.Blank:
                    Symbol = ' ';
                    Color = Color.normal;
                    IsWalkable = false;
                    break;
                case TileType.Wall:
                    Symbol = '#';
                    Color = Color.wall;
                    IsWalkable = false;
                    break;
                case TileType.Floor:
                    Symbol = '∙';
                    Color = Color.floor;
                    IsWalkable = true;
                    break;
                default:
                    break;
            }

            IsExplored = false;
            HasChanged = true;
        }

        public void Draw()
        {
            Game.artist.DrawSymbol(Symbol, x, y, Color);
            HasChanged = false;
        }
    }

    public enum TileType
    {
        Blank, Wall, Floor
    }
}
