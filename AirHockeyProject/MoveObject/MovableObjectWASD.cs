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
    public class MovableObjectWASD : IMoving
    {
        public ObjPosition ObjPosition { get; set; }
        public double PoseOffset { get; } = 1.5;

        public MovableObjectWASD(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop)
        {
            ObjPosition = new ObjPosition(movingObject, setLeft, setTop);
        }

        public void Move(Key key)
        {
            double x = ObjPosition.CurrentPose.X;
            double y = ObjPosition.CurrentPose.Y;

            if (key == Key.W)
                y -= PoseOffset;
            else if (key == Key.S)
                y += PoseOffset;
            else if (key == Key.A)
                x -= PoseOffset;
            else if (key == Key.D)
                x += PoseOffset;

            ObjPosition.CurrentPose = new Point(x, y);
        }
    }
}
