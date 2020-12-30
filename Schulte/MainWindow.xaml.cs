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
		//Style correctStyle;
		//Style incorrectStyle;
		//Time currectTime;
		//Time minTime;
		//private int size;
		//private Int32Point center;
		//int currentNum;
		//int correctClicks;
		//int wrongClicks;


		DispatcherTimer gameTimer;
		public MainWindow()
		{
			InitializeComponent();
			//gameTimer = new DispatcherTimer();
			//gameTimer.Tick += new EventHandler(timer_Tick);
			//gameTimer.Interval = new TimeSpan(0, 0, 1);
			//SetStartValues();
			//currectTime = new Time();
			//if (!InitializeStyles())
			//	this.Close();

			//CreateGrid();
			//CountCenter();
			//FillGrid();
			//SetCenterElement();

			//this.bar.Maximum = (size * size) - 1;
			
		}

	//	private bool InitializeStyles()
	//	{
	//		correctStyle = TryFindResource("pressStyle") as Style;
	//		if (correctStyle == null)
	//			return false;
	//		incorrectStyle = TryFindResource("marginStyle") as Style;

	//		if (incorrectStyle == null)
	//			return false;
	//		return true;

	//	}

	//	private void CreateGrid()
	//	{
	//		Grid.ColumnDefinitions.Clear();
	//		Grid.RowDefinitions.Clear();
	//		Grid.Children.Clear();
	//		size = (int)this.slider.Value;

	//		for (int i = 0; i < size; i++)
	//		{
	//			Grid.RowDefinitions.Add(new RowDefinition());
	//			Grid.ColumnDefinitions.Add(new ColumnDefinition());
	//		}
	//	}

	//	private void FillGrid()
	//	{
			
	//		Random random = new Random();
	//		int num;
	//		Border btn;
	//		TextBlock btnText;
	//		List<int> numbers = new List<int> { };
	//		for (int i = 1; i < (size * size); i++)
	//			numbers.Add(i);

	//		for (int i = 0; i < Grid.ColumnDefinitions.Count; i++)
	//		{
	//			for (int j = 0; j < Grid.RowDefinitions.Count; j++)
	//			{
	//				if ((center.X == i) && (center.Y == j))
	//					continue;

	//				btn = new Border();
	//				btnText = new TextBlock();
	//				btnText.VerticalAlignment = VerticalAlignment.Center;
	//				num = numbers[random.Next(0, numbers.Count)];
	//				btnText.Text = num.ToString();
	//				btn.Child = btnText;
	//				numbers.Remove(num);
	//				btn.Style = correctStyle;
	//				Grid.Children.Add(btn);
	//				Grid.SetColumn(btn, i);
	//				Grid.SetRow(btn, j);
	//			}
	//		}
	//	}

	//	private void SetCenterElement()
	//	{
	//		Border border = new Border();
	//		border.Child = new Image();
	//		var bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(Resource.eye.GetHbitmap(),
	//							  IntPtr.Zero,
	//							  Int32Rect.Empty,
	//							  BitmapSizeOptions.FromEmptyOptions());
	//		(border.Child as Image).Source = bitmapSource;
	//		border.Style = TryFindResource("baseStyle") as Style;
	//		Grid.Children.Add(border);
	//		Grid.SetColumn(border, center.X);
	//		Grid.SetRow(border, center.Y);
	//	}

	//	void timer_Tick(object sender, EventArgs e)
	//	{
	//		currectTime.AddSecond();
	//		timer.Text = currectTime.Text;
	//	}

	//	public void CountCenter()
	//	{
	//		int position = size - size / 2;

	//		center = new Int32Point(position - 1, position - 1);
	//	}
	//	private void Border_MouseUp(object sender, MouseButtonEventArgs e)
	//	{
	//		restartBtn.IsEnabled = false;
	//	}
	//	private void Border_MouseDown(object sender, MouseButtonEventArgs e)
	//	{
	//		if (currentNum == 0)
	//		{
	//			gameTimer.Start();
	//			slider.IsEnabled = false;
	//			restartBtn.IsEnabled = true;
	//			restartBtn.BorderThickness = new Thickness(0, 0, 0, 6);
	//			restartBtn.Margin = new Thickness(20, 0, 20, 10);
	//			restartBtn.MouseUp -= Border_MouseUp;

	//		}
	//		Border current = sender as Border;
	//		int thisButtonNum = int.Parse((current.Child as TextBlock)?.Text);
	//		if (currentNum + 1 == thisButtonNum)
	//		{
	//			correctClicks++;
	//			correctClicksText.Text = correctClicks.ToString();

	//			current.Style = correctStyle;
	//			currentNum++;
	//			current.IsEnabled = false;
	//			this.bar.Value++;
	//			current.Background = current.BorderBrush;
	//			CheckWin();
	//		}
	//		else
	//		{
	//			wrongClicks++;
	//			wrongClicksText.Text = wrongClicks.ToString();
	//			current.Style = incorrectStyle;
	//		}
	//	}

	//	private void CheckWin()
	//	{
	//		if (currentNum == (size*size) - 1)
	//		{
	//			gameTimer.Stop();
	//			slider.IsEnabled = true;
	//			if (minTime.IsReset())
	//				minTime = currectTime;
	//			else if (minTime >= currectTime)
	//				minTime = currectTime;
	//			recordTime.Text = minTime.Text;
	//			MessageBox.Show("YOU WIN", "WIN", MessageBoxButton.OK, MessageBoxImage.None);
	//		}
	//	}

	//	private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
	//	{
	//		if (Grid == null)
	//			return;
	//		this.bar.Value = 0;
	//		CreateGrid();
	//		CountCenter();
	//		FillGrid();
	//		SetCenterElement();
	//		minTime.ResetTime();
	//		recordTime.Text = minTime.Text;
	//		this.bar.Maximum = (size * size) - 1;
	//		restartBtn.IsEnabled = true;

	//	}

	//	private void Restart_MouseDown(object sender, MouseButtonEventArgs e)
	//	{
	//		gameTimer.Stop();
	//		SetStartValues();
	//		bar.Value = 0;
	//		CreateGrid();
	//		CountCenter();
	//		FillGrid();
	//		SetCenterElement();
	//		restartBtn.MouseUp += Border_MouseUp;

	//		slider.IsEnabled = true;

	//	}

	//	private void SetStartValues()
	//	{
	//		currectTime.ResetTime();
	//		correctClicks = 0;
	//		wrongClicks = 0;
	//		currentNum = 0;
	//		correctClicksText.Text = correctClicks.ToString();
	//		wrongClicksText.Text = wrongClicks.ToString();
	//		timer.Text = "00:00";
	//	}
	}

	

	


}


