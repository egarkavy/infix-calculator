using InfixCalculator.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfixCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var calculator = new AlgebroicTranslator();
            calculator.ToPolandNotation();
        }
    }
}
