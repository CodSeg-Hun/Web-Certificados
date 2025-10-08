'Imports Microsoft.VisualBasic
Imports System
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
'Imports System.Net

Public Module Email

    Public UsuarioSesion As String
    Public UsuarioIp As String

    Public Sub Enviar_Email(ByVal tipo As Exception, ByVal email As String)
        Try
            'Dim direccion As String = "jcoloma@carsegsa.com"
            'Dim direcciones As String() = direccion.Split(",")
            'Dim TextoBody As String = "Usuario Conectado:" & usuario_sesion & Chr(13) & "IP: " & usuario_ip & Chr(13) & tipo.StackTrace.ToString & Chr(13) & tipo.Message.ToString
            'Dim SmptCliente As String = "10.100.89.4"
            'Dim EmailEnvia As String = "s3taller@carsegsa.com"
            'Dim Motivo As String = "Envío de correos desde dll de libreria"
            'Formulario.EnvioMail(EmailEnvia, direcciones, "Mensaje Aplicación Certificados Web", False, Net.Mail.MailPriority.High, "", TextoBody, Nothing, SmptCliente)
            ExceptionHandler.MailException(tipo, email)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Module
