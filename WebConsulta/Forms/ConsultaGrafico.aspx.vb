Public Class ConsultaGrafico
    Inherits System.Web.UI.Page

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
                '    usuario = Session("user_id")
                '    ipmaquina = Request.ServerVariables("REMOTE_ADDR")
                '    pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
                InicializarControles()
                'Else
                '    Me.RadListView1.DataSource = Session("General")
                '    Me.RadListView1.DataBind()
            End If

            'usuario = Session("user_id")
            'ipmaquina = Request.ServerVariables("REMOTE_ADDR")
            'pantalla = System.IO.Path.GetFileName(Request.PhysicalPath)
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
            cmb_conceanio.DataSource = datosanio
            cmb_conceanio.DataValueField = "CODIGO"
            cmb_conceanio.DataTextField = "DESCRIPCION"
            cmb_conceanio.DataBind()
            ' Cargar mes
            Dim datosmes As New System.Data.DataSet
            datosmes = ConcesionariosAdapter.CargarDatos(3)
            cmb_mesinicial.DataSource = datosmes
            cmb_mesinicial.DataValueField = "CODIGO"
            cmb_mesinicial.DataTextField = "DESCRIPCION"
            cmb_mesinicial.DataBind()

            cmb_mesfinal.DataSource = datosmes
            cmb_mesfinal.DataValueField = "CODIGO"
            cmb_mesfinal.DataTextField = "DESCRIPCION"
            cmb_mesfinal.DataBind()
            LimpiarDatos()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub



    Private Sub LimpiarDatos()
        Try
            'Session("General") = Nothing
            'Session("CLIENTE") = Nothing
            'Session("VEHICULO") = Nothing
            Me.cmb_canal.SelectedValue = "C"
            Me.cmb_conceanio.SelectedValue = Now.Year
            Me.cmb_mesinicial.SelectedValue = Now.Month
            Me.cmb_mesfinal.SelectedValue = Now.Month
            Me.txtcanalID.Text = ""
            Me.txtcanal.Text = ""
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
                    Me.BtnImprimir.Enabled = False
                    Me.BtnCorreo.Enabled = False
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

#End Region

End Class