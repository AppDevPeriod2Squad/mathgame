using Microsoft.Maui.Controls;
using System.ComponentModel;
using System.Linq;

namespace FinalProject
{
    public partial class ConnectionsGamePage : GamePage
    {
        private readonly SpriteManager _spriteManager = new SpriteManager();
        private readonly Dictionary<int, string> _numberWords = new()
        {
            {1, "one"}, {2, "two"}, {3, "three"},
            {4, "four"}, {5, "five"}, {6, "six"}
        };
        private readonly Color[] _groupRowColors =
        {
            Colors.Yellow,
            Colors.LightGreen,
            Colors.Blue,
            Colors.Purple
        };
        private List<NumberRepresentation> _remainingItems = new();
        private List<int> _foundGroups = new();
        private List<NumberRepresentation> _selectedItems = new();
        private int _mistakesRemaining = 4;
        private int _groupsFound = 0;
        private User user;
        private Database db;

        public ConnectionsGamePage(Database database)
        {
            InitializeComponent();
            db = database;
            InitializeGame();
        }

        private async void AwardMoney(int score)
        {
            int remainingCents = score;
            Random random = new Random();
            int[] validCoins = { 1, 5, 10, 25 };

            while (remainingCents > 0)
            {
                int coinValue = validCoins[random.Next(0, validCoins.Length)];
                int coinAmount;

                switch (coinValue)
                {
                    case 1: // Pennies
                        coinAmount = Math.Min(remainingCents, 1);
                        user.Pennies += coinAmount;
                        remainingCents -= coinAmount;
                        break;

                    case 5: // Nickels
                        coinAmount = Math.Min(remainingCents / 5, 1) * 5;
                        user.Nickels += coinAmount / 5;
                        remainingCents -= coinAmount;
                        break;

                    case 10: // Dimes
                        coinAmount = Math.Min(remainingCents / 10, 1) * 10;
                        user.Dimes += coinAmount / 10;
                        remainingCents -= coinAmount;
                        break;

                    case 25: // Quarters
                        coinAmount = Math.Min(remainingCents / 25, 1) * 25;
                        user.Quarters += coinAmount / 25;
                        remainingCents -= coinAmount;
                        break;
                }
            }

            await db.UpdateExistingUserAsync(user);
        }
        private async void InitializeGame()
        {
            try
            {
                user = await db.GetUserAsync();
                if (user == null)
                    throw new Exception("Failed to retrieve user from database.");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to initialize game: {ex.Message}", "OK");
                Application.Current?.MainPage?.Navigation.PopToRootAsync();
                return;
            }

            _remainingItems.Clear();
            _selectedItems.Clear();
            _foundGroups.Clear();
            _mistakesRemaining = 4;
            _groupsFound = 0;

            MistakesLabel.Text = $"Mistakes remaining: {_mistakesRemaining}";
            StatusLabel.Text = "Select four items and then press SUBMIT.";

            var random = new Random();
            var chosenNumbers = new HashSet<int>();
            while (chosenNumbers.Count < 4)
            {
                chosenNumbers.Add(random.Next(1, 7));
            }

            foreach (int num in chosenNumbers)
            {
                _remainingItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Image,
                    ImageSource = _spriteManager.GetDiceImage(num)
                });
                _remainingItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Image,
                    ImageSource = _spriteManager.GetTenFramesImage(num)
                });
                _remainingItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Text,
                    DisplayText = num.ToString()
                });
                _remainingItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Text,
                    DisplayText = _numberWords[num]
                });
            }

            _remainingItems = _remainingItems.OrderBy(x => random.Next()).ToList();
            RebuildGrid();
        }

        private void RebuildGrid()
        {
            ItemsGrid.Children.Clear();
            ItemsGrid.RowDefinitions.Clear();
            ItemsGrid.ColumnDefinitions.Clear();

            for (int c = 0; c < 4; c++)
            {
                ItemsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            }

            int currentRow = 0;
            for (int i = 0; i < _foundGroups.Count; i++)
            {
                ItemsGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                var color = _groupRowColors[i % _groupRowColors.Length];
                var label = new Label
                {
                    Text = _foundGroups[i].ToString(),
                    BackgroundColor = color,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center,
                    FontAttributes = FontAttributes.Bold,
                    FontSize = 20,
                    Margin = new Thickness(5),
                    HeightRequest = 70
                };
                ItemsGrid.Add(label, 0, currentRow);
                Grid.SetColumnSpan(label, 4);
                currentRow++;
            }

            int leftoverCount = _remainingItems.Count;
            int neededRows = (int)Math.Ceiling(leftoverCount / 4.0);
            for (int i = 0; i < neededRows; i++)
            {
                ItemsGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            }

            int rowIndex = currentRow;
            int colIndex = 0;

            foreach (var item in _remainingItems)
            {
                var btn = new Button
                {
                    BackgroundColor = Colors.LightGray,
                    Padding = new Thickness(5),
                    Margin = 0,
                    FontSize = 16,
                    CommandParameter = item
                };

                if (item.DisplayType == RepresentationType.Image && item.ImageSource != null)
                {
                    btn.ImageSource = item.ImageSource;
                }
                else if (item.DisplayType == RepresentationType.Text && !string.IsNullOrEmpty(item.DisplayText))
                {
                    btn.Text = item.DisplayText;
                }

                btn.Clicked += OnItemClicked;
                ItemsGrid.Add(btn, colIndex, rowIndex);

                colIndex++;
                if (colIndex > 3)
                {
                    colIndex = 0;
                    rowIndex++;
                }
            }
        }

        private void OnItemClicked(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;
            var rep = btn.CommandParameter as NumberRepresentation;
            if (rep == null || rep.IsLocked) return;

            if (_selectedItems.Contains(rep))
            {
                _selectedItems.Remove(rep);
                btn.BackgroundColor = Colors.LightGray;
            }
            else
            {
                if (_selectedItems.Count >= 4)
                {
                    StatusLabel.Text = "Already selected 4. Submit or deselect an item.";
                    return;
                }
                _selectedItems.Add(rep);
                btn.BackgroundColor = Colors.Yellow;
            }
        }

        private void OnSubmitClicked(object sender, EventArgs e)
        {
            if (_selectedItems.Count != 4)
            {
                StatusLabel.Text = $"You must select exactly 4 items. Currently selected: {_selectedItems.Count}.";
                return;
            }

            var distinctNums = _selectedItems.Select(x => x.NumberValue).Distinct().Count();
            if (distinctNums == 1)
            {
                _groupsFound++;
                StatusLabel.Text = $"Correct group! Groups found: {_groupsFound} of 4.";

                int groupNumber = _selectedItems[0].NumberValue;
                foreach (var rep in _selectedItems) rep.IsLocked = true;

                foreach (var rep in _selectedItems)
                {
                    _remainingItems.Remove(rep);
                }

                _foundGroups.Add(groupNumber);
                _selectedItems.Clear();
                RebuildGrid();

                if (_groupsFound == 4)
                {
                    StatusLabel.Text = "Congratulations! You found all 4 groups!";
                    SubmitButton.IsEnabled = false;
                    ResetGame(true);
                }
            }
            else
            {
                _mistakesRemaining--;
                MistakesLabel.Text = $"Mistakes remaining: {_mistakesRemaining}";
                StatusLabel.Text = "Incorrect group! Try again.";

                ResetSelected();

                if (_mistakesRemaining <= 0)
                {
                    SubmitButton.IsEnabled = false;
                    StatusLabel.Text = "No more mistakes left. Game Over!";
                    foreach (var child in ItemsGrid.Children.OfType<Button>())
                        child.IsEnabled = false;
                    ResetGame(false);
                }
            }
        }

        private void ResetSelected()
        {
            foreach (var child in ItemsGrid.Children.OfType<Button>())
            {
                var rep = child.CommandParameter as NumberRepresentation;
                if (rep != null && _selectedItems.Contains(rep))
                {
                    child.BackgroundColor = Colors.LightGray;
                }
            }
            _selectedItems.Clear();
        }

        private async void ResetGame(bool ifWon)
        {
            if (ifWon)
            {
                AwardMoney(16);
                Dispatcher.Dispatch(async () =>
                {
                    bool restart = await DisplayAlert(
                        "Congratulations!",
                        $"You've found all the groups! \nWould you like to play again?",
                        "Yes",
                        "No"
                    );

                    if (restart)
                        await Navigation.PushAsync(new ConnectionsGamePage(db), false);
                    else
                        Application.Current?.MainPage?.Navigation.PopToRootAsync();
                });
            }
            else
            {
                Dispatcher.Dispatch(async () =>
                {
                    bool restart = await DisplayAlert(
                        "Game over",
                        $"You ran out of chances. \nWould you like to play again?",
                        "Yes",
                        "No"
                    );

                    if (restart)
                        await Navigation.PushAsync(new ConnectionsGamePage(db), false);
                    else
                        Application.Current?.MainPage?.Navigation.PopToRootAsync();
                });
            }
        }
    }

    public class NumberRepresentation
    {
        public int NumberValue { get; set; }
        public RepresentationType DisplayType { get; set; }
        public string DisplayText { get; set; }
        public ImageSource ImageSource { get; set; }
        public bool IsLocked { get; set; }
    }

    public enum RepresentationType
    {
        Text,
        Image
    }
}
