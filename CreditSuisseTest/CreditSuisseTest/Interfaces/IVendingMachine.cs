using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreditSuisseTest.Entities.CardVendingMachine;

namespace CreditSuisseTest.Interfaces
{
    interface IVendingMachine
    {
        string MachineID { get; }
        int VendingItemAmount { get; }
        Dictionary<VendingMachineItem, decimal> ItemPriceMapping { get; }
    }
}
