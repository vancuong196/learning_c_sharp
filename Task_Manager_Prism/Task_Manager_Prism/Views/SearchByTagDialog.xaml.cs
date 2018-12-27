
using Task_Manager_Prism.ViewModels;
using Taskmanager.DatabaseAccess;

using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_Prism.Views
{
    public sealed partial class SearchByTagDialog : ContentDialog
    {
        public SearchByTagDialog()
        {
            this.InitializeComponent();
            DataContext = new MainPageViewModel(new DatabaseAccessService(),null);
        }
        public string SelectedTag
        {
            get
            {
                if (cbTagName.SelectedItem==null)
                {
                    return null;
                }
                return cbTagName.SelectedItem as string;
            }
        }
    }
}
