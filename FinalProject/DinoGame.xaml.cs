using FinalProject.QuestionGeneratorStuff;
using Microsoft.Maui.Layouts;

namespace FinalProject;

public partial class DinoGame : GamePage
{
    private double initialX;
    private double initialY;

    public DinoGame()
    {
        InitializeComponent();

        AddTouchGestureToDino();
    }

    private void AddTouchGestureToDino()
    {
        var panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;

        DinoImage.GestureRecognizers.Add(panGesture);
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if (sender is Image dino)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    initialX = dino.TranslationX;
                    initialY = dino.TranslationY;
                    break;

                case GestureStatus.Running:
                    dino.TranslationX = initialX + e.TotalX;
                    dino.TranslationY = initialY + e.TotalY;
                    break;

                case GestureStatus.Completed:
                    initialX = dino.TranslationX;
                    initialY = dino.TranslationY;
                    break;
            }
        }
    }
    
    //   public void QuestionSetup()
    //{
    //	generator = new QuestionGenerator(new EventHandler((sender, e) => QuestionClicked(sender, e)), potentialAnswerTypes: new List<ImageType>() { ImageType.Dice, ImageType.Food });
    //	// probably move this stuff into generator constructor
    //	question = generator.Generate(QuestionSuperType.FindGreatest);
    //       question.Display(mainLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1000,1000"));
    //   }
    //public void QuestionClicked(object sender, EventArgs e)
    //{
    //	question = generator.Generate(QuestionSuperType.FindGreatest);
    //	question.Display(mainLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1000,1000"));
    //	if (e is QuestionEventArgs args)
    //	{
    //		if (args.WasCorrect)
    //		{
    //			mainLayout.Add(new Label() { Text = "You were correct !" });
    //		}
    //		else
    //           {
    //               mainLayout.Add(new Label() { Text = "You were wrong :(" });
    //           }
    //       }
    //}
}