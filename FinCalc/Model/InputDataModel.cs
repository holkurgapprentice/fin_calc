using System;
namespace FinCalc.Model
{
    internal class InputDataModel
    {
        public double? Amount { get; set; }
        public double? InterestAnnualRate { get; set; }
        public int? LengthInMonths { get; set; }
        public DateTime? InceptionDate { get; set; }
    }
}
