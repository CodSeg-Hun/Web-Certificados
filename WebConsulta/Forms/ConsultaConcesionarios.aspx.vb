'Imports Telerik.Web.Data
'Imports Telerik.Web.UI
Imports System.Net.Mail
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports Telerik.Web.UI.GridExcelBuilder


Public Class ConsultaConcesionarios
    Inherits System.Web.UI.Page

    Dim usuario As String
    Dim ipmaquina As String
    Dim pantalla As String

    Public Enum Operacion
        OExistosa = 1
        OInvalida = 2
        CSinDatos = 3
    End Enum


    ''' <summary>
    ''' Motivo: control de sesion
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Control_Sesion()
        Try
            If (Session("user_session") Is Nothing) Then
                Session("alert") = "Debe de iniciar sesión en el sistema"
                Me.Page.Response.Redirect("404.aspx", False)
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("user_id") Is Nothing Then
                Response.Redirect("404.aspx")
            End If
            If Not (IsPostBack) Then
                Control_Sesion()
                usuario = Session("user_id")
                ipmaquina = Request.ServerVariables("REMOTE_ADDR")
                pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
                InicializarControles()
            Else
                'Me.RadListView1.DataSource = Session("General")
                'Me.RadListView1.DataBind()
                RadGridDatos.DataSource = Session("General")
                RadGridDatos.DataBind()
            End If
            usuario = Session("user_id")
            ipmaquina = Request.ServerVariables("REMOTE_ADDR")
            pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub



