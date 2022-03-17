using System;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureStore.model;
using FurnitureStore.repository;
using FurnitureStore.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FurnitureView : ContentPage
    {
        private ObservableCollection<Furniture> _furnitures;
        private int categoryId;

        public FurnitureView(int categoryId)
        {
            InitializeComponent();
            this.categoryId = categoryId;
            var categoryRepository = CategoryRepository.GetInstance();
            var furnitureRepository = FurnitureRepository.GetInstance();

            var category = categoryRepository.Fetch(categoryId);
            _furnitures = new ObservableCollection<Furniture>(furnitureRepository.FetchByCategory(category));

            var listView = new ListView
            {
                HasUnevenRows = true,
                ItemsSource = _furnitures,
                ItemTemplate = new DataTemplate(() =>
                {
                    var nameLabel = new Label { FontSize = 18 };
                    nameLabel.SetBinding(Label.TextProperty, "Name");

                    var image = new Image();
                    image.HeightRequest = 100;
                    image.WidthRequest = 100;
                    // image.SetBinding(HeightProperty, "5");
                    // image.SetBinding(WidthProperty, "5");
                    image.SetBinding(Image.SourceProperty, "PhotoPath"); // TODO replace to binary array

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
            listView.ItemSelected += ListView_ItemTapped;


            var footer = BuildFooter();
            if (VmUser.IsAdmin())
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        listView, footer
                    }
                };
            }
            else
            {
                Content = new StackLayout
                {
                    Children =
                    {
                        listView
                    }
                };
            }
        }

        private StackLayout BuildFooter()
        {
            var addCategory = new Button
            {
                Text = "Добавить товар",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            addCategory.Clicked += OnAddButtonClicked;

            var removeCategory = new Button
            {
                Text = "Удалить товар",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };
            removeCategory.Clicked += OnRemoveButtonClicked;

            return new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Margin = 10,
                Children =
                {
                    addCategory, removeCategory
                }
            };
        }

        private async void OnAddButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateFurniture(categoryId));
        }

        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Удаление товара", "Введите название удаляемого товара");
            var furnitureRepository = FurnitureRepository.GetInstance();
            var categoryRepository = CategoryRepository.GetInstance();
            
            var currentCategory = categoryRepository.Fetch(categoryId);

            var furnitures = furnitureRepository.FetchByCategory(currentCategory);
            if (furnitures.Exists(furniture => furniture.Name == result))
            {
                furnitureRepository.Delete(furnitures.Find(category => category.Name == result).Id);
                _furnitures.Clear();
                furnitureRepository.FetchByCategory(currentCategory).ForEach(furniture => _furnitures.Add(furniture));
                await DisplayAlert("Удаление выполнено успешно", "Нажмите \"OK\" для продолжения", "OK");
            }
            else
            {
                await DisplayAlert("Категория с таким именем не обнаружена!", "Нажмите \"OK\" для продолжения", "OK");
            }
        }

        private async void ListView_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Furniture furniture)
            {
                ((ListView)sender).SelectedItem = null;
                await Navigation.PushAsync(new FurnitureDetailView(furniture));
            }
        }
    }
}