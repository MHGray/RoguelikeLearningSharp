using System;

namespace SimpleGame
{
    public abstract class Actor
    {
        public abstract string Name { get; set; }
        public char Symbol;
        public Color Color = Color.normal;
        public Point Pos = new Point();

        public virtual string Str { get;set; } = "1d3";
        public virtual string Con { get; set; } = "1";
        public virtual int HP { get; set; } = 3;
        public virtual int HPMax { get; set; } = 3;

        public abstract void Draw();
        public abstract void Update();

        public void Fight(Actor target)
        {
            int damage = Game.dice.Roll(Str);
            int defense = Game.dice.Roll(target.Con);
            int total = Math.Max((damage / 4) - (defense / 4), 0);
            target.HP -= total;
            Game.messages.Write(this.Name + " hits " + target.Name+" dealing " + total + " damage " + " " + damage + ":"+defense +"");
        }
    }
}
