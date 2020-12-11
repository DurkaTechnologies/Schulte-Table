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
	/// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
	///
	/// Step 1a) Using this custom control in a XAML file that exists in the current project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:Schulte.UserControls"
	///
	///
	/// Step 1b) Using this custom control in a XAML file that exists in a different project.
	/// Add this XmlNamespace attribute to the root element of the markup file where it is 
	/// to be used:
	///
	///     xmlns:MyNamespace="clr-namespace:Schulte.UserControls;assembly=Schulte.UserControls"
	///
	/// You will also need to add a project reference from the project where the XAML file lives
	/// to this project and Rebuild to avoid compilation errors:
	///
	///     Right click on the target project in the Solution Explorer and
	///     "Add Reference"->"Projects"->[Browse to and select this project]
	///
	///
	/// Step 2)
	/// Go ahead and use your control in the XAML file.
	///
	///     <MyNamespace:Tile/>
	///
	/// </summary>
	public class Tile : Label
	{
		public static DependencyProperty NumberProperty;
		public static DependencyProperty TileMarginProperty;
		public static DependencyProperty BorderStyleProperty;
		public static DependencyProperty TileRadiusProperty;

		static Tile() 
		{
			NumberProperty = DependencyProperty.Register("Number", typeof(int), typeof(Tile),
				new FrameworkPropertyMetadata(0));
			TileMarginProperty = DependencyProperty.Register("BorderMargin", typeof(Thickness), typeof(Tile),
				new FrameworkPropertyMetadata(new Thickness(0)));
			TileRadiusProperty = DependencyProperty.Register("TileRadius", typeof(CornerRadius), typeof(Tile),
				new FrameworkPropertyMetadata(new CornerRadius(8)));
			BorderStyleProperty = DependencyProperty.Register("BorderStyle", typeof(Style), typeof(Tile),
				new FrameworkPropertyMetadata(new Style(typeof(Border))));
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Tile), new FrameworkPropertyMetadata(typeof(Tile)));
		}

		public Tile()
		{
			BorderThickness = new Thickness(0, 0, 0, 8);
		}


		public int Number
		{
			get => (int)GetValue(NumberProperty);
			set => SetValue(NumberProperty, value);
		}
		public Style BorderStyle
		{
			get => (Style)GetValue(BorderStyleProperty);
			set => SetValue(BorderStyleProperty, value);
		}
		public Thickness BorderMargin
		{
			get => (Thickness)GetValue(TileMarginProperty);
			set => SetValue(TileMarginProperty, value);
		}

		public CornerRadius TileRadius
		{
			get => (CornerRadius)GetValue(TileRadiusProperty);
			set => SetValue(TileRadiusProperty, value);
		}
	}
}
