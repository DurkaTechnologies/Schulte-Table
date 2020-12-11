using Schulte.Commands;
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
	class MainMenuModel : INotifyPropertyChanged
	{
		private Command statsCommand;
		private Command startGamePlusCommand;
		private Command startGameMinusCommand;
		private Command startAboutThisCommand;

		public MainMenuModel()
		{
			startGamePlusCommand = new DelegateCommand(StartPlusGame, () => true);

		}

		public ICommand StartGamePlusCommand => startGamePlusCommand;

		public void StartPlusGame()
		{
			//NavigationService nav = NavigationService.GetNavigationService(MainMenu.GetMainMenu());
			//nav.Navigate(new Uri("GamePage.xaml", UriKind.RelativeOrAbsolute));
		}


		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}
}
