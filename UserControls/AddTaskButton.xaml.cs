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
    public partial class AddTaskButton : UserControl
    {
        public AddTaskButton()
        {
            InitializeComponent();
        }

        private void AddTask(object sender, RoutedEventArgs e)
        {
            if (Parent is StackPanel stack)
            {
                stack.Children.Add(new Task());
            }
        }

        private void DeleteClicked(object sender, RoutedEventArgs e)
        {
            DependencyObject parent = Parent;

            while (parent is not null && parent is not TaskList)
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent is null) throw new Exception("Parent is null");

            TaskList? taskList = parent as TaskList;
            if (taskList?.Parent is StackPanel stack) { stack.Children.Remove(taskList); }
            else { throw new Exception("TaskList's parent is not a StackPanel or TaskList is null."); }
        }
    }
}
