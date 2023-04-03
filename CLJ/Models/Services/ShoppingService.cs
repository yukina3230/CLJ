using System.Text.RegularExpressions;

namespace CLJ.Models.Services;

public class ShoppingService
{
    public static List<Product> ProductList;
    public static List<ShoppingCart> CartList;
    public int CurrentCartID { get; set; }
    
    public ShoppingService()
    {
        ProductList = new List<Product>();
        CartList = new List<ShoppingCart>();
    }
    
    public void CreateProduct(Product product)
    {
        ProductList.Add(product);
    }
    
    // public void EditProduct(int index, string name, string description, string message, int qty, double price, double weight)
    // {
    //     for (int i = 0; i < ProductList.Count; i++)
    //     {
    //         if (index == i)
    //         {
    //             if (ProductList[i] is PhysicalProduct)
    //             {
    //                 ProductList[i] = new PhysicalProduct(name, description, message, qty, price, weight);
    //             }
    //             else
    //             {
    //                 ProductList[i] = new DigitalProduct(name, description, message, qty, price);
    //             }
    //         }
    //     }
    // }

    public void AddProductToCurrentCart(int productIndex, string msg)
    {
        var product = GetProductList[productIndex];
        ((IGiftable)product).SetMessage(msg);

        foreach (var item in GetCartList)
        {
            if (item.CartID == CurrentCartID)
            {
                item.AddItem(product.GetName());
                item.CartProductList.Add(new CartItem(product));
                break;
            }
        }
    }
    
    public void AddCart()
    {
        GetCartList.Add(new ShoppingCart() {CartID = GetCartList.Count + 1});
    }

    public double GetCartAmount(int cartID)
    {
        double result = 0;
        foreach (var item in GetCartList)
        {
            if (item.CartID == cartID)
            {
                result = item.CartAmount();
                break;
            }
        }

        return result;
    }
    
    public int GetTotalCarts() => GetCartList.Count;

    public List<Product> GetProductList => ProductList;

    public List<ShoppingCart> GetCartList => CartList;

    public T Authorize<T>(string text, string regex)
    {
        bool valid = false;
        string value = "";
    
        while (!valid)
        {
            Console.Write(text);
            value = Console.ReadLine();

            if (!Regex.IsMatch(value, regex))
            {
                Console.Write("Input is invalid. Please try again!");
                Console.ReadKey();
                Console.WriteLine();
            }
            else if (String.IsNullOrWhiteSpace(value) && typeof(T) == typeof(string))
            {
                value = "";
                valid = true;
            }
            else if (String.IsNullOrWhiteSpace(value) && typeof(T) != typeof(string))
            {
                value = "0";
                valid = true;
            }
            else valid = true;
        }

        return (T)Convert.ChangeType(value, typeof(T));
    }
    
    public void LoadProductList(List<Product> productList)
    {
        int index = 0;
        string represent = "";
        foreach (var item in productList)
        {
            if (item is PhysicalProduct)
            {
                represent = ((PhysicalProduct)item).StrRepresent();
            }
            else represent = ((DigitalProduct)item).StrRepresent();
            Console.WriteLine($"{index + 1}. {represent}.");
            index++;
        }
    }
    
    public void LoadProductInfo(int index)
    {
        Console.WriteLine($"Name: {GetProductList[index].GetName()}.");
        Console.WriteLine($"Description: {GetProductList[index].GetDescription()}.");
        Console.WriteLine($"Quantity: {GetProductList[index].GetQuantity()}.");
        Console.WriteLine($"Price: {GetProductList[index].GetPrice()}.");
        if (GetProductList[index] is PhysicalProduct)
        {
            var product = GetProductList[index];
            Console.WriteLine($"Weight: {((PhysicalProduct)product).GetWeight()}.");
            Console.WriteLine("Type: Physical.");
        }
        else Console.WriteLine("Type: Digital.");
    }

    public void SetDefaultCart(int cardID) => CurrentCartID = cardID;
    
    public void LoadCartAmount()
    {
        Console.WriteLine($"Current Cart Amount: {GetCartAmount(CurrentCartID)}.");
    }

    public void LoadCartList()
    {
        // Mark this cart is default
        string defaultCart = "";
        GetCartList.Sort((x, y) => -x.CompareTo(y));
        foreach (var item in GetCartList)
        {
            if (item.CartID == CurrentCartID) defaultCart = "(Default)";
            else defaultCart = "";
            Console.WriteLine($"Cart {item.CartID} - Total Weight = {item.TotalWeight()}. {defaultCart}");
        }
    }

    public List<ShoppingCart> GetCartProductList()
    {
        return new List<ShoppingCart>();
    }
    
    public void LoadCartProductList()
    {
        
    }
}