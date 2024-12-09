
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class GroupOfDisplayables : Displayable
    {
        public List<Displayable>? DisplayableGroup { get; set; }
        public HorizontalStackLayout? Layout { get; set; }
        public int SpacingBetweenDisplayables { get; set; }
        public GroupOfDisplayables(List<Displayable>? displayableGroup, int spacingBetweenDisplayables = 10)
        {
            DisplayableGroup = displayableGroup;
            SpacingBetweenDisplayables = spacingBetweenDisplayables;
        }

        public override void Display(Layout parentLayout, DisplayableArgs? args = null)
        {
            base.Display(parentLayout, args);
            // find better way to do this spacing later
            Layout = new HorizontalStackLayout() { Spacing = args.Spacing};
            // -----
            MauiSource.HorizontalOptions = LayoutOptions.Start;
            double totalOptionsWidth = MauiSource.WidthRequest - MauiSource.Padding.HorizontalThickness;
            double optionWidth = (totalOptionsWidth - SpacingBetweenDisplayables * (DisplayableGroup.Count-1)) / DisplayableGroup.Count;
            Layout.Spacing = SpacingBetweenDisplayables;
            // Creates the Displayable but doesn't put in on screen to avoid errors
            foreach (var choice in DisplayableGroup)
            {
                choice.Display(parentLayout, new DisplayableArgs(
                    imageHeight: MauiSource.HeightRequest,
                    imageWidth: optionWidth,
                    addToParentLayout: false,
                    viewType: ViewType.ImageButton
                    //clickedEventHandler: new EventHandler((sender, e) => ButtonClicked(this, new EventArgs()))
                    )
                    );
                Layout.Add(choice.MauiSource);
            }
            MauiSource.Add(Layout);
        }
    }
}
