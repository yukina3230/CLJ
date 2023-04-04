namespace CLJ.Models;

public class DigitalProduct : Product, IGiftable
{
    private string message;
    
    public DigitalProduct() { }
    public DigitalProduct(string name, string description, string message, int quantity, double price) : base(name, description, quantity, price)
    {
        SetMessage(message);
    } 
    
    public override string StrRepresent() => $"DIGITAL - {GetName()}";
    
    public void SetMessage(string msg)
    {
        message = msg;
    }
    public string GetMessage() => message;
}