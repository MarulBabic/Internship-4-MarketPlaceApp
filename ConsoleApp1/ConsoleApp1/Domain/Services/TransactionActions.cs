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
            if (marketplace.transactions.Count == 0)
            {
                Console.WriteLine("\nNema transakcija.");
                return;  
            }

            foreach (var transaction in marketplace.transactions)
            {
                if (transaction.isReturnTransaction)
                {
                    Console.WriteLine($"\nId proizvoda: {transaction.productId}, Povrat proizvoda od kupca: {transaction.buyer.name} prodavacu: {transaction.seller.name}" +
                   $", Vrijeme transkacije: {transaction.transactionDate}");
                }
                else
                {
                    Console.WriteLine($"\nId proizvoda: {transaction.productId}, Kupac: {transaction.buyer.name}, Prodavac: {transaction.seller.name}" +
                        $", Vrijeme transkacije: {transaction.transactionDate}");
                }
            }
        }
    }
}
