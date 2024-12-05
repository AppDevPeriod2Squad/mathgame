using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace FinalProject
{
    public partial class NumberDisplayPage : ContentPage
    {
        public NumberDisplayPage()
        {
            InitializeComponent();

            var number = new Number
            {
                Val = 5, // Example value, also only value with ten frames rn
            };
            number.UpdateImageToTenFrames();
            number.Display(ParentLayout);
        }
    }
}
