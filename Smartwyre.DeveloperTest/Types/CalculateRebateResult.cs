namespace Smartwyre.DeveloperTest.Types;

public class CalculateRebateResult
{
    public bool Success { get; set; }
    public decimal RebateAmount { get; set; }
    public Rebate Rebate { get; set; }
}
