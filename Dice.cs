using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yatzy
{
    public class Dice
    {
        private readonly Random _random;
        private int _value;
        private bool _isHeld; // Track held state
        private static readonly Random _sharedRandom = new Random();

        public Dice()
        {
            _random = _sharedRandom;
            Roll();
        }

        public int Value => _value;
        public bool IsHeld => _isHeld;

        public void Roll()
        {
            if (!_isHeld)
                _value = _random.Next(1, 7);
        }

        public void Hold()
        {
            _isHeld = true;
        }

        public void Release()
        {
            _isHeld = false;
        }
    }
    public class DiceSet
    {
        private readonly List<Dice> _dice;
        public DiceSet(int numberOfDice)
        {
            _dice = new List<Dice>();
            for (int i = 0; i < numberOfDice; i++)
            {
                _dice.Add(new Dice());
            }
        }
        
        public int[] GetValues()
        {
            return _dice.Select(d => d.Value).ToArray();
        }
        // Add a field to track held dice
        private readonly HashSet<int> _heldIndices = new HashSet<int>();

        public void HoldDice(params int[] indices)
        {
            foreach (var index in indices)
            {
                int zeroBasedIndex = index - 1; // Convert to 0-based
                if (zeroBasedIndex < 0 || zeroBasedIndex >= _dice.Count)
                    throw new ArgumentOutOfRangeException($"Die index {index} is out of range (1-{_dice.Count}).");
                _dice[zeroBasedIndex].Hold();
            }
        }

        public void RollAll()
        {
            for (int i = 0; i < _dice.Count; i++)
            {
                if (!_heldIndices.Contains(i))
                {
                    _dice[i].Roll();
                }
            }
        }
        public void ReleaseDice(int index)
        {
            _heldIndices.Remove(index);
        }
        public void ReleaseAll()
        {
            _heldIndices.Clear();
        }
        public IReadOnlyCollection<int> HeldIndices => _heldIndices;
        public int Count => _dice.Count;
    }
}