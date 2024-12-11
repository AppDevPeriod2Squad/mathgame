
using Microsoft.Maui.Layouts;
using System;

namespace FinalProject
{
    public class QuestionAndAnswers : Displayable
    {
        public GroupOfDisplayables QuestionDisplayable { get; set; }
        public List<Displayable>? OptionsList { get; set; }
        public GroupOfDisplayables? OptionsDisplay { get; set; }
        public int SpacingBetweenQuestions { get; set; }
        public AbsoluteLayout temp = new AbsoluteLayout();
        public QuestionAndAnswers(List<Displayable> choices,Displayable question, int spacingBetweenQuestions = 0)
        {
            if (choices == null)
            {
                throw new Exception("choices can't be null");
            }
            // to convert question to a DisplayableGroup
            if (question is GroupOfDisplayables)
            {
                QuestionDisplayable = (GroupOfDisplayables) question;
            }
            else
            {
                QuestionDisplayable = new GroupOfDisplayables(question);
            }

            // is a list of Displayables but will actually be a list of GroupOfDisplayables
            if (choices.Count == 0 || !(choices[0] is GroupOfDisplayables))
            {
                OptionsList = new List<Displayable>();
                foreach (Displayable choice in choices)
                {
                    OptionsList.Add(new GroupOfDisplayables(choice));
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
            MauiSource.BackgroundColor = Color.FromRgb(0, 0, 100);
            //DisplayQuestion(parentLayout,args.Clone());
            DisplayOptions(parentLayout,args.Clone());
          
        }
        public void ButtonClicked(Object sender, EventArgs e)
        {
            // add functionality later
        }
        private void DisplayQuestion(Layout parentLayout, DisplayableArgs? args,double heightOfQuestion = -1)
        {
            if (heightOfQuestion < 0)
            {
                heightOfQuestion = MauiSource.HeightRequest / 4;
            }
            args.AbsoluteLayoutBounds = $"0,0,{MauiSource.WidthRequest},{heightOfQuestion}";
            QuestionDisplayable.Display(temp, args);
            QuestionDisplayable.MauiSource.BackgroundColor = Color.FromRgb(100, 0, 0);
        }
        private void DisplayOptions(Layout parentLayout,DisplayableArgs? args,double optionHeight = -1)
        {
            MauiSource.Add(temp);
            // Calculates the widths for each individual answer and setup
            if (optionHeight < 0) // sets the default height to 1/3 of the total question's height
            {
                optionHeight = (MauiSource.HeightRequest - MauiSource.Padding.VerticalThickness) / 3;
            }
            OptionsDisplay = new GroupOfDisplayables(OptionsList);
            // find a better way to copy the args without using the original args later
            args.ImageHeight = optionHeight;
            args.StackLayoutOrientation = StackOrientation.Horizontal;
            args.AbsoluteLayoutBounds = $"0,{MauiSource.HeightRequest-optionHeight},{MauiSource.WidthRequest/2},{optionHeight}";
            //args.
            Displayable d = new Number(imageType:ImageType.Dice,val:4);
            d.Display(temp, args);
            OptionsDisplay.Display(temp, args);

            // yo i think it would work if u are able to set the MauiSource to an AbsoluteLayout ????? 

        }

    }
}
