<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="CambiarContraseña.aspx.vb" Inherits="WebConsulta.CambiarContraseña" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../Styles/newlogin.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager2" runat="server"></telerik:RadScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel2">
         <AjaxSettings>
             <telerik:AjaxSetting AjaxControlID="BtnCambiar">
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="txt_password_last" LoadingPanelID="RadAjaxLoadingPanel2" />
                     <telerik:AjaxUpdatedControl ControlID="txt_password_new" LoadingPanelID="RadAjaxLoadingPanel2" />
                     <telerik:AjaxUpdatedControl ControlID="lbl_msg_login"  LoadingPanelID="RadAjaxLoadingPanel2" />
                 </UpdatedControls>
             </telerik:AjaxSetting>
             <telerik:AjaxSetting AjaxControlID="BtnCancelar">
                 <UpdatedControls>
                     <telerik:AjaxUpdatedControl ControlID="txt_password_last" LoadingPanelID="RadAjaxLoadingPanel2" />
                     <telerik:AjaxUpdatedControl ControlID="txt_password_new" LoadingPanelID="RadAjaxLoadingPanel2" />
                     <telerik:AjaxUpdatedControl ControlID="lbl_msg_login" LoadingPanelID="RadAjaxLoadingPanel2" />
                 </UpdatedControls>
             </telerik:AjaxSetting>
         </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" Skin="Simple"></telerik:RadAjaxLoadingPanel>
    <div id="inhalt">
        <div class="region02_pass"></div>
        <div class="region03_pass">
            <div class="region03_01">
                <h2>
                    <asp:Label ID="lbl_msg_caduca" runat="server" Font-Bold="False" ForeColor="Maroon">Modificar contraseña </asp:Label>
                </h2>
            </div>
            <div class="region03_02_pass">
                <asp:Label ID="lbl_usuario" runat="server" Text="Su usuario es:" />
                <asp:TextBox ID="txt_usuario" runat="server" Width="230px" MaxLength="15" TabIndex="1" BorderStyle="None" Font-Bold="True" ForeColor="#006699" Enabled="False" />
                <br />
                <br />
                <asp:Label ID="lbl_password_last" runat="server" Text="Ingrese su anterior contraseña" />
                <br />
                <telerik:RadTextBox ID="txt_password_last" runat="server" TextMode="Password" Width="230px" Height="22px" MaxLength="15" TabIndex="2" />
                <br />
                <br />
                <asp:Label ID="lbl_password_new" runat="server" Text="Ingrese su nueva contraseña" />
                <br />
                <telerik:RadTextBox ID="txt_password_new" runat="server" TextMode="Password" Width="230px" Height="22px" MaxLength="15" TabIndex="3" />
            </div>
            <div class="region03_03_pass">
                <asp:Label ID="lbl_msg_login" runat="server" Font-Bold="False" />
                <br />
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txt_password_last"
                    Text="Debe ingresar un password" runat="Server" Font-Bold="False" />--%>
                <br />
                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txt_password_new"
                    Text="Debe ingresar una nueva password" runat="Server" Font-Bold="False" />--%>
                <br />
                <br />
            </div>
            <div class="region03_06_pass">
                <telerik:RadButton ID="BtnCambiar" runat="server" Text="Cambiar Contraseña" OnClick="BtnCambiar_Click" Width="139px" Height="22px" />
            </div>
            <div class="region03_06_pass">
                <telerik:RadButton ID="BtnCancelar" runat="server" Text="Cancelar" OnClick="BtnCancelar_Click"  Width="139px" Height="22px" />
            </div>
            <br />
            <br />
        </div>
    </div>
</asp:Content>
