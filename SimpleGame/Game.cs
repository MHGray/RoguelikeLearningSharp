using System;
using System.Collections.Generic;

namespace SimpleGame
{
	class Game
	{
		public static Random rng;
		public const int WIDTH = 120;
		public const int HEIGHT = 30;
		//public static Artist artist;
		public static Player player = new Player();
		public static Map map;
        public static List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static AStar aStar = new AStar();
        public static Dice dice = new Dice();

		static void Main()
		{			
			map = new Map(100, 25);
            rng = new Random();
			map.Test();
            player.SetPos(Game.map.GetWalkableTilePos());
            for (int i = 0; i < 25; i++)
            {
                Enemies.Add(new Goblin());
            }
            
			map.Artist.Clear();

			Draw();
		}

		public static void Update()
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			player.Update(key);
            Enemies.ForEach(e => e.Update());
			Draw();
		}

		public static void Draw()
		{
			map.Draw();
            
            Enemies.ForEach(e => e.Draw());
            player.Draw();
            map.Artist.Draw();

			Update();
		}

		
	}
}
