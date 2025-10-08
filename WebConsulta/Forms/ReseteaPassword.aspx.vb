'Imports Microsoft.VisualBasic
Imports System
Imports System.Net
Imports System.Net.Mail
'Imports System.Collections.Generic
'Imports System.Web.UI
'Imports System.Xml
'Imports System.Xml.XPath
'Imports System.Collections
'Imports System.Data
'Imports System.Web.UI.WebControls
'Imports System.Drawing
'Imports System.Configuration
'Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI.WebControls.WebParts
'Imports System.Web.UI.HtmlControls
'Imports System.Threading
'Imports Libreria

Public Class ReseteaPassword
    Inherits System.Web.UI.Page

    Dim emailRegistado As String
    Dim emailNuevo As String
    Dim clave As String
    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: load de la pagina
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Datos_Log_Ingreso()
                Call Inhabilitar(False)
                Me.btn_regresar.Visible = False
                Me.btn_regresar.Enabled = False
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: Permite recuperar informacion respecto al usuario
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Recuperar_Datos()
        Try
            Call Inhabilitar(False)
            Me.txt_email_nuevo.Text = ""
            Me.lbl_msg_login.Text = ""
            Dim usuario As String = Me.txt_usuario.Text
            Dim obj2 As New PasswordAdminitracion
            Dim dtValidaUsuario As New System.Data.DataSet
            dtValidaUsuario = obj2.Verificar_Usuario(1, usuario)
            Dim msg2 As String = dtValidaUsuario.Tables("VAL_USER").Rows(0)("MSG").ToString()
            If msg2 = "UNR" Then
                Me.lbl_msg_login.Text = "El usuario no está registrado en el sistema"
            Else
                Session("clave") = msg2
                Dim obj As New PasswordAdminitracion
                Dim dtRecuperarPassword As New System.Data.DataSet
                dtRecuperarPassword = obj.Retornar_Password(2, usuario)
                Dim msg As String = dtRecuperarPassword.Tables("RECOVER_PASS").Rows(0)("MSG").ToString()
                If msg = "UNE" Then
                    Me.lbl_msg_login.Text = "El usuario no existe"
                End If
                If msg = "NRE" Then
                    Me.lbl_email_nuevo.Visible = True
                    Me.txt_email_nuevo.Visible = True
                    Me.rfv_usuario02.Enabled = True
                    Me.btn_validar.Enabled = True
                End If
                If msg <> "UNE" And msg <> "NRE" Then
                    Me.lbl_email_registrado.Visible = True
                    Me.lbl_email_registrado2.Visible = True
                    Me.lbl_email_registrado2.Enabled = True
                    Me.lbl_email_registrado2.Text = msg
                    Me.btn_validar.Enabled = True
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: Permite recuperar datos de ingreso
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_email_Click(sender As Object, e As EventArgs) Handles btn_email.Click
        Try
            Recuperar_Datos()
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: Permite inhabilitar objetos
    ''' </summary>
    ''' <param name="valor"></param>
    ''' <remarks></remarks>
    Protected Sub Inhabilitar(ByVal valor As Boolean)
        Try
            Me.lbl_email_nuevo.Visible = valor
            Me.txt_email_nuevo.Visible = valor
            Me.rfv_usuario02.Enabled = valor
            Me.lbl_email_registrado.Visible = valor
            Me.lbl_email_registrado2.Visible = valor
            Me.btn_validar.Enabled = valor
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: valida la recuperacion de datos para envio de mail y direcciona al login
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub btn_validar_Click(sender As Object, e As EventArgs) Handles btn_validar.Click
        Try
            If Envio_Email() Then
                Me.btn_regresar.Visible = True
                Me.btn_regresar.Enabled = True
                Me.btn_validar.Visible = False
                Me.btn_regresar.Enabled = True
                Me.rfv_usuario.Enabled = False
                Me.rfv_usuario02.Enabled = False
                'Response.Redirect("loginAdministracion.aspx", False)
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: funcion que establece el reeteo de datos
    ''' </summary>
    ''' <param name="password"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function ResetearPwd(ByVal password As String) As Boolean
        ResetearPwd = False
        Try
            Dim obj As New PasswordAdminitracion
            Dim dtLoginUsuario As New System.Data.DataSet
            Dim usuario As String = Me.txt_usuario.Text
            dtLoginUsuario = obj.Resetear_Password(usuario, password)
            If dtLoginUsuario.Tables(0).Rows.Count > 0 Then
                Dim msg As String = dtLoginUsuario.Tables("PASS").Rows(0)("MSG").ToString()
                If msg = "UNE" Then
                    'Me.lbl_msg_login.Text = "El usuario no existe en el sistema"
                    ResetearPwd = False
                End If
                If msg = "PMS" Then
                    ResetearPwd = True
                    'Else
                    '    ResetearPwd = True
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: envio de mail
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Function Envio_Email() As Boolean
        Envio_Email = False
        Try
            Dim password As String = "123"
            If ResetearPwd(password) Then
                Dim emailRegistrado = ""
                Dim emailNuevo = ""
                Dim emailDestino As String
                emailRegistrado = Me.lbl_email_registrado2.Text
                emailNuevo = Me.txt_email_nuevo.Text
                If emailRegistrado <> "" Then
                    emailDestino = emailRegistrado
                Else
                    emailDestino = emailNuevo
                End If
                'Dim emailOrigen As String = "s3taller@carsegsa.com"
                Dim emailOrigen As String = ConfigurationManager.AppSettings.Get("SmptCliente").ToString()
                Dim correo As New System.Net.Mail.MailMessage()
                correo.From = New System.Net.Mail.MailAddress(emailOrigen)
                correo.To.Add(emailDestino)
                correo.Subject = "Recuperación de Contraseña"
                correo.Body = "Estimado usuario su clave ha sido reestablecida : " _
                    & " ' " & password _
                    & " ' " & vbCrLf & vbCrLf & "Fecha y hora GMT: " & DateTime.Now.ToUniversalTime.ToString("dd/MM/yyyy HH:mm:ss")
                correo.IsBodyHtml = False
                correo.Priority = System.Net.Mail.MailPriority.Normal
                'Dim smtp As New System.Net.Mail.SmtpClient
                '---------------------------------------------
                ' Estos datos debes rellanarlos correctamente
                '---------------------------------------------
                'smtp.Host = "10.100.89.34"
                'smtp.Credentials = New System.Net.NetworkCredential("jcoloma", "12345")
                'smtp.EnableSsl = False
                'nuevo
                ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
                Dim smtp As New SmtpClient(ConfigurationManager.AppSettings.Get("SmptCliente").ToString(), ConfigurationManager.AppSettings.Get("SmptPort"))
                smtp.EnableSsl = True
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network
                smtp.UseDefaultCredentials = False
                smtp.Credentials = New Net.NetworkCredential(ConfigurationManager.AppSettings.Get("VentasMailUser").ToString(), ConfigurationManager.AppSettings.Get("VentasMailPassword").ToString())
                'smtp.Send(correo)
                Try
                    smtp.Send(correo)
                    Me.lbl_msg_login.Text = "Mensaje enviado satisfactoriamente"
                    Envio_Email = True
                Catch ex As Exception
                    lbl_msg_login.Text = "ERROR: " & ex.Message
                End Try
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Function


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: retorna al login
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Protected Sub btn_regresar_Click(sender As Object, e As EventArgs) Handles btn_regresar.Click
        Try
            Session.Clear()
            Session.Abandon()
            Response.Redirect("login.aspx", False)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
     End Sub

    ''' <summary>
    ''' Motivo: registro de log
    ''' Fecha: 29/06/2012
    ''' Autor: Maritza Narea
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


End Class