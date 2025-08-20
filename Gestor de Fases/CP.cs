using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_de_Fases
{
    public partial class CP: Form
    {
        Form1 F;

        public CP(Form1 formInicio)
        {
            InitializeComponent();
            F = formInicio;
        }

        private void CP_Load(object sender, EventArgs e)
        {
            Datematerialemobra.Value = DateTime.Now;
            BD d = new BD();
            List<string> l = new List<string>();
            d.ligarbdprepararacao();
            l = d.Procurarbdprepararacao("SELECT [Familia]  FROM [ArtigoTekla].[dbo].[Perfilagem3] where Dest='cp'");
            d.desligarbdprepararacao();
            foreach (var item in l)
            {

                if (!cbFamilia.Items.Contains(item))
                {
                    cbFamilia.Items.Add(item);
                }

            }
            cbFamilia.SelectedIndex = 0;
        }

        int linhaCONJ = 1;
        private void ButtonAddPerfilCP_Click(object sender, EventArgs e)
        {
            if (cbPerfil.Text.Trim() == "")
            {
                F.labelestado.Text = "Por favor, selecione um Perfil.";
                return;
            }
            else if (cbMaterial.Text.Trim() == "")
            {
                F.labelestado.Text = "Por favor, selecione um Material.";
                return;
            }
            else if (tbQtd.Text.Trim() == "" || tbQtd.Text.Trim() == "0")
            {
                F.labelestado.Text = "Por favor, coloque as quantidesd maior que 0.";
                return;
            }
            else if (tbComp.Text.Trim() == "")
            {
                F.labelestado.Text = "Por favor, insira o comprimento.";
                return;
            }            
            string Perfil = cbPerfil.Text.Trim();
            string comprimento = tbComp.Text.Trim().Replace('.', ',');
            string Quantidade = tbQtd.Text.Trim().Replace('.', ',');
            string pesoLiquido = Pesoliquido.Text.Replace('.', ',');
            BD Connect = new BD();
            Connect.ligarbdprepararacao();
            string Largura = Connect.Procurarbdprepararacao("SELECT [LarguraUtil] FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + Perfil + "'")[0];
            string artigo = Connect.Procurarbdprepararacao("SELECT artigo FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + Perfil + "'")[0];
            Connect.desligarbdprepararacao();
            double peso = double.Parse(pesoLiquido) * double.Parse(comprimento) / 1000 * double.Parse(Quantidade);
            double area = (double.Parse(comprimento) / 1000) * (double.Parse(Largura) / 1000) * double.Parse(Quantidade);                        
            string destinatario = "CP";
            string ral = tbRalpre.Text;
            string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
            string fase750 = F.labelfase750.Text.Trim();
            string noObra = F.labelNObra.Text.Trim();
            string material = cbMaterial.Text.Trim();
            string certificado = cbcertificado.Text.Trim();
            string ordemFabrico = "2." + noObra + "." + fase750 + "." + F.DataGridViewOrder.Rows.Count;
            string conjuntoPeca = "2." + noObra + "." + fase750 + "." + fase750 + "CJ" + linhaCONJ;
            string observacoes = tbObs.Text.Trim();
            if (cbFamilia.Text.ToUpper().Contains("CH PERFILADA"))
            {
                ral = "";
                if (tbFace1.Text != "")
                {
                    ral += tbFace1.Text + "_Face 1";
                }
                if (ral != "" && tbFace2.Text != "")
                {
                    ral += "|";
                }
                if (tbFace2.Text != "")
                {
                    ral += tbFace2.Text + "_Face 2";
                }
            }
            linhaCONJ += 1;
            F.DataGridViewOrder.Rows.Add(fase750, fase750, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, material, "", certificado, comprimento, peso.ToString("0.##"), area, "", "", dataFormatada , artigo, destinatario, observacoes, "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ral, "", "", "");
        }
        private void cbFamilia_SelectedIndexChanged(object sender, EventArgs e)
        {
            BD Connect = new BD();
            List<string> procura = new List<string>();
            cbMaterial.Items.Clear();
            if (cbFamilia.Text.ToUpper().Contains("CH PERFILADA"))
            {
                Connect.ligarbdprepararacao();
                procura = Connect.Procurarbdprepararacao("SELECT [valores]  FROM [ArtigoTekla].[dbo].[Armazem_Classes] where familia='galvanizada' or familia='Magnelis' and parametro ='classes'");
                Connect.desligarbdprepararacao();
                foreach (var item in procura)
                {
                    if (!cbMaterial.Items.Contains(item))
                    {
                        cbMaterial.Items.Add(item);
                    }
                }
                cbMaterial.SelectedIndex = 0;
                tbRalpre.Visible = false;
                labelRalpre.Visible = false;
                labelface1.Visible = true;
                labelface2.Visible = true;
                tbFace1.Visible = true;
                tbFace2.Visible = true;
            }
            else
            {
                Connect.ligarbdprepararacao();
                procura = Connect.Procurarbdprepararacao("SELECT [valores]  FROM [ArtigoTekla].[dbo].[Armazem_Classes] where familia='galvanizada' or familia='ALUZINC' and parametro ='classes'");
                Connect.desligarbdprepararacao();
                foreach (var item in procura)
                {
                    if (!cbMaterial.Items.Contains(item))
                    {
                        cbMaterial.Items.Add(item);
                    }
                }
                cbMaterial.SelectedIndex = 0;
                tbRalpre.Visible = true;
                labelRalpre.Visible = true;
                labelface1.Visible = false;
                labelface2.Visible = false;
                tbFace1.Visible = false;
                tbFace2.Visible = false;
            }
            cbPerfil.Items.Clear();
            procura.Clear();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT [Perfil]  FROM [ArtigoTekla].[dbo].[Perfilagem3] where Dest='cp' and familia ='" + cbFamilia.Text + "'");
            Connect.desligarbdprepararacao();
            foreach (var item in procura)
            {
                if (!cbPerfil.Items.Contains(item))
                {
                    cbPerfil.Items.Add(item);
                }
            }
            cbPerfil.SelectedIndex = 0;
            cbcertificado.SelectedIndex = 0;
        }
        private void cbPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            BD d = new BD();
            List<string> l = new List<string>();
            d.ligarbdprepararacao();
            Pesoliquido.Text = d.Procurarbdprepararacao("SELECT [Peso] FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + cbPerfil.Text + "'")[0];
            d.desligarbdprepararacao();
        }
        private void tbQtd_KeyPress(object sender, KeyPressEventArgs e)
        {
            int r = 0;

            if (int.TryParse(e.KeyChar.ToString(), out r))
            {

            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {

            }
            else
            {
                e.Handled = true;
            }
        }
        private void tbQtd_MouseClick(object sender, MouseEventArgs e)
        {
            ((Guna.UI2.WinForms.Guna2TextBox)sender).SelectAll();
        }
        private void tbQtd_Validating(object sender, CancelEventArgs e)
        {
           if (sender is Guna.UI2.WinForms.Guna2TextBox gunaTxt)
            {
                if (gunaTxt.Text.Trim() == "")
                    gunaTxt.Text = "1";
            }
        }
        private void tbComp_Validated(object sender, EventArgs e)
        {
           if (sender is Guna.UI2.WinForms.Guna2TextBox gunaTxt)
            {
                if (gunaTxt.Text.Trim() == "")
                    gunaTxt.Text = "0";
            }
        }      
       
    }
}
