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

namespace Excel
{
    public partial class Excel : Form
    {
        public const int maxColumn = 26;
        public const int maxRow = 100000;
        private int NumberRow = 0;
        private int NumberColumn = 0;
        private List<DataGridViewColumn> alphabet = new List<DataGridViewColumn>();
        public static string FromNumberToWord(int a)
        {
            return ((char)(a + 'A' - 1)).ToString();
        }
        Parser parser = new Parser();
        Namer handler = new Namer();
        public int currRow, currCol;
        public Dictionary<string, Cell> dictionary = new Dictionary<string, Cell>();
        public Dictionary<string, List<string>> dependson = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> dependentfrom = new Dictionary<string, List<string>>();
        public Dictionary<string, int> entrance = new Dictionary<string, int>();

        public Dictionary<string, Cell> getDict()
        {
            return this.dictionary;
        }
        public Excel()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            DataGridViewColumn A = new DataGridViewColumn();
            DataGridViewColumn B = new DataGridViewColumn();
            DataGridViewColumn C = new DataGridViewColumn();
            DataGridViewColumn D = new DataGridViewColumn();
            DataGridViewColumn E = new DataGridViewColumn();
            DataGridViewColumn F = new DataGridViewColumn();
            DataGridViewColumn G = new DataGridViewColumn();
            DataGridViewColumn H = new DataGridViewColumn();
            DataGridViewColumn I = new DataGridViewColumn();
            DataGridViewColumn J = new DataGridViewColumn();
            DataGridViewColumn K = new DataGridViewColumn();
            DataGridViewColumn L = new DataGridViewColumn();
            DataGridViewColumn M = new DataGridViewColumn();
            DataGridViewColumn N = new DataGridViewColumn();
            DataGridViewColumn O = new DataGridViewColumn();
            DataGridViewColumn P = new DataGridViewColumn();
            DataGridViewColumn Q = new DataGridViewColumn();
            DataGridViewColumn R = new DataGridViewColumn();
            DataGridViewColumn S = new DataGridViewColumn();
            DataGridViewColumn T = new DataGridViewColumn();
            DataGridViewColumn U = new DataGridViewColumn();
            DataGridViewColumn V = new DataGridViewColumn();
            DataGridViewColumn W = new DataGridViewColumn();
            DataGridViewColumn X = new DataGridViewColumn();
            DataGridViewColumn Y = new DataGridViewColumn();
            DataGridViewColumn Z = new DataGridViewColumn();
            Cell cellA = new Cell(); A.CellTemplate = cellA;
            Cell cellB = new Cell(); B.CellTemplate = cellB;
            Cell cellC = new Cell(); C.CellTemplate = cellC;
            Cell cellD = new Cell(); D.CellTemplate = cellD;
            Cell cellE = new Cell(); E.CellTemplate = cellE;
            Cell cellF = new Cell(); F.CellTemplate = cellF;
            Cell cellG = new Cell(); G.CellTemplate = cellG;
            Cell cellH = new Cell(); H.CellTemplate = cellH;
            Cell cellI = new Cell(); I.CellTemplate = cellI;
            Cell cellJ = new Cell(); J.CellTemplate = cellJ;
            Cell cellK = new Cell(); K.CellTemplate = cellK;
            Cell cellL = new Cell(); L.CellTemplate = cellL;
            Cell cellM = new Cell(); M.CellTemplate = cellM;
            Cell cellN = new Cell(); N.CellTemplate = cellN;
            Cell cellO = new Cell(); O.CellTemplate = cellO;
            Cell cellP = new Cell(); P.CellTemplate = cellP;
            Cell cellQ = new Cell(); Q.CellTemplate = cellQ;
            Cell cellR = new Cell(); R.CellTemplate = cellR;
            Cell cellS = new Cell(); S.CellTemplate = cellS;
            Cell cellT = new Cell(); T.CellTemplate = cellT;
            Cell cellU = new Cell(); U.CellTemplate = cellU;
            Cell cellV = new Cell(); V.CellTemplate = cellV;
            Cell cellW = new Cell(); W.CellTemplate = cellW;
            Cell cellX = new Cell(); X.CellTemplate = cellX;
            Cell cellY = new Cell(); Y.CellTemplate = cellY;
            Cell cellZ = new Cell(); Z.CellTemplate = cellZ;
            A.HeaderText = "A"; A.Name = "A";
            B.HeaderText = "B"; B.Name = "B";
            C.HeaderText = "C"; C.Name = "C";
            D.HeaderText = "D"; D.Name = "D";
            E.HeaderText = "E"; E.Name = "E";
            F.HeaderText = "F"; F.Name = "F";
            G.HeaderText = "G"; G.Name = "G";
            H.HeaderText = "H"; H.Name = "H";
            I.HeaderText = "I"; I.Name = "I";
            J.HeaderText = "J"; J.Name = "J";
            K.HeaderText = "K"; K.Name = "K";
            L.HeaderText = "L"; L.Name = "L";
            M.HeaderText = "M"; M.Name = "M";
            N.HeaderText = "N"; N.Name = "N";
            O.HeaderText = "O"; O.Name = "O";
            P.HeaderText = "P"; P.Name = "P";
            Q.HeaderText = "Q"; Q.Name = "Q";
            R.HeaderText = "R"; R.Name = "R";
            S.HeaderText = "S"; S.Name = "S";
            T.HeaderText = "T"; T.Name = "T";
            U.HeaderText = "U"; U.Name = "U";
            V.HeaderText = "V"; V.Name = "V";
            W.HeaderText = "W"; W.Name = "W";
            X.HeaderText = "X"; X.Name = "X";
            Y.HeaderText = "Y"; Y.Name = "Y";
            Z.HeaderText = "Z"; Z.Name = "Z";
            DataGridViewColumn[] alp = { A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z };
            for (int i = 0; i < 26; i++)
            {
                alphabet.Add(alp[i]);
            }
            CreateTable(5, 5);
        }
        
