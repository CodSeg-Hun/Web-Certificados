<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="ConsultaGrafico.aspx.vb" Inherits="WebConsulta.ConsultaGrafico" %>
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
                   

                    <telerik:RadButton ID="btn_buscar" runat="server" Width="40px" Height="22px"
                        Text="..." HoveredCssClass="goButtonClassHov" ForeColor="White" 
                        TabIndex="2"  OnClientClicked="openWin" AutoPostBack="False" >
                        <Image ImageUrl="../images/BotonNegro.png" HoveredImageUrl="../images/BotonNegro.png"
                            PressedImageUrl="../images/BotonNegro.png" IsBackgroundImage="true">
                        </Image>
                    </telerik:RadButton>
                    
                </div>
               
                <div class="text_maestra_seccion_label">Periodo </div><div class="text_maestra_seccion_label">
                            <telerik:RadComboBox ID="cmb_conceanio" runat="server" Width="100px" Height="150px"></telerik:RadComboBox>
                        </div>
                        <div class="text_maestra_seccion_separador">
                            <telerik:RadComboBox ID="cmb_mesinicial" runat="server" Width="200px" Height="150px"></telerik:RadComboBox>
                        </div>
                        <div class="text_maestra_seccion_control"></div>
                        <div class="text_maestra_seccion_control"></div>

                        <div class="text_maestra_seccion_label"></div>
                        <div class="text_maestra_seccion_label"></div>
                        <div class="text_maestra_seccion_separador">
                            <telerik:RadComboBox ID="cmb_mesfinal" runat="server" Width="200px" Height="150px"></telerik:RadComboBox>
                        </div>
                        <div class="text_maestra_seccion_control"></div>
                        <div class="text_maestra_seccion_control"></div>
                <%--<div class="text_maestra_seccion_label_filter"  >
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
                </div>--%>
                
                
            </div> 
            <div id="divlistas" class="content_detalle" runat="server">
               
            </div>      
        </div>
    </div>
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
    <telerik:RadNotification ID="RnMensajesError" runat="server" Animation="Slide" Height="100px" Position="Center" Width="414px" EnableRoundedCorners="True" EnableShadow="True">
    </telerik:RadNotification>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings> 
            <telerik:AjaxSetting AjaxControlID="RadTabPrincipal">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="previousTabHidden" />
                    <telerik:AjaxUpdatedControl ControlID="RadTabPrincipal" />
                    <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rntMensajes" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadListView1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="myframe" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="txtbusqueda" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="BtnBusquedaAvanzada" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rntMsgSistema" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnBusquedaAvanzada">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divlistas" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadListView1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="rntMsgSistema" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default">
    </telerik:RadAjaxLoadingPanel>
</asp:Content>

