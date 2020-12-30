using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;


namespace Schulte.ViewModels
{
    public class ViewModelsResolver
    {

        private readonly Dictionary<string, Func<INotifyPropertyChanged>> vmResolvers = new Dictionary<string, Func<INotifyPropertyChanged>>();

        public ViewModelsResolver()
        {
            //vmResolvers.Add(MainWindow.Ga, () => new Page1ViewModel());
            //vmResolvers.Add(MainViewModel.Page2ViewModelAlias, () => new Page2ViewModel());
            //vmResolvers.Add(MainViewModel.Page3ViewModelAlias, () => new Page3ViewModel());
            //vmResolvers.Add(MainViewModel.NotFoundPageViewModelAlias, () => new Page404ViewModel());
        }

        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
            //if (_vmResolvers.ContainsKey(alias))
            //{
                return vmResolvers[alias]();
            //}

            //return _vmResolvers[MainViewModel.NotFoundPageViewModelAlias]();
        }
    }
}
