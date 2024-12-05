using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    internal class SpriteManager
    {
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

        public SpriteManager() { }
    }
}