using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public abstract class User
    {
        private Guid Id;
        public string Name { get; private set; }
        public string Email {  get; private set; }

        public User(string name, string email )
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
        }
    }
}
