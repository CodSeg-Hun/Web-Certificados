Public Class Sitios
    Inherits System.Web.UI.Page


    Public Enum Operacion
        OExistosa = 1
        OInvalida = 2
        CSinDatos = 3
    End Enum


    '  Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Not IsPostBack Then
                If Session("user_id") IsNot Nothing Then
                    'Dim valueCfr01 As String
                    'valueCfr01 = HttpUtility.UrlEncode(EncryptQueryString(String.Format("973210")))
                    Dim identificador As String = ""
                    If Request.QueryString("Id") <> Nothing Then
                        Dim obj As New ConsultaWeb
                        Dim dtRuta As New System.Data.DataSet
                        identificador = DecryptQueryString(Request.QueryString("Id"))
                        dtRuta = obj.ConsultarRuta(identificador, "2")
                        If dtRuta.Tables.Count > 0 Then
                            Dim file As String = dtRuta.Tables(0).Rows(0)("RUTA").ToString()
                            myframe.Attributes.Add("src", file)
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            'Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
    End Sub


    Private Function DecryptQueryString(strQueryString As String) As String
        Dim descifrado As String = String.Empty
        Dim obt As New GeneraDataCphr
        Try
            descifrado = obt.Descifrar(strQueryString, "r0b1nr0y")
        Catch ex As Exception
            'Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
        Return descifrado
    End Function


    Public Function EncryptQueryString(ByVal strQueryString As String) As String
        Dim cifrado As String = String.Empty
        Dim obt As New GeneraDataCphr
        Try
            cifrado = obt.Cifrar(strQueryString, "r0b1nr0y")
        Catch ex As Exception
            'Me.Mensaje(ex.Message, Operacion.OInvalida, ex)
            Enviar_Email(ex, CType(Application("usuario_email"), String))
        End Try
        Return cifrado
    End Function

    ''' <summary>
    ''' Motivo: Personaliza mensaje a preentar al usuario
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' </summary>
    ''' <param name="Texto"></param>
    ''' <param name="OperacionRealizar"></param>
    ''' <remarks></remarks>
    'Private Sub Mensaje(ByVal texto As String, ByVal operacionRealizar As Int32, tipo As Exception)
    '    Try
    '        Dim titulo As String = String.Empty
    '        Dim icono As String = String.Empty
    '        Select Case operacionRealizar
    '            Case Operacion.OInvalida
    '                titulo = "Operación Inválida"
    '                icono = "Warning"
    '            Case Operacion.OExistosa
    '                titulo = "Operación Exitosa"
    '                icono = "Info"
    '            Case Operacion.CSinDatos
    '                titulo = "Consulta sin Datos"
    '                icono = "Info"
    '        End Select
    '        Me.RnMensajesError.Text = texto
    '        Me.RnMensajesError.Title = titulo
    '        Me.RnMensajesError.TitleIcon = icono
    '        Me.RnMensajesError.ContentIcon = icono
    '        Me.RnMensajesError.Show()
    '    Catch ex As Exception
    '        Enviar_Email(ex, CType(Application("usuario_email"), String))
    '    End Try
    'End Sub

End Class