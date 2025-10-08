Imports System
'Imports System.Web.UI.WebControls
'Imports Telerik.Web.UI

Public Class Administracion
    Inherits System.Web.UI.MasterPage

    Dim usuarioSistema As String
    Dim usuarioId As Integer

    ''' <summary>
    ''' Motivo: Load de login
    ''' Fecha: 28/06/2012
    ''' Autor: metodo de log de ingreso
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
            'Dim usuarioLogin As String = Session("user_session")
            'Dim formularioUrl As String = System.IO.Path.GetFileName(Request.PhysicalPath)
            Dim formularioId As Integer = 14
            Session("form_session_id") = formularioId
            'Registra_permanencia(usuario_ip, usuario_id, formulario_id_back, formulario_id, Session("user"))
            Session("ref_id") = Registra_Permanencia2(usuarioId, usuarioIp, formularioId, Session("ref_id"), formularioIdBack, Session("ref_id_back"))
            Session("ref_id_back") = Session("ref_id")
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Sub Page_Init(sender As Object, e As System.EventArgs) Handles Me.Init
        'Control_Sesion()
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
            If Not IsPostBack Then
                'Session("user") = "jcolom"
                'Session("user_id") = 1212
                'Session("user_session") = "atambo"
                'Session("user_id") = 889
                'If Session.Item("user") IsNot Nothing Then  
                'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
                If Session.Item("user_session") IsNot Nothing Then
                    usuarioId = Session("user_id")
                    'usuario_sistema = CType(Session.Item("user"), String)
                    'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
                    usuarioSistema = CType(Session.Item("user_session"), String)
                    Dim obj As New AdministracionLogin
                    Dim datosUsuarioLogin As New System.Data.DataSet
                    datosUsuarioLogin = obj.Datos_Login_Usuario(usuarioSistema)
                    If datosUsuarioLogin.Tables(0).Rows.Count > 0 Then
                        Dim nombre As String = datosUsuarioLogin.Tables(0).Rows(0)("displayname").ToString()
                        'Dim url_foto As String = datosUsuarioLogin.Tables(0).Rows(0)("foto").ToString()
                        Me.lbl_usuario_login.Text = nombre
                        Me.lbl_usuario_canal.Text = datosUsuarioLogin.Tables(0).Rows(0)("canal").ToString()
                        Session("canales") = datosUsuarioLogin.Tables(0).Rows(0)("canales").ToString()
                        ''Me.img_usuario_login.ImageUrl = url_foto
                        Llena_Menu()
                        Habilita_Menu(usuarioSistema)
                    End If
                Else
                    Session("error") = "Debe de iniciar sesión en el sistema"
                    Response.Redirect("404.aspx", False)
                End If
            Else
                'Dim mastermenu As RadMenu = DirectCast(Master.FindControl("radmenu1"), RadMenu)
                'usuario_sistema = CType(Session.Item("user"), String)
                'FOntaneda: 08/08/2012 - Se cambia la variable de sesion user por user_session para evitar cruze de datos entre sitio gerencial y administrativo
                usuarioSistema = CType(Session.Item("user_session"), String)
                Llena_Menu()
                Habilita_Menu(usuarioSistema)
                Me.RadMenu1.Visible = True
                Me.RadMenu1.Enabled = True
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: salir del sitio
    ''' Fecha: 28/06/2012
    ''' Autor: Maritza Narea 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub BntSalir_Click(sender As Object, e As EventArgs) Handles BntSalir.Click
        Try
            'Datos_Log_Ingreso()
            Session.Clear()
            Session.Abandon()
            Response.Redirect("Login.aspx", False)
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' Motivo: Permite habilitar menu
    ''' Fecha: 28/06/2012
    ''' Autor: Maritza Narea
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <remarks></remarks>
    Protected Sub Habilita_Menu(ByVal usuario As String)
        Try
            Dim itemsMenu As New DataSet
            Dim objeto As New AdministracionLogin
            ''''''''''''''''''''''
            'INICIA CODIGO PARA SUPERMAN
            ''''''''''''''''''''''
            'items_menu = objeto.datos_perfil_usuario(usuario, 2)
            'For i As Integer = 0 To items_menu.Tables(0).Rows.Count - 1
            '    Dim valor As Integer = items_menu.Tables(0).Rows(i).Item(2)
            '    For j As Integer = 0 To RadMenu1.Items.Count - 1
            '        '
            '        If RadMenu1.Items(j).Value = valor Then
            '            RadMenu1.Items(j).Enabled = True
            '        End If
            '        For k As Integer = 0 To RadMenu1.Items(j).Items.Count - 1
            '            '
            '            If RadMenu1.Items(j).Items(k).Value = valor Then
            '                RadMenu1.Items(j).Items(k).Enabled = True
            '            End If
            '        Next
            '    Next
            'Next
            ''''''''''''''''''''''
            'FINALIZA CODIGO PARA SUPERMAN
            ''''''''''''''''''''''
            ''''''''''''''''''''''
            'INICIA CODIGO PARA BANSHEE
            ''''''''''''''''''''''
            Dim proceso, empresaId, usuarioId As Integer
            Dim usuarioSh, tipoOpcionMenu, tipoMenu As String
            proceso = 1
            empresaId = 1
            usuarioSh = Session("user_session")
            usuarioId = Session("user_id")
            tipoOpcionMenu = "web"
            tipoMenu = "C"
            'itemsMenu = objeto.Datos_Menu2(proceso, empresaId, usuarioSh, usuarioId, tipoOpcionMenu, tipoMenu)
            itemsMenu = objeto.Datos_Menu3(tipoMenu, usuarioSh)
            For i As Integer = 0 To itemsMenu.Tables(0).Rows.Count - 1
                Dim valor As Integer = itemsMenu.Tables(0).Rows(i).Item(2)
                For j As Integer = 0 To RadMenu1.Items.Count - 1
                    If RadMenu1.Items(j).Value = valor Then
                        RadMenu1.Items(j).Enabled = True
                    End If
                    For k As Integer = 0 To RadMenu1.Items(j).Items.Count - 1
                        If RadMenu1.Items(j).Items(k).Value = valor Then
                            'If (usuarioSh = "LATAUT") And valor = 953001 Then
                            '    'If (usuarioSh = "LATAUT" Or usuarioSh = "AMAFLA") And valor = 953001 Then
                            '    RadMenu1.Items(j).Items(k).Enabled = False
                            '    RadMenu1.Items(j).Items(k).Visible = False
                            'Else
                            '    If (usuarioSh = "LATAUT" Or usuarioSh = "AMAFLA") And valor = 953002 Then
                            '        RadMenu1.Items(j).Items(k).Enabled = True
                            '    ElseIf usuarioSh = "GALVAR" And valor = 953002 Then
                            '        RadMenu1.Items(j).Items(k).Enabled = True
                            '    ElseIf valor = 953002 Then
                            '        RadMenu1.Items(j).Items(k).Enabled = False
                            '        RadMenu1.Items(j).Items(k).Visible = False
                            '    Else
                            '        RadMenu1.Items(j).Items(k).Enabled = True
                            '    End If
                            'End If
                            RadMenu1.Items(j).Items(k).Enabled = True
                        End If
                    Next
                Next
            Next
            ''''''''''''''''''''''
            'FINALIZA CODIGO PARA BANSHEE
            ''''''''''''''''''''''
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    ''' <summary>
    ''' FONTANEDA: MENU DINAMICO
    ''' 14/06/2012
    ''' </summary>
    ''' <remarks></remarks>
    Protected Sub Llena_Menu()
        Try
            Dim itemsMenu As New DataSet
            Dim objeto As New AdministracionLogin
            ''''''''''''''''''''''
            'INICIA CODIGO PARA SUPERMAN
            ''''''''''''''''''''''
            'items_menu = objeto.datos_menu(2)
            'RadMenu1.DataSource = items_menu.Tables(0)
            'RadMenu1.DataFieldID = "ID"
            'RadMenu1.DataFieldParentID = "PARENTID"
            'RadMenu1.DataTextField = "TEXT"
            'RadMenu1.DataNavigateUrlField = "URL"
            'RadMenu1.DataValueField = "ID"
            'RadMenu1.DataBind()
            'For i As Integer = 0 To items_menu.Tables(0).Rows.Count - 1
            '    For j As Integer = 0 To RadMenu1.Items.Count - 1
            '        RadMenu1.Items(j).Enabled = False
            '        For k As Integer = 0 To RadMenu1.Items(j).Items.Count - 1
            '            RadMenu1.Items(j).Items(k).Enabled = False
            '        Next
            '    Next
            'Next
            ''''''''''''''''''''''
            'FINALIZA CODIGO PARA SUPERMAN
            ''''''''''''''''''''''
            ''''''''''''''''''''''
            'INICIA CODIGO PARA BANSHEE
            ''''''''''''''''''''''
            Dim tipoMenu As String
            Dim usuarioSh As String
            usuarioSh = Session("user_session")
            tipoMenu = "C"
            itemsMenu = objeto.Datos_Menu3(tipoMenu, usuarioSh)
            RadMenu1.DataSource = itemsMenu.Tables(0)
            RadMenu1.DataFieldID = "CODIGO_MENU"
            RadMenu1.DataFieldParentID = "CODIGO_PADRE"
            RadMenu1.DataTextField = "CAPTION_FORMA"
            RadMenu1.DataNavigateUrlField = "ASSEMBLY"
            RadMenu1.DataValueField = "CODIGO_MENU"
            RadMenu1.DataBind()
            For i As Integer = 0 To itemsMenu.Tables(0).Rows.Count - 1
                For j As Integer = 0 To RadMenu1.Items.Count - 1
                    RadMenu1.Items(j).Enabled = False
                    For k As Integer = 0 To RadMenu1.Items(j).Items.Count - 1
                        RadMenu1.Items(j).Items(k).Enabled = False
                    Next
                Next
            Next
            ''''''''''''''''''''''
            'FINALIZA CODIGO PARA BANSHEE
            ''''''''''''''''''''''
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


End Class