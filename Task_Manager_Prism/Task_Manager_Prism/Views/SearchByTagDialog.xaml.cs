
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_Prism.Views
{
    public sealed partial class SearchByTagDialog : ContentDialog
    {
        public SearchByTagDialog()
        {
            this.InitializeComponent();
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
        public ObservableCollection<string> AllTags
        {
            set
            {
                DataContext = value;
            }
        }
    }
}
