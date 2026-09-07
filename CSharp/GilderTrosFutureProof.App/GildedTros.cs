using GilderTrosFutureProof.App.Models;
using System.Collections.Generic;

namespace GilderTrosFutureProof.App;

internal class GildedTros
{
    public static void UpdateQuality(IList<BaseItem> items)
    {
        if(items == null)
        {
            return;
        }

        foreach(var item in items)
        {
            item.UpdateItem();
        }
    }
}
