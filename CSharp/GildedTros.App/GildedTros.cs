using System.Collections.Generic;

namespace GildedTros.App
{
    public static class GildedTros
    {
        const int MaxQuality = 50;
        const int MinQuality = 0;

        static bool IsSmellyItem(string itemName) => itemName.Equals("Duplicate Code") ||
                                              itemName.Equals("Long Methods") ||
                                              itemName.Equals("Ugly Variable Names");

        static int GetBackstagePassesQualityUpdateFactor(int sellIn) => sellIn > 10 ? 1 : sellIn > 5 ? 2 : 3;

        public static void UpdateQuality(IList<Item> items)
        {
            for (int i = 0; i < items.Count; i++)
            {
                // Do not update the legendary item, since it is never sold and the quality never changes
                // it is not necessary to update it.
                if (items[i].Name.Equals("B-DAWG Keychain"))
                    continue;

                // Update good wine
                if (items[i].Name.Equals("Good Wine"))
                {
                    if (items[i].Quality < MaxQuality)
                        items[i].Quality = IncreaseItemQuality(items[i].Quality, MaxQuality, 1);
                }
                else if (items[i].Name.Contains("Backstage passes for")) // Update backstage passes

                {
                    // If sellin has passed
                    if (items[i].SellIn < 0)
                    {
                        if (items[i].Quality != MinQuality)
                            items[i].Quality = MinQuality;
                    }
                    else
                    {
                        items[i].Quality = IncreaseItemQuality(items[i].Quality, MaxQuality, GetBackstagePassesQualityUpdateFactor(items[i].SellIn));
                    }
                }
                else if (items[i].Quality > MinQuality)// Update other items (normal and smelly)
                {
                    items[i].Quality = DecreaseItemQuality(items[i].Quality, MinQuality, GetQualityDegradation(items[i].SellIn, IsSmellyItem(items[i].Name)));
                }

                // Update sellin
                items[i].SellIn--;
            }
        }

        static int IncreaseItemQuality(int startingValue, int limit, int updateQuantity)
            => startingValue >= limit ? limit : startingValue + updateQuantity > limit ? limit : startingValue + updateQuantity;

        static int DecreaseItemQuality(int startingValue, int limit, int updateQuantity)
            => startingValue <= limit ? limit : startingValue - updateQuantity < limit ? limit : startingValue - updateQuantity;

        static int GetQualityDegradation(int sellIn, bool isSmelly)
        {
            int degradation = sellIn < 0 ? 2 : 1;
            return isSmelly ? degradation * 2 : degradation;
        }
    }
}
