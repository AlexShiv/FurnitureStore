using System;
using System.Linq;
using System.Runtime.Serialization;
using FurnitureStore.model;
using FurnitureStore.repository;
using FurnitureStore.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PersonalAccount : ContentPage
    {
        private readonly User _user;
        private Entry _lastNameEntry;
        private Entry _firstNameEntry;
        private Entry _patronomicEntry;
        private DatePicker _birthdayDatePicker;
        private Entry _phoneEntry;
        private Switch _subscribeSwitch;
        private string _city;
        private string _address;
        private Picker _addressPicker;
        private Picker _cityPicker;

        public PersonalAccount()
        {
            InitializeComponent();

            var userRepository = UserRepository.GetInstance();
            var cityRepository = CityRepository.GetInstance();
            var addressRepository = AddressRepository.GetInstance();

            _user = VmUser.User;
            var cities = cityRepository.Fetch();
            var addresses = addressRepository.Fetch().ToList();
            cities.ForEach(city => city.Addresses = addresses.FindAll(address => address.CityId == city.Id).ToList());


            _lastNameEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Text = _user.LastName,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center,
            };
            var lastName = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        FontSize = 18,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Text = "Фамилия",
                        HorizontalTextAlignment = TextAlignment.Center,
                        VerticalTextAlignment = TextAlignment.Center
                    },
                    _lastNameEntry
                }
            };

            _firstNameEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Text = _user.FirstName,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var firstName = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Имя"
                    },
                    _firstNameEntry
                }
            };

            _patronomicEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Text = _user.Patronymic,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var patronymic = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Отчество"
                    },
                    _patronomicEntry
                }
            };

            _birthdayDatePicker = new DatePicker()
            {
                Format = "dd/MM/yyyy",
                HorizontalOptions = LayoutOptions.Center,
                Date = _user.Birthday,
                WidthRequest = 150,
            };
            var birthday = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "День рождения"
                    },
                    _birthdayDatePicker
                }
            };

            _phoneEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Text = _user.Phone,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            var phone = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Номер телефона"
                    },
                    _phoneEntry
                }
            };

            _cityPicker = new Picker()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Title = "Город",
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            cities.Select(city => city.Name).ToList()
                .ForEach(s => _cityPicker.Items.Add(s));
            _cityPicker.SelectedIndexChanged += picker_SelectedIndexChanged;
            var cityLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Город"
                    },
                    _cityPicker
                }
            };

            _addressPicker = new Picker()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                Title = "Адрес",
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };

            var addressLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label()
                    {
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        FontSize = 18,
                        Text = "Адрес"
                    },
                    _addressPicker
                }
            };

            _subscribeSwitch = new Switch()
            {
                IsToggled = _user.IsSubscribe,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150
            };
            var subscribe = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    new Label() { FontSize = 18, Text = "Подписаться на рассылку" },
                    _subscribeSwitch
                }
            };

            var saveUserData = new Button
            {
                Text = "Сохранить изменения",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            saveUserData.Clicked += OnButtonClicked;

            Content = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Margin = new Thickness(10),
                Children =
                {
                    lastName,
                    firstName,
                    patronymic,
                    birthday,
                    phone,
                    subscribe,
                    cityLayout,
                    addressLayout,
                    saveUserData
                }
            };
        }

        private void picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var cityName = _cityPicker.Items[_cityPicker.SelectedIndex];
            var addressRepository = AddressRepository.GetInstance();
            var cityRepository = CityRepository.GetInstance();
            _addressPicker.Items.Clear();

            var city = cityRepository.Fetch().First(c => c.Name == cityName);
            addressRepository.Fetch().ToList().FindAll(address => address.CityId == city.Id)
                .ForEach(address => _addressPicker.Items.Add(address.Name));
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                var addresses = AddressRepository.GetInstance().Fetch().ToList();
                _user.LastName = _lastNameEntry.Text;
                _user.FirstName = _firstNameEntry.Text;
                _user.Patronymic = _patronomicEntry.Text;
                _user.Birthday = _birthdayDatePicker.Date;
                _user.Phone = _phoneEntry.Text;
                _user.IsSubscribe = _subscribeSwitch.IsToggled;
                _user.AddressId = addresses.Find(address =>
                    address.Name == _addressPicker.Items[_addressPicker.SelectedIndex]).Id;

                var userRepository = UserRepository.GetInstance();
                userRepository.SaveItem(_user);
                await DisplayAlert("Данные пользователя успешно сохранены", "Нажмите \"OK\" для продолжения", "OK");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                await DisplayAlert("При сохранении данных произошла ошибка!", "Нажмите \"OK\" для продолжения", "OK");
            }
        }
    }
}