using System;
using System.Collections.Generic;
using System.Linq;
using FurnitureStore.model;
using FurnitureStore.repository;

namespace FurnitureStore.config
{
    public class DataManager
    {
        public static void FillTableDataIfEmpty()
        {
            var promoRepository = PromoRepository.GetInstance();
            if (!promoRepository.Fetch().Any())
            {
                promoRepository.SaveItem(new Promo
                {
                    Id = 0,
                    Name = "Name1",
                    Description = "Description1",
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now + TimeSpan.FromDays(7),
                    PhotoPath = "PhotoPath1"
                });
                promoRepository.SaveItem(new Promo
                {
                    Id = 0,
                    Name = "Name2",
                    Description = "Description2",
                    BeginDate = DateTime.Now,
                    EndDate = DateTime.Now + TimeSpan.FromDays(3),
                    PhotoPath = "PhotoPath2"
                });
            }

            var categoryRepository = CategoryRepository.GetInstance();
            if (!categoryRepository.Fetch().Any())
            {
                categoryRepository.SaveItem(new Category()
                {
                    Id = 0,
                    Name = "Диваны",
                    Furnitures = new List<Furniture>
                    {
                        new()
                        {
                            Id = 0,
                            Name = "Диван1",
                            Description = "Описание дивана1",
                            Height = 1,
                            Length = 1,
                            Width = 1,
                            Price = 1000,
                            PhotoPath = "SomePath1"
                        },
                        new()
                        {
                            Id = 0,
                            Name = "Диван2",
                            Description = "Описание дивана2",
                            Height = 2,
                            Length = 2,
                            Width = 2,
                            Price = 2000,
                            PhotoPath = "SomePath2"
                        }
                    }
                });
                categoryRepository.SaveItem(new Category
                {
                    Id = 0,
                    Name = "Столы",
                    Furnitures = new List<Furniture>()
                    {
                        new()
                        {
                            Id = 0,
                            Name = "Стол1",
                            Description = "Описание Стола1",
                            Height = 3,
                            Length = 3,
                            Width = 3,
                            Price = 3000,
                            PhotoPath = "SomePath3"
                        },
                        new()
                        {
                            Id = 0,
                            Name = "Стол2",
                            Description = "Описание стола2",
                            Height = 4,
                            Length = 4,
                            Width = 4,
                            Price = 4000,
                            PhotoPath = "SomePath4"
                        }
                    }
                });
            }
        }
    }
}