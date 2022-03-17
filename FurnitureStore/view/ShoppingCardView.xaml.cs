using System;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureStore.model;
using FurnitureStore.repository;
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

            var listView = buildListView(cards, mainDisplayInfo);

            var allItemPrise = new Label
            {
                FontSize = 15,
                // BackgroundColor = Color.Chartreuse,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            VmFullPrice.FullPriceLabel = allItemPrise;
            VmFullPrice.UpdateFullPrice();
            
            var clearShopingCard = new Button
            {
                Text = "Оформить заказ",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };

            var footer = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    allItemPrise, clearShopingCard
                }
            };
            Content = new StackLayout
            {
                Children =
                {
                    listView, footer
                }
            };
        }


        private ListView buildListView(ObservableCollection<ShoppingCard> cards, DisplayInfo mainDisplayInfo)
        {
            return new ListView
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

                        var fullPriceLabel = new Label
                        {
                            FontSize = 18,
                            VerticalTextAlignment = TextAlignment.Center,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand
                        };
                        fullPriceLabel.SetBinding(Label.TextProperty, "FullPrise");

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

                        var plus = new Button()
                        {
                            HeightRequest = 40,
                            WidthRequest = 40,
                            Text = "+",
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                            BorderWidth = 1,
                        };
                        plus.Clicked += OnPlusButtonClicked;

                        var minus = new Button()
                        {
                            HeightRequest = 40,
                            WidthRequest = 40,
                            Text = "-",
                            FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Button)),
                            BorderWidth = 1,
                        };
                        minus.Clicked += OnMinusButtonClicked;

                        var buttons = new StackLayout()
                        {
                            // BackgroundColor = Color.Blue,
                            Orientation = StackOrientation.Vertical,
                            Children =
                            {
                                plus,
                                countLabel,
                                minus
                            },
                            Padding = new Thickness(10),
                        };

                        var row = new StackLayout()
                        {
                            // BackgroundColor = Color.Chocolate,
                            Orientation = StackOrientation.Horizontal,
                            Padding = new Thickness(10, 10),
                            Children = { image, nameLabel, fullPriceLabel, buttons },
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
        }

        private void OnPlusButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var row = (ShoppingCard)button.Parent.Parent.Parent.BindingContext;

            var shoppingCardRepository = ShoppingCardRepository.GetInstance();
            var furnitureRepository = FurnitureRepository.GetInstance();

            row.Count++;
            row.FullPrise = row.Count * row.Furniture.Price;
            shoppingCardRepository.SaveItem(row);

            var updatedCards = shoppingCardRepository.Fetch().ToList();
            updatedCards.ForEach(card => { card.Furniture = furnitureRepository.Fetch(card.FurnitureId); });

            VmShoppingCard.UpdateList(updatedCards);
        }

        private void OnMinusButtonClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            var row = (ShoppingCard)button.Parent.Parent.Parent.BindingContext;

            var shoppingCardRepository = ShoppingCardRepository.GetInstance();
            var furnitureRepository = FurnitureRepository.GetInstance();

            row.Count--;
            row.FullPrise = row.Count * row.Furniture.Price;

            if (row.Count == 0)
            {
                shoppingCardRepository.Delete(row.Id);
            }
            else
            {
                shoppingCardRepository.SaveItem(row);
            }

            var updatedCards = shoppingCardRepository.Fetch().ToList();
            updatedCards.ForEach(card => { card.Furniture = furnitureRepository.Fetch(card.FurnitureId); });

            VmShoppingCard.UpdateList(updatedCards);
        }
    }
}