<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="ConsultaConcesionarios1.aspx.vb" Inherits="WebConsulta.ConsultaConcesionarios1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="x-ua-compatible" content="IE=9"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link href="../styles/styleconcesionario.css" rel="stylesheet" type="text/css" />

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="seccion_content_maestra_concesionario">
      <%--  <div class="content_datosrenovacionsubtitulo_bloque">
        </div>--%>
        
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
                    <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnImprimir" runat="server" Text="Imprimir" ForeColor="Black"
                            Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Imprimir" >
                            <Image ImageUrl="../Images/icon_imprimir.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>
                    <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnCorreo" runat="server" Text="EMail" ForeColor="Black"
                            Style="top: 0px; left: 0px; height: 28px; width: 32px" ToolTip="Envia Email" >
                            <Image ImageUrl="../Images/icon_email.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>
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
                     <%-- OnClick="BtnCorreo_Click"--%>
                    
                </div>
                
               <%-- <div class="text_maestra_seccion_control"></div>
                <div class="column04_cell_button"></div>--%>
                <div class="text_maestra_seccion_label">Canal</div>
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
                                    //                                   var tipo = arg.tipo;
                                    var txtcanalID = document.getElementById("<%=txtcanalID.ClientID %>");
                                    var txtcanalNombre = document.getElementById("<%=txtcanal.ClientID %>");
                                    //                                   var txttipo = document.getElementById("<%=cmb_canal.ClientID %>");
                                    txtcanalID.value = codigo;
                                    txtcanalNombre.value = nombre;
                                    //                                   txttipo.value = tipo;
                                }
                            }
                        </script>
                    </telerik:RadCodeBlock>
                  <%--  <telerik:RadButton ID="btn_buscar" runat="server" Text="" Style="top: 0px;
                        left: 0px; height: 20px; width: 20px" OnClientClicked="openWin">
                        <Image ImageUrl="../Images/icons/Lupa20x20.png" HoveredImageUrl="../Images/icons/Lupa20x20.png"
                            PressedImageUrl="../Images/icons/Lupa20x20.png" IsBackgroundImage="true">
                        </Image>
                    </telerik:RadButton>--%>

                    <telerik:RadButton ID="btn_buscar" runat="server" Width="40px" Height="22px"
                        Text="..." HoveredCssClass="goButtonClassHov" ForeColor="White" 
                        TabIndex="2"  OnClientClicked="openWin" AutoPostBack="False" >
                        <Image ImageUrl="../images/BotonNegro.png" HoveredImageUrl="../images/BotonNegro.png"
                            PressedImageUrl="../images/BotonNegro.png" IsBackgroundImage="true">
                        </Image>
                    </telerik:RadButton>
                    
                </div>
               
                <div class="text_maestra_seccion_label">Periodo</div>
                <div class="text_maestra_seccion_separador3">
                    <telerik:RadComboBox ID="cmb_anio" runat="server" Width="100px" Height="150px"></telerik:RadComboBox>
                     
                </div>
                <div class="text_maestra_seccion_separador5">
                    <telerik:RadComboBox ID="cmb_mes" runat="server" Width="200px" Height="150px"></telerik:RadComboBox>
                </div>
                <div class="text_maestra_seccion_label_filter"  >
                        Filtro de Datos
                </div>
                <div class="sec_RadTextBox">
                    <telerik:RadTextBox ID="txtbusqueda" runat="server" Height="22px" Width="280px" EmptyMessage="Búsqueda general" CssClass="content_border" />
                </div>
                <div class="text_maestra_seccion2_control1"  >
                    <div class="column04_cell_button_filter">
                        <telerik:RadButton ID="BtnBusquedaAvanzada" runat="server" Text="Filtrar" ForeColor="Black" Style="top: 2px; left: 5px;" 
                            ToolTip="Consulta de Filtro" Skin="Default" Height="20px" Width="20px">
                            <Image IsBackgroundImage="False" ImageUrl="../Images/icons/icon_filtro.png" />
                        </telerik:RadButton>
                    </div>
                    <div class="column04_cell_button_filter">
                        <telerik:RadButton ID="BtnFilterNo" runat="server" Text="Quitar el Filtro" ForeColor="Black" Style="top: 2px; left: 5px;" 
                            ToolTip="Consulta de Filtro" Skin="Default" Height="20px" Width="20px">
                            <Image IsBackgroundImage="False" ImageUrl="../Images/icons/icon_filtro_no.png" />
                        </telerik:RadButton>
                    </div>
                </div>
                
                
            </div> 
            <div id="divlistas" class="content_detalle" runat="server">
               <%-- <telerik:RadFilter RenderMode="Lightweight" runat="server" ID="RadFilter1" FilterContainerID="RadListView1" Visible="false"/>--%>
                <telerik:RadListView ID="RadListView1" runat="server" AllowPaging="True" PageSize="5" DataKeyNames="CODIGO">
                    <ItemTemplate>
                        <div class="div_principal_listview">
                            <asp:LinkButton ID="LinkButton1" CssClass="selectedButtons" runat="server" CommandName="Select">
                                <div class="content_principal_div">
                                    <div id="Div1" class="div_secundario_principal" runat="server">
                                        <div id="Div2" class="div_secundario_detalle" runat="server">
                                            <asp:Label ID="Label1" runat="server" Text="Label">Cliente: </asp:Label>
                                            <asp:Label ID="LblCodigo" runat="server" Text='<%# Bind("CLIENTE_ID") %>'  CssClass="div_secundario_color"/>
                                            <asp:Label ID="LblCliente" runat="server" Text='<%# Bind("CLIENTE") %>' CssClass="div_secundario_color" />
                                            <asp:Label ID="Label14" runat="server" Text=" | " />
                                            <asp:Label ID="Label10" runat="server" Text="Label">Orden Servicio: </asp:Label>
                                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("ORDEN_SERVICIO") %>' CssClass="div_secundario_color" />
                                            <asp:Label ID="Label13" runat="server" Text=" | " />
                                            <asp:Label ID="Label18" runat="server" Text="Label">Facturado: </asp:Label>
                                            <asp:Label ID="Label19" runat="server" Text='<%# Eval("FACTURADO") %>' CssClass="div_secundario_color" />
                                            <asp:Label ID="Label20" runat="server" Text=" | " />
                                            <asp:Label ID="Label21" runat="server" Text="Label">Trabajado: </asp:Label>
                                            <asp:Label ID="Label22" runat="server" Text='<%# Eval("TRABAJADO") %>' CssClass="div_secundario_color" />
                                        </div>
                                        <div class="div_secundario_detalle" >
                                            <asp:Label ID="Label16" runat="server" Text="Label">Producto: </asp:Label>
                                            <asp:Label ID="Label17" runat="server" Text='<%# Eval("PRODUCTO") %>' CssClass="div_secundario_color" />
                                            <asp:Label ID="Label9" runat="server" Text=" | " />
                                            <asp:Label ID="Label23" runat="server" Text="Label">Vehículo: </asp:Label>
                                            <asp:Label ID="Label25" runat="server" Text='<%# Bind("CODIGO") %>' CssClass="div_secundario_color" />
                                        </div>
                                        <div class="div_secundario_detalle">
                                            <asp:Label ID="Label7" runat="server" Text="Label">Nombre Completo: </asp:Label>
                                            <asp:Label ID="Label8" runat="server" Text='<%# Eval("VEHICULO") %>' CssClass="div_secundario_color" />
                                            <asp:Label ID="Label24" runat="server" Text=" | " />
                                            <asp:Label ID="Label2" runat="server" Text="Label">Chasis: </asp:Label>
                                            <asp:Label ID="Label6" runat="server" Text='<%# Eval("CHASIS") %>' CssClass="div_secundario_color" />
                                            <asp:Label ID="Label11" runat="server" Text=" | " />
                                            <asp:Label ID="Label3" runat="server" Text="Label">Placa: </asp:Label>
                                            <asp:Label ID="LblFecha" runat="server" Text='<%# Eval("PLACA") %>' CssClass="div_secundario_color" />
                                            <asp:Label ID="Label12" runat="server" Text=" | " />
                                            <asp:Label ID="Label4" runat="server" Text="Label">Año: </asp:Label>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Eval("ANIO") %>' CssClass="div_secundario_color" />
                                        </div>
                                    </div>
                                </div>
                            </asp:LinkButton></div></ItemTemplate><EmptyDataTemplate><div class="empty"> </div></EmptyDataTemplate><LayoutTemplate>
                        <div class="sec_listview">
                            <div class="rlvFloated rlvAutoScroll">
                                <div id="itemPlaceholder" runat="server"></div>
                            </div>
                            <asp:DataPager runat="server" ID="RadDataPager1" PagedControlID="RadListView1" PageSize="5" class="DataPager-group">
                                <Fields>
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="true" ShowPreviousPageButton="true"
                                        ShowNextPageButton="false" ShowLastPageButton="false" FirstPageText="«" PreviousPageText="<"
                                        ButtonCssClass="DataPager_next" />
                                    <asp:NumericPagerField ButtonCount="10" ButtonType="Link" CurrentPageLabelCssClass="DataPager_page"
                                        NumericButtonCssClass="DataPager_next" NextPreviousButtonCssClass="DataPager_next_button" />
                                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="false" ShowPreviousPageButton="false"
                                        ShowNextPageButton="true" ShowLastPageButton="true" NextPageText=">" LastPageText="»"
                                        ButtonCssClass="DataPager_next" />
                                </Fields>
                            </asp:DataPager>
                        </div>
                    </LayoutTemplate>
                </telerik:RadListView>
            </div>      
        </div>
        
    </div>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
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
                    <telerik:AjaxUpdatedControl  ControlID="txtbusqueda" />
                    <telerik:AjaxUpdatedControl  ControlID="BtnBusquedaAvanzada" />
                    <telerik:AjaxUpdatedControl  ControlID="BtnFilterNo" />
                    
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />

                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnBusquedaAvanzada">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtbusqueda" />
                    <telerik:AjaxUpdatedControl ControlID="BtnBusquedaAvanzada" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadListView1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtbusqueda" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnBusquedaAvanzada" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                </UpdatedControls>
            </telerik:AjaxSetting>

            <telerik:AjaxSetting AjaxControlID="BtnCorreo">
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
            </telerik:AjaxSetting>
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
            
            <telerik:AjaxSetting AjaxControlID="BtnFilterNo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

