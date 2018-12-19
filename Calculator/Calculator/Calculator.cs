using System;
using System.Linq;

namespace CalculatorApplicaiton
{
    class Calculator
    {

        private string MemoryStatus;
        private string FirstLineOfDisplay;
        private string SecondLineOfDisplay;
        private string Memory;
        private string Result;
        private string FirstOperand;
        private string SecondOperand;
        private string CurrentOperator;


        public Calculator()
        {
            FirstOperand = null;
            SecondOperand = null;
            CurrentOperator = null;
            Result = null;
            FirstLineOfDisplay = "";
            SecondLineOfDisplay = "";
            MemoryStatus = "";
            SetDisplay(FirstLineOfDisplay,SecondLineOfDisplay);
        }

        public void SetDisplay(string firstLine, string secondLine)
        {
            Console.Clear();
            Console.WriteLine(Constants.Hint);
            Console.WriteLine(MemoryStatus);
            FirstLineOfDisplay = firstLine;
            SecondLineOfDisplay = secondLine;
            Console.WriteLine(FirstLineOfDisplay);
            Console.WriteLine(SecondLineOfDisplay);
        }
        public void UpdateMemoryStatus()
        {
            if (Memory != null)
            {
                MemoryStatus = "M enabled";
            } else
            {
                MemoryStatus = "";
            }
            Console.Clear();
            Console.WriteLine(Constants.Hint);
            Console.WriteLine(MemoryStatus);
            Console.WriteLine(FirstLineOfDisplay);
            Console.WriteLine(SecondLineOfDisplay);
        }
        
        public void Start()
        {
            // Read a single key input from keyboard and process depend on what key has been pressed.
            while (true)
            {
                // Wait for key input.
                char pressedKeyName = ReadKey();

                // Memory related funtion key has been pressed.

                if (Constants.SupportedMemoryFuntionKey.Contains(pressedKeyName))
                {
                    OnMemoryFuntionKeyPressed(pressedKeyName);
                }
                // Operator key has been pressed.
                else if (Constants.SupportedOperatorKey.Contains(pressedKeyName))
                {
                    OnOperatorKeysPressed(pressedKeyName);

                }
                // Numberic key has been pressed.
                else if (Constants.SupportedNumberKey.Contains(pressedKeyName))
                {
                    OnNumberKeysPressed(pressedKeyName);
                }
                // Enter key has been press.
                else if (pressedKeyName == (char)Constants.EnterKeyCode)
                {
                    OnEnterKeyPressed();
                }
                // Esc key has been press.
                else if (pressedKeyName == (char) Constants.EscKeyCode)
                {

                    OnEscKeyPressed();
                }
                // Backspace key has been press.
                else if (pressedKeyName == (char)Constants.BackspaceKeyCode)
                { 
                    OnBackspaceKeyPressed();
                }
                // Negative key has been press. 
                else if (pressedKeyName == Constants.NegativeKeyName)
                {
                    OnNegativeKeyPressed();
                }
                // Inverse key pressed 
                else if (pressedKeyName == Constants.InverseKeyName)
                {
                    OnInverseKeyPressed();
                }
                // SquareRoot key pressed.
                else if (pressedKeyName == Constants.SqrtKeyName)
                {
                    OnSquareRootKeyPressed();
                }

            }
        }

