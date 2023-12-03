using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AirHockeyProject
{

    public class MovableObjectArrows : IMoving
    {
        public ObjPosition ObjPosition { get; set; }
        public double PoseOffset { get; } = 2.5;

        public MovableObjectArrows(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop)
        {
            ObjPosition = new ObjPosition(movingObject, setLeft, setTop);
        }

        public void Move(Key key)
        {
            double x = ObjPosition.CurrentPose.X;
            double y = ObjPosition.CurrentPose.Y;

            if (key == Key.Up)
                y -= PoseOffset;
            else if (key == Key.Down)
                y += PoseOffset;
            else if (key == Key.Left)
                x -= PoseOffset;
            else if (key == Key.Right)
                x += PoseOffset;

            ObjPosition.CurrentPose = new Point(x, y);
        }
    }
}