#Region "Procesos"


    Private Sub InicializarControles()
        Try
            ' Cargar tipo
            Dim datostipos As New System.Data.DataSet
            datostipos = ConcesionariosAdapter.CargarDatos(1)
            cmb_canal.DataSource = datostipos
            cmb_canal.DataValueField = "CODIGO"
            cmb_canal.DataTextField = "DESCRIPCION"
            cmb_canal.DataBind()
            ' Cargar anio
            Dim datosanio As New System.Data.DataSet
            datosanio = ConcesionariosAdapter.CargarDatos(2)
            cmb_anio.DataSource = datosanio
            cmb_anio.DataValueField = "CODIGO"
            cmb_anio.DataTextField = "DESCRIPCION"
            cmb_anio.DataBind()
            ' Cargar mes
            Dim datosmes As New System.Data.DataSet
            datosmes = ConcesionariosAdapter.CargarDatos(3)
            cmb_mes.DataSource = datosmes
            cmb_mes.DataValueField = "CODIGO"
            cmb_mes.DataTextField = "DESCRIPCION"
            cmb_mes.DataBind()
            LimpiarDatos()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub LimpiarDatos()
        Try
            Session("General") = Nothing
            Session("CLIENTE") = Nothing
            Session("VEHICULO") = Nothing
            Me.cmb_canal.SelectedValue = "C"
            Me.cmb_anio.SelectedValue = Now.Year
            Me.cmb_mes.SelectedValue = Now.Month
            Me.txtcanalID.Text = ""
            Me.txtcanal.Text = ""
            RadGridDatos.DataSource = Session("General")
            RadGridDatos.Height = 0
            RadGridDatos.DataBind()
            ConfigControles(1)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 08/08/2014
    ''' AUTOR: GALO ALVARADO
    ''' COMENTARIO: CONSULTA DE DATOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigControles(ByVal opcion As Integer)
        Try
            Select Case opcion
                Case 1
                    'Me.txtbusqueda.Enabled = False
                    'Me.BtnBusquedaAvanzada.Enabled = False
                    'Me.BtnFilterNo.Enabled = False
                    'Me.BtnImprimir.Enabled = False
                    'Me.BtnCorreo.Enabled = False
                Case 2
                    'Me.txtbusqueda.Enabled = True
                    'Me.BtnBusquedaAvanzada.Enabled = True
                    'Me.BtnFilterNo.Enabled = True
                Case 3
                    TxtCorreo.Visible = True
                    BtnCancela.Visible = True
                    BtnEnvia.Visible = True
                    titcorreo.Visible = True
                Case 4
                    TxtCorreo.Visible = False
                    BtnCancela.Visible = False
                    BtnEnvia.Visible = False
                    titcorreo.Visible = False
            End Select
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: Personaliza mensaje a preentar al usuario
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <param name="Texto"></param>
    ''' <param name="OperacionRealizar"></param>
    ''' <remarks></remarks>
    Private Sub Mensaje(ByVal texto As String, ByVal operacionRealizar As Int32)
        Try
            Dim titulo As String = String.Empty
            Dim icono As String = String.Empty
            Select Case operacionRealizar
                Case Operacion.OInvalida
                    titulo = "Operación Inválida"
                    icono = "Warning"
                Case Operacion.OExistosa
                    titulo = "Operación Exitosa"
                    icono = "Info"
                Case Operacion.CSinDatos
                    titulo = "Consulta sin Datos"
                    icono = "Info"
            End Select
            Me.RnMensajesError.Text = texto
            Me.RnMensajesError.Title = titulo
            Me.RnMensajesError.TitleIcon = icono
            Me.RnMensajesError.ContentIcon = icono
            Me.RnMensajesError.Show()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 08/08/2014
    ''' AUTOR: GALO ALVARADO
    ''' COMENTARIO: CONSULTA DE DATOS
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultaInicial()
        Try
            Dim consultar As Boolean
            Dim dTCstGeneral As New System.Data.DataSet
            Session("General") = Nothing
            dTCstGeneral = ConcesionariosAdapter.ConsultaDatos(Session.Item("user"), cmb_anio.SelectedValue.ToString, cmb_mes.SelectedValue.ToString, Me.txtcanalID.Text)
            Session("General") = dTCstGeneral
            consultar = False
            If dTCstGeneral.Tables(0).Rows.Count > 0 Then
                consultar = True
                RadGridDatos.DataSource = Nothing
                'Session("Consulta_InicialVehiculo") = dtInicial
                RadGridDatos.DataSource = dTCstGeneral.Tables(0)
                RadGridDatos.MasterTableView.DataKeyNames = New String() {"CODIGO"}
                RadGridDatos.Height = 440
                RadGridDatos.ClientSettings.Scrolling.AllowScroll = True
                RadGridDatos.ClientSettings.Scrolling.UseStaticHeaders = True
                RadGridDatos.ClientSettings.Scrolling.SaveScrollPosition = True
                RadGridDatos.DataBind()
                'Me.BtnImprimir.Enabled = True
                'Me.BtnCorreo.Enabled = True
            Else
                'LimpiaGridConsulta()
                RadGridDatos.DataSource = dTCstGeneral.Tables(0)
                RadGridDatos.Height = 200
                RadGridDatos.ClientSettings.Scrolling.AllowScroll = True
                RadGridDatos.ClientSettings.Scrolling.UseStaticHeaders = True
                RadGridDatos.ClientSettings.Scrolling.SaveScrollPosition = True
                RadGridDatos.DataBind()
                'Me.BtnImprimir.Enabled = False
                'Me.BtnCorreo.Enabled = False
                Me.Mensaje("No existen documentos registrados", Operacion.OInvalida)
            End If
            '*Dim obj As New ConcesionariosAdapter
            '*obj.RegistroActividadConcesionario(cmb_mes.SelectedValue.ToString, cmb_anio.SelectedValue.ToString, Me.txtcanalID.Text, usuario, "N", ipmaquina, pantalla, " ", False, consultar, " Consulto: ")
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    'Private Sub LimpiaGridConsulta()
    '    Try
    '        Dim dtc As New System.Data.DataSet
    '        dtc = CType(Session("General"), System.Data.DataSet)
    '        If Not dtc Is Nothing Then
    '            dtc.Tables(0).Rows.Clear()
    '        End If
    '        'RadListView1.DataSource = dtc
    '        'Me.RadListView1.VirtualItemCount = dtc.Tables(0).Rows.Count
    '        'RadListView1.DataBind()
    '        Me.RadGridDatos.DataSource = Nothing
    '        RadGridDatos.DataBind()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    'Private Sub ConsultaAvanzada(ByVal usuario As String, ByVal busqueda As String, ByVal tipo As String)
    '    Try
    '        Dim dtcstGeneral As New System.Data.DataSet
    '        'dtcstGeneral = DocumentosAdapter.ConsultaAvanzada(usuario, busqueda)
    '        Session("General") = dtcstGeneral
    '        Session("Consulta") = tipo
    '        If dtcstGeneral.Tables(0).Rows.Count > 0 Then
    '            RadListView1.DataSource = dtcstGeneral
    '            RadListView1.VirtualItemCount = dtcstGeneral.Tables(0).Rows.Count
    '            RadListView1.DataBind()
    '        Else
    '            LimpiaGridConsulta()
    '            'ConfigMsgNofitication(3, "No existen documentos según el criterio de búsqueda")
    '            Me.Mensaje("No existen documentos según el criterio de búsqueda", Operacion.OInvalida)
    '        End If
    '    Catch ex As Exception
    '        'ExceptionHandler.Captura_Error(ex)
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    'Protected Sub BtnBusquedaAvanzada_Click(sender As Object, e As EventArgs) Handles BtnBusquedaAvanzada.Click
    '    Try
    '        If Len(Me.txtbusqueda.Text.Trim()) > 3 Then
    '            RadListView1.FilterExpressions.Clear()
    '            RadListView1.FilterExpressions.BuildExpression().Contains("CHASIS", Me.txtbusqueda.Text.Trim()).Or()
    '            RadListView1.FilterExpressions.BuildExpression().Contains("CLIENTE", Me.txtbusqueda.Text.Trim()).Or()
    '            RadListView1.FilterExpressions.BuildExpression().Contains("VEHICULO", Me.txtbusqueda.Text.Trim()).Or()
    '            RadListView1.FilterExpressions.BuildExpression().Contains("PLACA", Me.txtbusqueda.Text.Trim()).Or()
    '            RadListView1.FilterExpressions.BuildExpression().Contains("PRODUCTO", Me.txtbusqueda.Text.Trim()).Build()
    '            'RadListView1.FilterExpressions.BuildExpression().Contains("ORDEN_SERVICIO", Me.txtbusqueda.Text.Trim()).Build()
    '            'RadListView1.Rebind()
    '        Else
    '            'RadListView1.FilterExpressions.Clear()
    '            Me.Mensaje("Deben de ingresar mas de 3 caracteres para filtrar", Operacion.OInvalida)
    '        End If
    '        RadListView1.Rebind()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    ''' <summary>
    ''' FECHA: 25/01/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: 
    ''' </summary>
    ''' <param name="url"></param>
    ''' <param name="target"></param>
    ''' <param name="windowFeatures"></param>
    ''' <remarks></remarks>
    'Public Shared Sub Redirect(ByVal url As String, ByVal target As String, ByVal windowFeatures As String)
    '    Try
    '        Dim context As HttpContext = HttpContext.Current
    '        If ([String].IsNullOrEmpty(target) OrElse target.Equals("_self", StringComparison.OrdinalIgnoreCase)) AndAlso [String].IsNullOrEmpty(windowFeatures) Then
    '            context.Response.Redirect(url)
    '        Else
    '            Dim page As System.Web.UI.Page = DirectCast(context.Handler, System.Web.UI.Page)
    '            If page Is Nothing Then
    '                Throw New InvalidOperationException("Cannot redirect to new window outside Page context.")
    '            End If
    '            url = page.ResolveClientUrl(url)
    '            Dim script As String
    '            If Not [String].IsNullOrEmpty(windowFeatures) Then
    '                script = "window.open(""{0}"", ""{1}"", ""{2}"");"
    '            Else
    '                script = "window.open(""{0}"", ""{1}"");"
    '            End If
    '            script = [String].Format(script, url, target, windowFeatures)
    '            ScriptManager.RegisterStartupScript(page, GetType(Page), "Redirect", script, True)
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub


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


    'Protected Sub GeneraPdf_Click()
    '    Try
    '        Dim fontNormal As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL)
    '        Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
    '        Dim fontcv As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
    '        Dim grey As New BaseColor(64, 64, 64)
    '        Dim negro As New BaseColor(0, 0, 0)
    '        Dim fontgris As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, grey)
    '        Dim fontnegro As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, negro)
    '        Dim espacio As String = Space(8)
    '        Dim espacio2 As String = Space(6)
    '        Dim dtconsulta2 As New System.Data.DataSet
    '        dtconsulta2 = Session("infoqr")
    '        Dim documento As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
    '        Dim file As String = "\\10.100.107.14\ImagenesDocumentos\cert_concesionario_" & dtconsulta2.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString() & ".pdf"
    '        Session("nombrepdf") = file
    '        Dim writer As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(file, FileMode.Create))
    '        Dim lineablanco As New iTextSharp.text.Paragraph(" ")
    '        Dim ev As New creacionpdf_con()
    '        documento.Open()
    '        documento.NewPage()
    '        writer.PageEvent = ev
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        Dim tablacv As New PdfPTable(2)
    '        tablacv.SetWidths(New Single() {160.0F, 40.0F})
    '        Dim codigoblan As New PdfPCell(New Phrase(" "))
    '        codigoblan.Border = 0
    '        codigoblan.HorizontalAlignment = Element.ALIGN_RIGHT
    '        tablacv.AddCell(codigoblan)
    '        Dim codigoveh As New PdfPCell(New Phrase("C.V. " & dtconsulta2.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), fontcv))
    '        codigoveh.Border = 0
    '        codigoveh.BackgroundColor = New BaseColor(230, 230, 230)
    '        codigoveh.HorizontalAlignment = Element.ALIGN_RIGHT
    '        tablacv.AddCell(codigoveh)
    '        documento.Add(tablacv)
    '        documento.Add(lineablanco)

    '        'If cbm_tipo.SelectedValue.ToString = "C" Then
    '        Dim nombre As New iTextSharp.text.Paragraph(espacio2 & "Certifica que el Sr.(a). " & dtconsulta2.Tables(0).Rows(0)("CLIENTE").ToString(), fontcliente)
    '        documento.Add(nombre)
    '        documento.Add(lineablanco)
    '        Dim linea1 As New iTextSharp.text.Paragraph(espacio & "Ha adquirido los siguientes sistemas:", fontNormal)
    '        documento.Add(linea1)
    '        documento.Add(lineablanco)
    '        ' Impresion de los productos , se genera tabla estaba fontbold
    '        Dim tabla As New PdfPTable(4)
    '        tabla.SetWidths(New Single() {40.0F, 15.0F, 18.0F, 10.0F})
    '        Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontnegro))
    '        titulo1.Border = 0
    '        'titulo1.Border = 2
    '        titulo1.BackgroundColor = New BaseColor(230, 230, 230)
    '        titulo1.HorizontalAlignment = Element.ALIGN_CENTER
    '        tabla.AddCell(titulo1)
    '        Dim titulo2 As New PdfPCell(New Phrase("COBERTURA", fontnegro))
    '        titulo2.Border = 0
    '        titulo2.BackgroundColor = New BaseColor(230, 230, 230)
    '        titulo2.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabla.AddCell(titulo2)
    '        Dim titulo3 As New PdfPCell(New Phrase("P.V.P INCLUIDO IVA", fontnegro))
    '        titulo3.Border = 0
    '        titulo3.BackgroundColor = New BaseColor(230, 230, 230)
    '        titulo3.HorizontalAlignment = Element.ALIGN_RIGHT
    '        tabla.AddCell(titulo3)
    '        Dim titulo4 As New PdfPCell(New Phrase(" ", fontnegro))
    '        titulo4.Border = 0
    '        'titulo4.BackgroundColor = New BaseColor(230, 230, 230)
    '        titulo4.HorizontalAlignment = Element.ALIGN_RIGHT
    '        tabla.AddCell(titulo4)
    '        If dtconsulta2.Tables(0).Rows.Count > 0 Then
    '            For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
    '                Dim det1 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("PRODUCTO").ToString(), fontNormal))
    '                det1.Border = 0
    '                det1.HorizontalAlignment = Element.ALIGN_LEFT
    '                tabla.AddCell(det1)
    '                Dim fechafinal As String = ""
    '                Dim valorfecha As String
    '                valorfecha = dtconsulta2.Tables(0).Rows(i)("COBERTURA").ToString()
    '                If Mid(valorfecha, 4, 2) = "01" Then fechafinal = Mid(valorfecha, 1, 3) & "Ene/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "02" Then fechafinal = Mid(valorfecha, 1, 3) & "Feb/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "03" Then fechafinal = Mid(valorfecha, 1, 3) & "Mar/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "04" Then fechafinal = Mid(valorfecha, 1, 3) & "Abr/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "05" Then fechafinal = Mid(valorfecha, 1, 3) & "May/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "06" Then fechafinal = Mid(valorfecha, 1, 3) & "Jun/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "07" Then fechafinal = Mid(valorfecha, 1, 3) & "Jul/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "08" Then fechafinal = Mid(valorfecha, 1, 3) & "Ago/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "09" Then fechafinal = Mid(valorfecha, 1, 3) & "Sep/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "10" Then fechafinal = Mid(valorfecha, 1, 3) & "Oct/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "11" Then fechafinal = Mid(valorfecha, 1, 3) & "Nov/" & Mid(valorfecha, 7, 4)
    '                If Mid(valorfecha, 4, 2) = "12" Then fechafinal = Mid(valorfecha, 1, 3) & "Dic/" & Mid(valorfecha, 7, 4)
    '                Dim det2 As New PdfPCell(New Phrase(fechafinal, fontNormal))
    '                det2.Border = 0
    '                det2.HorizontalAlignment = Element.ALIGN_LEFT
    '                tabla.AddCell(det2)
    '                Dim det3 As New PdfPCell(New Phrase("$" & dtconsulta2.Tables(0).Rows(i)("PRECIO_PRODUCTO").ToString(), fontNormal))
    '                det3.Border = 0
    '                det3.HorizontalAlignment = Element.ALIGN_RIGHT
    '                tabla.AddCell(det3)
    '                Dim det4 As New PdfPCell(New Phrase(" ", fontNormal))
    '                det4.Border = 0
    '                det4.HorizontalAlignment = Element.ALIGN_RIGHT
    '                tabla.AddCell(det4)
    '            Next
    '        End If
    '        documento.Add(tabla)
    '        documento.Add(lineablanco)
    '        'End If
    '        documento.Add(lineablanco)
    '        Dim texto2 As String = ""
    '        texto2 = "Estos sistemas se encuentran instalados en el (Vehículo/Barco/Avión/Cajero) con las siguientes características:"
    '        Dim linea2 As New iTextSharp.text.Paragraph(espacio & texto2, fontNormal)
    '        documento.Add(linea2)
    '        documento.Add(lineablanco)
    '        Dim tabladatos As New PdfPTable(4)
    '        tabladatos.SetWidths(New Single() {20.0F, 105.0F, 20.0F, 65.0F})
    '        '' fontnegro  para titulo,  fontgris para dato
    '        Dim vehic1 As New PdfPCell(New Phrase("MARCA", fontnegro))
    '        vehic1.Border = 0
    '        vehic1.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic1)
    '        ''Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontgris))
    '        Dim vehic2 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("MARCA").ToString(), fontgris))
    '        ''Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontNormal))
    '        vehic2.Border = 0
    '        vehic2.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic2)
    '        Dim vehic3 As New PdfPCell(New Phrase("AÑO", fontnegro))
    '        vehic3.Border = 0
    '        vehic3.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic3)
    '        Dim vehic4 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("ANIO").ToString(), fontgris))
    '        'Dim vehic4 As New PdfPCell(New Phrase(txtanio, fontgris))
    '        vehic4.Border = 0
    '        'estaba fontnormal
    '        vehic4.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic4)
    '        Dim vehic5 As New PdfPCell(New Phrase("MODELO", fontnegro))
    '        vehic5.Border = 0
    '        vehic5.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic5)
    '        'Dim vehic6 As New PdfPCell(New Phrase(Me.Modelo.Text, fontgris))
    '        Dim vehic6 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("MODELO").ToString(), fontgris))
    '        vehic6.Border = 0
    '        vehic6.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic6)
    '        Dim vehic7 As New PdfPCell(New Phrase("PLACA", fontnegro))
    '        vehic7.Border = 0
    '        vehic7.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic7)
    '        'Dim vehic8 As New PdfPCell(New Phrase(Me.Placa.Text, fontgris))
    '        Dim vehic8 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("PLACA").ToString(), fontgris))
    '        vehic8.Border = 0
    '        vehic8.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic8)
    '        Dim vehic9 As New PdfPCell(New Phrase("TIPO", fontnegro))
    '        vehic9.Border = 0
    '        vehic9.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic9)
    '        'Dim vehic10 As New PdfPCell(New Phrase(Me.Tipo.Text, fontgris))
    '        Dim vehic10 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("TIPO").ToString(), fontgris))
    '        vehic10.Border = 0
    '        vehic10.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic10)
    '        Dim vehic11 As New PdfPCell(New Phrase("CHASIS", fontnegro))
    '        vehic11.Border = 0
    '        vehic11.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic11)
    '        'Dim vehic12 As New PdfPCell(New Phrase(Me.Chasis.Text, fontgris))
    '        Dim vehic12 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("CHASIS").ToString(), fontgris))
    '        vehic12.Border = 0
    '        vehic12.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic12)
    '        Dim vehic13 As New PdfPCell(New Phrase("COLOR", fontnegro))
    '        vehic13.Border = 0
    '        vehic13.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic13)
    '        'Dim vehic14 As New PdfPCell(New Phrase(Me.Color.Text, fontgris))
    '        Dim vehic14 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("COLOR").ToString(), fontgris))
    '        vehic14.Border = 0
    '        vehic14.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic14)
    '        Dim vehic15 As New PdfPCell(New Phrase("MOTOR", fontnegro))
    '        vehic15.Border = 0
    '        vehic15.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic15)
    '        'Dim vehic16 As New PdfPCell(New Phrase(Me.Motor.Text, fontgris))
    '        Dim vehic16 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(0)("MOTOR").ToString(), fontgris))
    '        vehic16.Border = 0
    '        vehic16.HorizontalAlignment = Element.ALIGN_LEFT
    '        tabladatos.AddCell(vehic16)
    '        documento.Add(tabladatos)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        ' Imprime el codigo de barra
    '        'Dim codbarra As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(Session("nombrebarra")))
    '        'codbarra.ScalePercent(75.0F)
    '        'documento.Add(codbarra)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        ' Fecha de emisión del pdf
    '        Dim lblfechahoraemision As String
    '        lblfechahoraemision = "Info Emisión Docto : " & Session("user_session") & " | " & System.DateTime.Now.ToString("dd MMMM yyyy") & " | " & System.DateTime.Now.ToString("H:mm:ss")
    '        'lblfechahoraemision = espacio & lblfechahoraemision
    '        'Dim fecha As New iTextSharp.text.Paragraph(lblfechahoraemision, fontNormal)
    '        'documento.Add(fecha)
    '        Dim fechatrabajo As String = ""
    '        Dim valortrabajo As String = dtconsulta2.Tables(0).Rows(0)("FECHA_TRABAJO").ToString()
    '        If Mid(valortrabajo, 4, 2) = "01" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Enero/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "02" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Febrero/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "03" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Marzo/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "04" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Abril/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "05" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Mayo/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "06" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Junio/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "07" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Julio/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "08" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Agosto/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "09" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Septiembre/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "10" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Octubre/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "11" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Noviembre/" & Mid(valortrabajo, 7, 4)
    '        If Mid(valortrabajo, 4, 2) = "12" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Diciembre/" & Mid(valortrabajo, 7, 4)
    '        Dim tablacv2 As New PdfPTable(2)
    '        tablacv2.SetWidths(New Single() {80.0F, 120.0F})
    '        'If cbm_tipo.SelectedValue.ToString = "I" Then
    '        Dim fecha As New PdfPCell(New Phrase("Fecha : " & fechatrabajo, fontNormal))
    '        fecha.Border = 0
    '        fecha.HorizontalAlignment = Element.ALIGN_LEFT
    '        tablacv2.AddCell(fecha)
    '        tablacv2.AddCell(codigoblan)
    '        'ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
    '        '    Dim fecha As New PdfPCell(New Phrase("Fecha Desinstalación: " & fechatrabajo, fontNormal))
    '        '    fecha.Border = 0
    '        '    fecha.HorizontalAlignment = Element.ALIGN_LEFT
    '        '    tablacv2.AddCell(fecha)
    '        '    tablacv2.AddCell(codigoblan)
    '        'End If
    '        tablacv2.AddCell(codigoblan)
    '        Dim codigofecha As New PdfPCell(New Phrase(lblfechahoraemision, fontNormal))
    '        codigofecha.Border = 0
    '        codigofecha.HorizontalAlignment = Element.ALIGN_RIGHT
    '        tablacv2.AddCell(codigofecha)
    '        documento.Add(tablacv2)
    '        documento.Add(lineablanco)
    '        documento.Add(lineablanco)
    '        ' Imprime logo del fin de pagina 
    '        Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_footer_certificado.jpg"))
    '        logo.ScalePercent(75.0F)
    '        'logo.Left = 10
    '        'logo.Right = 10
    '        'logo.HorizontalAlignment = Element.ALIGN_CENTER
    '        documento.Add(logo)
    '        documento.Close()
    '        documento.Dispose()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


