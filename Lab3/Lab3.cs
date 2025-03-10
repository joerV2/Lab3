using System;
using ScottPlot;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScottPlot.Statistics;
using System.Windows.Forms.DataVisualization.Charting;

namespace Lab3
{
    public partial class Lab3 : Form
    {
        public Lab3()
        {
            InitializeComponent();
            ComparePerformance();
        }

        private void ComparePerformance()
        {
            int[] sizes = { 1000, 5000, 10000, 50000, 100000 };
            List<double> stackTimes = new List<double>();
            List<double> queueTimes = new List<double>();
            List<double> myArrayStackTimes = new List<double>();
            List<double> myLinkedListStackTimes = new List<double>();

            for (int i = 0; i < sizes.Length; i++)
            {
                int size = sizes[i];
                stackTimes.Add(TestStackPush(size));
                queueTimes.Add(TestQueueEnqueue(size));
                myArrayStackTimes.Add(TestMyArrayStackPush(size));
                myLinkedListStackTimes.Add(TestMyLinkedListStackPush(size));
            }

            PlotResults(sizes, stackTimes, queueTimes, myArrayStackTimes, myLinkedListStackTimes);
        }

        private double TestStackPush(int size)
        {
            Stack<int> stack = new Stack<int>();
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < size; i++) stack.Push(i);
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private double TestQueueEnqueue(int size)
        {
            Queue<int> queue = new Queue<int>();
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < size; i++) queue.Enqueue(i);
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private double TestMyArrayStackPush(int size)
        {
            MyStack<int> myStack = new MyStack<int>(true, size);
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < size; i++) myStack.Push(i);
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private double TestMyLinkedListStackPush(int size)
        {
            MyStack<int> myStack = new MyStack<int>(false);
            Stopwatch stopwatch = Stopwatch.StartNew();
            for (int i = 0; i < size; i++) myStack.Push(i);
            stopwatch.Stop();
            return stopwatch.Elapsed.TotalMilliseconds;
        }

        private void PlotResults(int[] sizes, List<double> stackTimes, List<double> queueTimes, List<double> myArrayStackTimes, List<double> myLinkedListStackTimes)
        {
            chart1.Series.Clear();

            AddSeriesToChart("Stack<T>", sizes, stackTimes, System.Drawing.Color.Blue);
            AddSeriesToChart("Queue<T>", sizes, queueTimes, System.Drawing.Color.Red);
            AddSeriesToChart("MyStack (Array)", sizes, myArrayStackTimes, System.Drawing.Color.Green);
            AddSeriesToChart("MyStack (LinkedList)", sizes, myLinkedListStackTimes, System.Drawing.Color.Orange);
        }

        private void AddSeriesToChart(string seriesName, int[] sizes, List<double> times, System.Drawing.Color color)
        {
            var series = new System.Windows.Forms.DataVisualization.Charting.Series(seriesName)
            {
                ChartType = SeriesChartType.Line,
                Color = color
            };

            for (int i = 0; i < sizes.Length; i++)
            {
                series.Points.AddXY(sizes[i], times[i]);
            }

            chart1.Series.Add(series);
        }
    }
}
