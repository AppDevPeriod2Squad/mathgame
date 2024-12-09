using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IImage = Microsoft.Maui.Graphics.IImage;
namespace MathGame.Sprites
{
    public class SpriteManager
    {
        public const String SpriteFolderPath = "C:\\Users\\caleb\\source\\repos\\MathGame\\MathGame\\Sprites\\";
        public Dictionary<String,IImage> Images { get; set; } = new Dictionary<String,IImage>();
        public int[] SpriteSize;
        public int Frame { get; set; } = 1; // something weird here LATER look at
        public SpriteManager(String spriteFolderPath)
        {
            String[] spritePaths = Directory.GetFiles(SpriteFolderPath+spriteFolderPath);
            foreach (String spritePath in spritePaths)
            {
                Stream fileStream = File.OpenRead(spritePath);
                IImage image = PlatformImage.FromStream(fileStream);
                if (spritePath.Contains("_"))
                {
                    int indexOfSplitter = spritePath.LastIndexOf("_");
                    String key = spritePath.Substring(indexOfSplitter+1,spritePath.Length-indexOfSplitter-5); // -3 for .png
                    Images.Add(key, image);
                }
            }
            //Stream fileStream = File.OpenRead(SpritePath);
            //IImage image = PlatformImage.FromStream(fileStream);
            //Images.Add(image);
            SpriteSize = new int[] { (int)GetSprite().Width, (int)GetSprite().Height};
        }

        public IImage GetSprite()
        {
            return Images[Frame.ToString()];
        }
    }
}
// fix up tomorrow, prob re-work idea