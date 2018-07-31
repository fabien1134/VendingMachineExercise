using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditSuisseTest.TransactionEntities
{
    public class AuthorizationResponse
    {
        public int ResponseCode { get; set; }
        public string AuthorizationRequestCryptogram { get; set; }
    }
}
