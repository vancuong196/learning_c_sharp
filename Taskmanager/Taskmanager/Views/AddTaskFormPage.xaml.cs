
using Taskmanager.Models;
using Taskmanager.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskmanager.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddTaskFromPage : Page
    {
        public AddTaskFromPage()
        {
            this.InitializeComponent();
        }

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));
           
        }
        public void OnSaveButtonClicked(object sender, RoutedEventArgs e)
        {
            string taskTitle = tbTaskName.Text;
            if (taskTitle == "")
            {
                ShowDialog("Title of task cannot be empty!");
                return;
            }
            string taskDescription = tbDescription.Text;
            bool isImportant;
            if (radioButtonYes.IsChecked == true)
            {
                isImportant = true;
            } else
            {
                isImportant = false;
            }
            string time = "";
            string date = "";

            if (tsIsTimeConstraint.IsOn)
            {
                time = timePicker.Time.ToString();
                date = datePicker.Date.Date.ToString("MM/dd/yyyy");
            }

            string tag;
            if (cbTag.SelectedItem == null)
            {
                tag = "None";
            } else
            {
                tag = cbTag.SelectedItem.ToString() ;
            }
           
            TaskItem taskItem = new TaskItem(-1,taskTitle,time,date,taskDescription,isImportant,tag);

            new ViewModelLocator().Main.SaveCommand.Execute(taskItem);
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(MainPage));

        }
        private void ShowDialog(string info)
        {
            ContentDialog dialog = new ContentDialog();
            dialog.Content = info;
            dialog.PrimaryButtonText = "OK";
            dialog.ShowAsync();
        }

    }
}
