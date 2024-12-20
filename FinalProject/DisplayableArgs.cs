using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class DisplayableArgs
    {
        public int Padding { get; set; }
        public int Spacing {  get; set; }
        public double ImageHeight {  get; set; }
        public double ImageWidth { get; set; }
        public StackOrientation StackLayoutOrientation { get; set; }
        public LayoutOptions? HorizontalOptions {  get; set; }
        public LayoutOptions? VerticalOptions {  get; set; }
        public int TextFontSize {  get; set; }
        public string? AbsoluteLayoutBounds {  get; set; }
        public AbsoluteLayoutFlags AbsoluteLayoutFlags { get; set; }
        public ViewType ViewType { get; set; }
        public EventHandler? ClickedEventHandler { get; set; }
        public String? Text { get; set; }
        public DisplayableArgs(int padding = 10,
           int spacing = 5,
           double imageHeight = 200,
           double imageWidth = 200,
           StackOrientation stackLayoutOrientation = StackOrientation.Vertical,
           LayoutOptions? horizontalOptions = null,
           LayoutOptions? verticalOptions = null,
           int textFontSize = 30,
           string? absoluteLayoutBounds = null,
           AbsoluteLayoutFlags absoluteLayoutFlags = AbsoluteLayoutFlags.None,
           ViewType viewType = ViewType.Image, // image by default
           String text = "",
           EventHandler? clickedEventHandler = null)
        {
            Padding = padding;
            Spacing = spacing;
            ImageHeight = imageHeight;
            ImageWidth = imageWidth;
            StackLayoutOrientation = stackLayoutOrientation;
            HorizontalOptions = horizontalOptions;
            VerticalOptions = verticalOptions;
            TextFontSize = textFontSize;
            AbsoluteLayoutBounds = absoluteLayoutBounds;
            AbsoluteLayoutFlags = absoluteLayoutFlags;
            ViewType = viewType;
            ClickedEventHandler = clickedEventHandler;
            Text = text;
        }
        public DisplayableArgs Clone()
        {
            return (DisplayableArgs)MemberwiseClone();
        }
        public void TransformLayoutBounds(String transforms)
        {
            var parts = transforms.Split(',');
            var parts2 = AbsoluteLayoutBounds.Split(',');
            if (parts.Length != 4 || parts2.Length != 4) {
                throw new ArgumentException("absoluteLayoutBounds must be a comma-separated string with four values.");
            }
            AbsoluteLayoutBounds = $"{double.Parse(parts[0]) + double.Parse(parts2[0])}," +
                $"{double.Parse(parts[1]) + double.Parse(parts2[1])}," +
                $"{double.Parse(parts[2]) + double.Parse(parts2[2])}" +
                $",{double.Parse(parts[3]) + double.Parse(parts2[3])}";
        }
    }
}
