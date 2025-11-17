using ProjectBomber;

namespace Porject_Gi_113
{
    public class MaiaGameMenui
    {
        public static void Main()
        {
            Console.Title = "Game Launcher";

            while (true)
            {
                Console.Clear();
                ShowCenteredMenu();
                Console.Write("\nEnter choice: ");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        MainGameGardener.StartGardener(); // Gardener
                        break;
                    case "2":
                        MainGameBlackjack.StartBlackjack(); // Blackjack
                        break;
                    case "3":
                        MainGameBomber.StartBlackBomber(); // Bomber
                        break;
                    case "4":
                        Console.WriteLine("\nExiting...");
                        return;
                    default:
                        Console.WriteLine("\nInvalid option!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void ShowCenteredMenu()
        {
            string[] menuLines = new string[]
            {
                "==============================",
                "        GAME LAUNCHER         ",
                "==============================",
                "1. Play Gardener",
                "2. Play Blackjack",
                "3. Play Bomber",
                "4. Exit"
            };

            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;
            int menuHeight = menuLines.Length;
            int topPadding = (screenHeight / 2) - (menuHeight / 2);

            Console.SetCursorPosition(0, Math.Max(0, topPadding));
            foreach (var line in menuLines)
            {
                int leftPadding = (screenWidth / 2) - (line.Length / 2);
                Console.SetCursorPosition(Math.Max(0, leftPadding), Console.CursorTop);
                Console.WriteLine(line);
            }
        }
    }
}