        public char ReadKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Char keyName = keyInfo.KeyChar;
            return keyName;            

        }
        private double Calculate(string firstNumber, string secondNumber, string operand )
        {
            double a;
            Double.TryParse(firstNumber, out a);
            double b;
            Double.TryParse(secondNumber, out b);
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
        private double SquareRoot(string number)
        {
            double a;
            Double.TryParse(number, out a);
            return Math.Sqrt(a);
        }
        private void OnEnterKeyPressed()
        {
            if (CurrentOperator == null)
            {
                this.Result = FirstOperand;
            }
            else
            {
                try
                {
                    Result = Calculate(FirstOperand, SecondOperand, CurrentOperator).ToString();
                    SetDisplay(FirstOperand + " " + CurrentOperator + " " + SecondOperand + " =", "Result: " + Result);
                    FirstOperand = null;
                    SecondOperand = null;
                    CurrentOperator = null;
                }
                catch (DivideByZeroException e)
                {
                    SetDisplay(FirstOperand + " " + CurrentOperator + " " + SecondOperand + " =", "Result: Divide By Zero Exception");
                    FirstOperand = null;
                    SecondOperand = null;
                    CurrentOperator = null;
                }

            }
        }
        private void OnNegativeKeyPressed()
        {
            if (FirstOperand == null)
            {
                FirstOperand = "-";
                SetDisplay("", FirstOperand);

            }
            else if (FirstOperand != null && CurrentOperator == null)
            {
                if (FirstOperand.Contains("-"))
                {
                    // Remove - sign in first operand.
                    FirstOperand = FirstOperand.Substring(1, FirstOperand.Length - 1);

                }
                else
                {
                    // Add - sign to first operand.
                    FirstOperand = "-" + FirstOperand;
                }
                SetDisplay("", FirstOperand);

            }
            else if (CurrentOperator != null)
            {
                if (SecondOperand == null)
                {
                    SecondOperand = "-";
                }
                else
                {
                    if (SecondOperand.Contains("-"))
                    {
                        // Remove - sign in second operand.
                        SecondOperand = SecondOperand.Substring(1, SecondOperand.Length - 1);

                    }
                    else
                    {
                        // Add - sign to second operand.
                        SecondOperand = "-" + SecondOperand;

                    }
                }
                SetDisplay(FirstOperand + " " + CurrentOperator + " ", SecondOperand);

            }
        }
        private void OnEscKeyPressed()
        {

            SetDisplay("", "");
            FirstOperand = null;
            SecondOperand = null;
            CurrentOperator = null;
            Result = null;
        }
        private void OnBackspaceKeyPressed()
        {
            if (CurrentOperator == null && FirstOperand != null)
            {
                if (FirstOperand.Length == 1)
                {
                    FirstOperand = null;
                }
                else
                {
                    FirstOperand = FirstOperand.Substring(0, FirstOperand.Length - 1);

                }
                SetDisplay("", FirstOperand);

            }
            else if (CurrentOperator != null && SecondOperand != null)
            {
                if (SecondOperand.Length == 1)
                {
                    SecondOperand = null;
                }
                else
                {
                    SecondOperand = SecondOperand.Substring(0, SecondOperand.Length - 1);

                }
                SetDisplay(FirstOperand + " " + CurrentOperator + " ", SecondOperand);

            }
        }
        private void OnNumberKeysPressed(char pressedKeyName)
        {
            if (CurrentOperator == null)
            {
                if (!(pressedKeyName == '.' && FirstOperand.Contains(pressedKeyName)))
                {
                    if (FirstOperand == null)
                    {
                        FirstOperand = pressedKeyName.ToString();
                    }
                    else
                    {
                        FirstOperand = FirstOperand + pressedKeyName;
                    }
                    SetDisplay("", FirstOperand);
                }
            }
            if (CurrentOperator != null)
            {
                if (!(pressedKeyName == '.' && SecondOperand.Contains(pressedKeyName)))
                {
                    if (SecondOperand == null)
                    {
                        SecondOperand = pressedKeyName.ToString();

                    }
                    else
                    {
                        SecondOperand = SecondOperand + pressedKeyName;
                    }
                    SetDisplay(FirstOperand + " " + CurrentOperator + " ", SecondOperand);
                }
            }

        }
        private void OnMemoryFuntionKeyPressed(char pressedKeyName)
        {
            // M+ has been pressed.
            if (pressedKeyName == Constants.MemoryPlusKeyName)
            {
                if (Memory == null)
                {
                    Memory = "0";
                }
                if (Result != null && FirstOperand == null)
                {
                    Memory = Calculate(Memory, Result, "+").ToString();
                    SetDisplay("resultM+ = ", "result = " + Result);
                }
                else if (CurrentOperator == null && FirstOperand != null)
                {
                    Memory = Calculate(Memory, FirstOperand, "+").ToString();
                    Result = FirstOperand;

                    SetDisplay(FirstOperand + "M+ = ", "result = " + Result);
                    FirstOperand = null;
                }
                else if (CurrentOperator != null && SecondOperand != null)
                {
                    Result = Calculate(FirstOperand, SecondOperand, CurrentOperator).ToString();
                    Memory = Calculate(Memory, Result, "+").ToString();

                    SetDisplay("(" + FirstOperand + " " + CurrentOperator + " " + SecondOperand + ")M+", "result = " + Result);
                    FirstOperand = null;
                    SecondOperand = null;
                    CurrentOperator = null;

                }
            }
            // M- has been pressd.
            else if (pressedKeyName == Constants.MemorySubtractionKeyName)
            {
                if (Memory == null)
                {
                    Memory = "0";
                }
                if (Result != null && FirstOperand == null)
                {
                    Memory = Calculate(Memory, Result, "-").ToString();
                    SetDisplay("resultM- = ", "result = " + Result);
                }
                else if (CurrentOperator == null && FirstOperand != null)
                {

                    Memory = Calculate(Memory, FirstOperand, "-").ToString();
                    Result = FirstOperand;
                    SetDisplay(FirstOperand + "M- = ", "result = " + Result);
                    FirstOperand = null;

                }
                else if (CurrentOperator != null && SecondOperand != null)
                {
                    Result = Calculate(FirstOperand, SecondOperand, CurrentOperator).ToString();
                    Memory = Calculate(Memory, Result, "-").ToString();

                    SetDisplay("(" + FirstOperand + " " + CurrentOperator + " " + SecondOperand + ")M-", "result = " + Result);
                    FirstOperand = null;
                    SecondOperand = null;
                    CurrentOperator = null;

                }
            }
            // MS has been pressed.
            else if (pressedKeyName == Constants.MemorySaveKeyName)
            {

                if (Result != null && FirstOperand == null)
                {
                    Memory = Result;
                }
                else if (CurrentOperator == null && FirstOperand != null)
                {
                    Memory = FirstOperand;

                }
                else if (CurrentOperator != null && SecondOperand != null)
                {
                    Memory = SecondOperand;

                }
            }
            // MR has been pressed.
            else if (pressedKeyName == Constants.MemoryRecallKeyName)
            {
                FirstOperand = null;
                SecondOperand = null;
                CurrentOperator = null;
                if (Memory != null)
                {
                    Result = Memory;
                    SetDisplay("Memory = " + Result, "");

                }
                else
                {
                    SetDisplay("", "Memory is empty");

                }


            }
            else if (pressedKeyName == Constants.MemoryClearKeyName)
            {
                this.Memory = null;
            }

            UpdateMemoryStatus();
        }
        private void OnOperatorKeysPressed(char pressedKeyName)
        {
            if (SecondOperand == null && FirstOperand != null)
            {
                CurrentOperator = pressedKeyName.ToString();

            }
            else if (FirstOperand == null && Result != null)
            {
                FirstOperand = Result;
                CurrentOperator = pressedKeyName.ToString();
            }
            else if (FirstOperand != null && SecondOperand != null)
            {
                Result = Calculate(FirstOperand, SecondOperand, CurrentOperator).ToString();
                FirstOperand = Result;
                SecondOperand = null;
                CurrentOperator = pressedKeyName.ToString();
                SetDisplay(FirstOperand + " " + CurrentOperator, "");
            }
            SetDisplay(FirstOperand + " " + CurrentOperator + " ", "");

        }
        private void OnInverseKeyPressed()
        {

            if (Result != null && FirstOperand == null)
            {
                string previousResult = Result;
                Result = Calculate("1", Result, "/").ToString();
                SetDisplay("(1/"+previousResult +") = ", "result = " + Result);
            }
            else if (CurrentOperator == null && FirstOperand != null)
            {
                Result = Calculate("1",FirstOperand,"/").ToString();
                SetDisplay("(1/"+FirstOperand + ") = ", "result = " + Result);
                FirstOperand = null;
            }
            else if (CurrentOperator != null && SecondOperand != null)
            {
                Result = Calculate(FirstOperand, SecondOperand, CurrentOperator).ToString();
                Result = Calculate("1", Result, "/").ToString();
                SetDisplay("1/(" + FirstOperand + " " + CurrentOperator + " " + SecondOperand + ")", "result = " + Result);
                FirstOperand = null;
                SecondOperand = null;
                CurrentOperator = null;

            }
        }
        private void OnSquareRootKeyPressed()
        {
            if (Result != null && FirstOperand == null)
            {
                string previousResult = Result;
                Result = SquareRoot(Result).ToString();
                SetDisplay("sqrt(" + previousResult + ") = ", "result = " + Result);
            }
            else if (CurrentOperator == null && FirstOperand != null)
            {
                Result = SquareRoot(FirstOperand).ToString();
                SetDisplay("sqrt(" + FirstOperand + ") = ", "result = " + Result);
                FirstOperand = null;
            }
            else if (CurrentOperator != null && SecondOperand != null)
            {
                Result = Calculate(FirstOperand, SecondOperand, CurrentOperator).ToString();
                Result = SquareRoot(Result).ToString();
                SetDisplay("sqrt(" + FirstOperand + " " + CurrentOperator + " " + SecondOperand + ")", "result = " + Result);
                FirstOperand = null;
                SecondOperand = null;
                CurrentOperator = null;
            }
        }
    }
 
}
