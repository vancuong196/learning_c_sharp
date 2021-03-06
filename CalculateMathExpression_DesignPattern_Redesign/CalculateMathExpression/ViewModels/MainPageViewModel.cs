﻿using CalculateMathExpression.DAL;
using CalculateMathExpression.Models;
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
        private IDataAccessService  _dataAccessService;
        private bool _radioButtonXChecked;
        private bool _radioButtonYChecked;
        private RelayCommand _saveRelayCommand;
        private RelayCommand _loadFormulaCommand;
        private RelayCommand<string> _buttonClickedCommand;
        private List<MainPageDataChangedListener> _dataChangedListeners;
        public MainPageViewModel(IInfomationService messageService, IDataAccessService dataAccessService)
        {
            _messageService = messageService;
            _dataAccessService = dataAccessService;
            _dataChangedListeners = new List<MainPageDataChangedListener>();
            IsRadioButtonXChecked = true;
            XFormula = "";
            YFormula = "";
            LoadFormulaCommand.Execute(null);
        }
        public void AddOnDataChangeListener(MainPageDataChangedListener listener)
        {
            _dataChangedListeners.Add(listener);
            OnSaveFormula(XFormula, YFormula);
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
        public RelayCommand LoadFormulaCommand
        {
            get
            {
                if (_loadFormulaCommand == null)
                {
                    return _loadFormulaCommand = new RelayCommand(() => {
                        LoadFormula();
                    }
                    );
                }
                return _loadFormulaCommand;
            }
        }
        private async void LoadFormula()
        {
            Dictionary<string, string> formula = await _dataAccessService.GetFormulas();
            if (formula==null|| formula.Count !=2)
            {
                return;
            }
            formula.TryGetValue("Y", out _yFormula);
            formula.TryGetValue("X",out _xFormula);
            XFormula = _xFormula;
            YFormula = _yFormula;
            OnSaveFormula(XFormula, YFormula);
           
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
                Dictionary<string, string> fs = new Dictionary<string, string>();
                fs.Add("Y", YFormula);
                fs.Add("X", XFormula);
                _dataAccessService.SaveFormulas(fs);
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


        private void AppendTextToFormula(string buttonCode)
        {
           
           
            FormulaElement lastElement;
            FormulaElement toAddElement = SupportedElement.GetInstance().GetElementByCode(buttonCode);
            if (IsRadioButtonYChecked == true)
            {
                
                if (YFormula== null || YFormula == "")
                {
                    lastElement = null;
                } else
                {
                   
                    lastElement = SupportedElement.GetInstance().GetElementByCode(YFormula[YFormula.Length-1].ToString());
                  

                }
                ILastElementGrammarValidator grammarValidator = GrammarValidatorFactory.GetLastElementValidator(YFormula);
                if (grammarValidator.Validate(lastElement, toAddElement))
                {
                    YFormula = YFormula + toAddElement.ShowForm;
                }
            }
            else
            {
                if (XFormula == null || XFormula == "")
                {
                    lastElement = null;
                }
                else
                {
                    lastElement = SupportedElement.GetInstance().GetElementByCode(XFormula[XFormula.Length - 1] + "");
                }
                ILastElementGrammarValidator grammarValidator = GrammarValidatorFactory.GetLastElementValidator(XFormula);
                if (grammarValidator.Validate(lastElement, toAddElement))
                {
                    XFormula = XFormula + toAddElement.ShowForm;
                }

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