#End Region


#Region "Controles radgrid"


    ''' <summary>
    ''' FECHA: 29/10/2019
    ''' AUTOR: GALO ALVARADO
    ''' COMENTARIO: EVENTO ITEMCOMMAND DEL GRID DE APROBACIÓN
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub RadGridDatos_ItemCommand(sender As Object, e As Telerik.Web.UI.GridCommandEventArgs) Handles RadGridDatos.ItemCommand
        Try
            Me.RadGridDatos.DataSource = Session("General")
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

    'Private Sub RadListView1_DataBound(sender As Object, e As System.EventArgs) Handles RadListView1.DataBound
    '    Try
    '        Me.RadListView1.DataSource = Session("General")
    '    Catch ex As Exception

    '    End Try
    'End Sub


    ''' <summary>
    ''' FECHA: 08/08/2014
    ''' AUTOR: GALO ALVARADO
    ''' COMENTARIO: DATOS DEL LISTVIEW
    ''' </summary>
    ''' <remarks></remarks>
    'Protected Sub RadListView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles RadListView1.SelectedIndexChanged
    '    Try
    '        Dim values As New Hashtable()
    '        If RadListView1.SelectedItems.Count = 0 Then
    '            RadListView1.ClearSelectedItems()
    '            RadListView1.Rebind()
    '            Return
    '        End If
    '        RadListView1.SelectedItems(0).ExtractValues(values)
    '        Session("CLIENTE") = values("CLIENTE_ID").ToString()
    '        Session("VEHICULO") = values("CODIGO").ToString()
    '        'Session("ORDEN") = values("ORDEN_SERVICIO").ToString()
    '        'Session("NOMBRE") = values("CLIENTE").ToString()

    '        If Session("CLIENTE") IsNot Nothing And Session("VEHICULO") IsNot Nothing Then
    '            Me.BtnImprimir.Enabled = True
    '            Me.BtnCorreo.Enabled = True
    '        Else
    '            Me.BtnImprimir.Enabled = False
    '            Me.BtnCorreo.Enabled = False
    '        End If
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    'Protected Sub RadListView1_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.RadListViewNeedDataSourceEventArgs) Handles RadListView1.NeedDataSource
    '    Try
    '        Me.RadListView1.DataSource = Session("General")
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    ''' <summary>
    ''' FECHA: 30/10/2019
    ''' AUTOR: GALO ALVARADO
    ''' COMENTARIO: MÉTODO PARA CONFIGURAR EL BOTÓN DE EXPORTAR ARCHIVO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConfigExportar()
        Try
            Dim dt As DateTime = Now.Date
            Me.RadGridDatos.ExportSettings.ExportOnlyData = True
            Me.RadGridDatos.ExportSettings.IgnorePaging = True
            Me.RadGridDatos.ExportSettings.OpenInNewWindow = True
            Me.RadGridDatos.ExportSettings.FileName = Session("user_session") & "_" & [String].Format("{0:dd/MMM/yyyy}", dt)
            Me.RadGridDatos.MasterTableView.ExportToExcel()


            'Me.RadGridDatos.PageSize = RadGridDatos.MasterTableView.VirtualItemCount
            'Me.RadGridDatos.ExportSettings.IgnorePaging = True
            'Me.RadGridDatos.ExportSettings.OpenInNewWindow = True
            'Me.RadGridDatos.MasterTableView.ExportToExcel()

            'RadGridDatos.ExportSettings.Excel.Format = DirectCast([Enum].Parse(GetType(GridExcelExportFormat), alternateText), GridExcelExportFormat)
            'RadGridDatos.ExportSettings.IgnorePaging = CheckBox1.Checked
            'RadGridDatos.ExportSettings.ExportOnlyData = True
            'RadGridDatos.ExportSettings.OpenInNewWindow = True
            'RadGridDatos.MasterTableView.ExportToExcel()


            'Select Case opcion
            '    'Exportar a XLS
            '    Case 1
            '        'Exportar a CSV
            '    Case 2
            '        rgdAprobacion.MasterTableView.ExportToCSV()
            '        'exportar a PDf
            '    Case 3
            '        rgdAprobacion.MasterTableView.ExportToPdf()
            '    Case 3
            'End Select
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    'Protected Sub RadGridDatos_ExcelMLWorkBookCreated(sender As Object, e As GridExcelMLWorkBookCreatedEventArgs)
    '    'If CheckBox2.Checked Then
    '    For Each row As RowElement In e.WorkBook.Worksheets(0).Table.Rows
    '        row.Cells(0).StyleValue = "Style1"
    '    Next

    '    Dim style As New StyleElement("Style1")
    '    style.InteriorStyle.Pattern = InteriorPatternType.Solid
    '    style.InteriorStyle.Color = System.Drawing.Color.LightGray
    '    e.WorkBook.Styles.Add(style)
    '    'End If
    'End Sub

