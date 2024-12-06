using Microsoft.Maui.Controls;
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
        public Layout StackLayout { get; set; }

        public virtual void Display(
            // default parameters
           Layout parentLayout,
           int padding=20,
           int spacing=15,
           double imageHeight = 200,
           double imageWidth = 200,
           LayoutOptions? horizontalOptions = null,
           LayoutOptions? verticalOptions = null,
           int textFontSize = 18,
           string? absoluteLayoutBounds = null,
           AbsoluteLayoutFlags absoluteLayoutFlags = AbsoluteLayoutFlags.None
       )
        {
            // assign value ONLY if already null, used to correctly implement default parameter
            horizontalOptions ??= LayoutOptions.Center;
            verticalOptions ??= LayoutOptions.Center;

            StackLayout = new StackLayout
            {
                Padding = new Thickness(padding),
                Spacing = spacing
            };

            // add an Image control if the ImageSource is not null
            if (ImageSource != null)
            {
                var image = new Image
                {
                    Source = ImageSource,
                    Aspect = Aspect.AspectFit,
                    HeightRequest = imageHeight,
                    WidthRequest = imageWidth,
                    HorizontalOptions = horizontalOptions.Value,
                    VerticalOptions = verticalOptions.Value
                };

                StackLayout.Children.Add(image);
            }
            // add a Label control if the Text is not null or empty
            if (!string.IsNullOrWhiteSpace(Text))
            {
                StackLayout.Children.Add(new Label
                {
                    Text = Text,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = textFontSize
                });
            }

            // checks if parentlayout is absolutelayout, and if it is will assign new variable "absoluteLayout" to parentlayout
            if (parentLayout is AbsoluteLayout absoluteLayout && !string.IsNullOrWhiteSpace(absoluteLayoutBounds))
            {
                var bounds = ParseBounds(absoluteLayoutBounds);
                AbsoluteLayout.SetLayoutBounds(StackLayout, bounds);
                AbsoluteLayout.SetLayoutFlags(StackLayout, absoluteLayoutFlags);
                absoluteLayout.Children.Add(StackLayout);
            }
            else
            {
                parentLayout.Children.Add(StackLayout);
            }
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
