Imports System.Collections.Generic
Imports System.Linq
Imports MessagingToolkit.Barcode
Imports System.Data
Imports System.IO

Public Class ConsultaReporteLatam

    Inherits System.Web.UI.Page

    Dim datVehiculo As Int32
    Dim datOs As Int32
    Dim datCliente As String
    Dim contParmBlank As Integer = 0


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


    Private Sub TituloReporte()
        Try
            Me.lbltitulodetfljcja.Text = "Certificado de Instalación"
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
            Dim dtdetalle As New System.Data.DataSet
            Dim dtconsulta As New System.Data.DataSet
            If tipo = "Nuevo" Then
                dtdetalle = Session("Detalle")
                If dtdetalle.Tables(0).Rows.Count > 0 Then
                    dtconsulta = obj.ConsultarImprimir(dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtdetalle.Tables(0).Rows(0)("CLIENTE").ToString(), Session("user_id"), "C")
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
            ElseIf tipo = "LinK" Then
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


    'Private Function GenerateRandomFilePdf(ByVal folderPath As String) As String
    '    Dim chars As String = "2346789ABCDEFGHJKLMNPQRTUVWXYZabcdefghjkmnpqrtuvwxyz"
    '    Dim rnd As New Random()
    '    Dim name As String = "cert_"
    '    Try
    '        Do
    '            While name.Length < 10
    '                name += chars.Substring(rnd.[Next](chars.Length), 1)
    '            End While
    '            name += ".pdf"
    '        Loop While File.Exists(folderPath & name)
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    '    Return name
    'End Function


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
            datOs = dtOsqr.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
            datVehiculo = dtOsqr.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
            datCliente = dtOsqr.Tables(0).Rows(0)("ID_CLIENTE").ToString()
            Dim data As String = "https://www.hunteronline.com.ec/WSConcesionarios/Forms/ConsultaReporteLatam.aspx?os= " & datOs & "&veh=" & datVehiculo & "&cli=" & datCliente
            If String.IsNullOrEmpty(data) Then
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
            barcodeEncoder.SaveImage(Server.MapPath(tempFileName), SaveOptions.Png)
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
                name += ".jpg"
            Loop While File.Exists(folderPath & name)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
        Return name
    End Function


End Class