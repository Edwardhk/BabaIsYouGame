﻿namespace BabaIsYouApp.Views
{
    using System;
    using System.Collections.Generic;
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

        // TODO: Set GetImage to Multiple layer return, thus creating layered tile
        public Image GetImage()
        {
            BitmapImage bitimg = new BitmapImage();
            bitimg.BeginInit();
            bitimg.UriSource = new Uri(@"../Assets/TileMap/" + _layer.Peek() + ".png", UriKind.RelativeOrAbsolute);
            bitimg.EndInit();

            Image img = new Image();
            img.Stretch = Stretch.Fill;
            img.Source = bitimg;
            img.Width = 40;
            img.Height = 40;

            return img;
        }
    }
}
