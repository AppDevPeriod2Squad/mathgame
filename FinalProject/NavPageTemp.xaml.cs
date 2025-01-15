using System.Threading.Tasks.Dataflow;

namespace FinalProject;

public partial class NavPageTemp : ContentPage
{
    Database database;
	public NavPageTemp()
	{
		InitializeComponent();
	}

    private async void Addition(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AdditionGame());
    }
    private async void Greater(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DinoGame(database));
    }
    private async void Profile(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profile());
    }

    //private async void Shop(object sender, EventArgs args)
    //{
    //    await Navigation.PushAsync(new Shop(database));
    //}
    private async void DinoGame(object sender, EventArgs args)
    {
        await Navigation.PushAsync(new DinoGame(database));
    }
}