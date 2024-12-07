﻿
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
        public override void Display(Layout parentLayout,DisplayableArgs? args=null)
        {
            base.Display(parentLayout,args);
            DisplayOptions(parentLayout);
        }
        public void ButtonClicked(Object sender, EventArgs e)
        {
            // add functionality later
        }
        public void DisplayOptions(Layout parentLayout,double optionHeight = -1)
        {
            // Calculates the widths for each individual answer and setup
            if (optionHeight < 0) // sets the default height to 1/3 of the total question's height
            {
                optionHeight = (MauiSource.HeightRequest - MauiSource.Padding.VerticalThickness) / 3;
            }
            OptionsLayout = new HorizontalStackLayout();
            double totalOptionsWidth = MauiSource.WidthRequest - MauiSource.Padding.HorizontalThickness;
            double optionWidth = (totalOptionsWidth - SpacingBetweenQuestions * (OptionsList.Count-1)) / OptionsList.Count;
            OptionsLayout.Spacing = SpacingBetweenQuestions;

            // Creates the Displayable but doesn't put in on screen to avoid errors
            foreach (var choice in OptionsList)
            {
                choice.Display(parentLayout, new DisplayableArgs(
                    imageHeight: optionHeight,
                    imageWidth: optionWidth,
                    addToParentLayout: false,
                    viewType: ViewType.ImageButton,
                    clickedEventHandler:new EventHandler((sender, e) => ButtonClicked(this, new EventArgs()))
                    )
                    );
                OptionsLayout.Add(choice.MauiSource);
            }

            // Creates an intermediate AbsoluteLayout to correctly position the VerticalStackLayout 
            AbsoluteLayout intermediaryLayout = new AbsoluteLayout();
            AbsoluteLayout.SetLayoutBounds(OptionsLayout, new Rect(0, optionHeight*2, totalOptionsWidth, optionHeight));
            AbsoluteLayout.SetLayoutFlags(OptionsLayout, AbsoluteLayoutFlags.None);
            intermediaryLayout.Children.Add(OptionsLayout);
            MauiSource.Add(intermediaryLayout);
            
        }

    }
}
