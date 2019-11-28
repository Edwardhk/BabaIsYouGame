namespace BabaIsYouApp.Views
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Collections.Generic;

    using System.Windows.Media;
    using System.Windows.Media.Imaging;

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
            _window.gridMain.Children.Clear();
            _window.gridMain.Rows = _tileMap.Count;
            _window.gridMain.Columns = _tileMap[0].Count;
            for (int i = 0; i < _tileMap.Count; i++)
            {
                for (int j = 0; j < _tileMap[i].Count; j++)
                {
                    TileView tile = _tileMap[i][j];
                    Image tmpImg = tile.GetImage();
                    Grid.SetRow(tmpImg, j);
                    Grid.SetRow(tmpImg, i);
                    _window.gridMain.Children.Add(tmpImg);
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
