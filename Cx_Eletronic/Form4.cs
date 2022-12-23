using Cx_Eletronic_2.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cx_Eletronic_2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void btnG_Ex_Click(object sender, EventArgs e)
        {
            Extrato ex = new();
            ex.DadosExtrato();
            string[] lines = File.ReadAllLines("../../../Arquivos/Extrato.txt");
            foreach(string line in lines)
            {
                textBox1.Text += line+Environment.NewLine;
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string texto = textBox1.Text + Environment.NewLine;
            Font fonte = new Font("Arial", 18, FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush cor = new SolidBrush(Color.Black);
            Point local = new Point(50, 50);

            e.Graphics.DrawString(texto, fonte, cor, local);
        }
    }
}
