using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace mainApp
{
    public sealed partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
        }
        private void Simon_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Window.Current.Content = new SimonPage();
            Window.Current.Activate();
        }
        private void Play_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Window.Current.Content = new PlayPage();
            Window.Current.Activate();
        }
        private void Balloons_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            Window.Current.Content = new Balloons();
            Window.Current.Activate();
        }
    }
}
