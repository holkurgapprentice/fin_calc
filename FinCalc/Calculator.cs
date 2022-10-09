using FinCalc.Model;
using System;
using System.Collections.Generic;

namespace FinCalc
{
    internal static class Calculator
    {
        internal static InputDataModel CollectData()
        {
            var result = new InputDataModel
            {
                Amount = Collect("Step 1/4 Provide amount of loan", double.Parse),
                InterestAnnualRate = Collect("Step 2/4 Provide interest rate", double.Parse),
                LengthInMonths = Collect("Step 3/4 Provide loan lenght", int.Parse),
                InceptionDate = Collect("Step 4/4 Provide inception date", DateTime.Parse)
            };

            return result;
        }

        private static T Collect<T>(string welcomeMessage, Func<string, T> parse)
        {
            object result = null;
            Console.WriteLine(welcomeMessage);
            do
            {
                var value = Console.ReadLine();
                try
                {
                    result = parse(value);
                    Console.WriteLine("Success!");
                }
                catch
                {
                    Console.WriteLine($"Something went wrong this is not proper for current field you provided: >>{value}<<");
                    Console.WriteLine("Try again");
                }
            }
            while (result == null);

            return (T)result;
        }

        internal static IList<InterestDetailModel> Calc(InputDataModel inputData)
        {
            var result = new List<InterestDetailModel>();
            var dateTo = inputData.InceptionDate.Value.AddMonths(inputData.LengthInMonths.Value);

            var inputIntrest = new InputIntrestRateDataModel
            {
                Amount = inputData.Amount.Value,
                InterestRate = AnnualRateToWeekly(inputData.InterestAnnualRate.Value),
                Cycles = CountWeeksInDataSpan(inputData.InceptionDate.Value, dateTo)
            };

            var evenPaymentWithInterest = Math.Round(CalcPayment(inputIntrest), 2);
            var principalOnlyEvenPayment = Math.Round(inputIntrest.Amount / inputIntrest.Cycles, 2);

            int cycleNo = 0;

            double principaBallance = 0;

            for (DateTime currentWeek = inputData.InceptionDate.Value; currentWeek.Date <= dateTo; currentWeek = currentWeek.AddDays(7))
            {
                var currentIntrest = new InterestDetailModel();
                if (currentWeek.Date == inputData.InceptionDate.Value.Date)
                    continue;

                cycleNo++;
                principaBallance += principalOnlyEvenPayment;

                if (cycleNo == inputIntrest.Cycles)
                {
                    principaBallance -= principalOnlyEvenPayment;
                    var lastPrincipal = inputIntrest.Amount - principaBallance;

                    currentIntrest.PaymentTerm = currentWeek;
                    currentIntrest.Interest = evenPaymentWithInterest - principalOnlyEvenPayment;
                    currentIntrest.Principal = lastPrincipal;
                    currentIntrest.Amount = currentIntrest.Principal + currentIntrest.Interest;
                    currentIntrest.BalanceAfter = principaBallance + currentIntrest.Principal;
                }
                else
                {
                    currentIntrest.PaymentTerm = currentWeek;
                    currentIntrest.Amount = evenPaymentWithInterest;
                    currentIntrest.Principal = principalOnlyEvenPayment;
                    currentIntrest.Interest = evenPaymentWithInterest - principalOnlyEvenPayment;
                    currentIntrest.BalanceAfter = principaBallance;
                }

             

                result.Add(currentIntrest);
            }

            return result;
        }

        private static int CountWeeksInDataSpan(DateTime inceptionDate, DateTime dateTo) => 
            (int)Math.Floor((dateTo - inceptionDate).TotalDays / 7);

        private static double AnnualRateToWeekly(double interestAnnualRate) => 
            interestAnnualRate * 7 / 365;

        private static double CalcPayment(InputIntrestRateDataModel input) =>
            input.Amount * (input.InterestRate / (1 - Math.Pow(1 + input.InterestRate, -input.Cycles)));
    }
}