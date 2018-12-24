
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
           
            string taskDescription = tbDescription.Text;
            bool isImportance;
            if (radioButtonYes.IsChecked == true)
            {
                isImportance = true;
            } else
            {
                isImportance = false;
            }
            string time = timePicker.Time.ToString();

            string date = datePicker.Date.Date.ToString();

            string tag;
            if (cbTag.SelectedItem == null)
            {
                tag = "None";
            } else
            {
                tag = (cbTag.SelectedValue as TagItem).Name;
            }
           
            TaskItem taskItem = new TaskItem(-1,taskTitle,time,date,taskDescription,isImportance,tag);

            new ViewModelLocator().Main.SaveCommand.Execute(taskItem);

        }
    }
}
