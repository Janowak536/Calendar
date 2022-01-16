using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Api.CalculatorForTests
{
    public class Calculator : ICalculator
    {
        public double Add(double a, double b)
        {
            return (a + b);
        }

        public double Divide(double a, double b)
        {
            if (a == 0)
            {
                throw new DivideByZeroException("Nie można dzielić przez zero.");
            }
            return (a / b);
        }

        public double Multiply(double a, double b)
        {
            return (a * b);
        }

        public double Subtract(double a, double b)
        {
            return (a - b);
        }
    }
}
