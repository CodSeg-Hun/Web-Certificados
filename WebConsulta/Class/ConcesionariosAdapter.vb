Public Class ConcesionariosAdapter

    Public Shared Function ConsultaDatos(ByVal usuario As String, ByVal anio As String, ByVal mes As String, ByVal canal As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@PROCESO", 5)
            base.AddParrameter("@ANIO", anio)
            base.AddParrameter("@MES", mes)
            base.AddParrameter("@CANAL", canal)
            ds = base.Consulta("INTRANET.sp_consulta_concesionario_web")
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function


    Public Shared Function CargarDatos(ByVal proceso As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@PROCESO", proceso)
            ds = base.Consulta("INTRANET.sp_consulta_concesionario_web")
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function


    Public Shared Function ConsultaDatosCanal(ByVal canal As String) As DataSet
        Dim ds As New DataSet
        Dim base As New DataBaseApp.CommandApp
        Try
            base.AddParrameter("@PROCESO", 4)
            base.AddParrameter("@CRIT_BUSQ", canal)
            ds = base.Consulta("INTRANET.sp_consulta_concesionario_web")
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function


    'Public Shared Function ConsultaDatosCliente(ByVal cliente As String) As DataSet
    '    Dim ds As New DataSet
    '    Dim base As New DataBaseApp.CommandApp
    '    Try
    '        base.AddParrameter("@PROCESO", "108")
    '        base.AddParrameter("@CRIT_BUSQ", cliente)
    '        ds = base.Consulta("Extranet.EXT_SP_CONSULTA_DATOS_PERSONALES")
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    '    Return ds
    'End Function


    Function RegistroActividadConcesionario(ByVal mes As String, ByVal anio As String, ByVal concesionario As String, ByVal usuario As Int64, ByVal impreso As String, _
                                            ByVal ipmaquina As String, ByVal pantalla As String, ByVal correo As String, ByVal latam As Boolean, ByVal consultar As Boolean, _
                                            ByVal tipo As String) As Int64
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
            Dim datos As String = "MES : " & LTrim(Trim(mes)) & " ANIO : " & LTrim(Trim(anio)) & " CONCESIONARIO: " & LTrim(Trim(concesionario)) & tipo.ToUpper() & consultar
            'If latam Then
            '    datos = datos & " Latam: " & latam
            'End If
            base.AddParrameter("@CONSULTA", datos)
            base.AddParrameter("@IP", ipmaquina)
            base.AddParrameter("@USUARIO", usuario)
            base.AddParrameter("@PANTALLA", pantalla)
            base.AddParrameter("@IMPRESO", impreso)
            base.AddParrameter("@CORREO", correo)
            base.EjecutaTransaction(cmd, tran)
            tran.Commit()
        Catch ex As Exception
            tran.Rollback()
            'Captura_Error(ex)
            Throw ex
        End Try
    End Function


End Class
