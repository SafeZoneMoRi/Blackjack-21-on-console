namespace Porject_Gi_113
{
    public class MaiaGameMenui
    {
        public static void Main()
        {
            Console.Title = "Game Launcher";

            int selectedIndex = 0;
            string[] menu = new string[]
            {
                "Play Blackjack",
                "Play Gardener",
                "Play Bomber",
                "Exit"
            };

            while (true)
            {
                Console.Clear();
                ShowArrowMenu(menu, selectedIndex);

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex - 1 + menu.Length) % menu.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selectedIndex = (selectedIndex + 1) % menu.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (selectedIndex)
                    {
                        case 0:
                            MainGameBlackjack.StartBlackjack();
                            break;
                        case 1:
                            MainGameGardener.StartGardener();
                            break;
                        case 2:
                            MainGameBomber.StartBlackBomber();
                            break;
                        case 3:
                            Console.WriteLine("\nExiting...");
                            return;
                    }
                }
            }
        }

        static void ShowArrowMenu(string[] menuLines, int selectedIndex)
        {
            int screenWidth = Console.WindowWidth;
            int screenHeight = Console.WindowHeight;

            int menuHeight = menuLines.Length + 3; // +3 สำหรับ Title
            int topPadding = (screenHeight / 2) - (menuHeight / 2);

            Console.SetCursorPosition(0, Math.Max(0, topPadding));
            PrintCentered("==============================");
            PrintCentered("        GAME LAUNCHER         ");
            PrintCentered("==============================");

            for (int i = 0; i < menuLines.Length; i++)
            {
                string prefix = (i == selectedIndex) ? "> " : "  ";
                string line = prefix + menuLines[i];
                PrintCentered(line);
            }
        }

        static void PrintCentered(string text)
        {
            int screenWidth = Console.WindowWidth;
            int leftPadding = (screenWidth / 2) - (text.Length / 2);
            Console.SetCursorPosition(Math.Max(0, leftPadding), Console.CursorTop);
            Console.WriteLine(text);
        }
    }
}
