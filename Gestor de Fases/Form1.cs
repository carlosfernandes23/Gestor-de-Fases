using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestor_de_Fases
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;
            this.Text = "";
            cbFolderObra.Checked = true;
            cbFolderObra.CheckedChanged += CbFolderObra_CheckedChanged;
            cbOtherFolder.CheckedChanged += CbOtherFolder_CheckedChanged;
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            this.Size = new Size(1525, 915); 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            DataGridViewOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewOrder.MultiSelect = false; 
            CarregarTabela();            

        }
        bool OBRAINSERIDA = false; private void buttonconectarobra_Click(object sender, EventArgs e)
        {
            Buscarobra();
        }               
        private void CarregarTabela()
        {          
            string[] colunas = {
                "Fase", "Lote", "Artigo", "Ordem de fabrico", "Conjunto/Peça", "Revisão",
                "Referência cliente", "Id quantificação", "Qt.", "Perfil", "Classe",
                "Req. Especial", "Certificado", "Comprimento", "Peso(T)", "Área",
                "Grau de preparação", "Desenho cliente", "Entrega", "Artigo interno",
                "Destinatário externo", "Observações", "Operações", "Outros serviços 1",
                "Observações", "Outros serviços 2", "Observações", "Outros serviços 3",
                "Observações", "Outros serviços 4", "Observações","Serviços de pintura 1",
                "Observações", "Serviços de pintura 2", "Observações",
                "Serviços de pintura 3", "Observações", "CDU_MTTolerancias",
                "CDU_MTEstadoSuperficie", "CDU_MTPropriedadesEsp", "Local de Descarga",
                "Espessura", "Largura", "Cor", "Marca", "Norma", "Esquema de pintura"
            };

            foreach (var nome in colunas)
            {
                DataGridViewOrder.Columns.Add(nome, nome);
            }

            foreach (DataGridViewColumn col in DataGridViewOrder.Columns)
            {
                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                col.ReadOnly = true;
            }

            int[] editaveis = { 6, 11, 17, 21 };
            foreach (int index in editaveis)
            {
                if (index < DataGridViewOrder.Columns.Count)
                    DataGridViewOrder.Columns[index].ReadOnly = false;
            }
        }
        private void Buscarobra()
        {
            try
            {
                string obra = TextBoxObra.Text.Trim().ToUpper();

                if (string.IsNullOrEmpty(obra))
                {
                    MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Inicio inicio = new Inicio();
                var info = inicio.BuscarObra(obra);
                labelobraview.Text = "Obra - ";
                labelNObra.Text = obra;
                labelDesignacao.Text = "Designação - " + info.Designacao;
                labelCliente.Text = "Cliente - " + info.Cliente;
                labelfase500.Text = info.Fase250;
                labelfase750.Text = info.Fase750;
                labelfase1000.Text = info.Fase1000;

                OBRAINSERIDA = true;
                labelConectado.Visible = true;
                TextBoxObra.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        bool menuExpandido = false; private void Buttonabrirpedido_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                if (menuExpandido)
                {                   
                    PanelMenuPedido.Height = 50;
                    Panelmenu.Height = 100;

                }
                else
                {                    
                    PanelMenuPedido.Height = 200;
                    Panelmenu.Height = 250;
                }

                PanelMenuPedido.Width = 200;
                menuExpandido = !menuExpandido;
            }
            else
            {
                MessageBox.Show(
                    "Por favor, insira o número da obra antes de pesquisar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
        private void AbrirFormNoPainel(Form formpanel)
        {
            PanelTodos.Controls.Clear();
            formpanel.TopLevel = false;
            formpanel.FormBorderStyle = FormBorderStyle.None;
            formpanel.Dock = DockStyle.Fill;
            formpanel.Size = new Size(1395, 390);
            formpanel.MaximumSize = new Size(1395, 390);
            formpanel.MinimumSize = new Size(1395, 390);
            PanelTodos.Controls.Add(formpanel);
            formpanel.Show();
        }
        private void ButtonCMarm_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                if (!VerificarEConfirmarSaida())
                {
                    return;
                }
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                labelEstado.Text = "CM - Armazem";
                AbrirFormNoPainel(new CM());
            }
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ButtonCP_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                if (!VerificarEConfirmarSaida())
                {
                    return;
                }
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                labelEstado.Text = "CP - Chapas e Perfilados";
                AbrirFormNoPainel(new CP(this));
            }
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }     
        private void ButtonCQ_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                if (!VerificarEConfirmarSaida())
                {
                    return;
                }
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new CQ(this));
                labelEstado.Text = "CQ - Corte e Quinagem";
            }
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ButtonCL_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                if (!VerificarEConfirmarSaida())
                {
                    return;
                }
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                labelEstado.Text = "CL - Corte a Laser";
                AbrirFormNoPainel(new CL(this));
            }
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ButtonDAP_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                if (!VerificarEConfirmarSaida())
                {
                    return;
                }
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new DAP(this));
                labelEstado.Text = "DAP - Departamento de aprovisionamentos";
            }
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void TextBoxObra_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
            {
                Buscarobra();
            }
        }
        private bool VerificarEConfirmarSaida()
        {
            int linhasVisiveis = DataGridViewOrder.Rows.Cast<DataGridViewRow>().Count(r => !r.IsNewRow);

            if (linhasVisiveis >= 1)
            {
                var resultado = MessageBox.Show(
                    "Existe dados na tabela! Deseja sair e eliminar os dados?",
                    "Confirmação",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resultado == DialogResult.Yes)
                {
                    DataGridViewOrder.Rows.Clear();
                    return true; 
                }
                else
                {
                    return false; 
                }
            }
            return true; 
        }
        private void EnviaCssobrafolder()
        {
            int pesototal = 0;
            int areatotal = 0;
            string Nobra = labelNObra.Text.ToUpper().Trim();
            string Designacao = labelDesignacao.Text.Trim();
            string Cliente = labelCliente.Text.Trim();
            string classeex = labelClasseEx.Text.Trim();

            string ano = null;
            if (labelNObra.Text.ToLower().Contains("pt"))
            {
                ano = labelNObra.Text.Substring(2, 2);
            }
            else
            {
                ano = labelNObra.Text.Substring(0, 2);
            }

            string filename = @"\\Marconi\company shared folder\OFELIZ\OFM\2.AN\2.CM\DP\1 Obras\20" + ano + "\\" + Nobra + @"\1.9 Gestão de fabrico\";
            List<string> l = new List<string>();
            foreach (DataGridViewRow item in DataGridViewOrder.Rows)
            {
                try
                {
                    if (!string.IsNullOrEmpty(item.Cells[0].Value.ToString()))
                    {
                        l.Add(item.Cells[0].Value.ToString());
                    }
                }
                catch (Exception)
                {         }
            }
            List<string> nova = new List<string>();
            nova.AddRange(l.Distinct());
            List<string> numerodelinhas = new List<string>();
            foreach (var item in nova)
            {
                int numero = 0;
                string Departamento = null;
                foreach (DataGridViewRow row in DataGridViewOrder.Rows)
                {
                    try
                    {
                        if (row.Cells[20].Value.ToString().Trim() == "CL" || row.Cells[20].Value.ToString().Trim() == "DAP" || row.Cells[20].Value.ToString().Trim() == "CQ" || row.Cells[20].Value.ToString().Trim() == "CP" || row.Cells[20].Value.ToString().Trim() == "08")
                        {
                            if (row.Cells[20].Value.ToString().Trim() == "08")
                            {
                                Departamento = "ARM";
                            }
                            else if (row.Cells[20].Value.ToString().Trim() == "CL")
                            {
                                Departamento = "LASER";
                            }
                            else
                            {
                                Departamento = row.Cells[20].Value.ToString().Trim();
                            }
                        }
                        else
                        {
                            Departamento = null;
                        }

                        if (Departamento != null)
                        {
                            if (!Directory.Exists(@"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\11 Partilhada\20" + ano + "\\" + Departamento + "\\" + Nobra + "\\" + Convert.ToInt16(row.Cells[0].Value.ToString()).ToString("000")))
                            {
                                Directory.CreateDirectory(@"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\11 Partilhada\20" + ano + "\\" + Departamento + "\\" + Nobra + "\\" + Convert.ToInt16(row.Cells[0].Value.ToString()).ToString("000"));
                            }
                        }
                    }
                    catch (Exception) { }

                    try
                    {
                        if (row.Cells[0].Value.ToString() == item)
                        {
                            numero += 1;
                        }
                    }
                    catch (Exception)
                    {          }
                }
                numerodelinhas.Add(item + ";" + numero);
            }
            foreach (var item in numerodelinhas)
            {

                DataGridView DGV = DataGridViewOrder;
                int columnCount = DGV.ColumnCount;
                string columnNames = "";
                string[] output = new string[(int.Parse(item.Split(';')[1]) + 8)];

                for (int i = 0; i < columnCount; i++)
                {
                    columnNames += DGV.Columns[i].HeaderText.ToString() + ";";
                }
                output[0] = "O FELIZ FICHA DE PEÇAS";
                output[1] = Designacao.Replace("Designação -", "Designação:;");
                output[2] = Cliente.Replace("Cliente -", "Cliente:;");
                output[3] = "Nº Obra:;" + Nobra;
                output[4] = "Data:;" + DateTime.Now.ToShortDateString();
                output[5] = "Classe de Execução:;" + classeex;
                output[6] = "Observações;" /*+ textBox1.Text*/ + ";;;;;;;;;;;;;" + pesototal + ";" + areatotal;
                output[7] += columnNames;
                int a = 0;
                bool b = false;
                foreach (DataGridViewRow row in DataGridViewOrder.Rows)
                {
                    b = false;
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.RowIndex + 1 != DGV.Rows.Count)
                        {
                            if (cell.Value != null)
                            {
                                if (cell.Value.ToString() == item.Split(';')[0] || b == true)
                                {
                                    output[a + 8] += cell.Value.ToString() + ";";
                                    b = true;
                                }
                            }
                            else
                            {
                                output[a + 8] += ";";
                                b = true;
                            }
                        }

                    }
                    if (b)
                    {
                        a++;
                    }

                }
                System.IO.Directory.CreateDirectory(filename + item.Split(';')[0]);

                if (!Directory.Exists(filename + item.Split(';')[0] + "\\2 nesting\\N\\20002"))
                {
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\2 nesting\\N\\20002");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\2 nesting\\N\\20003");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\2 nesting\\Q\\20002");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\2 nesting\\Q\\20003");
                }
                if (!Directory.Exists(filename + item.Split(';')[0] + "\\20002"))
                {
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20001");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20002");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20003");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20004");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20005");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20006");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20007");
                    Directory.CreateDirectory(filename + item.Split(';')[0] + "\\20009");
                }
                System.IO.File.WriteAllLines(filename + output[8].Split(';')[1] + "\\" + Nobra + "F" + item.Split(';')[0] + ".CSV", output, System.Text.Encoding.Default);
                MessageBox.Show("Dados exportados com sucesso", "exportação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }
        private void EnviarCssOtherfolder()
        {
            int pesototal = 0;
            int areatotal = 0;            
            string Nobra = labelNObra.Text.ToUpper().Trim();
            string Designacao = labelDesignacao.Text.Trim();
            string Cliente = labelCliente.Text.Trim();
            string classeex = labelClasseEx.Text.Trim();

            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {

                string filename = fbd.SelectedPath + "\\";
                List<string> l = new List<string>();
                foreach (DataGridViewRow item in DataGridViewOrder.Rows)
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(item.Cells[0].Value.ToString()))
                        {
                            l.Add(item.Cells[0].Value.ToString());
                        }
                    }
                    catch (Exception)
                    {


                    }

                }
                List<string> nova = new List<string>();
                nova.AddRange(l.Distinct());
                List<string> numerodelinhas = new List<string>();
                foreach (var item in nova)
                {

                    int numero = 0;

                    foreach (DataGridViewRow row in DataGridViewOrder.Rows)
                    {

                        try
                        {
                            if (row.Cells[0].Value.ToString() == item)
                            {
                                numero += 1;
                            }
                        }
                        catch (Exception)
                        {


                        }

                    }
                    numerodelinhas.Add(item + ";" + numero);
                }
                foreach (var item in numerodelinhas)
                {

                    DataGridView DGV = DataGridViewOrder;
                    int columnCount = DGV.ColumnCount;
                    string columnNames = "";
                    string[] output = new string[(int.Parse(item.Split(';')[1]) + 8)];

                    for (int i = 0; i < columnCount; i++)
                    {
                        columnNames += DGV.Columns[i].HeaderText.ToString() + ";";
                    }

                    output[0] = "O FELIZ FICHA DE PEÇAS";
                    output[1] = Designacao.Replace("Designação -", "Designação:;");
                    output[2] = Cliente.Replace("Cliente -", "Cliente:;");
                    output[3] = "Nº Obra:;" + Nobra;
                    output[4] = "Data:;" + DateTime.Now.ToShortDateString();
                    output[4] = "Data:;" + DateTime.Now.ToShortDateString();
                    output[5] = "Classe de Execução:;" + classeex;
                    output[6] = "Observações;" + /*textBox1.Text +*/ ";;;;;;;;;;;;;" + pesototal + ";" + areatotal;
                    output[7] += columnNames;
                    int a = 0;
                    bool b = false;
                    string ano = "20" + Nobra.Substring(0, 2);
                    string Departamento = null;
                    if (Nobra.Contains("PT"))
                    {
                        ano = "20" + Nobra.Substring(2, 2);
                    }

                    foreach (DataGridViewRow row in DataGridViewOrder.Rows)
                    {
                        b = false;
                        try
                        {
                            if (row.Cells[20].Value.ToString().Trim() == "CL")
                            {
                                Departamento = "LASER";
                            }
                            else if (row.Cells[20].Value.ToString().Trim() == "DAP" || row.Cells[20].Value.ToString().Trim() == "CQ" || row.Cells[20].Value.ToString().Trim() == "CP")
                            {
                                Departamento = row.Cells[20].Value.ToString().Trim();
                            }
                            else
                            {
                                Departamento = null;
                            }

                            if (Departamento != null)
                            {
                                if (!Directory.Exists(@"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\11 Partilhada\" + ano + "\\" + Departamento + "\\" + Nobra + "\\" + Convert.ToInt16(row.Cells[0].Value.ToString()).ToString("000")))
                                {
                                    Directory.CreateDirectory(@"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\11 Partilhada\" + ano + "\\" + Departamento + "\\" + Nobra + "\\" + Convert.ToInt16(row.Cells[0].Value.ToString()).ToString("000"));
                                }
                            }
                        }
                        catch (Exception) { }

                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            if (cell.RowIndex + 1 != DGV.Rows.Count)
                            {
                                if (cell.Value.ToString() == item.Split(';')[0] || b == true)
                                {
                                    output[a + 8] += cell.Value.ToString() + ";";
                                    b = true;
                                }
                            }

                        }
                        if (b)
                        {
                            a++;
                        }
                    }
                    System.IO.Directory.CreateDirectory(filename + item.Split(';')[0]);
                    System.IO.File.WriteAllLines(filename + output[8].Split(';')[1] + "\\" + Nobra + "F" + item.Split(';')[0] + ".CSV", output, System.Text.Encoding.Default);
                    MessageBox.Show("Dados exportados com sucesso", "exportação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        private void CbFolderObra_CheckedChanged(object sender, EventArgs e)
        {
            if (cbFolderObra.Checked)
            {
                cbOtherFolder.Checked = false;
            }
            else
            {
                if (!cbOtherFolder.Checked)
                    cbFolderObra.Checked = true;
            }
        }
        private void CbOtherFolder_CheckedChanged(object sender, EventArgs e)
        {
            if (cbOtherFolder.Checked)
            {
                cbFolderObra.Checked = false;
            }
            else
            {
                if (!cbFolderObra.Checked)
                    cbFolderObra.Checked = true;
            }
        }
        private void ButtonGerarPdf_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                labelnomeficheiro OF = new labelnomeficheiro(this);
                OF.ShowDialog();
                this.Visible = true;
            }
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ButtonFolderObra_Click(object sender, EventArgs e)
        {
            string nobra = labelNObra.Text.Trim();
            int ano = 0;

            if (nobra.Length >= 4)
            {
                string anoStr = nobra.StartsWith("PT", StringComparison.OrdinalIgnoreCase)
                    ? nobra.Substring(2, 2)
                    : nobra.Substring(0, 2);

                if (!int.TryParse("20" + anoStr, out ano))
                {
                    ano = 0;
                }
            }
            if (ano > 0)
            {
                string caminho = Path.Combine(
                    @"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\1 Obras",
                    ano.ToString(),
                    nobra,
                    "1.9 Gestão de fabrico"
                );

                if (Directory.Exists(caminho))
                {
                    Process.Start(caminho);
                }
                else
                {
                    MessageBox.Show("Número da Obra errado." + Environment.NewLine +
                                    "P.f. Insira o número da obra corretamente e tente de novo.",
                                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Número da Obra errado." + Environment.NewLine +
                                "P.f. Insira o número da obra corretamente e tente de novo.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void buttonmoveCSS_Click(object sender, EventArgs e)
        {
            if (cbFolderObra.Checked)
            {
                EnviaCssobrafolder();
            }
            else
            {
                EnviarCssOtherfolder();
            }
        }
        private void ButtonFolderR_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\r");

        }
        private void ButtonMovefiles_Click(object sender, EventArgs e)
        {
        }

        
    }
}
