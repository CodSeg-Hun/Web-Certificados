Imports System.Data


Public Class ConsultaWeb

    ''' <summary>
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' COMENTARIO: MÉDOTO PARA ENVÍO DE ERRORES VÍA EMAIL
    ''' </summary>
    ''' <param name="tipo"></param>
    ''' <remarks></remarks>
    'Protected Sub Captura_Error(ByVal tipo As Exception)
    '    Try
    '        Email.Enviar_Email(tipo, CType(Application("usuario_email"), String))
    '    Catch ex As Exception
    '        Captura_Error(ex)
    '    End Try
    'End Sub


    ''' <summary>
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' COMENTARIO: MÉDOTO PARA PODER CONSULTAR LOS DATOS INICIALES
    ''' </summary>
    ''' <param name="tipo"></param>
    ''' <remarks></remarks>
    Function Consulta_Datos(ByVal vehiculo As String, ByVal clienteid As String, ByVal nombre As String, ByVal chasis As String, _
                            ByVal placa As String, ByVal motor As String, ByVal usuario As Int64) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            If vehiculo <> "" Then base.AddParrameter("@CODIGO_VEHICULO", vehiculo)
            If placa <> "" Then base.AddParrameter("@PLACA", placa) 'MLNY 07-07-2023
            If chasis <> "" Then base.AddParrameter("@CHASIS", chasis)
            If motor <> "" Then base.AddParrameter("@MOTOR", motor)
            'If clienteid <> "" Then base.AddParrameter("@CLIENTE", clienteid)
            'If nombre <> "" Then base.AddParrameter("@NOMBRE", nombre)
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@proceso", 2)
            ds = base.Consulta("intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function ConsultarLatam(ByVal usuario As String, ByVal fechaini As String, ByVal fechafin As String, ByVal chasis As String, _
                            ByVal motor As String, ByVal vehiculo As String, ByVal proceso As Int32, ByVal estado As String, ByVal tipo As String, _
                            ByVal canal As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@FECHA_INI", fechaini)
            base.AddParrameter("@FECHA_FIN", fechafin)
            If vehiculo <> "" Then base.AddParrameter("@CODIGO_VEHICULO", vehiculo)
            If chasis <> "" Then base.AddParrameter("@CHASIS", chasis)
            If motor <> "" Then base.AddParrameter("@MOTOR", motor)
            base.AddParrameter("@proceso", proceso)
            base.AddParrameter("@ESTADO", estado)
            base.AddParrameter("@TIPO_CANAL", tipo)
            base.AddParrameter("@RUC_CANAL", canal)
            ds = base.Consulta("intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    ''' <summary>
    ''' Fecha: 08/08/2013
    ''' Autor: Galo Alvarado
    ''' COMENTARIO: MÉDOTO PARA PODER CONSULTAR LOS DATOS INICIALES
    ''' </summary>
    ''' <param name="tipo"></param>
    ''' <remarks></remarks>
    Function Consulta_Mensaje(ByVal vehiculo As String, ByVal usuario As Int64, ByVal placa As String, ByVal motor As String, ByVal chasis As String, ByVal tipo As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@CODIGO_VEHICULO", vehiculo) ' "1001169589"
            base.AddParrameter("@USUARIO", usuario) ' 3216  Int64
            base.AddParrameter("@proceso", 7)
            ''Adicionales 05/11/2013
            base.AddParrameter("@PLACA", placa) ' S/P
            base.AddParrameter("@MOTOR", motor) '"GW4D20M245A6014819            "
            base.AddParrameter("@CHASIS", chasis)
            base.AddParrameter("@TIPO", tipo) ' "c"
            ds = base.Consulta("intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function CertificadoDetalleServicios(ByVal opcion As String, ByVal cliente As String, ByVal vehiculo As String, ByVal orden As String, ByVal usuario As Int64) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@PROCESO", opcion)
            base.AddParrameter("@CLIENTE", cliente)
            base.AddParrameter("@CODIGO_VEHICULO", vehiculo)
            base.AddParrameter("@ORDEN_SERVICIO", orden)
            base.AddParrameter("@USUARIO", usuario)
            ds = base.Consulta("intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Shared Function ConsultaEstado() As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@proceso", 13)
            ds = base.Consulta("intranet.sp_consulta_web")
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function

    Public Shared Function ConsultaCanal(ByVal usuario As Int64) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@proceso", 15)
            base.AddParrameter("@USUARIO", usuario)
            ds = base.Consulta("intranet.sp_consulta_web")
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function


    Public Shared Function ConsultaTipo(ByVal usuario As Int64) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@proceso", 14)
            base.AddParrameter("@USUARIO", usuario)
            ds = base.Consulta("intranet.sp_consulta_web")
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function


    Function Consulta_Transaccion(ByVal vehiculo As String, ByVal usuario As Int64, ByVal tipo As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@CODIGO_VEHICULO", vehiculo) '  "1001169589"
            base.AddParrameter("@USUARIO", usuario) ' 3216
            base.AddParrameter("@TIPO", tipo) ' "C"
            base.AddParrameter("@PROCESO", 8)
            ds = base.Consulta("intranet.sp_consulta_web")
            Return ds ' producto: AB | Financiador:
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function Consulta_Orden(ByVal vehiculo As String, ByVal usuario As Int64, ByVal cliente As String, ByVal proceso As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@CODIGO_VEHICULO", vehiculo)
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@CLIENTE", cliente)
            base.AddParrameter("@PROCESO", proceso)
            ds = base.Consulta("intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function ConsultarRuta(ByVal codigomenu As String, ByVal opcion As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@CODIGOMENU", codigomenu)
            base.AddParrameter("@OPCION", opcion)
            ds = base.Consulta("Intranet.RPG_MENU_ANALITICA")
            ConsultarRuta = ds
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Function ConsultaInicial() As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@PROCESO", 1)
            ds = base.Consulta("Intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function ConsultaInicialVehiculo(ByVal proceso As Int32) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@PROCESO", proceso)
            ds = base.Consulta("Intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function ConsultaCodigo(ByVal vehiculo As String, ByVal clienteid As String, ByVal tipo As String, usuario As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@CODIGO_VEHICULO", vehiculo) ' 1001169589
            base.AddParrameter("@CLIENTE", clienteid) ' 1890010705001
            base.AddParrameter("@TIPO", tipo) ' N
            base.AddParrameter("@USUARIO", usuario) ' 3216
            base.AddParrameter("@PROCESO", 4)
            ds = base.Consulta("Intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function ConsultarImprimir(ByVal vehiculo As String, ByVal clienteid As String, ByVal usuarioid As Integer, ByVal tipo As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@CODIGO_VEHICULO", vehiculo) ' "1001169589"
            base.AddParrameter("@CLIENTE", clienteid) ' "1890010705001"
            base.AddParrameter("@USUARIO", usuarioid) ' 3216
            base.AddParrameter("@TIPO", tipo) ' 'C'
            base.AddParrameter("@PROCESO", 5)
            ds = base.Consulta("Intranet.sp_consulta_web")
            Return ds
        Catch ex As Exception
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


    Function Registro_Actividad(ByVal vehiculo As String, ByVal chasis As String, ByVal motor As String, ByVal usuario As Int64, ByVal impreso As String,
                                ByVal ipmaquina As String, ByVal pantalla As String, ByVal correo As String, ByVal origen As String, ByVal consultar As Boolean,
                                ByVal tipo As String) As Int64
        ' vehiculo: "1001169589", chasis: "8L4CBF192SC000419             ", motor: "GW4D20M245A6014819            ", usuario 3216, impreso: "S"
        ' ipmaquina: ::1, pantalla: ConsultaDatos.aspx, correo: " ", origen: "False", consultar: True, tipo: " Impreso: "
        'Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Dim cnn As SqlClient.SqlConnection = Nothing
        Dim cmd As SqlClient.SqlCommand = Nothing
        Dim tran As SqlClient.SqlTransaction = Nothing
        Try
            cmd = New SqlClient.SqlCommand
            cnn = base.Connection
            cnn.Open()
            base.ProcedureName = "Intranet.SP_LOG_CONSULTA_WEB"
            cmd.Connection = cnn
            tran = cnn.BeginTransaction("INGRESO")
            Dim datos As String = "VEHICULO : " & LTrim(Trim(vehiculo)) & " CHASIS : " & LTrim(Trim(chasis)) & " MOTOR: " & LTrim(Trim(motor)) & tipo.ToUpper() & consultar
            ' datoa: "VEHICULO : 1001169589 CHASIS : 8L4CBF192SC000419 MOTOR: GW4D20M245A6014819 IMPRESO: True"
            If origen = "L" Then
                datos = datos & " Latam: " & True
            End If
            If origen = "C" Then
                datos = datos & " Coris: " & True
            End If
            base.AddParrameter("@CONSULTA", datos) ' "VEHICULO : 1001169589 CHASIS : 8L4CBF192SC000419 MOTOR: GW4D20M245A6014819 IMPRESO: True"
            base.AddParrameter("@IP", ipmaquina) ' "::1"
            base.AddParrameter("@USUARIO", usuario) ' 3216
            base.AddParrameter("@PANTALLA", pantalla) ' "ConsultaDatos.aspx"
            base.AddParrameter("@IMPRESO", impreso) ' "S"
            base.AddParrameter("@CORREO", correo) ' " "
            base.EjecutaTransaction(cmd, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


End Class
