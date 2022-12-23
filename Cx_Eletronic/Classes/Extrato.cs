using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Cx_Eletronic_2.Classes
{
    internal class Extrato
    {

        public void DadosExtrato()
        {
            StreamWriter sw = new StreamWriter("../../../Arquivos/Extrato.txt");

            Operacoes op = new();
            DataTable table = op.Extrato();
            DataRow[] currentRows = table.Select(null, null, DataViewRowState.CurrentRows);

            sw.Write("--------------------------------------------------\n" +
                           "\t\t\t\t\t\t" + Validacao.banco + "\n\n" +
                           "----------------------------------------\n\n");
            if (currentRows.Length < 1)
                MessageBox.Show("No Current Rows Found");
            else
            {
                foreach (DataRow row in currentRows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        sw.Write(column.ColumnName+"\n");
                        sw.Write(row[column]+"\n\n");
                    }
                }
                sw.Close();
            }

        }
    }
}
