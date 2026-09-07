namespace GilderTrosFutureProof.App.Models
{
    public abstract class BaseItem
    {
        private Item _item;

        protected const int MaxQuality = 50;
        protected const int MinQuality = 0;

        public string Name
        {
            get => _item.Name;
            protected set => _item.Name = value;
        }

        public int SellIn
        {
            get => _item.SellIn;
            protected set => _item.SellIn = value;
        }

        public int Quality
        {
            get => _item.Quality;
            protected set => _item.Quality = value;
        }

        public BaseItem(Item item)
        {
            _item = item;
        }

        public abstract void UpdateItem();

        protected abstract int GetQualityUpdateFactor();

        protected void IncreaseItemQuality()
        {
            int updateQuantity = GetQualityUpdateFactor();

            Quality = Math.Min(MaxQuality, Quality + updateQuantity);
        }

        protected void DecreaseItemQuality()
        {
            int updateQuantity = GetQualityUpdateFactor();

            Quality = Math.Max(MinQuality, Quality - updateQuantity);
        }
    }
}
