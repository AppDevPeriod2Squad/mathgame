
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
        public void Display(Layout parentLayout,List<Displayable> choices,int padding = 20, int spacing = 15, double imageHeight = 200, double imageWidth = 200, LayoutOptions? horizontalOptions = null, LayoutOptions? verticalOptions = null, int textFontSize = 18, string? absoluteLayoutBounds = null, AbsoluteLayoutFlags absoluteLayoutFlags = AbsoluteLayoutFlags.None, Boolean addToParentLayout=true)
        {
            // maybe implement better way of doing Args in the future?? like a dictionary with the variable name key as smth idk
            // this just is super messy and long
            base.Display(parentLayout, padding, spacing, imageHeight, imageWidth, horizontalOptions, verticalOptions, textFontSize, absoluteLayoutBounds, absoluteLayoutFlags,addToParentLayout);
            DisplayOptions(parentLayout,choices);
        }
        public void DisplayOptions(Layout parentLayout,List<Displayable> choices,int optionHeight = 100, int optionWidth = 100)
        {
            OptionsList = choices;
            
            OptionsLayout = new HorizontalStackLayout();
            foreach (var choice in OptionsList)
            {
                choice.Display(parentLayout,imageHeight:optionHeight,imageWidth:optionWidth,addToParentLayout:false);
                OptionsLayout.Add(choice.StackLayout);
            }
            parentLayout.Add(OptionsLayout);
        }

    }
}
