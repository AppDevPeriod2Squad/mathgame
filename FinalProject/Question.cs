
using Microsoft.Maui.Layouts;
using System;

namespace FinalProject
{
    public class QuestionAndAnswers : Displayable
    {
        public List<Number> AnswerOptions { get; set; }
        public Displayable Question { get; set; }
        public List<Displayable> OptionsList { get; set; }
        public HorizontalStackLayout OptionsLayout { get; set; }
        public int SpacingBetweenQuestions { get; set; }
        public QuestionAndAnswers(List<Displayable> choices, int spacingBetweenQuestions = 30)
        {
            OptionsList = choices;
            SpacingBetweenQuestions = spacingBetweenQuestions;
        }
        public override void Display(Layout parentLayout, int padding = 10, int spacing = 5, double imageHeight = 200, double imageWidth = 200, StackOrientation stackLayoutOrientation = StackOrientation.Vertical, LayoutOptions? horizontalOptions = null, LayoutOptions? verticalOptions = null, int textFontSize = 18, string? absoluteLayoutBounds = null, AbsoluteLayoutFlags absoluteLayoutFlags = AbsoluteLayoutFlags.None, bool addToParentLayout = true)
        {
            base.Display(parentLayout, padding, spacing, imageHeight, imageWidth, stackLayoutOrientation, horizontalOptions, verticalOptions, textFontSize, absoluteLayoutBounds, absoluteLayoutFlags, addToParentLayout);
            DisplayOptions(parentLayout);
        }
        public void DisplayOptions(Layout parentLayout,double optionHeight = -1)
        {
            if (optionHeight < 0)
            {
                optionHeight = (MauiSource.HeightRequest - MauiSource.Padding.VerticalThickness) / 3;
            }
            OptionsLayout = new HorizontalStackLayout();
            double totalOptionsWidth = MauiSource.WidthRequest - MauiSource.Padding.HorizontalThickness;
            //- SpacingBetweenQuestions*OptionsList.Count
            double optionWidth = (totalOptionsWidth - SpacingBetweenQuestions * (OptionsList.Count-1)) / OptionsList.Count;
            OptionsLayout.Spacing = SpacingBetweenQuestions;
            //OptionsLayout.Margin = MarginOfQuestions;
            foreach (var choice in OptionsList)
            {
                choice.Display(parentLayout,imageHeight:optionHeight,imageWidth:optionWidth,addToParentLayout:false);
                OptionsLayout.Add(choice.MauiSource);
            }
            AbsoluteLayout intermediaryLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(OptionsLayout, new Rect(0, optionHeight*2, totalOptionsWidth, optionHeight));
            AbsoluteLayout.SetLayoutFlags(OptionsLayout, AbsoluteLayoutFlags.None);
            intermediaryLayout.Children.Add(OptionsLayout);
            MauiSource.Add(intermediaryLayout);
            
        }

    }
}
