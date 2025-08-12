namespace CheckoutKata.Interfaces;

public interface ICheckout
{
    void Scan(string itemIdentifier);
    int GetTotalPrice();
}
