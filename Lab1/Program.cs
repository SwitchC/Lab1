using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParserSpace;
namespace Lab1
{
    public class Cell : DataGridViewTextBoxCell
    {
        string val;
        string name;
        string exp;
        public Cell()
        {
            name = "A" + 1;
            val = "";
            exp = "";
        }
        public Cell(string n, string v, string e)
        {
            name = n;
            val = v;
            exp = e;
        }
        public string Name
        {
            get { return name;}
            set { name = value;}
        }
        public string Value
        {
            get { return val; }
            set { val= value; }
        }
        public string Exp
        {
            get { return exp; }
            set { exp = value; }
        }
    }
    public class Namer
    {
        public bool IsName(string s)
        {
            int lenS = s.Length;
            string colCord = "";
            int rowCord=0;
            if ((s[lenS - 1] >= 'A' && s[lenS - 1] <= 'Z'))
            {
                MessageBox.Show("Клітина не знайдена");
                return false;
            }
            for (int i = 1; i < lenS; i++)
            {
                if (s[i] >= 'A' && s[i] <= 'Z')
                {
                    colCord += s[i];
                }
                if (s[i] >= '0' && s[i] <= '9')
                {
                    rowCord += s[i] - '0';
                    rowCord *= 10;
                }
                if(s[i]>='A' && s[i]<='Z' && s[i-1]>='0'&& s[i-1]<='9')
                {
                    MessageBox.Show("Помилка в імені клітини");
                    return false;
                }
            }
            return true;
        }
        public List<string> ListNameCell(Cell cell)
        {
            string s = cell.Exp;
            List<string> res = new List<string>();
            int pos = 0;
            int lenS = s.Length;
            while (pos < lenS)
            {
                string name = "";
                if (s[pos] <= 'Z' && s[pos] >= 'A')
                {
                    while (pos < lenS && ((s[pos] >= '0' && s[pos] <= '9') || (s[pos] <= 'Z' && s[pos] >= 'A')))
                    {
                        name += s[pos];
                        pos++;
                    }
                    if (IsName(name)) res.Add(name);
                    else { }
                }
                pos++;
            }
            return res;
        }
    }
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
