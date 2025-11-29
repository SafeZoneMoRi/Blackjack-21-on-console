namespace Porject_Gi_113
{
    public class EventManager
    {
        private Random rng = new Random();

        public string RandomEvent()
        {
            string[] events = { 
                "Worm", 
                "Beetle", 
                "Aphid", 
                "Tornado", 
                "Heat" 
            };
            return events[rng.Next(events.Length)];
        }

        public string GetProtect(string eventName)
        {
            return eventName switch
            {
                "Worm"    => "Spray",
                "Beetle"  => "Axe",
                "Aphid"   => "Hormone",
                "Tornado" => "Rope",
                "Heat"    => "Water",
                _ => ""
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
