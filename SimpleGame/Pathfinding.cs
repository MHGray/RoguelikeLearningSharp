﻿using System;
using System.Collections.Generic;

namespace SimpleGame
{
    class Pathfinding
    {

    }

    class AStar{

        public List<Point> Find(Point pStart, Point pEnd, Func<Point,int> h, Func<Point, List<Point>> getNeighbors)
        {
            //List<Point> finalPath = new List<Point>();

            Point start = new Point(pStart);
            start.H = h(start);
            start.G = start.H +start.G;
            Point end = new Point(pEnd);

            List<Point> openList = new List<Point>();
            List<Point> closedList = new List<Point>();

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
                //Also close if the closedList count gets too high. This gives 'approximate'
                //path reasonably well
                if((curPos.X == end.X && curPos.Y == end.Y) || closedList.Count > 10)
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

    }
}