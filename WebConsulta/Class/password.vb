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
Imports System.Security.Cryptography


Public Class lib_password

    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: MÉTODO PARA CAPTURAR EXCEPCIONES Y ENVÍARLOS VÍA EMAIL
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
    ''' COMENTARIO: FUNCIÓN PARA VERIFICAR EL ESTADO DEL USUARIO EN EL SISTEMA
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Verificar_Usuario(ByVal opcion As String, ByVal usuario As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            'Dim obj As New lib_password
            base.AddParrameter("@OPCION", opcion)
            base.AddParrameter("@USUARIO", usuario)
            'ds = base.Consulta("Intranet.RPG_GER_VALIDA_USUARIO")
            ds = base.Consulta("Intranet.RPG_GER_VALIDA_USUARIO2")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: FUNCIÓN PARA REALIZAR LA MODIFICACIÓN DE LA CONTRASEÑA POR LA NUEVA
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="password"></param>
    ''' <param name="password_new"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Modificar_Password(ByVal usuario As String, ByVal password As String, ByVal passwordNew As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            Dim obj As New lib_password
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@PASSWORD", obj.Encriptar3s(password))
            base.AddParrameter("@PASSWORD_NEW", obj.Encriptar3s(passwordNew))
            'ds = base.Consulta("Intranet.RPG_GER_MODIFICAR_PASSWORD")
            ds = base.Consulta("Intranet.RPG_GER_MODIFICAR_PASSWORD2")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: FUNCIÓN PARA RETORNAR LA CONTRASEÑA NUEVA VÍA EMAIL
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Retornar_Password(ByVal opcion As String, ByVal usuario As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            'Dim obj As New lib_password
            base.AddParrameter("@OPCION", 2)
            base.AddParrameter("@USUARIO", usuario)
            'ds = base.Consulta("Intranet.RPG_GER_OBTENER_EMAIL_DOMINIO")
            ds = base.Consulta("Intranet.RPG_GER_VALIDA_USUARIO2")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: FELIX ONTANEDA
    ''' FECHA: 14/06/2012
    ''' COMENTARIO: FUNCIÓN PARA ENCRIPTAR LA CONTRASEÑA QUE SE ENVÍA A MODIFICAR
    ''' </summary>
    ''' <param name="Password"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Encriptar3S(ByVal password As String) As Byte()
        Encriptar3S = Nothing
        Dim objSha256 As New SHA256Managed
        Dim objTemporal As Byte()
        Try
            objTemporal = System.Text.Encoding.UTF8.GetBytes(password)
            objTemporal = objSha256.ComputeHash(objTemporal)
            Return objTemporal
        Catch ex As Exception
            Throw ex
        Finally
            objSha256.Clear()
        End Try
    End Function


    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 23/07/2012
    ''' COMENTARIO: FUNCIÓN PARA RESETEAR LA CONTRASEÑA DEL USUARIO QUE HAYA PASADO LA FECHA DE CADUCIDA DE PASSWORD
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="password_new"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Resetear_Password(ByVal usuario As String, ByVal passwordNew As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            'Dim obj As New lib_password
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@PASSWORD_NEW", Encriptar3S(passwordNew))
            'ds = base.Consulta("Intranet.RPG_GER_RESETEAR_PASSWORD")
            ds = base.Consulta("Intranet.RPG_GER_RESETEAR_PASSWORD2")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
