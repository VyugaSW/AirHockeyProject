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

        private Puck _puck;
        private double _tangentInclination;
        private double _YaxisOffset;

        public MovingLine(Puck puck, double tangentInclination, double YaxisOffset, string direction)
        {
            _puck = puck;
            _tangentInclination = tangentInclination;
            _YaxisOffset = YaxisOffset;

            MovingTimer = new DispatcherTimer();
            MovingTimer.Interval = TimeSpan.FromMilliseconds(2.0);

            if(direction == "Forward")
                MovingTimer.Tick += MoveForward;
            else
                MovingTimer.Tick += MoveBack;
        }

        public void MoveForward(object sender, EventArgs e)
        {
            double newX = _puck.ObjPosition.Pose.X + 1;
            double newY = newX * _tangentInclination + _YaxisOffset;

            _puck.ObjPosition.Pose = new Point(newX, newY);
        }
        public void MoveBack(object sender, EventArgs e)
        {
            double newX = _puck.ObjPosition.Pose.X - 1;
            double newY = newX * _tangentInclination + _YaxisOffset;

            _puck.ObjPosition.Pose = new Point(newX, newY);
        }
    }
}
