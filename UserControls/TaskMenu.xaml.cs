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
    public partial class TaskMenu : UserControl
    {
        private Task task;

        public TaskMenu()
        {
            InitializeComponent();
        }

        private void OpenDaysWindow(object sender, RoutedEventArgs e)
        {
            task.RepeatDaily = false;
            DOTWPOP.IsOpen = !DOTWPOP.IsOpen;
        }

        private void SetDaily(object sender, RoutedEventArgs e)
        {
            task.RepeatDaily = true;
        }

        public void SetTask(Task task)
        {
            this.task = task;

            if (task.RepeatDaily)
            {
                RepeatDaily.IsChecked = true;
            }
            else
            {
                RepeatSpecific.IsChecked = true;
            }
        }

        private void DOTWPOP_Opened(object sender, EventArgs e)
        {
            DOTWControl.SetTask(task);
        }
    }
}
