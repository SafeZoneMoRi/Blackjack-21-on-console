namespace Porject_Gi_113;

public class MainGameGardener
{
    public static void StartGardener()
    {
        Console.Title = "Gardener";
        MenuGardener();
    }

    static void MenuGardener()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("Select an option: "+"\nPress 1 for play"+"\nPress 2 for Rule"+"\nPress 3 for exit");
            Console.WriteLine("Please select an option: ");
            bool checkNum = int.TryParse(Console.ReadLine(), out int numSelect);
            
            switch (numSelect)
            {
                case 1: InGardener();
                    break;
                case 2: HowToPlay();
                    break;
                case 3: 
                    return;
                default:
                    Console.WriteLine("Incorrect option. Please enter 1 or 2.");
                    break;
            }
        }
    }

    static void HowToPlay()
    {
        Console.Clear();
        Console.WriteLine("How to play Gardener"
                          +"\n1.Press 1 - 5 select item to protect you tree"
                          +"\n2.Select item match the enemy"
                          +"\nWorm == Spray"+"\nBeetle == Axe"+"\nAphid == Hormone"+"\nTornado == Rope"+"\nHeat == Water");
        Console.WriteLine("\nPress 1 == Spray"+"\nPress 2 == Axe"+"\nPress 3 == Hormone"+"\nPress 4 == Rope"+"\nPress 5 == Water");
        Console.WriteLine("Press any key to return to the menu");
        Console.ReadKey();
    }
        
    static void InGardener()
    {
        int treeHp = 100;
        Console.WriteLine("Gardener");
        Console.Write("Enter your name: ");
        string playerName = Console.ReadLine();

        for (int day = 1; day <= 7; day++)
        {
            Console.Clear();
            Console.WriteLine($"Day {day}");
            Console.WriteLine($"Tree hp: {treeHp}");

            string eventName = RandomEvent();
            int timeLimit = 10;
                
            Console.WriteLine($"Event: {eventName}");
            Console.WriteLine($"You have time {timeLimit} seconds");
                
            string correctTool = GetProtect(eventName);
            Console.WriteLine($"Item Press 1 == Spray"+"\nPress 2 == Axe"+"\nPress 3 == Hormone"+"\nPress 4 == Rope"+"\nPress 5 == Water");
            Console.WriteLine($"Select Tool protect you tree: ");
                
            string selectItem = Console.ReadLine();
            if (selectItem == correctTool)
            {
                Console.WriteLine($"Defense success");
            }
            else 
            {
                int enemyDam = GetDamage(eventName);
                treeHp -= enemyDam;
                Console.WriteLine($"\nEnemy hit");
                Console.WriteLine($"\nTree crying :{treeHp}");
            }
            if (treeHp <= 0)
            {
                treeHp = 0;
                Console.WriteLine($"Tree die {treeHp}");
                break;
            }
                
        }
        Console.WriteLine($"Out of day");
        Console.WriteLine($"Tree hp: {treeHp}");
            
        int score = KeepPlayerScore(treeHp);
        Console.WriteLine($"You Score: {score}");
        Console.ReadKey();
    }
        
        
    static string RandomEvent()
    {
        Random enemy = new Random();
        string[] eventName = { "Worm", "Beetle", "Aphid", "Tornado", "Heat" };
        return eventName[enemy.Next(eventName.Length)];
    }
        
    static string GetProtect(string eventEnemy)
    {
        switch (eventEnemy)
        {
            case "Worm":
                return "1";
            case "Beetle":
                return "2";
            case "Aphid":
                return "3";
            case "Tornado":
                return "4";
            case "Heat":
                return "5";
            default:
                return "0";
        }
    }

    static int GetDamage(string eventEnemy)
    {
        Random enemy = new Random();
        switch (eventEnemy)
        {
            case "Worm":
                return 5;
            case "Beetle":
                return 10;
            case "Aphid":
                return enemy.Next(10,21);
            case "Tornado":
                return enemy.Next(50,101);
            case "Heat":
                return enemy.Next(50,101);
            default:
                return 0;
        }
    }

    static int KeepPlayerScore(int treeHp)
    {
        if (treeHp >= 81)
        {
            return 100;
        }
        else if (treeHp >= 71)
        {
            return 80;
        }
        else if (treeHp >= 61)
        {
            return 65;
        }
        else if (treeHp >= 51)
        {
            return 50;
        }
        else if (treeHp >= 10)
        {
            return 10;
        }
        else
        {
            return 0;
        }
    }
}