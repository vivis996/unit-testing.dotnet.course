using System;
using Moq;
using TestNinja.Mocking;

namespace TestNinja.Tests.Mocking;

[TestFixture]
public class OrderServiceTests
{
    private Mock<IStorage> _storage;
    private OrderService _service;

    [SetUp]
    public void SetUp()
    {
        this._storage = new Mock<IStorage>();
        this._service = new OrderService(this._storage.Object);
    }

    [Test]
    public void PlaceOrder_WhenCalled_StoreTheOrder()
    {
        var order = new Order();
        this._service.PlaceOrder(order);

        // To check if the method was called
        this._storage.Verify(s => s.Store(order));
    }
}
