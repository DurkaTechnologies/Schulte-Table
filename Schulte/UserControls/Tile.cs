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
	public class Tile : RoundButton
	{
		public static DependencyProperty NumberProperty;
		public static DependencyProperty InstanceProperty;

		static Tile() 
		{
			NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(Tile),
				new FrameworkPropertyMetadata(default));

			InstanceProperty = DependencyProperty.Register("Instance", typeof(Tile), typeof(Tile));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(Tile), new FrameworkPropertyMetadata(typeof(Tile)));
		}

		public Tile()
		{
			PropertyChanged += (sender, args) =>
			{
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

		public Tile Instance
		{
			get => (Tile)GetValue(InstanceProperty);
			set => SetValue(InstanceProperty, value);
		}

	}
}