        private void CreateTable(int row, int column)
        {
            NumberRow = row;
            NumberColumn = column;
            for (int i = 0; i < column; i++)
            {
                dataGridView.Columns.Add(alphabet[i]);
            }
            dataGridView.AllowUserToAddRows = false;
            dataGridView.Rows.Add(row);
            for (int i = 0; i < row; i++)
            {
                dataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
            }

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddRow_Click(object sender, EventArgs e)
        {
            NumberRow++;
            if (NumberRow > maxRow)
            {
                MessageBox.Show("MaxRow");
                NumberRow--;
                return;
            }
            try
            {
                dataGridView.Rows.Add(1);
                dataGridView.Rows[NumberRow - 1].HeaderCell.Value = (NumberRow).ToString();
            }
            catch (Exception)
            { }
        }

        private void AddColumn_Click(object sender, EventArgs e)
        {
            NumberColumn++;
            if (NumberColumn > maxColumn)
            {
                MessageBox.Show("MaxColumn");
                NumberColumn--;
                return;
            }
            try
            {
                dataGridView.Columns.Add(alphabet[NumberColumn - 1]);
            }
            catch (Exception)
            { }
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            try
            {
                string c = FromNumberToWord(currCol + 1) + (currRow + 1).ToString();
                List<string> anotherCells = handler.ListNameCell(dictionary[c]).Distinct().ToList();
                if (!dependson.ContainsKey(dictionary[c].Name))
                { dependson.Add(dictionary[c].Name, new List<string>()); }
                for (int i = 0; i < anotherCells.Count; i++)
                {
                    dependson[dictionary[c].Name].Add(anotherCells[i]);
                    if (!dependentfrom.ContainsKey(anotherCells[i]))
                    {
                        dependentfrom.Add(anotherCells[i], new List<string>());
                    }
                    dependentfrom[anotherCells[i]].Add(dictionary[c].Name);
                }
                CalcExp();
            }
            catch (Exception)
            { }
        }
        private void dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currRow = dataGridView.CurrentCell.RowIndex;
            currCol = dataGridView.CurrentCell.ColumnIndex;
            string currCellName = FromNumberToWord(currCol + 1) + (currRow + 1).ToString();
            if (!dictionary.ContainsKey(currCellName))
            {
                Cell cell = new Cell(currCellName, "0", "0");
                dictionary.Add(currCellName, cell);
            }
            textBox1.Text = dictionary[currCellName].Exp;
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
        }
        public void CalcExp()
        {
            entrance = new Dictionary<string, int>();
            buildEntranceDictionary();

            for (int j = 0; j < dataGridView.ColumnCount; j++)
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    string currCellName = FromNumberToWord(j + 1) + (i + 1).ToString();

                    if (dictionary.ContainsKey(currCellName) && dictionary[currCellName].Value != null)
                    {
                        try
                        {
                            dataGridView.Rows[i].Cells[j].Value = CalcCell(dictionary[currCellName], dataGridView).ToString();
                            dictionary[currCellName].Value = dataGridView.Rows[i].Cells[j].Value.ToString();

                        }
                        catch (ClassOfExceptions.ExceptionRecursion)
                        {
                            MessageBox.Show(ClassOfExceptions.ExceptionRecursion.what);
                            dictionary[currCellName].Exp = "0";
                            dictionary[currCellName].Value = "0";
                            dataGridView.Rows[i].Cells[j].Value = dictionary[currCellName].Value;
                            CalcExp();
                            return;
                        }

                    }
                }
            }
            entrance = new Dictionary<string, int>();
        }
        private void buildEntranceDictionary()
        {
            entrance = new Dictionary<string, int>();
            for (int j = 0; j < dataGridView.ColumnCount; j++)
            {
                for (int i = 0; i < dataGridView.RowCount; i++)
                {
                    string currcellName = FromNumberToWord(j + 1) + (i + 1).ToString();
                    entrance.Add(currcellName, 100);
                }
            }
        }

        class ClassOfExceptions
        {
            public class ExceptionRecursion : Exception
            {
                public static string what = "Рекурсія";
            }
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

        private void DelRow_Click(object sender, EventArgs e)
        {
            if (NumberRow > 0)
            {
                NumberRow--;
            }
            for (int i = 0; i < dataGridView.ColumnCount; i++)
            {
                string currCellName = FromNumberToWord(i + 1) + (dataGridView.RowCount).ToString();
                if (dictionary.ContainsKey(currCellName))
                {
                    if (dependentfrom[currCellName].Count != 0)
                    {
                        for (int j = 0; j < dependentfrom[currCellName].Count; j++)
                        {
                            dictionary[dependentfrom[currCellName][j]].Exp = "0";
                            dictionary[dependentfrom[currCellName][j]].Value = "0";
                        }
                    }
                    dictionary.Remove(currCellName);
                }
            }
            try
            {
                dataGridView.Rows.RemoveAt(dataGridView.Rows.Count - 1);
                CalcExp();
            }
            catch (Exception)
            { }
        }

        private void DelColumn_Click(object sender, EventArgs e)
        {
            if (NumberColumn > 0)
            {
                NumberColumn--;
            }
            if (NumberColumn== 0)
            {
                NumberRow = 0;
            }
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                string currCellName = FromNumberToWord(dataGridView.ColumnCount) + (i+1).ToString();
                if (dictionary.ContainsKey(currCellName))
                {
                    if (dependentfrom[currCellName].Count != 0)
                    {
                        for (int j = 0; j < dependentfrom[currCellName].Count; j++)
                        {
                            dictionary[dependentfrom[currCellName][j]].Exp = "0";
                            dictionary[dependentfrom[currCellName][j]].Value = "0";
                        }
                    }
                    dictionary.Remove(currCellName);
                }
            }
            try
            {
                dataGridView.Columns.RemoveAt(dataGridView.Columns.Count - 1);
                CalcExp();
            }
            catch (Exception)
            { }
        }

        private void save_Click(object sender, EventArgs e)
        {
            Save formSaveFile = new Save();
            formSaveFile.Show();
        }

        public string CalcCell(Cell c, DataGridView dgv)
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
            catch (KeyNotFoundException) { }
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
    }
    public class Cell : DataGridViewTextBoxCell
        {
            string val;
            string name;
            string exp;
            public Cell()
            {
                name = "";
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
                get { return name; }
                set { name = value; }
            }
            public string Value
            {
                get { return val; }
                set { val = value; }
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
                try
                {
                    Convert.ToDouble(s);
                    return false;
                }
                catch (Exception)
                {
                    int lenS = s.Length;
                    string colCord = "";
                    int rowCord = 0;
                    if ((s[lenS - 1] >= 'A' && s[lenS - 1] <= 'Z'))
                    {
                        return false;
                    }
                    for (int i = 1; i < lenS; i++)
                    {
                        if ((s[i] >= 'A' && s[i] <= 'Z') && (s[i - 1] >= '0' && s[i - 1] <= '9'))
                        {
                            return false;
                        }

                        if (s[i] >= 'A' && s[i] <= 'Z')
                        {
                            colCord += s[i];
                        }
                        if (s[i] >= '0' && s[i] <= '9')
                        {
                            rowCord += s[i] - '0';
                            rowCord *= 10;
                        }

                    }
                    return true;
                }
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
                    if ((s[pos] >= '0' && s[pos] <= '9') || (s[pos] <= 'Z' && s[pos] >= 'A'))
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
}
