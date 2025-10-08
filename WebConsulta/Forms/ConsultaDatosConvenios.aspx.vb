Imports System.Drawing
Imports System.IO
Imports System.Net
Imports System.Net.Mail
Imports System.Reflection
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports MessagingToolkit.Barcode
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq
Imports Telerik.Web.UI
Imports Method = RestSharp.Method

Public Class ConsultaDatosConvenios
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
    Dim textoA As String = ""
    Dim textoB As String = ""
    Dim textoC As String = ""
    Dim textoD As String = ""
    Dim a As Int32 = 0
    Dim b As Int32 = 0
    Dim c As Int32 = 0
    Dim d As Int32 = 0


    Public Enum Operacion
        OExistosa = 1
        OInvalida = 2
        CSinDatos = 3
    End Enum

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Session("user_session") = "PRNARA"
            'Session("user_id") = 1625
            If Session("user_id") Is Nothing Then
                Response.Redirect("404.aspx")
            End If
            If Not (IsPostBack) Then
                usuario = Session("user_id")
                Control_Sesion()
                Botones("Inicial")
                CargaListaEstado()
                CargaListaCanal()
                InicializaObjetos()
                Session("dsDatos") = Nothing
                Session("Consulta_Datos") = Nothing
                Session("Consulta_Vehiculo") = Nothing
                'Session("Consulta_General") = Nothing
                usuarioOficina = Session("user_id")
                ipmaquina = Request.ServerVariables("REMOTE_ADDR")
                pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
                usuarioMaster = ""
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
                If Not CType(Session("Consulta_General"), DataSet) Is Nothing Then
                    If CType(Session("Consulta_General"), DataSet).Tables.Count > 0 Then
                        RadGridConsultar.DataSource = CType(Session("Consulta_General"), DataSet).Tables(0)
                    Else
                        Me.RadGridConsultar.DataSource = Nothing
                    End If
                Else
                    Me.RadGridConsultar.DataSource = CType(Session("Consulta_InicialGeneral"), DataSet).Tables(0)
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

#Region "Procesos"


    Sub Botones(opcion As String)
        Try
            Select Case opcion
                Case "Inicial"
                    Me.BtnNuevo.Enabled = False
                    Me.BtnConsultar.Enabled = True
                    Me.BtnImprimir.Enabled = False
                    Me.BtnCorreo.Enabled = False
                    'Me.BtnExportar.Enabled = False
                    Me.txtChasis.Enabled = True
                    Me.TxtMotor.Enabled = True
                    Me.txtCodVehiculo.Enabled = True
                    Me.cbm_estado.Enabled = True
                Case "Consultar"
                    Me.BtnNuevo.Enabled = True
                    Me.BtnConsultar.Enabled = False
                    Me.BtnImprimir.Enabled = False
                    'Me.BtnExportar.Enabled = True
                    Me.BtnCorreo.Enabled = False
                    Me.txtChasis.Enabled = False
                    Me.TxtMotor.Enabled = False
                    Me.txtCodVehiculo.Enabled = False
                    Me.cbm_estado.Enabled = False
            End Select
            Me.TxtConCliente.Enabled = False
            Me.txtConModelo.Enabled = False
            Me.txtConPlaca.Enabled = False
            Me.txtConTipo.Enabled = False
            Me.txtConColor.Enabled = False
            Me.txtConMarca.Enabled = False
            'Me.txtConConcesionario.Enabled = False
            'Me.txtConFinanciera.Enabled = False
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
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


    Private Sub MensajeTexto(ByVal texto As String, ByVal operacionRealizar As Int32)
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
            Me.rdpFechaInicio.MinDate = "1/1/2012"
            Me.rdpFechaInicio.MaxDate = "31/12/" & Date.Now.Year
            Me.rdpFechaFin.MinDate = "1/1/2012"
            Me.rdpFechaFin.MaxDate = "31/12/" & Date.Now.Year
            Me.rdpFechaInicio.SelectedDate = Now.AddDays((Now.Day - 1) * -1)
            Me.rdpFechaFin.SelectedDate = Date.Now.Date
            Me.radtabdatos.SelectedIndex = 0
            Me.RadMultiPage1.SelectedIndex = 0
            radtabdatos.Tabs.Item(1).Enabled = False
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
            Me.Label1.Text = ""
            Me.Label2.Text = ""
            'Me.txtConConcesionario.Text = ""
            'Me.txtConFinanciera.Text = ""
            Me.Txtconanio.Text = ""
            Me.RadGridVehiculo.DataSource = Session("Consulta_InicialVehiculo")
            RadGridVehiculo.Height = 110
            RadGridVehiculo.DataBind()
            Me.RadGridConsultar.DataSource = Session("Consulta_InicialGeneral")
            RadGridConsultar.Height = 280
            RadGridConsultar.DataBind()
            Me.cbm_estado.SelectedValue = "T"
            'Me.cmb_canal.SelectedValue = "NNN"
            Session("dtResultado") = Nothing
            Me.rgdResultado.DataSource = Session("dtResultado")
            Session("nombrepdf") = Nothing
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
            ConsultaGeneral()
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
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function


    Private Sub CargaListaEstado()
        Try
            Dim dTListaMotivo As DataSet
            dTListaMotivo = ConsultaWeb.ConsultaEstado()
            If dTListaMotivo.Tables(0).Rows.Count > 0 Then
                Me.cbm_estado.DataSource = dTListaMotivo
                Me.cbm_estado.DataTextField = "DESCRIPCION"
                Me.cbm_estado.DataValueField = "CODIGO"
                cbm_estado.DataBind()
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub CargaListaCanal()
        Try
            Dim dTListaMotivo As DataSet
            dTListaMotivo = ConsultaWeb.ConsultaCanal(usuario)
            If dTListaMotivo.Tables(0).Rows.Count > 0 Then
                Me.cmb_canal.DataSource = dTListaMotivo
                Me.cmb_canal.DataTextField = "DESCRIPCION"
                Me.cmb_canal.DataValueField = "RUC"
                cmb_canal.DataBind()
            End If
            For b = 0 To dTListaMotivo.Tables(0).Rows.Count - 1
                If dTListaMotivo.Tables(0).Rows(b)("PREDETERMINADO").ToString() = "S" Then
                    Me.cmb_canal.SelectedValue = dTListaMotivo.Tables(0).Rows(b)("RUC")
                    Exit For
                End If
            Next
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

#End Region

