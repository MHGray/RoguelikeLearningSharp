using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SimpleGame
{
    class Enemy
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }
        public char Symbol { get; set; }

        public Enemy()
        {
            Point pos = Game.map.GetWalkableTilePos();
            X = pos.X;
            Y = pos.Y;
            Color = Color.normal;
            Symbol = '?';
        }
        
        public Enemy(int x, int y)
        {
            X = x;
            Y = y;
            Color = Color.normal;
            Symbol = '?';
        }

        public virtual void Draw()
        {
            Game.artist.DrawSymbol(Symbol, X, Y, Color);
        }

        public virtual void Update()
        {

        }
    }
    
    class Goblin : Enemy
    {
        public Goblin(int x, int y): base(x, y)
        {
            Color = Color.goblin;
            Symbol = 'g';
        }

        public Goblin(): base()
        {
            Color = Color.goblin;
            Symbol = 'g';
        }

        public override void Update()
        {
            //Clear self on Map
            Game.map.DrawTile(X, Y);

            //attempt move
            Point curPos = new Point(X, Y);

            switch ((Dir)Game.rng.Next(1, 10))
            {
                case Dir.u:
                    Y--;
                    break;
                case Dir.ul:
                    Y--;
                    X--;
                    break;
                case Dir.ur:
                    X++;
                    Y--;
                    break;
                case Dir.l:
                    X--;
                    break;
                case Dir.r:
                    X++;
                    break;
                case Dir.d:
                    Y++;
                    break;
                case Dir.dl:
                    Y++;
                    X--;
                    break;
                case Dir.dr:
                    Y++;
                    X++;
                    break;
                case Dir.w:
                    break;
                default:
                    break;
            }

            if(!Game.map.IsTileWalkable(X, Y))
            {
                X = curPos.X;
                Y = curPos.Y;
            }
        }

        public override void Draw()
        {
            Game.artist.DrawSymbol(Symbol, X, Y, Color);
        }
    }

    enum EnemyType
    {
        Goblin,
        Turret,
        Human
    }
}
