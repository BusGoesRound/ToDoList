using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ToDoList.UserControls
{
    public partial class DOTW : UserControl
    {

        private Task task;

        public DOTW()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool[] days = new bool[7];

            for (int i = 0; i < 7; i++)
            {
                if (ButtonStack.Children[i] is ToggleButton toggleButton)
                {
                    days[i] = toggleButton.IsChecked ?? false;
                }
            }

            task.SetDays(days);

            DependencyObject parent = VisualTreeHelper.GetParent(this);

            while (parent is not Popup)
            {
                if (VisualTreeHelper.GetParent(parent) == null)
                {
                    parent = LogicalTreeHelper.GetParent(parent);
                }
                else
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }

            if (parent is Popup popup)
            {
                popup.IsOpen = false;
            }
        }

        public void SetTask(Task task)
        {
            this.task = task;

            bool[] days = task.GetDays();
            for (int i = 0; i < 7; i++)
            {
                if (ButtonStack.Children[i] is ToggleButton toggleButton)
                {
                    toggleButton.IsChecked = days[i];
                }
            }
        }
    }
}
