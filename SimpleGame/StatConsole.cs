using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame
{
    class StatConsole
    {
        public int Width;
        public int Height;
        public Artist artist;

        public StatConsole(int width, int height)
        {
            Width = width;
            Height = height;
            artist = new Artist(Width, Height, 0, 25);
        }

        public void Draw()
        {
            //Line 1
            string line1 = "";
            line1 += "HP: " + Game.player.HP + "/" + Game.player.HPMax + "  Str: " +
                     Game.player.Str + "  Con: " + Game.player.Con;
            if(line1.Length > Width)
            {
                return;
            }
            artist.WriteLine(line1, 0, 0);
            artist.Draw();
            //Line 2
            //Line 3
        }

    }
}
