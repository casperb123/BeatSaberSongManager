﻿using BeatManager.UserControls.BeatSaver;
using BeatSaver.Entities;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace BeatManager.ViewModels.BeatSaverModels
{
    public class BeatmapOnlineDetailsUserControlViewModel : INotifyPropertyChanged
    {
        private readonly BeatmapOnlineDetailsUserControl userControl;
        private readonly MainWindow mainWindow;
        private OnlineBeatmap beatmap;
        private Characteristic characteristic;
        private IDifficulty difficulty;
        private BitmapImage coverImage;

        public BitmapImage CoverImage
        {
            get { return coverImage; }
            set
            {
                coverImage = value;
                OnPropertyChanged(nameof(CoverImage));
            }
        }

        public IDifficulty Difficulty
        {
            get { return difficulty; }
            set
            {
                difficulty = value;
                OnPropertyChanged(nameof(Difficulty));
            }
        }

        public Characteristic Characteristic
        {
            get { return characteristic; }
            set
            {
                characteristic = value;
                OnPropertyChanged(nameof(Characteristic));
            }
        }

        public OnlineBeatmap Beatmap
        {
            get { return beatmap; }
            set
            {
                beatmap = value;
                OnPropertyChanged(nameof(Beatmap));
                CreateDifficultySets();
                CoverImage = new BitmapImage(new Uri(Beatmap.RealCoverURL));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string prop)
        {
            if (!string.IsNullOrWhiteSpace(prop))
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public BeatmapOnlineDetailsUserControlViewModel(BeatmapOnlineDetailsUserControl userControl, MainWindow mainWindow)
        {
            this.userControl = userControl;
            this.mainWindow = mainWindow;
        }

        public void Back()
        {
            mainWindow.userControlMain.Content = mainWindow.ViewModel.BeatmapOnlineUserControl;
        }

        private void CreateDifficultySets()
        {
            userControl.stackPanelSets.Children.Clear();

            foreach (Characteristic characteristic in Beatmap.Metadata.Characteristics)
            {
                RadioButton radioButton = XamlReader.Parse(XamlWriter.Save(userControl.radioButtonDifficultyTemplate)) as RadioButton;
                radioButton.Style = App.Instance.Resources[$"ToggleButtonDifficultySet"] as Style;

                radioButton.Content = characteristic.Name;
                radioButton.ToolTip = characteristic.Name;
                radioButton.Visibility = Visibility.Visible;
                radioButton.Checked += (s, e) => Characteristic = characteristic;

                userControl.stackPanelSets.Children.Add(radioButton);
            }

            ((RadioButton)userControl.stackPanelSets.Children[0]).IsChecked = true;
            foreach (RadioButton difficulty in userControl.stackPanelDifficulties.Children)
            {
                if (difficulty.Visibility == Visibility.Visible)
                {
                    difficulty.IsChecked = true;
                    break;
                }
            }
        }

        public async Task DownloadSong()
        {
            await mainWindow.ViewModel.BeatmapOnlineUserControl.ViewModel.DownloadSong(Beatmap);
        }

        public void DeleteSong()
        {
            mainWindow.ViewModel.BeatmapOnlineUserControl.ViewModel.DeleteSong(Beatmap);
        }

        public void RefreshData()
        {
            mainWindow.ViewModel.BeatmapOnlineUserControl.ViewModel.BeatmapDetails(Beatmap, false);
        }

        public void OpenFolder()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = beatmap.Metadata.FolderPath,
                UseShellExecute = true,
                Verb = "open"
            });
        }

        public void PreviewBeatmap()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://skystudioapps.com/bs-viewer/?id={beatmap.Key}",
                UseShellExecute = true,
                Verb = "open"
            });
        }

        public void OpenBeatSaver()
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = $"https://beatsaver.com/beatmap/{beatmap.Key}",
                UseShellExecute = true,
                Verb = "open"
            });
        }

        public void OpenBigCover()
        {
            mainWindow.ViewModel.OpenBigCover(CoverImage);
        }
    }
}
