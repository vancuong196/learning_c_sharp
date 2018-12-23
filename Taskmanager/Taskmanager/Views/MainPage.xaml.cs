
using Taskmanager.Models;
using Taskmanager.ViewModels;
using Taskmanager.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Taskmanager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void OnDasboardButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void OnAddTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTaskFromPage));
            Window.Current.Activate();
            
        }

        private void OnImportantTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.ListViewTasks.ItemsSource = new ViewModelLocator().Main.ImportantTasks;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView =sender as ListView;
            TaskItem task = listView.SelectedItem as TaskItem;
            ContentDialog dialog = new ContentDialog();
            dialog.Content = task.Description;
            dialog.ShowAsync();
        }

        private void AllTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.ListViewTasks.ItemsSource = new ViewModelLocator().Main.AllTasks;

        }

        private void OnNormalTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.ListViewTasks.ItemsSource = new ViewModelLocator().Main.NormalTasks;

        }
    }
}
