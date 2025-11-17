namespace ProjectBomber
{
    public static class AdminToolBomber
    {
        public static void ShowAdminMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Admin Tool ===");
                Console.WriteLine("1. View Leaderboard");
                Console.WriteLine("2. Reset Leaderboard");
                Console.WriteLine("3. Back to Main Menu");
                Console.Write("Select: ");

                string select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        Console.Clear();
                        Calculator.ShowLeaderboard();
                        Console.WriteLine("\nPress any key to return...");
                        Console.ReadKey();
                        break;

                    case "2":
                        Console.Clear();
                        Calculator.ResetLeaderboard();
                        Console.WriteLine("Leaderboard has been reset!");
                        Console.WriteLine("Press any key to return...");
                        Console.ReadKey();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid input! Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }
    }
}