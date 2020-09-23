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
    public class MembershipUpgradeTest
    {
        private Mock<Customer> _mockCustomer;

        private Mock<IRepositories> _Repositories;
        private Mock<INotificationServices> _NotificationServices;
        private Mock<Customer> _mockCustomerSpy;
        private Mock<ICustomer> _mockICustomer;

        [OneTimeSetUp]
        public void Setup()
        {
            _mockCustomer = new Mock<Customer>(MockBehavior.Loose);
            _Repositories = new Mock<IRepositories>(MockBehavior.Loose);
            _mockCustomerSpy = new Mock<Customer>(MockBehavior.Loose);
            _NotificationServices = new Mock<INotificationServices>(MockBehavior.Loose);
            _mockICustomer = new Mock<ICustomer>(MockBehavior.Loose);
        }

        [Test]
        public void runShouldUpgradeMembership()
        {
            try
            {
                Membership membershipSilver = new Membership("membership-silver", null);
                Membership membershipGold = new Membership("membership-gold", membershipSilver);

                LineItem[] lineItems = new LineItem[]{
                new LineItem(membershipGold.getProduct(), "gold", new ProductCategory[]{
                    ProductCategory.Membership
                })
            };
                ICustomer customer = _mockICustomer.Object;
                _mockICustomer.Setup(m => m.hasMembership(membershipSilver)).Returns(true);
                Order order = new Order(customer, lineItems, null);
                Payment payment = new Payment(order);

                IRepositories repo = _Repositories.Object;
                _Repositories.Setup(m => m.findByProduct(membershipGold.getProduct())).Returns(membershipGold);

                INotificationServices notificationService = _NotificationServices.Object;
                IPaymentProcess sut = new MembershipUpgrade(repo, notificationService);
                sut.run(payment);

                _mockICustomer.Verify(m => m.addMembership(membershipGold, notificationService), Times.Once());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Test]
        public void runShouldNotifyCustomer()
        {
            try
            {

                Membership membershipSilver = new Membership("membership-silver", null);
                Membership membershipGold = new Membership("membership-gold", membershipSilver);

                LineItem[] lineItems = new LineItem[]{
            new LineItem(membershipGold.getProduct(), "gold", new ProductCategory[]{
                ProductCategory.Membership
            })
            };
                ICustomer customer = _mockICustomer.Object;
                _mockICustomer.SetupSequence(m => m.hasMembership(membershipSilver)).Returns(true);
                Order order = new Order(customer, lineItems, null);
                Payment payment = new Payment(order);

                IRepositories repo = _Repositories.Object;
                _Repositories.SetupSequence(m => m.findByProduct(membershipGold.getProduct())).Returns(membershipGold);
                INotificationServices notificationService = _NotificationServices.Object;

                IPaymentProcess sut = new MembershipUpgrade(repo, notificationService);
                sut.run(payment);
                _NotificationServices.Verify(m => m.notify(customer, membershipGold), Times.Once());
            }
            catch
            {
                throw;
            }

        }

    }
}
