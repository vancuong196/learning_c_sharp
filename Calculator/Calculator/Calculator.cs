using System;
using System.Collections.Generic;
using System.Linq;

namespace CalculatorApplicaiton
{
    public class Calculator
    {
        // Store status of memory.
        private string _memoryStatus;
        // Show current math expression.
        private string _firstLineOfDisplay;
        // Show current input number or result.
        private string _secondLineOfDisplay;
        // Store value.
        private string _memory;
        // Current result.
        private string _result;
        // The first operand in a math expression (ex, 15 + 20, 15 is first operand).
        private string _firstOperand;
        // The second operand.
        private string _secondOperand;
        // Current operator.
        private string _currentOperator;
        private List<string> _history;


        public Calculator()
        {
            _firstOperand = null;
            _secondOperand = null;
            _currentOperator = null;
            _result = null;
            _firstLineOfDisplay = "";
            _secondLineOfDisplay = "";
            _memoryStatus = "";
            _history = new List<string>();
            SetDisplay(_firstLineOfDisplay,_secondLineOfDisplay);
        }
      

        private void UpdateMemoryStatus()
        {
            if (_memory != null)
            {
                _memoryStatus = "M enabled";
            } else
            {
                _memoryStatus = "";
            }
            Console.Clear();
            Console.WriteLine(Constants.Hint);
            Console.WriteLine(_memoryStatus);
            Console.WriteLine(_firstLineOfDisplay);
            Console.WriteLine(_secondLineOfDisplay);
        }
        // Calculate a math expression.
        private double Calculate(string firstOperand, string secondOperand, string operand )
        {
            double a;
            Double.TryParse(firstOperand, out a);
            double b;
            Double.TryParse(secondOperand, out b);
            if (operand=="+")
            {
                return a + b;
            }
            else if (operand == "-")
            {
                return a - b;
            }
            else if (operand == "*")
            {
                return a * b;
            }
            else if (operand == "^")
            {
                return Math.Pow(a, b);
            }
            else
            {
                if (b == 0)
                {
                    throw new DivideByZeroException();
                }
                return a / b;
            }


        }
        private void OnResult(string mathExpression)
        {
            _history.Add(mathExpression);
        }
        // Calculate square root of a number.
        private double SquareRoot(string number)
        {
            double a;
            Double.TryParse(number, out a);
            return Math.Sqrt(a);
        }
        public void SetDisplay(string firstLine, string secondLine)
        {
            Console.Clear();
            Console.WriteLine(Constants.Hint);
            Console.WriteLine(_memoryStatus);
            _firstLineOfDisplay = firstLine;
            _secondLineOfDisplay = secondLine;
            Console.WriteLine(_firstLineOfDisplay);
            Console.WriteLine(_secondLineOfDisplay);
        }
        public void OnEnterKeyPressed()
        {
            if (_currentOperator == null)
            {
                this._result = _firstOperand;
            }
            else
            {
                try
                {
                    _result = Calculate(_firstOperand, _secondOperand, _currentOperator).ToString();
                    SetDisplay(_firstOperand + " " + _currentOperator + " " + _secondOperand + " =", "Result: " + _result);
                    OnResult(_firstOperand + " " + _currentOperator + " " + _secondOperand + " = " + _result);
                    _firstOperand = null;
                    _secondOperand = null;
                    _currentOperator = null;
                }
                catch (DivideByZeroException e)
                {
                    SetDisplay(_firstOperand + " " + _currentOperator + " " + _secondOperand + " =", "Result: Divide By Zero Exception");
                    OnResult(_firstOperand + " " + _currentOperator + " " + _secondOperand + " =  Divide By Zero Exception");
                    _firstOperand = null;
                    _secondOperand = null;
                    _currentOperator = null;
                }

            }
        }
        public void OnNegativeKeyPressed()
        {
            if (_firstOperand == null)
            {
                _firstOperand = "-";
                SetDisplay("", _firstOperand);

            }
            else if (_firstOperand != null && _currentOperator == null)
            {
                if (_firstOperand.Contains("-"))
                {
                    // Remove - sign in first operand.
                    _firstOperand = _firstOperand.Substring(1, _firstOperand.Length - 1);

                }
                else
                {
                    // Add - sign to first operand.
                    _firstOperand = "-" + _firstOperand;
                }
                SetDisplay("", _firstOperand);

            }
            else if (_currentOperator != null)
            {
                if (_secondOperand == null)
                {
                    _secondOperand = "-";
                }
                else
                {
                    if (_secondOperand.Contains("-"))
                    {
                        // Remove - sign in second operand.
                        _secondOperand = _secondOperand.Substring(1, _secondOperand.Length - 1);

                    }
                    else
                    {
                        // Add - sign to second operand.
                        _secondOperand = "-" + _secondOperand;

                    }
                }
                SetDisplay(_firstOperand + " " + _currentOperator + " ", _secondOperand);

            }
        }
        public void OnEscKeyPressed()
        {
            // Clear text and all input.
            SetDisplay("", "");
            _firstOperand = null;
            _secondOperand = null;
            _currentOperator = null;
            _result = null;
        }
        public void OnBackspaceKeyPressed()
        {
            if (_currentOperator == null && _firstOperand != null)
            {
                if (_firstOperand.Length == 1)
                {
                    _firstOperand = null;
                }
                else
                {
                    _firstOperand = _firstOperand.Substring(0, _firstOperand.Length - 1);

                }
                SetDisplay("", _firstOperand);

            }
            else if (_currentOperator != null && _secondOperand != null)
            {
                if (_secondOperand.Length == 1)
                {
                    _secondOperand = null;
                }
                else
                {
                    _secondOperand = _secondOperand.Substring(0, _secondOperand.Length - 1);

                }
                SetDisplay(_firstOperand + " " + _currentOperator + " ", _secondOperand);

            }
        }
        public void OnNumberKeysPressed(char pressedKeyName)
        {
            if (_currentOperator == null)
            {
                if (!(pressedKeyName == '.' && _firstOperand.Contains(pressedKeyName)))
                {
                    if (_firstOperand == null)
                    {
                        _firstOperand = pressedKeyName.ToString();
                    }
                    else
                    {
                        _firstOperand = _firstOperand + pressedKeyName;
                    }
                    SetDisplay("", _firstOperand);
                }
            }
            if (_currentOperator != null)
            {
                if (!(pressedKeyName == '.' && _secondOperand.Contains(pressedKeyName)))
                {
                    if (_secondOperand == null)
                    {
                        _secondOperand = pressedKeyName.ToString();

                    }
                    else
                    {
                        _secondOperand = _secondOperand + pressedKeyName;
                    }
                    SetDisplay(_firstOperand + " " + _currentOperator + " ", _secondOperand);
                }
            }

        }
        public void OnMemoryFuntionKeyPressed(char pressedKeyName)
        {
            // M+ has been pressed.
            if (pressedKeyName == Constants.MemoryPlusKeyName)
            {
                if (_memory == null)
                {
                    _memory = "0";
                }
                if (_result != null && _firstOperand == null)
                {
                    _memory = Calculate(_memory, _result, "+").ToString();
                    SetDisplay("resultM+ = ", "result = " + _result);
                    OnResult(_result + "M+ = " + _result);
                }
                else if (_currentOperator == null && _firstOperand != null)
                {
                    _memory = Calculate(_memory, _firstOperand, "+").ToString();
                    _result = _firstOperand;

                    SetDisplay(_firstOperand + "M+ = ", "result = " + _result);
                    OnResult(_firstOperand + "M+ = " + _result);
                    _firstOperand = null;
                }
                else if (_currentOperator != null && _secondOperand != null)
                {
                    _result = Calculate(_firstOperand, _secondOperand, _currentOperator).ToString();
                    _memory = Calculate(_memory, _result, "+").ToString();

                    SetDisplay("(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ")M+", "result = " + _result);
                    OnResult("(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ")M+ = " + _result);
                    _firstOperand = null;
                    _secondOperand = null;
                    _currentOperator = null;

                }
            }
            // M- has been pressd.
            else if (pressedKeyName == Constants.MemorySubtractionKeyName)
            {
                if (_memory == null)
                {
                    _memory = "0";
                }
                if (_result != null && _firstOperand == null)
                {
                    _memory = Calculate(_memory, _result, "-").ToString();
                    SetDisplay("resultM- = ", "result = " + _result);
                    OnResult(_result + "M- = " + _result);
                }
                else if (_currentOperator == null && _firstOperand != null)
                {

                    _memory = Calculate(_memory, _firstOperand, "-").ToString();
                    _result = _firstOperand;
                    SetDisplay(_firstOperand + "M- = ", "result = " + _result);
                    OnResult(_firstOperand + "M- = " + _result);
                    _firstOperand = null;

                }
                else if (_currentOperator != null && _secondOperand != null)
                {
                    _result = Calculate(_firstOperand, _secondOperand, _currentOperator).ToString();
                    _memory = Calculate(_memory, _result, "-").ToString();

                    SetDisplay("(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ")M-", "result = " + _result);
                    OnResult("(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ")M- = " + _result);
                    _firstOperand = null;
                    _secondOperand = null;
                    _currentOperator = null;

                }
            }
            // MS has been pressed.
            else if (pressedKeyName == Constants.MemorySaveKeyName)
            {

                if (_result != null && _firstOperand == null)
                {
                    _memory = _result;
                }
                else if (_currentOperator == null && _firstOperand != null)
                {
                    _memory = _firstOperand;

                }
                else if (_currentOperator != null && _secondOperand != null)
                {
                    _memory = _secondOperand;

                }
            }
            // MR has been pressed.
            else if (pressedKeyName == Constants.MemoryRecallKeyName)
            {
                _firstOperand = null;
                _secondOperand = null;
                _currentOperator = null;
                if (_memory != null)
                {
                    _result = _memory;
                    SetDisplay("Memory = " + _result, "");

                }
                else
                {
                    SetDisplay("", "Memory is empty");

                }


            }
            else if (pressedKeyName == Constants.MemoryClearKeyName)
            {
                this._memory = null;
            }

            UpdateMemoryStatus();
        }
        public void OnOperatorKeysPressed(char pressedKeyName)
        {
            if (_secondOperand == null && _firstOperand != null)
            {
                _currentOperator = pressedKeyName.ToString();

            }
            else if (_firstOperand == null && _result != null)
            {
                _firstOperand = _result;
                _currentOperator = pressedKeyName.ToString();
            }
            else if (_firstOperand != null && _secondOperand != null)
            {
                _result = Calculate(_firstOperand, _secondOperand, _currentOperator).ToString();
                _firstOperand = _result;
                _secondOperand = null;
                _currentOperator = pressedKeyName.ToString();
                SetDisplay(_firstOperand + " " + _currentOperator, "");
            }
            SetDisplay(_firstOperand + " " + _currentOperator + " ", "");

        }
        public void OnInverseKeyPressed()
        {

            if (_result != null && _firstOperand == null)
            {
                string previousResult = _result;
                _result = Calculate("1", _result, "/").ToString();
                SetDisplay("(1/"+previousResult +") = ", "result = " + _result);
                OnResult("(1/" + previousResult + ") = " + _result);
            }
            else if (_currentOperator == null && _firstOperand != null)
            {
                _result = Calculate("1",_firstOperand,"/").ToString();
                SetDisplay("(1/"+_firstOperand + ") = ", "result = " + _result);
                OnResult("(1/" + _firstOperand + ") = " + _result);
                _firstOperand = null;
            }
            else if (_currentOperator != null && _secondOperand != null)
            {
                _result = Calculate(_firstOperand, _secondOperand, _currentOperator).ToString();
                _result = Calculate("1", _result, "/").ToString();
                SetDisplay("1/(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ")", "result = " + _result);
                OnResult("1/(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ") = " + _result);
                _firstOperand = null;
                _secondOperand = null;
                _currentOperator = null;

            }
        }
        public void OnSquareRootKeyPressed()
        {
            if (_result != null && _firstOperand == null)
            {
                string previousResult = _result;
                _result = SquareRoot(_result).ToString();
                SetDisplay("sqrt(" + previousResult + ") = ", "result = " + _result);
                OnResult("sqrt(" + previousResult + ") = " + _result);
            }
            else if (_currentOperator == null && _firstOperand != null)
            {
                _result = SquareRoot(_firstOperand).ToString();
                SetDisplay("sqrt(" + _firstOperand + ") = ", "result = " + _result);
                OnResult("sqrt(" + _firstOperand + ") = " + _result);
                _firstOperand = null;
            }
            else if (_currentOperator != null && _secondOperand != null)
            {
                _result = Calculate(_firstOperand, _secondOperand, _currentOperator).ToString();
                _result = SquareRoot(_result).ToString();
                SetDisplay("sqrt(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ")", "result = " + _result);
                OnResult("sqrt(" + _firstOperand + " " + _currentOperator + " " + _secondOperand + ") = " + _result);
                _firstOperand = null;
                _secondOperand = null;
                _currentOperator = null;
            }
        }
        public void OnShowHistory()
        {
            Console.Clear();
            Console.WriteLine("History:");
            foreach (string line in _history)
            {
                Console.WriteLine(line);

            }
            Console.WriteLine("Pess any key to come back.");
            Console.ReadKey();
        }
        
    }
 
}
