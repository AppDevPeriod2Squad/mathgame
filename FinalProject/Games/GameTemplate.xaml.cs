using FinalProject.QuestionGeneratorStuff;
using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;
using System.Reflection.Emit;
using Label = Microsoft.Maui.Controls.Label;

namespace FinalProject;

public partial class GameTemplate : ContentPage
{
    public QuestionGenerator generator;
    public QuestionAndAnswers question;
    public QuestionSuperType questionType;
    public AbsoluteLayout displayLayout;
    protected Database database;
    User user;
    public GameTemplate()
	{
		InitializeComponent();
    }

    public async void GetUser()
    {
        user = await database.GetUserAsync();

    }

    public void QuestionSetup(AbsoluteLayout layout)
    {
        
        displayLayout = layout;
        //generator = new QuestionGenerator(new EventHandler((sender, e) => QuestionClicked(sender, e)), potentialAnswerTypes: new List<ImageType>() { ImageType.Dice });
        // probably move this stuff into generator constructor
        question = generator.Generate(questionType);
        question.Display(displayLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1,1", absoluteLayoutFlags: AbsoluteLayoutFlags.All));
        //Number nTest1 = new Number(val: 1, imageType: ImageType.Dice);
        //Number nTest2 = new Number(val: 1, imageType: ImageType.Dice);
        //Number nTest3 = new Number(val: 1, imageType: ImageType.Dice);
        //Number nTest4 = new Number(val: 1, imageType: ImageType.Dice);
        //GroupOfDisplayables gTest = new GroupOfDisplayables(new List<Displayable>() { nTest1, nTest2 });
        //GroupOfDisplayables gTest2 = new GroupOfDisplayables(new List<Displayable>() { nTest3, nTest4});
        //GroupOfDisplayables ggTest1 = new GroupOfDisplayables(new List<Displayable>() { gTest, gTest2 });
        //GroupOfDisplayables gggTest2 = new GroupOfDisplayables(new List<Displayable>() { ggTest1, ggTest1 });

        //gggTest2.Display(displayLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,.5,.5", absoluteLayoutFlags: AbsoluteLayoutFlags.All));
        //nTest1.Display(displayLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,.5,",absoluteLayoutFlags:AbsoluteLayoutFlags.All));
    }
    public void QuestionClicked(object sender, EventArgs e)
    {
        Rectangle r = new Rectangle() { Background=Color.FromRgb(255,255,255)};
        displayLayout.Add(r);
        AbsoluteLayout.SetLayoutBounds(r, new Rect(0, 0, 1, 1));
        AbsoluteLayout.SetLayoutFlags(r, AbsoluteLayoutFlags.All);
        
        question = generator.Generate(questionType);
        question.Display(displayLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1,1", absoluteLayoutFlags: AbsoluteLayoutFlags.All));
        if (e is QuestionEventArgs args)
        {
            if (args.WasCorrect)
            {
                displayLayout.Add(new Label() { Text = "You were correct !", TextColor = Color.FromRgb(0, 100, 30), FontSize=100 });
                int n = new Random().Next(10);
                if (n < 4)
                {
                    user.Pennies += new Random().Next(1, 10);
                }
                else if (n < 7) {
                    user.Nickels += new Random().Next(1, 7);
                }
                else if (n < 9)
                {
                    user.Dimes += new Random().Next(1, 5);
                } else
                {
                    user.Quarters += new Random().Next(1, 3);
                }
                database.UpdateExistingUserAsync(user);
               
            }
            else
            {
                displayLayout.Add(new Label() { Text = "You were wrong :(", TextColor = Color.FromRgb(100,30,0), FontSize = 100 });
            }
        }
    }
    }