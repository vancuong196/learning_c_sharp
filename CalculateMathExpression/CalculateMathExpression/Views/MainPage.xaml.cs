using CalculateMathExpression.ViewModels;
using info.lundin.math;
using System;
using System.Linq;
using Windows.Foundation;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;



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

        }


        private void OnGenericButtonClicked(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new ContentDialog();
            Button button = (Button)sender;
            if (RadioButtonY.IsChecked == true)
            {
                if ("*/".Contains(button.Content as string) && !CheckDivAndMulOperator(YMathFormulaTextBox.Text))
                {
                    return;
                }
                if (")".Contains(button.Content as string) && !CheckClosingBracket(YMathFormulaTextBox.Text))
                {
                    return;
                }
                if ("0123456789".Contains(button.Content as string) && !NumberCheck(YMathFormulaTextBox.Text))
                {
                    return;
                }
         
                YMathFormulaTextBox.Text = YMathFormulaTextBox.Text + button.Content;
            }
            else
            {
                if ("*/".Contains(button.Content as string) && !CheckDivAndMulOperator(XMathFormulaTextBox.Text))
                {
                    return;
                }
                if (")".Contains(button.Content as string) && !CheckClosingBracket(XMathFormulaTextBox.Text))
                {
                    return;
                }
                if ("0123456789".Contains(button.Content as string) && !NumberCheck(XMathFormulaTextBox.Text))
                {
                    return;
                }
                XMathFormulaTextBox.Text = XMathFormulaTextBox.Text + button.Content;

            }
        }
 
        private void OnSquareRootButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                if (!RootCheck(YMathFormulaTextBox.Text))
                {
                    return;
                }
                YMathFormulaTextBox.Text = YMathFormulaTextBox.Text + "²";
            }
            else
            {
                if (!RootCheck(XMathFormulaTextBox.Text))
                {
                    return;
                }
                XMathFormulaTextBox.Text = XMathFormulaTextBox.Text + "²";

            }
        }
        private void OnSqrtButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                YMathFormulaTextBox.Text = YMathFormulaTextBox.Text + " √(";
            }
            else
            {
                XMathFormulaTextBox.Text = XMathFormulaTextBox.Text + " √(";

            }
        }
        private void OnClearButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                YMathFormulaTextBox.Text = "";
            }
            else
            {
                XMathFormulaTextBox.Text = "";

            }
        }
        private void OnBackspaceButtonClicked(object sender, RoutedEventArgs e)
        {

            TextBox currentFocusTextBox;
            if (RaidoButtonX.IsChecked == true)
            {
                currentFocusTextBox = XMathFormulaTextBox;
            }
            else
            {
                currentFocusTextBox = YMathFormulaTextBox;
            }
           

            if (currentFocusTextBox.Text == "")
            {
                return;
            }
            else
            {
                if (currentFocusTextBox.Text.Length >= 2)
                {
                    if (currentFocusTextBox.Text[currentFocusTextBox.Text.Length - 1] == '(' && currentFocusTextBox.Text[currentFocusTextBox.Text.Length - 2] == '√')
                    {
                        currentFocusTextBox.Text = currentFocusTextBox.Text.Substring(0, currentFocusTextBox.Text.Length - 2);
                        return;
                    }
                    if (currentFocusTextBox.Text[currentFocusTextBox.Text.Length - 1] == '2' && currentFocusTextBox.Text[currentFocusTextBox.Text.Length - 2] == '^')
                    {
                        currentFocusTextBox.Text = currentFocusTextBox.Text.Substring(0, currentFocusTextBox.Text.Length - 2);
                        return;
                    }
                }
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
                if (!CheckSpecialButton(YMathFormulaTextBox.Text))
                {
                    return;
                }
                YMathFormulaTextBox.Text = YMathFormulaTextBox.Text + clickedButton.Tag;
            }
            else
            {
                if (!CheckSpecialButton(XMathFormulaTextBox.Text))
                {
                    return;
                }
                XMathFormulaTextBox.Text = XMathFormulaTextBox.Text + clickedButton.Tag;
            }
        }
        private void OnCalculateButtonClick(object sender, RoutedEventArgs e)
        {

            TextBox[] allTextBox = { xc1, xc2, xc3, xc4, yc1, yc2, yc3, yc4, xbc1, xbc2, xbc3, xbc4, ybc1, ybc2, ybc3, ybc4 };

            string xSpreadFormula = XMathExpressionTextBox.Text;

            string ySpreadFormula = YMathExpressionTextBox.Text;

            var neededTextBox = allTextBox.Where(s => xSpreadFormula.Contains((string)s.Tag) || ySpreadFormula.Contains((string)s.Tag));

            foreach ( TextBox textBox in neededTextBox)
            {
                if (textBox.Text =="")
                {
                    textBox.StartBringIntoView();
                    ShowDialog("Error, You must input a value for: " + textBox.Tag + "!");
                    return;
                } else
                {
                    double valueOfTextbox;
                    if (!Double.TryParse(textBox.Text, out valueOfTextbox))
                    {
                        ShowDialog("Error, Textbox: " + textBox.Tag + " does not contain a number!");
                        return;
                    }
                    ySpreadFormula = ySpreadFormula.Replace((string)textBox.Tag, valueOfTextbox.ToString());
                    xSpreadFormula = xSpreadFormula.Replace((string)textBox.Tag, valueOfTextbox.ToString());
                }
               
            }

            ySpreadFormula = ySpreadFormula.Replace("√", "sqrt");
            ySpreadFormula = ySpreadFormula.Replace("²", "^(2)");
            xSpreadFormula = xSpreadFormula.Replace("√", "sqrt");
            xSpreadFormula = xSpreadFormula.Replace("²", "^(2)");
            ExpressionParser parser = new ExpressionParser();
            string xResult;
            string yResult;
            try
            {
                xResult = Math.Round(parser.Parse(xSpreadFormula),3).ToString();
                
            } catch (Exception exception)
            {
                xResult = "Error!";
            }
            try
            {
                yResult = Math.Round(parser.Parse(ySpreadFormula),3).ToString();

            }
            catch (Exception exception)
            {
                yResult = "Error!";
            }
          
            ShowDialog("XSPREAD result: " + xResult + ";" + " YSPREAD result: " + yResult,"Result");

        }


        private void ShowDialog(string info, string title="")
        {
            ContentDialog alertDialog = new ContentDialog();
            alertDialog.Content = info;
            alertDialog.Title = title;
            alertDialog.PrimaryButtonText = "OK";
            alertDialog.SecondaryButtonText = "Cancel";
            alertDialog.ShowAsync();
        }
        public void OnSaveButtonClicked(object sender, RoutedEventArgs e)
        {
          
                
            (new ViewModelLocator()).Main.SaveCommand.Execute(null);
   
        }


        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
         //   ApplicationView.GetForCurrentView().TryResizeView(new Size(900, 600));
        }
        private bool CheckClosingBracket(string text)
        {
            if (text == "" || text == null)
            {
                return false;
            }
            int count = 0;
            if ("+-*/(".Contains(text[text.Length - 1]))
            {
                return false;
            }
            foreach (char c in text)
            {
                if (c == '(')
                {
                    count++;
                }
                if (c == ')')
                {
                    count--;
                }
            }
            if (count <= 0)
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
            
            if (text.Contains("²") &&text[text.Length-1]!= '²')
            {
                int i = text.Length - 1;
                while (i > 0)
                {
                    if (text[i] == '²')
                    {
                        break;
                    }
                    i--;
                }
                string subGap = text.Substring(i, text.Length - 1 - i);
                if (!(subGap.Contains("*") || subGap.Contains("+") || subGap.Contains("/") || subGap.Contains("-")||subGap.Contains("(") || subGap.Contains(")")))
                {
                    return false;
                }
            }


            if ("+-*/(²".Contains(text[text.Length - 1]))
            {
                return false;
            }
            return true;

        }
        private bool CheckDivAndMulOperator(string text)
        {
            if (text == "" || text == null)
            {
                return false;
            }
            int count = 0;
            if ("-+*/(".Contains(text[text.Length - 1]))
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

            if (")]²".Contains(text[text.Length - 1]))
            {
                return false;
            }
            return true;
        }
        private bool CheckSpecialButton(string text)
        {
            if (text == "")
            {
                return true;
            }

            if ("0123456789])".Contains(text[text.Length - 1]))
            {
                return false;
            }
            return true;
        }
    }
}
