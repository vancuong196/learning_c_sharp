
using Prism.Windows;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Task_Manager_Prism.Models;
using Task_Manager_Prism.Ultils;
using Task_Manager_Prism.ViewModels;
using Task_Manager_Prism.ViewModels;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Task_Manager_Prism.Views
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
       
        }

        private void OnDasboardButtonClick(object sender, RoutedEventArgs e)
        {
            TagList.Visibility = Visibility.Visible;
            lvTodayTask.Visibility = Visibility.Visible;
            this.lvTasks.Header = "Overdue tasks";
            (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.OverdueTaskListID);
            TagList.SelectedItem = "All";
            ChangeColor(sender as Button);
        }

        private void OnAddTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(AddTaskPage));
            Window.Current.Activate();

        }

        private void OnImportantTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            TagList.Visibility = Visibility.Visible;
            lvTodayTask.Visibility = Visibility.Collapsed;
            this.lvTasks.Header = "Important tasks";
            (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.ImportantTaskListID);
            TagList.SelectedItem = "All";
            ChangeColor(sender as Button);

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedItem == null)
            {
                return;
            }
            //    new ViewModelLocator().Main.SelectListItemRelayCommand.Execute((listView.SelectedItem as TaskItem).ID);
            (DataContext as MainPageViewModel).SelectListItemDelegateCommand.Execute((listView.SelectedItem as TaskItem).ID);
            Frame rootFrame = Window.Current.Content as Frame;
            //   rootFrame.Navigate(typeof(DetailTaskPage));
        }


        private void AllTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            TagList.Visibility = Visibility.Visible;

            this.lvTasks.Header = "All tasks";
            lvTodayTask.Visibility = Visibility.Collapsed;
            (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.AllTaskListID);
            TagList.SelectedItem = "All";
            ChangeColor(sender as Button);
        }

        private void OnNormalTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            TagList.Visibility = Visibility.Visible;
            (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.NormalTaskListID);
            this.lvTasks.Header = "Normal tasks";
            lvTodayTask.Visibility = Visibility.Collapsed;
            TagList.SelectedItem = "All";
            ChangeColor(sender as Button);
        }

        private void OnTaskWithoutDateButtonClicked(object sender, RoutedEventArgs e)
        {
            TagList.Visibility = Visibility.Visible;
            (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.NoneDateTaskListID);
            this.lvTasks.Header = "Tasks without day";
            lvTodayTask.Visibility = Visibility.Collapsed;
            TagList.SelectedItem = "All";
            ChangeColor(sender as Button);
        }

        private void OnFinishedTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            TagList.Visibility = Visibility.Visible;
            (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.FinishedTaskListID);
            this.lvTasks.Header = "Finished task";
            lvTodayTask.Visibility = Visibility.Collapsed;
            TagList.SelectedItem = "All";
            ChangeColor(sender as Button);
        }

        private void OnOverdueTaskButtonClicked(object sender, RoutedEventArgs e)
        {
            TagList.Visibility = Visibility.Visible;
            (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.OverdueTaskListID);
            this.lvTasks.Header = "Overdue task";
            lvTodayTask.Visibility = Visibility.Collapsed;
            TagList.SelectedItem = "All";
            ChangeColor(sender as Button);
        }

        private void OnSearchByTagButtonClicked(object sender, RoutedEventArgs e)
        {

            TagList.Visibility = Visibility.Collapsed;
            SearchByTagDialog dialog = new SearchByTagDialog();
            dialog.PrimaryButtonClick += SearchByTagDialog_PrimaryButtonClick;
            dialog.SecondaryButtonClick += SearchByTagDialog_CancelButtonClick;
            dialog.AllTags = (DataContext as MainPageViewModel).AllTags;
            dialog.PrimaryButtonText = "Ok";
            dialog.ShowAsync();
            ChangeColor(sender as Button);
        }
        private void SearchByTagDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        
            string tag = (sender as SearchByTagDialog).SelectedTag;
            if (tag == null || tag == "")
            {
                lvTasks.Header = "Result for all TAG:";
                lvTodayTask.Visibility = Visibility.Collapsed;
                (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.AllTaskListID);


            }
            else
            {
                lvTasks.Header = "Result for " + tag + " TAG:";
                lvTodayTask.Visibility = Visibility.Collapsed;
                (DataContext as MainPageViewModel).SelectTaskWithTag.Execute(tag);

            }
        }
        private void SearchByTagDialog_CancelButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
                lvTasks.Header = "Result for all TAG:";
                lvTodayTask.Visibility = Visibility.Collapsed;
                (DataContext as MainPageViewModel).LoadListCommand.Execute(Constants.AllTaskListID);
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
                //sender.ShowAsync(); 
            }
            else
            {

                (DataContext as MainPageViewModel).AddTagToDatabaseCommand.Execute(tag);

            }
        }
        private void ChangeColor(Button clickedButton)
        {
            var buttons = MenuButtonsGroup.Children.OfType<Button>();
            Debug.WriteLine("Button lenght: "+buttons.Count());
            foreach (Button b in buttons)
            {
                if (b.Name == clickedButton.Name )
                {
                    b.Background = new SolidColorBrush(Colors.Black);
                    b.Foreground = new SolidColorBrush(Colors.Red);
                } else
                {
                    b.Background = new SolidColorBrush(Colors.AliceBlue);
                    b.Foreground = new SolidColorBrush(Colors.Black);
                }
            }
        }

        private void TagList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView.SelectedItem == null)
            {
                return;
            }
            //    new ViewModelLocator().Main.SelectListItemRelayCommand.Execute((listView.SelectedItem as TaskItem).ID);
            (DataContext as MainPageViewModel).SelectTagItemDelegateCommand.Execute(listView.SelectedItem as string);
           
        }
    }
}
