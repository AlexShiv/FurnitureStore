using System;
using System.Linq;
using System.Threading.Tasks;
using FurnitureStore.config;
using FurnitureStore.model;
using FurnitureStore.repository;
using FurnitureStore.viewModel;
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

            var categories = VmCategory.Category;
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
                Text = "Добавить категорию",
                FontSize = Device.GetNamedSize(NamedSize.Micro, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                
            };
            addCategory.Clicked += OnAddButtonClicked;
            
            var removeCategory = new Button
            {
                Text = "Удалить категорию",
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
            try
            {
                string result = await DisplayPromptAsync("Добавление новой категории", "Введите название новой категории");
                var categoryRepository = CategoryRepository.GetInstance();
                categoryRepository.SaveItem(new Category()
                {
                    Name = result
                });
                VmCategory.UpdateList(categoryRepository.Fetch().ToList());
                await DisplayAlert("Добавление выполнено успешно", "Нажмите \"OK\" для продолжения", "OK");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await DisplayAlert("Ошибка при добавлении новой категории", "Нажмите \"OK\" для продолжения", "OK");
            }
        }
        private async void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            string result = await DisplayPromptAsync("Удаление категории", "Введите название удаляемой категории");
            var categoryRepository = CategoryRepository.GetInstance();
            var categories = categoryRepository.Fetch().ToList();
            if (categories.Exists(category => category.Name == result))
            {
                categoryRepository.Delete(categories.Find(category => category.Name == result).Id);
                VmCategory.UpdateList(categoryRepository.Fetch().ToList());
                await DisplayAlert("Удаление выполнено успешно", "Нажмите \"OK\" для продолжения", "OK");
            }
            else
            {
                await DisplayAlert("Категория с таким именем не обнаружена!", "Нажмите \"OK\" для продолжения", "OK");

            }
        }

        private async void ListView_ItemTapped(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Category category)
                await Navigation.PushAsync(new FurnitureView(category.Id));
        }
    }
}