using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreditSuisseTest.Abstract.BankIssuer;

namespace CreditSuisseTest.Interfaces
{
    public interface IBankCard
    {
        AuthorizeTransaction AuthorizeTransaction { get; set; }
        string CardNumber { get; set; }
        string Name { get; set; }
        int Expiration { get; set; }
        int CVC { get; set; }
        int ApplicationInterchangeProfile { get; set; }
        int ApplicationTransactionCounter { get; set; }
        string CryptogramKey { get; set; }
    }
}
