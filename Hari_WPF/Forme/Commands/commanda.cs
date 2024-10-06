using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hari_WPF.Forme.Commands
{
    public class Commanda : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action izvrsi;


        public Commanda(Action execute)
        {
            izvrsi = execute; 
        }
        

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            izvrsi.Invoke();
        }
    }
}
