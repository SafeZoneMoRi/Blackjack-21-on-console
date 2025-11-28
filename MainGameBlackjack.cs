namespace Porject_Gi_113
{
    public class MainGameBlackjack
    {
        static int playerBalance = 1000;

        public static void StartBlackjack()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "Blackjack - Console Game";
            ShowMenu();
        }

        static void ShowMenu()
        {
            int selected = 0;
            string[] menuItems = { "Start Game", "How to Play", "Exit" };

            while (true)
            {
                Console.Clear();
                Banner.ShowMenuBanner();

                for (int i = 0; i < menuItems.Length; i++)
                    Console.WriteLine((i == selected ? "> " : "  ") + menuItems[i]);

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + menuItems.Length) % menuItems.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % menuItems.Length;
                else if (key == ConsoleKey.Enter)
                {
                    switch (selected)
                    {
                        case 0: PlayGameLoop(); break;
                        case 1: ShowHowToPlay(); break;
                        case 2: return;
                    }
                }

                Console.SetCursorPosition(0, 0);
            }
        }

        static void ShowHowToPlay()
        {
            Console.Clear();
            Banner.ToturialMenuBanner();
            Console.WriteLine("\nObjective: Get as close to 21 as possible (but not over)");
            Console.WriteLine("- card J, Q, K = 10");
            Console.WriteLine("- A card = 1 or 11 depending on the situation");
            Console.WriteLine("- Use the Hit/Stand menu to play");
            Console.WriteLine("\nPress any key to return...");
            Console.ReadKey();
        }

        static void PlayGameLoop()
        {
            bool playAgain;
            do
            {
                PlayGame();
                if (playerBalance <= 0)
                {
                    Console.Clear();
                    Banner.ShowEndBanner();
                    Console.WriteLine("\nOut of money! Return to main menu...");
                    Console.WriteLine("Press any key to return...");
                    Console.ReadKey();
                    break;
                }
                playAgain = AskReplayMenu();
            } while (playAgain);
        }

        static void PlayGame()
        {
            if (playerBalance <= 0) return;

            var deck = new Deck();
            var player = new Hand();
            var dealer = new Hand();

            int bet = ShowBetMenu();
            playerBalance -= bet;
            
            player.AddCard(deck.DrawCard());
            dealer.AddCard(deck.DrawCard());
            player.AddCard(deck.DrawCard());
            dealer.AddCard(deck.DrawCard());

            while (true)
            {
                Console.Clear();
                Banner.ShowGameBanner();
                Console.WriteLine($"Your money: {playerBalance}฿ | Bet: {bet}฿");
                Console.WriteLine("Player:");
                CardDisplay.ShowCardGUI(player.Cards);
                Console.WriteLine($"\nTotal: {player.GetTotal()}");
                Console.WriteLine("\nDealer:");
                CardDisplay.ShowCardGUI(dealer.Cards, hideFirst: true);

                if (player.GetTotal() == 21)
                {
                    Console.WriteLine("Blackjack! You win!");
                    playerBalance += bet * 2;
                    return;
                }

                string choice = HitStandMenu();
                if (choice == "Hit")
                {
                    player.AddCard(deck.DrawCard());
                    if (player.GetTotal() > 21)
                    {
                        Console.Clear();
                        Banner.ShowGameBanner();
                        Console.WriteLine($"Your money: {playerBalance}฿ | Bet: {bet}฿");
                        Console.WriteLine("Player:");
                        CardDisplay.ShowCardGUI(player.Cards);
                        Console.WriteLine($"\nTotal: {player.GetTotal()}");
                        Console.WriteLine("\nBust! Dealer wins!");
                        return;
                    }
                }
                else break;
            }
            
            Console.WriteLine("\nDealer’s Turn:");
            CardDisplay.ShowCardGUI(dealer.Cards);

            while (dealer.GetTotal() < 17)
            {
                dealer.AddCard(deck.DrawCard());
                Console.Clear();
                Banner.ShowGameBanner();
                Console.WriteLine($"Your money: {playerBalance}฿ | Bet: {bet}฿");
                Console.WriteLine("Player:");
                CardDisplay.ShowCardGUI(player.Cards);
                Console.WriteLine($"\nTotal: {player.GetTotal()}");
                Console.WriteLine("\nDealer:");
                CardDisplay.ShowCardGUI(dealer.Cards);
                Thread.Sleep(800);
            }

            int pTotal = player.GetTotal();
            int dTotal = dealer.GetTotal();
            Console.WriteLine($"\nYour Total: {pTotal}");
            Console.WriteLine($"Dealer Total: {dTotal}");

            if (dTotal > 21 || pTotal > dTotal)
            {
                Console.WriteLine("You Win!");
                playerBalance += bet * 2;
            }
            else if (pTotal == dTotal)
            {
                Console.WriteLine("Draw!");
                playerBalance += bet;
            }
            else
            {
                Console.WriteLine("Dealer Wins!");
            }
        }

        static int ShowBetMenu()
        {
            int selected = 0;
            int[] betOptions = { 100, 200, 500, -1, 0 };

            while (true)
            {
                Console.Clear();
                Banner.ShowMoneyBanner();
                Console.WriteLine($"\nYour money: {playerBalance}฿");
                Console.WriteLine("Select bet amount:");

                for (int i = 0; i < betOptions.Length; i++)
                {
                    string label = betOptions[i] switch
                    {
                        -1 => "Custom (Specify the amount)",
                        0 => "All In",
                        _ => betOptions[i] + "฿"
                    };
                    Console.WriteLine((i == selected ? "> " : "  ") + label);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + betOptions.Length) % betOptions.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % betOptions.Length;
                else if (key == ConsoleKey.Enter)
                {
                    if (betOptions[selected] == 0) return playerBalance; // All In
                    else if (betOptions[selected] == -1)
                    {
                        while (true)
                        {
                            Console.Write("\nSelect bet amount: ");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int customBet) && customBet > 0 && customBet <= playerBalance)
                                return customBet;
                            Console.WriteLine($"Enter the amount you have (1-{playerBalance})");
                        }
                    }
                    else
                    {
                        if (betOptions[selected] > playerBalance) continue;
                        return betOptions[selected];
                    }
                }
            }
        }

        static string HitStandMenu()
        {
            int selected = 0;
            string[] options = { "Hit", "Stand" };
            int safeTop = Math.Max(Console.WindowHeight - options.Length - 1, 0);

            while (true)
            {
                for (int i = 0; i < options.Length; i++)
                {
                    Console.SetCursorPosition(0, safeTop + i);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, safeTop + i);
                    Console.WriteLine((i == selected ? "> " : "  ") + options[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + options.Length) % options.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % options.Length;
                else if (key == ConsoleKey.Enter) return options[selected];
            }
        }

        static bool AskReplayMenu()
        {
            int selected = 0;
            string[] items = { "Yes", "No" };
            int safeTop = Math.Max(Console.WindowHeight - items.Length - 1, 0);

            while (true)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    Console.SetCursorPosition(0, safeTop + i);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, safeTop + i);
                    Console.WriteLine((i == selected ? "> " : "  ") + items[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + items.Length) % items.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % items.Length;
                else if (key == ConsoleKey.Enter) return selected == 0;
            }
        }
    }
}
