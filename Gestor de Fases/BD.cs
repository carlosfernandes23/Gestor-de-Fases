using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gestor_de_Fases
{
    class BD
    {
        SqlConnection connection = new SqlConnection("Data Source=TESLA\\PRIMAVERA;Initial Catalog=PRIOFELIZ;Persist Security Info=True;User ID=CM;Password=OF€l1z201");
        SqlConnection connectionprepararacao = new SqlConnection("Data Source=GALILEU\\PREPARACAO;Initial Catalog=ArtigoTekla;Persist Security Info=True;User ID=SA;Password=preparacao");


        public void ligarbd()
        {

            connection.Open();

        }
        public List<string> Procurarbd(string Query)
        {

            SqlCommand MiComando = new SqlCommand(Query, connection);
            List<string> Result = new List<string>();

            using (SqlDataReader reader = MiComando.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.VisibleFieldCount; i++)
                    {
                        Result.Add(reader[i].ToString());
                    }
                }
            }
            return Result;
        }

        public void desligarbd()
        {
            connection.Close();
        }

        public void ligarbdprepararacao()
        {

            connectionprepararacao.Open();

        }
        public List<string> Procurarbdprepararacao(string Query)
        {
            SqlCommand MiComando = new SqlCommand(Query, connectionprepararacao);
            List<string> Result = new List<string>();

            using (SqlDataReader reader = MiComando.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int i = 0; i < reader.VisibleFieldCount; i++)
                    {
                        Result.Add(reader[i].ToString());
                    }
                }
            }
            return Result;
        }

        public void desligarbdprepararacao()
        {
            connectionprepararacao.Close();
        }
    }
}
