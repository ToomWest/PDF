using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace EjemploPDF
{
    public partial class GenerarPDF : Form
    {
        public GenerarPDF()
        {
            InitializeComponent();
        }

        private void btnGenerar_Click(object sender, EventArgs e)
        {
            //Se ejecuta el botón.
            CrearPDF();
        }

        private void CrearPDF()
        {
            //variables para nombre del documento
            string nombre = "DOCUMENTO";
            string fecha_hoy = DateTime.Today.ToString("dd-MM-yyyy");

            //Se instancia el guardado de archivo
            SaveFileDialog archivo = new SaveFileDialog();

            //Se instancia el tipo de archivo que se va a generar
            archivo.Filter = ".pdf file (*.pdf)|*.pdf";
            archivo.FileName = nombre + "_" + fecha_hoy;
            archivo.FilterIndex = 1;
            archivo.RestoreDirectory = true;

            //Consulta que pregunta si se genero el archivo con el boton Aceptar o OK
            if (archivo.ShowDialog() == DialogResult.OK)
            {
                //Documento PDF que se genera
                Document documento = new Document(iTextSharp.text.PageSize.LETTER, 40, 40, 50, 30);
                PdfWriter wri = PdfWriter.GetInstance(documento, new FileStream(archivo.FileName, FileMode.Create));
                documento.Open();

                //Se instancia una imagen con su ruta especifica
                iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance("../../Resources/Logo.png");
                //Se establece el aliniamiento, que puede ser izquierda, centro, derecha, etc.
                img.Alignment = Element.ALIGN_RIGHT;
                //Se establece el tamaño o escalado de la imagen 
                img.ScalePercent(30, 30);
                //Se agrega a imagen establecida al documento
                documento.Add(img);

                //Se genera un espacio para usar cuando se necesite
                Paragraph espacio = new Paragraph("\n");
                espacio.Alignment = Element.ALIGN_JUSTIFIED;
                espacio.Font.Size = 10;
                documento.Add(espacio);

                //Se instancia un parrafo
                Paragraph p_titulo = new Paragraph("Certificado de alumno regular");
                //Se establece el alineamiento del parrafo
                p_titulo.Alignment = Element.ALIGN_CENTER;
                //Se establece el tamaño del parrafo
                p_titulo.Font.Size = 10;
                //Se establece si el parrafo está en negrita
                p_titulo.Font.IsBold();
                //Se agrega el parrafo al documento
                documento.Add(p_titulo);

                //Se agrega un espacio
                documento.Add(espacio);

                //Se agrega el texto completo
                Paragraph p_texto = new Paragraph("El límite de estos servicios será cuando cada uno de ellos lleguen al 90% por ciento de su capacidad, luego de estos se definirán alarmas para generar un plan de capacidad y poder ampliar dicho servicio a más capacidad y poder controlar la situación."
                + "\n" + " La monitorización de los umbrales no sólo se dedicará a avisar sobre la superación de un umbral, sino que también debe monitorizar la velocidad de su evolución y predecir cuándo se alcanzará el umbral con estadísticas realizadas con anterioridad.");
                p_texto.Alignment = Element.ALIGN_CENTER;
                p_texto.Font.Size = 10;

                documento.Add(p_texto);

                //Pie de pagina del PDF
                iTextSharp.text.Rectangle page = documento.PageSize;
                PdfPTable head = new PdfPTable(1);
                head.TotalWidth = page.Width;
                Phrase phrase = new Phrase("Copyright, Todos los derechos reservados");
                phrase.Font.Size = 10;
                phrase.Font.Color = GrayColor.GRAY;
                PdfPCell c = new PdfPCell(phrase);
                c.Border = iTextSharp.text.Rectangle.NO_BORDER;
                c.VerticalAlignment = Element.ALIGN_BOTTOM;
                c.HorizontalAlignment = Element.ALIGN_CENTER;
                head.AddCell(c);
                head.WriteSelectedRows(
                  0, -1,
                  0,
                  40,
                  wri.DirectContent
                );
                
                documento.Close();
            }
        }
    }
}
