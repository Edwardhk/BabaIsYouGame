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
        private bool _isGUI;

        private MainWindow _window = Application.Current.Windows[0] as MainWindow;

        public List<List<TileView>> _tileMap;
        public TileMapView(List<List<Stack<string>>> gameState)
        {
            _isGUI = true;

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

        public List<List<TileView>> GetTileMap()
        {
            return _tileMap;
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
                    _window.gridMain.Focusable = true;
                    _window.gridMain.Focus();
                    TileView tile = _tileMap[i][j];
                    if (_isGUI)
                    {
                        Image tmpImg = tile.GetImage();
                        Grid.SetRow(tmpImg, i);
                        Grid.SetColumn(tmpImg, j);
                        _window.gridMain.Children.Add(tmpImg);
                    }
                    else
                    {
                        Label tmpLb = tile.GetLabel();
                        Grid.SetRow(tmpLb, i);
                        Grid.SetColumn(tmpLb, j);
                        _window.gridMain.Children.Add(tmpLb);
                    }
                }
            }
        }

        public void UpdateKillingViews(List<Tuple<int, int>> tuples)
        {
            foreach (var tuple in tuples)
            {
                _tileMap[tuple.Item1][tuple.Item2].Layer.Push("T_KILL");
            }
            UpdateViews();
        }

        public void UpdateWinningViews(List<Tuple<int, int>> tuples)
        {
            foreach(var tuple in tuples)
            {
                _tileMap[tuple.Item1][tuple.Item2].Layer.Push("T_WIN");
            }
            UpdateViews();
        }

        public void SwitchGUIMode(object sender, RoutedEventArgs e)
        {
            _isGUI = !_isGUI;
            UpdateViews();
        }
    }
}
