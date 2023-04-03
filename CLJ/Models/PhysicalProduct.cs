namespace CLJ.Models;

public class PhysicalProduct : Product, IGiftable
{
    private string message;
    private double weight;
    
    public PhysicalProduct(string name, string description, string message, int quantity, double price, double weight) : base(name, description, quantity, price)
    {
        SetMessage(message);
        SetWeight(weight);
    } 
    
    public double GetWeight() => weight;
    public void SetWeight(double value)
    {
        weight = Math.Abs(value);
    }
    public override string StrRepresent() => $"PHYSICAL - {GetName()}";
    
    public void SetMessage(string msg)
    {
        message = msg;
    }
    public string GetMessage() => message;
}