using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using static SimpleGame.AStar;

namespace SimpleGame
{
    class Enemy : Actor
    {
        public Enemy()
        {
            Point pos = Game.map.GetWalkableTilePos();
            Pos.X = pos.X;
            Pos.Y = pos.Y;
            Color = Color.normal;
            Symbol = '?';
        }
        
        public Enemy(int x, int y)
        {
            Pos.X = x;
            Pos.Y = y;
            Color = Color.normal;
            Symbol = '?';
        }

        public Point GetPos()
        {
            return Pos;
        }

        public virtual void Draw()
        {
            Game.map.Artist.DrawSymbol(Symbol, Pos.X, Pos.Y, Color);
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
                    return Math.Abs(p.X - end.X) +
                           Math.Abs(p.Y - end.Y);               
                },
                //getNeighbors
                (Point p) =>
                {                    
                    return Game.map.GetAdjPoints(p);
                });

            if(path.Count > 1)
            {
                Pos.X = path[1].X;
                Pos.Y = path[1].Y;
            }
            

            return;
        }

        public void RandomMove()
        {
            switch ((Dir)Game.rng.Next(1, 10))
            {
                case Dir.u:
                    Pos.Y--;
                    break;
                case Dir.ul:
                    Pos.Y--;
                    Pos.X--;
                    break;
                case Dir.ur:
                    Pos.X++;
                    Pos.Y--;
                    break;
                case Dir.l:
                    Pos.X--;
                    break;
                case Dir.r:
                    Pos.X++;
                    break;
                case Dir.d:
                    Pos.Y++;
                    break;
                case Dir.dl:
                    Pos.Y++;
                    Pos.X--;
                    break;
                case Dir.dr:
                    Pos.Y++;
                    Pos.X++;
                    break;
                case Dir.w:
                    break;
                default:
                    break;
            }
        }

        public override void Update()
        {
            //Clear self on Map
            Game.map.DrawTile(Pos.X, Pos.Y);

            //attempt move
            Point curPos = new Point(Pos.X, Pos.Y);



            MoveTowardsPlayer();

            if(!Game.map.IsTileWalkable(Pos.X, Pos.Y))
            {
                Pos.X = curPos.X;
                Pos.Y = curPos.Y;
            }
        }

        public override void Draw()
        {
            Game.map.Artist.DrawSymbol(Symbol, Pos.X, Pos.Y, Color);
        }
    }

    enum EnemyType
    {
        Goblin,
        Turret,
        Human
    }
}
