
namespace FinalProject;

public partial class Shop : ContentPage
{
    int quarters = 0;
    int dimes = 0;
    int nickels = 0;
    int pennies = 0;

    int needed = 69;
	public Shop()
	{
		InitializeComponent();
        PayAmount.Text = $"Cost: {needed} cents";
	}

    private async void QuarterAdd(object sender, EventArgs e)
    {
        quarters++;
        UpdateButtons();
    }

    private async void DimeAdd(object sender, EventArgs e)
    {
        dimes++;
        UpdateButtons();
    }

    private async void NickelAdd(object sender, EventArgs e)
    {
        nickels++;
        UpdateButtons();
    }

    private async void PennyAdd(object sender, EventArgs e)
    {
        pennies++;
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

}