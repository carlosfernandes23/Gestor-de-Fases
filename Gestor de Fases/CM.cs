using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;

namespace Gestor_de_Fases
{
    public partial class CM : Form
    {
        Form1 F;
        public CM(Form1 formInicio)
        {
            InitializeComponent();
            F = formInicio;         
        }
        private void CM_Load(object sender, EventArgs e)
        {                       
            CarregarNormasConjunto();
            CarregarNormasConector();
            cbConjunto.SelectedIndex = -1;
            cbAncoragem.SelectedIndex = -1;
            cbConector.SelectedIndex = -1;
            CloseAll();
            Procurarartigo();
        }
        private void CloseAll()
        {
            CloseConjBolt();
            VaraoeBuchasClose();
            CloseConjAcss();
            tbNorma.Visible = false;
        }
        private void cbConjunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAncoragem.SelectedIndex = -1;
            cbConector.SelectedIndex = -1;
            CloseAll();
            OpenConjBolt();
            CarregarConjuntoSelect();
            CarregarQuantConjunto();
        }       
        private void cbAncoragem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseAll();
            tbReqespAncor.Text = string.Empty;
            VaraoeBuchasOpen();
            cbDiametroAncor.DataSource = null;

            string textoSelecionado = cbAncoragem.Text;
            string botaoTexto = string.Empty;

