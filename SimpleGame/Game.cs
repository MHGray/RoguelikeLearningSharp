using System;
using System.Collections.Generic;

namespace SimpleGame
{
	class Game
	{
		public static Random rng;
		public const int WIDTH = 120;
		public const int HEIGHT = 30;
		public static Artist artist = new Artist(WIDTH,HEIGHT);
		public static Player player = new Player();
		public static Map map;
        public static List<Enemy> Enemies { get; set; } = new List<Enemy>();
        public static AStar aStar = new AStar();
        public static Dice dice = new Dice();

		static void Main()
		{			
			map = new Map(WIDTH, HEIGHT);
            rng = new Random(6);
			map.Test();
            player.SetPos(Game.map.GetWalkableTilePos());
            for (int i = 0; i < 25; i++)
            {
                Enemies.Add(new Goblin());
            }
            
			artist.Clear();
			artist.DrawRect('#', 10, 10, 10, 10, Color.normal);
			artist.DrawRect('#', 20, 20, 10, 10, Color.normal);

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
            artist.Draw();

			Update();
		}

		
	}
}
