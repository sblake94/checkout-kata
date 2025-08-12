namespace CheckoutKata.Interfaces;

public interface IPricingStrategy
{
    public int CalculatePrice(int quantity);
}
