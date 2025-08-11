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

        private void cbConjunto_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenConjBolt();

        }

        private void cbAncoragem_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseConjBolt();

            if (cbAncoragem.Text.Contains("Bucha Metálica") || cbAncoragem.Text.Contains("Bucha Quimica")) 
            {
                ConfigurarPosicoesBucha();
                BuchasOpen();
            }
            else if (cbAncoragem.Text.Contains("Varão Roscado") || cbAncoragem.Text.Contains("Varão Nervorado"))
            {
                ConfigurarPosicoesVarao();
                VaraoOpen();
            }
            else
            {
                BuchasClose();
                VaraoClose();
            }
        }

        private void cbConector_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseConjBolt();
        }

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

            labelDiamNut.Location = new Point(25, 220);
            labelqdNut.Location = new Point(275, 220);
            labelclasseNut.Location = new Point(375, 220);
            labelnormalNut.Location = new Point(585, 220);
            labelmarcaNut.Location = new Point(805, 220);
            labelobsNut.Location = new Point(910, 220);

            cbDiametroNut.Location = new Point(10, 250);
            tbqtdNut.Location = new Point(250, 250);
            cbClasseNut.Location = new Point(335, 250);
            cbNormaNut.Location = new Point(480, 250);
            tbMarcaNut.Location = new Point(760, 250);
            obsNut.Location = new Point(910, 250);

            labelDiamWasher.Location = new Point(25, 305);
            labelqdWasher.Location = new Point(275, 305);
            labelclasseWasher.Location = new Point(375, 305);
            labelnormalWasher.Location = new Point(585, 305);
            labelmarcaWasher.Location = new Point(805, 305);
            labelobsWasher.Location = new Point(910, 305);

            cbDiametroWasher.Location = new Point(10, 335);
            tbqtdWasher.Location = new Point(250, 335);
            cbClasseWasher.Location = new Point(335, 335);
            cbNormaWasher.Location = new Point(480, 335);
            tbMarcaWasher.Location = new Point(760, 335);
            obsWasher.Location = new Point(910, 335);

            ButtonAddBolt.Location = new Point(960, 170);
            ButtonAddNut.Location = new Point(960, 250);
            ButtonAddWasher.Location = new Point(960, 330);
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

            labelDiamNut.Visible = visible;
            labelqdNut.Visible = visible;
            labelclasseNut.Visible = visible;
            labelnormalNut.Visible = visible;
            labelmarcaNut.Visible = visible;
            labelobsNut.Visible = visible;
            cbDiametroNut.Visible = visible;
            tbqtdNut.Visible = visible;
            cbClasseNut.Visible = visible;
            cbNormaNut.Visible = visible;
            tbMarcaNut.Visible = visible;
            obsNut.Visible = visible;

            labelDiamWasher.Visible = visible;
            labelqdWasher.Visible = visible;
            labelclasseWasher.Visible = visible;
            labelnormalWasher.Visible = visible;
            labelmarcaWasher.Visible = visible;
            labelobsWasher.Visible = visible;
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

        private void OpenConjBolt() => SetConjBoltVisibility(true);

        private void CloseConjBolt() => SetConjBoltVisibility(false);

        private void ConfigurarPosicoesVarao()
        {
            labelDiametroAncor.Location = new Point(35, 230);
            labelCompAncor.Location = new Point(135, 230);
            labelQtdAncor.Location = new Point(285, 230);
            labelClasseAncor.Location = new Point(385, 230);
            labelNormaAncor.Location = new Point(595, 230);
            labelReqEspncor.Location = new Point(805, 230);
            labelMarcaAncor.Location = new Point(990, 230);
            labelObsAncor.Location = new Point(1085, 230);

            cbDiametroAncor.Location = new Point(20, 260);
            cbCompAncor.Location = new Point(125, 260);
            tbqtdAncor.Location = new Point(260, 260);
            cbClasseAncor.Location = new Point(345, 260);
            cbNormaAncor.Location = new Point(490, 260);
            tbReqespAncor.Location = new Point(770, 260);
            tbMarcaAncor.Location = new Point(940, 260);
            obsAcor.Location = new Point(1085, 260);

            ButtonAddAncor.Location = new Point(1130, 260);
        }

        private void ConfigurarPosicoesBucha()
        {
            labelDiametroAncor.Location = new Point(35, 230);
            labelQtdAncor.Location = new Point(150, 230);
            labelClasseAncor.Location = new Point(250, 230);
            labelNormaAncor.Location = new Point(460, 230);
            labelReqEspncor.Location = new Point(670, 230);
            labelMarcaAncor.Location = new Point(855, 230);
            labelObsAncor.Location = new Point(950, 230);

            cbDiametroAncor.Location = new Point(20, 260);
            tbqtdAncor.Location = new Point(125, 260);
            cbClasseAncor.Location = new Point(210, 260);
            cbNormaAncor.Location = new Point(355, 260);
            tbReqespAncor.Location = new Point(635, 260);
            tbMarcaAncor.Location = new Point(805, 260);
            obsAcor.Location = new Point(950, 260);

            ButtonAddAncor.Location = new Point(995, 260);
        }

        private void SetAncoragemVisibility(bool mostrar, bool mostrarCompr = false)
        {
            labelDiametroAncor.Visible = mostrar;
            labelQtdAncor.Visible = mostrar;
            labelClasseAncor.Visible = mostrar;
            labelNormaAncor.Visible = mostrar;
            labelReqEspncor.Visible = mostrar;
            labelMarcaAncor.Visible = mostrar;
            labelObsAncor.Visible = mostrar;

            cbDiametroAncor.Visible = mostrar;
            tbqtdAncor.Visible = mostrar;
            cbClasseAncor.Visible = mostrar;
            cbNormaAncor.Visible = mostrar;
            tbReqespAncor.Visible = mostrar;
            tbMarcaAncor.Visible = mostrar;
            obsAcor.Visible = mostrar;

            ButtonAddAncor.Visible = mostrar;

            labelCompAncor.Visible = mostrar && mostrarCompr;
            cbCompAncor.Visible = mostrar && mostrarCompr;
        }

        private void BuchasOpen() => SetAncoragemVisibility(true, false);

        private void BuchasClose() => SetAncoragemVisibility(false);

        private void VaraoOpen() => SetAncoragemVisibility(true, true);

        private void VaraoClose() => SetAncoragemVisibility(false);

    }
}
