namespace BabaIsYouApp.Models
{
    using System;
    using System.IO;
    using System.Collections.Generic;

    public class GameStateModel
    {
        private List<List<Stack<string>>> _gameState;
        private int _nrows, _ncols;

        public GameStateModel(string fileName)
        {
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
                _gameState.Add(tmpList);
            }
        }

        public int GetRow() { return _nrows; }
        public int GetCol() { return _ncols; }

        public List<List<Stack<string>>> GetState()
        {
            return _gameState;
        }

        public List<Tuple<int, int>> FindAll(string s)
        {
            if(s != "T_IS")
                s = s.Replace("T_", "");
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
