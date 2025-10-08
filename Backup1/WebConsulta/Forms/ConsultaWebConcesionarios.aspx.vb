'Imports System.Collections.Generic
'Imports System.Linq
'Imports MessagingToolkit.Barcode
'Imports System.Data
'Imports System.IO

Public Class ConsultaWebConcesionarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                Session("DetalleVehiculo") = Nothing
                Detalle("Nuevo")
                'CargaImagenQR()
                Me.lblfechahoraemision.Text = "Emitido el " & System.DateTime.Now.ToString("dd MMMM yyyy H:mm:ss")
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
            'Dim dtDetalleFlujo2 As New System.Data.DataSet
            'dtDetalleFlujo2 = obj.ConsultaDetalleFormaPago(empresa, proceso, Session("rpt_flj_cja_det_frm_pago_anio"))
            'Dim dtdetalle As New System.Data.DataSet
            Dim dtconsulta As New System.Data.DataSet
            If tipo = "Nuevo" Then
                'dtdetalle = Session("Detalle")
                'If dtdetalle.Tables(0).Rows.Count > 0 Then
                ''Me.Vehiculo.Text = dtdetalle.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                ''If dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "C" Then
                ''    Image1.ImageUrl = "../Images/background_banner_certificado.jpg"
                ''ElseIf dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "D" Then
                ''    Image1.ImageUrl = "../Images/background_banner_certificado_des.jpg"
                ''ElseIf dtdetalle.Tables(0).Rows(0)("TIPO").ToString() = "I" Then
                ''    Image1.ImageUrl = "../Images/background_banner_certificado_ins.jpg"
                ''End If
                'If Session("cbm_tipo") = "C" Then
                '    Image1.ImageUrl = "../Images/background_banner_certificado.jpg"
                '    grid.Visible = True
                '    tex01.Visible = True
                '    tex04.Visible = False
                '    tex03.Visible = False
                '    tex02.Visible = True
                '    textcabecera.Visible = True
                '    textcabecera01.Visible = False
                'ElseIf Session("cbm_tipo") = "D" Then
                '    grid.Visible = False
                '    tex01.Visible = False
                '    tex04.Visible = True
                '    tex03.Visible = False
                '    tex02.Visible = False
                '    textcabecera.Visible = False
                '    textcabecera01.Visible = True
                '    Image1.ImageUrl = "../Images/background_banner_certificado_des.jpg"
                'ElseIf Session("cbm_tipo") = "I" Then
                '    grid.Visible = False
                '    tex01.Visible = False
                '    tex04.Visible = False
                '    tex03.Visible = True
                '    tex02.Visible = False
                '    textcabecera.Visible = False
                '    textcabecera01.Visible = True
                '    Image1.ImageUrl = "../Images/background_banner_certificado_ins.jpg"
                'End If
                dtconsulta = obj.ConsultarImprimir(Session("VEHICULO").ToString(), Session("CLIENTE").ToString(), Session("user_id"), "G")
                Session("infoqr") = dtconsulta
                If dtconsulta.Tables(0).Rows.Count > 0 Then
                    Me.Nombre.Text = dtconsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                    Me.Vehiculo.Text = dtconsulta.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                    ''Me.Producto.Text = dtconsulta.Tables(0).Rows(0)("PRODUCTO").ToString()
                    ''Me.Cobertura.Text = dtconsulta.Tables(0).Rows(0)("COBERTURA").ToString()
                    Me.Marca.Text = dtconsulta.Tables(0).Rows(0)("MARCA").ToString()
                    Me.Anio.Text = dtconsulta.Tables(0).Rows(0)("ANIO").ToString()
                    Me.Modelo.Text = dtconsulta.Tables(0).Rows(0)("MODELO").ToString()
                    Me.Placa.Text = dtconsulta.Tables(0).Rows(0)("PLACA").ToString()
                    Me.Tipo.Text = dtconsulta.Tables(0).Rows(0)("TIPO").ToString()
                    Me.Chasis.Text = dtconsulta.Tables(0).Rows(0)("CHASIS").ToString()
                    Me.Color.Text = dtconsulta.Tables(0).Rows(0)("COLOR").ToString()
                    Me.Motor.Text = dtconsulta.Tables(0).Rows(0)("MOTOR").ToString()
                    ''Me.precioproducto.Text = dtconsulta.Tables(0).Rows(0)("PRECIO_PRODUCTO").ToString()
                    Me.grdproductosdetalle.DataSource = dtconsulta
                    'Session("datocliente") = Me.Nombre.Text + " | " + Me.Marca.Text + " | " + Me.Modelo.Text + " | " + Me.Chasis.Text + " | " + Me.Placa.Text
                End If
                'End If
                'Else
                'dtconsulta = obj.ConsultarImprimir(datVehiculo, datCliente, Session("user_id"), "C")
                'Session("infoqr") = dtconsulta
                'If dtconsulta.Tables(0).Rows.Count > 0 Then
                '    Me.Nombre.Text = dtconsulta.Tables(0).Rows(0)("CLIENTE").ToString()
                '    Me.Vehiculo.Text = dtconsulta.Tables(0).Rows(0)("CODIGO_VEHICULO").ToString()
                '    Me.Marca.Text = dtconsulta.Tables(0).Rows(0)("MARCA").ToString()
                '    Me.Anio.Text = dtconsulta.Tables(0).Rows(0)("ANIO").ToString()
                '    Me.Modelo.Text = dtconsulta.Tables(0).Rows(0)("MODELO").ToString()
                '    Me.Placa.Text = dtconsulta.Tables(0).Rows(0)("PLACA").ToString()
                '    Me.Tipo.Text = dtconsulta.Tables(0).Rows(0)("TIPO").ToString()
                '    Me.Chasis.Text = dtconsulta.Tables(0).Rows(0)("CHASIS").ToString()
                '    Me.Color.Text = dtconsulta.Tables(0).Rows(0)("COLOR").ToString()
                '    Me.Motor.Text = dtconsulta.Tables(0).Rows(0)("MOTOR").ToString()
                '    Me.grdproductosdetalle.DataSource = dtconsulta
                'End If
            End If
        Catch ex As Exception
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    
End Class