using QueueProcessor2.Objects;
using QueueProcessor2.Services;
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

namespace QueueProcessor2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var processes = new List<Proc>()
            {
                new Proc() { Name = "P1", Color = Objects.Color.white, Priority = 1, Burst = 12, Arrival = 0, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = 2, Burst = 8, Arrival = 4, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = 1, Burst = 6, Arrival = 5, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P4", Color = Objects.Color.green, Priority = 2, Burst = 5, Arrival = 12, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P5", Color = Objects.Color.red, Priority = 2, Burst = 10, Arrival = 18, TurnAroundTime = -1, WaitTime = -1}
            };

            var handler = new Handler(processes);

            var (events, results) = handler.DoWork();
            Console.WriteLine("Pause here to check results.");
        }
    }
}
