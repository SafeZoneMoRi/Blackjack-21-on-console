namespace Porject_Gi_113
{
    public class GardenerGame
    {
        private int treeHp = 100;
        private int[] timePerDay = { 10, 8, 6, 5, 4, 3, 3 };
        private EventManager eventManager = new EventManager();
        private PlayerGardener player = new PlayerGardener();

        public void Play()
        {
            Console.Clear();
            Console.WriteLine("Gardener");
            Console.Write("Enter your name: ");
            player.Name = Console.ReadLine(); 

            for (int day = 1; day <= timePerDay.Length; day++)
            {
                string currentEvent = eventManager.RandomEvent();
                string correctTool = eventManager.GetProtect(currentEvent);
                
                Console.Clear();
                Console.WriteLine($"Day {day}");
                Console.WriteLine($"Tree HP: {treeHp}\n");
                Console.WriteLine($"Event: {currentEvent}");
                Console.WriteLine($"Item Press 1 == Spray" +
                                  $"\n"+"" +
                                  "\nPress 2 == Axe"+"" +
                                  "\nPress 3 == Hormone"+"" +
                                  "\nPress 4 == Rope"+"" +
                                  "\nPress 5 == Water");

                if (!HandlePlayerChoice(correctTool, timePerDay[day - 1], currentEvent))
                    return;

                if (treeHp <= 0)
                {
                    GameOver("Your tree has died!");
                    return;
                }

                Thread.Sleep(1000);
            }

            Console.WriteLine($"\nOut of days. Tree HP: {treeHp}");
            Console.WriteLine($"Player: {player.Name} | Score: {player.CalculateScore(treeHp)}"); // ใช้ method ของ PlayerGardener
            Console.ReadKey();
        }

        private bool HandlePlayerChoice(string correctTool, int timeLimit, string currentEvent)
        {
            string choice = "";
            int timeLeft = timeLimit;
            int timerPosY = Console.CursorTop;

            Console.SetCursorPosition(0, timerPosY);
            Console.Write("Your choice: ");

            while (timeLeft > 0)
            {
                Console.SetCursorPosition(0, timerPosY);
                Console.Write($"Time left: {timeLeft}   ");

                if (Console.KeyAvailable)
                {
                    choice = Console.ReadKey(true).KeyChar.ToString();
                    Console.WriteLine($"\nYou selected: {choice}");
                    break;
                }

                Thread.Sleep(1000);
                timeLeft--;
            }

            Console.WriteLine("Time left: 0\n");

            if (choice == "")
            {
                GameOver("You failed to choose a tool in time!");
                return false;
            }

            if (choice == correctTool)
            {
                Console.WriteLine("Defense success");
            }
            else
            {
                int damage = eventManager.GetDamage(currentEvent);
                treeHp -= damage;
                Console.WriteLine($"\nEnemy hit! Tree HP: {treeHp}");
            }

            return true;
        }

        private void GameOver(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("=====================================");
            Console.WriteLine("            GAME OVER");
            Console.WriteLine("=====================================");
            Console.WriteLine($"\n{message}");
            Console.WriteLine($"\nFinal Tree HP: {treeHp}");
            Console.ResetColor();
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}
