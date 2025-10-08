Imports Newtonsoft.Json
Public Class SMSError
    <JsonProperty("codigo_error")>
    Public Property CodigoError As String

    <JsonProperty("mensaje")>
    Public Property Mensaje As String

End Class
