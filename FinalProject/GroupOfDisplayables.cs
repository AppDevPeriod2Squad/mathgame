
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
        public HorizontalStackLayout? Layout { get; set; }
        public int SpacingBetweenDisplayables { get; set; }
        
        public GroupOfDisplayables(List<Displayable>? displayableGroup, int spacingBetweenDisplayables = 5)
        {
            DisplayableGroup = displayableGroup;
            SpacingBetweenDisplayables = spacingBetweenDisplayables;
        }

        public override void Display(Layout parentLayout, DisplayableArgs? args = null)
        {
            args.Padding = 0;
            args.StackLayoutOrientation = StackOrientation.Horizontal;
            base.Display(parentLayout, args);
            double a = MauiSource.WidthRequest;
            MauiSource.HorizontalOptions = LayoutOptions.Start;

            //foreach (Displayable d in DisplayableGroup)
            //{
            //    d.Display(parentLayout, args);
            //    Layout.Add(d.MauiSource);
            //}
            double totalOptionsWidth = Args.ImageWidth;
            double optionWidth = (totalOptionsWidth-SpacingBetweenDisplayables*(DisplayableGroup.Count-1)) / DisplayableGroup.Count;
            double c = MauiSource.WidthRequest;
            // Creates the Displayable but doesn't put in on screen to avoid errors
            DisplayableArgs indivArgs = args.Clone();
            indivArgs.TransformLayoutBounds($"0,0,{-indivArgs.ImageWidth + optionWidth},0");
            foreach (var choice in DisplayableGroup)
            {
                choice.Display(AbsLayout, indivArgs
                    //clickedEventHandler: new EventHandler((sender, e) => ButtonClicked(this, new EventArgs()))
                    
                    );
                indivArgs.TransformLayoutBounds($"{optionWidth+SpacingBetweenDisplayables},0,0,0");
            }
            double b = MauiSource.WidthRequest;
        }
    }
}
