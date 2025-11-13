namespace Porject_Gi_113;

public class Hand
{
    public List<Card> Cards { get; } = new ();

    public void AddCard(Card card) => Cards.Add(card);

    public int GetTotal()
    {
        int total = Cards.Sum(c => c.Value);
        int aceCount = Cards.Count(c => c.Rank == "A");

        while (total > 21 && aceCount > 0)
        {
            total -= 10;
            aceCount--;
        }
        return total;
    }
}