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

'''AUTOR:       JCOLOMA
'''FECHA:       30/04/2012
'''OBJETIVO:    ADMINISTRA LAS FUNCIONES DE CONEXIÓN E INGRESO DE DATOS PARA EL LOG DE USUARIOS

Public Class Log

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

    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: FUNCIÓN PARA REGISTRAR LA AUDITORÍA DE LOS USUARIOS
    ''' </summary>
    ''' <param name="ip"></param>
    ''' <param name="id_usuario"></param>
    ''' <param name="id_formulario_back"></param>
    ''' <param name="id_formulario"></param>
    ''' <param name="desc_usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Registro_Actividad_Formulario(ByVal ip As String, ByVal idUsuario As Integer, ByVal idFormularioBack As Integer, ByVal idFormulario As Integer, ByVal descUsuario As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@IP", ip)
            base.AddParrameter("@ID_USUARIO", idUsuario)
            base.AddParrameter("@ID_FORMULARIO_BACK", idFormularioBack)
            base.AddParrameter("@ID_FORMULARIO", idFormulario)
            base.AddParrameter("@DESC_USUARIO", descUsuario)
            ds = base.Consulta("Intranet.SP_USUARIO_INGRESO_NEW")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: FUNCIÓN PARA REGISTRAR LA AUDITORÍA DE LOS USUARIOS
    ''' </summary>
    ''' <param name="ip"></param>
    ''' <param name="id_usuario"></param>
    ''' <param name="id_formulario_back"></param>
    ''' <param name="id_formulario"></param>
    ''' <param name="desc_usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Registro_Actividad_Formulario2(ByVal usuarioIngreso As Integer, ByVal ip As String, ByVal idMenu As Integer, _
                                            ByVal refMenu As Integer, ByVal idMenuBack As Integer, ByVal refIdBack As Integer) As Int64
        'Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Dim cnn As SqlClient.SqlConnection = Nothing
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim tran As SqlClient.SqlTransaction = Nothing
        Try
            cmd = New SqlClient.SqlCommand
            cnn = base.Connection
            cnn.Open()
            base.ProcedureName = "Intranet.SP_USUARIO_INGRESO_NEW"
            cmd.Connection = cnn
            tran = cnn.BeginTransaction("INGRESO")
            base.AddParrameter("@USUARIO_INGRESO", usuarioIngreso)
            base.AddParrameter("@IP", ip)
            base.AddParrameter("@ID_MENU", idMenu)
            base.AddParrameter("@REF_ID", refMenu)
            base.AddParrameter("@ID_MENU_BACK", idMenuBack)
            base.AddParrameter("@REF_ID_BACK", refIdBack)
            base.EjecutaTransaction(cmd, tran)
            Dim lastId As Long = Convert.ToInt64(cmd.Parameters("@REF_ID").Value)
            tran.Commit()
            Return lastId
        Catch ex As Exception
            tran.Rollback()
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: MÉTODO PARA OBTENER LA INFORMACIÓN DEL FORMULARIO AL CUAL SE INGRESA
    ''' </summary>
    ''' <param name="formulario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Consulta_Formulario(ByVal formulario As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@FORMULARIO", formulario)
            base.AddParrameter("@TIPO_MENU", "C")
            ds = base.Consulta("Intranet.SP_CONSULTA_ID_FORMULARIO_WEB")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
