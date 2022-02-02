using System;
using System.Globalization;
using System.Linq;
using FurnitureStore.config;
using FurnitureStore.model;
using FurnitureStore.repository;
using FurnitureStore.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FurnitureDetailView : ContentPage
    {
        private readonly Furniture _furniture;

        public FurnitureDetailView(Furniture furniture)
        {
            InitializeComponent();

            _furniture = furniture;
            var image = new Image
            {
                HeightRequest = 200,
                WidthRequest = 200,
                Source = _furniture.PhotoPath
            };

            var nameLabel = new Label
            {
                FontSize = 20,
                Text = _furniture.Name,
                Margin = Constant.THICKNESS
            };

            var sizeHitLabel = new Label
            {
                FontSize = 15,
                Text = $"ВxДхШ: {_furniture.Height}x{_furniture.Length}x{_furniture.Width}",
                Margin = Constant.THICKNESS
            };

            var priceLabel = new Label
            {
                FontSize = 15,
                Text = _furniture.Price.ToString(CultureInfo.CurrentCulture) + "руб.",
                Margin = Constant.THICKNESS
            };
            var descriptionLabel = new Label
            {
                FontSize = 15,
                Text = _furniture.Description,
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
            var shoppingCardRepository = ShoppingCardRepository.GetInstance();
            var furnitureRepository = FurnitureRepository.GetInstance();
            
            shoppingCardRepository.AddOrIncrementCount(_furniture);

            var shoppingCards = shoppingCardRepository.Fetch().ToList();
            shoppingCards.ForEach(card =>
            {
                card.Furniture = furnitureRepository.Fetch(card.FurnitureId);
            });
            
            VmShoppingCard.UpdateList(shoppingCards);

            await DisplayAlert("Товар успешно добавлен в корзину", "Нажмите \"OK\" для продолжения", "OK");
        }
    }
}