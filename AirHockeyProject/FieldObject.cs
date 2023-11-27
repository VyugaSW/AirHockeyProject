using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AirHockeyProject
{

    public class FieldObject
    {
        public ObjPosition ObjPosition { get; set; }

        public FieldObject(UIElement movingObject, Action<UIElement, double> setLeft, Action<UIElement, double> setTop)
        {
            ObjPosition = new ObjPosition(movingObject, setLeft, setTop);
        }

    }
}
