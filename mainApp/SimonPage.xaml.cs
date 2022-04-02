using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace mainApp
{
    public sealed partial class SimonPage : Page
    {
        List<Grid> ReadGrids = new List<Grid>();
        List<Grid> WriteGrids = new List<Grid>();
        Grid[] grids = new Grid[4];
        private bool hold;
        private bool volumeBool = true;
        public SimonPage()
        {
            InitializeComponent();
            grids[0] = green;
            grids[1] = yellow;
            grids[2] = red;
            grids[3] = blue;
            Sleep();
        }

        private void startGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Window.Current.Content = new MainMenu();
            Window.Current.Activate();
        }

        async void Sleep()
        {
            for (int i = 3; i > 0; i--)
            {
                Score.Text =  i.ToString();
                hertz900.Play();
                await Task.Delay(TimeSpan.FromSeconds(0.1));
                hertz900.Stop();
                await Task.Delay(TimeSpan.FromSeconds(0.9));
            }
            Score.Text = "0";
            hertz1300.Play();
            Game();
            await Task.Delay(TimeSpan.FromSeconds(0.3));
            hertz1300.Stop();

            
        }

        async private void Game()
        {
            await Task.Delay(TimeSpan.FromSeconds(0.5));
            Random();
        }
        private void Random()
        {
            Random rnd = new Random();
            ReadGrids.Add(grids[rnd.Next(0, 4)]);
            DisplayGrid();
        }
        async private void DisplayGrid()
        {
            green.IsTapEnabled = false;
            red.IsTapEnabled = false;
            yellow.IsTapEnabled = false;
            blue.IsTapEnabled = false;
            foreach(Grid grid in ReadGrids)
            {
                MediaElement note = (MediaElement)VisualTreeHelper.GetChild(grid, 0);
                await Task.Delay(TimeSpan.FromSeconds(1));
                if (volumeBool) note.Play();
                grid.Opacity = 0.7;
                await Task.Delay(TimeSpan.FromSeconds(1)); 
                grid.Opacity = 1;
                note.Stop();
                
            }
            Answer();
        }
        void Answer()
        {
            green.IsTapEnabled = true;
            red.IsTapEnabled = true;
            yellow.IsTapEnabled = true;
            blue.IsTapEnabled = true;
            if (WriteGrids.Count>0)
            {
                if (WriteGrids[WriteGrids.Count - 1] == ReadGrids[WriteGrids.Count - 1])
                {

                    if (WriteGrids.Count == ReadGrids.Count)
                    {
                        WriteGrids.Clear();
                        if (ReadGrids.Count < 10)
                            Score.Text =  ReadGrids.Count.ToString();
                        else
                            Score.Text =  ReadGrids.Count.ToString();
                        Game();

                    }
                }
                else
                {
                    StartGridText.Text = "המשחק נגמר,\n קיבלת " + Score.Text + " נקודות." + "\nלחץ לתפריט הראשי";
                    StartGrid.Visibility= Visibility;
                    green.IsTapEnabled = false;
                    red.IsTapEnabled = false;
                    yellow.IsTapEnabled = false;
                    blue.IsTapEnabled = false;
                    
                }
            }
        }

        #region tapped
        private void grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Grid grid = ((Grid) sender);
            WriteGrids.Add(grid);
            Click(grid);

            Answer();
        }
        
        async void Click(Grid tappedGrid)
        {
            MediaElement note = (MediaElement) VisualTreeHelper.GetChild(tappedGrid, 0);
            if (volumeBool) note.Play();
            tappedGrid.Opacity = 0.3;
            await Task.Delay(500);
            tappedGrid.Opacity = 1;
            note.Stop();
        }
        #endregion
        private void Mute_tapped(object sender, TappedRoutedEventArgs e)
        {
            volumeBool = !volumeBool;
            if (!volumeBool) Mute.Source = new BitmapImage(new Uri(Package.Current.InstalledLocation.Path + "\\Assets\\nute.png"));
            if (volumeBool) Mute.Source = new BitmapImage(new Uri(Package.Current.InstalledLocation.Path + "\\Assets\\voice.png"));
        }

        private void toHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Window.Current.Content = new MainMenu();
            Window.Current.Activate();
        }
    }
}
