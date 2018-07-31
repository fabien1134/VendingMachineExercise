using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest.TransactionEntities
{
    public class AuthorizationRequest
    {
        public string AuthorizationRequestCryptogram { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public int Expiration { get; set; }
        public int ServiceCode { get; set; }
        public int CVC { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransacionCurrencyCode { get; set; }
        public decimal TransactionAmount { get; set; }
        public int Pin { get; set; }
    }
}
