Imports System.Data
'Imports System.Configuration
'Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports System.Web.UI.WebControls.WebParts
'Imports System.Web.UI.HtmlControls
'Imports System.Data.SqlClient
'Imports Libreria

Public Class AdministracionLogin
    '----at
    'Dim conn As New SqlClient.SqlConnection("Data Source=superman,41430;Initial Catalog=reportes; User ID=syshunter_rpt; Password=qpzm7913")
    'Dim conn_intranet As New SqlClient.SqlConnection("Data Source=superman,41430;Initial Catalog=intranet; User ID=intranet; Password=qpzm7931")
    'Dim ds As New DataSet
    'Dim da As New SqlClient.SqlDataAdapter
    'Dim cmd As New SqlClient.SqlCommand
    'Dim DataEnvio As String


    'OBJETIVO:     Manejar los errores que se presenten y enviarlos vía email
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try

    '        Email.Enviar_Email(tipo)
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    Catch ex As Exception
    '        Captura_Error(ex)
    '    End Try

    'End Sub

    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: FUNCIÓN PARA OBTENER LOS DATOS DEL USUARIO Y SU ID
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="password"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Login_Usuario(ByVal usuario As String, ByVal password As String) As DataSet
        Login_Usuario = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim obj As New PasswordAdminitracion
            Dim ds As New DataSet
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@PASSWORD", obj.Encriptar3s(password))
            ds = base.Consulta("Intranet.RPG_GER_LOGIN_BACK3")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "LOGIN"
            End If
            Login_Usuario = ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function Login_Usuario_Ip(ByVal usuario As String) As DataSet
        Login_Usuario_Ip = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            'Dim obj As New PasswordAdminitracion
            Dim ds As New DataSet
            base.AddParrameter("@USUARIO", usuario)
            ds = base.Consulta("Intranet.RPG_GER_LOGIN_IP")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "IP"
            End If
            Login_Usuario_Ip = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: Permite obtener usuarios de dominio
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Datos_Login_Usuario(ByVal usuario As String) As DataSet
        Datos_Login_Usuario = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            base.AddParrameter("@samaccountname", usuario)
            'ds = base.Consulta("Intranet.SP_USUARIOS_DOMINIO")
            ds = base.Consulta("Intranet.SP_USUARIOS_DOMINIO2")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "DATOS_USUARIO_LOGIN"
            End If
            Datos_Login_Usuario = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: Permite obtener perfil de usuario
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Datos_Perfil_Usuario(ByVal usuario As String, ByVal opcion As Integer) As DataSet
        Datos_Perfil_Usuario = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            base.AddParrameter("@NOMBRE_USUARIO", usuario)
            base.AddParrameter("@OPCION", opcion)
            ds = base.Consulta("Intranet.SP_CONSULTA_USUARIO_PERFIL")
            'ds = base.Consulta("Intranet.SP_CONSULTA_USUARIO_PERFIL_NEW") '--> linea para pruebas
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "DATOS_USUARIO_PERFIL"
            End If
            Datos_Perfil_Usuario = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' FECHA: 28/06/2012
    ''' AUTOR: MNAREA
    ''' MOTIVO: Permite obtener items de menu
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Datos_Menu(ByVal opcion As Integer) As DataSet
        Datos_Menu = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            'FOntaneda: 08/08/2012 - Se antepone el envio del parametro Opcion ya que esta provocando errores
            base.AddParrameter("@OPCION", opcion)
            ds = base.Consulta("Intranet.SP_CONSULTA_ITEMS_MENU")
            ' base.AddParrameter("@OPCION", opcion)
            'ds = base.Consulta("Intranet.SP_CONSULTA_ITEMS_MENU_NEW") '--> LINEA PARA PRUEBAS
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "DATOS_MENU"
            End If
            Datos_Menu = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 04/09/2012
    ''' </summary>
    ''' <param name="proceso"></param>
    ''' <param name="empresa"></param>
    ''' <param name="usuario_sh"></param>
    ''' <param name="usuario_id"></param>
    ''' <param name="tipo_opcion_menu"></param>
    ''' <param name="tipo_menu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Datos_Menu2(ByVal proceso As Integer, ByVal empresa As Integer, ByVal usuarioSh As String, ByVal usuarioId As Integer, ByVal tipoOpcionMenu As String, ByVal tipoMenu As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@PROCESO", proceso)
            base.AddParrameter("@EMPRESA_ID", empresa)
            base.AddParrameter("@USUARIO_SH", usuarioSh)
            base.AddParrameter("@USUARIO_ID", usuarioId)
            base.AddParrameter("@TIPO_OPCION_MENU", tipoOpcionMenu)
            base.AddParrameter("@TIPO_MENU", tipoMenu)
            ds = base.Consulta("Intranet.sp_interfase_3s_web_habilita")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 04/09/2012
    ''' COMENTARIO: FUNCIÓN PARA EL LLENADO DEL MENÚ PREVIO A HABILITAR LAS OPCIONES
    ''' </summary>
    ''' <param name="proceso"></param>
    ''' <param name="empresa"></param>
    ''' <param name="usuario_sh"></param>
    ''' <param name="usuario_id"></param>
    ''' <param name="tipo_opcion_menu"></param>
    ''' <param name="tipo_menu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Datos_Menu3(ByVal tipoMenu As String, ByVal usuario As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@TIPO_MENU", tipoMenu)
            base.AddParrameter("@USUARIO_SH", usuario)
            ds = base.Consulta("Intranet.sp_interfase_3s_web_llenado")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
