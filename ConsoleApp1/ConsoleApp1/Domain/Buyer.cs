﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public class Buyer : User
    {
        public double balance { get; set; }
        public Buyer(string name, string email, double balance) : base(name, email)
        {
            this.balance = balance;
        }
    }
}