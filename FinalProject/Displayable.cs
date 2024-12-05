using Microsoft.Maui.Controls;
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
        public virtual string Text { get; set; } = string.Empty;

        public virtual void Display(
            Layout parentLayout,
            // bunch of default parameters
            double imageHeight = 200,
            double imageWidth = 200,
            LayoutOptions? horizontalOptions = null,
            LayoutOptions? verticalOptions = null,
            int textFontSize=18
        )
        {
            // assign value ONLY if already null, used to correctly implement default parameter
            horizontalOptions ??= LayoutOptions.Center;
            verticalOptions ??= LayoutOptions.Center;

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(20),
                Spacing = 15
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

                stackLayout.Children.Add(image);
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

            // add the StackLayout to the parent layout
            parentLayout.Children.Add(stackLayout);
        }
    }
}
