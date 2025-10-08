<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="ConsultaConcesionarios.aspx.vb" Inherits="WebConsulta.ConsultaConcesionarios" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/styleconcesionario.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="seccion_content_maestra_concesionario">
        <div class="seccion_content_maestra_data">
            <div class="seccion_content_maestra_titulo">
                Consulta de Concesionarios
            </div>
            <div class="seccion_content_maestra_titulo_corner">
            </div>
            <div class="content_filtrar">
                <div class="toolbar_maestra">
                    <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnNuevo" runat="server" Text="Nuevo" ForeColor="Black" Style="top: 0px;
                            left: 0px; height: 32px; width: 32px" ToolTip="Nuevo">
                            <Image ImageUrl="../Images/icon_nuevo.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>
                    <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnConsultar" runat="server" Text="Consultar" ForeColor="Black"
                            Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Consultar">
                            <Image ImageUrl="../Images/icon_buscar.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>
                    <%--<div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnImprimir" runat="server" Text="Imprimir" ForeColor="Black"
                            Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Imprimir" >
                            <Image ImageUrl="../Images/icon_imprimir.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>--%>
                     <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="btnExportar" runat="server" Text="Exportar" ForeColor="Black"
                            Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Exporta el listado al formato de archivo especificado">
                            <Image IsBackgroundImage="False" ImageUrl="../Images/download32x32.png" />
                        </telerik:RadButton>
                     </div>
                   <%-- <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnCorreo" runat="server" Text="EMail" ForeColor="Black"
                            Style="top: 0px; left: 0px; height: 28px; width: 32px" ToolTip="Envia Email" >
                            <Image ImageUrl="../Images/icon_email.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>--%>
                    <div class="text_maestra_seccion_label2" id="titcorreo" runat="server" visible="false">
                        Dirección Correo 
                    </div>
                    <div id="Div3" class="text_maestra_seccion_control3" runat="server">
                        <div  class="text_maestra_seccion1_control1" >
                            <telerik:RadTextBox ID="TxtCorreo" runat="server" Style="text-transform: uppercase;" visible="false"
                                Width="250px" Height="22px" MaxLength="150">
                            </telerik:RadTextBox>
                        </div>
                        <div  class="text_maestra_seccion2_control1" >
                            <telerik:RadButton ID="BtnEnvia" runat="server" Text="Enviar" OnClick="BtnEnviar" visible="false" Width="70px" Height="22px"></telerik:RadButton>
                        </div>
                        <div  class="text_maestra_seccion3_control1" >
                            <telerik:RadButton ID="BtnCancela" runat="server" Text="Cancelar" style="margin-left:2px"
                                Onclick="BntCancelar"  Visible="false" Width="70px" Height="22px">
                            </telerik:RadButton>
                        </div>
                    </div>
                </div>
                <div class="text_maestra_seccion_label_2">Canal</div>
                <div class="text_maestra_seccion_separador">
                    <telerik:RadComboBox ID="cmb_canal" runat="server" Width="200px" Height="150px"></telerik:RadComboBox>
                </div>
                <div class="text_maestra_seccion_separador4">
                    <telerik:RadTextBox ID="txtcanalID" runat="server" Width="180px" Enabled="False"></telerik:RadTextBox>
                </div>
                <div class="text_maestra_seccion_separador">
                    <telerik:RadTextBox ID="txtcanal" runat="server" Width="230px" ReadOnly="True" Enabled="False" />
                </div>
                <div class="column04_cell_button">
                    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                        <script type="text/javascript">
                            function openWin() {
                                var oWnd = radopen("busquedacanal.aspx", "RadWindow4");
                                oWnd.set_modal(true);
                                if (!oWnd.get_modal()) document.documentElement.focus();
                            }

                            function OnClientCloseCanal(oWnd, args) {
                                var arg = args.get_argument();
                               if (arg) {
                                   var codigo = arg.codigo;
                                   var nombre = arg.nombre;
                                   var txtcanalID = document.getElementById("<%=txtcanalID.ClientID %>");
                                   var txtcanalNombre = document.getElementById("<%=txtcanal.ClientID %>");
                                   txtcanalID.value = codigo;
                                   txtcanalNombre.value = nombre;
                                }
                            }
                        </script>
                    </telerik:RadCodeBlock>
                    <telerik:RadButton ID="btn_buscar" runat="server" Width="40px" Height="22px"
                        Text="..." HoveredCssClass="goButtonClassHov" ForeColor="White" 
                        TabIndex="2"  OnClientClicked="openWin" AutoPostBack="False" >
                        <Image ImageUrl="../images/BotonNegro.png" HoveredImageUrl="../images/BotonNegro.png"
                            PressedImageUrl="../images/BotonNegro.png" IsBackgroundImage="true">
                        </Image>
                    </telerik:RadButton>
                </div>
                <div class="column04_cell_button"></div>
                <div class="text_maestra_seccion_label_2">Periodo</div>
                <div class="text_maestra_seccion_separador3">
                    <telerik:RadComboBox ID="cmb_anio" runat="server" Width="100px" Height="150px"></telerik:RadComboBox>
                </div>
                <div class="text_maestra_seccion_separador5">
                    <telerik:RadComboBox ID="cmb_mes" runat="server" Width="200px" Height="150px"></telerik:RadComboBox>
                </div>
            </div> 
            <div  class="grid_maestra" >
                <telerik:RadGrid ID="RadGridDatos" runat="server" AllowMultiRowSelection="True" AutoGenerateColumns="False"  CellSpacing="0" Culture="es-ES"
                        GridLines="None" ShowFooter="True" Width="100%"  AllowFilteringByColumn="True">
                    <GroupingSettings CaseSensitive="False" />
                    <ExportSettings ExportOnlyData="True" IgnorePaging="True">
                        <Pdf AllowCopy="True" PageHeight="297mm" PageWidth="210mm" PaperSize="A4" />
                    </ExportSettings>
                    <ClientSettings>
                        <Selecting AllowRowSelect="True" CellSelectionMode="None" />
                        <Resizing AllowRowResize="false" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ORDEN_SERVICIO" ClientDataKeyNames="ORDEN_SERVICIO">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column">
                            <HeaderStyle Width="20px"></HeaderStyle>
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="CLIENTE" FilterControlAltText="Filter CLIENTE column" HeaderText="Cliente" ReadOnly="True"
                                UniqueName="CLIENTE" AutoPostBackOnFilter="True" FilterControlWidth="260px"
                                  CurrentFilterFunction="Contains" ShowFilterIcon="False" >
                                <FooterStyle Width="300px" />
                                <HeaderStyle Font-Bold="True" Width="300px" />
                                <ItemStyle Width="300px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="VEHICULO" FilterControlAltText="Filter VEHICULO column" HeaderText="Vehículo" ReadOnly="True"
                                UniqueName="VEHICULO" AutoPostBackOnFilter="True" FilterControlWidth="260px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                <FooterStyle Width="300px" />
                                <HeaderStyle Font-Bold="True" Width="300px" />
                                <ItemStyle Width="300px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PRODUCTO" FilterControlAltText="Filter PRODUCTO column" HeaderText="Producto" ReadOnly="True"
                                UniqueName="PRODUCTO" AutoPostBackOnFilter="True" FilterControlWidth="260px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                <FooterStyle Width="300px" />
                                <HeaderStyle Font-Bold="True" Width="300px" />
                                <ItemStyle Width="300px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CHASIS" FilterControlAltText="Filter CHASIS column" HeaderText="Chasis" ReadOnly="True"
                                UniqueName="CHASIS" AutoPostBackOnFilter="True" FilterControlWidth="100px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="False">
                                <FooterStyle Width="140px" />
                                <HeaderStyle Font-Bold="True" Width="140px" />
                                <ItemStyle Width="140px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ANIO"  FilterControlAltText="Filter ANIO column" HeaderText="Año" ReadOnly="True"
                                UniqueName="ANIO" AutoPostBackOnFilter="True" AllowFiltering="False"  >
                                <FooterStyle Width="100px" />
                                <HeaderStyle Font-Bold="True" Width="100px" />
                                <ItemStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PLACA"  FilterControlAltText="Filter PLACA column" HeaderText="Placa" ReadOnly="True"
                                UniqueName="PLACA" AutoPostBackOnFilter="True" AllowFiltering="False"  >
                                <FooterStyle Width="100px" />
                                <HeaderStyle Font-Bold="True" Width="100px" />
                                <ItemStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="FECHA" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA column"
                                HeaderText="Fecha" ReadOnly="True" UniqueName="FECHA" AutoPostBackOnFilter="True" DataType="System.DateTime" AllowFiltering="False" >
                                <FooterStyle Width="120px" />
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="120px" />
                                <ItemStyle Width="120px" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="SUBTOTAL"  FilterControlAltText="Filter SUBTOTAL column" HeaderText="Subtotal" ReadOnly="True"
                                UniqueName="SUBTOTAL" AutoPostBackOnFilter="True" AllowFiltering="False"  DataFormatString="{0:$###,##0.00}" >
                                <FooterStyle Width="100px" />
                                <HeaderStyle Font-Bold="True" Width="100px" />
                                <ItemStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ORDEN_SERVICIO"  FilterControlAltText="Filter ORDEN_SERVICIO column" HeaderText="Orden Servicio" ReadOnly="True"
                                UniqueName="ORDEN_SERVICIO" AutoPostBackOnFilter="True"  FilterControlWidth="80px"
                                CurrentFilterFunction="Contains" ShowFilterIcon="False"   >
                                <FooterStyle Width="120px" />
                                <HeaderStyle Font-Bold="True" Width="120px" />
                                <ItemStyle Width="120px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FACTURADO"  FilterControlAltText="Filter FACTURADO column" HeaderText="Facturado" ReadOnly="True"
                                UniqueName="FACTURADO" AutoPostBackOnFilter="True" AllowFiltering="False"  >
                                <FooterStyle Width="100px" />
                                <HeaderStyle Font-Bold="True" Width="100px" />
                                <ItemStyle Width="100px" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TRABAJADO"  FilterControlAltText="Filter TRABAJADO column" HeaderText="Trabajado" ReadOnly="True"
                                UniqueName="TRABAJADO" AutoPostBackOnFilter="True" AllowFiltering="False"  >
                                <FooterStyle Width="100px" />
                                <HeaderStyle Font-Bold="True" Width="100px" />
                                <ItemStyle Width="100px" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                        </EditFormSettings> 

                        <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>
                    </MasterTableView>

                    <PagerStyle PageSizeControlType="RadComboBox"></PagerStyle>

                    <FilterMenu EnableImageSprites="False"></FilterMenu>
                </telerik:RadGrid>
            </div>      
        </div>
    </div>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js">
            </asp:ScriptReference>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js">
            </asp:ScriptReference>
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false" ReloadOnShow="true" EnableShadow="true" runat="server">
        <Windows>
           <telerik:RadWindow ID="RadWindow4" runat="server" Behaviors="Close" OnClientClose="OnClientCloseCanal" NavigateUrl="busquedacanal.aspx"
                Opacity="100" Title="Seleccione un Canal" CssClass="element_radwindows" AutoSize="True">
           </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadNotification ID="RnMensajesError" runat="server" Animation="Slide" Height="100px" Position="Center" Width="414px" EnableRoundedCorners="True" EnableShadow="True">
    </telerik:RadNotification>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings> 
            <telerik:AjaxSetting AjaxControlID="BtnConsultar">
                <UpdatedControls>
                    <%--<telerik:AjaxUpdatedControl  ControlID="txtbusqueda" />
                    <telerik:AjaxUpdatedControl  ControlID="BtnBusquedaAvanzada" />
                    <telerik:AjaxUpdatedControl  ControlID="BtnFilterNo" />--%>
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                    <%--<telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel1" />--%>
                    <telerik:AjaxUpdatedControl ControlID="RadGridDatos" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridDatos">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridDatos" LoadingPanelID="RadAjaxLoadingPanel1"/>
                </UpdatedControls>
            </telerik:AjaxSetting>
          <%--  <telerik:AjaxSetting AjaxControlID="btnExportar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="BtnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridDatos" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmb_anio" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmb_canal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="cmb_mes" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtcanalID" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtcanal" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <%--<telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel1" />--%>
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%--<telerik:AjaxSetting AjaxControlID="BtnCorreo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCancela" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnCancela">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCancela" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <telerik:AjaxSetting AjaxControlID="BtnEnvia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCancela" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
           <%-- <telerik:AjaxSetting AjaxControlID="BtnFilterNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>
