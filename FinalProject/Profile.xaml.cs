using FinalProject.Gacha;
using System.ComponentModel;

namespace FinalProject;

public partial class Profile : ContentPage
{
    private User user;
    private Database db;
    private IDispatcherTimer _timer;

    public Profile(Database db)
    {
        this.db = db;
        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1); // Set interval
        _timer.Tick += Timer_Tick; // Attach the event
        _timer.Start(); // Start the timer

        InitializeComponent();
        Setup();
        container.Add(new NavElement(container, db));
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
        // Function to run periodically
        Setup();
    }

    public async void Setup()
    {
        user = await db.GetUserAsync();
        profileName.Text = user.Name;
        profilePicture.Source = Translator.pfpLinks[user.Picture];
        contentP.BackgroundImageSource = Translator.backgroundLinks[user.Background];
        profileXP.Text = $"{user.XP}\nXP";
        profileGames.Text = $"{user.GamesCompleted}\nGames";
        profileQuarters.Text = user.Quarters.ToString();
        profileDimes.Text = user.Dimes.ToString();
        profileNickels.Text = user.Nickels.ToString();
        profilePennies.Text = user.Pennies.ToString();
    }

    private async void EditProfile(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ItemSelect(db, true));
    }

    private async void EditBackground(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ItemSelect(db, false));
    }


}