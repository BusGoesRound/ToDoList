using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using ToDoList.Classes;
using ToDoList.UserControls;
using Task = ToDoList.UserControls.Task;

namespace ToDoList
{
    public partial class MainWindow : Window
    {
        private DateTime _lastResetDate = DateTime.Today;

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

        private void Timer_Tick(object sender, EventArgs e)
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

private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Save();
            Close();
        }

        private void MinimizeWindow(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private readonly JsonSerializerOptions jsonOptions = new() { WriteIndented = true };

        private void Save()
        {
            List<List<string>> tasks = [];

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

            foreach (List<string> taskList in tasks)
            {
                TaskList newList = new(taskList);
                TaskListPanel.Children.Insert(TaskListPanel.Children.Count - 1, newList);
            }
        }
    }
}