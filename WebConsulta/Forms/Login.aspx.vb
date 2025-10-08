'Imports Microsoft.VisualBasic
'Imports System
'Imports System.Collections.Generic
'Imports System.Web.UI
'Imports System.Xml
'Imports System.Xml.XPath
'Imports System.Collections
'Imports System.Data
'Imports System.Web.UI.WebControls
Imports System.Drawing
'Imports System.Configuration
'Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI.WebControls.WebParts
'Imports System.Web.UI.HtmlControls
'Imports System.Threading
'Imports Libreria

Public Class Login
    Inherits System.Web.UI.Page

    Dim usuario As String
    Dim password As String
    Dim urlpage As String
    Dim valueIphost, valuePchost, cadenaitems As String

    ''' <summary>
    ''' Motivo: Load de login
    ''' Fecha: 28/06/2012
    ''' Autor: compatibilidad en explorador
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub login_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        Try
            Response.AddHeader("X-UA-Compatible", "IE=8")
            Response.AddHeader("X-UA-Compatible", "IE=9")
            MyBase.OnPreInit(e)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 25/11/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: MÉTODO PARA OBTENER LA IP Y HOSTNAME DEL EQUIPO VISITANTE
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InfoUsuario()
        Try
            valueIphost = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If valueIphost = "" Or valueIphost Is Nothing Then
                valueIphost = Request.ServerVariables("REMOTE_ADDR")
            End If
            Dim computerName As String() = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).HostName.Split(New [Char]() {"."c})
            'Dim ecn As [String] = System.Environment.MachineName
            valuePchost = computerName(0).ToString()
            'Me.lbl_iphost2.Text = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_addr")).ToString
            'Me.lbl_iphost2.Text = System.Net.Dns.GetHostEntry(Request.ServerVariables("remote_host")).HostName
            Me.lbl_iphost2.Text = HttpContext.Current.Request.UserHostAddress
            Session("iphost") = valueIphost
            Session("pchost") = valuePchost
            Me.lbl_ip.Text = valueIphost & " / " & valueIphost
        Catch ex As Exception
            'Enviar_Email(ex, CType(Application("usuario_email"), String))
            Me.lbl_ip.Text = "Error en detección IP"
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: Load de login
    ''' Fecha: 28/06/2012
    ''' Autor: Maritza Narea
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim valueCfr01 As String
            'valueCfr01 = HttpUtility.UrlEncode(EncryptQueryString(String.Format("0601106776")))
            'Dim strDat As String = ""
            'Dim strClv As String = ""
            'If Request.QueryString("prmdtt") <> Nothing Then
            '    strClv = Request.RawUrl
            '    strDat = Request.QueryString("prmdtt").ToString
            '    Session("strDat") = DecryptQueryString(strDat)
            'End If
            Me.txt_Usuario.Focus()
            urlpage = ""
            urlpage = System.IO.Path.GetFileName(Request.PhysicalPath)
            Me.lbl_ip.Visible = False
            Me.lbl_iphost2.Visible = False
            If Not IsPostBack Then
                InfoUsuario()
                If Session("alert") IsNot Nothing Then
                    lbl_msg_login.Text = Session("alert")
                    Me.lbl_msg_login.ForeColor = Color.Red
                    Me.lbl_msg_login.Font.Bold = True
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

    'Private Function DecryptQueryString(strQueryString As String) As String
    '    Dim qs As String = ""
    '    Try
    '        Dim obt As New GeneraDataCphr
    '        qs = obt.Descifrar(strQueryString, "r0b1nr0y")
    '    Catch ex As Exception
    '        'ExceptionHandler.Captura_Error(ex)
    '    End Try
    '    Return qs
    'End Function

    'Public Function EncryptQueryString(ByVal strQueryString As String) As String
    '    Dim cifrado As String = String.Empty
    '    Dim obt As New GeneraDataCphr
    '    Try
    '        cifrado = obt.Cifrar(strQueryString, "r0b1nr0y")
    '    Catch ex As Exception
    '        'Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    '    Return cifrado
    'End Function

    ''' <summary>
    ''' Motivo: Metodo de inicio de sesion
    ''' Fecha: 28/06/2012
    ''' Autor: Maritza Narea
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub BtnLogin_Click(sender As Object, e As System.EventArgs) Handles BtnLogin.Click
        Try
            Dim obj As New AdministracionLogin
            Dim dtLoginUsuario As New System.Data.DataSet
            'Dim dtLoginIp As New System.Data.DataSet
            usuario = ""
            password = ""
            usuario = Me.txt_Usuario.Text
            password = Me.txt_password.Text
            dtLoginUsuario = obj.Login_Usuario(usuario, password)
            Dim msg As String = dtLoginUsuario.Tables("LOGIN").Rows(0)("MSG").ToString()
            'Dim master As String = dtLoginUsuario.Tables("LOGIN").Rows(0)("USUARIO_MASTER").ToString()
            'dtLoginIp = obj.Login_Usuario_Ip(usuario)
            'Dim ipDatos As String = dtLoginIp.Tables("IP").Rows(0)("IP").ToString()
            'If master <> "S" Then
            '    If ip_datos = Session("iphost") Then
            '        If msg = "UNE" Then
            '            Me.lbl_msg_login.Text = "El usuario no existe en el sistema"
            '        End If
            '        If msg = "UNA" Then
            '            Me.lbl_msg_login.Text = "El usuario no se encuentra en estado ACTIVO"
            '            Me.txt_Usuario.Focus()
            '        End If
            '        If msg = "PIN" Then
            '            Me.lbl_msg_login.Text = "La contraseña es incorrecta"
            '            Me.txt_password.Focus()
            '        End If
            '        If msg = "PCD" Then
            '            Dim usuario_id As Integer = dt_login_usuario.Tables("LOGIN").Rows(0)("USUARIO_ID").ToString()
            '            Session("user_id") = usuario_id
            '            'Session("user") = Me.txt_Usuario.Text
            '            'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
            '            Session("user_session") = Me.txt_Usuario.Text
            '            Session("pass_expire") = "Su contraseña ha caducado"
            '            Response.Redirect("CambiarContraseña.aspx", False)
            '        End If
            '        If msg = "PCR" Then
            '            Dim usuario_id As Integer = dt_login_usuario.Tables("LOGIN").Rows(0)("USUARIO_ID").ToString()
            '            Session("user_id") = usuario_id
            '            ' Session("user") = Me.txt_Usuario.Text
            '            'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
            '            Session("user_session") = Me.txt_Usuario.Text
            '            Session("user_master") = dt_login_usuario.Tables("LOGIN").Rows(0)("USUARIO_MASTER").ToString()
            '            'Datos_Log_Ingreso()
            '            Response.Redirect("PrincipalAdministracion.aspx", False)
            '        End If
            '    Else
            '        Me.lbl_msg_login.Text = "La ip registrada es incorrecta"
            '    End If
            'Else
            If msg = "UNE" Then
                Me.lbl_msg_login.Text = "El usuario no existe en el sistema"
            End If
            If msg = "UNA" Then
                Me.lbl_msg_login.Text = "El usuario no se encuentra en estado ACTIVO"
                Me.txt_Usuario.Focus()
            End If
            If msg = "PIN" Then
                Me.lbl_msg_login.Text = "La contraseña es incorrecta"
                Me.txt_password.Focus()
            End If
            If msg = "ROL" Then
                Me.lbl_msg_login.Text = "No tiene configurado el ROL de acceso"
                Me.txt_password.Focus()
            End If
            If msg = "PCD" Then
                Dim usuarioId As Integer = dtLoginUsuario.Tables("LOGIN").Rows(0)("USUARIO_ID").ToString()
                Session("user_id") = usuarioId
                'Session("user") = Me.txt_Usuario.Text
                'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
                Session("user_session") = Me.txt_Usuario.Text
                Session("pass_expire") = "Su contraseña ha caducado"
                Response.Redirect("ActualizarContraseña.aspx", False)
            End If
            If msg = "PCR" Then
                Dim usuarioId As Integer = dtLoginUsuario.Tables("LOGIN").Rows(0)("USUARIO_ID").ToString()
                Session("user_id") = usuarioId
                ' Session("user") = Me.txt_Usuario.Text
                'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
                Session("user_session") = UCase(Me.txt_Usuario.Text)
                Session("user_master") = dtLoginUsuario.Tables("LOGIN").Rows(0)("USUARIO_MASTER").ToString()
                Session("user_certificado") = dtLoginUsuario.Tables("LOGIN").Rows(0)("USUARIO_CERTIFICADO").ToString()
                'Datos_Log_Ingreso()
                Response.Redirect("PrincipalAdministracion.aspx", False)
            End If
            'End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: Metodo de log de ingreso
    ''' Fecha: 28/06/2012
    ''' Autor: Maritza Narea
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Datos_Log_Ingreso()
        Try
            'Dim formularioIdBack As Integer
            ''Dim permanencia As New LogAdministracion
            'If Session("form_session_id") Is Nothing Then
            '    formularioIdBack = 0
            'Else
            '    formularioIdBack = Session("form_session_id")
            'End If
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
            Session("ref_id") = Registra_Permanencia2(usuarioId, usuarioIp, formularioId, Session("ref_id"), Nothing, Session("ref_id_back"))
            Session("ref_id_back") = Session("ref_id")
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


End Class