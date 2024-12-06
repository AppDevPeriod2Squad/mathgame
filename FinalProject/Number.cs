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

        public int Val { get; set; }
        public override ImageSource? ImageSource { get; set; }
        public override string? Text { get; set; }
        public Number(int val = 0, string? text = null, ImageType imageType = ImageType.None)
        {
            Val = val;
            Text = text;
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
        }
    }
}
