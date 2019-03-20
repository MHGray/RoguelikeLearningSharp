using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame
{
    class MessageConsole
    {
        public int Width;
        public int Height;
        public Artist artist;
        private List<string> Messages;

        public MessageConsole(int width, int height)
        {
            Messages = new List<string>();
            Width = width;
            Height = height;
            artist = new Artist(Width, Height, Game.map.Width, 0);
        }

        public void Write(string msg)
        {
            if(msg.Length > Width)
            {
                List<string> lines = new List<string>();

                int lastSpaceInd = 0;

                while (msg.Length > Width)
                {
                    int curIndex = msg.IndexOf(' ', lastSpaceInd + 1);

                    if (curIndex > Width || curIndex == -1)
                    {
                        //break into a new message and start over
                        Messages.Add(msg.Substring(0, lastSpaceInd));
                        msg = msg.Substring(lastSpaceInd).TrimStart();
                        lastSpaceInd = 0;
                    }
                    else
                    {
                        lastSpaceInd = curIndex;
                    }
                }
            }

            Messages.Add(msg);
            while(Messages.Count > Height)
            {
                Messages.RemoveAt(0);
            }
            Draw();
        }

        public void Draw()
        {
            artist.WriteMessageBlock(Messages);
        }
    }
}
