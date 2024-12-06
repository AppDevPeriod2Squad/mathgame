
using MathGame.Number;
using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace MathGame
{
    public class DrawManager : IDrawable
    {
        public List<Displayable> Displayables { get; set; }
        public DrawManager()
        {
            Displayables = new List<Displayable>();
            Displayables.Add(new NumberRepresentation(1,RepresentationType.DICE));
            Displayables.Add(new NumberRepresentation(3, RepresentationType.DICE,50, 50));
            Displayables.Add(new NumberRepresentation(5, RepresentationType.DICE,100, 100,200,200));
            Displayables.Last().SetScale(.5);
        }
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            foreach (Displayable displayable in Displayables)
            {
                displayable.Display(canvas);
            }

        }
    }
}
