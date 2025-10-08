Public Class ConsultaReporteClaro
    Inherits System.Web.UI.Page
    Dim datVehiculo As Int32
    Dim datOs As Int32
    Dim datCliente As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("DetalleVehiculo") = Nothing
                Detalle2("Nuevo")
                Me.lblfechahoraemision.Text = "Emitido el " & System.DateTime.Now.ToString("dd MMMM yyyy H:mm:ss")
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

    Private Sub Detalle2(ByVal tipo As String)
        Try
            Dim obj As New ConsultaWeb
            Dim dtvehiculo As New System.Data.DataSet
            Dim dtVehiculoDetalle As New System.Data.DataSet
            Dim dtconsulta As New System.Data.DataSet
            If tipo = "Nuevo" Then
                dtvehiculo = Session("Detalle")
                dtVehiculoDetalle = Session("producto")
                If dtvehiculo.Tables(0).Rows.Count > 0 Then
                    If Session("cbm_tipo") = "8" Then ' Certificado de Venta
                        Me.lbltitulodetfljcja.Text = "Certificado de Venta"
                        Image1.ImageUrl = "../Images/background_banner_certificado_conecel.jpg"
                        grid.Visible = True
                        tex01.Visible = True
                        tex02.Visible = True
                        tex05.Visible = True
                        tex06.Visible = True
                        textcabecera.Visible = True
                        textcabecera01.Visible = False
                    ElseIf Session("cbm_tipo") = "2" Then ' Certificado de Desinstalación
                        grid.Visible = False
                        tex01.Visible = False
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        textcabecera.Visible = False
                        textcabecera01.Visible = True
                        Me.lbltitulodetfljcja.Text = "Certificado de Desinstalación"
                        Image1.ImageUrl = "../Images/background_banner_certificado_conecel.jpg"
                    ElseIf Session("cbm_tipo") = "1" Then ' Certificado de Instalación
                        grid.Visible = True
                        tex01.Visible = False
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        textcabecera.Visible = False
                        textcabecera01.Visible = True
                        grdproductosdetalle.Visible = True
                        grdproductosdetalle.MasterTableView.GetColumn("PRODUCTO").Display = True
                        grdproductosdetalle.MasterTableView.GetColumn("FECHAINSTALACION").Display = True
                        grdproductosdetalle.MasterTableView.GetColumn("COBERTURA").Display = True
                        grdproductosdetalle.Width = Unit.Pixel(670)
                        Me.lbltitulodetfljcja.Text = "Certificado de Instalación"
                        Image1.ImageUrl = "../Images/background_banner_certificado_conecel.jpg"
                    End If
                    'dtconsulta = obj.ConsultarImprimir(dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtdetalle.Tables(0).Rows(0)("CLIENTE").ToString(), Session("user_id"), dtdetalle.Tables(0).Rows(0)("TIPO").ToString())
                    'Session("infoqr") = dtconsulta
                    If dtvehiculo.Tables(0).Rows.Count > 0 Then
                        Me.Nombre.Text = dtvehiculo.Tables(0).Rows(0)("CLIENTE").ToString()
                        Me.Vehiculo.Text = dtvehiculo.Tables(0).Rows(0)("idVehiculo").ToString()
                        Me.Marca.Text = dtvehiculo.Tables(0).Rows(0)("MARCA").ToString()
                        Me.Anio.Text = dtvehiculo.Tables(0).Rows(0)("ANIO").ToString()
                        Me.Modelo.Text = dtvehiculo.Tables(0).Rows(0)("MODELO").ToString()
                        Me.Placa.Text = dtvehiculo.Tables(0).Rows(0)("PLACA").ToString()
                        Me.Tipo.Text = dtvehiculo.Tables(0).Rows(0)("TIPO").ToString()
                        Me.Chasis.Text = dtvehiculo.Tables(0).Rows(0)("CHASIS").ToString()
                        Me.Color.Text = dtvehiculo.Tables(0).Rows(0)("COLOR").ToString()
                        Me.Motor.Text = dtvehiculo.Tables(0).Rows(0)("MOTOR").ToString()
                        Me.grdproductosdetalle.DataSource = dtVehiculoDetalle
                    End If
                End If
            Else
                'dtvehiculo = obj.ConsultarImprimir(datVehiculo, datCliente, Session("user_id"), "C")
                'Session("infoqr") = dtconsulta
                If dtvehiculo.Tables(0).Rows.Count > 0 Then
                    Me.Nombre.Text = dtvehiculo.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.Vehiculo.Text = dtvehiculo.Tables(0).Rows(0)("idVehiculo").ToString()
                    Me.Marca.Text = dtvehiculo.Tables(0).Rows(0)("MARCA").ToString()
                    Me.Anio.Text = dtvehiculo.Tables(0).Rows(0)("ANIO").ToString()
                    Me.Modelo.Text = dtvehiculo.Tables(0).Rows(0)("MODELO").ToString()
                    Me.Placa.Text = dtvehiculo.Tables(0).Rows(0)("PLACA").ToString()
                    Me.Tipo.Text = dtvehiculo.Tables(0).Rows(0)("TIPO").ToString()
                    Me.Chasis.Text = dtvehiculo.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.Color.Text = dtvehiculo.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Motor.Text = dtvehiculo.Tables(0).Rows(0)("MOTOR").ToString()
                    Me.grdproductosdetalle.DataSource = dtVehiculoDetalle
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub

    ''' <summary>
    ''' FECHA: 27/02/2013
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: MÉTODO PARA OBTENER DATOS DE FLUJO
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Detalle(ByVal tipo As String)
        Try
            Dim obj As New ConsultaWeb
            Dim dtdetalle As New System.Data.DataSet
            Dim dtconsulta As New System.Data.DataSet
            If tipo = "Nuevo" Then
                dtdetalle = Session("Detalle")
                If dtdetalle.Tables(0).Rows.Count > 0 Then
                    If Session("cbm_tipo") = "C" Then
                        Me.lbltitulodetfljcja.Text = "Certificado de Venta"
                        Image1.ImageUrl = "../Images/background_banner_certificado_conecel.jpg"
                        grid.Visible = True
                        tex01.Visible = True
                        tex02.Visible = True
                        tex05.Visible = True
                        tex06.Visible = True
                        textcabecera.Visible = True
                        textcabecera01.Visible = False
                    ElseIf Session("cbm_tipo") = "D" Then
                        grid.Visible = False
                        tex01.Visible = False
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        textcabecera.Visible = False
                        textcabecera01.Visible = True
                        Me.lbltitulodetfljcja.Text = "Certificado de Desinstalación"
                        Image1.ImageUrl = "../Images/background_banner_certificado_conecel.jpg"
                    ElseIf Session("cbm_tipo") = "I" Then
                        grid.Visible = False
                        tex01.Visible = False
                        tex02.Visible = False
                        tex05.Visible = False
                        tex06.Visible = False
                        textcabecera.Visible = False
                        textcabecera01.Visible = True
                        Me.lbltitulodetfljcja.Text = "Certificado de Instalación"
                        Image1.ImageUrl = "../Images/background_banner_certificado_conecel.jpg"
                    End If
                    dtconsulta = obj.ConsultarImprimir(dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString(), dtdetalle.Tables(0).Rows(0)("CLIENTE").ToString(), Session("user_id"), dtdetalle.Tables(0).Rows(0)("TIPO").ToString())
                    Session("infoqr") = dtconsulta
                    If dtconsulta.Tables(0).Rows.Count > 0 Then
                        Me.Nombre.Text = dtconsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                        Me.Vehiculo.Text = dtconsulta.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                        Me.Marca.Text = dtconsulta.Tables(0).Rows(0)("MARCA").ToString()
                        Me.Anio.Text = dtconsulta.Tables(0).Rows(0)("ANIO").ToString()
                        Me.Modelo.Text = dtconsulta.Tables(0).Rows(0)("MODELO").ToString()
                        Me.Placa.Text = dtconsulta.Tables(0).Rows(0)("PLACA").ToString()
                        Me.Tipo.Text = dtconsulta.Tables(0).Rows(0)("TIPO").ToString()
                        Me.Chasis.Text = dtconsulta.Tables(0).Rows(0)("CHASIS").ToString()
                        Me.Color.Text = dtconsulta.Tables(0).Rows(0)("COLOR").ToString()
                        Me.Motor.Text = dtconsulta.Tables(0).Rows(0)("MOTOR").ToString()
                        Me.grdproductosdetalle.DataSource = dtconsulta
                    End If
                End If
            Else
                dtconsulta = obj.ConsultarImprimir(datVehiculo, datCliente, Session("user_id"), "C")
                Session("infoqr") = dtconsulta
                If dtconsulta.Tables(0).Rows.Count > 0 Then
                    Me.Nombre.Text = dtconsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.Vehiculo.Text = dtconsulta.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                    Me.Marca.Text = dtconsulta.Tables(0).Rows(0)("MARCA").ToString()
                    Me.Anio.Text = dtconsulta.Tables(0).Rows(0)("ANIO").ToString()
                    Me.Modelo.Text = dtconsulta.Tables(0).Rows(0)("MODELO").ToString()
                    Me.Placa.Text = dtconsulta.Tables(0).Rows(0)("PLACA").ToString()
                    Me.Tipo.Text = dtconsulta.Tables(0).Rows(0)("TIPO").ToString()
                    Me.Chasis.Text = dtconsulta.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.Color.Text = dtconsulta.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Motor.Text = dtconsulta.Tables(0).Rows(0)("MOTOR").ToString()
                    Me.grdproductosdetalle.DataSource = dtconsulta
                End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


End Class