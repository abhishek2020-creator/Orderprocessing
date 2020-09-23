using BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Domain
{
    public interface ICustomer
    {
        void addMembership(Membership membership, INotificationServices notificationService);
        Boolean hasMembership(Membership membership);
    }
}
