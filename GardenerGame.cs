namespace Porject_Gi_113
{
    public class GardenerGame
    {
        private int treeHp = 100;
        private int[] timePerDay = { 10, 8, 6, 5, 4, 3, 3 };
        private EventGardener _eventGardener = new EventGardener();
        private PlayerGardener player = new PlayerGardener();

        public void Play()
        {
            Console.Clear();
            Console.WriteLine("Gardener");
            Console.Write("Enter your name: ");
            player.Name = Console.ReadLine();

            for (int day = 1; day <= timePerDay.Length; day++)
            {
                string currentEvent = _eventGardener.RandomEvent();
                string correctTool = _eventGardener.GetProtect(currentEvent);

                Console.Clear();
                Console.WriteLine($"Day {day}");
                Console.WriteLine($"Tree HP: {treeHp}\n");

                // 🔥 แสดงต้นไม้ตาม HP ปัจจุบัน
                ShowTreeState();
                Console.WriteLine();

                Console.WriteLine($"Event: {currentEvent}");

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
            Console.WriteLine($"Player: {player.Name} | Score: {player.CalculateScore(treeHp)}");
            Console.ReadKey();
        }
        private void ShowTreeState()
        {
            if (treeHp >= 80)
                Banner.TreeStateOne();
            else if (treeHp >= 60)
                Banner.TreeStateTwo();
            else
                Banner.TreeStateThree();
        }
        private bool HandlePlayerChoice(string correctTool, int timeLimit, string currentEvent)
        {
            string[] tools =
            {
                "Spray",
                "Axe",
                "Hormone",
                "Rope",
                "Water"
            };
            int selected = 0;
            int timeLeft = timeLimit;
            int top = Console.CursorTop;

            var watch = System.Diagnostics.Stopwatch.StartNew();

            while (timeLeft > 0)
            {
                for (int i = 0; i < tools.Length; i++)
                {
                    Console.SetCursorPosition(0, top + 1 + i);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, top + 1 + i);
                    Console.WriteLine((i == selected ? "> " : "  ") + tools[i]);
                }
                
                Console.SetCursorPosition(0, top);
                Console.Write($"Time left: {timeLeft}   ");
                
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.UpArrow)
                        selected = (selected - 1 + tools.Length) % tools.Length;
                    else if (key == ConsoleKey.DownArrow)
                        selected = (selected + 1) % tools.Length;
                    else if (key == ConsoleKey.Enter)
                    {
                        string choice = tools[selected];
                        Console.SetCursorPosition(0, top + tools.Length + 2);
                        Console.WriteLine($"\nYou selected: {choice}\n");

                        if (choice == correctTool)
                        {
                            Console.WriteLine("Defense success");
                        }
                        else
                        {
                            int damage = _eventGardener.GetDamage(currentEvent);
                            treeHp -= damage;
                            Console.WriteLine($"\nEnemy hit! Tree HP: {treeHp}");
                        }
                        return true;
                    }
                }
                
                if (watch.ElapsedMilliseconds >= 1000)
                {
                    timeLeft--;
                    watch.Restart();
                }

                Thread.Sleep(20);
            }
            
            Console.SetCursorPosition(0, top + tools.Length + 2);
            Console.WriteLine("\nTime left: 0\n");
            GameOver("You failed to choose a tool in time!");
            return false;
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
