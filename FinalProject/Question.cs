
using Microsoft.Maui.Layouts;
using System;

namespace FinalProject
{
    public class QuestionAndAnswers : Displayable
    {
        public List<Number>? AnswerOptions { get; set; }
        public Displayable? QuestionDisplayable { get; set; }
        public List<Displayable>? OptionsList { get; set; }
        public GroupOfDisplayables? OptionsDisplay { get; set; }
        public int SpacingBetweenQuestions { get; set; }
        //public AbsoluteLayout temp = new AbsoluteLayout();
        
        public QuestionAndAnswers(List<Displayable> choices,Displayable question, int spacingBetweenQuestions = 0)
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
            QuestionDisplayable = question;
            SpacingBetweenQuestions = spacingBetweenQuestions;
        }
        public override void Display(Layout parentLayout,DisplayableArgs? args=null)
        {
            base.Display(parentLayout,args.Clone());
            MauiSource.BackgroundColor = Color.FromRgb(0, 0, 100);
            DisplayQuestion(parentLayout, args.Clone());
            DisplayOptions(parentLayout, args.Clone());

        }
        public void ButtonClicked(Object sender, EventArgs e)
        {
            // add functionality later
        }
        private void DisplayQuestion(Layout parentLayout, DisplayableArgs? args,double heightOfQuestion = -1)
        {
            if (heightOfQuestion < 0)
            {
                heightOfQuestion = Args.ImageHeight / 16;
            }
            args.StackLayoutOrientation = StackOrientation.Horizontal;
            args.TransformLayoutBounds($"{args.ImageWidth*2},0,0,{-args.ImageHeight + heightOfQuestion}");
            QuestionDisplayable.Display(AbsLayout, args);
        }
        private void DisplayOptions(Layout parentLayout,DisplayableArgs? args,double optionHeight = -1)
        {
            // Calculates the widths for each individual answer and setup
            if (optionHeight < 0) // sets the default height to 1/3 of the total question's height
            {
                optionHeight = (Args.ImageHeight - Args.Padding*2) / 3;
            }
            OptionsDisplay = new GroupOfDisplayables(OptionsList);
            // find a better way to copy the args without using the original args later
            args.ImageHeight = optionHeight;
            //args.StackLayoutOrientation = StackOrientation.Horizontal;
            //args.AbsoluteLayoutBounds = $"0,{optionHeight},{Args.ImageWidth},{optionHeight}";
            //args.AbsoluteLayoutBounds = $"0,0,100,100";
            args.TransformLayoutBounds($"0,{2*optionHeight},0,{-Args.ImageHeight+optionHeight}");
            //args.
            OptionsDisplay.Display(AbsLayout, args);
            //new Number(val: 3, imageType: ImageType.Dice).Display(AbsLayout, args);
            // yo i think it would work if u are able to set the MauiSource to an AbsoluteLayout ????? 

        }

    }
}