#Region "botones"


    Protected Sub BtnExportar_Click(sender As Object, e As System.EventArgs) Handles BtnExportar.Click
        Try
            'RadGrid1.ExportSettings.Excel.Format = DirectCast([Enum].Parse(GetType(GridExcelExportFormat), alternateText), GridExcelExportFormat)

            'Dim alternateText As String = "Xlsx"
            'Dim alternateText As String = TryCast(sender, ImageButton).AlternateText
            'If alternateText = "Xlsx" AndAlso CheckBox2.Checked Then
            RadGridConsultar.MasterTableView.GetColumn("DET_ESTADO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("DET_ESTADO").ItemStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("CHASIS").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("MOTOR").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("CODIGO_VEHICULO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("NOMBRE_COMPLETO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("CODIGO_CLIENTE").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("PLACA").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("MARCA").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("MODELO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("COLOR").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("CIUDAD").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("FECHA").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("PLAZO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("TURNO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("ANIO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("ORDEN_SERVICIO").HeaderStyle.BackColor = Color.LightGray
            'RadGridConsultar.MasterTableView.GetColumn("TIPO_TRANSACCION").HeaderStyle.BackColor = Color.LightGray
            '    RadGrid1.MasterTableView.GetColumn("EmployeeID").ItemStyle.BackColor = Color.LightGray
            'End If
            'RadGridConsultar.ExportSettings.Excel.Format = DirectCast([Enum].Parse(GetType(GridExcelExportFormat), "Xlsx"), GridExcelExportFormat)
            RadGridConsultar.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.ExcelML
            RadGridConsultar.ExportSettings.IgnorePaging = True
            RadGridConsultar.ExportSettings.ExportOnlyData = True
            RadGridConsultar.ExportSettings.OpenInNewWindow = True
            RadGridConsultar.MasterTableView.ExportToExcel()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub BtnConsultar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnConsultar.Click
        Try
            Dim bandera As Boolean = True
            If Me.cmb_canal.SelectedValue = "NNN" Then
                Me.MensajeTexto("Debe Seleccionar un canal para poder consultar", Operacion.OInvalida)
                bandera = False
            Else
                If Me.rdpFechaInicio.SelectedDate > Me.rdpFechaFin.SelectedDate Then
                    'Throw New Exception("La fecha Inicio debe de ser mayor a la fecha final, Por Verificar")
                    Me.MensajeTexto("La fecha Inicio debe de ser mayor a la fecha final, Por Verificar", Operacion.OInvalida)
                    bandera = False
                Else
                    If Me.rdpFechaInicio.SelectedDate.Value > Now.Date Then
                        'Throw New Exception("Fecha de inicio no puede ser mayor a la actual, Por Verificar")
                        Me.MensajeTexto("Fecha de inicio no puede ser mayor a la actual, Por Verificar", Operacion.OInvalida)
                        bandera = False
                    End If
                    Dim anioInicio = Year(rdpFechaInicio.SelectedDate.Value)
                    Dim anioFin = Year(rdpFechaFin.SelectedDate.Value)
                    Dim diasInicio = (DatePart("y", rdpFechaInicio.SelectedDate.Value))
                    Dim diasFin = (DatePart("y", rdpFechaFin.SelectedDate.Value))
                    Dim mesInicio = Month(rdpFechaInicio.SelectedDate.Value)
                    Dim mesFin = Month(rdpFechaFin.SelectedDate.Value)
                    If (anioFin - anioInicio) = 0 Then
                        If (mesFin - mesInicio) = 0 Then
                            bandera = True
                        Else
                            If (mesFin - mesInicio) = 1 Then
                                If (diasFin - diasInicio) > 31 Then
                                    'Throw New Exception("No se pueden consultar mas de 31 dias, Por Verificar")
                                    Me.MensajeTexto("No se pueden consultar mas de 31 dias, Por Verificar", Operacion.OInvalida)
                                    bandera = False
                                End If
                            Else
                                'Throw New Exception("No se pueden Consultar mas de un Mes, Por Verificar")
                                Me.MensajeTexto("No se pueden Consultar mas de un Mes, Por Verificar", Operacion.OInvalida)
                                bandera = False
                            End If
                        End If
                    Else
                        If (anioFin - anioInicio) = 1 Then
                            If (mesFin - mesInicio) = 0 Then
                                bandera = True
                            Else
                                If (mesFin - mesInicio) = 1 Then
                                    If (diasFin - diasInicio) > 31 Then
                                        'Throw New Exception("No se pueden consultar mas de 31 dias, Por Verificar")
                                        Me.MensajeTexto("No se pueden consultar mas de 31 dias, Por Verificar", Operacion.OInvalida)
                                        bandera = False
                                    End If
                                Else
                                    'Throw New Exception("No se pueden Consultar mas de un Mes, Por Verificar")
                                    Me.MensajeTexto("No se pueden Consultar mas de un Mes, Por Verificar", Operacion.OInvalida)
                                    bandera = False
                                End If
                            End If
                        Else
                            'Throw New Exception("No se pueden Consultar mas de un Mes, Por Verificar")
                            Me.MensajeTexto("No se pueden Consultar mas de un Mes, Por Verificar", Operacion.OInvalida)
                            bandera = False
                        End If
                    End If
                End If
            End If
            If bandera Then
                Botones("Inicial")
                If (Me.txtChasis.Text <> "") Or (Me.TxtMotor.Text <> "") Or (Me.txtCodVehiculo.Text <> "") Then
                    'Botones("Consultar")
                    Consultar("1")
                    ConsultarporCampo()
                    'RadGridConsultar.DataBind()
                Else
                    Consultar("2")
                    'RadGridConsultar.DataBind()
                End If
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
            Dim dtTransaccion As New System.Data.DataSet
            Dim latam As Boolean = False
            Dim coris As Boolean = False
            Dim origen As String = ""
            dtTransaccion = obj.Consulta_Transaccion(Me.txtCodVehiculo.Text, usuarioOficina, "C")
            If dtTransaccion.Tables(0).Rows.Count > 0 Then
                For b = 0 To dtTransaccion.Tables(0).Rows.Count - 1
                    If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "LA" Then
                        latam = True
                        origen = "L"
                        Exit For
                    End If
                    If dtTransaccion.Tables(0).Rows(b)("PRODUCTO").ToString() = "AT" Then
                        coris = True
                        origen = "C"
                        Exit For
                    End If
                Next
            End If
            obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, " ", origen, True, " Impreso: ")
            If latam Then
                Redirect("ConsultaReporteLatam.aspx", "_blank", "menubar=0,width=850,height=850")
            Else
                If coris Then
                    'falta la pagina
                    Session("origen") = origen
                    Redirect("ConsultaReporteConvenio.aspx", "_blank", "menubar=0,width=850,height=850")
                Else
                    Redirect("ConsultaWebReporte.aspx", "_blank", "menubar=0,width=850,height=850")
                End If
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Protected Sub BtnCorreo_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCorreo.Click
        Try
            'TxtCorreo.Visible = True
            TxtCorreo.Enabled = True
            BtnCancela.Visible = True
            BtnEnvia.Visible = True
            'titcorreo.Visible = True
            BtnConsultar.Visible = False
            BtnNuevo.Visible = False
            'Dim obj As New ConsultaWeb
            'obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, " ", True)
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


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


    Private Sub RadGridConsultar_ItemDataBound(sender As Object, e As Telerik.Web.UI.GridItemEventArgs) Handles RadGridConsultar.ItemDataBound
        Try
            If TypeOf e.Item Is GridDataItem Then
                Dim dataItem As GridDataItem = e.Item
                Dim myCell As TableCell = dataItem("TIPO_TRANSACCION")
                If myCell.Text = "DLA" Then
                    'dataItem.BackColor = Drawing.Color.Red
                    dataItem.ForeColor = Drawing.Color.Red
                End If
                Dim estado As Boolean = False
                Dim myCell2 As TableCell = dataItem("DET_ESTADO")
                If myCell2.Text = "INSTALADO" Then
                    a += 1
                    textoA = " Instalado - " & a
                    estado = True
                End If
                If myCell2.Text = "PENDIENTE INSTALAR" Then
                    b += 1
                    textoB = " Pendiente Instalar - " & b
                    estado = True
                End If
                Dim myCell3 As TableCell = dataItem("ACC")
                If myCell3.Text = "001" Then
                    c += 1
                    textoC = " Instalación - " & c
                    estado = True
                End If
                If myCell3.Text = "003" Then
                    d += 1
                    textoD = " Reinstalación - " & d
                    estado = True
                End If
                If estado Then
                    Dim texto As String = "Estado de Trabajo: " + textoA + " " + textoB
                    Label1.Text = texto
                    Dim texto2 As String = "Acción Comercial: " + textoC + " " + textoD
                    Label2.Text = texto2
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub RadGridConsultar_NeedDataSource(ByVal sender As Object, ByVal e As Telerik.Web.UI.GridNeedDataSourceEventArgs) Handles RadGridConsultar.NeedDataSource
        Try
            Me.RadGridConsultar.DataSource = Session("Consulta_General")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub RadGridConsultar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles RadGridConsultar.SelectedIndexChanged
        Try
            Dim item As GridDataItem = Nothing
            item = RadGridConsultar.SelectedItems(0)
            txtCodVehiculo.Text = item("CODIGO_VEHICULO").Text.Trim
            txtConCodCliente.Text = item("CODIGO_CLIENTE").Text.Trim.ToString
            txtConTipo.Text = item("TIPO").Text.Trim.ToString
            txtChasis.Text = item("CHASIS").Text.Trim.ToString
            TxtMotor.Text = item("MOTOR").Text.Trim.ToString
            txtConPlaca.Text = item("PLACA").Text.Trim.ToString
            Txtconanio.Text = item("ANIO").Text.Trim.ToString
            TxtConCliente.Text = item("NOMBRE_COMPLETO").Text.Trim.ToString
            txtConColor.Text = item("COLOR").Text.Trim.ToString
            txtConMarca.Text = item("MARCA").Text.Trim.ToString
            txtConModelo.Text = item("MODELO").Text.Trim.ToString
            'cbm_estado.Text = item("DET_ESTADO").Text.Trim.ToString
            'cbm_estado.DataBind()
            Dim estado As String = item("DET_ESTADO").Text.Trim.ToString
            If Validar() Then
                If estado = ("INSTALADO") Then
                    ConsultarDetalle()
                    'radtabdatos.Tabs.Item(1).Selected = True
                    'RadMultiPage1.SelectedIndex = 1
                    radtabdatos.Tabs.Item(1).Enabled = True
                Else
                    'radtabdatos.Tabs.Item(0).Selected = True
                    Me.RadMultiPage1.SelectedIndex = 0
                    radtabdatos.Tabs.Item(1).Enabled = False
                    Me.BtnImprimir.Enabled = False
                    Me.BtnCorreo.Enabled = False
                    accionHabilita = 0
                    'Throw New Exception("No se puede Imprimir el documento")
                    Me.MensajeTexto("No se puede Imprimir el documento", Operacion.OInvalida)
                End If
            Else
                'Throw New Exception("Ingrese datos para poder consultar")
                Me.MensajeTexto("Ingrese datos para poder consultar", Operacion.OInvalida)
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


#End Region

#Region "Procedimientos generales"


    Protected Sub AddAttributesToRender(ByVal writer As HtmlTextWriter, ByVal email As String)
        writer.AddAttribute(HtmlTextWriterAttribute.Href, email)
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


    Private Sub ConsultaGeneral()
        Try
            Dim obj As New ConsultaWeb
            Dim dtInicial As New System.Data.DataSet
            dtInicial = CType(Session("Consulta_General"), DataSet)
            dtInicial = obj.ConsultaInicialVehiculo(10)
            'dtInicial = obj.ConsultaInicialVehiculo(11)
            RadGridConsultar.DataSource = Nothing
            Session("Consulta_InicialGeneral") = dtInicial
            RadGridConsultar.DataSource = dtInicial.Tables(0)
            'RadGridConsultar.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
            RadGridConsultar.Height = 280
            RadGridConsultar.ClientSettings.Scrolling.AllowScroll = True
            RadGridConsultar.ClientSettings.Scrolling.UseStaticHeaders = True
            RadGridConsultar.ClientSettings.Scrolling.SaveScrollPosition = True
            RadGridConsultar.DataBind()
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub InhabilitaCertificado(ByVal accion As Boolean)
        Try
            Me.BtnImprimir.Enabled = accion
            Me.BtnCorreo.Enabled = accion
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub




    Private Sub ConsultarporCampo()
        Session("Detalle") = Nothing
        Session("dtResultado") = Nothing
            ' *-*-*-*-*-*-*-*-*-*-*-
            Dim obj As New ConsultaWeb
            Dim consultar As Boolean
            Dim resultadoRESTlet As New List(Of String)
            Dim rucCanales As String = Session("ruccanales")
        resultadoRESTlet = RequestApi.Fetch("1", Me.txtChasis.Text, Me.TxtMotor.Text, Me.txtCodVehiculo.Text, Me.txtConPlaca.Text, "", rucCanales, Method.POST, "4042")
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

        'Session("errores") = Nothing
        If Session("errores") IsNot Nothing Then
                Dim errores As List(Of SMSError) = JsonConvert.DeserializeObject(Of List(Of SMSError))(Session("errores").ToString)
                For Each errorItem As SMSError In errores
                    If errorItem.CodigoError = "003" Then
                        ' Vehículo no registrado en el sistema. No existen datos disponibles. Por favor, verifique la información.
                        Me.MensajeTexto(errorItem.Mensaje, Operacion.OInvalida)
                        LimpiarControl()
                        Return
                    ElseIf errorItem.CodigoError = "007" Then
                        Me.MensajeTexto(errorItem.Mensaje, Operacion.OInvalida)
                        LimpiarControl()
                        Return
                    ElseIf errorItem.CodigoError = "017" Then
                        Me.MensajeTexto(errorItem.Mensaje, Operacion.OInvalida)
                        LimpiarControl()
                        Return
                    Else
                        mensajes.Add(errorItem.Mensaje)
                    End If
                Next
            Else
            Botones("Consultar")
            Dim producto_conteo = 0
            For i As Integer = 0 To resultadoRESTlet.Count - 1
                    If i = 0 Then ' [0] = Bien
                        vehiculo = resultadoRESTlet(i)
                        Dim bienObject As JObject = JObject.Parse(vehiculo)
                        Dim codigo_estado_bien As String = bienObject("codigo_estado_bien").ToString() ' 11 = VIGENTE O ACTIVO
                        bien = JsonConvert.DeserializeObject(Of Bien)(vehiculo)
                        Dim datasetVehiculo As DataSet = ConvertToDataSet(bien)
                        Session("Detalle") = datasetVehiculo
                        Session("codigo_estado_bien") = codigo_estado_bien
                        flagBien = True
                    Else
                        ' [...] productos
                        Botones("Consultar")
                        'Me.cbm_tipo.Enabled = True
                        consultar = True
                        detalle = resultadoRESTlet(i)
                        Dim productoObject As JObject = JObject.Parse(detalle)
                        Dim estado_codigo As String = productoObject("codigo_estado_internal").ToString()
                        Dim familia_producto_codigo As String = productoObject("familia").ToString()
                        Session("familia_codigo_producto") = familia_producto_codigo
                        Session("codigo_estado_producto") = estado_codigo
                        producto = JsonConvert.DeserializeObject(Of Producto)(detalle)

                    If (producto_conteo = 0) Then
                        dataset = ConvertToDataSet(producto)

                    Else
                        Dim dataSetTemporal = ConvertToDataSet(producto)
                        dataset.Merge(dataSetTemporal)
                    End If
                    producto_conteo += 1
                    flagProducto = True

                End If
                Next
            End If

        'Session("bien") = Nothing
        'Session("producto") = Nothing
        Try

                If flagBien Then
                    'Session("Detalle") = Nothing
                    Session("dtResultado") = Nothing
                    Dim dtConsulta As New System.Data.DataSet
                    'dtConsulta = CType(Session("Consulta_Datos"), DataSet)
                    Me.txtCodVehiculo.Text = bien.IdVehiculo
                    Me.txtChasis.Text = bien.Chasis
                    Me.TxtMotor.Text = bien.Motor
                    'Me.txtConConcesionario.Text = bien.Concesionario
                    'Me.txtConFinanciera.Text = bien.Financiera
                    Me.TxtConCliente.Text = bien.Cliente
                    Me.txtConCodCliente.Text = bien.IdCliente
                    Me.txtConMarca.Text = bien.Marca
                    Me.txtConModelo.Text = bien.Modelo
                    Me.txtConPlaca.Text = bien.Placa
                    Me.txtConTipo.Text = bien.Tipo
                    Me.txtConColor.Text = bien.Color
                    Me.Txtconanio.Text = bien.Anio
                    txtanio = bien.Anio
                    'Me.txtCodConvenio.Text = bien.CodigoConvenio
                End If

                If flagProducto = True Then
                    RadGridVehiculo.DataSource = dataset
                    RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
                    RadGridVehiculo.Height = 150
                    RadGridVehiculo.DataBind()
                    InhabilitaCertificado(True)
                    consultar = True
                    Session("datocliente") = Me.TxtConCliente.Text + " | " + Me.txtConMarca.Text + " | " + Me.txtConModelo.Text + " | " + Me.txtChasis.Text + " | " + Me.txtConPlaca.Text
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
    End Sub

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

    'Private Sub ConsultarporCampo()
    '    Try
    '        Session("Detalle") = Nothing
    '        Session("dtResultado") = Nothing
    '        Dim obj As New ConsultaWeb
    '        Dim consultar As Boolean
    '        Dim dtConsulta As New System.Data.DataSet
    '        dtConsulta = CType(Session("Consulta_Datos"), DataSet)
    '        dtConsulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, "", "", Me.txtChasis.Text, "", Me.TxtMotor.Text, usuarioOficina)
    '        consultar = False
    '        If dtConsulta.Tables(0).Rows.Count > 0 Then
    '            ''Inhabilito el botón de impresión de certificados si este campo viene con 0
    '            accionHabilita = dtConsulta.Tables(0).Rows(0)("CAMB_PROP_ESTADO").ToString()
    '            varRei = dtConsulta.Tables(0).Rows(0)("ESTADO_REI").ToString()
    '            If varRei = True Then
    '                accionHabilita = 0
    '            End If
    '            'Dim dtResultado As New System.Data.DataSet
    '            'dtResultado = obj.Consulta_Mensaje(Me.txtCodVehiculo.Text, usuarioOficina, txtConPlaca.Text, TxtMotor.Text, txtChasis.Text)
    '            'Session("dtResultado") = dtResultado
    '            'If dtResultado.Tables(0).Rows.Count > 0 Then
    '            '    rgdResultado.DataSource = Session("dtResultado")
    '            '    rgdResultado.DataBind()
    '            '    rntResultado.Title = "Mensaje de la Aplicación Consulta Certificados"
    '            '    rntResultado.TitleIcon = "info"
    '            '    rntResultado.ContentIcon = "info"
    '            '    rntResultado.ShowSound = "info"
    '            '    rntResultado.Width = 380
    '            '    rntResultado.Height = 250
    '            '    rntResultado.Show()
    '            '    accionHabilita = 0
    '            'End If
    '            'Botones("Consultar")
    '            If accionHabilita <> 0 Then
    '                consultar = True
    '                Me.txtCodVehiculo.Text = dtConsulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
    '                Me.txtChasis.Text = dtConsulta.Tables(0).Rows(0)("CHASIS").ToString()
    '                Me.TxtMotor.Text = dtConsulta.Tables(0).Rows(0)("MOTOR").ToString()
    '                'Me.txtConConcesionario.Text = dtConsulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
    '                'Me.txtConFinanciera.Text = dtConsulta.Tables(0).Rows(0)("FINANCIERA").ToString()
    '                Me.TxtConCliente.Text = dtConsulta.Tables(0).Rows(0)("CLIENTE").ToString()
    '                Me.txtConCodCliente.Text = dtConsulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
    '                Me.txtConMarca.Text = dtConsulta.Tables(0).Rows(0)("MARCA").ToString()
    '                Me.txtConModelo.Text = dtConsulta.Tables(0).Rows(0)("MODELO").ToString()
    '                Me.txtConPlaca.Text = dtConsulta.Tables(0).Rows(0)("PLACA").ToString()
    '                Me.txtConTipo.Text = dtConsulta.Tables(0).Rows(0)("TIPO").ToString()
    '                Me.txtConColor.Text = dtConsulta.Tables(0).Rows(0)("COLOR").ToString()
    '                Me.Txtconanio.Text = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
    '                txtanio = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
    '                Dim dtVehiculo As New System.Data.DataSet
    '                dtVehiculo = obj.ConsultaCodigo(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text, "C", usuarioOficina)
    '                If dtVehiculo.Tables(0).Rows.Count > 0 Then
    '                    Session("Detalle") = dtVehiculo
    '                    RadGridVehiculo.DataSource = dtVehiculo.Tables(0)
    '                    RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
    '                    RadGridVehiculo.Height = 150
    '                    Me.BtnImprimir.Enabled = True
    '                    Me.BtnCorreo.Enabled = True
    '                Else
    '                    RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
    '                    Me.BtnImprimir.Enabled = False
    '                    Me.BtnCorreo.Enabled = False
    '                    accionHabilita = 0
    '                    consultar = False
    '                End If
    '            End If
    '            If Session("user_master") = "S" Then
    '                consultar = True
    '                Me.txtCodVehiculo.Text = dtConsulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
    '                Me.txtChasis.Text = dtConsulta.Tables(0).Rows(0)("CHASIS").ToString()
    '                Me.TxtMotor.Text = dtConsulta.Tables(0).Rows(0)("MOTOR").ToString()
    '                'Me.txtConConcesionario.Text = dtConsulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
    '                'Me.txtConFinanciera.Text = dtConsulta.Tables(0).Rows(0)("FINANCIERA").ToString()
    '                Me.TxtConCliente.Text = dtConsulta.Tables(0).Rows(0)("CLIENTE").ToString()
    '                Me.txtConCodCliente.Text = dtConsulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
    '                Me.txtConMarca.Text = dtConsulta.Tables(0).Rows(0)("MARCA").ToString()
    '                Me.txtConModelo.Text = dtConsulta.Tables(0).Rows(0)("MODELO").ToString()
    '                Me.txtConPlaca.Text = dtConsulta.Tables(0).Rows(0)("PLACA").ToString()
    '                Me.txtConTipo.Text = dtConsulta.Tables(0).Rows(0)("TIPO").ToString()
    '                Me.txtConColor.Text = dtConsulta.Tables(0).Rows(0)("COLOR").ToString()
    '                Me.Txtconanio.Text = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
    '            End If
    '            RadGridVehiculo.DataBind()
    '            InhabilitaCertificado(accionHabilita)
    '            Session("datocliente") = Me.TxtConCliente.Text + " | " + Me.txtConMarca.Text + " | " + Me.txtConModelo.Text + " | " + Me.txtChasis.Text + " | " + Me.txtConPlaca.Text
    '        Else
    '            'Throw New Exception("No Existen Datos que Presentar, Por Verificar")
    '            Me.MensajeTexto("No Existen Datos que Presentar, Por Verificar", Operacion.OInvalida)
    '        End If
    '        obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "N", ipmaquina, pantalla, " ", " ", consultar, " Consulto: ")
    '    Catch ex As Exception
    '        Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    ''' <summary>
    ''' Motivo: método que permite consultar los datos deacuerdo a los criterios
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Consultar(ByVal opcion As String)
        Try
            Session("Detalle") = Nothing
            Session("dtResultado") = Nothing
            Session("Consulta_General") = Nothing
            Dim obj As New ConsultaWeb
            Dim dtGeneral As New System.Data.DataSet
            Dim codigo As String = ""
            Dim motor As String = ""
            Dim chasis As String = ""
            Dim proceso As Int32 = 0
            If opcion = "1" Then
                codigo = Me.txtCodVehiculo.Text.ToString
                motor = Me.TxtMotor.Text.ToString
                chasis = Me.txtChasis.Text.ToString
                proceso = 17
            Else
                codigo = ""
                motor = ""
                chasis = ""
                proceso = 16
            End If
            Dim tipoCanal As String = cmb_canal.SelectedValue.ToString.Substring(0, 1)
            Dim rucCanal As String = cmb_canal.SelectedValue.ToString.Substring(2, Len(cmb_canal.SelectedValue.ToString) - 2)
            dtGeneral = obj.ConsultarLatam(Session.Item("user_id"), rdpFechaInicio.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", ""),
                                            rdpFechaFin.SelectedDate.Value.ToString("yyyy/MM/dd").Replace("/", ""), chasis, motor, codigo, proceso, cbm_estado.SelectedValue.ToString, tipoCanal, rucCanal)
            If dtGeneral.Tables(0).Rows.Count > 0 Then
                Session("Consulta_General") = dtGeneral
                RadGridConsultar.DataSource = dtGeneral.Tables(0)
                RadGridConsultar.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
                RadGridConsultar.Height = 280
                RadGridConsultar.DataBind()
                Botones("Consultar")
            Else
                'Throw New Exception("No Existen Datos que Presentar, Por Verificar")
                Me.MensajeTexto("No Existen Datos que Presentar, Por Verificar", Operacion.OInvalida)
            End If
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub ConsultarDetalle()
        Try
            Session("Detalle") = Nothing
            Session("dtResultado") = Nothing
            'Session("Consulta_General") = Nothing
            Dim consultar As Boolean
            Dim dtConsulta As New System.Data.DataSet
            Dim obj As New ConsultaWeb
            dtConsulta = CType(Session("Consulta_Datos"), DataSet)
            dtConsulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, "", "", Me.txtChasis.Text, "", Me.TxtMotor.Text, usuarioOficina)
            consultar = False
            If dtConsulta.Tables(0).Rows.Count > 0 Then
                'Inhabilito el botón de impresión de certificados si este campo viene con 0
                accionHabilita = dtConsulta.Tables(0).Rows(0)("CAMB_PROP_ESTADO").ToString()
                varRei = dtConsulta.Tables(0).Rows(0)("ESTADO_REI").ToString()
                If varRei = True Then
                    accionHabilita = 0
                End If
                'Dim dtResultado As New System.Data.DataSet
                'dtResultado = obj.Consulta_Mensaje(Me.txtCodVehiculo.Text, usuarioOficina, txtConPlaca.Text, TxtMotor.Text, txtChasis.Text)
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
                '    accionHabilita = 0
                'End If
                Botones("Consultar")
                If accionHabilita <> 0 Then
                    consultar = True
                    Me.txtCodVehiculo.Text = dtConsulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
                    Me.txtChasis.Text = dtConsulta.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.TxtMotor.Text = dtConsulta.Tables(0).Rows(0)("MOTOR").ToString()
                    'Me.txtConConcesionario.Text = dtConsulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
                    'Me.txtConFinanciera.Text = dtConsulta.Tables(0).Rows(0)("FINANCIERA").ToString()
                    Me.TxtConCliente.Text = dtConsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.txtConCodCliente.Text = dtConsulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
                    Me.txtConMarca.Text = dtConsulta.Tables(0).Rows(0)("MARCA").ToString()
                    Me.txtConModelo.Text = dtConsulta.Tables(0).Rows(0)("MODELO").ToString()
                    Me.txtConPlaca.Text = dtConsulta.Tables(0).Rows(0)("PLACA").ToString()
                    Me.txtConTipo.Text = dtConsulta.Tables(0).Rows(0)("TIPO").ToString()
                    Me.txtConColor.Text = dtConsulta.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Txtconanio.Text = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
                    txtanio = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
                    Dim dtVehiculo As New System.Data.DataSet
                    dtVehiculo = obj.ConsultaCodigo(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text, "C", usuarioOficina)
                    If dtVehiculo.Tables(0).Rows.Count > 0 Then
                        Session("Detalle") = dtVehiculo
                        RadGridVehiculo.DataSource = dtVehiculo.Tables(0)
                        RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
                        RadGridVehiculo.Height = 150
                        Me.BtnImprimir.Enabled = True
                        Me.BtnCorreo.Enabled = True
                    Else
                        RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
                        Me.BtnImprimir.Enabled = False
                        Me.BtnCorreo.Enabled = False
                        accionHabilita = 0
                        consultar = False
                    End If
                End If
                If Session("user_master") = "S" Then
                    consultar = True
                    Me.txtCodVehiculo.Text = dtConsulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
                    Me.txtChasis.Text = dtConsulta.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.TxtMotor.Text = dtConsulta.Tables(0).Rows(0)("MOTOR").ToString()
                    'Me.txtConConcesionario.Text = dtConsulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
                    'Me.txtConFinanciera.Text = dtConsulta.Tables(0).Rows(0)("FINANCIERA").ToString()
                    Me.TxtConCliente.Text = dtConsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.txtConCodCliente.Text = dtConsulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
                    Me.txtConMarca.Text = dtConsulta.Tables(0).Rows(0)("MARCA").ToString()
                    Me.txtConModelo.Text = dtConsulta.Tables(0).Rows(0)("MODELO").ToString()
                    Me.txtConPlaca.Text = dtConsulta.Tables(0).Rows(0)("PLACA").ToString()
                    Me.txtConTipo.Text = dtConsulta.Tables(0).Rows(0)("TIPO").ToString()
                    Me.txtConColor.Text = dtConsulta.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Txtconanio.Text = dtConsulta.Tables(0).Rows(0)("ANIO").ToString()
                End If
                RadGridVehiculo.DataBind()
                InhabilitaCertificado(accionHabilita)
                Session("datocliente") = Me.TxtConCliente.Text + " | " + Me.txtConMarca.Text + " | " + Me.txtConModelo.Text + " | " + Me.txtChasis.Text + " | " + Me.txtConPlaca.Text
            Else
                'Throw New Exception("No Existen Datos que Presentar, Por Verificar")
                Me.MensajeTexto("No Existen Datos que Presentar, Por Verificar", Operacion.OInvalida)
            End If
            obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "N", ipmaquina, pantalla, " ", " ", consultar, " Consulto: ")
        Catch ex As Exception
            Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

#End Region


#Region "DATOS DEL GRID"


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


    Protected Sub Btnenviar(ByVal sender As Object, ByVal e As EventArgs) Handles BtnEnvia.Click, BtnEnvia.Click
        Try
            If Me.TxtCorreo.Text = "" Then
                'Throw New Exception("No ha ingresado dirección de correo...")
                Me.MensajeTexto("No ha ingresado dirección de correo...", Operacion.OInvalida)
            Else
                Dim obj As New ConsultaWeb
                Dim dtTransaccion As New System.Data.DataSet
                Dim latam As Boolean = False
                Dim coris As Boolean = False
                Dim coneca As Boolean = False
                Dim financiera As Boolean = False
                Dim origen As String = ""
                dtTransaccion = obj.Consulta_Transaccion(Me.txtCodVehiculo.Text, usuarioOficina, "C")
                Dim producto_familia_nombre = Session("familia_codigo_producto").ToString()
                Session("txtCodConvenio") = Me.txtCodVehiculo.Text
                If dtTransaccion.Tables(0).Rows.Count > 0 Then
                    If producto_familia_nombre = "LA" Then
                        latam = True
                        origen = "L"
                    End If
                    If producto_familia_nombre = "AT" Then
                        coris = True
                        origen = "C"
                    End If

                    If producto_familia_nombre = "CM" Then
                        coneca = True
                        origen = "O"
                    End If

                    If producto_familia_nombre = "CH" Then
                        financiera = True
                        origen = "C"
                    End If
                End If
                Dim orden As String = ""
                'If Not latam Then
                '    ' Se llama  que genera el archivo PDF
                '    CargaImagenQR()
                '    GeneraPdf_Click()
                'Else
                '    dtTransaccion = obj.Consulta_Orden(Me.txtCodVehiculo.Text, usuarioOficina, Me.txtConCodCliente.Text)
                '    orden = dtTransaccion.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
                'End If
                Dim x As String = "x"
                If latam Then
                    'dtTransaccion = obj.Consulta_Orden(Me.txtCodVehiculo.Text, usuarioOficina, Me.txtConCodCliente.Text, "9")
                    'orden = dtTransaccion.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
                    orden = x
                Else
                    If coris Then
                        'dtTransaccion = obj.Consulta_Orden(Me.txtCodVehiculo.Text, usuarioOficina, Me.txtConCodCliente.Text, "18")
                        'orden = dtTransaccion.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
                        orden = x
                    ElseIf financiera Then
                        CargaImagenQR()
                        GeneraPdf_Click("CON")
                    ElseIf coneca Then
                        CargaImagenQR()
                        GeneraPdf_Click("ONE")
                    Else
                        CargaImagenQR()
                        GeneraPdf_Click("NOR")
                    End If
                End If
                Dim htmlcuerpo As String
                If latam Then
                    htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Instalación del Producto</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                Else
                    htmlcuerpo = "<html><font style=""font-family:Calibri; color:#666666;"">Estimado(a).</font><body><br/> <font style= ""color:#666666;""> Adjunto se encuentra el </font><font style=""color: #000000; "">Certificado de Instalación del Producto Hunter</font> <font style=""color: #666666; "">realizado en el vehículo que detallamos </font><br/>"
                End If
                htmlcuerpo = htmlcuerpo + " <br/> "
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Marca : </font> <font style=""color: #666666;"">" + "&nbsp;" + Me.txtConMarca.Text + "</font> <br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Modelo: </font> <font style=""color: #666666;"">" + Me.txtConModelo.Text + "</font><br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Chasis: </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + Me.txtChasis.Text + "</font><br/>"
                htmlcuerpo = htmlcuerpo + "<font style=""color: #000000;"">Placa : </font> <font style=""color: #666666;"">" + "&nbsp;" + "&nbsp;" + "&nbsp;" + Me.txtConPlaca.Text + "</font><br/>"
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
                    If coris Then
                        mailMessage.Subject = "Certificado de Instalación del Producto, " + Session("datocliente")
                        Session("nombrepdf") = "\\10.100.107.14\Coris_Certificado\cert_" & Me.txtConCodCliente.Text & "_" & Me.txtCodVehiculo.Text & "_" & orden & ".pdf"
                    Else
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
                mailMessage.To.Add(correo1)
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
                'SmtpClient.Send(mailMessage)
                mailMessage.Dispose()
                'TxtCorreo.Visible = False
                BtnCancela.Visible = False
                BtnEnvia.Visible = False
                'titcorreo.Visible = False
                BtnConsultar.Visible = True
                BtnNuevo.Visible = True
                BtnCorreo.Enabled = True
                BtnNuevo.Enabled = True
                BtnImprimir.Enabled = False
                obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "S", ipmaquina, pantalla, Me.TxtCorreo.Text, origen, True, " Email: ")
                TxtCorreo.Text = ""
                'If Not latam Then
                If Not latam And Not coris Then
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


    Protected Sub GeneraPdf_Click(origen As String)
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
           

            If origen = "ONE" Then
                Dim ev As New creacionpdf_coneca()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
            Else
                Dim ev As New creacionpdf()
                documento.Open()
                documento.NewPage()
                writer.PageEvent = ev
            End If
            'If cbm_tipo.SelectedValue.ToString = "C" Then
            '    Dim ev As New creacionpdf()
            '    documento.Open()
            '    documento.NewPage()
            '    writer.PageEvent = ev

            'documento.Open()
            'documento.NewPage()
            'writer.PageEvent = ev
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
            'Dim nombre As New iTextSharp.text.Paragraph(espacio2 & "Certifica que el Sr.(a). " & Me.TxtConCliente.Text, fontcliente)
            'Dim nombre As New iTextSharp.text.Paragraph(espacio2 & "Certificamos que el Sr./Sra. " & Me.TxtConCliente.Text, fontcliente)
            'documento.Add(nombre)
            'documento.Add(lineablanco)
            'Dim linea1 As New iTextSharp.text.Paragraph(espacio & "Ha adquirido los siguientes sistemas:", fontNormal)
            'documento.Add(linea1)

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

            documento.Add(lineablanco)
            ' Impresion de los productos , se genera tabla estaba fontbold
            'Dim tabla As New PdfPTable(5)
            'tabla.SetWidths(New Single() {5.0F, 40.0F, 20.0F, 32.0F, 3.0F})
            Dim tabla As New PdfPTable(2)
            tabla.SetWidths(New Single() {50.0F, 50.0F})
            'Dim titulo4 As New PdfPCell(New Phrase(" ", fontnegro))
            'titulo4.Border = 0
            'titulo4.HorizontalAlignment = Element.ALIGN_RIGHT
            'tabla.AddCell(titulo4)
            Dim titulo1 As New PdfPCell(New Phrase("PRODUCTO", fontnegro))
            titulo1.Border = 0
            titulo1.BackgroundColor = New BaseColor(230, 230, 230)
            titulo1.HorizontalAlignment = Element.ALIGN_CENTER
            tabla.AddCell(titulo1)
            Dim titulo2 As New PdfPCell(New Phrase("VIGENCIA DEL SERVICIO*", fontnegro))
            titulo2.Border = 0
            titulo2.BackgroundColor = New BaseColor(230, 230, 230)
            titulo2.HorizontalAlignment = Element.ALIGN_LEFT
            tabla.AddCell(titulo2)
            If origen = "CON" Or origen = "ONE" Then
                'tabla.AddCell(titulo4)
            Else
                'Dim titulo3 As New PdfPCell(New Phrase("P.V.P INCLUIDO IVA", fontnegro))
                'titulo3.Border = 0
                'titulo3.BackgroundColor = New BaseColor(230, 230, 230)
                'titulo3.HorizontalAlignment = Element.ALIGN_RIGHT
                'tabla.AddCell(titulo3)
            End If

            'tabla.AddCell(titulo4)
            If dtconsulta2.Tables(0).Rows.Count > 0 Then
                For i = 0 To dtconsulta2.Tables(0).Rows.Count - 1
                    'tabla.AddCell(titulo4)
                    Dim det1 As New PdfPCell(New Phrase(dtconsulta2.Tables(0).Rows(i)("descripcion").ToString(), fontNormal))
                    det1.Border = 0
                    det1.HorizontalAlignment = Element.ALIGN_LEFT
                    tabla.AddCell(det1)
                    Dim fechafinal As String = ""
                    Dim valorfecha As String
                    valorfecha = dtconsulta2.Tables(0).Rows(i)("fecha_fin").ToString()
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
                    Dim dia As String = CInt(Mid(valorfecha, 1, 2)).ToString() ' Extrae el día y lo convierte a un número para eliminar el 0 inicial.
                    Dim mes As String = Mid(valorfecha, 4, 2) ' Extrae el mes.
                    Dim año As String = Mid(valorfecha, 7, 4) ' Extrae el año.
                    If meses.ContainsKey(mes) Then
                        fechafinal = dia & "/" & meses(mes) & "/" & año
                    End If
                    'If Mid(valorfecha, 4, 2) = "01" Then fechafinal = Mid(valorfecha, 1, 3) & "ene/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "02" Then fechafinal = Mid(valorfecha, 1, 3) & "feb/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "03" Then fechafinal = Mid(valorfecha, 1, 3) & "mar/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "04" Then fechafinal = Mid(valorfecha, 1, 3) & "abr/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "05" Then fechafinal = Mid(valorfecha, 1, 3) & "may/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "06" Then fechafinal = Mid(valorfecha, 1, 3) & "jun/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "07" Then fechafinal = Mid(valorfecha, 1, 3) & "jul/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "08" Then fechafinal = Mid(valorfecha, 1, 3) & "ago/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "09" Then fechafinal = Mid(valorfecha, 1, 3) & "sep/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "10" Then fechafinal = Mid(valorfecha, 1, 3) & "oct/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "11" Then fechafinal = Mid(valorfecha, 1, 3) & "nov/" & Mid(valorfecha, 7, 4)
                    'If Mid(valorfecha, 4, 2) = "12" Then fechafinal = Mid(valorfecha, 1, 3) & "dic/" & Mid(valorfecha, 7, 4)
                    Dim det2 As New PdfPCell(New Phrase(fechafinal, fontNormal))
                    det2.Border = 0
                    det2.HorizontalAlignment = Element.ALIGN_LEFT
                    tabla.AddCell(det2)
                    If origen = "CON" Or origen = "ONE" Then
                        Dim det5 As New PdfPCell(New Phrase("    ", fontNormal))
                        det5.Border = 0
                        det5.HorizontalAlignment = Element.ALIGN_RIGHT
                        tabla.AddCell(det5)
                    Else
                        'Dim det3 As New PdfPCell(New Phrase("$" & dtconsulta2.Tables(0).Rows(i)("PRECIO_PRODUCTO").ToString(), fontNormal))
                        'det3.Border = 0
                        'det3.HorizontalAlignment = Element.ALIGN_RIGHT
                        'tabla.AddCell(det3)
                    End If

                    Dim det4 As New PdfPCell(New Phrase(" ", fontNormal))
                    det4.Border = 0
                    det4.HorizontalAlignment = Element.ALIGN_RIGHT
                    tabla.AddCell(det4)
                Next
            End If
            documento.Add(tabla)
            documento.Add(lineablanco)
            'documento.Add(lineablanco)
            'Dim linea2 As New iTextSharp.text.Paragraph(espacio & "Estos sistemas se encuentran instalados en el (Vehículo/Barco/Avión/Cajero) con las siguientes características:", fontNormal)
            'documento.Add(linea2)

            Dim textoParrafo2 As New PdfPTable(1)
            textoParrafo2.SetWidths(New Single() {200.0F})

            Dim tituloText41 As New PdfPCell(New Phrase("*La vigencia del servicio se contará desde la instalación del equipo.", fontNormal))
            tituloText41.Border = 0
            textoParrafo2.AddCell(tituloText41)
            textoParrafo2.AddCell(codigoblan)
            textoParrafo2.AddCell(codigoblan)
            Dim tituloText4 As New PdfPCell(New Phrase("En el vehículo con las siguientes características:", fontNormal))
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

            Dim textoParrafo21 As New PdfPTable(1)
            textoParrafo21.SetWidths(New Single() {240.0F})
            Dim tituloText42 As New PdfPCell(New Phrase("El presente certificado, no constituye confirmación de instalación del equipo.", fontNormal))
            tituloText42.Border = 0
            tituloText42.HorizontalAlignment = Element.ALIGN_CENTER
            textoParrafo21.AddCell(tituloText42)
            documento.Add(textoParrafo21)


            'documento.Add(lineablanco)
            documento.Add(lineablanco)
            ' Imprime el codigo de barra
            Dim codbarra As iTextSharp.text.Image = iTextSharp.text.Image.GetInstance(Server.MapPath(Session("nombrebarra")))
            codbarra.ScalePercent(75.0F)
            ' CODIGO QR
            'documento.Add(codbarra)
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
            'documento.Add(lineablanco)
            Dim textoParrafo22 As New PdfPTable(1)
            textoParrafo22.SetWidths(New Single() {240.0F})
            Dim tituloText43 As New PdfPCell(New Phrase("Los términos y condiciones del servicio pueden ser consultadas en nuestra página web", fontNormal))
            tituloText43.Border = 0
            tituloText43.HorizontalAlignment = Element.ALIGN_LEFT
            textoParrafo22.AddCell(tituloText43)
            documento.Add(textoParrafo22)
            documento.Add(lineablanco)
            ' Fecha de emisión del pdf
            Dim lblfechahoraemision As String
            lblfechahoraemision = "Emitido el " & System.DateTime.Now.ToString("dd MMMM yyyy H:mm:ss")
            lblfechahoraemision = espacio & lblfechahoraemision
            Dim fecha As New iTextSharp.text.Paragraph(lblfechahoraemision, fontNormal)
            documento.Add(fecha)
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
            'dtconsulta = obj.ConsultarImprimir(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text, Session("user_id"), "C")
            'dtconsulta = Session("Detalle")
            dtconsulta = Session("producto")
            Session("infoqr") = dtconsulta
            Dim dtOsqr As New DataSet
            dtOsqr = Session("infoqr")
            'datOs = dtOsqr.Tables(0).Rows(0)("NUMERO_GENERAL").ToString()
            datOs = "01092024"
            Dim horaActual As DateTime = TimeOfDay
            'datVehiculo = dtOsqr.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
            'datCliente = dtOsqr.Tables(0).Rows(0)("ID_CLIENTE").ToString()
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
                name += ".jpg"
            Loop While File.Exists(folderPath & name)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
        Return name
    End Function

#End Region



End Class