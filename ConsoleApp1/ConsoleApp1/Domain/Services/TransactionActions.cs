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
            if (marketplace.Transactions.Count == 0)
            {
                Console.WriteLine("\nNema transakcija.");
                return;  
            }

            foreach (var transaction in marketplace.Transactions)
            {
                if (transaction.IsReturnTransaction)
                {
                    Console.WriteLine($"\nId proizvoda: {transaction.ProductId}, Povrat proizvoda od kupca: {transaction.Buyer.Name} prodavacu: {transaction.Seller.Name}" +
                   $", Vrijeme transkacije: {transaction.TransactionDate}");
                }
                else
                {
                    Console.WriteLine($"\nId proizvoda: {transaction.ProductId}, Kupac: {transaction.Buyer.Name}, Prodavac: {transaction.Seller.Name}" +
                        $", Vrijeme transkacije: {transaction.TransactionDate}");
                }
            }
        }
    }
}
