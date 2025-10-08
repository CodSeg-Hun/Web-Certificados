<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="ConsultadatosBak.aspx.vb" Inherits="WebConsulta.ConsultadatosBak" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Container -->
    <div id="container">
        <!-- Background wrapper -->
        <div id="bgwrap">
            <!-- Main Content -->
            <div id="content">
                <div id="main">
                    <div class="body_resize">
                        <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
                        </telerik:RadScriptManager>
                        <br />
                        <div class="btn_impresion">
                            Consulta General Vehiculo
                             <%--<asp:Label ID="lbltituloform" runat="server"></asp:Label>--%>
                        </div>
                        <br />
                        <telerik:RadNotification ID="RnMensajesError" runat="server" Animation="Slide" Height="100px"
                            Position="Center" Width="414px" EnableRoundedCorners="True" EnableShadow="True">
                        </telerik:RadNotification>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" Style="height: 600px; width: 1150px;" HorizontalAlign="NotSet" LoadingPanelID="RadAjaxLoadingPanel2">
                            <div class="admin_sec_separador_001">
                            </div>
                            <div class="admin_sec_button_bar_001">
                                <div class="admin_sec_button_001">
                                    <telerik:RadButton ID="BtnNuevo" runat="server" Text="Nuevo" ForeColor="Black" Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Nuevo">
                                       <%-- <Image ImageUrl="../Images/icons/New file.png" IsBackgroundImage="False" HoveredImageUrl="../Images/icons/New file.png"
                                            PressedImageUrl="../Images/icons/New file.png" DisabledImageUrl="../Images/icons/New file.png" />--%>
                                        <Image ImageUrl="../Images/icons/New file.png"  IsBackgroundImage="False"/>
                                    </telerik:RadButton>
                                </div>
                                <div class="admin_sec_button_001">
                                    <telerik:RadButton ID="BtnCancelar" runat="server" Text="Cancelar" ForeColor="Black" Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Cancelar">
                                        <Image ImageUrl="../Images/icons/Undo.png" IsBackgroundImage="False" DisabledImageUrl="../Images/icons/Undo.png"
                                            HoveredImageUrl="../Images/icons/Undo.png" PressedImageUrl="../Images/icons/Undo.png" />
                                           <%-- <Image ImageUrl="../Images/icons/Undo.png"  IsBackgroundImage="False"/>--%>
                                    </telerik:RadButton>
                                </div>
                                <div class="admin_sec_button_001">
                                    <telerik:RadButton ID="BtnConsultar" runat="server" Text="Consultar" ForeColor="Black" Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Consultar">
                                       <%-- <Image ImageUrl="../Images/icons/Preview.png" IsBackgroundImage="False" DisabledImageUrl="../Images/icons/Preview.png"
                                            HoveredImageUrl="../Images/icons/Preview.png" PressedImageUrl="../Images/icons/Preview.png" />--%>
                                        <Image ImageUrl="../Images/icons/Preview.png"  IsBackgroundImage="False"/>
                                    </telerik:RadButton>
                                </div>
                                 <div class="admin_sec_button_001">
                                    <telerik:RadButton ID="BtnImprimir" runat="server" Text="Imprimir" ForeColor="Black" Style="top: 0px; left: 0px; height: 32px; width: 32px" ToolTip="Consultar">
                                       <%-- <Image ImageUrl="../Images/icons/icono_imprimir.jpg" IsBackgroundImage="False" DisabledImageUrl="../Images/icons/icono_imprimir.jpg"
                                            HoveredImageUrl="../Images/icons/icono_imprimir.jpg" PressedImageUrl="../Images/icons/icono_imprimir.jpg" />--%>
                                        <Image ImageUrl="../Images/icons/icono_imprimir.jpg"  IsBackgroundImage="False"/>
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div class="admin_sec_separador_001">
                            </div>
                            <div class="admin_sec_content_central_001">
                                <telerik:RadTabStrip ID="RadTabParametro" runat="server" MultiPageID="RadMultiPage" SelectedIndex="0" Skin="Vista">
                                    <Tabs>
                                        <telerik:RadTab runat="server" Text="Consulta" PageViewID="RadPageConsulta" SelectedIndex="0">
                                        </telerik:RadTab>
                                        <telerik:RadTab runat="server" Text="Vehiculo" PageViewID="RadPageVehiculo" Selected="True">
                                        </telerik:RadTab>
                                    </Tabs>
                                </telerik:RadTabStrip>
                                <telerik:RadMultiPage ID="RadMultiPage" runat="server" Height="763px" Width="922px" SelectedIndex="0">
                                    <telerik:RadPageView ID="RadPageConsulta" runat="server" Height="400px" Width="1100px">
                                        <div class="admin_sec_separador_018">
                                        </div>
                                        <%--<div class="admin_sec_label_001">
                                            <asp:Label ID="Label7" runat="server" Text="C.I./RUC"></asp:Label>
                                         </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadNumericTextBox ID="txtRuc" runat="server" Style="text-align: right"
                                                DataType="System.Int32" MaxLength="14" MinValue="0">
                                                <numberformat decimaldigits="0" zeropattern="n" />
                                            </telerik:RadNumericTextBox>
                                            </div>
                                        <div class="admin_sec_label_001">
                                        </div>
                                        <div class="admin_sec_control_001">
                                        </div>
                                        <div class="admin_sec_separador_015">
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label8" runat="server" Text="Nombre"></asp:Label>
                                         </div>
                                        <div class="admin_sec_separador_002">
                                            <telerik:RadTextBox ID="txtNombre" runat="server" MaxLength="80" Style="text-transform: uppercase;" Width="500px">
                                            </telerik:RadTextBox>
                                            </div>
                                        <div class="admin_sec_separador_015">
                                        </div>
                                        --%>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label9" runat="server" Text="Cod. Vehiculo"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadNumericTextBox ID="txtCodVehiculo" runat="server" MaxLength="10">
                                                <NumberFormat DecimalDigits="0" GroupSeparator="" GroupSizes="3" NegativePattern="-n" ZeroPattern="n" />
                                            </telerik:RadNumericTextBox>
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label3" runat="server" Text="Chasis"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtChasis" runat="server" MaxLength="30" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_separador_015">
                                        </div>
                                        <div class="admin_sec_label_001">
                                           <asp:Label ID="Label10" runat="server" Text="Motor"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="TxtMotor" runat="server" MaxLength="30" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <%--<asp:Label ID="Label7" runat="server" Text="Placa"></asp:Label>--%>
                                        </div>
                                        <div class="admin_sec_control_001">
                                           <%-- <telerik:RadTextBox ID="TxtPlaca" runat="server" MaxLength="8" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>--%>
                                        </div>
                                        <div class="admin_sec_separador_015"></div>
                                        <div class="admin_sec_control_001"></div>
                                        <div class="admin_sec_separador_002"></div>
                                        <div class="admin_sec_content_central_002">
                                            <telerik:RadGrid ID="RadGridDatos" runat="server" AllowPaging="True" AutoGenerateColumns="False" CellSpacing="0"
                                                Culture="es-ES" GridLines="None" Skin="Vista" AllowMultiRowSelection="True" ShowFooter="True"
                                                OnSelectedIndexChanged="RadGridDatos_SelectedIndexChanged" >
                                                <GroupingSettings CaseSensitive="false" />
                                                <SortingSettings SortedBackColor="BurlyWood" />
                                                <ClientSettings EnablePostBackOnRowClick="true">
                                                    <Selecting AllowRowSelect="True" CellSelectionMode="None" />                                  
                                                    <Resizing AllowRowResize="false" />
                                                </ClientSettings>
                                                <MasterTableView Width="100%" AutoGenerateColumns="false" EditMode="InPlace" GridLines="None" TableLayout="Auto" AllowPaging="False">
                                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn AutoPostBackOnFilter="True" DataField="ID_CLIENTE" FilterControlAltText="Filter ID_CLIENTE column"
                                                            FilterControlWidth="160px" HeaderText="Cod.Cliente" ReadOnly="True" SortExpression="ID_CLIENTE" 
                                                            UniqueName="ID_CLIENTE">
                                                            <HeaderStyle Font-Bold="True" Width="100px" />
                                                            <ItemStyle Width="100px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CLIENTE" FilterControlAltText="Filter CLIENTE column" HeaderText="Cliente" ReadOnly="True"
                                                            UniqueName="CLIENTE" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="220px" />
                                                            <ItemStyle Width="220px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ID_VEHICULO" FilterControlAltText="Filter ID_VEHICULO column" HeaderText="Cod.Vehiculo" ReadOnly="True" 
                                                            UniqueName="ID_VEHICULO" AutoPostBackOnFilter="True" FilterControlWidth="160px" SortExpression="ID_VEHICULO">
                                                            <HeaderStyle Font-Bold="True" Width="90px" />
                                                            <ItemStyle Width="90px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CONCESIONARIO" FilterControlAltText="Filter CONCESIONARIO column" HeaderText="Concesionario" ReadOnly="True"
                                                            UniqueName="ID_VEHICULO" AutoPostBackOnFilter="True" FilterControlWidth="160px" SortExpression="CONCESIONARIO">
                                                            <HeaderStyle Font-Bold="True" Width="110px" />
                                                            <ItemStyle Width="110px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PLACA" FilterControlAltText="Filter PLACA column" HeaderText="Placa" ReadOnly="True" 
                                                            UniqueName="PLACA" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="90px" />
                                                            <ItemStyle Width="90px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="MARCA" FilterControlAltText="Filter MARCA column" HeaderText="Marca" ReadOnly="True" 
                                                            UniqueName="MARCA" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="80px" />
                                                            <ItemStyle Width="80px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="MODELO" FilterControlAltText="Filter MODELO column" HeaderText="Modelo" ReadOnly="True" 
                                                            UniqueName="MODELO" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="250px" />
                                                            <ItemStyle Width="250px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="COLOR" FilterControlAltText="Filter COLOR column" HeaderText="Color" ReadOnly="True" 
                                                            UniqueName="COLOR" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="100px" />
                                                            <ItemStyle Width="100px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TIPO" FilterControlAltText="Filter TIPO column"  HeaderText="Tipo" ReadOnly="True" 
                                                            UniqueName="TIPO" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="150px" />
                                                            <ItemStyle Width="150px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="MOTOR" FilterControlAltText="Filter MOTOR column"  HeaderText="Motor" ReadOnly="True" 
                                                            UniqueName="MOTOR" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="100px" />
                                                            <ItemStyle Width="100px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="CHASIS" FilterControlAltText="Filter CHASIS column" HeaderText="Chasis" ReadOnly="True" 
                                                            UniqueName="CHASIS" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="140px" />
                                                            <ItemStyle Width="140px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ANIO" FilterControlAltText="Filter ANIO column" HeaderText="Año" ReadOnly="True" 
                                                            UniqueName="ANIO" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="50px" />
                                                            <ItemStyle Width="50px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ESTADO" FilterControlAltText="Filter ESTADO column" HeaderText="Estado" ReadOnly="True" 
                                                            UniqueName="ESTADO" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="120px" />
                                                            <ItemStyle Width="120px" />
                                                        </telerik:GridBoundColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                    <PagerStyle PageButtonCount="10" Mode="NumericPages"/>
                                                </MasterTableView>
                                                <PagerStyle PageButtonCount="10" />
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                    <telerik:RadPageView ID="RadPageVehiculo" runat="server" Height="350px" Width="1100px">
                                        <div class="admin_sec_separador_018">
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="lblCliente" runat="server" Text="Cliente"></asp:Label>
                                        </div>
                                        <div class="admin_sec_separador_002">
                                            <telerik:RadTextBox ID="TxtConCliente" runat="server" Style="text-transform: uppercase;" Width="500px">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_separador_015">
                                            <telerik:RadTextBox ID="txtConCodCliente" runat="server" Visible="False" >
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label2" runat="server" Text="Cod. Vehiculo"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConCodVehiculo" runat="server" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label1" runat="server" Text="Marca"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConMarca" runat="server" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_separador_015">
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label11" runat="server" Text="Modelo"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConModelo" runat="server" Style="text-transform: uppercase;" Width="250px">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label12" runat="server" Text="Placa"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConPlaca" runat="server" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_separador_015">
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label5" runat="server" Text="Tipo"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConTipo" runat="server" Style="text-transform: uppercase;" >
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label6" runat="server" Text="Chasis"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConChasis" runat="server" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_separador_015">
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label13" runat="server" Text="Color"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConColor" runat="server" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_label_001">
                                            <asp:Label ID="Label14" runat="server" Text="Motor"></asp:Label>
                                        </div>
                                        <div class="admin_sec_control_001">
                                            <telerik:RadTextBox ID="txtConMotor" runat="server" Style="text-transform: uppercase;">
                                            </telerik:RadTextBox>
                                        </div>
                                        <div class="admin_sec_separador_015"></div>
                                        <div class="admin_sec_label_001"></div>
                                        <div class="admin_sec_control_001"></div>
                                        <div class="admin_sec_separador_002"></div>
                                        <div class="admin_sec_content_central_002">
                                            <telerik:RadGrid ID="RadGridVehiculo" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                                                 Culture="es-ES" GridLines="None" Skin="Vista" ShowFooter="True" Width="800px"  >
                                                <ClientSettings >
                                                        <Selecting AllowRowSelect="True" CellSelectionMode="None" />                                  
                                                    <Resizing AllowRowResize="false" />
                                                </ClientSettings>
                                                <MasterTableView Width="100%" AutoGenerateColumns="false" EditMode="InPlace" 
                                                    GridLines="None" TableLayout="Auto" ShowGroupFooter="True" AllowNaturalSort="False">
                                                    <CommandItemSettings ExportToPdfText="Export to PDF" />
                                                    <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column" Visible="True">
                                                    </RowIndicatorColumn>
                                                    <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column" Visible="True">
                                                    </ExpandCollapseColumn>
                                                    <Columns>
                                                        <telerik:GridBoundColumn DataField="DESCRIPCION" FilterControlAltText="Filter DESCRIPCION column"
                                                            HeaderText="Descripción" ReadOnly="True" UniqueName="DESCRIPCION" AutoPostBackOnFilter="True"
                                                            FilterControlWidth="220px">
                                                            <HeaderStyle Font-Bold="True" Width="220px" />
                                                            <ItemStyle Width="220px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ESTADO" FilterControlAltText="Filter ESTADO column" HeaderText="Estado" ReadOnly="True"
                                                            UniqueName="ESTADO" AutoPostBackOnFilter="True" FilterControlWidth="160px">
                                                            <HeaderStyle Font-Bold="True" Width="160px" />
                                                            <ItemStyle Width="160px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridDateTimeColumn DataField="FECHA_CONTRATO" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_CONTRATO column"
                                                            HeaderText="Fecha Contrato" ReadOnly="True" UniqueName="FECHA_CONTRATO" AutoPostBackOnFilter="True"
                                                            DataType="System.DateTime" FilterControlWidth="160px" Visible="False">
                                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="120px" />
                                                            <ItemStyle Width="120px" />
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridDateTimeColumn DataField="FECHA_INICIAL" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_INICIAL column"
                                                            HeaderText="Fecha Inicial" ReadOnly="True" UniqueName="FECHA_INICIAL" AutoPostBackOnFilter="True"
                                                            DataType="System.DateTime" FilterControlWidth="160px">
                                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" />
                                                            <ItemStyle Width="160px" />
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridDateTimeColumn DataField="FECHA_FIN" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_FIN column"
                                                            HeaderText="Fecha Fin" ReadOnly="True" UniqueName="FECHA_FIN" AutoPostBackOnFilter="True" 
                                                            DataType="System.DateTime" FilterControlWidth="160px">
                                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" />
                                                            <ItemStyle Width="160px" />
                                                        </telerik:GridDateTimeColumn>
                                                        <telerik:GridBoundColumn DataField="ESTADO_TALLER" FilterControlAltText="Filter ESTADO_TALLER column" HeaderText="Estado Taller" ReadOnly="True" 
                                                            UniqueName="ESTADO_TALLER" AutoPostBackOnFilter="True" FilterControlWidth="160px" Visible="False">
                                                            <HeaderStyle  Font-Bold="True" Width="120px" />
                                                            <ItemStyle Width="120px" />
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridDateTimeColumn DataField="FECHA_TALLER" DataFormatString="{0:dd/MMMM/yyyy}" FilterControlAltText="Filter FECHA_TALLER column"
                                                            HeaderText="Fecha Ing.Taller" ReadOnly="True" UniqueName="FECHA_TALLER" AutoPostBackOnFilter="True" 
                                                            DataType="System.DateTime" FilterControlWidth="160px" Visible="False">
                                                            <HeaderStyle HorizontalAlign="Left" Font-Bold="True" Width="160px" />
                                                            <ItemStyle Width="160px" />
                                                        </telerik:GridDateTimeColumn>
                                                    </Columns>
                                                    <EditFormSettings>
                                                        <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                                        </EditColumn>
                                                    </EditFormSettings>
                                                    <PagerStyle PageButtonCount="10" Mode="NumericPages"/>
                                                </MasterTableView>
                                                <PagerStyle PageButtonCount="10" />
                                                <FilterMenu EnableImageSprites="False">
                                                </FilterMenu>
                                            </telerik:RadGrid>
                                        </div>
                                    </telerik:RadPageView>
                                </telerik:RadMultiPage>
                                <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server">
                                    <AjaxSettings>
                                        <telerik:AjaxSetting AjaxControlID="BtnNuevo">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RnMensajesError"  LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnCancelar" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadTabParametro" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadMultiPage" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelCssClass="" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="BtnCancelar">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnNuevo" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadTabParametro" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadMultiPage" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelCssClass="" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="BtnConsultar">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RnMensajesError" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadTabParametro" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadMultiPage" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelCssClass="" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                        <telerik:AjaxSetting AjaxControlID="RadGridDatos">
                                            <UpdatedControls>
                                                <telerik:AjaxUpdatedControl ControlID="RnMensajesError" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnNuevo" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnCancelar" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnConsultar" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="BtnImprimir" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadTabParametro" LoadingPanelID="RadAjaxLoadingPanel2" />
                                                <telerik:AjaxUpdatedControl ControlID="RadMultiPage" LoadingPanelID="RadAjaxLoadingPanel2" UpdatePanelCssClass="" />
                                            </UpdatedControls>
                                        </telerik:AjaxSetting>
                                    </AjaxSettings>
                                </telerik:RadAjaxManager>
                                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Simple">
                                </telerik:RadAjaxLoadingPanel>
                            </div>
                        </telerik:RadAjaxPanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>