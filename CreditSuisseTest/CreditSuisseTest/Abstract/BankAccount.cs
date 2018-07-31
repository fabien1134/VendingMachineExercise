using CreditSuisseTest.Entities;
using CreditSuisseTest.Interfaces;
using CreditSuisseTest.TransactionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest.Abstract
{
    public class BankIssuer : IBankIssuer
    {
        public delegate AuthorizationResponse AuthorizeTransaction(AuthorizationRequest authorizationRequest);
        public AuthorizeTransaction AuthorizeTransactionCallback { get; private set; } = default;
        private Dictionary<string, int> m_customerCardnumberToAccountMapping = new Dictionary<string, int>();
        private Dictionary<int, IAccount> m_accountNumberToAccountMapping = new Dictionary<int, IAccount>();

        private Object thisLock = new Object();

        public BankIssuer((int account, int pin)[] accountNumberCollection = default, decimal minimumAllowedBalance = 0.50m, decimal currentBalance = 10m)
        {
            AuthorizeTransactionCallback = new AuthorizeTransaction(AuthorizePaymentTransactionHandler);
            if (accountNumberCollection != default)
                foreach ((int account, int pin) accountPinAssociation in accountNumberCollection)
                {
                    if (m_accountNumberToAccountMapping.Keys.Contains(accountPinAssociation.account))
                    {
                        Console.WriteLine($"Skipped {accountPinAssociation} Account Creation, Duplicate Record Found");
                    }
                    else
                    {
                        m_accountNumberToAccountMapping.Add(accountPinAssociation.account, new CustomerAccount()
                        {
                            MinimumAllowedBalance = minimumAllowedBalance,
                            CurrentBalance = currentBalance,
                            Pin = accountPinAssociation.pin
                        });
                    }
                }
        }


        public bool CreateCustomerAccount((int account, int pin) accountPinAssociation, decimal minimumAllowedBalance = 0.50m)
        {
            bool created = false;

            if (m_accountNumberToAccountMapping.Keys.Contains(accountPinAssociation.account))
            {
                Console.WriteLine($"Skipped {accountPinAssociation} Account Creation, Duplicate Record Found");
            }
            else
            {
                m_accountNumberToAccountMapping.Add(accountPinAssociation.account, new CustomerAccount()
                {
                    CurrentBalance = minimumAllowedBalance,
                    Pin = accountPinAssociation.pin
                });
                created = true;
            }

            return created;
        }

        public IBankCard IssueCard(int accountNumber, out int pin, CardType cardType = CardType.CashCard)
        {
            string CardNumber = Guid.NewGuid().ToString();
            IBankCard bankCard = default;
            pin = 0;
            //Ensures only unique cardnumbers wil be stored
            if (m_customerCardnumberToAccountMapping.Keys.Contains(CardNumber))
                throw new Exception($"Card:{CardNumber} Could Not Be Issued As It Is Not Unique");
            if (!m_accountNumberToAccountMapping.Keys.Contains(accountNumber))
                throw new Exception($"No Account Found Associated With Account Number:{accountNumber}");

            m_customerCardnumberToAccountMapping.Add(CardNumber, accountNumber);

            pin = m_accountNumberToAccountMapping[accountNumber].Pin;

            if (pin == 0)
                throw new Exception("No Valid Pin Detected During The Card Issue Process");


            switch (cardType)
            {
                case CardType.CashCard:
                    bankCard = new CashCard()
                    {
                        CardNumber = CardNumber,
                        AuthorizeTransaction = AuthorizeTransactionCallback
                    };
                    break;
                case CardType.DebitCard:
                    break;
                case CardType.CreditCard:
                    break;
                default:
                    break;
            }

            return bankCard;
        }

        public AuthorizationResponse AuthorizePaymentTransactionHandler(AuthorizationRequest authorizationRequest)
        {
            AuthorizationResponse authorizationResponse = new AuthorizationResponse()
            {
                ResponseCode = (int)AuthorizationResponseCode.ProcessingError
            };

            lock (thisLock)
            {
                //Get the associated cards account number
                if (ReturnCustomerAccount(authorizationRequest.CardNumber, out IAccount custumerAccount) == default)
                    return authorizationResponse;

                if (custumerAccount.Pin != authorizationRequest.Pin)
                {
                    authorizationResponse.ResponseCode = (int)AuthorizationResponseCode.IncorrectPassword;
                    return authorizationResponse;
                }

                decimal newBalance = custumerAccount.CurrentBalance - authorizationRequest.TransactionAmount;

                if (newBalance < 0.50m)
                {
                    authorizationResponse.ResponseCode = (int)AuthorizationResponseCode.InsufficientFunds;
                    return authorizationResponse;
                }

                authorizationResponse.ResponseCode = (int)AuthorizationResponseCode.Authorised;

                custumerAccount.CurrentBalance = newBalance;
            }

            return authorizationResponse;
        }

        private IAccount ReturnCustomerAccount(string cardNumber, out IAccount customerAccount)
        {
            customerAccount = default;

            if (!m_customerCardnumberToAccountMapping.Keys.Contains(cardNumber))
                return default;

            int bankAccountNumber = m_customerCardnumberToAccountMapping[cardNumber];

            if (!m_accountNumberToAccountMapping.Keys.Contains(bankAccountNumber))
                return default;

            customerAccount = m_accountNumberToAccountMapping[bankAccountNumber];

            return customerAccount;
        }

        public enum CardType
        {
            CashCard,
            DebitCard,
            CreditCard
        };

        public enum AuthorizationResponseCode
        {
            IncorrectPassword = 99,
            ProcessingError = 500,
            Authorised = 200,
            InsufficientFunds = 450
        };
    }
}
