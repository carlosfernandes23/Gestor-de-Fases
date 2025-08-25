using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Drawing.Imaging;
using System.Globalization;
using System.Web.UI.WebControls.WebParts;

namespace Gestor_de_Fases
{   

    public partial class labelnomeficheiro: Form
    {
        Form1 F;

        public labelnomeficheiro(Form1 formInicio)
        {
            InitializeComponent();
            F = formInicio;

            Print.DragDrop += new DragEventHandler(DragDrop);
            Print.DragEnter += new DragEventHandler(DragEnter);
            cbA4.Checked = true;
            cbA4.CheckedChanged += A4_CheckedChanged;
            cbA3.CheckedChanged += A3_CheckedChanged;
            lbnome.Click += Click;
        }

        private void labelnomeficheiro_Load(object sender, EventArgs e)
        {
            try
            {
                DataGridViewCell a = F.DataGridViewOrder.SelectedCells[0];
                labelNomeFile.Text = F.DataGridViewOrder.Rows[a.RowIndex].Cells[4].Value.ToString().Split('.')[1] + "." + F.DataGridViewOrder.Rows[a.RowIndex].Cells[4].Value.ToString().Split('.').Last();
            }
            catch (Exception) 
            { 
                MessageBox.Show("Tem de posicionar o cursor na célula com o nome que pretende atribuir o ficheiro."); 
                this.Close();
            }
            Listbox();
        }
        private void Listbox()
        {
            lbnome.Items.Clear();
            foreach (DataGridViewRow row in F.DataGridViewOrder.Rows)
            {
                if (row.Cells[4].Value != null)
                {
                    string nomeCompleto = row.Cells[4].Value.ToString();
                    string nomeProcessado;

                    if (nomeCompleto.Contains("."))
                    {
                        string[] partes = nomeCompleto.Split('.');
                        nomeProcessado = partes[1] + "." + partes.Last();
                    }
                    else
                    {
                        nomeProcessado = nomeCompleto; 
                    }
                    lbnome.Items.Add(nomeProcessado);
                }
            }
            if (lbnome.Items.Count > 0)
            {
                lbnome.SelectedIndex = 0; 
            }
        }
        private void Click(object sender, EventArgs e)
        {
            if (lbnome.SelectedItem != null)
            {
                labelNomeFile.Text = lbnome.SelectedItem.ToString();
            }
        }
        protected void GerarPDF()
        {
            using (Stream inputPdfStream = new FileStream(@"A4.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new FileStream("C:\\r\\1.Jpeg", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(@"result.pdf", FileMode.Create, FileAccess.Write, FileShare.None))

            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                image.ScaleToFit(450f, 450f);
                image.SetAbsolutePosition(90, 350);

                pdfContentByte.AddImage(image);
                stamper.Close();
            }
        }
        private void InsertTextToPdfA4()
        {
            int a = 0;
            string Cliente = F.labelCliente.Text;
            string Designacao = F.labelDesignacao.Text;
            string userName = Environment.UserName;
            string NomeDesenho = labelNomeFile.Text;
            string Classeex = F.labelClasseEx.Text;
            string Grauprep = F.labelGrauprep.Text;
            string Obra = F.labelNObra.Text;
            string Data = DateTime.Now.ToShortDateString().Replace('/', '.');
            string Modelo = Obra + "-" + Cliente;
            string loteconjunto = NomeDesenho.Substring((Obra + ".").Length);
            string fase = F.DataGridViewOrder.Rows[0].Cells[0].Value?.ToString()?.Trim() ?? string.Empty;
            int indiceSelecionado = lbnome.SelectedIndex + 1;
            string Nomecoluna = "2." + Obra + "." + fase + "." + indiceSelecionado;
            string Quantidade = string.Empty; 
            string Perfil = string.Empty;
            string Comprimento = string.Empty;
            string Peso = string.Empty;
            string Material = string.Empty;
            string Revisão = string.Empty;
            string Objeto = tbnomeobjecto.Text;
            string Escala = "S/E";
            string Destinatario = string.Empty;
            string Artigo = string.Empty;
            foreach (DataGridViewRow row in F.DataGridViewOrder.Rows)
            {
                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() == Nomecoluna)
                {
                    Revisão = row.Cells[5].Value?.ToString() ?? string.Empty;
                    Quantidade = row.Cells[8].Value?.ToString() ?? string.Empty;
                    Perfil = row.Cells[9].Value?.ToString() ?? string.Empty;
                    Material = row.Cells[10].Value?.ToString() ?? string.Empty;
                    Comprimento = row.Cells[13].Value?.ToString() ?? string.Empty;
                    Peso = row.Cells[14].Value?.ToString() ?? string.Empty;
                    Artigo = row.Cells[19].Value?.ToString() ?? string.Empty;
                    Destinatario = row.Cells[20].Value?.ToString() ?? string.Empty;
                    break; 
                }
            }
            if (Cliente.Replace("Cliente -", "").Length > 56)
            {
                a = 56;
            }
            else
            {
                a = Cliente.Replace("Cliente -", "").Length;
            }
            int b = 0;
            if (Designacao.Replace("Designação -", "").Length > 56)
            {
                b = 56;
            }
            else
            {
                b = Designacao.Replace("Designação -", "").Length;
            }
            foreach (DataGridViewRow row in F.DataGridViewOrder.Rows)
            {
                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() == Nomecoluna)
                {
                    Revisão = row.Cells[5].Value?.ToString() ?? string.Empty;
                    Quantidade = row.Cells[8].Value?.ToString() ?? string.Empty;
                    Perfil = row.Cells[9].Value?.ToString() ?? string.Empty;
                    Material = row.Cells[10].Value?.ToString() ?? string.Empty;
                    Comprimento = row.Cells[13].Value?.ToString() ?? string.Empty;
                    Peso = row.Cells[14].Value?.ToString() ?? string.Empty;
                    Artigo = row.Cells[19].Value?.ToString() ?? string.Empty;
                    Destinatario = row.Cells[20].Value?.ToString() ?? string.Empty;
                    break;
                }
            }
            string sourceFileName = @"result.pdf";
            string newFileName = "C:\\r\\2." + labelNomeFile.Text + ".pdf";
            using (Stream pdfStream = new FileStream(sourceFileName, FileMode.Open))
            using (Stream newpdfStream = new FileStream(newFileName, FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, newpdfStream);
                PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);

