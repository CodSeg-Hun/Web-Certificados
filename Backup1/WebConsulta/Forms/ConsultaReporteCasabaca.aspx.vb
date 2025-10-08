Imports System.Collections.Generic
Imports System.Linq
Imports MessagingToolkit.Barcode
Imports System.Data
Imports System.IO

Public Class ConsultaReporteCasabaca
    Inherits System.Web.UI.Page

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
                Session("DetalleVehiculo") = Nothing
                Detalle("Nuevo")
                'TituloReporte()
                CargaImagenQR()
                Me.lblfechahoraemision.Text = "Emitido el " & System.DateTime.Now.ToString("dd MMMM yyyy H:mm:ss")
            End If
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
                        textcabecera.Visible = True
                        textcabecera01.Visible = False
                    ElseIf Session("cbm_tipo") = "D" Then
                        grid.Visible = False
                        tex01.Visible = False
                        tex04.Visible = True
                        tex03.Visible = False
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        tex07.Visible = False
                        textcabecera.Visible = False
                        textcabecera01.Visible = True
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
                        textcabecera.Visible = False
                        textcabecera01.Visible = True
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

End Class