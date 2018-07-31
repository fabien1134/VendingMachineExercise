using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest.Interfaces
{
    public interface IAccount
    {
        List<string> CardNumbers { get; set; }
        decimal MinimumAllowedBalance { get; set; }
        decimal CurrentBalance { get; set; }
        int Pin { get; set; }
    }
}
