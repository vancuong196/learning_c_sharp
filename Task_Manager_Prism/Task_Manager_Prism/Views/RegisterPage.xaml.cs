﻿using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_Prism.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        private void UserNameTb_KeyDown(object sender, KeyRoutedEventArgs e)
        {
               if (e.Key == Windows.System.VirtualKey.Enter)
            {
                RegisterBtn.Command.Execute(null);
            }
            
        }
    }
}
