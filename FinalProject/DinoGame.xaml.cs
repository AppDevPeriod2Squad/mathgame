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
}
