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
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            this.Size = new Size(1430, 990);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
        }

        bool menuExpandido = false;
        private void Buttonabrirpedido_Click(object sender, EventArgs e)
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

            PanelMenuPedido.Width = 170;

            menuExpandido = !menuExpandido;
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
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            menuExpandido = !menuExpandido;
            AbrirFormNoPainel(new CM());
        }

        private void ButtonCP_Click(object sender, EventArgs e)
        {
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            menuExpandido = !menuExpandido;
            AbrirFormNoPainel(new CP());
        }     

        private void ButtonCQ_Click(object sender, EventArgs e)
        {
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            menuExpandido = !menuExpandido;
            AbrirFormNoPainel(new CQ());
        }

        private void ButtonCL_Click(object sender, EventArgs e)
        {
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            menuExpandido = !menuExpandido;
            AbrirFormNoPainel(new CL());
        }

        private void ButtonDAP_Click(object sender, EventArgs e)
        {
            Panelmenu.Height = 100;
            PanelMenuPedido.Height = 50;
            menuExpandido = !menuExpandido;
            AbrirFormNoPainel(new DAP());
        }



    }
}
