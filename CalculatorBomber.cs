namespace Porject_Gi_113
{
    public static class Calculator
    {
        static string leaderboardFile = "leaderboard.txt";

        public static int CalculatorScore(int miss)
        {
            if (miss == 2) return 5;
            if (miss == 1) return 7;
            if (miss == 0) return 10;
            return 0;
        }

        public static void SaveScore(string playerName, int score)
        {
            if (score <= 0) return;
            string playerScore = $"{playerName} : {score}";

            if (!File.Exists(leaderboardFile))
                File.WriteAllText(leaderboardFile, "");

            File.AppendAllText(leaderboardFile, playerScore + Environment.NewLine);
        }

        public static void ShowLeaderboard()
        {
            if (!File.Exists(leaderboardFile))
                File.WriteAllText(leaderboardFile, "");

            string[] lines = File.ReadAllLines(leaderboardFile);

            if (lines.Length == 0)
            {
                Console.WriteLine("No scores yet.");
                return;
            }

            var scores = new List<(string Name, int Score)>();
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int score))
                {
                    scores.Add((parts[0].Trim(), score));
                }
            }

            if (scores.Count == 0)
            {
                Console.WriteLine("No valid scores yet.");
                return;
            }

            var sorted = scores.OrderByDescending(s => s.Score).ToList();

            Console.WriteLine("=== Leaderboard ===");
            int rank = 1;
            foreach (var entry in sorted)
            {
                Console.WriteLine($"{rank}. {entry.Name} : {entry.Score}");
                rank++;
            }
        }
        public static void ResetLeaderboard()
        {
            if (File.Exists(leaderboardFile))
                File.WriteAllText(leaderboardFile, "");
            Console.WriteLine("Leaderboard cleared!");
        }
    }
}
