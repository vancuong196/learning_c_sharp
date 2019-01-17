

using Task_Manager_Prism.Models;
using Task_Manager_Prism.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_Prism.Views
{

    public sealed partial class EditTaskPage : Page
    {
        public EditTaskPage()
        {
            this.InitializeComponent();
         //   datePicker.Date = (this.DataContext as EditTaskPage). 
        }

        private void OnSaveButtonClicked(object sender, RoutedEventArgs e)
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
            }
            else
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
            }
            else
            {
                tag = cbTag.SelectedValue as string;
            }

            TaskItem taskItem = new TaskItem(-1, taskTitle, time, date, taskDescription, isImportant, tag);

            (DataContext as EditTaskPageViewModel).UpdateCommand.Execute(taskItem);
     

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
