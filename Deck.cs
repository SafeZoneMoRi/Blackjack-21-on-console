namespace Porject_Gi_113;

public class Deck
{
    private List<Card> cards = new();
    private Random rand = new();

    public Deck()
    {
        string[] suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        string[] ranks = { "2","3","4","5","6","7","8","9","10","J","Q","K","A" };

        foreach (var s in suits)
        foreach (var r in ranks)
            cards.Add(new Card(r, s));

        Shuffle();
    }

    public void Shuffle() => cards = cards.OrderBy(c => rand.Next()).ToList();

    public Card DrawCard()
    {
        if (cards.Count == 0)
        {
            Shuffle();
        }
        var card = cards[0];
        cards.RemoveAt(0);
        return card;
    }
}