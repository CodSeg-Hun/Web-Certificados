'Imports System.Web.SessionState

Public Class Global_asax
    Inherits System.Web.HttpApplication
  
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al iniciar la aplicación
        Application("usuario_email") = "fontaneda@carsegsa.com,galvarado@carsegsa.com"
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al iniciar la sesión
        Dim dtConsulta As New System.Data.DataSet
        Session("Consulta_Inicial") = dtConsulta

        Dim dtVehiculo As New System.Data.DataSet
        Session("Consulta_InicialVehiculo") = dtVehiculo

        Dim dtConsultaDatos As New System.Data.DataSet
        Session("Consulta_Datos") = dtConsultaDatos

    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al comienzo de cada solicitud
        Dim ci As New System.Globalization.CultureInfo("es-ES")
        ci.NumberFormat.CurrencyDecimalSeparator = "."
        ci.NumberFormat.CurrencyGroupSeparator = ","

        ci.NumberFormat.NumberDecimalSeparator = "."
        ci.NumberFormat.NumberGroupSeparator = ","

        ci.NumberFormat.PercentDecimalSeparator = "."
        ci.NumberFormat.PercentGroupSeparator = ","

        ci.DateTimeFormat.DateSeparator = "/"
        ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"
        System.Threading.Thread.CurrentThread.CurrentCulture = ci
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena al intentar autenticar el uso
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena cuando se produce un error
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' ' Se desencadena cuando finaliza la sesión
        'Dim dt_total_01 As New System.Data.DataSet
        Session("dsGeneral") = Nothing

    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Se desencadena cuando finaliza la aplicación
    End Sub

End Class