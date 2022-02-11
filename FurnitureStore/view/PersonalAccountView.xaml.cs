using System;
using System.Linq;
using System.Runtime.Serialization;
using FurnitureStore.model;
using FurnitureStore.repository;
using Xamarin.Forms;
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

        public PersonalAccount()
        {
            InitializeComponent();

            var userRepository = UserRepository.GetInstance();
            _user = userRepository.Fetch().First();

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
                    saveUserData
                }
            };
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            try
            {
                _user.LastName = _lastNameEntry.Text;
                _user.FirstName = _firstNameEntry.Text;
                _user.Patronymic = _patronomicEntry.Text;
                _user.Birthday = _birthdayDatePicker.Date;
                _user.Phone = _phoneEntry.Text;
                _user.IsSubscribe = _subscribeSwitch.IsToggled;

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