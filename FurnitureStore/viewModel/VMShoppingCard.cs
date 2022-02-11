using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureStore.model;
using FurnitureStore.repository;

namespace FurnitureStore.viewModel
{
    public static class VmShoppingCard
    {
        private static ObservableCollection<ShoppingCard> _cards;

        public static ObservableCollection<ShoppingCard> Cards
        {
            get
            {
                var shoppingCardRepository = ShoppingCardRepository.GetInstance();
                var furnitureRepository = FurnitureRepository.GetInstance();

                var updatedCards = shoppingCardRepository.Fetch().ToList();
                updatedCards.ForEach(card => { card.Furniture = furnitureRepository.Fetch(card.FurnitureId); });

                _cards = null;
                _cards = new ObservableCollection<ShoppingCard>(updatedCards);

                return _cards;
            }
        }

        public static void UpdateList(List<ShoppingCard> newList)
        {
            _cards.Clear();
            newList.ForEach(card => _cards.Add(card));
        }        
        public static void UpdateElement(ShoppingCard card)
        {
            var first = _cards.First(shoppingCard => shoppingCard.Equals(card));
            first.Count = card.Count;
        }
    }
}