<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConsultaReporte.aspx.vb" Inherits="WebConsulta.ConsultaReporte" EnableEventValidation = "false"%>
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
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/background_banner_certificado3.jpg" />
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
                        <telerik:RadGrid ID="grdproductosdetalle" runat="server" CellSpacing="0" Culture="es-ES" GridLines="None" Width="400px" Skin="Transparent">
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
                                        HeaderText="PRODUCTO" UniqueName="PRODUCTO" DataField="descripcion">
                                        <FooterStyle BorderWidth="420px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="330px" />
                                        <ItemStyle Width="420px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="PRODUCTO" UniqueName="PRODUCTO" DataField="PRODUCTO">
                                        <FooterStyle BorderWidth="420px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="420px" />
                                        <ItemStyle Width="420px" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn Display="False" AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="FECHA DE INSTALACION" UniqueName="FECHAINSTALACION" DataFormatString="{0:dd/MMMM/yyyy}"
                                        DataField="fecha_inicial">
                                        <FooterStyle BorderWidth="170px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="168px" />
                                        <ItemStyle Width="170px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="False" AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="VIGENCIA DEL SERVICIO*" UniqueName="COBERTURA" DataFormatString="{0:dd/MMMM/yyyy}"
                                        DataField="fecha_fin">
                                        <FooterStyle BorderWidth="170px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="170px" />
                                        <ItemStyle Width="170px" />
                                    </telerik:GridBoundColumn>
                                    <%--<telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="VIGENCIA DEL SERVICIO*" UniqueName="COBERTURA" DataFormatString="{0:dd/MMMM/yyyy}"
                                        DataField="COBERTURA">
                                        <FooterStyle BorderWidth="170px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="170px" />
                                        <ItemStyle Width="170px" />
                                    </telerik:GridBoundColumn>--%>
                                   <%-- <telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column"
                                        DataFormatString="{0:$###,##0.00}" HeaderText="P.V.P. INCLUIDO IVA" UniqueName="PRECIO_PRODUCTO"
                                        DataType="System.Decimal" DataField="PRECIO_PRODUCTO">
                                        <FooterStyle BorderWidth="130px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="130px" />
                                        <ItemStyle Width="130px" HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>--%>
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
                    <div class="row_text02" runat="server" id="tex05">
                         Con Orden de Instalación en el vehículo:
                    </div>
                    <div class="row_text02" runat="server" id="tex03">
                        Certifica que se ha realizado la instalación de nuestros productos en el (vehículo o embarcación) con las siguientes características:
                    </div>
                    <div class="row_text02" runat="server" id="tex04">
                        Certifica que se ha realizado la desinstalación de nuestros productos en el (vehículo o embarcación) con las siguientes características:
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
                        <%-- ↓ Eliminado --%>
                        <%--<asp:Image ID="imgqrgenerator" runat="server" Height="76px" />--%>
                    </div>
                     <div class="row_text02" runat="server" id="tex07">
                       Los términos y condiciones del servicio pueden ser consultadas en nuestra página web
                    </div>  
                    <div class="row_text01">
                        <strong>
                            <asp:Label ID="lblfechahoraemision" runat="server"></asp:Label>
                        </strong>
                    </div>
                    <div id="footer" style="width: 720px; overflow: hidden;">
                        <asp:Image ID="Image2" runat="server" 
                            ImageUrl="~/Images/background_footer_certificado3.jpg" 
                            style="width: 802px; object-fit: cover; object-position: top center;" />
                    </div>

                </div>
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
            </div>
        </form>
    </body>
</html>
