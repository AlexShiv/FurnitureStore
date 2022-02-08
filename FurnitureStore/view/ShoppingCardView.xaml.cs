using FurnitureStore.viewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingCardView : ContentPage
    {
        public ShoppingCardView()
        {
            InitializeComponent();

            var cards = VmShoppingCard.Cards;
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            var listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = cards,
                ItemTemplate = new DataTemplate(() =>
                    {
                        var nameLabel = new Label
                        {
                            FontSize = 18,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                        };
                        nameLabel.SetBinding(Label.TextProperty, "Furniture.Name");

                        var priceLabel = new Label
                        {
                            FontSize = 18,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        priceLabel.SetBinding(Label.TextProperty, "Furniture.Price");
                        
                        var countLabel = new Label
                        {
                            FontSize = 15,
                            // BackgroundColor = Color.Chartreuse,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                        };
                        countLabel.SetBinding(Label.TextProperty, "Count");

                        var image = new Image
                        {
                            HeightRequest = 100,
                            WidthRequest = 100,
                            // BackgroundColor = Color.Brown,
                        };
                        image.SetBinding(Image.SourceProperty, "Furniture.PhotoPath");


                        var buttons = new StackLayout()
                        {
                            // BackgroundColor = Color.Blue,
                            Orientation = StackOrientation.Vertical,
                            Children =
                            {
                                new Button()
                                {
                                    HeightRequest = 40,
                                    WidthRequest = 40,
                                    Text = "+",
                                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                                    BorderWidth = 1,
                                },
                                countLabel,
                                new Button()
                                {
                                    HeightRequest = 40,
                                    WidthRequest = 40,
                                    Text = "-",
                                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                                    BorderWidth = 1,
                                }
                            },
                            Padding = new Thickness(10),
                        };
                        var row = new StackLayout()
                        {
                            // BackgroundColor = Color.Chocolate,
                            Orientation = StackOrientation.Horizontal,
                            Padding = new Thickness(10, 10),
                            Children = { image, nameLabel, priceLabel, buttons },
                            WidthRequest = mainDisplayInfo.Width,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            
                        };

                        // TODO название наверху
                        // TODO добавить кнопку удаления товара из корзины
                        return new ViewCell
                        {
                            View = row
                        };
                    }
                ),
                BindingContext = cards
            };


            Content = new StackLayout
            {
                Children =
                {
                    listView
                }
            };
        }
    }
}