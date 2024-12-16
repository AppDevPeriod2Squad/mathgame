using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject
{
    public class Background : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int rarity;
        public int Rarity
        {
            get { return rarity; }
            set { if (rarity != value) { rarity = value; OnPropertyChanged("Rarity"); } }
        }

        private bool colorType;
        public bool ColorType
        {
            get { return colorType; }
            set { if (colorType != value) {colorType = value; OnPropertyChanged("ColorType"); } }
        }

        private string colorString;
        public string ColorString
        {
            get { return colorString; }
            set { if (colorString != value) { colorString = value; OnPropertyChanged("ColorString"); } }
        }
    }
}