using CreditSuisseTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest.Entities
{
    public class CustomerAccount : IAccount
    {
        public List<string> CardNumbers { get; set; }
        public decimal MinimumAllowedBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public int Pin { get; set; }
    }
}
