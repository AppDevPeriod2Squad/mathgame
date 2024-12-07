
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
        public QuestionAndAnswers(List<Displayable> choices)
        {
            OptionsList = choices;
        }
        public override void Display(Layout parentLayout, int padding = 10, int spacing = 5, double imageHeight = 200, double imageWidth = 200, StackOrientation stackLayoutOrientation = StackOrientation.Vertical, LayoutOptions? horizontalOptions = null, LayoutOptions? verticalOptions = null, int textFontSize = 18, string? absoluteLayoutBounds = null, AbsoluteLayoutFlags absoluteLayoutFlags = AbsoluteLayoutFlags.None, bool addToParentLayout = true)
        {
            base.Display(parentLayout, padding, spacing, imageHeight, imageWidth, stackLayoutOrientation, horizontalOptions, verticalOptions, textFontSize, absoluteLayoutBounds, absoluteLayoutFlags, addToParentLayout);
            DisplayOptions(parentLayout);
        }
        public void DisplayOptions(Layout parentLayout,int optionHeight = 100, int optionWidth = 100)
        {
            OptionsLayout = new HorizontalStackLayout();
            foreach (var choice in OptionsList)
            {
                choice.Display(parentLayout,imageHeight:optionHeight,imageWidth:optionWidth,addToParentLayout:false);
                OptionsLayout.Add(choice.MauiSource);
            }
            parentLayout.Add(OptionsLayout);
        }

    }
}
