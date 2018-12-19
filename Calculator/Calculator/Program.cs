
using System;
using System.Linq;

namespace CalculatorApplicaiton
{
    class Program
    {

        static void Main(string[] args)
        {

            Calculator calculator = new Calculator();
            while (true)
            {
                // Wait for key input.
                char pressedKeyName = ReadKey();

                // Memory related funtion key has been pressed.

                if (Constants.SupportedMemoryFuntionKey.Contains(pressedKeyName))
                {
                    calculator.OnMemoryFuntionKeyPressed(pressedKeyName);
                }
                // Operator key has been pressed.
                else if (Constants.SupportedOperatorKey.Contains(pressedKeyName))
                {
                    calculator.OnOperatorKeysPressed(pressedKeyName);

                }
                // Numberic key has been pressed.
                else if (Constants.SupportedNumberKey.Contains(pressedKeyName))
                {
                    calculator.OnNumberKeysPressed(pressedKeyName);
                }
                // Enter key has been press.
                else if (pressedKeyName == (char)Constants.EnterKeyCode)
                {
                    calculator.OnEnterKeyPressed();
                }
                // Esc key has been press.
                else if (pressedKeyName == (char)Constants.EscKeyCode)
                {

                    calculator.OnEscKeyPressed();
                }
                // Backspace key has been press.
                else if (pressedKeyName == (char)Constants.BackspaceKeyCode)
                {
                    calculator.OnBackspaceKeyPressed();
                }
                // Negative key has been press. 
                else if (pressedKeyName == Constants.NegativeKeyName)
                {
                    calculator.OnNegativeKeyPressed();
                }
                // Inverse key pressed 
                else if (pressedKeyName == Constants.InverseKeyName)
                {
                    calculator.OnInverseKeyPressed();
                }
                // SquareRoot key pressed.
                else if (pressedKeyName == Constants.SqrtKeyName)
                {
                    calculator.OnSquareRootKeyPressed();
                }

            }
        }
        // Read single key from keyboard.
        public static char ReadKey()
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Char keyName = keyInfo.KeyChar;
            return keyName;

        }
    }

}
