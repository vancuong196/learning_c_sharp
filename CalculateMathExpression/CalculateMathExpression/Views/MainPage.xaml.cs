﻿using info.lundin.math;
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
                YMathFormularTextBox.Text = YMathFormularTextBox.Text + button.Content;
            }
            else
            {
                XMathFormularTextBox.Text = XMathFormularTextBox.Text + button.Content;

            }
        }
        public void OnSquareRootButtonClicked(object sender, RoutedEventArgs e)
        {
            if (RadioButtonY.IsChecked == true)
            {
                YMathFormularTextBox.Text = YMathFormularTextBox.Text + "^2";
            }
            else
            {
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
                YMathFormularTextBox.Text = YMathFormularTextBox.Text + clickedButton.Tag;
            }
            else
            {
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
                ShowDialog("XSPREAD result: " + resultOfX + "/n" + "YSPREAD result: " + resultOfY);
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

    }
}