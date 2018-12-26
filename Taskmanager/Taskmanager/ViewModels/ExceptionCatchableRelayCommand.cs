using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taskmanager.ViewModels
{
    public class ExceptionCatchableRelayCommand : RelayCommand
    {

            public ExceptionCatchableRelayCommand(Action execute) : base(execute)
            {
            }

             public override void Execute(object parameter)
            {
            try
            {
                base.Execute(parameter);
            } catch (Exception e)
            {
                throw e;
            }

             }
    }
}
