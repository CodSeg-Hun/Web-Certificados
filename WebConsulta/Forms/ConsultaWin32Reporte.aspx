<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConsultaWin32Reporte.aspx.vb" Inherits="WebConsulta.ConsultaWin32Reporte" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Certificado Syshunter</title>
        <link href="../Styles/certificadowin32.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div class="book">
                <div class="page">
                    <div class="subpage">
                        <div id="content">
                            <div class="row_text02">
                                CONT. No. <strong><asp:Label ID="lbloswin32" runat="server"></asp:Label></strong>
                            </div>
                            <div class="row_text01">
                            </div>
                            <div class="row_idtf01">
                                C. V.<asp:Label ID="lblvehiculowin32" runat="server"></asp:Label>
                            </div>
                            <div class="row_text01">
                                Certifica que el Sr. (a).<strong><asp:Label ID="lblnombrewin23" runat="server"></asp:Label></strong>
                            </div>
                            <div class="row_text01">
                                Ha adquirido los siguientes sistemas:
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
                                            <telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column" HeaderText="PRODUCTO" 
                                                UniqueName="PRODUCTO" DataField="PRODUCTO">
                                                <FooterStyle BorderWidth="350px" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="350px" />
                                                <ItemStyle Width="350px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column" HeaderText="COBERTURA" 
                                                UniqueName="COBERTURA" DataFormatString="{0:dd/MMMM/yyyy}" DataField="COBERTURA">
                                                <FooterStyle BorderWidth="130px" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="130px" />
                                                <ItemStyle Width="130px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn AllowFiltering="False" FilterControlAltText="Filter column column"  DataFormatString="{0:$###,##0.00}" 
                                                HeaderText="P.V.P. INCLUIDO IVA" UniqueName="PRECIO_PRODUCTO" DataType="System.Decimal" DataField="PRECIO_PRODUCTO">
                                                <FooterStyle BorderWidth="130px" />
                                                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="130px" />
                                                <ItemStyle Width="130px" HorizontalAlign="Right" />
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
                            <div class="row_text02">
                                Estos sistemas se encuentran instalados en el (Vehículo/Barco/Avión/Cajero) con la siguientes características:
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
                            <div class="row_secc01">
                                <asp:Image ID="imgqrgenerator" runat="server" />
                            </div>
                            <div class="row_text01">
                            </div>
                            <div class="row_text01">
                                <strong>
                                    <asp:Label ID="lblfechahoraemision" runat="server"></asp:Label>
                                </strong>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                <Scripts>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js"></asp:ScriptReference>
                    <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js"></asp:ScriptReference>
                </Scripts>
            </telerik:RadScriptManager>
        </form>
    </body>
</html>
