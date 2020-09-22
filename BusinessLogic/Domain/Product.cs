using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Domain
{
    public class Product
    {
        private string _Product;
        public Product(string product)
        {
            _Product = product;
        }
        public string getProduct()
        {
            return _Product;
        }
    }
}
