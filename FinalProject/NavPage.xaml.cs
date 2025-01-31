namespace FinalProject;

public partial class NavPage : ContentPage
{
    Database database;
	public NavPage(Database db) 
	{
        InitializeComponent();
        database = db;
        container.Add(new NavElement(container, db));
    }

    private async void Dino(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DinoGame(database));
    }
    private async void Matching(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ConnectionsGamePage(database));
    }
    private async void Adding(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdditionGame(database));
    }
    private async void Greater(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new GreaterGame(database));
    }

    private async void Fish(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BeachGamePage(database));
    }
    private async void Multiply(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MultiplicationGame(database));
    }
    private async void Division(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DivisionGame(database));
    }
    private async void Subtraction(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SubtractionGame(database));
    }
}