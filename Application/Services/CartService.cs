namespace Application.Services;

/// <summary>
/// In-memory cart service scoped to a SignalR circuit (one per user session).
/// </summary>
public class CartService
{
    private readonly List<CartItem> _items = new();

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
    public int ItemCount => _items.Sum(i => i.Quantity);
    public decimal Total => _items.Sum(i => i.Price * i.Quantity);

    public event Action? OnChange;

    public void AddItem(int productId, string productName, decimal price, string? imageUrl = null, int quantity = 1)
    {
        var existing = _items.FirstOrDefault(i => i.ProductId == productId);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                ProductId = productId,
                ProductName = productName,
                Price = price,
                ImageUrl = imageUrl,
                Quantity = quantity
            });
        }
        OnChange?.Invoke();
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item == null) return;

        if (quantity <= 0)
            _items.Remove(item);
        else
            item.Quantity = quantity;

        OnChange?.Invoke();
    }

    public void RemoveItem(int productId)
    {
        _items.RemoveAll(i => i.ProductId == productId);
        OnChange?.Invoke();
    }

    public void Clear()
    {
        _items.Clear();
        OnChange?.Invoke();
    }
}

public class CartItem
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public int Quantity { get; set; } = 1;
}
