using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.MonthCalendar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Gestor_de_Fases
{
    public partial class DAP: Form
    {
        Form1 F;

        public DAP(Form1 formInicio)
        {
            InitializeComponent();
            F = formInicio;
        }
        private void DAP_Load(object sender, EventArgs e)
        {
            SearchArtigo();
            cbEsppExtInt.Validating += SetDefaultIfEmpty;
            tbpeso.Validated += SetDefaultIfEmpty;
            tbArea.Validating += SetDefaultIfEmpty;
            tbComp.Validated += SetDefaultIfEmpty;
            tbQtd.Validated += SetDefaultIfEmpty;
            cbcertificado.SelectedIndex = 1;
            tbComp.KeyPress += KeyPress;
        }
        private void SearchArtigo()
        {
            List<string> procura = new List<string>();
            BD Connect = new BD();
            Connect.ligarbd();
            procura = Connect.Procurarbd("SELECT [Descricao] FROM [PRIOFELIZ].[dbo].[MT_ViewArtigoUnidades]WHERE ARTIGO LIKE 'MPC%'");
            Connect.desligarbd();
            cbTipoPerfil.Items.Clear();
            foreach (var item in procura)
            {
                if (!cbTipoPerfil.Items.Contains(item))
                {
                    cbTipoPerfil.Items.Add(item);
                }
            }
            procura.Clear();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT Familia FROM [ArtigoTekla].[dbo].[Armazem_Classes] WHERE parametro = 'classes'");
            Connect.desligarbdprepararacao();
            cbTipoMaterial.Items.Clear();
            foreach (var item in procura)
            {
                if (!cbTipoMaterial.Items.Contains(item))
                {
                    cbTipoMaterial.Items.Add(item);
                }
            }
        }
        private void cbTipoPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> procura1 = new List<string>();
            List<string> procura = new List<string>();
            BD Connect = new BD();
            Connect.ligarbd();
            procura1 = Connect.Procurarbd("SELECT [Artigo] FROM [PRIOFELIZ].[dbo].[MT_ViewArtigoUnidades]WHERE ARTIGO LIKE 'MPC%' AND [Descricao]='" + cbTipoPerfil.Text + "'");
            Connect.desligarbd();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT PERFIL FROM [ArtigoTekla].[dbo].[Perfilagem3] WHERE ARTIGO ='" + procura1.First() + "' AND DEST='DAP'");
            Connect.desligarbdprepararacao();
            cbPerfil.Items.Clear();
            foreach (var item in procura)
            {
                if (!cbPerfil.Items.Contains(item))
                {
                    cbPerfil.Items.Add(item);
                }
            }
            procura.Clear();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT REQUISITO FROM [ArtigoTekla].[dbo].[Requisitos] WHERE FAMILIA ='" + cbTipoPerfil.Text + "'");
            Connect.desligarbdprepararacao();
            cbReqEs.Items.Clear();
            foreach (var item in procura)
            {
                if (!cbReqEs.Items.Contains(item))
                {
                    cbReqEs.Items.Add(item);
                }
            }
            procura.Clear();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT [Material]FROM [ArtigoTekla].[dbo].[MateriaisSuplementares]");
            Connect.desligarbdprepararacao();
            cbMaterial2.Items.Clear();
            foreach (var item in procura)
            {
                if (!cbMaterial2.Items.Contains(item))
                {
                    cbMaterial2.Items.Add(item);
                }
            }
            if (cbTipoPerfil.Text.Trim().ToUpper().Contains("PAINEL") || cbTipoPerfil.Text.Trim().ToUpper().Contains("CHAPA"))
            {
                labelEspExtInt.Text = "Espessura ext|int";
                cbMaterial2.Visible = true;
                labelRalExt.Visible = true;
                tbRalext.Visible = true;
                labelRalInt.Visible = true;
                tbRalint.Visible = true;
            }
            else
            {
                labelEspExtInt.Text = "Espessura";
                cbMaterial2.Visible = false;
                cbMaterial2.Text = "";
                labelRalExt.Visible = false;
                tbRalext.Visible = false;
                tbRalext.Text = "";
                labelRalInt.Visible = false;
                tbRalint.Visible = false;
                tbRalint.Text = "";
            }
        }
        private void cbPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> procura = new List<string>();
            BD Connect = new BD();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT peso FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + cbPerfil.Text + "'");
            Connect.desligarbdprepararacao();
            label4.Text = procura[0].ToString();

            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT espessura FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + cbPerfil.Text + "'");
            Connect.desligarbdprepararacao();
            cbEsppExtInt.Items.Clear();
            foreach (var item in procura)
            {
                if (!cbEsppExtInt.Items.Contains(item))
                {
                    cbEsppExtInt.Items.Add(item);
                }
            }
            try
            {
                cbEsppExtInt.SelectedIndex = 0;
            }
            catch (Exception)
            {

            }

        }
        private void cbTipoMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> procura = new List<string>();
            BD Connect = new BD();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT valores FROM [ArtigoTekla].[dbo].[Armazem_Classes] where parametro = 'classes' and FAMILIA ='" + cbTipoMaterial.Text + "'");
            Connect.desligarbdprepararacao();
            cbMaterial.Items.Clear();
            foreach (var item in procura)
            {
                if (!cbMaterial.Items.Contains(item))
                {
                    cbMaterial.Items.Add(item);
                }
            }
        }
        private void cbEsppExtInt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> procura = new List<string>();
            BD Connect = new BD();
            Connect.ligarbdprepararacao();
            procura = Connect.Procurarbdprepararacao("SELECT peso FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + cbPerfil.Text + "'and espessura = '" + cbEsppExtInt.Text.Trim() + "'");
            Connect.desligarbdprepararacao();
            labelKmporM.Text = procura[0].ToString();
        }
        private void SetDefaultIfEmpty(object sender, EventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox tb)
            {
                if (string.IsNullOrWhiteSpace(tb.Text))
                {
                    switch (tb.Name)
                    {
                        case "cbTipoMaterial":
                            tb.Text = "1"; 
                            break;
                        default:
                            tb.Text = "0"; 
                            break;
                    }
                }
            }
            else if (sender is Guna.UI2.WinForms.Guna2ComboBox cb)
            {
                if (string.IsNullOrWhiteSpace(cb.Text))
                {
                    cb.Text = "0";
                }
            }
        }
        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            int r = 0;
            if (int.TryParse(e.KeyChar.ToString(), out r))
            {    }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {   }
            else if (e.KeyChar == Convert.ToChar(46))
            {    }
            else if (e.KeyChar == Convert.ToChar(","))
            {
                e.KeyChar = Convert.ToChar(".");
            }
            else
            {
                e.Handled = true;
            }
        }
        int linhaCONJ = 1;
        private void ButtonAddPerfilDap_Click(object sender, EventArgs e)
        {
            string Tipoperfil = cbTipoPerfil.Text.Trim();
            string Perfil = cbPerfil.Text.Trim();
            string Tipomaterial = cbTipoMaterial.Text.Trim();
            string Material = cbMaterial.Text.Trim();
            string Material2 = cbMaterial2.Text.Trim();
            string Pesotb = tbpeso.Text.Trim();
            string Areatb = tbArea.Text.Trim();
            string Comprimento = tbComp.Text.Trim().Replace('.', ',');
            string Quantidade = tbQtd.Text.Trim().Replace('.', ',');
            string ExpInteExt = cbEsppExtInt.Text.Trim();
            string RalExt = tbRalext.Text.Trim();
            string RalInt = tbRalint.Text.Trim();
            string Reqespeciais = cbReqEs.Text.Trim();
            string Certificado = cbcertificado.Text.Trim();
            string Observacoes = tbObs.Text.Trim();
            if (Perfil == "")
            {
                MessageBox.Show("O perfil não pode estar em branco ", "erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (Tipomaterial == "" || double.Parse(Tipomaterial) == 0)
            {
                MessageBox.Show("A quantidade não pode ser 0 ou branco ", "erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                List<string> procura = new List<string>();
                List<string> l1 = new List<string>();
                BD Connect = new BD();
                procura.Clear();
                Connect.ligarbdprepararacao();
                procura = Connect.Procurarbdprepararacao("SELECT [Peso], [LarguraUtil] FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + Perfil + "' and espessura ='" + ExpInteExt + "'");
                Connect.desligarbdprepararacao();
                for (int i = procura.Count; i < 2; i++)
                { procura.Add("");  }

                if ((procura[0].ToString() == "" && Areatb != "0" && Pesotb != "0") || (procura[0].ToString() != "" && Areatb == "0" && Pesotb == "0"))
                {
                    double peso = 0;
                    double area = 0;
                    if (Pesotb == "0")
                    {
                        peso = Convert.ToDouble(procura[0].ToString()) * Convert.ToDouble(Comprimento) * Convert.ToDouble(Quantidade) / 1000;
                    }
                    else
                    {
                        peso = double.Parse(Pesotb.Replace(".", ","));
                    }

                    if (Areatb == "0")
                    {
                        area = Convert.ToDouble(procura[1].ToString()) * Convert.ToDouble(Comprimento) * Convert.ToDouble(Quantidade) / 1000000;
                    }
                    else
                    {
                        area = double.Parse(Areatb.Replace(".", ","));
                    }

                    string artigo = null;
                    string destinatario = null;
                    string cor = null;
                    string cor1 = null;
                    string cor2 = null;
                    string marca = null;
                    string MATERIAL = null;
                    string fase750 = F.labelfase750.Text.Trim();
                    string noObra = F.labelNObra.Text.Trim();
                    string ordemFabrico = "2." + noObra + "." + fase750 + "." + F.DataGridViewOrder.Rows.Count;
                    string conjuntoPeca = "2." + noObra + "." + fase750 + "." + fase750 + "CJ" + linhaCONJ;
                    string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
                    procura.Clear();
                    Connect.ligarbdprepararacao();
                    procura = Connect.Procurarbdprepararacao("SELECT artigo, dest, marca FROM [ArtigoTekla].[dbo].[Perfilagem3] where perfil ='" + Perfil + "' and espessura ='" + ExpInteExt + "'");
                    Connect.desligarbdprepararacao();
                    Connect.ligarbd();
                    l1 = Connect.Procurarbd("SELECT [Artigo] FROM [PRIOFELIZ].[dbo].[MT_ViewArtigoUnidades]WHERE ARTIGO LIKE 'MPC%' AND [Descricao]='" + Tipoperfil + "'");
                    Connect.desligarbd();
                    try
                    {
                        marca = procura[2].ToString();
                    }
                    catch (Exception)
                    {     }
                    try
                    {
                        artigo = l1[0].ToString();
                    }
                    catch (Exception)
                    {      }
                    try
                    {
                        destinatario = procura[1].ToString();
                    }
                    catch (Exception)
                    {     }

                    try
                    {
                        if (RalExt != "")
                        {
                            cor1 = ExpInteExt.Split('_')[0].Trim() + "EXT" + RalExt + "|";
                        }

                    }
                    catch (Exception)
                    {
                        cor1 = ExpInteExt + "EXT" + RalExt + "|";
                    }
                    try
                    {
                        if (RalInt != "")
                        {
                            cor2 = ExpInteExt.Split('_')[1].Trim() + "INT" + RalInt;
                        }

                    }
                    catch (Exception)
                    {

                        cor2 = ExpInteExt + "INT" + RalInt;
                    }


                    if (Tipoperfil.ToUpper().Contains("PAINEL"))
                    {
                        MATERIAL = Material2 + " - " + Material;

                        cor = cor1 + cor2;

                    }
                    else if (Tipoperfil.ToUpper().Contains("CHAPA"))
                    {
                        cor = "EXT" + RalExt + "|" + "INT" + RalInt;
                        MATERIAL = Material;
                    }
                    else
                    {
                        MATERIAL = Material;
                    }

                    if (string.IsNullOrEmpty(destinatario))
                    {
                        destinatario = "DAP";
                    }

                    linhaCONJ += 1;
                    F.DataGridViewOrder.Rows.Add(fase750, fase750, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, MATERIAL, Reqespeciais, Certificado, Comprimento, peso.ToString("0.##"), area, "", "", dataFormatada, artigo, destinatario, Observacoes, "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", cor, marca, "", "");
                }
                else
                {
                    MessageBox.Show("Erro a procurar o peso na base de dados." + Environment.NewLine + "Por favor defina um peso e área diferente de 0" + Environment.NewLine + "No caso de painel verique se o \"Perfil\" \"Espessura ext|int\" está preenchido.", "", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }
                      

    }
}
