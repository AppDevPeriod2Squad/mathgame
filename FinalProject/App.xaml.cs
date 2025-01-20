namespace FinalProject
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Database db = new Database();
<<<<<<< HEAD
            MainPage = new NavigationPage(new NavPageTemp(db));
=======
            MainPage = new NavigationPage(new Profile(db));
>>>>>>> fc5c4ad943c29cd9b0c16e406905a0d2ec460d7b
        }
    }
}
