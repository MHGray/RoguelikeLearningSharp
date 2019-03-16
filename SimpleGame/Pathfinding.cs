using System;
using System.Collections.Generic;

namespace SimpleGame
{
    class Pathfinding
    {

    }

    class AStar{

        public List<Point> Find(Point pStart, Point pEnd, Func<Point,int> h, Func<Point, List<APoint>> getNeighbors)
        {
            //List<Point> finalPath = new List<Point>();

            APoint start = new APoint(pStart);
            start.H = h(start);
            start.G = start.H +start.G;
            APoint end = new APoint(pEnd);

            List<APoint> openList = new List<APoint>();
            List<APoint> closedList = new List<APoint>();

            openList.Add(start);

            while (openList.Count > 0)
            {
                int next = 0;
                for (int i = 0; i < openList.Count; i++)
                {
                    if(openList[i].F < openList[next].F)
                    {
                        next = i;
                    }
                }

                var curPos = openList[next];

                //check if at end
                if((curPos.X == end.X && curPos.Y == end.Y) || openList.Count > 5)
                {
                    var cur = curPos;
                    List<Point> finalPath = new List<Point>();
                    while(cur.Parent != null)
                    {
                        finalPath.Add(cur);
                        cur = cur.Parent;
                    }
                    finalPath.Add(cur);
                    finalPath.Reverse();
                    return finalPath;
                }

                //add current to closed and remove from open
                closedList.Add(curPos);
                openList.Remove(curPos);

                var neighbors = getNeighbors(curPos);

                neighbors.ForEach(neighbor =>
                {
                    if (closedList.Exists(q => q.X == neighbor.X && q.Y == neighbor.Y))
                    {
                        return;
                    }

                    int gScore = curPos.G + 1;
                    bool isBestG = false;

                    if (! (openList.Exists(q => q.X == neighbor.X && q.Y == neighbor.Y)))
                    {
                        isBestG = true;
                        neighbor.H = h(neighbor);
                        neighbor.G = gScore;
                        neighbor.F = neighbor.G + neighbor.H;
                        openList.Add(neighbor);
                    }else if(gScore < neighbor.G)
                    {
                        isBestG = true;
                    }

                    if (isBestG)
                    {
                        neighbor.Parent = curPos;
                        neighbor.G = gScore;
                        neighbor.F = neighbor.G + neighbor.H;
                    }
                });
            }

            return null;

        }

        public class APoint : Point
        {
            public int F = 0;
            public int G = 0;
            public int H = 0;
            public APoint Parent = null;

            public APoint(int x, int y) : base(x, y)
            {
                X = x;
                Y = y;
            }

            public APoint(Point p):base(p)
            {
                X = p.X;
                Y = p.Y;
            }

        }

    }
}
