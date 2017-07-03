using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MWX.XamForms.Popup.Examples.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string name =null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

    }
}
