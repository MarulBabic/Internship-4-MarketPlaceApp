using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Domain
{
    public abstract class User
    {
        private Guid id;
        public string name { get; private set; }
        public string email {  get; private set; }

        public User(string name, string email )
        {
            id = Guid.NewGuid();
            this.name = name;
            this.email = email;
        }
    }
}
