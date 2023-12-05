using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirHockeyProject
{

 

    public class CollisionObject : ICollisable
    {
        public ObjPosition ObjPosition { get; set; }
        public IMovable[] ObjectsOnField { get; }

        public double MaxYBorder { get; } = 300;
        public double MinYBorder { get; } = 0;
        public double MaxXBorder { get; } = 500;
        public double MinXBorder { get; } = 0;

        private MovingLine _movingLine = null;
        private DispatcherTimer _collisionTimer;

        public CollisionObject(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop, IMovable[] objects)
        {
            ObjPosition = new ObjPosition(movingObject, setLeft, setTop);
            _collisionTimer = new DispatcherTimer();
            ObjectsOnField = objects;

            DispatcherTimerInit();
        }

        private void DispatcherTimerInit()
        {
            _collisionTimer.Interval = TimeSpan.FromMilliseconds(1.0);
            _collisionTimer.Tick += CheckCollision;
            _collisionTimer.Start();
        }
        public void CheckCollision(object sender, EventArgs e)
        {
            CheckCollisionObjects();
            CheckCollisionBorders();
        }
        private void CheckCollisionObjects()
        {
            if (ObjectsOnField != null)
            {
                foreach (IMovable obj in ObjectsOnField)
                {
                    if (IsCollision(obj))
                    {
                        _collisionTimer.Stop();
                        CreateLineMoving(new MovingLine(this, obj.ObjPosition.CurrentPose, this.ObjPosition.CurrentPose));
                        _collisionTimer.Start();

                        break;
                    }
                }
            }
        }
        private void CheckCollisionBorders()
        {
            _collisionTimer.Stop();

            MovingLine tempLine = null;
            double radiusObj = (ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;

            if (ObjPosition.CurrentPose.X + radiusObj > MaxXBorder)
                tempLine = new MovingLine(this, "X");
            else if (ObjPosition.CurrentPose.X  < MinXBorder)
                tempLine = new MovingLine(this, "X");

            if (ObjPosition.CurrentPose.Y + radiusObj > MaxYBorder)
                tempLine = new MovingLine(this, "Y");
            else if (ObjPosition.CurrentPose.Y  < MinYBorder)
                tempLine = new MovingLine(this, "Y");

            CreateLineMoving(tempLine);

            _collisionTimer.Start();
        }
        private bool IsCollision(IMovable objCollision)
        {
            double lenBetweenCenters = (objCollision.ObjPosition.CurrentPose - this.ObjPosition.CurrentPose).Length;
            double radiusObj = (objCollision.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;
            double radiusPuck = (this.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;

            if ((int)lenBetweenCenters <= radiusPuck + radiusObj)
                return true;

            return false;
        }
        private void CreateLineMoving(MovingLine objLine)
        {
            if (objLine != null)
            {
                _movingLine?.MovingTimer.Stop();
                _movingLine = objLine;
                _movingLine.MovingTimer.Start();
            }
        }
        public void ClearMoving()
        {
            _movingLine?.MovingTimer.Stop();
            _movingLine = null;
        }
        public void CreateMoving(Point pointOne, Point pointTwo)
        {
            _movingLine?.MovingTimer.Stop();
            CreateLineMoving(new MovingLine(this, pointOne, pointTwo));
        }
    }
}


