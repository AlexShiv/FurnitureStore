using FurnitureStore.config;
using FurnitureStore.repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PromoView : ContentPage
    {
        public PromoView()
        {
            InitializeComponent();
            var promoRepository = PromoRepository.GetInstance();

            var promo = promoRepository.Fetch();
            Content = new StackLayout
            {
                Children =
                {
                    new ListView
                    {
                        HasUnevenRows = true,
                        ItemsSource = promo,
                        ItemTemplate = new DataTemplate(() =>
                        {
                            var nameLabel = new Label
                            {
                                FontSize = 20,
                                Margin = Constant.THICKNESS
                            };
                            nameLabel.SetBinding(Label.TextProperty, "Name");
                            // TODO возможно слишком большые отступы
                            var descriptionLabel = new Label
                            {
                                FontSize = 15,
                                Margin = Constant.THICKNESS
                            };
                            descriptionLabel.SetBinding(Label.TextProperty, "Description");

                            return new ViewCell
                            {
                                View = new StackLayout
                                {
                                    Padding = new Thickness(0, 10),
                                    Orientation = StackOrientation.Vertical,
                                    Children = { nameLabel, descriptionLabel }
                                }
                            };
                        })
                    }
                }
            };
        }
    }
}