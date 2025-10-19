using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;

namespace ConsoleRPG
{
    public class Player : Creature
    {
        public int Level { get; private set; }
        public int Experience { get; private set; }
        public int RequiredExperience => Level * 100;

        public Player(string name) : base(name, 100, 15)
        {
            Level = 1;
            Experience = 0;
        }

        public void GainExperience(int exp)
        {
            if (exp < 0)
                throw new ArgumentException("- ");

            Experience += exp;
            Console.WriteLine($"{Name} get {exp} xp. xp: {Experience}/{RequiredExperience}");

            while (Experience >= RequiredExperience)
            {
                LevelUp();
            }
        }

        private void LevelUp()
        {
            Level++;
            Experience -= RequiredExperience;

            int healthIncrease = 20;
            int attackIncrease = 5;

            MaxHealth += healthIncrease;
            Health += healthIncrease;
            AttackPower += attackIncrease;

            Console.WriteLine($"🎉 {Name} get {Level} level!");
            Console.WriteLine($"❤️  maxheal up {MaxHealth}");
            Console.WriteLine($"⚔️  damage up {AttackPower}");
        }

        public override void DisplayInfo()
        {
            string status = IsAlive ? "live" : "die";
            Console.WriteLine($"name {Name}");
            Console.WriteLine($"levfel {Level}");
            Console.WriteLine($"hp: {Health}/{MaxHealth}");
            Console.WriteLine($"damage {AttackPower}");
            Console.WriteLine($"Опыт: {Experience}/{RequiredExperience}");
            Console.WriteLine($"statud: {status}");

        }

        public void UseHealthPotion()
        {
            try
            {
                int healAmount = 30;
                Console.WriteLine($"{Name} use heal posion");
                Heal(healAmount);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }
    }
}