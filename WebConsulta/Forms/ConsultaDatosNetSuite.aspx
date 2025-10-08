<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="ConsultaDatosNetSuite.aspx.vb" Inherits="WebConsulta.ConsultaDatosNetsuite" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../styles/stylenetsuite.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="seccion_content_maestra">
        <h1>Información del Certificado</h1>
        <div class="acciones">
            <telerik:RadButton ID="BtnNuevo" runat="server" Text="Nuevo" ForeColor="Black"
                Style="height: 32px; width: 32px; display: none;" ToolTip="Nuevo">
                <Image ImageUrl="../Images/icon_nuevo.png" IsBackgroundImage="False" />
            </telerik:RadButton>

            <telerik:RadButton ID="BtnConsultar" runat="server" Text="Consultar" ForeColor="Black"
                Style="height: 32px; width: 32px; display: none;" ToolTip="Consultar">
                <Image ImageUrl="../Images/icon_buscar.png" IsBackgroundImage="False" />
            </telerik:RadButton>

            <telerik:RadButton ID="BtnImprimir" runat="server" Text="Imprimir" ForeColor="Black"
                Style="height: 32px; width: 32px" ToolTip="Imprimir" OnClick="BtnImprimir_Click">
                <Image ImageUrl="../Images/icon_imprimir.png" IsBackgroundImage="False" />
            </telerik:RadButton>

            <telerik:RadButton ID="BtnCorreo" runat="server" Text="EMail" ForeColor="Black"
                Style="height: 32px; width: 32px;" ToolTip="Envia Email" OnClick="BtnCorreo_Click">
                <Image ImageUrl="../Images/icon_email.png" IsBackgroundImage="False" />
            </telerik:RadButton>
        </div>

        <div>
            <asp:DropDownList ID="cboBarcodeType" runat="server" Visible="False" Style="display: none;">
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

            <telerik:RadTextBox ID="txtConCodCliente" runat="server" MaxLength="80" Width="220px" Style="display: none;"></telerik:RadTextBox>
            <telerik:RadTextBox ID="Txtconanio" runat="server" MaxLength="80" Width="220px" Visible="False" Style="display: none;"></telerik:RadTextBox>
            <telerik:RadTextBox ID="txtCodConvenio" runat="server" MaxLength="80" Width="220px" Visible="False" Style="display: none;"></telerik:RadTextBox>


            <div class="group_correo" style="margin-top: 10px;">
                <div class="fields__label" id="titcorreo" runat="server" visible="false" style="margin-top: 3px; margin-right:5px;">Dirección Correo </div>

                <telerik:RadTextBox ID="TxtCorreo" runat="server" Style="margin-top: 0px; margin-right:10px; text-transform: uppercase;" Visible="false"
                    Width="250px" Height="22px" MaxLength="150">
                </telerik:RadTextBox>
                <telerik:RadButton ID="BtnEnvia" runat="server" Text="Enviar" OnClick="BtnEnviar" Visible="false" Width="70px" Height="22px"></telerik:RadButton>

                <telerik:RadButton ID="BtnCancela" runat="server" Text="Cancelar" Style="margin-left: 10px;"
                    OnClick="BntCancelar" Visible="false" Width="70px" Height="22px">
                </telerik:RadButton>
            </div>

            <h2 class="subtitle">Información principal</h2>
            <div class="info_main">
                <div class="fiedls">
                    <label class="fields__label">Cod. VEHÍCULO</label>
                    <telerik:RadTextBox ID="txtCodVehiculo" runat="server" MaxLength="14" Style="text-transform: uppercase;"></telerik:RadTextBox>
                </div>
                <div class="fiedls">
                    <label class="fields__label">CLIENTE</label>
                    <telerik:RadTextBox class="fields__input" ID="TxtConCliente" runat="server" Style="text-transform: uppercase;" Width="290"></telerik:RadTextBox>
                </div>
                <div class="fiedls">
                    <label class="fields__label">TIPO</label>
                    <telerik:RadTextBox ID="txtConTipo" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                </div>
                <div class="fiedls">
                    <label class="fields__label">PLACA</label>
                    <telerik:RadTextBox ID="txtConPlaca" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                </div>
                <div class="fiedls">
                    <label class="fields__label">CONCESIONARIO</label>
                    <telerik:RadTextBox class="fields__input" ID="txtConConcesionario" runat="server" Style="text-transform: uppercase;" Width="290"></telerik:RadTextBox>
                </div>
                <div class="fiedls">
                    <label class="fields__label">MODELO</label>
                    <telerik:RadTextBox ID="txtConModelo" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                </div>
                <div class="fiedls">
                    <label class="fields__label">CHASIS</label>
                    <telerik:RadTextBox ID="txtChasis" runat="server" MaxLength="30" Style="text-transform: uppercase;" Rows="1" Width="290"></telerik:RadTextBox>
                </div>
                <div class="fiedls">
                    <label class="fields__label">FINANCIERA</label>
                    <telerik:RadTextBox ID="txtConFinanciera" runat="server" Style="text-transform: uppercase;" Width="290"></telerik:RadTextBox>
                </div>

                <div class="fiedls">
                    <label class="fields__label">COLOR</label>
                    <telerik:RadTextBox ID="txtConColor" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                </div>

                <div class="fiedls">
                    <label class="fields__label">MOTOR</label>
                    <telerik:RadTextBox ID="TxtMotor" runat="server" MaxLength="30" Style="text-transform: uppercase;" Rows="1"></telerik:RadTextBox>
                </div>

                <div class="fiedls">
                    <label class="fields__label">TIPO REPORTE</label>
                    <telerik:RadComboBox ID="cbm_tipo" runat="server" Width="240px" Height="150px" CssClass="cmbTipoc"></telerik:RadComboBox>
                </div>

                <div class="fiedls">
                    <label class="fields__label">MARCA</label>
                    <telerik:RadTextBox ID="txtConMarca" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                </div>
            </div>

            <asp:Label ID="lbl_canales" runat="server" Font-Size="Small"></asp:Label>

            <h2 class="subtitle" style="margin: 0;">Detalle</h2>

            <asp:Literal ID="TableProducto" runat="server"></asp:Literal>


            <%--            <table style="width:100%; border-collapse: collapse;">
                <thead>
                    <tr style="background: #607799; color:#fff; font-size:13px; text-align:left;">
                        <th style="padding:5px;">Descripción</th>
                        <th style="padding:5px;">Estado Cobertura</th>
                        <th style="padding:5px;">Estado de la Instalación</th>
                        <th style="padding:5px;">Cobertura Inicial</th>
                        <th style="padding:5px;">Cobertura Final</th>
                    </tr>
                </thead>
                <tbody>
                    <tr style="font-size: 12px; color:#262626;">
                        <td style="padding:5px;">HUNTER MED / FAMILIAR</td>
                        <td style="padding:5px;">ACTIVO</td>
                        <td style="padding:5px;">001 - INSTALADO</td>
                        <td style="padding:5px;">24/09/2023</td>
                        <td style="padding:5px;">24/09/2026</td>
                    </tr>
                </tbody>
                <tfoot>
                    <tr style="font-size: 12px; color:#262626;">
                        <td colspan="3" style="padding:5px;"></td>
                    </tr>
                </tfoot>
            </table>--%>


            <div style="margin-top: 10px; display: none;">
                <telerik:RadGrid ID="RadGridVehiculo" runat="server" AutoGenerateColumns="False" CellSpacing="0" Culture="es-ES"
                    GridLines="None" Skin="Default" ShowFooter="True" Width="930px" Style="">
                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="true">
                        <%--<Selecting AllowRowSelect="True" CellSelectionMode="None"/>--%>
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowRowResize="false" />
                    </ClientSettings>
                    <MasterTableView AutoGenerateColumns="false" EditMode="InPlace" GridLines="None"
                        TableLayout="Auto" ShowGroupFooter="True" AllowNaturalSort="False">
                        <CommandItemSettings ExportToPdfText="Export to PDF" />
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True" />
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True" />
                        <Columns>
                            <telerik:GridBoundColumn DataField="DESCRIPCION" FilterControlAltText="Filter DESCRIPCION column" HeaderText="Descripción" ReadOnly="True"
                                UniqueName="DESCRIPCION" AutoPostBackOnFilter="True" FilterControlWidth="300px">
                                <HeaderStyle Font-Bold="True" Width="350px" CssClass="header_table" />
                                <ItemStyle Width="300px" CssClass="item_table" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ESTADO" FilterControlAltText="Filter ESTADO column" HeaderText="Estado" ReadOnly="True"
                                UniqueName="ESTADO" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                <HeaderStyle Font-Bold="True" Width="300px" CssClass="header_table" />
                                <ItemStyle Width="200px" CssClass="item_table" />
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="FECHA_CONTRATO" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_CONTRATO column"
                                HeaderText="Fecha Contrato" ReadOnly="True" UniqueName="FECHA_CONTRATO" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                FilterControlWidth="160px" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="120px" CssClass="header_table" />
                                <ItemStyle Width="120px" CssClass="item_table" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="FECHA_INICIAL" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_INICIAL column"
                                HeaderText="Fecha Inicial" ReadOnly="True" UniqueName="FECHA_INICIAL" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                FilterControlWidth="160px">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" CssClass="header_table" />
                                <ItemStyle Width="160px" CssClass="item_table" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridDateTimeColumn DataField="FECHA_FIN" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_FIN column"
                                HeaderText="Fecha Fin" ReadOnly="True" UniqueName="FECHA_FIN" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                FilterControlWidth="160px">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" CssClass="header_table" />
                                <ItemStyle Width="160px" CssClass="item_table" />
                            </telerik:GridDateTimeColumn>
                            <telerik:GridBoundColumn DataField="ESTADO_TALLER" FilterControlAltText="Filter ESTADO_TALLER column" HeaderText="Estado Taller" ReadOnly="True"
                                UniqueName="ESTADO_TALLER" AutoPostBackOnFilter="True" FilterControlWidth="160px" Visible="False">
                                <HeaderStyle Font-Bold="True" Width="120px" CssClass="header_table" />
                                <ItemStyle Width="120px" CssClass="item_table" />
                            </telerik:GridBoundColumn>
                            <telerik:GridDateTimeColumn DataField="FECHA_TALLER" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_TALLER column"
                                HeaderText="Fecha Ing.Taller" ReadOnly="True" UniqueName="FECHA_TALLER" AutoPostBackOnFilter="True" DataType="System.DateTime"
                                FilterControlWidth="160px" Visible="False">
                                <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" CssClass="header_table" />
                                <ItemStyle Width="160px" CssClass="item_table" />
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
            <asp:Label ID="mensajetexto" runat="server"></asp:Label>
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
                    <telerik:AjaxUpdatedControl ControlID="BtnNuevo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtChasis" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtMotor" LoadingPanelID="RadAjaxLoadingPanel2" />
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
                    <telerik:AjaxUpdatedControl ControlID="BtnNuevo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtChasis" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtMotor" LoadingPanelID="RadAjaxLoadingPanel2" />
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
                    <telerik:AjaxUpdatedControl ControlID="RadGridVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
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
                    <telerik:AjaxUpdatedControl ControlID="cbm_tipo" LoadingPanelID="RadAjaxLoadingPanel2" />

                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnEnvia">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCancela" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rntResultado" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnCancela">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_tipo" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>




        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Simple"></telerik:RadAjaxLoadingPanel>
</asp:Content>
