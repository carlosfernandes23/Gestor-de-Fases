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
    public partial class CQ : Form
    {
        Form1 F;

        public CQ(Form1 formInicio)
        {
            InitializeComponent();
            F = formInicio;
        }

        private void CQ_Load(object sender, EventArgs e)
        {
            Datematerialemobra.Value = DateTime.Now;
            cbOperacao.SelectedIndex = 1;
            cbFamiliaMaterial.SelectedIndex = 7;

            var keyPressControls = new Control[] { tbEspessura, tbComp, tbLargura, tbQtd };
            foreach (var ctl in keyPressControls)
                ctl.KeyPress += KeyPress;

            var validating1Controls = new Control[] { tbEspessura, tbLargura, tbComp };
            foreach (var ctl in validating1Controls)
                ctl.Validating += Validating1;

            tbQtd.Validating += Validating0;

            var mouseClickControls = new Control[] { tbLargura, tbComp, tbQtd };
            foreach (var ctl in mouseClickControls)
                ctl.MouseClick += MouseClick;

        }

        int linhaCONJ = 1;
        private void ButtonAddPerfilCP_Click(object sender, EventArgs e)
        {
            string comprimento = tbComp.Text.Trim().Replace('.', ',');
            string largura = tbLargura.Text.Trim().Replace('.', ',');
            string Quantidade = tbQtd.Text.Trim().Replace('.', ',');
            string Espessura = tbEspessura.Text.Trim().Replace('.', ',');
            string pesoLiquido = Pesoliquido.Text.Replace('.', ',');
            string quantidade = tbQtd.Text.Trim().Replace('.', ',');
            double peso = double.Parse(pesoLiquido) * double.Parse(comprimento) / 1000 * double.Parse(largura) / 1000 * double.Parse(Espessura) * double.Parse(quantidade);
            double area = (double.Parse(comprimento) / 1000) * (double.Parse(largura) / 1000 * double.Parse(quantidade) * 2);

            if (comprimento == "0" || largura == "0")
            {
                F.labelestado.Text = "O campo comprimento e largura são de preenchimento obrigatório.\nPor favor, coloque as medidas o mais próximo da realidade possível.";
                return;
            }

            string familiaMaterial = cbFamiliaMaterial.Visible
                            ? cbFamiliaMaterial.Text.Trim()
                            : TextBoxfamilia.Text.Trim();

            string certificado = cbcertificado.Text.Trim();
            string operacao = cbOperacao.Text.Trim();
            string fase750 = F.labelfase750.Text.Trim();
            string noObra = F.labelNObra.Text.Trim();

            string Material = cbMaterial.Visible
                           ? cbMaterial.Text.Trim()
                           : TextBoxmaterial.Text.Trim();
            string dataFormatada = Datematerialemobra.Value.ToString("dd/MM/yyyy");
            string Perfil = "CHA" + Espessura;
            string ral = tbRal.Text.Trim();
            string ordemFabrico = "2." + noObra + "." + fase750 + "." + F.DataGridViewOrder.Rows.Count;
            string conjuntoPeca = "2." + noObra + "." + fase750 + "." + fase750 + "CJ" + linhaCONJ;

            string artigo = null;
            string destinatario = "CQ";
            string unidade = " (kg)";

            bool pintada = true;
            if (familiaMaterial.ToUpper().Contains("PINTADA") && ral == "")
            {
                pintada = false;
            }
            if (pintada)
            {
                bool pass = true;                
                if (double.Parse(Espessura.Replace('.', ',')) <= 1)
                {
                    unidade = " (m2)";
                }
                BD Connect = new BD();
                Connect.ligarbd();
                if (familiaMaterial.ToLower() == "xadrez" || familiaMaterial.ToLower() == "gota")
                {
                    familiaMaterial = "LAQ";
                }

                try
                {
                    artigo = Connect.Procurarbd("SELECT Artigo FROM [PRIOFELIZ].[dbo].[MT_ViewArtigoUnidades] where Familia ='CM' AND SubFamilia='CQ' and Descricao = '" + familiaMaterial + " " + operacao + unidade + "'")[0];
                }
                catch (Exception)
                {
                    try
                    {
                        artigo = Connect.Procurarbd("SELECT Artigo FROM [PRIOFELIZ].[dbo].[MT_ViewArtigoUnidades] where Familia ='CM' AND SubFamilia='CQ' and Descricao = '" + familiaMaterial + " " + operacao + " (kg)'")[0];
                    }
                    catch (Exception)
                    {
                        try
                        {
                            artigo = Connect.Procurarbd("SELECT Artigo FROM [PRIOFELIZ].[dbo].[MT_ViewArtigoUnidades] where Familia ='CM' AND SubFamilia='CQ' and Descricao like '%" + familiaMaterial + " " + operacao + "%'")[0];
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("sem artigo");
                            pass = false;
                        }
                    }
                }
                Connect.desligarbd();

                if (pass)
                {
                    linhaCONJ += 1;
                    F.DataGridViewOrder.Rows.Add(fase750, fase750, "Chapa", ordemFabrico, conjuntoPeca, 0, "", "", Quantidade, Perfil, Material, "", certificado, comprimento, peso.ToString("0.##"), area.ToString("0.##"), "", "", dataFormatada, artigo, destinatario, "", "Opção 8", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", ral, "", "", "");
                }

            }
            else
            {
                F.labelestado.Text = "Falta o ral para a chapa pré-pintada";
            }
        }
        private void cbFamiliaMaterial_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Familiamaterial = cbFamiliaMaterial.Text.Trim();
            if (Familiamaterial == "Aluminio")
            {
                Pesoliquido.Text = "2.697";
            }
            else if (Familiamaterial == "Inox")
            {
                Pesoliquido.Text = "8";
            }
            else if (Familiamaterial == "Cobre")
            {
                Pesoliquido.Text = "8.89";
            }
            else
            {
                Pesoliquido.Text = "7.85";
            }
            List<string> procura = new List<string>();
            BD Connect = new BD();
            try
            {
                Connect.ligarbdprepararacao();
                procura = Connect.Procurarbdprepararacao("Select valores FROM [ArtigoTekla].[dbo].[Armazem_Classes] where [Parametro]='Classes' and [Familia] like '%" + cbFamiliaMaterial.Text + "%'");
                Connect.desligarbdprepararacao();

                cbMaterial.Items.Clear();
                foreach (string item in procura)
                {
                    if (!cbMaterial.Items.Contains(item.Trim()))
                    {
                        cbMaterial.Items.Add(item.Trim());
                    }

                }
            }
            catch (Exception)
            {    }
            procura.Clear();
            try
            {
                Connect.ligarbdprepararacao();
                cbMaterial.Text = Connect.Procurarbdprepararacao("Select valores FROM [ArtigoTekla].[dbo].[Armazem_Classes] where [Descricao] = 'Defeito' AND [Parametro]='Classes' and [Familia] like '%" + cbFamiliaMaterial.Text + "%'")[0];
                Connect.desligarbdprepararacao();
            }
            catch (Exception)
            {   }
        }
        private void KeyPress(object sender, KeyPressEventArgs e)
        {
            int r = 0;
            if (int.TryParse(e.KeyChar.ToString(), out r))
            {

            }
            else if (e.KeyChar == Convert.ToChar(Keys.Back))
            {

            }
            else if (e.KeyChar == Convert.ToChar(46))
            {

            }
            else if (e.KeyChar == Convert.ToChar(","))
            {
                e.KeyChar = Convert.ToChar(".");
            }
            else
            {
                e.Handled = true;
            }
        }
        private void MouseClick(object sender, MouseEventArgs e)
        {
            ((Guna.UI2.WinForms.Guna2TextBox)sender).SelectAll();
        }
        private void Validating1(object sender, CancelEventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox gunaTxt)
            {
                if (gunaTxt.Text.Trim() == "")
                    gunaTxt.Text = "1";
            }
        }
        private void Validating0(object sender, CancelEventArgs e)
        {
            if (sender is Guna.UI2.WinForms.Guna2TextBox gunaTxt)
            {
                if (gunaTxt.Text.Trim() == "")
                    gunaTxt.Text = "0";
            }
        }

        private void Buttoneditfamilia_Click(object sender, EventArgs e)
        {
            if (cbFamiliaMaterial.Visible)
            {
                cbFamiliaMaterial.Visible = false;
                TextBoxfamilia.Visible = true;
                cbFamiliaMaterial.SelectedIndex = -1;
            }
            else
            {
                cbFamiliaMaterial.Visible = true;
                TextBoxfamilia.Visible = false;
                cbFamiliaMaterial.SelectedIndex = 6;
            }
        }

        private void Buttoneditmaterial_Click(object sender, EventArgs e)
        {
            if (cbMaterial.Visible)
            {
                cbMaterial.Visible = false;
                TextBoxmaterial.Visible = true;
                cbFamiliaMaterial.SelectedIndex = -1;
            }
            else
            {
                cbMaterial.Visible = true;
                TextBoxmaterial.Visible = false;
            }
        }
    }
}
