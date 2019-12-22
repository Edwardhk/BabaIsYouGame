namespace BabaIsYouApp.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using BabaIsYouApp.Models;

    class RuleController
    {
        private Dictionary<string, RuleBaseObject> _rulesDict;
        private GameStateModel _gameStateModel;
        private string[] _rulePrefix, _ruleSuffix;

        public RuleController(GameStateModel gs)
        {
            _rulesDict = new Dictionary<string, RuleBaseObject>();
            _gameStateModel = gs;

            _rulePrefix = new string[] {
                "T_BABA",
                "T_BLANK",
                "T_BALL",
                "T_ROCK",
                "T_TREE",
                "T_MOUNT",
                "T_WATER"
            };

            _ruleSuffix = new string[] {
                "T_WIN",
                "T_YOU",
                "T_KILL",
                "T_PUSH"
            };

            // Example: map["T_BABA"] = RuleBaseObject with properties like _name, _canKill, _canWin...
            foreach (string ruleName in _rulePrefix)
            {
                // Construct rule for eg. "T_BABA"
                RuleBaseObject ruleObj = new RuleBaseObject(ruleName);
                ruleObj.CanPush = true;

                // Construct rule for eg. "BABA"
                RuleBaseObject ruleUnderlyingObj = new RuleBaseObject(ruleName.Replace("T_", ""));

                _rulesDict.Add(ruleName, ruleObj);
                _rulesDict.Add(ruleName.Replace("T_", ""), ruleUnderlyingObj);
            }
            RecalculateRule();
        }

        public void RecalculateRule()
        {
            // Calculate Rule base on "IS" textbox
            List<Tuple<int, int>> res = _gameStateModel.FindAll("T_IS");

            foreach (var tuple in res)
            {
                string left = GetValue(tuple.Item1, tuple.Item2 - 1);
                string right = GetValue(tuple.Item1, tuple.Item2 + 1);
                string up = GetValue(tuple.Item1 - 1, tuple.Item2);
                string down = GetValue(tuple.Item1 + 1, tuple.Item2);
                Console.WriteLine(String.Format("[DEBUG:RuleController] Locate IS: ({0}, {1})", tuple.Item1, tuple.Item2));

                if (_rulePrefix.Contains(left) && _ruleSuffix.Contains(right))
                {
                    Console.WriteLine(String.Format("[DEBUG:RuleController] Added Rule: {0} IS {1}", left, right));

                    if (right == "T_WIN")
                        _rulesDict[left].CanWin = true;
                    if (right == "T_YOU")
                        _rulesDict[left].CanControl = true;
                    if (right == "T_KILL")
                        _rulesDict[left].CanKill = true;
                    if (right == "T_PUSH")
                        _rulesDict[left].CanPush = true;
                }
                else if (_rulePrefix.Contains(up) && _ruleSuffix.Contains(down))
                {
                    Console.WriteLine(String.Format("[DEBUG:RuleController] Added Rule: {0} IS {1}", up, down));

                    if (down == "T_WIN")
                        _rulesDict[up].CanWin = true;
                    if (down == "T_YOU")
                        _rulesDict[up].CanControl = true;
                    if (down == "T_KILL")
                        _rulesDict[up].CanKill = true;
                    if (down == "T_PUSH")
                        _rulesDict[up].CanPush = true;
                }
                else
                    return;
            }

        }

        public List<Tuple<int, int>> GetAllControllableXY()
        {
            List<Tuple<int, int>> res = new List<Tuple<int, int>>();
            List<string> controllableArr = new List<string>();

            foreach (var entry in _rulesDict)
            {
                Console.WriteLine("ENTRY: " + entry.Key);
                if (entry.Value.CanControl)
                    controllableArr.Add(entry.Key);
            }

            foreach (var controllable in controllableArr)
            {
                res.AddRange(_gameStateModel.FindAll(controllable));
            }

            foreach (var x in res)
            {
                Console.WriteLine(String.Format("[RuleController] GetAllControllableXY(): ({0}, {1})", x.Item1, x.Item2));
            }


            return res;
        }

        public string GetValue(int row, int col)
        {
            if (row < 0 || row >= _gameStateModel.GetRow())
                return "OUT_BOUND";
            if (col < 0 || col >= _gameStateModel.GetCol())
                return "OUT_BOUND";
            return _gameStateModel.GetState()[row][col].Peek();
        }
    }
}
