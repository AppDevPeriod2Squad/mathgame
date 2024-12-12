using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Number : Displayable
    {
        private readonly SpriteManager spriteManager = new SpriteManager();

        private int val; 
        public int Val
        {
            get { return val; }
            set
            {
                val = value;
                UpdateImageSource(lastSavedImageType);
            }
        }
        public override ImageSource? ImageSource { get; set; }
        private ImageType lastSavedImageType {  get; set; }
        public Number(int val = 1, ImageType imageType = ImageType.None)
        {
            Val = val;
            UpdateImageSource(imageType);
        }

        private void UpdateImageSource(ImageType imageType)
        {
            ImageSource = imageType switch
            {
                ImageType.TenFrames => spriteManager.GetTenFramesImage(Val),
                ImageType.Dice => spriteManager.GetDiceImage(Val),
                _ => null
            };
            lastSavedImageType = imageType;
        }
    }
}
