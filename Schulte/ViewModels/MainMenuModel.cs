
using Schulte.Commands;
using Schulte.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Schulte.ViewModels
{
	class MainMenuModel : BaseViewModel
	{
		//public static readonly string Page1ViewModelAlias = "Page1VM";
		//public static readonly string Page2ViewModelAlias = "Page2VM";
		//public static readonly string Page3ViewModelAlias = "Page3VM";
		//public static readonly string NotFoundPageViewModelAlias = "404VM";

		private Command goToPathCommand;
		private Command statsCommand;
		private Command startGamePlusCommand;
		private Command startGameMinusCommand;
		private Command startAboutThisCommand;

		//private readonly IViewModelsResolver _resolver;


		private readonly INotifyPropertyChanged _p1ViewModel;
		private readonly INotifyPropertyChanged _p2ViewModel;
		private readonly INotifyPropertyChanged _p3ViewModel;

		public MainMenuModel()
		{
			startGamePlusCommand = new DelegateCommand(StartPlusGame, () => true);
		}

		private void InitializeCommands()
		{

			//GoToPathCommand = new RelayCommand<string>(GoToPathCommandExecute);

			statsCommand = new DelegateCommand(StartPlusGame, () => true);

			//GoToPage2Command = new RelayCommand<INotifyPropertyChanged>(GoToPage2CommandExecute);

			//GoToPage3Command = new RelayCommand<INotifyPropertyChanged>(GoToPage3CommandExecute);
		}


		public ICommand StartGamePlusCommand => startGamePlusCommand;

		public void StartPlusGame()
		{

			Navigation.Navigation.Navigate(Navigation.Navigation.GameBoardAlias, GamePageViewModel.GetInstance());

		}

	}
}
