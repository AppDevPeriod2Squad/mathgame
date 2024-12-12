
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

            var numberOptions = new List<Displayable> { new Number(val: 1, imageType: ImageType.Dice), new Number(val: 5, imageType: ImageType.Dice), new Number(val: 4, imageType: ImageType.Dice) };
            var numbers = new List<Displayable>() { new Number(val: 1, imageType: ImageType.Dice), new Symbol(symbolType:SymbolType.Plus),new Number(val: 3, imageType: ImageType.Dice) };
            var numbersGroups = new List<Displayable>() { new GroupOfDisplayables(numbers), new GroupOfDisplayables(numbers) , new GroupOfDisplayables(numbers) };
            var questionPrompt = new Prompt("test");
            var question = new QuestionAndAnswers(numberOptions,questionPrompt);
            var question2 = new QuestionAndAnswers(numbersGroups, questionPrompt);
            //question.Display(ParentLayout, new DisplayableArgs(absoluteLayoutBounds: "300,300,600,300"));
            //number.Display(ParentLayout, new DisplayableArgs(absoluteLayoutBounds: "200,0,300,100"));
            //question.MauiSource.BackgroundColor = Color.FromRgb(100, 50, 100);
            question2.Display(ParentLayout, new DisplayableArgs(absoluteLayoutBounds: "800,300,800,300"));
            question.MauiSource.BackgroundColor = Color.FromRgb(100, 0, 0);
        }
    }
}
