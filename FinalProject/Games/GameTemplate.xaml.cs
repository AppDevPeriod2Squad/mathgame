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
    public Boolean WasCorrect { get; set; } = false;
    public Boolean Answered { get; set; } = false;
    public Boolean AutoGenerate {  get; set; }
    public Boolean DoWhiteBackground { get; set; }
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

    public void QuestionSetup(AbsoluteLayout layout,Database db,Boolean doDisplay = true,Boolean doAuto = true,Boolean displayWhiteBackground = true)
    {
        database = db;
        DoWhiteBackground = displayWhiteBackground;
        displayLayout = layout;
        AutoGenerate = doAuto;
        GetUser();
        //generator = new QuestionGenerator(new EventHandler((sender, e) => QuestionClicked(sender, e)), potentialAnswerTypes: new List<ImageType>() { ImageType.Dice });
        // probably move this stuff into generator constructor
        question = generator.Generate(questionType);
        if (doDisplay)
        {
            question.Display(displayLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1,1", absoluteLayoutFlags: AbsoluteLayoutFlags.All));
        }
        
    }
    public void QuestionClicked(object sender, EventArgs e)
    {
        
        
        if (e is QuestionEventArgs args)
        {
            Answered = true;
            if (args.WasCorrect)
            {
                WasCorrect = true;
                if (DoWhiteBackground)
                {
                    Rectangle r = new Rectangle() { Background = Color.FromRgb(255, 255, 255) };
                    displayLayout.Add(r);
                    AbsoluteLayout.SetLayoutBounds(r, new Rect(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(r, AbsoluteLayoutFlags.All);
                    HorizontalStackLayout lc = new HorizontalStackLayout();
                    Label l = new Label() { Text = "You were correct !", TextColor = Color.FromRgb(0, 100, 30)};
                    AbsoluteLayout.SetLayoutBounds(lc, new Rect(0, 0, 1, .1));
                    AbsoluteLayout.SetLayoutFlags(lc, AbsoluteLayoutFlags.All);
                    lc.Add(l);
                    displayLayout.Add(lc);
                }

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
                WasCorrect = false;
                if (DoWhiteBackground)
                {
                    Rectangle r = new Rectangle() { Background = Color.FromRgb(255, 255, 255) };
                    displayLayout.Add(r);
                    AbsoluteLayout.SetLayoutBounds(r, new Rect(0, 0, 1, 1));
                    AbsoluteLayout.SetLayoutFlags(r, AbsoluteLayoutFlags.All);
                    HorizontalStackLayout lc = new HorizontalStackLayout();
                    Label l = new Label() { Text = "You were wrong :(", TextColor = Color.FromRgb(100, 30, 0)};
                    AbsoluteLayout.SetLayoutBounds(lc, new Rect(0, 0, 1, .1));
                    AbsoluteLayout.SetLayoutFlags(lc, AbsoluteLayoutFlags.All);
                    displayLayout.Add(lc);
                    lc.Add(l);
                }
                
            }
        }
        if (AutoGenerate)
        {
            question = generator.Generate(questionType);
            question.Display(displayLayout, new DisplayableArgs(absoluteLayoutBounds: "0,0,1,1", absoluteLayoutFlags: AbsoluteLayoutFlags.All));

        }

    }
    public ContentPage GetContentPage()
    {
        return content_page;
    }

    }