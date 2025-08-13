namespace Yatzy
{
    public class Player
    {
        public string? Name { get; set; }
        public int TotalScore { get; set; }

        public Player(string name)
        {
            Name = name;
            TotalScore = 0;
        }
        public void AddScore(int score)
        {
            TotalScore += score;
        }
        public override string ToString()
        {
            return $"{Name} - Total Score: {TotalScore}";
        }
        public List<string> GetScoreBoard()
        {
            return new List<string> { $"{Name} - Total Score: {TotalScore}" };
        }
        public void ResetScore()
        {
            TotalScore = 0;
        }
        public void Reset()
        {
            Name = null;
            TotalScore = 0;
        }
        public void SetName(string name)
        {
            Name = name;
        }
        public string GetName()
        {
            return Name ?? "Unknown Player";
        }
        public void SetTotalScore(int score)
        {
            TotalScore = score;
        }
        public int GetTotalScore()
        {
            return TotalScore;
        }
        public void UpdateScore(int score)
        {
            TotalScore += score;
        }
        public void ClearScore()
        {
            TotalScore = 0;
        }
        public void DisplayScore()
        {
            Console.WriteLine($"{Name} - Total Score: {TotalScore}");
        }
        public void DisplayScoreBoard()
        {
            Console.WriteLine($"{Name} - Total Score: {TotalScore}");
        }
        public void AddToScoreBoard(List<string> scoreBoard)
        {
            scoreBoard.Add($"{Name} - Total Score: {TotalScore}");
        }
    }
}