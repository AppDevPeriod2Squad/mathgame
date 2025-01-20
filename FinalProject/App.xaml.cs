namespace FinalProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Database db = new Database();

            MainPage = new NavigationPage(new NavPageTemp(db));


        }
    }
}
