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

        public Label GetLabel()
        {
            Label res = new Label();

            string target = "";

            switch (_layer.Peek())
            {
                // OBJECT
                case "BABA":
                    target = "/\\\n\\/"; break;
                case "ROCK":
                    target = "OO\nOO"; break;
                case "WATER":
                    target = "~~\n~~"; break;
                case "BLANK":
                    target = "  \n  "; break;
                case "TREE":
                    target = "~~\n||"; break;
                case "BALL":
                    target = "@@\n@@"; break;

                // PREFIX
                case "T_BABA":
                    target = "BA\nBA"; break;
                case "T_ROCK":
                    target = "RO\nCK"; break;
                case "T_TREE":
                    target = "TR\nEE"; break;
                case "T_BALL":
                    target = "BA\nLL"; break;

                case "T_IS":
                    target = "I \n S"; break;

                // SUFFIX
                case "T_PUSH":
                    target = "PU\nSH"; break;
                case "T_STOP":
                    target = "ST\nOP"; break;
                case "T_WIN":
                    target = "W \nIN"; break;
                case "T_YOU":
                    target = "Y \nOU"; break;
                default:
                    target = _layer.Peek(); break;
            }

            res.FontFamily = new FontFamily("Consolas");
            res.Content = target;
            res.Foreground = Brushes.Green;

            return res;
        }
    }
}
