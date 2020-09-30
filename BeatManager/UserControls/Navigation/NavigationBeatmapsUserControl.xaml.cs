﻿using BeatManager.ViewModels.Navigation;
using System;
using System.Windows;
using System.Windows.Controls;

namespace BeatManager.UserControls.Navigation
{
    /// <summary>
    /// Interaction logic for NavigationBeatmapsUserControl.xaml
    /// </summary>
    public partial class NavigationBeatmapsUserControl : UserControl
    {
        public event EventHandler LocalEvent;
        public event EventHandler OnlineEvent;

        public NavigationBeatmapsUserControlViewModel ViewModel;

        public NavigationBeatmapsUserControl()
        {
            InitializeComponent();
            ViewModel = new NavigationBeatmapsUserControlViewModel();
            DataContext = ViewModel;
        }

        private void RadioButtonLocal_Click(object sender, RoutedEventArgs e)
        {
            LocalEvent?.Invoke(this, EventArgs.Empty);
        }

        private void RadioButtonOnline_Click(object sender, RoutedEventArgs e)
        {
            OnlineEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}
