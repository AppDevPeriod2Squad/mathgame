using Microsoft.Maui.Controls;
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
        public Layout MauiSource { get; set; } = new StackLayout();
        public AbsoluteLayout AbsLayout = new AbsoluteLayout();
        public DisplayableArgs Args { get; set; }
        public abstract Boolean Compare(Displayable d);
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
            args.Spacing = 0;
            args.Padding = 0;
            args.HorizontalOptions ??= LayoutOptions.Center;
            args.VerticalOptions ??= LayoutOptions.Center;


            Layout stackLayout;
            stackLayout = new StackLayout
            {
                Padding = new Thickness(args.Padding),
                Spacing = args.Spacing,
                Orientation = args.StackLayoutOrientation
            };
            //temp
            stackLayout.Padding = 0;
            stackLayout.Padding = 0;

            // checks if parentlayout is absolutelayout, and if it is will assign new variable "absoluteLayout" to parentlayout
            if (parentLayout is AbsoluteLayout absoluteLayout && !string.IsNullOrWhiteSpace(args.AbsoluteLayoutBounds))
            {
                var bounds = ParseBounds(args.AbsoluteLayoutBounds);
                AbsoluteLayout.SetLayoutBounds(stackLayout, bounds);
                AbsoluteLayout.SetLayoutFlags(stackLayout, args.AbsoluteLayoutFlags);
                //absoluteLayout.Children.Add(stackLayout);
                Random r = new Random();
                //stackLayout.Background = Color.FromRgb(r.Next(0,100), r.Next(0, 100), r.Next(0, 100));
                AbsLayout = absoluteLayout;
                args.ImageHeight = bounds.Height;
                args.ImageWidth = bounds.Width;

            }
            else
            {
                parentLayout.Children.Add(stackLayout);
            }





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
                            HeightRequest = -1,
                            WidthRequest = -1,
                            HorizontalOptions = args.HorizontalOptions.Value,
                            VerticalOptions = args.VerticalOptions.Value
                            
                        };
                        var bounds = ParseBounds(args.AbsoluteLayoutBounds);
                        AbsoluteLayout.SetLayoutBounds(image, bounds);
                        AbsoluteLayout.SetLayoutFlags(image, args.AbsoluteLayoutFlags);
                        if (parentLayout is AbsoluteLayout absoluteLayout2)
                        {
                            absoluteLayout2.Children.Add(image);
                        }
                        break;
                    case ViewType.ImageButton:
                        var button = new ImageButton
                        {
                            WidthRequest = -1,
                            HeightRequest = -1,
                            HorizontalOptions = args.HorizontalOptions.Value,
                            VerticalOptions = args.VerticalOptions.Value,
                            Source = ImageSource,
                            Aspect = Aspect.AspectFit,

                        };
                        
                        
                        button.Clicked += args.ClickedEventHandler;
                        var bounds2 = ParseBounds(args.AbsoluteLayoutBounds);
                        AbsoluteLayout.SetLayoutBounds(button, bounds2);
                        AbsoluteLayout.SetLayoutFlags(button, args.AbsoluteLayoutFlags);
                        if (parentLayout is AbsoluteLayout absoluteLayout3)
                        {
                            absoluteLayout3.Children.Add(button);
                        }


                        break;
                    case ViewType.None:
                        break;
                }
                
            }
            // add a Label control if the Text is not null or empty
            Label label = new Label(); 
            if (!string.IsNullOrWhiteSpace(args.Text))
            {
                label = new Label
                {
                    Text = args.Text,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromRgb(0, 0, 0),
                    FontSize = 40
                    //FontSize = args.TextFontSize,


                };
                var stack = new HorizontalStackLayout();
                stack.HorizontalOptions = LayoutOptions.Center;
                stack.VerticalOptions = LayoutOptions.Start;
                var bounds = ParseBounds(args.AbsoluteLayoutBounds);
                AbsoluteLayout.SetLayoutBounds(stack, bounds);
                AbsoluteLayout.SetLayoutFlags(stack, args.AbsoluteLayoutFlags);
                parentLayout.Children.Add(stack);
                stack.Add(label);
            }
            
            // checks if wants to actually add the Layout to parent layout 
            MauiSource = stackLayout; // update Displayable field to reflect stackLayout code
            Args = args;
        }

        protected Rect ParseBounds(string bounds)
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
