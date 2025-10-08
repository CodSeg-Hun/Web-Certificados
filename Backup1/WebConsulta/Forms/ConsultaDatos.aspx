<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="ConsultaDatos.aspx.vb" Inherits="WebConsulta.ConsultaDatos" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="seccion_content_maestra">
        <div class="seccion_content_maestra_titulo">
            Consulta General de Vehículo
        </div>
        <div class="seccion_content_maestra_titulo_corner">
        </div>
        <div class="seccion_content_maestra_data">
            <div class="seccion_toolbar_text">
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
                            Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Imprimir" OnClick="BtnImprimir_Click">
                            <Image ImageUrl="../Images/icon_imprimir.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>
                    <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnCorreo" runat="server" Text="EMail" ForeColor="Black"
                            Style="top: 0px; left: 0px; height: 28px; width: 32px" ToolTip="Envia Email" OnClick="BtnCorreo_Click">
                            <Image ImageUrl="../Images/icon_email.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>
                    <div class="text_maestra_seccion_label2" id="titcorreo" runat="server" visible="false">
                        Dirección Correo 
                    </div>
                    <div class="text_maestra_seccion_control3" runat="server">
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
                <div class="text_maestra">
                    <div class="text_maestra_seccion_label">
                        Chasis
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtChasis" runat="server" MaxLength="30" Style="text-transform: uppercase;" Rows="1"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Motor
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="TxtMotor" runat="server" MaxLength="30" Style="text-transform: uppercase;" Rows="1"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadTextBox ID="txtConCodCliente" runat="server" MaxLength="80" Width="220px" Visible="False"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Cod. Vehículo
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadNumericTextBox ID="txtCodVehiculo" runat="server" MaxLength="10">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="3" NegativePattern="-n" ZeroPattern="n" />
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Color
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConColor" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadTextBox ID="Txtconanio" runat="server" MaxLength="80" Width="220px" Visible="False"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Cliente
                    </div>
                    <div class="text_maestra_seccion_control2">
                        <telerik:RadTextBox ID="TxtConCliente" runat="server" Style="text-transform: uppercase;" Width="490px"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador">
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
                    <div class="text_maestra_seccion_label">
                        Concesionario
                    </div>
                    <div class="text_maestra_seccion_control2">
                        <telerik:RadTextBox ID="txtConConcesionario" runat="server" Style="text-transform: uppercase;" Width="490px"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadTextBox ID="txtCodConvenio" runat="server" MaxLength="80" Width="220px" Visible="False"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Financiera
                    </div>
                    <div class="text_maestra_seccion_control2">
                        <telerik:RadTextBox ID="txtConFinanciera" runat="server" Style="text-transform: uppercase;" Width="490px"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador"></div>
                    <div class="text_maestra_seccion_label">
                        Tipo Reporte
                    </div>
                    <div class="text_maestra_seccion_separador">
                         <telerik:RadComboBox ID="cbm_tipo" runat="server" Width="240px" Height="150px"></telerik:RadComboBox>
                    </div>
                    <div class="text_maestra_seccion_label"></div>
                    <div class="text_maestra_seccion_control"></div>
                    <div class="text_maestra_seccion_control"></div>
                    <div class="text_maestra_seccion_label">
                        Marca
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConMarca" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Modelo
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConModelo" runat="server" Style="text-transform: uppercase;" Width="160px"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador"></div>
                    <div class="text_maestra_seccion_label">
                        Placa
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConPlaca" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Tipo
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConTipo" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador"></div>

                    <div class="text_maestra_seccion_control6">
                        Canales que tienen configurados para la emisión de certificado son:
                        <asp:Label ID="lbl_canales" runat="server"  Font-Size="Small"></asp:Label>
                    </div>
                   
                   
                </div>
                <div class="grid_maestra">
                    <telerik:RadGrid ID="RadGridVehiculo" runat="server" AutoGenerateColumns="False" CellSpacing="0" Culture="es-ES"
                        GridLines="None" Skin="Default" ShowFooter="True" Width="920px">
                        <ClientSettings>
                            <Selecting AllowRowSelect="True" CellSelectionMode="None" />
                            <Resizing AllowRowResize="false" />
                        </ClientSettings>
                        <MasterTableView Width="100%" AutoGenerateColumns="false" EditMode="InPlace" GridLines="None"
                            TableLayout="Auto" ShowGroupFooter="True" AllowNaturalSort="False">
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True" />
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="DESCRIPCION" FilterControlAltText="Filter DESCRIPCION column" HeaderText="Descripción" ReadOnly="True"
                                    UniqueName="DESCRIPCION" AutoPostBackOnFilter="True" FilterControlWidth="300px">
                                    <HeaderStyle Font-Bold="True" Width="300px" />
                                    <ItemStyle Width="300px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ESTADO" FilterControlAltText="Filter ESTADO column" HeaderText="Estado" ReadOnly="True"
                                    UniqueName="ESTADO" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                    <HeaderStyle Font-Bold="True" Width="200px" />
                                    <ItemStyle Width="200px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="FECHA_CONTRATO" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_CONTRATO column"
                                    HeaderText="Fecha Contrato" ReadOnly="True" UniqueName="FECHA_CONTRATO" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                    FilterControlWidth="160px" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="120px" />
                                    <ItemStyle Width="120px" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="FECHA_INICIAL" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_INICIAL column" 
                                    HeaderText="Fecha Inicial" ReadOnly="True" UniqueName="FECHA_INICIAL" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                    FilterControlWidth="160px">
                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" />
                                    <ItemStyle Width="160px" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridDateTimeColumn DataField="FECHA_FIN" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_FIN column" 
                                    HeaderText="Fecha Fin" ReadOnly="True" UniqueName="FECHA_FIN" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                    FilterControlWidth="160px">
                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" />
                                    <ItemStyle Width="160px" />
                                </telerik:GridDateTimeColumn>
                                <telerik:GridBoundColumn DataField="ESTADO_TALLER" FilterControlAltText="Filter ESTADO_TALLER column" HeaderText="Estado Taller" ReadOnly="True"
                                    UniqueName="ESTADO_TALLER" AutoPostBackOnFilter="True" FilterControlWidth="160px" Visible="False">
                                    <HeaderStyle Font-Bold="True" Width="120px" />
                                    <ItemStyle Width="120px" />
                                </telerik:GridBoundColumn>
                                <telerik:GridDateTimeColumn DataField="FECHA_TALLER" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_TALLER column"
                                    HeaderText="Fecha Ing.Taller" ReadOnly="True" UniqueName="FECHA_TALLER" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                    FilterControlWidth="160px" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" />
                                    <ItemStyle Width="160px" />
                                </telerik:GridDateTimeColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column" />
                            </EditFormSettings>
                            <PagerStyle PageButtonCount="10" Mode="NumericPages" />
                        </MasterTableView>
                        <PagerStyle PageButtonCount="10" />
                        <FilterMenu EnableImageSprites="False" />
                    </telerik:RadGrid>
                </div>
                <div class="text_maestra_seccion_label_mensaje_nuevo">
                    <asp:Label ID="mensajetexto" runat="server"></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <telerik:RadScriptManager ID="RadScriptManager2" runat="server"></telerik:RadScriptManager>
    <telerik:RadNotification ID="RnMensajesError" runat="server" Animation="Slide" Height="100px"
        Position="Center" Width="414px" EnableRoundedCorners="True" EnableShadow="True">
    </telerik:RadNotification>
    <telerik:RadNotification ID="rntResultado" runat="server" Animation="Fade" Height="16px"
        Position="Center" Width="450px" ContentIcon="deny" EnableRoundedCorners="True"
        EnableShadow="True" Font-Bold="True" Font-Size="Medium" Opacity="95" TitleIcon="deny"
        ForeColor="Black" Skin="Default" Overlay="True">
        <ContentTemplate>
            <telerik:RadGrid ID="rgdResultado" runat="server" AutoGenerateColumns="False" CellSpacing="0" Culture="es-ES" GridLines="None" Width="350px">
                <MasterTableView>
                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                    <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column" />
                    <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column" />
                    <Columns>
                        <telerik:GridBoundColumn DataField="CODIGO_ID" FilterControlAltText="Filter column column"
                            HeaderText="Codigo" UniqueName="column" Visible="false">
                            <FooterStyle Width="80px" />
                            <HeaderStyle Font-Bold="True" Width="80px" />
                            <ItemStyle Width="80px" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn DataField="MENSAJE" FilterControlAltText="Filter column column"
                            HeaderText="Mensaje" UniqueName="column">
                            <FooterStyle Width="250px" />
                            <HeaderStyle Font-Bold="True" Width="250px" />
                            <ItemStyle Width="250px" />
                        </telerik:GridBoundColumn>
                    </Columns>
                    <EditFormSettings>
                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                        </EditColumn>
                    </EditFormSettings>
                </MasterTableView>
                <FilterMenu EnableImageSprites="False" />
            </telerik:RadGrid>
        </ContentTemplate>
    </telerik:RadNotification>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
    <telerik:RadWindow ID="wdwcertificado" runat="server" Height="850px" NavigateUrl="ConsultaDatos.aspx" Width="850px"></telerik:RadWindow>
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel2">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="BtnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BtnNuevo"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtChasis" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtMotor" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConCodCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtCodVehiculo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtConColor" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtConCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConConcesionario" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConFinanciera" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConMarca" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConModelo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConPlaca" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConTipo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_tipo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="mensajetexto" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCancela" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnImprimir">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_tipo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rntResultado" LoadingPanelID="RadAjaxLoadingPanel2" />
                  
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnConsultar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BtnNuevo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtChasis" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="TxtMotor" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtConCodCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtCodVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConColor" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtConCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConConcesionario" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConFinanciera" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConMarca" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConModelo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConPlaca" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConTipo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridVehiculo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rntResultado" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_tipo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="mensajetexto" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnCorreo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCancela" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rntResultado" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnEnvia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCancela" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="rntResultado" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnCancela">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Simple"></telerik:RadAjaxLoadingPanel>
</asp:Content>
