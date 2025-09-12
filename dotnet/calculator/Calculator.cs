using System;
using System.Collections.Generic;


namespace CalculatorApp
{
    public static class Calculator
    {
        public static double Calculate(string input)
        {
            /*
             Главная функция: принимает строку-выражение и возвращает результат.
            */
            var tokens = Tokenize(input);
            var result = EvalTokens(tokens);
            return result;
        }

        public static List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var currentToken = "";
            var isNumber = false;
            for (var i = 0; i < expression.Length; i++)
            {
                if (expression[i] == '+' || expression[i] == '-' || expression[i] == '*' || expression[i] == '/' 
                    || expression[i] == '(' || expression[i] == ')')
                {
                    if (isNumber)
                    {
                        tokens.Add(currentToken);
                        currentToken = "";
                        isNumber = false;
                    }
                    tokens.Add(expression[i].ToString());
                }
                else if (expression[i] == '0' || expression[i] == '1' || expression[i] == '2' || expression[i] == '3' ||
                         expression[i] == '4' || expression[i] == '5' || expression[i] == '6' || expression[i] == '7' ||
                         expression[i] == '8' || expression[i] == '9')
                {
                    currentToken += expression[i];
                    isNumber = true;
                }
            }

            if (isNumber)
            {
                tokens.Add(currentToken);
            }
                
            
            return tokens;
        }

        public static double EvalTokens(List<string> Tokens)
        {
            
            var value = new Dictionary<string, int>();
            value.Add("+", 1);
            value.Add("-", 1);
            value.Add("*", 2);
            value.Add("/", 2);
            var operators = new Stack<string>();
            var numbers = new Stack<double>();
            
            double ApplyOperator(string op, double left, double right)
            {
                return op switch
                {
                    "+" => right + left,
                    "-" => right - left,
                    "*" => right * left,
                    "/" => right / left,
                    _   => throw new InvalidOperationException("Unknown operator")
                };
            }
            for (var i = 0; i < Tokens.Count; i++)
            {
                var token = Tokens[i];
                if (value.ContainsKey(token))
                {
                    while (operators.Count > 0 && operators.Peek() != "(" && value[operators.Peek()] >= value[token])
                    {
                        var arg1 = numbers.Pop();
                        var arg2 = numbers.Pop();
                        var operand = operators.Pop();
                        var res = ApplyOperator(operand, arg1, arg2);

                        numbers.Push(res);
                    }
                    operators.Push(token);
                }
                else if (token == "(")
                {
                    operators.Push(token);
                }
                else
                if (token == ")")
                {
                    while (operators.Count > 0 && operators.Peek() != "(")
                    {
                        var arg1 = numbers.Pop();
                        var arg2 = numbers.Pop();
                        var operand = operators.Pop();
                        var res = ApplyOperator(operand, arg1, arg2);
                        numbers.Push(res);
                    }
                    if (operators.Peek() == "(")
                    {
                        operators.Pop();
                    }
                }
                else
                {
                    numbers.Push(double.Parse(token));
                }
            }
            
            while (operators.Count > 0)
            {
                var arg1 = numbers.Pop();
                var arg2 = numbers.Pop();
                var operand = operators.Pop();
                var res = ApplyOperator(operand, arg1, arg2);
                numbers.Push(res);
            }

            return numbers.Pop();
        }
    }
}

