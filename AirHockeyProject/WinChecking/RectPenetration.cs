using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace AirHockeyProject
{
    public class RectPenetration
    {
        RectangleGeometry[] Rectangles { get; }

        public RectPenetration(RectangleGeometry[] Rectangles)
        {
            this.Rectangles = Rectangles;
        }

        public RectangleGeometry PenetrationCheck(IMovable obj)
        {
            foreach(RectangleGeometry rect in Rectangles)
            {
                if(IsInside(obj, rect))
                    return rect;
            }
            return null;
        }
        private bool IsInside(IMovable obj, RectangleGeometry rect)
        {
           if(obj.ObjPosition.CurrentPose.Y > rect.Rect.TopLeft.Y 
                && obj.ObjPosition.CurrentPose.Y < rect.Rect.BottomRight.Y)
            {
                if (obj.ObjPosition.CurrentPose.X > rect.Rect.TopLeft.X - (obj.ObjPosition.MovingObject as FrameworkElement).Width / 4
                && obj.ObjPosition.CurrentPose.X < rect.Rect.BottomRight.X)
                {
                    return true;
                }

                else if (obj.ObjPosition.CurrentPose.X > rect.Rect.TopLeft.X - (obj.ObjPosition.MovingObject as FrameworkElement).Width / 2
                && obj.ObjPosition.CurrentPose.X < rect.Rect.BottomRight.X)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
