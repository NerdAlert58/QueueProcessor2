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
using System.Windows.Shapes;

namespace QueueProcessor2.Services
{
    /// <summary>
    /// Interaction logic for finished.xaml
    /// </summary>
    public partial class finished : Window
    {
        public finished()
        {
            InitializeComponent();
        }

        public finished(float tt, float wt, float cpu, string waitTimes, string turnaroundTimes)
        {
            InitializeComponent();
            turnAroundTime.Content = tt.ToString();
            waitTimeLabel.Content = wt.ToString();
            cpuLabel.Content = cpu.ToString();
            processWaitTime.Text = waitTimes;
            processTurnaround.Text = turnaroundTimes;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
