using FinCalc.Model;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinCalc
{
    internal static class Printer
    {
        internal static void Print(IList<InterestDetailModel> input)
        {
            if (input == null)
            {
                Console.WriteLine("Nothing to print");
            }
            else
            {
                Console.WriteLine("Table of interest");
                foreach (var interest in input)
                {
                    Console.WriteLine($"Payment no. {input.IndexOf(interest) + 1}/{input.Count} Amount: {interest.Amount:C} Interest: {interest.Interest:C} Principal: {interest.Principal:C} BalanceAfter: {interest.BalanceAfter:C} PaymentTerm: {interest.PaymentTerm:d}");
                }
                Console.WriteLine($"After {input.Count} payments you've paid {input.Sum(i => i.Amount):C} which contains interests {input.Sum(i => i.Interest):C}");
            }
        }

        internal static void Print(ValidationResult result)
        {
            if (result == null || result.Errors == null || !result.Errors.Any())
            {
                Console.WriteLine("Nothing to print");
            }
            else
            {
                Console.WriteLine("Errors found!");
                Console.WriteLine(result);
            }
        }

        internal static void Print(InputDataModel inputData)
        {
            if (inputData == null)
            {
                Console.WriteLine("Nothing to print");
            }
            else
            {
                Console.WriteLine($"Inputed data: Amount: {inputData.Amount:C} / Annual interest rate {inputData.InterestAnnualRate:P} / Length in months {inputData.LengthInMonths} / Inception date {inputData.InceptionDate:d}");
            }
        }
    }
}
