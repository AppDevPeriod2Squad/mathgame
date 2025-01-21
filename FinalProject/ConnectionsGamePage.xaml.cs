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
        private List<NumberRepresentation> _allItems = new();
        private List<NumberRepresentation> _selectedItems = new();
        private int _groupsFound = 0;
        private int _mistakesRemaining = 4;

        public ConnectionsGamePage()
        {
            InitializeComponent();
            InitializeGame();
        }

        private void InitializeGame()
        {
            _allItems.Clear();
            _selectedItems.Clear();
            _groupsFound = 0;
            _mistakesRemaining = 4;
            MistakesLabel.Text = $"Mistakes remaining: {_mistakesRemaining}";
            StatusLabel.Text = "Select four items and then press SUBMIT.";

            var random = new Random();
            var chosenNumbers = new HashSet<int>();
            while (chosenNumbers.Count < 4)
                chosenNumbers.Add(random.Next(1, 7));

            foreach (int num in chosenNumbers)
            {
                _allItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Image,
                    ImageSource = _spriteManager.GetDiceImage(num)
                });

                _allItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Image,
                    ImageSource = _spriteManager.GetTenFramesImage(num)
                });

                _allItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Text,
                    DisplayText = num.ToString()
                });

                _allItems.Add(new NumberRepresentation
                {
                    NumberValue = num,
                    DisplayType = RepresentationType.Text,
                    DisplayText = _numberWords[num]
                });
            }

            _allItems = _allItems.OrderBy(x => random.Next()).ToList();
            BuildGrid();
        }

        private void BuildGrid()
        {
            ItemsGrid.Children.Clear();
            int row = 0, col = 0;

            for (int i = 0; i < _allItems.Count; i++)
            {
                var item = _allItems[i];
                var button = new Button
                {
                    BackgroundColor = Colors.LightGray,
                    Padding = new Thickness(5),
                    Margin = 0,
                    FontSize = 16,
                    CommandParameter = item
                };

                if (item.DisplayType == RepresentationType.Image && item.ImageSource != null)
                {
                    button.ImageSource = item.ImageSource;
                }
                else if (item.DisplayType == RepresentationType.Text && !string.IsNullOrEmpty(item.DisplayText))
                {
                    button.Text = item.DisplayText;
                }

                button.Clicked += OnItemClicked;

                ItemsGrid.Add(button, col, row);

                col++;
                if (col > 3)
                {
                    col = 0;
                    row++;
                }
            }
        }

        private void OnItemClicked(object sender, EventArgs e)
        {
            if (sender is not Button btn) return;

            var item = btn.CommandParameter as NumberRepresentation;
            if (item == null || item.IsLocked) return;

            if (_selectedItems.Contains(item))
            {
                _selectedItems.Remove(item);
                btn.BackgroundColor = Colors.LightGray;
            }
            else
            {
                if (_selectedItems.Count >= 4)
                {
                    StatusLabel.Text = "Already selected 4. Submit or deselect an item.";
                    return;
                }

                _selectedItems.Add(item);
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

            var distinctNumbers = _selectedItems.Select(x => x.NumberValue).Distinct().Count();
            if (distinctNumbers == 1)
            {
                _groupsFound++;
                StatusLabel.Text = $"Correct group! Groups found: {_groupsFound} of 4.";

                foreach (var rep in _selectedItems) rep.IsLocked = true;

                foreach (var view in ItemsGrid.Children.OfType<Button>())
                {
                    var rep = view.CommandParameter as NumberRepresentation;
                    if (_selectedItems.Contains(rep))
                    {
                        view.BackgroundColor = Colors.LightGreen;
                        view.IsEnabled = false;
                    }
                }

                _selectedItems.Clear();

                if (_groupsFound == 4)
                {
                    StatusLabel.Text = "Congratulations! You found all 4 groups!";
                    SubmitButton.IsEnabled = false;
                    
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
                    GameOver();
                }
            }
        }

        private void ResetSelected()
        {
            foreach (var view in ItemsGrid.Children.OfType<Button>())
            {
                var rep = view.CommandParameter as NumberRepresentation;
                if (rep != null && _selectedItems.Contains(rep))
                {
                    view.BackgroundColor = Colors.LightGray;
                }
            }
            _selectedItems.Clear();
        }

        private async void ResetGame()
        {
            await Navigation.PushAsync(new ConnectionsGamePage(), false);
        }

        private void GameOver()
        {
            SubmitButton.IsEnabled = false;

            foreach (var btn in ItemsGrid.Children.OfType<Button>())
                btn.IsEnabled = false;

            Dispatcher.Dispatch(async () =>
            {
                bool restart = await DisplayAlert(
                    "Game Over",
                    $"Game Over! You ran out of mistakes!\nYou found {_groupsFound} groups\n\nWould you like to play again?",
                    "Yes",
                    "No"
                );

                if (restart)
                    ResetGame();
                else
                    Application.Current?.MainPage?.Navigation.PopToRootAsync();
            });
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
