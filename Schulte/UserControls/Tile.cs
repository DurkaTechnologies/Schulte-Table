using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Schulte.UserControls
{
	public enum PressType
	{
		Correct,
		Incorrect,
		None
	}

	public class Tile : RoundButton, INotifyPropertyChanged
	{
		public static DependencyProperty NumberProperty;
		public static DependencyProperty IsNumberNullProperty;
		public static DependencyProperty IsCorrectPressProperty;
		public static DependencyProperty InstanceProperty;
		public static DependencyProperty SecondBackgroundProperty;
		public static DependencyProperty SecondBorderBrushProperty;

		static Tile()
		{
			NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(Tile),
				new FrameworkPropertyMetadata(default));

			IsNumberNullProperty = DependencyProperty.Register("IsNumberNull", typeof(bool), typeof(Tile),
				new FrameworkPropertyMetadata(null));

			IsCorrectPressProperty = DependencyProperty.Register("IsCorrectPress", typeof(PressType), typeof(Tile),
				new FrameworkPropertyMetadata(PressType.None));

			InstanceProperty = DependencyProperty.Register("Instance", typeof(Tile), typeof(Tile));

			SecondBackgroundProperty = DependencyProperty.Register("SecondBackground", typeof(SolidColorBrush), typeof(Tile),
				new FrameworkPropertyMetadata(null));

			SecondBorderBrushProperty = DependencyProperty.Register("SecondBorderBrush", typeof(Brush), typeof(Tile),
				new FrameworkPropertyMetadata(null));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(Tile), new FrameworkPropertyMetadata(typeof(Tile)));
		}

		public Tile()
		{
			PropertyChanged += (sender, args) =>
			{
				if (args.PropertyName == nameof(Number))
					if (Number < 0)
						IsNumberNull = true;
					else
						IsNumberNull = false;

				if (args.PropertyName == nameof(Instance))
					Instance = this;

			};
		}

		public int Number
		{
			get => (int)GetValue(NumberProperty);
			set
			{
				SetValue(NumberProperty, value);
				OnPropertyChanged();
				OnPropertyChanged(nameof(Instance));
			}
		}

		public bool? IsNumberNull
		{
			get => (bool)GetValue(IsNumberNullProperty);
			set => SetValue(IsNumberNullProperty, value);
		}

		public PressType? IsCorrectPress
		{
			get => (PressType)GetValue(IsCorrectPressProperty);
			set
			{
				SetValue(IsCorrectPressProperty, value);
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		public Tile Instance
		{
			get => (Tile)GetValue(InstanceProperty);
			set => SetValue(InstanceProperty, value);
		}

		public SolidColorBrush SecondBackground
		{
			get => (SolidColorBrush)GetValue(SecondBackgroundProperty);
			set => SetValue(SecondBackgroundProperty, value);
		}

		public Brush SecondBorderBrush
		{
			get => (Brush)GetValue(SecondBorderBrushProperty);
			set => SetValue(SecondBorderBrushProperty, value);
		}
	}
}
