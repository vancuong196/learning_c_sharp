using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Task_Manager_Prism.Views
{
    public sealed partial class AddTagDialog : ContentDialog
    {
        public AddTagDialog()
        {
            this.InitializeComponent();
        }

        public string TagName
        {
            get
            {
                return tbTagName.Text;

            }
        }
    }
}
