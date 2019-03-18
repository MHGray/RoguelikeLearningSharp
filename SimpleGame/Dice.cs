using System;
using System.Text.RegularExpressions;

namespace SimpleGame
{
    class Dice
    {
        private Random _rng { get;set; } = new Random();

        public int Roll(string diceNotation)
        {
            //dice notation should be 
            // NdV or C
            // N = number of dice
            // V = value of dice
            // C = a constant
            //Each portion will be split by a semicolon ; and can have a leading - operator
            //"1d6+1", 1d6-12d8+1, -200d20+3d8-2d6+7-8-6+1 are all valid roll strings

            string pattern = @"(-?\+?\d+d\d+)|(-?\+?\d+)";
            RegexOptions options = RegexOptions.Multiline;
            int total = 0;

            foreach (Match m in Regex.Matches(diceNotation, pattern, options))
            {
                //Console.WriteLine("'{0}' found at index {1}.", m.Value, m.Index);
                //parse each m
                string value = m.Value;
                bool adding = true;
                if (value.StartsWith("+"))
                {
                    value = value.Remove(0, 1);
                }else if (value.StartsWith("-"))
                {
                    adding = false;
                    value = value.Remove(0, 1);
                }

                //If it is a dice Value do this
                if (value.Contains("d"))
                {
                    string[] die = value.Split('d');
                    int numDice = Convert.ToInt32(die[0]);
                    int valueDice = Convert.ToInt32(die[1]);
                    if (adding)
                    {
                        total += RollDie(numDice, valueDice);
                    }
                    else
                    {
                        total -= RollDie(numDice, valueDice);
                    }
                }
                else
                {
                    int valueDice = Convert.ToInt32(value);
                    if (adding)
                    {
                        total += valueDice;
                    }
                    else
                    {
                        total -= valueDice;
                    }
                }
            }

            //Test to see what is actually getting rolled
            Game.map.Artist.DrawRectFill(' ', 1, 1, 12, 1, Color.normal);
            Game.map.Artist.DrawRect('∙', 0, 0, 14, 3, Color.normal);

            string totalWord = total.ToString();
            for (int i = 0; i < totalWord.Length; i++)
            {
                Game.map.Artist.DrawSymbol(totalWord[i], 1 + i, 1, Color.normal);
            }

            return total;
        }

        int RollDie(int num, int value)
        {
            int total = 0;
            for (int i = 0; i < num; i++)
            {
                int roll = _rng.Next(1, value + 1);
                if(roll == value)
                {
                    roll += (RollDie(1, value));
                }
                total += roll;

            }
            return total;
        }

    }
}
