namespace CLJ.UI;

public partial class MainUI
{
    private void ShowCartUI()
    {
        HeaderText("MANAGE SHOPPING CARTS");
        Console.WriteLine("1. Add Products To The Current Shopping Cart.");
        Console.WriteLine("2. Remove Products From The Current Shopping Cart.");
        Console.WriteLine("3. Display The Cart Amount");
        Console.WriteLine("4. Create New Shopping Cart.");
        Console.WriteLine("5. Display All Shopping Carts.");
        Console.WriteLine("6. Set Default Cart.");
        Console.WriteLine("0. Return To Main Menu.");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();
        
        switch (option)
        {
            case "1":
                AddProductToCart();
                break;
            case "2":
                RemoveProductFromCart();
                break;
            case "3":
                DisplayCartAmount();
                break;
            case "4":
                CreateShoppingCart();
                break;
            case "5":
                DisplayAllCarts();
                break;
            case "6":
                SetDefaultCart();
                break;
            case "0":
                ShowMainUI();
                break;
            default:
                Console.Write("Wrong option. Please try again!");
                Console.ReadKey();
                ShowCartUI();
                break;
        }
    }
    
    private void AddProductToCart()
    {
        int itemIndex = 0;
        string message = "";
        
        HeaderText("ADD PRODUCT TO CURRENT CART");
        
        string option = service.Authorize<string>("Did you buy this product as a gift? (Yes - Y/ No - N): ", @"^[yYnN]+$");
        if (option == "y" || option == "Y")
        {
            Console.Write("- Write in a message to the recipient: ");
            message = Console.ReadLine();
        }

        if (service.GetProductList.Count != 0)
        {
            service.LoadProductList(service.GetProductList);

            bool valid;
            do
            {
                valid = true;
                int selectIndex = service.Authorize<int>("\nSelect a product you want to add. (1, 2, 3, ...): ", @"^[0-9]+$");
                itemIndex = selectIndex - 1;
                    
                if (selectIndex <= 0 || selectIndex > service.GetProductList.Count)
                {
                    Console.Write("You selected a product that is not on the list. Please try again!");
                    Console.ReadKey();
                    Console.WriteLine();
                    valid = false;
                }
            } while (!valid);
            
            service.AddProductToCurrentCart(itemIndex, message);
            Console.Write("\nProduct added to your current cart.");
        }
        else
        {
            Console.WriteLine("You have no products!");
        }
        
        FooterText("C");
    }
    
    // TODO
    private void RemoveProductFromCart()
    {
        int itemIndex = 0;
        string message = "";
        
        HeaderText("ADD PRODUCT TO CURRENT CART");
        
        string option = service.Authorize<string>("Did you buy this product as a gift? (Yes - Y/ No - N): ", @"^[yYnN]+$");
        if (option == "y" || option == "Y")
        {
            Console.Write("- Write in a message to the recipient: ");
            message = Console.ReadLine();
        }

        if (service.GetCartProductList().Count != 0)
        {
            service.LoadCartProductList();

            bool valid;
            do
            {
                valid = true;
                int selectIndex = service.Authorize<int>("\nSelect a product you want to add. (1, 2, 3, ...): ", @"^[0-9]+$");
                itemIndex = selectIndex - 1;
                    
                if (selectIndex <= 0 || selectIndex > service.GetProductList.Count)
                {
                    Console.Write("You selected a product that is not on the list. Please try again!");
                    Console.ReadKey();
                    Console.WriteLine();
                    valid = false;
                }
            } while (!valid);
            
            service.AddProductToCurrentCart(itemIndex, message);
            Console.Write("\nProduct removed from this current cart.");
        }
        else
        {
            Console.WriteLine("You have no products!");
        }
        
        FooterText("C");
    }
    
    private void DisplayCartAmount()
    {
        HeaderText("CART AMOUNT");
        service.LoadCartAmount();
        FooterText("C");
    }

    private void CreateShoppingCart()
    {
        HeaderText("CREATE SHOPPING CART");
        
        string option = service.Authorize<string>("Do you want to create a new cart? (Yes - Y/ No - N): ", @"^[yYnN]+$");
        if (option == "y" || option == "Y")
        {
            service.AddCart();
            Console.WriteLine("A new cart has been created.");
        }
        
        FooterText("C");
    }

    private void DisplayAllCarts()
    {
        HeaderText("ALL CARTS");
        service.LoadCartList();
        FooterText("C");
    }

    private void SetDefaultCart()
    {
        int cartID = 1, itemIndex = 0;
        
        service.LoadCartList();

        bool valid;
        do
        {
            valid = true;
            cartID = service.Authorize<int>("\nSelect cart. (1, 2, ...): ", @"^[0-9]+$");
                    
            if (cartID <= 0 || cartID > service.GetCartList.Count)
            {
                Console.Write("You selected a cart that is not on the list. Please try again!");
                Console.ReadKey();
                Console.WriteLine();
                valid = false;
            }
        } while (!valid);
        
        service.SetDefaultCart(cartID);
        Console.WriteLine($"Card {cartID} has been set as default.");
        
        FooterText("C");
    }
}