
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
	public class RoundButton : Button
	{
		public static DependencyProperty ButtonThicknessProperty;
		public static DependencyProperty BorderStyleProperty;
		public static DependencyProperty BorderMarginProperty;
		public static DependencyProperty ButtonRadiusProperty;

		static RoundButton()
		{
			// Регистрация свойств зависимости
			ButtonThicknessProperty = DependencyProperty.Register("ButtonThickness", typeof(Thickness), typeof(RoundButton),
				new FrameworkPropertyMetadata(new Thickness(0,0,0,8)));
			BorderMarginProperty = DependencyProperty.Register("BorderMargin", typeof(Thickness), typeof(RoundButton),
				new FrameworkPropertyMetadata(new Thickness(0)));
			ButtonRadiusProperty = DependencyProperty.Register("ButtonRadius", typeof(CornerRadius), typeof(RoundButton),
				new FrameworkPropertyMetadata(new CornerRadius(8)));
			BorderStyleProperty = DependencyProperty.Register("BorderStyle", typeof(Style), typeof(RoundButton),
				new FrameworkPropertyMetadata(new Style(typeof(Border))));

			DefaultStyleKeyProperty.OverrideMetadata(typeof(RoundButton), new FrameworkPropertyMetadata(typeof(RoundButton)));
		}

		public Thickness ButtonThickness 
		{ 
			get => (Thickness)GetValue(BorderThicknessProperty);
			set => SetValue(BorderThicknessProperty, value);
		}

		public Style BorderStyle
		{
			get => (Style)GetValue(BorderStyleProperty);
			set => SetValue(BorderStyleProperty, value);
		}

		public Thickness BorderMargin
		{
			get => (Thickness)GetValue(BorderMarginProperty);
			set => SetValue(BorderMarginProperty, value);
		}
		public CornerRadius ButtonRadius
		{
			get => (CornerRadius)GetValue(ButtonRadiusProperty);
			set => SetValue(ButtonRadiusProperty, value);
		}
	}
}
