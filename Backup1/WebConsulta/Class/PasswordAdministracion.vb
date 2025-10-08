Imports System.Data
'Imports System.Configuration
'Imports System.Web
'Imports System.Web.Security
'Imports System.Web.UI
'Imports System.Web.UI.WebControls
'Imports System.Web.UI.WebControls.WebParts
'Imports System.Web.UI.HtmlControls
'Imports System.Data.SqlClient
Imports Libreria
Imports System.Security.Cryptography


Public Class PasswordAdminitracion

    '----at
    ''OBJETIVO:     Manejar los errores que se presenten y enviarlos vía email
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try

    '        Email.Enviar_Email(tipo)

    '    Catch ex As Exception
    '        Captura_Error(ex)
    '    End Try

    'End Sub
    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: Verifica ususario
    ''' Fecha: 14/06/2012
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Verificar_Usuario(ByVal opcion As String, ByVal usuario As String) As DataSet
        Verificar_Usuario = Nothing
        Dim base As New DataBaseApp.CommandApp
        Dim ds As New DataSet
        Try
            base.AddParrameter("@OPCION", opcion)
            base.AddParrameter("@USUARIO", usuario)
            'ds = base.Consulta("Intranet.RPG_GER_VALIDA_USUARIO")
            ds = base.Consulta("Intranet.RPG_GER_VALIDA_USUARIO2")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "VAL_USER"
            End If
            Verificar_Usuario = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: Modificar password
    ''' Fecha: 14/06/2012 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="password"></param>
    ''' <param name="password_new"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Modificar_Password(ByVal usuario As String, ByVal password As String, ByVal passwordNew As String) As DataSet
        Modificar_Password = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@PASSWORD", Formulario.Encriptar3s(password))
            base.AddParrameter("@PASSWORD_NEW", Formulario.Encriptar3s(passwordNew))
            'ds = base.Consulta("Intranet.RPG_GER_MODIFICAR_PASSWORD")
            ds = base.Consulta("Intranet.RPG_GER_MODIFICAR_PASSWORD2")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "PASS"
            End If
            Modificar_Password = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo: Retornar password
    ''' Fecha: 14/06/2012  
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Retornar_Password(ByVal opcion As String, ByVal usuario As String) As DataSet
        Retornar_Password = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            base.AddParrameter("@OPCION", 2)
            base.AddParrameter("@USUARIO", usuario)
            'ds = base.Consulta("Intranet.RPG_GER_OBTENER_EMAIL_DOMINIO")
            ds = base.Consulta("Intranet.RPG_GER_VALIDA_USUARIO2")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "RECOVER_PASS"
            End If
            Retornar_Password = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Autor: MNAREA
    ''' Motivo ENCRIPTAR CONTRASENIA METODO 3S
    ''' Fecha: 14/06/2012
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
    ''' Autor: MNAREA
    ''' Motivo Resetear password
    ''' Fecha: 14/06/2012 
    ''' </summary>
    ''' <param name="usuario"></param>
    ''' <param name="password_new"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Resetear_Password(ByVal usuario As String, ByVal passwordNew As String) As DataSet
        Resetear_Password = Nothing
        Try
            Dim base As New DataBaseApp.CommandApp
            Dim ds As New DataSet
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@PASSWORD_NEW", Formulario.Encriptar3s(passwordNew))
            'ds = base.Consulta("Intranet.RPG_GER_RESETEAR_PASSWORD")
            ds = base.Consulta("Intranet.RPG_GER_RESETEAR_PASSWORD2")
            If ds.Tables.Count > 0 Then
                ds.Tables(0).TableName = "PASS"
            End If
            Resetear_Password = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
