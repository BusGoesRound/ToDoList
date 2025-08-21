using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDoList.Classes;
using ToDoList.UserControls;
using Task = ToDoList.UserControls.Task;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Load();
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

        private void AddTask(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = (DependencyObject)sender;
            while (parent != null && parent is not StackPanel)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent is StackPanel stackPanel)
            {
                UserControls.Task newTask = new();
                stackPanel.Children.Add(newTask);
            }
        }

        JsonSerializerOptions jsonOptions = new() { WriteIndented = true }; 

        private void Save()
        {
            List<string> tasks = [];

            foreach (var child in Tasks.Children)
            {
                if (child is UserControls.Task task)
                {
                    tasks.Add(task.ToString());
                }
            }

            string json = JsonSerializer.Serialize(tasks, jsonOptions);
            File.WriteAllText("tasks.json", json);
        }

        private void Load()
        {
            var tasks = JsonSerializer.Deserialize<List<string>>(File.ReadAllText("tasks.json"), jsonOptions) ?? [];

            foreach (string task in tasks)
            {
                string[] taskString = task.Split('|');
                Task newTask = new(taskString[0], bool.Parse(taskString[1]));
                Tasks.Children.Add(newTask);
            }
        }
    }
}