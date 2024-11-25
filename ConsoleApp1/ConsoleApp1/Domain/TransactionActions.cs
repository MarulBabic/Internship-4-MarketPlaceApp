using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class TransactionActions
    {
        public static void ViewAllTransactions(Marketplace marketplace)
        {
            foreach(var transaction in marketplace.transactions)
            {
                Console.WriteLine($"\nId proizvoda: {transaction.productId}, Kupac: {transaction.buyer.name}, Prodavac: {transaction.seller.name}"+
                    $", Vrijeme transkacije: {transaction.transactionDate}");
            }
        }
    }
}
