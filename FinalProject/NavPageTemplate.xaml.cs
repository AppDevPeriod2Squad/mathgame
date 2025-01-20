using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;

namespace FinalProject;

public partial class NavElement : AbsoluteLayout
{
	private Database db;
	public AbsoluteLayout ParentLayout {  get; set; }
	private Dictionary<String, Button> buttons;
	public NavElement(AbsoluteLayout c,Database db) : base() {
		this.db = db;
		ParentLayout = c;

		//container.WidthRequest = 

		if (ParentLayout != null)
        {
			Grid g = new Grid();
			ColumnDefinition def = new ColumnDefinition() { Width = GridLength.Star };

            g.ColumnDefinitions = new ColumnDefinitionCollection(new ColumnDefinition[] {def,def,def});
			Button shop = new Button() { Text = "shop" };
			shop.Clicked += buttonClicked;
            g.Add(shop, column: 0);
            Button profile = new Button() { Text = "profile" };
            profile.Clicked += buttonClicked;
            g.Add(profile, column: 1);
            Button games = new Button() { Text = "games" };
            games.Clicked += buttonClicked;
			g.Add(games,column:2);
            AbsoluteLayout.SetLayoutFlags(g, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(g, new Rect(0, 1, 1, .1));
            ParentLayout.Add(g);
			buttons = new Dictionary<String, Button>();
			buttons.Add("shop", shop);
			buttons.Add("games", games);
			buttons.Add("profile", profile);
        }
		
	}
    private async void buttonClicked(object sender, EventArgs e)
    {
		if (sender is Button button)
		{
			if (buttons["games"] == button)
			{
				await Navigation.PushAsync(new NavPageTemp(db));
			}
            if (buttons["profile"] == button)
            {
                await Navigation.PushAsync(new Profile(db));
            }
            if (buttons["shop"] == button)
            {
                await Navigation.PushAsync(new Shop(db));
            }

        }
	}
}