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

namespace Schulte.UserControls
{
	/// <summary>
	/// Interaction logic for WinPage.xaml
	/// </summary>
	public partial class WinPage : UserControl
	{
		public WinPage()
		{
			InitializeComponent();
		}

		private void RoundButton_Click(object sender, RoutedEventArgs e)
		{
			GamePageViewModel.GetInstance().RestartGame();
		}
	}
}
