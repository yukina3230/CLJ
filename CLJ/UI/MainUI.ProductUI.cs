using System.Text.RegularExpressions;
using CLJ.Models;
using CLJ.Models.Services;

namespace CLJ.UI;

public partial class MainUI
{
    private void ShowProductUI()
    {
        HeaderText("MANAGE PRODUCTS");
        Console.WriteLine("1. Add New Products.");
        Console.WriteLine("2. Edit Products.");
        Console.WriteLine("3. Display All Products.");
        Console.WriteLine("0. Return To Main Menu.");
        Console.Write("Choose an option: ");
        string option = Console.ReadLine();

        switch (option)
        {
            case "1":
                AddNewProducts();
                break;
            case "2":
                EditProduct();
                break;
            case "3":
                DisplayAllProducts();
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

    private void AddNewProducts()
    {
        Product product = new DigitalProduct();
        string name = "", description = "", type = "";
        int qty = 0;
        double price = 0;
        
        HeaderText("ADD NEW PRODUCTS");
        
        // Enter Product Type
        type = service.Authorize<string>("- Choose Product Type (Digital or Physical) (Digital - D / Physical - P): ", @"^[dDpP]+$");
        
        //Enter Product Name
        bool valid;
        do
        {
            valid = true;
            name = service.Authorize<string>("- Enter Product Name: ", @"^\S+(\s+\S+)*$");
            
            if (service.GetProductList.Count > 0)
            {
                foreach (var item in service.GetProductList)
                {
                    if (name == item.GetName())
                    {
                        Console.Write("Product already exists! Please try another name.");
                        Console.ReadKey();
                        Console.WriteLine();
                        valid = false;
                        break;
                    }
                }
            }
        } while (!valid);
        
        //Enter Product Description
        description = service.Authorize<string>("- Enter Product Description: ", @"^\S+(\s+\S+)*$");
        
        // Enter Quantity
        qty = service.Authorize<int>("- Enter Product Quantity: ", @"^[0-9]+$");
        
        // Enter Price
        price = service.Authorize<double>("- Enter Product Price: ", @"^[0-9]+(\.[0-9]+)?$");
        
        // Create Product Object and Enter Weight
        if (type == "d" || type == "D")
        {
            product = new DigitalProduct(name, description, "", qty, price);
        }
        else
        {
            bool valid1 = false;
            double weight = service.Authorize<double>("- Enter Product Weight (Kg): ", @"^[0-9]+(\.[0-9]+)?$");
            product = new PhysicalProduct(name, description, "", qty, price, weight);
        }
        
        service.CreateProduct(product);
        Console.WriteLine("\nProduct has been added!");
        
        FooterText("P");
    }
    
    private void EditProduct()
    {
        string name = "", description = "", message = "";
        int qty = 0, itemIndex = 0, selectIndex = 0;
        double price = 0, weight = 0;
        
        HeaderText("EDIT PRODUCTS");
        
        if (service.GetProductList.Count > 0)
        {
            service.LoadProductList(service.GetProductList);
            
            // Select Product to Edit
            bool valid;
            do
            {
                valid = true;
                selectIndex = service.Authorize<int>("Select a product to edit. (1, 2, 3, ...): ", @"^[0-9]+$");
                itemIndex = selectIndex - 1;
                
                if (selectIndex <= 0 || selectIndex > service.GetProductList.Count)
                {
                    Console.Write("You selected a product that is not on the list. Please try again!");
                    Console.ReadKey();
                    Console.WriteLine();
                    valid = false;
                }
            } while (!valid);
            
            var product = service.GetProductList[itemIndex];
            Console.WriteLine("\n(Press Enter if you want to keep the value)");
            name = service.Authorize<string>($"- Enter Product Name: ({service.GetProductList[itemIndex].GetName()}) => ", @"^\S+|$");
            if (String.IsNullOrEmpty(name)) name = service.GetProductList[itemIndex].GetName();
            description = service.Authorize<string>($"- Enter Product Description: ({service.GetProductList[itemIndex].GetDescription()}) => ", @"^\S+|$");
            if (String.IsNullOrEmpty(description)) description = service.GetProductList[itemIndex].GetDescription();
            qty = service.Authorize<int>($"- Enter Product Quantity: ({service.GetProductList[itemIndex].GetQuantity()}) => ", @"^[0-9]+|$");
            if (qty.ToString() == "0") qty = service.GetProductList[itemIndex].GetQuantity();
            price = service.Authorize<double>($"- Enter Product Price: ({service.GetProductList[itemIndex].GetPrice()}) => ", @"^[0-9]+(\.[0-9]+)?|$");
            if (price.ToString() == "0") price = service.GetProductList[itemIndex].GetPrice();
            
            
            if (service.GetProductList[itemIndex] is PhysicalProduct)
            {
                weight = service.Authorize<double>($"- Enter Product Weight: ({((PhysicalProduct)product).GetWeight()}) => ", @"^[0-9]+(\.[0-9]+)?|$");
                if (weight.ToString() == "0") weight = ((PhysicalProduct)product).GetWeight();
                service.GetProductList[itemIndex] = new PhysicalProduct(name, description, message, qty, price, weight);
                // service.EditProduct(itemIndex, name, description, message, qty, price, weight);
            }
            else
            {
                service.GetProductList[itemIndex] = new DigitalProduct(name, description, message, qty, price);
            }
            
            Console.Write("\nProduct information updated.");
        }
        else
        {
            Console.WriteLine("You have no products!");
        }
        
        FooterText("P");
    }
    
    private void DisplayAllProducts()
    {
        int itemIndex = 0;
        
        HeaderText("ALL PRODUCTS");
        if (service.GetProductList.Count != 0)
        {
            service.LoadProductList(service.GetProductList);
            
            bool valid;
            do
            {
                valid = true;
                int selectIndex = service.Authorize<int>("Select a product to query. (1, 2, 3, ...): ", @"^[0-9]+$");
                itemIndex = selectIndex - 1;
                
                if (selectIndex <= 0 || selectIndex > service.GetProductList.Count)
                {
                    Console.Write("You selected a product that is not on the list. Please try again!");
                    Console.ReadKey();
                    Console.WriteLine();
                    valid = false;
                }
            } while (!valid);

            service.LoadProductInfo(itemIndex);
        }
        else
        {
            Console.WriteLine("You have no products!");
        }
        
        FooterText("P");
    }
}