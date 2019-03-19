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
        private List<string> _messages;

        private List<string> Messages {
            get { return _messages; }
            set {
                _messages = value;
                Draw();
            }
        }

        public MessageConsole(int width, int height)
        {
            _messages = new List<string>();
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
            //TODO This currently doesn't handle messages distinctly
            //I should probably pass a message list instead of a straight
            //up string and then just write it out in the artist.
            //No need to process each message twice.
            string messageBox = "";
            Messages.ForEach(m => messageBox += m );
            artist.WriteMessageBlock(messageBox);
        }
    }
}
