﻿using BeatManager.ViewModels.BeatSaverModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BeatManager.UserControls.BeatSaver
{
    /// <summary>
    /// Interaction logic for BeatmapOnlineDetailsUserControl.xaml
    /// </summary>
    public partial class BeatmapOnlineDetailsUserControl : UserControl
    {
        public readonly BeatmapOnlineDetailsUserControlViewModel ViewModel;

        public BeatmapOnlineDetailsUserControl(MainWindow mainWindow)
        {
            InitializeComponent();
            ViewModel = new BeatmapOnlineDetailsUserControlViewModel(this, mainWindow);
            DataContext = ViewModel;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Back();
        }

        private async void ButtonDownload_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.DownloadSong();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.DeleteSong();
        }

        private void ButtonOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenFolder();
        }

        private void ButtonOpenBeatsaver_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenBeatSaver();
        }

        private void ButtonPreviewBeatmap_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.PreviewBeatmap();
        }

        private void ButtonRefreshData_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.RefreshData();
        }

        private void RadioButtonEasyDifficulty_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Difficulty = ViewModel.Characteristic.Difficulties.Easy;
        }

        private void RadioButtonNormalDifficulty_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Difficulty = ViewModel.Characteristic.Difficulties.Normal;
        }

        private void RadioButtonHardDifficulty_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Difficulty = ViewModel.Characteristic.Difficulties.Hard;
        }

        private void RadioButtonExpertDifficulty_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Difficulty = ViewModel.Characteristic.Difficulties.Expert;
        }

        private void RadioButtonExpertPlusDifficulty_Checked(object sender, RoutedEventArgs e)
        {
            ViewModel.Difficulty = ViewModel.Characteristic.Difficulties.ExpertPlus;
        }

        private void ButtonBigCover_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.OpenBigCover();
        }
    }
}
