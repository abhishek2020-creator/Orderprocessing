using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Domain
{
    public class LineItem
    {
        private String _product;
        public String _name;
        private Dictionary<String, ProductCategory> _categories;
        public LineItem(String product, String name, ProductCategory[] categories)
        {
            _product = product;
            _name = name;

            _categories = new Dictionary<String, ProductCategory>();
            if (categories != null)
            {
                foreach (ProductCategory cat in categories)
                {
                    if (!_categories.ContainsKey(cat.getProductName()))
                        _categories.Add(cat.getProductName(), cat);
                }
            }
        }
        public String getProduct()
        {
            return _product;
        }
        public String getName()
        {
            return _name;
        }
        public Boolean hasCategory(ProductCategory category)
        {
            return _categories.ContainsKey(category.getProductName());
        }
    }
}
