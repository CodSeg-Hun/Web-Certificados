Imports System
Imports System.Net
'Imports System.Net
'Imports System.Net.NetworkInformation
Imports System.Net.Mail
Imports System.Web
'Imports Libreria

Public Class ExceptionHandler


    Public Shared Sub MailException(ByVal exception As Exception, ByVal email As String)
        Try
            Dim mailMessage As New MailMessage()
            'Dim mailAddress As New MailAddress(ConfigurationManager.AppSettings.Get("ErrorMailAddress").ToString())
            Dim mailAddress As New MailAddress("s3taller@carsegsa.com")
            mailMessage.From = mailAddress
            mailMessage.IsBodyHtml = True
            Dim mailToCollection As [String]() = email.ToString().Split(",")
            For Each mailTo As String In mailToCollection
                mailMessage.To.Add(mailTo)
            Next
            mailMessage.Subject = "Mensaje Web Consulta"
            mailMessage.Body = MailBody(exception)
            mailMessage.Priority = MailPriority.High
            'Dim smtpClient As New SmtpClient(ConfigurationManager.AppSettings.Get("SmptCliente").ToString())
            'Dim smtpClient As New SmtpClient("10.100.89.34")
            'nuevo
            ServicePointManager.SecurityProtocol = CType(3072, SecurityProtocolType)
            Dim smtp As New SmtpClient(ConfigurationManager.AppSettings.Get("SmptCliente").ToString(), ConfigurationManager.AppSettings.Get("SmptPort"))
            smtp.EnableSsl = True
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network
            smtp.UseDefaultCredentials = False
            smtp.Credentials = New Net.NetworkCredential(ConfigurationManager.AppSettings.Get("VentasMailUser").ToString(), ConfigurationManager.AppSettings.Get("VentasMailPassword").ToString())
            smtp.Send(mailMessage)
            'SmtpClient.Send(mailMessage)
            mailMessage.Dispose()
        Catch ex As Exception
        End Try
    End Sub


    Private Shared Function MailBody(ByVal exception As Exception) As String
        Dim builder As New StringBuilder()
        Try
            Dim ipMaquina As String = String.Empty
            'If Not HttpContext.Current.Session("iphost") Is Nothing Then ipMaquina = HttpContext.Current.Session("iphost").ToString()
            Dim usuario As String = String.Empty
            ' Dim titulo As String = String.Empty
            If Not HttpContext.Current.Session("user_session") Is Nothing Then
                usuario = HttpContext.Current.Session("user_session").ToString()
                ipMaquina = HttpContext.Current.Session("iphost").ToString()
            End If
            builder.Append("<html><head><title>Error en ")
            builder.Append(exception.Source).Append("</title><meta http-equiv=""Content-Type""")
            builder.Append("content=""text/html"";charset=""iso-8859-1""><style type=""text/css"">")
            builder.Append("<!--.basix {font-family: Verdana, Arial, Helvetica, sans-serif;font-size: 12px;")
            builder.Append("}.header1 {font-family: Verdana, Arial, Helvetica, sans-serif;")
            builder.Append("font-size: 12px; font-weight: bold;color: #000099;}.tlbbkground1")
            builder.Append(" {background-color: #000099;}--></style></head><body>")
            builder.Append("<table width=""85%"" border=""0"" align=""center"" cellpadding = ""5""")
            builder.Append("cellspacing=""1"" class=""tlbbkground1""><tr bgcolor=""#eeeeee"">")
            builder.Append("<td colspan=""2"" class=""header1""><div align=""center"">")
            builder.Append("Error en Sistema : '").Append("Sección Web Consulta").Append("'</div></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>Fecha y Hora</td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(DateTime.Now.ToString("dd/MMM/yyyy HH:mm:ss"))
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>Usuario Red</td>")
            'builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(My.User.Name)
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(usuario)
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>Dirección IP</td>")
            'builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(ipMaquina.Split(New Char() {"*"c})(0))
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(ipMaquina)
            builder.Append("</td></tr> ")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>PC </td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(Environment.MachineName)
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>Source</td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(exception.Source)
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>Clase</td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(exception.TargetSite.DeclaringType)
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>Método</td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(exception.TargetSite.Name)
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append("nowrap>Línea</td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(DirectCast(exception.TargetSite.CallingConvention, Integer))
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>Message</td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(exception.Message)
            builder.Append("</td></tr>")
            builder.Append("<tr><td width=""100"" align=""right"" bgcolor=""#eeeeee""class=""header1""")
            builder.Append(" nowrap>StackTrace</td>")
            builder.Append("<td bgcolor=""#FFFFFF"" class=""basix"">").Append(exception.StackTrace)
            builder.Append("</td></tr>")
            builder.Append("</table></body></html>")
        Catch exc As Exception
            Throw exc
        End Try
        Return builder.ToString()
    End Function


End Class
