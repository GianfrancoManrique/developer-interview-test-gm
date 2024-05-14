using Smartwyre.DeveloperTest.Repositories;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private IProductRepository _productRepository;
    private IRebateRepository _rebateRepository;

    public RebateService(IProductRepository productRepository, IRebateRepository rebateRepository)
    {
        _productRepository = productRepository;
        _rebateRepository = rebateRepository;
    }

    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        Rebate rebate = _rebateRepository.GetRebate(request.RebateIdentifier);
        Product product = _productRepository.GetProduct(request.ProductIdentifier);

        var result = new CalculateRebateResult();
        var rebateAmount = 0m;

        if (rebate == null)
        {
            result.Success = false;
            return result;
        }

        switch (rebate?.Incentive)
        {
            case IncentiveType.FixedCashAmount:

                var invalidFixedCashAmount = !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount) || rebate.Amount == 0;
                if (invalidFixedCashAmount)
                {
                    result.Success = false;
                    return result;
                }

                rebateAmount = rebate.Amount;
                result.Success = true;
                result.RebateAmount = rebateAmount;
                result.Rebate = rebate;

                break;

            case IncentiveType.FixedRateRebate:

                var invalidFixedRateRebate = product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate) || (rebate.Percentage == 0 || product.Price == 0 || request.Volume == 0);
                if (invalidFixedRateRebate)
                {
                    result.Success = false;
                    return result;
                }

                rebateAmount += product.Price * rebate.Percentage * request.Volume;
                result.Success = true;
                result.RebateAmount = rebateAmount;
                result.Rebate = rebate;

                break;

            case IncentiveType.AmountPerUom:
                var invalidAmountPerUom = product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom) || (rebate.Amount == 0 || request.Volume == 0);
                if (invalidAmountPerUom)
                {
                    result.Success = false;
                    return result;
                }

                rebateAmount += rebate.Amount * request.Volume;
                result.Success = true;
                result.RebateAmount = rebateAmount;
                result.Rebate = rebate;

                break;
        }

        return result;
    }

    public Rebate StoreCalculation(CalculateRebateResult rebateResult)
    {
        if (rebateResult.Rebate == null)
        {
            return null;
        }
        return _rebateRepository.StoreCalculation(rebateResult.Rebate, rebateResult.RebateAmount);
    }
}
