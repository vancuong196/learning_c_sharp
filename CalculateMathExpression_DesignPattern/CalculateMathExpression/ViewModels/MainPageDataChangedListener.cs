﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateMathExpression.ViewModels
{
    public interface MainPageDataChangedListener
    {
        void OnSavedFormula(string formulaX, string formulaY);

    }
}
