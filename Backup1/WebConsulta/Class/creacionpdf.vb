
Imports iTextSharp.text.pdf
Imports iTextSharp.text

Public Class creacionpdf
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

    'Public Overrides Sub OnEndPage(writer As PdfWriter, document As Document)
    '    'base.OnEndPage(writer, document); 
    '    ' Writing Footer on Page 
    '    Dim tab As New PdfPTable(1)
    '    Dim cell As New PdfPCell(New Phrase("Prueba de Pie de Página"))
    '    cell.Border = 0
    '    tab.TotalWidth = 300.0F
    '    tab.AddCell(cell)
    '    tab.WriteSelectedRows(0, -1, 300, 30, writer.DirectContent)
    'End Sub

    'override the OnStartPage event handler to add our header
    'Public Overrides Sub OnStartPage(writer As PdfWriter, doc As Document)

    'End Sub

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
        Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_banner_certificado.jpg"))
        'Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance("http://190.95.210.36:8090/WSConcesionarios2/Images/background_banner_certificado.jpg")
        'Dim logo As String = "C:\Users\galvarado\Downloads\master_container_background_email.jpg"
        'I used a large version of the logo to maintain the quality when the size was reduced. I guess you could reduce the size manually and use a smaller version, but I used iTextSharp to reduce the scale. As you can see, I reduced it down to 7% of original size.
        logo.ScalePercent(75.0F)
        'logo.ScaleToFit(1000.0F, 30.0F)
        'create instance of a table cell to contain the logo
        Dim cell As New PdfPCell(logo)
        'align the logo to the right of the cell
        '*cell.HorizontalAlignment = Element.ALIGN_LEFT
        cell.HorizontalAlignment = Element.ALIGN_CENTER
        'add a bit of padding to bring it away from the right edge
        '*cell.PaddingRight = 0
        'cell.PaddingLeft = 0
        cell.PaddingLeft = 10
        cell.PaddingRight = 10
        ''''cell.PaddingTop = 20
        'remove the border
        cell.Border = 0
        'Add the cell to the table
        headerTbl.AddCell(cell)
        'write the rows out to the PDF output stream. I use the height of the document to position the table. Positioning seems quite strange in iTextSharp and caused me the biggest headache.. It almost seems like it starts from the bottom of the page and works up to the top, so you may ned to play around with this.
        'headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height - 10), writer.DirectContent)
        headerTbl.WriteSelectedRows(0, -1, 0, (doc.PageSize.Height), writer.DirectContent)
        'headerTbl.WriteSelectedRows(0, -1, 0, 500, writer.DirectContent)
        'Dim pdfContent As PdfContentByte
        ''Move the pointer and draw line to separate header section from rest of page
        'Dim p1Header As New Phrase("BlueLemonCode generated page", footer)
        'Dim p2Header As New Phrase("confidential", footer)
        ''create iTextSharp.text Image object using local image path
        ''Dim imgPDF As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpRuntime.AppDomainAppPath + "\images\bluelemoncode.jpg")
        ''Create PdfTable object
        'Dim pdfTab As New PdfPTable(2)
        ''We will have to create separate cells to include image logo and 2 separate strings
        '' Dim pdfCell1 As New PdfPCell(imgPDF)
        'Dim pdfCell2 As New PdfPCell(p1Header)
        'Dim pdfCell3 As New PdfPCell(p2Header)
        ''set the alignment of all three cells and set border to 0
        ''pdfCell1.HorizontalAlignment = Element.ALIGN_LEFT
        'pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER
        'pdfCell3.HorizontalAlignment = Element.ALIGN_RIGHT
        ''pdfCell1.Border = 0
        'pdfCell2.Border = 0
        'pdfCell3.Border = 0
        ''add all three cells into PdfTable
        ''pdfTab.AddCell(pdfCell1)
        'pdfTab.AddCell(pdfCell2)
        'pdfTab.AddCell(pdfCell3)
        'pdfTab.TotalWidth = doc.PageSize.Width - 20
        ''call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
        ''first param is start row. -1 indicates there is no end row and all the rows to be included to write
        ''Third and fourth param is x and y position to start writing
        'pdfTab.WriteSelectedRows(0, -1, 10, doc.PageSize.Height - 15, writer.DirectContent)
        ''set pdfContent value
        'pdfContent = writer.DirectContent
        ''Move the pointer and draw line to separate header section from rest of page
        'pdfContent.MoveTo(10, doc.PageSize.Height - 35)
        ''pdfContent.LineTo(doc.PageSize.Width - 40, doc.PageSize.Height - 35)
        'pdfContent.LineTo(doc.PageSize.Width - 10, doc.PageSize.Height - 35)
        'pdfContent.Stroke()
        '**********************************************
        ' texto de transparente
        '**********************************************
        'Dim cb As PdfContentByte = writer.DirectContentUnder
        'Dim baseFont1 As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED)
        'Dim gs As New PdfGState()
        'gs.FillOpacity = 0.35F
        'gs.StrokeOpacity = 0.35F
        'cb.BeginText()
        'cb.SetGState(gs)
        'cb.SetRGBColorFill(220, 220, 220)
        'cb.SetFontAndSize(baseFont1, 75)
        'cb.SetTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_STROKE)
        'cb.ShowTextAligned(Element.ALIGN_CENTER, "No Tributable", (doc.PageSize.Width) / 2, doc.PageSize.Height / 2, 315)
        'cb.EndText()
        Dim cb2 As PdfContentByte = writer.DirectContentUnder
        Dim baseFont2 As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.EMBEDDED)
        cb2.BeginText()
        'cb2.SetColorFill(BaseColor.LIGHT_GRAY)
        cb2.SetFontAndSize(baseFont2, 10)
        'cb2.ShowTextAligned(Element.ALIGN_CENTER, "NO VALIDA PARA EFECTOS TRIBUTARIOS", doc.PageSize.Width - 10, doc.PageSize.Height / 2, -90)
        cb2.EndText()
        '**********************************************
        ' pie de pagina
        '**********************************************
        'I use a PdfPtable with 2 columns to position my footer where I want it
        'Dim footerTbl As New PdfPTable(2)
        '''Dim footerTbl As New PdfPTable(1)
        'set the width of the table to be the same as the document
        '''footerTbl.TotalWidth = doc.PageSize.Width
        'Center the table on the page
        '''footerTbl.HorizontalAlignment = Element.ALIGN_CENTER
        'Create a paragraph that contains the footer text
        'Dim para As New iTextSharp.text.Paragraph("Some footer text", Footer)
        'add a carriage return
        'para.Add(Environment.NewLine)
        'para.Add("Some more footer text")
        'create a cell instance to hold the text
        'Dim cell As New PdfPCell(para)
        'set cell border to 0
        'cell.Border = 0
        'add some padding to bring away from the edge
        'cell.PaddingLeft = 10
        'add cell to table
        'footerTbl.AddCell(cell)
        'create new instance of Paragraph for 2nd cell text
        'para = New iTextSharp.text.Paragraph("Some text for the second cell", footer)
        ' Dim mensaje As String = CType(Session.Item("NumeroPages"), System.String) 'Session("NumeroPages")
        'Dim para As New iTextSharp.text.Paragraph("Pág. " & writer.PageNumber, Footer)
        ''create new instance of cell to hold the text
        ''cell = New PdfPCell(para)
        'Dim cellpie As New PdfPCell(para)
        ''align the text to the right of the cell
        'cellpie.HorizontalAlignment = Element.ALIGN_RIGHT
        ''set border to 0
        'cellpie.Border = 0
        '' add some padding to take away from the edge of the page
        'cellpie.PaddingRight = 10
        ''add the cell to the table
        'footerTbl.AddCell(cellpie)
        ''write the rows out to the PDF output stream.
        ''footerTbl.WriteSelectedRows(0, -1, 0, (doc.BottomMargin + 10), writer.DirectContent)
        'footerTbl.WriteSelectedRows(0, -1, 0, 25, writer.DirectContent)

        '***********************************************************************
        ' linea separador
        '***********************************************************************.
        'pdfContent = writer.DirectContent
        'Move the pointer and draw line to separate header section from rest of page
        ''''''pdfContent.MoveTo(10, doc.PageSize.Height - 810)
        'pdfContent.LineTo(doc.PageSize.Width - 40, doc.PageSize.Height - 35)
        ''''''pdfContent.LineTo(doc.PageSize.Width - 10, doc.PageSize.Height - 810)
        'pdfContent.ResetCMYKColorFill(footer)
        'pdfContent.SetColorStroke(BaseColor.GRAY)
        'pdfContent.Stroke()
    End Sub


    'Public Overrides Sub OnEndPage(writer As PdfWriter, documento As Document)
    '    Dim sPiePagina As String = "PIE DE PAGINA"
    '    documento.Add(New Paragraph(sPiePagina.Trim()))
    'End Sub


End Class
