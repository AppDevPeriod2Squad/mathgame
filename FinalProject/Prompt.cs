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
        public Prompt(String promptString)
        {
            PromptString = promptString;
        }
        public override void Display(Layout parentLayout, DisplayableArgs? args = null)
        {
            if (args == null)
            {
                args = new DisplayableArgs(); 
            }
            args.Text = PromptString;
            base.Display(parentLayout, args);
        }
    }
}
