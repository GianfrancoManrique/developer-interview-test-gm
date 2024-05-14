using NUnit.Framework;
using Moq;
using Smartwyre.DeveloperTest.MockData;
using Smartwyre.DeveloperTest.Repositories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Tests;

[TestFixture]
public class PaymentServiceTests
{
    private Mock<IProductRepository> _productRepository { get; set; }
    private Mock<IRebateRepository> _rebateRepository { get; set; }
    private RebateService _rebateService;

    [SetUp]
    public void Setup()
    {
        _productRepository = new Mock<IProductRepository>();
        _rebateRepository = new Mock<IRebateRepository>();
        _rebateService = new RebateService(_productRepository.Object, _rebateRepository.Object);
    }

    [Test]
    public void CalculateRebateNull_Fails()
    {
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r9999",
            ProductIdentifier = "p1",
            Volume = 5
        };

        _productRepository.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(ProductMockData.Products[0]);

        var result = _rebateService.Calculate(request);

        Assert.That(result.Success.Equals(false));
        Assert.That(result.RebateAmount.Equals(0));
    }

    [Test]
    public void CalculateRebateAmountPerUom_Success()
    {
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r1",
            ProductIdentifier = "p1",
            Volume = 5
        };

        _productRepository.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(ProductMockData.Products[0]);
        _rebateRepository.Setup(p => p.GetRebate(request.RebateIdentifier)).Returns(RebateMockData.Rebates[0]);

        var result = _rebateService.Calculate(request);

        Assert.That(result.Success.Equals(true));
        Assert.That(result.RebateAmount.Equals(500));
    }

    [Test]
    public void CalculateRebateAmountPerUom_Fails()
    {
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r1",
            ProductIdentifier = "p1",
            Volume = 0
        };

        _productRepository.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(ProductMockData.Products[0]);
        _rebateRepository.Setup(p => p.GetRebate(request.RebateIdentifier)).Returns(RebateMockData.Rebates[0]);

        var result = _rebateService.Calculate(request);

        Assert.That(result.Success.Equals(false));
        Assert.That(result.RebateAmount.Equals(0));
    }

    [Test]
    public void CalculateFixedRateRebate_Success()
    {
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r3",
            ProductIdentifier = "p3",
            Volume = 5
        };

        _productRepository.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(ProductMockData.Products[2]);
        _rebateRepository.Setup(p => p.GetRebate(request.RebateIdentifier)).Returns(RebateMockData.Rebates[2]);

        var result = _rebateService.Calculate(request);

        Assert.That(result.Success.Equals(true));
        Assert.That(result.RebateAmount.Equals(45.00m));
    }

    [Test]
    public void CalculateFixedRateRebate_Fails()
    {
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r3",
            ProductIdentifier = "p3",
            Volume = 0
        };

        _productRepository.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(ProductMockData.Products[2]);
        _rebateRepository.Setup(p => p.GetRebate(request.RebateIdentifier)).Returns(RebateMockData.Rebates[2]);

        var result = _rebateService.Calculate(request);

        Assert.That(result.Success.Equals(false));
        Assert.That(result.RebateAmount.Equals(0));
    }

    [Test]
    public void CalculateFixedCashAmount_Success()
    {
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r2",
            ProductIdentifier = "p2"
        };

        _productRepository.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(ProductMockData.Products[1]);
        _rebateRepository.Setup(p => p.GetRebate(request.RebateIdentifier)).Returns(RebateMockData.Rebates[1]);

        var result = _rebateService.Calculate(request);

        Assert.That(result.Success.Equals(true));
        Assert.That(result.RebateAmount.Equals(200));
    }

    [Test]
    public void CalculateFixedCashAmount_Fails()
    {
        CalculateRebateRequest request = new CalculateRebateRequest
        {
            RebateIdentifier = "r4",
            ProductIdentifier = "p2",
        };

        _productRepository.Setup(p => p.GetProduct(request.ProductIdentifier)).Returns(ProductMockData.Products[1]);
        _rebateRepository.Setup(p => p.GetRebate(request.RebateIdentifier)).Returns(RebateMockData.invalidRebate);

        var result = _rebateService.Calculate(request);

        Assert.That(result.Success.Equals(false));
        Assert.That(result.RebateAmount.Equals(0));
    }

    [Test]
    public void StoreCalculation_Success()
    {
        var calculateResult = new CalculateRebateResult
        {
            Rebate = RebateMockData.Rebates[0],
            RebateAmount = 500
        };

        _rebateRepository.Setup(p => p.StoreCalculation(calculateResult.Rebate, calculateResult.RebateAmount)).Returns(RebateMockData.updatedRebate);

        var result = _rebateService.StoreCalculation(calculateResult);

        Assert.That(result.Amount.Equals(500));
    }

    [Test]
    public void StoreCalculation_Fails()
    {
        var calculateResult = new CalculateRebateResult { RebateAmount = 500 };

        _rebateRepository.Setup(p => p.StoreCalculation(calculateResult.Rebate, calculateResult.RebateAmount)).Returns(RebateMockData.updatedRebate);

        var result = _rebateService.StoreCalculation(calculateResult);

        Assert.That(result, Is.Null);
    }
}
