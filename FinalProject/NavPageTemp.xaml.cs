namespace FinalProject;

public partial class NavPageTemp : ContentPage
{
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
        await Navigation.PushAsync(new DinoGame());
    }
}