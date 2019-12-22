namespace BabaIsYouApp.Views
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    class TileView
    {
        private Stack<string> _layer;
        private int _width;
        private int _height;

        public TileView(Stack<string> layer, int x, int y)
        {
            _height = 40;
            _width = 40;
            _layer = layer;
        }

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public Stack<string> Layer { get { return _layer; } }

        public Image GetImage()
        {
            var group = new DrawingGroup();
            List<string> layerStrs = new List<string>();
            // Reverse the stack as the image stack from the bottom
            var enumerator = _layer.GetEnumerator();
            while (enumerator.MoveNext())
                layerStrs.Add(enumerator.Current);
            layerStrs.Reverse();

            foreach(string s in layerStrs)
                group.Children.Add(new ImageDrawing(new BitmapImage(new Uri(@"../../Assets/TileMap/" + s + ".png", UriKind.RelativeOrAbsolute)), new Rect(0, 0, 40, 40)));

            Image img = new Image();
            img.Stretch = Stretch.Fill;
            img.Source = new DrawingImage(group);
            img.Width = _width;
            img.Height = _height;

            return img;
        }
    }
}
