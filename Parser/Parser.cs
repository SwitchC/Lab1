using System;
using System.Collections.Generic;
namespace ParserSpace
{
    public class Parser
    {
        public Parser()
        { }
        private byte priority(string s)
        {
            switch (s)
            {
                case ".": return 0;
                case "(":
                    return 1;
                case ")":
                    return 2;
                case "+":
                case "-":
                    return 3;
                case "*":
                case "/":
                    return 4;
                case "^":
                    return 5;
                case "max":
                case "min":
                case "mod":
                case "div":
                    return 6;
                case "~":
                    return 7;
                default: return 8;
            }
        }
        private List<string> separate(string t)
        {
            var answer = new List<string>();
            var line = "";
            for (int i = 0; i < t.Length; i++)
            {
                var symbol = t[i].ToString();
                if (symbol == " ") continue;
                else if (priority(symbol) <= 5 || priority(symbol) == 7 || symbol == ".")
                {
                    if (line.Length != 0) answer.Add(line);
                    line = "";
                    answer.Add(symbol);
                    continue;
                }
                else if (i < t.Length - 2 && i > 0)
                {
                    string func = "";
                    func = t[i].ToString() + t[i + 1].ToString() + t[i + 2].ToString();
                    if (priority(func) == 6)
                    {
                        if (line.Length != 0) answer.Add(line);
                        line = "";
                        answer.Add(func);
                        i++;
                        i++;
                        continue;
                    }
                    line += symbol;
                }
                else line += symbol;
            }
            if (line.Length != 0) answer.Add(line);
            return answer;
        }
        private List<string> postfix(string line)
        {
            var lineS = separate(line);
            Stack<string> stack = new Stack<string>();
            List<string> answer = new List<string>();
            if (lineS.Count == 0)
            {
                return lineS;
            }
            foreach (var t in lineS)
            {
                if (priority(t) == 8)
                {
                    answer.Add(t);
                    continue;
                }
                else if (priority(t) == 6)
                {
                    stack.Push(t);
                    continue;
                }
                else if (priority(t) == 1)
                {
                    stack.Push(t);
                    continue;
                }
                else if (priority(t) == 2)
                {
                    while (true)
                    {
                        if (stack.Count == 0)
                        {
                            break;
                        }
                        string top = stack.Pop();
                        if (top == "(" && stack.Count != 0 && priority(stack.Peek()) == 6)
                        {
                            answer.Add(stack.Pop());
                            break;
                        }
                        else if (top == "(")
                        {
                            break;
                        }
                        else answer.Add(top);

                    }
                }
                else if (priority(t) == 0)
                {
                    while (true)
                    {
                        if (stack.Count == 0)
                        {
                            break;
                        }
                        string top = stack.Pop();
                        if (top == "(")
                        {
                            stack.Push("(");
                            break;
                        }
                        else answer.Add(top);
                    }
                }
                else if ((priority(t) <= 5 && priority(t) > 2) || priority(t) == 7)
                {
                    while ((stack.Count != 0) && (priority(t) <= priority(stack.Peek())))
                    {
                        answer.Add(stack.Pop());
                    }
                    stack.Push(t);

                }
            }
            while (stack.Count != 0)
            {
                answer.Add(stack.Pop());
            }
            return answer;
        }
        public string result(string line)
        {
            try
            {
                var input = postfix(line);
                var stack = new Stack<double>();
                foreach (var w in input)
                {
                    if (priority(w) == 8)
                    {
                        stack.Push(Convert.ToDouble(w));
                        continue;
                    }
                    else if (w == "-")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(b - a);
                    }
                    else if (w == "+")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(b + a);
                    }
                    else if (w == "*")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(b * a);
                    }
                    else if (w == "/")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(b / a);
                    }
                    else if (w == "/")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(b / a);
                    }
                    else if (w == "^")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(Math.Pow(b, a));
                    }
                    else if (w == "~")
                    {
                        var a = stack.Pop();
                        stack.Push(a * (-1));
                    }
                    else if (w == "max")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(Math.Max(b, a));
                    }
                    else if (w == "min")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        stack.Push(Math.Min(b, a));
                    }
                    else if (w == "mod")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        var a1 = Convert.ToInt32(a);
                        var b1 = Convert.ToInt32(b);
                        stack.Push(b1 / a1);
                    }
                    else if (w == "div")
                    {
                        var a = stack.Pop();
                        var b = stack.Pop();
                        var a1 = Convert.ToInt32(a);
                        var b1 = Convert.ToInt32(b);
                        stack.Push(b1 % a1);
                    }
                }
                if (stack.Count > 1)
                {
                    return "Error";
                }
                string result = stack.Peek().ToString();
                double dResult = Convert.ToDouble(result);
                if (dResult < 0)
                {
                    double positivedResult = -1 * dResult;
                    string answer = "~" + positivedResult.ToString();
                    return answer;
                }
                return result;
            }
            catch (Exception)
            {
                return "Error";
            }
        }
    }
}
