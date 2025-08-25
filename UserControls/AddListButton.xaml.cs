using System;
using System.Collections.Generic;
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
    public partial class AddListButton : UserControl
    {
        public AddListButton()
        {
            InitializeComponent();
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
                TaskList newList = new();
                stackPanel.Children.Insert(stackPanel.Children.Count - 1,newList);
            }
        }
    }
}
