using System;

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

		static void Main()
		{
			
			map = new Map(WIDTH, HEIGHT);
			rng = new Random(6);
			map.Test();
			artist.Clear();
			artist.DrawRect('#', 10, 10, 10, 10, Color.normal);
			artist.DrawRect('#', 20, 20, 10, 10, Color.normal);

			Draw();
		}

		public static void Update()
		{
			ConsoleKeyInfo key = Console.ReadKey(true);
			if(key.Key == ConsoleKey.C)
			{
				artist.Clear();
			}
			player.Update(key);
			Draw();
		}

		public static void Draw()
		{
			map.Draw();
			player.Draw();
			artist.Draw();

			Update();
		}

		
	}
}
