Imports System.Net.Mail
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MessagingToolkit.Barcode

Public Class ConsultaDatos
    Inherits System.Web.UI.Page

    Dim ipmaquina As String
    Dim pantalla As String
    Dim usuarioOficina As String
    Dim usuario As Integer = 0
    Dim accionHabilita As Boolean
    Dim varRei As Boolean
    Dim usuarioMaster As String
    Dim txtanio As String
    Dim datVehiculo As Int32
    Dim datOs As Int32
    Dim datCliente As String

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
                CargaListaTipo(Session("user_id"))
                Control_Sesion()
                Botones("Inicial")
                InicializaObjetos()
                usuario = Session("user_id")
                Session("dsDatos") = Nothing
                Session("Consulta_Datos") = Nothing
                Session("Consulta_Vehiculo") = Nothing
                usuarioOficina = Session("user_id")
                ipmaquina = Request.ServerVariables("REMOTE_ADDR")
                pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
                usuarioMaster = ""
                Me.lbl_canales.Text = Session("canales")
            Else
                If Not CType(Session("Detalle"), DataSet) Is Nothing Then
                    If CType(Session("Detalle"), DataSet).Tables.Count > 0 Then
                        RadGridVehiculo.DataSource = CType(Session("Detalle"), DataSet).Tables(0)
                    Else
                        Me.RadGridVehiculo.DataSource = Nothing
                    End If
                Else
                    Me.RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
                End If
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
                    Me.btnNuevo.Enabled = False
                    Me.btnConsultar.Enabled = True
                    Me.btnImprimir.Enabled = False
                    Me.btnCorreo.Enabled = False
                    'Me.btnEnviar.Enabled = False
                    Me.txtChasis.Enabled = True
                    Me.TxtMotor.Enabled = True
                    Me.txtCodVehiculo.Enabled = True
                    Me.cbm_tipo.Enabled = False
                    'Me.cbm_tipo.Enabled = True
                    Me.mensajetexto.Visible = False
                    Me.TxtCorreo.Visible = False
                    Me.BtnCancela.Visible = False
                    Me.BtnEnvia.Visible = False
                    Me.titcorreo.Visible = False
                    Me.txtConPlaca.Enabled = True 'MLNY 07-07-2023
                Case "Consultar"
                    Me.btnNuevo.Enabled = True
                    Me.btnConsultar.Enabled = False
                    Me.btnImprimir.Enabled = False
                    Me.btnCorreo.Enabled = False
                    'Me.btnEnviar.Enabled = False
                    Me.txtChasis.Enabled = False
                    Me.TxtMotor.Enabled = False
                    Me.txtCodVehiculo.Enabled = False
                    Me.cbm_tipo.Enabled = False
                    Me.txtConPlaca.Enabled = False 'MLNY 07-07-2023
            End Select
            Me.TxtConCliente.Enabled = False
            Me.txtConModelo.Enabled = False
            'Me.txtConPlaca.Enabled = False 'MLNY 07-07-2023
            Me.txtConTipo.Enabled = False
            Me.txtConColor.Enabled = False
            Me.txtConMarca.Enabled = False
            Me.txtConConcesionario.Enabled = False
            Me.txtConFinanciera.Enabled = False
            'Me.RadTextBox2.Enabled = False
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
    Private Sub Mensaje(ByVal texto As String, ByVal operacionRealizar As Int32, tipo As Exception)
        Try
            Dim titulo As String = String.Empty
            Dim icono As String = String.Empty
            Select Case operacionRealizar
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
            Me.RnMensajesError.Text = texto
            Me.RnMensajesError.Title = Titulo
            Me.RnMensajesError.TitleIcon = Icono
            Me.RnMensajesError.ContentIcon = Icono
            Me.RnMensajesError.Show()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


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


    Private Sub LimpiarControl()
        Try
            Me.txtCodVehiculo.Text = ""
            Me.txtChasis.Text = ""
            Me.TxtMotor.Text = ""
            Me.TxtCorreo.Text = ""
            Me.txtConCodCliente.Text = ""
            Me.TxtConCliente.Text = ""
            Me.txtConModelo.Text = ""
            Me.txtConPlaca.Text = ""
            Me.txtConTipo.Text = ""
            Me.txtConColor.Text = ""
            Me.txtConMarca.Text = ""
            Me.txtConConcesionario.Text = ""
            Me.txtConFinanciera.Text = ""
            Me.Txtconanio.Text = ""
            Me.txtCodConvenio.Text = ""
            Me.RadGridVehiculo.DataSource = Session("Consulta_InicialVehiculo")
            RadGridVehiculo.Height = 110
            RadGridVehiculo.DataBind()
            Session("Detalle") = Nothing
            Session("dtResultado") = Nothing
            Me.rgdResultado.DataSource = Session("dtResultado")
            Session("nombrepdf") = Nothing
            'Me.cbm_tipo.SelectedValue = "C"
            Me.cbm_tipo.SelectedValue = "N"
            Me.mensajetexto.Text = "Nota: Este certificado No esta habilitado para presentar precios de productos"
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
            ConsultarVehiculo()
            LimpiarControl()
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
                'MLNY 07-07-2023
            ElseIf Me.txtConPlaca.Text <> "" Then
                Validar = True
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function

#End Region

#Region "botones"


    Private Sub BtnConsultar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConsultar.Click
        Try
            If Validar() Then
                Botones("Inicial")
                'If cbm_tipo.SelectedValue.ToString = "N" Then
                '    Me.Mensaje("Seleccione el Tipo de Reporte a Consultar", Operacion.OInvalida)
                'Else
                Consultar()
                'End If
            Else
                Me.Mensaje("Ingrese datos para poder consultar", Operacion.OInvalida)
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub BtnNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnNuevo.Click
        Try
            Botones("Inicial")
            LimpiarControl()
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub BtnImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnImprimir.Click
        Try
            Dim obj As New ConsultaWeb
            If cbm_tipo.SelectedValue.ToString = "N" Then
                Me.Mensaje("Seleccione el Tipo de Reporte a Consultar", Operacion.OInvalida)
            Else
                Dim dtResultado As New System.Data.DataSet
                dtResultado = obj.Consulta_Mensaje(Me.txtCodVehiculo.Text, usuarioOficina, txtConPlaca.Text, TxtMotor.Text, txtChasis.Text, cbm_tipo.SelectedValue.ToString)
                Session("dtResultado") = dtResultado
                If dtResultado.Tables(0).Rows.Count > 0 Then
                    rgdResultado.DataSource = Session("dtResultado")
                    rgdResultado.DataBind()
                    rntResultado.Title = "Mensaje de la Aplicación Consulta Certificados"
                    rntResultado.TitleIcon = "info"
                    rntResultado.ContentIcon = "info"
                    rntResultado.ShowSound = "info"
                    rntResultado.Width = 380
                    rntResultado.Height = 250
                    rntResultado.Show()
                    'accionHabilita = 0
                Else
                    'Dim obj As New ConsultaWeb
                    Dim dtTransaccion As New System.Data.DataSet
                    Dim latam As Boolean = False
                    Dim casabaca As Boolean = False
                    Dim coneca As Boolean = False
                    Dim coris As Boolean = False
                    Dim induato As Boolean = False
                    Dim lader As Boolean = False
                    Dim conecel As Boolean = False
                    Dim ambacar As Boolean = False
                    Dim mareauto As Boolean = False
                    dtTransaccion = obj.Consulta_Transaccion(Me.txtCodVehiculo.Text, usuarioOficina, cbm_tipo.SelectedValue.ToString)
                    If dtTransaccion.Tables(0).Rows.Count > 0 Then
                        For b = 0 To dtTransaccion.Tables(0).Rows.Count - 1
                            If Me.txtCodConvenio.Text = "024" Or dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "LH" Then
                                lader = True
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "RB" Or dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "VC" Then
                                conecel = True
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "MH" Then
                                mareauto = True
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "AB" Then
                                ambacar = True
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "LA" Then
                                latam = True
                                'MsgBox("aqui...")
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "AT" Then
                                coris = True
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "HT" Then
                                casabaca = True
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "CM" Then
                                coneca = True
                                Exit For
                            End If
                            If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "IG" Then
                                induato = True
                                Exit For
                            End If
                        Next
                    End If
                    Session("cbm_tipo") = cbm_tipo.SelectedValue.ToString
                    obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, " ", latam, True, " Impreso: ")
                    If latam Then
                        Redirect("ConsultaReporteLatam.aspx", "_blank", "menubar=0,width=850,height=850")
                    Else
                        If casabaca Then
                            Redirect("ConsultaReporteCasabaca.aspx", "_blank", "menubar=0,width=850,height=850")
                        ElseIf coneca Or induato Or lader Or mareauto Then
                            Redirect("ConsultaReporte.aspx", "_blank", "menubar=0,width=850,height=850")
                        ElseIf conecel Then
                            Redirect("ConsultaReporteClaro.aspx", "_blank", "menubar=0,width=850,height=850")
                        ElseIf coris Then
                            Redirect("ConsultaWebReporte.aspx", "_blank", "menubar=0,width=850,height=850")
                        ElseIf ambacar Then
                            Redirect("ConsultaWebAmbacar.aspx", "_blank", "menubar=0,width=850,height=850")
                        Else
                            Redirect("ConsultaWebReporte.aspx", "_blank", "menubar=0,width=850,height=850")
                            'Redirect("ConsultaWebReporte2.aspx", "_blank", "menubar=0,width=850,height=850")
                        End If
                       
                    End If
                End If
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub BtnCorreo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCorreo.Click
        Try
            Dim obj As New ConsultaWeb
            If cbm_tipo.SelectedValue.ToString = "N" Then
                Me.Mensaje("Seleccione el Tipo de Reporte a Consultar", Operacion.OInvalida)
            Else
                Dim dtResultado As New System.Data.DataSet
                dtResultado = obj.Consulta_Mensaje(Me.txtCodVehiculo.Text, usuarioOficina, txtConPlaca.Text, TxtMotor.Text, txtChasis.Text, cbm_tipo.SelectedValue.ToString)
                Session("dtResultado") = dtResultado
                If dtResultado.Tables(0).Rows.Count > 0 Then
                    rgdResultado.DataSource = Session("dtResultado")
                    rgdResultado.DataBind()
                    rntResultado.Title = "Mensaje de la Aplicación Consulta Certificados"
                    rntResultado.TitleIcon = "info"
                    rntResultado.ContentIcon = "info"
                    rntResultado.ShowSound = "info"
                    rntResultado.Width = 380
                    rntResultado.Height = 250
                    rntResultado.Show()
                    'accionHabilita = 0
                Else
                    TxtCorreo.Visible = True
                    BtnCancela.Visible = True
                    BtnEnvia.Visible = True
                    titcorreo.Visible = True
                    'BtnConsultar.Visible = False
                    'BtnNuevo.Visible = False
                End If
            End If
            'Dim obj As New ConsultaWeb
            'obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, " ", False)
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub AddAttributesToRender(ByVal writer As HtmlTextWriter, ByVal email As String)
        'MyBase.AddAttributesToRender(writer)
        writer.AddAttribute(HtmlTextWriterAttribute.Href, email)
    End Sub


