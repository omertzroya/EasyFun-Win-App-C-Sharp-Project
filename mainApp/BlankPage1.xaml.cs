using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Media.Capture;
using Windows.Security.Credentials.UI;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace mainApp
{
    public sealed partial class ProfilesPage : Page
    {
        private int page = 0;
        private int columns;
        public ProfilesPage()
        {
            this.InitializeComponent();
            GetProfiles();
        }

        private async void GetProfiles()
        {
            StorageFolder profilesFolder = await Package.Current.InstalledLocation.CreateFolderAsync("profiles", CreationCollisionOption.OpenIfExists);
            IReadOnlyList<IStorageFolder> profilesList = await profilesFolder.GetFoldersAsync();
            columns = 0;
            if (profilesList.Count <= 9)
            {
                columns = 1;
            }
            if (profilesList.Count%3 == 0 && profilesList.Count > 9)
            {
                columns = profilesList.Count - 9;
                columns = columns/3;
            }
            if (profilesList.Count%3 != 0 && profilesList.Count > 9)
            {
                columns = (int) Math.Round((decimal) (profilesList.Count/6));
            }
            ProfilesGrid.ColumnDefinitions.Clear();
            ProfilesGrid.Children.Clear();
            for (int i = 0; i < columns; i++)
            {
                ProfilesGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(0.9, GridUnitType.Star)});
            }

            for (int i = 0; i < 3; i++)
            {
                ProfilesGrid.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(3*columns, GridUnitType.Star)
                });
            }

            for (int i = 0; i < columns; i++)
            {
                ProfilesGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = new GridLength(0.9, GridUnitType.Star)});
            }

            int column = columns - 1;
            int row = 0;
            foreach (var profile in profilesList)
            {
                if (row > 2) row = 0;
                if (row == 0) column++;
                Grid grid = new Grid();
                grid.Name = profile.Name;
                Grid.SetColumn(grid, column);
                Grid.SetRow(grid, row);
                row++;

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(profile.Path + "/image.jpg"));
                grid.Children.Add(image);

                if (profile.Name != "פרופיל חדש") grid.Tapped += Profile_Tapped;

                ProfilesGrid.Children.Add(grid);
            }
            {

                if (row > 2) row = 0;
                if (row == 0) column++;
                Grid grid = new Grid();
                Grid.SetColumn(grid, column);
                Grid.SetRow(grid, row);
                row++;

                Image image = new Image();
                image.Source = new BitmapImage(new Uri(Package.Current.InstalledLocation.Path + "\\Assets\\plus.png"));
                grid.Children.Add(image);

                grid.Tapped += NewProfile;

                ProfilesGrid.Children.Add(grid);
            }

        }

        private async void Profile_Tapped(object sender, TappedRoutedEventArgs e)
        {
            await FileIO.WriteTextAsync(await Package.Current.InstalledLocation.GetFileAsync("profileName.txt"), ((Grid)sender).Name);
            Window.Current.Content = new MainMenu();
            Window.Current.Activate();
        }

        private void NewProfile(object sender, TappedRoutedEventArgs e)
        {
            ProfileNameBox.Text = "";
            NewProfileGrid.Visibility = Visibility.Visible;
        }

        private async void AddProfile(object sender, TappedRoutedEventArgs e)
        {
            await Package.Current.InstalledLocation.CreateFolderAsync("profiles\\"+ProfileNameBox.Text, CreationCollisionOption.FailIfExists);
            CameraCapture();
            NewProfileGrid.Visibility = Visibility.Collapsed;
        }



         async void CameraCapture()
        {
            // Remember to set permissions in the manifest!

            // using Windows.Media.Capture;
            // using Windows.Storage;
            // using Windows.UI.Xaml.Media.Imaging;

            CameraCaptureUI cameraUI = new CameraCaptureUI();

            cameraUI.PhotoSettings.AllowCropping = false;
            cameraUI.PhotoSettings.MaxResolution = CameraCaptureUIMaxPhotoResolution.MediumXga;

            Windows.Storage.StorageFile capturedMedia =
                await cameraUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (capturedMedia != null)
            {
                using (var streamCamera = await capturedMedia.OpenAsync(FileAccessMode.Read))
                {

                    BitmapImage bitmapCamera = new BitmapImage();
                    bitmapCamera.SetSource(streamCamera);
                    // To display the image in a XAML image object, do this:
                    // myImage.Source = bitmapCamera;

                    // Convert the camera bitap to a WriteableBitmap object, 
                    // which is often a more useful format.

                    int width = bitmapCamera.PixelWidth;
                    int height = bitmapCamera.PixelHeight;

                    WriteableBitmap wBitmap = new WriteableBitmap(width, height);

                    using (var stream = await capturedMedia.OpenAsync(FileAccessMode.Read))
                    {
                        wBitmap.SetSource(stream);
                        SaveImageAsJpeg(wBitmap);
                        
                    }
                }
            }
        }

         private async void SaveImageAsJpeg(WriteableBitmap wBitmap)
        {
             StorageFile file = await Package.Current.InstalledLocation.CreateFileAsync("profiles\\"+ProfileNameBox.Text+"\\image.jpg");


            if (file != null)
            {

                using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.ReadWrite))
                {
                    BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, stream);
                    Stream pixelStream = wBitmap.PixelBuffer.AsStream();
                    byte[] pixels = new byte[pixelStream.Length];
                    await pixelStream.ReadAsync(pixels, 0, pixels.Length);
                    encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)wBitmap.PixelWidth, (uint)wBitmap.PixelHeight, 96.0, 96.0, pixels);
                    await encoder.FlushAsync();
                }
            }
             copyDefaultFilse();
             GetProfiles();
        }

        async void copyDefaultFilse()
        {
            StorageFile fileToCopy = await Package.Current.InstalledLocation.GetFileAsync("default\\ballonsSettings.txt");
            fileToCopy.CopyAsync(await Package.Current.InstalledLocation.GetFolderAsync("profiles\\" + ProfileNameBox.Text));
            fileToCopy = await Package.Current.InstalledLocation.GetFileAsync("default\\simonSetings.txt");
            fileToCopy.CopyAsync(await Package.Current.InstalledLocation.GetFolderAsync("profiles\\" + ProfileNameBox.Text));
        }

        private void ScrollLeftGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (ProfilesGrid.Children.Count > 9 && page > 0)
            {
                page--;
                foreach (Grid grid in ProfilesGrid.Children)
                {
                    Grid.SetColumn(grid, Grid.GetColumn(grid) + 1);
                }
            }
        }
        private void ScrollRightGrid_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (ProfilesGrid.Children.Count > 9 && page < columns)
            {
                page++;
                foreach (Grid grid in ProfilesGrid.Children)
                {
                    Grid.SetColumn(grid, Grid.GetColumn(grid) - 1);
                }
            }
        }

        private void cencel_tapped(object sender, TappedRoutedEventArgs e)
        {
            NewProfileGrid.Visibility = Visibility.Collapsed;
        }
    }
}
