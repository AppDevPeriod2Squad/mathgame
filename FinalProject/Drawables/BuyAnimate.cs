using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Drawables
{
    public class BuyAnimate : IDrawable
    {
        long start = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        float armsInTime = 2f;
        float outTime = 1f;
        public async void Draw(ICanvas canvas, RectF dirtyRect)
        {
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
                        canvas.FillColor = Color.FromRgb(1, 0.773f * (1-(dist/max-centerSize)*(1/(1-centerSize))), (float)(i * dotsPerBranch + j)/(branches * dotsPerBranch));
                        canvas.FillCircle((float)(dirtyRect.Width / 2 + dist * Math.Cos(ang * Math.PI / 180.0f)), 
                            dirtyRect.Height / 2 + dist * (float)Math.Sin(ang * Math.PI / 180.0f), 
                            60 * (dist-circSize*max)/((1-circSize)*max));
                    }
                }
                canvas.FillColor = Color.FromRgb(1, 0.773f, 0);
                canvas.FillCircle((float)dirtyRect.Width / 2, (float)dirtyRect.Height / 2, circSize * max);
            }
            else
            {
                canvas.FillColor = Color.FromRgb(1, 0.773f, 0);
                canvas.FillCircle((float)dirtyRect.Width / 2, (float)dirtyRect.Height / 2, (float)(max * centerSize * (1+Math.Pow(((t-armsInTime)/outTime), 10))));
                if ((float)(max * centerSize * (1 + Math.Pow(((t - armsInTime) / outTime), 10))) > Math.Max((int)dirtyRect.Width / 2, (int)dirtyRect.Height / 2))
                {
                    
                }
            }
            

        }
    }
}
