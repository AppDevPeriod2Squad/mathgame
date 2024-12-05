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

            // add an Image control if the ImageSource is not null
            if (ImageSource != null)
            {
                var image = new Image
                {
                    Source = ImageSource,
                    Aspect = Aspect.AspectFit, 
                    HeightRequest = 200, // set desired height, NEED TO ADD FUNCTIONALITY TO ALLOW CHANGES TO THIS
                    WidthRequest = 200,  // set desired width, NEED TO ADD FUNCTIONALITY TO ALLOW CHANGES TO THIS
                    HorizontalOptions = LayoutOptions.Center, // Center horizontally, NEED TO ADD FUNCTIONALITY TO ALLOW CHANGES TO THIS
                    VerticalOptions = LayoutOptions.Center   // Center vertically, NEED TO ADD FUNCTIONALITY TO ALLOW CHANGES TO THIS
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
                    FontSize = 18
                });
            }

            parentLayout.Children.Add(stackLayout);
        }
    }
}
