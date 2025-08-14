namespace Yatzy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Yatzy game!");
            Console.WriteLine("This is a console application for playing Yatzy.");
            Console.WriteLine("You can roll dice, hold them, and score points based on the Yatzy rules.");
            Console.WriteLine("Let's get started!");
            Console.WriteLine("Please enter the amount of players who want to play: ");
            int numberOfPlayers = Console.ReadLine() is string input && int.TryParse(input, out int players) ? players : 1;

            // Ask each player for their name
            List<Player> playersList = new List<Player>();
            for (int p = 0; p < numberOfPlayers; p++)
            {
                Console.Write($"Player {p + 1}, please write your Username: ");
                string? playerName = Console.ReadLine();

                // If no name entered, assign "Unknown player" or "Unknown player N" for multiple unknowns
                if (string.IsNullOrWhiteSpace(playerName))
                {
                    playerName = numberOfPlayers == 1 ? "Unknown player" : $"Unknown player {p + 1}";
                }

                playersList.Add(new Player(playerName));
            }

            YatzyScorer scorer = new YatzyScorer();
            int maxRounds = 13;
            for (int round = 1; round <= maxRounds; round++)
            {
                Console.WriteLine($"\n--- Round {round} ---");
                foreach (var player in playersList)
                {
                    Console.WriteLine($"\n{player.GetName()}'s turn!");
                    DiceSet diceSet = new DiceSet(5);
                    int rollsLeft = 3;
                    bool hasRolled = false;
                    while (rollsLeft > 0)
                    {
                        if (!hasRolled)
                        {
                            Console.WriteLine("Press 'r' to roll the dice.");
                            string? firstUserInputInner = Console.ReadLine();
                            if (firstUserInputInner?.ToLower() == "r")
                            {
                                diceSet.RollAll();
                                hasRolled = true;
                                rollsLeft--;
                            }
                            else
                            {
                                Console.WriteLine("You must roll first.");
                            }
                            continue;
                        }
                        Console.WriteLine($"Rolls left: {rollsLeft}");
                        Console.WriteLine("Current Dice Values: " + string.Join(", ", diceSet.GetValues()));
                        Console.WriteLine("Press 'r' to roll again, 'h' to hold a die, or 's' to score.");
                        string? userInput = Console.ReadLine();
                        if (userInput?.ToLower() == "r")
                        {
                            diceSet.RollAll();
                            rollsLeft--;
                        }
                        else if (userInput?.ToLower() == "h")
                        {
                            Console.Write("Enter the indices of the dice to hold (e.g., 1,3,5): ");
                            string? indicesInput = Console.ReadLine();
                            var indices = indicesInput?.Split(',')
                                .Select(s => int.TryParse(s.Trim(), out int idx) ? idx : -1)
                                .Where(idx => idx != -1)
                                .ToArray();

                            if (indices != null && indices.Length > 0)
                            {
                                try
                                {
                                    diceSet.HoldDice(indices);
                                    Console.WriteLine($"Dice at indices {string.Join(", ", indices)} are now held.");
                                }
                                catch (ArgumentOutOfRangeException ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid indices. Please enter numbers between 1 and 5, separated by commas.");
                            }
                        }
                        else if (userInput?.ToLower() == "s")
                        {
                            break;
                        }
                    }
                    // Score after rolling/holding
                    int onesScore = scorer.CalculateOnes(diceSet.GetValues());
                    int twosScore = scorer.CalculateTwos(diceSet.GetValues());
                    int threesScore = scorer.CalculateThrees(diceSet.GetValues());
                    int foursScore = scorer.CalculateFours(diceSet.GetValues());
                    int fivesScore = scorer.CalculateFives(diceSet.GetValues());
                    int sixesScore = scorer.CalculateSixes(diceSet.GetValues());
                    int pairScore = scorer.CalculatePair(diceSet.GetValues());
                    int twoPairsScore = scorer.CalculateTwoPairs(diceSet.GetValues());
                    int chanceScore = scorer.CalculateChance(diceSet.GetValues());
                    int threeOfAKindScore = scorer.CalculateThreeOfAKind(diceSet.GetValues());
                    int fourOfAKindScore = scorer.calculateFourOfAKind(diceSet.GetValues());
                    int fullHouseScore = scorer.CalculateFullHouse(diceSet.GetValues());
                    int smallStraightScore = scorer.CalculateSmallStraight(diceSet.GetValues());
                    int largeStraightScore = scorer.CalculateLargeStraight(diceSet.GetValues());
                    int yatzyScore = scorer.CalculateYatzy(diceSet.GetValues());

                    Console.WriteLine(
                        $"Scores: Ones: {onesScore}, Twos: {twosScore}, Threes: {threesScore}, Fours: {foursScore}, Fives: {fivesScore}, Sixes: {sixesScore}, " +
                        $"Pair: {pairScore}, Two Pairs: {twoPairsScore}, Chance: {chanceScore}, Three of a Kind: {threeOfAKindScore}, Four of a Kind: {fourOfAKindScore}, " +
                        $"Full House: {fullHouseScore}, Small Straight: {smallStraightScore}, Large Straight: {largeStraightScore}, Yatzy: {yatzyScore}"
                    );
                    // Print final dice values after last roll
                    Console.WriteLine("Final Dice Values: " + string.Join(", ", diceSet.GetValues()));

                    // Calculate all possible scores
                    var allScoreOptions = new Dictionary<string, int>
                    {
                        { "Ones", scorer.CalculateOnes(diceSet.GetValues()) },
                        { "Twos", scorer.CalculateTwos(diceSet.GetValues()) },
                        { "Threes", scorer.CalculateThrees(diceSet.GetValues()) },
                        { "Fours", scorer.CalculateFours(diceSet.GetValues()) },
                        { "Fives", scorer.CalculateFives(diceSet.GetValues()) },
                        { "Sixes", scorer.CalculateSixes(diceSet.GetValues()) },
                        { "Pair", scorer.CalculatePair(diceSet.GetValues()) },
                        { "Two Pairs", scorer.CalculateTwoPairs(diceSet.GetValues()) },
                        { "Chance", scorer.CalculateChance(diceSet.GetValues()) },
                        { "Three of a Kind", scorer.CalculateThreeOfAKind(diceSet.GetValues()) },
                        { "Four of a Kind", scorer.calculateFourOfAKind(diceSet.GetValues()) },
                        { "Full House", scorer.CalculateFullHouse(diceSet.GetValues()) },
                        { "Small Straight", scorer.CalculateSmallStraight(diceSet.GetValues()) },
                        { "Large Straight", scorer.CalculateLargeStraight(diceSet.GetValues()) },
                        { "Yatzy", scorer.CalculateYatzy(diceSet.GetValues()) }
                    };

                    // Filter out used categories
                    var availableScoreOptions = allScoreOptions
                        .Where(option => !player.HasUsedCategory(option.Key))
                        .ToList();

                    if (availableScoreOptions.Count == 0)
                    {
                        Console.WriteLine("No categories left to score.");
                        continue;
                    }

                    // Display available score options
                    Console.WriteLine("Choose a category to score:");
                    int optionIndex = 1;
                    foreach (var option in availableScoreOptions)
                    {
                        Console.WriteLine($"{optionIndex}. {option.Key}: {option.Value}");
                        optionIndex++;
                    }

                    // Get player's choice
                    int chosenIndex = 0;
                    while (chosenIndex < 1 || chosenIndex > availableScoreOptions.Count)
                    {
                        Console.Write("Enter the number of your chosen category: ");
                        string? choiceInput = Console.ReadLine();
                        int.TryParse(choiceInput, out chosenIndex);
                    }

                    var chosenCategory = availableScoreOptions[chosenIndex - 1].Key;
                    var chosenScore = availableScoreOptions[chosenIndex - 1].Value;
                    player.AddScore(chosenScore);
                    player.UseCategory(chosenCategory);
                    Console.WriteLine($"{player.GetName()} scored {chosenScore} points in {chosenCategory}.");
                    Console.WriteLine($"Total Score: {player.GetTotalScore()}");
                }
            }
            Console.WriteLine("Game over! Thanks for playing Yatzy.");
            Console.WriteLine("Final Scores:");
            foreach (var player in playersList)
            {
                Console.WriteLine($"{player.GetName()}: {player.GetTotalScore()} points");
            }
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}
