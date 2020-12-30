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

	public class ScoreBlock : UserControl
	{
		public static DependencyProperty InfoProperty;
		public static DependencyProperty QuantityProperty;
		public static DependencyProperty CornerRadiusProperty;
		public static DependencyProperty BorderStyleProperty;
		public static DependencyProperty TextStyleProperty;
		public static DependencyProperty InfoPaddingProperty;
		public static DependencyProperty NumberPaddingProperty;


		static ScoreBlock()
		{
			InfoProperty = DependencyProperty.Register("ScoreInfo", typeof(String), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(null));
			QuantityProperty = DependencyProperty.Register("Quantity", typeof(int), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(0));
			BorderStyleProperty = DependencyProperty.Register("BorderStyle", typeof(Style), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(new Style(typeof(Border))));
			TextStyleProperty = DependencyProperty.Register("TextStyle", typeof(Style), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(new Style(typeof(Label))));
			CornerRadiusProperty = DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(new CornerRadius(8)));
			InfoPaddingProperty = DependencyProperty.Register("InfoPadding", typeof(Thickness), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(new Thickness(0,5,0,0)));
			NumberPaddingProperty = DependencyProperty.Register("NumberPadding", typeof(Thickness), typeof(ScoreBlock),
				new FrameworkPropertyMetadata(new Thickness(0,0,0,5)));
			DefaultStyleKeyProperty.OverrideMetadata(typeof(ScoreBlock), new FrameworkPropertyMetadata(typeof(ScoreBlock)));
		}
		public ScoreBlock()
		{
			BorderThickness = new Thickness(0, 0, 0, 8);
			Style style = new Style(typeof(Label));
			style.Setters.Add(new Setter(Label.FontSizeProperty, 25.0));
			style.Setters.Add(new Setter(Label.FontWeightProperty, FontWeights.Bold));
			style.Setters.Add(new Setter(Label.ForegroundProperty, new SolidColorBrush(Colors.Black)));
			style.Setters.Add(new Setter(Label.VerticalAlignmentProperty, VerticalAlignment.Center));
			TextStyle = style;
		}

		public Style BorderStyle
		{
			get => (Style)GetValue(BorderStyleProperty);
			set => SetValue(BorderStyleProperty, value);
		}

		public Style TextStyle
		{
			get => (Style)GetValue(TextStyleProperty);
			set => SetValue(TextStyleProperty, value);
		}

		public int Quantity
		{
			get => (int)GetValue(QuantityProperty);
			set => SetValue(QuantityProperty, value);
		}

		public String ScoreInfo
		{
			get => (String)GetValue(InfoProperty);
			set => SetValue(InfoProperty, value);
		}

		public CornerRadius CornerRadius
		{
			get => (CornerRadius)GetValue(CornerRadiusProperty);
			set => SetValue(CornerRadiusProperty, value);
		}

		public Thickness InfoPadding
		{
			get => (Thickness)GetValue(InfoPaddingProperty);
			set => SetValue(InfoPaddingProperty, value);
		}

		public Thickness NumberPadding
		{
			get => (Thickness)GetValue(NumberPaddingProperty);
			set => SetValue(NumberPaddingProperty, value);
		}
	}
}
