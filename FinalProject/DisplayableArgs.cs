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
        public Boolean AddToParentLayout {  get; set; }
        public ViewType ViewType { get; set; }
        public EventHandler? ClickedEventHandler { get; set; }
        public DisplayableArgs(int padding = 10,
           int spacing = 5,
           double imageHeight = 200,
           double imageWidth = 200,
           StackOrientation stackLayoutOrientation = StackOrientation.Vertical,
           LayoutOptions? horizontalOptions = null,
           LayoutOptions? verticalOptions = null,
           int textFontSize = 18,
           string? absoluteLayoutBounds = null,
           AbsoluteLayoutFlags absoluteLayoutFlags = AbsoluteLayoutFlags.None,
           Boolean addToParentLayout = true,
           ViewType viewType = ViewType.Image, // image by default
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
            AddToParentLayout = addToParentLayout;
            ViewType = viewType;
            ClickedEventHandler = clickedEventHandler;
        }
    }
}
