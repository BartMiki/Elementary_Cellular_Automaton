using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CellularAutomata
{
    public class Automata
    {
        private CanvasHandler _canvasHandler;
        private Rule _rule;
        private bool[] _currentCells;
        private bool[] _futureCells;
        private int _iterations;
        private TimeSpan _iterationDelay;

        private Automata() { }

        public void RunAutomata()
        {
            for (int i = 0; i < _iterations; i++)
            {
                _canvasHandler.PrintIteration(_currentCells,i);
                CalculateNextIteration();
                _currentCells = _futureCells;
                _futureCells = new bool[_currentCells.Length];
                Thread.Sleep(_iterationDelay);
            }
        }

        private void CalculateNextIteration()
        {
            for (int cellId = 0; cellId < _currentCells.Length; cellId++)
            {
                _futureCells[cellId] = CalculateNextCell(cellId);
            }
        }

        private bool CalculateNextCell(int cellId)
        {
            int leftId = cellId == 0 ? _currentCells.Length - 1 : cellId - 1;
            int rightId = cellId == (_currentCells.Length - 1) ? 0 : cellId + 1;

            bool left = _currentCells[leftId];
            bool self = _currentCells[cellId];
            bool right = _currentCells[rightId];

            return _rule.GetNextStatus(left, self, right);
        }

        public class AutomataBuilder
        {
            private readonly Automata _automata;

            public AutomataBuilder() => _automata = new Automata();

            public AutomataBuilder AddCanvasHandler(CanvasHandler canvasHandler)
            {
                _automata._canvasHandler = canvasHandler;
                return this;
            }

            public AutomataBuilder AddInitialCells(bool[] cells)
            {
                _automata._currentCells = cells;
                _automata._futureCells = new bool[cells.Length];
                return this;
            }

            public AutomataBuilder AddRule(byte ruleNumber)
            {
                _automata._rule = new Rule(ruleNumber);
                return this;
            }

            public AutomataBuilder AddIterationsCount(int iterations)
            {
                _automata._iterations = iterations;
                return this;
            }

            public AutomataBuilder AddIterationDelay(TimeSpan delay)
            {
                _automata._iterationDelay = delay;
                return this;
            }

            public Automata ToAutomata()
            {
                return _automata;
            }
        }
    }
}
