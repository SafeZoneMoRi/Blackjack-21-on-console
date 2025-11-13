namespace Porject_Gi_113;

public class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.Title = "🃏 Blackjack - Console Game";
        ShowMenu();
    }

    static void ShowMenu()
    {
        while (true)
        {
            Console.Clear();
            Banner.ShowMenuBanner();
            Console.WriteLine("1. Start Game");
            Console.WriteLine("2. How to Play");
            Console.WriteLine("3. Exit");
            Console.Write("Choose (1-3): ");

            switch (Console.ReadLine())
            {
                case "1": PlayGame(); break;
                case "2": ShowHowToPlay(); break;
                case "3": return;
                default:
                    Console.WriteLine("⚠️ Invalid option!");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ShowHowToPlay()
    {
        Console.Clear();
        Banner.ToturialMenuBanner();
        Console.WriteLine("\n🎯 เป้าหมาย: ให้แต้มใกล้ 21 มากที่สุด (แต่ห้ามเกิน)");
        Console.WriteLine("- ไพ่ J, Q, K = 10");
        Console.WriteLine("- ไพ่ A = 1 หรือ 11 แล้วแต่สถานการณ์");
        Console.WriteLine("- พิมพ์ H เพื่อจั่วไพ่ / S เพื่อหยุดจั่ว");
        Console.WriteLine("\nกดปุ่มอะไรก็ได้ เพื่อกลับ...");
        Console.ReadKey();
    }
static int playerBalance = 1000;

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
    
    int bet = 0;
    while (true)
    {
        Console.Clear();
        Banner.ShowMoneyBanner();
        Console.WriteLine($"\n💰 เงินของคุณ: {playerBalance}฿");
        Console.Write("💵 วางเดิมพัน: ");
        string input = Console.ReadLine();

        if (!int.TryParse(input, out bet) || bet <= 0 || bet > playerBalance)
        {
            Console.WriteLine($"⚠️ ใส่จำนวนเงินที่คุณมี (1-{playerBalance})");
            Console.ReadKey();
            continue;
        }
        break;
    }

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

        Console.Write("\nHit or Stand (H/S): ");
        string choice = Console.ReadLine()?.ToUpper();

        if (choice == "H")
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
        else if (choice == "S") break;
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

    static void AskReplay()
    {
        Console.Write("\nเล่นอีกครั้งไหม? (Y/N): ");
        if (Console.ReadLine()?.ToUpper() == "Y")
            PlayGame();
    }
}