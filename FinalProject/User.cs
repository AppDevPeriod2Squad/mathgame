using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class User : INotifyPropertyChanged
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { if (name != value) { name = value; OnPropertyChanged("Name"); } }
        }

        private int quarters;
        public int Quarters
        {
            get { return quarters; }
            set { if (quarters != value) { quarters = value; OnPropertyChanged("Quarters"); } }
        }

        private int dimes;
        public int Dimes
        {
            get { return dimes; }
            set { if (dimes != value) { dimes = value; OnPropertyChanged("Dimes"); } }
        }

        private int nickels;
        public int Nickels
        {
            get { return nickels; }
            set { if (nickels != value) { nickels = value; OnPropertyChanged("Nickels"); } }
        }

        private int pennies;
        public int Pennies
        {
            get { return pennies; }
            set { if (pennies != value) { pennies = value; OnPropertyChanged("Pennies"); } }
        }

        [TextBlob("StringsBlobbed")]
        private List<string> backgrounds { get; set; }

        private int xp;
        public int XP
        {
            get { return xp;}
            set { if (xp != value) { xp = value; OnPropertyChanged("XP"); } } 
        }

        private int gamesCompleted;
        public int GamesCompleted
        {
            get { return gamesCompleted; }
            set { if (gamesCompleted != value) { gamesCompleted = value; OnPropertyChanged("GamesCompleted"); } }
        }
    }
}
