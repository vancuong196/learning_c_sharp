using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using System;
using System.Diagnostics;

namespace CalculateMathExpression.ViewModels
{
    public class MainPageViewModel: ViewModelBase
    {
        private string _yFormula;
        private string _xFormula;
        private string _savedxFormula;
        private string _savedyFormula;
        private IMessageService _messageService;
        public MainPageViewModel()
        {
            _messageService = new MessageService();
        }

        private RelayCommand _saveRelayCommand;
        
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
            
            
        }
        
        
    }
}
