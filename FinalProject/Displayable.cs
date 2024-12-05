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

        public virtual void Display(Layout parentLayout)
        {
            var stackLayout = new StackLayout
            {
                Padding = new Thickness(20),
                Spacing = 15
            };

            // Add an Image control if the ImageSource is not null
            if (ImageSource != null)
            {
                var image = new Image
                {
                    Source = ImageSource,
                    Aspect = Aspect.AspectFit, // Maintain aspect ratio and fit within the available space
                    HeightRequest = 200, // Set desired height
                    WidthRequest = 200,  // Set desired width
                    HorizontalOptions = LayoutOptions.Center, // Center horizontally
                    VerticalOptions = LayoutOptions.Center   // Center vertically
                };

                stackLayout.Children.Add(image);
            }

            // Add a Label control if the Text is not null or empty
            if (!string.IsNullOrWhiteSpace(Text))
            {
                stackLayout.Children.Add(new Label
                {
                    Text = Text,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    FontSize = 18
                });
            }

            // Add the StackLayout to the parent layout
            parentLayout.Children.Add(stackLayout);
        }
    }
}
