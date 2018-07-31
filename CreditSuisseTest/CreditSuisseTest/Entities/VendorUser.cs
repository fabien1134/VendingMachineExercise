using CreditSuisseTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest.Entities
{
    public class VendorUser
    {
        public string Name { get; set; }
        public IBankCard BankCard { get; set; }
        public int Pin { get; set; }
    }
}
