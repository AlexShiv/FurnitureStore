using System;
using FurnitureStore.config;
using FurnitureStore.repository;
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

                UserRepository.GetInstance();
                PromoRepository.GetInstance();
                CategoryRepository.GetInstance();
                ShoppingCardRepository.GetInstance();
                CityRepository.GetInstance();
                AddressRepository.GetInstance();
                FurnitureRepository.GetInstance();

                MainPage = new MainPage();
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