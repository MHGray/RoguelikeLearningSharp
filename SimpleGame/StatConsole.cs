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
            artist = new Artist(Width, Height, 0, Game.map.Height);
        }

        public void Draw()
        {
            //Line 1
            string line1 = "";
            line1 += "HP: " + Game.stage.GetPlayer().HP + "/" + Game.stage.GetPlayer().HPMax + "  Str: " +
                     Game.stage.GetPlayer().Str + "  Con: " + Game.stage.GetPlayer().Con;
            if(line1.Length > Width)
            {
                return;
            }
            artist.WriteLine(line1, 0, 0);
            artist.Draw();
        }
    }
}
