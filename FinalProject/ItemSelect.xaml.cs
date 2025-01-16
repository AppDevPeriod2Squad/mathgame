using FinalProject.Gacha;
using System;
using System.Numerics;

namespace FinalProject;

public partial class ItemSelect : ContentPage
{
    Database database;
    User user;
    Boolean usePfp;
    List<int> items = new List<int>();
    List<int> count = new List<int>();
    Dictionary<int, int> firstIndex = new Dictionary<int, int>();
    private IDispatcherTimer _timer;

    public ItemSelect(Database db, bool pfp)
    {
        database = db;
        usePfp = pfp;

        _timer = Dispatcher.CreateTimer();
        _timer.Interval = TimeSpan.FromSeconds(1); // Set interval
        _timer.Tick += Timer_Tick; // Attach the event
        _timer.Start(); // Start the timer

        InitializeComponent();
        SetupList();

    }


    private void Timer_Tick(object sender, EventArgs e)
    {
        // Function to run periodically
        SetupGrid();
        _timer.Stop();
    }


    public async void SetupList()
    {
        user = await database.GetUserAsync();
        if (usePfp)
        {
            String[] baseList = user.Images.Split(' ');
            List<int> indices = new List<int>();
            foreach (string x in baseList)
            {
                indices.Add(int.Parse(x));
            }
            int index = 0;
            foreach (int x in indices)
            {
                if (!items.Contains(x))
                {
                    items.Add(x);
                    count.Add(1);
                    firstIndex.Add(x, index);
                }
                else
                {
                    int indexX = items.IndexOf(x);
                    count[indexX]++;
                }
                index++;
            }

        }
        else
        {
            String[] baseList = user.Backgrounds.Split(' ');
            List<int> indices = new List<int>();
            foreach (string x in baseList)
            {
                indices.Add(int.Parse(x));
            }
            int index = 0;

            foreach (int x in indices)
            {
                if (!items.Contains(x))
                {
                    items.Add(x);
                    count.Add(1);
                    firstIndex.Add(x, index);

                }
                else
                {
                    int indexX = items.IndexOf(x);
                    count[indexX]++;
                }
                index++;
            }
        }

        SetupGrid();
    }

    public void SetupGrid()
    {
        itemGrid.Clear();
        int W = (int)itemGrid.Width;
        itemGrid.ColumnSpacing = 10;
        itemGrid.RowSpacing = 10;
        int cols = (W * 4 / 5) / 100;
        if (cols == 0)
        {
            cols = 1;
        }
        ColumnDefinition[] colDefs = new ColumnDefinition[cols];
        for (int i = 0; i < cols; i++)
        {
            colDefs[i] = new ColumnDefinition(100);

        }
        itemGrid.ColumnDefinitions = new ColumnDefinitionCollection(colDefs);
        RowDefinition[] rowDefs = new RowDefinition[2 * (items.Count % cols == 0 ? items.Count / cols : items.Count / cols + 1)];
        for (int i = 0; i < rowDefs.Length; i++)
        {
            rowDefs[i] = new RowDefinition() { Height = i % 2 == 0 ? GridLength.Auto : 20 };
        }
        itemGrid.RowDefinitions = new RowDefinitionCollection(rowDefs);

        for (int j = 0; j < items.Count; j++)
        {
            int col = j % cols;
            int row = 2 * (j / cols);
            string[] stars = new string[usePfp ? Translator.pfpRarities[items[j]] : Translator.backgroundRarities[items[j]]];
            Array.Fill(stars, "*");
            ImageButton ib = new ImageButton() { Source = usePfp ? Translator.pfpLinks[items[j]] : Translator.backgroundLinks[items[j]], Aspect = Aspect.AspectFit };
            ib.ClassId = j.ToString();
            ib.Clicked += async (sender, args) => await SelectCommand(items[int.Parse(ib.ClassId)]);

            itemGrid.Add(ib, col, row);
            itemGrid.Add(new Label() { FontSize = 16, Text = $"{String.Join(" ", stars)}", HorizontalTextAlignment = TextAlignment.Start, TextColor = Translator.RarityColor[usePfp ? Translator.pfpRarities[items[j]] : Translator.backgroundRarities[items[j]]] }, col, row + 1);
            itemGrid.Add(new Label() { FontSize = 16, Text = $"{count[j].ToString()}x", HorizontalTextAlignment = TextAlignment.End }, col, row + 1);
        }
    }

    public async Task SelectCommand(int select)
    {
        if (usePfp)
        {
            user.Picture = select;

        }
        else
        {
            user.Background = select;
        }
        await database.UpdateExistingUserAsync(user);
        await Navigation.PopAsync();
    }


}