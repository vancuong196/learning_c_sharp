
using System.Collections.Generic;
using System.Diagnostics;
using Prism.Windows.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_Prism.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailPage : Page, INavigationAware
    {
        public DetailPage()
        {
            this.InitializeComponent();
        }
        public void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            (Window.Current.Content as Frame).Navigate(typeof(MainPage));
        }

        public void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            Debug.WriteLine("-------------------");
        }

        public void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            throw new System.NotImplementedException();
        }

        private void OnDeletedButtonClicked(object sender, RoutedEventArgs e)
        {

            //  new ViewModelLocator().Main.DeleteCommand.Execute((sender as AppBarButton).Tag.ToString());
             
      //      (Window.Current.Content as Frame).Navigate(typeof(MainPage));
        }

        private void OnEditButtonClicked(object sender, RoutedEventArgs e)
        {
        //    (Window.Current.Content as Frame).Navigate(typeof(EditTaskFormPage));
        }
    }
   
}
