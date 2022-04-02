using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace mainApp
{
    public sealed partial class Balloons : Page
    {
        List<Grid> balloonsInMove = new List<Grid>();
        List<int> columnsList = new List<int>();
        DispatcherTimer BalloonsAnimation = new DispatcherTimer();
        DispatcherTimer CreateBalloons = new DispatcherTimer();
        private bool volumeBool = true;
        private bool setColumns = true;
        private bool FirstRun = true;
        IList<string> variables = new List<string>();  
        
        public Balloons()
        {
            InitializeComponent();
            readFile();
        }

        public async void readFile()
        {            
            StorageFolder folder = Package.Current.InstalledLocation;
            string profilname = await FileIO.ReadTextAsync(await folder.GetFileAsync("profileName.txt"));
            variables = await FileIO.ReadLinesAsync(await folder.GetFileAsync("profiles\\" + profilname + "\\ballonsSettings.txt"));
            BalloonsAnimation.Interval = TimeSpan.FromMilliseconds(1);
            BalloonsAnimation.Start();
            BalloonsAnimation.Tick += BalloonsAnimation_Tick;
            CreateBalloons.Interval = TimeSpan.FromSeconds(1);
            CreateBalloons.Start();
            CreateBalloons.Tick += CreateBalloons_Tick;

        }

        private void CreateBalloons_Tick(object sender, object e)
        {
            if (columnsList.Count != 0 || FirstRun)
            {
                FirstRun = false;
                CreateBalloon();
            }
               
        }
        void BalloonsAnimation_Tick(object sender, object e)
        {
            for (int i = 0; i < balloonsInMove.Count; i++)
            {
                var balloonInMove = balloonsInMove[i];
                double move = Convert.ToDouble(variables[1]);
                Thickness margin = balloonInMove.Margin;
                margin.Bottom += move;
                margin.Top -= move;
                balloonInMove.Margin = margin;
                if (margin.Bottom >= -balloonInMove.Height / 2 && !columnsList.Contains(Grid.GetColumn(balloonInMove)))
                {
                    columnsList.Add(Grid.GetColumn(balloonInMove));
                }
                if (margin.Bottom <= -balloonInMove.Height / 2 && columnsList.Contains(Grid.GetColumn(balloonInMove)))
                {
                    columnsList.Remove(Grid.GetColumn(balloonInMove));
                }
                if (margin.Bottom >= Window.Current.Bounds.Height)
                {
                    MainGrid.Children.Remove(balloonInMove);
                    balloonsInMove.Remove(balloonInMove);
                }
            }
        }
        private async void balloons_tappted(object sender, TappedRoutedEventArgs e)
        {
            var balloon = (Grid) ((Ellipse) sender).Parent;
            var balloonPopingSound = (MediaElement)VisualTreeHelper.GetChild(balloon, 1);

            balloon.Visibility = Visibility.Collapsed;
            if (volumeBool) balloonPopingSound.Play();
            AddScore();
            await Task.Delay(TimeSpan.FromSeconds(1));
            columnsList.Add(Grid.GetColumn(balloon));
            balloonsInMove.Remove(balloon);
            MainGrid.Children.Remove(balloon);
        }
        private async void CreateBalloon()
        {


            Grid balloonGrid = new Grid();
            balloonGrid.Height = Convert.ToDouble(variables[0]);
            balloonGrid.Width = balloonGrid.Height / (350.0 / 132);
            balloonGrid.Margin = new Thickness(0, 4.0/5 * Window.Current.Bounds.Height, 0, 0 - balloonGrid.Height);
            balloonGrid.Visibility = Visibility.Visible;

            balloonGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            balloonGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            StorageFolder installitionFolder = Package.Current.InstalledLocation;

            Image image = new Image();
            image.Source = new BitmapImage(new Uri(installitionFolder.Path + "\\Assets\\Balloon.png"));
            Grid.SetRowSpan(image, 2);
            balloonGrid.Children.Add(image);

            MediaElement balloonPopingSound = new MediaElement();
            balloonPopingSound.AutoPlay = false;
            var balloonSoundUri = new Uri(installitionFolder.Path + "\\Assets\\Balloon Popping.wav");
            balloonPopingSound.Source = balloonSoundUri;
            balloonPopingSound.Volume = 1;
            balloonGrid.Children.Add(balloonPopingSound);

            Ellipse ellipse = new Ellipse();
            ellipse.Fill = new SolidColorBrush(Colors.White);
            ellipse.Opacity = 0;
            ellipse.Tapped += balloons_tappted;
            Grid.SetRow(ellipse, 0);
            balloonGrid.Children.Add(ellipse);

            if (setColumns)
            {
                setColumns = false;
                int abc = (int) Math.Floor(Window.Current.Bounds.Width/balloonGrid.Width);
                for (int i = 0; i < abc; i++)
                {
                    MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                    columnsList.Add(i);
                }
                Grid.SetColumnSpan(BackgroundImage, abc);
            }

            Random rnd = new Random();
            int index = rnd.Next(columnsList.Count);
            int column = columnsList[index];
            columnsList.Remove(column);
            Grid.SetColumn(balloonGrid , column);

            MainGrid.Children.Add(balloonGrid);

            balloonsInMove.Add(balloonGrid);
        }
        private void AddScore()
        {
            string score = ScoreTextBlock.Text.Replace("ניקוד: ", "");
            ScoreTextBlock.Text = ScoreTextBlock.Text.Replace(score, (Convert.ToInt32(score) + 1).ToString());
        }
        private void Mute_Tapped(object sender, TappedRoutedEventArgs e)
        {
            volumeBool = !volumeBool;
            if (!volumeBool) Mute.Source = new BitmapImage(new Uri(Package.Current.InstalledLocation.Path + "\\Assets\\nute.png"));
            if (volumeBool) Mute.Source = new BitmapImage(new Uri(Package.Current.InstalledLocation.Path + "\\Assets\\voice.png"));
        }
        private void toHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BalloonsAnimation.Stop();
            CreateBalloons.Stop();
            Window.Current.Content = new MainMenu();
            Window.Current.Activate();
        }
    }

}
