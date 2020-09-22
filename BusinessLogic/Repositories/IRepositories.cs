using BusinessLogic.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Repositories
{
    public interface IRepositories
    {
        Membership findByProduct(String product);
    }
}
