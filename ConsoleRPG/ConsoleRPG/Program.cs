using System;

namespace ConsoleRPG
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Title = "Console RPG Game";
                Console.ForegroundColor = ConsoleColor.Cyan;

                Game game = new Game();
                game.StartGame();

                Console.WriteLine("\n press button for play   ");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"critical error: {ex.Message}");
                Console.WriteLine("press button for exit   ");
                Console.ReadKey();
            }
        }
    }
}