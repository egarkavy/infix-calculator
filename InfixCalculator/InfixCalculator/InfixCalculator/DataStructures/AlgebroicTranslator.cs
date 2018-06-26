using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfixCalculator.DataStructures
{
    class AlgebroicTranslator
    {
        Stack<string> output = new Stack<string>();
        Stack<string> operationStack = new Stack<string>();
        Dictionary<string, int> operatesPriorities;
        Regex SymbolRegexp = new Regex(@"/|\+|\-|\*|\^");
        Regex DigitRegexp = new Regex(@"\d");

        private string Pattern { get; set; }
        public string Output { get; set; }
        public AlgebroicTranslator(string pattern = "3 + 4 * 2 / ( 1 - 5 ) ^ 2")
        {
            Pattern = pattern;
            operatesPriorities = new Dictionary<string, int>()
            {
                {"+", 1},
                {"-", 1},
                {"*", 2},
                {"/", 2},
                {"^", 3 }
            };
            Output = "";
        }

        public void ToPolandNotation()
        {
            var allSimbols = Pattern.Split(' ');

            foreach (var symbol in allSimbols)
            {
                if (symbol == "(")
                {
                    operationStack.Push(symbol);
                }
                if (DigitRegexp.IsMatch(symbol))
                {
                    output.Push(symbol);
                }
                if (symbol == ")")
                {

                    var ejected = operationStack.Pop();

                    while (ejected != "(")
                    {
                        output.Push(ejected);

                        ejected = operationStack.Pop();
                    }
                }
                if (SymbolRegexp.IsMatch(symbol))
                {
                    if (!operationStack.IsEmpty())
                    {
                        var ejected = operationStack.Pull();
                        while (operatesPriorities.Any(x => x.Key == ejected) && operatesPriorities[symbol] <= operatesPriorities[ejected])
                        {
                            output.Push(operationStack.Pop());
                            ejected = operationStack.Pull();
                        }
                    }

                    operationStack.Push(symbol);
                }
            }

            while (!operationStack.IsEmpty())
            {
                output.Push(operationStack.Pop());
            }

            Output += output.Pop();

            while (!output.IsEmpty())
            {
                Output += " " + output.Pop();
            }

            var answer = CalculatePrefix(Output);

            Console.WriteLine(answer);
        }

        private double CalculatePrefix(string expression)
        {
            var splittedSimbols = Output.Split(' ');
            operationStack = new Stack<string>();
            Stack<string> operandStack = new Stack<string>();

            Stack<string> stack = new Stack<string>();

            double result = 0;

            for (int i = 0; i < splittedSimbols.Count(); i++)
            {
                if (DigitRegexp.IsMatch(splittedSimbols[i])) //if digit
                {
                    if (!stack.IsEmpty() && DigitRegexp.IsMatch(stack.Pull())) //and stack also contains digit on the top
                    {
                        var operand = double.Parse(splittedSimbols[i]);

                        while (!stack.IsEmpty() && DigitRegexp.IsMatch(stack.Pull()))
                        {
                            var left = operand;
                            var right = double.Parse(stack.Pop());
                            var @operator = stack.Pop(); //operator MUST be on the top after above Pop()

                            operand = Calculate(left, right, @operator);
                        }

                        stack.Push(operand.ToString());
                    }
                    else
                    {
                        stack.Push(splittedSimbols[i]); //if no digits on the top - just Push()
                    }
                }
                else //if not digit - just Push()
                {
                    stack.Push(splittedSimbols[i]);
                }
            }
            return double.Parse(stack.Pop()); //answer will be on the top

        }

        private double Calculate(double left, double right, string @operator)
        {
            double result = 0;
            switch (@operator)
            {
                case "+":
                    {
                        result = left + right;
                        break;
                    }
                case "-":
                    {
                        result = left - right;
                        break;
                    }
                case "*":
                    {
                        result = left * right;
                        break;
                    }
                case "/":
                    {
                        result = left / right;
                        break;
                    }
                case "^":
                    {
                        result = Math.Pow(left, right);
                        break;
                    }
            }

            return result;
        }

    }
}
