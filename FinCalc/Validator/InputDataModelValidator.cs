using FinCalc.Model;
using FluentValidation;
using System;

namespace FinCalc.Validator
{
    internal class InputDataModelValidator : AbstractValidator<InputDataModel>
    {
        public InputDataModelValidator()
        {
            RuleFor(i => i.Amount)
                .GreaterThan(0);
            RuleFor(i => i.InterestAnnualRate)
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(1);
            RuleFor(i => i.LengthInMonths)
                .GreaterThan(0);
            RuleFor(i => i.InceptionDate)
                .GreaterThan(DateTime.Now);
        }
    }
}