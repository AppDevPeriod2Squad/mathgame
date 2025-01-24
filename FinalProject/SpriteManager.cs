using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class SpriteManager
    {
        public const int NumberOfImageTypes = 1; // available image types with all image assets

        public ImageSource GetTenFramesImage(int value)
        {
            string filename = $"{value}.png";
            return ImageSource.FromFile($"tenframes_{filename}");
        }

        public ImageSource GetDiceImage(int value)
        {
            string filename = $"{value}.png";
            return ImageSource.FromFile($"dice_{filename}");
        }
        public ImageSource GetPlus()
        {
            return ImageSource.FromFile("plus_1.png");
        }
        public ImageSource GetMultiply()
        {
            Random r = new Random();
            if (r.Next(0,10) == 1)
            {
                return ImageSource.FromFile("golden_multiplication.jpg");
            }
            return ImageSource.FromFile("multiply.png");
        }
        public ImageSource GetDivision()
        {
            return ImageSource.FromFile("division.png");
        }
        public ImageSource GetMinus()
        {
            return ImageSource.FromFile("minus.png");
        }
        public ImageSource GetFoodImage()
        {
            return ImageSource.FromFile("food.png");
        }

        public SpriteManager() { }
    }
}