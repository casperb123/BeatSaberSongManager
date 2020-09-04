﻿using BeatSaberSongManager.Entities;
using BeatSaberSongManager.ViewModels;
using BeatSaverApi;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BeatSaberSongManager.UserControls
{
    /// <summary>
    /// Interaction logic for BeatmapOnlineUserControl.xaml
    /// </summary>
    public partial class BeatmapOnlineUserControl : UserControl
    {
        public BeatMapOnlineUserControlViewModel ViewModel;

        public BeatmapOnlineUserControl(MainWindow mainWindow)
        {
            InitializeComponent();
            ViewModel = new BeatMapOnlineUserControlViewModel(mainWindow, this);
            DataContext = ViewModel;
        }

        private void WebClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void WebClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Map_Download(object sender, RoutedEventArgs e)
        {
            string songKey = ((Button)sender).Tag.ToString();
            ViewModel.DownloadSong(songKey);
        }

        private void Map_Delete(object sender, RoutedEventArgs e)
        {
            string songKey = ((Button)sender).Tag.ToString();
            ViewModel.DeleteSong(songKey);
        }

        private void Map_Details(object sender, RoutedEventArgs e)
        {
            string songKey = ((Button)sender).Tag.ToString();
        }

        private void RadioButtonSearch_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentMapSort = MapSort.Search;
            ViewModel.BeatSaverMaps = null;
        }

        private void RadioButtonHot_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentMapSort = MapSort.Hot;
            ViewModel.GetBeatSaverMaps(MapSort.Hot);
        }

        private void RadioButtonRating_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentMapSort = MapSort.Rating;
            ViewModel.GetBeatSaverMaps(MapSort.Rating);
        }

        private void RadioButtonLatest_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentMapSort = MapSort.Latest;
            ViewModel.GetBeatSaverMaps(MapSort.Latest);
        }

        private void RadioButtonDownloads_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentMapSort = MapSort.Downloads;
            ViewModel.GetBeatSaverMaps(MapSort.Downloads);
        }

        private void RadioButtonPlays_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.CurrentMapSort = MapSort.Plays;
            ViewModel.GetBeatSaverMaps(MapSort.Plays);
        }

        private void DataGridMaps_TargetUpdated(object sender, DataTransferEventArgs e)
        {
            ViewModel.MainWindow.rectangleLoading.Visibility = Visibility.Hidden;
            ViewModel.MainWindow.progressRingLoading.Visibility = Visibility.Hidden;
            ViewModel.MainWindow.progressRingLoading.IsActive = false;
            ViewModel.UpdatePageButtons();
        }

        private async void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxSearch.Text))
            {
                await ViewModel.MainWindow.ShowMessageAsync("Beat Saver search", "The search query can't be null or empty");
                return;
            }
            if (!radioButtonSearch.IsChecked.Value)
                radioButtonSearch.IsChecked = true;

            ViewModel.GetBeatSaverMaps(textBoxSearch.Text);
        }

        private void buttonFirstPage_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentMapSort == MapSort.Search)
                ViewModel.FirstPage(textBoxSearch.Text);
            else
                ViewModel.FirstPage();
        }

        private void buttonPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentMapSort == MapSort.Search)
                ViewModel.PreviousPage(textBoxSearch.Text);
            else
                ViewModel.PreviousPage();
        }

        private void buttonNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentMapSort == MapSort.Search)
                ViewModel.NextPage(textBoxSearch.Text);
            else
                ViewModel.NextPage();
        }

        private void buttonLastPage_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentMapSort == MapSort.Search)
                ViewModel.LastPage(textBoxSearch.Text);
            else
                ViewModel.LastPage();
        }
    }
}