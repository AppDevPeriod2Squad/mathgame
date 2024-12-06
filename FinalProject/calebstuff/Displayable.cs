using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame
{
    public abstract class Displayable
    {
        public int StartX { get; set; }
        public int StartY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public Displayable()
        {

        }
        public Displayable(int startX, int startY)
        {
            this.StartX = startX;
            this.StartY = startY;
        }
        public Displayable(int startX, int startY,int width, int height)
        {
            this.StartX = startX;
            this.StartY = startY;
            this.Width = width;
            this.Height = height;
        }
        public abstract void SetScale(double scale);




        public abstract void Display(ICanvas canvas);
    }
}
