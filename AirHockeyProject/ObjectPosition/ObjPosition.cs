using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirHockeyProject
{


    public class ObjPosition
    {
        private Point _currentPose;
        private Point _pastPose;
        public Point CurrentPose 
        { 
            get => _currentPose;
            set
            {
                PastPose = _currentPose;
                _currentPose = value;
            }
        }
        public Point PastPose
        {
            get => _pastPose;
            set => _pastPose = value;
        }

        public UIElement MovingObject { get; private set; }

        private readonly DispatcherTimer _positionTimer;
   
        private event Action<UIElement, double> EventSetLeft;
        private event Action<UIElement, double> EventSetTop;

        public ObjPosition(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop)
        {
            _positionTimer = new DispatcherTimer();
            CurrentPose = new Point(0,0);
            PastPose = new Point(0,0);

            EventSetLeft = setLeft;
            EventSetTop = setTop;
            MovingObject = movingObject;

            DispatcherTimerInit();
        }


        private void DispatcherTimerInit()
        {
            _positionTimer.Interval = TimeSpan.FromMilliseconds(10.0);
            _positionTimer.Tick += UpdatePosition;
            _positionTimer.Start();
        }
        private void UpdatePosition(object sender, EventArgs e)
        {
            if (MovingObject != null && EventSetTop != null && EventSetLeft != null)
            {
                EventSetLeft(MovingObject, CurrentPose.X);
                EventSetTop(MovingObject, CurrentPose.Y);
            }
        }
    }
}
