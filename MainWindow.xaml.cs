using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;
using System.Windows.Threading;
using ToDoList.UserControls;
using Task = ToDoList.UserControls.Task;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        private DateTime _lastResetDate = DateTime.Today;

        const double DEFAULT_WIDTH = 400;
        const double DEFAULT_HEIGHT = 400;
        const double DEFAULT_X = 633;
        const double DEFAULT_Y = 1520;

        const double CORNER_X = 1033;
        const double CORNER_Y = 1920;

        public MainWindow()
        {
            InitializeComponent();
            Load();

            DispatcherTimer timer = new()
            {
                Interval = TimeSpan.FromMinutes(1)
            };
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        // Daily Reset ______________________________________________________________________________________________________________

        private void Timer_Tick(object? sender, EventArgs e)
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
                if(child is TaskList taskList)
                {
                    taskList.ResetTasks();
                }
            }
        }

        // Window Controls ______________________________________________________________________________________________________________

        private void CloseWindow(object sender, RoutedEventArgs e)  
        {
            Save();
            Close();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        // Save and Load ______________________________________________________________________________________________________________

        private readonly JsonSerializerOptions jsonOptions = new() { WriteIndented = true };

        private void Save()
        {
            List<List<string>> tasks = [];

            tasks.Add(["Window Size|" + Width + "|" + Height]);

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

            string[] windowSize = tasks[0][0].Split('|');
            if (windowSize[0] == "Window Size")
            {
                Width = double.Parse(windowSize[1]);
                Height = double.Parse(windowSize[2]);
                Top = CORNER_X - Height;
                Left = CORNER_Y - Width;
                tasks.RemoveAt(0);
            }

            foreach (List<string> taskList in tasks)
            {
                TaskList newList = new(taskList);
                TaskListPanel.Children.Add(newList);
            }
        }

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

            if (newHeight > MinHeight)
            {
                Height = newHeight;
                Top = newTop;
            }
        }

        private void AddList(object sender, RoutedEventArgs e)
        {
            TaskListPanel.Children.Add(new TaskList());
        }
    }
}