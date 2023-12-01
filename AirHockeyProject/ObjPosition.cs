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
        public Point CurrentPose { get; set; }
        public Point PastPose { get; set; }

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
            _positionTimer.Interval = TimeSpan.FromMilliseconds(0.1);
            _positionTimer.Tick += UpdatePosition;
            _positionTimer.Start();
        }
        private void UpdatePosition(object sender, EventArgs e)
        {
            if (MovingObject != null && EventSetTop != null && EventSetLeft != null)
            {
                PastPose = new Point(CurrentPose.X, CurrentPose.Y);
                EventSetLeft(MovingObject, CurrentPose.X);
                EventSetTop(MovingObject, CurrentPose.Y);
            }
        }
    }
}
