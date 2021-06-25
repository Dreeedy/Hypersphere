using System.Windows;

namespace Hypersphere
{
    interface IMouseMovement
    {
        Point previous
        {
            get; set;
        }

        Point current
        {
            get; set;
        }
    }
}
