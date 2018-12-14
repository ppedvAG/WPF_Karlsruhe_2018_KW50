using ppedv.Weihnachtsbaeckerei.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ppedv.Weihnachtsbaeckerei.UI.WPF.ViewModels
{
    public class SaveCommand : ICommand
    {
        EfContext con;
        public SaveCommand(EfContext con)
        {
            this.con = con;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            con.SaveChanges();
        }
    }
}
