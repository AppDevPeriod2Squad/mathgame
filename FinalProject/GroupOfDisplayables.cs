
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
        public new double AbsoluteWidth { get { return AbsoluteWidth_; } set { AbsoluteWidth_ = AbsoluteWidthReq(); } }
        private double AbsoluteWidth_;
        private double AbsoluteWidthReq()
        {
            double sum = 0;
            foreach (Displayable d in DisplayableGroup)
            {
                sum += d.AbsoluteWidth;
            }
            sum += MauiSource.Padding.HorizontalThickness;
            sum += SpacingBetweenDisplayables * (DisplayableGroup.Count - 1);
            return sum;
        }
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
            args.Padding = 2;
            args.Spacing = 2;
            SpacingBetweenDisplayables = args.Spacing;
            // -----
            base.Display(parentLayout, args);
            double totalOptionsWidth = MauiSource.WidthRequest-args.Padding*3;
            double optionWidth = (totalOptionsWidth - SpacingBetweenDisplayables * (DisplayableGroup.Count-1)) / DisplayableGroup.Count;
            //Layout.Spacing = SpacingBetweenDisplayables;
            // Creates the Displayable but doesn't put in on screen to avoid errors
            foreach (var choice in DisplayableGroup)
            {
                DisplayableArgs tempArgs = args.Clone();
                tempArgs.ImageWidth = optionWidth;
                tempArgs.Spacing = SpacingBetweenDisplayables;
                choice.Display(MauiSource, tempArgs
                    );
                
            }
            MauiSource.BackgroundColor = Color.FromRgb(10, 10, 100);
        }
    }
}