#End Region

#Region "Botones"


    Protected Sub BtnConsultar_Click(sender As Object, e As EventArgs) Handles BtnConsultar.Click
        Try
            If Len(Me.txtcanalID.Text.Trim()) > 3 Then
                ConfigControles(2)
                ConsultaInicial()
            Else
                Me.Mensaje("Debe de Seleccionar un Canal", Operacion.OInvalida)
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Try
            LimpiarDatos()
            'Me.RadListView1.DataSource = Session("General")
            'Me.RadListView1.DataBind()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    'Private Sub BtnFilterNo_Click(sender As Object, e As System.EventArgs) Handles BtnFilterNo.Click
    '    Try
    '        RadListView1.FilterExpressions.Clear()
    '        RadListView1.Rebind()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub

    'Protected Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click
    '    Try
    '        'Dim obj As New ConcesionariosAdapter
    '        'obj.RegistroActividadConcesionario(cmb_mes.SelectedValue.ToString, cmb_anio.SelectedValue.ToString, Me.txtcanalID.Text, usuario, "N", ipmaquina, pantalla, " ", False, True, " Impreso: ")
    '        'Redirect("ConsultaWebConcesionarios.aspx", "_blank", "menubar=0,width=850,height=850")

    '        ConfigExportar()
    '        'Dim dt As DateTime = Now.Date
    '        ''Me.RadGridDatos.PageSize = RadGridDatos.MasterTableView.VirtualItemCount
    '        'Me.RadGridDatos.ExportSettings.ExportOnlyData = True
    '        'Me.RadGridDatos.ExportSettings.IgnorePaging = True
    '        'Me.RadGridDatos.ExportSettings.OpenInNewWindow = True
    '        'Me.RadGridDatos.ExportSettings.FileName = Session("user") & "_" & [String].Format("{0:dd/MMM/yyyy}", dt)
    '        'Me.RadGridDatos.MasterTableView.ExportToExcel()
    '        'ConfigureExport()
    '        'RadGridDatos.MasterTableView.ExportToExcel()

    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    'Public Sub ConfigureExport()
    '    RadGridDatos.ExportSettings.ExportOnlyData = True
    '    RadGridDatos.ExportSettings.IgnorePaging = True
    '    RadGridDatos.ExportSettings.OpenInNewWindow = True
    '    RadGridDatos.ExportSettings.UseItemStyles = True
    'End Sub

    Protected Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Try

            If Session("General") IsNot Nothing Then
                ConfigExportar()
            End If

            'Dim dTCstGeneral As New System.Data.DataSet
            'dTCstGeneral = Session("General") 
            'If dTCstGeneral.Tables(0).Rows.Count > 0 Then
            '    'consultar = True
            '    'If Session("General").Rows.Count > 0 Then

            'Else
            '    Me.Mensaje("Debe de tener datos para poder exportarlos", Operacion.OInvalida)
            'End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    'Protected Sub BtnCorreo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCorreo.Click
    '    Try
    '        'TxtCorreo.Visible = True
    '        'BtnCancela.Visible = True
    '        'BtnEnvia.Visible = True
    '        'titcorreo.Visible = True
    '        ConfigControles(3)
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    Protected Sub Btnenviar(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnvia.Click
        Try
            'If Me.TxtCorreo.Text = "" Then
            '    Me.Mensaje("No ha ingresado dirección de correo...", Operacion.OInvalida)
            'Else
            '    Dim dtTransaccion As New System.Data.DataSet
            '    Dim obj As New ConsultaWeb
            '    dtTransaccion = obj.ConsultarImprimir(Session("VEHICULO").ToString(), Session("CLIENTE").ToString(), Session("user_id"), "G")
            '    Session("infoqr") = dtTransaccion
            '    GeneraPdf_Click()
            '    Dim htmlcuerpo As String = ""
            '    htmlcuerpo = "<HTML> <font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font>  <BODY> <br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
            '    htmlcuerpo = htmlcuerpo + " <br/> "
            '    htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Marca : </font> <font style=""color: #666666;"">" + "&nbsp;" + dtTransaccion.Tables(0).Rows(0)("MARCA").ToString() + "</font> <br/>"
            '    htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Modelo: </font> <font style=""color: #666666;"">" + dtTransaccion.Tables(0).Rows(0)("MODELO").ToString() + "</font><br/>"
            '    htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Chasis: </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + dtTransaccion.Tables(0).Rows(0)("CHASIS").ToString() + "</font><br/>"
            '    htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Placa : </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + "&nbsp;" + dtTransaccion.Tables(0).Rows(0)("PLACA").ToString() + "</font><br/>"
            '    htmlcuerpo = htmlcuerpo + " <br/> "
            '    htmlcuerpo = htmlcuerpo + " <br/> "
            '    htmlcuerpo = htmlcuerpo + " <font style=""color: #666666;"">Saludos Cordiales</font>"
            '    htmlcuerpo = htmlcuerpo + " <br/> "
            '    htmlcuerpo = htmlcuerpo + " <br/> "
            '    htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;""> Servicio al cliente</font>"
            '    htmlcuerpo = htmlcuerpo + " <br/> "
            '    htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;""> Carseg S.A.</font></ BODY>"
            '    htmlcuerpo = htmlcuerpo + "</ HTML>"
            '    Dim mailMessage As New MailMessage()
            '    Dim mailAddress As New MailAddress("noticarseg@carsegsa.com")
            '    mailMessage.From = mailAddress
            '    mailMessage.IsBodyHtml = True
            '    mailMessage.Subject = "Certificado de Hunter, " + dtTransaccion.Tables(0).Rows(0)("CLIENTE").ToString()
            '    mailMessage.Body = htmlcuerpo
            '    mailMessage.Priority = MailPriority.High
            '    Dim attachment As New Attachment(Session("nombrepdf"))
            '    mailMessage.Attachments.Add(attachment)
            '    Dim mailToBcc As String = Application("usuario_email").ToString()
            '    Dim mailToBccCollection As [String]() = mailToBcc.Split(",")
            '    For Each mailTooBcc As String In mailToBccCollection
            '        mailMessage.Bcc.Add(mailTooBcc)
            '    Next
            '    Dim smtpClient As New SmtpClient("10.100.89.34")
            '    ' Para enviar 2 destinatarios
            '    Dim correo1 As String
            '    Dim correo2 As String = ""
            '    correo1 = Catalogo(texto:=Me.TxtCorreo.Text)
            '    correo2 = Catalogo(texto:=Me.TxtCorreo.Text, valorDevolver:="D")
            '    mailMessage.To.Add(correo1)
            '    If correo2 <> "" Then
            '        mailMessage.To.Add(correo2)
            '    End If
            '    smtpClient.Send(mailMessage)
            '    mailMessage.Dispose()
            '    'TxtCorreo.Visible = False
            '    'BtnCancela.Visible = False
            '    'BtnEnvia.Visible = False
            '    'titcorreo.Visible = False
            '    'BtnConsultar.Visible = True
            '    'BtnNuevo.Visible = True
            '    'BtnCorreo.Enabled = True
            '    'BtnNuevo.Enabled = True
            '    BtnImprimir.Enabled = False
            '    BtnCorreo.Enabled = False
            '    ConfigControles(4)
            '    Dim objconcesionario As New ConcesionariosAdapter
            '    objconcesionario.RegistroActividadConcesionario(cmb_mes.SelectedValue.ToString, cmb_anio.SelectedValue.ToString, Me.txtcanalID.Text, usuario, "N", ipmaquina, pantalla, " ", False, True, " Impreso: ")
            '    TxtCorreo.Text = ""
            '    Dim destino As String = Session("nombrepdf")
            '    If (System.IO.File.Exists(destino)) Then
            '        System.IO.File.Delete(destino)
            '    End If
            '    Me.Mensaje("Se ha enviado el archivo pdf correctamente", Operacion.OExistosa)
            'End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub Bntcancelar(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCancela.Click
        Try
            ConfigControles(4)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

#End Region




  
End Class