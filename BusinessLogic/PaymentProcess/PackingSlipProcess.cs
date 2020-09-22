using BusinessLogic.Domain;
using BusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.PaymentProcess
{
    // If the payment is for a physical product, generate a packing slip for shipping.
    // If the payment is for a book, create a duplicate packing slip for the royalty department.
    // if the payment is for the video “Learning to Ski,” add a free “First Aid” video to the packing slip
    public class PackingSlipProcess
    {


        private IPackingSlipServices _packingSlipService;
        private IShippingServices _shippingService;
        private IRoyaltyServices _royaltyService;

        public PackingSlipProcess(IShippingServices shippingService,
                                   IRoyaltyServices royaltyService,
                                  IPackingSlipServices packingSlipService)
        {
            _shippingService = shippingService;
            _royaltyService = royaltyService;
            _packingSlipService = packingSlipService;
        }


        public void run(Payment payment)
        {
            Order order = payment.getOrder();
            LineItem[] lineItems = order.getLineItems();

            Boolean generateForShipping = false;
            Boolean generateForRoyalty = false;

            foreach (LineItem lineItem in lineItems)
            {
                if (lineItem.hasCategory(ProductCategory.Physical))
                    generateForShipping = true;
                if (lineItem.hasCategory(ProductCategory.Books))
                    generateForRoyalty = true;

                if (lineItem.getProduct() == "learning-to-ski")
                    order.addGiftByProduct("first-aid");
            }

            _packingSlipService.generate(order);

            if (generateForShipping)
                _shippingService.generatePackingSlip(order);
            if (generateForRoyalty)
                _royaltyService.generatePackingSlip(order);
        }
    }

}
