using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FurnitureStore.viewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FurnitureStore.view
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        private Entry _loginEntry;
        private Entry _passwordEntry;

        public LoginView()
        {
            InitializeComponent();

            _loginEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };
            _passwordEntry = new Entry()
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 150,
                VerticalTextAlignment = TextAlignment.Center
            };

            var loginLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = 18,
                Text = "Номер телефона"
            };
            var login = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    loginLabel,
                    _loginEntry
                }
            };
            var passwordLabel = new Label()
            {
                HorizontalOptions = LayoutOptions.StartAndExpand,
                FontSize = 18,
                Text = "Пароль"
            };
            var password = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Children =
                {
                    passwordLabel,
                    _passwordEntry
                }
            };

            var logo = new Label()
            {
                HorizontalOptions = LayoutOptions.Center,
                Padding = 50,
                FontSize = 20,
                Text = "Мебельный магазин"
            };
            var loginButton = new Button()
            {
                Text = "Войти",
                HorizontalOptions = LayoutOptions.Center,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Button)),
                BorderWidth = 1,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Margin = 30
            };
            loginButton.Clicked += OnButtonClicked;

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.Center,
                Margin = new Thickness(20),
                Children =
                {
                    logo,
                    login,
                    password,
                    loginButton
                }
            };
        }

        private async void OnButtonClicked(object sender, EventArgs e)
        {
            if (VmUser.CheckPassword(_loginEntry.Text, _passwordEntry.Text))
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                await DisplayAlert("Ошибка", $"Не верные имя пользователя или пароль", "OK");
            }
        }
    }
}