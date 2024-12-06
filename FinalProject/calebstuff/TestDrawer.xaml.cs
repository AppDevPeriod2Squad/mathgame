namespace MathGame;

public partial class TestDrawer : ContentPage
{
	DrawManager manager;
	public TestDrawer()
	{
		InitializeComponent();
		manager = new DrawManager();
		graphicsView.Drawable = manager;
		
	}
}