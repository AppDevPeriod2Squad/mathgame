
using Microsoft.Maui.Layouts;
using System;

namespace FinalProject
{
    public class QuestionAndAnswers : Displayable
    {
        public List<Number>? AnswerOptions { get; set; }
        public Displayable? Question { get; set; }
        public List<Displayable>? OptionsList { get; set; }
        public GroupOfDisplayables? OptionsDisplay { get; set; }
        public int SpacingBetweenQuestions { get; set; }
        public QuestionAndAnswers(List<Displayable> choices, int spacingBetweenQuestions = 30)
        {
            if (choices == null)
            {
                throw new Exception("choices can't be null");
            }
            // is a list of Displayables but will actually be a list of GroupOfDisplayables
            if (choices.Count == 0 || !(choices[0] is GroupOfDisplayables))
            {
                OptionsList = new List<Displayable>();
                foreach (Displayable choice in choices)
                {
                    OptionsList.Add(new GroupOfDisplayables(new List<Displayable>() { choice }));
                }
            }
            else
            {
                // to convert choices to a List of GroupOfDisplayables
                OptionsList = new List<Displayable>();
                foreach (GroupOfDisplayables choice in choices)
                {
                    List<Displayable> group = new List<Displayable>();
                    foreach (Displayable individualDisplayable in choice.DisplayableGroup)
                    {
                        group.Add(individualDisplayable);
                    }
                    OptionsList.Add(new GroupOfDisplayables(group));
                }
            }
            SpacingBetweenQuestions = spacingBetweenQuestions;
        }
        public override void Display(Layout parentLayout,DisplayableArgs? args=null)
        {
            base.Display(parentLayout,args);
            DisplayOptions(parentLayout,args);
        }
        public void ButtonClicked(Object sender, EventArgs e)
        {
            // add functionality later
        }
        public void DisplayOptions(Layout parentLayout,DisplayableArgs? args,double optionHeight = -1)
        {
            // Calculates the widths for each individual answer and setup
            if (optionHeight < 0) // sets the default height to 1/3 of the total question's height
            {
                optionHeight = (MauiSource.HeightRequest - MauiSource.Padding.VerticalThickness) / 3;
            }
            OptionsDisplay = new GroupOfDisplayables(OptionsList);
            OptionsDisplay.Display(parentLayout,new DisplayableArgs(imageWidth:args.ImageWidth-args.Padding*2,imageHeight:optionHeight,addToParentLayout:false));
            OptionsDisplay.MauiSource.BackgroundColor = Color.FromRgb(0, 100, 0);
            OptionsDisplay.Layout.BackgroundColor = Color.FromRgb(0, 100, 100);
            AbsoluteLayout intermediaryLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(OptionsDisplay.MauiSource, new Rect(0, optionHeight*2, MauiSource.WidthRequest, optionHeight));
            AbsoluteLayout.SetLayoutFlags(OptionsDisplay.MauiSource, AbsoluteLayoutFlags.None);
            intermediaryLayout.BackgroundColor = Color.FromRgb(0, 0, 100);
            intermediaryLayout.Children.Add(OptionsDisplay.MauiSource);
            MauiSource.Add(intermediaryLayout);
            
        }

    }
}
