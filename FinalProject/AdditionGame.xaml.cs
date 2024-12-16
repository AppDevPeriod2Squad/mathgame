using FinalProject.QuestionGeneratorStuff;
using System.Reflection.Emit;
using Label = Microsoft.Maui.Controls.Label;

namespace FinalProject;

public partial class AdditionGame : ContentPage
{
    private QuestionGenerator generator;
    private QuestionAndAnswers question;
    public AdditionGame()
	{
		InitializeComponent();
        QuestionSetup();
	}
    public void QuestionSetup()
    {
        generator = new QuestionGenerator(new EventHandler((sender, e) => QuestionClicked(sender, e)));
        // probably move this stuff into generator constructor
        question = generator.Generate(QuestionSuperType.Addition, potentialTypes: new List<ImageType>() { ImageType.Dice });
        question.Display(mainLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1000,1000"));
    }
    public void QuestionClicked(object sender, EventArgs e)
    {
        question = generator.Generate(QuestionSuperType.Addition, potentialTypes: new List<ImageType>() { ImageType.Dice });
        question.Display(mainLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1000,1000"));
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