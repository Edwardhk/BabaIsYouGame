namespace BabaIsYouApp.Controllers
{
    using System;
    using System.Linq;
    using System.Windows.Input;
    using System.Windows.Controls;
    using System.Collections.Generic;
    using System.Windows.Controls.Primitives;

    using BabaIsYouApp.Views;
    using BabaIsYouApp.Models;

    class MainController
    {
        // -Views- //
        private UniformGrid _gridMain;
        private TileMapView _tileMapView;

        // -Model- //
        private GameStateModel _gameStateModel;

        // -Controller- //
        // Keep track of multiple properties, eg. T_BABA { _canWin: true, _canPush: false ... }
        private Dictionary<string, RuleBaseObject> _rulesDict;
        private string[] _rulePrefix, _ruleSuffix;

        private KillRuleController _killRuleController;

        public MainController(GameStateModel gs, TileMapView tm)
        {
            // Model
            _gameStateModel = gs;

            // Internal Controllers
            ResetRules();
            RecalculateRule();
            _killRuleController = new KillRuleController(_gameStateModel);

            // Views
            _tileMapView = tm;
            _gridMain = _tileMapView.GetWindow().gridMain;
            _gridMain.KeyDown += new KeyEventHandler(HandleKeyboardInput);
        }

        private void ResetRules()
        {
            _rulesDict = new Dictionary<string, RuleBaseObject>();

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
                "T_PUSH",
                "T_STOP"
            };

            // Example: map["T_BABA"] = RuleBaseObject with properties like _name, _canKill, _canWin...
            foreach (string ruleName in _rulePrefix)
            {
                // FOR rule text object like "T_BABA"
                _rulesDict.Add(ruleName, new RuleBaseObject(ruleName));
                _rulesDict[ruleName].CanPush = true;

                // FOR underlying object like "BABA"
                _rulesDict.Add(ruleName.Replace("T_", ""), new RuleBaseObject(ruleName.Replace("T_", "")));
            }

            foreach (string ruleName in _ruleSuffix)
            {
                _rulesDict.Add(ruleName, new RuleBaseObject(ruleName));
                _rulesDict[ruleName].CanPush = true;
            }

            _rulesDict.Add("T_IS", new RuleBaseObject("T_IS"));
            _rulesDict["T_IS"].CanPush = true;
        }

        // Wrapper for outbound variables
        private string GetValue(int row, int col)
        {
            if (row < 0 || row >= _gameStateModel.GetState().Count)
                return "OUT_BOUND";
            if (col < 0 || col >= _gameStateModel.GetState()[row].Count)
                return "OUT_BOUND";

            return _gameStateModel.GetState()[row][col].Peek();
        }

        private void RecalculateRule()
        {
            Console.WriteLine("[DEBUG:MainController] RecalculateRule()");
            // Calculate Rule base on "IS" textbox
            List<Tuple<int, int>> res = _gameStateModel.FindAll("T_IS");

            // Only two combinations are allowed, i.e. Left & Right, Up & Down
            foreach (var tuple in res)
            {
                string left = GetValue(tuple.Item1, tuple.Item2 - 1);
                string right = GetValue(tuple.Item1, tuple.Item2 + 1);
                string up = GetValue(tuple.Item1 - 1, tuple.Item2);
                string down = GetValue(tuple.Item1 + 1, tuple.Item2);


                Console.WriteLine(String.Format("[DEBUG:MainController] Locate IS: ({0}, {1})", tuple.Item1, tuple.Item2));

                if (_rulePrefix.Contains(left) && _ruleSuffix.Contains(right))
                {
                    Console.WriteLine(String.Format("[DEBUG:MainController] Added LR Rule: {0} IS {1}", left, right));
                    left = left.Replace("T_", "");
                    if (right == "T_WIN")
                        _rulesDict[left].CanWin = true;
                    if (right == "T_YOU")
                        _rulesDict[left].CanControl = true;
                    if (right == "T_KILL")
                        _rulesDict[left].CanKill = true;
                    if (right == "T_PUSH")
                        _rulesDict[left].CanPush = true;
                    if (right == "T_STOP")
                        _rulesDict[left].CanStop = true;
                }

                if (_rulePrefix.Contains(up) && _ruleSuffix.Contains(down))
                {
                    Console.WriteLine(String.Format("[DEBUG:MainController] Added UD Rule: {0} IS {1}", up, down));
                    up = up.Replace("T_", "");
                    if (down == "T_WIN")
                        _rulesDict[up].CanWin = true;
                    if (down == "T_YOU")
                        _rulesDict[up].CanControl = true;
                    if (down == "T_KILL")
                        _rulesDict[up].CanKill = true;
                    if (down == "T_PUSH")
                        _rulesDict[up].CanPush = true;
                    if (down == "T_STOP")
                        _rulesDict[up].CanStop = true;
                }
            }


            /// TESTING ///
            _rulesDict["ROCK"].CanPush = true;
            /// END TESTING ///
            return;
        }

        public List<Tuple<int, int>> FindAllControllableXY()
        {
            List<Tuple<int, int>> res = new List<Tuple<int, int>>();

            foreach (var dict in _rulesDict)
            {
                if (dict.Value.CanControl)
                {
                    Console.WriteLine(String.Format("[DEBUG:MainController] Found Controllable: {0}", dict.Key));
                    res.AddRange(_gameStateModel.FindAll(dict.Key));
                }
            }

            return res;
        }


        private void HandleKeyboardInput(object sender, KeyEventArgs e)
        {
            Console.WriteLine("HandleKeyboardInput()");
            if (e.Key >= Key.Left && e.Key <= Key.Down)
                HandleMovement(e.Key);
            else if (e.Key == Key.U)
                HandleUndoMove();
        }

        private void HandleMovement(Key key)
        {
            ResetRules();
            RecalculateRule();
            List<Tuple<int, int>> res = FindAllControllableXY();
            List<List<Stack<string>>> target = _gameStateModel.GetState();

            for (int i = 0; i < res.Count; i++)
            {
                int srcRow = res[i].Item1;
                int srcCol = res[i].Item2;
                if (key == Key.Left)
                    target = MoveSrcToDest(target, srcRow, srcCol, srcRow, srcCol - 1, 'L');
                else if (key == Key.Right)
                    target = MoveSrcToDest(target, srcRow, srcCol, srcRow, srcCol + 1, 'R');
                else if (key == Key.Up)
                    target = MoveSrcToDest(target, srcRow, srcCol, srcRow - 1, srcCol, 'U');
                else if (key == Key.Down)
                    target = MoveSrcToDest(target, srcRow, srcCol, srcRow + 1, srcCol, 'D');
            }

            _tileMapView.UpdateTileMap(target);
            _tileMapView.UpdateViews();
        }

        private List<List<Stack<string>>> MoveSrcToDest(List<List<Stack<string>>> target, int srcRow, int srcCol,
                                            int destRow, int destCol, char dir)
        {
            List<List<Stack<string>>> res = target;

            // If movement leads to out-bound value, fix the movement to the bound
            if (destRow < 0) destRow = 0;
            if (destCol < 0) destCol = 0;
            if (destRow >= res.Count) destRow = res.Count - 1;
            if (destCol >= res[0].Count) destCol = res[0].Count - 1;
            Console.WriteLine(String.Format("MoveSrcToDest({0}, {1}, {2}, {3})", srcRow, srcCol, destRow, destCol));

            // If stepping on same tile
            if (destRow == srcRow && destCol == srcCol)
                return target;

            //// Prevent duplicated layer
            //if(target[destRow][destCol].Peek() != target[srcRow][srcCol].Peek())

            // Dealing with actual rule
            RuleBaseObject destRule = _rulesDict[res[destRow][destCol].Peek()];
            if (destRule.CanWin)
            {
                HandleWinning();
            }
            else if (destRule.CanKill)
            {
                HandleKilling();
            }
            else if (destRule.CanPush)
            {
                bool thisBlockIsPushable = false;
                List<Tuple<int, int>> potentialPushableXY = new List<Tuple<int, int>>();
                List<Tuple<int, int, int, int>> potentialPushableXYs = new List<Tuple<int, int, int, int>>();
                switch (dir)
                {
                    case 'L':
                        for (int i = destCol; i >= 0; i--)
                        {
                            string currentStep = target[destRow][i].Peek();
                            // Arrive the critical block that affecting the previous blocks's pushing logic
                            if (!_rulesDict[currentStep].CanPush)
                            {
                                thisBlockIsPushable = !_rulesDict[currentStep].CanStop;
                                break;
                            }
                            else
                                potentialPushableXYs.Add(new Tuple<int, int, int, int>(destRow, i, destRow, i - 1));
                        }
                        break;
                    case 'R':
                        for (int i = destCol; i < target.Count; i++)
                        {
                            string currentStep = target[destRow][i].Peek();
                            if (!_rulesDict[currentStep].CanPush)
                            {
                                thisBlockIsPushable = !_rulesDict[currentStep].CanStop;
                                break;
                            }
                            else
                                potentialPushableXYs.Add(new Tuple<int, int, int, int>(destRow, i, destRow, i + 1));
                        }
                        break;
                    case 'U':
                        for (int i = destRow; i >= 0; i--)
                        {
                            string currentStep = target[i][destCol].Peek();
                            if (!_rulesDict[currentStep].CanPush)
                            {
                                thisBlockIsPushable = !_rulesDict[currentStep].CanStop;
                                break;
                            }
                            else
                                potentialPushableXYs.Add(new Tuple<int, int, int, int>(i, destCol, i - 1, destCol));
                        }
                        break;
                    case 'D':
                        for (int i = destRow; i < target.Count; i++)
                        {
                            string currentStep = target[i][destCol].Peek();
                            if (!_rulesDict[currentStep].CanPush)
                            {
                                thisBlockIsPushable = !_rulesDict[currentStep].CanStop;
                                break;
                            }
                            else
                                potentialPushableXYs.Add(new Tuple<int, int, int, int>(i, destCol, i + 1, destCol));
                        }
                        break;
                }

                if (thisBlockIsPushable)
                {
                    foreach (var tuple in potentialPushableXYs)
                    {
                        int srcX = tuple.Item1;
                        int srcY = tuple.Item2;
                        int destX = tuple.Item3;
                        int destY = tuple.Item4;
                        Console.WriteLine("PUSH: " + target[srcX][srcY].Peek());

                        res[destX][destY].Push(res[srcX][srcY].Peek());
                        if (res[srcX][srcY].Count != 1)
                            res[srcX][srcY].Pop();
                    }
                    // And the character
                    res[destRow][destCol].Push(res[srcRow][srcCol].Peek());
                    if (res[srcRow][srcCol].Count != 1)
                        res[srcRow][srcCol].Pop();
                }
            }
            else if(!destRule.CanStop)
            {
                res[destRow][destCol].Push(res[srcRow][srcCol].Peek());
                if (res[srcRow][srcCol].Count != 1)
                    res[srcRow][srcCol].Pop();
            }

            return res;
        }

        private void HandleUndoMove()
        {
            Console.WriteLine("UNDO");

        }
        private void HandleKilling()
        {
            Console.WriteLine("KILLED");

        }
        private void HandleWinning()
        {
            Console.WriteLine("WON");
        }
    }
}
