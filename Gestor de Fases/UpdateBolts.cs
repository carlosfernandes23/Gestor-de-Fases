using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_de_Fases
{
    public partial class UpdateBolts: Form
    {
        public UpdateBolts()
        {
            InitializeComponent();
            this.Size = new Size(590, 620);
            this.MinimumSize = new Size(590, 620);
            this.MaximumSize = new Size(590, 620);
            cbTipo.SelectedIndexChanged += (s, e) => ChooseTamanho();
            tbDiametro.KeyPress += KeyPress;
            tbDiametro.TextChanged += TextChanged;
        }
        private void UpdateBolts_Load(object sender, EventArgs e)
        {
            CarregarTipos();
            ChoosePanel();
            ConjuntoView();
            ChooseTamanho();
            ClasseView();
            CarregarNormas();
        }
        private void CarregarTipos()
        {
            var tipos = new List<object>
            {
                new { Codigo = "BM", Nome = "Parafuso" },
                new { Codigo = "C", Nome = "Conector" },
                new { Codigo = "VRSM", Nome = "Varão Roscado" },
                new { Codigo = "NR", Nome = "Varão Nervurado" }
            };

            cbTipo.DataSource = tipos;
            cbTipo.DisplayMember = "Nome";
            cbTipo.ValueMember = "Codigo";
            cbTipo.SelectedIndex = 0;
        }
        private void ChoosePanel()
        {
            if (cbchoosepanel.SelectedIndex == 0)
            {
                PanelConjunto.Visible = true;
                labelConjunto.Visible = true;
                PanelTamanho.Visible = false;
                labelTamanho.Visible = false;
                PanelClasse.Visible = false;
                labelClasse.Visible = false;

                PanelConjunto.Location = new Point(15, 75);
                labelConjunto.Location = new Point(230, 55);
                PanelTamanho.Location = new Point(590, 75);
                labelTamanho.Location = new Point(810, 55); 
                PanelClasse.Location = new Point(1160, 75); 
                labelClasse.Location = new Point(1400, 55);                
                labelEstado.Location = new Point(25, 590); 
            }
            else if (cbchoosepanel.SelectedIndex == 1)
            {
                PanelConjunto.Visible = false;
                labelConjunto.Visible = false;
                PanelTamanho.Visible = true;
                labelTamanho.Visible = true;
                PanelClasse.Visible = false;
                labelClasse.Visible = false;

                PanelTamanho.Location = new Point(15, 75);
                labelTamanho.Location = new Point(230, 55);              
                PanelConjunto.Location = new Point(590, 75);
                labelConjunto.Location = new Point(810, 55);
                PanelClasse.Location = new Point(1160, 75);
                labelClasse.Location = new Point(1400, 55);                
                labelEstado.Location = new Point(25, 530);
            }
            else if (cbchoosepanel.SelectedIndex == 2)
            {
                PanelConjunto.Visible = false;
                labelConjunto.Visible = false;
                PanelTamanho.Visible = false;
                labelTamanho.Visible = false;
                PanelClasse.Visible = true;
                labelClasse.Visible = true;
                PanelClasse.Location = new Point(15, 75);
                labelClasse.Location = new Point(230, 55);
                PanelConjunto.Location = new Point(1160, 75);
                labelConjunto.Location = new Point(1400, 55);
                PanelTamanho.Location = new Point(590, 75);
                labelTamanho.Location = new Point(810, 55);                
                labelEstado.Location = new Point(25, 530);
            }

        }
        private void ChooseTamanho()
        {
            if (cbTipo.SelectedIndex == 0)
            {
                ViewTamanho("BM");
            }
            if (cbTipo.SelectedIndex == 1)
            {
                ViewTamanho("C");
            }
            else if (cbTipo.SelectedIndex == 2)
            {
                ViewTamanho("VRSM");
            }
            else if (cbTipo.SelectedIndex == 3)
            {
                ViewTamanho("NR");
            }
        }
        private void ConjuntoView()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = "SELECT id, Norma, Bolt, NUT, Washer, Comprimento, Tipo FROM dbo.Conjunto";
                DataTable dataTable = Connect.Procurarbd(query);
                dataGridViewConjuntos.DataSource = dataTable;
                dataGridViewConjuntos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewConjuntos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewConjuntos.ReadOnly = true;
                dataGridViewConjuntos.Columns["id"].Visible = false;
                dataGridViewConjuntos.Columns["Tipo"].Visible = false;
                dataGridViewConjuntos.Columns["Norma"].HeaderText = "Norma do Conjunto";
                dataGridViewConjuntos.Columns["Bolt"].HeaderText = "Parafuso";
                dataGridViewConjuntos.Columns["Nut"].HeaderText = "Porca";
                dataGridViewConjuntos.Columns["Washer"].HeaderText = "Anilha";
                dataGridViewConjuntos.ClearSelection();
                dataGridViewConjuntos.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                labelEstado.Text = "Erro ao conectar à base de dados dos Conjuntos: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void ConjuntoViewSelect(string Tipo)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = "SELECT id, Norma, Bolt, NUT, Washer, Comprimento, Tipo " +
                               "FROM dbo.Conjunto WHERE Tipo = @Tipo";

                DataTable dataTable;
                using (SqlCommand cmd = new SqlCommand(query, Connect.GetConnection()))
                {
                    cmd.Parameters.AddWithValue("@Tipo", Tipo);  
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    dataTable = new DataTable();
                    adapter.Fill(dataTable);
                }
                dataGridViewConjuntos.DataSource = dataTable;
                dataGridViewConjuntos.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewConjuntos.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewConjuntos.ReadOnly = true;
                dataGridViewConjuntos.Columns["id"].Visible = false;
                dataGridViewConjuntos.Columns["Tipo"].Visible = false;
                dataGridViewConjuntos.Columns["Norma"].HeaderText = "Norma do Conjunto";
                dataGridViewConjuntos.Columns["Bolt"].HeaderText = "Parafuso";
                dataGridViewConjuntos.Columns["Nut"].HeaderText = "Porca";
                dataGridViewConjuntos.Columns["Washer"].HeaderText = "Anilha";
                dataGridViewConjuntos.ClearSelection();
                dataGridViewConjuntos.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                labelEstado.Text = "Erro ao conectar à base de dados dos Conjuntos: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void ViewTamanho(string tipo)
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();

                string query = "";
                if (tipo == "BM")
                    query = "SELECT id, Norma, BM FROM dbo.Tamanho_BM";
                else if (tipo == "C")
                    query = "SELECT id, C FROM dbo.Tamanho_Conector";
                else if (tipo == "VRSM")
                    query = "SELECT id, VRSM FROM dbo.Tamanho_VRSM";
                else if (tipo == "NR")
                    query = "SELECT id, NR FROM dbo.Tamanho_NR";
                else
                    throw new ArgumentException("Tipo inválido");

                DataTable dataTable = Connect.Procurarbd(query);
                dataGridViewTamanho.DataSource = dataTable;
                dataGridViewTamanho.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewTamanho.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewTamanho.ReadOnly = true;
                dataGridViewTamanho.Columns["id"].Visible = false;

                if (tipo == "BM")
                {
                    dataGridViewTamanho.Columns["Norma"].HeaderText = "Norma do Parafuso";
                    dataGridViewTamanho.Columns["BM"].HeaderText = "Parafuso";
                    dataGridViewTamanho.Columns["Norma"].Width = 220;
                    dataGridViewTamanho.Columns["BM"].Width = 120;
                }
                else if (tipo == "C")
                {
                    dataGridViewTamanho.Columns["C"].HeaderText = "Conector";
                    dataGridViewTamanho.Columns["VRSM"].Width = 120;
                }
                else if (tipo == "VRSM")
                {
                    dataGridViewTamanho.Columns["VRSM"].HeaderText = "Varão Roscado";
                    dataGridViewTamanho.Columns["VRSM"].Width = 120;
                }
                else if (tipo == "NR")
                {
                    dataGridViewTamanho.Columns["NR"].HeaderText = "Varão Nervurado";
                    dataGridViewTamanho.Columns["NR"].Width = 120;
                }
                dataGridViewTamanho.ClearSelection();
                dataGridViewTamanho.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                labelEstado.Text = "Erro ao conectar à base de dados do tipo " + tipo + ": " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void ClasseView()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = @" SELECT id, Classe, Tipo FROM dbo.Classe";
                DataTable dataTable = Connect.Procurarbd(query);
                dataGridViewClasse.DataSource = dataTable;
                dataGridViewClasse.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewClasse.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                dataGridViewClasse.ReadOnly = true;
                dataGridViewClasse.Columns["id"].Visible = false;
                dataGridViewClasse.ClearSelection();
                dataGridViewClasse.AutoResizeColumns();
            }
            catch (Exception ex)
            {
                labelEstado.Text = "Erro ao conectar à base de dados dos Conjuntos: " + ex.Message;
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }      
        private void InserirConjunto()
        {
            InsertBD Connect = new InsertBD();
            string mensagem;
            string Norma = tbNormal.Text;
            string Parafuso = tbParafuso.Text;
            string Porca = tbPorca.Text;
            string Anilha = tbAnilha.Text;
            string Comprimento = tbComp.Text.Trim().PadLeft(3, '0');
            string Tipo = cbTipoConjunto.Text;
            bool sucesso = Connect.InsertConjunto(Norma, Parafuso, Porca, Anilha, Comprimento, Tipo, out mensagem );
            labelEstado.Text = mensagem;
            if (sucesso)
            {
              ConjuntoView();
            }
        }
        private void InserirItem()
        {
            string norma = cbNorma.Text;
            string tipo = cbTipo.Text;
            string diametro = tbDiametro.Text.Trim().PadLeft(2, '0');
            string mensagem;
            InsertBD connect = new InsertBD();
            bool sucesso = false;

            switch (tipo)
            {
                case "BM":
                    sucesso = connect.InsertBM(norma, diametro, tipo, out mensagem);
                    if (sucesso) ViewTamanho("BM");
                    break;

                case "C":
                    sucesso = connect.InsertC(diametro, tipo, out mensagem);
                    if (sucesso) ViewTamanho("C");
                    break;

                case "VRSM":
                    sucesso = connect.InsertVRSM(diametro, tipo, out mensagem);
                    if (sucesso) ViewTamanho("VRSM");
                    break;

                case "NR":
                    sucesso = connect.InsertNR(diametro, tipo, out mensagem);
                    if (sucesso) ViewTamanho("NR");
                    break;

                default:
                    mensagem = "Por favor selecione um Tipo.";
                    break;
            }
            labelEstado.Text = mensagem;
            if (sucesso)
                tbDiametro.Text = "";
        }
        private void InserirClasse()
        {
            string Classe = tbClasse.Text;
            string Tipo = cbtipoclasse.Text;
            InsertBD Connect = new InsertBD();
            string mensagem;
            bool sucesso = Connect.InsertClasse(Classe, Tipo, out mensagem);
            labelEstado.Text = mensagem;
            if (sucesso)
            {
              ClasseView();
              tbClasse.Text = "";
            }
        }
        private void AtualizarConjunto()
        {
            UpdateBD Connect = new UpdateBD();
            string mensagem;
            if (dataGridViewConjuntos.SelectedRows.Count == 0)
            {
              MessageBox.Show("Selecione um conjunto para atualizar.");
              return;
            }
            int id = Convert.ToInt32(dataGridViewConjuntos.SelectedRows[0].Cells["id"].Value);
            string Norma = tbNormal.Text;
            string Parafuso = tbParafuso.Text;
            string Porca = tbPorca.Text;
            string Anilha = tbAnilha.Text;
            string Comprimento = tbComp.Text.Trim().PadLeft(3, '0');
            string Tipo = cbTipoConjunto.Text;
            bool sucesso = Connect.UpdateConjunto(id ,Norma, Parafuso, Porca, Anilha, Comprimento, Tipo, out mensagem);
            labelEstado.Text = mensagem;
            if (sucesso)
            {
              ConjuntoView();
            }
        }
        private void AtualizarItem()
        {
            if (dataGridViewTamanho.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione um conjunto para atualizar.");
                return;
            }

            if (cbTipo.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione um tipo antes de atualizar!");
                return;
            }

            int id = Convert.ToInt32(dataGridViewTamanho.SelectedRows[0].Cells["id"].Value);
            string Norma = cbNorma.Text;
            string Tipo = cbTipo.Text;
            string diametro = tbDiametro.Text.Trim().PadLeft(2, '0'); 
            string mensagem;

            UpdateBD Connect = new UpdateBD();
            bool sucesso = false;

            switch (Tipo)
            {
                case "BM":
                    sucesso = Connect.UpdateBM(id, Norma, diametro, Tipo, out mensagem);
                    if (sucesso) ViewTamanho("BM");
                    break;

                case "C":
                    sucesso = Connect.UpdateC(id, diametro, Tipo, out mensagem);
                    if (sucesso) ViewTamanho("C");
                    break;

                case "VRSM":
                    sucesso = Connect.UpdateVRSM(id, diametro, Tipo, out mensagem);
                    if (sucesso) ViewTamanho("VRSM");
                    break;

                case "NR":
                    sucesso = Connect.UpdateNR(id, diametro, Tipo, out mensagem);
                    if (sucesso) ViewTamanho("NR");
                    break;

                default:
                    mensagem = "Por favor selecione um Tipo.";
                    break;
            }

            labelEstado.Text = mensagem;
        }
        private void AtualizarClasse()
        {
            string Classe = tbClasse.Text;
            string Tipo = cbtipoclasse.Text;
            UpdateBD Connect = new UpdateBD();
            string mensagem;
            if (dataGridViewClasse.SelectedRows.Count == 0)
            {
              MessageBox.Show("Selecione um conjunto para atualizar.");
              return;
            }
            int id = Convert.ToInt32(dataGridViewClasse.SelectedRows[0].Cells["id"].Value);
            bool sucesso = Connect.UpdateClasse(id,Classe, Tipo, out mensagem);
            labelEstado.Text = mensagem;
            if (sucesso)
            {
              ClasseView();
              tbClasse.Text = "";
            }
        }
        private void EliminarConjunto()
        {
            DeleteBD Connect = new DeleteBD();
            string mensagem;
            if (dataGridViewConjuntos.SelectedRows.Count == 0)
            {
               MessageBox.Show("Selecione uma linha para eliminar.");
               return;
            }
            int id = Convert.ToInt32(dataGridViewConjuntos.SelectedRows[0].Cells["id"].Value);            
            bool sucesso = Connect.DeleteConjunto(id, out mensagem);
            labelEstado.Text = mensagem;
            if (sucesso)
            {
              ConjuntoView();
            }
        }
        private void Eliminar()
        {
            DeleteBD Connect = new DeleteBD();
            string mensagem;
            if (dataGridViewTamanho.SelectedRows.Count == 0)
            {
               MessageBox.Show("Selecione uma linha para eliminar.");
               return;
            }
            int id = Convert.ToInt32(dataGridViewTamanho.SelectedRows[0].Cells["id"].Value);
            string Tipo = cbTipo.Text;
            bool sucesso = Connect.Delete(id,Tipo, out mensagem);
            labelEstado.Text = mensagem;
            if (sucesso)
            {
                ViewTamanho(Tipo);
            }
        }        
        private void EliminarClasse()
        {
            DeleteBD Connect = new DeleteBD();
            string mensagem;
            if (dataGridViewClasse.SelectedRows.Count == 0)
            {
               MessageBox.Show("Selecione uma linha para eliminar.");
               return;
            }
            int id = Convert.ToInt32(dataGridViewClasse.SelectedRows[0].Cells["id"].Value);
            bool sucesso = Connect.DeleteClasse(id, out mensagem);
            labelEstado.Text = mensagem;
            if (sucesso)
            {
              ClasseView();
              tbClasse.Text = "";
            }
        }
        private void CarregarNormas()
        {
            BDParafusaria Connect = new BDParafusaria();
            try
            {
                Connect.ConectarBD();
                string query = "SELECT DISTINCT Norma FROM dbo.Conjunto";
                DataTable dt = Connect.Procurarbd(query);

                cbNorma.DataSource = dt;
                cbNorma.DisplayMember = "Norma"; 
                cbNorma.ValueMember = "Norma";   
                cbNorma.SelectedIndex = -1; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar normas: " + ex.Message);
            }
            finally
            {
                Connect.DesonectarBD();
            }
        }
        private void cbTipoConjunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoSelecionado = cbTipoConjunto.SelectedItem.ToString();
            ConjuntoViewSelect(tipoSelecionado);
        }
        private void cbchoosepanel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChoosePanel();
        }
        private void cbTipo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTipo.SelectedIndex == 1 || cbTipo.SelectedIndex == 2)
            {
                cbNorma.Visible = false;
                labelnorma.Visible = false;
                labeltipo.Location = new Point(175, 320);
                labeldiametro.Location = new Point(340, 320);
                cbTipo.Location = new Point(100, 350);
                tbDiametro.Location = new Point(335, 350);

            }
            ChooseTamanho();
        }
        private void ButtonInsertConj_Click(object sender, EventArgs e)
        {            
            if (string.IsNullOrWhiteSpace(tbNormal.Text))
            {
                MessageBox.Show("Introduza uma Norma!");
                return;

            }
            if (string.IsNullOrWhiteSpace(tbParafuso.Text))
            {
                MessageBox.Show("Introduza o nome do Parafuso!");
                return;
            }           
            InserirConjunto();
        }
        private void ButtonInsertTama_Click(object sender, EventArgs e)
        {
            if (cbTipo.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione um tipo antes de inserir !");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbDiametro.Text) || tbDiametro.Text == "00" || tbDiametro.Text == "0")
            {
                MessageBox.Show("Introduza um diâmetro válido!");
                return;
            }
            InserirItem();
        }
        private void ButtonInsertClasse_Click(object sender, EventArgs e)
        {
            if (cbtipoclasse.SelectedIndex < 0)
            {
                MessageBox.Show("Selecione um tipo antes de inserir a Classe!");
                return;
            }
            if (string.IsNullOrWhiteSpace(tbClasse.Text))
            {
                MessageBox.Show("Introduza um nome de Classe!");
                return;
            }
            InserirClasse();
        }
        private void ButtonUpgConj_Click(object sender, EventArgs e)
        {
            AtualizarConjunto();
        }
        private void ButtonUpgTama_Click(object sender, EventArgs e)
        {
            AtualizarItem();
        }
        private void ButtonUpgClasse_Click(object sender, EventArgs e)
        {
            AtualizarClasse();
        }
        private void ButtonDelConj_Click(object sender, EventArgs e)
        {
            EliminarConjunto();
        }
        private void ButtonDelTama_Click(object sender, EventArgs e)
        {
            Eliminar();
        }
        private void ButtonDelClasse_Click(object sender, EventArgs e)
        {
            EliminarClasse();
        }
        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void TextChanged(object sender, EventArgs e)
        {
            if (tbDiametro.Text.Length > 2)
            {
                tbDiametro.Text = tbDiametro.Text.Substring(0, 3);
                tbDiametro.SelectionStart = tbDiametro.Text.Length; 
            }
        }               

    }
}
