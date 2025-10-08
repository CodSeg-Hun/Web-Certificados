Imports System.Net.Mail
Imports System.IO
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MessagingToolkit.Barcode
Imports System.Net
Imports System.Security.Cryptography
Imports DevExpress.XtraReports.Parameters
Imports Org.BouncyCastle.Asn1.Crmf
Imports System.Web.Services
Imports RestSharp
Imports ParameterType = RestSharp.ParameterType
Imports DevExpress.XtraPrinting.Native.WebClientUIControl
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports System.Reflection
Imports DevExpress.CodeParser
Imports Method = RestSharp.Method
Imports Telerik.Web.UI
Imports System.Data.Common
Imports DevExpress.XtraReports.Native.CodeCompletion
Imports System.Web.Services.Description
Imports Telerik.Charting
Imports System.Drawing.Imaging
Imports Telerik.Web

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
    Dim bien As Bien


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
                RadGridVehiculo.DataBind()
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
            Dim usuarioId As String = Session("user_id") ' 3216
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
                    Me.BtnNuevo.Enabled = False
                    Me.BtnConsultar.Enabled = True
                    Me.BtnImprimir.Enabled = False
                    Me.BtnCorreo.Enabled = False
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
                    Me.BtnNuevo.Enabled = True
                    Me.BtnConsultar.Enabled = False
                    Me.BtnImprimir.Enabled = False
                    Me.BtnCorreo.Enabled = False
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

    Private Sub MessageController(parametros As List(Of String))
        ' Crear un DataTable con las columnas que coincidan con las del RadGrid
        Dim dtx As New DataTable()
        dtx.Columns.Add("CODIGO_ID", GetType(String))
        dtx.Columns.Add("MENSAJE", GetType(String))

        ' Variable para el contador de códigos
        Dim codigo As Integer = 1

        ' Iterar sobre la lista de parámetros y agregar filas al DataTable
        For Each parametro As String In parametros
            dtx.Rows.Add(codigo.ToString("D3"), parametro)
            codigo += 1
        Next

        ' Asignar el DataTable al RadGrid
        rgdResultado.DataSource = dtx
        rgdResultado.DataBind()

        ' Configuración de rntResultado
        rntResultado.Title = "Mensaje de la Aplicación Consulta Certificados"
        rntResultado.TitleIcon = "info"
        rntResultado.ContentIcon = "info"
        rntResultado.ShowSound = "info"
        rntResultado.Width = 380
        rntResultado.Height = 250
        rntResultado.Show()
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

    Private Function ConsutlarRestlet()
        'MessageController()
        Dim obj As New ConsultaWeb
        Dim consultar As Boolean
        Dim resultadoRESTlet As New List(Of String)
        Dim rucCanales As String = Session("ruccanales")
        'Dim script = "4042" ' Sandbox
        Dim script = "4846" ' Producción
        resultadoRESTlet = RequestApi.Fetch("1", Me.txtChasis.Text, Me.TxtMotor.Text, Me.txtCodVehiculo.Text, Me.txtConPlaca.Text, "", rucCanales, Method.POST, script)
        Dim mensajes As New List(Of String)()

        Dim vehiculo As String
        Dim detalle As String
        Dim bien As Bien
        Dim producto As Producto
        Dim dataset As New DataSet()
        Dim dataTable As New DataTable()
        Dim flagBien As Boolean
        Dim flagProducto As Boolean
        consultar = False

        If Session("errores") IsNot Nothing Then
            Dim errores As List(Of SMSError) = JsonConvert.DeserializeObject(Of List(Of SMSError))(Session("errores").ToString)

            For Each errorItem As SMSError In errores
                If errorItem.CodigoError = "003" Then
                    ' Vehículo no registrado en el sistema. No existen datos disponibles. Por favor, verifique la información.
                    Me.Mensaje(errorItem.Mensaje, Operacion.OInvalida)
                    LimpiarControl()
                    Return -1
                ElseIf errorItem.CodigoError = "007" Then
                    Me.Mensaje(errorItem.Mensaje, Operacion.OInvalida)
                    LimpiarControl()
                    Return -1
                ElseIf errorItem.CodigoError = "017" Then
                    Me.Mensaje(errorItem.Mensaje, Operacion.OInvalida)
                    LimpiarControl()
                    Return -1
                Else
                    mensajes.Add(errorItem.Mensaje)
                End If

            Next

        Else
            Botones("Consultar")
            Dim producto_conteo = 0
            Dim familias_codigo_producto As New List(Of String)
            Dim codigos_estado_producto As New List(Of String)
            For i As Integer = 0 To resultadoRESTlet.Count - 1
                If i = 0 Then
                    ' -*-*-*-*-*-*-*-*- BIENE *-*-*-*-*-*-*-*-*-
                    vehiculo = resultadoRESTlet(i)
                    Dim bienObject As JObject = JObject.Parse(vehiculo)
                    Dim codigo_estado_bien As String = bienObject("codigo_estado_bien").ToString() ' 11 = VIGENTE O ACTIVO
                    bien = JsonConvert.DeserializeObject(Of Bien)(vehiculo)
                    Dim datasetVehiculo As DataSet = ConvertToDataSet(bien)
                    Session("Detalle") = datasetVehiculo
                    Session("codigo_estado_bien") = codigo_estado_bien
                    flagBien = True
                Else
                    Botones("Consultar")
                    Me.cbm_tipo.Enabled = True
                    detalle = resultadoRESTlet(i)
                    Dim productoObject As JObject = JObject.Parse(detalle)
                    Dim estado_codigo As String = productoObject("codigo_estado_internal").ToString()
                    Session("codigo_estado_producto") = estado_codigo
                    Dim familia_producto_codigo As String = productoObject("familia").ToString()
                    Session("familia_codigo_producto") = familia_producto_codigo

                    producto = JsonConvert.DeserializeObject(Of Producto)(detalle)
                    If (producto_conteo = 0) Then
                        dataset = ConvertToDataSet(producto)
                        Session("producto") = dataset
                    Else
                        Dim dataSetTemporal = ConvertToDataSet(producto)
                        dataset.Merge(dataSetTemporal)
                        familias_codigo_producto.Add(familia_producto_codigo)
                        codigos_estado_producto.Add(estado_codigo)
                    End If
                    flagProducto = True
                    producto_conteo += 1

                End If
                Session("dataset") = dataset
            Next

        End If

        Try

            If flagBien Then
                'Session("Detalle") = Nothing
                Session("dtResultado") = Nothing
                Dim dtConsulta As New System.Data.DataSet
                'dtConsulta = CType(Session("Consulta_Datos"), DataSet)
                Me.txtCodVehiculo.Text = bien.IdVehiculo
                Me.txtChasis.Text = bien.Chasis
                Me.TxtMotor.Text = bien.Motor
                Me.txtConConcesionario.Text = bien.Concesionario
                Me.txtConFinanciera.Text = bien.Financiera
                Me.TxtConCliente.Text = bien.Cliente
                Me.txtConCodCliente.Text = bien.IdCliente
                Me.txtConMarca.Text = bien.Marca
                Me.txtConModelo.Text = bien.Modelo
                Me.txtConPlaca.Text = bien.Placa
                Me.txtConTipo.Text = bien.Tipo
                Me.txtConColor.Text = bien.Color
                Me.Txtconanio.Text = bien.Anio
                txtanio = bien.Anio
                Me.txtCodConvenio.Text = bien.CodigoConvenio
            End If

            If flagProducto = True Then
                RadGridVehiculo.DataSource = Session("dataset")
                RadGridVehiculo.Height = 150
                RadGridVehiculo.DataBind()
                InhabilitaCertificado(True)
                Me.BtnImprimir.Enabled = True
                consultar = True
                Session("datocliente") = Me.TxtConCliente.Text + " | " + Me.txtConMarca.Text + " | " + Me.txtConModelo.Text + " | " + Me.txtChasis.Text + " | " + Me.txtConPlaca.Text
                Me.cbm_tipo.Enabled = True
            End If


            If mensajes.Count > 0 Then
                MessageController(mensajes)
                InhabilitaCertificado(False)
                consultar = False
            End If

            obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "N", ipmaquina, pantalla, " ", False, consultar, " Consulto: ")

        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function

