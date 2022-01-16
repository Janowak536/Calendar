using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calendar.Api.CalculatorForTests
{
    [Route("calculator/[controller]")]
        [ApiController]
        public class CalculatorController2 : ControllerBase
        {
            private ICalculator _calculator = null;

            public CalculatorController2(ICalculator calculator)
            {
                _calculator = calculator;
            }

            [HttpPost]
            [Route("Add")]
            public double Add(double a, double b)
            {
                return _calculator.Add(a, b);
            }
            [HttpPost]
            [Route("Divide")]
            public double Divide(double a, double b)
            {
                return _calculator.Divide(a, b);
            }
            [HttpPost]
            [Route("Multiply")]
            public double Multiply(double a, double b)
            {
                return _calculator.Multiply(a, b);
            }
            [HttpPost]
            [Route("Subtract")]
            public double Subtract(double a, double b)
            {
                return _calculator.Subtract(a, b);
            }
        }
    
}
