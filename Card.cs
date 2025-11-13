namespace Porject_Gi_113;

public class Card
{
    public string Rank { get; }
    public string Suit { get; }
    public int Value { get; }

    public Card(string rank, string suit)
    {
        Rank = rank;
        Suit = suit;
        Value = rank switch
        {
            "J" or "Q" or "K" => 10,
            "A" => 11,
            _ => int.Parse(rank)
        };
    }

    public override string ToString()
    {
        string symbol = Suit switch
        {
            "Hearts" => "♥",
            "Diamonds" => "♦",
            "Clubs" => "♣",
            "Spades" => "♠",
            _ => "?"
        };
        return $"{Rank}{symbol}";
    }
}