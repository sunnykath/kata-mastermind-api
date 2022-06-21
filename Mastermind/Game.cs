using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.Randomizer;

namespace Mastermind
{
    public class Game
    {
        private Colours[] _selectedColours;
        private readonly List<Clue> _clues;
        private bool _hasWonGame;
        
        private readonly IRandomizer _randomizer;
        
        public Game(IRandomizer randomizer)
        {
            _randomizer = randomizer;
            _selectedColours = System.Array.Empty<Colours>();
            _clues = new List<Clue>();
            _hasWonGame = false;
        }

        public void Initialise()
        {
            _selectedColours = _randomizer.GetRandomColours(Constants.SelectedNumberOfColours);
        }
        
        public void EvaluateAnswer(Colours[] predictedAnswer)
        {
            ValidateInputArray(predictedAnswer);
            
            for (var index = 0; index < _selectedColours.Length; index++)
            {
                var selectedColour = _selectedColours[index];
                
                if (!predictedAnswer.Contains(selectedColour)) continue;

                _clues.Add(predictedAnswer[index] == selectedColour ? Clue.Black : Clue.White);
            }

            UpdateGameWonStatus();
        }
        
        public Colours[] GetSelectedColours()
        {
            return _selectedColours;
        }
        
        public Clue[] GetClues()
        {
            return _clues.ToArray();
        }

        public bool HasWonGame()
        {
            return _hasWonGame;
        }
        
        private static void ValidateInputArray<T>(IReadOnlyCollection<T> inputArray)
        {
            if (inputArray.Count != Constants.SelectedNumberOfColours)
            {
                throw new Exception(Constants.InvalidNumberOfColoursExceptionMessage);
            }
        }
        
        private void UpdateGameWonStatus()
        {
            if (_clues.Count(c => c == Clue.Black) == 4)
            {
                _hasWonGame = true;
            }
        }
    }
}