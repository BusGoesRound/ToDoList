using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Threading;
using ToDoList.UserControls;

namespace ToDoList
{
	public partial class MainWindow : Window
	{
		private DateTime _lastResetDate = DateTime.Today;

		const double CORNER_X = 1033;
		const double CORNER_Y = 1920;

		public MainWindow()
		{
			InitializeComponent();
			Load();
			
			//label.Content = $"Screen Width: {SystemParameters.PrimaryScreenWidth}, Screen Height: {SystemParameters.PrimaryScreenHeight}";

            Closing += (s, e) => Save();

			DispatcherTimer timer = new() { Interval = TimeSpan.FromMinutes(1) };
			timer.Tick += Timer_Tick;
			timer.Start();
		}

		// Daily Reset ______________________________________________________________________________________________________________

		private void Timer_Tick(object? sender, EventArgs e)
		{
			CheckReset();
        }

		private void CheckReset()
		{
			if (DateTime.Today > _lastResetDate)
			{
				_lastResetDate = DateTime.Today;
				ResetTasks();
			}
        }

        private void ResetTasks()
		{
			foreach (var child in TaskListPanel.Children)
			{
				if (child is TaskList taskList) { taskList.ResetTasks(); }
			}
		}

		// Window Controls ______________________________________________________________________________________________________________

		private void CloseWindow(object sender, RoutedEventArgs e) => Close();

		private void MinimizeWindow(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;

		// Save and Load ______________________________________________________________________________________________________________

		private readonly JsonSerializerOptions jsonOptions = new() { WriteIndented = true };

		private void Save()
		{
			List<List<string>> tasks = [];
			List<string> globalData = ["Window Size|" + Width + "|" + Height, "Last Reset Date|" + _lastResetDate];
			tasks.Add(globalData);

			foreach (var child in TaskListPanel.Children)
			{
				if (child is TaskList taskList)
				{
					tasks.Add(taskList.ToList());
				}
			}

			string json = JsonSerializer.Serialize(tasks, jsonOptions);
			File.WriteAllText("tasks.json", json);
		}

		private void Load()
		{
			var tasks = JsonSerializer.Deserialize<List<List<string>>>(File.ReadAllText("tasks.json"), jsonOptions) ?? [];

			bool dataHeaderExists = false;

			if (tasks[0].Count > 0)
			{
                string[] windowSize = tasks[0][0].Split('|');

                if (windowSize[0] == "Window Size")
                {
                    Width = double.Parse(windowSize[1]);
                    Height = double.Parse(windowSize[2]);
                    Top = CORNER_X - Height;
                    Left = CORNER_Y - Width;
                    dataHeaderExists = true;

                }
            }

			if(tasks[0].Count > 1)
			{
                string[] lastReset = tasks[0][1].Split('|');

                if (lastReset[0] == "Last Reset Date")
                {
                    _lastResetDate = DateTime.Parse(lastReset[1]);
                    CheckReset();
                    dataHeaderExists = true;
                }
            }

			if(dataHeaderExists) { tasks.RemoveAt(0); }

            foreach (List<string> taskList in tasks)
			{
				TaskList newList = new(taskList);
				TaskListPanel.Children.Add(newList);
			}
		}

        //Screen Resize ______________________________________________________________________________________________________________

        private void ScreenResize(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
		{
			double newWidth = Width - e.HorizontalChange;
			double newHeight = Height - e.VerticalChange;
			double newLeft = Left + e.HorizontalChange;
			double newTop = Top + e.VerticalChange;

			if (newWidth > MinWidth)
			{
				Width = newWidth;
				Left = newLeft;
			}
			else
			{
				Width = MinWidth;
				Left = CORNER_Y - MinWidth;
            }

			if (newHeight > MinHeight)
			{
				Height = newHeight;
				Top = newTop;
			}
			else
			{
				Height = MinHeight;
				Top = CORNER_X - MinHeight;
            }
        }

		private void AddList(object sender, RoutedEventArgs e) => TaskListPanel.Children.Add(new TaskList());
	}
}