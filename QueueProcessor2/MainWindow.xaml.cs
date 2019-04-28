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
        IDictionary<int, Event> globalEvents;
        ProcResults globalResults;
        int value = 0;
        finished newWindow;
        public MainWindow()
        {
            InitializeComponent();

          /*  var processes = new List<Proc>()
            {
                new Proc() { Name = "P1", Color = Objects.Color.white, Priority = 1, Burst = 12, Arrival = 0, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = 2, Burst = 8, Arrival = 4, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = 1, Burst = 6, Arrival = 5, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P4", Color = Objects.Color.green, Priority = 2, Burst = 5, Arrival = 12, TurnAroundTime = -1, WaitTime = -1},
                new Proc() { Name = "P5", Color = Objects.Color.red, Priority = 2, Burst = 10, Arrival = 18, TurnAroundTime = -1, WaitTime = -1}
            };

            var handler = new Handler(processes);

            var (events, results) = handler.DoWork();
            Console.WriteLine("Pause here to check results.");*/
        }
        private void buttonCalc(object sender, RoutedEventArgs e)
        {
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Visible;
            button5.Visibility = Visibility.Visible;
            button6.Visibility = Visibility.Visible;

            var procList = new List<Proc>()
               {
                   new Proc() { Name = "P1", Color = Objects.Color.white, Priority = Int32.Parse(priority1.Text), Burst = Int32.Parse(burst1.Text), InitialBurst = Int32.Parse(burst1.Text), Arrival = Int32.Parse(arrival1.Text)},
                   new Proc() { Name = "P2", Color = Objects.Color.blue, Priority = Int32.Parse(priority2.Text), Burst = Int32.Parse(burst2.Text), InitialBurst = Int32.Parse(burst2.Text), Arrival = Int32.Parse(arrival2.Text)},
                   new Proc() { Name = "P3", Color = Objects.Color.purple, Priority = Int32.Parse(priority3.Text), Burst = Int32.Parse(burst3.Text), InitialBurst = Int32.Parse(burst3.Text), Arrival = Int32.Parse(arrival3.Text)},
                   new Proc() { Name = "P4", Color = Objects.Color.green, Priority = Int32.Parse(priority4.Text), Burst = Int32.Parse(burst4.Text), InitialBurst = Int32.Parse(burst4.Text), Arrival = Int32.Parse(arrival4.Text)},
                   new Proc() { Name = "P5", Color = Objects.Color.red, Priority = Int32.Parse(priority5.Text), Burst = Int32.Parse(burst5.Text), InitialBurst = Int32.Parse(burst5.Text), Arrival = Int32.Parse(arrival5.Text)},
               };
            var handler = new Handler(procList);

            var (events, results) = handler.DoWork();
            buttoncalc.Visibility = Visibility.Hidden;
            globalResults = results;
            newWindow = new finished(globalResults.AverageTurnAroundTime, globalResults.AverageWaitTime, globalResults.CPUUtilization, globalResults.WaitTimes, globalResults.TurnAroundTimes);
            globalEvents = events;
            Drawing(globalEvents);
        }
        private void Drawing(IDictionary<int, Event> events)
        {

            if (value < 0)
            {
                value = 0;
            }
            else if (value >= events.Keys.Count - 1)
            {
                value = events.Values.Count - 1;
                ganttChart.Text = events[value].Gantt;
                newWindow.ShowDialog();

                button1.Visibility = Visibility.Hidden;
                button2.Visibility = Visibility.Hidden;
                button3.Visibility = Visibility.Hidden;
                button4.Visibility = Visibility.Hidden;
                button5.Visibility = Visibility.Hidden;
                button6.Visibility = Visibility.Hidden;
            }

            burst1.Text = events[value].Processes[0].Burst.ToString();
            burst2.Text = events[value].Processes[1].Burst.ToString();
            burst3.Text = events[value].Processes[2].Burst.ToString();
            burst4.Text = events[value].Processes[3].Burst.ToString();
            burst5.Text = events[value].Processes[4].Burst.ToString();


            qRemaining.Content = events[value].TimeQuantum.ToString();
            currentProcess.Content = events[value].CurrentProc.Name.ToString();

            timeBox.Content = (value+1).ToString();
            string tmpstring = "";

            if (events[value].Finished != null)
            {
                foreach (var x in events[value].Finished)
                {
                    tmpstring += x.Name.ToString();
                    tmpstring += ", ";
                }
            }
            finishedProcess.Content = tmpstring;
            tmpstring = "";
            if (events[value].Waiting_1 != null)
            {
                foreach (var x in events[value].Waiting_1)
                {
                    tmpstring += x.Name.ToString();
                    tmpstring += ", ";
                }
            }
            waitingProcess1.Content = tmpstring;

            tmpstring = "";
            if (events[value].Waiting_2 != null)
            {
                foreach (var x in events[value].Waiting_2)
                {
                    tmpstring += x.Name.ToString();
                    tmpstring += ", ";
                }
            }
            waitingProcess2.Content = tmpstring;



            ganttChart.Text = events[value].Gantt;
        }
        private void IndexAdjust1(object sender, RoutedEventArgs e)
        {
            value += 1;
            Drawing(globalEvents);
        }
        private void IndexAdjust5(object sender, RoutedEventArgs e)
        {
            value += 5;
            Drawing(globalEvents);
        }
        private void IndexAdjust10(object sender, RoutedEventArgs e)
        {
            value += 10;
            Drawing(globalEvents);
        }
        private void IndexAdjustm1(object sender, RoutedEventArgs e)
        {
            value += -1;
            Drawing(globalEvents);
        }
        private void IndexAdjustm5(object sender, RoutedEventArgs e)
        {
            value += -5;
            Drawing(globalEvents);
        }
        private void IndexAdjustm10(object sender, RoutedEventArgs e)
        {
            value += -10;
            Drawing(globalEvents);
        }
        private void buttonReset_Click(object sender, RoutedEventArgs e)
        {
            value = 0;
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
            button4.Visibility = Visibility.Visible;
            button5.Visibility = Visibility.Visible;
            button6.Visibility = Visibility.Visible;
            newWindow = new finished(globalResults.AverageTurnAroundTime, globalResults.AverageWaitTime, globalResults.CPUUtilization, globalResults.WaitTimes, globalResults.TurnAroundTimes);
            Drawing(globalEvents);
        }
    }
}
