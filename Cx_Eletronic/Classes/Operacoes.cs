using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Cx_Eletronic_2.Classes
{
    internal class Operacoes
    {
        public bool sAtorizado = false;
        public bool dAutorizado = false;
        public bool tAutorizado = false;
        public bool cR = false;
        public string mensagem = "";
        public string remetente = Dados.NomeR;

        public void Saque(string valor)
        {
            decimal valorAtual = Decimal.Parse(Dados.Saldo);
            decimal valorSacado = Decimal.Parse(valor);
            decimal saldo;
            DateTime date = DateTime.Today;

            saldo = valorAtual - valorSacado;

            Conexao con = new();
            SqlCommand cmd = new();

            cmd.CommandText = "update Conta_Bancaria set Saldo = @saldo where Número_da_Conta = @conta;" +
                "insert into Extrato values ('" + date.ToString("yyyy-MM-dd") + "', 'Saque', @valor, @conta, @saldo)";
            cmd.Parameters.AddWithValue("@saldo", saldo);
            cmd.Parameters.AddWithValue("@conta", Dados.conta);
            cmd.Parameters.AddWithValue("@valor", Decimal.Parse(valor));

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                con.Desconectar();
                sAtorizado = true;
                Dados.Saldo = saldo.ToString();
            }
            catch (SqlException)
            {
                mensagem = "Erro Ao Tentae Se Conectar com o Banco de Dados.";
            }
        }

        public void Deposito(string valor)
        {
            decimal valorAtual = Decimal.Parse(Dados.Saldo);
            decimal valorDepositado = Decimal.Parse(valor);
            decimal saldo;
            DateTime date = DateTime.Today;

            saldo = valorAtual + valorDepositado;

            Conexao con = new();
            SqlCommand cmd = new();

            cmd.CommandText = "update Conta_Bancaria set Saldo = @saldo where Número_da_Conta = @conta;" +
                "insert into Extrato values ('" + date.ToString("yyyy-MM-dd") + "','Depósito', @valor, @conta, @saldo)";
            cmd.Parameters.AddWithValue("@saldo", saldo);
            cmd.Parameters.AddWithValue("@conta", Dados.conta);
            cmd.Parameters.AddWithValue("@valor", Decimal.Parse(valor));

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                con.Desconectar();
                dAutorizado = true;
                Dados.Saldo = saldo.ToString();
            }
            catch (SqlException)
            {
                mensagem = "Erro Ao Tentae Se Conectar com o Banco de Dados.";
            }
        }

        public DataTable Extrato()
        {
            Conexao con = new();
            SqlCommand cmd = new();
            cmd.CommandText = "select _Data, Descricao, Valor, Saldo from Extrato where Conta = @conta_bank";
            cmd.Parameters.AddWithValue("@conta_bank", Dados.conta);
            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                con.Desconectar();

            }
            catch (SqlException)
            {
                mensagem = "Erro ao Se Conectar com o Banco de Dados.";
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable Extrato = new DataTable();
            adapter.Fill(Extrato);
            return Extrato;
        }

        public void Transferencia(string valor, string ag, string c)
        {
            decimal vT = Decimal.Parse(valor); //Valor a ser transferido e retirado
            decimal vA = decimal.Parse(Dados.Saldo); //Saldo da conta que está realizando a operacao
            decimal t = vA - vT;
            decimal vS = Decimal.Parse(Dados.SaldoR);
            decimal s = vS + vT;
            Conexao con = new();
            SqlCommand cmd = new();
            DateTime date = DateTime.Today;

            cmd.CommandText = "update Conta_Bancaria set Saldo = @t where Número_da_Conta = @conta;" +
                "update Conta_Bancaria set Saldo = (@vT + Saldo) where Número_da_Agência = @ag and Número_da_Conta = @c;" +
                "insert into Extrato values ('" + date.ToString("yyyy-MM-dd") + "', 'Transferência Enviada para " +
                remetente +
                "', @valor, @conta, @saldo)" +
                "insert into Extrato values ('" + date.ToString("yyyy-MM-dd") + "', 'Transferência Recebida de " +
                Dados.Nome +
                "', @valor, @c, @s)";
            cmd.Parameters.AddWithValue("@t", t);
            cmd.Parameters.AddWithValue("@conta", Dados.conta);
            cmd.Parameters.AddWithValue("@vT", vT);
            cmd.Parameters.AddWithValue("@ag", ag);
            cmd.Parameters.AddWithValue("@c", c);
            cmd.Parameters.AddWithValue("@valor", Decimal.Parse(valor));
            cmd.Parameters.AddWithValue("@saldo", t);
            cmd.Parameters.AddWithValue("@s", s);

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                con.Desconectar();
                tAutorizado = true;
                Dados.Saldo = t.ToString();
            }
            catch (SqlException)
            {
                mensagem = "Erro Ao Tentae Se Conectar com o Banco de Dados.";
            }
        }

        public void CompraA(string cartao)
        {
            Validacao validacao = new();
            validacao.ValidarCartao(cartao);
            Validacao.ReconhecerAgencia(cartao);
            Dados.ReconhecerUsuario(cartao);
            Conexao con = new();
            SqlCommand cmd = new();
            decimal valorP = decimal.Parse(Dados.valorProdutoA);
            decimal c = Decimal.Parse(Dados.Saldo);
            decimal saldo = c - valorP;
            DateTime date = DateTime.Today;

            cmd.CommandText = "update Conta_Bancaria set Saldo = @saldo where Número_da_Agência = " + Validacao.agencia + " and Número_da_Conta = " + Dados.conta + ";" +
               "insert into Extrato values('" + date.ToString("yyyy-MM-dd") + "', 'Compra Efetuada na Loja_A', @valor, @conta, @saldo)";
            cmd.Parameters.AddWithValue("@saldo", saldo);
            cmd.Parameters.AddWithValue("@conta", Dados.conta);
            cmd.Parameters.AddWithValue("@valor", Decimal.Parse(Dados.valorProdutoA));

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                con.Desconectar();
                Dados.Saldo = saldo.ToString();
                cR = true;
            }
            catch (SqlException)
            {
                mensagem = "Erro ao Tentar se Conectar com o Banco de Dados.";
            }
        }

        public void CompraB(string cartao)
        {
            Validacao validacao = new();
            validacao.ValidarCartao(cartao);
            Validacao.ReconhecerAgencia(cartao);
            Dados.ReconhecerUsuario(cartao);
            Conexao con = new();
            SqlCommand cmd = new();
            decimal valorP = decimal.Parse(Dados.valorProdutoB);
            decimal c = Decimal.Parse(Dados.Saldo);
            decimal saldo = c - valorP;
            DateTime date = DateTime.Today;

            cmd.CommandText = "update Conta_Bancaria set Saldo = @saldo where Número_da_Agência = " + Validacao.agencia + " and Número_da_Conta = " + Dados.conta + ";" +
               "insert into Extrato values('" + date.ToString("yyyy-MM-dd") + "', 'Compra Efetuada na Loja_B', @valor, @conta, @saldo)";
            cmd.Parameters.AddWithValue("@saldo", saldo);
            cmd.Parameters.AddWithValue("@conta", Dados.conta);
            cmd.Parameters.AddWithValue("@valor", Decimal.Parse(Dados.valorProdutoB));

            try
            {
                cmd.Connection = con.Conectar();
                cmd.ExecuteNonQuery();
                con.Desconectar();
                Dados.Saldo = saldo.ToString();
                cR = true;
            }
            catch (SqlException)
            {
                mensagem = "Erro ao Tentar se Conectar com o Banco de Dados.";
            }
        }
    }
}
