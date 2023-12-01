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
        public Puck Puck { get; }
        public double TangnentInclination { get; }
        public double YaxisOffset { get; }


        private double CoordinateOffset = 0.7;

        public MovingLine(Puck puck, double tangentInclination, double YaxisOffset, string direction)
        {
            Puck = puck;
            TangnentInclination = tangentInclination;
            this.YaxisOffset = YaxisOffset;

            MovingTimer = new DispatcherTimer();
            MovingTimer.Interval = TimeSpan.FromMilliseconds(2.0);

            if (direction == "Forward")
                MovingTimer.Tick += MoveForward;
            else
                MovingTimer.Tick += MoveBack;
        }

        public void MoveForward(object sender, EventArgs e)
        {
            double newX = Puck.ObjPosition.CurrentPose.X + CoordinateOffset;
            double newY = newX * TangnentInclination + YaxisOffset;

            Puck.ObjPosition.CurrentPose = new Point(newX, newY);
        }
        public void MoveBack(object sender, EventArgs e)
        {
            double newX = Puck.ObjPosition.CurrentPose.X - CoordinateOffset;
            double newY = newX * TangnentInclination + YaxisOffset;

            Puck.ObjPosition.CurrentPose = new Point(newX, newY);
        }

        static private double CalculateTangentInclination(Point pointOne, Point pointTwo)
            => (pointOne.Y - pointTwo.Y) / (pointOne.X - pointTwo.X);
        static private double CalculateYaxisOffset(Point point, double tangentInclination)
            => point.Y - tangentInclination * point.X;

        static public MovingLine CalculateLineMoving(Puck puck, Point pointOne, Point pointTwo)
        {
            double tangentInclination = CalculateTangentInclination(pointOne, pointTwo);
            double YaxisOffset = CalculateYaxisOffset(pointOne, tangentInclination);
            string mode;

            if (pointOne.X < pointTwo.X)
                mode = "Forward";
            else
                mode = "Back";
            return new MovingLine(puck, tangentInclination, YaxisOffset, mode);
        }
        static public MovingLine CalculateLineMoving(Puck puck, ObjPosition objPosition, string borderAxis)
        {
            if (!Point.Equals(objPosition.CurrentPose, objPosition.PastPose))
            {
                double tangentInclination = -CalculateTangentInclination(objPosition.PastPose, objPosition.CurrentPose);
                double YaxisOffset = CalculateYaxisOffset(objPosition.CurrentPose, tangentInclination);
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
                return new MovingLine(puck, tangentInclination, YaxisOffset, mode);
            }
            return null;
        }
    }
}
