using FinalProject.QuestionGeneratorStuff;
using System.Reflection.Emit;
using Label = Microsoft.Maui.Controls.Label;

namespace FinalProject;

public partial class AdditionGame : GameTemplate
{

    public AdditionGame(Database db)
	{
        database = db;
        GetUser();
		InitializeComponent();
        questionType = QuestionSuperType.Addition;
        generator = new QuestionGenerator(new EventHandler((sender, e) => QuestionClicked(sender, e)), potentialAnswerTypes: new List<ImageType>() { ImageType.Dice,ImageType.TenFrames});
        QuestionSetup(mainLayout);
    }
}