Imports System
Imports System.Security.Cryptography
Imports System.IO

Public Class GeneraDataCphr

    'Private Shared encryptionKey As String = "h1s2t3e4s5"
    Private Shared key As Byte() = {}
    Private Shared iv As Byte() = {&H12, &H34, &H56, &H78, &H90, &HAB, &HCD, &HEF}

    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: FUNCIÓN PARA DESCIFRAR CADENAS DE CARACTERES
    ''' </summary>
    ''' <param name="stringToDecrypt"></param>
    ''' <param name="sEncryptionKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Descifrar(stringToDecrypt As String, sEncryptionKey As String) As String
        Dim inputByteArray As Byte() = New Byte(stringToDecrypt.Length) {}
        Try
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey)
            Dim des As New DESCryptoServiceProvider()
            inputByteArray = Convert.FromBase64String(stringToDecrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateDecryptor(key, iv), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Dim encoding As System.Text.Encoding = System.Text.Encoding.UTF8
            Return encoding.GetString(ms.ToArray())
        Catch e As Exception
            Return e.Message
        End Try
    End Function



    ''' <summary>
    ''' AUTOR: JONATHAN COLOMA
    ''' COMENTARIO: 
    ''' </summary>
    ''' <param name="stringToEncrypt"></param>
    ''' <param name="SEncryptionKey"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Cifrar(stringToEncrypt As String, sEncryptionKey As String) As String
        Try
            key = System.Text.Encoding.UTF8.GetBytes(sEncryptionKey)
            Dim des As New DESCryptoServiceProvider()
            Dim inputByteArray As Byte() = Encoding.UTF8.GetBytes(stringToEncrypt)
            Dim ms As New MemoryStream()
            Dim cs As New CryptoStream(ms, des.CreateEncryptor(key, iv), CryptoStreamMode.Write)
            cs.Write(inputByteArray, 0, inputByteArray.Length)
            cs.FlushFinalBlock()
            Return Convert.ToBase64String(ms.ToArray())
        Catch e As Exception
            Return e.Message
        End Try
    End Function



End Class
