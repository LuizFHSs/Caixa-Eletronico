using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cx_Eletronic_2.Classes
{
    internal class Validacao
    {
        public static string agencia = "";
        public static string banco = "";
        public static bool exiteC = false;
        public static bool erroRag = false;
        public bool ap = false;
        public string mensagem = "";

        public void ValidarCartao(string valor)
        {
            SqlCommand cmd = new();
            Conexao con = new();
            SqlDataReader dr;

            cmd.CommandText = "select Cartão from Conta_Bancaria where Cartão = @valor";
            cmd.Parameters.AddWithValue("@valor", valor);
            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    exiteC = true;
                }
                con.Desconectar();
                dr.Close();
            }
            catch (SqlException)
            {
                erroRag = true;
            }

        }

        public static void ReconhecerAgencia(string valor)
        {
            SqlCommand cmd = new();
            Conexao con = new();
            SqlDataReader dr;
            string num0;
            string num1;
            string num2;
            string num3;
            string num4;
            string num5;
            TextElementEnumerator text = StringInfo.GetTextElementEnumerator(valor);
            text.MoveNext();
            text.MoveNext();
            num0 = (string)text.Current;
            text.MoveNext();//3
            num1 = (string)text.Current;
            text.MoveNext();//4
            num2 = (string)text.Current;
            text.MoveNext();//5
            num3 = (string)text.Current;
            text.MoveNext();//6
            num4 = (string)text.Current;
            text.MoveNext();
            num5 = (string)text.Current;
            agencia = num0 + num1 + num2 + num3 + num4 + num5;

            if (exiteC)
            {
                cmd.CommandText = "select Número_da_Agência from Conta_Bancaria where Número_da_Agência = @agencia";
                cmd.Parameters.AddWithValue("@agencia", agencia);
                try
                {
                    cmd.Connection = con.Conectar();
                    dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        if (dr.Read())
                        {
                            if (dr["Número_da_Agência"].ToString() == agencia)
                            {
                                switch (agencia)
                                {
                                    case "123456":
                                        banco = "Banco do Brasil";
                                        break;
                                    case "234567":
                                        banco = "Caixa Econômica";
                                        break;
                                    case "345678":
                                        banco = "Itau";
                                        break;
                                    case "456789":
                                        banco = "Nubank";
                                        break;
                                    case "567891":
                                        banco = "Bradesco";
                                        break;
                                    default:
                                        banco = "Banco Não Reconhecido";
                                        break;
                                }
                            }
                        }
                    }
                    con.Desconectar();
                    dr.Close();
                }
                catch (SqlException)
                {
                    erroRag = true;
                }
            }
            else
            {
                erroRag = true;
            }
        }

        public void ValidarSenha(string valor)
        {
            Conexao con = new();
            SqlCommand cmd = new();
            SqlDataReader dr;
            string c = Dados.conta;
            cmd.CommandText = "select Número_da_Conta, Senha from Conta_Bancaria where Senha = @valor";
            cmd.Parameters.AddWithValue("@valor", valor);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    if(dr.Read())
                    {
                        if (dr["Número_da_Conta"].ToString() == c &&  valor == dr["Senha"].ToString())
                        {
                            ap = true;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                mensagem = "Falha ao Tentar Se Relacionar com o Banco de Dados.";
            }
        }

    }
}
