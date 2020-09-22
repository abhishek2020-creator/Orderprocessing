using System;
using System.Collections.Generic;
using System.Text;
using BusinessLogic.Domain;
namespace BusinessLogic.PaymentProcess
{

    public interface IPaymentProcess
    {
        void run(Payment payment);
    }

}
