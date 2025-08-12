using CheckoutKata.Enums;
using CheckoutKata.Interfaces;

namespace CheckoutKata;

public class Checkout : ICheckout
{
    public int GetTotalPrice()
    {
        throw new NotImplementedException();
    }

    public void Scan(StockKeepingUnit item)
    {
        throw new NotImplementedException();
    }
}
