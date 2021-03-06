﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Taskmanager.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskmanager.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailTaskPage : Page
    {
        public DetailTaskPage()
        {
            this.InitializeComponent();
        }
        public void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(MainPage));
        }

        private void OnDeletedButtonClicked(object sender, RoutedEventArgs e)
        {

            //  new ViewModelLocator().Main.DeleteCommand.Execute((sender as AppBarButton).Tag.ToString());
             
            (Window.Current.Content as Frame).Navigate(typeof(MainPage));
        }

        private void OnEditButtonClicked(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(EditTaskFormPage));
        }
    }
   
}
