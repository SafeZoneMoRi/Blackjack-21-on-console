namespace Porject_Gi_113;

public class CardDisplay
{
    public static void ShowCardGUI(List<Card> cards, bool hideFirst = false)
    {
        if (cards.Count == 0)
        {
            Console.WriteLine("(no cards)");
            return;
        }

        string[] top = new string[cards.Count];
        string[] mid1 = new string[cards.Count];
        string[] mid2 = new string[cards.Count];
        string[] mid3 = new string[cards.Count];
        string[] bot = new string[cards.Count];

        for (int i = 0; i < cards.Count; i++)
        {
            if (i == 0 && hideFirst)
            {
                top[i] = "╔═════════╗";
                mid1[i] = "║░░░░░░░░░║";
                mid2[i] = "║░░░░░░░░░║";
                mid3[i] = "║░░░░░░░░░║";
                bot[i] = "╚═════════╝";
                continue;
            }

            var c = cards[i];
            string symbol = c.Suit switch
            {
                "Hearts" => "♥",
                "Diamonds" => "♦",
                "Clubs" => "♣",
                "Spades" => "♠",
                _ => "?"
            };

            string rank = c.Rank.Length == 1 ? c.Rank + " " : c.Rank;

            top[i] = "╔═════════╗";
            mid1[i] = $"║ {rank}      ║";
            mid2[i] = $"║    {symbol}    ║";
            mid3[i] = $"║       {rank}║";
            bot[i] = "╚═════════╝";
        }

        Console.WriteLine(string.Join("   ", top));
        Console.WriteLine(string.Join("   ", mid1));
        Console.WriteLine(string.Join("   ", mid2));
        Console.WriteLine(string.Join("   ", mid3));
        Console.WriteLine(string.Join("   ", bot));
    }
}