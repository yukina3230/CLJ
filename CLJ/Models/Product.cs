namespace CLJ.Models;

public abstract class Product
{
    private string name;
    private string description;
    private int quantity;
    private double price;
    
    public Product() {}
    public Product(string name, string description, int quantity, double price)
    {
        SetName(name);
        SetDescription(description);
        SetQuantity(quantity);
        SetPrice(price);
    }
    
    public string GetName() => name;
    public void SetName(string value)
    {
        name = value;
    }

    public string GetDescription() => description;
    public void SetDescription(string value)
    {
        description = value;
    }

    public int GetQuantity() => quantity;
    public void SetQuantity(int value)
    {
        quantity = Math.Abs(value);
    }

    public double GetPrice() => price;
    public void SetPrice(double value)
    {
        price = Math.Abs(value);
    }

    public abstract string StrRepresent();
}