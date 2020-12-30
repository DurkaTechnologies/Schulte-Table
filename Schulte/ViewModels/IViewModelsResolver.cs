using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Schulte.ViewModels
{
    public interface IViewModelsResolver
    {
        INotifyPropertyChanged GetViewModelInstance(string alias);
    }
}
