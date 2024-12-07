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
        public virtual string? Text { get; set; } = string.Empty;
        public Layout MauiSource { get; set; } = new StackLayout();

        public virtual void Display(
           // default parameters
           Layout parentLayout,
           int padding=10,
           int spacing=5,
           double imageHeight = 200,
           double imageWidth = 200,
           StackOrientation stackLayoutOrientation=StackOrientation.Vertical,
           LayoutOptions? horizontalOptions = null,
           LayoutOptions? verticalOptions = null,
           int textFontSize = 18,
           string? absoluteLayoutBounds = null,
           AbsoluteLayoutFlags absoluteLayoutFlags = AbsoluteLayoutFlags.None,
           Boolean addToParentLayout = true,
           ViewType viewType = ViewType.Image, // image by default
           EventHandler onClickEvent = null
       )
        {
            // assign value ONLY if already null, used to correctly implement default parameter
            horizontalOptions ??= LayoutOptions.Center;
            verticalOptions ??= LayoutOptions.Center;

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(padding),
                Spacing = spacing,
                Orientation=stackLayoutOrientation,
                HeightRequest=imageHeight,
                WidthRequest=imageWidth
            };

            // add an Image control if the ImageSource is not null
            if (ImageSource != null)
            {

                // adds the certain View to stackLayout depending on what viewType
                switch (viewType)
                {
                    case ViewType.Image:
                        var image = new Image
                        {
                            Source = ImageSource,
                            Aspect = Aspect.AspectFit,
                            HeightRequest = imageHeight,
                            WidthRequest = imageWidth,
                            HorizontalOptions = horizontalOptions.Value,
                            VerticalOptions = verticalOptions.Value
                        };
                        stackLayout.Children.Add(image);
                        break;
                    case ViewType.ImageButton:
                        var button = new ImageButton
                        {
                            HeightRequest = imageHeight,
                            WidthRequest = imageWidth,
                            HorizontalOptions = horizontalOptions.Value,
                            VerticalOptions = verticalOptions.Value,
                            Source=ImageSource,
                            Aspect=Aspect.AspectFit,   
                        };
                        button.Clicked += onClickEvent;
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
                    FontSize = textFontSize
                });
            }
            MauiSource = stackLayout; // update Displayable field to reflect stackLayout code
            // checks if wants to actually add the Layout to parent layout 
            if (!addToParentLayout)
            {
                return;
            }
            // checks if parentlayout is absolutelayout, and if it is will assign new variable "absoluteLayout" to parentlayout
            if (parentLayout is AbsoluteLayout absoluteLayout && !string.IsNullOrWhiteSpace(absoluteLayoutBounds))
            {
                var bounds = ParseBounds(absoluteLayoutBounds);
                AbsoluteLayout.SetLayoutBounds(stackLayout, bounds);
                AbsoluteLayout.SetLayoutFlags(stackLayout, absoluteLayoutFlags);
                absoluteLayout.Children.Add(stackLayout);
            }
            else
            {
                parentLayout.Children.Add(stackLayout);
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
