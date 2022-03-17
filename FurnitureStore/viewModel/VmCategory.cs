using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FurnitureStore.model;
using FurnitureStore.repository;
using Xamarin.Forms.Internals;

namespace FurnitureStore.viewModel
{
    public static class VmCategory
    {
        private static ObservableCollection<Category> _category;

        public static ObservableCollection<Category> Category
        {
            get
            {
                var categoryRepository = CategoryRepository.GetInstance();
                var updatedCards = categoryRepository.Fetch().ToList();

                _category = null;
                _category = new ObservableCollection<Category>(updatedCards);
                return _category;
            }
        }

        public static void UpdateList(List<Category> newList)
        {
            _category.Clear();
            newList.ForEach(card => _category.Add(card));
        }

        public static void DropList()
        {
            _category.Clear();
            var categoryRepository = CategoryRepository.GetInstance();
            categoryRepository.Fetch().ForEach(category => categoryRepository.Delete(category.Id));
        }
    }
}