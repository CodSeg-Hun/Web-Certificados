Imports System.Security.Cryptography

Public Class OAuthBase

    Const HMACSHA256SignatureType As String = "HMAC-SHA256"
    Const OAuthVersion As String = "1.0"
    Const OAuthParameterPrefix As String = "oauth_"
    Const OAuthConsumerKeyKey As String = "oauth_consumer_key"
    Const OAuthVersionKey As String = "oauth_version"
    Const OAuthSignatureMethodKey As String = "oauth_signature_method"
    Const OAuthTimestampKey As String = "oauth_timestamp"
    Const OAuthNonceKey As String = "oauth_nonce"
    Const OAuthTokenKey As String = "oauth_token"

    Public Class QueryParameter

        Public Sub New(ByVal name As String, ByVal value As String)
            Me.name = name
            Me.value = value
        End Sub

        'Public ReadOnly Property Name As String
        'Public ReadOnly Property Value As String

        Private _name As String
        Public Property name() As String
            Get
                Return _name
            End Get
            Set(ByVal value As String)
                _name = value
            End Set
        End Property

        Private _value As String
        Public Property value() As String
            Get
                Return _value
            End Get
            Set(ByVal value As String)
                _value = value
            End Set
        End Property
    End Class

    Protected Class QueryParameterComparer
        Implements IComparer(Of QueryParameter)

        Public Function Compare(x As QueryParameter, y As QueryParameter) As Integer Implements IComparer(Of QueryParameter).Compare
            If x.name = y.name Then
                Return String.Compare(x.value, y.value)
            Else
                Return String.Compare(x.name, y.name)
            End If
        End Function

    End Class

    Public Shared Function ComputeHash(ByVal hashAlgorithm As HashAlgorithm, ByVal data As String) As String
        Dim dataBuffer As Byte() = Encoding.ASCII.GetBytes(data)
        Dim hashBytes As Byte() = hashAlgorithm.ComputeHash(dataBuffer)
        Return Convert.ToBase64String(hashBytes)
    End Function

    Public Shared Function GetQueryParameters(ByVal parameters As String) As List(Of QueryParameter)
        If parameters.StartsWith("?") Then
            parameters = parameters.Remove(0, 1)
        End If

        Dim result As List(Of QueryParameter) = New List(Of QueryParameter)()

        If Not String.IsNullOrEmpty(parameters) Then
            Dim p As String() = parameters.Split("&")

            For Each s As String In p

                If Not String.IsNullOrEmpty(s) AndAlso Not s.StartsWith(OAuthParameterPrefix) Then

                    If s.IndexOf("=") > -1 Then
                        Dim temp As String() = s.Split("=")
                        result.Add(New QueryParameter(temp(0), temp(1)))
                    Else
                        result.Add(New QueryParameter(s, String.Empty))
                    End If
                End If
            Next
        End If
        Return result
    End Function

    Public Shared Function UrlEncode(ByVal value As String) As String
        Dim result As StringBuilder = New StringBuilder()

        Dim unreservedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_.~"

        For Each symbol As Char In value

            If unreservedChars.IndexOf(symbol) <> -1 Then
                result.Append(symbol)
            Else
                'result.Append("%"c & String.Format("{0:X2}", CInt(symbol)))
                result.Append("%" & String.Format("{0:X2}", Asc(symbol)))
            End If
        Next

        Return result.ToString()
    End Function

    Public Shared Function NormalizeRequestParameters(ByVal parameters As IList(Of QueryParameter)) As String
        Dim sb As StringBuilder = New StringBuilder()
        Dim p As QueryParameter = Nothing

        For i As Integer = 0 To parameters.Count - 1
            p = parameters(i)
            sb.AppendFormat("{0}={1}", p.name, p.value)

            If i < parameters.Count - 1 Then
                sb.Append("&")
            End If
        Next

        Return sb.ToString()

    End Function

    Public Shared Function GenerateSignatureBase(ByVal url As Uri, ByVal consumerKey As String, ByVal token As String, ByVal httpMethod As String, ByVal timeStamp As String, ByVal nonce As String, ByVal signatureType As String) As String      'If token Is Nothing Then

        Dim parameters As List(Of QueryParameter) = GetQueryParameters(url.Query)
        parameters.Add(New QueryParameter(OAuthVersionKey, OAuthVersion))
        parameters.Add(New QueryParameter(OAuthNonceKey, nonce))
        parameters.Add(New QueryParameter(OAuthTimestampKey, timeStamp))
        parameters.Add(New QueryParameter(OAuthSignatureMethodKey, signatureType))
        parameters.Add(New QueryParameter(OAuthConsumerKeyKey, consumerKey))
        parameters.Add(New QueryParameter(OAuthTokenKey, token))
        parameters.Sort(New QueryParameterComparer())
        Dim normalizedUrl = String.Format("{0}://{1}", url.Scheme, url.Host)

        If Not ((url.Scheme = "http" AndAlso url.Port = 80) OrElse (url.Scheme = "https" AndAlso url.Port = 443)) Then
            normalizedUrl += ":" & url.Port
        End If

        normalizedUrl += url.AbsolutePath
        Dim normalizedRequestParameters = NormalizeRequestParameters(parameters)

        Dim signatureBase As StringBuilder = New StringBuilder()
        signatureBase.AppendFormat("{0}&", httpMethod.ToUpper())
        signatureBase.AppendFormat("{0}&", UrlEncode(normalizedUrl))
        signatureBase.AppendFormat("{0}", UrlEncode(normalizedRequestParameters))
        Return signatureBase.ToString()

    End Function


    Public Shared Function GenerateSignatureUsingHash(ByVal signatureBase As String, ByVal hash As HashAlgorithm) As String
        Return ComputeHash(hash, signatureBase)
    End Function


    Public Shared Function GenerateSignatureSHA256(ByVal url As Uri, ByVal consumerKey As String, ByVal consumerSecret As String, ByVal token As String, ByVal tokenSecret As String, ByVal httpMethod As String, ByVal timeStamp As String, ByVal nonce As String) As String
        Dim sha256base As String = GenerateSignatureBase(url, consumerKey, token, httpMethod, timeStamp, nonce, HMACSHA256SignatureType)

        Dim hmacsha256 As HMACSHA256 = New HMACSHA256()

        hmacsha256.Key = Encoding.ASCII.GetBytes(String.Format("{0}&{1}", UrlEncode(consumerSecret), If(String.IsNullOrEmpty(tokenSecret), "", UrlEncode(tokenSecret))))


        Return GenerateSignatureUsingHash(sha256base, hmacsha256)
    End Function


    Public Shared Function GenerateSignature(ByVal url As Uri, ByVal consumerKey As String, ByVal consumerSecret As String, ByVal token As String, ByVal tokenSecret As String, ByVal httpMethod As String, ByVal timeStamp As String, ByVal nonce As String) As String
        Dim signature = GenerateSignatureSHA256(url, consumerKey, consumerSecret, token, tokenSecret, httpMethod, timeStamp, nonce)

        If signature.Contains("+") Then
            signature = signature.Replace("+", "%2B")
        End If

        If signature.Contains("=") Then
            signature = signature.Replace("=", "%3D")
        End If

        Return signature
    End Function

    Public Shared Function GenerateTimeStamp() As String
        Dim ts As TimeSpan = DateTime.UtcNow - New DateTime(1970, 1, 1, 0, 0, 0, 0)
        Return Convert.ToInt64(ts.TotalSeconds).ToString()
    End Function

    Public Shared Function GenerateNonce() As String
        Dim random = New Random()
        Return random.[Next](123400, 9999999).ToString()
    End Function


End Class
