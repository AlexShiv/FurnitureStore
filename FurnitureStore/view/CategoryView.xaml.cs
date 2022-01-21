using System.Threading.Tasks;
using FurnitureStore.config;
using FurnitureStore.model;
using FurnitureStore.repository;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoryView : ContentPage
    {
        public CategoryView()
        {
            InitializeComponent();
            var categoryRepository = CategoryRepository.GetInstance();

            var categories = categoryRepository.Fetch();
            var listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = categories,
                ItemTemplate = new DataTemplate(() =>
                {
                    var nameLabel = new Label
                    {
                        FontSize = 18,
                        Margin = Constant.THICKNESS
                    };
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    return new ViewCell
                    {
                        View = new StackLayout()
                        {
                            Padding = new Thickness(0, 10),
                            Orientation = StackOrientation.Vertical,
                            Children = { nameLabel }
                        }
                    };
                })
            };
            listView.ItemSelected += ListView_ItemTapped;
            
            Content = new StackLayout
            {
                Children =
                {
                    listView
                }
            };
        }
        
        private async void ListView_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Category category)
                 await Navigation.PushAsync(new FurnitureView(category.Id));
        }
    }
}