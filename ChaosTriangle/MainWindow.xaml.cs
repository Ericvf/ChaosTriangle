using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChaosTriangle
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// https://www.youtube.com/watch?v=kbKtFN71Lfs
        /// </summary>
        IEnumerable<Point> triangles;
        Point p;

        public MainWindow()
        {
            InitializeComponent();

            var center = 600;
            triangles = BuildTriangle(3, 500, center, 0);
            var r = new Random((int)(DateTime.Now.Ticks % int.MaxValue));
            p = new Point(r.Next(300), r.Next(300));

            this.Loaded += MainWindow_Loaded;

            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromTicks(1);
            timer.Tick += (s, e) =>
            {
                var x = r.Next(triangles.Count());
                var tt = triangles.ElementAt(x);
                p = new Point(center + (tt.X - p.X) / 2, center + (tt.Y - p.Y) / 2);
                var shape = new Ellipse()
                {
                    Width = 5,
                    Height = 5,
                    Fill = Brushes.Black,
                };

                shape.SetValue(Canvas.LeftProperty, p.X );
                shape.SetValue(Canvas.TopProperty,  p.Y );
                this.Canvas.Children.Add(shape);
            };

            timer.Start();
        }

        IEnumerable<Point> BuildTriangle(int c, float Radius, float center, float Angle)
        {
            for (int i = 0; i < c; i++)
                yield return new Point()
                {
                    X = Radius * (float)Math.Cos(Angle + i * 2 * Math.PI / c) + center,
                    Y = Radius * (float)Math.Sin(Angle + i * 2 * Math.PI / c) + center
                };
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var triangle in triangles)
            {
                var shape = new Ellipse()
                {
                    Width = 10,
                    Height = 10,
                    Fill = Brushes.Black,
                };

                shape.SetValue(Canvas.LeftProperty, triangle.X);
                shape.SetValue(Canvas.TopProperty, triangle.Y);

                this.Canvas.Children.Add(shape);
            }
        }
    }
}
