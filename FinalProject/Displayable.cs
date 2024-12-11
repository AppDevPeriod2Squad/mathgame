﻿using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Internals;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public abstract class Displayable
    {
        public virtual ImageSource? ImageSource { get; set; } = null;
        public virtual string? Text { get; set; } = string.Empty;
        public Layout MauiSource { get; set; } = new AbsoluteLayout(); // temp change from StackLayout
        public double AbsoluteWidth { get { return AbsoluteWidth_; } set { AbsoluteWidth_ = (MauiSource.WidthRequest + MauiSource.Padding.HorizontalThickness);} }
        private double AbsoluteWidth_;
        public virtual void Display(
           // default parameters
           Layout parentLayout,
           DisplayableArgs? args = null
       )
        {
            // if args is not set
            if (args == null)
            {
                args = new DisplayableArgs();
            }
            // added way to set Text property
            Text = args.Text;
            // assign value ONLY if already null, used to correctly implement default parameter
            args.HorizontalOptions ??= LayoutOptions.Center;
            args.VerticalOptions ??= LayoutOptions.Center;

            Layout stackLayout = new StackLayout
            {
                Padding = new Thickness(args.Padding),
                Spacing = args.Spacing,
                Orientation= args.StackLayoutOrientation,
                HeightRequest= args.ImageHeight,
                WidthRequest= args.ImageWidth
            };

            // add an Image control if the ImageSource is not null
            if (ImageSource != null)
            {

                // adds the certain View to stackLayout depending on what viewType
                switch (args.ViewType)
                {
                    case ViewType.Image:
                        var image = new Image
                        {
                            Source = ImageSource,
                            Aspect = Aspect.AspectFit,
                            HeightRequest = args.ImageHeight,
                            WidthRequest = args.ImageWidth,
                            HorizontalOptions = args.HorizontalOptions.Value,
                            VerticalOptions = args.VerticalOptions.Value
                        };
                        stackLayout.Children.Add(image);
                        break;
                    case ViewType.ImageButton:
                        var button = new ImageButton
                        {
                            HeightRequest = args.ImageHeight,
                            WidthRequest = args.ImageWidth,
                            HorizontalOptions = args.HorizontalOptions.Value,
                            VerticalOptions = args.VerticalOptions.Value,
                            Source=ImageSource,
                            Aspect=Aspect.AspectFit,   
                        };
                        button.Clicked += args.ClickedEventHandler;
                        stackLayout.Children.Add(button);
                        break;
                    case ViewType.None:
                        break;
                }
                
            }
            // add a Label control if the Text is not null or empty
            if (!string.IsNullOrWhiteSpace(Text))
            {
                stackLayout.Children.Add(new Label
                {
                    Text = Text,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = args.TextFontSize
                });
            }

            // checks if parentlayout is absolutelayout, and if it is will assign new variable "absoluteLayout" to parentlayout
            if (parentLayout is AbsoluteLayout absoluteLayout && !string.IsNullOrWhiteSpace(args.AbsoluteLayoutBounds))
            {
                var bounds = ParseBounds(args.AbsoluteLayoutBounds);
                AbsoluteLayout.SetLayoutBounds(stackLayout, bounds);
                AbsoluteLayout.SetLayoutFlags(stackLayout, args.AbsoluteLayoutFlags);
                absoluteLayout.Children.Add(stackLayout);
                // test code
            }
            else
            {
                parentLayout.Children.Add(stackLayout);
            }
            MauiSource = stackLayout;
            // update Displayable field to reflect stackLayout code
            AbsoluteWidth = MauiSource.WidthRequest + MauiSource.Padding.HorizontalThickness;
        }

        private Rect ParseBounds(string bounds)
        {
            var parts = bounds.Split(',');
            if (parts.Length != 4)
                throw new ArgumentException("absoluteLayoutBounds must be a comma-separated string with four values.");

            return new Rect(
                double.Parse(parts[0]),
                double.Parse(parts[1]),
                double.Parse(parts[2]),
                double.Parse(parts[3])
            );
        }
    }
}
