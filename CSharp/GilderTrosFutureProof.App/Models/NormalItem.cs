namespace GilderTrosFutureProof.App.Models
{
    internal class NormalItem : BaseItem
    {
        public bool IsSmelly { get; private set; }

        public NormalItem(Item item, bool isSmelly) : base(item)
        {
            IsSmelly = isSmelly;
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
            int degradation = SellIn < 0 ? 2 : 1;
            return IsSmelly ? degradation * 2 : degradation;
        }
    }
}
