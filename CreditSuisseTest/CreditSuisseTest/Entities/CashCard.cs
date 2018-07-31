using CreditSuisseTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreditSuisseTest.Abstract.BankIssuer;

namespace CreditSuisseTest.Entities
{
    public class CashCard : IBankCard
    {
        public AuthorizeTransaction AuthorizeTransaction { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public int Expiration { get; set; }
        public int CVC { get; set; }
        public int ApplicationInterchangeProfile { get; set; }
        public int ApplicationTransactionCounter { get; set; }
        public string CryptogramKey { get; set; }
    }
}
