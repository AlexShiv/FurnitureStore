using FurnitureStore.repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FurnitureView : ContentPage
    {
        public FurnitureView(int categoryId)
        {
            InitializeComponent();
            var categoryRepository = CategoryRepository.GetInstance();
            var furnitureRepository = FurnitureRepository.GetInstance();
            
            var category = categoryRepository.Fetch(categoryId);
            var furnitures = furnitureRepository.FetchByCategory(category);
            
            var listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = furnitures,
                ItemTemplate = new DataTemplate(() =>
                {
                    var nameLabel = new Label { FontSize = 18 };
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    var image = new Image();
                    image.HeightRequest = 100;
                    image.WidthRequest = 100;
                    // image.SetBinding(HeightProperty, "5");
                    // image.SetBinding(WidthProperty, "5");
                    image.SetBinding(Image.SourceProperty, "PhotoPath");

                    var vertical = new StackLayout()
                    {
                        Padding = new Thickness(0, 10),
                        Orientation = StackOrientation.Vertical,
                        Children = { nameLabel }
                    };

                    var horizontal = new StackLayout()
                    {
                        Padding = new Thickness(0, 10),
                        Orientation = StackOrientation.Horizontal,
                        Children = { image, vertical }
                    };
                    return new ViewCell()
                    {
                        View = horizontal
                    };
                })
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