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
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                pdfContentByte.SetColorFill(BaseColor.BLACK);
                pdfContentByte.SetFontAndSize(baseFont, 8);
                pdfContentByte.BeginText();
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, labelNomeFile.Text, 500, 80, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Environment.UserName.ToUpper(), 165, 133, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, DateTime.Now.ToShortDateString(), 117, 133, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Cliente.Replace("Cliente -", "").ToUpper().Substring(0, a), 325, 133, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Designacao.Replace("Designação -", "").ToUpper().Substring(0, b), 325, 120, 0);
                pdfContentByte.SetFontAndSize(BaseFont.CreateFont("3OF9_NEW_0.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED), 20);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "*" + labelNomeFile.Text.Split('.')[0] + "." + labelNomeFile.Text.Split('.')[1] + "*", 330, 30, 0);
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
                image.ScaleToFit(1000f, 550f);
                image.SetAbsolutePosition(90, 250);

                pdfContentByte.AddImage(image);
                stamper.Close();
            }
        }
        private void InsertTextToPdfA3()
        {
            int a = 0;
            string Cliente = F.labelCliente.Text;
            string Designacao = F.labelDesignacao.Text;
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
                BaseFont baseFont = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.NOT_EMBEDDED);
                pdfContentByte.SetColorFill(BaseColor.BLACK);
                pdfContentByte.SetFontAndSize(baseFont, 8);
                pdfContentByte.BeginText();
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, labelNomeFile.Text, 1100, 80, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Environment.UserName.ToUpper(), 763, 133, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, DateTime.Now.ToShortDateString(), 713, 133, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Cliente.Replace("Cliente -", "").ToUpper().Substring(0, a), 920, 133, 0);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, Designacao.Replace("Designação -", "").ToUpper().Substring(0, b), 920, 120, 0);
                pdfContentByte.SetFontAndSize(BaseFont.CreateFont("3OF9_NEW_0.TTF", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED), 20);
                pdfContentByte.ShowTextAligned(PdfContentByte.ALIGN_CENTER, "*" + labelNomeFile.Text.Split('.')[0] + "." + labelNomeFile.Text.Split('.')[1] + "*", 920, 30, 0);
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
        {
            Print.Image = null;

        }
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
