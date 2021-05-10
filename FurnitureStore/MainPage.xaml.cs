using FurnitureStore.view;
using Xamarin.Forms;

namespace FurnitureStore
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            NavigationPage page = new NavigationPage(new PersonalAccount())
            {
                IconImageSource = ImageSource.FromFile("Promo.svg"),
                Title = "qwqw"
            };
            Children.Add(page);
        }
    }
}