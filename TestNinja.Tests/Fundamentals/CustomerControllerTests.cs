﻿using TestNinja.Fundamentals;

namespace TestNinja.Tests.Fundamentals;

[TestFixture]
public class CustomerControllerTests
{
    private CustomerController _controller;

    [SetUp]
    public void SetUp()
    {
        this._controller = new CustomerController();
    }

    [Test]
    public void GetCustomer_IdIsZero_ReturnNotFound()
    {
        var result = _controller.GetCustomer(0);

        // NotFound
        Assert.That(result, Is.TypeOf<NotFound>());

        // NotFound or one of its derivatives
        // Assert.That(result, Is.InstanceOf<NotFound>());
    }

    [Test]
    public void GetCustomer_IdIsNotZero_ReturnOk()
    {
        var result = _controller.GetCustomer(1);

        // Ok
        Assert.That(result, Is.TypeOf<Ok>());

        // Ok or one of its derivatives
        // Assert.That(result, Is.InstanceOf<Ok>());
    }
}