#Region "botones"
    Private Sub BtnConsultar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConsultar.Click
        Try
            If Validar() Then
                Botones("Inicial")
                'If cbm_tipo.SelectedValue.ToString = "N" Then
                '    Me.Mensaje("Seleccione el Tipo de Reporte a Consultar", Operacion.OInvalida)
                'Consultar()
                Session("errores") = Nothing
                ConsutlarRestlet()

            Else
                Me.Mensaje("Ingrese datos para poder consultar", Operacion.OInvalida)
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub BtnNuevo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnNuevo.Click
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
            Dim isActive As Boolean = False
            'Dim cmbOpcion = cbm_tipo.SelectedValue.ToString()
            Dim selectedOption As String = Me.cbm_tipo.Text
            'Dim cmbOpcion As String = Me.cbm_tipo.SelectedValue
            Dim codigo_estado_producto = Session("codigo_estado_producto").ToString()

            Dim cmbOpcion As String

            Select Case Me.cbm_tipo.SelectedValue.ToString()
                Case "N"
                    cmbOpcion = "0" ' Ninguno
                Case "D"
                    cmbOpcion = "2" ' Desinstalación
                Case "C"
                    cmbOpcion = "8" ' Venta
                Case "I"
                    cmbOpcion = "1" ' Instalación
                Case Else
                    cmbOpcion = "0" ' Valor por defecto opcional
            End Select

            If cmbOpcion = "0" Then
                Me.Mensaje("Seleccione el Tipo de Reporte a Consultar", Operacion.OInvalida)
                Return
            Else
                Select Case cmbOpcion
                    Case "8"
                        '                                                                  estado de la Orden de servicio 
                        If codigo_estado_producto = "1" Or codigo_estado_producto = "8" Or codigo_estado_producto = "7" Then
                            ' Emite certificado de Venta

                        Else
                            Me.Mensaje("Producto no cumple las condiciones para emitir certificado de Venta", Operacion.OInvalida)
                            Return
                        End If

                    Case "1"
                        If codigo_estado_producto = "1" Or codigo_estado_producto = "8" Then
                            ' Emite certificado de Instalación

                        Else
                            Me.Mensaje("Producto no cumple las condiciones para emitir certificado de Instalación", Operacion.OInvalida)
                            Return
                        End If

                    Case "2"
                        If codigo_estado_producto = "2" Then
                            ' Producto desinstalado, puede emitir certificado de Desinstalación

                        Else
                            Me.Mensaje("Producto no cumple las condiciones para emitir certificado de Desinstalación", Operacion.OInvalida)
                            Return
                        End If

                    Case Else
                        Me.Mensaje("Opción no válida", Operacion.OInvalida)
                        Return
                End Select
            End If

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
            ' procesos 8
            'dtTransaccion = obj.Consulta_Transaccion(Me.txtCodVehiculo.Text, usuarioOficina, cbm_tipo.SelectedValue.ToString)
            Dim producto_familia_nombre = Session("familia_codigo_producto").ToString()
            Session("txtCodConvenio") = Me.txtCodConvenio.Text

            If producto_familia_nombre IsNot Nothing Then
                If Me.txtCodConvenio.Text = "024" Or producto_familia_nombre = "LH" Then
                    lader = True ' ✅
                End If
                If producto_familia_nombre = "RB" Or producto_familia_nombre = "VC" Then
                    conecel = True ' ✅
                End If
                If producto_familia_nombre = "MH" Then
                    mareauto = True ' ✅
                End If
                If producto_familia_nombre = "AB" Then
                    ambacar = True ' ✅
                End If
                If producto_familia_nombre = "LA" Then
                    latam = True
                End If
                If producto_familia_nombre = "AT" Then
                    coris = True ' NO ENCONTRADO
                End If
                If producto_familia_nombre = "HT" Then
                    casabaca = True
                End If
                If producto_familia_nombre = "CM" Then
                    coneca = True
                End If
                If producto_familia_nombre = "IG" Then
                    induato = True
                End If
            End If

            'Session("cbm_tipo") = cbm_tipo.SelectedValue.ToString
            Session("cbm_tipo") = cmbOpcion
            'Redirect("ConsultaWebAmbacar.aspx", "_blank", "menubar=0,width=850,height=850")

            'Session("cbm_tipo") = cbm_tipo.SelectedValue.ToString
            'obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, " ", latam, True, " Impreso: ")
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
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

    Protected Sub BtnCorreo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCorreo.Click
        Try
            Dim codigo_estado_producto = Session("codigo_estado_producto").ToString()

            Dim cmbOpcion As String

            Select Case Me.cbm_tipo.SelectedValue.ToString()
                Case "N"
                    cmbOpcion = "0" ' Ninguno
                Case "D"
                    cmbOpcion = "2" ' Desinstalación
                Case "C"
                    cmbOpcion = "8" ' Venta
                Case "I"
                    cmbOpcion = "1" ' Instalación
                Case Else
                    cmbOpcion = "0" ' Valor por defecto opcional
            End Select

            If cmbOpcion = "0" Then
                Me.Mensaje("Seleccione el Tipo de Reporte a Consultar", Operacion.OInvalida)
                Return
            Else
                Select Case cmbOpcion
                    Case "8"
                        If codigo_estado_producto = "1" Or codigo_estado_producto = "8" Or codigo_estado_producto = "7" Then
                            ' Emite certificado de Venta

                        Else
                            Me.Mensaje("Producto no cumple las condiciones para emitir certificado de Venta", Operacion.OInvalida)
                            Return
                        End If

                    Case "1"
                        If codigo_estado_producto = "1" Or codigo_estado_producto = "8" Then
                            ' Emite certificado de Instalación

                        Else
                            Me.Mensaje("Producto no cumple las condiciones para emitir certificado de Instalación", Operacion.OInvalida)
                            Return
                        End If

                    Case "2"
                        If codigo_estado_producto = "2" Then
                            ' Producto desinstalado, puede emitir certificado de Desinstalación

                        Else
                            Me.Mensaje("Producto no cumple las condiciones para emitir certificado de Desinstalación", Operacion.OInvalida)
                            Return
                        End If

                    Case Else
                        Me.Mensaje("Opción no válida", Operacion.OInvalida)
                        Return
                End Select
            End If
            'Dim obj As New ConsultaWeb
            If cbm_tipo.SelectedValue.ToString = "N" Then
                Me.Mensaje("Seleccione el Tipo de Reporte a Consultar", Operacion.OInvalida)
            Else
                'Dim dtResultado As New System.Data.DataSet
                'dtResultado = obj.Consulta_Mensaje(Me.txtCodVehiculo.Text, usuarioOficina, txtConPlaca.Text, TxtMotor.Text, txtChasis.Text, cbm_tipo.SelectedValue.ToString)
                'Session("dtResultado") = dtResultado
                'If dtResultado.Tables(0).Rows.Count > 0 Then
                '    rgdResultado.DataSource = Session("dtResultado")
                '    rgdResultado.DataBind()
                '    rntResultado.Title = "Mensaje de la Aplicación Consulta Certificados"
                '    rntResultado.TitleIcon = "info"
                '    rntResultado.ContentIcon = "info"
                '    rntResultado.ShowSound = "info"
                '    rntResultado.Width = 380
                '    rntResultado.Height = 250
                '    rntResultado.Show()
                '    'accionHabilita = 0
                'Else
                Me.cbm_tipo.Enabled = False
                TxtCorreo.Visible = True
                BtnCancela.Visible = True
                BtnEnvia.Visible = True
                titcorreo.Visible = True
                'BtnConsultar.Visible = False
                'BtnNuevo.Visible = False
                'End If
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
                'cbm_tipo.DataSource = Nothing
                'cbm_tipo.Items.Clear()
                'cbm_tipo.DataBind()

                'cbm_tipo.DataSource = Nothing
                'cbm_tipo.Items.Clear()

                'cbm_tipo.Items.Add(New RadComboBoxItem("Ninguno", "0"))                        ' 
                'cbm_tipo.Items.Add(New RadComboBoxItem("Certificado de Desinstalación", "2"))  ' 002 - DESINSTALADO
                'cbm_tipo.Items.Add(New RadComboBoxItem("Certificado de Venta", "8"))           ' 008 - ENTREGADO A CLIENTE
                'cbm_tipo.Items.Add(New RadComboBoxItem("Certificado de Instalación", "1"))     ' 001 - INSTALADO
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
            Me.BtnImprimir.Enabled = accion
            Me.BtnCorreo.Enabled = accion
            Me.cbm_tipo.Enabled = accion
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
            If dtConsulta.Tables(0).Rows.Count > 0 Then ' 1
                ''Inhabilito el botón de impresión de certificados si este campo viene con 0
                If cbm_tipo.SelectedValue.ToString = "N" Then
                    accionHabilita = dtConsulta.Tables(0).Rows(0)("CAMB_PROP_ESTADO").ToString() ' 1
                    varRei = dtConsulta.Tables(0).Rows(0)("ESTADO_REI").ToString() ' 0
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
    '            Botones("Editar")
    '            codigo = item("ID_VEHICULO").Text.Trim.ToString
    '            If codigo > 0 Then
    '                'RadTabParametro.Tabs.Item(1).Enabled = True
    '                'RadTabParametro.Tabs.Item(0).Enabled = False
    '                'RadTabParametro.SelectedIndex = 1
    '                Botones("Consultar")
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
    '                    Me.BtnImprimir.Enabled = True
    '                Else
    '                    RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
    '                    Me.BtnImprimir.Enabled = False
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
        ' url: "ConsultaWebAmbacar.aspx" , target: "_blank", windowFeatures: "menubar=0,width=850,height=850"
        Try
            Dim context As HttpContext = HttpContext.Current
            If ([String].IsNullOrEmpty(target) OrElse target.Equals("_self", StringComparison.OrdinalIgnoreCase)) AndAlso [String].IsNullOrEmpty(windowFeatures) Then
                context.Response.Redirect(url)
            Else
                Dim page As System.Web.UI.Page = DirectCast(context.Handler, System.Web.UI.Page)
                If page Is Nothing Then
                    Throw New InvalidOperationException("Cannot redirect to new window outside Page context.")
                End If
                url = page.ResolveClientUrl(url) ' "ConsultaWebAmbacar.aspx"
                Dim script As String
                If Not [String].IsNullOrEmpty(windowFeatures) Then
                    script = "window.open(""{0}"", ""{1}"", ""{2}"");" ' "window.open(""{0}"", ""{1}"", ""{2}"");"
                Else
                    script = "window.open(""{0}"", ""{1}"");"
                End If
                script = [String].Format(script, url, target, windowFeatures) ' "window.open(""{0}"", ""{1}"", ""{2}"");" | window.open("{0}", "{1}", "{2}");
                ScriptManager.RegisterStartupScript(page, GetType(Page), "Redirect", script, True)
                ' {ASP.forms_consultadatos_aspx}, type' window.open("ConsultaWebAmbacar.aspx", "_blank", "menubar=0,width=850,height=850");
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub Btnenviar(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnvia.Click
        Try
            If Me.TxtCorreo.Text = "" Then
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
                ' procesos 8

                Dim producto_familia_nombre = Session("familia_codigo_producto").ToString()
                Session("txtCodConvenio") = Me.txtCodConvenio.Text
                If producto_familia_nombre IsNot Nothing Then
                    If Me.txtCodConvenio.Text = "024" Or producto_familia_nombre = "LH" Then
                        lader = True
                    End If
                    If producto_familia_nombre = "RB" Or producto_familia_nombre = "VC" Then
                        conecel = True
                    End If
                    If producto_familia_nombre = "MH" Then
                        mareauto = True
                    End If
                    If producto_familia_nombre = "LA" Then
                        latam = True
                    End If
                    If producto_familia_nombre = "AT" Then
                        coris = True
                    End If
                    If producto_familia_nombre = "HT" Then
                        casabaca = True
                    End If
                    If producto_familia_nombre = "CM" Then
                        coneca = True
                    End If
                    'If producto_familia_nombre = "CM" And dtTransaccion.Tables(0).Rows(i)("FINANCIADOR").ToString() = "ONE" Then
                    '    coneca = True
                    'End If
                    'If dtTransaccion.Tables(0).Rows(i)("FINANCIADOR").ToString() = "ONE" Then
                    '    coneca = True
                    'End If
                    'If dtTransaccion.Tables(0).Rows(i)("FINANCIADOR").ToString() = "ORI" Then
                    '    financiera = True
                    'End If
                    If producto_familia_nombre = "CH" Then
                        financiera = True
                    End If
                    If producto_familia_nombre = "IG" Then
                        induato = True
                    End If
                    If producto_familia_nombre = "AB" Then
                        ambacar = True
                    End If

                End If
                Dim orden As String = ""
                If Not latam Then
                    ' Se llama procedimiento que genera el archivo PDF
                    CargaImagenQR()
                    If casabaca Then
                        GeneraPdf_Click("NOR")
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
                    'dtTransaccion = obj.Consulta_Orden(Me.txtCodVehiculo.Text, usuarioOficina, Me.txtConCodCliente.Text, "9")
                    'orden = dtTransaccion.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
                    orden = DateTime.Now.ToString("yyyyMMdd")

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
                'Dim mailAddress As New MailAddress("noticarseg@carsegsa.com")
                Dim mailAddress As New MailAddress(ConfigurationManager.AppSettings.Get("VentasMailUser").ToString())
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
                'Dim smtpClient As New SmtpClient("10.100.89.34")
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
                'nuevo
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                Dim smtp As New SmtpClient(ConfigurationManager.AppSettings.Get("SmptCliente").ToString(), ConfigurationManager.AppSettings.Get("SmptPort"))
                smtp.EnableSsl = True
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                smtp.UseDefaultCredentials = False
                smtp.Credentials = New Net.NetworkCredential(ConfigurationManager.AppSettings.Get("VentasMailUser").ToString(), ConfigurationManager.AppSettings.Get("VentasMailPassword").ToString())
                smtp.Send(mailMessage)
                'ENVIA PDF
                'smtpClient.Send(mailMessage)
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
                cbm_tipo.Enabled = True
                'obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, Me.TxtCorreo.Text, latam, True, " Email: ")
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
            Me.cbm_tipo.Enabled = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub GeneraPdf_Click(origen As String)
        Try
            Dim fontNormal As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.NORMAL)
            Dim fontcliente As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
            Dim fontcv As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 11, iTextSharp.text.Font.BOLD)
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
            Dim file As String = "\\10.100.107.14\ImagenesDocumentos\cert_" & Me.txtCodVehiculo.Text & ".pdf"
            Session("nombrepdf") = file
            Dim writer As PdfWriter = PdfWriter.GetInstance(documento, New FileStream(file, FileMode.Create))
            Dim lineablanco As New iTextSharp.text.Paragraph(" ")
            Dim colorBorde As New BaseColor(203, 209, 212)
            Dim fontProducto As iTextSharp.text.Font = FontFactory.GetFont("Calibri", 9, iTextSharp.text.Font.BOLD)

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

            If origen = "COR" Or origen = "LAD" Or origen = "CNL" Or origen = "AMB" Or origen = "ONE" Then
                Dim nombtitulo As String = ""
                If cbm_tipo.SelectedValue.ToString = "C" Then
                    nombtitulo = " CERTIFICADO DE VENTA  "
                ElseIf cbm_tipo.SelectedValue.ToString = "D" Then
                    nombtitulo = " CERTIFICADO DE DESINTALACIÓN "
                ElseIf cbm_tipo.SelectedValue.ToString = "I" Then
                    nombtitulo = " CERTIFICADO DE INSTALACIÓN "
                End If

                Dim titulo As New PdfPCell(New Phrase(nombtitulo, fontTitulo)) With {
                    .Border = 0,
                    .Colspan = 2,
                    .BackgroundColor = New BaseColor(230, 230, 230),
                    .HorizontalAlignment = Element.ALIGN_CENTER
                }
                tablacv.AddCell(titulo)
                Dim codigoblan2 As New PdfPCell(New Phrase(" "))
                codigoblan2.Border = 0
                codigoblan2.HorizontalAlignment = Element.ALIGN_RIGHT
                tablacv.AddCell(codigoblan2)
                tablacv.AddCell(codigoblan2)
            End If

            Dim codigoblan As New PdfPCell(New Phrase(" ")) With {
                .Border = 0,
                .HorizontalAlignment = Element.ALIGN_RIGHT
            }
            tablacv.AddCell(codigoblan)
            Dim codigoveh As New PdfPCell(New Phrase("C.V. " & Me.txtCodVehiculo.Text, fontcv)) With {
                .BackgroundColor = BaseColor.WHITE,
                .BorderColor = colorBorde,
                .Padding = 5,
                .HorizontalAlignment = Element.ALIGN_CENTER
            }
            tablacv.AddCell(codigoveh)
            documento.Add(tablacv)
            documento.Add(lineablanco)

            ' CERTIFICADO DE VENTA
            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim textoParrafo As New PdfPTable(1)
                textoParrafo.SetWidths(New Single() {200.0F})

                Dim phrase As New Phrase From {
                    New Chunk("Certificamos que el Sr./Sra./Empresa ", fontNormal), ' Texto normal
                    New Chunk(Me.TxtConCliente.Text.ToUpper(), fontcliente) ' Nombre en negrita
                }
                Dim tituloText1 As New PdfPCell(phrase)
                tituloText1.Border = 0

                textoParrafo.AddCell(tituloText1)
                textoParrafo.AddCell(codigoblan)
                textoParrafo.AddCell(codigoblan)
                Dim tituloText2 As New PdfPCell(New Phrase("Ha contratado el siguiente producto:", fontNormal))
                tituloText2.Border = 0
                textoParrafo.AddCell(tituloText2)
                textoParrafo.AddCell(codigoblan)
                documento.Add(textoParrafo)
                documento.Add(lineablanco)
                ' Impresion de los productos , se genera tabla
                Dim tabla As New PdfPTable(2)
                tabla.SetWidths(New Single() {50.0F, 50.0F})
                Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontnegro)) With {
                    .BorderColor = colorBorde,
                    .BackgroundColor = New BaseColor(230, 230, 230),
                    .HorizontalAlignment = Element.ALIGN_CENTER
                }
                tabla.AddCell(titulo1)
                Dim celdaVacia As New PdfPCell(New Phrase(" ")) With {
                    .Border = PdfPCell.NO_BORDER
                }
                tabla.AddCell(celdaVacia)
                If dtconsulta2.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                        Dim det1 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("descripcion").ToString(), fontNormal))
                        det1.BorderColor = colorBorde
                        det1.HorizontalAlignment = Element.ALIGN_CENTER
                        tabla.AddCell(det1)
                        Dim celdaVacia2 As New PdfPCell(New Phrase(" "))
                        celdaVacia2.Border = PdfPCell.NO_BORDER
                        tabla.AddCell(celdaVacia2)
                        If origen = "COR" Or origen = "CON" Or origen = "ONE" Or origen = "IND" Or origen = "LAD" Or origen = "AMB" Or origen = "MAR" Then
                            Dim det5 As New PdfPCell(New Phrase("    ", fontNormal))
                            det5.Border = 0
                            det5.HorizontalAlignment = Element.ALIGN_RIGHT
                            tabla.AddCell(det5)
                        End If

                        Dim det4 As New PdfPCell(New Phrase("    ", fontNormal))
                        det4.Border = 0
                        det4.HorizontalAlignment = Element.ALIGN_RIGHT
                        tabla.AddCell(det4)
                    Next
                End If
                documento.Add(tabla)
            End If

            ' CERTIFICADO DE INSTALACIÓN
            If cbm_tipo.SelectedValue.ToString = "I" And (origen = "CNL" Or origen = "AMB" Or origen = "MAR" Or origen = "LAD" Or origen = "NOR" Or origen = "ONE" Or origen = "COR" Or origen = "CH") Then
                Dim textoParrafo As New PdfPTable(1)
                textoParrafo.SetWidths(New Single() {200.0F})
                Dim phrase As New Phrase From {
                    New Chunk("Certificamos que el Sr./Sra./Empresa ", fontNormal), ' Texto normal
                    New Chunk(Me.TxtConCliente.Text.ToUpper(), fontcliente) ' Nombre en negrita
                }
                Dim tituloText1 As New PdfPCell(phrase)
                tituloText1.Border = 0
                textoParrafo.AddCell(tituloText1)
                textoParrafo.AddCell(codigoblan)
                textoParrafo.AddCell(codigoblan)
                Dim tituloText2 As New PdfPCell(New Phrase("Ha adquirido los siguientes sistemas:", fontNormal))
                tituloText2.Border = 0
                textoParrafo.AddCell(tituloText2)
                textoParrafo.AddCell(codigoblan)
                documento.Add(textoParrafo)
                documento.Add(lineablanco)

                ' Impresion de los productos , se genera tabla
                Dim tabla As New PdfPTable(3)
                tabla.SetWidths(New Single() {40.0F, 24.0F, 22.0F}) ' Distribución de columnas

                Dim tituloProducto As New PdfPCell(New Phrase("PRODUCTO", fontProducto)) With {
                    .HorizontalAlignment = Element.ALIGN_CENTER,
                    .BackgroundColor = BaseColor.WHITE,
                    .BorderColor = colorBorde,
                    .Padding = 5
                }
                tabla.AddCell(tituloProducto)

                Dim tituloInstalacion As New PdfPCell(New Phrase("FECHA DE INSTALACIÓN", fontProducto)) With {
                    .HorizontalAlignment = Element.ALIGN_CENTER,
                    .BackgroundColor = BaseColor.WHITE,
                    .BorderColor = colorBorde,
                    .Padding = 5
                }
                tabla.AddCell(tituloInstalacion)

                Dim tituloVigencia As New PdfPCell(New Phrase("VIGENCIA DE SERVICIO", fontProducto)) With {
                    .HorizontalAlignment = Element.ALIGN_CENTER,
                    .BackgroundColor = BaseColor.WHITE,
                    .BorderColor = colorBorde,
                    .Padding = 5
                }
                tabla.AddCell(tituloVigencia)

                If dtconsulta2.Tables(0).Rows.Count > 0 Then
                    For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                        ' Descripción del producto
                        Dim descripcion As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("descripcion").ToString(), fontNormal)) With {
                            .BackgroundColor = BaseColor.WHITE,
                            .BorderColor = colorBorde,
                            .HorizontalAlignment = Element.ALIGN_CENTER,
                            .Padding = 5
                        }
                        tabla.AddCell(descripcion)

                        ' Fecha de instalación
                        Dim fechainstalacion As String = ObtenerFormatoFecha(dtconsulta2.Tables(0).Rows(i)("fecha_inicial").ToString())
                        Dim detFechaInstalacion As New PdfPCell(New Phrase(fechainstalacion, fontNormal)) With {
                            .BackgroundColor = BaseColor.WHITE,
                            .BorderColor = colorBorde,
                            .HorizontalAlignment = Element.ALIGN_CENTER,
                            .Padding = 5
                        }
                        tabla.AddCell(detFechaInstalacion)

                        ' Fecha de vigencia (instalación - fin)
                        Dim fechafin As String = ObtenerFormatoFecha(dtconsulta2.Tables(0).Rows(i)("fecha_fin").ToString())

                        Dim detVigencia As New PdfPCell(New Phrase(fechafin, fontNormal)) With {
                            .BackgroundColor = BaseColor.WHITE,
                            .BorderColor = colorBorde,
                            .HorizontalAlignment = Element.ALIGN_CENTER,
                            .Padding = 5
                        }
                        tabla.AddCell(detVigencia)
                    Next
                End If
                documento.Add(tabla)
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

            Dim textoParrafo2 As New PdfPTable(1)
            textoParrafo2.SetWidths(New Single() {240.0F})

            If cbm_tipo.SelectedValue.ToString = "C" Then
                Dim tituloText41 As New PdfPCell(New Phrase(texto3, fontNormal)) With {
                    .Border = 0,
                    .HorizontalAlignment = Element.ALIGN_LEFT
                }
                textoParrafo2.AddCell(tituloText41)
            End If
            textoParrafo2.AddCell(codigoblan)
            Dim tituloText4 As New PdfPCell(New Phrase(texto2, fontNormal))
            tituloText4.Border = 0
            textoParrafo2.AddCell(tituloText4)
            documento.Add(textoParrafo2)
            documento.Add(lineablanco)
            ' *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-* INFORMACIÓN BIEN *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
            Dim tabladatos As New PdfPTable(4)
            tabladatos.SetWidths(New Single() {20.0F, 105.0F, 20.0F, 65.0F})
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
            ' *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-* INFORMACIÓN BIEN *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*

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
            If origen IsNot "AMB" Then
                documento.Add(lineablanco)
                documento.Add(lineablanco)
            End If

            Dim textoParrafo22 As New PdfPTable(1)
            textoParrafo22.SetWidths(New Single() {240.0F})
            Dim tituloText43 As New PdfPCell(New Phrase("Los términos y condiciones del servicio pueden ser consultadas en nuestra página web", fontNormal)) With {
                .Border = 0,
                .HorizontalAlignment = Element.ALIGN_LEFT
            }
            textoParrafo22.AddCell(tituloText43)
            documento.Add(textoParrafo22)
            documento.Add(lineablanco)

            ' Fecha de emisión del pdf
            Dim lblfechahoraemision As String
            lblfechahoraemision = "Info Emisión Docto : " & Session("user_session") & " | " & System.DateTime.Now.ToString("dd MMMM yyyy") & " | " & System.DateTime.Now.ToString("H:mm:ss")
            Dim tablacv2 As New PdfPTable(2)
            tablacv2.SetWidths(New Single() {120.0F, 80.0F})
            Dim codigofecha As New PdfPCell(New Phrase(lblfechahoraemision, fontNormal)) With {
                .Border = 0,
                .HorizontalAlignment = Element.ALIGN_LEFT
            }
            tablacv2.AddCell(codigofecha)
            tablacv2.AddCell(codigoblan)
            documento.Add(tablacv2)
            ' Imprime logo del fin de pagina 
            If origen = "NOR" Then
                documento.Add(lineablanco)
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
            'dtconsulta = obj.ConsultarImprimir(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text, Session("user_id"), cbm_tipo.SelectedValue.ToString)
            dtconsulta = Session("Detalle")
            Session("infoqr") = dtconsulta
            Dim dtOsqr As New DataSet
            'Dim dataos As String
            dtOsqr = Session("infoqr")
            'dataos = dtOsqr.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
            datOs = "29082024"
            Dim horaActual As DateTime = TimeOfDay
            'datVehiculo = dtOsqr.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
            'datVehiculo = dtOsqr.Tables(0).Rows(0)("idvehiculo").ToString()
            Dim datVehiculoid As String
            datVehiculoid = dtOsqr.Tables(0).Rows(0)("idvehiculo").ToString()

            'datCliente = dtOsqr.Tables(0).Rows(0)("ID_CLIENTE").ToString()
            Dim datClienteBien As String
            datClienteBien = dtOsqr.Tables(0).Rows(0)("idcliente").ToString()
            'Dim data As String = "http://190.95.210.35:8083/login.aspx?os=" & dataos
            'Dim data As String = "https://www.hunteronline.com.ec/WSConcesionarios/Forms/ConsultaWebReporte.aspx?os= " & datOs & "&veh=" & datVehiculo & "&cli=" & datCliente
            Dim data As String = "https://www.hunteronline.com.ec/WSConcesionarios/Forms/ConsultaWebReporte.aspx?os= " & horaActual.ToString() & "&veh=" & datVehiculoid & "&cli=" & datCliente
            If String.IsNullOrEmpty(data) Then
                Return
            End If
            ' 0931760904
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