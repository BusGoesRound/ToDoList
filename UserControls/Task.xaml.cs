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

        // Constructors ______________________________________________________________________________________________________________

        public Task()
		{
            InitializeComponent();
            DataContext = this;

            TaskText = "Task";
        }

        public Task(string task)
        {
            InitializeComponent();
            DataContext = this;

            string[] taskString = task.Split('|');

            IsChecked = bool.Parse(taskString[1]);
            if(taskString.Length == 3){ RepeatDaily = bool.Parse(taskString[2]); UpdateRepeatDaily(); }
            UpdateCheckBox();
            TaskText = taskString[0];
        }

        public Task(string taskText, bool isChecked)
        {
            InitializeComponent();
            DataContext = this;

            IsChecked = isChecked;
            UpdateCheckBox();
            TaskText = taskText;
        }

        // ToString ______________________________________________________________________________________________________________

        override public string ToString()
        {
            return TaskText + "|" + IsChecked + "|" + RepeatDaily;
        }

        //Checkbox ______________________________________________________________________________________________________________

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

        //Repeat ______________________________________________________________________________________________________________

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

        public void ResetTask()
        {
            if (RepeatDaily)
            {
                IsChecked = false;
                UpdateCheckBox();
            }
        }

        //Delete ______________________________________________________________________________________________________________

        private void DeleteClicked(object sender, RoutedEventArgs e)
        {
            if (Parent is StackPanel parentStackPanel)
            {
                parentStackPanel.Children.Remove(this);
            }
        }

        // Dependency Properties ______________________________________________________________________________________________________________

        private static readonly DependencyProperty DotColorProperty = DependencyProperty.Register("DotColor", typeof(Brush), typeof(Task));
        private static readonly DependencyProperty DotColor2Property = DependencyProperty.Register("DotColor2", typeof(Brush), typeof(Task));
        private static readonly DependencyProperty TaskTextProperty = DependencyProperty.Register("TaskText", typeof(string), typeof(Task));

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
    }
}
