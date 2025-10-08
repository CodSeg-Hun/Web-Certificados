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

Public Module log_control

    Public Sub Registra_Permanencia(ByVal ip As String, ByVal idUsuario As Integer, ByVal idFormularioBack As Integer, ByVal idFormulario As Integer, ByVal descUsuario As String)
        Try
            Dim obj As New Log
            Dim dtRegistroLog As New System.Data.DataSet
            dtRegistroLog = obj.Registro_Actividad_Formulario(ip, idUsuario, idFormularioBack, idFormulario, descUsuario)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function Registra_Permanencia2(ByVal usuarioIngreso As Integer, ByVal ip As String, ByVal idMenu As Integer, ByVal refMenu As Integer, ByVal idMenuBack As Integer, ByVal refIdBack As Integer) As Long
        Try
            Dim obj As New Log
            'Dim dtRegistroLog2 As New System.Data.DataSet
            Dim idLastObtenido As Int64
            idLastObtenido = obj.Registro_Actividad_Formulario2(usuarioIngreso, ip, idMenu, refMenu, idMenuBack, refIdBack)
            Return idLastObtenido
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub Consulta_Formulario(ByVal formulario As String)
        Try
            Dim obj As New Log
            Dim dtConsultaFormulario As New System.Data.DataSet
            dtConsultaFormulario = obj.Consulta_Formulario(formulario)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Module



