namespace Porject_Gi_113
{
    public class EventManager
    {
        private Random rng = new Random();

        public string RandomEvent()
        {
            string[] events = { "Worm", "Beetle", "Aphid", "Tornado", "Heat" };
            return events[rng.Next(events.Length)];
        }

        public string GetProtect(string eventName)
        {
            return eventName switch
            {
                "Worm" => "1",
                "Beetle" => "2",
                "Aphid" => "3",
                "Tornado" => "4",
                "Heat" => "5",
                _ => "0",
            };
        }

        public int GetDamage(string eventName)
        {
            return eventName switch
            {
                "Worm" => 5,
                "Beetle" => 10,
                "Aphid" => rng.Next(10, 21),
                "Tornado" => rng.Next(50, 101),
                "Heat" => rng.Next(50, 101),
                _ => 0,
            };
        }
    }
}