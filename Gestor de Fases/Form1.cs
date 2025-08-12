using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestor_de_Fases
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            this.Size = new Size(1525, 915); 
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            CarregarTabela();
        }

        bool OBRAINSERIDA = false;
        private void buttonconectarobra_Click(object sender, EventArgs e)
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
                "Destinatário externo", "Observações 1", "Operações", "Outros serviços 1",
                "Observações 2", "Outros serviços 2", "Observações 3", "Outros serviços 3",
                "Observações 4", "Outros serviços 4", "Serviços de pintura 1",
                "Observações 5", "Serviços de pintura 2", "Observações 6",
                "Serviços de pintura 3", "Observações 7", "CDU_MTTolerancias",
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
                labelNObra.Text = obra;
                labelDesignacao.Text = info.Designacao;
                labelCliente.Text = info.Cliente;
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

        bool menuExpandido = false;
        private void Buttonabrirpedido_Click(object sender, EventArgs e)
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
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
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
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new CP());
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
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new CQ());
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
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new CL());
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
                Panelmenu.Height = 100;
                PanelMenuPedido.Height = 50;
                menuExpandido = !menuExpandido;
                AbrirFormNoPainel(new DAP());
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


    }
}
