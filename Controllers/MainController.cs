namespace BabaIsYouApp.Controllers
{
    using System;
    using System.Windows.Input;
    using System.Windows.Controls;
    using System.Collections.Generic;

    using BabaIsYouApp.Views;
    using BabaIsYouApp.Models;

    class MainController
    {
        private Canvas _canvasMain;
        private GameStateModel _gameStateModel;
        private TileMapView _tileMapView;
        

        public MainController(GameStateModel gs, TileMapView tm)
        {
            _gameStateModel = gs;
            _tileMapView = tm;
            _canvasMain = _tileMapView.GetWindow().canvasMain;
            _canvasMain.KeyDown += new KeyEventHandler(HandleKeyboardInput);
        }

        private void HandleKeyboardInput(object sender, KeyEventArgs e)
        {
            Console.WriteLine("HandleKeyboardInput()");
            _canvasMain.Focus();
            if (e.Key >= Key.Left && e.Key <= Key.Down)
                HandleMovement(e.Key);
            else if (e.Key == Key.U)
                HandleUndoMove();
        }

        private void HandleMovement(Key key)
        {
            List<Tuple<int, int>> res = _gameStateModel.FindAll("BABA");
            List<List<Stack<string>>> target = _gameStateModel.GetState();

            for(int i=0; i<res.Count; i++)
            {
                Console.WriteLine(String.Format("LOCATE BABA: ({0}, {1})", res[i].Item1, res[i].Item2));
            }

            switch (key)
            {
                case Key.Left:
                    for (int i = 0; i < res.Count; i++)
                        target = Swap(target, res[i].Item1, res[i].Item2, res[i].Item1, res[i].Item2 - 1);
                    _tileMapView.UpdateTileMap(target);
                    _tileMapView.UpdateViews();
                    break;
                case Key.Right:
                    break;
                case Key.Up:
                    break;
                case Key.Down:
                    break;
            }
        }

        private List<List<Stack<string>>> Swap(List<List<Stack<string>>> target, int srcRow, int srcCol,
                                            int destRow, int destCol)
        {
            List<List<Stack<string>>> res = target;

            if (destRow < 0) destRow = 0;
            if (destCol < 0) destCol = 0;
            if (destRow >= target.Count) destRow = target.Count;
            if (destCol >= target[0].Count) destCol = target[0].Count;

            target[destRow][destCol].Push(target[srcRow][srcCol].Peek());

            if(target[srcRow][srcCol].Count != 1)
                target[srcRow][srcCol].Pop();

            return res;
        }

        private void HandleUndoMove()
        {
            Console.WriteLine("UNDO");

        }
    }
}
