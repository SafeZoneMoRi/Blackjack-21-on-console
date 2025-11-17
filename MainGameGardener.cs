namespace Porject_Gi_113
{
    public class MainGameGardener
    {
        public static void StartGardener()
        {
            Console.Title = "Gardener";

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Select an option:" +
                                  "\nPress 1 for play" +
                                  "\nPress 2 for Rule" +
                                  "\nPress 3 for exit");
                Console.WriteLine("Please select an option: ");
                bool checkNum = int.TryParse(Console.ReadLine(), out int numSelect);

                if (!checkNum)
                {
                    Console.WriteLine("Incorrect option. Please enter 1, 2, or 3.");
                    Console.ReadKey();
                    continue;
                }

                switch (numSelect)
                {
                    case 1:
                        new GardenerGame().Play();
                        break;
                    case 2:
                        ShowHowToPlay();
                        break;
                    case 3:
                        return;
                    default:
                        Console.WriteLine("Incorrect option. Please enter 1, 2, or 3.");
                        Console.ReadKey();
                        break;
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
            Console.WriteLine("\nPress 1 == Spray\nPress 2 == Axe\nPress 3 == Hormone\nPress 4 == Rope\nPress 5 == Water");
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadKey();
        }
    }
}