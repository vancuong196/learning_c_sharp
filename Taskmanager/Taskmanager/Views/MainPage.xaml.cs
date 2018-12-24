
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
            lvTodayTask.Visibility = Visibility.Visible;
            this.lvTasks.Header = "Overdue tasks";
            this.lvTasks.ItemsSource = new ViewModelLocator().Main.OverdueTasks;




        }

        private void OnAddTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTaskFromPage));
            Window.Current.Activate();
            
        }

        private void OnImportantTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.lvTasks.ItemsSource = new ViewModelLocator().Main.ImportantTasks;
            lvTodayTask.Visibility = Visibility.Collapsed;
            this.lvTasks.Header = "Important tasks";

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView =sender as ListView;
            new ViewModelLocator().Main.SelectListItemRelayCommand.Execute((listView.SelectedItem as TaskItem).ID);
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(DetailTaskPage));
        }

        private void AllTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.lvTasks.ItemsSource = new ViewModelLocator().Main.AllTasks;
            this.lvTasks.Header = "All tasks";
            lvTodayTask.Visibility = Visibility.Collapsed;


        }

        private void OnNormalTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.lvTasks.ItemsSource = new ViewModelLocator().Main.NormalTasks;
            this.lvTasks.Header = "Normal tasks";
            lvTodayTask.Visibility = Visibility.Collapsed;



        }

        private void OnTaskWithoutDateButtonClicked(object sender, RoutedEventArgs e)
        {
            this.lvTasks.ItemsSource = new ViewModelLocator().Main.NoDateTasks;
            this.lvTasks.Header = "Tasks without day";
            lvTodayTask.Visibility = Visibility.Collapsed;



        }

        private void OnFinishedTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.lvTasks.ItemsSource = new ViewModelLocator().Main.FinishedTasks;
            this.lvTasks.Header = "Finished task";
            lvTodayTask.Visibility = Visibility.Collapsed;

        }

        private void OnOverdueTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            this.lvTasks.ItemsSource = new ViewModelLocator().Main.OverdueTasks;
            this.lvTasks.Header = "Overdue task";
            lvTodayTask.Visibility = Visibility.Collapsed;
        }
    }
}
