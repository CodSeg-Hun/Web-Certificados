Public Class ActualizarContraseña
    Inherits System.Web.UI.Page

    Dim usuario As String
    Dim password As String
    Dim passwordNew As String
    Dim usuarioId As Integer
    Dim usuarioDesc As String

    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: control de sesion
    ''' Fecha: 14/06/2012  
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Control_Sesion()
        Try
            'If (Session("user") Is Nothing) Then  
            'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
            If (Session("user_session") Is Nothing) Then
                Response.Redirect("404.aspx")
                Session("error") = "Debe de iniciar sesión en el sistema"
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try

    End Sub


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: Load Modificar password
    ''' Fecha: 14/06/2012 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                'If Session.Item("user") IsNot Nothing Then  
                'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
                If Session.Item("user_session") IsNot Nothing Then
                    usuarioId = Session("user_id")
                    'usuario_desc = CType(Session.Item("user"), String)
                    'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
                    usuarioDesc = CType(Session.Item("user_session"), String)
                    Me.txt_usuario.Text = usuarioDesc
                    Datos_Log_Ingreso()
                    Me.BtnRegresar.Visible = False
                    Me.BtnRegresar.Enabled = False
                    If Session("pass_expire") IsNot Nothing Then
                        Me.lbl_msg_caduca.Text = Session("pass_expire")
                    End If
                Else
                    Response.Redirect("404.aspx")
                    Session("error") = "Debe de iniciar sesión en el sistema"
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: Captura error
    ''' Fecha: 14/06/2012 
    ''' </summary>
    ''' <param name="tipo"></param>
    ''' <remarks></remarks>
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try
    '        'RnMensajesError.Text = tipo.ToString
    '        'RnMensajesError.Title = "Error en Aplicación 'Reportes Gerenciales'"
    '        'RnMensajesError.Show()
    '        Email.usuario_ip = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString
    '        'Email.usuario_sesion = Session("user").ToString
    '        'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
    '        Email.usuario_sesion = Session("user_session").ToString
    '        Email.Enviar_Email(tipo)
    '    Catch ex As Exception
    '        Throw
    '    End Try
    'End Sub


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: Valida contrasenia
    ''' Fecha: 14/06/2012 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnValidar_Click(sender As Object, e As EventArgs) Handles BtnValidar.Click
        Try
            Dim obj As New PasswordAdminitracion
            Dim dtLoginUsuario As New System.Data.DataSet
            usuario = ""
            password = ""
            usuario = Me.txt_usuario.Text
            password = Me.txt_password_last.Text
            passwordNew = Me.txt_password_new.Text
            dtLoginUsuario = obj.Modificar_Password(usuario, password, passwordNew)
            Dim msg As String = dtLoginUsuario.Tables("PASS").Rows(0)("MSG").ToString()
            If msg = "UNE" Then
                Me.lbl_msg_login.Text = "El usuario no existe en el sistema"
            End If
            If msg = "PNE" Then
                Me.lbl_msg_login.Text = "La contraseña anterior es incorrecta"
                Me.txt_password_last.Focus()
            End If
            If msg = "PIG" Then
                Me.lbl_msg_login.Text = "La contraseña nueva no puede ser igual a la anterior"
                txt_password_new.Focus()
            End If
            If msg = "PMS" Then
                Me.lbl_msg_login.Text = "La contraseña ha sido modificada"
                Me.txt_password_last.Text = ""
                Me.txt_password_new.Text = ""
                Me.BtnRegresar.Visible = True
                Me.BtnRegresar.Enabled = True
                Me.BtnValidar.Visible = False
                'Me.BtnRegresar.Enabled = True
                Me.RequiredFieldValidator2.Enabled = False
                Me.RequiredFieldValidator3.Enabled = False
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: regresa a login
    ''' Fecha: 14/06/2012  
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnRegresar_Click(sender As Object, e As EventArgs) Handles BtnRegresar.Click
        Try
            Datos_Log_Ingreso()
            Session.Clear()
            Session.Abandon()
            Response.Redirect("Login.aspx", False)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: Log de ingreso
    ''' Fecha: 14/06/2012 
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


    ''' <summary>
    ''' Autor: FONTANEDA
    ''' Motivo: Se añade boton de cancelara para regresar al login
    ''' Fecha: 07/08/2012
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles BtnCancelar.Click
        Try
            Datos_Log_Ingreso()
            Session.Clear()
            Session.Abandon()
            Response.Redirect("Login.aspx", False)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

End Class