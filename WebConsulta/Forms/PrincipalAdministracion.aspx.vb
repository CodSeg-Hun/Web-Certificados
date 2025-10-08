Public Class Principal
    Inherits System.Web.UI.Page

    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Response.AddHeader("X-UA-Compatible", "IE=8")
        MyBase.OnPreInit(e)
    End Sub

    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: MÉTODO PARA LA CAPTURA DE EXCEPCIONES Y ENVÍO VÍA EMAIL
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'PARA DESHABILITAR EL "VOLVER ATRÁS"
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            Me.Form.Attributes.Add("onLoad", "disableBackButton();")
            Datos_Log_Ingreso()
            Control_Sesion()
        Catch ex As Exception
            'Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    'PROCEDIMIENTO PARA VALIDAR EL USUARIO DE SESIÓN
    Private Sub Control_Sesion()
        Try
            'If (Session("user") Is Nothing) Then 
            'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
            If (Session("user_session") Is Nothing) Then
                Session("alert") = "Debe de iniciar sesión en el sistema"
                Response.Redirect("404.aspx")
            End If
        Catch ex As Exception
            'Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 26/07/2012
    ''' COMENTARIO: MÉTODO PARA REGISTRAR EL LOG DE AUDITORÍA
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Datos_Log_Ingreso()
        Try
            Dim formularioIdBack As Integer
            If Session("form_session_id") Is Nothing Then
                formularioIdBack = 0
            Else
                formularioIdBack = Session("form_session_id")
            End If
            Dim usuarioIp As String = Request.ServerVariables("REMOTE_ADDR")
            Dim usuarioId As String = Session("user_id")
            'Dim usuario_login As String = Session("user")
            'Dim formulario_url As String = System.IO.Path.GetFileName(Request.PhysicalPath)
            Dim formularioUrl2 As String = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath)
            Dim obj As New Log
            Dim dtInfoFormulario As New System.Data.DataSet
            dtInfoFormulario = obj.Consulta_Formulario(formularioUrl2)
            Dim formularioId As Integer = dtInfoFormulario.Tables(0).Rows(0)("ID").ToString()
            Session("form_session_id") = formularioId
            'Registra_permanencia(usuario_ip, usuario_id, formulario_id_back, formulario_id, Session("user"))
            Session("ref_id") = Registra_Permanencia2(usuarioId, usuarioIp, formularioId, Session("ref_id"), formularioIdBack, Session("ref_id_back"))
            Session("ref_id_back") = Session("ref_id")
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


End Class