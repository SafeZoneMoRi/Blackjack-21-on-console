namespace Porject_Gi_113
{
    public class MainGameGardener
    {
        public static void StartGardener()
        {
            string[] menuItems =
            {
                "StartGame", 
                "Rule Game", 
                "Exit"
            };
            int selected = 0;

            while (true)
            {
                Console.Clear();
                Banner.ShowMenuGardener();
                
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (i == selected)
                        Console.WriteLine("> " + menuItems[i]);
                    else
                        Console.WriteLine("  " + menuItems[i]);
                }

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.UpArrow)
                {
                    selected = (selected - 1 + menuItems.Length) % menuItems.Length;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    selected = (selected + 1) % menuItems.Length;
                }
                else if (key == ConsoleKey.Enter)
                {
                    switch (selected)
                    {
                        case 0:
                            new GardenerGame().Play();
                            break;
                        case 1:
                            ShowHowToPlay();
                            break;
                        case 2:
                            return;
                    }
                }
            }
        }

        private static void ShowHowToPlay()
        {
            Console.Clear();
            Console.WriteLine("How to play Gardener" +
                              "\n1.Press 1 - 5 select item to protect your tree" +
                              "\n2.Select item match the enemy" +
                              "\nWorm == Spray\nBeetle == Axe\nAphid == Hormone\nTornado == Rope\nHeat == Water");
            Console.WriteLine("\nMove cursor to select");
            Console.WriteLine("\nPress any key to return to the menu");
            Console.ReadKey();
        }
    }
}
