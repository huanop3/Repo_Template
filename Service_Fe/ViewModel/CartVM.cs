public class Cart
{

    public List<CartItem> items = new List<CartItem>();

    public void AddToCart(CartItem newItem)
    {
        CartItem? item = items.Find(n => n.Id == newItem.Id);
        if (item != null)
        {
            item.Quantity += 1;

        }
        else
        {
            items.Add(newItem);

        }
    }
    public void DeleteItem(int id)
    {
        items = items.Where(x => x.Id != id).ToList();
    }
    public void UpdateItem(int id, int quantity)
    {
        CartItem? item = items.Find(x => x.Id == id);
        if (item != null)
        {
            item.Quantity = quantity;
        }
    }
    public void UpdateItemCartButton(int id, int quantity)
    {
        CartItem? item = items.Find(x => x.Id == id);
        if (item != null)
        {
            item.Quantity += quantity;
        }
    }
}
public class CartItem
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public decimal StartingPrice { get; set; }

    public string? Seller { get; set; }
    public decimal Quantity { get; set; }
}
