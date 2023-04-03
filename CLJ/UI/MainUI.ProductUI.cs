namespace CLJ.UI;

public class ProductUI : MainUI
{
    public void ShowProductUI()
    {
        Console.Clear();
        Console.WriteLine("-- MANAGE PRODUCTS --");
        Console.WriteLine();
        Console.WriteLine("1. Manage Products.");
        Console.WriteLine("2. Manage Shopping Carts.");
        Console.WriteLine("0. Return To Main Menu.");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        
        switch (option)
        {
            case "1":
                ShowProductUI();
                break;
            case "2":
                break;
            case "0":
                ShowMainUI();
                break;
            default:
                Console.Write("Wrong option. Please try again!");
                Console.ReadKey();
                ShowProductUI();
                break;
        }
}