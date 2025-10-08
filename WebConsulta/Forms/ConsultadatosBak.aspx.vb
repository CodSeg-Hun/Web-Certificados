Imports Telerik.Web.UI

Public Class ConsultadatosBak
    Inherits System.Web.UI.Page

    Dim ipmaquina As String
    Dim pantalla As String
    Dim usuarioOficina As String
    'Dim usuariosession As String
    Dim usuario As Integer = 0

    Public Enum Operacion
        OExistosa = 1
        OInvalida = 2
        CSinDatos = 3
    End Enum

#Region "LOG"

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
            Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Session("user_session") = "atambo"
            'Session("user_id") = 889
            If Session("user_id") Is Nothing Then
                Response.Redirect("404.aspx")
            End If
            If Not (IsPostBack) Then
                Control_Sesion()
                botones("Inicial")
                'Datos_Log_Ingreso()
                InicializaObjetos()
                usuario = Session("user_id")
                Session("dsDatos") = Nothing
                Session("Consulta_Datos") = Nothing
                Session("Consulta_Vehiculo") = Nothing
                usuarioOficina = Session("user_id")
                'usuariosession = Session("user_session")
                ipmaquina = Request.ServerVariables("REMOTE_ADDR")
                pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
            Else
                If Not CType(Session("Consulta_Datos"), DataSet) Is Nothing Then
                    If CType(Session("Consulta_Datos"), DataSet).Tables.Count > 0 Then
                        RadGridDatos.DataSource = CType(Session("Consulta_Datos"), DataSet).Tables(0)
                    Else
                        Me.RadGridDatos.DataSource = Nothing
                    End If
                Else
                    Me.RadGridDatos.DataSource = CType(Session("Consulta_Inicial"), DataSet).Tables(0)
                End If
                'usuario_sh = Session("user_session")
                'usuario_id = Session("user_id")
                'Session("user_session") = usuariosession
                'Session("user_id") = usuarioOficina
            End If
            usuarioOficina = Session("user_id")
            ipmaquina = Request.ServerVariables("REMOTE_ADDR")
            pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: registro de log
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Datos_Log_Ingreso()
        Try
            Dim formularioIdBack As Integer
            'Dim permanencia As New LogAdministracion
            If Session("form_session_id") Is Nothing Then
                formularioIdBack = 0
            Else
                formularioIdBack = Session("form_session_id")
            End If
            Dim usuarioIp As String = Request.ServerVariables("REMOTE_ADDR")
            Dim usuarioId As String = Session("user_id")
            'Dim usuario_login As String = Session("user")
            'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
            'Dim usuario_login As String = Session("user_session")
            'Dim formulario_url As String = System.IO.Path.GetFileName(Request.PhysicalPath)
            Dim formularioUrl2 As String = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath)
            Dim obj As New LogAdministracion
            Dim dtInfoFormulario As New System.Data.DataSet
            dtInfoFormulario = obj.Consulta_Formulario(formularioUrl2)
            Dim formularioId As Integer = dtInfoFormulario.Tables("INFO_FORMULARIO").Rows(0)("ID").ToString()
            Session("form_session_id") = formularioId
            'Registra_permanencia(usuario_ip, usuario_id, formulario_id_back, formulario_id, Session("user"))
            Session("ref_id") = Registra_Permanencia2(usuarioId, usuarioIp, formularioId, Session("ref_id"), formularioIdBack, Session("ref_id_back"))
            Session("ref_id_back") = Session("ref_id")
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


#End Region

