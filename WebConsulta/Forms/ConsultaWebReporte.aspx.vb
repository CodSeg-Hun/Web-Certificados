
Imports System.Collections.Generic
Imports System.Data
Imports System.IO
Imports System.Linq
Imports System.Net
Imports System.Net.Mail
Imports System.Reflection
Imports DevExpress.CodeParser
Imports DevExpress.PivotGrid.CriteriaVisitors
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Libreria
Imports MessagingToolkit.Barcode
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class ConsultaWebReporte
    Inherits System.Web.UI.Page
    'Public tempFileName As String = String.Empty
    Dim datVehiculo As Int32
    Dim datOs As Int32
    Dim datCliente As String
    Dim contParmBlank As Integer = 0

    Private Shared barcodeFormats As New Dictionary(Of String, BarcodeFormat)() From {
       {"QR Code", BarcodeFormat.QRCode},
       {"Data Matrix", BarcodeFormat.DataMatrix},
       {"PDF417", BarcodeFormat.PDF417},
       {"Aztec", BarcodeFormat.Aztec},
       {"Bookland/ISBN", BarcodeFormat.Bookland},
       {"Codabar", BarcodeFormat.Codabar},
       {"Code 11", BarcodeFormat.Code11},
       {"Code 128", BarcodeFormat.Code128},
       {"Code 128-A", BarcodeFormat.Code128A},
       {"Code 128-B", BarcodeFormat.Code128B},
       {"Code 128-C", BarcodeFormat.Code128C},
       {"Code 39", BarcodeFormat.Code39},
       {"Code 39 Extended", BarcodeFormat.Code39Extended},
       {"Code 93", BarcodeFormat.Code93},
       {"EAN-13", BarcodeFormat.EAN13},
       {"EAN-8", BarcodeFormat.EAN8},
       {"FIM", BarcodeFormat.FIM},
       {"Interleaved 2 of 5", BarcodeFormat.Interleaved2of5},
       {"ITF-14", BarcodeFormat.ITF14},
       {"LOGMARS", BarcodeFormat.LOGMARS},
       {"MSI 2 Mod 10", BarcodeFormat.MSI2Mod10},
       {"MSI Mod 10", BarcodeFormat.MSIMod10},
       {"MSI Mod 11", BarcodeFormat.MSIMod11},
       {"MSI Mod 11 Mod 10", BarcodeFormat.MSIMod11Mod10},
       {"PostNet", BarcodeFormat.PostNet},
       {"Plessey", BarcodeFormat.ModifiedPlessey},
       {"Standard 2 of 5", BarcodeFormat.Standard2of5},
       {"Telepen", BarcodeFormat.Telepen},
       {"UPC 2 Digit Ext.", BarcodeFormat.UPCSupplemental2Digit},
       {"UPC 5 Digit Ext.", BarcodeFormat.UPCSupplemental5Digit},
       {"UPC-A", BarcodeFormat.UPCA},
       {"UPC-E", BarcodeFormat.UPCE}
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
                    'Session("DetalleVehiculo") = Nothing
                    Detalle2("Link")
                Else
                    'Session("DetalleVehiculo") = Nothing
                    Detalle2("Nuevo")
                End If
                'Session("DetalleVehiculo") = Nothing
                'Detalle()
                TituloReporte()
                'CargaImagenQR()
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

    Private Sub Detalle2(ByVal tipo As String)
        Try
            Dim obj As New ConsultaWeb
            'Dim dtDetalleFlujo2 As New System.Data.DataSet
            'dtDetalleFlujo2 = obj.ConsultaDetalleFormaPago(empresa, proceso, Session("rpt_flj_cja_det_frm_pago_anio"))
            Dim dtvehiculo As New System.Data.DataSet
            Dim dtVehiculoDetalle As New System.Data.DataSet
            Dim dtconsulta As New System.Data.DataSet
            If tipo = "Nuevo" Then
                dtvehiculo = Session("Detalle")
                dtVehiculoDetalle = Session("producto")
                If dtvehiculo.Tables(0).Rows.Count > 0 Then
                    'Me.Vehiculo.Text = dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                    'If dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "C" Then
                    '    Image1.ImageUrl = "../Images/background_banner_certificado.jpg"
                    'ElseIf dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "D" Then
                    '    Image1.ImageUrl = "../Images/background_banner_certificado_des.jpg"
                    'ElseIf dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "I" Then
                    '    Image1.ImageUrl = "../Images/background_banner_certificado_ins.jpg"
                    'End If
                    If Session("cbm_tipo") = "8" Then ' Certificado de Venta
                        Image1.ImageUrl = "../Images/background_banner_certificado3.jpg"
                        grid.Visible = True
                        tex01.Visible = True
                        tex04.Visible = False
                        tex03.Visible = False
                        tex02.Visible = True
                        tex05.Visible = True
                        tex06.Visible = True
                        tex07.Visible = True
                    ElseIf Session("cbm_tipo") = "2" Then ' Certificado de Desinstalación
                        grid.Visible = False
                        tex01.Visible = False
                        tex04.Visible = True
                        tex03.Visible = False
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        tex07.Visible = False
                        Image1.ImageUrl = "../Images/background_banner_certificado_des.jpg"
                    ElseIf Session("cbm_tipo") = "1" Then ' Certificado de Instalación
                        grid.Visible = True
                        tex01.Visible = True
                        tex04.Visible = False
                        tex03.Visible = True
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        tex07.Visible = True
                        grdproductosdetalle.MasterTableView.GetColumn("PRODUCTO").Display = True
                        grdproductosdetalle.MasterTableView.GetColumn("FECHAINSTALACION").Display = True
                        grdproductosdetalle.MasterTableView.GetColumn("COBERTURA").Display = True
                        grdproductosdetalle.Width = Unit.Pixel(670)
                        Image1.ImageUrl = "../Images/background_banner_certificado_ins3.jpg"
                    End If
                    'dtconsulta = obj.ConsultarImprimir(dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtdetalle.Tables(0).Rows(0)("CLIENTE").ToString(), Session("user_id"), dtdetalle.Tables(0).Rows(0)("TIPO").ToString())
                    'dtconsulta = obj.ConsultarImprimir(dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtdetalle.Tables(0).Rows(0)("CLIENTE").ToString(), Session("user_id"), Session("cbm_tipo"))
                    'Session("infoqr") = dtconsulta
                    If dtvehiculo.Tables(0).Rows.Count > 0 Then
                        Me.Nombre.Text = dtvehiculo.Tables(0).Rows(0)("CLIENTE").ToString().ToUpper()
                        Me.Vehiculo.Text = dtvehiculo.Tables(0).Rows(0)("idVehiculo").ToString()
                        ''Me.Producto.Text = dtconsulta.Tables(0).Rows(0)("PRODUCTO").ToString()
                        ''Me.Cobertura.Text = dtconsulta.Tables(0).Rows(0)("COBERTURA").ToString()
                        Me.Marca.Text = dtvehiculo.Tables(0).Rows(0)("MARCA").ToString()
                        Me.Anio.Text = dtvehiculo.Tables(0).Rows(0)("ANIO").ToString()
                        Me.Modelo.Text = dtvehiculo.Tables(0).Rows(0)("MODELO").ToString()
                        Me.Placa.Text = dtvehiculo.Tables(0).Rows(0)("PLACA").ToString()
                        Me.Tipo.Text = dtvehiculo.Tables(0).Rows(0)("TIPO").ToString()
                        Me.Chasis.Text = dtvehiculo.Tables(0).Rows(0)("CHASIS").ToString()
                        Me.Color.Text = dtvehiculo.Tables(0).Rows(0)("COLOR").ToString()
                        Me.Motor.Text = dtvehiculo.Tables(0).Rows(0)("MOTOR").ToString()
                        ''Me.precioproducto.Text = dtconsulta.Tables(0).Rows(0)("PRECIO_PRODUCTO").ToString()

                        Me.grdproductosdetalle.DataSource = dtVehiculoDetalle
                        'Session("datocliente") = Me.Nombre.Text + " | " + Me.Marca.Text + " | " + Me.Modelo.Text + " | " + Me.Chasis.Text + " | " + Me.Placa.Text
                    End If
                End If
            Else
                ' *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-
                ' GENERACIÓN DE PDF desde LINK
                'dtconsulta = obj.ConsultarImprimir(datVehiculo, datCliente, Session("user_id"), "C")
                'Session("infoqr") = dtconsulta
                If dtvehiculo.Tables(0).Rows.Count > 0 Then
                    Me.Nombre.Text = dtvehiculo.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.Vehiculo.Text = dtvehiculo.Tables(0).Rows(0)("idVehiculo").ToString()
                    Me.Marca.Text = dtvehiculo.Tables(0).Rows(0)("MARCA").ToString()
                    Me.Anio.Text = dtvehiculo.Tables(0).Rows(0)("ANIO").ToString()
                    Me.Modelo.Text = dtvehiculo.Tables(0).Rows(0)("MODELO").ToString()
                    Me.Placa.Text = dtvehiculo.Tables(0).Rows(0)("PLACA").ToString()
                    Me.Tipo.Text = dtvehiculo.Tables(0).Rows(0)("TIPO").ToString()
                    Me.Chasis.Text = dtvehiculo.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.Color.Text = dtvehiculo.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Motor.Text = dtvehiculo.Tables(0).Rows(0)("MOTOR").ToString()
                    Me.grdproductosdetalle.DataSource = dtVehiculoDetalle
                End If
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
                        'Me.Producto.Text = dtconsulta.Tables(0).Rows(0)("PRODUCTO").ToString()
                        'Me.Cobertura.Text = dtconsulta.Tables(0).Rows(0)("COBERTURA").ToString()
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

    Function ConvertToDataSet(ByVal obj As Object) As DataSet
        Dim dataSet As New DataSet()
        Dim dataTable As New DataTable(obj.GetType().Name)

        ' Obtener propiedades del objeto
        Dim properties As PropertyInfo() = obj.GetType().GetProperties()

        ' Crear columnas en DataTable
        For Each prop As PropertyInfo In properties
            dataTable.Columns.Add(prop.Name, GetType(String))
        Next

        ' Crear una fila en DataTable
        Dim dataRow As DataRow = dataTable.NewRow()
        For Each prop As PropertyInfo In properties
            dataRow(prop.Name) = prop.GetValue(obj, Nothing)?.ToString()
        Next
        dataTable.Rows.Add(dataRow)

        ' Añadir DataTable al DataSet
        dataSet.Tables.Add(dataTable)

        Return dataSet
    End Function

    Protected Sub BtnCorreo_Click(sender As Object, e As EventArgs) Handles BtnCorreo.Click
        Dim script As String = "document.getElementById('op').style.display = 'block';" ' o 'none'
        ClientScript.RegisterStartupScript(Me.GetType(), "MostrarOcultarDiv", script, True)
    End Sub

    Function Catalogo(ByVal texto As String, Optional ByVal valorDevolver As String = "C") As String
        Catalogo = String.Empty
        Try
            ' Se genera 2 registros
            Dim cadenas() As String = texto.ToString.Split(";"c)
            Select Case valorDevolver
                Case "C"
                    Catalogo = cadenas(0)
                Case "D"
                    If cadenas.Length > 1 Then
                        Catalogo = cadenas(1)
                    End If
                Case Else
                    Throw New Exception("No existe programada esa función ")
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Protected Sub GeneraPdf_Click(origen As String)
        Try
            Dim dtvehiculo As New System.Data.DataSet
            Dim dtVehiculoDetalle As New System.Data.DataSet
            dtvehiculo = Session("Detalle")
            dtVehiculoDetalle = Session("producto")
            If dtvehiculo.Tables(0).Rows.Count > 0 Then
                Me.Nombre.Text = dtvehiculo.Tables(0).Rows(0)("CLIENTE").ToString().ToUpper()
                Me.Vehiculo.Text = dtvehiculo.Tables(0).Rows(0)("idVehiculo").ToString()
                ''Me.Producto.Text = dtconsulta.Tables(0).Rows(0)("PRODUCTO").ToString()
                ''Me.Cobertura.Text = dtconsulta.Tables(0).Rows(0)("COBERTURA").ToString()
                Me.Marca.Text = dtvehiculo.Tables(0).Rows(0)("MARCA").ToString()
                Me.Anio.Text = dtvehiculo.Tables(0).Rows(0)("ANIO").ToString()
                Me.Modelo.Text = dtvehiculo.Tables(0).Rows(0)("MODELO").ToString()
                Me.Placa.Text = dtvehiculo.Tables(0).Rows(0)("PLACA").ToString()
                Me.Tipo.Text = dtvehiculo.Tables(0).Rows(0)("TIPO").ToString()
                Me.Chasis.Text = dtvehiculo.Tables(0).Rows(0)("CHASIS").ToString()
                Me.Color.Text = dtvehiculo.Tables(0).Rows(0)("COLOR").ToString()
                Me.Motor.Text = dtvehiculo.Tables(0).Rows(0)("MOTOR").ToString()
                ''Me.precioproducto.Text = dtconsulta.Tables(0).Rows(0)("PRECIO_PRODUCTO").ToString()

                Me.grdproductosdetalle.DataSource = dtVehiculoDetalle
                'Session("datocliente") = Me.Nombre.Text + " | " + Me.Marca.Text + " | " + Me.Modelo.Text + " | " + Me.Chasis.Text + " | " + Me.Placa.Text
            End If

            Dim fontNormal As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL)
            Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
            Dim fontcv As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
            Dim fontProducto As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD)
            Dim fontTitulo As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD)
            Dim grey As New BaseColor(64, 64, 64)
            Dim negro As New BaseColor(0, 0, 0)
            Dim fontgris As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, grey)
            Dim fontnegro As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, negro)
            Dim espacio As String = Space(8)
            Dim espacio2 As String = Space(6)
            Dim dtconsulta2 As New System.Data.DataSet
            dtconsulta2 = Session("producto")
            Dim documento As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim file As String = "\\10.100.107.14\ImagenesDocumentos\cert_" & Me.Vehiculo.Text & ".pdf"
            Session("nombrepdf") = file
            Dim writer As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(file, FileMode.Create))
            Dim lineablanco As New iTextSharp.text.Paragraph(" ")

            If origen = "NOR" Then
                If Session("cbm_tipo") = "C" Or Session("cbm_tipo") = "8" Then '' VENTA
                    Dim ev As New creacionpdf()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf Session("cbm_tipo") = "D" Or Session("cbm_tipo") = "2" Then '' DESINSTALACIÓN
                    Dim ev As New creacionpdf_des()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf Session("cbm_tipo") = "I" Or Session("cbm_tipo") = "1" Then '' INSTALACION
                    Dim ev As New creacionpdf_ins()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                End If
            End If
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            Dim tablacv As New PdfPTable(2)
            tablacv.SetWidths(New Single() {160.0F, 40.0F})

            Dim codigoblan As New PdfPCell(New Phrase(" "))
            codigoblan.Border = 0
            'codigoblan.BackgroundColor = New BaseColor(230, 230, 230)
            codigoblan.HorizontalAlignment = Element.ALIGN_RIGHT
            tablacv.AddCell(codigoblan)
            documento.Add(lineablanco)
            Dim colorBorde As New BaseColor(203, 209, 212)
            Dim codigoveh As New PdfPCell(New Phrase("C.V. " & Me.Vehiculo.Text, fontcv))
            codigoveh.BackgroundColor = BaseColor.WHITE
            codigoveh.BorderColor = colorBorde
            codigoveh.Padding = 5
            'codigoveh.Border = 0
            'codigoveh.BackgroundColor = New BaseColor(230, 230, 230)
            codigoveh.HorizontalAlignment = Element.ALIGN_CENTER
            tablacv.AddCell(codigoveh)
            documento.Add(tablacv)
            documento.Add(lineablanco)
            If Session("cbm_tipo") = "C" Or Session("cbm_tipo") = "8" Then
                Dim textoParrafo As New PdfPTable(1)
                textoParrafo.SetWidths(New Single() {200.0F})
                Dim phrase As New Phrase()
                phrase.Add(New Chunk("Certifica que el Sr. (a) ", fontNormal)) ' Texto normal
                phrase.Add(New Chunk(Me.Nombre.Text.ToUpper(), fontcliente)) ' Nombre en negrita
                Dim tituloText1 As New PdfPCell(phrase)
                'Dim tituloText1 As New PdfPCell(New Phrase("Certifica que el Sr. (a) " & Me.TxtConCliente.Text.ToUpper(), fontcliente))
                tituloText1.Border = 0
                textoParrafo.AddCell(tituloText1)
                textoParrafo.AddCell(codigoblan)
                textoParrafo.AddCell(codigoblan)
                'textoParrafo.AddCell(lineablanco)
                Dim tituloText2 As New PdfPCell(New Phrase("Ha adquirido los siguientes sistemas:", fontNormal))
                'Dim tituloText2 As New PdfPCell(New Phrase("Ha adquirido los siguientes sistemas:", fontNormal))
                tituloText2.Border = 0
                textoParrafo.AddCell(tituloText2)
                textoParrafo.AddCell(codigoblan)
                documento.Add(textoParrafo)
                Dim tabla As New PdfPTable(2)
                tabla.SetWidths(New Single() {50.0F, 50.0F}) ' Ambas columnas con 50% del ancho
                Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontProducto))
                titulo1.HorizontalAlignment = Element.ALIGN_CENTER
                titulo1.BackgroundColor = BaseColor.WHITE
                titulo1.BorderColor = colorBorde
                titulo1.Padding = 5
                tabla.AddCell(titulo1)

                ' Celda vacía en la segunda columna (sin bordes visibles)
                Dim celdaVacia As New PdfPCell(New Phrase(" "))
                celdaVacia.Border = PdfPCell.NO_BORDER
                tabla.AddCell(celdaVacia)

                ' Verificar si hay datos en la tabla
                If dtconsulta2.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                        ' Descripción del producto en la primera columna
                        Dim descripcion As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("descripcion").ToString(), fontNormal))
                        descripcion.BackgroundColor = BaseColor.WHITE
                        descripcion.BorderColor = colorBorde
                        descripcion.HorizontalAlignment = Element.ALIGN_CENTER
                        descripcion.Padding = 5
                        tabla.AddCell(descripcion)

                        ' Segunda columna vacía sin bordes visibles
                        Dim celdaVacia2 As New PdfPCell(New Phrase(" "))
                        celdaVacia2.Border = PdfPCell.NO_BORDER
                        tabla.AddCell(celdaVacia2)
                    Next
                End If

                ' Agregar la tabla al documento
                documento.Add(tabla)

            End If
            If Session("cbm_tipo") = "I" Or Session("cbm_tipo") = "1" Then
                Dim textoParrafo As New PdfPTable(1)
                textoParrafo.SetWidths(New Single() {200.0F})
                Dim phrase As New Phrase()
                phrase.Add(New Chunk("Certifica que el Sr. (a) ", fontNormal)) ' Texto normal
                phrase.Add(New Chunk(Me.Nombre.Text.ToUpper(), fontcliente)) ' Nombre en negrita
                Dim tituloText1 As New PdfPCell(phrase)
                tituloText1.Border = 0
                textoParrafo.AddCell(tituloText1)
                textoParrafo.AddCell(codigoblan)
                'textoParrafo.AddCell(lineablanco)
                Dim tituloText2 As New PdfPCell(New Phrase("Ha adquirido los siguientes sistemas:", fontNormal))
                tituloText2.Border = 0
                textoParrafo.AddCell(tituloText2)
                textoParrafo.AddCell(codigoblan)
                documento.Add(textoParrafo)
                Dim tabla As New PdfPTable(3)
                tabla.SetWidths(New Single() {44.0F, 22.0F, 22.0F}) ' Distribución de columnas

                Dim tituloProducto As New PdfPCell(New Phrase("PRODUCTO", fontProducto))
                tituloProducto.HorizontalAlignment = Element.ALIGN_CENTER
                tituloProducto.BackgroundColor = BaseColor.WHITE
                tituloProducto.BorderColor = colorBorde
                tituloProducto.Padding = 5
                tabla.AddCell(tituloProducto)

                Dim tituloInstalacion As New PdfPCell(New Phrase("FECHA DE INSTALACIÓN", fontProducto))
                tituloInstalacion.HorizontalAlignment = Element.ALIGN_CENTER
                tituloInstalacion.BackgroundColor = BaseColor.WHITE
                tituloInstalacion.BorderColor = colorBorde
                tituloInstalacion.Padding = 5
                tabla.AddCell(tituloInstalacion)

                Dim tituloVigencia As New PdfPCell(New Phrase("VIGENCIA DE SERVICIO", fontProducto))
                tituloVigencia.HorizontalAlignment = Element.ALIGN_CENTER
                tituloVigencia.BackgroundColor = BaseColor.WHITE
                tituloVigencia.BorderColor = colorBorde
                tituloVigencia.Padding = 5
                tabla.AddCell(tituloVigencia)

                If dtconsulta2.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                        ' Descripción del producto
                        Dim descripcion As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("descripcion").ToString(), fontNormal))
                        descripcion.BackgroundColor = BaseColor.WHITE
                        descripcion.BorderColor = colorBorde
                        descripcion.HorizontalAlignment = Element.ALIGN_CENTER
                        descripcion.Padding = 5
                        tabla.AddCell(descripcion)

                        ' Fecha de instalación
                        Dim fechainstalacion As String = ObtenerFormatoFecha(dtconsulta2.Tables(0).Rows(i)("fecha_inicial").ToString())
                        Dim detFechaInstalacion As New PdfPCell(New Phrase(fechainstalacion, fontNormal))
                        detFechaInstalacion.BackgroundColor = BaseColor.WHITE
                        detFechaInstalacion.BorderColor = colorBorde
                        detFechaInstalacion.HorizontalAlignment = Element.ALIGN_CENTER
                        detFechaInstalacion.Padding = 5
                        tabla.AddCell(detFechaInstalacion)

                        ' Fecha de vigencia (instalación - fin)
                        Dim fechafin As String = ObtenerFormatoFecha(dtconsulta2.Tables(0).Rows(i)("fecha_fin").ToString())

                        Dim detVigencia As New PdfPCell(New Phrase(fechafin, fontNormal))
                        detVigencia.BackgroundColor = BaseColor.WHITE
                        detVigencia.BorderColor = colorBorde
                        detVigencia.HorizontalAlignment = Element.ALIGN_CENTER
                        detVigencia.Padding = 5
                        tabla.AddCell(detVigencia)
                    Next
                End If
                documento.Add(tabla)
            End If

            Dim texto2 As String = ""
            Dim texto3 As String = ""
            documento.Add(lineablanco)

            If Session("cbm_tipo") = "C" Or Session("cbm_tipo") = "8" Then
                texto2 = "Con Orden de Instalación en el vehículo: "
                texto3 = "*La vigencia del servicio se contará desde la instalación del equipo. "
            ElseIf Session("cbm_tipo") = "D" Or Session("cbm_tipo") = "2" Then
                texto2 = "Se certifica que se ha realizado la desinstalación con las siguientes características:"
            ElseIf Session("cbm_tipo") = "I" Or Session("cbm_tipo") = "1" Then
                texto2 = "En el vehículo o embarcación con las siguientes características:"
            End If
            'Dim linea2 As New iTextSharp.text.Paragraph(espacio & texto2, fontNormal)
            'documento.Add(linea2)

            Dim textoParrafo2 As New PdfPTable(1)
            textoParrafo2.SetWidths(New Single() {240.0F})

            If Session("cbm_tipo") = "C" Or Session("cbm_tipo") = "8" Then
                Dim tituloText41 As New PdfPCell(New Phrase(texto3, fontNormal))
                tituloText41.Border = 0
                tituloText41.HorizontalAlignment = Element.ALIGN_LEFT
                textoParrafo2.AddCell(tituloText41)
            End If
            textoParrafo2.AddCell(codigoblan)
            Dim tituloText4 As New PdfPCell(New Phrase(texto2, fontNormal))
            tituloText4.Border = 0
            textoParrafo2.AddCell(tituloText4)
            documento.Add(textoParrafo2)
            documento.Add(lineablanco)
            Dim tabladatos As New PdfPTable(4)
            tabladatos.SetWidths(New Single() {20.0F, 105.0F, 20.0F, 65.0F})
            Dim vehic7 As New PdfPCell(New Phrase("PLACA", fontnegro))
            vehic7.Border = 0
            vehic7.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic7)
            'Dim vehic8 As New PdfPCell(New Phrase(Me.Placa.Text, fontgris))
            Dim vehic8 As New PdfPCell(New Phrase(Me.Placa.Text, fontgris))
            vehic8.Border = 0
            vehic8.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic8)
            Dim vehic13 As New PdfPCell(New Phrase("COLOR", fontnegro))
            vehic13.Border = 0
            vehic13.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic13)
            'Dim vehic14 As New PdfPCell(New Phrase(Me.Color.Text, fontgris))
            Dim vehic14 As New PdfPCell(New Phrase(Me.Color.Text, fontgris))
            vehic14.Border = 0
            vehic14.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic14)
            ' fontnegro  para titulo,  fontgris para dato
            Dim vehic1 As New PdfPCell(New Phrase("MARCA", fontnegro))
            'Dim vehic1 As New PdfPCell(New Phrase("MARCA", fontBold))
            'Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)
            vehic1.Border = 0
            vehic1.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic1)
            'Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontgris))
            Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontgris))
            'Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontNormal))
            vehic2.Border = 0
            vehic2.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic2)
            Dim vehic3 As New PdfPCell(New Phrase("AÑO", fontnegro))
            vehic3.Border = 0
            vehic3.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic3)
            Dim vehic4 As New PdfPCell(New Phrase(Me.Anio.Text, fontgris))
            'Dim vehic4 As New PdfPCell(New Phrase(txtanio, fontgris))
            vehic4.Border = 0
            'estaba fontnormal
            vehic4.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic4)
            Dim vehic5 As New PdfPCell(New Phrase("MODELO", fontnegro))
            vehic5.Border = 0
            vehic5.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic5)
            'Dim vehic6 As New PdfPCell(New Phrase(Me.Modelo.Text, fontgris))
            Dim vehic6 As New PdfPCell(New Phrase(Me.Modelo.Text, fontgris))
            vehic6.Border = 0
            vehic6.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic6)
            Dim vehic15 As New PdfPCell(New Phrase("MOTOR", fontnegro))
            vehic15.Border = 0
            vehic15.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic15)
            'Dim vehic16 As New PdfPCell(New Phrase(Me.Motor.Text, fontgris))
            Dim vehic16 As New PdfPCell(New Phrase(Me.Motor.Text, fontgris))
            vehic16.Border = 0
            vehic16.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic16)
            Dim vehic9 As New PdfPCell(New Phrase("TIPO", fontnegro))
            vehic9.Border = 0
            vehic9.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic9)
            'Dim vehic10 As New PdfPCell(New Phrase(Me.Tipo.Text, fontgris))
            Dim vehic10 As New PdfPCell(New Phrase(Me.Tipo.Text, fontgris))
            vehic10.Border = 0
            vehic10.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic10)
            Dim vehic11 As New PdfPCell(New Phrase("CHASIS", fontnegro))
            vehic11.Border = 0
            vehic11.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic11)
            'Dim vehic12 As New PdfPCell(New Phrase(Me.Chasis.Text, fontgris))
            Dim vehic12 As New PdfPCell(New Phrase(Me.Chasis.Text, fontgris))
            vehic12.Border = 0
            vehic12.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic12)
            documento.Add(tabladatos)

            documento.Add(lineablanco)
            If Session("cbm_tipo") = "C" Or Session("cbm_tipo") = "8" Then
                Dim textoParrafo21 As New PdfPTable(1)
                textoParrafo21.SetWidths(New Single() {240.0F})
                Dim tituloText42 As New PdfPCell(New Phrase("El presente certificado, no constituye confirmación de instalación del equipo.", fontNormal))
                tituloText42.Border = 0
                tituloText42.HorizontalAlignment = Element.ALIGN_CENTER
                textoParrafo21.AddCell(tituloText42)
                documento.Add(textoParrafo21)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                Dim textoParrafo22 As New PdfPTable(1)
                textoParrafo22.SetWidths(New Single() {240.0F})
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                Dim tituloText43 As New PdfPCell(New Phrase("Los términos y condiciones del servicio pueden ser consultadas en nuestra página web", fontNormal))
                tituloText43.Border = 0
                tituloText43.HorizontalAlignment = Element.ALIGN_LEFT
                textoParrafo22.AddCell(tituloText43)
                documento.Add(textoParrafo22)
            End If
            If Session("cbm_tipo") = "I" Or Session("cbm_tipo") = "1" Then
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
            End If

            ' Fecha de emisión del pdf
            Dim lblfechahoraemision As String
            lblfechahoraemision = "Emisión: " & Session("nombre_ejecutiva") & " | " & System.DateTime.Now.ToString("dd MMMM yyyy") & " | " & System.DateTime.Now.ToString("H:mm:ss")
            Dim fechatrabajo As String = ""
            fechatrabajo = ObtenerFormatoFecha(dtconsulta2.Tables(0).Rows(0)("fecha_inicial").ToString())
            Dim tablacv2 As New PdfPTable(2)
            tablacv2.SetWidths(New Single() {120.0F, 80.0F})
            If Session("cbm_tipo") = "I" Or Session("cbm_tipo") = "1" Then
                Dim fechaInstalacion = "Fecha Instalación: " & fechatrabajo
                If origen = "CNL" Then
                    fechaInstalacion = ""
                End If
                Dim fecha As New PdfPCell(New Phrase(fechaInstalacion, fontNormal))
                fecha.Border = 0
                fecha.HorizontalAlignment = Element.ALIGN_LEFT
                tablacv2.AddCell(fecha)
                tablacv2.AddCell(codigoblan)
            ElseIf Session("cbm_tipo") = "D" Or Session("cbm_tipo") = "2" Then
                Dim fecha As New PdfPCell(New Phrase("Fecha Desinstalación: " & fechatrabajo, fontNormal))
                fecha.Border = 0
                fecha.HorizontalAlignment = Element.ALIGN_LEFT
                tablacv2.AddCell(fecha)
                tablacv2.AddCell(codigoblan)
            End If
            Dim codigofecha As New PdfPCell(New Phrase(lblfechahoraemision, fontNormal))
            codigofecha.Border = 0
            codigofecha.HorizontalAlignment = Element.ALIGN_LEFT
            tablacv2.AddCell(codigofecha)
            tablacv2.AddCell(codigoblan)
            documento.Add(tablacv2)
            documento.Add(lineablanco)
            If origen = "NOR" Then
                Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_footer_certificado3.jpg"))
                logo.ScalePercent(75.0F)
                documento.Add(logo)
            End If
            documento.Close()
            documento.Dispose()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

    Function ObtenerFormatoFecha(fecha As String) As String
        Dim meses As New Dictionary(Of String, String) From {
            {"01", "Ene"},
            {"02", "Feb"},
            {"03", "Mar"},
            {"04", "Abr"},
            {"05", "May"},
            {"06", "Jun"},
            {"07", "Jul"},
            {"08", "Ago"},
            {"09", "Sep"},
            {"10", "Oct"},
            {"11", "Nov"},
            {"12", "Dic"}
        }

        Dim mes As String = Mid(fecha, 4, 2)
        Dim dia As String = Mid(fecha, 1, 3)
        Dim año As String = Mid(fecha, 7, 4)

        If meses.ContainsKey(mes) Then
            Return dia & meses(mes) & "/" & año
        Else
            Return "Fecha inválida"
        End If
    End Function

    Protected Sub RadButton2_Click(sender As Object, e As EventArgs) Handles RadButton2.Click
        Try
            If (emailDestino.Value <> "") Then

                GeneraPdf_Click("NOR")
                Dim htmlcuerpo As String = ""
                htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body> <br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Venta del Producto</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                If Session("cbm_tipo") = "C" Then
                    htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Venta del Producto Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                ElseIf Session("cbm_tipo") = "D" Then
                    htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Desinstalación del Producto de Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                ElseIf Session("cbm_tipo") = "I" Then
                    htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Instalación del Producto de Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                End If
                'htmlcuerpo = "<HTML> <font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font>  <BODY> <br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Instalación del Producto Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Marca : </font> <font style=""color: #666666;"">" + "&nbsp;" + Me.Marca.Text + "</font> <br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Modelo: </font> <font style=""color: #666666;"">" + Me.Modelo.Text + "</font><br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Chasis: </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + Me.Chasis.Text + "</font><br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Placa : </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + "&nbsp;" + Me.Placa.Text + "</font><br/>"
                'htmlcuerpo = htmlcuerpo + " <h5> Modelo: " + Me.txtConModelo.Text + "</h5>"
                'htmlcuerpo = htmlcuerpo + " <h5> Chasis: " + Me.txtChasis.Text + "</h5>"
                'htmlcuerpo = htmlcuerpo + " <h5> Placa:  " + Me.txtConPlaca.Text + "</h5>"
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + " <font style=""color: #666666;"">Saludos Cordiales</font>"
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;""> Servicio al cliente</font>"
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;""> Carseg S.A.</font></body>"
                htmlcuerpo = htmlcuerpo + "</html>"

                Dim mailMessage As New MailMessage()
                Dim mailAddress As New MailAddress("noticarseg@carsegsa.com")
                mailMessage.From = mailAddress
                mailMessage.IsBodyHtml = True

                If Session("cbm_tipo") = "C" Or Session("cbm_tipo") = "8" Then ' VENTA
                    mailMessage.Subject = "Certificado de Venta del Producto Hunter, " + Session("datocliente")
                ElseIf Session("cbm_tipo") = "D" Or Session("cbm_tipo") = "2" Then ' DESINSTALACION
                    mailMessage.Subject = "Certificado de Desinstalación del Producto Hunter, " + Session("datocliente")
                ElseIf Session("cbm_tipo") = "I" Or Session("cbm_tipo") = "1" Then ' INSTALACION
                    mailMessage.Subject = "Certificado de Instalación del Producto Hunter, " + Session("datocliente")
                End If

                mailMessage.Body = htmlcuerpo
                mailMessage.Priority = MailPriority.High
                Dim attachment As New Attachment(Session("nombrepdf"))
                mailMessage.Attachments.Add(attachment)
                'mailMessage.Bcc.Add("fontaneda@carsegsa.com")
                'mailMessage.Bcc.Add("galvarado@carsegsa.com")
                Dim mailToBcc As String = Application("usuario_email").ToString()
                Dim mailToBccCollection As [String]() = mailToBcc.Split(",")
                For Each mailTooBcc As String In mailToBccCollection
                    mailMessage.Bcc.Add(mailTooBcc)
                Next
                'Dim smtpClient As New SmtpClient("10.100.89.34")
                ' Para enviar 2 destinatarios
                Dim correo1 As String
                Dim correo2 As String = ""
                'correo1 = Catalogo(texto:=Me.Correo.Text)
                'correo2 = Catalogo(texto:=Me.Correo.Text, valorDevolver:="D")
                'mailMessage.To.Add(Me.TxtCorreo.Text)
                mailMessage.To.Add(emailDestino.Value)
                'smtpClient.Send(mailMessage)
                If correo2 <> "" Then
                    mailMessage.To.Add(correo2)
                End If
                'ENVIA PDF
                'nuevo
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                Dim smtp As New SmtpClient(ConfigurationManager.AppSettings.Get("SmptCliente").ToString(), ConfigurationManager.AppSettings.Get("SmptPort"))
                smtp.EnableSsl = True
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                smtp.UseDefaultCredentials = False
                smtp.Credentials = New Net.NetworkCredential(ConfigurationManager.AppSettings.Get("VentasMailUser").ToString(), ConfigurationManager.AppSettings.Get("VentasMailPassword").ToString())
                smtp.Send(mailMessage)
                'SmtpClient.Send(mailMessage)
                'borra el archivo pdf
                mailMessage.Dispose()
                emailDestino.Value = ""
                Dim destino As String = Session("nombrepdf")

                If (System.IO.File.Exists(destino)) Then
                    System.IO.File.Delete(destino)
                End If

                ' Mostrar mensaje de exito
                Dim script As String = "document.getElementById('controlesCorreo').style.display = 'none'; " &
                      "document.getElementById('mensajeExito').style.display = 'block'; " &
                      "setTimeout(function() { " &
                      "   document.getElementById('mensajeExito').style.display = 'none'; " &
                      "   document.getElementById('op').style.display = 'none'; " &
                      "}, 4000);" ' 4000ms = 4 segundos

                ScriptManager.RegisterStartupScript(Me, Me.GetType(), "MostrarOcultarDiv2", script, True)

            End If

        Catch ex As Exception
            Dim a = ex
        End Try
    End Sub
End Class