            if (textoSelecionado.Contains("Bucha Metálica"))
            {
                botaoTexto = "Adicionar Bucha Metálica";
                CarregarNormasbuchas();
            }
            else if (textoSelecionado.Contains("Bucha Quimica") || textoSelecionado.Contains("Varão Roscado") || textoSelecionado.Contains("Varão Nervorado"))
            {
                if (textoSelecionado.Contains("Bucha Quimica")) 
                    botaoTexto = "Adicionar Bucha Quimica";
                else if (textoSelecionado.Contains("Varão Roscado")) 
                    botaoTexto = "Adicionar Varão Roscado";
                else botaoTexto = "Adicionar Varão Nervorado";

                cbNormaAncor.DataSource = null;
                cbNormaAncor.Items.Clear(); 

                if (textoSelecionado.Contains("Varão Nervorado"))
                {
                    cbNormaAncor.Items.Add("A400");
                    cbNormaAncor.Items.Add("A500");
                    cbNormaAncor.Items.Add("A1000");
                }
                else 
                {
                    cbNormaAncor.Items.Add("DIN 976");
                }

                cbNormaAncor.SelectedIndex = 0;
            }
            else
            {
                CloseAll();
            }
            ButtonAddAncor.Text = botaoTexto;
            CarregarAncoragemSelect();
        }
        private void cbConector_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbConjunto.SelectedIndex = -1;
            cbAncoragem.SelectedIndex = -1;
            CloseAll();
            VaraoeBuchasOpen();
            CarregarConectorSelect();
        }
        private void buttonAcess_Click(object sender, EventArgs e)
        {
            cbConjunto.SelectedIndex = -1;
            cbAncoragem.SelectedIndex = -1;
            cbConector.SelectedIndex = -1;
            CloseAll();
            OpenConjAcss();
            tbNorma.Visible = false;
        }
        private void OpenConjBolt() => SetConjBoltVisibility(true);
        private void CloseConjBolt() => SetConjBoltVisibility(false);
        private void OpenConjAcss() => SetConjAcssVisibility(true);
        private void CloseConjAcss() => SetConjAcssVisibility(false);
        private void ConfigurarPosicoesConjBolt()
        {
            labelDiamBolt.Location = new Point(25, 145);
            labelCompBolt.Location = new Point(125, 145);
            labelqdBolt.Location = new Point(260, 145);
            labelclasseBolt.Location = new Point(410, 145);
            labelnormalBolt.Location = new Point(640, 145);
            labelmarcaBolt.Location = new Point(805, 145);
            labelobsBolt.Location = new Point(910, 145);
            labelcertificado.Location = new Point(960, 145);

            cbDiametroBolt.Location = new Point(10, 175);
            cbComprimentoBolt.Location = new Point(115, 175);
            tbqtdBolt.Location = new Point(250, 175);
            cbClasseBolt.Location = new Point(305, 175);
            cbNormaBolt.Location = new Point(560, 175);
            tbMarcaBolt.Location = new Point(765, 175);
            obsBolt.Location = new Point(910, 175);
            cbCertificadoConj.Location = new Point(955, 175);

            cbDiametroNut.Location = new Point(10, 225);
            tbqtdNut.Location = new Point(250, 225);
            cbClasseNut.Location = new Point(305, 225);
            cbNormaNut.Location = new Point(560, 225);
            tbMarcaNut.Location = new Point(765, 225);
            obsNut.Location = new Point(910, 225);

            cbDiametroWasher.Location = new Point(10, 275);
            tbqtdWasher.Location = new Point(250, 275);
            cbClasseWasher.Location = new Point(305, 275);
            cbNormaWasher.Location = new Point(560, 275);
            tbMarcaWasher.Location = new Point(765, 275);
            obsWasher.Location = new Point(910, 275);

            ButtonAddBolt.Location = new Point(1045, 170);
            ButtonAddNut.Location = new Point(1045, 225);
            ButtonAddWasher.Location = new Point(1045, 275);
            ButtonAddConj.Location = new Point(1210, 170);
        }
        private void SetConjBoltVisibility(bool visible)
        {
            ConfigurarPosicoesConjBolt();

            labelDiamBolt.Visible = visible;
            labelCompBolt.Visible = visible;
            labelqdBolt.Visible = visible;
            labelclasseBolt.Visible = visible;
            labelnormalBolt.Visible = visible;
            labelmarcaBolt.Visible = visible;
            labelobsBolt.Visible = visible;
            labelcertificado.Visible = visible;
            cbDiametroBolt.Visible = visible;
            cbComprimentoBolt.Visible = visible;
            tbqtdBolt.Visible = visible;
            cbClasseBolt.Visible = visible;
            cbNormaBolt.Visible = visible;
            tbMarcaBolt.Visible = visible;
            obsBolt.Visible = visible;
            cbCertificadoConj.Visible = visible;
            cbDiametroNut.Visible = visible;
            tbqtdNut.Visible = visible;
            cbClasseNut.Visible = visible;
            cbNormaNut.Visible = visible;
            tbMarcaNut.Visible = visible;
            obsNut.Visible = visible;

            cbDiametroWasher.Visible = visible;
            tbqtdWasher.Visible = visible;
            cbClasseWasher.Visible = visible;
            cbNormaWasher.Visible = visible;
            tbMarcaWasher.Visible = visible;
            obsWasher.Visible = visible;

            ButtonAddBolt.Visible = visible;
            ButtonAddNut.Visible = visible;
            ButtonAddWasher.Visible = visible;
            ButtonAddConj.Visible = visible;
        }
        private void ConfigurarPosicoesVaraoeBucha()
        {
            labelDiametroAncor.Location = new Point(50, 190);
            labelCompAncor.Location = new Point(155, 190);
            labelQtdAncor.Location = new Point(265, 190);
            labelClasseAncor.Location = new Point(410, 190);
            labelNormaAncor.Location = new Point(635, 190);
            labelReqEspncor.Location = new Point(805, 190);
            labelCertificadoAncor.Location = new Point(922, 190);
            labelMarcaAncor.Location = new Point(1015, 190);
            labelObsAncor.Location = new Point(1085, 190);

            cbDiametroAncor.Location = new Point(20, 220);
            cbCompAncor.Location = new Point(160, 220);
            tbqtdAncor.Location = new Point(255, 220);
            cbClasseAncor.Location = new Point(310, 220);
            cbNormaAncor.Location = new Point(565, 220);
            tbReqespAncor.Location = new Point(770, 220);
            cbCertificadoAncor.Location = new Point(920, 220);
            tbMarcaAncor.Location = new Point(999, 220);
            obsAcor.Location = new Point(1085, 220);

            ButtonAddAncor.Location = new Point(1130, 220);
        }
        private void SetAncoragemVisibility(bool visible)
        {
            ConfigurarPosicoesVaraoeBucha();
            labelDiametroAncor.Visible = visible;
            labelCompAncor.Visible = visible;
            labelQtdAncor.Visible = visible;
            labelClasseAncor.Visible = visible;
            labelNormaAncor.Visible = visible;
            labelReqEspncor.Visible = visible;
            labelCertificadoAncor.Visible = visible;
            labelMarcaAncor.Visible = visible;
            labelObsAncor.Visible = visible;
            cbDiametroAncor.Visible = visible;
            cbCompAncor.Visible = visible;
            tbqtdAncor.Visible = visible;
            cbClasseAncor.Visible = visible;
            cbNormaAncor.Visible = visible;
            tbReqespAncor.Visible = visible;
            cbCertificadoAncor.Visible = visible;
            tbMarcaAncor.Visible = visible;
            obsAcor.Visible = visible;
            ButtonAddAncor.Visible = visible;
        }
        private void VaraoeBuchasOpen() => SetAncoragemVisibility(true);
        private void VaraoeBuchasClose() => SetAncoragemVisibility(false);
        private void SetConjAcssVisibility(bool visible)
        {
            ConfigauraPosicoesAcessorio();

            labelqtdAcss.Visible = visible;
            labelPerfilAcss.Visible = visible;
            labelNormalAcss.Visible = visible;
            labelCompAcss.Visible = visible;
            labelCertfAcss.Visible = visible;
            labelReqEAcss.Visible = visible;
            labelMarcaAcss.Visible = visible;

            tbQtdAcss.Visible = visible;
            tbPerfilAcss.Visible = visible;
            tbNormalAcss.Visible = visible;
            tbCompAcss.Visible = visible;
            cbCertificadoAcss.Visible = visible;
            tbReqEAcss.Visible = visible;
            tbMarcalAcss.Visible = visible;

            ButtonAddAcss.Visible = visible;
        }
        private void ConfigauraPosicoesAcessorio()
        {
            labelqtdAcss.Location = new Point(35, 190);
            labelPerfilAcss.Location = new Point(200, 190);
            labelNormalAcss.Location = new Point(420, 190);
            labelCompAcss.Location = new Point(570, 190);
            labelCertfAcss.Location = new Point(700, 190);
            labelReqEAcss.Location = new Point(825, 190);
            labelMarcaAcss.Location = new Point(1015, 190);

            tbQtdAcss.Location = new Point(13, 220);
            tbPerfilAcss.Location = new Point(100, 220);
            tbNormalAcss.Location = new Point(360, 220);
            tbNorma.Location = new Point(565, 220); 
            tbCompAcss.Location = new Point(545, 220);
            cbCertificadoAcss.Location = new Point(690, 220);
            tbReqEAcss.Location = new Point(785, 220);
            tbMarcalAcss.Location = new Point(970, 220);

            ButtonAddAcss.Location = new Point(1115, 220);
        }
        private void CarregarNormasConjunto()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = "SELECT DISTINCT Norma FROM dbo.Conjunto WHERE Tipo IN ('Conjunto', 'Autoperfurante')";
                DataTable dt = Connect.Procurarbd(query);                
                cbConjunto.DataSource = dt; 
                cbConjunto.DisplayMember = "Norma"; 
                cbConjunto.ValueMember = "Norma"; 
                cbConjunto.SelectedIndex = -1;
                
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar normas do Conjunto: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarNormasConector()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = "SELECT DISTINCT Norma FROM dbo.Conjunto WHERE Tipo IN ('Conector')";
                DataTable dt = Connect.Procurarbd(query);
                cbConector.DataSource = dt;
                cbConector.DisplayMember = "Norma";
                cbConector.ValueMember = "Norma";
                cbConjunto.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar normas do Conecto: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarConjuntoSelect()
        {
            string Tipo = cbConjunto.Text;
            CarregarDiametroConj(Tipo);
            if (ExisteValoresNaColuna(Tipo, "Bolt"))
            {
                CarregarClassePorTipo("Parafuso", cbClasseBolt);
                CarregarNorma(Tipo, "Bolt", cbNormaBolt);               
            }
            else
            {
                cbDiametroBolt.Visible = false;
                cbComprimentoBolt.Visible = false;
                tbqtdBolt.Visible = false;
                cbClasseBolt.Visible = false;
                cbNormaBolt.Visible = false;
                tbMarcaBolt.Visible = false;
                obsBolt.Visible = false;
                ButtonAddBolt.Visible = false;
            }

            if (ExisteValoresNaColuna(Tipo, "Nut"))
            {
                CarregarClassePorTipo("Porca", cbClasseNut);
                CarregarNorma(Tipo, "Nut", cbNormaNut);
            }
            else
            {
                cbDiametroNut.Visible = false;
                tbqtdNut.Visible = false;
                cbClasseNut.Visible = false;
                cbNormaNut.Visible = false;
                tbMarcaNut.Visible = false;
                obsNut.Visible = false;
                ButtonAddNut.Visible = false;
            }
            if (ExisteValoresNaColuna(Tipo, "Washer"))
            {
                CarregarClassePorTipo("Anilha", cbClasseWasher);
                CarregarNorma(Tipo, "Washer", cbNormaWasher);
            }
            else
            {
                cbDiametroWasher.Visible = false;
                tbqtdWasher.Visible = false;
                cbClasseWasher.Visible = false;
                cbNormaWasher.Visible = false;
                tbMarcaWasher.Visible = false;
                obsWasher.Visible = false;
                ButtonAddWasher.Visible = false;
            }
        }
        private void CarregarTodosConjuntos()
        {
            CarregarTodosDiametro();
            CarregarClassePorTipo("Parafuso", cbClasseBolt);
            CarregarClassePorTipo("Porca", cbClasseNut);
            CarregarClassePorTipo("Anilha", cbClasseWasher);
            CarregarTodasNorma("Bolt", cbNormaBolt);
            CarregarTodasNorma("Nut", cbNormaNut);
            CarregarTodasNorma("Washer", cbNormaWasher);
        }
        private void CarregarConectorSelect()
        {
            string Tipo = cbConector.Text;
            tbNorma.Text = Tipo;
            cbNormaAncor.Visible = false;
            tbNorma.Visible = true;
            CarregarDiametroConector(Tipo);
            CarregarClassePorTipo("Parafuso", cbClasseAncor); cbClasseAncor.SelectedIndex = 8;          
            string Qtd = tbqtdtotalconjconect.Text;
            tbqtdAncor.Text = Qtd;
            tbReqespAncor.Text = "EN-ISO 13918";
            ButtonAddAncor.Text = "Adicionar Conector";
        }
        private void CarregarTodosConector()
        {
            string Tipo = cbConector.Text;
            tbNorma.Text = Tipo;
            cbNormaAncor.Visible = false;
            tbNorma.Visible = true;
            CarregarDiametroConector(Tipo);
            CarregarTodasNormaConector("Bolt", cbNormaAncor); cbNormaAncor.SelectedIndex = 8;
            string Qtd = tbqtdtotalconjconect.Text;
            tbqtdAncor.Text = Qtd;
            ButtonAddAncor.Text = "Adicionar Conector";
        }
        private void CarregarAncoragemSelect()
        {
            string Tipo = cbAncoragem.Text;
            cbNormaAncor.Visible = true;
            tbNorma.Visible = false;
            cbDiametroAncor.DataSource = null;
            CarregarClassePorTipo("Parafuso", cbClasseAncor);
            if (cbClasseAncor.Items.Count > 8) 
                cbClasseAncor.SelectedIndex = 8;

            switch (cbAncoragem.SelectedIndex)
            {
                case 0:
                    CarregarDiametroAncoragemMta();
                    break;
                case 1:
                case 2: 
                    CarregarDiametroAncoragem("VRSM");
                    break;
                case 3:
                    CarregarDiametroAncoragem("NR");
                    break;
                default:
                    break;
            }
            string Qtd = tbqtdtotalanco.Text;
            tbqtdAncor.Text = Qtd;
        }
        private void CarregarDiametroConj(string Norma)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = "SELECT DISTINCT BM FROM dbo.Tamanho_BM WHERE Norma = @Norma";
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Norma", Norma);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cbDiametroBolt.DataSource = dt;
                    cbDiametroBolt.DisplayMember = "BM";
                    cbDiametroBolt.ValueMember = "BM";
                    cbDiametroBolt.SelectedIndex = -1;

                    DataTable dtNut = dt.Copy();
                    foreach (DataRow row in dtNut.Rows)
                    {
                        if (row["BM"] != DBNull.Value)
                        {
                            string valor = row["BM"].ToString();
                            row["BM"] = valor.Replace("BM", "NM");
                        }
                    }
                    cbDiametroNut.DataSource = dtNut;
                    cbDiametroNut.DisplayMember = "BM";
                    cbDiametroNut.ValueMember = "BM";
                    cbDiametroNut.SelectedIndex = -1;

                    DataTable dtWasher = dt.Copy();
                    foreach (DataRow row in dtWasher.Rows)
                    {
                        if (row["BM"] != DBNull.Value)
                        {
                            string valor = row["BM"].ToString();
                            row["BM"] = valor.Replace("BM", "WM");
                        }
                    }
                    cbDiametroWasher.DataSource = dtWasher;
                    cbDiametroWasher.DisplayMember = "BM";
                    cbDiametroWasher.ValueMember = "BM";
                    cbDiametroWasher.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar diâmetros dos Conjuntos: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarDiametroConector(string Norma)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = "SELECT DISTINCT C FROM dbo.Tamanho_Conector";
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cbDiametroAncor.DataSource = dt;
                    cbDiametroAncor.DisplayMember = "C";
                    cbDiametroAncor.ValueMember = "C";
                    cbDiametroAncor.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar diâmetro do Conector: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarDiametroAncoragemMta()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = $"SELECT DISTINCT BM FROM dbo.Tamanho_BM WHERE Norma = 'MTA'";
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["BM"] != DBNull.Value)
                        {
                            row["BM"] = row["BM"].ToString().Replace("BM", "BUM");
                        }
                    }
                    cbDiametroAncor.DataSource = dt;
                    cbDiametroAncor.DisplayMember = "BM";
                    cbDiametroAncor.ValueMember = "BM";

                    if (dt.Rows.Count > 0)
                        cbDiametroAncor.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar diâmetro do Conector: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarDiametroAncoragem(string Coluna)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = $"SELECT DISTINCT {Coluna} FROM dbo.Tamanho_{Coluna}";
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    cbDiametroAncor.DataSource = dt;
                    cbDiametroAncor.DisplayMember = Coluna;
                    cbDiametroAncor.ValueMember = Coluna;
                    cbDiametroAncor.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar diâmetro do Conector: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarClassePorTipo(string tipo, ComboBox combo)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();

                string query = "SELECT DISTINCT Classe FROM dbo.Classe WHERE Tipo = @Tipo";

                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Tipo", tipo);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    combo.DataSource = dt;
                    combo.DisplayMember = "Classe";
                    combo.ValueMember = "Classe";
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar classes: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarNorma(string norma, string Tabela, ComboBox combo)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();

                string query = $"SELECT DISTINCT {Tabela} FROM dbo.Conjunto WHERE Norma = @Norma";

                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Norma", norma);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    combo.DataSource = dt;
                    combo.DisplayMember = Tabela;
                    combo.ValueMember = Tabela;
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar Norma: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }


        }
        private void CarregarTodosDiametro()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();

                string query = "SELECT DISTINCT BM FROM dbo.Tamanho_BM";

                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    cbDiametroBolt.DataSource = dt;
                    cbDiametroBolt.DisplayMember = "BM";
                    cbDiametroBolt.ValueMember = "BM";
                    cbDiametroBolt.SelectedIndex = -1;

                    DataTable dtNut = dt.Copy();
                    foreach (DataRow row in dtNut.Rows)
                    {
                        if (row["BM"] != DBNull.Value)
                        {
                            string valor = row["BM"].ToString();
                            row["BM"] = valor.Replace("BM", "NM");
                        }
                    }
                    cbDiametroNut.DataSource = dtNut;
                    cbDiametroNut.DisplayMember = "BM";
                    cbDiametroNut.ValueMember = "BM";
                    cbDiametroNut.SelectedIndex = -1;

                    DataTable dtWasher = dt.Copy();
                    foreach (DataRow row in dtWasher.Rows)
                    {
                        if (row["BM"] != DBNull.Value)
                        {
                            string valor = row["BM"].ToString();
                            row["BM"] = valor.Replace("BM", "WM");
                        }
                    }
                    cbDiametroWasher.DataSource = dtWasher;
                    cbDiametroWasher.DisplayMember = "BM";
                    cbDiametroWasher.ValueMember = "BM";
                    cbDiametroWasher.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar diâmetros: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarTodasNorma(string Tabela, ComboBox combo)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = $"SELECT DISTINCT {Tabela} FROM dbo.Conjunto";
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    combo.DataSource = dt;
                    combo.DisplayMember = Tabela;
                    combo.ValueMember = Tabela;
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar Norma: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarTodasNormaConector(string Tabela, ComboBox combo)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = $"SELECT DISTINCT {Tabela} FROM dbo.Conjunto WHERE Tipo = 'Conector'";
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    combo.DataSource = dt;
                    combo.DisplayMember = Tabela;
                    combo.ValueMember = Tabela;
                    combo.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar Norma: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }


        }
        private bool ExisteValoresNaColuna(string norma, string coluna)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();

                string query = $"SELECT {coluna} FROM dbo.Conjunto WHERE Norma = @Norma";

                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Norma", norma);

                    object result = cmd.ExecuteScalar();

                    if (result == null || result == DBNull.Value)
                        return false;

                    string valor = result.ToString().Trim();
                    return !string.IsNullOrEmpty(valor);
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao verificar coluna " + coluna + ": " + ex.Message;
                return false;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void CarregarQuantConjunto()
        {            
            // ================== Parafuso ==================
            int Qtd;
            if (!int.TryParse(tbqtdtotalconj.Text, out Qtd))
            {
                F.labelestado.Text = "Quantidade inválida!";
                return;
            }
            tbqtdtotalconj.Text = Qtd.ToString();

            if (cbDiametroBolt.Visible == true)
            {
                tbqtdBolt.Text = Qtd.ToString();
                cbDiametroBolt.SelectedIndex = 1;
                cbNormaBolt.SelectedIndex = 0;
                cbClasseBolt.SelectedIndex = 8;
            }

            // ================== Porca ==================
            int QtdNut;
            if (!int.TryParse(tbqtdtotalconj.Text, out QtdNut))
            {
                F.labelestado.Text = "Quantidade inválida!";
                return;
            }
            tbqtdtotalconj.Text = QtdNut.ToString();

            if (cbConjunto.Text == "EN15048-(ISO4014)" ||
                cbConjunto.Text == "EN15048-(ISO4017)" ||
                cbConjunto.Text == "EN-14399-3-HR" ||
                cbConjunto.Text == "EN-14399-4-HV")
            {
                tbqtdNut.Text = "";
                cbDiametroNut.SelectedIndex = -1;
                cbClasseNut.SelectedIndex = -1;
                cbNormaNut.SelectedIndex = -1;
            }
            else
            {
                if (cbDiametroNut.Visible == true)
                {
                    tbqtdNut.Text = QtdNut.ToString();
                    cbDiametroNut.SelectedIndex = 1;
                    cbClasseNut.SelectedIndex = 8;
                    cbNormaNut.SelectedIndex = 0;
                }
            }

            // ================== Anilha ==================
            int QtdWasher;
            if (!int.TryParse(tbqtdtotalconj.Text, out QtdWasher))
            {
                F.labelestado.Text = "Quantidade inválida!";
                return;
            }
            tbqtdtotalconj.Text = QtdWasher.ToString();

            if (cbConjunto.Text == "EN-14399-3-HR" || cbConjunto.Text == "EN-14399-4-HV")
            {
                tbqtdWasher.Text = "";
                cbDiametroWasher.SelectedIndex = -1;
                cbClasseWasher.SelectedIndex = -1;
                cbNormaWasher.SelectedIndex = -1;
            }
            else
            {
                if (cbDiametroWasher.Visible == true)
                {
                    tbqtdWasher.Text = (QtdWasher * 2).ToString();
                    cbDiametroWasher.SelectedIndex = 1;
                    cbClasseWasher.SelectedIndex = 5;
                    cbNormaWasher.SelectedIndex = 0;
                }
            }
        }
        private void CarregarNormasbuchas()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = $"SELECT DISTINCT Norma FROM dbo.Conjunto WHERE Tipo = 'Bucha Mecânica'";
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);                    
                    cbNormaAncor.DataSource = dt;
                    cbNormaAncor.DisplayMember = "Norma";
                    cbNormaAncor.ValueMember = "Norma";
                    cbNormaAncor.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                F.labelestado.Text = "Erro ao carregar Norma: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void Bolt()
        {
            if (cbDiametroBolt.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione um Diametro do Parafuso";
                return;
            }
            else if (cbComprimentoBolt.Text.Trim() == "")
            {
                F.labelestado.Text = "Escreva o Comprimento do Parafuso";
                return;
            }
            else if (tbqtdBolt.Text.Trim() == "")
            {
                F.labelestado.Text = "Escreva uma quantidade de Parafusos";
                return;
            }
            else if (cbClasseBolt.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Classe do Parafuso";
                return;
            }
            else if (cbNormaBolt.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Norma do Parafuso";
                return;
            }
            else
            {
                linhaCONJ += 1;
                string Quantidade = tbqtdBolt.Text.Trim();
                string Diametro = cbDiametroBolt.Text.Trim();
                string Comprimento = cbComprimentoBolt.Text.Trim();
                string Classe = cbClasseBolt.Text.Trim();
                string ReqEspNorma = cbNormaBolt.Text.Trim();
                string Certificado = cbCertificadoConj.Text.Trim();
                string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
                string Perfil = Diametro + "X" + Comprimento;
                string ArtigoInterno = Diametro + "X" + Comprimento.PadLeft(3, '0');
                string Marca = tbMarcaBolt.Text.Trim();
                string fase1000 = F.labelfase1000.Text.Trim();
                string noObra = F.labelNObra.Text.Trim();
                string ordemFabrico = "2." + noObra + "." + fase1000 + "." + F.DataGridViewOrder.Rows.Count;
                string conjuntoPeca = "2." + noObra + "." + fase1000 + "." + fase1000 + "CJ" + linhaCONJ;
                string Observacoes = string.Empty;
                if (obsBolt.Checked)
                { Observacoes = tbObs.Text.Trim(); }
                else { Observacoes = ""; }
                F.DataGridViewOrder.Rows.Add(fase1000, fase1000, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, Classe, ReqEspNorma, Certificado, "", "", "", "", "", dataFormatada, ArtigoInterno, "08", Observacoes, "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Marca, "", "");
            }
        }
        private void Nut()
        {
            if (cbDiametroNut.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione um Diametro da Porca";
                return;
            }
            else if (tbqtdNut.Text.Trim() == "")
            {
                F.labelestado.Text = "Escreva uma quantidade de Porcas";
                return;
            }
            else if (cbClasseNut.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Classe da Porca";
                return;
            }
            else if (cbNormaNut.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Norma da Porca";
                return;
            }
            else
            {
                linhaCONJ += 1;
                string Quantidade = tbqtdNut.Text.Trim();
                string Diametro = cbDiametroNut.Text.Trim();
                string Classe = cbClasseNut.Text.Trim();
                string ReqEspNorma = cbNormaNut.Text.Trim();
                string Certificado = cbCertificadoConj.Text.Trim();
                string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
                string Perfil = Diametro;
                string ArtigoInterno = Diametro;
                string Marca = tbMarcaNut.Text.Trim();
                string fase1000 = F.labelfase1000.Text.Trim();
                string noObra = F.labelNObra.Text.Trim();
                string ordemFabrico = "2." + noObra + "." + fase1000 + "." + F.DataGridViewOrder.Rows.Count;
                string conjuntoPeca = "2." + noObra + "." + fase1000 + "." + fase1000 + "CJ" + linhaCONJ;
                string Observacoes = string.Empty;
                if (obsNut.Checked)
                { Observacoes = tbObs.Text.Trim(); }
                else { Observacoes = ""; }
                F.DataGridViewOrder.Rows.Add(fase1000, fase1000, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, Classe, ReqEspNorma, Certificado, "", "", "", "", "", dataFormatada, ArtigoInterno, "08", Observacoes, "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Marca, "", "");
            }
        }
        private void Washer()
        {
            if (cbDiametroWasher.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione um Diametro da Anilha";
                return;
            }
            else if (tbqtdWasher.Text.Trim() == "")
            {
                F.labelestado.Text = "Escreva uma quantidade de Anilhas";
                return;
            }
            else if (cbClasseWasher.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Classe da Anilha";
                return;
            }
            else if (cbNormaWasher.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Norma da Anilha";
                return;
            }
            else
            {
                linhaCONJ += 1;
                string Quantidade = tbqtdWasher.Text.Trim();
                string Diametro = cbDiametroWasher.Text.Trim();
                string Classe = cbClasseWasher.Text.Trim();
                string ReqEspNorma = cbNormaWasher.Text.Trim();
                string Certificado = cbCertificadoConj.Text.Trim();
                string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
                string Perfil = Diametro;
                string ArtigoInterno = Diametro;
                string Marca = tbMarcaWasher.Text.Trim();
                string fase1000 = F.labelfase1000.Text.Trim();
                string noObra = F.labelNObra.Text.Trim();
                string ordemFabrico = "2." + noObra + "." + fase1000 + "." + F.DataGridViewOrder.Rows.Count;
                string conjuntoPeca = "2." + noObra + "." + fase1000 + "." + fase1000 + "CJ" + linhaCONJ;
                string Observacoes = string.Empty;
                if (obsWasher.Checked)
                { Observacoes = tbObs.Text.Trim(); }
                else { Observacoes = ""; }
                F.DataGridViewOrder.Rows.Add(fase1000, fase1000, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, Classe, ReqEspNorma, Certificado, "", "", "", "", "", dataFormatada, ArtigoInterno, "08", Observacoes, "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Marca, "", "");
            }
        }
        private void Ancoragem()
        {
            if (cbDiametroAncor.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione um Diametro ";
                return;
            }
            else if (tbqtdAncor.Text.Trim() == "")
            {
                F.labelestado.Text = "Escreva uma quantidade";
                return;
            }
            else if (cbClasseAncor.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Classe ";
                return;
            }
            else if (cbNormaAncor.Text.Trim() == "")
            {
                F.labelestado.Text = "Selecione uma Norma ";
                return;
            }
            else
            {
                linhaCONJ += 1;
                string Quantidade = tbqtdAncor.Text.Trim();
                string Diametro = cbDiametroAncor.Text.Trim();
                string Classe = cbClasseAncor.Text.Trim();
                string ReqEspNorma = tbReqespAncor.Text.Trim();
                string Certificado = cbCertificadoAncor.Text.Trim();
                string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
                string Perfil = Diametro;
                string ArtigoInterno = Diametro;
                string Marca = tbMarcaAncor.Text.Trim();
                string fase1000 = F.labelfase1000.Text.Trim();
                string noObra = F.labelNObra.Text.Trim();
                string ordemFabrico = "2." + noObra + "." + fase1000 + "." + F.DataGridViewOrder.Rows.Count;
                string conjuntoPeca = "2." + noObra + "." + fase1000 + "." + fase1000 + "CJ" + linhaCONJ;
                string Observacoes = string.Empty;
                if (obsAcor.Checked)
                { Observacoes = tbObs.Text.Trim(); }
                else { Observacoes = ""; }
                F.DataGridViewOrder.Rows.Add(fase1000, fase1000, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, Classe, ReqEspNorma, Certificado, "", "", "", "", "", dataFormatada, ArtigoInterno, "08", Observacoes, "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Marca, "", "");

            }
        }
        private void Acessorios()
        {
            if (tbQtdAcss.Text.Trim() == "")
            {
                F.labelestado.Text = "Escreva uma quantidade";
                return;
            }
            else if (tbPerfilAcss.Text.Trim() == "")
            {
                F.labelestado.Text = "Defina um Perfil";
                return;
            }
            else if (tbNormalAcss.Text.Trim() == "")
            {
                F.labelestado.Text = "Defina uma Norma";
                return;
            }                    
            else
            {
                linhaCONJ += 1;
                string Quantidade = tbQtdAcss.Text.Trim();
                string Certificado = cbCertificadoAcss.Text.Trim();
                string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
                string Perfil = tbPerfilAcss.Text.Trim();
                string ReqEspecial = tbReqEAcss.Text.Trim();
                string Marca = tbMarcalAcss.Text.Trim();
                string Norma = tbNormalAcss.Text.Trim();
                string Comprimento = tbCompAcss.Text.Trim();
                string fase1000 = F.labelfase1000.Text.Trim();
                string noObra = F.labelNObra.Text.Trim();
                string ordemFabrico = "2." + noObra + "." + fase1000 + "." + F.DataGridViewOrder.Rows.Count;
                string conjuntoPeca = "2." + noObra + "." + fase1000 + "." + fase1000 + "CJ" + linhaCONJ;
                string Observacoes = string.Empty;
                string Artigo = ARTIGO;
                if (obsAcor.Checked)
                { Observacoes = tbObs.Text.Trim(); }
                else { Observacoes = ""; }

                F.DataGridViewOrder.Rows.Add(fase1000, fase1000, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, Perfil, ReqEspecial, Certificado, Comprimento, "", "", "", "", dataFormatada, Artigo, "08", "", "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", Marca, Norma, "");
            }
        }
        string ARTIGO = null;
        private void Procurarartigo()
        {
            List<string> lista1 = new List<string>();
            List<string> lista2 = new List<string>();
            BD Connet = new BD();
            Connet.ligarbd();
            lista1 = Connet.Procurarbd("SELECT [Artigo] FROM [PRIOFELIZ].[dbo].[MT_ViewArtigoUnidades]WHERE ARTIGO LIKE 'MPC%' AND [Descricao]='" + "Acessorios" + "'");
            Connet.desligarbd();
            ARTIGO = lista1.First();
            Connet.ligarbdprepararacao();
            lista2 = Connet.Procurarbdprepararacao("SELECT PERFIL FROM [ArtigoTekla].[dbo].[Perfilagem3] WHERE ARTIGO ='" + ARTIGO + "' AND DEST='08'");
            Connet.desligarbdprepararacao();

            Connet.ligarbdprepararacao();
            lista2 = Connet.Procurarbdprepararacao("SELECT marca FROM [ArtigoTekla].[dbo].[Perfilagem3] WHERE ARTIGO ='" + lista1.First() + "' AND DEST='08'");
            Connet.desligarbdprepararacao();                       
        }
        private void cbAncoragem_Click(object sender, EventArgs e)
        {
            cbConector.SelectedIndex = -1;
            cbConjunto.SelectedIndex = -1;
        }        
        private void buttonBoltSolto_Click(object sender, EventArgs e)
        {
            cbAncoragem.SelectedIndex = -1;
            cbConector.SelectedIndex = -1;
            CloseAll();
            OpenConjBolt();
            CarregarTodosConjuntos();
            CarregarQuantConjunto();

        }       
        private void ButtonAtualisarPara_Click(object sender, EventArgs e)
        {
            UpdateBolts OF = new UpdateBolts();
            OF.ShowDialog();
            this.Visible = true;
        }
        private void buttonConectorSolto_Click(object sender, EventArgs e)
        {
            CloseAll();
            VaraoeBuchasOpen();
            CarregarTodosConector();
        }
        int linhaCONJ = 0;
        private void ButtonAddBolt_Click(object sender, EventArgs e)
        {
            Bolt();
        }
        private void ButtonAddNut_Click(object sender, EventArgs e)
        {
            Nut();
        }
        private void ButtonAddWasher_Click(object sender, EventArgs e)
        {
            Washer();
        }
        private void ButtonAddAncor_Click(object sender, EventArgs e)
        {
            Ancoragem();
        }
        private void ButtonAddAcss_Click(object sender, EventArgs e)
        {
            Acessorios();
        }
        private void ButtonAddConj_Click(object sender, EventArgs e)
        {
            Bolt();
            Nut();
            Washer();
        }
    }
}

