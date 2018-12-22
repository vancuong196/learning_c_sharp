using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

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
        }
        private void OnGenericButtonClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            Button button = (Button)sender;
            if (RadioButtonY.IsChecked == true)
            {
                MathExpressionTextBoxY.Text = MathExpressionTextBoxY.Text + button.Content;
            }
            else
            {
                MathExpressionTextBoxX.Text = MathExpressionTextBoxX.Text + button.Content;

            }
        }
        public void OnSquareRootButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                MathExpressionTextBoxY.Text = MathExpressionTextBoxY.Text + "^2";
            }
            else
            {
                MathExpressionTextBoxX.Text = MathExpressionTextBoxX.Text + "^2";

            }
        }
        public void OnSqrtButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                MathExpressionTextBoxY.Text = MathExpressionTextBoxY.Text + " √(";
            }
            else
            {
                MathExpressionTextBoxX.Text = MathExpressionTextBoxX.Text + " √(";

            }
        }
        private void OnClearButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                MathExpressionTextBoxY.Text = "";
            }
            else
            {
                MathExpressionTextBoxX.Text = "";

            }
        }
        private void OnBackspaceButtonClicked(object sender, RoutedEventArgs e)
        {

            TextBox currentFocusTextBox;
            if (RaidoButtonX.IsChecked == true)
            {
                currentFocusTextBox = MathExpressionTextBoxX;
            }
            else
            {
                currentFocusTextBox = MathExpressionTextBoxY;
            }
            if (currentFocusTextBox.Text == "")
            {
                return;
            }
            else
            {
                if (currentFocusTextBox.Text[currentFocusTextBox.Text.Length-1]==']')
                {
                    int i = currentFocusTextBox.Text.Length - 1;
                    while (i > 0)
                    {
                        i--;
                        if (currentFocusTextBox.Text[i] == '[')
                        {
                            currentFocusTextBox.Text = currentFocusTextBox.Text.Substring(0, i);
                            break;
                        }
                        
                    }
                } else
                {
                    currentFocusTextBox.Text = currentFocusTextBox.Text.Substring(0, currentFocusTextBox.Text.Length - 1);
                }

            }

        }
        private void OnSpecialButtonClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            Button button = (Button)sender;
            if (RadioButtonY.IsChecked == true)
            {
                MathExpressionTextBoxY.Text = MathExpressionTextBoxY.Text + button.Tag;
            }
            else
            {
                MathExpressionTextBoxX.Text = MathExpressionTextBoxX.Text + button.Tag;

            }
        }
    }
}
