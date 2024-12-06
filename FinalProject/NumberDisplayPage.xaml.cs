using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace FinalProject
{
    public partial class NumberDisplayPage : ContentPage
    {
        public NumberDisplayPage()
        {
            InitializeComponent();

            var number = new Number(val: 5, imageType: ImageType.TenFrames);
            var number2 = new Number(val: 2, imageType: ImageType.Dice);
            var question = new QuestionAndAnswers();
            number.Display(ParentLayout);
            number2.Display(ParentLayout);
            List<Displayable> choices = new List<Displayable>();
            choices.Add(new Number(val:5, imageType: ImageType.Dice));
            choices.Add(new Number(val: 2, imageType: ImageType.Dice));
            choices.Add(new Number(val: 1, imageType: ImageType.Dice));
            choices.Add(new Number(val: 5, imageType: ImageType.Dice));
            question.Display(ParentLayout,choices,absoluteLayoutBounds: "200,200,500,500");
            question.StackLayout.BackgroundColor = Color.FromRgb(100, 0, 0);

        }
    }
}
