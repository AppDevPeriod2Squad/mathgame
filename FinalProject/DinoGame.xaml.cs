using FinalProject.QuestionGeneratorStuff;

namespace FinalProject;

public partial class DinoGame : ContentPage // Probably make a GamePage class in the future that this extends, not needed for now.
{
	private QuestionGenerator generator;
	private QuestionAndAnswers question;
	public DinoGame()
	{
		InitializeComponent();
		QuestionSetup();
	}
	public void QuestionSetup()
	{
		generator = new QuestionGenerator(new EventHandler((sender, e) => QuestionClicked(sender, e)), potentialAnswerTypes: new List<ImageType>() { ImageType.Dice, ImageType.Food });
		// probably move this stuff into generator constructor
		question = generator.Generate(QuestionSuperType.FindGreatest);
        question.Display(mainLayout, new DisplayableArgs(absoluteLayoutBounds: $"0,0,{mainLayout.WidthRequest},{mainLayout.HeightRequest}"));
    }
	public void QuestionClicked(object sender, EventArgs e)
	{
		question = generator.Generate(QuestionSuperType.FindGreatest);
		question.Display(mainLayout, new DisplayableArgs(absoluteLayoutBounds: $"0,0,{mainLayout.WidthRequest},{mainLayout.HeightRequest}"));
		if (e is QuestionEventArgs args)
		{
			if (args.WasCorrect)
			{
				mainLayout.Add(new Label() { Text = "You were correct !" });
			}
			else
            {
                mainLayout.Add(new Label() { Text = "You were wrong :(" });
            }
        }
    }
}