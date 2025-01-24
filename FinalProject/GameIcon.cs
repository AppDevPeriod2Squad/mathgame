using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class GameIcon : AbsoluteLayout
    {
        public VerticalStackLayout? Stack { get; set; }
        public String? ButtonImageSource { get; set; }
        public GameIcon(String imgSource = "trash.png") : base()
        {
            ButtonImageSource = imgSource;
            StackSetup();
            AbsoluteLayout.SetLayoutFlags(Stack,AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(Stack, new Rect(0, 0, 1, 1));
            Add(Stack);
            
        }
        private void StackSetup()
        {
            Stack = new VerticalStackLayout();
            ImageButton button = new ImageButton() { Source = ButtonImageSource };

        }
    }
}
