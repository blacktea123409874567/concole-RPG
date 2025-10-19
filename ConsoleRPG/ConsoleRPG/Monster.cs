using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace ConsoleRPG
{
    public class Monster : Creature
    {
        public int ExperienceReward { get; private set; }

        public Monster(string name, int maxHealth, int attackPower, int experienceReward)
            : base(name, maxHealth, attackPower)
        {
            ExperienceReward = experienceReward;
        }

        public static Monster CreateRandomMonster()
        {
            Random random = new Random();
            string[] monsterNames = { "Гоблин", "Орк", "Скелет", "Зомби", "Волк", "Паук"};
            string name = monsterNames[random.Next(monsterNames.Length)];

            int health = random.Next(30, 61);
            int attack = random.Next(8, 16);
            int exp = random.Next(20, 41);

            return new Monster(name, health, attack, exp);
        }

        public override void DisplayInfo()
        {
            string status = IsAlive ? "live" : "die";
            Console.WriteLine($"name {Name}");
            Console.WriteLine($"hp {Health}/{MaxHealth}");
            Console.WriteLine($"damage {AttackPower}");
            Console.WriteLine($"win reward {ExperienceReward} p");
            Console.WriteLine($"status {status}\n\n");
        }
    }

}
