using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage;

namespace FinalProject
{
    public partial class NumberDisplayPage : ContentPage
    {
        public NumberDisplayPage()
        {
            InitializeComponent();

            var number = new Number(val: 5, imageType: ImageType.TenFrames);
            var number2 = new Number(val: 2, imageType: ImageType.Dice);
            number.Display(ParentLayout);
            number2.Display(ParentLayout);

        }
    }
}