#Region "Procesos"

    ''' <summary>
    ''' Motivo: proceso de botones para presentarlos
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <remarks></remarks>
    Sub Botones(opcion As String)
        Try
            Select Case opcion
                Case "Inicial"
                    Me.BtnNuevo.Enabled = True
                    Me.BtnCancelar.Enabled = False
                    Me.BtnConsultar.Enabled = True
                    Me.BtnImprimir.Enabled = False
                Case "Consultar"
                    Me.BtnNuevo.Enabled = False
                    Me.BtnCancelar.Enabled = True
                    Me.BtnConsultar.Enabled = False
                    'Me.txtConCodCliente.Enabled = False
                    Me.TxtConCliente.Enabled = False
                    Me.txtConModelo.Enabled = False
                    Me.txtConPlaca.Enabled = False
                    Me.txtConTipo.Enabled = False
                    Me.txtConChasis.Enabled = False
                    Me.txtConColor.Enabled = False
                    Me.txtConMotor.Enabled = False
                    Me.txtConMarca.Enabled = False
                    Me.txtConCodVehiculo.Enabled = False
                    Me.BtnImprimir.Enabled = False
            End Select
            ' Me.btnEditar.Visible = False
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary> 
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' COMENTARIO: MÉTODO PARA EL CONTROL, VISUALIZACIÓN Y ENVÍO DE ERRORES VÍA EMAIL
    ''' </summary>
    ''' <param name="tipo"></param>
    ''' <remarks></remarks>
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try
    '        Email.Enviar_Email(tipo)
    '    Catch ex As Exception
    '        Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
    '        Captura_Error(ex)
    '    End Try
    'End Sub

    ''' <summary>
    ''' Motivo: Personaliza mensaje a preentar al usuario
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <param name="Texto"></param>
    ''' <param name="OperacionRealizar"></param>
    ''' <remarks></remarks>
    Private Sub Mensaje(ByVal texto As String, ByVal operacionRealizar As Int32, Tipo As Exception)
        Try
            Dim titulo As String = String.Empty
            Dim icono As String = String.Empty
            Select Case OperacionRealizar
                Case Operacion.OInvalida
                    Titulo = "Operación Inválida"
                    Icono = "Warning"
                Case Operacion.OExistosa
                    Titulo = "Operación Exitosa"
                    Icono = "Info"
                Case Operacion.CSinDatos
                    Titulo = "Consulta sin Datos"
                    Icono = "Info"
            End Select
            Me.RnMensajesError.Text = Texto
            Me.RnMensajesError.Title = Titulo
            Me.RnMensajesError.TitleIcon = Icono
            Me.RnMensajesError.ContentIcon = Icono
            Me.RnMensajesError.Show()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub LimpiarControl()
        Try
            If RadTabParametro.SelectedIndex = 0 Then
                Me.txtCodVehiculo.Text = ""
                'Me.txtRuc.Text = ""
                'Me.txtNombre.Text = ""
                Me.txtChasis.Text = ""
                'Me.TxtPlaca.Text = ""
                Me.TxtMotor.Text = ""
                Me.RadGridDatos.DataSource = Session("Consulta_Inicial")
                RadGridDatos.Height = 110
                RadGridDatos.DataBind()
            End If
            If RadTabParametro.SelectedIndex = 1 Then
                Me.txtConCodCliente.Text = ""
                Me.TxtConCliente.Text = ""
                Me.txtConModelo.Text = ""
                Me.txtConPlaca.Text = ""
                Me.txtConTipo.Text = ""
                Me.txtConChasis.Text = ""
                Me.txtConColor.Text = ""
                Me.txtConMotor.Text = ""
                Me.txtConMarca.Text = ""
                Me.txtConCodVehiculo.Text = ""
                Me.RadGridVehiculo.DataSource = Session("Consulta_InicialVehiculo")
                RadGridVehiculo.Height = 110
                RadGridVehiculo.DataBind()
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: método que permite inicializar objetos
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InicializaObjetos()
        Try
            ConsultarInicial()
            ConsultarVehiculo()
            LimpiarControl()
            RadTabParametro.Tabs.Item(1).Enabled = False
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub CargaDatos()
        Try
            Dim item As GridDataItem
            item = RadGridDatos.SelectedItems(0)
            Me.txtConCodVehiculo.Text = CType(item("ID_VEHICULO").Text, Int64)
            Me.TxtConCliente.Text = item("ID_CLIENTE").Text + " - " + item("CLIENTE").Text
            Me.txtConModelo.Text = item("MODELO").Text
            Me.txtConPlaca.Text = item("PLACA").Text
            Me.txtConTipo.Text = item("TIPO").Text
            Me.txtConChasis.Text = item("CHASIS").Text
            Me.txtConColor.Text = item("COLOR").Text
            Me.txtConMotor.Text = item("MOTOR").Text
            Me.txtConMarca.Text = item("MARCA").Text
            Me.txtConCodCliente.Text = item("ID_CLIENTE").Text
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Function Validar() As Boolean
        Validar = False
        Try
            If txtCodVehiculo.Text <> "" Then
                Validar = True
            ElseIf Me.txtChasis.Text <> "" Then
                Validar = True
            ElseIf Me.TxtMotor.Text <> "" Then
                Validar = True
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function

    'Private Sub GeneraTituloForm()
    '    Try
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub

#End Region

#Region "botones"

    Private Sub BtnConsultar_Click(sender As Object, e As EventArgs) Handles BtnConsultar.Click
        Try
            If Validar() Then
                Botones("Inicial")
                Consultar()
                'RadGridDatos.DataBind()
            Else
                Throw New Exception("Ingrese datos para poder consultar")
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Try
            Botones("Inicial")

            RadTabParametro.SelectedIndex = 0
            RadMultiPage.SelectedIndex = 0
            LimpiarControl()
            RadTabParametro.DataBind()
            RadTabParametro.Tabs.Item(0).Enabled = True
            RadTabParametro.Tabs.Item(1).Enabled = False
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub BtnNuevo_Click(sender As Object, e As EventArgs) Handles BtnNuevo.Click
        Try
            Botones("Inicial")
            LimpiarControl()
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles BtnImprimir.Click
        Try
            If RadTabParametro.SelectedIndex = 1 Then
                Dim obj As New ConsultaWeb
                'Dim ipmaquina As String = Request.ServerVariables("REMOTE_ADDR")
                'Dim pantalla As String = System.IO.Path.GetFileName(Request.PhysicalPath)
                'Dim usuarioOficina As Integer = Session("user_id")
                '*obj.Registro_Actividad(Me.txtConCodVehiculo.Text, Me.txtConChasis.Text, Me.txtConMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, " ", False)
                Redirect("ConsultaWebReporte.aspx", "_blank", "menubar=0,width=850,height=800")
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

#End Region


#Region "Procedimientos generales"


    ''' <summary>
    ''' Motivo: método que permite obtener los datos vacios de los grid
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultarInicial()
        Try
            Dim obj As New ConsultaWeb
            Dim dtInicial As New System.Data.DataSet
            dtInicial = CType(Session("Consulta_Datos"), DataSet)
            dtInicial = obj.ConsultaInicial()
            RadGridDatos.DataSource = Nothing
            Session("Consulta_Inicial") = dtInicial
            RadGridDatos.DataSource = dtInicial.Tables(0)
            RadGridDatos.MasterTableView.DataKeyNames = New String() {"ID_CLIENTE"}
            RadGridDatos.Height = 110
            RadGridDatos.ClientSettings.Scrolling.AllowScroll = True
            RadGridDatos.ClientSettings.Scrolling.UseStaticHeaders = True
            RadGridDatos.ClientSettings.Scrolling.SaveScrollPosition = True
            RadGridDatos.DataBind()
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: método que permite obtener los datos vacios de los grid
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ConsultarVehiculo()
        Try
            Dim obj As New ConsultaWeb
            Dim dtInicial As New System.Data.DataSet
            dtInicial = CType(Session("Consulta_Vehiculo"), DataSet)
            dtInicial = obj.ConsultaInicialVehiculo(3)
            RadGridVehiculo.DataSource = Nothing
            Session("Consulta_InicialVehiculo") = dtInicial
            RadGridVehiculo.DataSource = dtInicial.Tables(0)
            RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
            RadGridVehiculo.Height = 110
            RadGridVehiculo.ClientSettings.Scrolling.AllowScroll = True
            RadGridVehiculo.ClientSettings.Scrolling.UseStaticHeaders = True
            RadGridVehiculo.ClientSettings.Scrolling.SaveScrollPosition = True
            RadGridVehiculo.DataBind()
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: método que permite consultar los datos deacuerdo a los criterios
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Consultar()
        Try
            Dim obj As New ConsultaWeb
            Dim dtConsulta As New System.Data.DataSet
            'Dim usuarioOficina As Integer = Session("user_id")
            dtConsulta = CType(Session("Consulta_Datos"), DataSet)
            'dt_consulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, Me.txtRuc.Text, Me.txtNombre.Text, Me.txtChasis.Text, Me.TxtPlaca.Text, Me.TxtMotor.Text)
            'dt_consulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text)
            dtConsulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, "", "", Me.txtChasis.Text, "", Me.TxtMotor.Text, usuarioOficina)
            'Dim ipmaquina As String = Request.ServerVariables("REMOTE_ADDR")
            'Dim pantalla As String = System.IO.Path.GetFileName(Request.PhysicalPath)
            '*obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "N", ipmaquina, pantalla, " ", False)
            RadGridDatos.DataSource = Nothing
            If dtConsulta.Tables(0).Rows.Count > 0 Then
                Session("Consulta_Datos") = dtConsulta
                RadGridDatos.DataSource = dtConsulta.Tables(0)
                RadGridDatos.MasterTableView.DataKeyNames = New String() {"ID_CLIENTE"}
                RadGridDatos.Height = 150
            Else
                RadGridDatos.DataSource = CType(Session("Consulta_Inicial"), DataSet).Tables(0)
                RadGridDatos.Height = 110
                Throw New Exception("No Existen Datos que Presentar, Por Verificar")
            End If
            RadGridDatos.ClientSettings.Scrolling.AllowScroll = True
            RadGridDatos.ClientSettings.Scrolling.UseStaticHeaders = True
            RadGridDatos.ClientSettings.Scrolling.SaveScrollPosition = True
            RadGridDatos.DataBind()
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


#End Region


#Region "DATOS DEL GRID"

    Protected Sub RadGridDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadGridDatos.SelectedIndexChanged
        Try
            Dim item As GridDataItem = Nothing
            Dim codigo As Integer
            item = RadGridDatos.SelectedItems(0)
            If Not (item Is Nothing) Then
                botones("Editar")
                codigo = item("ID_VEHICULO").Text.Trim.ToString
                If codigo > 0 Then
                    RadTabParametro.Tabs.Item(1).Enabled = True
                    RadTabParametro.Tabs.Item(0).Enabled = False
                    RadTabParametro.SelectedIndex = 1
                    botones("Consultar")
                    LimpiarControl()
                    CargaDatos()
                    RadMultiPage.SelectedIndex = 1
                    RadTabParametro.Tabs.Item(1).Enabled = True
                    RadTabParametro.Tabs.Item(0).Enabled = False
                    RadTabParametro.DataBind()
                    RadGridDatos.SelectedIndexes.Clear()
                    'LimpiarControl()
                    'RadGridVehiculo.DataSource = Nothing
                    RadGridVehiculo.DataSource = Session("Consulta_InicialVehiculo")
                    Dim obj As New ConsultaWeb
                    Dim dtVehiculo As New System.Data.DataSet
                    dtVehiculo = obj.ConsultaCodigo(Me.txtConCodVehiculo.Text, Me.txtConCodCliente.Text, "C", "")
                    If dtVehiculo.Tables(0).Rows.Count > 0 Then
                        Session("Detalle") = dtVehiculo
                        RadGridVehiculo.DataSource = dtVehiculo.Tables(0)
                        RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
                        RadGridVehiculo.Height = 150
                        Me.BtnImprimir.Enabled = True
                    Else
                        RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
                        Me.BtnImprimir.Enabled = False
                    End If
                    RadGridVehiculo.DataBind()
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

#End Region



    ''' <summary>
    ''' FECHA: 25/01/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: 
    ''' </summary>
    ''' <param name="url"></param>
    ''' <param name="target"></param>
    ''' <param name="windowFeatures"></param>
    ''' <remarks></remarks>
    ''' 
    Public Shared Sub Redirect(ByVal url As String, ByVal target As String, ByVal windowFeatures As String)
        Try
            Dim context As HttpContext = HttpContext.Current
            If ([String].IsNullOrEmpty(target) OrElse target.Equals("_self", StringComparison.OrdinalIgnoreCase)) AndAlso [String].IsNullOrEmpty(windowFeatures) Then
                context.Response.Redirect(url)
            Else
                Dim page As Page = DirectCast(context.Handler, Page)
                If page Is Nothing Then
                    Throw New InvalidOperationException("Cannot redirect to new window outside Page context.")
                End If
                url = page.ResolveClientUrl(url)
                Dim script As String
                If Not [String].IsNullOrEmpty(windowFeatures) Then
                    script = "window.open(""{0}"", ""{1}"", ""{2}"");"
                Else
                    script = "window.open(""{0}"", ""{1}"");"
                End If
                script = [String].Format(script, url, target, windowFeatures)
                ScriptManager.RegisterStartupScript(page, GetType(Page), "Redirect", script, True)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class