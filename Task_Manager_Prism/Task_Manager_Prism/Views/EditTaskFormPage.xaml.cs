using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Taskmanager.Models;

using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Taskmanager.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditTaskFormPage : Page
    {
        public EditTaskFormPage()
        {
            this.InitializeComponent();
        }

        private void OnBackButtonClicked(object sender, RoutedEventArgs e)
        {
          //  (Window.Current.Content as Frame).Navigate(typeof(DetailTaskPage));
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

          //  new ViewModelLocator().Main.UpdateCommand.Execute(taskItem);
          //  (Window.Current.Content as Frame).Navigate(typeof(DetailTaskPage));

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
