using FinCalc.Validator;
using System;

namespace FinCalc
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World! My name is Simple financial calculator");
            var inputData = Calculator.CollectData();

            Printer.Print(inputData);

            var validator = new InputDataModelValidator();
            var validationResult = validator.Validate(inputData);

            if (validationResult.IsValid)
            {
                var calculatedData = Calculator.Calc(inputData);
                Printer.Print(calculatedData);
            }
            else
            {
                Printer.Print(validationResult);
            }

            Console.WriteLine("Thats all, thank you, press any key to close");
            Console.ReadLine();
        }
    }
}
