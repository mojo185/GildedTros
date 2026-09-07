namespace GilderTrosFutureProof.App.Models;

internal class GoodWineItem : BaseItem
{
    public GoodWineItem(Item item) : base(item)
    {
    }

    public override void UpdateItem()
    {
        if (Quality < MaxQuality)
            IncreaseItemQuality();

        // Update sellin
        SellIn--;
    }

    protected override int GetQualityUpdateFactor()
    {
        return 1;
    }
}
