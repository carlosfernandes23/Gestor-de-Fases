using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

    class BDParafusaria
    {
        SqlConnection connectionparafusaria = new SqlConnection("Data Source=GALILEU\\PREPARACAO;Initial Catalog=Parafusaria;Persist Security Info=True;User ID=SA;Password=preparacao");

        public void ConectarBD()
        {
            if (connectionparafusaria.State == ConnectionState.Closed)
            {
                connectionparafusaria.Open();
            }
        }

        public void DesonectarBD()
        {
            if (connectionparafusaria.State == ConnectionState.Open)
            {
                connectionparafusaria.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return connectionparafusaria;
        }

        public DataTable Procurarbd(string Query)
        {
            SqlCommand MiComando = new SqlCommand(Query, connectionparafusaria);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(MiComando);
            DataTable dataTable = new DataTable();

            // Preenchendo o DataTable com os dados da consulta
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

        public List<string> Procurarbdlist(string Query)
        {
            SqlCommand MiComando = new SqlCommand(Query, connectionparafusaria);
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

        public DataTable BuscarRegistros(SqlCommand command)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            return dataTable;
        }

    }

    public class InsertBD
    {
        private readonly BDParafusaria Connect;
        public InsertBD()
        {
            Connect = new BDParafusaria();
        }
        public bool InsertConjunto(string norma, string parafuso, string porca, string anilha, string comprimento, string Tipo, out string mensagem)
        {
            string query = @"INSERT INTO dbo.Conjunto 
                         (Norma, Bolt, Nut, Washer, Comprimento,Tipo) 
                         VALUES (@Norma, @Bolt, @Nut, @Washer, @Comprimento, @Tipo)";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Norma", norma);
                    cmd.Parameters.AddWithValue("@Bolt", parafuso);
                    cmd.Parameters.AddWithValue("@Nut", porca);
                    cmd.Parameters.AddWithValue("@Washer", anilha);
                    cmd.Parameters.AddWithValue("@Comprimento", comprimento ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Novo Conjunto inserido com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao inserir o novo Conjunto: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool InsertBM(string norma, string diametro, string Tipo, out string mensagem)
        {
            string BM = Tipo + diametro;
            string query = @"INSERT INTO dbo.Tamanho_BM (Norma, BM) 
                         VALUES (@Norma, @BM)";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Norma", norma);
                    cmd.Parameters.AddWithValue("@BM", BM);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Novo Parafuso inserido com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao inserir o novo Parafuso: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool InsertC(string diametro, string Tipo, out string mensagem)
        {
            string C = Tipo + diametro;
            string query = @"INSERT INTO dbo.Tamanho_Conector (C) 
                         VALUES (@C)";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@C", C);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Novo Conector inserido com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao inserir o novo Conector : " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool InsertVRSM(string diametro, string Tipo, out string mensagem)
        {
            string VRSM = Tipo + diametro;
            string query = @"INSERT INTO dbo.Tamanho_VRSM (VRSM) 
                         VALUES (@VRSM)";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@VRSM", VRSM);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Novo Varão Roscado inserido com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao inserir o novo Varão Roscado: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool InsertNR(string diametro, string Tipo, out string mensagem)
        {
            string NR = Tipo + diametro;
            string query = @"INSERT INTO dbo.Tamanho_NR (NR) 
                         VALUES (@NR)";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@NR", NR);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Novo Varão Nervorado inserido com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao inserir o novo Varão Nervorado: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool InsertClasse(string classe,string tipo, out string mensagem)
        {
            string query = @"INSERT INTO dbo.Classe (Classe, Tipo) 
                         VALUES (@Classe, @Tipo)";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Classe", classe);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Nova Classe inserido com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao inserir nova Classe: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
    }

    public class UpdateBD
    {
        private readonly BDParafusaria Connect;
        public UpdateBD()
        {
            Connect = new BDParafusaria();
        }
        public bool UpdateConjunto(int id, string norma, string parafuso, string porca, string anilha, string comprimento, string Tipo, out string mensagem)
        {
            string query = @" UPDATE dbo.Conjunto SET Norma = @Norma, Bolt = @Bolt, Nut = @Nut, Washer = @Washer, Comprimento = @Comprimento, Tipo = @Tipo WHERE ID = @ID"; 
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Norma", norma);
                    cmd.Parameters.AddWithValue("@Bolt", parafuso);
                    cmd.Parameters.AddWithValue("@Nut", porca);
                    cmd.Parameters.AddWithValue("@Washer", anilha);
                    cmd.Parameters.AddWithValue("@Comprimento", comprimento ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        mensagem = "Conjunto atualizado com sucesso.";
                        return true;
                    }
                    else
                    {
                        mensagem = "Nenhum registro encontrado para atualizar.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao atualizar o Conjunto: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool UpdateBM(int id, string norma, string diametro, string Tipo, out string mensagem)
        {
            string BM = Tipo + diametro;
            string query = @"UPDATE dbo.Tamanho_BM SET Norma = @Norma, BM = @BM WHERE ID = @ID"; 
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Norma", norma);
                    cmd.Parameters.AddWithValue("@BM", BM);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Parafuso atualizado com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao atualizar o Parafuso: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool UpdateC(int id, string diametro, string Tipo, out string mensagem)
        {
            string C = Tipo + diametro;
            string query = @"UPDATE dbo.Tamanho_Conector SET C = @C WHERE ID = @ID";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@C", C);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Conector atualizado com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao atualizar o Conector: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool UpdateVRSM(int id, string diametro, string Tipo, out string mensagem)
        {
            string VRSM = Tipo + diametro;
            string query = @"UPDATE dbo.Tamanho_VRSM SET VRSM = @VRSM WHERE ID = @ID";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@VRSM", VRSM);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Varão Roscado atualizado com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao atualizar o Varão Roscado: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool UpdateNR(int id, string diametro, string Tipo, out string mensagem)
        {
            string NR = Tipo + diametro;
            string query = @"UPDATE dbo.Tamanho_NR SET NR = @VR WHERE ID = @ID";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@NR", NR);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Varão Nervorado atualizado com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao atualizar o Varão Nervorado: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool UpdateClasse(int id, string classe, string tipo, out string mensagem)
        {
            string query = @"UPDATE dbo.Classe SET Classe = @Classe, Tipo = @Tipo WHERE ID = @ID";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Classe", classe);
                    cmd.Parameters.AddWithValue("@Tipo", tipo);
                    cmd.ExecuteNonQuery();
                }
                mensagem = "Classe atualizada com sucesso.";
                return true;
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao atualizar Classe: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }

    }

    public class DeleteBD
    {
        private readonly BDParafusaria Connect;
        public DeleteBD()
        {
            Connect = new BDParafusaria();
        }
        public bool DeleteConjunto(int id, out string mensagem)
        {
            string query = @"DELETE FROM dbo.Conjunto WHERE ID = @ID";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);                   
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        mensagem = "Conjunto eliminado com sucesso.";
                        return true;
                    }
                    else
                    {
                        mensagem = "Nenhum registro encontrado para eliminado.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao atualizar Conjunto: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool Delete(int id, string Tipo, out string mensagem)
        {
            string query = $@"DELETE dbo.Tamanho_{Tipo} WHERE ID = @ID";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        mensagem = "Linha eliminado com sucesso.";
                        return true;
                    }
                    else
                    {
                        mensagem = "Nenhum registro encontrado para eliminado.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao eliminar Conjunto: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        public bool DeleteClasse(int id, out string mensagem)
        {
            string query = @"DELETE dbo.Classe WHERE ID = @ID";
            try
            {
                Connect.ConectarBD();
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    int rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        mensagem = "Classe eliminado com sucesso.";
                        return true;
                    }
                    else
                    {
                        mensagem = "Nenhuma Classe encontrada para eliminado.";
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                mensagem = "Erro ao eliminar Classe: " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }

    }

}
