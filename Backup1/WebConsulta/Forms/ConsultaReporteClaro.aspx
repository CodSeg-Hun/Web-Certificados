<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConsultaReporteClaro.aspx.vb" Inherits="WebConsulta.ConsultaReporteClaro" EnableEventValidation = "false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>Consulta Web</title>
        <script type="text/javascript">
            function impre(num) {
                document.getElementById(num).className = "ver";
                print();
                document.getElementById(num).className = "nover";

            }
        </script>
        <link href="../Styles/certificado.css" rel="stylesheet" type="text/css" />
        <style type="text/css">


p.p15{
margin-bottom:0pt;
margin-top:0pt;
text-align:left;
font-size:9,0000pt; font-family:'Arial MT'; }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div style="width: 40px; height: 30px; float: left;">
                <input type="button" onclick="impre('container');return false" style="background-image: url('../Images/icons/icon_print.png');
                    background-repeat: no-repeat; background-position: center center; height: 29px; width: 35px; background-color: #FFFFFF; border: none;" />
                <%-- <asp:Button ID="Button1" runat="server" Text="." onclick="GeneraPDF_click" style="background-image: url('../Images/icons/page-over.png')"
                    width="35px" height="30px" ToolTip="Genera archivo PDF" />  --%>
            </div>
            <div id="container">
                <div id="banner">
                    <%--   <div style="width: 450px; height: 30px; float: left; margin-top: -5px;">
                        <h2>
                            <asp:Label ID="lbltitulodetfljcja" runat="server" Text=""></asp:Label>
                        </h2>
                    </div>--%>
                    <asp:Image ID="Image1" runat="server" 
                        ImageUrl="~/Images/background_banner_certificado_conecel.jpg" />
                    
                </div>
                <div id="content">
                    <%--<div id="printablediv" style="margin-top: 10px; height: 355px;">--%>
                    <div class="row_text02">
                        <h6>
                            <asp:Label ID="lbltitulodetfljcja" runat="server" Text=""></asp:Label>
                        </h6>
                    </div>
                    <div class="row_text02"></div>
                    <div class="row_text01">
                        
                    </div> 
                    <div class="row_idtf01">
                        C. V.<asp:Label ID="Vehiculo" runat="server"></asp:Label>
                    </div>
                    <div class="row_text01" runat="server" id="textcabecera">
                        Certifica que el Sr. (a). <strong><asp:Label ID="Nombre" runat="server"></asp:Label></strong>
                    </div>
                    <div class="row_text01" runat="server" id="textcabecera01"></div>
                    <div class="row_text01" runat="server" id="tex01">
                        Ha contratado el producto:
                    </div>
                    <div class="row_secc01" runat="server" id="grid">
                        <%--<div class="row_detl01_title">
                            PRODUCTO</div>
                        <div class="row_detl02_title">
                            COBERTURA</div>
                        <div class="row_detl02_title">
                            P.V.P. + IVA</div>
                            <div class="row_detl01_desc">
                            <asp:Label ID="Producto" runat="server"></asp:Label>
                        </div>
                        <div class="row_detl02_desc">
                            <asp:Label ID="Cobertura" runat="server"></asp:Label>
                        </div>
                        <div class="row_detl02_desc">
                            <asp:Label ID="precioproducto" runat="server"></asp:Label>
                        </div>--%>
                        <telerik:RadGrid ID="grdproductosdetalle" runat="server" CellSpacing="0" Culture="es-ES" GridLines="None" Width="670px" Skin="Transparent">
                            <MasterTableView AutoGenerateColumns="False">
                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="PRODUCTO" UniqueName="PRODUCTO" DataField="PRODUCTO">
                                        <FooterStyle BorderWidth="420px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="420px" />
                                        <ItemStyle Width="420px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="VIGENCIA DEL SERVICIO*" UniqueName="COBERTURA_INI" DataFormatString="{0:dd/MMMM/yyyy}"
                                        DataField="COBERTURA_INI">
                                        <FooterStyle BorderWidth="170px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="170px" />
                                        <ItemStyle Width="170px" />
                                    </telerik:GridBoundColumn>
                                 
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                            </MasterTableView>
                            <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                    </div>
                    <%--<div class="row_text02" runat="server" id="tex02">
                        <p class="p15" 
                            style="margin-left:49,5000pt; margin-bottom:0pt; margin-top:0pt; ">
                            <span style="mso-spacerun:'yes'; font-size:9,0000pt; font-family:'Arial MT'; ">
                            En&nbsp;el&nbsp;vehículo&nbsp;con&nbsp;las&nbsp;siguientes&nbsp;características:<o:p></o:p></span></p>
                    </div>--%>
                    <div class="row_text02" runat="server" id="tex02">
                        <%--Estos sistemas se encuentran instalados en el (vehículo o embarcación) con la siguientes características:--%>
                        *La vigencia del servicio se contará desde la instalación del equipo. 
                    </div>
                    <div class="row_text02" runat="server" id="tex05">
                         Con Orden de Instalación en el vehículo:
                    </div>
                   <%-- <div class="row_text02" runat="server" id="tex03">
                        
                    </div>--%>
                    <div class="row_text02" runat="server" id="tex04">
                        
                    </div>
                    <%--            <div class="row_text02">
                    </div>--%>
                    <div class="row_detl03_title">
                        MARCA
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Marca" runat="server"></asp:Label>
                    </div>
                    <div class="row_detl03_title">
                        AÑO
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Anio" runat="server"></asp:Label>
                    </div>
                    <div class="row_detl03_title">
                        MODELO
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Modelo" runat="server"></asp:Label>
                    </div>
                    <div class="row_detl03_title">
                        PLACA
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Placa" runat="server"></asp:Label>
                    </div>
                    <div class="row_detl03_title">
                        TIPO
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Tipo" runat="server"></asp:Label>
                    </div>
                    <div class="row_detl03_title">
                        CHASIS
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Chasis" runat="server"></asp:Label>
                    </div>
                    <div class="row_detl03_title">
                        COLOR
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Color" runat="server"></asp:Label>
                    </div>
                    <div class="row_detl03_title">
                        MOTOR
                    </div>
                    <div class="row_detl03_desc">
                        <asp:Label ID="Motor" runat="server"></asp:Label>
                    </div>
                    <div class="row_text02" runat="server" id="tex06">
                         <center>El presente certificado, no constituye confirmación de instalación del equipo.</center>
                    </div>
                    <div class="row_secc01">
                       <br />
                       El cliente podrá monitorear su vehículo a través de la app Claro Flotas Powered by Hunter y/o página web https://www.flotasmonitoreo.claro.com.ec/Artemis
                       <br /><br />
                       El cliente podrá utilizar únicamente las funcionalidades del sistema de monitoreo habilitadas durante la instalación.
                    </div>
                    <div class="row_text01">
                    </div>
                    <div class="row_text01">
                        <strong>
                            <asp:Label ID="lblfechahoraemision" runat="server"></asp:Label>
                        </strong>
                    </div>
                    <div id="footer">
                        <%--<asp:Image ID="Image2" runat="server" ImageUrl="~/Images/background_footer_certificado.jpg" />--%>
                    </div>
                </div>
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
            </div>
        </form>
    </body>
</html>
