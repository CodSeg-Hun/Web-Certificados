Public Class conexion


    Dim HMACSHA256SignatureType As String = "HMAC-SHA256"
    Dim OAuthVersion As String = "1.0"
    Dim httpMethodPost = "POST"
    Dim httpMethodGet = "GET"
    Dim httpMethodPut = "PUT"

    ' *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    'Dim oauthConsumerKey = "b61ef7d183239d254c09cfa1051fa362559767dfa52a5b7effa9fb3a78771373"
    'Dim oauthToken = "9c680c18a3da6257c5d61469756e1086c468495349fbb35f5cc9cd9e45581ddc"
    'Dim oauthConsumerSecret = "aed86ff1499f8ab02c760eff7d35723a402e9e20e968311e4b81a37e7fd6041d"
    'Dim oauthTokenSecret = "2bcacc32e946368f417895e452fb7ff4b7337941ac6ea51f4f8e5eaa767ef721"
    ' *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    'Dim realm = "7451241"
    'Dim realmRuta = "7451241"
    'Dim oauthConsumerKey = "ce63cf522b9a8693077aec7daa3f7f3b11cb7bccd42a109e2db4ddd9f2071f94"
    'Dim oauthToken = "fd0f8baa2fbea3f7f095d8a560e6838ac8008f74381615a113ab025d750f2d80"
    'Dim oauthConsumerSecret = "3875e1166246fda15fb8916ed35f9429bf5e4c65aa884a2d1b2a4d05d0c52228"
    'Dim oauthTokenSecret = "4ecdff3d28ccedb08c69cace6f5efa0e53740a0c5a29dc18eb48c20e9daf0598"
    ' *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*- DESARROLLO *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    'Dim realm = "7451241_SB1"
    'Dim realmRuta = "7451241-sb1"
    'Dim oauthConsumerKey = "34206458e216d79b8544b5c2388e3de205f767762039ed372a6873815bb9e307"
    'Dim oauthToken = "1e3b21c96674cbcaf6b5b65ab4382797fb3b5152f9ae3a32db8476a10226bfed"
    'Dim oauthConsumerSecret = "af16911981a2b78c55d39afdd7ad3868334c38a9c14ff90bb8fcdcbc1c290a18"
    'Dim oauthTokenSecret = "718232eba70a3a8a9cac7a914866fe9f2d1987e26c6afbb191804268864e3b84"
    ' *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*- PRODUCCIÓN *-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*
    Dim realm = "7451241"
    Dim realmRuta = "7451241"
    Dim oauthConsumerKey = "ce63cf522b9a8693077aec7daa3f7f3b11cb7bccd42a109e2db4ddd9f2071f94"
    Dim oauthToken = "fd0f8baa2fbea3f7f095d8a560e6838ac8008f74381615a113ab025d750f2d80"
    Dim oauthConsumerSecret = "3875e1166246fda15fb8916ed35f9429bf5e4c65aa884a2d1b2a4d05d0c52228"
    Dim oauthTokenSecret = "4ecdff3d28ccedb08c69cace6f5efa0e53740a0c5a29dc18eb48c20e9daf0598"

    Function ObtenerDatos(ByVal opcion As String) As String
        Dim cadena As String = ""
        If opcion = "1" Then
            cadena = HMACSHA256SignatureType
        ElseIf opcion = "2" Then
            cadena = OAuthVersion
        ElseIf opcion = "3" Then
            cadena = oauthConsumerKey
        ElseIf opcion = "4" Then
            cadena = oauthToken
        ElseIf opcion = "5" Then
            cadena = oauthConsumerSecret
        ElseIf opcion = "6" Then
            cadena = oauthTokenSecret
        ElseIf opcion = "7" Then
            cadena = httpMethodPost
        ElseIf opcion = "8" Then
            cadena = httpMethodGet
        ElseIf opcion = "9" Then
            cadena = realm
        ElseIf opcion = "10" Then
            cadena = httpMethodPut
        End If
        Return cadena
    End Function

    Function ObtenerRuta(ByVal script As String, ByVal deploy As String) As String
        'Dim cadena As String = ""
        'Dim generalruta As String = "https://7451241.restlets.api.netsuite.com/app/site/hosting/restlet.nl?script="
        Dim generalruta As String = "https://" + realmRuta + ".restlets.api.netsuite.com/app/site/hosting/restlet.nl?script="

        Dim cadena As String = generalruta + script + "&deploy=" + deploy

        Return cadena
    End Function

End Class