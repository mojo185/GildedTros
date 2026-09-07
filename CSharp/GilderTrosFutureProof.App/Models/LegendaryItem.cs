namespace GilderTrosFutureProof.App.Models;

internal class LegendaryItem : BaseItem
{
    protected new const int MaxQuality = 80;

    public LegendaryItem(Item item) : base(item)
    {
    }

    public override void UpdateItem()
    {
        // Do not update the legendary item, since it is never sold and the quality never changes
        // it is not necessary to update it.
        if(Quality != MaxQuality)
        {
            Quality = MaxQuality;
        }

        SellIn--;

        return;
    }

    protected override int GetQualityUpdateFactor()
    {
        return 0;
    }
}
