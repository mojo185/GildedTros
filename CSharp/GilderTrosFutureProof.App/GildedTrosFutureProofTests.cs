using GilderTrosFutureProof.App.Helpers;
using GilderTrosFutureProof.App.Models;
using System.Collections.Generic;
using Xunit;

namespace GilderTrosFutureProof.App;

public class GildedTrosFutureProofTests
{
    #region Conversion Tests
    [Theory]
    [InlineData("Normal item", 10, 20, typeof(NormalItem))]
    [InlineData("B-DAWG Keychain", 10, 80, typeof(LegendaryItem))]
    [InlineData("Long Methods", 10, 20, typeof(NormalItem))]
    [InlineData("Backstage passes for Re:factor", 10, 20, typeof(BackStagePassesItem))]
    [InlineData("Good Wine", 10, 20, typeof(GoodWineItem))]
    public void ConvertSingleTest(string name, int sellIn, int quality, Type resultType)
    {
        IList<Item> Items = new List<Item> { new Item { Name = name, SellIn = sellIn, Quality = quality } };
        var convertedItems = ConversionHelper.ConvertItemsToBaseItems(Items);
        Assert.IsType(resultType, convertedItems[0]);
    }

    [Fact]
    public void MultiConvertTest()
    {
        IList<Item> Items = new List<Item> {
            new Item { Name = "Normal item", SellIn = 0, Quality = 0 },
            new Item { Name = "B-DAWG Keychain", SellIn = 3, Quality = 80 },
            new Item { Name = "Long Methods", SellIn = 5, Quality = 2 },
            new Item { Name = "Backstage passes for Re:factor", SellIn = 1, Quality = 6 },
            new Item { Name = "Good Wine", SellIn = 1, Quality = 6 },
            new Item { Name = "Backstage passes for HAXX", SellIn = 1, Quality = 6 },
            new Item { Name = "Duplicate Code", SellIn = 5, Quality = 2 },
            new Item { Name = "B-DAWG Keychain", SellIn = 3, Quality = 80 },
            new Item { Name = "Ugly Variable Names", SellIn = 5, Quality = 2 },
        };
        var convertedItems = ConversionHelper.ConvertItemsToBaseItems(Items);

        Assert.IsType<NormalItem>(convertedItems[0]);
        Assert.IsType<LegendaryItem>(convertedItems[1]);
        Assert.IsType<NormalItem>(convertedItems[2]);
        Assert.IsType<BackStagePassesItem>(convertedItems[3]);
        Assert.IsType<GoodWineItem>(convertedItems[4]);
        Assert.IsType<BackStagePassesItem>(convertedItems[5]);
        Assert.IsType<NormalItem>(convertedItems[6]);
        Assert.IsType<LegendaryItem>(convertedItems[7]);
        Assert.IsType<NormalItem>(convertedItems[8]);
    }

    [Fact]
    public void ConvertNormalItem()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Normal item", SellIn = 5, Quality = 20 } };
        var convertedItems = ConversionHelper.ConvertItemsToBaseItems(Items);

        var normalItem = Assert.IsType<NormalItem>(convertedItems[0]);

