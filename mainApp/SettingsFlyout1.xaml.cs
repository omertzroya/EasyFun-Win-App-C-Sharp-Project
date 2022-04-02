using System;
using System.Collections.Generic;
using Windows.ApplicationModel;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace mainApp
{
    public sealed partial class SettingsFlyout1 : SettingsFlyout
    {
        DispatcherTimer balloonSpeedTimer = new DispatcherTimer();
        double move = 1;
        IList<string> lines = new List<string>();
        StorageFile sff;
        public SettingsFlyout1()
        {
            InitializeComponent();
            balloonSize.Maximum = balloon.Width * 350 / 132;
            balloonSize.Value = balloon.Height;
            BalloonSpeedSlider.Value = 1;
            balloonSpeedTimer.Interval = TimeSpan.FromMilliseconds(1);
            balloonSpeedTimer.Tick += balloonSpeedTimer_Tick;
            balloonSpeedTimer.Start();
            Unloaded += SettingsFlyout1_Unloaded;
            Loaded += SettingsFlyout1_Loaded;
        }

        async void SettingsFlyout1_Loaded(object sender, RoutedEventArgs e)
        {
            StorageFolder sf = Package.Current.InstalledLocation;
            string profilname = await FileIO.ReadTextAsync(await sf.GetFileAsync("profileName.txt"));
            sff = await sf.GetFileAsync("profiles\\" + profilname + "\\ballonsSettings.txt");
            lines = await FileIO.ReadLinesAsync(sff);
            balloonSize.Value = Convert.ToDouble(lines[0]);
            BalloonSpeedSlider.Value = Convert.ToDouble(lines[1]);
        }


        void SettingsFlyout1_Unloaded(object sender, RoutedEventArgs e)
        {
            balloonSpeedTimer.Stop();
        }

        void balloonSpeedTimer_Tick(object sender, object e)
        {
            Thickness margin = BalloonSpeedImage.Margin;
            margin.Bottom += move;
            margin.Top -= move;
            if (margin.Bottom >= 150)
            {
                margin.Top = 150;
                margin.Bottom = 0 - 150;
            }
            BalloonSpeedImage.Margin = margin;
        }
        private void BalloonSize_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if(balloonSize != null) balloon.Height = balloonSize.Value;
        }
        private async void Save_OnClick(object sender, RoutedEventArgs e)
        {
            string[] lines =
            {
                balloonSize.Value.ToString(),
                BalloonSpeedSlider.Value.ToString()
            };
            await FileIO.WriteLinesAsync(sff, lines);
        }

        private void BalloonSpeed_OnValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (BalloonSpeedSlider != null) move = BalloonSpeedSlider.Value; 
        }
    }
}
