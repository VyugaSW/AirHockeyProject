using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirHockeyProject
{
    public interface ICollisable : IMovable
    {
        IMovable[] ObjectsOnField { get; }

        double MaxYBorder { get; }
        double MinYBorder { get; }
        double MaxXBorder { get; }
        double MinXBorder { get; }

        void CheckCollision(object sender, EventArgs e);
    }
}
