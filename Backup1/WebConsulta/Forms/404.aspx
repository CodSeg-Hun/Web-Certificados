<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="404.aspx.vb" Inherits="WebConsulta._ErrorAplicacion" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <!--Inicio Metatag para impedir robots -->
        <meta name="robots" content="noindex" />
        <!--Fin Metatag para impedir robots -->
        <!--Inicio Script para bloquear click derecho -->
        <script language="javascript" type="text/javascript">
            var message = "Click derecho deshabilitado";
            function clickIE4() {
                if (event.button == 2) {
                    alert(message);
                    return false;
                }
            }

            function clickNS4(e) {
                if (document.layers || document.getElementById && !document.all) {
                    if (e.which == 2 || e.which == 3) {
                        alert(message);
                        return false;
                    }
                }
            }
            if (document.layers) {
                document.captureEvents(Event.MOUSEDOWN);
                document.onmousedown = clickNS4;
            }
            else if (document.all && !document.getElementById) {
                document.onmousedown = clickIE4;
            }
            document.oncontextmenu = new Function("alert(message);return false")
        </script>
        <!--Fin Script para bloquear click derecho -->
        <title>Error en el Sistema</title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />  
        <link href="../Styles/404_stylesheet.css" rel="stylesheet" type="text/css" />
        <script src="../Script/js/404_jquery.pngFix.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
        	<div id="warp">
		        <!-- Header top -->
		        <div id="header_top"></div>
		        <!-- End header top -->
		        <!-- Header -->
		        <div id="header">
			        <h2>Mensaje del Sistema</h2>
		        </div>
		        <!-- End Header -->
		        <!-- The content div -->
		        <div id="content">
			        <!-- text -->
			        <div id="text">
				        <!-- The info text -->
				        <h3><asp:Label ID="lbl_error_msg" runat="server" ForeColor="Maroon"></asp:Label></h3>
                        <br />
				        <!-- End info text -->
				        <!-- Page links -->
				        <ul>
                            Ir a: 
					        <li><a href="Login.aspx">&raquo; Login Consulta General</a></li>
					        <%--<li><a href="http://torch/intranet2011/Default.aspx">&raquo; Intranet</a></li>--%>
				        </ul>
				        <!-- End page links -->	
			        </div>
			        <!-- End info text -->
			        <!-- Book icon -->
			        <img id="book" src="../images/img-01.png" alt="Book iCon" />
			        <!-- End Book icon -->
			        <div style="clear:both;"></div>
		        </div>
		        <!-- End Content -->
		        <!-- Footer bottom -->
		        <div id="footer_bottom"></div>
		        <!-- End Footer bottom -->
		        <div style="clear:both;"></div>
	        </div>
	        <!-- End Warp around everything -->
        </form>
    </body>
</html>
