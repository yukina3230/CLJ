using CLJ.Models.Services;

namespace CLJ.Models;

public class ShoppingCart : IComparable<ShoppingCart>
{
    public int CartID { get; set; }
    public List<string> ProductNameList { get; set; }
    public List<CartItem> CartProductList { get; set; }

    public ShoppingCart()
    {
        ProductNameList = new List<string>();
        CartProductList = new List<CartItem>();
    }

    // TODO
    public bool AddItem(string productName)
    {
        bool result = true;

        foreach (var prod in ShoppingService.ProductList)
        {
            if (prod.GetName() == productName)
            {
                if (prod.GetQuantity() == 0)
                {
                    result = false;
                    Console.WriteLine("This product is out of stock.");
                    break;
                }
                else
                {
                    foreach (var prodName in ProductNameList)
                    {
                        if (productName == prodName)
                        {
                            result = false;
                            Console.WriteLine("This product is already in the cart.");
                            break;
                        }
                        else
                        {
                            ProductNameList.Add(productName);
                            prod.SetQuantity(prod.GetQuantity() - 1);
                            break;
                        }
                    }
                }
            }
        }
        
        return result;
    }

    public double TotalWeight()
    {
        double totalWeight = 0;
        foreach (var prod in CartProductList)
        {
            if (prod.Product is PhysicalProduct)
            {
                totalWeight += ((PhysicalProduct)prod.Product).GetWeight();
            }
        }

        return totalWeight;
    }
    
    public double CartAmount()
    {
        const double BaseFee = 0.1;
        double shippingFee = 0, totalPrice = 0;
        
        foreach (var prod in CartProductList)
        {
            totalPrice += prod.Product.GetPrice();
        }
        shippingFee = TotalWeight() * BaseFee;
        
        return totalPrice + shippingFee;
    }

    public int CompareTo(ShoppingCart other)
    {
        if (other == null) return 1;
        return TotalWeight().CompareTo(other.TotalWeight());
    }
}