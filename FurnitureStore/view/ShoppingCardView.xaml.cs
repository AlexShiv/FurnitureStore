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
                            WidthRequest = mainDisplayInfo.Width / 4,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center
                        };
                        nameLabel.SetBinding(Label.TextProperty, "Furniture.Name");

                        var priceLabel = new Label
                        {
                            FontSize = 18,
                            WidthRequest = mainDisplayInfo.Width / 4,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center
                        };
                        priceLabel.SetBinding(Label.TextProperty, "Furniture.Price");
                        
                        var countLabel = new Label
                        {
                            FontSize = 18,
                            WidthRequest = mainDisplayInfo.Width / 4,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            BackgroundColor = Color.Chartreuse,
                        };
                        countLabel.SetBinding(Label.TextProperty, "Count");

                        var image = new Image
                        {
                            HeightRequest = 100,
                            WidthRequest = mainDisplayInfo.Width / 4,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            BackgroundColor = Color.Brown,
                            Margin = new Thickness(0,30)
                        };
                        image.SetBinding(Image.SourceProperty, "Furniture.PhotoPath");


                        var buttons = new StackLayout()
                        {
                            BackgroundColor = Color.Blue,
                            Orientation = StackOrientation.Vertical,
                            Children =
                            {
                                new Button()
                                {
                                    Text = "+",
                                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                                    BorderWidth = 1,
                                    Margin = new Thickness(10),
                                },
                                countLabel,
                                new Button()
                                {
                                    Text = "-",
                                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                                    BorderWidth = 1,
                                    Margin = new Thickness(10),
                                }
                            },
                            WidthRequest = mainDisplayInfo.Width / 4.5,
                        };
                        var row = new FlexLayout()
                        {
                            Padding = new Thickness(10, 10),
                            Children = { image, nameLabel, priceLabel, buttons },
                            WidthRequest = mainDisplayInfo.Width,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            
                        };

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