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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            if (!Validacao.erroRag)
            {
                switch (Validacao.banco)
                {
                    case "Banco do Brasil":
                        this.BackColor = Color.Yellow;
                        this.lblNomeBanco.ForeColor = Color.Blue;
                        break;
                    case "Caixa Econômica":
                        this.BackColor = Color.Blue;
                        this.lblNomeBanco.ForeColor = Color.White;
                        break;
                    case "Itau":
                        this.BackColor = Color.Blue;
                        this.lblNomeBanco.ForeColor = Color.Yellow;
                        break;
                    case "Nubank":
                        this.BackColor = Color.DarkViolet;
                        this.lblNomeBanco.ForeColor = Color.White;
                        break;
                    case "Bradesco":
                        this.BackColor = Color.Red;
                        this.lblNomeBanco.ForeColor = Color.White;
                        break;
                    default:
                        this.BackColor = SystemColors.Control;
                        this.lblNomeBanco.ForeColor = Color.Black;
                        break;
                }
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            lblNomeBanco.Text = Validacao.banco;
        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            Validacao validacao = new Validacao();
            validacao.ValidarSenha(txtSenha.Text);
            if(validacao.ap)
            {
                MessageBox.Show("Bem Vindo " + Dados.Nome + "!", "Login");
                Form3 form = new Form3();
                form.Show();
            }
            else
            {
                MessageBox.Show("Senha Incorreta!");

            }

        }

        private void txtSenha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 44)
            {
                e.Handled = true;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
