using System.Globalization;
using System.Linq;
using FurnitureStore.repository;
using Xamarin.Forms;

namespace FurnitureStore.viewModel
{
    public static class VmFullPrice
    {
        private static Label _fullPriceLabel;

        public static Label FullPriceLabel
        {
            get => _fullPriceLabel;
            set => _fullPriceLabel = value;
        }

        public static void UpdateFullPrice()
        {
            var shoppingCardRepository = ShoppingCardRepository.GetInstance();

            _fullPriceLabel.Text = "итог: " + shoppingCardRepository.Fetch().ToList().Sum(card => card.FullPrise);
        }
    }
}