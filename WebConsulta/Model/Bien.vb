Imports Newtonsoft.Json
Public Class Bien

    <JsonProperty("id_cliente")> '1890010705001
    Public Property IdCliente As String

    <JsonProperty("cliente")> '"AMBACAR CIA LTDA"
    Public Property Cliente As String

    <JsonProperty("id_vehiculo")> '1001169589
    Public Property IdVehiculo As String

    <JsonProperty("placa")> 'S/P
    Public Property Placa As String

    <JsonProperty("marca")> 'GWM
    Public Property Marca As String

    <JsonProperty("modelo")> 'POER AC 2.0 CD 4X2 TM DIESEL
    Public Property Modelo As String

    <JsonProperty("tipo")> ' AUTO
    Public Property Tipo As String

    <JsonProperty("color")> ' BLANCO
    Public Property Color As String

    <JsonProperty("motor")> 'GW4D20M245A6014819
    Public Property Motor As String

    <JsonProperty("chasis")> '8L4CBF192SC000419
    Public Property Chasis As String

    <JsonProperty("anio")> '2024
    Public Property Anio As String

    <JsonProperty("estado")> 'ACTIVO
    Public Property Estado As String

    <JsonProperty("concesionario")> 'AMBACAR CIA LTDA
    Public Property Concesionario As String

    <JsonProperty("financiera")> 'SIN BANCO / FINANCIERA
    Public Property Financiera As String

    <JsonProperty("aseguradora")> 'SIN ASEGURADORA
    Public Property Aseguradora As String

    <JsonProperty("camb_prop_estado")> '1
    Public Property CamPropEstado As String

    <JsonProperty("estado_rei")> '0
    Public Property EstadoRei As String

    <JsonProperty("codigo_convenio")> '027
    Public Property CodigoConvenio As String
End Class
