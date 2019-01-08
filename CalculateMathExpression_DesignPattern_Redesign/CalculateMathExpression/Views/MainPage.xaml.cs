using CalculateMathExpression.ViewModels;
using info.lundin.math;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;

namespace CalculateMathExpression
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
          
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Size(900, 500));
            UpdatePermissions();

        }
        public void UpdatePermissions()
        {
            IEnumerable<ToggleButton> buttons1 = ButtonsGroup1.Children.OfType<ToggleButton>();
            IEnumerable<ToggleButton> buttons2 = ButtonsGroup2.Children.OfType<ToggleButton>();
            IEnumerable<ToggleButton> buttons3 = ButtonsGroup3.Children.OfType<ToggleButton>();
            buttons1 = buttons1.Concat(buttons2);
            buttons1 = buttons1.Concat(buttons3);
            foreach (ToggleButton button in buttons1)
            {
                Debug.WriteLine(button.Content);
            }
        }
    
    }
    
}
