using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace ToDoList.UserControls
{
    /// <summary>
    /// Interaction logic for TaskList.xaml
    /// </summary>
    public partial class TaskList : UserControl
    {
        public TaskList()
        {
            InitializeComponent();
        }

        public TaskList(List<string> taskList)
        {
            InitializeComponent();

            foreach (string task in taskList)
            {
                Task newTask = new(task);
                Add(newTask);
            }
        }

        public void ResetTasks()
        {
            foreach (var child in List.Children)
            {
                if (child is Task task)
                {
                    task.ResetTask();
                }
            }
        }

        public List<string> ToList()
        {
            List<string> tasks = [];

            foreach (var child in List.Children)
            {
                if (child is Task task)
                {
                    tasks.Add(task.ToString());
                }
            }

            return tasks;
        }

        public void Add(Task task)
        {
            List.Children.Add(task);
        }
    }
}
