using CreditSuisseTest.Abstract;
using CreditSuisseTest.Interfaces;
using CreditSuisseTest.TransactionEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreditSuisseTest.Abstract.BankIssuer;

namespace CreditSuisseTest.Entities
{
    public class CardVendingMachine : VendingMachine
    {
        public CardVendingMachine(int vendingItemAmount, Dictionary<VendingMachineItem, decimal> itemPriceMapping) : base()
        {
            ItemPriceMapping = itemPriceMapping;
            VendingItemAmount = vendingItemAmount;
        }


        public bool BuyItems(VendingMachineItem selectedItem, int itemAmount, int pin, IBankCard card)
        {
            //Validate the users input and environment will allow them to make a valid purchase
            if (!AreVendingItemsPresent(VendingItemAmount))
            {
                Console.WriteLine("No Items Available To Purchase");
                return false;
            }
            else if (!IsUserSelectionValid(itemAmount, VendingItemAmount))
            {
                Console.WriteLine($"Ensure Item Amount Is Valid");
                return false;
            }

            decimal transactionAmount = CalculateTransactionAmount(selectedItem, itemAmount);

            //Validate 
            if (SendAuthorizationRequest(pin, card, transactionAmount, out bool transactionAuthorized, out AuthorizationResponseCode authorizationResponseCode))
                VendingItemAmount -= itemAmount;

            Console.WriteLine($"{card.Name}: Transaction[{authorizationResponseCode.ToString()}]");

            return transactionAuthorized;
        }

        private decimal CalculateTransactionAmount(VendingMachineItem selectedItem, int itemAmount) => ItemPriceMapping[selectedItem] * itemAmount;

        private bool SendAuthorizationRequest(int pin, IBankCard card, decimal transactionAmount, out bool transactionAuthorized, out AuthorizationResponseCode authorizationResponseCode)
        {
            transactionAuthorized = false;

            AuthorizationResponse authorizationResponse = card.AuthorizeTransaction(new TransactionEntities.AuthorizationRequest()
            {
                CardNumber = card.CardNumber,
                Pin = pin,
                TransactionAmount = transactionAmount
            });

            authorizationResponseCode = (AuthorizationResponseCode)authorizationResponse.ResponseCode;
            transactionAuthorized = authorizationResponseCode == AuthorizationResponseCode.Authorised;
            return transactionAuthorized;
        }


        private bool AreVendingItemsPresent(int vendingItemAmount) => vendingItemAmount >= 1;

        private bool IsUserSelectionValid(int itemAmount, int vendingItemAmount) => (itemAmount > 0 && itemAmount <= vendingItemAmount);


        public enum VendingMachineItem
        {
            Pepsi = 0
        };
    }
}