#End Region


#Region "Procedimientos generales"


    Private Sub CargaListaTipo(ByVal usuario As String)
        Try
            Dim dTListaTipo As DataSet
            dTListaTipo = ConsultaWeb.ConsultaTipo(usuario)
            If dTListaTipo.Tables(0).Rows.Count > 0 Then
                Me.cbm_tipo.DataSource = dTListaTipo
                Me.cbm_tipo.DataTextField = "DESCRIPCION"
                Me.cbm_tipo.DataValueField = "CODIGO"
                cbm_tipo.DataBind()
            End If
        Catch ex As Exception
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


    Private Sub InhabilitaCertificado(ByVal accion As Boolean)
        Try
            Me.btnImprimir.Enabled = accion
            Me.btnCorreo.Enabled = accion
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
            Session("Detalle") = Nothing
            Session("dtResultado") = Nothing
            Dim consultar As Boolean
            Dim obj As New ConsultaWeb
            Dim dtConsulta As New System.Data.DataSet
            dtConsulta = CType(Session("Consulta_Datos"), DataSet)
            'dtConsulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, "", "", Me.txtChasis.Text, "", Me.TxtMotor.Text, usuarioOficina)
            dtConsulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, "", "", Me.txtChasis.Text, Me.txtConPlaca.Text, Me.TxtMotor.Text, usuarioOficina) 'MLNY 07-07-2023
            consultar = False
            If dtConsulta.Tables(0).Rows.Count > 0 Then
                ''Inhabilito el botón de impresión de certificados si este campo viene con 0
                If cbm_tipo.SelectedValue.ToString = "N" Then
                    accionHabilita = dtConsulta.Tables(0).Rows(0)("CAMB_PROP_ESTADO").ToString()
                    varRei = dtConsulta.Tables(0).Rows(0)("ESTADO_REI").ToString()
                    If varRei = True Then
                        accionHabilita = 0
                    End If
                Else
                    accionHabilita = 1
                End If
                Dim dtResultado As New System.Data.DataSet
                dtResultado = obj.Consulta_Mensaje(Me.txtCodVehiculo.Text, usuarioOficina, txtConPlaca.Text, TxtMotor.Text, txtChasis.Text, cbm_tipo.SelectedValue.ToString)
                Session("dtResultado") = dtResultado
                If dtResultado.Tables(0).Rows.Count > 0 Then
                    rgdResultado.DataSource = Session("dtResultado")
                    rgdResultado.DataBind()
                    rntResultado.Title = "Mensaje de la Aplicación Consulta Certificados"
                    rntResultado.TitleIcon = "info"
                    rntResultado.ContentIcon = "info"
                    rntResultado.ShowSound = "info"
                    rntResultado.Width = 380
                    rntResultado.Height = 250
                    rntResultado.Show()
                    accionHabilita = 0
                End If
                '*Botones("Consultar")
                If accionHabilita <> 0 Then
                    consultar = True
                    Me.txtCodVehiculo.Text = dtConsulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
                    Me.txtChasis.Text = dtConsulta.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.TxtMotor.Text = dtConsulta.Tables(0).Rows(0)("MOTOR").ToString()
                    Me.txtConConcesionario.Text = dtConsulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
                    Me.txtConFinanciera.Text = dtConsulta.Tables(0).Rows(0)("FINANCIERA").ToString()
                    Me.TxtConCliente.Text = dtConsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.txtConCodCliente.Text = dtConsulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
                    Me.txtConMarca.Text = dtConsulta.Tables(0).Rows(0)("MARCA").ToString()
                    Me.txtConModelo.Text = dtConsulta.Tables(0).Rows(0)("MODELO").ToString()
                    Me.txtConPlaca.Text = dtConsulta.Tables(0).Rows(0)("PLACA").ToString()
                    Me.txtConTipo.Text = dtConsulta.Tables(0).Rows(0)("TIPO").ToString()
                    Me.txtConColor.Text = dtConsulta.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Txtconanio.Text = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
                    txtanio = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
                    Me.txtCodConvenio.Text = dtConsulta.Tables(0).Rows(0)("CODIGO_CONVENIO").ToString()
                    Dim dtVehiculo As New System.Data.DataSet
                    dtVehiculo = obj.ConsultaCodigo(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text, cbm_tipo.SelectedValue.ToString, usuarioOficina)
                    If dtVehiculo.Tables(0).Rows(0)("MENSAJE").ToString() = "SI" Then
                        mensajetexto.Visible = True
                    Else
                        mensajetexto.Visible = False
                    End If
                    If dtVehiculo.Tables(0).Rows.Count > 0 Then
                        Botones("Consultar")
                        Session("Detalle") = dtVehiculo
                        RadGridVehiculo.DataSource = dtVehiculo.Tables(0)
                        RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
                        RadGridVehiculo.Height = 150
                        Me.cbm_tipo.Enabled = True
                        Me.cbm_tipo.SelectedValue = "N"
                        'Me.btnImprimir.Enabled = True
                        'Me.BtnCorreo.Enabled = True
                    Else
                        consultar = False
                        RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
                        'Me.btnImprimir.Enabled = False
                        'Me.BtnCorreo.Enabled = False
                        accionHabilita = 0
                        Me.txtCodVehiculo.Text = ""
                        Me.txtChasis.Text = ""
                        Me.TxtMotor.Text = ""
                        Me.txtConConcesionario.Text = ""
                        Me.txtConFinanciera.Text = ""
                        Me.TxtConCliente.Text = ""
                        Me.txtConCodCliente.Text = ""
                        Me.txtConMarca.Text = ""
                        Me.txtConModelo.Text = ""
                        Me.txtConPlaca.Text = ""
                        Me.txtConTipo.Text = ""
                        Me.txtConColor.Text = ""
                        Me.Txtconanio.Text = ""
                        txtanio = ""
                    End If
                End If
                If cbm_tipo.SelectedValue.ToString = "N" Then
                    If Session("user_master") = "S" Then
                        consultar = True
                        Me.txtCodVehiculo.Text = dtConsulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
                        Me.txtChasis.Text = dtConsulta.Tables(0).Rows(0)("CHASIS").ToString()
                        Me.TxtMotor.Text = dtConsulta.Tables(0).Rows(0)("MOTOR").ToString()
                        Me.txtConConcesionario.Text = dtConsulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
                        Me.txtConFinanciera.Text = dtConsulta.Tables(0).Rows(0)("FINANCIERA").ToString()
                        Me.TxtConCliente.Text = dtConsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                        Me.txtConCodCliente.Text = dtConsulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
                        Me.txtConMarca.Text = dtConsulta.Tables(0).Rows(0)("MARCA").ToString()
                        Me.txtConModelo.Text = dtConsulta.Tables(0).Rows(0)("MODELO").ToString()
                        Me.txtConPlaca.Text = dtConsulta.Tables(0).Rows(0)("PLACA").ToString()
                        Me.txtConTipo.Text = dtConsulta.Tables(0).Rows(0)("TIPO").ToString()
                        Me.txtConColor.Text = dtConsulta.Tables(0).Rows(0)("COLOR").ToString()
                        Me.Txtconanio.Text = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
                        'Me.cbm_tipo.Enabled = True
                        'Me.cbm_tipo.SelectedValue = "N"
                    End If
                End If
                RadGridVehiculo.DataBind()
                InhabilitaCertificado(accionHabilita)
                Session("datocliente") = Me.TxtConCliente.Text + " | " + Me.txtConMarca.Text + " | " + Me.txtConModelo.Text + " | " + Me.txtChasis.Text + " | " + Me.txtConPlaca.Text
            Else
                'Throw New Exception("No Existen Datos que Presentar, Por Verificar")
                Me.Mensaje("No Existen Datos que Presentar, Por Verificar", Operacion.OInvalida)
            End If
            obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "N", ipmaquina, pantalla, " ", False, consultar, " Consulto: ")
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


