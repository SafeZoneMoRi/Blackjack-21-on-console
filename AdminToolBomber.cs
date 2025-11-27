namespace Porject_Gi_113
{
    public static class AdminToolBomber
    {
        public static void ShowAdminMenu()
        {
            string[] options = 
            {
                "View Leaderboard",
                "Reset Leaderboard",
                "Back to Main Menu"
            };

            int selected = 0;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Admin Tool ===\n");
                
                for (int i = 0; i < options.Length; i++)
                {
                    Console.WriteLine((i == selected ? "> " : "  ") + options[i]);
                }
                
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                    selected = (selected - 1 + options.Length) % options.Length;

                else if (key == ConsoleKey.DownArrow)
                    selected = (selected + 1) % options.Length;

                else if (key == ConsoleKey.Enter)
                {
                    switch (selected)
                    {
                        case 0:
                            Console.Clear();
                            Calculator.ShowLeaderboard();
                            Console.WriteLine("\nPress any key to return...");
                            Console.ReadKey();
                            break;

                        case 1:
                            Console.Clear();
                            Calculator.ResetLeaderboard();
                            Console.WriteLine("Leaderboard has been reset!");
                            Console.WriteLine("\nPress any key to return...");
                            Console.ReadKey();
                            break;

                        case 2:
                            return;
                    }
                }
            }
        }
    }
}
