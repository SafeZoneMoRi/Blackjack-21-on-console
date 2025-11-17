namespace ProjectBomber
{
    public static class MainGameBomber
    {
        private static Random bomber = new Random();

        public static void StartBlackBomber()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("==== Bomber Game ====");
                Console.WriteLine("1. Start Game");
                Console.WriteLine("2. How to Play");
                Console.WriteLine("3. View Leaderboard");
                Console.WriteLine("4. Admin Tool");
                Console.WriteLine("5. Exit");
                Console.Write("Select: ");

                string select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        PlayGame();
                        break;

                    case "2":
                        HowToPlayBomber.RuleToPlay();
                        break;

                    case "3":
                        Console.Clear();
                        Calculator.ShowLeaderboard();
                        Console.WriteLine("\nPress any key to return...");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.Clear();
                        Console.Write("Enter admin password: ");
                        string password = Console.ReadLine();
                        if (password == "Toonnaja")
                        {
                            AdminToolBomber.ShowAdminMenu();
                        }
                        else
                        {
                            Console.WriteLine("Incorrect password! Press any key to return...");
                            Console.ReadKey();
                        }
                        break;

                    case "5":
                        return;

                    default:
                        Console.WriteLine("Invalid input! Press any key...");
                        Console.ReadKey();
                        break;
                }
            }
        }
        private static void PlayGame()
        {
            Console.Clear();
            Console.Write("Enter Your Name: ");
            string playerName = Console.ReadLine();
            int totalScore = 0;

            for (int level = 1; level <= 10; level++)
            {
                Console.Clear();
                Console.WriteLine($"Mission ==> {level}");

                int score = LevelGame();

                totalScore += score;

                if (score == 0)
                {
                    Console.WriteLine($"\nMission Failed! Total Score: {totalScore}");
                    Calculator.SaveScore(playerName, totalScore); // บันทึกคะแนนแม้แพ้
                    Console.WriteLine("Returning to menu...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine($"Your Score: {totalScore}");
                Console.WriteLine("Press any key to continue to next mission...");
                Console.ReadKey();
            }

            Console.Clear();
            Console.WriteLine("End of Mission");
            Console.WriteLine($"Player: {playerName} | Total Score: {totalScore}");
            Calculator.SaveScore(playerName, totalScore);
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        private static int LevelGame()
        {
            int bombX = bomber.Next(1, 4);
            int bombY = bomber.Next(1, 4);
            // Console.WriteLine($"DEBUG: Bomb is at {bombX},{bombY}");
            int miss = 0;

            while (miss < 3)
            {
                Console.WriteLine("Point Coordinates");

                int x;
                while (true)
                {
                    Console.Write("Enter number 1-3 to mark x = ");
                    string inputX = Console.ReadLine();
                    if (int.TryParse(inputX, out x) && x >= 1 && x <= 3) break;
                    Console.WriteLine("Invalid input! Please enter number 1-3.");
                }

                int y;
                while (true)
                {
                    Console.Write("Enter number 1-3 to mark y = ");
                    string inputY = Console.ReadLine();
                    if (int.TryParse(inputY, out y) && y >= 1 && y <= 3) break;
                    Console.WriteLine("Invalid input! Please enter number 1-3.");
                }

                if (x == bombX && y == bombY)
                {
                    Console.WriteLine("Happy! Bomb defused.");
                    return Calculator.CalculatorScore(miss);
                }
                else
                {
                    miss++;
                    Console.WriteLine($"Wrong find again: {miss}/3");
                }
            }

            Console.WriteLine("Booooommmmmmmm! Mission Failed.");
            return 0;
        }
    }
}
