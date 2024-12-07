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
            number.Display(ParentLayout);
            number2.Display(ParentLayout);

            var numberOptions = new List<Displayable> { new Number(val: 1, imageType: ImageType.Dice), new Number(val: 5, imageType: ImageType.Dice), new Number(val: 4, imageType: ImageType.Dice) };
            var question = new QuestionAndAnswers(numberOptions);
            question.Display(ParentLayout,imageHeight:300,imageWidth:300);
            question.MauiSource.BackgroundColor = Color.FromRgb(100, 0, 0);
        }
    }
}
