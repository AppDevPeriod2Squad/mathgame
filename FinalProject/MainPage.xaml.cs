// hi there sigmas!

namespace FinalProject
{
    public partial class MainPage : ContentPage
    {
        int count;

        public MainPage()
        {
            InitializeComponent();
            count = 0;
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }

}
