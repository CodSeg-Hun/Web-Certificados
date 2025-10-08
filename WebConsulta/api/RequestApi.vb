Imports DevExpress.DataProcessing.InMemoryDataProcessor
Imports Newtonsoft.Json.Linq
Imports RestSharp
Imports System.Net

Public Class RequestApi

    Public Shared Function Fetch(ByVal opcion As String, ByVal chasis As String, ByVal motor As String, ByVal codVehiculo As String, ByVal placa As String, ByVal cobertura As String, ByVal rucCanales As String, ByVal metodo As RestSharp.Method, ByVal script As String) As List(Of String)
        Dim resultados As New List(Of String)()
        Try
            ServicePointManager.Expect100Continue = True
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls Or SecurityProtocolType.Tls11 Or SecurityProtocolType.Tls12 Or SecurityProtocolType.Ssl3

            Dim ruta As New conexion
            Dim API_URL As String = ruta.ObtenerRuta(script, "1")
            Dim HMACSHA256SignatureType As String = ruta.ObtenerDatos("1")
            Dim OAuthVersion As String = ruta.ObtenerDatos("2")
            Dim oauthConsumerKey = ruta.ObtenerDatos("3")
            Dim oauthToken = ruta.ObtenerDatos("4")
            Dim oauthConsumerSecret = ruta.ObtenerDatos("5")
            Dim oauthTokenSecret = ruta.ObtenerDatos("6")
            Dim realm = ruta.ObtenerDatos("9")
            Dim httpMethod = ruta.ObtenerDatos("7")
            Dim auth As OAuthBase = New OAuthBase()
            Dim fechaActual As DateTime = DateTime.UtcNow
            Dim content As String = SerializarData(opcion, rucCanales, chasis, motor, codVehiculo, placa, cobertura)
            Dim timestamp = auth.GenerateTimeStamp()
            Dim nonce = auth.GenerateNonce()
            Dim client = New RestClient(API_URL)
            client.Timeout = -1
            Dim request = New RestRequest("", metodo)
            Dim url As Uri = New Uri(API_URL)
            Dim signature = auth.GenerateSignature(url, oauthConsumerKey, oauthConsumerSecret, oauthToken, oauthTokenSecret, httpMethod, timestamp, nonce)
            request.AddHeader("Authorization", "OAuth realm=""" & realm & """, oauth_token=""" & oauthToken & """, oauth_consumer_key=""" & oauthConsumerKey & """," & " oauth_nonce=""" & nonce & """, oauth_timestamp=""" & timestamp & """, oauth_signature_method=""" & HMACSHA256SignatureType & """, oauth_version=""" & OAuthVersion & """, oauth_signature=""" & signature & """")
            request.AddHeader("Content-Type", "application/json")
            request.AddParameter("application/json", content, ParameterType.RequestBody)
            Dim response As IRestResponse = client.Execute(request)
            If response.StatusCode = HttpStatusCode.OK Then
                Dim responseBody As String = response.Content
                Dim jsonObject As JObject = JObject.Parse(responseBody)
                Dim errorMessage As Object
                Dim bienObject As JObject
                Dim producto As JArray
                Dim bienToken As JToken = jsonObject("bien")
                HttpContext.Current.Session("errores") = Nothing
                If jsonObject.ContainsKey("errores") Then
                    HttpContext.Current.Session("errores") = jsonObject("errores").ToString()
                End If

                If jsonObject("bien") IsNot Nothing AndAlso bienToken.Type <> JTokenType.Null Then
                    bienObject = CType(jsonObject("bien"), JObject)
                Else
                    bienObject = Nothing
                End If

                If jsonObject("producto") IsNot Nothing Then
                    producto = TryCast(jsonObject("producto"), JArray)
                Else
                    producto = Nothing
                End If

                ' Convertir dataObject a JSON string y agregarlo a la lista
                If bienObject IsNot Nothing Then
                    resultados.Add(bienObject.ToString())
                    'Else
                    '    resultados.Add("Sin resultados")
                End If

                ' Convertir cada objeto en detalle a JSON string y agregarlo a la lista
                If producto IsNot Nothing Then
                    For Each item As JObject In producto
                        resultados.Add(item.ToString())
                    Next
                    'Else
                    '    resultados.Add("Sin resultados")
                End If
            Else
            End If

            Return resultados

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            'Return New List(Of String) From {"{}"}
        End Try
    End Function

    Shared Function SerializarData(opcion As String, rucCanales As String, Optional ByVal chasis As String = "", Optional ByVal motor As String = "", Optional ByVal codVehiculo As String = "", Optional ByVal placa As String = "", Optional ByVal cobertura As String = "") As String
        ' Serialize the data into a JSON string format
        Dim serializedData As String = $"
                    {{""opcion"":""{opcion}"",
                      ""username"":""{HttpContext.Current.Session("user_session")}"",
                      ""ruc_canales"":""{rucCanales}"",
                      ""chasis"":""{chasis}"",
                      ""motor"":""{motor}"",
                      ""id_vehiculo"":""{codVehiculo}"",
                      ""placa"":""{placa}"",
                      ""cobertura"":""{cobertura}"",
                      ""certificado"":""{HttpContext.Current.Session("cmbo")}"",
                      ""user_certificado"":""{HttpContext.Current.Session("user_certificado")}""
                    }}"
        Return serializedData
    End Function

End Class
