using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace BusinessLogic.Domain
{
    public class Order
    {
        private Customer _customer;
        private LineItem[] _lineItems;
        private Agent _agent;
        private HashSet<String> _giftproduct;
        public Order(Customer customer, LineItem[] lineItems, Agent agent)
        {
            if (lineItems == null || lineItems.Count() == 0)
                throw new ArgumentException("line items are required");
            _lineItems = lineItems;
            _customer = customer;
            _agent = agent;
            _giftproduct = new HashSet<String>();
        }


        public Customer getCustomer()
        {
            return _customer;
        }


        public LineItem[] getLineItems()
        {
            return _lineItems;
        }


        public Agent getAgent()
        {
            return _agent;
        }

      
        public void addGiftByProduct(String product)
        {
            if (!_giftproduct.Contains(product))
                _giftproduct.Add(product);
        }

        public String[] getGiftProduct()
        {
            return _giftproduct.ToArray();
        }
    }
}
