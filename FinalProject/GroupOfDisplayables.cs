
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
        public GroupOfDisplayables(Displayable displayable,int spacingBetweenDisplayables = 5)
        {
            DisplayableGroup = new List<Displayable>() { displayable };
            SpacingBetweenDisplayables = spacingBetweenDisplayables;
        }
        public void GroupClicked(object? sender, EventArgs e)
        {
            if (sender is GroupOfDisplayables groupOfDisplayables)
            {
                // to send the event notification all the way up the groupofdisplayable chain
                Args.ClickedEventHandler?.Invoke(sender, new EventArgs());
            }
            else
            {
                Args.ClickedEventHandler?.Invoke(this, new EventArgs());
            }
        }
        public double EvaluateEquation()
        {
            double tot = 0;
            Displayable prevDisp = null;
            foreach (Displayable disp in DisplayableGroup)
            {
                if (disp is Number && prevDisp is Symbol)
                {
                    Symbol? symbol = prevDisp as Symbol;
                    Number? number = disp as Number;
                    switch (symbol.lastSavedSymbolType)
                    {
                        case SymbolType.Plus:
                            tot += number.Val;
                            break;
                    }
                }
                prevDisp = disp;
            }
            if (DisplayableGroup.Count > 0 && DisplayableGroup[0] is Number num)
            {
                tot += num.Val;
            }
            return tot;
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
            indivArgs.ClickedEventHandler = new EventHandler((sender, e) => GroupClicked(sender, e));
            foreach (var choice in DisplayableGroup)
            {
                choice.Display(AbsLayout, indivArgs
                    //clickedEventHandler: new EventHandler((sender, e) => ButtonClicked(this, new EventArgs()))
                    
                    );
                indivArgs.TransformLayoutBounds($"{optionWidth+SpacingBetweenDisplayables},0,0,0");
            }
            double b = MauiSource.WidthRequest;
        }
        public override bool Compare(Displayable d)
        {
            if (d is GroupOfDisplayables g)
            {
                if (g.DisplayableGroup.Count != DisplayableGroup.Count) return false;

                for (int i = 0; i < g.DisplayableGroup.Count; i++)
                {
                    if (!g.DisplayableGroup[i].Compare(DisplayableGroup[i]))
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        }

    }
    
}
