using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.IO;
namespace Excel
{
    public partial class Save : Form
    {
        public Save()
        {
            InitializeComponent();
            Excel formMiniExcel = new Excel();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string path = @textBox1.Text + @"\" + textBox2.Text + ".txt";
                DataGridView mainTable = Program.MainForm.dataGridView;
                StreamWriter sw = new StreamWriter(path);
                sw.WriteLine(mainTable.ColumnCount);
                sw.WriteLine(mainTable.RowCount);
                Dictionary<string, Cell> mainDict = Program.MainForm.getDict();
                foreach (KeyValuePair<string, Cell> keyValue in mainDict)
                {
                    sw.WriteLine(keyValue.Key);
                    sw.WriteLine(mainDict[keyValue.Key].Value);
                    sw.WriteLine(mainDict[keyValue.Key].Exp);
                }
                sw.Flush();
                sw.Close();
                this.Close();
            }
            catch (Exception)
            { }
        }
    }
}
