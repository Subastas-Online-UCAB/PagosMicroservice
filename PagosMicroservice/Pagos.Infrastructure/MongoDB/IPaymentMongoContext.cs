﻿using MongoDB.Driver;
using Pagos.Infrastructure.MongoDB.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pagos.Infrastructure.MongoDB
{
    public interface IPaymentMongoContext
    {
        IMongoCollection<PaymentDocument> Payment { get; }
    }
}
