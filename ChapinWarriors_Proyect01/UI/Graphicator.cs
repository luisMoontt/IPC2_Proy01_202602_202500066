using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using ChapinWarriors.Models;

namespace ChapinWarriors.UI
{
    public static class MissionGraphExporter
    {
        private static string ObtenerCarpetaClase([CallerFilePath] string rutaArchivo = "")
        {
            return Path.GetDirectoryName(rutaArchivo)!;
        }

        private static string ColorPorTipo(int type)
        {
            switch (type)
            {
                case 1: return "black";      // Intransitable
                case 2: return "#8FCB7C";    // Punto de entrada
                case 3: return "#4A90D9";    // Unidad civil
                case 4: return "#A9A9A9";    // Recurso
                case 5: return "#C0392B";    // Unidad militar
                case 9: return "#F2D9A0";    // Camino
                default: return "white";     // Libre
            }
        }

        private static string ConstruirTablaMapa(MapMatrix missionMap, int rows, int columns)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<TABLE BORDER=\"0\" CELLBORDER=\"1\" CELLSPACING=\"0\" CELLPADDING=\"8\">");

            sb.Append("<TR><TD></TD>");
            for (int c = 0; c < columns; c++)
            {
                sb.Append($"<TD><B>{c + 1}</B></TD>");
            }
            sb.Append("</TR>");

            for (int r = 0; r < rows; r++)
            {
                sb.Append($"<TR><TD><B>{r + 1}</B></TD>");

                for (int c = 0; c < columns; c++)
                {
                    MapNode? nodo = missionMap.GetMapNode(r, c);
                    int tipo = nodo != null ? nodo.getNodeType() : 0;
                    string color = ColorPorTipo(tipo);
                    sb.Append($"<TD BGCOLOR=\"{color}\"></TD>");
                }

                sb.Append("</TR>");
            }

            sb.Append("</TABLE>");
            return sb.ToString();
        }

        private static string FilaLeyenda(string color, string etiqueta)
        {
            return $"<TR><TD BGCOLOR=\"{color}\" WIDTH=\"16\" HEIGHT=\"16\"></TD><TD ALIGN=\"LEFT\">{etiqueta}</TD></TR>";
        }

        private static string ConstruirLeyenda()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<TABLE BORDER=\"0\" CELLBORDER=\"0\" CELLSPACING=\"4\">");
            sb.Append(FilaLeyenda("black", "Intransitable"));
            sb.Append(FilaLeyenda("#8FCB7C", "Punto de entrada"));
            sb.Append(FilaLeyenda("#F2D9A0", "Camino"));
            sb.Append(FilaLeyenda("#C0392B", "Unidad militar"));
            sb.Append(FilaLeyenda("#4A90D9", "Unidad civil"));
            sb.Append(FilaLeyenda("#A9A9A9", "Recurso"));
            sb.Append("</TABLE>");

            return sb.ToString();
        }

        public static void ExportarMision(
            MapMatrix missionMap,
            int rows,
            int columns,
            string cityName,
            string tipoMisionTexto,
            string robotName,
            string robotType,
            int? vidaRestante,
            bool exito,
            int objetivoRow,
            int objetivoCol,
            string etiquetaObjetivo,
            Military[] militaresEnfrentados)
        {
            string titulo = $"Ruta de {tipoMisionTexto.ToLower()}:";

            StringBuilder texto = new StringBuilder();
            texto.Append($"Tipo de misión: {tipoMisionTexto.ToLower()}<BR ALIGN=\"LEFT\"/>");
            texto.Append($"{etiquetaObjetivo}: {objetivoRow},{objetivoCol}<BR ALIGN=\"LEFT\"/>");
            texto.Append($"Robot utilizado: {robotName} ({robotType})<BR ALIGN=\"LEFT\"/>");

            if (vidaRestante != null)
            {
                texto.Append($"Vida restante: {vidaRestante}<BR ALIGN=\"LEFT\"/>");
            }

            texto.Append($"Resultado: {(exito ? "MISION EXITOSA" : "MISION FALLIDA")}<BR ALIGN=\"LEFT\"/>");

            if (militaresEnfrentados.Length > 0)
            {
                texto.Append("Unidades militares enfrentadas:<BR ALIGN=\"LEFT\"/>");
                foreach (Military m in militaresEnfrentados)
                {
                    texto.Append($" - ({m.getM_Row()},{m.getM_Column()}) capacidad restante: {Math.Max(m.getM_Capacity(), 0)}<BR ALIGN=\"LEFT\"/>");
                }
            }

            string dot =
                "digraph MissionMap {\n" +
                "    graph [fontname=\"Helvetica\"];\n" +
                "    node [shape=plaintext, fontname=\"Helvetica\"];\n" +
                "    mapa [label=<\n" +
                "        <TABLE BORDER=\"0\" CELLBORDER=\"0\" CELLSPACING=\"12\">\n" +
                $"            <TR><TD><FONT POINT-SIZE=\"18\"><B>{titulo}</B></FONT></TD></TR>\n" +
                $"            <TR><TD>{ConstruirTablaMapa(missionMap, rows, columns)}</TD></TR>\n" +
                $"            <TR><TD>{ConstruirLeyenda()}</TD></TR>\n" +
                $"            <TR><TD ALIGN=\"LEFT\">{texto}</TD></TR>\n" +
                "        </TABLE>\n" +
                "    >];\n" +
                "}\n";

            string carpeta = ObtenerCarpetaClase();
            string nombreBase = $"mision_{cityName}_{DateTime.Now:yyyyMMdd_HHmmss}";
            string rutaDot = Path.Combine(carpeta, nombreBase + ".dot");
            string rutaPdf = Path.Combine(carpeta, nombreBase + ".pdf");

            File.WriteAllText(rutaDot, dot);

            ProcessStartInfo psiDot = new ProcessStartInfo
            {
                FileName = "dot",
                Arguments = $"-Tpdf \"{rutaDot}\" -o \"{rutaPdf}\"",
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process proceso = Process.Start(psiDot)!)
            {
                proceso.WaitForExit();
            }

            ProcessStartInfo psiAbrir = new ProcessStartInfo
            {
                FileName = rutaPdf,
                UseShellExecute = true
            };

            Process.Start(psiAbrir);
        }
    }
}