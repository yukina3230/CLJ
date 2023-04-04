using CLJ.Models;
using CLJ.Models.Services;

namespace CLJ.UI;

public partial class MainUI
{
    private ShoppingService service;

    public MainUI()
    {
        service = new ShoppingService();
        
        // Create default Cart
        service.AddCart();
        service.CurrentCartID = 1;
    }

    public void ShowMainUI()
    {
        HeaderText("ONLINE SHOPPING SERVICE");
        Console.WriteLine($"1. Manage Products. {ShowProductCount()}");
        Console.WriteLine($"2. Manage Shopping Carts. (Total Carts: {service.GetTotalCarts()})");
        Console.WriteLine("0. Exit.");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        
        switch (option)
        {
            case "1":
                ShowProductUI();
                break;
            case "2":
                ShowCartUI();
                break;
            case "0":
                Console.WriteLine("Good Bye!");
                Environment.Exit(0);
                break;
            default:
                Console.Write("Wrong option. Please try again!");
                Console.ReadKey();
                ShowMainUI();
                break;
        }
    }

    private string ShowProductCount()
    {
        string result = "";
        if (service.GetProductList.Count > 0) result = $"(Total Products: {service.GetProductList.Count})";
        return result;
    }
    
    private void HeaderText(string text)
    {
        Console.Clear();
        Console.WriteLine($"-- {text} --");
        Console.WriteLine();
    }
    
    private void FooterText(string nav)
    {
        Console.Write("\nPress any key to go back.");
        Console.ReadKey();

        switch (nav)
        {
            case "M":
                ShowMainUI();
                break;
            case "P":
                ShowProductUI();
                break;
            case "C":
                ShowCartUI();
                break;
        }
    }
}