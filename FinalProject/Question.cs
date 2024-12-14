
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
        public GroupOfDisplayables? CorrectAnswer {  get; set; }
        public EventHandler? questionHandler {  get; set; }
        //public AbsoluteLayout temp = new AbsoluteLayout();
        
        public QuestionAndAnswers(List<Displayable> choices,Displayable question, EventHandler? questionClickedHandler = null, GroupOfDisplayables? correctAnswer = null, int spacingBetweenQuestions = 60)
        {
            if (choices == null)
            {
                throw new Exception("choices can't be null");
            }
            if (correctAnswer == null)
            {
                CorrectAnswer = new GroupOfDisplayables(new Prompt("LOL NO ANSWER"));
            }
            CorrectAnswer = correctAnswer;
            questionHandler = questionClickedHandler;
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
            OptionsDisplay = new GroupOfDisplayables(OptionsList, spacingBetweenDisplayables: spacingBetweenQuestions);
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
            if (sender is GroupOfDisplayables group1 && CorrectAnswer is GroupOfDisplayables group2)
            {
                if (group1.Compare(group2)){
                    questionHandler?.Invoke(this, e);
                }
            }
        }
        private void DisplayQuestion(Layout parentLayout, DisplayableArgs? args,double heightOfQuestion = -1)
        {
            if (heightOfQuestion < 0)
            {
                heightOfQuestion = Args.ImageHeight / 6;
            }
            args.StackLayoutOrientation = StackOrientation.Horizontal;
            args.HorizontalOptions = LayoutOptions.Center;
            args.TransformLayoutBounds($"0,0,0,{-Args.ImageHeight + heightOfQuestion}");
            QuestionDisplayable.Display(AbsLayout, args);
        }
        private void DisplayOptions(Layout parentLayout,DisplayableArgs? args,double optionHeight = -1)
        {
            // Calculates the widths for each individual answer and setup
            if (optionHeight < 0) // sets the default height to 1/3 of the total question's height
            {
                optionHeight = (Args.ImageHeight - Args.Padding*2) / 3;
            }

            // find a better way to copy the args without using the original args later
            args.ImageHeight = optionHeight;
            args.ClickedEventHandler = new EventHandler((sender,e)=>ButtonClicked(sender,e));
            args.ViewType = ViewType.ImageButton;
            //args.StackLayoutOrientation = StackOrientation.Horizontal;
            //args.AbsoluteLayoutBounds = $"0,{optionHeight},{Args.ImageWidth},{optionHeight}";
            //args.AbsoluteLayoutBounds = $"0,0,100,100";
            args.TransformLayoutBounds($"0,{2*optionHeight},0,{-Args.ImageHeight+optionHeight}");
            //args.
            OptionsDisplay.Display(AbsLayout, args);
            //new Number(val: 3, imageType: ImageType.Dice).Display(AbsLayout, args);
            // yo i think it would work if u are able to set the MauiSource to an AbsoluteLayout ????? 

        }

        public override bool Compare(Displayable d)
        {
            throw new NotImplementedException();
        }
    }
}
