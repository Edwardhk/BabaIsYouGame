namespace BabaIsYouApp.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.Generic;

    class TileMapView
    {
        private MainWindow _window = Application.Current.Windows[0] as MainWindow;

        public List<List<TileView>> _tileMap;
        public TileMapView(List<List<Stack<string>>> gameState)
        {
            _tileMap = new List<List<TileView>>();
            for (int i = 0; i < gameState.Count; i++)
            {
                List<TileView> tmpList = new List<TileView>();
                for (int j = 0; j < gameState[i].Count; j++)
                {
                    TileView tmp = new TileView(gameState[i][j], j, i);
                    tmpList.Add(tmp);
                }
                _tileMap.Add(tmpList);
            }
        }

        public MainWindow GetWindow() { return _window; }

        public void UpdateTileMap(List<List<Stack<string>>> gameState)
        {
            for (int i = 0; i < gameState.Count; i++)
            {
                for (int j = 0; j < gameState[i].Count; j++)
                {
                    TileView tmp = new TileView(gameState[i][j], j, i);
                    _tileMap[i][j] = tmp;
                }
            }
        }

        public void UpdateViews()
        {
            _window.canvasMain.Children.Clear();
            for (int i = 0; i < _tileMap.Count; i++)
            {
                for (int j = 0; j < _tileMap[i].Count; j++)
                {
                    TileView tile = _tileMap[i][j];
                    Button btn = tile.GetButton();
                    //Console.WriteLine(String._format("{0}\t(L{1}, T{2})", tile.Val, tile.LeftOffSet, tile.TopOffSet));
                    Canvas.SetLeft(btn, tile.LeftOffSet);
                    Canvas.SetTop(btn, tile.TopOffSet);
                    _window.canvasMain.Children.Add(btn);
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < _tileMap.Count; i++)
            {
                for (int j = 0; j < _tileMap[i].Count; j++)
                {
                    Console.Write(_tileMap[i][j].Layer.Peek() + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
