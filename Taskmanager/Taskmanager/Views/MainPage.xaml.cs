
using System.Diagnostics;
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
            Debug.WriteLine("--------------init view--------------");
            new ViewModelLocator().Main.LoadCommand.Execute(null);
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
            if (listView.SelectedItem == null)
            {
                return;
            }
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

        private void OnSearchByTagButtonClicked(object sender, RoutedEventArgs e)
        {
            
            SearchByTagDialog dialog = new SearchByTagDialog();
            dialog.PrimaryButtonClick += SearchByTagDialog_PrimaryButtonClick;
            dialog.PrimaryButtonText = "Ok";
            dialog.ShowAsync();
        }
        private void SearchByTagDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string tag = (sender as SearchByTagDialog).SelectedTag;
            if (tag==null|| tag == "")
            {
                lvTasks.Header = "Result for all TAG:";
                lvTodayTask.Visibility = Visibility.Collapsed;
                this.lvTasks.ItemsSource = new ViewModelLocator().Main.AllTasks;
                

            } else
            {
                lvTasks.Header = "Result for "+tag+" TAG:";
                lvTodayTask.Visibility = Visibility.Collapsed;
                new ViewModelLocator().Main.SelectTaskWithTag.Execute(tag);
                this.lvTasks.ItemsSource = new ViewModelLocator().Main.SearchResultTasks;
            }
        }

        private void OnAddTagButtonClicked(object sender, RoutedEventArgs e)
        {
            AddTagDialog dialog = new AddTagDialog();
            dialog.PrimaryButtonClick += AddTagTagDialog_PrimaryButtonClick;
            dialog.PrimaryButtonText = "Ok";
            dialog.ShowAsync();
        }
        private void AddTagTagDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            string tag = (sender as AddTagDialog).TagName;
            if (tag == null || tag == "")
            {
                sender.ShowAsync(); 
            }
            else
            {
               
                new ViewModelLocator().Main.AddTagToDatabaseCommand.Execute(tag);
                
            }
        }
    }
}
