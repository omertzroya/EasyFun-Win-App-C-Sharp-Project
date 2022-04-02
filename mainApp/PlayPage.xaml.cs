using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace mainApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PlayPage : Page
    {
        private List<Grid> recordGrids = new List<Grid>();
        private bool isRecord;
        public PlayPage()
        {
            InitializeComponent();
        }
        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            click((Grid)sender);
            if (isRecord) recordGrids.Add((Grid)sender);
        }
        async Task click(Grid grid)
        {
            MediaElement note = (MediaElement)VisualTreeHelper.GetChild(grid, 0);
            note.Play();
            grid.Opacity = 0.3;
            await Task.Delay(1000);
            grid.Opacity = 1;
            note.Stop();
        }
        private void toHome_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Window.Current.Content = new MainMenu();
            Window.Current.Activate();
        }
        private void Recored_Tapped(object sender, TappedRoutedEventArgs e)
        {
            isRecord = !isRecord;
            if (isRecord)
            {
                recordGrids.Clear();
                Recored.Source = new BitmapImage(new Uri(Package.Current.InstalledLocation.Path + "\\Assets\\stop.png"));
            }
            if (!isRecord)
            {
                Recored.Source = new BitmapImage(new Uri(Package.Current.InstalledLocation.Path + "\\Assets\\record.png")); 
            }
        }
        private async void Play_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!isRecord)
            {
                foreach (Grid recordGrid in recordGrids)
                {
                    await click(recordGrid);
                }
            }
        }
    }
}
