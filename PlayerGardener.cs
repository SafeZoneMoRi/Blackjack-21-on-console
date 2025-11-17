namespace Porject_Gi_113
{
    public class PlayerGardener
    {
        public string Name { get; set; }

        public int CalculateScore(int treeHp)
        {
            if (treeHp >= 81) return 100;
            if (treeHp >= 71) return 80;
            if (treeHp >= 61) return 65;
            if (treeHp >= 51) return 50;
            if (treeHp >= 10) return 10;
            return 0;
        }
    }
}