                BaseFont fontNormal = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                BaseFont fontBold = BaseFont.CreateFont(@"C:\Windows\Fonts\arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                pdfContentByte.SetColorFill(BaseColor.BLACK);
                pdfContentByte.BeginText();

                pdfContentByte.SetFontAndSize(fontNormal, 7);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, loteconjunto, 76, 809, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Quantidade, 135, 809, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Quantidade, 135, 799, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 8);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, loteconjunto, 345, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Quantidade, 375, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Perfil, 410, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Comprimento, 455, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Peso, 495, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Material, 540, 802, 0);

                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Cliente.Replace("Cliente -", "").ToUpper().Substring(0, a), 305, 65, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Designacao.Replace("Designação -", "").ToUpper().Substring(0, b), 305, 50, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 10);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Objeto, 305, 90, 0);

                pdfContentByte.SetFontAndSize(fontBold, 8);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, NomeDesenho, 175, 95, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 6);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, userName, 180, 75, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Data, 250, 75, 0);

                pdfContentByte.SetFontAndSize(fontBold, 10);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Classeex, 88, 50, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Grauprep, 85, 20, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Escala, 268, 90, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 7);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Modelo, 330, 6, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Data, 580, 6, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 8);
                pdfContentByte.SetFontAndSize(BaseFont.CreateFont("3OF9_NEW_0.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED), 20);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "*" + NomeDesenho.Split('.')[0] + "." + NomeDesenho.Split('.')[1] + "*", 305, 20, 0);
                
                pdfContentByte.SetColorFill(BaseColor.GRAY);
                pdfContentByte.SetFontAndSize(fontNormal, 11);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Destinatario, 592, 210, 90);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Artigo, 592, 340, 90);

                pdfContentByte.EndText();
                pdfStamper.Close();
            }

        }
        protected void GerarPDFA3()
        {
            using (Stream inputPdfStream = new FileStream(@"A3.pdf", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new FileStream("C:\\r\\1.Jpeg", FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream outputPdfStream = new FileStream(@"result.pdf", FileMode.Create, FileAccess.Write, FileShare.None))

            {
                var reader = new PdfReader(inputPdfStream);
                var stamper = new PdfStamper(reader, outputPdfStream);
                var pdfContentByte = stamper.GetOverContent(1);

                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(inputImageStream);
                image.ScaleToFit(1500f, 650f);
                image.SetAbsolutePosition(100, 120);

                pdfContentByte.AddImage(image);
                stamper.Close();
            }
        }
        private void InsertTextToPdfA3()
        {
            int a = 0;
            string Cliente = F.labelCliente.Text;
            string Designacao = F.labelDesignacao.Text;
            string userName = Environment.UserName;
            string NomeDesenho = labelNomeFile.Text;
            string Classeex = F.labelClasseEx.Text;
            string Grauprep = F.labelGrauprep.Text;
            string Obra = F.labelNObra.Text;
            string Data = DateTime.Now.ToShortDateString().Replace('/', '.');
            string Modelo = Obra + "-" + Cliente;
            string loteconjunto = NomeDesenho.Substring((Obra + ".").Length);
            string fase = F.DataGridViewOrder.Rows[0].Cells[0].Value?.ToString()?.Trim() ?? string.Empty;
            int indiceSelecionado = lbnome.SelectedIndex + 1;
            string Nomecoluna = "2." + Obra + "." + fase + "." + indiceSelecionado;
            string Quantidade = string.Empty;
            string Perfil = string.Empty;
            string Comprimento = string.Empty;
            string Peso = string.Empty;
            string Material = string.Empty;
            string Revisão = string.Empty;
            string Objeto = tbnomeobjecto.Text;
            string Escala = "S/E";
            string Destinatario = string.Empty;
            string Artigo = string.Empty;
            string Areatxt = "ÁREA DE NEGÓCIO: ";
            string Artigotxt = "ARTIGO : ";
            string Descricaotxt = "DESCRIÇÃO: ";

            foreach (DataGridViewRow row in F.DataGridViewOrder.Rows)
            {
                if (row.Cells[3].Value != null && row.Cells[3].Value.ToString() == Nomecoluna)
                {
                    Revisão = row.Cells[5].Value?.ToString() ?? string.Empty;
                    Quantidade = row.Cells[8].Value?.ToString() ?? string.Empty;
                    Perfil = row.Cells[9].Value?.ToString() ?? string.Empty;
                    Material = row.Cells[10].Value?.ToString() ?? string.Empty;
                    Comprimento = row.Cells[13].Value?.ToString() ?? string.Empty;
                    Peso = row.Cells[14].Value?.ToString() ?? string.Empty;
                    Artigo = row.Cells[19].Value?.ToString() ?? string.Empty;
                    Destinatario = row.Cells[20].Value?.ToString() ?? string.Empty;
                    break;
                }
            }
            if (Cliente.Replace("Cliente -", "").Length > 56)
            {
                a = 56;
            }
            else
            {
                a = Cliente.Replace("Cliente -", "").Length;
            }
            int b = 0;
            if (Designacao.Replace("Designação -", "").Length > 56)
            {
                b = 56;
            }
            else
            {
                b = Designacao.Replace("Designação -", "").Length;
            }
            string sourceFileName = @"result.pdf";
            string newFileName = "C:\\r\\2." + labelNomeFile.Text + ".pdf";
            using (Stream pdfStream = new FileStream(sourceFileName, FileMode.Open))
            using (Stream newpdfStream = new FileStream(newFileName, FileMode.Create, FileAccess.ReadWrite))
            {
                PdfReader pdfReader = new PdfReader(pdfStream);
                PdfStamper pdfStamper = new PdfStamper(pdfReader, newpdfStream);
                PdfContentByte pdfContentByte = pdfStamper.GetOverContent(1);

                BaseFont fontNormal = BaseFont.CreateFont(@"C:\Windows\Fonts\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                BaseFont fontBold = BaseFont.CreateFont(@"C:\Windows\Fonts\arialbd.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

                pdfContentByte.SetColorFill(BaseColor.BLACK);
                pdfContentByte.BeginText();

                pdfContentByte.SetFontAndSize(fontNormal, 7);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, loteconjunto, 76, 809, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Quantidade, 135, 809, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Quantidade, 135, 799, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 8);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, loteconjunto, 940, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Quantidade, 970, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Perfil, 1010, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Comprimento, 1055, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Peso, 1093, 802, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Material, 1140, 802, 0);

                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Cliente.Replace("Cliente -", "").ToUpper().Substring(0, a), 905, 65, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Designacao.Replace("Designação -", "").ToUpper().Substring(0, b), 905, 50, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 10);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Objeto, 905, 90, 0);

                pdfContentByte.SetFontAndSize(fontBold, 8);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, NomeDesenho, 780, 95, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 6);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, userName, 775, 73, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Data, 845, 73, 0);

                pdfContentByte.SetFontAndSize(fontBold, 10);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Classeex, 683, 50, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Grauprep, 680, 20, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Escala, 862, 90, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 7);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Modelo, 915, 6, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_RIGHT, Data, 1175, 6, 0);

                pdfContentByte.SetFontAndSize(fontNormal, 8);
                pdfContentByte.SetFontAndSize(BaseFont.CreateFont("3OF9_NEW_0.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED), 20);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, "*" + NomeDesenho.Split('.')[0] + "." + NomeDesenho.Split('.')[1] + "*", 905, 20, 0);

                pdfContentByte.SetColorFill(BaseColor.GRAY);
                pdfContentByte.SetFontAndSize(fontNormal, 11);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Areatxt, 1187, 120, 90);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Artigotxt, 1187, 320, 90);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Descricaotxt, 1187, 600, 90);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Destinatario, 1187, 250, 90);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_LEFT, Artigo, 1187, 420, 90);
                pdfContentByte.EndText();
                pdfStamper.Close();
            }
        }
        private void Print_Click(object sender, EventArgs e)
        {
            if (Clipboard.GetImage() != null)
            {
                Print.Image = Clipboard.GetImage();
            }
            else
            {
                MessageBox.Show("Tem de existir uma imagem na area de transferencia");
            }
        }
        private void ButtonGerarOF_Click(object sender, EventArgs e)
        {
            if (Print.Image != null)
            {
                if (cbA4.Checked == true)
                {
                    Print.Image.Save(@"C:\r\1.Jpeg", ImageFormat.Jpeg);
                    GerarPDF();
                    InsertTextToPdfA4();
                    if (System.IO.File.Exists(@"C:\r\1.Jpeg"))
                    {
                        System.IO.File.Delete(@"C:\r\1.Jpeg");
                    }
                    if (System.IO.File.Exists(@"result.pdf"))
                    {
                        System.IO.File.Delete(@"result.pdf");
                    }
                    MessageBox.Show("Ficheiro " + labelNomeFile.Text + " criado com sucesso.");
                }
                else
                {
                    Print.Image.Save(@"C:\r\1.Jpeg", ImageFormat.Jpeg);
                    GerarPDFA3();
                    InsertTextToPdfA3();
                    if (System.IO.File.Exists(@"C:\r\1.Jpeg"))
                    {
                        System.IO.File.Delete(@"C:\r\1.Jpeg");
                    }
                    if (System.IO.File.Exists(@"result.pdf"))
                    {
                        System.IO.File.Delete(@"result.pdf");
                    }
                    MessageBox.Show("Ficheiro " + labelNomeFile.Text + " criado com sucesso.");
                }
                Print.Image = null;
            }
            else
            {
                MessageBox.Show("Tem de existir uma imagem ");
            }
        }
        private void DragDrop(object sender, DragEventArgs e)
        {

            string[] droppedfiles = (string[])e.Data.GetData(DataFormats.FileDrop);
            var filestream = (MemoryStream[])e.Data.GetData("FileContents");
            foreach (string item in droppedfiles)
            {
                Print.Image = System.Drawing.Image.FromFile(item);
            }
        }
        private void DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false) == true)
            {
                e.Effect = DragDropEffects.All;
            }
        }
        private void ButtonTakePrint_Click(object sender, EventArgs e)
        {
            Bitmap Imagem = new Bitmap(Print.Width, Print.Height);
            Graphics gfx = Graphics.FromImage(Imagem);
            gfx.CopyFromScreen(new Point(this.Location.X + Print.Location.X , this.Location.Y + Print.Location.Y), new Point(0, 0), Imagem.Size);
            Print.Image = Imagem;
        }
        private void ButtonClean_Click(object sender, EventArgs e)
        { Print.Image = null; }
        private void A4_CheckedChanged(object sender, EventArgs e)
        {
            if (cbA4.Checked)
            {
                cbA3.Checked = false;
            }
            else
            {
                if (!cbA3.Checked)
                    cbA4.Checked = true;
            }
        }
        private void A3_CheckedChanged(object sender, EventArgs e)
        {
            if (cbA3.Checked)
            {
                cbA4.Checked = false;
            }
            else
            {
                if (!cbA3.Checked)
                    cbA4.Checked = true;
            }
        }
    }
}
