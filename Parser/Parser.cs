using System;
using System.Collections.Generic;
namespace ParserSpace
{
    public class Parser
    {
        public Parser()
        { }
        public string str_error = "";
        private List<string> operators = new List<string>(new string[]
        {"(",")","+","-","*","/","^","max","min","mod","div","~"});
        private List<string> standart_operators = new List<string>(new string[]
        {"(",")","+","-","*","/","^"});
        private byte priority(string s)
        {
            switch (s)
            {
                case "(":
                case ")":
                    return 0;
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                case "^":
                    return 3;
                case "max":
                case "min":
                case "mod":
                case "div":
                    return 4;
                case "~":
                    return 5;
                default: return 6;
            }
        }
        private bool thisIsAFunction(string line)
        {
            return priority(line) == 4;
        }
        private List<string> Separated(string c)
        {
            List<string> output = new List<string>();
            string Line = new string("");
            for (int k = 0; k < c.Length; k++)
            {
                string symbol = c[k].ToString();
                if (symbol == ".")
                {
                    if (Line != "")
                        output.Add(Line);
                    Line = "";
                    continue;
                }
                if (symbol == " ") continue;
                else
                if (priority(symbol) <= 3 || priority(symbol) == 4)
                {
                    if (Line != "")
                        output.Add(Line);
                    Line = "";
                    output.Add(symbol);
                }
                else

                    if (k + 2 < c.Length)
                {
                    string fu = new string("");
                    fu += c[k].ToString();
                    fu += c[k + 1].ToString();
                    fu += c[k + 2].ToString();
                    if (thisIsAFunction(fu))
                    {
                        if (Line != "")
                            output.Add(Line);
                        Line = "";
                        output.Add(fu);
                        k++;
                        k++;
                        continue;
                    }
                    else Line += symbol;
                }
                else Line += symbol;
                if (k == c.Length - 1 && priority(symbol) == 6)
                { output.Add(Line); }
            }
            return output;
        }
        public string[] Postfix(string input)
        {
            List<string> outputSep = new List<string>();
            Stack<string> stack = new Stack<string>();
            foreach (string c in Separated(input))
            {
                if (operators.Contains(c))
                {
                    if (stack.Count > 0 && !c.Equals("("))
                    {
                        if (c.Equals(")"))
                        {
                            string s = stack.Pop();
                            while (s != "(")
                            {
                                outputSep.Add(s);
                                s = stack.Pop();
                            }
                        }
                        else if (priority(c) > priority(stack.Peek()))
                            stack.Push(c);
                        else
                        {
                            while (stack.Count > 0 && priority(c) <= priority(stack.Peek()))
                                outputSep.Add(stack.Pop());
                            stack.Push(c);
                        }
                    }
                    else stack.Push(c);
                }
                else
                {
                    for (int i = 0; i < c.Length; i++)
                    {
                        if (Char.IsLetter(c[i]))
                        {
                            throw new Exception();
                        }
                        
                    }
                    outputSep.Add(c);
                }
            }
            if (stack.Count > 0)
                foreach (string c in stack)
                 outputSep.Add(c);
            return outputSep.ToArray();
        }

        public double result(string input)
        {
            Stack<string> stack = new Stack<string>();
            Queue<string> queue = new Queue<string>(Postfix(input));
            if (queue.Count == 0)
                return 0;
            string str = queue.Dequeue();
            try
            {
                while (queue.Count >= 0)
                {
                    if (!operators.Contains(str))
                    {
                        stack.Push(str);
                        if (queue.Count > 0)
                            str = queue.Dequeue();
                        else break;
                    }
                    else
                    {
                        double summ = 0;
                        switch (str)
                        {
                            case "+":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = a + b;
                                    break;
                                }
                            case "~":
                                {
                                    double a = Convert.ToDouble(stack.Pop());
                                    summ = -a;
                                    break;
                                }
                            case "-":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = b - a;
                                    break;
                                }
                            case "*":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = a * b;
                                    break;

                                }
                            case "/":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = b / a;
                                    break;

                                }
                            case "max":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = Math.Max(a, b);
                                    break;

                                }
                            case "min":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = Math.Min(a, b);
                                    break;

                                }
                            case "mod":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = b % a;
                                    break;
                                }
                            case "div":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = b / a;
                                    break;

                                }
                            case "^":
                                {

                                    double a = Convert.ToDouble(stack.Pop());
                                    double b = Convert.ToDouble(stack.Pop());
                                    summ = Math.Pow(b, a);
                                    break;

                                }
                        }
                        stack.Push(summ.ToString());
                        if (queue.Count > 0)
                            str = queue.Dequeue();
                        else break;
                    }
                }
                string ss = stack.Pop();
                return Convert.ToDouble(ss);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
        
        }

    }
}
