using CreditSuisseTest.Abstract;
using CreditSuisseTest.Entities;
using CreditSuisseTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreditSuisseTest.Entities.CardVendingMachine;

namespace CreditSuisseTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<VendingMachineItem, decimal> itemPriceMapping = new Dictionary<VendingMachineItem, decimal>() {
               { VendingMachineItem.Pepsi,0.50m}
            };

            try
            {
                //For every account number and pin provided a customer acount belonging to the bank issuer will be created
                //Each of the accounts will be assigned the provided minimumAllowedBalance and currentBalance
                BankIssuer bankIssuer = new BankIssuer(accountNumberCollection: new(int account, int pin)[] { (9999, 999), (8888, 888), (7777, 777), (6666, 666) }, minimumAllowedBalance: 0.50m, currentBalance: 10m);

                //The desired collection of users will be returned that contain cards that is associated with an account belongging to the bankcard issuer
                //A prefix can be added to uniquly identify created groups of vendor users
                List<VendorUser> vendorUsers = AssignCardToGeneratedVendorUsers(vendorUserAmount: 2, accountNumber: 9999, bankIssuer: bankIssuer, namePrefix: "Smiths");

                //A vending machine will be created with the amount of items that are currently contained in the bank and a price mapping
                CardVendingMachine mainVendingMachine = new CardVendingMachine(vendingItemAmount: 25, itemPriceMapping);

                ////Users can use their cashcards to purchase an item contained in the vedning machine,they must provide a pin a a message will be displayed
                ////in the console to indicate the status of their purchase
                //mainVendingMachine.BuyItems(VendingMachineItem.Pepsi, itemAmount: 1, pin: vendorUsers[0].Pin, card: vendorUsers[0].BankCard);
                //mainVendingMachine.BuyItems(VendingMachineItem.Pepsi, itemAmount: 1, pin: vendorUsers[1].Pin, card: vendorUsers[1].BankCard);

                //Both users with a joint account are fighting to purchase items from the same vedning machine
                Parallel.ForEach(vendorUsers, (vendorUser) =>
                {
                    while (mainVendingMachine.BuyItems(VendingMachineItem.Pepsi, itemAmount: 1, pin: vendorUser.Pin, card: vendorUser.BankCard))
                    { }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Issue During Application Execution: {ex.Message}");
            }
        }


        private static List<VendorUser> AssignCardToGeneratedVendorUsers(int vendorUserAmount, int accountNumber, IBankIssuer bankIssuer, string namePrefix = default)
        {
            List<VendorUser> vendorUsers = new List<VendorUser>();
            const int offset = 1;
            for (int i = 0; i < vendorUserAmount; i++)
            {
                string userName = $"{namePrefix} Vendor User {i + offset}";
                vendorUsers.Add(new VendorUser()
                {
                    Name = userName,
                    BankCard = bankIssuer.IssueCard(accountNumber: accountNumber, out int pin)
                });

                vendorUsers[i].Pin = pin;
                vendorUsers[i].BankCard.Name = userName;
            }

            return vendorUsers;
        }
    }
}
