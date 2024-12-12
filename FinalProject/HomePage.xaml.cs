namespace FinalProject;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

		// will implement adding game icons better once we actually make framework for making games, for now using displayable images
		var ListOfGames = new List<Displayable>(); // change type from displayable to game class or somethign in future
		var game1Displayable = new Displayable() { ImageSource = ImageSource.FromFile("FeedTheDino.png") };

	}
}