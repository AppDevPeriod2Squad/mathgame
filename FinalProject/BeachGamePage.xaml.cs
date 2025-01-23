using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject
{
    public partial class BeachGamePage : GamePage
    {
        // Game State
        private bool _isLayoutReady;
        private bool _isGamePaused; 
        private bool _isGameOver;  
        private int _score;
        private int _itemsClicked;   
        private int _timeLeft = 90;  

        // Random Number Generator
        private readonly Random _rng = new Random();
        private User user;
        private Database db;

        /*
         * Weighted spawn list: each item has:
         *   - Image file
         *   - Points (fish can be positive, trash negative)
         *   - Probability
         * 
         */
        private readonly List<Spawnable> _spawnables = new List<Spawnable>
        {
            new Spawnable("trash.png",           -5, 0.20),
            new Spawnable("fish_common.png",      2, 0.30),
            new Spawnable("fish_uncommon.png",    1, 0.20),
            new Spawnable("fish_rare.png",        3, 0.15),
            new Spawnable("fish_epic.png",        4, 0.10),
            new Spawnable("fish_legendary.png",   5, 0.05)
        };

        private async void InitalizeDatabase()
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
        }

        public BeachGamePage(Database database)
        {
            InitializeComponent();
            db = database;
            InitalizeDatabase();
            SizeChanged += BeachGamePage_SizeChanged;
        }

        private void BeachGamePage_SizeChanged(object sender, EventArgs e)
        {
            // Start the game once the layout has a valid size
            if (!_isLayoutReady && Width > 0 && Height > 0)
            {
                _isLayoutReady = true;
                StartGameLoop();
                StartCountdown();
            }
        }

        /// <summary>
        /// Continuously spawns 1–3 items every 2 seconds, until the game is over.
        /// </summary>
        private void StartGameLoop()
        {
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                // If game is over, no more spawns
                if (_isGameOver) return false;

                // If game is paused (math question), skip spawns but keep timer alive
                if (_isGamePaused) return true;

                // Otherwise spawn 1–3 items
                int count = _rng.Next(1, 4);
                for (int i = 0; i < count; i++)
                {
                    SpawnAndAnimateItem();
                }
                return true;
            });
        }

        /// <summary>
        /// Starts the 90-second countdown. Once it hits 0, end the game.
        /// </summary>
        private void StartCountdown()
        {
            timeLabel.Text = $"Time: {_timeLeft}";

            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // If game already ended, stop
                if (_isGameOver) return false;

                // Decrement time
                _timeLeft--;
                timeLabel.Text = $"Time: {_timeLeft}";

                // Check for end of timer
                if (_timeLeft <= 0)
                {
                    EndGame();
                    return false; // Stop the countdown timer
                }

                // Otherwise continue counting
                return true;
            });
        }

        /// <summary>
        /// Spawns a single item (fish or trash) using weighted probability,
        /// with random direction, amplitude, etc. On tap, updates score
        /// and handles the math question if it's the 10th tap, 20th tap, etc.
        /// </summary>
        private void SpawnAndAnimateItem()
        {
            // 1) Choose an item from the weighted list
            Spawnable chosen = PickSpawnable();

            // 2) Create the Image
            var itemImage = new Image
            {
                Source = chosen.ImageKey,
                WidthRequest = 80,
                HeightRequest = 80
            };

            // 3) Tap gesture
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                // No action if the game is already over
                if (_isGameOver) return;

                // Adjust score
                AddScore(chosen.Points);

                // Remove from layout so it can't be tapped again
                gameLayout.Children.Remove(itemImage);

                _itemsClicked++;
                if (_itemsClicked % 10 == 0)
                {
                    await PauseGameAndShowQuestion();
                }
            };
            itemImage.GestureRecognizers.Add(tapGesture);

            double layoutWidth = gameLayout.Width;
            double layoutHeight = gameLayout.Height;

            double baseline = layoutHeight * 0.85;
            double amplitude = _rng.Next(200, 401); // 200..400
            bool fromLeft = (_rng.Next(2) == 0);

            double startX, endX;
            if (fromLeft)
            {
                startX = -itemImage.WidthRequest;
                endX = layoutWidth + itemImage.WidthRequest;
            }
            else
            {
                startX = layoutWidth + itemImage.WidthRequest;
                endX = -itemImage.WidthRequest;
            }

            // Place item at start
            AbsoluteLayout.SetLayoutBounds(
                itemImage,
                new Rect(startX, baseline, itemImage.WidthRequest, itemImage.HeightRequest)
            );
            AbsoluteLayout.SetLayoutFlags(itemImage, AbsoluteLayoutFlags.None);

            gameLayout.Children.Add(itemImage);

            // 5) Animate across in 3.5s
            uint animDuration = 3500;
            var animation = new Animation(
                callback: progress =>
                {
                    double x = startX + (endX - startX) * progress;
                    double y = baseline - amplitude * (4 * progress * (1 - progress));
                    AbsoluteLayout.SetLayoutBounds(
                        itemImage,
                        new Rect(x, y, itemImage.WidthRequest, itemImage.HeightRequest)
                    );
                },
                start: 0,
                end: 1);

            animation.Commit(
                this,
                name: $"MoveItem_{Guid.NewGuid()}",
                rate: 16,
                length: animDuration,
                easing: Easing.Linear,
                finished: (v, c) =>
                {
                    // If still in layout and game not over then remove it
                    if (gameLayout.Children.Contains(itemImage))
                    {
                        gameLayout.Children.Remove(itemImage);
                    }
                }
            );
        }

        /// <summary>
        /// Weighted random pick from the _spawnables list.
        /// </summary>
        private Spawnable PickSpawnable()
        {
            double r = _rng.NextDouble();
            double cumulative = 0.0;

            foreach (var spawnable in _spawnables)
            {
                cumulative += spawnable.Probability;
                if (r <= cumulative)
                    return spawnable;
            }
            // Fallback if floating rounding occurs
            return _spawnables.Last();
        }

        /// <summary>
        /// Pauses game, asks a quick math question. 
        /// - If correct => show "CORRECT" (green) for 2s
        /// - If incorrect => show "INCORRECT" (red) for 2s and subtract 10 points
        /// Then resume the game if not over.
        /// </summary>
        private async Task PauseGameAndShowQuestion()
        {
            // If the game ended in the end, skip
            if (_isGameOver) return;

            _isGamePaused = true;

            // Simple addition
            int a = _rng.Next(1, 11);
            int b = _rng.Next(1, 11);
            int correctAnswer = a + b;

            string userEntry = await DisplayPromptAsync(
                "Math Question",
                $"What is {a} + {b}?",
                accept: "OK",
                cancel: "Cancel",
                keyboard: Keyboard.Numeric);

            // If user cancels or provides non-integer => force wrong
            int userVal;
            bool valid = int.TryParse(userEntry, out userVal);

            if (valid && userVal == correctAnswer)
            {
                // Correct
                await ShowFeedback("CORRECT", Colors.Green);
            }
            else
            {
                // Incorrect
                AddScore(-10);
                await ShowFeedback("INCORRECT", Colors.Red);
            }

            // Resume if game is still going
            if (!_isGameOver)
            {
                _isGamePaused = false;
            }
        }

       
        private async Task ShowFeedback(string message, Color color)
        {
            feedbackLabel.Text = message;
            feedbackLabel.TextColor = color;
            feedbackLabel.IsVisible = true;

            await Task.Delay(2000);

            feedbackLabel.IsVisible = false;
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

        private async void EndGame()
        {
            _isGameOver = true;
            _isGamePaused = true; // no new spawns or questions
            AwardMoney(_score);

            // Show final score & ask if user wants to play again
            bool answer = await DisplayAlert(
                "Game Over",
                $"Time is up!\nYour Score: {_score}\nPlay Again?",
                "Yes",
                "No");

            if (answer)
            {
                // If "Yes"
                await Navigation.PushAsync(new BeachGamePage(db), false);
            }
            else
            {
                // If "No"
                await Application.Current?.MainPage?.Navigation.PopToRootAsync();
            }
        }

        private void AddScore(int points)
        {
            _score += points;
            scoreLabel.Text = $"Score: {_score}";
        }
    }

    /// <summary>
    /// Describes a spawnable object (fish or trash),
    /// with an image file, point value, and spawn probability.
    /// </summary>
    public record struct Spawnable(string ImageKey, int Points, double Probability);
}
