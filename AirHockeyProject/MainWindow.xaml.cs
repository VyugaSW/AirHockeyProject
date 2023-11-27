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
using System.Windows.Threading;

namespace AirHockeyProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public FieldObject Hockey { get; set; }
        public Puck Puck { get; set; }
        DispatcherTimer _timer;
        public MainWindow()
        {
            InitializeComponent();
            Hockey = new FieldObject(ellipse, Canvas.SetLeft, Canvas.SetTop);
            Puck = new Puck(ellipsePuck, Canvas.SetLeft, Canvas.SetTop, new FieldObject[] { Hockey });
            Hockey.ObjPosition.Pose = new Point(10, 50);
            Puck.ObjPosition.Pose = new Point(20, 20);

            PreviewKeyDown += Window_PreviewKeyDown;
        }

        public void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            double x = Hockey.ObjPosition.Pose.X;
            double y = Hockey.ObjPosition.Pose.Y;
            switch (e.Key)
            {
                case Key.Up:
                    y -= 1.5;
                    break;
                case Key.Down:
                    y += 1.5;
                    break;
                case Key.Left:
                    x -= 1.5;
                    break;
                case Key.Right:
                    x += 1.5;
                    break;
                case Key.D: // TESTS
                    Puck.CalculateLineMoving(Puck.ObjPosition.Pose, Hockey.ObjPosition.Pose);
                    break;
            }

            Hockey.ObjPosition.Pose = new Point(x,y);
        }
    }
}
