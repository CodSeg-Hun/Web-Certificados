Public Class _ErrorAplicacion
    Inherits System.Web.UI.Page

    'Dim error_msg As String

    ''' <summary>
    ''' FECHA: 10/06/2012
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: MÉTODO PARA MOSTRAR LOS ERRORES PRESETNADOS Y ENVIARLOS VÍA EMAIL
    ''' </summary>
    ''' <param name="tipo"></param>
    ''' <remarks></remarks>
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try

    '        Email.usuario_ip = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList(0).ToString
    '        Email.usuario_sesion = Session("user").ToString
    '        Email.Enviar_Email(tipo)

    '    Catch ex As Exception
    '        Captura_Error(ex)
    '    End Try

    'End Sub

    
    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' FECHA: 26/07/2012
    ''' COMENTARIO: EVENTO LOAD DEL FORMULARIO PÁGINA "404"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If (Session("user") Is Nothing) Then
                    Session.Clear()
                    Session.Abandon()
                    Me.lbl_error_msg.Text = "Debe de iniciar sesión para poder ingresar a los reportes"
                Else
                    If (Session("user") Is Nothing) Then
                        Session.Clear()
                        Session.Abandon()
                        Session("alert") = "Se ha finalizado el tiempo de consulta, por favor ingrese nuevamente"
                        Response.Redirect("login.aspx")
                    Else
                        Session("alert") = "Usted no puede acceder al reporte seleccionado"
                        Me.lbl_error_msg.Text = Session("alert")
                        Session.Clear()
                        Session.Abandon()
                        'Response.Redirect("login.aspx")
                    End If
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

End Class