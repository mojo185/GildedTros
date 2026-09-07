namespace GilderTrosFutureProof.App.Models
{
    internal class BackStagePassesItem : BaseItem
    {
        public BackStagePassesItem(Item item) : base(item)
        {
        }

        public override void UpdateItem()
        { 
            // If sellin has passed
            if (SellIn < 0)
            {
                if (Quality != MinQuality)
                    Quality = MinQuality;
            }
            else
            {
                IncreaseItemQuality();
            }

            // Update sellin
            SellIn--;
        }

        protected override int GetQualityUpdateFactor()
        {
            return SellIn > 10 ? 0 : SellIn > 5 ? 2 : 3;
        }
    }
}
