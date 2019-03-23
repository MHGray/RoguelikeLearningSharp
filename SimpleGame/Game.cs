using System;
using System.Collections.Generic;

namespace SimpleGame
{
	class Game
	{
		public static Random rng;
		public const int WIDTH = 120;
		public const int HEIGHT = 30;
		public static Map map;
        public static AStar aStar = new AStar();
        public static Dice dice = new Dice();
        public static StatConsole stats;
        public static MessageConsole messages;
        public static Stage stage;

        static void Main()
		{
            Console.WindowWidth = WIDTH;
            Console.WindowHeight = HEIGHT+2;
			map = new Map(80, 25);
            stage = new Stage();
            stats = new StatConsole(80, 5);
            messages = new MessageConsole(40,30);
            rng = new Random(6);
			map.Test();
            stage.Add(new Player());
            for (int i = 0; i < 6; i++)
            {
                stage.Add(new Goblin());
            }
            stage.GetPlayer().SetPos(map.GetWalkableTilePos());

            map.Artist.Clear();
            stats.Draw();

			Draw();
		}

		public static void Update()
		{
            stage.Update();
            
			Draw();
		}

		public static void Draw()
		{
			map.Draw();
            stage.Draw();
            map.Artist.Draw();

			Update();
		}
	}
}
