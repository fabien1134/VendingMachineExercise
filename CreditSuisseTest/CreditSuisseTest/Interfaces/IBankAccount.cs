using CreditSuisseTest.TransactionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreditSuisseTest.Abstract.BankIssuer;

namespace CreditSuisseTest.Interfaces
{
    interface IBankIssuer
    {
        IBankCard IssueCard(int accountNumber, out int pin, CardType cardType = CardType.CashCard);
        AuthorizationResponse AuthorizePaymentTransactionHandler(AuthorizationRequest authorizationRequest);
        bool CreateCustomerAccount((int account, int pin) accountPinAssociation, decimal minimumAllowedBalance = 0.50m);
    }
}
