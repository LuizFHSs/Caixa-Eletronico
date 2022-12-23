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
    public partial class Form3 : Form
    {
        public static string ag = "";
        public static string c = "";
        public static string valor = "";

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Operacoes operacao = new Operacoes();
            Extrato extrato = new Extrato();
            
            lblBanco.Text = Validacao.banco;
            lblUser.Text = Dados.Nome;
            lblSaldo.Text = Dados.Saldo;

            dataGridView1.DataSource = operacao.Extrato();

        }

        private void btnSacar_Click(object sender, EventArgs e)
        {
            Operacoes operacao = new Operacoes();
            operacao.Saque(txtValor.Text);

            if (operacao.sAtorizado)
            {
                MessageBox.Show("Saque Efetuado com Sucesso!");
                lblSaldo.Text = Dados.Saldo;
                dataGridView1.DataSource = operacao.Extrato();
            }
        }

        private void btnDeposit_Click(object sender, EventArgs e)
        {
            Operacoes operacao = new Operacoes();
            operacao.Deposito(txtValor.Text);

            if (operacao.dAutorizado)
            {
                MessageBox.Show("Depósito Efetuado com Sucesso!");
                lblSaldo.Text = Dados.Saldo;
                dataGridView1.DataSource = operacao.Extrato();
            }
        }

        private void btnExtrato_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ag = txtAgencia.Text;
            c = txtConta.Text;
            valor = txtValor.Text;
            Dados dados = new();
            dados.ReconhecerRemetente(txtAgencia.Text, txtConta.Text);
            if (dados.achou)
            {
                Form5 form5 = new Form5();
                form5.Show();
                dataGridView1.DataSource = form5.source;
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            switch (Validacao.banco)
            {
                case "Banco do Brasil":
                    this.panel1.BackColor = Color.Yellow;
                    this.lblBanco.ForeColor = Color.Blue;
                    this.pictureBox1.Load("../../../Imagens/BB.png");
                    break;
                case "Caixa Econômica":
                    this.panel1.BackColor = Color.White;
                    this.lblBanco.ForeColor = Color.Blue;
                    this.pictureBox1.Load("../../../Imagens/Caixa_Economica.png");
                    break;
                case "Itau":
                    this.panel1.BackColor = Color.Blue;
                    this.lblBanco.ForeColor = Color.Yellow;
                    this.pictureBox1.Load("../../../Imagens/itau.png");
                    break;
                case "Nubank":
                    this.panel1.BackColor = Color.White;
                    this.lblBanco.ForeColor = Color.DarkViolet;
                    this.pictureBox1.Load("../../../Imagens/Nubank.png");
                    break;
                case "Bradesco":
                    this.panel1.BackColor = Color.White;
                    this.lblBanco.ForeColor = Color.Red;
                    this.pictureBox1.Load("../../../Imagens/bradesco.png");
                    break;
                default:
                    this.panel1.BackColor = SystemColors.Control;
                    this.lblBanco.ForeColor = Color.Black;
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lblSaldo.Text = Dados.Saldo;
        }
    }
}
