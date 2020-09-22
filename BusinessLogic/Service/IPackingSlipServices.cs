using BusinessLogic.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Service
{
    public interface IPackingSlipServices
    {
        PackingSlip generate(Order order);
    }
}
