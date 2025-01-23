
using Microsoft.Maui.Layouts;
using System;

namespace FinalProject
{
    public class QuestionAndAnswers : Displayable
    {
        public Displayable? QuestionPrompt { get; set; }
        public GroupOfDisplayables? OptionsDisplay { get; set; }
        public int SpacingBetweenQuestions { get; set; }
        public GroupOfDisplayables? CorrectAnswer {  get; set; }
        public EventHandler? questionHandler {  get; set; }
        
        public QuestionAndAnswers(List<Displayable> choices,
            Displayable questionPrompt,
            EventHandler? questionClickedHandler = null, 
            GroupOfDisplayables? correctAnswer = null, 
            int spacingBetweenQuestions = 0)
        {
            // Sets all the Displayables that the Question has, which are the QuestionPrompt and the OptionsDisplay
            // Also sets other variables that get used to handle specific tasks, like user interface and spacing
            // SPACING POTENTIALLY REMOVED IN FAVOR OF USING DISPLAYS SPACING IN FUTURE
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
            QuestionPrompt = questionPrompt;
            SpacingBetweenQuestions = spacingBetweenQuestions;
            OptionsDisplay = new GroupOfDisplayables(ConvertToGroupedList(choices), spacingBetweenDisplayables: 0.025);
            
            //foreach (GroupOfDisplayables g in OptionsDisplay.DisplayableGroup)
            //{
            //    Random r = new Random();
            //    g.Background = Color.FromRgb(r.Next(0, 100), r.Next(0, 100), r.Next(0, 100));
            //}
        }
        public static List<Displayable> ConvertToGroupedList(List<Displayable> displayables)
        {
            // This function makes a new list of GroupOfDisplayable with all Displayables that were already Groups staying the same but putting
            // that were't into a GroupOfDisplayable
            List<Displayable> groupedList = new List<Displayable>();
            foreach (var displayable in displayables)
            {
                if (displayable is GroupOfDisplayables group)
                {
                    groupedList.Add(group);
                }
                else
                {
                    groupedList.Add(new GroupOfDisplayables(displayable));
                }
            }
            return groupedList;
        }
        public override void Display(Layout parentLayout,DisplayableArgs? args=null)
        {
            // Displays itself, which sets all need Properties for it's subdisplays
            // Properties like AbsLayout, and MauiSource, which are used to interact with the screen and Args
            // Args is used to reference the total QuestionAndAnswers args (can be renamed for clarity later maybe?)
            base.Display(parentLayout,args);
            // Displays Question and the Options, args are cloned to prevent reference type args from unintended issues
            //DisplayQuestion(args.Clone());
            //DisplayOptions(args.Clone());
            
            var temp = args.Clone();
            temp.ClickedEventHandler = new EventHandler((sender, e) => ButtonClicked(sender, e));
            temp.ViewType = ViewType.ImageButton;
            temp.TransformLayoutBounds($"0,{temp.ImageHeight},0,{-temp.ImageHeight + temp.ImageHeight / 3}");
            OptionsDisplay.Display(parentLayout, temp);

            var temp2 = args.Clone();
            temp.TransformLayoutBounds($"0,0,0,{-temp.ImageHeight + temp.ImageHeight / 3}");
            QuestionPrompt.Display(parentLayout, temp2);
        }
        public void ButtonClicked(Object sender, EventArgs e)
        {
            // add doc later
            if (sender is GroupOfDisplayables group1 && CorrectAnswer is GroupOfDisplayables group2)
            {
                if (group1.Compare(group2)){
                    questionHandler?.Invoke(this,new QuestionEventArgs(true));
                }
                else
                {
                    questionHandler?.Invoke(this, new QuestionEventArgs(false));
                }
            }
        }
        private void DisplayQuestion(DisplayableArgs? args,double heightOfQuestion = -1)
        {

            if (heightOfQuestion < 0) // Sets default to 1/6 of the total Height 
            {
                heightOfQuestion = Args.ImageHeight / 6;
            }
            if (args == null)
            {
                args = new DisplayableArgs();
            }
            // Sets the height of the questions, does through absolute-layout
            // TransformLayoutBounds to be reworked in future to make it less complicated to use to just set one value while not changing the rest
            args.TransformLayoutBounds($"0,0,0,{-Args.ImageHeight + heightOfQuestion}");
            QuestionPrompt.Display(AbsLayout, args);
        }
        private void DisplayOptions(DisplayableArgs? args,double optionHeight = -1)
        {
            if (optionHeight < 0) // sets the default height to 1/3 of the total question's height
            {
                optionHeight = (Args.ImageHeight) / 3;
            }
            if (args == null)
            {
                args =new DisplayableArgs();
            }
            args.ClickedEventHandler = new EventHandler((sender,e)=>ButtonClicked(sender,e));
            args.ViewType = ViewType.ImageButton;
            args.TransformLayoutBounds($"0,{2*optionHeight},0,{-Args.ImageHeight+optionHeight}");
            args.TransformLayoutBounds($"0,{0},0,{-Args.ImageHeight + 1}");
            OptionsDisplay.SpacingBetweenDisplayables = 0.05;
            OptionsDisplay.Display(AbsLayout, args);
        }

        public override bool Compare(Displayable d)
        {
            throw new NotImplementedException();
        }
    }
}
