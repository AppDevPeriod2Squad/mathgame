namespace FinalProject;

public partial class Profile : ContentPage
{
	private User user;
	public Profile(Database db)
	{
		InitializeComponent();
		user = new User();
		user.Name = "Sigma Male";
		user.Picture = "https://www.newtraderu.com/wp-content/uploads/9-Secret-Strengths-Of-The-Sigma-MaleUnderstanding-The-Lone-Wolf-scaled.jpg";
		user.XP = 69;
		user.GamesCompleted = 42;
		user.Quarters = 2;
		user.Dimes = 3;
		user.Nickels = 4;
		user.Pennies = 5;
        Setup();
		container.Add(new NavElement(container, db));

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