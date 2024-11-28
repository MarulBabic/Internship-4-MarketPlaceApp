using ConsoleApp1.Data;
using ConsoleApp1.Domain;
using ConsoleApp1.Presentation;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Marketplace marketplace = new Marketplace();
            Buyer buyer1 = new Buyer("Ivan", "ivan@gmail.com",1500);
            marketplace.buyers.Add(buyer1);

            Seller seller1 = new Seller("Marko","marko@gmail.com");
            marketplace.sellers.Add(seller1);

            Seller seller2 = new Seller("Josip", "josip@gmail.com");
            marketplace.sellers.Add(seller1);


            Product product1 = new Product("mobitel", "novi mobitel", 1200, Data.Category.Elektronika, seller1);
            Product product2 = new Product("televizija", "novi tv", 500, Data.Category.Elektronika, seller1);
            Product product3 = new Product("laptop", "novi laptop", 800, Data.Category.Elektronika, seller1);

            marketplace.products.Add(product1);
            marketplace.products.Add(product2);
            marketplace.products.Add(product3);

            PromoCode promo1 = new PromoCode("Fall2024", 15.0, DateTime.ParseExact("12-12-2024", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), new List<Category> { Category.Elektronika});
            PromoCode promo2 = new PromoCode("Winter2025", 20.0, DateTime.ParseExact("12-02-2025", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), new List<Category> { Category.Knjige });
            PromoCode promo3 = new PromoCode("Summer2024", 25.0, DateTime.ParseExact("12-09-2024", "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture), new List<Category> { Category.Knjige, Category.Odjeca });

            MarketplaceActions.AddPromoCode(marketplace,promo1);
            MarketplaceActions.AddPromoCode(marketplace, promo2);
            MarketplaceActions.AddPromoCode(marketplace, promo3);

            HomeMenu.ViewHomeMenu(marketplace);
        }
    }
}
