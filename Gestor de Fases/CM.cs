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
    public partial class CM: Form
    {
        public CM()
        {
            InitializeComponent();
        }

        private void CM_Load(object sender, EventArgs e)
        {

        }

        private void CloseAll()
        {
            CloseConjBolt();
            VaraoeBuchasClose();
            CloseConjAcss();            
        }

        private void cbConjunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbAncoragem.SelectedIndex = -1;
            cbConector.SelectedIndex = -1;
            CloseAll();
            OpenConjBolt();           
        }

        private void cbAncoragem_SelectedIndexChanged(object sender, EventArgs e)
        {            
            CloseAll();           

            if (cbAncoragem.Text.Contains("Bucha Metálica"))
            {
                ConfigurarPosicoesVaraoeBucha();
                ButtonAddAncor.Text = "Adicionar Bucha Metálica";
                VaraoeBuchasOpen();                
            }
            else if (cbAncoragem.Text.Contains("Bucha Quimica"))
            {
                ConfigurarPosicoesVaraoeBucha();
                ButtonAddAncor.Text = "Adicionar Bucha Quimica";
                VaraoeBuchasOpen();                
            }
            else if (cbAncoragem.Text.Contains("Varão Roscado"))
            {
                ConfigurarPosicoesVaraoeBucha();
                ButtonAddAncor.Text = "Adicionar Varão Roscado";
                VaraoeBuchasOpen();                
            }
            else if (cbAncoragem.Text.Contains("Varão Nervorado"))
            {
                ConfigurarPosicoesVaraoeBucha();
                ButtonAddAncor.Text = "Adicionar Varão Nervorado";
                VaraoeBuchasOpen();              
            }
            else
            {
                CloseAll();               
            }
        }

        private void cbConector_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbConjunto.SelectedIndex = -1;
            cbAncoragem.SelectedIndex = -1;
            CloseAll();            
        }

        private void buttonAcess_Click(object sender, EventArgs e)
        {
            cbConjunto.SelectedIndex = -1;
            cbAncoragem.SelectedIndex = -1;
            cbConector.SelectedIndex = -1;
            CloseAll();
            OpenConjAcss();           
        }
      
        private void OpenConjBolt() => SetConjBoltVisibility(true);

        private void CloseConjBolt() => SetConjBoltVisibility(false);

        private void OpenConjAcss() => SetConjAcssVisibility(true);

        private void CloseConjAcss() => SetConjAcssVisibility(false);

        private void ConfigurarPosicoesConjBolt()
        {
            labelDiamBolt.Location = new Point(25, 145);
            labelCompBolt.Location = new Point(125, 145);
            labelqdBolt.Location = new Point(275, 145);
            labelclasseBolt.Location = new Point(375, 145);
            labelnormalBolt.Location = new Point(585, 145);
            labelmarcaBolt.Location = new Point(805, 145);
            labelobsBolt.Location = new Point(910, 145);

            cbDiametroBolt.Location = new Point(10, 175);
            cbComprimentoBolt.Location = new Point(115, 175);
            tbqtdBolt.Location = new Point(250, 175);
            cbClasseBolt.Location = new Point(335, 175);
            cbNormaBolt.Location = new Point(480, 175);
            tbMarcaBolt.Location = new Point(760, 175);
            obsBolt.Location = new Point(910, 175);

            cbDiametroNut.Location = new Point(10, 225);
            tbqtdNut.Location = new Point(250, 225);
            cbClasseNut.Location = new Point(335, 225);
            cbNormaNut.Location = new Point(480, 225);
            tbMarcaNut.Location = new Point(760, 225);
            obsNut.Location = new Point(910, 225);

            cbDiametroWasher.Location = new Point(10, 275);
            tbqtdWasher.Location = new Point(250, 275);
            cbClasseWasher.Location = new Point(335, 275);
            cbNormaWasher.Location = new Point(480, 275);
            tbMarcaWasher.Location = new Point(760, 275);
            obsWasher.Location = new Point(910, 275);

            ButtonAddBolt.Location = new Point(960, 170);
            ButtonAddNut.Location = new Point(960, 225);
            ButtonAddWasher.Location = new Point(960, 275);
            ButtonAddConj.Location = new Point(1135, 170);
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
            cbDiametroBolt.Visible = visible;
            cbComprimentoBolt.Visible = visible;
            tbqtdBolt.Visible = visible;
            cbClasseBolt.Visible = visible;
            cbNormaBolt.Visible = visible;
            tbMarcaBolt.Visible = visible;
            obsBolt.Visible = visible;

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
            labelDiametroAncor.Location = new Point(35, 190);
            labelCompAncor.Location = new Point(135, 190);
            labelQtdAncor.Location = new Point(285, 190);
            labelClasseAncor.Location = new Point(385, 190);
            labelNormaAncor.Location = new Point(595, 190);
            labelReqEspncor.Location = new Point(805, 190);
            labelMarcaAncor.Location = new Point(990, 190);
            labelObsAncor.Location = new Point(1085, 190);

            cbDiametroAncor.Location = new Point(20, 220);
            cbCompAncor.Location = new Point(125, 220);
            tbqtdAncor.Location = new Point(260, 220);
            cbClasseAncor.Location = new Point(345, 220);
            cbNormaAncor.Location = new Point(490, 220);
            tbReqespAncor.Location = new Point(770, 220);
            tbMarcaAncor.Location = new Point(940, 220);
            obsAcor.Location = new Point(1085, 220);

            ButtonAddAncor.Location = new Point(1130, 220);
        }      

        private void SetAncoragemVisibility(bool mostrar)
        {
            labelDiametroAncor.Visible = mostrar;
            labelCompAncor.Visible = mostrar;
            labelQtdAncor.Visible = mostrar;
            labelClasseAncor.Visible = mostrar;
            labelNormaAncor.Visible = mostrar;
            labelReqEspncor.Visible = mostrar;
            labelMarcaAncor.Visible = mostrar;
            labelObsAncor.Visible = mostrar;

            cbDiametroAncor.Visible = mostrar;
            cbCompAncor.Visible = mostrar;
            tbqtdAncor.Visible = mostrar;
            cbClasseAncor.Visible = mostrar;
            cbNormaAncor.Visible = mostrar;
            tbReqespAncor.Visible = mostrar;
            tbMarcaAncor.Visible = mostrar;
            obsAcor.Visible = mostrar;

            ButtonAddAncor.Visible = mostrar;           
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
            tbCompAcss.Location = new Point(545, 220);
            cbCertificadoAcss.Location = new Point(690, 220);
            tbReqEAcss.Location = new Point(785, 220);
            tbMarcalAcss.Location = new Point(970, 220);

            ButtonAddAcss.Location = new Point(1115, 220);
        }

        private void cbAncoragem_Click(object sender, EventArgs e)
        {
            cbConector.SelectedIndex = -1;
            cbConjunto.SelectedIndex = -1;
        }

        private void ButtonAddAcss_Click(object sender, EventArgs e)
        {
            string um = tbPerfilAcss.Text;
            string dois = tbNormalAcss.Text;

            MessageBox.Show($"Acessório adicionado com sucesso {um} {dois}!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
