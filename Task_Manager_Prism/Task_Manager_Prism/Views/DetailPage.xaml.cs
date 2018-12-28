
using System.Collections.Generic;
using System.Diagnostics;
using Prism.Windows.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



namespace Task_Manager_Prism.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page
    {
        public DetailPage()
        {
            this.InitializeComponent();
        }
        public void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(MainPage));
        }


    }
   
}
