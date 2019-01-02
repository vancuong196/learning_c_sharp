using CalculateMathExpression.Models;
using CalculateMathExpression.Utils;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using info.lundin.math;
using System;
using System.Linq;

namespace CalculateMathExpression.ViewModels
{
    public class CalculateTabModel : ViewModelBase, MainPageDataChangedListener
    {

        private string _savedxFormula;
        private string _savedyFormula;
        private RelayCommand _saveCommand;
        private IInfomationService _messageService;
        public TextboxItem XC1Box { set; get; }
        public TextboxItem XC2Box { set; get; }
        public TextboxItem XC3Box { set; get; }
        public TextboxItem XC4Box { set; get; }
        public TextboxItem YC1Box { set; get; }
        public TextboxItem YC2Box { set; get; }
        public TextboxItem YC3Box { set; get; }
        public TextboxItem YC4Box { set; get; }
        public TextboxItem XBC1Box { set; get; }
        public TextboxItem XBC2Box { set; get; }
        public TextboxItem XBC3Box { set; get; }
        public TextboxItem XBC4Box { set; get; }
        public TextboxItem YBC1Box { set; get; }
        public TextboxItem YBC2Box { set; get; }
        public TextboxItem YBC3Box { set; get; }
        public TextboxItem YBC4Box { set; get; }
        public CalculateTabModel(IInfomationService messageService)
        {
            _messageService = messageService;
            SavedXFormula = "";
            SavedYFormula = "";
            XC1Box = new TextboxItem("[ΔXC1]", "0");
            XC2Box = new TextboxItem("[ΔXC2]", "0");
            XC3Box = new TextboxItem("[ΔXC3]", "0");
            XC4Box = new TextboxItem("[ΔXC4]", "0");
            YC1Box = new TextboxItem("[ΔYC1]", "0");
            YC2Box = new TextboxItem("[ΔYC2]", "0");
            YC3Box = new TextboxItem("[ΔYC3]", "0");
            YC4Box = new TextboxItem("[ΔYC4]", "0");
            XBC1Box = new TextboxItem("[ΔXBC1]", "0");
            XBC2Box = new TextboxItem("[ΔXBC2]", "0");
            XBC3Box = new TextboxItem("[ΔXBC3]", "0");
            XBC4Box = new TextboxItem("[ΔXBC4]", "0");
            YBC1Box = new TextboxItem("[ΔYBC1]", "0");
            YBC2Box = new TextboxItem("[ΔYBC2]", "0");
            YBC3Box = new TextboxItem("[ΔYBC3]", "0");
            YBC4Box = new TextboxItem("[ΔYBC4]", "0");
        }

        public string SavedYFormula
        {
            set
            {
                Set(ref _savedyFormula, value);
            }
            get
            {
                return _savedyFormula;
            }
        }
        public RelayCommand CalculateCommand {
            get
            {
                if (_saveCommand == null)
                {
                    return _saveCommand = new RelayCommand(OnCalculateButtonClick);
                }
                return _saveCommand;
            }
            }

        private void OnCalculateButtonClick()
        {

            TextboxItem[] allTextBox = { XC1Box, XC2Box,XC3Box,XC4Box,YC1Box,YC2Box,YC3Box,YC4Box,XBC1Box,XBC2Box,XBC3Box,XBC4Box,YBC1Box,YBC2Box,YBC3Box,YBC4Box };

            string xSpreadFormula = SavedXFormula;

            string ySpreadFormula = SavedYFormula;

            var neededTextBox = allTextBox.Where(s => xSpreadFormula.Contains(s.Name) || ySpreadFormula.Contains(s.Name));

            foreach (TextboxItem textBox in neededTextBox)
            {
                if (textBox.Value == "")
                {
                   
                    _messageService.ShowMessgae("Error! You must input a value for: " + textBox.Name + "!");
                    return;
                }
                else
                {
                    double valueOfTextbox;
                    if (!Double.TryParse(textBox.Value, out valueOfTextbox))
                    {
                        _messageService.ShowMessgae("Error! Textbox: " + textBox.Name + " does not contain a number!");
                        return;
                    }
                    ySpreadFormula = ySpreadFormula.Replace((string)textBox.Name, valueOfTextbox.ToString());
                    xSpreadFormula = xSpreadFormula.Replace((string)textBox.Name, valueOfTextbox.ToString());
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
                xResult = Math.Round(parser.Parse(xSpreadFormula), 3).ToString();

            }
            catch (Exception exception)
            {
                xResult = "Error!";
            }
            try
            {
                yResult = Math.Round(parser.Parse(ySpreadFormula), 3).ToString();

            }
            catch (Exception exception)
            {
                yResult = "Error!";
            }

           _messageService.ShowMessgae("XSPREAD result: " + xResult + ";" + " YSPREAD result: " + yResult);

        }

        public void OnSavedFormula(string formulaX, string formulaY)
        {
            if (formulaX != null)
            {
                this.SavedXFormula = formulaX;
            }
           if (formulaY != null)
            {
                this.SavedYFormula = formulaY;
            }
            
        }

        public string SavedXFormula
        {
            set
            {
                Set(ref _savedxFormula, value);
            }
            get
            {
                return _savedxFormula;
            }
        }



    }
}
