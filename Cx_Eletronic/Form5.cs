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
    public partial class Form5 : Form
    {
        public BindingSource source = new BindingSource();
        public Form5()
        {
            InitializeComponent();
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            label3.Text = Dados.NomeR;
            label4.Text = Dados.BancoR;
            label6.Text = Form3.valor;
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        { 
            Operacoes operacao = new Operacoes();
            operacao.Transferencia(Form3.valor, Form3.ag, Form3.c);
            source.DataSource = operacao.Extrato();

            this.Close();

            MessageBox.Show("Tranferência Realizada com Sucesso!");
        }
    }
}
