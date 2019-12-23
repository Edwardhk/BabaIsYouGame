namespace BabaIsYouApp.Views
{
    using System;
    using System.Media;
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

            GameStateModel gs = new GameStateModel("../../Assets/Levels/L1.txt");
            TileMapView tm = new TileMapView(gs.GetState());
            tm.UpdateViews();
            MainController mc = new MainController(gs, ref tm);

            SoundPlayer player = new SoundPlayer("../../Assets/Audio/LittleRoot.wav");
            player.Load();
            player.Play();
        }
        private void FocusGrid(object sender, RoutedEventArgs e)
        {
            gridMain.Focus();
        }
    }
}
