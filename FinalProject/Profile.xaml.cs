namespace FinalProject;

public partial class Profile : ContentPage
{
	private User user;
	public Profile(Database db)
	{
		InitializeComponent();
		Setup();
	}

	public void Setup()
	{
		user = new User() { Picture="dino_happy.png"};
		//temp
		profileName.Text = user.Name;
		profilePicture.Source = user.Picture;
		profileXP.Text = $"{user.XP}\nXP";
		profileGames.Text = $"{user.GamesCompleted}\nGames";
		profileQuarters.Text=user.Quarters.ToString();
		profileDimes.Text=user.Dimes.ToString();
		profileNickels.Text=user.Nickels.ToString();
		profilePennies.Text=user.Pennies.ToString();
	}
}