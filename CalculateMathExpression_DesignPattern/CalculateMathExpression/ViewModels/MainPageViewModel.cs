using CalculateMathExpression.Utils;
using CalculateMathExpression.Utils.FormulaHelper;
using CalculateMathExpression.Utils.GrammarValidate;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CalculateMathExpression.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _yFormula;
        private string _xFormula;
        private IInfomationService _messageService;
        private bool _radioButtonXChecked;
        private bool _radioButtonYChecked;
        private RelayCommand _saveRelayCommand;
        private RelayCommand<string> _buttonClickedCommand;
        private List<MainPageDataChangedListener> _dataChangedListeners;
        public MainPageViewModel(IInfomationService messageService)
        {
            _messageService = messageService;
            _dataChangedListeners = new List<MainPageDataChangedListener>();
            IsRadioButtonXChecked = true;
            XFormula = "";
            YFormula = "";
        }
        public void AddOnDataChangeListener(MainPageDataChangedListener listener)
        {
            _dataChangedListeners.Add(listener);
        }
        public bool IsRadioButtonXChecked{
            set
            {
                Set(ref _radioButtonXChecked, value);
                Set(ref _radioButtonYChecked, !value);
            }
            get
            {
                return _radioButtonXChecked;
            }
         }
        public bool IsRadioButtonYChecked
        {
            set
            {
                Set(ref _radioButtonXChecked, !value);
                Set(ref _radioButtonYChecked, value);
            }
            get
            {
                return _radioButtonYChecked;
            }
        }


        public RelayCommand SaveCommand
        {
            get
            {
                if (_saveRelayCommand == null)
                {
                    return _saveRelayCommand = new RelayCommand(()=> {
                        try
                        {
                            saveFormula();
                            _messageService.ShowMessgae("Saved!");
                        } catch (Exception e)
                        {
                            _messageService.ShowMessgae(e.Message);
                        }
                       
                        }
                    );
                }
                return _saveRelayCommand;
            }
        }
        public RelayCommand<string> ButtonClickedCommand
        {
            get
            {
                if (_buttonClickedCommand == null)
                {
                    return _buttonClickedCommand = new RelayCommand<string> (ButtonClickedHandler);
                }
                return _buttonClickedCommand;
            }
        }
        private void ButtonClickedHandler(string buttonType)
        {
           
            if (buttonType == "clear")
            {
                OnClearButtonClicked();
            } else if (buttonType == "back")
            {
                OnBackspaceButtonClicked();
            } else
            {
                AppendTextToFormula(buttonType);
                
            }

        }

        private void OnClearButtonClicked()
        {
            if (IsRadioButtonYChecked == true)
            {

                YFormula = "";
            }
            else
            {

                XFormula = "";
            }
        }

 
        public string YFormula
        {
            set
            {
                Set(ref _yFormula, value);
            }
            get
            {
                return _yFormula;
            }
        }
        public string XFormula
        {
            set
            {
                Set(ref _xFormula, value);
            }
            get
            {
                return _xFormula;
            }
        }


        private void saveFormula()
        {
            try
            {
                GrammarValidatorFactory.GetSentenceGrammarValidator().Validate(XFormula);
                GrammarValidatorFactory.GetSentenceGrammarValidator().Validate(YFormula);
                OnSaveFormula(XFormula, YFormula);
            }
            catch (Exception e)
            {
                throw e;
            }       
        }

        public void OnSaveFormula(string xFormula, string yFormula)
        {
            if (_dataChangedListeners.Count == 0)
            {
                return;
            }
            foreach (MainPageDataChangedListener listener in _dataChangedListeners)
            {
                if (listener != null)
                {
                    listener.OnSavedFormula(xFormula, yFormula);
                }
            }
        }


        private void AppendTextToFormula(string rawText)
        {

            IGrammarValidator grammarValidator = GrammarValidatorFactory.GetGrammarValidator(rawText);

            string toAppendText = "";
            if ("0123456789*)-+(/".Contains(rawText))
            {
                toAppendText = rawText;
            }
            else if (rawText!=null && rawText.Contains("["))
            {
                toAppendText = rawText;
            }
            else if (rawText == "sqrt")
            {
                toAppendText = "√(";
            }
            else if (rawText == "sqr")
            {
                toAppendText = "²";
            }

            if (IsRadioButtonYChecked == true && grammarValidator.Validate(YFormula))
            {
                YFormula = YFormula+toAppendText;
            }
            else if (grammarValidator.Validate(XFormula))
            {
                XFormula = XFormula+toAppendText;

            }

        }
        private void OnBackspaceButtonClicked()
        {
            if (IsRadioButtonXChecked == true)
            {
                XFormula = FormulaCutter.GetInstance().CutLastElement(XFormula);
            } else
                YFormula = FormulaCutter.GetInstance().CutLastElement(YFormula);
        }

        
    }
}
