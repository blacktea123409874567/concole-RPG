using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRPG
{
    public abstract class Creature
    {
        public string Name { get; protected set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; protected set; }
        public int AttackPower { get; protected set; }
        public bool IsAlive => Health > 0;
        
        protected Creature(string name, int maxHealth, int attackPower)
        {
            Name = name;
            MaxHealth = maxHealth;
            Health = maxHealth;
            AttackPower = attackPower;
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("- damage ");

            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public virtual void Attack(Creature target)
        {
            if (!IsAlive)
                throw new InvalidOperationException("you can'tattack");

            if (!target.IsAlive)
                throw new InvalidOperationException("he died");

            Console.WriteLine($"{Name} attack {target.Name}  of {AttackPower} damage");
            target.TakeDamage(AttackPower);
        }

        public virtual void DisplayInfo()
        {
            string status = IsAlive ? "Жив" : "Мертв";
            Console.WriteLine($"{Name} | heal: {Health}/{MaxHealth} | damage: {AttackPower} | status: {status}");
        }

        public void Heal(int amount)
        {
            if (amount < 0)
                throw new ArgumentException("-hp");

            if (!IsAlive)
                throw new InvalidOperationException("pozno");

            Health += amount;
            if (Health > MaxHealth)
                Health = MaxHealth;

            Console.WriteLine($"{Name} healing {amount} hp.count hp: {Health}/{MaxHealth}");
        }
    }
}