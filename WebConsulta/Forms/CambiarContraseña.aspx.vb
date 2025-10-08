Imports System

Public Class CambiarContraseña
     Inherits System.Web.UI.Page
    Dim usuario As String
    Dim password As String
    Dim passwordNew As String
    Dim usuarioId As Integer
    Dim usuarioDesc As String

    ''' <summary>
    ''' Autor: GALVAR
    ''' Motivo: control de sesion
    ''' Fecha: 16/10/2019  
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Control_Sesion()
        Try
            If (Session("user_session") Is Nothing) Then
                Response.Redirect("404.aspx")
                Session("error") = "Debe de iniciar sesión en el sistema"
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Autor: GALVAR
    ''' Motivo: PAGE DE LOAD
    ''' Fecha: 16/10/2019  
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session.Item("user_session") IsNot Nothing Then
                Control_Sesion()
                usuarioId = Session("user_id")
                usuarioDesc = CType(Session.Item("user_session"), String)
                Me.txt_usuario.Text = usuarioDesc
                Me.txt_password_last.Text = ""
                Me.txt_password_new.Text = ""
                'Me.txt_password_last.setfocus()
                'Datos_Log_Ingreso()
                If Session("pass_expire") IsNot Nothing Then
                    Me.lbl_msg_caduca.Text = Session("pass_expire")
                End If
            Else
                Response.Redirect("404.aspx")
                Session("error") = "Debe de iniciar sesión en el sistema"
            End If
        End If
    End Sub


    ''' <summary>
    ''' Autor: GALVAR
    ''' Motivo: log de ingreso
    ''' Fecha: 16/10/2019  
    ''' </summary>
    ''' <remarks></remarks>
    'Private Sub Datos_Log_Ingreso()
    '    Try
    '        Dim formularioIdBack As Integer
    '        If Session("form_session_id") Is Nothing Then
    '            formularioIdBack = 0
    '        Else
    '            formularioIdBack = Session("form_session_id")
    '        End If
    '        Dim usuarioIp As String = Request.ServerVariables("REMOTE_ADDR")
    '        Dim usuarioId As String = Session("user_id")
    '        Dim formularioUrl2 As String = System.IO.Path.GetFileNameWithoutExtension(Request.PhysicalPath)
    '        Dim obj As New LogAdministracion
    '        Dim dtInfoFormulario As New System.Data.DataSet
    '        dtInfoFormulario = obj.Consulta_Formulario(formularioUrl2)
    '        Dim formularioId As Integer = dtInfoFormulario.Tables("INFO_FORMULARIO").Rows(0)("ID").ToString()
    '        Session("form_session_id") = formularioId
    '        Session("ref_id") = Registra_Permanencia2(usuarioId, usuarioIp, formularioId, Session("ref_id"), formularioIdBack, Session("ref_id_back"))
    '        Session("ref_id_back") = Session("ref_id")
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub


    ''' <summary>
    ''' Autor: GALVAR
    ''' Motivo: boton de cancelar
    ''' Fecha: 16/10/2019  
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs) Handles BtnCancelar.Click
        Try
            'Datos_Log_Ingreso()
            'Session.Clear()
            'Session.Abandon()
            'Response.Redirect("Login.aspx", False)
            Me.txt_password_last.Text = ""
            Me.txt_password_new.Text = ""
            Me.lbl_msg_login.Text = ""
            'Me.RequiredFieldValidator2.Enabled = False
            'Me.RequiredFieldValidator3.Enabled = False
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Autor: GALVAR
    ''' Motivo: boton de cambiar
    ''' Fecha: 16/10/2019  
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub BtnCambiar_Click(sender As Object, e As System.EventArgs) Handles BtnCambiar.Click
        Try
            If txt_password_last.Text = "" Then
                Me.lbl_msg_login.Text = "Debe ingresar el ultimo password"
            ElseIf txt_password_new.Text = "" Then
                Me.lbl_msg_login.Text = "Debe ingresar el nuevo password"
            ElseIf Len(txt_password_last.Text) < 6 Then
                Me.lbl_msg_login.Text = "Debe ingresar mas de 6 caracteres para su password anterior"
            ElseIf Len(txt_password_new.Text) < 6 Then
                Me.lbl_msg_login.Text = "Debe ingresar mas de 6 caracteres para su nuevo password"
            Else
                Dim obj As New PasswordAdminitracion
                Dim dtLoginUsuario As New System.Data.DataSet
                usuario = ""
                password = ""
                passwordNew = ""
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
                    Me.BtnCambiar.Visible = False
                    'Me.RequiredFieldValidator2.Enabled = False
                    'Me.RequiredFieldValidator3.Enabled = False
                    '  Datos_Log_Ingreso()
                    Session.Clear()
                    Session.Abandon()
                    Response.Redirect("Login.aspx", False)
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub



End Class