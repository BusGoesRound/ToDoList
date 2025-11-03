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
		public bool RepeatDaily = true;
        private readonly bool[] days = new bool[7];

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

			TaskText = taskString[0];

            IsChecked = bool.Parse(taskString[1]);
			UpdateCheckBox();

			RepeatDaily = bool.Parse(taskString[2]);

            string[] daysString = taskString[3].Split(',');
			for (int i = 0; i < daysString.Length; i++)
			{
				days[i] = bool.Parse(daysString[i]);
			}
        }

		public Task(string taskText, bool isChecked)
		{
			InitializeComponent();
			DataContext = this;

            TaskText = taskText;

            IsChecked = isChecked;
			UpdateCheckBox();
		}

		// ToString ______________________________________________________________________________________________________________

		override public string ToString()
		{
			string text = TaskText + "|" + IsChecked + "|" + RepeatDaily + "|";
			text += string.Join(",", days);
			return text;
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

		public void ResetTask()
		{
			if (RepeatDaily)
			{
   				IsChecked = false;
				UpdateCheckBox();
            }
			else if (days[(int)DateTime.Now.DayOfWeek])
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

		private static readonly DependencyProperty DotColorProperty
			= DependencyProperty.Register("DotColor", typeof(Brush), typeof(Task));
		private static readonly DependencyProperty TaskTextProperty
			= DependencyProperty.Register("TaskText", typeof(string), typeof(Task));

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

        private void MenuClicked(object sender, RoutedEventArgs e)
        {
			OverlayMenu.IsOpen = !OverlayMenu.IsOpen;
        }

		public void SetDays(bool[] days)
		{
			days.CopyTo(this.days, 0);
        }

        private void OverlayMenu_Opened(object sender, EventArgs e)
        {
			DotsMenu.SetTask(this);
        }

		public bool[] GetDays()
        {
			return days;
        }

        //private void MoveTask(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        //{
        //	return;
        //}
    }
}
