
Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class creacionpdf_lad

    Inherits PdfPageEventHelper
    'I create a font object to use within my footer
    Protected ReadOnly Property Footer() As Font
        Get
            ' create a basecolor to use for the footer font, if needed.
            Dim grey As New BaseColor(128, 128, 128)
            Dim font1 As Font = FontFactory.GetFont("Arial", 9, Font.NORMAL, grey)
            Return font1
        End Get
    End Property



    'override the OnPageEnd event handler to add our footer
    Public Overrides Sub OnEndPage(ByVal writer As PdfWriter, ByVal doc As Document)
        '**********************************************
        ' encabezado de pagina
        '**********************************************
        'I use a PdfPtable with 1 column to position my header where I want it
        Dim headerTbl As New PdfPTable(1)
        'set the width of the table to be the same as the document
        headerTbl.TotalWidth = doc.PageSize.Width
        'I use an image logo in the header so I need to get an instance of the image to be able to insert it. I believe this is something you couldn't do with older versions of iTextSharp
        Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_banner_certificado_lader.jpg"))
        'I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
        logo.ScalePercent(75.0F)
        'logo.ScaleToFit(1000.0F, 30.0F)
        'create instance of a table cell to contain the logo
        Dim cell As New PdfPCell(logo)
        'align the logo to the right of the cell
        cell.HorizontalAlignment = Element.ALIGN_CENTER
        'add a bit of padding to bring it away from the right edge
        cell.PaddingLeft = 10
        cell.PaddingRight = 10
        cell.Border = 0
        'Add the cell to the table
        headerTbl.AddCell(cell)
        headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height), writer.DirectContent)

        Dim cb2 As PdfContentByte = writer.DirectContentUnder
        Dim baseFont2 As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED)
        cb2.BeginText()
        'cb2.SetColorFill(BaseColor.LIGHT_GRAY)
        cb2.SetFontAndSize(baseFont2, 10)
        'cb2.ShowTextAligned(Element.ALIGN_CENTER, "NO VALIDA PARA EFECTOS TRIBUTARIOS", doc.PageSize.Width - 10, doc.PageSize.Height / 2, -90)
        cb2.EndText()

    End Sub

End Class
