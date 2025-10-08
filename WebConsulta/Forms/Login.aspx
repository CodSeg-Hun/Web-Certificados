<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="WebConsulta.Login" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="DevExpress.Web.ASPxEditors.v12.1, Version=12.1.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title>Login</title>
        <meta name="robots" content="ALL" />
        <meta name="revisit-after" content="1 days" />
        <meta charset="UTF-8" />
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <meta name="viewport" content="width=device-width, initial-scale=1" />
        <meta http-equiv="X-UA-Compatible" content="IE=8; IE=9; IE=10; IE=EmulateIE8; IE=7; IE=EmulateIE7 ; IE=edge" />
       <%-- <meta name="robots" content="noindex" />--%>
        <script type="text/javascript">
            var message = "© Carseg S.A. 2012. Todos los derechos reservados";
            function clickIE4() 
            {
                if (event.button == 2) 
                {
                    alert(message);
                    return false;
                }
            }

            function clickNS4(e) 
            {
                if (document.layers || document.getElementById && !document.all) 
                {
                    if (e.which == 2 || e.which == 3) 
                    {
                        alert(message);
                        return false;
                    }
                }
            }

            if (document.layers) 
            {
                document.captureEvents(Event.MOUSEDOWN);
                document.onmousedown = clickNS4;
            }  else if (document.all && !document.getElementById) 
            {
                document.onmousedown = clickIE4;
            }

            document.oncontextmenu = new Function("alert(message);return false")

        </script>
        <link rel="shortcut icon" href="../Images/favicon.ico" />
        <link rel="icon" type="image/gif" href="../Images/animated_favicon1.gif" />
       <%-- <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
        <meta http-equiv="X-UA-Compatible" content="IE=100" />
        <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7" />--%>
        <link href="../Styles/styleblack.css" rel="stylesheet" type="text/css" />
        <script type="text/javascript">
            window.history.forward();
            function noBack() { window.history.forward(); }
        </script>
    </head>
    <body oncontextmenu="return false">
        <form id="form2" runat="server">
        <%--    <div class="bottom_img">
        </div>
        <div id="inhalt">
            <div class="region01_adm">
            </div>
            <div class="region02_adm">
            </div>
            <div class="region03">
                <div class="region03_01">
                </div>
                <div class="region03_02">--%><%--<br />
                    <br />
                    <br />--%><%--</div>
                <div class="region03_03">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_usuario"
                        Text="Debe ingresar un usuario" runat="Server" Font-Bold="False" ForeColor="Maroon" />
                    <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_password"
                        Text="Debe ingresar un password" runat="Server" Font-Bold="False" ForeColor="Maroon" />
                    <br />
                    <br />
                </div>
                <div class="region03_04">--%><%--</div>
                <div class="region03_05">
                    <div class="icon_password">
                    </div>
                    <asp:HyperLink ID="hyp_pass" runat="server" Font-Overline="False" Font-Underline="False"
                        ForeColor="#006699" NavigateUrl="~/Forms/ReseteaPassword.aspx" Font-Bold="False">Recuperar contraseña</asp:HyperLink>
                </div>
            </div>
            <div class="region04">
                &copy; Carseg S.A. 2012. Todos los derechos reservados |
            </div>
            &nbsp;&nbsp;&nbsp;
        </div>--%><%--    --%>
            <div class="screen_principal">
                <div class="screen_seccion01">
                    <div class="label">
                        Usuario:
                    </div>
                    <div class="textbox">
                        <asp:TextBox ID="txt_Usuario" runat="server" Width="210px" TabIndex="1" BorderStyle="None" Height="20px" MaxLength="6"></asp:TextBox>
                    </div>
                    <div class="label">
                        Password..:
                    </div>
                    <div class="textbox">
                        <asp:TextBox ID="txt_password" runat="server" Width="210px" TabIndex="2" TextMode="Password" BorderStyle="None" Height="20px" MaxLength="20" onKeyDown="submitButton(event)"></asp:TextBox>
                    </div>
                    <div class="button">
                        <telerik:RadButton ID="BtnLogin" runat="server" Height="26px" Text="Ingresar" Width="90px">
                        </telerik:RadButton>
                    </div>
                    <div class="label">
                        <asp:Label ID="lbl_ip" runat="server" Font-Bold="False" ForeColor="White"></asp:Label>
                    </div>
                    <div class="label_aviso">
                        <asp:Label ID="lbl_msg_login" runat="server" Font-Bold="False" ForeColor="White"></asp:Label>
                    </div>
                    <div class="label">
                        <asp:Label ID="lbl_iphost2" runat="server" Font-Bold="False" ForeColor="White"></asp:Label>
                    </div>
                </div>
            </div>
            <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
                <script type="text/javascript">
                    function submitButton(event) 
                    {
                        if (event.which == 13) 
                        {
                            $('#btn_login').trigger('click');
                        }
                    }
                </script>
            </telerik:RadCodeBlock>
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
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Default"  AnimationDuration="5">
            </telerik:RadAjaxLoadingPanel>
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="BtnLogin">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="txt_Usuario" />
                            <telerik:AjaxUpdatedControl ControlID="txt_password" />
                            <telerik:AjaxUpdatedControl ControlID="lbl_msg_login" />
                            <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator1" />
                            <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidator2" />
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
        </form>
    </body>
</html>
