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
		private ICollection<RoundButton> tileCollection;
		private int size;
		private int center;
		Random random;
		ImageButton centerElement;
		IEnumerable<Style> styles;

		private GameView()
		{
			random = new Random();
			CreateCenterElement();
			styles = ResourcesParser.GetStylesFromResourcesDictionary("pack://application:,,,/Resources/TileDictionary.xaml");
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(Size))
					FillGrid();
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

		public void FillGrid()
		{
			int number;
			Tile tile;
			int quantityTiles = (size * size);
			List<int> numbers = GenarateNumbers(quantityTiles);
			int stylesQuantity = styles.Count();
			ICollection<RoundButton> tempCollection = new ObservableCollection<RoundButton> { };
			CountCenter();
			for (int i = 0; i < quantityTiles; i++)
			{
				tile = new Tile();
				if (i == center)
				{
					tempCollection.Add(centerElement);
					continue;
				}
				tile.Style = styles.ElementAt(0);
				number = numbers[random.Next(0, numbers.Count)];
				tile.Number = number;
				numbers.Remove(number);
				tempCollection.Add(tile);
			}

			if (tileCollection != null)
				tileCollection.Clear();
			TileCollection = tempCollection;
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

		private void CreateCenterElement()
		{
			if (centerElement == null)
			{
				centerElement = new ImageButton();
				var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Resource.eye.GetHbitmap(),
						  IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
				centerElement.LeaveImage = bitmapSource;

				bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Resource.eyeEnter.GetHbitmap(),
						  IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
				centerElement.EnterImage = bitmapSource;
			}
		}

		public void CountCenter()
		{
			center = (int)Math.Round((float)((size * size) / 2.0), MidpointRounding.AwayFromZero) - 1;
		}

		public void SetSchulteMode()
		{
			int stylesQuantity = styles.Count();
			int quantityTiles = (size * size);
			for (int i = 0; i < quantityTiles; i++)
			{
				if (i == center)
					continue;
				TileCollection.ElementAt(i).Style = styles.ElementAt(random.Next(0, stylesQuantity));
			}
		}

		public void SetDefaultMode()
		{
			int quantityTiles = (size * size);
			for (int i = 0; i < quantityTiles; i++)
			{
				if (i == center)
					continue;
				TileCollection.ElementAt(i).Style = styles.ElementAt(0);
			}
		}

		public List<int> GenarateNumbers(int quantity)
		{
			List<int> numbers = new List<int> { };
			for (int i = 1; i < quantity; i++)
				numbers.Add(i);

			return numbers;
		}

		public event PropertyChangedEventHandler PropertyChanged;

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
