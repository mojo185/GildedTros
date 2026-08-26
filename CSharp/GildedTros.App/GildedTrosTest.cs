using System.Collections.Generic;
using Xunit;

namespace GildedTros.App
{
    public class GildedTrosTest
    {
        [Fact]
        public void foo()
        {
            IList<Item> Items = new List<Item> { new Item { Name = "foo", SellIn = 0, Quality = 0 } };
            GildedTros.UpdateQuality(Items);
            Assert.Equal("foo", Items[0].Name);
        }

        [Fact]
        public void NormalItem_DecreasesQualityByOne()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Normal Item",
                    SellIn = 10,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(19, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void NormalItem_AfterSellByDate_DecreasesQualityByTwo()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Normal Item",
                    SellIn = 0,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(19, items[0].Quality);
            Assert.Equal(-1, items[0].SellIn);

            GildedTros.UpdateQuality(items);

            Assert.Equal(17, items[0].Quality);
            Assert.Equal(-2, items[0].SellIn);
        }

        [Fact]
        public void NormalItem_QualityCannotBecomeNegative()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Normal Item",
                    SellIn = 10,
                    Quality = 0
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(0, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void NormalItem_QualityAtOne_BecomesZero()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Normal Item",
                    SellIn = 10,
                    Quality = 1
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(0, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void GoodWine_IncreasesQuality()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Good Wine",
                    SellIn = 10,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(21, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void GoodWine_AtQuality50_Remains50()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Good Wine",
                    SellIn = 10,
                    Quality = 50
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(50, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void GoodWine_AtQuality48_Remains50()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Good Wine",
                    SellIn = 10,
                    Quality = 49
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(50, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void LegendaryItem_DoesNotChange()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "B-DAWG Keychain",
                    SellIn = 10,
                    Quality = 80
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(10, items[0].SellIn);
            Assert.Equal(80, items[0].Quality);
        }

        [Fact]
        public void LegendaryItemNegativeSellin_DoesNotChange()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "B-DAWG Keychain",
                    SellIn = 0,
                    Quality = 80
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(0, items[0].SellIn);
            Assert.Equal(80, items[0].Quality);
        }

        [Fact]
        public void BackstagePass_MoreThan10Days_IncreasesByOne()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = 11,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(21, items[0].Quality);
            Assert.Equal(10, items[0].SellIn);
        }

        [Fact]
        public void BackstagePass_At10Days_IncreasesByTwo()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = 10,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(22, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void BackstagePass_At5Days_IncreasesByThree()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = 5,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(23, items[0].Quality);
            Assert.Equal(4, items[0].SellIn);
        }

        [Fact]
        public void BackstagePass_At6Days_IncreasesByTwo()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = 6,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(22, items[0].Quality);
        }

        [Fact]
        public void BackstagePass_AfterConcert_DropsToZero()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = -1,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(0, items[0].Quality);
            Assert.Equal(-2, items[0].SellIn);
        }

        [Fact]
        public void BackstagePass_AfterConcert_DropsToZeroAndStaysZero()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = -1,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(0, items[0].Quality);
            Assert.Equal(-2, items[0].SellIn);

            GildedTros.UpdateQuality(items);

            Assert.Equal(0, items[0].Quality);
            Assert.Equal(-3, items[0].SellIn);
        }

        [Fact]
        public void BackstagePass_AfterConcert_SellinZeroUpdateQualityBy3()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = 0,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(23, items[0].Quality);
            Assert.Equal(-1, items[0].SellIn);
        }

        [Fact]
        public void BackstagePass_CannotExceedQuality50()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = 5,
                    Quality = 48
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(50, items[0].Quality);
            Assert.Equal(4, items[0].SellIn);
        }

        [Fact]
        public void BackstagePass_CannotExceedQuality50SellinAbove5()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Backstage passes for Re:factor",
                    SellIn = 6,
                    Quality = 49
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(50, items[0].Quality);
            Assert.Equal(5, items[0].SellIn);
        }

        [Fact]
        public void SmellyItem_DegradesTwiceAsFast()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Duplicate Code",
                    SellIn = 10,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(18, items[0].Quality);
            Assert.Equal(9, items[0].SellIn);
        }

        [Fact]
        public void SmellyItem_AfterSellByDate_DegradesFourTimes()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Long Methods",
                    SellIn = -1,
                    Quality = 20
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(16, items[0].Quality);
            Assert.Equal(-2, items[0].SellIn);
        }

        [Fact]
        public void AllSmellyItems_DegradeTwiceAsFast()
        {
            var items = new List<Item>
            {
                new Item { Name = "Duplicate Code", SellIn = 10, Quality = 20 },
                new Item { Name = "Long Methods", SellIn = 10, Quality = 20 },
                new Item { Name = "Ugly Variable Names", SellIn = 10, Quality = 20 }
            };

            GildedTros.UpdateQuality(items);

            Assert.All(items, item => Assert.Equal(18, item.Quality));
        }

        [Fact]
        public void MultipleItems_AreAllUpdated()
        {
            var items = new List<Item>
            {
                new Item { Name = "Normal Item", SellIn = 10, Quality = 20 },
                new Item { Name = "Good Wine", SellIn = 10, Quality = 20 },
                new Item { Name = "B-DAWG Keychain", SellIn = 10, Quality = 80 }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(19, items[0].Quality);
            Assert.Equal(21, items[1].Quality);
            Assert.Equal(80, items[2].Quality);

            Assert.Equal(9, items[0].SellIn);
            Assert.Equal(9, items[1].SellIn);
            Assert.Equal(10, items[2].SellIn);
        }

        [Fact]
        public void ItemWithZeroQuality_RemainsAtZero()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Name = "Normal Item",
                    SellIn = 5,
                    Quality = 0
                }
            };

            GildedTros.UpdateQuality(items);

            Assert.Equal(0, items[0].Quality);
            Assert.Equal(4, items[0].SellIn);
        }
    }
}