        Assert.Equal("Normal item", normalItem.Name);
        Assert.Equal(5, normalItem.SellIn);
        Assert.Equal(20, normalItem.Quality);
        Assert.False(normalItem.IsSmelly);
    }

    [Fact]
    public void ConvertIsSmellyItems()
    {
        IList<Item> items = new List<Item> {
            new Item { Name = "Long Methods", SellIn = 5, Quality = 20 },
            new Item { Name = "Duplicate Code", SellIn = 5, Quality = 20 },
            new Item { Name = "Ugly Variable Names", SellIn = 5, Quality = 20 }
        };
        var convertedItems = ConversionHelper.ConvertItemsToBaseItems(items);

        for (int i = 0; i < convertedItems.Count; i++)
        {
            var normalItem = Assert.IsType<NormalItem>(convertedItems[i]);

            Assert.Equal(items[i].Name, normalItem.Name);
            Assert.Equal(5, normalItem.SellIn);
            Assert.Equal(20, normalItem.Quality);
            Assert.True(normalItem.IsSmelly);
        }
    }

    [Fact]
    public void ConvertGoodWineItem()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Good Wine", SellIn = 5, Quality = 20 } };
        var convertedItems = ConversionHelper.ConvertItemsToBaseItems(Items);

        var goodWine = Assert.IsType<GoodWineItem>(convertedItems[0]);

        Assert.Equal("Good Wine", goodWine.Name);
        Assert.Equal(5, goodWine.SellIn);
        Assert.Equal(20, goodWine.Quality);
    }

    [Fact]
    public void ConvertLegendaryItem()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "B-DAWG Keychain", SellIn = 5, Quality = 20 } };
        var convertedItems = ConversionHelper.ConvertItemsToBaseItems(Items);

        var legendary = Assert.IsType<LegendaryItem>(convertedItems[0]);

        Assert.Equal("B-DAWG Keychain", legendary.Name);
        Assert.Equal(5, legendary.SellIn);
        Assert.Equal(20, legendary.Quality);
    }

    [Fact]
    public void ConvertBackstagePassesItem()
    {
        IList<Item> Items = new List<Item> { new Item { Name = "Backstage passes for Re:factor", SellIn = 5, Quality = 20 } };
        var convertedItems = ConversionHelper.ConvertItemsToBaseItems(Items);

        var backstagePass = Assert.IsType<BackStagePassesItem>(convertedItems[0]);

        Assert.Equal("Backstage passes for Re:factor", backstagePass.Name);
        Assert.Equal(5, backstagePass.SellIn);
        Assert.Equal(20, backstagePass.Quality);
    }
    #endregion

    #region UpdateQuality Tests

    #region normal and smelly item tests
    [Fact]
    public void UpdateNormalItem()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 20, SellIn = 5, Name = "This is a normal item" }, false) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(19, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateNormalItemTo0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 1, SellIn = 5, Name = "This is a normal item" }, false) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(0, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void TryUpdateNormalItemBelow0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 0, SellIn = 5, Name = "This is a normal item" }, false) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(0, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateNormalItemSellinIs0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 20, SellIn = 0, Name = "This is a normal item" }, false) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(19, items[0].Quality);
        Assert.Equal(-1, items[0].SellIn);
    }

    [Fact]
    public void UpdateNormalItemSellinBelow0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 20, SellIn = -1, Name = "This is a normal item" }, false) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(18, items[0].Quality);
        Assert.Equal(-2, items[0].SellIn);
    }

    [Fact]
    public void UpdateSmellyItem()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 20, SellIn = 5, Name = "Long Methods" }, true) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(18, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateSmellyItemTo0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 2, SellIn = 5, Name = "Long Methods" }, true) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(0, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateSmellyItemTo0From1()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 1, SellIn = 5, Name = "Long Methods" }, true) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(0, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void TryUpdateSmellyItemBelow0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 0, SellIn = 5, Name = "Long Methods" }, true) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(0, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateSmellyItemSellinIs0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 20, SellIn = 0, Name = "Long Methods" }, true) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(18, items[0].Quality);
        Assert.Equal(-1, items[0].SellIn);
    }

    [Fact]
    public void UpdateSmellyItemSellinBelow0()
    {
        List<BaseItem> items = new List<BaseItem> { new NormalItem(new Item() { Quality = 20, SellIn = -1, Name = "Long Methods" }, true) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(16, items[0].Quality);
        Assert.Equal(-2, items[0].SellIn);
    }
    #endregion

    #region good wine tests
    [Fact]
    public void UpdateGoodWineItem()
    {
        List<BaseItem> items = new List<BaseItem> { new GoodWineItem(new Item() { Quality = 20, SellIn = 5, Name = "Good Wine" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(21, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateGoodWineItemTo50()
    {
        List<BaseItem> items = new List<BaseItem> { new GoodWineItem(new Item() { Quality = 49, SellIn = 5, Name = "Good Wine" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void TryUpdateGoodWineItemAbove50()
    {
        List<BaseItem> items = new List<BaseItem> { new GoodWineItem(new Item() { Quality = 50, SellIn = 5, Name = "Good Wine" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void TryUpdateGoodWineItemSellinBelow0()
    {
        List<BaseItem> items = new List<BaseItem> { new GoodWineItem(new Item() { Quality = 40, SellIn = -1, Name = "Good Wine" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(41, items[0].Quality);
        Assert.Equal(-2, items[0].SellIn);
    }

    [Fact]
    public void UpdateGoodWineItemFrom0()
    {
        List<BaseItem> items = new List<BaseItem> { new GoodWineItem(new Item() { Quality = 0, SellIn = 15, Name = "Good Wine" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(1, items[0].Quality);
        Assert.Equal(14, items[0].SellIn);
    }
    #endregion region

    #region BackstagePasses tests
    [Fact]
    public void UpdateBackstagePassesItem()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = 11, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(20, items[0].Quality);
        Assert.Equal(10, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemSellinIs10()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = 10, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(22, items[0].Quality);
        Assert.Equal(9, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemSellinIsBelow10()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = 9, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(22, items[0].Quality);
        Assert.Equal(8, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemSellinIs6()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = 6, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(22, items[0].Quality);
        Assert.Equal(5, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemSellinIs5()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = 5, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(23, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemSellinIsBelow5()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = 4, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(23, items[0].Quality);
        Assert.Equal(3, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemSellinIs0()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = 0, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(23, items[0].Quality);
        Assert.Equal(-1, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemSellinIsBelow0()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 20, SellIn = -1, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(0, items[0].Quality);
        Assert.Equal(-2, items[0].SellIn);
    }

    [Fact]
    public void TryUpdateBackstagePassesItemQualityAbove50SellinAbove10()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 50, SellIn = 11, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(10, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemQualityTo50SellinBelow10()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 48, SellIn = 9, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(8, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemQualityTo50From49SellinBelow10()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 49, SellIn = 9, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(8, items[0].SellIn);
    }

    [Fact]
    public void TryUpdateBackstagePassesItemQualityAbove50SellinBelow10()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 50, SellIn = 9, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(8, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemQualityTo50SellinBelow5()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 48, SellIn = 4, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(3, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemQualityTo50From49SellinBelow5()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 49, SellIn = 4, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(3, items[0].SellIn);
    }

    [Fact]
    public void UpdateBackstagePassesItemQualityTo50From48SellinBelow5()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 48, SellIn = 4, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(3, items[0].SellIn);
    }

    [Fact]
    public void TryUpdateBackstagePassesItemQualityAbove50SellinBelow5()
    {
        List<BaseItem> items = new List<BaseItem> { new BackStagePassesItem(new Item() { Quality = 50, SellIn = 4, Name = "Backstage passes for Re:factor" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(50, items[0].Quality);
        Assert.Equal(3, items[0].SellIn);
    }
    #endregion

    #region LegendaryItem tests
    [Fact]
    public void UpdateLegendaryItem()
    {
        List<BaseItem> items = new List<BaseItem> { new LegendaryItem(new Item() { Quality = 80, SellIn = 5, Name = "B-DAWG Keychain" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(80, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }

    [Fact]
    public void UpdateLegendaryItemSellinNegative()
    {
        List<BaseItem> items = new List<BaseItem> { new LegendaryItem(new Item() { Quality = 80, SellIn = -1, Name = "B-DAWG Keychain" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(80, items[0].Quality);
        Assert.Equal(-2, items[0].SellIn);
    }

    [Fact]
    public void UpdateLegendaryItemQualityWrong()
    {
        List<BaseItem> items = new List<BaseItem> { new LegendaryItem(new Item() { Quality = 20, SellIn = 5, Name = "B-DAWG Keychain" }) };

        GildedTros.UpdateQuality(items);

        Assert.Equal(80, items[0].Quality);
        Assert.Equal(4, items[0].SellIn);
    }
    #endregion

    #region
    [Fact]
    public void UpdateQualityOfMultipleItems()
    {
        List<BaseItem> items = new()
        {
            new NormalItem( new Item { Name = "Normal", SellIn = 5, Quality = 20 }, false),
            new NormalItem( new Item { Name = "Long Methods", SellIn = 5, Quality = 20 }, true),
            new GoodWineItem( new Item { Name = "Good Wine", SellIn = 5, Quality = 20 }),
            new LegendaryItem( new Item { Name = "B-DAWG Keychain", SellIn = 5, Quality = 80 }),
            new BackStagePassesItem( new Item { Name = "Backstage passes for Re:factor", SellIn = 10, Quality = 20 })
        };

        GildedTros.UpdateQuality(items);

        Assert.Equal(19, items[0].Quality);
        Assert.Equal(18, items[1].Quality);
        Assert.Equal(21, items[2].Quality);
        Assert.Equal(80, items[3].Quality);
        Assert.Equal(22, items[4].Quality);
    }
    #endregion

    #endregion
}