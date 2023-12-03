using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AirHockeyProject
{
    public interface IMoving : IMovable
    {
        double PoseOffset { get; }
        void Move(Key key1);
    }
}
