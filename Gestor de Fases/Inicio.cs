using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Gestor_de_Fases
{
    class Inicio
    {
        public ObraInfo BuscarObra(string numeroObra)
        {
            if (string.IsNullOrWhiteSpace(numeroObra))
                throw new ArgumentException("Número da obra não pode estar vazio.");

            numeroObra = numeroObra.ToUpper();

            BD d = new BD();
            d.ligarbd();

            List<string> list = d.Procurarbd(
                "select titulo , nome from [PRIOFELIZ].[dbo].[MT_View_Obras_Clientes_Descricao] " +
                "where codigo = '" + numeroObra + "'"
            );

            d.desligarbd();

            if (list == null || list.Count < 2)
                throw new Exception("Número da obra inválido.");

            int ano = 0;
            try
            {
                ano = int.Parse("20" + numeroObra.Substring(0, 2));
            }
            catch
            {
                ano = int.Parse("20" + numeroObra.Substring(2, 2));
            }

            string caminho = $@"\\Marconi\COMPANY SHARED FOLDER\OFELIZ\OFM\2.AN\2.CM\DP\1 Obras\{ano}\{numeroObra.Trim()}\1.9 Gestão de fabrico\";

            return new ObraInfo
            {
                Designacao = list[0],
                Cliente = list[1],
                Ano = ano,
                Fase250 = CalcularProximaFase(caminho, 249),
                Fase750 = CalcularProximaFase(caminho, 749),
                Fase1000 = CalcularProximaFase(caminho, 999)
            };
        }       

        private string CalcularProximaFase(string caminho, int inicio)
        {
            int f = inicio;
            string fase;
            var nomesDePasta = Directory.GetDirectories(caminho)
                                        .Select(Path.GetFileName)
                                        .ToList();
            do
            {
                f++;
                fase = f.ToString("000");
            } while (nomesDePasta.Any(nome => nome.StartsWith(fase, StringComparison.OrdinalIgnoreCase)));
            return fase;
        }

    }

    public class ObraInfo
    {
        public string Designacao { get; set; }
        public string Cliente { get; set; }
        public int Ano { get; set; }
        public string Fase250 { get; set; }
        public string Fase750 { get; set; }
        public string Fase1000 { get; set; }
    }


}

