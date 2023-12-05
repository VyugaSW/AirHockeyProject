using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirHockeyProject
{
    public class MovingLine
    {
        public DispatcherTimer MovingTimer { get; }
        public IMovable Puck { get; }
        public double TangnentInclination { get; private set; }
        public double YaxisOffset { get; private set; }


        private double CoordinateOffset = 0.7;

        public MovingLine(IMovable puck, double tangentInclination, double YaxisOffset, string direction)
        {
            Puck = puck;
            TangnentInclination = tangentInclination;
            this.YaxisOffset = YaxisOffset;
            MovingTimer = new DispatcherTimer();

            DisptacherTimerInit(direction);
        }
        public MovingLine(IMovable puck, Point pointOne, Point pointTwo)
        {
            Puck = puck;
            MovingTimer = new DispatcherTimer();

            DisptacherTimerInit(CalculateLineMoving(pointOne,pointTwo));
        }
        public MovingLine(IMovable puck, string borderAxis)
        {
            Puck = puck;
            MovingTimer = new DispatcherTimer();
            string mode = CalculateLineMoving(puck.ObjPosition, borderAxis);
            DisptacherTimerInit(mode);
        }


        private void DisptacherTimerInit(string direction)
        {
            MovingTimer.Interval = TimeSpan.FromMilliseconds(5.0);

            if (direction == "Forward")
                MovingTimer.Tick += MoveForward;
            else if(direction == "Back")
                MovingTimer.Tick += MoveBack;
        }

        public void MoveForward(object sender, EventArgs e)
        {
            double newX = Puck.ObjPosition.CurrentPose.X + CoordinateOffset;
            double newY = newX * TangnentInclination + YaxisOffset;

            Puck.ObjPosition.PastPose = Puck.ObjPosition.CurrentPose;
            Puck.ObjPosition.CurrentPose = new Point(newX, newY);
        }
        public void MoveBack(object sender, EventArgs e)
        {
            double newX = Puck.ObjPosition.CurrentPose.X - CoordinateOffset;
            double newY = newX * TangnentInclination + YaxisOffset;

            Puck.ObjPosition.PastPose = Puck.ObjPosition.CurrentPose;
            Puck.ObjPosition.CurrentPose = new Point(newX, newY);
        }

        private double CalculateTangentInclination(Point pointOne, Point pointTwo)
            => (pointOne.Y - pointTwo.Y) / (pointOne.X - pointTwo.X);
        private double CalculateYaxisOffset(Point point, double tangentInclination)
            => point.Y - tangentInclination * point.X;

        private string CalculateLineMoving(Point pointOne, Point pointTwo)
        {
            TangnentInclination = CalculateTangentInclination(pointOne, pointTwo);
            YaxisOffset = CalculateYaxisOffset(pointOne, TangnentInclination);
            string mode;

            if (pointOne.X < pointTwo.X)
                mode = "Forward";
            else
                mode = "Back";

            return mode;
        }
        private string CalculateLineMoving(ObjPosition objPosition, string borderAxis)
        {
            if (!Point.Equals(objPosition.CurrentPose, objPosition.PastPose))
            {
                TangnentInclination = -CalculateTangentInclination(objPosition.PastPose, objPosition.CurrentPose);
                YaxisOffset = CalculateYaxisOffset(objPosition.CurrentPose, TangnentInclination);
                string mode = string.Empty;

                if (borderAxis == "Y")
                {
                    if (objPosition.CurrentPose.X < objPosition.PastPose.X)
                    {
                        mode = "Back";
                    }
                    else
                    {
                        mode = "Forward";
                    }
                }
                else if (borderAxis == "X")
                {
                    if (objPosition.CurrentPose.X > objPosition.PastPose.X)
                    {
                        mode = "Back";
                    }
                    else
                    {
                        mode = "Forward";
                    }
                }
                return mode;
            }
            return string.Empty;
        }
    }
}
