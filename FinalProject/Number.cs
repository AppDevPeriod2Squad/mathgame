using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Number : Displayable
    {
        private SpriteManager spriteManager = new SpriteManager();
        private int val;

        private int myVar;



        public int Val
        {
            get { return val; }
            set
            {
                val = value;
                ImageSource = spriteManager.GetTenFramesImage(val); // use ten frames by default
            }
        }
        private ImageSource? imageSource;
        public override ImageSource ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; }
        }

        public override string? Text { get; set; }

        public void UpdateImageToTenFrames()
        { 
            ImageSource = spriteManager.GetTenFramesImage(Val);
        }
    }
}