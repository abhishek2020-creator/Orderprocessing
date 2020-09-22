using BusinessLogic.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Service
{
    public interface IRoyaltyServices
    {
        PackingSlip generatePackingSlip(Order order);
    }
}
