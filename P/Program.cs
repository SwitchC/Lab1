using System;
using System.Collections.Generic;
namespace P
{
    class Parser
    {
        public
            List<string> POLIS = new List<string>();
        static int priority(string a)
        {

            switch (a)
            {
                case "(":
                    return 0;
                case ")":
                    return 1;
                case "+":
                    return 2;
                case "-":
                    return 2;
                case "*":
                    return 3;
                case "/":
                    return 3;
                case "^":
                    return 4;
         
                default:
                    return -1;
            }
        }
        static double checkNumber(string f)
        {
            try 
            {
                return Convert.ToDouble(f);
            }
            catch 
            {
                return -1;
            }


        }
        public Parser()
        { }
        public Parser(string line)
        {
            var answer = new List<string>();
            var MyStack = new Stack<string>();
            int f = 0;
            foreach (char t in line)
            {
                string i = Convert.ToString(t);
                if (i == " ") continue;
                if (priority(i) == -1)
                {
                    answer.Add("");
                    answer[f] += i;
                }
                if (priority(i) > 1)
                {

                    if (MyStack.Count == 0)
                    {
                        f++;
                        answer.Add("");
                        MyStack.Push(i);
                    }
                    else
                        while (true)
                        {
                            if (MyStack.Count == 0)
                            {
                                f++;
                                answer.Add("");
                                MyStack.Push(i);
                                break;
                            }
                            else if (priority(MyStack.Peek()) >= priority(i))
                            {
                                f++;
                                answer.Add("");
                                answer[f] += MyStack.Peek();
                                MyStack.Pop();
                            }
                            else
                            {
                                f++;
                                answer.Add("");
                                MyStack.Push(i);
                                break;
                            }
                        }
                }
                if (priority(i) == 0)
                {
                    answer.Add("");
                    f++;
                    MyStack.Push(i);
                };
                if (priority(i) == 1)
                {
                    try
                    {
                        while (true)
                        {
                            if (MyStack.Count == 0)
                            {
                                Console.Write("Error");
                                throw new Exception();
                            }
                            else if (priority(MyStack.Peek()) == 0) { MyStack.Pop(); break; }
                            else
                            {
                                answer.Add("");
                                f++;
                                answer[f] += MyStack.Pop();
                            };
                        }
                    }
                    catch
                    { break; }

                }

            }
            while (MyStack.Count != 0)
            {
                if (MyStack.Peek() == "(")
                {
                    Console.Write("Error");
                    break;
                }
                answer.Add("");
                f++;
                answer[f] += MyStack.Pop();
            }
            foreach (string a in answer)
            {
                if (a != "")
                {
                    POLIS.Add(a);
                }
            }
        }
        public double Calculate()
        {
            var MyStack = new Stack<double>();
            foreach (string i in POLIS)
            {
                double p= checkNumber(i);
                if (p != -1)
                {
                    MyStack.Push(p);
                }
                else
                {
                    try
                    {
                        double a = MyStack.Pop();
                        double b = MyStack.Pop();
                        if (i == "+")
                        { MyStack.Push(a + b); }
                        if (i == "-")
                        { MyStack.Push(a - b); }
                        if (i == "*")
                        { MyStack.Push(a * b); }
                        if (i == "/")
                        { MyStack.Push(a /b); }
                        if (i == "^")
                        { MyStack.Push(Math.Pow(b,a)); }
                    }
                    catch { Console.Write("Error"); }
                }
            }
            return MyStack.Pop();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                string d = Console.ReadLine();
                Parser a = new Parser(d);
                foreach (string p in a.POLIS)
                {
                    Console.Write(p);
                    Console.Write(" ");
                }
                Console.WriteLine("");
                Console.WriteLine(a.Calculate());
                Console.ReadKey();
            }
        }
    }
}
