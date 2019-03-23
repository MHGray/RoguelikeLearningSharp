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
        private int _hp = 3;

        public override string Name { get; set; }

        public override int HP 
        {
            get 
            {
                return _hp;
            }
            set 
            {
                _hp = value;
                CheckIfDead();
            }
        }

        private void CheckIfDead()
        {
            if(HP <= 0)
            {
                Game.stage.Remove(this);
                Game.map.DrawTile(Pos.X, Pos.Y);
            }
        }

        public Enemy()
        {
            Name = "Problem Naming";
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

        public override void Draw()
        {
            Game.map.Artist.DrawSymbol(Symbol, Pos.X, Pos.Y, Color);
        }

        public override void Update()
        {

        }
    }
    
    class Goblin : Enemy
    {
        public Goblin()
        {
            Name = "Goblin";
            Color = Color.goblin;
            Symbol = 'g';
        }

        private static int PathfindingHeuristic(Point p, Point end)
        {
            return Math.Abs(p.X - end.X) + Math.Abs(p.Y - end.Y);
        }

        public Point MoveTowardsPlayer()
        {
            Point start = GetPos();
            Point end = Game.stage.GetPlayer().GetPos();

            List<Point> path = Game.aStar.Find(start, end, PathfindingHeuristic, Game.map.GetAdjPoints);

            Point nextPos = new Point();

            if(path.Count > 1)
            {
                nextPos.X = path[1].X;
                nextPos.Y = path[1].Y;
            }
            
            return nextPos;
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
            Point attemptedMove = MoveTowardsPlayer();

            if (Game.stage.IsPosOccupied(attemptedMove))
            {
                if(Game.stage.GetPlayer().Pos.X == attemptedMove.X && Game.stage.GetPlayer().Pos.Y == attemptedMove.Y)
                {
                    Fight(Game.stage.GetPlayer());
                }
                return;
            }
            else
            {
               Pos.X = attemptedMove.X;
               Pos.Y = attemptedMove.Y;
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
