using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ToDoList.UserControls
{
	public partial class Task : UserControl
	{
		public bool IsChecked = false;
        private readonly Brush BackgroundColor = (SolidColorBrush)Application.Current.Resources["ForegroundColor"];

        public Task()
		{
            InitializeComponent();
            DataContext = this;
        }

		private void CheckBoxClicked(object sender, RoutedEventArgs e)
		{
			IsChecked = !IsChecked;

            if (IsChecked) DotColor = BackgroundColor;
            else DotColor = Brushes.Transparent;
        }

		public static DependencyProperty DotColorProperty = DependencyProperty.Register("DotColor", typeof(Brush), typeof(Task));

		public Brush DotColor
		{
			get { return (Brush)GetValue(DotColorProperty); }
			set { SetValue(DotColorProperty, value); }
        }
    }
}
