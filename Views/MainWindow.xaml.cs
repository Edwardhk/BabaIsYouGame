using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace BabaIsYouApp.Views
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using BabaIsYouApp.Controllers;
    using BabaIsYouApp.Models;
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameStateModel gs = new GameStateModel("../../Assets/Levels/DEMO.txt");
            TileMapView tm = new TileMapView(gs.GetState());
            tm.UpdateViews();
            MainController mc = new MainController(gs, tm);
        }
    }
}
