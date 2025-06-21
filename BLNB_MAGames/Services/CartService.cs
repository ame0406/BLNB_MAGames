using SharedParams.Tables;

namespace BLNB_MAGames.Services
{
    public class CartService
    {
        private List<Stocks> _items = new();

        public IReadOnlyList<Stocks> Items => _items;

        // 🔔 Événement appelé quand le panier change
        public event Action? OnChange;

        public void AddItem(Stocks item)
        {
            if (!_items.Any(i => i.Id == item.Id))
            {
                _items.Add(item);
                NotifyStateChanged();
            }
        }

        public void RemoveItem(int id)
        {
            _items.RemoveAll(i => i.Id == id);
            NotifyStateChanged();
        }

        public void Clear()
        {
            _items.Clear();
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }


}
