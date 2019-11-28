namespace BabaIsYouApp.Models
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    class GameStateModel
    {
        private List<string[]> _currentState;
        private List<List<Stack<string>>> _gameState;
        private int _nrows, _ncols;

        public GameStateModel(string fileName)
        {
            _currentState = new List<string[]>();
            _gameState = new List<List<Stack<string>>>();

            var lines = File.ReadAllLines(fileName);
            _nrows = Convert.ToInt32(lines[0]);
            _ncols = Convert.ToInt32(lines[1]);

            for (var i = 2; i < lines.Length; i++)
            {
                string[] words = lines[i].Split('\t');
                List<Stack<string>> tmpList = new List<Stack<string>>();
                foreach (string s in words)
                {
                    Stack<string> tmpStack = new Stack<string>();
                    tmpStack.Push("BLANK"); // Blank as the base layer
                    tmpStack.Push(s);
                    tmpList.Add(tmpStack);
                }
                _currentState.Add(words);
                _gameState.Add(tmpList);
            }
        }

        public GameStateModel(GameStateModel copy)
        {
            _currentState = copy._currentState;
            _nrows = copy._nrows;
            _ncols = copy._ncols;
        }

        public GameStateModel(List<string[]> copy)
        {
            _currentState = copy;
        }

        public void Print()
        {
            foreach (var state in _currentState)
            {
                foreach (var s in state)
                {
                    Console.WriteLine(s);
                }
                Console.WriteLine();
            }
        }

        public List<List<Stack<string>>> GetState()
        {
            return _gameState;
        }

        public bool IsValidMove(string s, int row, int col, char dir)
        {
            switch (dir)
            {
                case 'L':
                    return (col - 1 >= 0);
                case 'R':
                    return (col + 1 < _currentState[0].Length);
                case 'U':
                    return (row - 1 >= 0);
                case 'D':
                    return (row + 1 < _currentState.Count);
                default:
                    return false;
            }
        }

        public List<Tuple<int, int>> FindAll(string s)
        {
            List<Tuple<int, int>> res = new List<Tuple<int, int>>();

            for (var i = 0; i < _gameState.Count; i++)
            {
                for (var j = 0; j < _gameState[i].Count; j++)
                {
                    if (s == _gameState[i][j].Peek())
                    {
                        res.Add(Tuple.Create(i, j));
                    }
                }
            }

            return res;
        }
    }
}
