<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="Administracion.Master" CodeBehind="Sitios.aspx.vb" Inherits="WebConsulta.Sitios" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager2" runat="server"></telerik:RadScriptManager>
    <div class="seccion_header_sitios">
        <iframe id="myframe" runat="server" width="100%" height="95%" frameborder="0"></iframe>
    </div>
</asp:Content>
