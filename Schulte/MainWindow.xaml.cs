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
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Schulte
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private int seconds;
		private int minutes;

		Style style;
		Style incorrect;
		private int size;
		private int quantity;
		private Int32Point center;
		int currentNum;
		DispatcherTimer gameTimer;
		ColorAnimation animation1;
		ColorAnimation animation2;
		public MainWindow()
		{
			InitializeComponent();
			seconds = 0;
			minutes = 0;
			gameTimer = new DispatcherTimer();
			gameTimer.Tick += new EventHandler(timer_Tick);
			gameTimer.Interval = new TimeSpan(0, 0, 1);
			currentNum = 0;
			Random random = new Random();
			
			style = new Style();
			style = TryFindResource("numStyle") as Style;
			incorrect = TryFindResource("marginStyle") as Style; 
			style.Setters.Add(new EventSetter { Event = Button.ClickEvent, Handler = new RoutedEventHandler(Button_Click) });
			incorrect.Setters.Add(new EventSetter { Event = Button.ClickEvent, Handler = new RoutedEventHandler(Button_Click) });

			animation1 = new ColorAnimation();
			//animation1.From = Color.FromRgb(216, 84, 174);
			animation1.To = Colors.Red;
			animation1.Duration = TimeSpan.FromMilliseconds(500);

			 animation2 = new ColorAnimation();
			//animation1.From = Color.FromRgb(216, 84, 174);
			animation2.To = Color.FromRgb(216, 84, 174);
			animation2.Duration = TimeSpan.FromMilliseconds(500);
			CreateGrid();

			CountCenter();
			FillGrid();

			Border border = new Border();
			border.Child = new Image();
			var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Resource.eye.GetHbitmap(),
								  IntPtr.Zero,
								  Int32Rect.Empty,
								  BitmapSizeOptions.FromEmptyOptions());
			(border.Child as Image).Source  = bitmapSource;
			//border.Style = style;
			Grid.Children.Add(border);
			Grid.SetColumn(border, center.X);
			Grid.SetRow(border, center.Y);

			this.bar.Maximum = (size * size) - 1;

		}

		private void CreateGrid()
		{
				Grid.ColumnDefinitions.Clear();
				Grid.RowDefinitions.Clear();
			Grid.Children.Clear();
			size = (int)this.slider.Value;

			for (int i = 0; i < size; i++)
			{
				Grid.RowDefinitions.Add(new RowDefinition());
				Grid.ColumnDefinitions.Add(new ColumnDefinition());
			}
		}

		private void FillGrid()
		{
			
			Random random = new Random();
			int num;
			Button btn;
			List<int> numbers = new List<int> { };
			for (int i = 1; i < (size * size); i++)
				numbers.Add(i);

			for (int i = 0; i < Grid.ColumnDefinitions.Count; i++)
			{
				for (int j = 0; j < Grid.RowDefinitions.Count; j++)
				{
					if ((center.X == i) && (center.Y == j))
						continue;

					btn = new Button();
					num = numbers[random.Next(0, numbers.Count)];
					btn.Content = num;
					numbers.Remove(num);
					btn.Style = style;
					Grid.Children.Add(btn);
					Grid.SetColumn(btn, i);
					Grid.SetRow(btn, j);
				}
			}
			
		}
		private void myanim_Completed(object sender, EventArgs e)
		{

		}

		private void SetCenterElement()
		{
			Border border = new Border();
			border.Child = new Image();
			var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Resource.eye.GetHbitmap(),
								  IntPtr.Zero,
								  Int32Rect.Empty,
								  BitmapSizeOptions.FromEmptyOptions());
			(border.Child as Image).Source = bitmapSource;
			//border.Style = style;
			Grid.Children.Add(border);
			Grid.SetColumn(border, center.X);
			Grid.SetRow(border, center.Y);
		}

		private void SetStyle(string styleKey)
		{
			style = TryFindResource("numStyle") as Style;
			if (style == null)
			{
				
				style = new Style();
				MessageBox.Show("Incorrect style", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}
		}

		void timer_Tick(object sender, EventArgs e)
		{
			++seconds;
			if (seconds % 60 == 0)
			{
				minutes++;
				seconds = 0;
				timer.Text = (minutes < 10 ? $"0{minutes}" : $"{minutes}") + ":00";
			}
			else
				timer.Text = (minutes < 10 ? $"0{minutes}" : $"{minutes}") + ':'
					+ (seconds < 10 ? $"0{seconds}" : $"{seconds}");
		}

		public void CountCenter()
		{
			int position = size - size / 2;

			center = new Int32Point(position - 1, position - 1);
		}
	
		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (currentNum == 0)
				gameTimer.Start();
			Button current = sender as Button;
			int thisButtonNum = int.Parse(current.Content.ToString());
			if (currentNum+1 == thisButtonNum)
			{
				current.Style = style;
				currentNum++;
				current.IsEnabled = false;
				this.bar.Value++;
				current.BorderThickness = new Thickness(0);
				current.Margin = new Thickness(3,6,3,3);
			}
			else
				current.Style = incorrect;
		}



		private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (Grid == null)
				return;
			this.bar.Maximum = (size * size) - 1;
			CreateGrid();
			CountCenter();

			FillGrid();
			SetCenterElement();
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
	
	public class GameBoard
	{
		public GameBoard(int size, string stylekey)
		{
			Size = size;
			CountCenter();
			style = new Style();

		}
		public GameBoard(int size, ref Grid grid, Style style)
		{
			Size = size;
			CountCenter();
			this.style = style;
		}

		private int size;
		private Int32Point center;
		Style style;
		int currentNum;

		public int Size
		{ 
			get => size; 
			set => size = (value > 2) && (value % 2 != 0) ? value : 5; 
		}

		public Int32Point Center => center;

		//public void SetGrid(ref Grid grid)
		//{
		//	if (grid == null)
		//		MessageBox.Show("Grid is null", "Error", MessageBoxButton.OKCancel, MessageBoxImage.Error);
		//	else
		//		this.grid = grid;
		//}

		public void CountCenter()
		{
			int positionX = size - size / 2;

			center = new Int32Point(size - 1, size - 1);
		}

		public void CreateButtons()
		{
			int [] numbers = new int[] { };
			for (int i = 1; i < (size * size); i++)
			{
				Array.Resize(ref numbers, numbers.Length + 1);
				numbers[i] = i + 1;
			}

			Random random = new Random();

			for (int i = 0; i < size; i++)
			{
				for (int j = 0; j < size; j++)
				{
					if ((center.X == size) && (center.Y == size))
						continue;

				}
			}
		}

	}
}


