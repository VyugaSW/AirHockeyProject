using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirHockeyProject
{

    public class Puck
    {
        public ObjPosition ObjPosition { get; set; }

        private readonly DispatcherTimer _collisionTimer;
        private readonly FieldObject[] _objectsOnField;
        private MovingLine _movingLine = null;

        private const double MaxBorder = 500;

        public Puck(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop, FieldObject[] objects)
        {
            ObjPosition = new ObjPosition(movingObject, setLeft, setTop);
            _collisionTimer = new DispatcherTimer();
            _objectsOnField = objects;

            DispatcherTimerInit();
        }

        private void DispatcherTimerInit()
        {
            _collisionTimer.Interval = TimeSpan.FromMilliseconds(10.0);
            _collisionTimer.Tick += CheckCollision;
            _collisionTimer.Start();
        }

        private void CheckCollision(object sender, EventArgs e)
        {
            CheckCollisionObjects();
            CheckCollisionBorders();
        }
        private void CheckCollisionObjects()
        {
            foreach (FieldObject obj in _objectsOnField)
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
        private void CheckCollisionBorders()
        {
            _collisionTimer.Stop();

            MovingLine tempLine = null;

            if (ObjPosition.CurrentPose.X + 10 > MaxBorder)
                tempLine = new MovingLine(this, "X");
            else if (ObjPosition.CurrentPose.X  < 0)
                tempLine = new MovingLine(this, "X");

            if (ObjPosition.CurrentPose.Y + 10 > MaxBorder)
                tempLine = new MovingLine(this, "Y");
            else if (ObjPosition.CurrentPose.Y  < 0)
                tempLine = new MovingLine(this, "Y");

            CreateLineMoving(tempLine);

            _collisionTimer.Start();
        }
        private bool IsCollision(FieldObject objCollision)
        {
            double lenBetweenCenters = (objCollision.ObjPosition.CurrentPose - this.ObjPosition.CurrentPose).Length;
            double radiusObj = (objCollision.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;
            double radiusPuck = (this.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;

            if ((int)lenBetweenCenters == radiusPuck + radiusObj)
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


    }
}


