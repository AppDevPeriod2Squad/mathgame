using FinalProject.QuestionGeneratorStuff;
using Microsoft.Maui.Dispatching;
using Microsoft.Maui.Layouts;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FinalProject
{
    public partial class DinoGame : GamePage, INotifyPropertyChanged
    {
        private int _score;
        private double _initialX;
        private double _initialY;
        private readonly Dictionary<int, (Image Image, Rect Position)> _numberImages = new();
        private readonly SpriteManager _spriteManager = new();
        private User user;
        private Database db;
        private int Lives = 3;

        private int waveCount = 0;
        private const double FallDistance = 0.02;
        private const double TimerIntervalSeconds = 0.1;
        private double CurrentFallSpeed = FallDistance;
        //private const double SpeedIncreaseRate = 0.002;
        private const double MaxFallSpeed = 0.06;

        public event PropertyChangedEventHandler PropertyChanged;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                OnPropertyChanged();
            }
        }

        public DinoGame(Database database)
        {
            InitializeComponent();
            BindingContext = this;
            db = database;
            InitializeGameAsync();
        }
        private async void InitializeGameAsync()
        {
            await DisplayInstructions();
            InitializeGame();
        }
        private async Task DisplayInstructions()
        {
            await DisplayAlert("Instructions", $"Drag the dinosaur to make sure it only eats the bigger number. If you do not eat the bigger number you will lose a life.", "OK");
        }
        private async void InitializeGame()
        {
            try
            {
                user = await db.GetUserAsync();
                if (user == null)
                    throw new Exception("Failed to retrieve user from database.");

                ConfigureDinoGesture();
                GenerateDiceImages();
                AddLivesImages();
                StartFallingAnimation();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to initialize game: {ex.Message}", "OK");
                Application.Current?.MainPage?.Navigation.PopToRootAsync();
            }
        }

        private async void AwardMoney()
        {
            int remainingCents = Score;
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


        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void AddLivesImages()
        {
            const int heartSize = 55;
            const int spacing = 6;
            const int margin = 6;

            for (int i = 0; i < Lives; i++)
            {
                var heartImage = new Image
                {
                    Source = "heart.png",
                    WidthRequest = heartSize,
                    HeightRequest = heartSize
                };

                var positionX = margin + i * (heartSize + spacing);
                var positionY = margin;
                AbsoluteLayout.SetLayoutBounds(heartImage, new Rect(positionX, positionY, heartSize, heartSize));
                AbsoluteLayout.SetLayoutFlags(heartImage, AbsoluteLayoutFlags.None);

                mainLayout.Children.Add(heartImage);
            }
        }

        private void ConfigureDinoGesture()
        {
            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += DinoPanUpdated;
            DinoImage.GestureRecognizers.Add(panGesture);
        }

        private void DinoPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (sender is not Image dino) return;

            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    _initialX = dino.TranslationX;
                    break;
                case GestureStatus.Running:
                    UpdateDinoPosition(dino, e.TotalX);
                    CheckAndHandleCollision();
                    break;
                case GestureStatus.Completed:
                    _initialX = dino.TranslationX;
                    break;
            }
        }

        private void UpdateDinoPosition(Image dino, double totalX)
        {
            var newTranslationX = _initialX + totalX;
            var dinoWidth = dino.Width * dino.Scale;
            var minTranslationX = -mainLayout.Width * 0.5 + dinoWidth * 0.5;
            var maxTranslationX = mainLayout.Width * 0.5 - dinoWidth * 0.5;
            dino.TranslationX = Math.Clamp(newTranslationX, minTranslationX, maxTranslationX);
        }

        private void CheckAndHandleCollision()
        {
            var dinoBounds = new Rect(
                (mainLayout.Width * 0.5) + DinoImage.TranslationX - (DinoImage.Width * DinoImage.Scale / 2),
                (mainLayout.Height * 0.9) - (DinoImage.Height * DinoImage.Scale / 2),
                DinoImage.Width * DinoImage.Scale,
                DinoImage.Height * DinoImage.Scale
            );

            var diceValues = _numberImages.Keys.ToList();

            foreach (var (key, (image, position)) in _numberImages.ToList())
            {
                var diceBounds = new Rect(
                    position.X * mainLayout.Width - (image.Width / 2),
                    position.Y * mainLayout.Height - (image.Height / 2),
                    image.Width,
                    image.Height
                );

                if (!dinoBounds.IntersectsWith(diceBounds)) continue;

                var smallestValue = diceValues.Min();
                var largestValue = diceValues.Max();

                if (key == smallestValue)
                {
                    Lives--;
                    UpdateLivesDisplay();
                    if (Lives <= 0)
                    {
                        EndGame();
                        return;
                    }
                }
                else if (key == largestValue)
                {
                    Score++;
                }

                foreach (var (img, _) in _numberImages.Values)
                    mainLayout.Children.Remove(img);

                _numberImages.Clear();
                break;
            }
        }

        private void UpdateLivesDisplay()
        {
            foreach (var child in mainLayout.Children.ToList())
            {
                if (child is Image img && img.Source.ToString().Contains("heart.png"))
                    mainLayout.Children.Remove(img);
            }

            AddLivesImages();
        }

        private async void ResetGame()
        {
            await Navigation.PushAsync(new DinoGame(db), false);
        }

        private async void EndGame()
        {
            _numberImages.Clear();
            mainLayout.Children.Clear();

            AwardMoney();
            user.GamesCompleted++;
            await db.UpdateExistingUserAsync(user);

            //string updatedProfileInfo =
            //    $"Your Profile:\n" +
            //    $"- Games Played: {user.GamesCompleted}\n" +
            //    $"- Quarters: {user.Quarters}\n" +
            //    $"- Dimes: {user.Dimes}\n" +
            //    $"- Nickels: {user.Nickels}\n" +
            //    $"- Pennies: {user.Pennies}";

            Dispatcher.Dispatch(async () =>
            {
                bool restart = await DisplayAlert(
                    "Game Over",
                    $"You've lost all your lives!\nYour score: {Score}\n\nWould you like to play again?",
                    "Yes",
                    "No"
                );

                if (restart)
                    ResetGame();
                else
                    Application.Current?.MainPage?.Navigation.PopToRootAsync();
            });
        }

        private void GenerateDiceImages()
        {
            var random = new Random();
            var imageType = random.NextInt64(SpriteManager.NumberOfImageTypes);

            if (imageType == 0)
            {
                var val1 = random.Next(1, 7);
                var val2 = random.Next(1, 7);

                while (val1 == val2)
                    val2 = random.Next(1, 7);

                var positions = GenerateValidPositions(random, 2);

                CreateDiceImage(val1, positions[0]);
                CreateDiceImage(val2, positions[1]);
            }
        }

        private List<Rect> GenerateValidPositions(Random random, int count)
        {
            const double minDistance = 0.2;
            var positions = new List<Rect>();

            while (positions.Count < count)
            {
                var x = random.NextDouble() * 0.8 + 0.1;
                var y = 0.01;
                var newPosition = new Rect(x, y, 30, 30);

                if (positions.All(pos => CalculateDistance(newPosition, pos) >= minDistance))
                    positions.Add(newPosition);
            }

            return positions;
        }

        private double CalculateDistance(Rect position1, Rect position2)
        {
            var dx = position1.X - position2.X;
            var dy = position1.Y - position2.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        private void CreateDiceImage(int val, Rect position)
        {
            if (_numberImages.ContainsKey(val)) return;

            var diceImage = new Image
            {
                Source = _spriteManager.GetDiceImage(val),
                WidthRequest = 50,
                HeightRequest = 50
            };

            _numberImages[val] = (diceImage, position);
            RefreshDiceImages();
        }

        private void RefreshDiceImages()
        {
            foreach (var child in mainLayout.Children.ToList())
            {
                if (child is Image img && _numberImages.Values.Select(n => n.Image).Contains(img))
                    mainLayout.Children.Remove(img);
            }

            foreach (var (image, position) in _numberImages.Values)
            {
                AbsoluteLayout.SetLayoutBounds(image, position);
                AbsoluteLayout.SetLayoutFlags(image, AbsoluteLayoutFlags.PositionProportional);
                mainLayout.Children.Add(image);
            }
        }

        private void StartFallingAnimation()
        {
            Dispatcher.StartTimer(TimeSpan.FromSeconds(TimerIntervalSeconds), () =>
            {
                if (Lives <= 0) return false; // Stop the timer if the game is over

                var removedKeys = new List<int>();
                bool lifeDeductedThisFrame = false;

                foreach (var key in _numberImages.Keys.ToList())
                {
                    var (image, position) = _numberImages[key];
                    var newY = position.Y + CurrentFallSpeed;

                    if (newY > 1) // Block has fallen off the screen
                    {
                        mainLayout.Children.Remove(image);
                        removedKeys.Add(key);

                        if (!lifeDeductedThisFrame)
                        {
                            Lives--;
                            UpdateLivesDisplay();
                            lifeDeductedThisFrame = true;

                            if (Lives <= 0)
                            {
                                EndGame();
                                return false; // End game, stop the timer
                            }
                        }
                    }
                    else
                    {
                        var newPosition = new Rect(position.X, newY, position.Width, position.Height);
                        _numberImages[key] = (image, newPosition);
                        AbsoluteLayout.SetLayoutBounds(image, newPosition);
                    }
                }

                // Remove the blocks that fell off
                foreach (var key in removedKeys)
                    _numberImages.Remove(key);

                // If all blocks are cleared, generate new ones
                if (_numberImages.Count == 0)
                {
                    waveCount++;

                    switch (waveCount) 
                    {
                        case 3:
                            CurrentFallSpeed = 0.025;
                            break;
                        case 8:
                            CurrentFallSpeed = 0.03;
                            break;
                        case 20:
                            CurrentFallSpeed = 0.04;
                            break;
                        case 35:
                            CurrentFallSpeed = 0.05;
                            break;
                        case 50:
                            CurrentFallSpeed = 0.06;
                            break;
                        case 100:
                            CurrentFallSpeed = 0.08;
                            break;
                        case 150:
                            CurrentFallSpeed = 0.09;
                            break;
                        case 200:
                            CurrentFallSpeed = 0.1;
                            break;
                    }


                    GenerateDiceImages();
                }

                CheckAndHandleCollision();

                return true;
            });
        }
    }
}
