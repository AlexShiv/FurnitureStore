using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureStore.model;
using FurnitureStore.repository;
using FurnitureStore.viewModel;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
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

            var listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = cards,
                ItemTemplate = new DataTemplate(() =>
                    {
                        var nameLabel = new Label { FontSize = 18 };
                        nameLabel.SetBinding(Label.TextProperty, "Furniture.Name");

                        var countLabel = new Label { FontSize = 18 };
                        countLabel.SetBinding(Label.TextProperty, "Count");

                        var image = new Image
                        {
                            HeightRequest = 100,
                            WidthRequest = 100
                        };
                        image.SetBinding(Image.SourceProperty, "Furniture.PhotoPath");
                        
                        var row = new FlexLayout()
                        {
                            Padding = new Thickness(0, 10),
                            Children = { image, nameLabel, countLabel },
                            WidthRequest = DeviceDisplay.MainDisplayInfo.Width
                        };
                        row.SetValue(FlexLayout.BasisProperty, "100%");
                        
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