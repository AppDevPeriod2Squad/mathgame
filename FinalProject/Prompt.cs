using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Prompt : Displayable
    {
        public String PromptString { get; set; }
        public Prompt(String s)
        {
            PromptString = s;
        }
        public override void Display(Layout parentLayout, DisplayableArgs? args = null)
        {
            args.Text = PromptString; ;
            base.Display(parentLayout, args);
        }
    }
}
