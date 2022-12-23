using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cx_Eletronic_2.Classes
{
    internal class Dados
    {
        public static string Nome = "";
        public static string NomeR = "";
        public static string BancoR = "";
        public static string Saldo = "";
        public static string SaldoR = "";
        public static string conta = "";
        public static string mensagem = "";
        public bool achou = false;
        public static string valorProdutoA = "";
        public static string valorProdutoB = "";


        public static void ReconhecerUsuario(string valor)
        {
            Conexao con = new();
            SqlCommand cmd = new();
            SqlDataReader dr;
            string num0;
            string num1;
            string num2;
            string num3;
            string num4;
            string num5;
            string num6;
            string num7;
            string ag = Validacao.banco;
            TextElementEnumerator text = StringInfo.GetTextElementEnumerator(valor);
            text.MoveNext();//2
            text.MoveNext();//3
            text.MoveNext();//4
            text.MoveNext();//5
            text.MoveNext();//6
            text.MoveNext();//7
            text.MoveNext();//8
            text.MoveNext();//9
            num0 = (string)text.Current;//2
            text.MoveNext();
            num1 = (string)text.Current;//1
            text.MoveNext();
            num2 = (string)text.Current;//4
            text.MoveNext();
            num3 = (string)text.Current;//3
            text.MoveNext();
            num4 = (string)text.Current;//6
            text.MoveNext();
            num5 = (string)text.Current;//5
            text.MoveNext();
            num6 = (string)text.Current;//8
            text.MoveNext();
            num7 = (string)text.Current;
            conta = num0 + num1 + num2 + num3 + num4 + num5 + num6 + num7;

            switch(ag)
            {
                case "Banco do Brasil":
                    cmd.CommandText = "select Nome, Saldo, Número_da_Conta from Conta_Bancaria where Número_da_Conta = @conta";
                    cmd.Parameters.AddWithValue("@conta", conta);
                    try
                    {
                        cmd.Connection = con.Conectar();
                        dr = cmd.ExecuteReader();
                        if(dr.HasRows)
                        {
                            if(dr.Read())
                            {
                                if (dr["Número_da_Conta"].ToString() == conta)
                                {
                                    Nome = dr["Nome"].ToString();
                                    Saldo = dr["Saldo"].ToString();
                                }
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        mensagem = "Erro ao Tentar Se Relacionar com o Banco de Dados.";
                    }
                    break;
                case "Caixa Econômica":
                    cmd.CommandText = "select Nome, Saldo, Número_da_Conta from Conta_Bancaria where Número_da_Conta = @conta";
                    cmd.Parameters.AddWithValue("@conta", conta);
                    try
                    {
                        cmd.Connection = con.Conectar();
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                if (dr["Número_da_Conta"].ToString() == conta)
                                {
                                    Nome = dr["Nome"].ToString();
                                    Saldo = dr["Saldo"].ToString();
                                }
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        mensagem = "Erro ao Tentar Se Relacionar com o Banco de Dados.";
                    }
                    break;
                case "Itau":
                    cmd.CommandText = "select Nome, Saldo, Número_da_Conta from Conta_Bancaria where Número_da_Conta = @conta";
                    cmd.Parameters.AddWithValue("@conta", conta);
                    try
                    {
                        cmd.Connection = con.Conectar();
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                if (dr["Número_da_Conta"].ToString() == conta)
                                {
                                    Nome = dr["Nome"].ToString();
                                    Saldo = dr["Saldo"].ToString();
                                }
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        mensagem = "Erro ao Tentar Se Relacionar com o Banco de Dados.";
                    }
                    break;
                case "Nubank":
                    cmd.CommandText = "select Nome, Saldo, Número_da_Conta from Conta_Bancaria where Número_da_Conta = @conta";
                    cmd.Parameters.AddWithValue("@conta", conta);
                    try
                    {
                        cmd.Connection = con.Conectar();
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                if (dr["Número_da_Conta"].ToString() == conta)
                                {
                                    Nome = dr["Nome"].ToString();
                                    Saldo = dr["Saldo"].ToString();
                                }
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        mensagem = "Erro ao Tentar Se Relacionar com o Banco de Dados.";
                    }
                    break;
                case "Bradesco":
                    cmd.CommandText = "select Nome, Saldo, Número_da_Conta from Conta_Bancaria where Número_da_Conta = @conta";
                    cmd.Parameters.AddWithValue("@conta", conta);
                    try
                    {
                        cmd.Connection = con.Conectar();
                        dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            if (dr.Read())
                            {
                                if (dr["Número_da_Conta"].ToString() == conta)
                                {
                                    Nome = dr["Nome"].ToString();
                                    Saldo = dr["Saldo"].ToString();
                                }
                            }
                        }
                    }
                    catch (SqlException)
                    {
                        mensagem = "Erro ao Tentar Se Relacionar com o Banco de Dados.";
                    }
                    break;
            }
        }

        public void ReconhecerRemetente(string ag, string c)
        {
            Conexao con = new();
            SqlCommand cmd = new();
            SqlDataReader dr;

            cmd.CommandText = "select Nome, Número_da_Agência, Saldo from Conta_Bancaria where Número_da_Agência = @ag and Número_da_Conta = @c";
            cmd.Parameters.AddWithValue("@ag", ag);
            cmd.Parameters.AddWithValue("@c", c);

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    achou = true;
                    if(dr.Read())
                    {
                        switch (ag)
                        {
                            case "123456":
                                NomeR = dr["Nome"].ToString();
                                SaldoR = dr["Saldo"].ToString();
                                BancoR = "Banco do Brasil";
                                break;
                            case "234567":
                                NomeR = dr["Nome"].ToString();
                                SaldoR = dr["Saldo"].ToString();
                                BancoR = "Caixa Econômica";
                                break;
                            case "345678":
                                NomeR = dr["Nome"].ToString();
                                SaldoR = dr["Saldo"].ToString();
                                BancoR = "Itau";
                                break;
                            case "456789":
                                NomeR = dr["Nome"].ToString();
                                SaldoR = dr["Saldo"].ToString();
                                BancoR = "Nubank";
                                break;
                            case "567891":
                                NomeR = dr["Nome"].ToString();
                                SaldoR = dr["Saldo"].ToString();
                                BancoR = "Bradesco";
                                break;
                            default:
                                BancoR = "Banco Não Reconhecido";
                                break;
                        }
                    }
                }
            }
            catch (SqlException)
            {
                mensagem = "Erro Ao Tentae Se Conectar com o Banco de Dados.";
            }
        }

        public void ValorProdutoL_A(string np)
        {
            Conexao con = new();
            SqlCommand cmd = new();
            SqlDataReader dr;

            cmd.CommandText = "select Valor from Loja_A where Nome_Produto = '" + np + "'";

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if(dr.HasRows)
                {
                    if(dr.Read())
                    {
                        valorProdutoA = dr["Valor"].ToString();
                    }
                }
            }
            catch (SqlException)
            {
                mensagem = "Erro ao Tentar Se Conectar com o Banco de Dados.";
            }
        }

        public void ValorProdutoL_B(string np)
        {
            Conexao con = new();
            SqlCommand cmd = new();
            SqlDataReader dr;

            cmd.CommandText = "select Valor from Loja_B where Nome_Produto = '" + np + "'";

            try
            {
                cmd.Connection = con.Conectar();
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    if (dr.Read())
                    {
                        valorProdutoB = dr["Valor"].ToString();
                    }
                }
            }
            catch (SqlException)
            {
                mensagem = "Erro ao Tentar Se Conectar com o Banco de Dados.";
            }
        }

    }
}
