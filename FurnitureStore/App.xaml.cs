using System;
using FurnitureStore.config;
using FurnitureStore.repository;
using FurnitureStore.view;
using Xamarin.Forms;

namespace FurnitureStore
{
    public partial class App : Application
    {
        public App()
        {
            try
            {
                InitializeComponent();

                PromoRepository.GetInstance();
                CategoryRepository.GetInstance();
                FurnitureRepository.GetInstance();
                CityRepository.GetInstance();
                AddressRepository.GetInstance();
                UserRepository.GetInstance();
                ShoppingCardRepository.GetInstance();

                MainPage = new LoginView();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}