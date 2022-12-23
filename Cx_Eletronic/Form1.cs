using Cx_Eletronic_2.Classes;

namespace Cx_Eletronic_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void txtNumCartao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 44)
            {
                e.Handled = true;
            }
        }

        private void btnAcessar_Click(object sender, EventArgs e)
        {
            Validacao validacao = new Validacao();
            validacao.ValidarCartao(txtNumCartao.Text);
            Validacao.ReconhecerAgencia(txtNumCartao.Text);
            Dados.ReconhecerUsuario(txtNumCartao.Text);
            if(Validacao.exiteC)
            {
                Form2 form = new Form2();
                form.Show();
            }
        }

        private void btnComprar_Click(object sender, EventArgs e)
        {
            Form6 form = new Form6();
            form.Show();
        }
    }
}