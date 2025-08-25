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
        private readonly Brush BackgroundColor = (SolidColorBrush)Application.Current.Resources["ForegroundColor"];

        public bool IsChecked = false;
        public bool RepeatDaily = false;

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

        public void UpdateCheckBox()
        {
            if (IsChecked) DotColor = BackgroundColor;
            else DotColor = Brushes.Transparent;
        }

        public static DependencyProperty DotColorProperty = DependencyProperty.Register("DotColor", typeof(Brush), typeof(Task));
        public static DependencyProperty DotColor2Property = DependencyProperty.Register("DotColor2", typeof(Brush), typeof(Task));
        public static DependencyProperty TaskTextProperty = DependencyProperty.Register("TaskText", typeof(string), typeof(Task));

        public Brush DotColor
		{
			get { return (Brush)GetValue(DotColorProperty); }
			set { SetValue(DotColorProperty, value); }
        }

        public Brush DotColor2
        {
            get { return (Brush)GetValue(DotColor2Property); }
            set { SetValue(DotColor2Property, value); }
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

        private void RepeatCheckbox(object sender, RoutedEventArgs e)
        {
            RepeatDaily = !RepeatDaily;
            UpdateRepeatDaily();
        }

        private void UpdateRepeatDaily()
        {
            if (RepeatDaily) DotColor2 = BackgroundColor;
            else DotColor2 = Brushes.Transparent;
        }
    }
}
