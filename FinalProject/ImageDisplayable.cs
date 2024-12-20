using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class ImageDisplayable : Displayable
    {
        public ImageDisplayable(String source) {
            ImageSource = ImageSource.FromFile(source);
        }

        public override void Display(Layout parentLayout, DisplayableArgs? args = null)
        {
            
            base.Display(parentLayout, args);
        }
        public override bool Compare(Displayable d)
        {
            throw new NotImplementedException();
        }
    }
}
