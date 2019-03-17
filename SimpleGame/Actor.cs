using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGame
{
    public abstract class Actor
    {
        public char Symbol;
        public Color Color = Color.normal;
        public int X;
        public int Y;

        public string Str = "1d3";
        public string Con = "1d2";
        public int HP = 3;
    }
}
