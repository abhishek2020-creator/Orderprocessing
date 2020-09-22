using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Domain
{
    public class ProductCategory
    {
        private string _Productname;
        private ProductCategory(string name)
        {
            _Productname = name;
        }


        public string getProductName()
        {
            return _Productname;
        }

        public static ProductCategory Books = new ProductCategory("Books");
        public static ProductCategory Physical = new ProductCategory("Physical");
        public static ProductCategory Virtual = new ProductCategory("Virtual");
        public static ProductCategory Membership = new ProductCategory("Membership");
        public static ProductCategory Videos = new ProductCategory("Videos");
    }
}
