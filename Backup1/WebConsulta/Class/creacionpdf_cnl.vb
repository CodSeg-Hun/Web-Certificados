Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class creacionpdf_cnl

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
        headerTbl.TotalWidth = doc.PageSize.Width
        'Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance("../Images/background_banner_certificado_conecel.jpg")
        Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_banner_certificado_conecel.jpg"))
        logo.ScalePercent(73.0F)
        Dim cell As New PdfPCell(logo)
        'align the logo to the right of the cell
        cell.HorizontalAlignment = Element.ALIGN_CENTER
        cell.PaddingTop = 10
        cell.PaddingLeft = 25
        cell.PaddingRight = 25
        cell.Border = 0
        headerTbl.AddCell(cell)
        headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height), writer.DirectContent)
        Dim pdfContent As PdfContentByte
        Dim cb2 As PdfContentByte = writer.DirectContentUnder
        Dim baseFont2 As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED)
        cb2.BeginText()
        cb2.SetColorFill(BaseColor.LIGHT_GRAY)
        cb2.SetFontAndSize(baseFont2, 12)
        cb2.ShowTextAligned(Element.ALIGN_CENTER, "Convenio Claro Flota ", doc.PageSize.Width - 10, doc.PageSize.Height / 2, -90)
        cb2.EndText()
        '**********************************************
        ' pie de pagina
        '**********************************************
        Dim footerTbl As New PdfPTable(2)
        footerTbl.TotalWidth = doc.PageSize.Width
        footerTbl.HorizontalAlignment = Element.ALIGN_CENTER
        Dim para0 As New iTextSharp.text.Paragraph(" ", Footer)
        Dim para1 As New iTextSharp.text.Paragraph(" ", Footer)
        Dim cellpie0 As New PdfPCell(para0)
        cellpie0.HorizontalAlignment = Element.ALIGN_LEFT
        cellpie0.Border = 0
        cellpie0.PaddingLeft = 10
        footerTbl.AddCell(cellpie0)
        Dim cellpie1 As New PdfPCell(para1)
        cellpie1.HorizontalAlignment = Element.ALIGN_RIGHT
        cellpie1.Border = 0
        cellpie1.PaddingRight = 10
        footerTbl.AddCell(cellpie1)
        footerTbl.WriteSelectedRows(0, -1, 0, 25, writer.DirectContent)
        '***********************************************************************
        ' linea separador
        '***********************************************************************.
        pdfContent = writer.DirectContent
        pdfContent.MoveTo(10, doc.PageSize.Height - 810)
        pdfContent.LineTo(doc.PageSize.Width - 10, doc.PageSize.Height - 810)
        pdfContent.SetColorStroke(BaseColor.GRAY)
        pdfContent.Stroke()
    End Sub

End Class
