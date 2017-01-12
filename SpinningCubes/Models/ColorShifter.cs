using System.Windows.Media;
using SpinningCubes.Extensions;

namespace SpinningCubes.Models
{
    public class ColorShifter
    {
        private int _r;
        private int _g;
        private int _b;

        public int Red { get { return _r; } }
        public int Green { get { return _g; } }
        public int Blue { get { return _b; } }

        public Color ColorFromRgb
        {
            get
            {
                var r = _r.ToAbsoluteColor();
                var g = _g.ToAbsoluteColor();
                var b = _b.ToAbsoluteColor();
                return Color.FromRgb((byte)r, (byte)g, (byte)b);
            }
        }

        public ColorShifter(int r, int g, int b)
        {
            _r = r;
            _g = g;
            _b = b;
        }

        public void Increment(int step = 1)
        {
            _r = Increment(_r, step);
            _g = Increment(_g, step);
            _b = Increment(_b, step);
        }

        public ColorShifter Clone()
        {
            return new ColorShifter(_r, _g, _b);
        }

        private int Increment(int value, int step)
        {
            value = value + step;
            return value > 255 ? value - 510 : value;
        }
    }
}
