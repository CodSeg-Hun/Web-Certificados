Public Class busquedacanal
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                BtnConsulta.Enabled = True
                BtnAceptar.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Protected Sub BtnConsulta_Click(sender As Object, e As EventArgs) Handles BtnConsulta.Click
        Try
            If Len(Me.txtbusqueda.Text) > 2 Then
                Dim dTDatosCliente As New DataSet
                dTDatosCliente = ConcesionariosAdapter.ConsultaDatosCanal(Me.txtbusqueda.Text)
                rgdconsulta.DataSource = dTDatosCliente
                rgdconsulta.DataBind()
            Else
                MostrarMensaje("Por favor ingresar al menos tres caracteres para iniciar la busqueda")
                Me.rgdconsulta.DataSource = ""
                Me.rgdconsulta.DataBind()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub MostrarMensaje(ByVal mensaje As String)
        Try
            rntMensajes.Text = mensaje
            rntMensajes.Title = "Mensaje de la Aplicación HunterOnline"
            rntMensajes.TitleIcon = "warning"
            rntMensajes.ContentIcon = "warning"
            rntMensajes.ShowSound = "warning"
            rntMensajes.Width = 400
            rntMensajes.Height = 100
            rntMensajes.Show()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class