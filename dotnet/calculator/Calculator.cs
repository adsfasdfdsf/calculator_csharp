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
            var postfix = ToPostfix(tokens);
            var result = EvalPostfix(postfix);
            return result;
        }

        public static List<string> Tokenize(string expression)
        {
            /*
             Разбивает строку на токены (числа, операторы, скобки).
             Пример: "2 + 5 * ( 3 - 7 )"
             -> ["2", "+", "5", "*", "(", "3", "-", "7", ")"]
            */
            throw new NotImplementedException("implement me");
        }

        public static List<string> ToPostfix(List<string> tokens)
        {
            /*
             Преобразует выражение в постфиксную форму (обратная польская запись).

             Здесь мы используем ДВА "стека":
               1. output = []   // выходной список
               2. stack = []    // стек операторов и скобок

             🔹 Примеры:
             ["2", "+", "3"]                -> ["2", "3", "+"]
             ["2", "+", "3", "*", "4"]      -> ["2", "3", "4", "*", "+"]
             ["(", "2", "+", "3", ")", "*", "4"] -> ["2", "3", "+", "4", "*"]
             ["2", "+", "5", "*", "(", "3", "-", "7", ")"] -> ["2", "5", "3", "7", "-", "*", "+"]
            */
            var output = new List<string>();
            var stack = new Stack<string>();
            throw new NotImplementedException("implement me");
        }

        public static double EvalPostfix(List<string> postfixTokens)
        {
            /*
             Считает значение выражения в постфиксной записи.
             Здесь мы используем ОДИН стек чисел.

             🔹 Примеры:

             ["2", "3", "+"] -> 5
             ["2", "3", "5", "*", "+"] -> 17
             ["10", "2", "-", "3", "+"] -> 11
             ["2", "3", "+", "4", "1", "-", "*"] -> 15
            */
            var stack = new Stack<double>();

            throw new NotImplementedException("implement me");
        }
    }
}
