using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Drawables
{
    
    public class BuyAnimate : IDrawable
    {
        long start = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public static string imageLink = "https://i.ytimg.com/vi/SqtaV1NFFEQ/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLAUtLwIBPy4fjjXYdz2WwuoSiHOIA";
        Microsoft.Maui.Graphics.IImage image;
        public static int rarity;
        float armsInTime = 2f;
        float outTime = 1f;
        bool firstTick = true;
        float bigSize = 0;
        public async Task LoadImageAsync(string url)
        {
            using var stream = await new HttpClient().GetStreamAsync(url);
            image = PlatformImage.FromStream(stream);
        }
        public async void Draw(ICanvas canvas, RectF dirtyRect)
        {
            if (firstTick)
            {
                LoadImageAsync(imageLink);
                firstTick = false;
            }
            float t = (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() - start) / 1000.0f;
            int max = Math.Min((int)dirtyRect.Width / 2, (int)dirtyRect.Height / 2);
            float centerSize = 0.4f;
            if (t < armsInTime)
            {
                int branches = 12;
                int dotsPerBranch = 6;
                float circSize = t / armsInTime * centerSize;

                for (int i = 0; i < branches; i++)
                {
                    for (int j = 0; j < dotsPerBranch; j++)
                    {
                        float dist = circSize * max + ((float)j) / dotsPerBranch * (max * (1-circSize)) * (armsInTime - t);
                        float ang = ((float)i) / branches * 360 + ((float)j) / dotsPerBranch * 360.0f / branches + t / armsInTime * 360;
                        canvas.FillColor = Color.FromRgb(0.6f + 0.4f * (1 - (dist / max - centerSize) * (1 / (1 - centerSize))), 
                            0f + 0.5f * (1-(dist/max-centerSize)*(1/(1-centerSize)) + 0.5f * ((float)(i * dotsPerBranch + j) / (branches * dotsPerBranch))), 
                            0.3f + 0.4f * (1 - (dist / max - centerSize) * (1 / (1 - centerSize))) + 0.3f * ((float)(i * dotsPerBranch + j)/(branches * dotsPerBranch)));
                        canvas.FillCircle((float)(dirtyRect.Width / 2 + dist * Math.Cos(ang * Math.PI / 180.0f)), 
                            dirtyRect.Height / 2 + dist * (float)Math.Sin(ang * Math.PI / 180.0f), 
                            60 * (dist-circSize*max)/((1-circSize)*max));
                    }
                }
                canvas.FillColor = Color.FromRgb(1.0f, 1.0f, 1.0f);
                canvas.FillCircle((float)dirtyRect.Width / 2, (float)dirtyRect.Height / 2, circSize * max);
            }
            else
            {
                float tN = Math.Min(1f, t-armsInTime);
                if (rarity == 2)
                {
                    canvas.FillColor = Color.FromRgb(1 - 0.5f * tN, 1 - 0.5f * tN, 1 - 0.5f * tN);
                } else if (rarity == 3)
                {
                    canvas.FillColor = Color.FromRgb(1 - tN, 1 - (255-176.0f)/255.0f * tN, 1 - (255 - 240.0f) / 255.0f * tN);

                }
                else if (rarity == 4)
                {
                    float[] a = { 1 - (255 - 112.0f) / 255.0f * tN, 1 - (255 - 148.0f) / 255.0f * tN, 1 - (255 - 160.0f) / 255.0f * tN };
                    canvas.FillColor = Color.FromRgb(1 - (255 - 112.0f) / 255.0f * tN, 1 - (255 - 48.0f) / 255.0f * tN, 1 - (255 - 160.0f) / 255.0f * tN);
                }
                else if (rarity == 5)
                {
                    canvas.FillColor = Color.FromRgb(1 - (255 - 255.0f) / 255.0f * tN, 1 - (255 - 192.0f) / 255.0f * tN, 1 - (255 - 0.0f) / 255.0f * tN);

                }
                canvas.FillCircle((float)dirtyRect.Width / 2, (float)dirtyRect.Height / 2, (float)(max * centerSize * (1+Math.Pow(((t-armsInTime)/outTime), 10))));
                if ((float)(max * centerSize * (1 + Math.Pow(((t - armsInTime) / outTime), 10))) > Math.Max((int)dirtyRect.Width / 2, (int)dirtyRect.Height / 2))
                {
                    if (bigSize == 0)
                    {
                        bigSize = t;
                    }
                    float size = Math.Min((t-bigSize) * 800, 400);
                    if (image != null) {
                        canvas.DrawImage(image, (float)dirtyRect.Width / 2-size/2, (float)dirtyRect.Height / 2-size/2, size, size);
                    }

                    if (size == 400)
                    {
                        canvas.DrawString("Tap anywhere to exit", (float)dirtyRect.Width / 2, 10, HorizontalAlignment.Center);
                    }
                }
                
            }
            

        }
    }
}
