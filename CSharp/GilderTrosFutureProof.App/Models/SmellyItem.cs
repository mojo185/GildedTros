namespace GilderTrosFutureProof.App.Models
{
    internal sealed class SmellyItem : NormalItem
    {
        public SmellyItem(Item item) : base(item)
        {
        }

        public override void UpdateItem()
        {
            if (Quality > MinQuality)
            {
                DecreaseItemQuality();
            }

            // Update sellin
            SellIn--;
        }

        protected override int GetQualityUpdateFactor()
        {
            int degradation = base.GetQualityUpdateFactor();
            return degradation * 2;
        }
    }
}
