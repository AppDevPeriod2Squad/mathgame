using MathGame.Sprites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MathGame.Number
{
    public class NumberRepresentation : Displayable
    {
        public SpriteManager SpriteManager { get; set; }
        public int Number { get; set; }
        public NumberRepresentation(int number, RepresentationType type)
        {
            StartX = 0;
            StartY = 0;
            Number = number;
            if (type == RepresentationType.DICE)
            {
                SetToDice();
            }
            if (type == RepresentationType.TENFRAMES)
            {
                SetToTenFrames();   
            }
            SetToDefaultSize();
        }
        public NumberRepresentation(int number,RepresentationType type, int startX, int startY) : this(number,type)
        {
            StartX = startX;
            StartY = startY;
        }
        public NumberRepresentation(int number, RepresentationType type, int startX, int startY, int width, int height) : this(number,type, startX, startY)
        {
            Width = width;
            Height = height;
        }
        public void SetToDefaultSize()
        {
            if (SpriteManager != null)
            {
                Width = SpriteManager.SpriteSize[0];
                Height = SpriteManager.SpriteSize[1];
            }
        }
        public void SetToDice()
        {
            SpriteManager = new SpriteManager("DiceSprites") { Frame=Number};
        }
        public void SetToTenFrames()
        {
            SpriteManager = new SpriteManager("DiceSprites") { Frame=Number};
        }
        public override void SetScale(double scale)
        {
            SetToDefaultSize();
            Width = (int)(Width * scale);
            Height = (int)(Height * scale);
        }
        public override void Display(ICanvas canvas)
        {
            // yo this took so long to figure out bro
            canvas.DrawImage(SpriteManager.GetSprite(), StartX, StartY, Width, Height); // Draw the image on the canvas YAYAYAYAYAY

        }
    }
}
