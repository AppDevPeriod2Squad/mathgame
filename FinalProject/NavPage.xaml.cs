using FinalProject.Gacha;

namespace FinalProject;

public partial class NavPage : ContentPage
{
	Database db;
	public NavPage(Database db)
	{
		this.db = db;
		InitializeComponent();
		EggHuntGameButton.Source = """https://media.istockphoto.com/id/1300470309/vector/cute-cartoon-happy-egg-charactercter.jpg?s=612x612&w=0&k=20&c=j-ZL7fNqIbA_-J0V29LTy5u_jMKP2SqcmCr-dX2vLDw=""";
		ShopB.Source = "https://cbx-prod.b-cdn.net/COLOURBOX52791296.jpg?width=800&height=800&quality=70";
		Setup();


	}

	public async void Setup()
	{
		User user = await db.GetUserAsync();
		ProfileB.Source = Translator.pfpLinks[user.Picture];
	}

    private async void GetProfile(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Profile(db));
    }

    private async void GetShop(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Shop(db));
    }

    private async void PlayDino(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new DinoGame(db));
    }

    private async void PlayEgg(object sender, EventArgs e)
    {
       
    }
}