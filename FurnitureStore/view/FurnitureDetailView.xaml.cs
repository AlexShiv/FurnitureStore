using System;
using System.Globalization;
using System.Threading.Tasks;
using FurnitureStore.config;
using FurnitureStore.model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FurnitureDetailView : ContentPage
    {
        public FurnitureDetailView(Furniture furniture)
        {
            InitializeComponent();

            var image = new Image
            {
                HeightRequest = 200,
                WidthRequest = 200,
                Source = furniture.PhotoPath
            };

            var nameLabel = new Label
            {
                FontSize = 20,
                Text = furniture.Name,
                Margin = Constant.THICKNESS
            };

            var sizeHitLabel = new Label
            {
                FontSize = 15,
                Text = $"ВxДхШ: {furniture.Height}x{furniture.Length}x{furniture.Width}",
                Margin = Constant.THICKNESS
            };

            var priceLabel = new Label
            {
                FontSize = 15,
                Text = furniture.Price.ToString(CultureInfo.CurrentCulture) + "руб.",
                Margin = Constant.THICKNESS
            };
            var descriptionLabel = new Label
            {
                FontSize = 15,
                Text = furniture.Description,
                Margin = Constant.THICKNESS
            };

            var addToShoppingCard = new Button
            {
                Text = "Добавить в корзину",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            addToShoppingCard.Clicked += OnButtonClicked;

            Content = new StackLayout
            {
                Children =
                {
                    image,
                    nameLabel,
                    sizeHitLabel,
                    priceLabel,
                    descriptionLabel,
                    addToShoppingCard
                }
            };
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Товар успешно добавлен в корзину", "Нажмите \"OK\" для продолжения", "OK");
        }
    }
}