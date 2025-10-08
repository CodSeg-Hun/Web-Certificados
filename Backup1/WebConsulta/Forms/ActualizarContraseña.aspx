<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ActualizarContraseña.aspx.vb" Inherits="WebConsulta.ActualizarContraseña" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>Modificar Contraseña</title>
        <link href="../Styles/newlogin.css" rel="stylesheet" type="text/css" />
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div class="bottom_img" ></div>
                <div id="inhalt" >
                    <div class="region01_adm"> </div>
                    <div class="region02_pass"> </div>
                    <div class="region03_pass"> 
                        <div class="region03_01"> 
                            <h2>                
                                <asp:Label ID="lbl_msg_caduca" runat="server" Font-Bold="False"  ForeColor="Maroon" >Modificar contraseña </asp:Label>
                            </h2>
                        </div>
                        <div class="region03_02_pass">  
                            <asp:Label ID="lbl_usuario" runat="server" Text="Su usuario es:"></asp:Label>
                            <asp:TextBox ID="txt_usuario" runat="server" Width="230px" MaxLength="15" TabIndex="1" BorderStyle="None" Font-Bold="True" ForeColor="#006699" />
                            <br />
                            <br />
                            <asp:Label ID="lbl_password_last" runat="server" Text="Ingrese su anterior contraseña" />
                            <br />
                            <asp:TextBox ID="txt_password_last" runat="server" Width="230px" MaxLength="15" TabIndex="2" TextMode="Password" />
                            <br />
                            <br />
                            <asp:Label ID="lbl_password_new" runat="server" Text="Ingrese su nueva contraseña" />
                            <br />
                            <asp:TextBox ID="txt_password_new" runat="server" Width="230px" MaxLength="15" TabIndex="3" TextMode="Password" />
                        </div>
                        <div class="region03_03_pass"> 
                            <asp:Label ID="lbl_msg_login" runat="server" Font-Bold="False"></asp:Label>
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_password_last" Text="Debe ingresar un password" 
                                runat="Server" Font-Bold="False" />
                            <br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_password_new" Text="Debe ingresar una nueva password" 
                                runat="Server" Font-Bold="False" />
                            <br />
                            <br />
                        </div>
                        <div class="region03_06_pass"> 
                            <dx:ASPxButton ID="BtnValidar" runat="server" Text="Cambiar Contraseña"  Width="139px" TabIndex="4" Font-Names="Arial">
                            </dx:ASPxButton>
                        </div>
                        <div class="region03_06_pass"> 
                            <dx:ASPxButton ID="BtnCancelar" runat="server" Text="Cancelar" Width="139px" TabIndex="4" Font-Names="Arial" CausesValidation="False">
                            </dx:ASPxButton>
                        </div>
                        <br />
                        <br />
                        <div class="region03_05_pass"> 
                            <dx:ASPxButton ID="BtnRegresar" runat="server" Text="Regresar Login" Width="139px" TabIndex="5" 
                                CssFilePath="~/App_Themes/Youthful/{0}/styles.css" CssPostfix="Youthful" 
                                SpriteCssFilePath="~/App_Themes/Youthful/{0}/sprite.css">
                            </dx:ASPxButton>
                        </div>
                    </div>
                </div>
            </div>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Runat="server" Skin="Default">
            </telerik:RadAjaxLoadingPanel>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="BtnValidar">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator2" />
                            <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator3" />
                            <telerik:AjaxUpdatedControl ControlID="BtnValidar" />
                            <telerik:AjaxUpdatedControl ControlID="BtnRegresar" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        </form>
    </body>
</html>
