using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static SimpleGame.AStar;

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

        public Point GetPos()
        {
            return new Point(X, Y);
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

        public void MoveTowardsPlayer()
        {
            Point start = this.GetPos();
            Point end = Game.player.GetPos();



            List<Point> path = Game.aStar.Find(start, end,
                //h
                (Point p) =>
                {
                    if (Game.map.IsTileWalkable(p))
                    {
                        return Math.Abs(p.X - end.X) +
                           Math.Abs(p.Y - end.Y);
                    }
                    else
                    {
                        return 999999;
                    }                    
                },
                //getNeighbors
                (Point p) =>
                {
                    List<APoint> points = new List<APoint>();
                    Game.map.GetAdjTiles(p).ForEach(point => points.Add(new APoint (point)));
                    return points;
                });

            if(path.Count > 1)
            {
                X = path[1].X;
                Y = path[1].Y;
            }
            

            return;
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

            MoveTowardsPlayer();

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
