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
        private MovingLine _movingline = null;

        public Puck(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop, FieldObject[] objects)
        {
            ObjPosition = new ObjPosition(movingObject, setLeft, setTop);
            _collisionTimer = new DispatcherTimer();
            _objectsOnField = objects;

            DispatcherTimerInit();
        }

        private void DispatcherTimerInit()
        {
            _collisionTimer.Interval = TimeSpan.FromMilliseconds(0.01);
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
                    CalculateLineMoving(obj.ObjPosition.Pose, this.ObjPosition.Pose);
                    break;
                }
            }
        }
        private void CheckCollisionBorders()
        {

            if (ObjPosition.Pose.X > 500)
                CalculateLineMoving(ObjPosition.Pose, "Up_X");
            else if (ObjPosition.Pose.X < 0)
                CalculateLineMoving(ObjPosition.Pose, "Down_X");

            if (ObjPosition.Pose.Y > 500)
                CalculateLineMoving(ObjPosition.Pose, "Up_Y");
            else if (ObjPosition.Pose.Y < 0)
                CalculateLineMoving(ObjPosition.Pose, "Down_Y");
        }
        private bool IsCollision(FieldObject objCollision)
        {
            double lenBetweenCenters = (objCollision.ObjPosition.Pose - this.ObjPosition.Pose).Length;
            double radiusObj = (objCollision.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;
            double radiusPuck = (this.ObjPosition.MovingObject as FrameworkElement).ActualWidth / 2;

            if ((int)lenBetweenCenters == radiusPuck + radiusObj)
                return true;

            return false;
        }

        private void CalculateLineMoving(Point point, string border)
        {
            double tangentInclination = 0;
            double YaxisOffset;
            string mode = "Forward";

            switch(border)
            {
                case "Down_X":
                case "Down_Y":
                    tangentInclination = 1;
                    break;
                case "Up_X":
                    tangentInclination = 1;
                    mode = "Back";
                    break;
                case "Up_Y":
                    tangentInclination = -1;
                    break;
            }

            YaxisOffset = point.Y - tangentInclination * point.X;

            CreateLineMoving(tangentInclination, YaxisOffset, mode);
        }
        public void CalculateLineMoving(Point pointOne, Point pointTwo)
        {
            double tangentInclination = (pointOne.Y - pointTwo.Y) / (pointOne.X - pointTwo.X);
            double YaxisOffset = pointOne.Y - tangentInclination * pointOne.X;
            string mode = string.Empty;

            if (pointOne.X < pointTwo.X)
                mode = "Forward";
            else
                mode = "Back";

            CreateLineMoving(tangentInclination, YaxisOffset, mode);
        }
        private void CreateLineMoving(double tangentInclination, double YaxisOffset, string mode)
        {
            _movingline?.MovingTimer.Stop();
            _movingline = new MovingLine(this, tangentInclination, YaxisOffset, mode);
            _movingline.MovingTimer.Start();
        }

    }

}


