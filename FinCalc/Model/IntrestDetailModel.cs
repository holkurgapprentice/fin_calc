using System;

namespace FinCalc.Model
{
    internal class InterestDetailModel
    {
        public double Amount { get; set; }
        public double Interest { get; set; }
        public double Principal { get; set; }
        public double BalanceAfter { get; set; }
        public DateTime PaymentTerm { get; set; }
    }
}
