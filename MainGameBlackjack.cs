namespace Porject_Gi_113
{
    public class MainGameBlackjack
    {
        static int playerBalance = 1000;

        public static void StartBlackjack()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Title = "🃏 Blackjack - Console Game";
            ShowMenu();
        }

        static void ShowMenu()
        {
            int selected = 0;
            string[] menuItems =
            {
                "Start Game", 
                "How to Play", 
                "Exit"
            };

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
                        case 0: PlayGame(); break;
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
            Console.WriteLine("\n🎯 เป้าหมาย: ให้แต้มใกล้ 21 มากที่สุด (แต่ห้ามเกิน)");
            Console.WriteLine("- ไพ่ J, Q, K = 10");
            Console.WriteLine("- ไพ่ A = 1 หรือ 11 แล้วแต่สถานการณ์");
            Console.WriteLine("- ใช้เมนู Hit / Stand เพื่อเล่น");
            Console.WriteLine("\nกดปุ่มอะไรก็ได้ เพื่อกลับ...");
            Console.ReadKey();
        }

        static void PlayGame()
        {
            if (playerBalance <= 0)
            {
                Console.Clear();
                Banner.ShowEndBanner();
                Console.WriteLine("\n💀 เงินหมด! กลับไปหน้าเมนูหลัก...");
                Console.WriteLine("กดปุ่มอะไรก็ได้ เพื่อกลับ...");
                Console.ReadKey();
                return;
            }

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
                Console.WriteLine($"💰 เงินของคุณ: {playerBalance}฿ | เดิมพัน: {bet}฿");
                Console.WriteLine("🧍‍♂️ Player:");
                CardDisplay.ShowCardGUI(player.Cards);
                Console.WriteLine($"\nTotal: {player.GetTotal()}");
                Console.WriteLine("\n🤵 Dealer:");
                CardDisplay.ShowCardGUI(dealer.Cards, hideFirst: true);

                if (player.GetTotal() == 21)
                {
                    Console.WriteLine("🎉 Blackjack! You win!");
                    playerBalance += bet * 2;
                    AskReplay();
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
                        Console.WriteLine($"💰 เงินของคุณ: {playerBalance}฿ | เดิมพัน: {bet}฿");
                        Console.WriteLine("🧍‍♂️ Player:");
                        CardDisplay.ShowCardGUI(player.Cards);
                        Console.WriteLine($"\nTotal: {player.GetTotal()}");
                        Console.WriteLine("\n💀 Bust! Dealer wins!");
                        AskReplay();
                        return;
                    }
                }
                else if (choice == "Stand")
                {
                    break;
                }
            }

            Console.WriteLine("\n🤵 Dealer’s Turn:");
            CardDisplay.ShowCardGUI(dealer.Cards);

            while (dealer.GetTotal() < 17)
            {
                dealer.AddCard(deck.DrawCard());
                Console.Clear();
                Banner.ShowGameBanner();
                Console.WriteLine($"💰 เงินของคุณ: {playerBalance}฿ | เดิมพัน: {bet}฿");
                Console.WriteLine("🧍‍♂️ Player:");
                CardDisplay.ShowCardGUI(player.Cards);
                Console.WriteLine($"\nTotal: {player.GetTotal()}");
                Console.WriteLine("\n🤵 Dealer:");
                CardDisplay.ShowCardGUI(dealer.Cards);
                Thread.Sleep(800);
            }

            int pTotal = player.GetTotal();
            int dTotal = dealer.GetTotal();

            Console.WriteLine($"\nYour Total: {pTotal}");
            Console.WriteLine($"Dealer Total: {dTotal}");

            if (dTotal > 21 || pTotal > dTotal)
            {
                Console.WriteLine("🎉 You Win!");
                playerBalance += bet * 2;
            }
            else if (pTotal == dTotal)
            {
                Console.WriteLine("🤝 Draw!");
                playerBalance += bet;
            }
            else
            {
                Console.WriteLine("💀 Dealer Wins!");
            }

            AskReplay();
        }

        // เมนูเดิมพันใหม่ พร้อม All In และ Custom
        static int ShowBetMenu()
        {
            int selected = 0;
            int[] betOptions = new int[] { 100, 200, 500, -1, 0 }; // -1 = Custom, 0 = All In
            int top = Console.CursorTop;

            while (true)
            {
                Console.Clear();
                Banner.ShowMoneyBanner();
                Console.WriteLine($"\n💰 เงินของคุณ: {playerBalance}฿");
                Console.WriteLine("💵 เลือกจำนวนเดิมพัน:");

                for (int i = 0; i < betOptions.Length; i++)
                {
                    string label = betOptions[i] switch
                    {
                        -1 => "Custom (ระบุเอง)",
                        0 => "All In",
                        _ => betOptions[i] + "฿"
                    };
                    Console.WriteLine((i == selected ? "▶ " : "  ") + label);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + betOptions.Length) % betOptions.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % betOptions.Length;
                else if (key == ConsoleKey.Enter)
                {
                    if (betOptions[selected] == 0) return playerBalance; // All In
                    else if (betOptions[selected] == -1) // Custom
                    {
                        while (true)
                        {
                            Console.Write("\n💵 ระบุจำนวนเงินเดิมพัน: ");
                            string input = Console.ReadLine();
                            if (int.TryParse(input, out int customBet) && customBet > 0 && customBet <= playerBalance)
                                return customBet;
                            Console.WriteLine($"⚠️ ใส่จำนวนเงินที่คุณมี (1-{playerBalance})");
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
            int top = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, top);
                for (int i = 0; i < options.Length; i++)
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, top + i);
                    Console.WriteLine((i == selected ? "▶ " : "  ") + options[i]);
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
            int top = Console.CursorTop;

            while (true)
            {
                Console.SetCursorPosition(0, top);
                for (int i = 0; i < items.Length; i++)
                {
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, top + i);
                    Console.WriteLine((i == selected ? "▶ " : "  ") + items[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow) selected = (selected - 1 + items.Length) % items.Length;
                else if (key == ConsoleKey.DownArrow) selected = (selected + 1) % items.Length;
                else if (key == ConsoleKey.Enter) return selected == 0;
            }
        }

        static void AskReplay()
        {
            bool playAgain = AskReplayMenu();
            if (playAgain) PlayGame();
        }
    }
}
