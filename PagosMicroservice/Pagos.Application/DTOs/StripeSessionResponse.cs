using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Application.DTO
{
    public class StripeSessionResponse
    {
        public string SessionId { get; set; }
        public string PublicKey { get; set; }
        public string Url { get; set; }
    }
}
