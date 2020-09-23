using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Moq;
using BusinessLogic.Domain;
using BusinessLogic.PaymentProcess;
using BusinessLogic.Repositories;
using BusinessLogic.Service;
using System.Linq;

namespace BusinessLogicTest
{
    public class PackingSlipProcessTest
    {
        private Mock<Customer> _mockCustomer;
        private Mock<IPackingSlipServices> _PackingSlipServices;
        private Mock<IShippingServices> _ShippingServices;
        private Mock<IRoyaltyServices> _RoyaltyServices;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockCustomer = new Mock<Customer>(MockBehavior.Loose);
            _PackingSlipServices = new Mock<IPackingSlipServices>(MockBehavior.Loose);
            _ShippingServices = new Mock<IShippingServices>(MockBehavior.Loose);
            _RoyaltyServices = new Mock<IRoyaltyServices>(MockBehavior.Loose);
        }


        [Test]
        public void runShouldNotGenerateForShippingWhenNoValidItemsAvailable()
        {
            try
            {
                LineItem[] lineItems = new LineItem[]{
            new LineItem("itemA", "itemA", new ProductCategory[]{
                ProductCategory.Membership
            }),
            new LineItem("itemB", "itemB", new ProductCategory[]{
                ProductCategory.Virtual,
            })
            };

                Order order = new Order(_mockCustomer.Object, lineItems, null);
                Payment payment = new Payment(order);

                IPackingSlipServices packingSlipService = _PackingSlipServices.Object;
                IShippingServices shippingService = _ShippingServices.Object;
                IRoyaltyServices royaltyService = _RoyaltyServices.Object;

                IPaymentProcess sut = new PackingSlipProcess(shippingService, royaltyService, packingSlipService);
                sut.run(payment);

                _ShippingServices.Verify(mock => mock.generatePackingSlip(order), Times.Never());
            }
            catch
            {
                throw;
            }
        }

        [Test]
        public void runShouldGenerateForShippingWhenPhysicalAvailable()
        {
            try
            {
                LineItem[] lineItems = new LineItem[]{
                    new LineItem("itemA", "itemA", new ProductCategory[]{
                        ProductCategory.Physical
                    }),
                    new LineItem("itemB", "itemB", new ProductCategory[]{
                        ProductCategory.Membership,
                    })
                };

                Order order = new Order(_mockCustomer.Object, lineItems, null);
                Payment payment = new Payment(order);

                IPackingSlipServices packingSlipService = _PackingSlipServices.Object;
                IShippingServices shippingService = _ShippingServices.Object;
                IRoyaltyServices royaltyService = _RoyaltyServices.Object;

                IPaymentProcess sut = new PackingSlipProcess(shippingService, royaltyService, packingSlipService);
                sut.run(payment);

                _ShippingServices.Verify(mock => mock.generatePackingSlip(order), Times.Once());
            }
            catch
            {
                throw;
            }
        }

        [Test]
        public void runShouldGenerateForRoyaltyWhenBooksAvailable()
        {
            try
            {
                LineItem[]
                lineItems = new LineItem[]{
            new LineItem("item1", "item1", new ProductCategory[]{
                ProductCategory.Books
            }),
            new LineItem("item2", "item2", new ProductCategory[]{
                ProductCategory.Membership,
            })
                };
                Order order = new Order(_mockCustomer.Object, lineItems, null);
                Payment payment = new Payment(order);

                IPackingSlipServices packingSlipService = _PackingSlipServices.Object;
                IShippingServices shippingService = _ShippingServices.Object;
                IRoyaltyServices royaltyService = _RoyaltyServices.Object;

                IPaymentProcess sut = new PackingSlipProcess(shippingService, royaltyService, packingSlipService);
                sut.run(payment);

                _ShippingServices.Verify(mock => mock.generatePackingSlip(order), Times.Once());
            }
            catch
            {
                throw;
            }
        }

        [Test]
        public void runShouldAddFirstAidGiftWhenRequested()
        {
            try
            {
                LineItem[]
                lineItems = new LineItem[]{
            new LineItem("learning-to-ski", "Learning to Ski", new ProductCategory[]{
                ProductCategory.Videos
            })
                };
                Order order = new Order(_mockCustomer.Object, lineItems, null);
                Payment payment = new Payment(order);

                IPackingSlipServices packingSlipService = _PackingSlipServices.Object;
                IShippingServices shippingService = _ShippingServices.Object;
                IRoyaltyServices royaltyService = _RoyaltyServices.Object;

                IPaymentProcess sut = new PackingSlipProcess(shippingService, royaltyService, packingSlipService);
                sut.run(payment);

                String[] gifts = order.getGiftProduct();
                Assert.NotNull(gifts);
                Assert.True(gifts.Count() == 1);
                Assert.True(Array.BinarySearch(gifts, "first-aid") == 0);
            }
            catch
            {
                throw;
            }
        }

        [Test]
        public void runShouldGeneratePackingSlip()
        {
            try
            {
                LineItem[]
                lineItems = new LineItem[]{
            new LineItem("item1", "item1", new ProductCategory[]{
                ProductCategory.Physical
            })
                };
                Order order = new Order(_mockCustomer.Object, lineItems, null);
                Payment payment = new Payment(order);

                IPackingSlipServices packingSlipService = _PackingSlipServices.Object;
                IShippingServices shippingService = _ShippingServices.Object;
                IRoyaltyServices royaltyService = _RoyaltyServices.Object;

                IPaymentProcess sut = new PackingSlipProcess(shippingService, royaltyService, packingSlipService);
                sut.run(payment);
                _PackingSlipServices.Verify(mock => mock.generate(order), Times.Once());
            }
            catch
            {
                throw;
            }

        }
    }
}
