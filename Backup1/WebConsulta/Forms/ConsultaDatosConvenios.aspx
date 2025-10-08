<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="ConsultaDatosConvenios.aspx.vb" Inherits="WebConsulta.ConsultaDatosConvenios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="seccion_content_maestra">
        <div class="seccion_content_maestra_titulo">
            Consulta de Movimientos
        </div>
        <div class="seccion_content_maestra_titulo_corner"></div>
        <div class="seccion_content_maestra_data">
            <div class="seccion_toolbar_text">
                <div class="toolbar_maestra_nuevo">
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
                    <div class="toolbar_maestra_icon">
                        <telerik:RadButton ID="BtnExportar" runat="server" Text="Exportar" ForeColor="Black" AlternateText="Xlsx"
                            Style="top: 0px; left: 0px; height: 28px; width: 32px" ToolTip="Exportar Data" OnClick="BtnExportar_Click">
                            <Image ImageUrl="../Images/download32x32.png" IsBackgroundImage="False" />
                        </telerik:RadButton>
                    </div>
                    <div class="text_maestra_seccion_control">
                         <telerik:RadTextBox ID="txtConTipo" runat="server" Style="text-transform: uppercase;" Visible="False"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConCodCliente" runat="server" MaxLength="80" Width="220px" Visible="False"></telerik:RadTextBox>
                    </div>
                    
                </div>
                <div class="text_maestra">
                    <div class="text_maestra_seccion_label">Fecha Inicio</div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadDatePicker ID="rdpFechaInicio" runat="server" ToolTip="Ingrese la fecha de inicio" Width="120px" Height="21px">
                            <Calendar ID="Calendar1" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput ID="DateInput1" runat="server" DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MM/yyyy" DisplayText="" LabelWidth="40%" type="text" value="" Height="18px">
                            </DateInput>
                            <DatePopupButton ImageUrl="../Images/icons/Calendario21x20.png" HoverImageUrl="../Images/icons/Calendario21x20.png" Height="21" Width="20">
                            </DatePopupButton>
                        </telerik:RadDatePicker>
                    </div>
                    <div class="text_maestra_seccion_label">Fecha Final</div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadDatePicker ID="rdpFechaFin" runat="server" ToolTip="Ingrese la fecha de final" Width="120px" Height="21px">
                            <Calendar ID="Calendar2" runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput ID="DateInput2" runat="server" DisplayDateFormat="dd/MMM/yyyy" DateFormat="dd/MM/yyyy" DisplayText="" LabelWidth="40%" type="text" value="" Height="18px">
                            </DateInput>
                            <DatePopupButton ImageUrl="../Images/icons/Calendario21x20.png" HoverImageUrl="../Images/icons/Calendario21x20.png" Height="21" Width="20">
                            </DatePopupButton>
                        </telerik:RadDatePicker>
                    </div>
                   
                   
                    <div class="text_maestra_seccion_control"></div>
                    <div class="text_maestra_seccion_label">
                        Chasis
                    </div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadTextBox ID="txtChasis" runat="server" MaxLength="30" Style="text-transform: uppercase;" Rows="1"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Motor
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="TxtMotor" runat="server" MaxLength="30" Style="text-transform: uppercase;" Rows="1"></telerik:RadTextBox>
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
                        Cod. Vehículo
                    </div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadNumericTextBox ID="txtCodVehiculo" runat="server" MaxLength="10">
                            <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="3" NegativePattern="-n" ZeroPattern="n" />
                        </telerik:RadNumericTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Placa
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConPlaca" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
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
                       <telerik:RadTextBox ID="txtConColor" runat="server" Style="text-transform: uppercase;" Visible="False"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">Marca</div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadTextBox ID="txtConMarca" runat="server" Style="text-transform: uppercase;"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_label">
                        Modelo
                    </div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadTextBox ID="txtConModelo" runat="server" Style="text-transform: uppercase;" Width="160px"></telerik:RadTextBox>
                    </div>
                    <div class="text_maestra_seccion_separador">
                       
                    </div>
                    <div class="text_maestra_seccion_label">Canal</div>
                    <div class="text_maestra_seccion_separador">
                        <telerik:RadComboBox ID="cmb_canal" runat="server" Width="200px" Height="150px"></telerik:RadComboBox>
                    </div>
                    <div class="text_maestra_seccion_label">Estado</div>
                    <div class="text_maestra_seccion_control">
                        <telerik:RadComboBox ID="cbm_estado" runat="server" Width="150px" Height="150px"></telerik:RadComboBox>
                    </div>
                    <div class="text_maestra_seccion_separador"></div>

                    <div class="text_maestra_seccion_label" id="titcorreo" runat="server" >
                        Dirección Correo 
                    </div>
                    <div id="Div1" class="text_maestra_seccion_control5" runat="server">
                        <div  class="text_maestra_seccion1_control1" >
                            <telerik:RadTextBox ID="TxtCorreo" runat="server"  Style="text-transform: uppercase;" 
                                Width="250px" Height="20px" MaxLength="150" Enabled="False">
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
                    <div class="text_maestra_seccion_label_mensaje" id="Div2" runat="server" >
                        Nota: En Rojo los Demos
                    </div>

                    <div  class="text_maestra_seccion_control5" >
                        <asp:Label ID="Label1" runat="server"  CssClass="text_maestra_seccion_label_mensaje2" />
                    </div>
                    <div   class="text_maestra_seccion_control5" >
                        <asp:Label ID="Label2" runat="server" CssClass="text_maestra_seccion_label_mensaje2" />
                    </div>
                </div>
                <div class="content_tab_master">
                    <telerik:RadTabStrip ID="radtabdatos" runat="server" AutoPostBack="True"
                        Width="100%" SelectedIndex="1" MultiPageID="RadMultiPage1" Skin="" 
                        CssClass="tab_general">
                        <Tabs>
                            <telerik:RadTab Text="Datos Generales" ImageUrl="../Images/icono_consulta.png" TabIndex="0"
                                 PageViewID="RadPageView1" CssClass="tab_individual" Selected="True" >
                            </telerik:RadTab>
                            <telerik:RadTab Text="Productos" ImageUrl="../Images/icono_vehiculo.png" TabIndex="1"
                                PageViewID="RadPageView2" CssClass="tab_individual"  >
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="1">
                        <telerik:RadPageView ID="RadPageView1" runat="server" Width="938px">
                            <div class="content_tab_datos02">
                                <telerik:RadGrid ID="RadGridConsultar" runat="server" AllowCustomPaging="True" 
                                    Width="944px" ShowStatusBar="True"  EnableEmbeddedSkins="False" CellSpacing="0" 
                                    Culture="es-ES" GridLines="None" OnSelectedIndexChanged="RadGridConsultar_SelectedIndexChanged" 
                                    AllowFilteringByColumn="True" ShowFooter="True">
                                    <GroupingSettings CaseSensitive="false" />
                                    <SortingSettings SortedBackColor="BurlyWood" />
                                    <ClientSettings EnablePostBackOnRowClick="True" EnableRowHoverStyle="true">
                                        <Selecting AllowRowSelect="True" />
                                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    </ClientSettings>
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="CODIGO_VEHICULO"  NoDetailRecordsText="" NoMasterRecordsText="">
                                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                        <RowIndicatorColumn Visible="True" FilterControlAltText="Filter RowIndicator column"></RowIndicatorColumn>
                                        <ExpandCollapseColumn Visible="True" FilterControlAltText="Filter ExpandColumn column"></ExpandCollapseColumn>
                                        <Columns>
                                            <telerik:GridBoundColumn Aggregate="Count" DataField="DET_ESTADO" FilterControlAltText="Filter DET_ESTADO column" HeaderText="Estado" ReadOnly="True"
                                                UniqueName="DET_ESTADO"  AutoPostBackOnFilter="True"  FilterControlWidth="140px" FooterText="Nro. Registros: ">
                                                <HeaderStyle Font-Bold="True" Width="140px" />
                                                <ItemStyle Width="140px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CHASIS" FilterControlAltText="Filter CHASIS column" HeaderText="Chasis" ReadOnly="True"
                                                UniqueName="CHASIS" AutoPostBackOnFilter="True" FilterControlWidth="160px" >
                                                <HeaderStyle Font-Bold="True" Width="160px" />
                                                <ItemStyle Width="160px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MOTOR" FilterControlAltText="Filter MOTOR column" HeaderText="Motor" ReadOnly="True"
                                                UniqueName="MOTOR" AutoPostBackOnFilter="True" FilterControlWidth="160px" DataType="System.String" DataFormatString="{0:#########}" >
                                                <HeaderStyle Font-Bold="True" Width="160px" />
                                                <ItemStyle Width="160px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CODIGO_VEHICULO" FilterControlAltText="Filter CODIGO_VEHICULO column" HeaderText="Cod. Vehículo" ReadOnly="True"
                                                UniqueName="CODIGO_VEHICULO" AutoPostBackOnFilter="True" FilterControlWidth="100px"  AllowFiltering="False" DataType="System.String">
                                                <HeaderStyle Font-Bold="True" Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="NOMBRE_COMPLETO" FilterControlAltText="Filter NOMBRE_COMPLETO column" HeaderText="Cliente" ReadOnly="True"
                                                UniqueName="NOMBRE_COMPLETO" AutoPostBackOnFilter="True" FilterControlWidth="300px" >
                                                <HeaderStyle Font-Bold="True" Width="300px" />
                                                <ItemStyle Width="300px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CODIGO_CLIENTE" FilterControlAltText="Filter CODIGO_CLIENTE column" HeaderText="Cod. Cliente" ReadOnly="True"
                                                UniqueName="CODIGO_CLIENTE" AutoPostBackOnFilter="True" FilterControlWidth="140px" DataFormatString="{0:########}" >
                                                <HeaderStyle Font-Bold="True" Width="0px" />
                                                <ItemStyle Width="0px"  />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PLACA" FilterControlAltText="Filter PLACA column" HeaderText="Placa" ReadOnly="True"
                                                UniqueName="PLACA" AutoPostBackOnFilter="True" FilterControlWidth="140px" >
                                                <HeaderStyle Font-Bold="True" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MARCA" FilterControlAltText="Filter MARCA column" HeaderText="Marca" ReadOnly="True"
                                                UniqueName="MARCA" AutoPostBackOnFilter="True" FilterControlWidth="140px" AllowFiltering="False">
                                                <HeaderStyle Font-Bold="True" Width="140px" />
                                                <ItemStyle Width="140px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="MODELO" FilterControlAltText="Filter MODELO column" HeaderText="Modelo" ReadOnly="True"
                                                UniqueName="MODELO" AutoPostBackOnFilter="True" FilterControlWidth="200px" >
                                                <HeaderStyle Font-Bold="True" Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TIPO" FilterControlAltText="Filter TIPO column" HeaderText="Tipo" ReadOnly="True"
                                                UniqueName="TIPO" AutoPostBackOnFilter="True" FilterControlWidth="140px" >
                                                <HeaderStyle Font-Bold="True" Width="140px" />
                                                <ItemStyle Width="140px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="COLOR" FilterControlAltText="Filter COLOR column" HeaderText="Color" ReadOnly="True"
                                                UniqueName="COLOR" AutoPostBackOnFilter="True" FilterControlWidth="140px" AllowFiltering="False">
                                                <HeaderStyle Font-Bold="True" Width="140px" />
                                                <ItemStyle Width="140px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="FECHA" FilterControlAltText="Filter FECHA column" HeaderText="Fecha" ReadOnly="True"
                                                UniqueName="FECHA" AutoPostBackOnFilter="True" FilterControlWidth="140px" AllowFiltering="False">
                                                <HeaderStyle Font-Bold="True" Width="140px" />
                                                <ItemStyle Width="140px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PLAZO" FilterControlAltText="Filter PLAZO column" HeaderText="Plazo" ReadOnly="True"
                                                UniqueName="PLAZO" AutoPostBackOnFilter="True" FilterControlWidth="140px" >
                                                <HeaderStyle Font-Bold="True" Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ANIO" FilterControlAltText="Filter ANIO column" HeaderText="Anio" ReadOnly="True"
                                                UniqueName="ANIO" AutoPostBackOnFilter="True" FilterControlWidth="100px"  >
                                                <HeaderStyle Font-Bold="True" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ORDEN_SERVICIO" FilterControlAltText="Filter ORDEN_SERVICIO column" HeaderText="Orden Servicio" ReadOnly="True"
                                                UniqueName="ORDEN_SERVICIO" AutoPostBackOnFilter="True" FilterControlWidth="140px" DataType="System.String">
                                                <HeaderStyle Font-Bold="True" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="ACC" FilterControlAltText="Filter ACC column" HeaderText="Acción" ReadOnly="True"
                                                UniqueName="ACC" AutoPostBackOnFilter="True" FilterControlWidth="0px" DataType="System.String">
                                                <HeaderStyle Font-Bold="True" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="EJEC" FilterControlAltText="Filter EJEC column" HeaderText="Ejecutiva" ReadOnly="True"
                                                UniqueName="EJEC" AutoPostBackOnFilter="True" FilterControlWidth="200px"  AllowFiltering="False" DataType="System.String">
                                                <HeaderStyle Font-Bold="True" Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="CTP" FilterControlAltText="Filter CTP column" HeaderText="Cambio CTP" ReadOnly="True"
                                                UniqueName="CTP" AutoPostBackOnFilter="True" FilterControlWidth="100px"  AllowFiltering="False" DataType="System.String">
                                                <HeaderStyle Font-Bold="True" Width="100px" />
                                                <ItemStyle Width="100px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="TIPO_TRANSACCION" FilterControlAltText="Filter TIPO_TRANSACCION column" HeaderText="Transacción" ReadOnly="True"
                                                UniqueName="TIPO_TRANSACCION" AutoPostBackOnFilter="True" FilterControlWidth="0px"  >
                                                <HeaderStyle Font-Bold="True" Width="0px" />
                                                <ItemStyle Width="0px" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn DataField="PROD" FilterControlAltText="Filter PROD column" HeaderText="Producto" ReadOnly="True"
                                                UniqueName="PROD" AutoPostBackOnFilter="True" FilterControlWidth="200px"  AllowFiltering="False" DataType="System.String">
                                                <HeaderStyle Font-Bold="True" Width="200px" />
                                                <ItemStyle Width="200px" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <EditFormSettings>
                                            <EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
                                        </EditFormSettings>
                                        <PagerStyle PageSizeControlType="RadComboBox" Mode="NumericPages"></PagerStyle>
                                    </MasterTableView>
                                    <PagerStyle PageSizeControlType="RadComboBox" AlwaysVisible="True"></PagerStyle>
                                    <FilterMenu EnableImageSprites="False"></FilterMenu>
                                    <HeaderContextMenu EnableEmbeddedSkins="False">
                                    </HeaderContextMenu>
                                </telerik:RadGrid>
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                             <div class="content_tab_datos02">
                                <telerik:RadGrid ID="RadGridVehiculo" runat="server" AutoGenerateColumns="False" CellSpacing="0" Culture="es-ES"
                                    GridLines="None" Skin="Default" ShowFooter="True" Width="920px" height="220px">
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
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
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

    <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
    </telerik:RadWindowManager>
   <%-- <telerik:RadWindow ID="wdwcertificado" runat="server" Height="850px" NavigateUrl="ConsultaDatos.aspx" Width="850px">
    </telerik:RadWindow>--%>
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel2">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="BtnNuevo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BtnNuevo"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <%-- <telerik:AjaxUpdatedControl ControlID="BtnExportar" LoadingPanelID="RadAjaxLoadingPanel2"/>--%>
                    <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtConTipo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtChasis" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtMotor"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConCodCliente"  LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtCodVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConPlaca"  LoadingPanelID="RadAjaxLoadingPanel2" />
                   <%-- <telerik:AjaxUpdatedControl ControlID="txtConConcesionario" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConFinanciera" LoadingPanelID="RadAjaxLoadingPanel2" />--%>
                    <telerik:AjaxUpdatedControl ControlID="TxtConCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConColor"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConMarca" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConModelo"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <%--<telerik:AjaxUpdatedControl ControlID="RadMultiPage1"  LoadingPanelID="RadAjaxLoadingPanel2" />--%>
                    <telerik:AjaxUpdatedControl ControlID="RadGridConsultar" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="RadGridVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="radtabdatos"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaInicio"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaFin"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_estado"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cmb_canal"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Label1"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Label2"  LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnConsultar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BtnNuevo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                   <%-- <telerik:AjaxUpdatedControl ControlID="BtnExportar" LoadingPanelID="RadAjaxLoadingPanel2"/>--%>
                    <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConTipo"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtChasis" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtMotor" LoadingPanelID="RadAjaxLoadingPanel2" />
                  <%--  <telerik:AjaxUpdatedControl ControlID="txtConConcesionario" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConFinanciera" LoadingPanelID="RadAjaxLoadingPanel2" />--%>
                    <telerik:AjaxUpdatedControl ControlID="txtConCodCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtCodVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConPlaca" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtConCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConColor" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtConMarca" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtConModelo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="RadGridConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rntResultado" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="radtabdatos"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaInicio"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaFin"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_estado"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Label1"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Label2"  LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="radtabdatos">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2"/>
                        <telerik:AjaxUpdatedControl ControlID="radtabdatos" LoadingPanelID="RadAjaxLoadingPanel2"/>
                        <telerik:AjaxUpdatedControl ControlID="RadMultiPage1" LoadingPanelID="RadAjaxLoadingPanel2" />
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
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="rntResultado" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_estado"  LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="BtnCancela">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="titcorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnEnvia" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_estado" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridConsultar">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="BtnImprimir"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="BtnCorreo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConTipo" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtChasis" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="TxtMotor" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConCodCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtCodVehiculo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConPlaca" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Txtconanio" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="TxtConCliente" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="txtConColor" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtConMarca" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="txtConModelo" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="radtabdatos"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridVehiculo"  LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2"/>
                    <telerik:AjaxUpdatedControl ControlID="rntResultado"  LoadingPanelID="RadAjaxLoadingPanel2"  />
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaInicio"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="rdpFechaFin"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="cbm_estado"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Label1"  LoadingPanelID="RadAjaxLoadingPanel2" />
                    <telerik:AjaxUpdatedControl ControlID="Label2"  LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Simple"></telerik:RadAjaxLoadingPanel>
</asp:Content>
