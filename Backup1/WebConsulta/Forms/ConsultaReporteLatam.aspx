<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConsultaReporteLatam.aspx.vb" Inherits="WebConsulta.ConsultaReporteLatam" EnableEventValidation = "false" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Consulta Reporte Latam</title>
        <script type="text/javascript">
            function impre(num) {
                document.getElementById(num).className = "ver";
                print();
                document.getElementById(num).className = "nover";

            }
        </script>
        <link href="../Styles/certificado.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div style="width: 40px; height: 30px; float: left;">
                <input type="button" onclick="impre('container');return false" style="background-image: url('../Images/icons/icon_print.png');
                    background-repeat: no-repeat; background-position: center center; height: 29px; width: 35px; background-color: #FFFFFF; border: none;" />
            </div>
            <div id="container">
                <div id="banner">
                    <asp:Image ID="Image1" runat="server" ImageUrl="https://www.hunteronline.com.ec/IMGCOTIZADORWEB/Imagenescampanias/LogoCabeceraMotorfy.png" />
                    <div></div>
                    <div></div>
                    <div style="width: 450px; height: 50px; float: left; margin-top: -5px; margin-left: 180px;">
                        <h6>
                            <asp:Label ID="lbltitulodetfljcja" runat="server" Text=""></asp:Label>
                        </h6>
                    </div>
                </div>
                <div id="content">
                    <div class="row_text02"></div>
                    <div class="row_text01">
                        <asp:DropDownList ID="cboBarcodeType" runat="server" Visible="False">
                            <asp:ListItem Selected="True">QR Code</asp:ListItem>
                            <asp:ListItem>Data Matrix</asp:ListItem>
                            <asp:ListItem>PDF417</asp:ListItem>
                            <asp:ListItem>Aztec</asp:ListItem>
                            <asp:ListItem>Bookland/ISBN</asp:ListItem>
                            <asp:ListItem>Codabar</asp:ListItem>
                            <asp:ListItem>Code 11</asp:ListItem>
                            <asp:ListItem>Code 128</asp:ListItem>
                            <asp:ListItem>Code 128-A</asp:ListItem>
                            <asp:ListItem>Code 128-B</asp:ListItem>
                            <asp:ListItem>Code 128-C</asp:ListItem>
                            <asp:ListItem>Code 39</asp:ListItem>
                            <asp:ListItem>Code 39 Extended</asp:ListItem>
                            <asp:ListItem>Code 93</asp:ListItem>
                            <asp:ListItem>EAN-8</asp:ListItem>
                            <asp:ListItem>EAN-13</asp:ListItem>
                            <asp:ListItem>FIM</asp:ListItem>
                            <asp:ListItem>Interleaved 2 of 5</asp:ListItem>
                            <asp:ListItem>ITF-14</asp:ListItem>
                            <asp:ListItem>LOGMARS</asp:ListItem>
                            <asp:ListItem>MSI 2 Mod 10</asp:ListItem>
                            <asp:ListItem>MSI Mod 10</asp:ListItem>
                            <asp:ListItem>MSI Mod 11</asp:ListItem>
                            <asp:ListItem>MSI Mod 11 Mod 10</asp:ListItem>
                            <asp:ListItem>Plessey</asp:ListItem>
                            <asp:ListItem>PostNet</asp:ListItem>
                            <asp:ListItem>Standard 2 of 5</asp:ListItem>
                            <asp:ListItem>Telepen</asp:ListItem>
                            <asp:ListItem>UPC 2 Digit Ext.</asp:ListItem>
                            <asp:ListItem>UPC 5 Digit Ext.</asp:ListItem>
                            <asp:ListItem>UPC-A</asp:ListItem>
                            <asp:ListItem>UPC-E</asp:ListItem>
                        </asp:DropDownList>
                    </div> 
                    <div class="row_idtf01">
                        C. V.<asp:Label ID="Vehiculo" runat="server"></asp:Label>
                    </div>
                    <div class="row_text01">
                        Certifica que el Sr. (a). <strong><asp:Label ID="Nombre" runat="server"></asp:Label></strong>
                    </div>
                    <div class="row_text01">
                        Ha contratado el producto:
                    </div>
                    <div class="row_secc01">
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
                                        <FooterStyle BorderWidth="400px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="400px" />
                                        <ItemStyle Width="400px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="VIGENCIA DEL SERVICIO*" UniqueName="COBERTURA" DataFormatString="{0:dd/MMMM/yyyy}"
                                        DataField="COBERTURA">
                                        <FooterStyle BorderWidth="200px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="200px" />
                                        <ItemStyle Width="200px" />
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
                     <div class="row_text02" runat="server" id="tex02">
                        <%--Estos sistemas se encuentran instalados en el (vehículo o embarcación) con la siguientes características:--%>
                        *La vigencia del servicio se contará desde la instalación del equipo. 
                    </div>
                    <div class="row_text02">
                        Con Orden de Instalación en el vehículo:
                    </div>
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
                    <div class="row_secc01"  >
                        <div class="row_detl03_desc"></div>
                        <div class="row_detl03_desc"></div>
                        <asp:Image ID="imgqrgenerator" runat="server"  Height="76px" />
                    </div>
                     <div class="row_text02" runat="server" id="tex07">
                       Los términos y condiciones del servicio pueden ser consultadas en nuestra página web
                    </div>  
                    <div class="row_text01">
                        <strong>
                            <asp:Label ID="lblfechahoraemision" runat="server"></asp:Label>
                        </strong>
                    </div>
                    <div id="footer">
                        <asp:Image ID="Image2" runat="server" style="margin-left: -100px;" 
                        ImageUrl="https://www.hunteronline.com.ec/IMGCOTIZADORWEB/Imagenescampanias/LogoPieMotorfy.png" />
                    </div>
                </div>
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
            </div>
        </form>
    </body>
</html>
