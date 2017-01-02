using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfTest.Models
{
    public class AngleShifter
    {
        private double _angle;

        public double Angle { get { return _angle; } }

        public AngleShifter(double angle)
        {
            _angle = angle;
        }

        public void Increment(double step = 1)
        {
            _angle = Increment(_angle, step);
        }

        private double Increment(double angle, double step)
        {
            _angle = _angle + step;
            return _angle > 180 ? _angle - 360 : _angle;
        }
    }
}
