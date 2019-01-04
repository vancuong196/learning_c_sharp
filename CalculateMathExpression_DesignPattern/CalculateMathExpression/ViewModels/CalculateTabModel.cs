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
        private ICalculator _calculator;
        public FormulaVariable XC1Box { set; get; }
        public FormulaVariable XC2Box { set; get; }
        public FormulaVariable XC3Box { set; get; }
        public FormulaVariable XC4Box { set; get; }
        public FormulaVariable YC1Box { set; get; }
        public FormulaVariable YC2Box { set; get; }
        public FormulaVariable YC3Box { set; get; }
        public FormulaVariable YC4Box { set; get; }
        public FormulaVariable XBC1Box { set; get; }
        public FormulaVariable XBC2Box { set; get; }
        public FormulaVariable XBC3Box { set; get; }
        public FormulaVariable XBC4Box { set; get; }
        public FormulaVariable YBC1Box { set; get; }
        public FormulaVariable YBC2Box { set; get; }
        public FormulaVariable YBC3Box { set; get; }
        public FormulaVariable YBC4Box { set; get; }

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
        public CalculateTabModel(IInfomationService messageService, ICalculator calculator)
        {
            _calculator = calculator;
            _messageService = messageService;
            SavedXFormula = "";
            SavedYFormula = "";
            XC1Box = new FormulaVariable("[ΔXC1]", "0");
            XC2Box = new FormulaVariable("[ΔXC2]", "0");
            XC3Box = new FormulaVariable("[ΔXC3]", "0");
            XC4Box = new FormulaVariable("[ΔXC4]", "0");
            YC1Box = new FormulaVariable("[ΔYC1]", "0");
            YC2Box = new FormulaVariable("[ΔYC2]", "0");
            YC3Box = new FormulaVariable("[ΔYC3]", "0");
            YC4Box = new FormulaVariable("[ΔYC4]", "0");
            XBC1Box = new FormulaVariable("[ΔXBC1]", "0");
            XBC2Box = new FormulaVariable("[ΔXBC2]", "0");
            XBC3Box = new FormulaVariable("[ΔXBC3]", "0");
            XBC4Box = new FormulaVariable("[ΔXBC4]", "0");
            YBC1Box = new FormulaVariable("[ΔYBC1]", "0");
            YBC2Box = new FormulaVariable("[ΔYBC2]", "0");
            YBC3Box = new FormulaVariable("[ΔYBC3]", "0");
            YBC4Box = new FormulaVariable("[ΔYBC4]", "0");
            
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

            FormulaVariable[] allVariables = { XC1Box, XC2Box,XC3Box,XC4Box,YC1Box,YC2Box,YC3Box,YC4Box,XBC1Box,XBC2Box,XBC3Box,XBC4Box,YBC1Box,YBC2Box,YBC3Box,YBC4Box };

            string xSpreadFormula = "" ;

            string ySpreadFormula = "";

            FormulaDictionary formulaDictionary = new FormulaDictionary(allVariables);

            try 
            {
                xSpreadFormula = formulaDictionary.TryTranslateToMathEpression(SavedXFormula);
                ySpreadFormula = formulaDictionary.TryTranslateToMathEpression(SavedYFormula);

            } catch (Exception e)
            {
                _messageService.ShowMessgae(e.Message);
                return;
            }

            string xResult;
            string yResult;
            try
            {
                xResult = Math.Round(_calculator.Calculate(xSpreadFormula), 3).ToString();

            }
            catch (Exception exception)
            {
                xResult = "Error!";
            }
            try
            {
                yResult = Math.Round(_calculator.Calculate(ySpreadFormula), 3).ToString();

            }
            catch (Exception exception)
            {
                yResult = "Error!";
            }

           _messageService.ShowMessgae("XSPREAD result: " + xResult + ";" + " YSPREAD result: " + yResult);

        }



    }
}
