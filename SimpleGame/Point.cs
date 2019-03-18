namespace SimpleGame
{
    /// <summary>
    /// Create a Point
    /// </summary>
    public class Point
    {
        public int X;
        public int Y;
        public int F = 0;
        public int G = 0;
        public int H = 0;
        public Point Parent = null;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Point()
        {
            X = 0;
            Y = 0;
        }
    }
}
