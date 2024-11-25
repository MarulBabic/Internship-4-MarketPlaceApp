using ConsoleApp1.Domain;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Marketplace marketplace = new Marketplace();
            Buyer buyer1 = BuyerActions.BuyerRegistration(marketplace);
            marketplace.buyers.Add(buyer1);

            Seller seller1 = SellerActions.SellerRegistration(marketplace);
            marketplace.sellers.Add(seller1);


            Product product1 = new Product("mobitel", "novi mobitel", 1200, Data.Category.Elektronika, seller1);
            Product product2 = new Product("televizija", "novi tv", 500, Data.Category.Elektronika, seller1);
            Product product3 = new Product("laptop", "novi laptop", 800, Data.Category.Elektronika, seller1);

            marketplace.products.Add(product1);
            marketplace.products.Add(product2);
            marketplace.products.Add(product3);

            UserActions.LoginUser(marketplace);
            
        }
    }
}
