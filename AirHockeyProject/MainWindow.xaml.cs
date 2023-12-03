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
        public MovableObjectArrows Hockey1 { get; set; }
        public MovableObjectWASD Hockey2 { get; set; }
        public CollisionObject Puck { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            Hockey1 = new MovableObjectArrows(ellipse1, Canvas.SetLeft, Canvas.SetTop);
            Hockey2 = new MovableObjectWASD(ellipse2, Canvas.SetLeft, Canvas.SetTop);
            Puck = new CollisionObject(ellipsePuck, Canvas.SetLeft, Canvas.SetTop, new IMovable[] { Hockey1, Hockey2 });

            Hockey1.ObjPosition.CurrentPose = new Point(113, 145);
            Hockey2.ObjPosition.CurrentPose = new Point(373, 145);
            Puck.ObjPosition.CurrentPose = new Point(243, 145);

            KeyDown += Window_KeyDown;
            KeyUp += Window_KeyUp;
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Hockey1.Move(e.Key);
        }

        public void Window_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.Key);
        }
    }
}
