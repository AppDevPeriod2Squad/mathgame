using Microsoft.Maui.Controls;
using Microsoft.Maui.Layouts;
using System;

namespace FinalProject
{
    public partial class BeachGamePage : GamePage
    {
        private bool _isLayoutReady;
        private readonly Random _rng = new Random();
        private readonly string[] _fishImages = new[]
        {
            "fish_common.png",
            "fish_uncommon.png",
            "fish_rare.png",
            "fish_epic.png",
            "fish_legendary.png"
        };

        public BeachGamePage()
        {
            InitializeComponent();
            SizeChanged += BeachGamePage_SizeChanged;
        }

        private void BeachGamePage_SizeChanged(object sender, EventArgs e)
        {
            if (!_isLayoutReady && Width > 0 && Height > 0)
            {
                _isLayoutReady = true;
                StartGameLoop();
            }
        }

        private void StartGameLoop()
        {
            Device.StartTimer(TimeSpan.FromSeconds(2), () =>
            {
                int numberOfFish = _rng.Next(1, 4);
                for (int i = 0; i < numberOfFish; i++)
                {
                    SpawnAndAnimateFish();
                }
                return true;
            });
        }

        private void SpawnAndAnimateFish()
        {
            string fishSource = _fishImages[_rng.Next(_fishImages.Length)];
            double amplitude = _rng.Next(200, 401);
            bool fromLeft = _rng.Next(2) == 0;

            var fish = new Image
            {
                Source = fishSource,
                WidthRequest = 80,
                HeightRequest = 80
            };

            double layoutWidth = gameLayout.Width;
            double layoutHeight = gameLayout.Height;
            double baseline = layoutHeight * 0.85;

            double startX, endX;
            if (fromLeft)
            {
                startX = -fish.WidthRequest;
                endX = layoutWidth + fish.WidthRequest;
            }
            else
            {
                startX = layoutWidth + fish.WidthRequest;
                endX = -fish.WidthRequest;
            }

            AbsoluteLayout.SetLayoutBounds(fish,
                new Rect(startX, baseline, fish.WidthRequest, fish.HeightRequest));
            AbsoluteLayout.SetLayoutFlags(fish, AbsoluteLayoutFlags.None);

            gameLayout.Children.Add(fish);

            uint animationDuration = 3500;

            var animation = new Animation(
                callback: (progress) =>
                {
                    double x = startX + (endX - startX) * progress;
                    double y = baseline - amplitude * (4 * progress * (1 - progress));
                    AbsoluteLayout.SetLayoutBounds(
                        fish,
                        new Rect(x, y, fish.WidthRequest, fish.HeightRequest)
                    );
                },
                start: 0,
                end: 1
            );

            animation.Commit(
                owner: this,
                name: $"FishAnimation_{Guid.NewGuid()}",
                rate: 16,
                length: animationDuration,
                easing: Easing.Linear,
                finished: (v, c) =>
                {
                    gameLayout.Children.Remove(fish);
                }
            );
        }
    }
}
