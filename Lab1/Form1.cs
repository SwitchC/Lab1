using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ParserSpace;

namespace Lab1
{

    public partial class Form1 : Form
    {
        public static string FromNumberToWord(dynamic a)
        {
            return ((char)(a+'A'-1)).ToString();
        }
        Parser parser = new Parser();
        Namer handler = new Namer();
        public int currRow, currCol;
        public Dictionary<string, Cell> dictionary = new Dictionary<string, Cell>();
        public Dictionary<string, List<string>> dependson = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> depentfrom = new Dictionary<string, List<string>>();
        public Dictionary<string, int> entrance = new Dictionary<string, int>();
        public DataGridView mainTable()
        {
            return this.dataGridView1;
        }
        public Dictionary<string, Cell> getDict()
        {
            return this.dictionary;
        }
        public Form1()
        {
            InitializeComponent();
            DataGridViewColumn A = new DataGridViewColumn();
            DataGridViewColumn B = new DataGridViewColumn();
            DataGridViewColumn C = new DataGridViewColumn();
            DataGridViewColumn D = new DataGridViewColumn();
            DataGridViewColumn E = new DataGridViewColumn();
            DataGridViewColumn F = new DataGridViewColumn();
            DataGridViewColumn G = new DataGridViewColumn();
            DataGridViewColumn H = new DataGridViewColumn();
            DataGridViewColumn I = new DataGridViewColumn();
            Cell cellA = new Cell(); A.CellTemplate = cellA;
            Cell cellB = new Cell(); B.CellTemplate = cellB;
            Cell cellC = new Cell(); C.CellTemplate = cellC;
            Cell cellD = new Cell(); D.CellTemplate = cellD;
            Cell cellE = new Cell(); E.CellTemplate = cellE;
            Cell cellF = new Cell(); F.CellTemplate = cellF;
            Cell cellG = new Cell(); G.CellTemplate = cellG;
            Cell cellH = new Cell(); H.CellTemplate = cellH;
            Cell cellI = new Cell(); I.CellTemplate = cellI;
            A.HeaderText = "A";A.Name = "A";
            B.HeaderText = "B"; B.Name = "B";
            C.HeaderText = "C"; C.Name = "C";
            D.HeaderText = "D"; D.Name = "D";
            E.HeaderText = "E"; E.Name = "E";
            F.HeaderText = "F"; F.Name = "F";
            G.HeaderText = "G"; G.Name = "G";
            H.HeaderText = "H"; H.Name = "H";
            I.HeaderText = "I"; I.Name = "I";
            dataGridView1.Columns.Add(A);
            dataGridView1.Columns.Add(B);
            dataGridView1.Columns.Add(C);
            dataGridView1.Columns.Add(D);
            dataGridView1.Columns.Add(E);
            dataGridView1.Columns.Add(F);
            dataGridView1.Columns.Add(G);
            dataGridView1.Columns.Add(H);
            dataGridView1.Columns.Add(I);
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.Rows.Add(20);
            for (int i = 0; i < 20; i++)
            {
                dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }
        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string currCellName = FromNumberToWord(currCol + 1) + (currRow + 1).ToString();
                dictionary[currCellName].Exp = textBox1.Text;
            }
            catch { }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            string c = FromNumberToWord(currCol + 1) + (currRow + 1).ToString();
            List<string> anotherCells = handler.ListNameCell(dictionary[c]).Distinct().ToList();
            if (!dependson.ContainsKey(dictionary[c].Name))
            { dependson.Add(dictionary[c].Name, new List<string>()); }
            for (int i = 0; i < anotherCells.Count; i++)
            {
                dependson[dictionary[c].Name].Add(anotherCells[i]);
                if (!depentfrom.ContainsKey(anotherCells[i]))
                {
                    depentfrom.Add(anotherCells[i], new List<string>());
                }
                depentfrom[anotherCells[i]].Add(dictionary[c].Name);
            }
            CalcExp();
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e )
        {
            currRow = dataGridView1.CurrentCell.RowIndex;
            currCol = dataGridView1.CurrentCell.ColumnIndex;
            string currCellName = FromNumberToWord(currCol + 1) + (currRow + 1).ToString();
            if (!dictionary.ContainsKey(currCellName))
            {
                Cell cell = new Cell(currCellName, "0", "0");
                dictionary.Add(currCellName, cell);

            }
            label1.Text = currCellName;
            textBox1.Text = dictionary[currCellName].Exp;
        
        }
        private void buildEntranceDictionary()
        {
            entrance = new Dictionary<string, int>();
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string currcellName = FromNumberToWord(j + 1) + (i + 1).ToString();
                    entrance.Add(currcellName, 100);
                }
            }
        }
        public void CalcExp()
        {
            entrance = new Dictionary<string, int>();
            buildEntranceDictionary();

            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                for (int i = 0; i < dataGridView1.RowCount; i++)
                {
                    string currCellName = FromNumberToWord(j + 1) + (i + 1).ToString();

                    if (dictionary.ContainsKey(currCellName) && dictionary[currCellName].Value!=null)
                    {
                        try
                        {
                            dataGridView1.Rows[i].Cells[j].Value = CalcCell(dictionary[currCellName], dataGridView1).ToString();
                            dictionary[currCellName].Value = dataGridView1.Rows[i].Cells[j].Value.ToString();

                        }
                        catch (ClassOfExceptions.ExceptionRecursion)
                        {
                            MessageBox.Show(ClassOfExceptions.ExceptionRecursion.what);
                            dictionary[currCellName].Exp = "0";
                            dictionary[currCellName].Value = "0";
                            dataGridView1.Rows[i].Cells[j].Value = dictionary[currCellName].Value;
                            CalcExp();
                            return;
                        }

                    }
                }
            }
            entrance = new Dictionary<string, int>();
        }
        class ClassOfExceptions
        {
            public class ExceptionRecursion : Exception
            {
                public static string what = "Рекурсія";
            }
        }
        public double CalcCell(Cell c, DataGridView dgv)
        {
            try
            { 
                entrance[c.Name]--;
                if (entrance[c.Name] < 0)
                {
                    c.Value = "0";
                    throw new ClassOfExceptions.ExceptionRecursion();
                }
            
            }
            catch(KeyNotFoundException) { }
            string saveFormula = c.Exp;
            List<string> anotherCells = handler.ListNameCell(c).Distinct().ToList();
            if (anotherCells.Count == 0)
            {
                return parser.result(c.Exp);
            }
            else
            {
                for (int i = 0; i < anotherCells.Count; i++)
                {
                    if (dictionary.ContainsKey(anotherCells[i]))
                    {
                        c.Exp = c.Exp.Replace(anotherCells[i], CalcCell(dictionary[anotherCells[i]], dgv).ToString() + " ");
                    }
                    else
                    {
                        c.Exp = c.Exp.Replace(anotherCells[i], "0");
                    }
                }
                string finishFormula = c.Exp;
                c.Exp = saveFormula;
                return parser.result(finishFormula);
            
            }
        
        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

    }
}
