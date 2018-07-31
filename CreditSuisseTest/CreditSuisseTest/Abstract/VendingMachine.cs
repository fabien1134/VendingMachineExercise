using CreditSuisseTest.Entities;
using CreditSuisseTest.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CreditSuisseTest.Entities.CardVendingMachine;

namespace CreditSuisseTest.Abstract
{
    public class VendingMachine : IVendingMachine
    {
        #region Properties
        public string MachineID { get; private set; } = default;
        public int VendingItemAmount { get; protected set; } = default;
        public Dictionary<VendingMachineItem, decimal> ItemPriceMapping { get; protected set; }
        #endregion

        public VendingMachine() => MachineID = Guid.NewGuid().ToString();
    }
}
