using System;

namespace SimpleGame
{
    class Dice
    {
        private Random _rng = new Random();

        public int Roll(string diceNotation)
        {
            //dice notation should be 
            // NdV or C
            // N = number of dice
            // V = value of dice
            // C = a constant
            //Each portion will be split by an operator '+' which could be + or -
            //"1d6+1", 1d6-12d8+1, -200d20+3d8-2d6+7-8-6+1 are all valid roll strings
            //It should also remove all whitespace in case I'm a doofus

             return _rng.Next();
        }

    }
}
