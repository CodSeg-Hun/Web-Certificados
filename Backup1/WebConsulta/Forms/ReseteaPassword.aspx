<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReseteaPassword.aspx.vb" Inherits="WebConsulta.ReseteaPassword" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <link href="../Styles/newlogin.css" rel="stylesheet" type="text/css" />
        <title>Recuperar Contraseña</title>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div class="bottom_img" ></div>
                <div id="inhalt" >
		            <div class="region01_adm"></div>
                    <div class="region02_recover"> </div>
                    <div class="region03_pass"> 
                        <div class="region03_01"> <h2> Recuperar Contraseña </h2></div>
                        <div class="region03_02_pass">  
                            <asp:Label ID="lbl_usuario" runat="server" Text="Su usuario es:"></asp:Label>
                            <br />
                            <div class="valid_01">
                                <div class="valid_01_01">
                                    <asp:TextBox ID="txt_usuario" runat="server" Width="147px" MaxLength="20" TabIndex="1" Font-Bold="True" ForeColor="#006699"></asp:TextBox>
                                </div>
                                <div class="valid_01_02">
                                    <dx:ASPxButton ID="btn_email" runat="server" Text="Validar" Width="25px" TabIndex="4">
                                    </dx:ASPxButton>
                                </div>
                            </div>
                            <br />
                            <asp:Label ID="lbl_email_registrado" runat="server" Text="Su email registrado es:"></asp:Label>
                            <br />
                            <asp:Label ID="lbl_email_registrado2" runat="server" Font-Bold="False"  ForeColor="Black"></asp:Label>
                            <br />
                            <br />
                            <asp:Label ID="lbl_email_nuevo" runat="server"  Text="Ingrese su email"></asp:Label>
                            <br />
                            <asp:TextBox ID="txt_email_nuevo" runat="server" Width="230px" MaxLength="50"  TabIndex="3"></asp:TextBox>
                        </div>
                        <div class="region03_03_pass"> 
                            <asp:Label ID="lbl_msg_login" runat="server" Font-Bold="False"></asp:Label>
                            <br />
                            <asp:RequiredFieldValidator ID="rfv_usuario" ControlToValidate="txt_usuario" Text="Debe ingresar su usuario" 
                                runat="Server" Font-Bold="False" />
                            <br />
                            <asp:RegularExpressionValidator ID="rfv_usuario02" ControlToValidate="txt_email_nuevo" runat="server" 
                                ErrorMessage="El email ingresado no es válido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Font-Bold="False">
                            </asp:RegularExpressionValidator>
                            <br />
                            <br />
                            <br />
                            <br />
                            <br />
                        </div>
                        <div class="region03_04_pass"> 
                            <dx:ASPxButton ID="btn_validar" runat="server" Text="Enviar Email"  Width="139px" TabIndex="4">
                            </dx:ASPxButton>
                            <br />
                        </div>
                        <div class="region03_04_pass"> 
                            <dx:ASPxButton ID="btn_regresar" runat="server" Text="Regresar Login"  Width="139px" TabIndex="5" CssFilePath="~/App_Themes/Office2003Olive/{0}/styles.css" 
                                    CssPostfix="Office2003Olive"  SpriteCssFilePath="~/App_Themes/Office2003Olive/{0}/sprite.css">
                            </dx:ASPxButton>
                        </div>
                        <br />
                        <br />
                    </div>
                </div>
            </div>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"   DefaultLoadingPanelID="RadAjaxLoadingPanel1">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="btn_email">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="lbl_email_registrado" />
                            <telerik:AjaxUpdatedControl ControlID="lbl_email_nuevo" />
                            <telerik:AjaxUpdatedControl ControlID="txt_email_nuevo" />
                            <telerik:AjaxUpdatedControl ControlID="lbl_msg_login" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="btn_validar">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="lbl_msg_login" />
                            <telerik:AjaxUpdatedControl ControlID="btn_regresar" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" Runat="server"  Skin="Default">
            </telerik:RadAjaxLoadingPanel>
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
        </form>
    </body>
</html>
