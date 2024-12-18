﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Seller : User
    {
        public double Income {  get; private set; }
        public Seller(string name, string email) : base(name, email)
        {

        }
        public void AddIncome(double amount)
        {
            Income += amount;
        }

        public double GetIncome()
        {
            return Income;
        }
    }
}
