using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Diagnostics;

namespace CalculateMathExpression.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private string _yFormula;
        private string _xFormula;
        private string _savedxFormula;
        private string _savedyFormula;
        private IMessageService _messageService;
        private bool _radioButtonXChecked;
        private bool _radioButtonYChecked;
        private RelayCommand _saveRelayCommand;
        private RelayCommand<string> _buttonClickedCommand;

        public MainPageViewModel()
        {
            _messageService = new MessageService();
            IsRadioButtonXChecked = true;
            XFormula = "";
            YFormula = "";
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
        private void ButtonClickedHandler(string content)
        {
            if (content.Length == 1 && "0123456789*)-+(/".Contains(content))
            {
                OnGenericButtonClicked(content);
            }
            else if (content.Contains("["))
            {
                OnVariableButtonClicked(content);
            }
            else if (content == "sqrt")
            {
                OnSqrtButtonClicked();
            } else if (content == "sqr")
            {
                OnSquareRootButtonClicked();
            } else if (content == "clear")
            {
                OnClearButtonClicked();
            } else if (content == "back")
            {
                OnBackspaceButtonClicked();
            }

        }
        private void OnGenericButtonClicked(string content)
        {
            
            
            if (IsRadioButtonYChecked == true)
            {
                
                if ("*/".Contains(content) && !IsCanAddMulOrDivOperater(YFormula))
                {
                    return;
                }
                if (")".Contains(content) && !IsCanAddClosingBracket(YFormula))
                {
                    return;
                }
                if ("0123456789".Contains(content) && !IsCanAddNumber(YFormula))
                {
                    return;
                }
                if (content == "-")
                {

                    if ("+-*/".Contains(YFormula[YFormula.Length - 1]))
                    {
                        return;
                    }
                }
                if (content == "+")
                {
                    if (YFormula == "")
                    {
                        return;
                    }
                    if ("+-*/(".Contains(YFormula[YFormula.Length - 1]))
                    {
                        return;
                    }
                }


                YFormula = YFormula + content;
            }
            else
            {
                if ("*/".Contains(content) && !IsCanAddMulOrDivOperater(XFormula))
                {
                    return;
                }
                if (")".Contains(content) && !IsCanAddClosingBracket(XFormula))
                {
                    return;
                }
                if ("0123456789".Contains(content) && !IsCanAddNumber(XFormula))
                {
                    return;
                }
                if (content == "-")
                {

                    if (XFormula.Length > 0 && "+-*/".Contains(XFormula[XFormula.Length - 1]))
                    {
                        return;
                    }
                }
                if (content == "+")
                {
                    if (XFormula == "")
                    {
                        return;
                    }
                    if ("+-*/(".Contains(XFormula[XFormula.Length - 1]))
                    {
                        return;
                    }
                }
                XFormula = XFormula + content;

            }
        }
        private bool IsCanAddClosingBracket(string text)
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
        private bool IsCanAddMulOrDivOperater(string text)
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

        private bool IsCanAddNumber(string text)
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

        private void saveFormula()
        {


            int count = 0;
            if (YFormula != "" && YFormula!=null)
            {
                if ("^+-*/(√".Contains(YFormula[YFormula.Length - 1]))
                {
                    throw new System.Exception("YFormula is not correct! (End with '"+ YFormula[YFormula.Length - 1]+"').");
                }
                foreach (char c in YFormula)
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
                if (count != 0)
                {
                    throw new Exception("Error! Missing closing bracket in YFormula.");
                }
            }
            if (XFormula != "" && XFormula!=null)
            {
                if ("^+-*/(√".Contains(XFormula[XFormula.Length - 1]))
                {
                    throw new System.Exception("XFormula is not correct! (End with '"+ XFormula[XFormula.Length - 1]+"')");
                }
                count = 0;
                foreach (char c in XFormula)
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
                if (count != 0)
                {
                    throw new Exception("Error! Missing closing bracket in XFormula");
                }
                

            }

            SavedXFormula = XFormula;
            SavedYFormula = YFormula;
            (new ViewModelLocator()).SecondTabViewModel.SavedXFormula = SavedXFormula;
            (new ViewModelLocator()).SecondTabViewModel.SavedYFormula = SavedYFormula;
            
            
        }


        private void OnSquareRootButtonClicked()
        {
            if (IsRadioButtonYChecked == true)
            {
                if (!IsCanAddSquareRoot(YFormula))
                {
                    return;
                }
                YFormula = YFormula + "²";
            }
            else
            {
                if (!IsCanAddSquareRoot(XFormula))
                {
                    return;
                }
                XFormula = XFormula + "²";

            }
        }
        private void OnSqrtButtonClicked()
        {
            if (IsRadioButtonYChecked == true)
            {
                YFormula = YFormula + " √(";
            }
            else
            {
                XFormula = XFormula + " √(";

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
        private void OnBackspaceButtonClicked()
        {


            if (IsRadioButtonXChecked == true)
            {


                if (XFormula == "")
                {
                    return;
                }
                else
                {
                    if (XFormula.Length >= 2)
                    {
                        if (XFormula[XFormula.Length - 1] == '(' && XFormula[XFormula.Length - 2] == '√')
                        {
                            XFormula = XFormula.Substring(0, XFormula.Length - 2);
                            return;
                        }

                    }
                    if (XFormula[XFormula.Length - 1] == ']')
                    {
                        int i = XFormula.Length - 1;
                        while (i > 0)
                        {
                            i--;
                            if (XFormula[i] == '[')
                            {
                                XFormula = XFormula.Substring(0, i);
                                break;
                            }

                        }
                    }
                    else
                    {
                        XFormula = XFormula.Substring(0, XFormula.Length - 1);
                    }

                }
            } else
            {
                if (YFormula == "")
                {
                    return;
                }
                else
                {
                    if (YFormula.Length >= 2)
                    {
                        if (YFormula[YFormula.Length - 1] == '(' && YFormula[YFormula.Length - 2] == '√')
                        {
                            YFormula = YFormula.Substring(0, YFormula.Length - 2);
                            return;
                        }

                    }
                    if (YFormula[YFormula.Length - 1] == ']')
                    {
                        int i = YFormula.Length - 1;
                        while (i > 0)
                        {
                            i--;
                            if (YFormula[i] == '[')
                            {
                                YFormula = YFormula.Substring(0, i);
                                break;
                            }

                        }
                    }
                    else
                    {
                        YFormula = YFormula.Substring(0, YFormula.Length - 1);
                    }

                }
            }

        }

        private void OnVariableButtonClicked(string content)
        {

           
            if (IsRadioButtonYChecked == true)
            {
                if (!IsCanAddVariableToFormula(YFormula))
                {
                    return;
                }
                YFormula = YFormula + content;
            }
            else
            {
                if (!IsCanAddVariableToFormula(XFormula))
                {
                    return;
                }
                XFormula = XFormula + content;
            }
        }
        private bool IsCanAddSquareRoot(string text)
        {
            if (text == "" || text == null)
            {
                return false;
            }

            if (text.Contains("²") && text[text.Length - 1] != '²')
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
                if (!(subGap.Contains("*") || subGap.Contains("+") || subGap.Contains("/") || subGap.Contains("-") || subGap.Contains("(") || subGap.Contains(")")))
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

        private bool IsCanAddVariableToFormula(string text)
        {
            if (text == "")
            {
                return true;
            }

            if ("0123456789])²".Contains(text[text.Length - 1]))
            {
                return false;
            }
            return true;
        }


    }
}
