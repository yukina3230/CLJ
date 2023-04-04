namespace CLJ.Models;

public class CartItem
{
    public Product Product { get; set; }

    public int CartProdQty { get; set; } = 1;
    
    public string ProductName { get; set; }

    public CartItem(Product product)
    {
        Product = product;
    }
    
    public CartItem(Product product, int productQty)
    {
        Product = product;
        CartProdQty = productQty;
    }
}