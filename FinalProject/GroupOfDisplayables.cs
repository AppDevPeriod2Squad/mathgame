
using Microsoft.Maui.Controls.Shapes;
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
        public double SpacingBetweenDisplayables { get; set; }
        public double transformAmnt;
        public Color Background {  get; set; }
        public GroupOfDisplayables(List<Displayable>? displayableGroup, double spacingBetweenDisplayables = 0.005, Color background = null)
        {
            // DisplayableGroup is the List of the displayables that this group contains
            Background = background;
            DisplayableGroup = displayableGroup;
            SpacingBetweenDisplayables = spacingBetweenDisplayables;
        }
        public GroupOfDisplayables(Displayable displayable,double spacingBetweenDisplayables = 0.005, Color background = null)
        {
            Background = background;
            // Alternate constructor if you just want one Displayable in your group (for simplicity)
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
                // for the base of the GroupOfDisplayable
                Args.ClickedEventHandler?.Invoke(this, new EventArgs());
            }
        }
        public double EvaluateEquation()
        {
            // Goes through all the Displayables in the group and evaluates them
            double tot = 0;
            Displayable prevDisp = null;
            if (DisplayableGroup.Count > 0 && DisplayableGroup[0] is Number num)
            {
                tot += num.Val;
            }
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
                        case SymbolType.Multiply:
                            tot *= number.Val;
                            break;
                        case SymbolType.Division:
                            tot /= number.Val;
                            break;
                        case SymbolType.Minus:
                            tot -= number.Val;
                            break;

                    }
                }
                prevDisp = disp;
            }
            
            return tot;
        }

        public override void Display(Layout parentLayout, DisplayableArgs? args = null)
        {

            
            double spacing = SpacingBetweenDisplayables;
            base.Display(parentLayout, args);
            if (Background != null)
            {
                Rectangle r = new Rectangle() { Background = Background};
                
                AbsoluteLayout.SetLayoutBounds(r,ParseBounds( args.AbsoluteLayoutBounds));
               
                AbsoluteLayout.SetLayoutFlags(r, args.AbsoluteLayoutFlags);
                parentLayout.Add(r);
            }
            DisplayableArgs indivArgs = args.Clone();
            // finds how wide each of the individual Displayables in the group will be to evenly space them
            double totalOptionsWidth = Args.ImageWidth;
            //double optionWidth = (totalOptionsWidth-SpacingBetweenDisplayables*(DisplayableGroup.Count-1)) / DisplayableGroup.Count;
            double optionWidth = (totalOptionsWidth) / DisplayableGroup.Count;
            // the args for each indiv displayable
            
            indivArgs.TransformLayoutBounds($"{spacing},0,{-indivArgs.ImageWidth + optionWidth},0");
            indivArgs.ClickedEventHandler = new EventHandler((sender, e) => GroupClicked(sender, e));
            double amnt = DisplayableGroup.Count;
            double pow = Math.Log(optionWidth) / Math.Log(1/2.0);
            transformAmnt = (1 / 2.0) * Math.Pow(10, -(pow - 1));
            optionWidth -= 2 * spacing;
            foreach (var choice in DisplayableGroup)
            {
                //    if ((indivArgs.AbsoluteLayoutBounds[2]).ToString() != "6")
                //{
                //    choice.Display(parentLayout, indivArgs
                //   );
                indivArgs.TransformLayoutBounds($"{spacing},0,{-spacing * 2},0");
                choice.Display(parentLayout, indivArgs);
                indivArgs.TransformLayoutBounds($"{spacing},0,{spacing * 2},0");




                // changes the LayoutBounds by that the width and spacing to correctly put them 

                if (choice is GroupOfDisplayables sub)
                {
                    transformAmnt = sub.transformAmnt*(Math.Log(amnt,2));
                }
                var a = indivArgs.AbsoluteLayoutBounds;
                indivArgs.TransformLayoutBounds($"{optionWidth + transformAmnt},0,0,0");
                //indivArgs.TransformLayoutBounds($"{optionWidth + .05},0,0,0");
            }
        }
        public override bool Compare(Displayable d)
        {
            // checks to see if each Displayable in both groups are the same
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
