using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using FurnitureStore.model;
using FurnitureStore.repository;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateFurniture : ContentPage
    {
        private readonly int _categoryId;

        private Entry _nameEntry;
        private Entry _descriptionEntry;
        private Entry _lengthEntry;
        private Entry _heightEntry;
        private Entry _widthEntry;
        private Entry _priceEntry;
        private string _photoPath;

        public CreateFurniture(int categoryId)
        {
            _categoryId = categoryId;
            InitializeComponent();

            _nameEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center,
            };
            var name = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        FontSize = 18,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Text = "Название",
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    },
                    _nameEntry
                }
            };

            _descriptionEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var description = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Описание"
                    },
                    _descriptionEntry
                }
            };

            _lengthEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var length = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Длина"
                    },
                    _lengthEntry
                }
            };

            _heightEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var height = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Высота"
                    },
                    _heightEntry
                }
            };

            _widthEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var width = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Ширина"
                    },
                    _widthEntry
                }
            };

            _priceEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var price = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Цена"
                    },
                    _priceEntry
                }
            };

            var photoButton = new Button()
            {
                Text = "Выбрать файл",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                BorderWidth = 1,
            };
            photoButton.Clicked += PickPhoto;
            var photo = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Фотография"
                    },
                    photoButton
                }
            };

            var createFurniture = new Button
            {
                Text = "Создать товар",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            createFurniture.Clicked += OnButtonClicked;

            Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(10),
                Children =
                {
                    name,
                    description,
                    length,
                    height,
                    width,
                    price,
                    photo,
                    createFurniture
                }
            };
        }

        private async void PickPhoto(object sender, EventArgs e)
        {
            var options = PickOptions.Images;
            var result = await FilePicker.PickAsync(options);
            if (result != null)
            {
                _photoPath = result.FullPath;
                // var stream = await result.OpenReadAsync();
                // var memoryStream = new MemoryStream();
                // await stream.CopyToAsync(memoryStream);
                // _photoPath = memoryStream.ToArray();
                // var readBytes = new BinaryReader(stream).ReadBytes();
                Console.WriteLine("qwqw");
            }
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var furniture = new Furniture()
                {
                    Name = _nameEntry.Text,
                    Description = _descriptionEntry.Text,
                    Length = Convert.ToDouble(_lengthEntry.Text),
                    Height = Convert.ToDouble(_heightEntry.Text),
                    Width = Convert.ToDouble(_widthEntry.Text),
                    Price = Convert.ToDouble(_priceEntry.Text),
                    PhotoPath = _photoPath,
                    CategoryId = _categoryId
                };

                var furnitureRepository = FurnitureRepository.GetInstance();
                furnitureRepository.SaveItem(furniture);
                await DisplayAlert("Товар успешно добавлен", "Нажмите \"OK\" для продолжения", "OK");
                
                await Navigation.PushAsync(new FurnitureView(_categoryId));

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await DisplayAlert("При создании товара произошла ошибка!", "Нажмите \"OK\" для продолжения", "OK");
            }
        }
    }
}