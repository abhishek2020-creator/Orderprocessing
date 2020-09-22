using BusinessLogic.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Service
{
    public interface IShippingServices
    {
        PackingSlip generatePackingSlip(Order order);
    }
}
