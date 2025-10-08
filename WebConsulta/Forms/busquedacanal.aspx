<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="busquedacanal.aspx.vb" Inherits="WebConsulta.busquedacanal" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title></title>
        <meta http-equiv="x-ua-compatible" content="IE=9"/>
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <link href="../Styles/estilomodal.css" rel="stylesheet" type="text/css" />
        <link href="../Styles/estilogrid.css" rel="stylesheet" type="text/css" />
        <style type="text/css">
            .RadButton.rbImageButton
            {
                border: 0 none;
                outline: 0 none;
            }
            .RadButton.rbImageButton
            {
                border: 0 none;
                outline: 0 none;
            }
            .RadButton.rbImageButton
            {
                border: 0 none;
                outline: 0 none;
            }
            .RadButton.rbImageButton
            {
                border: 0 none;
                outline: 0 none;
            }
            .RadButton.rbImageButton
            {
                border: 0 none;
                outline: 0 none;
            }
            .RadButton_Default
            {
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
                font-size: 12px;
            }
            .rbImageButton
            {
                position: relative;
                display: inline-block;
                cursor: pointer;
                text-decoration: none;
                text-align: center;
            }
            .RadButton
            {
                cursor: pointer;
            }
            .RadButton
            {
                font-size: 12px;
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
            }
            .RadButton_Default
            {
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
                font-size: 12px;
            }
            .rbImageButton
            {
                position: relative;
                display: inline-block;
                cursor: pointer;
                text-decoration: none;
                text-align: center;
            }
            .RadButton
            {
                cursor: pointer;
            }
            .RadButton
            {
                font-size: 12px;
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
            }
            .RadButton_Default
            {
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
                font-size: 12px;
            }
            .rbImageButton
            {
                position: relative;
                display: inline-block;
                cursor: pointer;
                text-decoration: none;
                text-align: center;
            }
            .RadButton
            {
                cursor: pointer;
            }
            .RadButton
            {
                font-size: 12px;
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
            }
            .RadButton_Default
            {
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
                font-size: 12px;
            }
            .rbImageButton
            {
                position: relative;
                display: inline-block;
                cursor: pointer;
                text-decoration: none;
                text-align: center;
            }
            .RadButton
            {
                cursor: pointer;
            }
            .RadButton
            {
                font-size: 12px;
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
            }
            .rbImageButton
            {
                position: relative;
                display: inline-block;
                cursor: pointer;
                text-decoration: none;
                text-align: center;
            }
            .RadButton
            {
                cursor: pointer;
            }
            .RadButton
            {
                font-size: 12px;
                font-family: "Segoe UI" ,Arial,Helvetica,sans-serif;
            }
            .rbHideElement
            {
                display: none;
                width: 0 !important;
                height: 0 !important;
                overflow: hidden !important;
            }
            .rbText
            {
                display: inline-block;
            }
            .rbHideElement
            {
                display: none;
                width: 0 !important;
                height: 0 !important;
                overflow: hidden !important;
            }
            .rbText
            {
                display: inline-block;
            }
            .rbHideElement
            {
                display: none;
                width: 0 !important;
                height: 0 !important;
                overflow: hidden !important;
            }
            .rbText
            {
                display: inline-block;
            }
            .rbHideElement
            {
                display: none;
                width: 0 !important;
                height: 0 !important;
                overflow: hidden !important;
            }
            .rbText
            {
                display: inline-block;
            }
            .rbHideElement
            {
                display: none;
                width: 0 !important;
                height: 0 !important;
                overflow: hidden !important;
            }
            .rbText
            {
                display: inline-block;
            }
        </style>
    </head>
    <body class="body">
        <form id="form1" method="post" runat="server">
            <div class="contenedor">
                <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
                </telerik:RadScriptManager>
                <script type="text/javascript">
                    function GetRadWindow() {
                        var oWindow = null;
                        if (window.radWindow) oWindow = window.radWindow;
                        else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                        return oWindow;
                    }
                    function AdjustRadWidow() {
                        var oWindow = GetRadWindow();
                        setTimeout(function () { oWindow.autoSize(true); if ($telerik.isChrome || $telerik.isSafari) ChromeSafariFix(oWindow); }, 500);
                    }

                    function ChromeSafariFix(oWindow) {
                        var iframe = oWindow.get_contentFrame();
                        var body = iframe.contentWindow.document.body;
                        setTimeout(function () {
                            var height = body.scrollHeight;
                            var width = body.scrollWidth;
                            var iframeBounds = $telerik.getBounds(iframe);
                            var heightDelta = height - iframeBounds.height;
                            var widthDelta = width - iframeBounds.width;
                            if (heightDelta > 0) oWindow.set_height(oWindow.get_height() + heightDelta);
                            if (widthDelta > 0) oWindow.set_width(oWindow.get_width() + widthDelta);
                            oWindow.center();
                        }, 310);
                    }
                    function returnParent() {
                        var oWnd = GetRadWindow();
                        oWnd.close();
                    }
                    function returnToParent() {
                        var oArg = new Object();
                        oArg.codigo = "";
                        oArg.nombre = "";
//                        oArg.tipo = "";
                        var grid = $find("<%=rgdconsulta.ClientID %>");
                        var MasterTable = grid.get_masterTableView();
                        var selectedRows = MasterTable.get_selectedItems();
                        for (var i = 0; i < selectedRows.length; i++) {
                            var row = selectedRows[i];
                            var codigo = MasterTable.getCellByColumnUniqueName(row, "RUC")
                            var cliente = MasterTable.getCellByColumnUniqueName(row, "NOMBRE")
//                            var tipo = MasterTable.getCellByColumnUniqueName(row, "CODIGO_TIPO")
                            oArg.codigo = codigo.innerHTML;
                            oArg.nombre = cliente.innerHTML;
//                            oArg.tipo = tipo.innerHTML;
                        }
                        var oWnd = GetRadWindow();
                        if (oArg.codigo) {
                            oWnd.close(oArg);
                        } else {
                            alert("Por favor seleccione un cliente.");
                        }
                    }
                </script>
                <div class="busqueda">
                    <div class="menu">
                        <div class="icono_texto">
                            <telerik:RadTextBox ID="txtbusqueda" runat="server" Height="22px" Width="280px" EmptyMessage="Búsqueda general"
                                CssClass="content_border">
                            </telerik:RadTextBox>
                        </div>
                        <div class="icono">
                            <telerik:RadButton ID="BtnConsulta" runat="server" Text="Consultar" ForeColor="Black"
                                Style="top: 0px; left: 0px; height: 28px; width: 28px" ToolTip="Consulta información del cliente">
                                <Image IsBackgroundImage="False" ImageUrl="../images/icons/search28x28.png" />
                            </telerik:RadButton>
                        </div>
                        <div class="icono">
                            <telerik:RadButton ID="BtnAceptar" runat="server" Text="Aceptar" ForeColor="Black" ToolTip="Selecciona cliente"
                                Style="top: 0px; left: 0px; height: 28px; width: 28px" OnClientClicked="returnToParent" AutoPostBack="False">
                                <Image IsBackgroundImage="False" ImageUrl="../images/icons/check28x28.png" />
                            </telerik:RadButton>
                        </div>
                        <div class="icono">
                            <telerik:RadButton ID="BtnClose" runat="server" Text="Cerrar" ForeColor="Black" ToolTip="Cerrar pantalla" 
                                Style="top: 0px; left: 0px; height: 28px; width: 28px" OnClientClicked="returnParent" AutoPostBack="False">
                                <Image IsBackgroundImage="False" ImageUrl="../images/icons/turnoffwin28x28.jpg" />
                            </telerik:RadButton>
                        </div>
                    </div>
                    <div class="data">
                        <telerik:RadGrid ID="rgdconsulta" runat="server" CellSpacing="0" Culture="es-ES"
                            GridLines="None" AutoGenerateColumns="False" Height="300px" Width="630px" AllowAutomaticUpdates="True"
                            Skin="MyCustomSkin" EnableEmbeddedSkins="false">
                            <ClientSettings EnableRowHoverStyle="true">
                                <Selecting AllowRowSelect="True" />
                                <Scrolling AllowScroll="True" UseStaticHeaders="True"></Scrolling>
                            </ClientSettings>
                            <MasterTableView>
                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                                    <HeaderStyle Width="10px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                                    <HeaderStyle Width="10px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="RUC" HeaderText="Id cliente" UniqueName="RUC">
                                        <FooterStyle Width="120px" CssClass="estilogridcontrol" />
                                        <HeaderStyle Font-Bold="True" Width="120px" CssClass="estilogridcontrol" />
                                        <ItemStyle Width="120px" CssClass="estilogridcontrol" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NOMBRE" HeaderText="Cliente" UniqueName="NOMBRE">
                                        <FooterStyle Width="240px" CssClass="estilogridcontrol" />
                                        <HeaderStyle Font-Bold="True" Width="240px" CssClass="estilogridcontrol" />
                                        <ItemStyle Width="240px" CssClass="estilogridcontrol" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_COMPANIA" HeaderText="Tipo" UniqueName="TIPO_COMPANIA">
                                        <FooterStyle Width="160px" CssClass="estilogridcontrol" />
                                        <HeaderStyle Font-Bold="True" Width="160px" CssClass="estilogridcontrol" />
                                        <ItemStyle Width="160px" CssClass="estilogridcontrol" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ESTADO" HeaderText="Estado" UniqueName="ESTADO">
                                        <FooterStyle Width="100px" CssClass="estilogridcontrol" />
                                        <HeaderStyle Font-Bold="True" Width="100px" CssClass="estilogridcontrol" />
                                        <ItemStyle Width="100px" CssClass="estilogridcontrol" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="CODIGO_TIPO" HeaderText="Estado" UniqueName="CODIGO_TIPO">
                                        <FooterStyle Width="1px" CssClass="estilogridcontrol" />
                                        <HeaderStyle Font-Bold="True" Width="1px" CssClass="estilogridcontrol" />
                                        <ItemStyle Width="1px" CssClass="estilogridcontrol" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
            <telerik:RadNotification ID="rntMensajes" runat="server" Animation="Fade" Position="Center"
                EnableRoundedCorners="True" Overlay="True">
            </telerik:RadNotification>
        </form>
    </body>
</html>