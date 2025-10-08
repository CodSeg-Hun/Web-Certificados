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

Public Class Login

    '----at
    'Dim conn_intranet As New SqlClient.SqlConnection("Data Source=superman,41430;Initial Catalog=intranet; User ID=intranet; Password=qpzm7931")

    'Dim ds As New DataSet
    'Dim da As New SqlClient.SqlDataAdapter
    'Dim cmd As New SqlClient.SqlCommand
    ' Dim DataEnvio As String

    ''OBJETIVO:     Manejar los errores que se presenten y enviarlos vía email
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try

    '        Email.Enviar_Email(tipo)

    '    Catch ex As Exception
    '        Captura_Error(ex)
    '    End Try

    'End Sub
    ''' <summary>
    '''AUTOR:       MNAREA
    '''FECHA:       29/06/2012
    '''OBJETIVO:    Regitra activiad de formulario
    ''' </summary>
    ''' <param name="ip"></param>
    ''' <param name="id_usuario"></param>
    ''' <param name="id_formulario_back"></param>
    ''' <param name="id_formulario"></param>
    ''' <param name="desc_usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Registro_Actividad_Formulario(ByVal ip As String, ByVal idUsuario As Integer, ByVal idFormularioBack As Integer, ByVal idFormulario As Integer, ByVal descUsuario As String) As DataSet
        Registro_Actividad_Formulario = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            base.AddParrameter("@IP", ip)
            base.AddParrameter("@ID_USUARIO", idUsuario)
            base.AddParrameter("@ID_FORMULARIO_BACK", idFormularioBack)
            base.AddParrameter("@ID_FORMULARIO", idFormulario)
            base.AddParrameter("@DESC_USUARIO", descUsuario)
            ds = base.Consulta("Intranet.SP_USUARIO_INGRESO_NEW")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "ACTIVIDAD_FORMULARIO"
            End If
            Registro_Actividad_Formulario = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Function Consulta_Formulario(ByVal formulario As String) As DataSet
        Consulta_Formulario = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            base.AddParrameter("@FORMULARIO", formulario)
            base.AddParrameter("@TIPO_MENU", "C")
            ds = base.Consulta("Intranet.SP_CONSULTA_ID_FORMULARIO_WEB")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "INFO_FORMULARIO"
            End If
            Consulta_Formulario = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
