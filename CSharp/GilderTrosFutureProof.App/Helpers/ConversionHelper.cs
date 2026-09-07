using GilderTrosFutureProof.App.Models;

namespace GilderTrosFutureProof.App.Helpers
{
    internal static class ConversionHelper
    {
        internal  static List<BaseItem> ConvertItemsToBaseItems(IList<Item> items)
        {
            if(items == null)
            {
                return new List<BaseItem>();
            }

            var baseItems = new List<BaseItem>();

            foreach (var item in items)
            {
                if (item.Name.Equals("B-DAWG Keychain"))
                {
                    baseItems.Add(new LegendaryItem(item));
                    continue;
                }
                else if (item.Name.Equals("Good Wine"))
                {
                    baseItems.Add(new GoodWineItem(item));
                    continue;
                }
                else if (item.Name.Contains("Backstage passes"))
                {
                    baseItems.Add(new BackStagePassesItem(item));
                    continue;
                }

                baseItems.Add(new NormalItem(item, IsSmellyItem(item.Name)));
                continue;
            }

            return baseItems;
        }

        private static bool IsSmellyItem(string itemName) => itemName.Equals("Duplicate Code") ||
                                              itemName.Equals("Long Methods") ||
                                              itemName.Equals("Ugly Variable Names");
    }
}
