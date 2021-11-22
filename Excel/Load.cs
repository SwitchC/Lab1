using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Excel
{
    public partial class Load : Form
    {
        public Load()
        {
            InitializeComponent();
            Excel formMiniExcel = new Excel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = @textBox1.Text;
            List<string> files = (from a in Directory.GetFiles(path) select Path.GetFileName(a)).ToList();
            int k = 0;
            for (int i = 0; i < files.Count; i++)
            {
                if (textBox2.Text == files[i])
                {
                    k++;
                }
            }
            if (k == 0)
            {
                MessageBox.Show("NO FILE");
                return;
            }
            else
            {
                string namefile = @textBox2.Text;
                StreamReader sr = new StreamReader(@textBox1.Text+namefile);
                Program.MainForm.dictionary.Clear();
                Program.MainForm.dependentfrom.Clear();
                Program.MainForm.dependson.Clear();
                uint newColumnCount =0;
                uint newRowCount=0;
                for (; Program.MainForm.NumberColumn > 0;)
                {
                    Program.MainForm.DelColumn_Click(null, null);
                }
                for (; Program.MainForm.NumberRow > 1;)
                {
                    Program.MainForm.DelRow_Click(null, null);
                }
                try
                {
                    newColumnCount = UInt32.Parse(sr.ReadLine());
                    newRowCount = UInt32.Parse(sr.ReadLine());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                for (int i = 0; i < newRowCount; i++)
                {
                    Program.MainForm.NumberRow++;
                    if (Program.MainForm.NumberRow > Program.MainForm.maxRow)
                    {
                        MessageBox.Show("MaxRow");
                        Program.MainForm.NumberRow--;
                        return;
                    }
                    try
                    {
                        Program.MainForm.dataGridView.Rows.Add(1);
                        Program.MainForm.dataGridView.Rows[Program.MainForm.NumberRow - 1].HeaderCell.Value = (Program.MainForm.NumberRow).ToString();
                    }
                    catch (Exception)
                    { }
                }
                for(int i=0;i<newColumnCount;i++)
                {
                    Program.MainForm.AddColumn_Click(null, null);
                }
                string name;
                while ((name = sr.ReadLine()) != null)
                {
                    string value = sr.ReadLine();
                    string exp = sr.ReadLine();
                    Cell c = new Cell();
                    c.Name = name;
                    c.Value = value;
                    c.Exp = exp;
                    try
                    {
                        Program.MainForm.dictionary.Add(name, c);
                    }
                    catch (System.ArgumentException)
                    {
                        Program.MainForm.dictionary.Remove(name);
                        Program.MainForm.dictionary.Add(name, c);
                    }
                }
                Program.MainForm.CalcExp();
                this.Close();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
