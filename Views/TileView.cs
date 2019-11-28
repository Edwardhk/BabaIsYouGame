namespace BabaIsYouApp.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    class TileView
    {
        private Stack<string> _layer;
        private int _leftOffSet;
        private int _topOffSet;
        private int _width;
        private int _height;

        public TileView(Stack<string> layer, int x, int y)
        {
            _height = 40;
            _width = 40;
            _layer = layer;
            _leftOffSet = x * _height;
            _topOffSet = y * _width;
        }

        public int LeftOffSet { get { return _leftOffSet; } }
        public int TopOffSet { get { return _topOffSet; } }
        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public Stack<string> Layer { get { return _layer; } }

        public Button GetButton()
        {
            Button res = new Button();

            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@"../Assets/TileMap/" + _layer.Peek() + ".png", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();

            Image img = new Image();
            img.Stretch = Stretch.Fill;
            img.Source = bitimg;

            res.Content = img;
            res.Background = new ImageBrush(bitimg);
            res.Height = _height;
            res.Width = _width;
            res.BorderBrush = new SolidColorBrush(Colors.Transparent);

            return res;
        }
    }
}
