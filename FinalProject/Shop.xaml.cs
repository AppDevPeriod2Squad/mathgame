
using Microsoft.Maui.Animations;

namespace FinalProject;

public partial class Shop : ContentPage
{
    int quarters = 0;
    int dimes = 0;
    int nickels = 0;
    int pennies = 0;

    int needed = 0;

    Database db;
    User user;
	public Shop(Database database)
	{
		InitializeComponent();
        db = database;
        Load();
	}

    private async void Load()
    {
        user = await db.GetUserAsync();
        needed = user.ChangeNeeded;
        PayAmount.Text = $"Cost: {needed} cents";

    }

    private async void QuarterAdd(object sender, EventArgs e)
    {
        quarters++;
        if (quarters > user.Quarters)
        {
            quarters = 0;
        }
        UpdateButtons();
    }

    private async void DimeAdd(object sender, EventArgs e)
    {
        dimes++;
        if (dimes > user.Dimes)
        {
            dimes = 0;
        }
        UpdateButtons();
    }

    private async void NickelAdd(object sender, EventArgs e)
    {
        nickels++;
        if (nickels > user.Nickels)
        {
            nickels = 0;
        }
        UpdateButtons();
    }

    private async void PennyAdd(object sender, EventArgs e)
    {
        pennies++;
        if (pennies > user.Pennies)
        {
            pennies = 0;
        }
        UpdateButtons();
    }
    
    private void UpdateButtons()
    {
        quarter.Text = quarters.ToString();
        dime.Text = dimes.ToString();
        nickel.Text = nickels.ToString();
        penny.Text = pennies.ToString();
        if (25 * quarters + 10 * dimes + 5 * nickels + pennies == needed)
        {
            Pay.IsEnabled = true;
        } else
        {
            Pay.IsEnabled = false;

        }
    }

    private async void Buy(object sender, EventArgs e)
    {
        user.Quarters -= quarters;
        user.Dimes -= dimes;
        user.Nickels -= nickels;
        user.Pennies -= pennies;
        user.ChangeNeeded = new Random().Next(50, 100);
        needed = user.ChangeNeeded;
        quarters = 0;
        dimes = 0;
        nickels = 0;
        pennies = 0;
        PayAmount.Text = $"Cost: {needed} cents";
        UpdateButtons();
    }

}