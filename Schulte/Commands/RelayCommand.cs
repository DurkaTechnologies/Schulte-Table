using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Schulte.Commands
{
	class RelayCommand : ICommand
	{
		private static readonly Func<object, bool> defaultCanExecuteMethod = (parameter) => true;

		private readonly Func<object, bool> canExecuteMethod;
		private readonly Action<object> executeMethod;

		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action<object> executeMethod) :
			this(executeMethod, defaultCanExecuteMethod)
		{
		}

		public RelayCommand(Action<object> executeMethod, Func<object,  bool> canExecuteMethod)
		{
			this.canExecuteMethod = canExecuteMethod;
			this.executeMethod = executeMethod;
		}

		public bool CanExecute(object parameter)
		{
			return canExecuteMethod(parameter);
		}

		public void Execute(object parameter)
		{
				executeMethod(parameter);
		}

		protected virtual void OnCanExecuteChanged(EventArgs e)
		{
			CanExecuteChanged?.Invoke(this, e);
		}

		public void RaiseCanExecuteChanged()
		{
			OnCanExecuteChanged(EventArgs.Empty);
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute(parameter);
		}

		void ICommand.Execute(object parameter)
		{
			Execute(parameter);
		}
	}
}
