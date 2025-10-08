Public Class ConsultaWin32Reporte
    Inherits System.Web.UI.Page

    Dim vehiculo As Int32
    Dim os As Int32
    Dim username As String
    Dim contParmBlank As Integer = 0
    Dim accionHabilita As Boolean

    ''' <summary>
    ''' FECHA: 23/13/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: EVENTO LOAD FORMULARIO CERTIFICADO WIN32 SYSHUNTER
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If ValidaParametros() = True Then
                    Me.lblvehiculowin32.Text = vehiculo
                    lbloswin32.Text = os
                    Consultar(vehiculo, "jcolom", os)
                    Me.lblfechahoraemision.Text = "Emitido el " & System.DateTime.Now.ToString("dd MMMM yyyy H:mm:ss")
                Else
                    Me.lbloswin32.Text = "NO DATA"
                    'Response.Redirect("http://www.hunter.com.ec", False)
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary> 
    ''' Fecha: 23/12/2013
    ''' Autor: Galo Alvarado
    ''' COMENTARIO: MÉTODO PARA EL CONTROL, VISUALIZACIÓN Y ENVÍO DE ERRORES VÍA EMAIL
    ''' </summary>
    ''' <param name="tipo"></param>
    ''' <remarks></remarks>
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try
    '        Email.Enviar_Email(tipo)
    '    Catch ex As Exception
    '        Captura_Error(ex)
    '    End Try
    'End Sub

    ''' <summary>
    ''' FECHA: 23/12/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: FUNCIÓN PARA VERIFICAR VALORES NULOS Y ESPACIOS EN BLANCOS
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
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
    ''' FECHA: 23/12/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: FUNCIÓN PARA VERIFICAR QUE TODOS LOS PARÁMETROS ESTÉN CORRECTOS EN EL QUERYSTRING
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ValidaParametros() As Boolean
        Try
            If IsNullOrBlank(Page.Request.QueryString("os")) = True Then
                contParmBlank += 1
            Else
                os = Page.Request.QueryString("os")
            End If
            If IsNullOrBlank(Page.Request.QueryString("veh")) = True Then
                contParmBlank += 1
            Else
                vehiculo = Page.Request.QueryString("veh")
            End If
            If IsNullOrBlank(Page.Request.QueryString("user")) = True Then
                contParmBlank += 1
            Else
                username = Page.Request.QueryString("user")
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


    ''' <summary>
    ''' FECHA: 23/12/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: MÉTODO PARA REALIZAR LA CONSULTA Y CARGAR LOS DATOS DEL CERTIFICADO DESDE EL SYSHUNTER
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Consultar(ByVal vehiculo As Int32, ByVal username As String, ByVal os As Int32)
        Try
            'Dim obj2 As New AdministracionLogin
            'Dim dt_login_usuario As New System.Data.DataSet
            Session("Detalle") = Nothing
            Session("dtResultado") = Nothing
            Dim obj As New ConsultaWeb
            Dim dtConsulta As New System.Data.DataSet
            dtConsulta = CType(Session("Consulta_Datos"), DataSet)
            'dt_consulta = obj.Consulta_Datos(Me.txtCodVehiculo.Text, "", "", Me.txtChasis.Text, "", Me.TxtMotor.Text, usuarioOficina)
            'obj.Registro_Actividad(Me.txtCodVehiculo.Text, Me.txtChasis.Text, Me.TxtMotor.Text, usuarioOficina, "N", ipmaquina, pantalla)
            dtConsulta = obj.Consulta_Datos(vehiculo, "", "", "", "", "", 1212)
            'obj.Registro_Actividad(vehiculo, "", "", 1212, "N", "10.100.107.217", "win32", "WIN32")
            If dtConsulta.Tables(0).Rows.Count > 0 Then
                ''Inhabilito el botón de impresión de certificados si este campo viene con 0
                'accion_habilita = dt_consulta.Tables(0).Rows(0)("CAMB_PROP_ESTADO").ToString()
                'var_rei = dt_consulta.Tables(0).Rows(0)("ESTADO_REI").ToString()
                'If var_rei = True Then
                'accion_habilita = 0
                'End If
                'Dim dt_Resultado As New System.Data.DataSet
                'dt_Resultado = obj.Consulta_Mensaje(Me.txtCodVehiculo.Text, usuarioOficina, txtConPlaca.Text, TxtMotor.Text, txtChasis.Text)
                'Session("dtResultado") = dt_Resultado
                'If dt_Resultado.Tables(0).Rows.Count > 0 Then
                '    rgdResultado.DataSource = Session("dtResultado")
                '    rgdResultado.DataBind()
                '    rntResultado.Title = "Mensaje de la Aplicación Consulta Certificados"
                '    rntResultado.TitleIcon = "info"
                '    rntResultado.ContentIcon = "info"
                '    rntResultado.ShowSound = "info"
                '    rntResultado.Width = 380
                '    rntResultado.Height = 250
                '    rntResultado.Show()
                '    accion_habilita = 0
                'End If
                ' botones("Consultar")
                'If accion_habilita <> 0 Then
                'Me.txtCodVehiculo.Text = dt_consulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
                'Me.txtChasis.Text = dt_consulta.Tables(0).Rows(0)("CHASIS").ToString()
                'Me.TxtMotor.Text = dt_consulta.Tables(0).Rows(0)("MOTOR").ToString()
                'Me.txtConConcesionario.Text = dt_consulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
                'Me.TxtConCliente.Text = dt_consulta.Tables(0).Rows(0)("CLIENTE").ToString()
                'Me.txtConCodCliente.Text = dt_consulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
                'Me.txtConMarca.Text = dt_consulta.Tables(0).Rows(0)("MARCA").ToString()
                'Me.txtConModelo.Text = dt_consulta.Tables(0).Rows(0)("MODELO").ToString()
                'Me.txtConPlaca.Text = dt_consulta.Tables(0).Rows(0)("PLACA").ToString()
                'Me.txtConTipo.Text = dt_consulta.Tables(0).Rows(0)("TIPO").ToString()
                'Me.txtConColor.Text = dt_consulta.Tables(0).Rows(0)("COLOR").ToString()
                Dim dtVehiculo As New System.Data.DataSet
                'dt_vehiculo = obj.ConsultaCodigo(Me.txtCodVehiculo.Text, Me.txtConCodCliente.Text)
                dtVehiculo = obj.ConsultaCodigo(vehiculo, "", "C", "")
                If dtVehiculo.Tables(0).Rows.Count > 0 Then
                    Session("Detalle") = dtVehiculo
                    'RadGridVehiculo.DataSource = dt_vehiculo.Tables(0)
                    'RadGridVehiculo.MasterTableView.DataKeyNames = New String() {"CODIGO_VEHICULO"}
                    'RadGridVehiculo.Height = 150
                    'Me.btnImprimir.Enabled = True
                    'Me.grdproductosdetalle.DataSource = Session("Detalle")
                    'grdproductosdetalle.DataBind()
                    'Else
                    'RadGridVehiculo.DataSource = CType(Session("Consulta_InicialVehiculo"), DataSet).Tables(0)
                    'Me.btnImprimir.Enabled = False
                    'accion_habilita = 0
                    'End If
                    Detalle()
                    'End If
                    'If Session("user_master") = "S" Then
                    '    Me.txtCodVehiculo.Text = dt_consulta.Tables(0).Rows(0)("ID_VEHICULO").ToString()
                    '    Me.txtChasis.Text = dt_consulta.Tables(0).Rows(0)("CHASIS").ToString()
                    '    Me.TxtMotor.Text = dt_consulta.Tables(0).Rows(0)("MOTOR").ToString()
                    '    Me.txtConConcesionario.Text = dt_consulta.Tables(0).Rows(0)("CONCESIONARIO").ToString()
                    '    Me.TxtConCliente.Text = dt_consulta.Tables(0).Rows(0)("CLIENTE").ToString()
                    '    Me.txtConCodCliente.Text = dt_consulta.Tables(0).Rows(0)("ID_CLIENTE").ToString()
                    '    Me.txtConMarca.Text = dt_consulta.Tables(0).Rows(0)("MARCA").ToString()
                    '    Me.txtConModelo.Text = dt_consulta.Tables(0).Rows(0)("MODELO").ToString()
                    '    Me.txtConPlaca.Text = dt_consulta.Tables(0).Rows(0)("PLACA").ToString()
                    '    Me.txtConTipo.Text = dt_consulta.Tables(0).Rows(0)("TIPO").ToString()
                    '    Me.txtConColor.Text = dt_consulta.Tables(0).Rows(0)("COLOR").ToString()
                End If
                'RadGridVehiculo.DataBind()
                'InhabilitaCertificado(accion_habilita)
            Else
                Throw New Exception("No Existen Datos que Presentar, Por Verificar")
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
    Private Sub Detalle()
        Try
            Dim obj As New ConsultaWeb
            'Dim dtDetalleFlujo2 As New System.Data.DataSet
            'dtDetalleFlujo2 = obj.ConsultaDetalleFormaPago(empresa, proceso, Session("rpt_flj_cja_det_frm_pago_anio"))
            Dim dtdetalle As New System.Data.DataSet
            Dim dtconsulta As New System.Data.DataSet
            'grd_cabecera_presupuesto.DataSource = Nothing
            dtdetalle = Session("Detalle")
            'If dtdetalle.Tables(0).Rows.Count > 0 Then
            'Me.Vehiculo.Text = dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
            dtconsulta = obj.ConsultarImprimir(dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtdetalle.Tables(0).Rows(0)("CLIENTE").ToString(), Session("user_id"), "C")
            'dtconsulta = obj.ConsultarImprimir(vehiculo, 0, 1212)
            Session("infoqr") = dtconsulta
            If dtconsulta.Tables(0).Rows.Count > 0 Then
                Me.lblnombrewin23.Text = dtconsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                Me.lblvehiculowin32.Text = dtconsulta.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
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
            End If
            'End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub



End Class