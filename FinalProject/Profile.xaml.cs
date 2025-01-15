namespace FinalProject;

public partial class Profile : ContentPage
{
    private Database db;
    private User user;

    public Profile(Database database)
    {
        InitializeComponent();
        db = database; 
        LoadProfile(); 
    }

    private async void LoadProfile()
    {
        try
        {
            user = await db.GetUserAsync(); 
            if (user == null)
            {
                await DisplayAlert("Error", "Failed to load user data.", "OK");
                return;
            }

            
            profileName.Text = user.Name;
            profilePicture.Source = user.Picture;
            profileXP.Text = $"{user.XP}\nXP";
            profileGames.Text = $"{user.GamesCompleted}\nGames";
            profileQuarters.Text = user.Quarters.ToString();
            profileDimes.Text = user.Dimes.ToString();
            profileNickels.Text = user.Nickels.ToString();
            profilePennies.Text = user.Pennies.ToString();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"An error occurred while loading the profile: {ex.Message}", "OK");
        }
    }
}
