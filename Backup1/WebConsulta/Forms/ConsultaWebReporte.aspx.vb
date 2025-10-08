
Imports System.Collections.Generic
Imports System.Linq
Imports MessagingToolkit.Barcode
Imports System.Data
Imports System.IO


Public Class ConsultaWebReporte
    Inherits System.Web.UI.Page
    'Public tempFileName As String = String.Empty
    Dim datVehiculo As Int32
    Dim datOs As Int32
    Dim datCliente As String
    Dim contParmBlank As Integer = 0

    Private Shared barcodeFormats As New Dictionary(Of String, BarcodeFormat)() From { _
       {"QR Code", BarcodeFormat.QRCode}, _
       {"Data Matrix", BarcodeFormat.DataMatrix}, _
       {"PDF417", BarcodeFormat.PDF417}, _
       {"Aztec", BarcodeFormat.Aztec}, _
       {"Bookland/ISBN", BarcodeFormat.Bookland}, _
       {"Codabar", BarcodeFormat.Codabar}, _
       {"Code 11", BarcodeFormat.Code11}, _
       {"Code 128", BarcodeFormat.Code128}, _
       {"Code 128-A", BarcodeFormat.Code128A}, _
       {"Code 128-B", BarcodeFormat.Code128B}, _
       {"Code 128-C", BarcodeFormat.Code128C}, _
       {"Code 39", BarcodeFormat.Code39}, _
       {"Code 39 Extended", BarcodeFormat.Code39Extended}, _
       {"Code 93", BarcodeFormat.Code93}, _
       {"EAN-13", BarcodeFormat.EAN13}, _
       {"EAN-8", BarcodeFormat.EAN8}, _
       {"FIM", BarcodeFormat.FIM}, _
       {"Interleaved 2 of 5", BarcodeFormat.Interleaved2of5}, _
       {"ITF-14", BarcodeFormat.ITF14}, _
       {"LOGMARS", BarcodeFormat.LOGMARS}, _
       {"MSI 2 Mod 10", BarcodeFormat.MSI2Mod10}, _
       {"MSI Mod 10", BarcodeFormat.MSIMod10}, _
       {"MSI Mod 11", BarcodeFormat.MSIMod11}, _
       {"MSI Mod 11 Mod 10", BarcodeFormat.MSIMod11Mod10}, _
       {"PostNet", BarcodeFormat.PostNet}, _
       {"Plessey", BarcodeFormat.ModifiedPlessey}, _
       {"Standard 2 of 5", BarcodeFormat.Standard2of5}, _
       {"Telepen", BarcodeFormat.Telepen}, _
       {"UPC 2 Digit Ext.", BarcodeFormat.UPCSupplemental2Digit}, _
       {"UPC 5 Digit Ext.", BarcodeFormat.UPCSupplemental5Digit}, _
       {"UPC-A", BarcodeFormat.UPCA}, _
       {"UPC-E", BarcodeFormat.UPCE} _
    }

    Private barcodeEncoder As New BarcodeEncoder()

    Private Sub CargaImagenQR()
        Try
            Dim dtOsqr As New DataSet
            'Dim dataos As String
            dtOsqr = Session("infoqr")
            'dataos = dtOsqr.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
            datOs = dtOsqr.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
            datVehiculo = dtOsqr.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
            datCliente = dtOsqr.Tables(0).Rows(0)("ID_CLIENTE").ToString()
            'Dim data As String = "http://190.95.210.35:8083/login.aspx?os=" & dataos
            Dim data As String = "https://www.hunteronline.com.ec/WSConcesionarios/Forms/ConsultaWebReporte.aspx?os= " & datOs & "&veh=" & datVehiculo & "&cli=" & datCliente
            If String.IsNullOrEmpty(data) Then
                'imgqrgenerator.Visible = False
                Return
            End If
            ' Get barcode format
            Dim fmt As BarcodeFormat = BarcodeFormat.QRCode
            If barcodeFormats.ContainsKey(cboBarcodeType.Text) Then
                fmt = barcodeFormats(cboBarcodeType.Text)
            End If
            Dim tempFileName As String = String.Empty
            Dim image As System.Drawing.Image = barcodeEncoder.Encode(fmt, data)
            tempFileName = "~/" + GenerateRandomFileName(Server.MapPath("~/"))
            Session("nombrebarra") = tempFileName
            'tempFileName = "c:\tmp\archivo.jpg"   '+ GenerateRandomFileName(Server.MapPath("c:\tmp\"))
            barcodeEncoder.SaveImage(Server.MapPath(tempFileName), SaveOptions.Png)
            'barcodeEncoder.SaveImage(tempFileName, SaveOptions.Jpg)
            Me.imgqrgenerator.ImageUrl = tempFileName
            imgqrgenerator.Visible = True
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Function GenerateRandomFileName(ByVal folderPath As String) As String
        Dim chars As String = "2346789ABCDEFGHJKLMNPQRTUVWXYZabcdefghjkmnpqrtuvwxyz"
        Dim rnd As New Random()
        Dim name As String = ""
        Try
            Do
                name = String.Empty
                While name.Length < 5
                    name += chars.Substring(rnd.[Next](chars.Length), 1)
                End While
                ''name += ".png"
                name += ".jpg"
            Loop While File.Exists(folderPath & name)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
        Return name
    End Function

    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If ValidaParametros() = True Then
                    Session("DetalleVehiculo") = Nothing
                    Detalle("LinK")
                Else
                    Session("DetalleVehiculo") = Nothing
                    Detalle("Nuevo")
                End If
                'Session("DetalleVehiculo") = Nothing
                'Detalle()
                TituloReporte()
                CargaImagenQR()
                Me.lblfechahoraemision.Text = "Emitido el " & System.DateTime.Now.ToString("dd MMMM yyyy H:mm:ss")
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Public Function ValidaParametros() As Boolean
        Try
            If IsNullOrBlank(Page.Request.QueryString("os")) = True Then
                contParmBlank += 1
            Else
                datOs = Page.Request.QueryString("os")
            End If
            If IsNullOrBlank(Page.Request.QueryString("veh")) = True Then
                contParmBlank += 1
            Else
                datVehiculo = Page.Request.QueryString("veh")
            End If
            If IsNullOrBlank(Page.Request.QueryString("cli")) = True Then
                contParmBlank += 1
            Else
                datCliente = Page.Request.QueryString("cli")
            End If
            If contParmBlank = 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function


    Public Function IsNullOrBlank(ByVal str As String) As Boolean
        Try
            If String.IsNullOrEmpty(str) Then
                Return True
            End If
            For Each c In str
                If Not Char.IsWhiteSpace(c) Then
                    Return False
                End If
            Next
            Return True
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function


    ''' <summary>
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' COMENTARIO: MÉTODO PARA ACTUALIZAR EL TITULO DEL FORMULARIO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TituloReporte()
        Try
            'Dim tituloDefault As String = "CERTIFICADO DE PROPIEDAD"
            'Dim aniofljcja As String
            'If Not Session("rpt_flj_cja_det_anio") Is Nothing Then
            '    aniofljcja = Session("rpt_flj_cja_det_anio")
            'Else
            '    aniofljcja = Now.Year
            'End If
            'lbltitulodetfljcja.Text = tituloDefault '& " - " & aniofljcja
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 27/02/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: MÉTODO PARA OBTENER DATOS DE FLUJO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Detalle(ByVal tipo As String)
        Try
            Dim obj As New ConsultaWeb
            'Dim dtDetalleFlujo2 As New System.Data.DataSet
            'dtDetalleFlujo2 = obj.ConsultaDetalleFormaPago(empresa, proceso, Session("rpt_flj_cja_det_frm_pago_anio"))
            Dim dtdetalle As New System.Data.DataSet
            Dim dtconsulta As New System.Data.DataSet
            If tipo = "Nuevo" Then
                dtdetalle = Session("Detalle")
                If dtdetalle.Tables(0).Rows.Count > 0 Then
                    'Me.Vehiculo.Text = dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                    'If dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "C" Then
                    '    Image1.ImageUrl = "../Images/background_banner_certificado.jpg"
                    'ElseIf dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "D" Then
                    '    Image1.ImageUrl = "../Images/background_banner_certificado_des.jpg"
                    'ElseIf dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "I" Then
                    '    Image1.ImageUrl = "../Images/background_banner_certificado_ins.jpg"
                    'End If
                    If Session("cbm_tipo") = "C" Then
                        Image1.ImageUrl = "../Images/background_banner_certificado.jpg"
                        grid.Visible = True
                        tex01.Visible = True
                        tex04.Visible = False
                        tex03.Visible = False
                        tex02.Visible = True
                        tex05.Visible = True
                        tex06.Visible = True
                        tex07.Visible = True
                    ElseIf Session("cbm_tipo") = "D" Then
                        grid.Visible = False
                        tex01.Visible = False
                        tex04.Visible = True
                        tex03.Visible = False
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        tex07.Visible = False
                        Image1.ImageUrl = "../Images/background_banner_certificado_des.jpg"
                    ElseIf Session("cbm_tipo") = "I" Then
                        grid.Visible = False
                        tex01.Visible = False
                        tex04.Visible = False
                        tex03.Visible = True
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        tex07.Visible = False
                        Image1.ImageUrl = "../Images/background_banner_certificado_ins.jpg"
                    End If
                    dtconsulta = obj.ConsultarImprimir(dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtdetalle.Tables(0).Rows(0)("CLIENTE").ToString(), Session("user_id"), dtdetalle.Tables(0).Rows(0)("TIPO").ToString())
                    Session("infoqr") = dtconsulta
                    If dtconsulta.Tables(0).Rows.Count > 0 Then
                        Me.Nombre.Text = dtconsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                        Me.Vehiculo.Text = dtconsulta.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                        ''Me.Producto.Text = dtconsulta.Tables(0).Rows(0)("PRODUCTO").ToString()
                        ''Me.Cobertura.Text = dtconsulta.Tables(0).Rows(0)("COBERTURA").ToString()
                        Me.Marca.Text = dtconsulta.Tables(0).Rows(0)("MARCA").ToString()
                        Me.Anio.Text = dtconsulta.Tables(0).Rows(0)("ANIO").ToString()
                        Me.Modelo.Text = dtconsulta.Tables(0).Rows(0)("MODELO").ToString()
                        Me.Placa.Text = dtconsulta.Tables(0).Rows(0)("PLACA").ToString()
                        Me.Tipo.Text = dtconsulta.Tables(0).Rows(0)("TIPO").ToString()
                        Me.Chasis.Text = dtconsulta.Tables(0).Rows(0)("CHASIS").ToString()
                        Me.Color.Text = dtconsulta.Tables(0).Rows(0)("COLOR").ToString()
                        Me.Motor.Text = dtconsulta.Tables(0).Rows(0)("MOTOR").ToString()
                        ''Me.precioproducto.Text = dtconsulta.Tables(0).Rows(0)("PRECIO_PRODUCTO").ToString()
                        Me.grdproductosdetalle.DataSource = dtconsulta
                        'Session("datocliente") = Me.Nombre.Text + " | " + Me.Marca.Text + " | " + Me.Modelo.Text + " | " + Me.Chasis.Text + " | " + Me.Placa.Text
                    End If
                End If
            Else
                dtconsulta = obj.ConsultarImprimir(datVehiculo, datCliente, Session("user_id"), "C")
                Session("infoqr") = dtconsulta
                If dtconsulta.Tables(0).Rows.Count > 0 Then
                    Me.Nombre.Text = dtconsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.Vehiculo.Text = dtconsulta.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                    Me.Marca.Text = dtconsulta.Tables(0).Rows(0)("MARCA").ToString()
                    Me.Anio.Text = dtconsulta.Tables(0).Rows(0)("ANIO").ToString()
                    Me.Modelo.Text = dtconsulta.Tables(0).Rows(0)("MODELO").ToString()
                    Me.Placa.Text = dtconsulta.Tables(0).Rows(0)("PLACA").ToString()
                    Me.Tipo.Text = dtconsulta.Tables(0).Rows(0)("TIPO").ToString()
                    Me.Chasis.Text = dtconsulta.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.Color.Text = dtconsulta.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Motor.Text = dtconsulta.Tables(0).Rows(0)("MOTOR").ToString()
                    Me.grdproductosdetalle.DataSource = dtconsulta
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    'Private Sub ConviertePdf_1()
    '    Try
    '        'System.ArgumentException = {"El control de script 'grdproductosdetalle' no está registrado. Los controles de script deben registrarse mediante RegisterScriptControl() antes de llamar a RegisterScriptDescriptors().
    '        'Nombre del parámetro: scriptControl"}
    '        'grdproductosdetalle.RegisterWithScriptManager = True
    '        'RegisterScriptContro()
    '        Dim contenido As String = ""
    '        Response.ContentType = "aplication/pdf"
    '        Response.AddHeader("content-disposition", "attachment;filename=archivo.pdf")
    '        Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '        Dim sw As New StringWriter()
    '        Dim hw As New HtmlTextWriter(sw)
    '        contenido = "<h5>EXPORTA HTML TO PDF</h5><br/><br/><b><u>Convierte desde html a PDF</u></b><br/><br/><br/><font color='blue'>Ejemplo de pdf!!!</font>"
    '        form1.RenderControl(hw)
    '        'Dim sr As New System.IO.StringReader(contenido.ToString)
    '        Dim sr As New System.IO.StringReader(sw.ToString())
    '        'Dim Doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
    '        Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
    '        Dim htmlparser As New HTMLWorker(doc)
    '        'PdfWriter.GetInstance(Doc, Response.OutputStream)
    '        iTextSharp.text.pdf.PdfWriter.GetInstance(Doc, System.Web.HttpContext.Current.Response.OutputStream)
    '        Doc.Open()
    '        htmlparser.Parse(sr)
    '        Doc.Close()
    '        Response.Write(Doc)
    '        Response.End()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    'Private Sub conviertePdf_2()
    '    'Dim Document As New iTextSharp.text.Document()
    '    'Try
    '    '    iTextSharp.text.pdf.PdfWriter.GetInstance(Document, New FileStream("c:\tmp\archivo.pdf", FileMode.Create))
    '    '    Document.Open()
    '    '    Dim webclient As New Net.WebClient()
    '    '    Dim htmltext As String
    '    '    'Dim k As Int
    '    '    'htmltext = webclient.DownloadString("http://localhost:59500/my.html")
    '    '    htmltext = webclient.DownloadString("c:\tfs\3s_web\webconsulta\webconsulta\forms\consultawebreporte.aspx")
    '    '    Response.Write(htmltext)
    '    '    'Dim htmlarraylist As String
    '    '    Dim htmlarraylist As List(Of IElement) = HTMLWorker.ParseToList(New StringReader(htmltext), Nothing)
    '    '    For k As Integer = 0 To htmlarraylist.Count
    '    '        Document.Add((IElement), htmlarraylist(k))
    '    '    Next
    '    '    Document.Close()
    '    'Catch ex As Exception
    '    '    Captura_Error(ex)
    '    'End Try
    '    'Dim document As New Document()
    '    'Try
    '    '    iTextSharp.text.pdf.PdfWriter.GetInstance(document, New FileStream("c:\my.pdf", FileMode.Create))
    '    '    document.Open()
    '    '    Dim wc As New Net.WebClient()
    '    '    Dim htmlText As String = wc.DownloadString("c:\tfs\3s_web\webconsulta\webconsulta\forms\consultawebreporte.aspx")
    '    '    Response.Write(htmlText)
    '    '    Dim htmlarraylist As List(Of IElement) = HTMLWorker.ParseToList(New System.IO.StringReader(htmlText), Nothing)
    '    '    For k As Integer = 0 To htmlarraylist.Count - 1
    '    '        document.Add(DirectCast(htmlarraylist(k), IElement))
    '    '    Next
    '    '    document.Close()
    '    'Catch
    '    '    Captura_Error(ex)
    '    'End Try
    '    Try
    '        Response.ContentType = "aplication/pdf"
    '        Response.AddHeader("content-disposition", "attachment;filename=archivo.pdf")
    '        Response.Cache.SetCacheability(HttpCacheability.NoCache)
    '        Dim wc As New Net.WebClient()
    '        Dim htmlText As String = wc.DownloadString("c:\tfs\3s_web\webconsulta\webconsulta\forms\consultawebreporte.aspx")
    '        Dim sw As New StringWriter()
    '        Dim hw As New HtmlTextWriter(sw)
    '        'form1.RenderControl(hw)
    '        Dim sr As New System.IO.StringReader(hw.ToString())
    '        Dim doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
    '        Dim htmlparser As New HTMLWorker(Doc)
    '        iTextSharp.text.pdf.PdfWriter.GetInstance(Doc, Response.OutputStream)
    '        Doc.Open()
    '        htmlparser.Parse(sr)
    '        Doc.Close()
    '        Response.Write(Doc)
    '        Response.End()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    'Private Sub ConviertePdf_3()
    '    'Dim Doc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
    '    'Dim contenido As String
    '    ''Dim parsedhtmlelements As String
    '    ''Dim htmlelement As String
    '    'Try
    '    '    iTextSharp.text.pdf.PdfWriter.GetInstance(Doc, System.Web.HttpContext.Current.Response.OutputStream)
    '    '    Doc.Open()
    '    '    contenido = "<h5>EXPORT HTML CONTENT TO PDF</h5><br/><br/><b><u>This content is convert from html string to PDF</u></b><br/><br/><br/><font color='red'>Samples from Ravi!!!</font>"
    '    '    Dim parsedhtmlelements = HTMLWorker.ParseToList(New StringReader(contenido), Nothing)

    '    '    For Each htmlElement As Element In parsedhtmlelements
    '    '        Doc.Add(TryCast(htmlElement, IElement))
    '    '    Next
    '    '    Doc.Close()
    '    '    Response.ContentType = "application/pdf"
    '    '    Response.AddHeader("content-disposition", "attachment: filename=" + "nuevo" + ".pdf")
    '    '    System.Web.HttpContext.Current.Response.Write(Doc)
    '    '    Response.Flush()
    '    '    Response.End()
    '    '    'Dim htmlparser As New HTMLWorker(Doc)
    '    'Catch ex As Exception
    '    '    Captura_Error(ex)
    '    'End Try
    '    Dim pdfDoc As New iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F)
    '    iTextSharp.text.pdf.PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream)
    '    Try
    '        '//Open PDF Document to write data
    '        pdfDoc.Open()
    '        '    //Assign Html content in a string to write in PDF
    '        Dim filename As String = "nuevo.pdf"
    '        Dim contents As String = ""
    '        Dim sr As New System.IO.StreamReader("c:\tmp\archivo.html")
    '        'System.IO.StreamReader(sr)
    '        '        //Read file from server path
    '        sr = File.OpenText(Server.MapPath("ConsultaWebReporte.aspx"))
    '        '        //store content in the variable
    '        contents = sr.ReadToEnd()
    '        sr.Close()
    '        Dim parsedhtmlelements = HTMLWorker.ParseToList(New StringReader(contents), Nothing)
    '        For Each htmlElement As Element In parsedhtmlelements
    '            pdfDoc.Add(TryCast(htmlElement, IElement))
    '        Next
    '        pdfDoc.Close()
    '        Response.ContentType = "application/pdf"
    '        Response.AddHeader("Content-Disposition", "attachment: filename=" + FILENAME)
    '        System.Web.HttpContext.Current.Response.Write(pdfDoc)
    '        Response.Flush()
    '        Response.End()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub

    
    ' ''Protected Sub GeneraPDF_Click(ByVal sender As Object, ByVal e As EventArgs) Handles Button1.Click
    ' ''    Try

    ' ''        Dim fontNormal As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL)
    ' ''        Dim fontBold As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD)
    ' ''        'Dim fontTitulo As iTextSharp.text.Font = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.UNDERLINE)
    ' ''        Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
    ' ''        'Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)
    ' ''        Dim fontcv As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
    ' ''        Dim grey As New BaseColor(64, 64, 64)
    ' ''        'Dim grey As New BaseColor(128, 128, 128)
    ' ''        Dim negro As New BaseColor(0, 0, 0)

    ' ''        Dim fontgris As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, grey)
    ' ''        Dim fontnegro As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, negro)

    ' ''        Dim espacio As String = Space(8)
    ' ''        Dim espacio2 As String = Space(6)
    ' ''        Dim dtconsulta2 As New System.Data.DataSet

    ' ''        dtconsulta2 = Session("infoqr")

    ' ''        Dim Documento As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
    ' ''        'Dim htmlparser As New HTMLWorker(Documento)

    ' ''        'Session("datocliente") = Me.Nombre.Text & " | " & Me.Marca.Text & " | " & Me.Modelo.Text & " | " & Me.Chasis.Text & " | " & Me.Placa.Text
    ' ''        ''Dim File1 As String = String.Empty
    ' ''        ''File1 = GenerateRandomFilePDF(Server.MapPath("~/"))
    ' ''        ''Dim file As String = Server.MapPath("/" & File1)
    ' ''        ''Session("nombrepdf") = file

    ' ''        'Dim file As String = "C:\certificado\certificado.pdf"
    ' ''        'Dim file As String = Server.MapPath("/cert_" & Me.Vehiculo.Text & ".pdf")
    ' ''        'Dim file As String = "c:\certificado\ce_" & Me.Vehiculo.Text & ".pdf"
    ' ''        Dim file As String = "\\10.100.107.14\ImagenesDocumentos\cert_" & Me.Vehiculo.Text & ".pdf"
    ' ''        Session("nombrepdf") = file
    ' ''        'Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("c:\tmp\banner_003.png"))
    ' ''        Dim writer As PdfWriter = PdfWriter.GetInstance(Documento, New FileStream(file, FileMode.Create))
    ' ''        Dim lineablanco As New iTextSharp.text.Paragraph(" ")

    ' ''        Dim ev As New creacionpdf()

    ' ''        Documento.Open()
    ' ''        Documento.NewPage()

    ' ''        ''Documento.Add(lineablanco)
    ' ''        ''Documento.Add(lineablanco)

    ' ''        writer.PageEvent = ev

    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)

    ' ''        Dim tablacv As New PdfPTable(2)
    ' ''        tablacv.SetWidths(New Single() {160.0F, 40.0F})
    ' ''        Dim codigoblan As New PdfPCell(New Phrase(" "))
    ' ''        codigoblan.Border = 0
    ' ''        'codigoblan.BackgroundColor = New BaseColor(230, 230, 230)
    ' ''        codigoblan.HorizontalAlignment = Element.ALIGN_RIGHT
    ' ''        tablacv.AddCell(codigoblan)

    ' ''        Dim codigoveh As New PdfPCell(New Phrase("C.V. " & Me.Vehiculo.Text, fontcv))
    ' ''        codigoveh.Border = 0
    ' ''        codigoveh.BackgroundColor = New BaseColor(230, 230, 230)
    ' ''        codigoveh.HorizontalAlignment = Element.ALIGN_RIGHT
    ' ''        tablacv.AddCell(codigoveh)
    ' ''        Documento.Add(tablacv)

    ' ''        'Dim paragraph As New iTextSharp.text.Paragraph(Space(190) & "C.V. " & Me.Vehiculo.Text, fontBold)
    ' ''        'Documento.Add(paragraph)
    ' ''        Documento.Add(lineablanco)
    ' ''        Dim nombre As New iTextSharp.text.Paragraph(espacio2 & "Certifica que el Sr.(a). " & Me.Nombre.Text, fontcliente)

    ' ''        Documento.Add(nombre)
    ' ''        Documento.Add(lineablanco)
    ' ''        Dim linea1 As New iTextSharp.text.Paragraph(espacio & "Ha adquirido los siguientes sistemas:", fontNormal)

    ' ''        Documento.Add(linea1)
    ' ''        Documento.Add(lineablanco)



    ' ''        ' Impresion de los productos , se genera tabla
    ' ''        ' estaba fontbold
    ' ''        Dim tabla As New PdfPTable(4)
    ' ''        tabla.SetWidths(New Single() {40.0F, 15.0F, 18.0F, 10.0F})
    ' ''        Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontnegro))
    ' ''        titulo1.Border = 0
    ' ''        'titulo1.Border = 2
    ' ''        titulo1.BackgroundColor = New BaseColor(230, 230, 230)
    ' ''        'titulo1.BackgroundColor = Font(230, 230, 230)
    ' ''        'titulo1.BorderColor = New BaseColor(230, 230, 230)
    ' ''        titulo1.HorizontalAlignment = Element.ALIGN_CENTER
    ' ''        tabla.AddCell(titulo1)

    ' ''        Dim titulo2 As New PdfPCell(New Phrase("COBERTURA", fontnegro))
    ' ''        titulo2.Border = 0
    ' ''        titulo2.BackgroundColor = New BaseColor(230, 230, 230)
    ' ''        titulo2.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabla.AddCell(titulo2)

    ' ''        Dim titulo3 As New PdfPCell(New Phrase("P.V.P INCLUIDO IVA", fontnegro))
    ' ''        titulo3.Border = 0
    ' ''        titulo3.BackgroundColor = New BaseColor(230, 230, 230)
    ' ''        titulo3.HorizontalAlignment = Element.ALIGN_RIGHT
    ' ''        tabla.AddCell(titulo3)

    ' ''        Dim titulo4 As New PdfPCell(New Phrase(" ", fontnegro))
    ' ''        titulo4.Border = 0
    ' ''        'titulo4.BackgroundColor = New BaseColor(230, 230, 230)
    ' ''        titulo4.HorizontalAlignment = Element.ALIGN_RIGHT
    ' ''        tabla.AddCell(titulo4)

    ' ''        If dtconsulta2.Tables(0).Rows.Count > 0 Then
    ' ''            For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
    ' ''                Dim det1 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("PRODUCTO").ToString(), fontNormal))
    ' ''                det1.Border = 0
    ' ''                det1.HorizontalAlignment = Element.ALIGN_LEFT

    ' ''                tabla.AddCell(det1)

    ' ''                Dim fechafinal As String
    ' ''                Dim valorfecha As String
    ' ''                valorfecha = dtconsulta2.Tables(0).Rows(i)("COBERTURA").ToString()
    ' ''                If Mid(valorfecha, 4, 2) = "01" Then fechafinal = Mid(valorfecha, 1, 3) & "ene/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "02" Then fechafinal = Mid(valorfecha, 1, 3) & "feb/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "03" Then fechafinal = Mid(valorfecha, 1, 3) & "mar/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "04" Then fechafinal = Mid(valorfecha, 1, 3) & "abr/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "05" Then fechafinal = Mid(valorfecha, 1, 3) & "may/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "06" Then fechafinal = Mid(valorfecha, 1, 3) & "jun/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "07" Then fechafinal = Mid(valorfecha, 1, 3) & "jul/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "08" Then fechafinal = Mid(valorfecha, 1, 3) & "ago/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "09" Then fechafinal = Mid(valorfecha, 1, 3) & "sep/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "10" Then fechafinal = Mid(valorfecha, 1, 3) & "oct/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "11" Then fechafinal = Mid(valorfecha, 1, 3) & "nov/" & Mid(valorfecha, 7, 4)
    ' ''                If Mid(valorfecha, 4, 2) = "12" Then fechafinal = Mid(valorfecha, 1, 3) & "dic/" & Mid(valorfecha, 7, 4)

    ' ''                Dim det2 As New PdfPCell(New Phrase(fechafinal, fontNormal))
    ' ''                det2.Border = 0
    ' ''                det2.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''                tabla.AddCell(det2)

    ' ''                Dim det3 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("PRECIO_PRODUCTO").ToString(), fontNormal))
    ' ''                det3.Border = 0
    ' ''                det3.HorizontalAlignment = Element.ALIGN_RIGHT
    ' ''                tabla.AddCell(det3)

    ' ''                Dim det4 As New PdfPCell(New Phrase(" ", fontNormal))
    ' ''                det4.Border = 0
    ' ''                det4.HorizontalAlignment = Element.ALIGN_RIGHT
    ' ''                tabla.AddCell(det4)

    ' ''            Next
    ' ''        End If
    ' ''        Documento.Add(tabla)

    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)
    ' ''        'Dim linea2 As New iTextSharp.text.Paragraph("    Estos sistemas se encuentran instalados en el vehículo/barco/avión/cajero con las siguientes características:", fontNormal)
    ' ''        Dim linea2 As New iTextSharp.text.Paragraph(espacio & "Estos sistemas se encuentran instalados en el (Vehículo/Barco/Avión/Cajero) con las siguientes características:", fontNormal)
    ' ''        Documento.Add(linea2)
    ' ''        Documento.Add(lineablanco)

    ' ''        Dim tabladatos As New PdfPTable(4)
    ' ''        tabladatos.SetWidths(New Single() {20.0F, 105.0F, 20.0F, 65.0F})

    ' ''        ' fontnegro  para titulo,  fontgris para dato
    ' ''        Dim vehic1 As New PdfPCell(New Phrase("MARCA", fontnegro))
    ' ''        'Dim vehic1 As New PdfPCell(New Phrase("MARCA", fontBold))
    ' ''        'Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)
    ' ''        vehic1.Border = 0
    ' ''        vehic1.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic1)
    ' ''        Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontgris))
    ' ''        'Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontNormal))
    ' ''        vehic2.Border = 0
    ' ''        vehic2.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic2)
    ' ''        Dim vehic3 As New PdfPCell(New Phrase("AÑO", fontnegro))
    ' ''        vehic3.Border = 0
    ' ''        vehic3.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic3)
    ' ''        Dim vehic4 As New PdfPCell(New Phrase(Me.Anio.Text, fontgris))
    ' ''        vehic4.Border = 0
    ' ''        'estaba fontnormal
    ' ''        vehic4.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic4)

    ' ''        Dim vehic5 As New PdfPCell(New Phrase("MODELO", fontnegro))
    ' ''        vehic5.Border = 0
    ' ''        vehic5.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic5)
    ' ''        Dim vehic6 As New PdfPCell(New Phrase(Me.Modelo.Text, fontgris))
    ' ''        vehic6.Border = 0
    ' ''        vehic6.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic6)
    ' ''        Dim vehic7 As New PdfPCell(New Phrase("PLACA", fontnegro))
    ' ''        vehic7.Border = 0
    ' ''        vehic7.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic7)
    ' ''        Dim vehic8 As New PdfPCell(New Phrase(Me.Placa.Text, fontgris))
    ' ''        vehic8.Border = 0
    ' ''        vehic8.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic8)

    ' ''        Dim vehic9 As New PdfPCell(New Phrase("TIPO", fontnegro))
    ' ''        vehic9.Border = 0
    ' ''        vehic9.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic9)
    ' ''        Dim vehic10 As New PdfPCell(New Phrase(Me.Tipo.Text, fontgris))
    ' ''        vehic10.Border = 0
    ' ''        vehic10.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic10)
    ' ''        Dim vehic11 As New PdfPCell(New Phrase("CHASIS", fontnegro))
    ' ''        vehic11.Border = 0
    ' ''        vehic11.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic11)
    ' ''        Dim vehic12 As New PdfPCell(New Phrase(Me.Chasis.Text, fontgris))
    ' ''        vehic12.Border = 0
    ' ''        vehic12.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic12)

    ' ''        Dim vehic13 As New PdfPCell(New Phrase("COLOR", fontnegro))
    ' ''        vehic13.Border = 0
    ' ''        vehic13.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic13)
    ' ''        Dim vehic14 As New PdfPCell(New Phrase(Me.Color.Text, fontgris))
    ' ''        vehic14.Border = 0
    ' ''        vehic14.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic14)
    ' ''        Dim vehic15 As New PdfPCell(New Phrase("MOTOR", fontnegro))
    ' ''        vehic15.Border = 0
    ' ''        vehic15.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic15)
    ' ''        Dim vehic16 As New PdfPCell(New Phrase(Me.Motor.Text, fontgris))
    ' ''        vehic16.Border = 0
    ' ''        vehic16.HorizontalAlignment = Element.ALIGN_LEFT
    ' ''        tabladatos.AddCell(vehic16)

    ' ''        Documento.Add(tabladatos)

    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)

    ' ''        ' Imprime el codigo de barra
    ' ''        Dim codbarra As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(Session("nombrebarra")))
    ' ''        codbarra.ScalePercent(75.0F)
    ' ''        Documento.Add(codbarra)
    ' ''        '
    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)
    ' ''        ' Fecha de emisión del pdf
    ' ''        Me.lblfechahoraemision.Text = espacio & Me.lblfechahoraemision.Text
    ' ''        Dim fecha As New iTextSharp.text.Paragraph(Me.lblfechahoraemision.Text, fontNormal)
    ' ''        Documento.Add(fecha)

    ' ''        Documento.Add(lineablanco)
    ' ''        Documento.Add(lineablanco)

    ' ''        ' Imprime logo del fin de pagina 
    ' ''        Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_footer_certificado.jpg"))
    ' ''        logo.ScalePercent(75.0F)
    ' ''        'logo.HorizontalAlignment = Element.ALIGN_CENTER
    ' ''        Documento.Add(logo)

    ' ''        Documento.Close()
    ' ''        Documento.Dispose()

    ' ''    Catch ex As Exception
    ' ''        Enviar_Email(ex, CType(Application("usuario_email"), String))
    ' ''    End Try
    ' ''End Sub

    Private Function GenerateRandomFilePdf(ByVal folderPath As String) As String
        Dim chars As String = "2346789ABCDEFGHJKLMNPQRTUVWXYZabcdefghjkmnpqrtuvwxyz"
        Dim rnd As New Random()
        Dim name As String = "cert_"
        Try
            Do
                'name = String.Empty
                While name.Length < 10
                    name += chars.Substring(rnd.[Next](chars.Length), 1)
                End While
                ''name += ".png"
                name += ".pdf"
            Loop While File.Exists(folderPath & name)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
        Return name
    End Function


End Class
