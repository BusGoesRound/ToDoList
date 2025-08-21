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
            TaskText = "Task";

            InitializeComponent();
            DataContext = this;
        }

        public Task(string taskText, bool isChecked = false)
        {
            IsChecked = isChecked;
            UpdateCheckBox();
            TaskText = taskText;

            InitializeComponent();
            DataContext = this;
        }

        override public string ToString()
        {
            return TaskText + "|" + IsChecked;
        }

        private void CheckBoxClicked(object sender, RoutedEventArgs e)
		{
			IsChecked = !IsChecked;
            UpdateCheckBox();
        }

        private void UpdateCheckBox()
        {
            if (IsChecked) DotColor = BackgroundColor;
            else DotColor = Brushes.Transparent;
        }

        public static DependencyProperty DotColorProperty = DependencyProperty.Register("DotColor", typeof(Brush), typeof(Task));
        public static DependencyProperty TaskTextProperty = DependencyProperty.Register("TaskText", typeof(string), typeof(Task));

        public Brush DotColor
		{
			get { return (Brush)GetValue(DotColorProperty); }
			set { SetValue(DotColorProperty, value); }
        }

        public string TaskText
        {
            get { return (string)GetValue(TaskTextProperty); }
            set { SetValue(TaskTextProperty, value); }
        }

        private void DeleteClicked(object sender, RoutedEventArgs e)
        {
            if (Parent is StackPanel parentStackPanel)
            {
                parentStackPanel.Children.Remove(this);
            }
        }
    }
}
