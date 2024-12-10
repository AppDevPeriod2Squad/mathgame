
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class GroupOfDisplayables : Displayable
    {
        public List<Displayable>? DisplayableGroup { get; set; }
        public int SpacingBetweenDisplayables { get; set; }
        public GroupOfDisplayables(Displayable d, int spacingBetweenDisplayables = 10)
        {
            // alternative constructor if you only want one Displayable in a group, for ease
            DisplayableGroup = new List<Displayable>() { d };
            SpacingBetweenDisplayables = spacingBetweenDisplayables;
        }
        public GroupOfDisplayables(List<Displayable>? displayableGroup, int spacingBetweenDisplayables = 10)
        {
            DisplayableGroup = displayableGroup;
            SpacingBetweenDisplayables = spacingBetweenDisplayables;
        }

        public override void Display(Layout parentLayout, DisplayableArgs? args = null)
        {
            // SETTING SPACING AND PADDING TO 0 CAUSE BUG CAUSED BY NOT ACCOUNTING FOR SMALLER PADDING IN THE GROUP'S GROUPS
            args.Padding = 0;
            args.Spacing = 0;
            SpacingBetweenDisplayables = 0;
            // -----
            base.Display(parentLayout, args);
            double totalOptionsWidth = MauiSource.WidthRequest - MauiSource.Padding.HorizontalThickness;
            double optionWidth = (totalOptionsWidth - SpacingBetweenDisplayables * (DisplayableGroup.Count-1)) / DisplayableGroup.Count;
            //Layout.Spacing = SpacingBetweenDisplayables;
            // Creates the Displayable but doesn't put in on screen to avoid errors
            foreach (var choice in DisplayableGroup)
            {
                DisplayableArgs tempArgs = args.Clone();
                tempArgs.AddToParentLayout = false;
                tempArgs.ImageWidth = optionWidth;
                tempArgs.Spacing = SpacingBetweenDisplayables;
                choice.Display(MauiSource, tempArgs
                    );
                MauiSource.Add(choice.MauiSource);
            }
        }
    }
}
