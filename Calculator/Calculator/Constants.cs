using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorApplicaiton
{
    public class Constants
    {
        public const string SupportedOperatorKey = "+-*/^";
        public const string SupportedNumberKey = "1234567890.";
        public const string SupportedMemoryFuntionKey = "mlpsc";
        public const string Hint = "KEY HINT: esc:Reset | m:MR | p:M+ | s:M- | c:MC | l:MS | n:+/- | i:1/x | r:sqrt | enter:= | backspace";
        public const int EnterKeyCode = 13;
        public const char InverseKeyName = 'i';
        public const char NegativeKeyName = 'n';
        public const char SqrtKeyName = 'r';
        public const int EscKeyCode = 27;
        public const int BackspaceKeyCode = 8;
        public const char MemorySaveKeyName = 'l';
        public const char MemoryPlusKeyName = 'p';
        public const char MemorySubtractionKeyName = 's';
        public const char MemoryRecallKeyName = 'm';
        public const char MemoryClearKeyName = 'c';

    }
}
