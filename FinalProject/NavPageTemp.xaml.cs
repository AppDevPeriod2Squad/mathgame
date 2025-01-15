namespace FinalProject;

public partial class NavPageTemp : ContentPage
{
    Database db;
	public NavPageTemp(Database database)
	{
		InitializeComponent();
        db = database ?? throw new ArgumentNullException(nameof(database));
    }

    private async void Addition(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdditionGame());
    }
    private async void Greater(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DinoGame(db));
    }
    private async void Profile(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profile(db));
    }

    private async void Shop(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new Shop(db));
    }
    private async void DinoGame(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new DinoGame(db));
    }
}