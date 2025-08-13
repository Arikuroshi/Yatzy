namespace Yatzy
{
    public class YatzyScorer
    {
        public int CalculateChance(int[] dice, int face)
        {
            return dice.Count(d => d == face) * face;
        }
        public int CalculateThreeOfAKind(int[] dice)
        {
            var threeOfAKind = dice.GroupBy(d => d).FirstOrDefault(g => g.Count() >= 3);
            return threeOfAKind != null ? threeOfAKind.Key * 3 : 0;
        }

        public int calculateFourOfAKind(int[] dice)
        {
            var fourOfAKind = dice.GroupBy(d => d).FirstOrDefault(g => g.Count() >= 4);
            return fourOfAKind != null ? fourOfAKind.Key * 4 : 0;
        }

        public int CalculateFullHouse(int[] dice)
        {
            var groups = dice.GroupBy(d => d).ToList();
            if (groups.Count == 2 && groups.Any(g => g.Count() == 3))
            {
                return groups.Sum(g => g.Key * g.Count());
            }
            return 0;
        }

        public int CalculateSmallStraight(int[] dice)
        {
            var required = new[] { 1, 2, 3, 4, 5 };
            return required.All(dice.Contains) ? 15 : 0;
        }

        public int CalculateLargeStraight(int[] dice)
        {
            var uniqueDice = new HashSet<int>(dice);
            if (uniqueDice.SetEquals(new[] { 1, 2, 3, 4, 5 }) || uniqueDice.SetEquals(new[] { 2, 3, 4, 5, 6 }))
            {
                return 20; // Sum of the large straight
            }
            return 0;
        }

        public int CalculateYatzy(int[] dice)
        {
            var yatzy = dice.GroupBy(d => d).FirstOrDefault(g => g.Count() == 5);
            return yatzy != null ? 50 : 0; // Yatzy score is 50
        }

        public int CalculateOnes(int[] dice) => dice.Where(d => d == 1).Sum();
        public int CalculateTwos(int[] dice) => dice.Where(d => d == 2).Sum();
        public int CalculateThrees(int[] dice) => dice.Where(d => d == 3).Sum();
        public int CalculateFours(int[] dice) => dice.Where(d => d == 4).Sum();
        public int CalculateFives(int[] dice) => dice.Where(d => d == 5).Sum();
        public int CalculateSixes(int[] dice) => dice.Where(d => d == 6).Sum();

        public int CalculatePair(int[] dice)
        {
            return dice.GroupBy(d => d)
                .Where(g => g.Count() >= 2)
                .OrderByDescending(g => g.Key)
                .Select(g => g.Key * 2)
                .FirstOrDefault();
        }

        public int CalculateTwoPairs(int[] dice)
        {
            var pairs = dice.GroupBy(d => d)
                .Where(g => g.Count() >= 2)
                .OrderByDescending(g => g.Key)
                .Take(2)
                .ToList();
            return pairs.Count == 2 ? pairs.Sum(g => g.Key * 2) : 0;
        }

        public int CalculateChance(int[] dice) => dice.Sum();

    }
}