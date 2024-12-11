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
            //number.Display(ParentLayout);
            //number2.Display(ParentLayout);

            var numberOptions = new List<Displayable> { new Number(val: 1, imageType: ImageType.Dice), new Number(val: 5, imageType: ImageType.Dice), new Number(val: 4, imageType: ImageType.Dice), new Number(val: 4, imageType: ImageType.Dice) };
            var numbers = new List<Displayable>() { new Number(val: 1, imageType: ImageType.Dice), new Symbol(symbolType:SymbolType.Plus),new Number(val: 3, imageType: ImageType.Dice), new Symbol(symbolType: SymbolType.Plus), new Number(val: 6, imageType: ImageType.Dice) };
            var numbersGroups = new List<Displayable>() { new GroupOfDisplayables(numbers), new GroupOfDisplayables(numbers) , new GroupOfDisplayables(numbers) };
            var test = new List<Displayable>() { new GroupOfDisplayables(numbersGroups),new GroupOfDisplayables(numbersGroups) };
            var question = new QuestionAndAnswers(numberOptions,new Prompt(" test"));
            var question2 = new QuestionAndAnswers(test, new Prompt(" test"));
            //question.Display(ParentLayout, new DisplayableArgs(imageHeight: 300, imageWidth: 1000));
            question.Display(ParentLayout, new DisplayableArgs(imageHeight: 500, imageWidth:1500, spacing: 30,absoluteLayoutBounds:"0,0,1500,500"));
            question.MauiSource.BackgroundColor = Color.FromRgb(0, 200, 0);
            //question.MauiSource.BackgroundColor = Color.FromRgb(100, 0, 0);
        }
    }
}
