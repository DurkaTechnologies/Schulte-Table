using Schulte.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Schulte
{
	/// <summary>
	/// Interaction logic for MainMenu.xaml
	/// </summary>
	public partial class MainMenu : Page
	{

		public MainMenu()
		{
			InitializeComponent();
			//Navigation.Navigation.Service = NavigationService.GetNavigationService(this);

			//this.DataContext = new MainMenuModel(); ;
		}

		private void Page_Loaded(object sender, RoutedEventArgs e)
		{
			Navigation.Navigation.Service = NavigationService.GetNavigationService(this);

			this.DataContext = new MainMenuModel();
		}

		//public static MainMenu GetMainMenu()
		//{
		//	return new MainMenu();
		//}

		//private void RoundButton_Click(object sender, RoutedEventArgs e)
		//{
		//	NavigationService nav = NavigationService.GetNavigationService(this);
		//	nav.Navigate(new Uri("GamePage.xaml", UriKind.RelativeOrAbsolute));
		//}
	}
}
