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
    public partial class GameWindow : Window
    {
        public MovableObjectArrows HockeyStick1 { get; }
        public MovableObjectWASD HockeyStick2 { get; }
        public CollisionObject Puck { get; }
        public Score ScoreHockey1 { get; set; }
        public Score ScoreHockey2 { get; set; }

        private RectPenetration _gatesPenetraion;
        private DispatcherTimer _winCheck;
        public GameWindow()
        {
            InitializeComponent();
            _gatesPenetraion = new RectPenetration(new RectangleGeometry[] { Gates1, Gates2 });
            HockeyStick1 = new MovableObjectArrows(ellipse1, Canvas.SetLeft, Canvas.SetTop);
            HockeyStick2 = new MovableObjectWASD(ellipse2, Canvas.SetLeft, Canvas.SetTop);
            ScoreHockey1 = new Score();
            ScoreHockey2 = new Score();
            Puck = new CollisionObject(ellipsePuck, Canvas.SetLeft, Canvas.SetTop, new IMovable[] { HockeyStick1, HockeyStick2 });
            SetPositions();
            DispatcherTimerInit();

            TextBlockScore1.DataContext = ScoreHockey1;
            TextBlockScore2.DataContext = ScoreHockey2;

            KeyDown += Window_KeyDown;
            PreviewKeyDown += Window_PreviewKeyDown;
        }

        private void DispatcherTimerInit()
        {
            _winCheck = new DispatcherTimer();
            _winCheck.Interval = TimeSpan.FromMilliseconds(2.0);
            _winCheck.Tick += WinCheck;
            _winCheck.Start();
        }
        private void WinCheck(object sender, EventArgs e)
        {
            RectangleGeometry gates;
            if ((gates = _gatesPenetraion.PenetrationCheck(Puck)) != null)
            {
                if (gates == Gates2)
                    ScoreHockey1.AddScore(1);
                else
                    ScoreHockey2.AddScore(1);

                SetPositions();
                Puck.ClearMoving();
            }
        }
        private void SetPositions()
        {
            HockeyStick1.ObjPosition.CurrentPose = new Point(113, 143);
            HockeyStick2.ObjPosition.CurrentPose = new Point(373, 143);
            Puck.ObjPosition.CurrentPose = new Point(243, 143);
        }
        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            double width = (HockeyStick1.ObjPosition.MovingObject as FrameworkElement).Width / 2;

            if (e.Key == Key.Down && HockeyStick1.ObjPosition.CurrentPose.Y + width < Puck.MaxYBorder)
                HockeyStick1.Move(e.Key);
            else if (e.Key == Key.Up && HockeyStick1.ObjPosition.CurrentPose.Y > Puck.MinYBorder)
                HockeyStick1.Move(e.Key);
            else if (e.Key == Key.Left && HockeyStick1.ObjPosition.CurrentPose.X > Puck.MinXBorder)
                HockeyStick1.Move(e.Key);
            else if (e.Key == Key.Right && HockeyStick1.ObjPosition.CurrentPose.X < Puck.MaxXBorder / 2 - width - 7)
                HockeyStick1.Move(e.Key);

        }
        public void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            double width = (HockeyStick2.ObjPosition.MovingObject as FrameworkElement).Width / 2;

            if (e.Key == Key.S && HockeyStick2.ObjPosition.CurrentPose.Y + width < Puck.MaxYBorder)
                HockeyStick2.Move(e.Key);
            else if (e.Key == Key.W && HockeyStick2.ObjPosition.CurrentPose.Y > Puck.MinYBorder)
                HockeyStick2.Move(e.Key);
            else if (e.Key == Key.A && HockeyStick2.ObjPosition.CurrentPose.X > Puck.MaxXBorder / 2 + width / 2)
                HockeyStick2.Move(e.Key);
            else if (e.Key == Key.D && HockeyStick2.ObjPosition.CurrentPose.X < Puck.MaxXBorder - width / 2)
                HockeyStick2.Move(e.Key);

        }
    }
}
