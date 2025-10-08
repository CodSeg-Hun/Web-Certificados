<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConsultaWebReporte.aspx.vb" Inherits="WebConsulta.ConsultaWebReporte" EnableEventValidation = "false"  %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title>Certificado</title>
        <script type="text/javascript">
            function impre(num) {
                document.getElementById(num).className = "ver";
                print();
                document.getElementById(num).className = "nover";
            }

            document.addEventListener("DOMContentLoaded", function () {
                // Obtener parámetros de la URL
                const urlParams = new URLSearchParams(window.location.search);

                // Verificar si existe un parámetro específico
                const tieneParametrosAdicionales = urlParams.has('c') ||
                    urlParams.has('c');

                // Obtener el valor de un parámetro específico
                // const valorParametro = urlParams.get('parametro');

                // Controlar visibilidad del botón de correo
                const emailButton = document.getElementById("divEmailButton");
                if (emailButton) {
                    emailButton.style.display = tieneParametrosAdicionales ? "block" : "none";
                }
            });
            function enviarCorreo() {
                const email = document.getElementById('emailDestino').value;
                if (email) {
                    // Mostrar indicador de carga
                    document.getElementById('RadButton2').disabled = true;
                    document.getElementById('RadButton2').value = "Enviando...";

                    // Ejecutar el postback
                    __doPostBack('RadButton2', '');
                } else {
                    alert('Por favor ingrese un correo válido');
                }
            }

            function cancelarOperacion() {
                document.getElementById('op').style.display = 'none';
            }
        </script>
        <link href="../Styles/certificado.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div style="width: 40px; height: 30px; float: left;">
                <input type="button" onclick="impre('container');return false" style="background-image: url('../Images/icons/icon_print.png');
                    background-repeat: no-repeat; background-position: center center; height: 29px; width: 35px; background-color: #FFFFFF; border: none;" />
            </div>
            <div id="divEmailButton" style="width: 40px; height: 30px; float: left;">
                <telerik:RadButton ID="BtnCorreo" runat="server" Text="EMail" ForeColor="Black"
                    Style="height: 32px; width: 32px;" ToolTip="Envia Email" OnClick="BtnCorreo_Click">
                    <Image ImageUrl="../Images/icon_email.png" IsBackgroundImage="False" />
                </telerik:RadButton>
            </div>

            <div id="op" style="background-color: white; width: 300px; height: auto; display: none; position: absolute; z-index: 1; padding: 25px 25px; border-radius: 5px; box-shadow: 0 0 10px rgba(0,0,0,0.5);">
                <form style="clear: both;">
                    <div id="controlesCorreo">
                        <div style="margin-bottom: 10px;">
                            <label for="emailInput" style="display: block; color: #000; margin-bottom: 5px;">Correo Electrónico:</label>
                            <input type="email" runat="server"  id="emailDestino" style="width: 100%; padding: 5px; border: 1px solid #ccc; border-radius: 3px;" required="" />
                        </div>
        
                        <div style="text-align: right;">
                            <%--<button type="button" onclick="enviarCorreo()" 
                                    style="padding: 5px 10px; margin-right: 5px; border:1px solid #ccc; border-radius: 3px; cursor: pointer;">
                                Enviar
                            </button>--%>
                            <div style="display:flex">
                                <telerik:RadButton ID="RadButton2" runat="server" Text="Enviar correo" ForeColor="Black"
                                    Style="height: 22px; width: 32px; padding-right: 60px;" ToolTip="Envia Email">
                                </telerik:RadButton>
                                <telerik:RadButton ID="btnCancelar" runat="server" Text="Cancelar" 
                                    ForeColor="Black" 
                                    Style=" border:1px solid #ccc; border-radius: 3px; cursor: pointer;"
                                    ToolTip="Cancelar operación"
                                    OnClientClicked="cancelarOperacion"
                                    AutoPostBack="false">
                                </telerik:RadButton>
                            </div>
                        </div>
                    </div>
                    
                    <div id="mensajeExito"  style="width: 100%; padding: 5px; display: none;" >
                        <p>Envíado correctamente</p>
                        <hr />
                    </div>
                </form>
            </div>
            
            <div id="container">
                <div id="banner">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/background_banner_certificado3.jpg" />
                </div>
                <div id="content">
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
                    <div class="row_text01">
                        Certifica que el Sr. (a). <strong><asp:Label ID="Nombre" runat="server"></asp:Label></strong>
                    </div>
                    <div class="row_text01" runat="server" id="tex01">
                        Ha adquirido los siguientes sistemas:
                    </div>
                    <div class="row_secc01" runat="server" id="grid">
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
                                        <FooterStyle BorderWidth="350px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="330px" />
                                        <ItemStyle Width="350px" HorizontalAlign="Center"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="False" AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="FECHA DE INSTALACION" UniqueName="FECHAINSTALACION" DataFormatString="{0:dd/MMMM/yyyy}"
                                        DataField="fecha_inicial">
                                        <FooterStyle BorderWidth="170px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="168px" />
                                        <ItemStyle Width="170px" HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn Display="False" AllowFiltering="False" FilterControlAltText="Filter column column"
                                        HeaderText="VIGENCIA DEL SERVICIO" UniqueName="COBERTURA" DataFormatString="{0:dd/MMMM/yyyy}"
                                        DataField="fecha_fin">
                                        <FooterStyle BorderWidth="168px" />
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Width="168px" />
                                        <ItemStyle Width="168px" HorizontalAlign="Center" />
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
                    <div class="row_text02" runat="server" id="tex02">
                        *La vigencia del servicio se contará desde la instalación del equipo*
                    </div>
                    <div class="row_text02" runat="server" id="tex05">
                         Con Orden de Instalación en el vehículo:
                    </div>
                    <div class="row_text02" runat="server" id="tex03">
                       En el vehículo o embarcación con las siguientes características:
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
                    <div class="row_text02" >                    </div>
                    <div class="row_text02" runat="server" id="tex06">
                         <center>El presente certificado, no constituye confirmación de instalación del equipo.</center>
                    </div>
                    <div class="row_secc01">
                        <asp:Image ID="imgqrgenerator" runat="server" Height="76px" />
                    </div>
                    <div class="row_text02" runat="server" id="tex07">
                       Los términos y condiciones del servicio pueden ser consultadas en nuestra página web
                    </div>                    
                    <div class="row_text01">
                        <strong>
                            <asp:Label ID="lblfechahoraemision" runat="server"></asp:Label>
                        </strong>
                    </div>

                     <div id="footer">
                         <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/background_footer_certificado3.jpg"  Width="807px" />
                     </div>
                </div>
                <telerik:RadScriptManager ID="RadScriptManager2" runat="server"></telerik:RadScriptManager>
                <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel2">
                    <AjaxSettings>
                        <telerik:AjaxSetting AjaxControlID="RadButton2">
                            <UpdatedControls>
                                <telerik:AjaxUpdatedControl ControlID="RadButton2"  LoadingPanelID="RadAjaxLoadingPanel2" />
                                <telerik:AjaxUpdatedControl ControlID="emailDestino"  LoadingPanelID="RadAjaxLoadingPanel2" />
                            </UpdatedControls>

                        </telerik:AjaxSetting>

                    </AjaxSettings>

                </telerik:RadAjaxManager>
                <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Simple"></telerik:RadAjaxLoadingPanel>
            </div>
        </form>
    </body>
</html>
