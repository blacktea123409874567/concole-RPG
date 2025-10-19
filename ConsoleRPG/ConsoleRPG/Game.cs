using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ConsoleRPG
{
    public class Game
    {
        private Player player;
        private Monster[] monsters;
        private bool gameRunning;

        public Game()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            try
            {
                Console.WriteLine("welcome in Console RPG!");
                Console.Write("input name hero: ");
                string playerName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(playerName))
                    playerName = "Жора";

                player = new Player(playerName);

                Random random = new Random();
                int monsterCount = random.Next(2, 3);
                monsters = new Monster[monsterCount];

                for (int i = 0; i < monsterCount; i++)
                {
                    monsters[i] = Monster.CreateRandomMonster();
                }

                Console.WriteLine($"\ngame on! \n{player.Name} vs {monsterCount} monters!");
                gameRunning = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error {ex.Message}");
                gameRunning = false;
            }
        }

        public void StartGame()
        {
            if (!gameRunning)
            {
                Console.WriteLine("error ,can't on game ");
                return;
            }

            try
            {
                while (gameRunning && player.IsAlive && AreMonstersAlive())
                {

                    PlayerTurn();

                    if (!AreMonstersAlive()) break;

                    MonstersTurn();
                    CheckGameState();


                }

                EndGame();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
                Console.WriteLine("Game over.");
            }
        }

        private void PlayerTurn()
        {
            try
            {
                Console.WriteLine("\n--- player turn ---");
                player.DisplayInfo();
                DisplayMonstersInfo();

                Console.WriteLine("\nchoise turn:");
                Console.WriteLine("1 - attack monster");
                Console.WriteLine("2 -use heal posion");
                Console.WriteLine("3 - display info");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AttackMonster();
                        break;
                    case "2":
                        player.UseHealthPotion();
                        break;
                    case "3":
                        player.DisplayInfo();
                        DisplayMonstersInfo();
                        PlayerTurn(); 
                        break;
                    default:
                        Console.WriteLine("uncorrect input.");
                        PlayerTurn();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error: {ex.Message}");
            }
        }

        private void AttackMonster()
        {
            try
            {
                Console.WriteLine("\nchoise monster attack:");
                for (int i = 0; i < monsters.Length; i++)
                {
                    if (monsters[i].IsAlive)
                    {
                        Console.WriteLine($"{i + 1} - {monsters[i].Name} (hp: {monsters[i].Health})");
                    }
                }

                string input = Console.ReadLine();
                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= monsters.Length)
                {
                    Monster target = monsters[choice - 1];
                    if (target.IsAlive)
                    {
                        player.Attack(target);
                        if (!target.IsAlive)
                        {
                            Console.WriteLine($"🎯 {target.Name} !");
                            player.GainExperience(target.ExperienceReward);
                        }
                    }
                    else
                    {
                        Console.WriteLine("ершы ьщтыек куфвн вшу! срщшыу.");
                        AttackMonster();
                    }
                }
                else
                {
                    Console.WriteLine("uncorrect choise.");
                    AttackMonster();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error for attack monster: {ex.Message}");
            }
        }

        private void MonstersTurn()
        {
            Console.WriteLine("\n--- monster turn ---");

            foreach (var monster in monsters)
            {
                if (monster.IsAlive && player.IsAlive)
                {
                    try
                    {
                        monster.Attack(player);
                        if (!player.IsAlive)
                        {
                            Console.WriteLine($"💀 {player.Name} you lose!");
                            break;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"error for attack you {monster.Name}: {ex.Message}");
                    }

                }
            }
        }

        private void DisplayMonstersInfo()
        {
            Console.WriteLine("\n--- monsters ---");
            foreach (var monster in monsters)
            {
                monster.DisplayInfo();
            }
        }

        private bool AreMonstersAlive()
        {
            foreach (var monster in monsters)
            {
                if (monster.IsAlive)
                    return true;
            }
            return false;
        }

        private void CheckGameState()
        {
            if (!player.IsAlive)
            {
                gameRunning = false;
            }
            else if (!AreMonstersAlive())
            {
                gameRunning = false;
            }
        }

        private void EndGame()
        {
            Console.WriteLine("\n" + new string('=', 50));
            Console.WriteLine("game over");

            if (player.IsAlive)
            {
                Console.WriteLine($"🎉 win! {player.Name} reward {player.Level} level!");
            }
            else
            {
                Console.WriteLine($"💀 lose! {player.Name} die.");
            }

            Console.WriteLine("\nfinal stastic:");
            player.DisplayInfo();
            DisplayMonstersInfo();
        }
    }
}