#End Region

#Region "DATOS DEL GRID"


    'Protected Sub RadGridDatos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadGridDatos.SelectedIndexChanged
    '    Try
    '        Dim item As GridDataItem = Nothing
    '        Dim codigo As Integer
    '        'item = RadGridDatos.SelectedItems(0)
    '        If Not (item Is Nothing) Then
    '            botones("Editar")
    '            codigo = item("ID_VEHICULO").Text.Trim.ToString
    '            If codigo > 0 Then
    '                'RadTabParametro.Tabs.Item(1).Enabled = True
    '                'RadTabParametro.Tabs.Item(0).Enabled = False
    '                'RadTabParametro.SelectedIndex = 1
    '                botones("Consultar")
    '                LimpiarControl()
    '                CargaDatos()
    '                'RadMultiPage.SelectedIndex = 1
    '                'RadTabParametro.Tabs.Item(1).Enabled = True
    '                'RadTabParametro.Tabs.Item(0).Enabled = False
    '                'RadTabParametro.DataBind()
    '                'RadGridDatos.SelectedIndexes.Clear()
    '                'LimpiarControl()
    '                'RadGridVehiculo.DataSource = Nothing
    '                RadGridVehiculo.DataSource = Session("Consulta_InicialVehiculo")
    '                Dim obj As New ConsultaWeb
    '                Dim dt_vehiculo As New System.Data.DataSet
    '                dt_vehiculo = obj.ConsultaCodigo(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text)
    '                If dt_vehiculo.Tables(0).Rows.Count > 0 Then
    '                    Session("Detalle") = dt_vehiculo
    '                    RadGridVehiculo.DataSource = dt_vehiculo.Tables(0)
    '                    RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
    '                    RadGridVehiculo.Height = 150
    '                    Me.btnImprimir.Enabled = True
    '                Else
    '                    RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
    '                    Me.btnImprimir.Enabled = False
    '                End If
    '                RadGridVehiculo.DataBind()
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Captura_Error(ex)
    '    End Try
    'End Sub

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
    Public Shared Sub Redirect(ByVal url As String, ByVal target As String, ByVal windowFeatures As String)
        Try
            Dim context As HttpContext = HttpContext.Current
            If ([String].IsNullOrEmpty(target) OrElse target.Equals("_self", StringComparison.OrdinalIgnoreCase)) AndAlso [String].IsNullOrEmpty(windowFeatures) Then
                context.Response.Redirect(url)
            Else
                Dim page As System.Web.UI.Page = DirectCast(context.Handler, System.Web.UI.Page)
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


    Protected Sub Btnenviar(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnvia.Click
        Try
            If Me.TxtCorreo.Text = "" Then
                'Throw New Exception("No ha ingresado dirección de correo...")
                Me.Mensaje("No ha ingresado dirección de correo...", Operacion.OInvalida)
            Else
                Dim obj As New ConsultaWeb
                Dim dtTransaccion As New System.Data.DataSet
                Dim latam As Boolean = False
                Dim casabaca As Boolean = False
                Dim coris As Boolean = False
                Dim coneca As Boolean = False
                Dim induato As Boolean = False
                Dim financiera As Boolean = False
                Dim lader As Boolean = False
                Dim conecel As Boolean = False
                Dim ambacar As Boolean = False
                Dim mareauto As Boolean = False
                dtTransaccion = obj.Consulta_Transaccion(Me.txtCodVehiculo.Text, usuarioOficina, cbm_tipo.SelectedValue.ToString)
                If dtTransaccion.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtTransaccion.Tables(0).Rows.Count - 1
                        If Me.txtCodConvenio.Text = "024" Or dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "LH" Then
                            lader = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "RB" Or dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "VC" Then
                            conecel = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "MH" Then
                            mareauto = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "LA" Then
                            latam = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "AT" Then
                            coris = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "HT" Then
                            casabaca = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "CM" And dtTransaccion.Tables(0).Rows(i)("FINANCIADOR").ToString() = "ONE" Then
                            coneca = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("FINANCIADOR").ToString() = "ONE" Then
                            coneca = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("FINANCIADOR").ToString() = "ORI" Then
                            financiera = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "CH" Then
                            financiera = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "IG" Then
                            induato = True
                            Exit For
                        End If
                        If dtTransaccion.Tables(0).Rows(i)("PRODUCTO").ToString() = "AB" Then
                            ambacar = True
                            Exit For
                        End If

                    Next
                End If
                Dim orden As String = ""
                If Not latam Then
                    ' Se llama procedimiento que genera el archivo PDF
                    CargaImagenQR()
                    If casabaca Then
                        GeneraPdf_Casabaca()
                    ElseIf conecel Then
                        GeneraPdf_Click("CNL")
                    ElseIf coris Then
                        GeneraPdf_Click("COR")
                    ElseIf coneca Then
                        GeneraPdf_Click("ONE")
                    ElseIf financiera Then
                        GeneraPdf_Click("CON")
                    ElseIf induato Then
                        GeneraPdf_Click("IND")
                    ElseIf lader Then
                        GeneraPdf_Click("LAD")
                    ElseIf ambacar Then
                        GeneraPdf_Click("AMB")
                    ElseIf mareauto Then
                        GeneraPdf_Click("MAR")
                    Else
                        GeneraPdf_Click("NOR")
                    End If
                Else
                    dtTransaccion = obj.Consulta_Orden(Me.txtCodVehiculo.Text, usuarioOficina, Me.txtConCodCliente.Text, "9")
                    orden = dtTransaccion.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
                End If
                Dim htmlcuerpo As String = ""
                If latam Then
                    htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body> <br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Venta del Producto</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                Else
                    If cbm_tipo.SelectedValue.ToString = "C" Then
                        htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Venta del Producto Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                    ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                        htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Desinstalación del Producto de Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                    ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                        htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Instalación del Producto de Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                    End If
                End If
                'htmlcuerpo = "<HTML> <font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font>  <BODY> <br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Instalación del Producto Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Marca : </font> <font style=""color: #666666;"">" + "&nbsp;" + Me.txtConMarca.Text + "</font> <br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Modelo: </font> <font style=""color: #666666;"">" + Me.txtConModelo.Text + "</font><br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Chasis: </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + Me.txtChasis.Text + "</font><br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Placa : </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + "&nbsp;" + Me.txtConPlaca.Text + "</font><br/>"
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
                If latam Then
                    mailMessage.Subject = "Certificado de Instalación del Producto, " + Session("datocliente")
                    Session("nombrepdf") = "\\10.100.107.14\LatamAUTOS_Certificado\cert_" & Me.txtConCodCliente.Text & "_" & Me.txtCodVehiculo.Text & "_" & orden & ".pdf"
                Else
                    If cbm_tipo.SelectedValue.ToString = "C" Then
                        mailMessage.Subject = "Certificado de Venta del Producto Hunter, " + Session("datocliente")
                    ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                        mailMessage.Subject = "Certificado de Desinstalación del Producto Hunter, " + Session("datocliente")
                    ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                        mailMessage.Subject = "Certificado de Instalación del Producto Hunter, " + Session("datocliente")
                    End If
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
                Dim smtpClient As New SmtpClient("10.100.89.34")
                ' Para enviar 2 destinatarios
                Dim correo1 As String
                Dim correo2 As String = ""
                correo1 = Catalogo(texto:=Me.TxtCorreo.Text)
                correo2 = Catalogo(texto:=Me.TxtCorreo.Text, valorDevolver:="D")
                'mailMessage.To.Add(Me.TxtCorreo.Text)
                mailMessage.To.Add(correo1)
                'smtpClient.Send(mailMessage)
                If correo2 <> "" Then
                    mailMessage.To.Add(correo2)
                End If
                smtpClient.Send(mailMessage)
                mailMessage.Dispose()
                TxtCorreo.Visible = False
                BtnCancela.Visible = False
                BtnEnvia.Visible = False
                titcorreo.Visible = False
                BtnConsultar.Visible = True
                BtnNuevo.Visible = True
                BtnCorreo.Enabled = True
                BtnNuevo.Enabled = True
                BtnImprimir.Enabled = False
                obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, Me.TxtCorreo.Text, latam, True, " Email: ")
                TxtCorreo.Text = ""
                If Not latam Then
                    'borra el archivo pdf
                    Dim destino As String = Session("nombrepdf")
                    If (System.IO.File.Exists(destino)) Then
                        System.IO.File.Delete(destino)
                    End If
                End If
                rntResultado.Title = "Se ha enviado el archivo pdf correctamente"
                rntResultado.TitleIcon = "info"
                rntResultado.ContentIcon = "info"
                rntResultado.ShowSound = "info"
                rntResultado.Width = 380
                rntResultado.Height = 100
                rntResultado.Show()
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
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


    Protected Sub Bntcancelar(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCancela.Click
        Try
            TxtCorreo.Visible = False
            BtnCancela.Visible = False
            BtnEnvia.Visible = False
            titcorreo.Visible = False
            BtnConsultar.Visible = True
            BtnNuevo.Visible = True
            BtnCorreo.Enabled = True
            TxtCorreo.Text = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub GeneraPdf_Click(origen As String)
        Try
            Dim fontNormal As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL)
            'Dim fontBold As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD)
            'Dim fontTitulo As iTextSharp.text.Font = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.UNDERLINE)
            Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
            'Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.NORMAL)
            Dim fontcv As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
            Dim fontTitulo As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 13, iTextSharp.text.Font.BOLD)
            Dim grey As New BaseColor(64, 64, 64)
            'Dim grey As New BaseColor(128, 128, 128)
            Dim negro As New BaseColor(0, 0, 0)
            Dim fontgris As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, grey)
            Dim fontnegro As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, negro)
            Dim espacio As String = Space(8)
            Dim espacio2 As String = Space(6)
            Dim dtconsulta2 As New System.Data.DataSet
            dtconsulta2 = Session("infoqr")
            Dim documento As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim file As String = "\\10.100.107.14\ImagenesDocumentos\cert_" & Me.txtCodVehiculo.Text & ".pdf"
            'Dim filePdf As String = fila.RUTA & fila.ID_CLIENTE & "_" & fila.ID_VEHICULO & "_" & fila.NUMERO_GENERAL & ".pdf"
            Session("nombrepdf") = file
            Dim writer As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(file, FileMode.Create))
            Dim lineablanco As New iTextSharp.text.Paragraph(" ")
            'Dim ev As New creacionpdf()

            If origen = "NOR" Or origen = "CON" Or origen = "IND" Then

                If cbm_tipo.SelectedValue.ToString = "C" Then
                    Dim ev As New creacionpdf()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                    Dim ev As New creacionpdf_des()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                    Dim ev As New creacionpdf_ins()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                End If
            End If

            If origen = "MAR" Then
                If cbm_tipo.SelectedValue.ToString = "C" Then
                    Dim ev As New creacionpdf_mareauto()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                    ' ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                    '   Dim ev As New creacionpdf_lad()
                    ' documento.Open()
                    ' documento.NewPage()
                    ' writer.PageEvent = ev
                ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                    Dim ev As New creacionpdf_ins_mareauto()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                End If
            End If

            If origen = "LAD" Then
                If cbm_tipo.SelectedValue.ToString = "C" Then
                    Dim ev As New creacionpdf_lad()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                    Dim ev As New creacionpdf_lad()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                    Dim ev As New creacionpdf_lad()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                End If
            End If
            If origen = "ONE" Then
                If cbm_tipo.SelectedValue.ToString = "C" Then
                    Dim ev As New creacionpdf_coneca()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                    Dim ev As New creacionpdf_coneca()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                    Dim ev As New creacionpdf_coneca()
                    documento.Open()
                    documento.NewPage()
                    writer.PageEvent = ev
                End If
            End If
            If origen = "COR" Then
                'If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim ev As New creacionpdf_cor()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
                'ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                '    Dim ev As New creacionpdf_cor()
                '    documento.Open()
                '    documento.NewPage()
                '    writer.PageEvent = ev
                'ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                '    Dim ev As New creacionpdf_cor()
                '    documento.Open()
                '    documento.NewPage()
                '    writer.PageEvent = ev
                'End If
            End If
            If origen = "AMB" Then
                Dim ev As New creacionpdf_amb()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
            End If
            If origen = "CNL" Then
                Dim ev As New creacionpdf_cnl()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
            End If
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            Dim tablacv As New PdfPTable(2)
            tablacv.SetWidths(New Single() {160.0F, 40.0F})

            If origen = "COR" Or origen = "LAD" Or origen = "CNL" Or origen = "AMB" Then
                Dim nombtitulo As String = ""
                If cbm_tipo.SelectedValue.ToString = "C" Then
                    nombtitulo = " CERTIFICADO DE VENTA  "
                ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                    nombtitulo = " CERTIFICADO DE DESINTALACIÓN "
                ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                    nombtitulo = " CERTIFICADO DE INSTALACIÓN "
                End If
                Dim titulo As New PdfPCell(New Phrase(nombtitulo, fontTitulo))
                titulo.Border = 0
                titulo.Colspan = 2
                titulo.BackgroundColor = New BaseColor(230, 230, 230)
                titulo.HorizontalAlignment = Element.ALIGN_CENTER
                tablacv.AddCell(titulo)
                'documento.Add(tablacv)
                Dim codigoblan2 As New PdfPCell(New Phrase(" "))
                codigoblan2.Border = 0
                'codigoblan.BackgroundColor = New BaseColor(230, 230, 230)
                codigoblan2.HorizontalAlignment = Element.ALIGN_RIGHT
                tablacv.AddCell(codigoblan2)
                tablacv.AddCell(codigoblan2)
            End If
            Dim codigoblan As New PdfPCell(New Phrase(" "))
            codigoblan.Border = 0
            'codigoblan.BackgroundColor = New BaseColor(230, 230, 230)
            codigoblan.HorizontalAlignment = Element.ALIGN_RIGHT
            tablacv.AddCell(codigoblan)
            Dim codigoveh As New PdfPCell(New Phrase("C.V. " & Me.txtCodVehiculo.Text, fontcv))
            codigoveh.Border = 0
            codigoveh.BackgroundColor = New BaseColor(230, 230, 230)
            codigoveh.HorizontalAlignment = Element.ALIGN_RIGHT
            tablacv.AddCell(codigoveh)
            documento.Add(tablacv)
            documento.Add(lineablanco)
            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim textoParrafo As New PdfPTable(1)
                textoParrafo.SetWidths(New Single() {200.0F})
                Dim tituloText1 As New PdfPCell(New Phrase("Certificamos que el Sr./Sra./Empresa " & Me.TxtConCliente.Text, fontcliente))
                tituloText1.Border = 0
                textoParrafo.AddCell(tituloText1)
                textoParrafo.AddCell(codigoblan)
                textoParrafo.AddCell(codigoblan)
                'textoParrafo.AddCell(lineablanco)
                Dim tituloText2 As New PdfPCell(New Phrase("Ha contratado el producto:", fontNormal))
                'Dim tituloText2 As New PdfPCell(New Phrase("Ha adquirido los siguientes sistemas:", fontNormal))
                tituloText2.Border = 0
                textoParrafo.AddCell(tituloText2)
                textoParrafo.AddCell(codigoblan)
                documento.Add(textoParrafo)
                documento.Add(lineablanco)
                ' Impresion de los productos , se genera tabla
                Dim tabla As New PdfPTable(5)
                If origen = "COR" Or origen = "CON" Or origen = "ONE" Or origen = "IND" Or origen = "LAD" Or origen = "AMB" Or origen = "MAR" Then
                    tabla.SetWidths(New Single() {5.0F, 40.0F, 25.0F, 15.0F, 5.0F})
                Else
                    tabla.SetWidths(New Single() {5.0F, 40.0F, 25.0F, 25.0F, 5.0F})
                End If
                Dim titulo4 As New PdfPCell(New Phrase(" ", fontnegro))
                titulo4.Border = 0
                'titulo4.BackgroundColor = New BaseColor(230, 230, 230)
                titulo4.HorizontalAlignment = Element.ALIGN_RIGHT
                tabla.AddCell(titulo4)
                Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontnegro))
                titulo1.Border = 0
                'titulo1.Border = 2
                titulo1.BackgroundColor = New BaseColor(230, 230, 230)
                titulo1.HorizontalAlignment = Element.ALIGN_CENTER
                tabla.AddCell(titulo1)
                Dim titulo2 As New PdfPCell(New Phrase("VIGENCIA DEL SERVICIO*", fontnegro))
                titulo2.Border = 0
                titulo2.BackgroundColor = New BaseColor(230, 230, 230)
                titulo2.HorizontalAlignment = Element.ALIGN_LEFT
                tabla.AddCell(titulo2)
                If origen = "COR" Or origen = "CON" Or origen = "ONE" Or origen = "IND" Or origen = "LAD" Or origen = "AMB" Or origen = "MAR" Then
                    tabla.AddCell(titulo4)
                Else
                    Dim titulo3 As New PdfPCell(New Phrase("P.V.P INCLUIDO IVA", fontnegro))
                    titulo3.Border = 0
                    titulo3.BackgroundColor = New BaseColor(230, 230, 230)
                    titulo3.HorizontalAlignment = Element.ALIGN_RIGHT
                    tabla.AddCell(titulo3)
                End If
                tabla.AddCell(titulo4)
                If dtconsulta2.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                        tabla.AddCell(titulo4)
                        Dim det1 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("PRODUCTO").ToString(), fontNormal))
                        det1.Border = 0
                        det1.HorizontalAlignment = Element.ALIGN_LEFT
                        tabla.AddCell(det1)
                        Dim fechafinal As String = ""
                        Dim valorfecha As String
                        valorfecha = dtconsulta2.Tables(0).Rows(i)("COBERTURA").ToString()
                        If Mid(valorfecha, 4, 2) = "01" Then fechafinal = Mid(valorfecha, 1, 3) & "Ene/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "02" Then fechafinal = Mid(valorfecha, 1, 3) & "Feb/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "03" Then fechafinal = Mid(valorfecha, 1, 3) & "Mar/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "04" Then fechafinal = Mid(valorfecha, 1, 3) & "Abr/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "05" Then fechafinal = Mid(valorfecha, 1, 3) & "May/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "06" Then fechafinal = Mid(valorfecha, 1, 3) & "Jun/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "07" Then fechafinal = Mid(valorfecha, 1, 3) & "Jul/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "08" Then fechafinal = Mid(valorfecha, 1, 3) & "Ago/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "09" Then fechafinal = Mid(valorfecha, 1, 3) & "Sep/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "10" Then fechafinal = Mid(valorfecha, 1, 3) & "Oct/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "11" Then fechafinal = Mid(valorfecha, 1, 3) & "Nov/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "12" Then fechafinal = Mid(valorfecha, 1, 3) & "Dic/" & Mid(valorfecha, 7, 4)
                        Dim det2 As New PdfPCell(New Phrase(fechafinal, fontNormal))
                        det2.Border = 0
                        det2.HorizontalAlignment = Element.ALIGN_LEFT
                        tabla.AddCell(det2)
                        If origen = "COR" Or origen = "CON" Or origen = "ONE" Or origen = "IND" Or origen = "LAD" Or origen = "AMB" Or origen = "MAR" Then
                            Dim det5 As New PdfPCell(New Phrase("    ", fontNormal))
                            det5.Border = 0
                            det5.HorizontalAlignment = Element.ALIGN_RIGHT
                            tabla.AddCell(det5)
                        Else
                            Dim det3 As New PdfPCell(New Phrase("$" & dtconsulta2.Tables(0).Rows(i)("PRECIO_PRODUCTO").ToString(), fontNormal))
                            det3.Border = 0
                            det3.HorizontalAlignment = Element.ALIGN_RIGHT
                            tabla.AddCell(det3)
                        End If

                        Dim det4 As New PdfPCell(New Phrase("    ", fontNormal))
                        det4.Border = 0
                        det4.HorizontalAlignment = Element.ALIGN_RIGHT
                        tabla.AddCell(det4)
                    Next
                End If
                documento.Add(tabla)
                documento.Add(lineablanco)
            End If
            If cbm_tipo.SelectedValue.ToString = "I" And (origen = "CNL" Or origen = "AMB" Or origen = "MAR") Then
                Dim textoParrafo As New PdfPTable(1)
                textoParrafo.SetWidths(New Single() {200.0F})
                Dim tituloText1 As New PdfPCell(New Phrase("Certificamos que el Sr./Sra./Empresa " & Me.TxtConCliente.Text, fontcliente))
                tituloText1.Border = 0
                textoParrafo.AddCell(tituloText1)
                textoParrafo.AddCell(codigoblan)
                textoParrafo.AddCell(codigoblan)
                'textoParrafo.AddCell(lineablanco)
                Dim tituloText2 As New PdfPCell(New Phrase("Ha adquirido los siguientes sistemas:", fontNormal))
                tituloText2.Border = 0
                textoParrafo.AddCell(tituloText2)
                textoParrafo.AddCell(codigoblan)
                documento.Add(textoParrafo)
                documento.Add(lineablanco)
                ' Impresion de los productos , se genera tabla
                Dim tabla As New PdfPTable(5)
                tabla.SetWidths(New Single() {5.0F, 48.0F, 25.0F, 15.0F, 5.0F})
                Dim titulo4 As New PdfPCell(New Phrase(" ", fontnegro))
                titulo4.Border = 0
                'titulo4.BackgroundColor = New BaseColor(230, 230, 230)
                titulo4.HorizontalAlignment = Element.ALIGN_RIGHT
                tabla.AddCell(titulo4)
                Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontnegro))
                titulo1.Border = 0
                'titulo1.Border = 2
                titulo1.BackgroundColor = New BaseColor(230, 230, 230)
                titulo1.HorizontalAlignment = Element.ALIGN_CENTER
                tabla.AddCell(titulo1)
                Dim titulo2 As New PdfPCell(New Phrase("INSTALACIÓN", fontnegro))
                titulo2.Border = 0
                titulo2.BackgroundColor = New BaseColor(230, 230, 230)
                titulo2.HorizontalAlignment = Element.ALIGN_LEFT
                tabla.AddCell(titulo2)
                tabla.AddCell(titulo4)
                tabla.AddCell(titulo4)

                If dtconsulta2.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                        tabla.AddCell(titulo4)
                        Dim det1 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("PRODUCTO").ToString(), fontNormal))
                        det1.Border = 0
                        det1.HorizontalAlignment = Element.ALIGN_LEFT
                        tabla.AddCell(det1)
                        Dim fechafinal As String = ""
                        Dim valorfecha As String
                        If origen = "AMB" Then
                            valorfecha = dtconsulta2.Tables(0).Rows(i)("COBERTURA").ToString()
                        Else
                            valorfecha = dtconsulta2.Tables(0).Rows(i)("COBERTURA_INI").ToString()
                        End If
                        If Mid(valorfecha, 4, 2) = "01" Then fechafinal = Mid(valorfecha, 1, 3) & "Ene/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "02" Then fechafinal = Mid(valorfecha, 1, 3) & "Feb/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "03" Then fechafinal = Mid(valorfecha, 1, 3) & "Mar/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "04" Then fechafinal = Mid(valorfecha, 1, 3) & "Abr/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "05" Then fechafinal = Mid(valorfecha, 1, 3) & "May/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "06" Then fechafinal = Mid(valorfecha, 1, 3) & "Jun/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "07" Then fechafinal = Mid(valorfecha, 1, 3) & "Jul/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "08" Then fechafinal = Mid(valorfecha, 1, 3) & "Ago/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "09" Then fechafinal = Mid(valorfecha, 1, 3) & "Sep/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "10" Then fechafinal = Mid(valorfecha, 1, 3) & "Oct/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "11" Then fechafinal = Mid(valorfecha, 1, 3) & "Nov/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "12" Then fechafinal = Mid(valorfecha, 1, 3) & "Dic/" & Mid(valorfecha, 7, 4)
                        Dim det2 As New PdfPCell(New Phrase(fechafinal, fontNormal))
                        det2.Border = 0
                        det2.HorizontalAlignment = Element.ALIGN_LEFT
                        tabla.AddCell(det2)
                        Dim det5 As New PdfPCell(New Phrase("    ", fontNormal))
                        det5.Border = 0
                        det5.HorizontalAlignment = Element.ALIGN_RIGHT
                        tabla.AddCell(det5)


                        Dim det4 As New PdfPCell(New Phrase("    ", fontNormal))
                        det4.Border = 0
                        det4.HorizontalAlignment = Element.ALIGN_RIGHT
                        tabla.AddCell(det4)
                    Next
                End If
                documento.Add(tabla)
                documento.Add(lineablanco)





            End If


            documento.Add(lineablanco)
            Dim texto2 As String = ""
            Dim texto3 As String = ""
            If cbm_tipo.SelectedValue.ToString = "C" Then
                texto2 = "Con Orden de Instalación en el vehículo: "
                texto3 = "*La vigencia del servicio se contará desde la instalación del equipo. "
            ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                texto2 = "Se certifica que se ha realizado la desinstalación con las siguientes características:"
            ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                texto2 = "En el vehículo con las siguientes características:"
            End If
            'Dim linea2 As New iTextSharp.text.Paragraph(espacio & texto2, fontNormal)
            'documento.Add(linea2)

            Dim textoParrafo2 As New PdfPTable(1)
            textoParrafo2.SetWidths(New Single() {240.0F})

            If cbm_tipo.SelectedValue.ToString = "C" Then
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
            Dim vehic8 As New PdfPCell(New Phrase(Me.txtConPlaca.Text, fontgris))
            vehic8.Border = 0
            vehic8.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic8)
            Dim vehic13 As New PdfPCell(New Phrase("COLOR", fontnegro))
            vehic13.Border = 0
            vehic13.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic13)
            'Dim vehic14 As New PdfPCell(New Phrase(Me.Color.Text, fontgris))
            Dim vehic14 As New PdfPCell(New Phrase(Me.txtConColor.Text, fontgris))
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
            Dim vehic2 As New PdfPCell(New Phrase(Me.txtConMarca.Text, fontgris))
            'Dim vehic2 As New PdfPCell(New Phrase(Me.Marca.Text, fontNormal))
            vehic2.Border = 0
            vehic2.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic2)
            Dim vehic3 As New PdfPCell(New Phrase("AÑO", fontnegro))
            vehic3.Border = 0
            vehic3.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic3)
            Dim vehic4 As New PdfPCell(New Phrase(Me.Txtconanio.Text, fontgris))
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
            Dim vehic6 As New PdfPCell(New Phrase(Me.txtConModelo.Text, fontgris))
            vehic6.Border = 0
            vehic6.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic6)
            Dim vehic15 As New PdfPCell(New Phrase("MOTOR", fontnegro))
            vehic15.Border = 0
            vehic15.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic15)
            'Dim vehic16 As New PdfPCell(New Phrase(Me.Motor.Text, fontgris))
            Dim vehic16 As New PdfPCell(New Phrase(Me.TxtMotor.Text, fontgris))
            vehic16.Border = 0
            vehic16.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic16)
            Dim vehic9 As New PdfPCell(New Phrase("TIPO", fontnegro))
            vehic9.Border = 0
            vehic9.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic9)
            'Dim vehic10 As New PdfPCell(New Phrase(Me.Tipo.Text, fontgris))
            Dim vehic10 As New PdfPCell(New Phrase(Me.txtConTipo.Text, fontgris))
            vehic10.Border = 0
            vehic10.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic10)
            Dim vehic11 As New PdfPCell(New Phrase("CHASIS", fontnegro))
            vehic11.Border = 0
            vehic11.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic11)
            'Dim vehic12 As New PdfPCell(New Phrase(Me.Chasis.Text, fontgris))
            Dim vehic12 As New PdfPCell(New Phrase(Me.txtChasis.Text, fontgris))
            vehic12.Border = 0
            vehic12.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic12)
            documento.Add(tabladatos)


            
            documento.Add(lineablanco)
            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim textoParrafo21 As New PdfPTable(1)
                textoParrafo21.SetWidths(New Single() {240.0F})
                Dim tituloText42 As New PdfPCell(New Phrase("El presente certificado, no constituye confirmación de instalación del equipo.", fontNormal))
                tituloText42.Border = 0
                tituloText42.HorizontalAlignment = Element.ALIGN_CENTER
                textoParrafo21.AddCell(tituloText42)
                documento.Add(textoParrafo21)
            End If




            documento.Add(lineablanco)

            If origen = "CNL" Then
                ' PRODUCTOS INSTALADOS
                Dim productodesc As New PdfPTable(3)
                'productodesc.SetWidths(New Single() {10.0F, 20.0F, 180.0F})
                productodesc.SetWidths(New Single() {10.0F, 20.0F, 180.0F})
                Dim prod1 As New PdfPCell(New Phrase(" Funcionalidades disponible en la Aplicación ", fontcliente))
                prod1.Border = 0
                prod1.Colspan = 3
                prod1.HorizontalAlignment = Element.ALIGN_LEFT
                productodesc.AddCell(prod1)


                Dim obj As New ConsultaWeb
                Dim dtproducto As New System.Data.DataSet
                dtproducto = obj.CertificadoDetalleServicios("19", dtconsulta2.Tables(0).Rows(0)("ID_CLIENTE").ToString(), dtconsulta2.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtconsulta2.Tables(0).Rows(0)("NUMERO_GENERAL").ToString(), usuarioOficina)

                If dtproducto.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtproducto.Tables(0).Rows.Count - 1
                        Dim prod2 As New PdfPCell(New Phrase(" ", fontgris))
                        prod2.Border = 0
                        prod2.HorizontalAlignment = Element.ALIGN_CENTER
                        productodesc.AddCell(prod2)
                        Dim check As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance("https://www.hunteronline.com.ec/IMGCOTIZADORWEB/Imagenescampanias/chequeado.png")
                        check.ScalePercent(75.0F)
                        Dim prod3 As New PdfPCell(check)
                        prod3.Border = 0
                        prod3.HorizontalAlignment = Element.ALIGN_CENTER
                        productodesc.AddCell(prod3)
                        Dim prod4 As New PdfPCell(New Phrase(dtproducto.Tables(0).Rows(i)("DESCRIPCION").ToString(), fontgris))
                        prod4.Border = 0
                        prod4.HorizontalAlignment = Element.ALIGN_LEFT
                        productodesc.AddCell(prod4)


                    Next

                    Dim prod5 As New PdfPCell(New Phrase("  ", fontgris))
                    prod5.Border = 0
                    prod5.Colspan = 3
                    prod5.HorizontalAlignment = Element.ALIGN_LEFT
                    productodesc.AddCell(prod5)

                    Dim prod6 As New PdfPCell(New Phrase("  ", fontgris))
                    prod6.Border = 0
                    prod6.Colspan = 3
                    prod6.HorizontalAlignment = Element.ALIGN_LEFT
                    productodesc.AddCell(prod6)

                    Dim texto As String = " El cliente podrá monitorear su vehículo a través de la app Claro Flotas Powered by Hunter y/o página web https://www.flotasmonitoreo.claro.com.ec/Artemis  "
                    Dim prod7 As New PdfPCell(New Phrase(texto.ToUpper(), fontgris))
                    prod7.Border = 0
                    prod7.Colspan = 3
                    prod7.HorizontalAlignment = Element.ALIGN_LEFT
                    productodesc.AddCell(prod7)

                    texto = " *El cliente podrá utilizar únicamente las funcionalidades del sistema de monitoreo habilitadas durante la instalación.  "
                    Dim prod8 As New PdfPCell(New Phrase(texto.ToUpper(), fontgris))
                    prod8.Border = 0
                    prod8.Colspan = 3
                    prod8.HorizontalAlignment = Element.ALIGN_LEFT
                    productodesc.AddCell(prod8)
                    documento.Add(productodesc)
                End If
            Else
                documento.Add(lineablanco)

                If origen = "LAD" Or origen = "AMB" Or origen = "MAR" Then
                    documento.Add(lineablanco)
                Else
                    ' Imprime el codigo de barra
                    Dim codbarra As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(Session("nombrebarra")))
                    codbarra.ScalePercent(75.0F)
                    documento.Add(codbarra)
                End If
                
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)
                documento.Add(lineablanco)

            End If
            documento.Add(lineablanco)

            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim textoParrafo22 As New PdfPTable(1)
                textoParrafo22.SetWidths(New Single() {240.0F})
                Dim tituloText43 As New PdfPCell(New Phrase("Los términos y condiciones del servicio pueden ser consultadas en nuestra página web", fontNormal))
                tituloText43.Border = 0
                tituloText43.HorizontalAlignment = Element.ALIGN_LEFT
                textoParrafo22.AddCell(tituloText43)
                documento.Add(textoParrafo22)
                'Else
                '    
            End If
            documento.Add(lineablanco)
            ' Fecha de emisión del pdf
            Dim lblfechahoraemision As String
            lblfechahoraemision = "Info Emisión Docto : " & Session("user_session") & " | " & System.DateTime.Now.ToString("dd MMMM yyyy") & " | " & System.DateTime.Now.ToString("H:mm:ss")
            'lblfechahoraemision = espacio & lblfechahoraemision
            'Dim fecha As New iTextSharp.text.Paragraph(lblfechahoraemision, fontNormal)
            'documento.Add(fecha)
            Dim fechatrabajo As String = ""
            Dim valortrabajo As String = dtconsulta2.Tables(0).Rows(0)("FECHA_TRABAJO").ToString()
            If Mid(valortrabajo, 4, 2) = "01" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Enero/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "02" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Febrero/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "03" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Marzo/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "04" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Abril/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "05" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Mayo/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "06" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Junio/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "07" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Julio/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "08" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Agosto/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "09" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Septiembre/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "10" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Octubre/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "11" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Noviembre/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "12" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Diciembre/" & Mid(valortrabajo, 7, 4)
            Dim tablacv2 As New PdfPTable(2)
            tablacv2.SetWidths(New Single() {80.0F, 120.0F})
            If cbm_tipo.SelectedValue.ToString = "I" Then
                Dim fechaInstalacion = "Fecha Instalación: " & fechatrabajo
                If origen = "CNL" Then
                    fechaInstalacion = ""
                End If
                'Dim fecha As New PdfPCell(New Phrase("Fecha Instalación: " & fechatrabajo, fontNormal))
                Dim fecha As New PdfPCell(New Phrase(fechaInstalacion, fontNormal))
                fecha.Border = 0
                fecha.HorizontalAlignment = Element.ALIGN_LEFT
                tablacv2.AddCell(fecha)
                tablacv2.AddCell(codigoblan)
            ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                Dim fecha As New PdfPCell(New Phrase("Fecha Desinstalación: " & fechatrabajo, fontNormal))
                fecha.Border = 0
                fecha.HorizontalAlignment = Element.ALIGN_LEFT
                tablacv2.AddCell(fecha)
                tablacv2.AddCell(codigoblan)
            End If
            tablacv2.AddCell(codigoblan)
            Dim codigofecha As New PdfPCell(New Phrase(lblfechahoraemision, fontNormal))
            codigofecha.Border = 0
            codigofecha.HorizontalAlignment = Element.ALIGN_RIGHT
            tablacv2.AddCell(codigofecha)
            documento.Add(tablacv2)
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            ' Imprime logo del fin de pagina 
            If origen = "NOR" Then
                Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_footer_certificado.jpg"))
                logo.ScalePercent(75.0F)
                documento.Add(logo)
            ElseIf origen = "COR" Then
            ElseIf origen = "CNL" Then
                'Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance("https://www.hunteronline.com.ec/IMGCOTIZADORWEB/Imagenescampanias/LogoHunterAgua.png")
                'logo.ScalePercent(75.0F)
                'logo.Alignment = iTextSharp.text.Image.ALIGN_CENTER
                'documento.Add(logo)
            ElseIf origen = "CON" Or origen = "ONE" Or origen = "IND" Or origen = "AMB" Or origen = "MAR" Then
            End If
            'logo.Left = 10
            'logo.Right = 10
            'logo.HorizontalAlignment = Element.ALIGN_CENTER
            documento.Close()
            documento.Dispose()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub GeneraPdf_Casabaca()
        Try
            Dim fontNormal As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL)
            Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
            Dim fontcv As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
            Dim grey As New BaseColor(64, 64, 64)
            Dim negro As New BaseColor(0, 0, 0)
            Dim fontgris As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, grey)
            Dim fontnegro As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL, negro)
            Dim espacio As String = Space(8)
            Dim espacio2 As String = Space(6)
            Dim dtconsulta2 As New System.Data.DataSet
            dtconsulta2 = Session("infoqr")
            Dim documento As New Document(PageSize.A4, 10.0F, 10.0F, 100.0F, 0.0F)
            Dim file As String = "\\10.100.107.14\ImagenesDocumentos\cert_" & Me.txtCodVehiculo.Text & ".pdf"
            Session("nombrepdf") = file
            Dim writer As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(file, FileMode.Create))
            Dim lineablanco As New iTextSharp.text.Paragraph(" ")
            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim ev As New creacionpdf()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
            ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                Dim ev As New creacionpdf_des()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
            ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                Dim ev As New creacionpdf_ins()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
            End If
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            Dim tablacv As New PdfPTable(2)
            tablacv.SetWidths(New Single() {160.0F, 40.0F})
            Dim codigoblan As New PdfPCell(New Phrase(" "))
            codigoblan.Border = 0
            codigoblan.HorizontalAlignment = Element.ALIGN_RIGHT
            tablacv.AddCell(codigoblan)
            Dim codigoveh As New PdfPCell(New Phrase("C.V. " & Me.txtCodVehiculo.Text, fontcv))
            codigoveh.Border = 0
            codigoveh.BackgroundColor = New BaseColor(230, 230, 230)
            codigoveh.HorizontalAlignment = Element.ALIGN_RIGHT
            tablacv.AddCell(codigoveh)
            documento.Add(tablacv)
            documento.Add(lineablanco)
            If cbm_tipo.SelectedValue.ToString = "C" Then
                'Dim nombre As New iTextSharp.text.Paragraph(espacio2 & "Certifica que el Sr.(a). " & Me.TxtConCliente.Text, fontcliente)
                'Dim nombre As New iTextSharp.text.Paragraph(espacio2 & "Certificamos que el Sr./Sra. " & Me.TxtConCliente.Text, fontcliente)
                'documento.Add(nombre)
                'documento.Add(lineablanco)
                'Dim linea1 As New iTextSharp.text.Paragraph(espacio & "Ha adquirido los siguientes sistemas:", fontNormal)
                'documento.Add(linea1)
                'documento.Add(lineablanco)


                Dim textoParrafo As New PdfPTable(1)
                textoParrafo.SetWidths(New Single() {200.0F})
                Dim tituloText1 As New PdfPCell(New Phrase("Certificamos que el Sr./Sra. " & Me.TxtConCliente.Text, fontcliente))
                tituloText1.Border = 0
                textoParrafo.AddCell(tituloText1)
                textoParrafo.AddCell(codigoblan)
                textoParrafo.AddCell(codigoblan)
                'textoParrafo.AddCell(lineablanco)
                Dim tituloText2 As New PdfPCell(New Phrase("Ha contratado el producto:", fontNormal))
                tituloText2.Border = 0
                textoParrafo.AddCell(tituloText2)
                textoParrafo.AddCell(codigoblan)
                'textoParrafo.AddCell(codigoblan)
                documento.Add(textoParrafo)

                ' Impresion de los productos , se genera tabla
                ' estaba fontbold
                Dim tabla As New PdfPTable(5)
                tabla.SetWidths(New Single() {5.0F, 40.0F, 25.0F, 15.0F, 5.0F})
                Dim titulo4 As New PdfPCell(New Phrase(" ", fontnegro))
                titulo4.Border = 0
                'titulo4.BackgroundColor = New BaseColor(230, 230, 230)
                titulo4.HorizontalAlignment = Element.ALIGN_RIGHT
                tabla.AddCell(titulo4)
                Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontnegro))
                titulo1.Border = 0
                'titulo1.Border = 2
                titulo1.BackgroundColor = New BaseColor(230, 230, 230)
                'titulo1.BackgroundColor = Font(230, 230, 230)
                'titulo1.BorderColor = New BaseColor(230, 230, 230)
                titulo1.HorizontalAlignment = Element.ALIGN_CENTER
                tabla.AddCell(titulo1)
                Dim titulo2 As New PdfPCell(New Phrase("VIGENCIA DEL SERVICIO*", fontnegro))
                titulo2.Border = 0
                titulo2.BackgroundColor = New BaseColor(230, 230, 230)
                titulo2.HorizontalAlignment = Element.ALIGN_LEFT
                tabla.AddCell(titulo2)
                'Dim titulo3 As New PdfPCell(New Phrase("P.V.P INCLUIDO IVA", fontnegro))
                Dim titulo3 As New PdfPCell(New Phrase(" ", fontnegro))
                titulo3.Border = 0
                'titulo3.BackgroundColor = New BaseColor(230, 230, 230)
                titulo3.HorizontalAlignment = Element.ALIGN_RIGHT
                tabla.AddCell(titulo3)
                tabla.AddCell(titulo4)
                If dtconsulta2.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                        tabla.AddCell(titulo4)
                        Dim det1 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("PRODUCTO").ToString(), fontNormal))
                        det1.Border = 0
                        det1.HorizontalAlignment = Element.ALIGN_LEFT
                        tabla.AddCell(det1)
                        Dim fechafinal As String = ""
                        Dim valorfecha As String
                        valorfecha = dtconsulta2.Tables(0).Rows(i)("COBERTURA").ToString()
                        If Mid(valorfecha, 4, 2) = "01" Then fechafinal = Mid(valorfecha, 1, 3) & "Ene/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "02" Then fechafinal = Mid(valorfecha, 1, 3) & "Feb/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "03" Then fechafinal = Mid(valorfecha, 1, 3) & "Mar/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "04" Then fechafinal = Mid(valorfecha, 1, 3) & "Abr/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "05" Then fechafinal = Mid(valorfecha, 1, 3) & "May/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "06" Then fechafinal = Mid(valorfecha, 1, 3) & "Jun/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "07" Then fechafinal = Mid(valorfecha, 1, 3) & "Jul/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "08" Then fechafinal = Mid(valorfecha, 1, 3) & "Ago/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "09" Then fechafinal = Mid(valorfecha, 1, 3) & "Sep/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "10" Then fechafinal = Mid(valorfecha, 1, 3) & "Oct/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "11" Then fechafinal = Mid(valorfecha, 1, 3) & "Nov/" & Mid(valorfecha, 7, 4)
                        If Mid(valorfecha, 4, 2) = "12" Then fechafinal = Mid(valorfecha, 1, 3) & "Dic/" & Mid(valorfecha, 7, 4)
                        Dim det2 As New PdfPCell(New Phrase(fechafinal, fontNormal))
                        det2.Border = 0
                        det2.HorizontalAlignment = Element.ALIGN_LEFT
                        tabla.AddCell(det2)
                        'Dim det3 As New PdfPCell(New Phrase("$" & dtconsulta2.Tables(0).Rows(i)("PRECIO_PRODUCTO").ToString(), fontNormal))
                        Dim det3 As New PdfPCell(New Phrase(" ", fontNormal))
                        det3.Border = 0
                        det3.HorizontalAlignment = Element.ALIGN_RIGHT
                        tabla.AddCell(det3)
                        Dim det4 As New PdfPCell(New Phrase(" ", fontNormal))
                        det4.Border = 0
                        det4.HorizontalAlignment = Element.ALIGN_RIGHT
                        tabla.AddCell(det4)
                    Next
                End If
                documento.Add(tabla)
                documento.Add(lineablanco)
            End If
            documento.Add(lineablanco)
            Dim texto2 As String = ""
            Dim texto3 As String = ""
            If cbm_tipo.SelectedValue.ToString = "C" Then
                texto2 = "*La vigencia del servicio se contará desde la instalación del equipo. "
                texto3 = "Con Orden de Instalación en el vehículo:"
            ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                texto2 = "Se certifica que se ha realizado la desinstalación de nuestros productos del (vehículo o embarcación) con las siguientes características:"
            ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                texto2 = "Se certifica que se ha realizado la instalación de nuestros productos en el (vehículo o embarcación) con las siguientes características:"
            End If
            'Dim linea2 As New iTextSharp.text.Paragraph(espacio & texto2, fontNormal)
            'documento.Add(linea2)

            Dim textoParrafo2 As New PdfPTable(1)
            textoParrafo2.SetWidths(New Single() {200.0F})
            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim tituloText41 As New PdfPCell(New Phrase(texto2, fontNormal))
                tituloText41.Border = 0
                tituloText41.HorizontalAlignment = Element.ALIGN_LEFT
                textoParrafo2.AddCell(tituloText41)
            End If
            textoParrafo2.AddCell(codigoblan)
            Dim tituloText4 As New PdfPCell(New Phrase(texto3, fontNormal))
            tituloText4.Border = 0
            textoParrafo2.AddCell(tituloText4)
            documento.Add(textoParrafo2)
            documento.Add(lineablanco)
            Dim tabladatos As New PdfPTable(4)
            tabladatos.SetWidths(New Single() {20.0F, 105.0F, 20.0F, 65.0F})
            ' fontnegro  para titulo,  fontgris para dato
            Dim vehic7 As New PdfPCell(New Phrase("PLACA", fontnegro))
            vehic7.Border = 0
            vehic7.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic7)
            Dim vehic8 As New PdfPCell(New Phrase(Me.txtConPlaca.Text, fontgris))
            vehic8.Border = 0
            vehic8.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic8)
            Dim vehic13 As New PdfPCell(New Phrase("COLOR", fontnegro))
            vehic13.Border = 0
            vehic13.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic13)
            Dim vehic14 As New PdfPCell(New Phrase(Me.txtConColor.Text, fontgris))
            vehic14.Border = 0
            vehic14.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic14)
            Dim vehic1 As New PdfPCell(New Phrase("MARCA", fontnegro))
            vehic1.Border = 0
            vehic1.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic1)
            Dim vehic2 As New PdfPCell(New Phrase(Me.txtConMarca.Text, fontgris))
            vehic2.Border = 0
            vehic2.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic2)
            Dim vehic3 As New PdfPCell(New Phrase("AÑO", fontnegro))
            vehic3.Border = 0
            vehic3.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic3)
            Dim vehic4 As New PdfPCell(New Phrase(Me.Txtconanio.Text, fontgris))
            vehic4.Border = 0
            'estaba fontnormal
            vehic4.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic4)
            Dim vehic5 As New PdfPCell(New Phrase("MODELO", fontnegro))
            vehic5.Border = 0
            vehic5.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic5)
            Dim vehic6 As New PdfPCell(New Phrase(Me.txtConModelo.Text, fontgris))
            vehic6.Border = 0
            vehic6.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic6)
            Dim vehic15 As New PdfPCell(New Phrase("MOTOR", fontnegro))
            vehic15.Border = 0
            vehic15.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic15)
            Dim vehic16 As New PdfPCell(New Phrase(Me.TxtMotor.Text, fontgris))
            vehic16.Border = 0
            vehic16.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic16)
            Dim vehic9 As New PdfPCell(New Phrase("TIPO", fontnegro))
            vehic9.Border = 0
            vehic9.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic9)
            Dim vehic10 As New PdfPCell(New Phrase(Me.txtConTipo.Text, fontgris))
            vehic10.Border = 0
            vehic10.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic10)
            Dim vehic11 As New PdfPCell(New Phrase("CHASIS", fontnegro))
            vehic11.Border = 0
            vehic11.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic11)
            Dim vehic12 As New PdfPCell(New Phrase(Me.txtChasis.Text, fontgris))
            vehic12.Border = 0
            vehic12.HorizontalAlignment = Element.ALIGN_LEFT
            tabladatos.AddCell(vehic12)
            documento.Add(tabladatos)
            documento.Add(lineablanco)

            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim textoParrafo21 As New PdfPTable(1)
                textoParrafo21.SetWidths(New Single() {240.0F})
                Dim tituloText42 As New PdfPCell(New Phrase("El presente certificado, no constituye confirmación de instalación del equipo.", fontNormal))
                tituloText42.Border = 0
                tituloText42.HorizontalAlignment = Element.ALIGN_CENTER
                textoParrafo21.AddCell(tituloText42)
                documento.Add(textoParrafo21)
            End If
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            ' Imprime el codigo de barra
            Dim codbarra As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(Session("nombrebarra")))
            codbarra.ScalePercent(75.0F)
            documento.Add(codbarra)
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim textoParrafo22 As New PdfPTable(1)
                textoParrafo22.SetWidths(New Single() {240.0F})
                Dim tituloText43 As New PdfPCell(New Phrase("Los términos y condiciones del servicio pueden ser consultadas en nuestra página web", fontNormal))
                tituloText43.Border = 0
                tituloText43.HorizontalAlignment = Element.ALIGN_LEFT
                textoParrafo22.AddCell(tituloText43)
                documento.Add(textoParrafo22)
                'Else
            End If
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            ' Fecha de emisión del pdf
            Dim lblfechahoraemision As String
            lblfechahoraemision = "Info Emisión Docto : " & Session("user_session") & " | " & System.DateTime.Now.ToString("dd MMMM yyyy") & " | " & System.DateTime.Now.ToString("H:mm:ss")
            Dim fechatrabajo As String = ""
            Dim valortrabajo As String = dtconsulta2.Tables(0).Rows(0)("FECHA_TRABAJO").ToString()
            If Mid(valortrabajo, 4, 2) = "01" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Enero/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "02" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Febrero/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "03" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Marzo/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "04" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Abril/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "05" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Mayo/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "06" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Junio/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "07" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Julio/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "08" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Agosto/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "09" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Septiembre/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "10" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Octubre/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "11" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Noviembre/" & Mid(valortrabajo, 7, 4)
            If Mid(valortrabajo, 4, 2) = "12" Then fechatrabajo = Mid(valortrabajo, 1, 3) & "Diciembre/" & Mid(valortrabajo, 7, 4)
            Dim tablacv2 As New PdfPTable(2)
            tablacv2.SetWidths(New Single() {80.0F, 120.0F})
            If cbm_tipo.SelectedValue.ToString = "I" Then
                Dim fecha As New PdfPCell(New Phrase("Fecha Instalación: " & fechatrabajo, fontNormal))
                fecha.Border = 0
                fecha.HorizontalAlignment = Element.ALIGN_LEFT
                tablacv2.AddCell(fecha)
                tablacv2.AddCell(codigoblan)
            ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                Dim fecha As New PdfPCell(New Phrase("Fecha Desinstalación: " & fechatrabajo, fontNormal))
                fecha.Border = 0
                fecha.HorizontalAlignment = Element.ALIGN_LEFT
                tablacv2.AddCell(fecha)
                tablacv2.AddCell(codigoblan)
            End If
            tablacv2.AddCell(codigoblan)
            Dim codigofecha As New PdfPCell(New Phrase(lblfechahoraemision, fontNormal))
            codigofecha.Border = 0
            codigofecha.HorizontalAlignment = Element.ALIGN_RIGHT
            tablacv2.AddCell(codigofecha)
            documento.Add(tablacv2)
            documento.Add(lineablanco)
            documento.Add(lineablanco)
            ' Imprime logo del fin de pagina 
            Dim logo As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath("../Images/background_footer_certificado.jpg"))
            logo.ScalePercent(75.0F)
            documento.Add(logo)
            documento.Close()
            documento.Dispose()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


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
            Dim obj As New ConsultaWeb
            Dim dtconsulta As New System.Data.DataSet
            dtconsulta = obj.ConsultarImprimir(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text, Session("user_id"), cbm_tipo.SelectedValue.ToString)
            Session("infoqr") = dtconsulta
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



End Class