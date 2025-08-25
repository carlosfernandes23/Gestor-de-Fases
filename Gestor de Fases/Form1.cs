using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters.Entities;
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
            string user = Environment.UserName;
            string formatado = user.Replace(".", " ");
            string resultado = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formatado);
            labeluser.Text = resultado;
            DateTime horaAtual = DateTime.Now;
            string saudacao = ObterSaudacao(horaAtual);
            labelSaudacao.Text = saudacao;
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            this.Size = new Size(1600, 910); 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            DataGridViewOrder.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridViewOrder.MultiSelect = false; 
            Userpic.Click += Abrirstettings;
            CarregarTabela();           
            LimparPasta();
            foreach (DataGridViewColumn coluna in DataGridViewOrder.Columns)
            {
                coluna.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            DataGridViewOrder.ColumnHeaderMouseClick += ColumnHeaderMouseClick;
            DataGridViewOrder.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }
        bool OBRAINSERIDA = false; private void buttonconectarobra_Click(object sender, EventArgs e)
        {
            if (TextBoxObra.Text.Length < 8)
            {
                MessageBox.Show("O código da obra deve ter pelo menos 8 dígitos!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextBoxObra.Focus();
                return;
            } 
            else
            {
                Buscarobra();
                ClasseexeGrauprep();
            }               
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
                labelConectado.ForeColor = Color.Green;
                labelConectado.Text = "Conectado";
                labelestado.Text = "";
                TextBoxObra.Clear();
            }
            catch (Exception ex)
            {
                labelConectado.ForeColor = Color.Red;
                labelConectado.Text = "Erro! nao encontrou a obra";
                labelestado.Text = ex.Message;
                labelNObra.Text = "";
                labelDesignacao.Text = "Designação - ";
                labelCliente.Text = "Cliente - ";
                labelfase500.Text = "";
                labelfase750.Text = "";
                labelfase1000.Text = "";
                labelClasseEx.Text = "";
                labelGrauprep.Text = "";
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
                labelestado.Text = "";
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                labelDestino.Text = "CM - Armazem";
                AbrirFormNoPainel(new CM(this));
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
                labelestado.Text = "";
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                labelDestino.Text = "CP - Chapas e Perfilados";
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
                labelestado.Text = "";
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new CQ(this));
                labelDestino.Text = "CQ - Corte e Quinagem";
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
                labelestado.Text = "";
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                labelDestino.Text = "CL - Corte a Laser";
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
                labelestado.Text = "";
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new DAP(this));
                labelDestino.Text = "DAP - Dep de aprovisionamentos";
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
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
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
                    { }
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
                            { numero += 1; }
                        }
                        catch (Exception)
                        { }
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
        private void MoverficheirospastaR()
        {
            string nome = null;
            string obra = labelNObra.Text.Trim();
            bool RESPOSTA = false;
            ArrayList FICHEIROSEMFALTA = new ArrayList();
            foreach (DataGridViewRow item in DataGridViewOrder.Rows)
            {
                try
                {
                    if (item.Index != DataGridViewOrder.Rows.Count - 1)
                    {

                        nome = item.Cells[4].Value.ToString().Split('.')[1] + "." + item.Cells[4].Value.ToString().Split('.').Last();

                    }
                }
                catch (Exception)
                {

                }


                try
                {
                    if (!item.Cells[4].Value.ToString().ToUpper().Contains("H"))
                    {
                        if (!File.Exists("C:\\r\\2." + nome + ".pdf"))
                        {
                            FICHEIROSEMFALTA.Add("2." + nome + ".pdf");
                        }
                    }
                }
                catch (Exception)
                {


                }
            }
            if (FICHEIROSEMFALTA.Count > 0)
            {

                string r = null;
                foreach (var item in FICHEIROSEMFALTA)
                {
                    r += item + " \n";
                }
                if (MessageBox.Show("Faltaram na pasta processo os ficheiros: " + Environment.NewLine + r + Environment.NewLine + "Deseja prosseguir mesmo assim ?", "Ficheiros em falta", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    RESPOSTA = true;
                }
            }


            if (FICHEIROSEMFALTA.Count == 0)
            {
                RESPOSTA = true;
            }

            FICHEIROSEMFALTA.Clear();

            if (RESPOSTA == true)
            {
                foreach (DataGridViewRow item in DataGridViewOrder.Rows)
                {
                    if (item.Index != DataGridViewOrder.Rows.Count - 1)
                    {
                        if (item.Cells[4].Value.ToString().Split('.').Count() == 3)
                        {
                            nome = item.Cells[4].Value.ToString().Split('.')[1] + "." + item.Cells[4].Value.ToString().Split('.')[2];
                        }
                        else
                        {
                            nome = item.Cells[4].Value.ToString().Split('.')[1] + "." + item.Cells[4].Value.ToString().Split('.')[3];
                        }


                        string ano = "";
                        if (obra.ToUpper().Contains("PT"))
                        {
                            ano = obra.ToUpper().Substring(2, 2);
                        }
                        else
                        {
                            ano = obra.ToUpper().Substring(0, 2);
                        }
                        string departamento = "";
                        string artigo = "";
                        string caminhocm = @"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\1 Obras\20" + ano + "\\" + obra + @"\1.9 Gestão de fabrico\" + item.Cells[0].Value.ToString().Trim() + "\\20001\\";

                        string caminholaser = @"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\11 Partilhada\20" + ano + "\\LASER\\" + obra + "\\" + item.Cells[0].Value.ToString().Trim() + "\\" + item.Cells[9].Value.ToString().Trim() + "_" + item.Cells[10].Value.ToString().Trim() + "\\";

                        string caminhocq = @"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\11 Partilhada\20" + ano + "\\CQ\\" + obra + "\\" + item.Cells[0].Value.ToString().Trim() + "\\";

                        string caminhocp = @"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\11 Partilhada\20" + ano + "\\CP\\" + obra + "\\" + item.Cells[0].Value.ToString().Trim() + "\\";



                        try
                        {
                            departamento = (string)item.Cells[20].Value;
                        }
                        catch (Exception)
                        {
                        }
                        try
                        {
                            artigo = (string)item.Cells[2].Value;
                        }
                        catch (Exception)
                        {
                        }
                        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
                        if (artigo.Trim().ToUpper() == "CONJUNTO")
                        {
                            if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                            {
                                if (!Directory.Exists(caminhocm.Replace("20001", "20004")))
                                {
                                    Directory.CreateDirectory(caminhocm.Replace("20001", "20004"));
                                }
                                File.Move("C:\\r\\2." + nome + ".pdf", caminhocm.Replace("20001", "20004") + "2." + nome + ".pdf");
                            }
                            else
                            {
                                FICHEIROSEMFALTA.Add("2." + nome + ".pdf ");
                            }
                        }
                        else if (artigo.Trim().ToUpper() == "PERFIL")
                        {
                            try
                            {
                                if (item.Cells[22].Value.ToString().ToLower() == "corte e furação")
                                {
                                    if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                                    {
                                        if (!Directory.Exists(caminhocm.Replace("20001", "20003")))
                                        {
                                            Directory.CreateDirectory(caminhocm.Replace("20001", "20003"));
                                        }
                                        File.Move("C:\\r\\2." + nome + ".pdf", caminhocm.Replace("20001", "20003") + "2." + nome + ".pdf");
                                    }
                                    else
                                    {
                                        FICHEIROSEMFALTA.Add("2." + nome + ".pdf ");
                                    }
                                }
                                else if (item.Cells[22].Value.ToString().ToLower() == "corte")
                                {
                                    if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                                    {
                                        if (!Directory.Exists(caminhocm.Replace("20001", "20002")))
                                        {
                                            Directory.CreateDirectory(caminhocm.Replace("20001", "20002"));
                                        }
                                        File.Move("C:\\r\\2." + nome + ".pdf", caminhocm.Replace("20001", "20002") + "2." + nome + ".pdf");
                                    }
                                    else
                                    {
                                        FICHEIROSEMFALTA.Add("2." + nome + ".pdf ");
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Erro falta a operação na linha da peça " + item.Cells[4].Value.ToString());

                            }

                        }
                        else if (departamento.Trim().ToUpper() == "DAP")
                        {
                            if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                            {
                                if (!Directory.Exists(caminhocm.Replace("20001", "20004")))
                                {
                                    Directory.CreateDirectory(caminhocm.Replace("20001", "20004"));
                                }
                                File.Move("C:\\r\\2." + nome + ".pdf", caminhocm.Replace("20001", "20004") + "2." + nome + ".pdf");
                            }
                            else
                            {
                                FICHEIROSEMFALTA.Add("2." + nome + ".pdf ");
                            }
                        }
                        else if (departamento.Trim().ToUpper() == "CP")
                        {
                            if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                            {
                                if (!Directory.Exists(caminhocp))
                                {
                                    Directory.CreateDirectory(caminhocp);
                                }
                                if (!Directory.Exists(caminhocm.Replace("20001", "20004")))
                                {
                                    Directory.CreateDirectory(caminhocm.Replace("20001", "20004"));
                                }
                                File.Copy("C:\\r\\2." + nome + ".pdf", caminhocp + nome + ".pdf");
                                File.Move("C:\\r\\2." + nome + ".pdf", caminhocm.Replace("20001", "20004") + "2." + nome + ".pdf");
                            }
                            else
                            {
                                FICHEIROSEMFALTA.Add("2." + nome + ".pdf ");
                            }
                        }
                        else if (departamento.Trim().ToUpper() == "CP")
                        {
                            if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                            {
                                if (!Directory.Exists(caminhocp))
                                {
                                    Directory.CreateDirectory(caminhocp);
                                }
                                if (!Directory.Exists(caminhocm.Replace("20001", "20004")))
                                {
                                    Directory.CreateDirectory(caminhocm.Replace("20001", "20004"));
                                }
                                File.Copy("C:\\r\\2." + nome + ".pdf", nome + ".pdf");
                                File.Move("C:\\r\\2." + nome + ".pdf", caminhocm.Replace("20001", "20004") + "2." + nome + ".pdf");
                            }
                            else
                            {
                                FICHEIROSEMFALTA.Add(nome + ".pdf ");
                            }
                        }
                        else if (departamento.Trim().ToUpper() == "CQ")
                        {
                            if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                            {
                                if (!Directory.Exists(caminhocq))
                                {
                                    Directory.CreateDirectory(caminhocq);
                                }
                                if (!Directory.Exists(caminhocm))
                                {
                                    Directory.CreateDirectory(caminhocm);
                                }
                                File.Copy("C:\\r\\2." + nome + ".pdf", caminhocq + "2." + nome + ".pdf");
                                File.Move("C:\\r\\2." + nome + ".pdf", caminhocm + "2." + nome + ".pdf");
                            }
                            else
                            {
                                FICHEIROSEMFALTA.Add("2." + nome + ".pdf ");
                            }
                        }
                        else if (departamento.Trim().ToUpper() == "CL")
                        {
                            if (File.Exists("C:\\r\\2." + nome + ".pdf"))
                            {
                                if (!Directory.Exists(caminholaser))
                                {
                                    Directory.CreateDirectory(caminholaser);
                                }
                                if (!Directory.Exists(caminhocm))
                                {
                                    Directory.CreateDirectory(caminhocm);
                                }
                                File.Copy("C:\\r\\2." + nome + ".pdf", caminholaser + "2." + nome + ".pdf");
                                File.Move("C:\\r\\2." + nome + ".pdf", caminhocm + "2." + nome + ".pdf");
                                if (File.Exists("C:\\r\\2." + nome + ".dxf"))
                                {
                                    File.Move("C:\\r\\2." + nome + ".dxf", caminholaser + "2." + nome + ".dxf");
                                }
                                else
                                {
                                    FICHEIROSEMFALTA.Add("2." + nome + ".dxf ");
                                }
                            }
                            else
                            {
                                FICHEIROSEMFALTA.Add("2." + nome + ".pdf ");
                            }
                        }
                    }
                }
                if (FICHEIROSEMFALTA.Count > 0)
                {

                    string r = null;
                    foreach (var item1 in FICHEIROSEMFALTA)
                    {
                        r += item1 + " \n";
                    }
                    MessageBox.Show("Faltaram na pasta processo os ficheiros: " + Environment.NewLine + r, "Ficheiros em falta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                }
            }
        }
        private void ClasseexeGrauprep()
        {
            string nobra = labelNObra.Text.Trim();
            string ano = "20" + nobra.Substring(0, 2);
            string caminhoclasse = Path.Combine(
                @"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\1 Obras",
                ano,
                nobra,
                @"1.8 Projeto\1.8.2 Tekla\classeex.txt"
            );
            if (File.Exists(caminhoclasse))
            {
               string conteudoClasse = File.ReadAllText(caminhoclasse).Trim();
               labelClasseEx.Text = conteudoClasse;                              
            }
            string caminhoGrau = Path.Combine(
                @"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\1 Obras",
                ano,
                nobra,
                @"1.8 Projeto\1.8.2 Tekla\grauprep.txt"
            );
            if (File.Exists(caminhoGrau))
            {
                string conteudoGrau = File.ReadAllText(caminhoGrau).Trim();
                labelGrauprep.Text = conteudoGrau;
            }
        }
        private void LimparPasta()
        {
            string path = @"C:\r";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                return;
            }

            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch (Exception)
                    {           }
                }
                foreach (string dir in Directory.GetDirectories(path))
                {
                    try
                    {
                        Directory.Delete(dir, true); 
                    }
                    catch (Exception)
                    {      }
                }
            }
        }
        static string ObterSaudacao(DateTime hora)
        {
            if (hora.Hour >= 0 && hora.Hour < 13)
            {
                return "Bom Dia";
            }
            else if (hora.Hour >= 13 && hora.Hour < 18)
            {
                return "Boa Tarde";
            }
            else if (hora.Hour == 18 && hora.Minute <= 30)
            {
                return "Boa Tarde";
            }
            else
            {
                return "Boa Noite";
            }
        }
        private void Abrirstettings(object sender, EventArgs e)
        {
            try
            {
                Process.Start(new ProcessStartInfo("ms-settings:yourinfo") { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível abrir as configurações: " + ex.Message);
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
            if (OBRAINSERIDA)
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
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void buttonmoveCSS_Click(object sender, EventArgs e)
        {
            if (DataGridViewOrder.Rows.Count >= 2 )
            {
                EnviarCssOtherfolder();
            }
            else
            {
                MessageBox.Show("Atenção , a lista está sem Conteudo!",
                                "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ButtonFolderR_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("C:\\r");

        }      
        private void ButtonMovefiles_Click(object sender, EventArgs e)
        {
            if (OBRAINSERIDA)
            {
                EnviaCssobrafolder();
                MoverficheirospastaR();

                System.Threading.Tasks.Task.Delay(2000).Wait();
                AppAbrirPrimavera primaveraHandler = new AppAbrirPrimavera();
                primaveraHandler.AbrirPrimaveira();
            }
            else
            {
                MessageBox.Show("Por favor, insira o número da obra antes de pesquisar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewOrder.Columns[e.ColumnIndex].SortMode = DataGridViewColumnSortMode.NotSortable;
        }
        public void buttonAbrirPrimavera_Click(object sender, EventArgs e)
        {
            AppAbrirPrimavera primaveraHandler = new AppAbrirPrimavera();
            primaveraHandler.AbrirPrimaveira();
        }
        public class AppAbrirPrimavera
        {
            [DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);

            [DllImport("user32.dll")]
            public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            const int SW_RESTORE = 9;

            public void AbrirPrimaveira()
            {
                try
                {
                    string nomeProcesso = "Erp100EV";
                    string appPath = @"C:\Program Files (x86)\PRIMAVERA_EVO\SG100\Apl\Erp100EV.exe";

                    Process[] processos = Process.GetProcessesByName(nomeProcesso);

                    if (processos.Length > 0)
                    {
                        Process processoExistente = processos[0];

                        IntPtr hWnd = processoExistente.MainWindowHandle;

                        if (hWnd != IntPtr.Zero)
                        {
                            ShowWindow(hWnd, SW_RESTORE);
                            SetForegroundWindow(hWnd);
                        }
                        else
                        {
                            MessageBox.Show("Primavera já está aberto, mas não foi possível aceder à janela principal.");
                        }
                    }
                    else
                    {
                        if (System.IO.File.Exists(appPath))
                        {
                            Process processoNovo = Process.Start(appPath);

                            Thread.Sleep(1000);

                            IntPtr hWnd = processoNovo.MainWindowHandle;

                            if (hWnd != IntPtr.Zero)
                            {
                                ShowWindow(hWnd, SW_RESTORE);
                                SetForegroundWindow(hWnd);
                            }
                            else
                            {
                                //MessageBox.Show("Primavera iniciado, mas não foi possível aceder à janela principal.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("O Primavera não foi encontrado no PC.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao tentar abrir o Primavera: " + ex.Message);
                }
            }
        }
               
    }
}
