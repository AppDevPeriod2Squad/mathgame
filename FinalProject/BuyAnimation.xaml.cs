using System.Timers;

namespace FinalProject;

public partial class BuyAnimation : ContentPage
{
    public BuyAnimation(Database db)
    {
        InitializeComponent();

        var timer = new System.Timers.Timer(1000/30);
        timer.Elapsed += new ElapsedEventHandler(Redraw);
        timer.Start();
    }

    public void Redraw(object source, ElapsedEventArgs e)
    {
        //var clock = (ClockDrawable) this.ClockGraphicsView.Drawable;
        var graphicsView = this.AnimationGraphicsView;

        graphicsView.Invalidate();
    }
}