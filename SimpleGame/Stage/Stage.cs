using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame
{
    class Stage
    {
        List<Actor> Actors = new List<Actor>();

        public Actor GetActor(Point pos)
        {
            return Actors.Find(a => a.Pos.X == pos.X && a.Pos.Y == pos.Y);
        }

        public bool IsPosOccupied(Point pos)
        {
            if (Actors.Exists(a =>
            {
                return a.Pos.X == pos.X && a.Pos.Y == pos.Y;
            })) 
            {
                return true;
            }
            return false;
        }

        public Player GetPlayer()
        {
            return (Player)Actors.Find(p => {
                if(p is Player)
                {
                    return true;
                }
                return false;
            });
        }

        public void Add(Actor actor)
        {
            Actors.Add(actor);
        }

        public void Remove(Actor actor)
        {
            Actors.Remove(actor);
        }

        public void Draw()
        {
            Actors.ForEach(a => a.Draw());
        }

        public void Update()
        {
            Actors.ForEach(a => a.Update());
        }
    }
}
