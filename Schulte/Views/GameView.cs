using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;
using Schulte.UserControls;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Schulte.ViewModels;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace Schulte.Views
{
	class GameView : INotifyPropertyChanged
	{

		public event PropertyChangedEventHandler PropertyChanged;

		private ICollection<RoundButton> tileCollection;
		private int size;
		private Int32Point center;
		Random random;

		private GameView()
		{
			random = new Random();
			FillGrid();

			//SetStartValues();
			//currectTime = new Time();

			//CreateGrid();
			//CountCenter();
			//FillGrid();
			//SetCenterElement();
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(Size))
				{
					FillGrid();
				}
			};
		}

		private static GameView instance;
		private ICommand TilePressCommand { get; set; }
		public static GameView GetInstance()
		{
			if (instance == null)
				instance = new GameView();
			return instance;
		}

		private void FillGrid()
		{
			int number;
			Tile tile;

			List<int> numbers = new List<int> { };
			int quantityTiles = (size * size);
			for (int i = 1; i < quantityTiles; i++)
				numbers.Add(i);
			ICollection<RoundButton> tempCollection = new ObservableCollection<RoundButton> { };
			int center = (int)Math.Round((float)((size * size) / 2.0), MidpointRounding.AwayFromZero) -1;
			for (int i = 0; i < quantityTiles; i++)
			{
				tile = new Tile();
				if (i == center)
				{
					ImageButton centerTile = new ImageButton();
					var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Resource.eye.GetHbitmap(),
							  IntPtr.Zero,
							  Int32Rect.Empty,
							  BitmapSizeOptions.FromEmptyOptions());
					centerTile.LeaveImage = bitmapSource;
					centerTile.IsEnabled = false;
					tempCollection.Add((RoundButton)centerTile);
					continue;
				}

				number = numbers[random.Next(0, numbers.Count)];
				tile.Number = number;
				numbers.Remove(number);
				tempCollection.Add(tile);
			}

			if (tileCollection != null)
				tileCollection.Clear();
			tileCollection = tempCollection;
		}

		public int Size
		{
			get => size;
			set
			{
				size = value;
				OnPropertyChanged();
			}
		}


		public ICollection<RoundButton> TileCollection
		{
			get => tileCollection;
			set
			{
				tileCollection = value;
				OnPropertyChanged();
			}
		}


		private void SetCenterElement()
		{
			ImageButton centerControl = new ImageButton();
			//centerControl.EnterImage =  ImageSource;
			//var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Resource.eye.GetHbitmap(),
			//					  IntPtr.Zero,
			//					  Int32Rect.Empty,
			//					  BitmapSizeOptions.FromEmptyOptions());
			//centerControl.BorderStyle = TryFindResource("baseStyle") as Style;
			//Grid.Children.Add(border);
			//Grid.SetColumn(border, center.X);
			//Grid.SetRow(border, center.Y);
		}



		public void CountCenter()
		{
			int position = size - size / 2;

			center = new Int32Point(position - 1, position - 1);
		}

		//private void Border_MouseUp(object sender, MouseButtonEventArgs e)
		//{
		//	restartBtn.IsEnabled = false;
		//}

		/*private void Border_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (currentNum == 0)
			{
				gameTimer.Start();
				slider.IsEnabled = false;
				restartBtn.IsEnabled = true;
				restartBtn.BorderThickness = new Thickness(0, 0, 0, 6);
				restartBtn.Margin = new Thickness(20, 0, 20, 10);
				restartBtn.MouseUp -= Border_MouseUp;

			}
			Border current = sender as Border;
			int thisButtonNum = int.Parse((current.Child as TextBlock)?.Text);
			if (currentNum + 1 == thisButtonNum)
			{
				correctClicks++;
				correctClicksText.Text = correctClicks.ToString();

				current.Style = correctStyle;
				currentNum++;
				current.IsEnabled = false;
				this.bar.Value++;
				current.Background = current.BorderBrush;
				CheckWin();
			}
			else
			{
				wrongClicks++;
				wrongClicksText.Text = wrongClicks.ToString();
				current.Style = incorrectStyle;
			}
		}*/

		//private void CheckWin()
		//{
		//	if (currentNum == (size * size) - 1)
		//	{
		//		gameTimer.Stop();
		//		slider.IsEnabled = true;
		//		if (minTime.IsReset())
		//			minTime = currectTime;
		//		else if (minTime >= currectTime)
		//			minTime = currectTime;
		//		recordTime.Text = minTime.Text;
		//		MessageBox.Show("YOU WIN", "WIN", MessageBoxButton.OK, MessageBoxImage.None);
		//	}
		//}

		//private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		//{
		//	if (Grid == null)
		//		return;
		//	this.bar.Value = 0;
		//	CreateGrid();
		//	CountCenter();
		//	FillGrid();
		//	SetCenterElement();
		//	minTime.ResetTime();
		//	recordTime.Text = minTime.Text;
		//	this.bar.Maximum = (size * size) - 1;
		//	restartBtn.IsEnabled = true;

		//}

		//private void Restart_MouseDown(object sender, MouseButtonEventArgs e)
		//{
		//	gameTimer.Stop();
		//	SetStartValues();
		//	bar.Value = 0;
		//	CreateGrid();
		//	CountCenter();
		//	FillGrid();
		//	SetCenterElement();
		//	restartBtn.MouseUp += Border_MouseUp;

		//	slider.IsEnabled = true;

		//}

		


		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

	}

	public struct Int32Point
	{

		public Int32Point(int x, int y)
		{
			X = x;
			Y = y;
		}
		public int X { get; set; }
		public int Y { get; set; }
	}

}
