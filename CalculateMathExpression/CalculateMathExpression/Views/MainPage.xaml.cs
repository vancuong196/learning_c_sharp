using info.lundin.math;
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
                if ("*/".Contains(button.Content as string) && !DivAndMulOperatorCheck(YMathFormularTextBox.Text))
                {
                    return;
                }
                if (")".Contains(button.Content as string) && !CheckOpeningBracket(YMathFormularTextBox.Text))
                {
                    return;
                }
                if ("0123456789".Contains(button.Content as string) && !NumberCheck(YMathFormularTextBox.Text))
                {
                    return;
                }
                YMathFormularTextBox.Text = YMathFormularTextBox.Text + button.Content;
            }
            else
            {
                if ("*/".Contains(button.Content as string) && !DivAndMulOperatorCheck(XMathFormularTextBox.Text))
                {
                    return;
                }
                if (")".Contains(button.Content as string) && !CheckOpeningBracket(XMathFormularTextBox.Text))
                {
                    return;
                }
                if ("0123456789".Contains(button.Content as string) && !NumberCheck(XMathFormularTextBox.Text))
                {
                    return;
                }
                XMathFormularTextBox.Text = XMathFormularTextBox.Text + button.Content;

            }
        }
        private bool CheckOpeningBracket(string text)
        {
            if (text=="" || text == null)
            {
                return false;
            }
            int count = 0;
            if ("+-*/^(".Contains(text[text.Length - 1]))
            {
                return false; 
            }
            foreach (char c in text)
            {
                if (c=='(')
                {
                    count++;
                }
                if (c == ')')
                {
                    count--;
                }
            }
            if (count<=0)
            {
                return false;
            }
            return true;

        }
        private bool RootCheck(string text)
        {
            if (text == "" || text == null)
            {
                return false;
            }
            int count = 0;
            if ("+-*/(^".Contains(text[text.Length - 1]))
            {
                return false;
            }
            return true;

        }
        private bool DivAndMulOperatorCheck(string text)
        {
            if (text == "" || text == null)
            {
                return false;
            }
            int count = 0;
            if ("-+*/(^".Contains(text[text.Length - 1]))
            {
                return false;
            }
            return true;
        }
        private bool NumberCheck(string text)
        {
            if (text == "")
            {
                return true;
            }
            int count = 0;
            if (")".Contains(text[text.Length - 1]))
            {
                return false;
            }
            return true;
        }
        public void OnSquareRootButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                if (!RootCheck(YMathFormularTextBox.Text))
                {
                    return;
                }
                YMathFormularTextBox.Text = YMathFormularTextBox.Text + "^2";
            }
            else
            {
                if (!RootCheck(XMathFormularTextBox.Text))
                {
                    return;
                }
                XMathFormularTextBox.Text = XMathFormularTextBox.Text + "^2";

            }
        }
        public void OnSqrtButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                YMathFormularTextBox.Text = YMathFormularTextBox.Text + " √(";
            }
            else
            {
                XMathFormularTextBox.Text = XMathFormularTextBox.Text + " √(";

            }
        }
        private void OnClearButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                YMathFormularTextBox.Text = "";
            }
            else
            {
                XMathFormularTextBox.Text = "";

            }
        }
        private void OnBackspaceButtonClicked(object sender, RoutedEventArgs e)
        {

            TextBox currentFocusTextBox;
            if (RaidoButtonX.IsChecked == true)
            {
                currentFocusTextBox = XMathFormularTextBox;
            }
            else
            {
                currentFocusTextBox = YMathFormularTextBox;
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
            
            Button clickedButton = (Button)sender;
            if (RadioButtonY.IsChecked == true)
            {
                if (!NumberCheck(YMathFormularTextBox.Text))
                {
                    return;
                }
                YMathFormularTextBox.Text = YMathFormularTextBox.Text + clickedButton.Tag;
            }
            else
            {
                if (!NumberCheck(XMathFormularTextBox.Text))
                {
                    return;
                }
                XMathFormularTextBox.Text = XMathFormularTextBox.Text + clickedButton.Tag;
            }
        }
        private void OnCalculateButtonClick(object sender, RoutedEventArgs e)
        {

            TextBox[] allTextBox = { xc1, xc2, xc3, xc4, yc1, yc2, yc3, yc4, xbc1, xbc2, xbc3, xbc4, ybc1, ybc2, ybc3, ybc4 };

            string xSpreadFormular = XMathFormularTextBox.Text;

            string ySpreadFormular = YMathFormularTextBox.Text;

            var neededTextBox = allTextBox.Where(s => xSpreadFormular.Contains((string)s.Tag) || ySpreadFormular.Contains((string)s.Tag));

            foreach ( TextBox textBox in neededTextBox)
            {
                if (textBox.Text =="")
                {
                    textBox.StartBringIntoView();
                    ShowDialog("Error, You must input a vaule for textbox: " + textBox.Tag + "!");
                    return;
                } else
                {
                    double valueOfTextbox;
                    if (!Double.TryParse(textBox.Text, out valueOfTextbox))
                    {
                        ShowDialog("Error, Textbox: " + textBox.Tag + " does not contain a number!");
                        return;
                    }
                    ySpreadFormular = ySpreadFormular.Replace((string)textBox.Tag, valueOfTextbox.ToString());
                    xSpreadFormular = xSpreadFormular.Replace((string)textBox.Tag, valueOfTextbox.ToString());
                }
               
            }

            ySpreadFormular = ySpreadFormular.Replace("√", "sqrt");
            xSpreadFormular = xSpreadFormular.Replace("√", "sqrt");
            ExpressionParser parser = new ExpressionParser();
            try
            {
                double resultOfX = parser.Parse(xSpreadFormular);
                double resultOfY = parser.Parse(ySpreadFormular);
                ShowDialog("XSPREAD result: " + resultOfX + ";" + " YSPREAD result: " + resultOfY);
            } catch (Exception exception)
            {
                ShowDialog("Formular ERROR: "+exception.Message);
            }

        }

        private void ShowDialog(string info)
        {
            ContentDialog alertDialog = new ContentDialog();
            alertDialog.Content = info;
            alertDialog.PrimaryButtonText = "OK";
            alertDialog.SecondaryButtonText = "Cancel";
            alertDialog.ShowAsync();
        }
        public void OnSaveButtonClicked(object sender, RoutedEventArgs e)
        {
            if (YMathFormularTextBox.Text != "")
            {
                if ("^+-*/(√".Contains(YMathFormularTextBox.Text[YMathFormularTextBox.Text.Length - 1]))
                {
                    ShowDialog("Formular error!");
                }
            }
            if (XMathFormularTextBox.Text != "")
            {
                if ("^+-*/(√".Contains(XMathFormularTextBox.Text[XMathFormularTextBox.Text.Length - 1]))
                {
                    ShowDialog("Formular error!");
                }
            }
        }

    }
}
