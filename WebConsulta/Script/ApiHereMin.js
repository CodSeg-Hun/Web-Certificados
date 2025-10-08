/ *
 * Explicación de por qué el diseño parece tan complicado:
 * El contenedor de la interfaz de usuario necesita una posición (absoluta o relativa) para evitar problemas de índice z (DomMarker en la parte superior de la interfaz de usuario)
 * Por eso tiene estos estilos adicionales:
 * posición: absoluta;
 * ancho: 100%;
 * altura: 100%;
 * Para evitar que el contenedor de la interfaz de usuario capture todos los eventos, el contenedor es desplazado por
 * izquierda: 100%;
 * Para neutralizar el desplazamiento de los elementos de la IU dentro del contenedor de la IU, se necesitan los siguientes ajustes:
 * - InfoBubble (.H_ib): izquierda: -100%;
 * - ancla izquierda (.H_l_left): margin-left: -100%;
 * - ancla central (.H_l_center): izquierda: -50%; (quedó: 50%)
 * - ancla derecha (.H_l_right): derecha: 100%; (tenía razón: 0)
 * margen izquierdo: -100%;
 * /

.H_ui {
  tamaño de fuente: 10px;
  familia de fuentes: "Lucida Grande", Arial, Helvetica, sans-serif;
  -moz-user-select: ninguno;
  -khtml-user-select: ninguno;
  -webkit-user-select: ninguno;
  -o-user-select: ninguno;
  -ms-user-select: ninguno;
  / * coloque la interfaz de usuario en la parte superior de la impresión para que se pueda hacer clic en ambos * /
  índice z: 2;
  posición: absoluta;
  ancho: 100%;
  altura: 100%;
  izquierda: 100%;
}
.H_ui * {
  / * normalizar en caso de que a alguna otra normalización CSS le gusten las cosas de manera diferente * /
  tamaño de caja: caja de contenido;
  -moz-box-sizing: content-box;
}
.H_noevs {
  eventos de puntero: ninguno;
}

/ *
 * Diseño
 * /
.H_l_left {
  posición: absoluta;
  izquierda: 16px;
  margen izquierdo: -100%;
}
.H_l_center {
  posición: absoluta;
  izquierda: -50%;
}
.H_l_right {
  posición: absoluta;
  derecha: calc (100% + 16px);
  margen izquierdo: -100%;
}
.H_l_top {
  arriba: 16px;
}
.H_l_middle {
  arriba: 50%;
}
.H_l_bottom {
  abajo: 16px;
}

/ * Corregir MAPSJS-579 para navegadores modernos * /
[class ^ = H_l_] {
    eventos de puntero: ninguno;
}
.H_ctl {
    / * hackear para IE9-10, auto no funciona para ellos * /
    puntero-eventos: visiblePainted;
    eventos de puntero: auto;
}

.H_l_horizontal .H_ctl {
  flotador izquierdo;
}

.H_l_anchor {
  Limpia los dos;
  flotar derecho;
}

.H_l_vertical .H_ctl {
  Limpia los dos;
}

.H_l_right .H_l_vertical .H_ctl {
  flotar derecho;
}

.H_l_right.H_l_middle.H_l_vertical .H_ctl {
  flotar derecho;
}

/ **
 * Estilos de elementos
 * /

.H_ctl {
  margen: .8em;
  posición: relativa;
  -ms-touch-action: ninguno;
}

.H_btn {
  cursor: puntero;
}

.H_grp .H_btn,
.H_rdo_buttons .H_btn {
    sombra de caja: ninguna;
}
.H_grp .H_btn.H_active,
.H_rdo_buttons .H_btn.H_active {
  fondo: ninguno;
}

.H_btn {
  sombra de caja: 0em 0 0.4em 0 rgba (15, 22, 33, 0.6);
  radio de borde: 0.5em;
  ancho: 4em;
  altura: 4em;
  fondo: #fff;
}

.H_disabled,
.H_disabled: hover {
  cursor: predeterminado;
}

.H_rdo_title {
  tamaño de fuente: 14px;
  altura: 40px;
  altura de línea: 40px;
  color de fondo: # 575B63;
  color: #fff;
  padding-left: 16px;
  padding-right: 16px;
  radio del borde: 5px 5px 0 0;
  margen inferior: 8px;
  cursor: predeterminado;
}
.H_rdo ul {
  estilo de lista: ninguno;
  margen: 0 automático;
  acolchado: 0;
}

/ **
 * Elementos base
 * /
.H_ctl.H_grp {
  fondo: #fff;
  sombra de caja: 0em 0 0.4em 0 rgba (15, 22, 33, 0.6);
  radio de borde: 0.5em;
}
/ * Divisor de botones * /
.H_zoom .H_el {
  posición: relativa;
}
.H_l_vertical .H_zoom .H_el: last-child :: after,
.H_l_horizontal .H_zoom .H_el: last-child :: after {
  contenido: ninguno;
}

.H_l_vertical .H_zoom .H_el {
  margen inferior: 0.1em;
}
.H_l_vertical .H_zoom .H_el: last-child {
  margen inferior: 0;
}
.H_l_vertical .H_zoom .H_el :: after {
  contenido: "";
  posición: absoluta;
  ancho: 2.6em;
  altura: 0.1em;
  abajo: -0,1em;
  izquierda: 0,7em;
  fondo: rgba (15, 22, 33, 0,1);
}

.H_l_horizontal .H_zoom .H_el {
  margen derecho: 0.1em;
}
.H_l_horizontal .H_zoom .H_el: last-child {
  margen derecho: 0;
}
.H_l_horizontal .H_zoom .H_el :: after {
  contenido: "";
  posición: absoluta;
  ancho: 0.1em;
  altura: 2.6em;
  superior: 0,7em;
  derecha: -0.1em;
  fondo: rgba (15, 22, 33, 0,1);
}
/ * Fin: Botón divisor * /
.H_l_horizontal .H_grp .H_btn,
.H_l_vertical .H_ctl {
  flotador izquierdo;
}


/ ** Panel de menú * /
.H_overlay {
  tamaño de fuente: 14px;
  color: rgba (15, 22, 33, 0,6);
  caja-sombra: 0px 0 4px 0 rgba (15, 22, 33, 0.6);
  radio del borde: 5px;
  posición: absoluta;
  ancho mínimo: 200px;
  fondo: #fff;
  pantalla: ninguna;
  índice z: 100;
  fondo acolchado: 4px;
}

.H_overlay .H_separator {
  contenido: "";
  posición: relativa;
  bloqueo de pantalla;
  margen: 8px 16px 8px 16px;
  altura: 1px;
  fondo: rgba (15, 22, 33, 0,1);
}

.H_overlay .H_btn,
.H_overlay .H_rdo li {
  ancho: 184px;
  altura: 24px;
  altura de línea: 24px;
  relleno: 8px 16px;
}
.H_overlay .H_btn {
  radio del borde: 0px;
}

.H_overlay .H_btn: desplazarse,
.H_overlay .H_rdo li: hover {
  color: rgba (15, 22, 33, 0.8);
}

.H_overlay .H_btn.H_disabled,
.H_overlay .H_rdo.H_disabled li,
.H_overlay .H_btn.H_disabled: hover,
.H_overlay .H_rdo.H_disabled li: hover {
  color: rgba (15, 22, 33, 0,2);
}

.H_overlay .H_btn.H_active,
.H_overlay .H_rdo.H_active li,
.H_overlay .H_btn.H_active: desplazarse,
.H_overlay .H_rdo.H_active li: hover {
  color: # 0F1621;
}

.H_overlay> *: last-child {
  Limpia los dos;
}
.H_overlay> .H_btn {
  espacio en blanco: nowrap;
}

.H_overlay.H_open {
  bloqueo de pantalla;
}

.H_overlay :: before, .H_overlay :: after {
  contenido: " ";
  ancho: 0;
  altura: 0;
  estilo de borde: sólido;
  posición: absoluta;
}
.H_overlay.H_left :: before {
  ancho del borde: 10px 10px 10px 0;
  border-color: transparente rgba (15, 22, 33, 0.2) transparente transparente;
  izquierda: -12px;
}
.H_overlay.H_left :: after {
  ancho del borde: 10px 10px 10px 0;
  border-color: transparente #fff transparente transparente;
  izquierda: -10px;
}
.H_overlay.H_top.H_left :: after {
  color del borde: transparente # 575B63 transparente transparente;
}

.H_overlay.H_right :: before {
  ancho del borde: 10px 0 10px 10px;
  color del borde: transparente transparente transparente rgba (15, 22, 33, 0.2);
  izquierda: calc (100% + 1px);
}
.H_overlay.H_right :: after {
  ancho del borde: 10px 0 10px 10px;
  border-color: transparente transparente transparente #fff;
  izquierda: 100%;
}
.H_overlay.H_top.H_right :: after {
  color del borde: transparente transparente transparente # 575B63;
}

.H_overlay.H_top :: antes,
.H_overlay.H_top :: after {
  arriba: 10px;
}
.H_overlay.H_bottom :: antes,
.H_overlay.H_bottom :: after {
  abajo: 10px;
}
.H_overlay.H_middle :: antes,
.H_overlay.H_middle :: after {
  arriba: 50%;
  margen superior: -10px;
}

.H_overlay.H_top.H_center :: before {
  ancho del borde: 0 10px 10px 10px;
  border-color: transparente transparente rgba (15, 22, 33, 0.2) transparente;
  superior: -11px;
  izquierda: 50%;
  margen izquierdo: -10px;
}
.H_overlay.H_top.H_center :: after {
  ancho del borde: 0 10px 10px 10px;
  color del borde: transparente transparente # 575B63 transparente;
  superior: -10px;
  izquierda: 50%;
  margen izquierdo: -10px;
}

.H_overlay.H_bottom.H_center :: before {
  ancho del borde: 10px 10px 0 10px;
  border-color: rgba (15, 22, 33, 0.2) transparente transparente transparente;
  abajo: -11px;
  izquierda: 50%;
  margen izquierdo: -10px;
}
.H_overlay.H_bottom.H_center :: after {
  ancho del borde: 10px 10px 0 10px;
  border-color: #fff transparente transparente transparente;
  abajo: -10px;
  izquierda: 50%;
  margen izquierdo: -10px;
}


/ ** InfoBubble * /
.H_ib {
  posición: absoluta;
  izquierda: .91em;
  izquierda: -100%;
}
.H_ib_tail {
  posición: absoluta;
  ancho: 20px;
  altura: 10px;
  margen: -10px -10px;
}

.H_ib_tail :: before {
  abajo: -1px;
  ancho del borde: 10px;
  posición: absoluta;
  bloqueo de pantalla;
  contenido: "";
  color del borde: transparente;
  estilo de borde: sólido;
  derecha: 0px;
}

.H_ib_tail :: after {
  abajo: 0;
  posición: absoluta;
  bloqueo de pantalla;
  contenido: "";
  color del borde: blanco;
  estilo de borde: sólido;
  ancho del borde: 10px;
}

.H_ib.H_ib_top .H_ib_tail :: after {
  ancho del borde: 10px 10px 0px 10px;
  color del borde: blanco transparente;
}

.H_ib.H_ib_top .H_ib_tail :: before {
  border-top-color: rgba (15, 22, 33, 0.2);
  border-bottom-width: 0px;
}

.H_ib_notail .H_ib_tail {
  pantalla: ninguna;
}
.H_ib_body {
  fondo: blanco;
  posición: absoluta;
  abajo: .5em;
  acolchado: 0;
  derecha: 0px;
  radio del borde: 5px;
  margen derecho: -3em;
  caja-sombra: 0px 0 4px 0 rgba (15, 22, 33, 0.6);
  margen inferior: 0.5em;
}

.H_ib_close {
  tamaño de fuente: .6em;
  posición: absoluta;
  derecha: 12px;
  ancho: 12px;
  altura: 12px;
  cursor: puntero;
  superior: 12px;
  índice z: 1;
  fondo: ninguno;
  sombra de caja: ninguna;
}

.H_ib_close svg.H_icon {
    arriba: 0;
    transformar: ninguno;
    ancho: auto;
    altura: auto;
}

.H_ib_noclose .H_ib_close {
  pantalla: ninguna;
}
.H_ib_noclose .H_ib_body {
  relleno: 0 0 0 0;
}

.H_ib_content {
  ancho mínimo: 6em;
  fuente: 14px "Lucida Grande", Arial, Helvetica, sans-serif;
  altura de línea: 24px;
  margen: 16px 28px 20px 16px;
  color: rgba (15,22,33, .8);
  selección de usuario: texto;
  -moz-user-select: texto;
  -khtml-user-select: texto;
  -webkit-user-select: texto;
  -o-user-select: texto;
  -ms-user-select: texto;
}


/ * ############################################## ## SLIDER ############################################# ######### * /

.H_l_horizontal .H_zoom_slider {
  ancho mínimo: 262px;
}
.H_slider {
  cursor: puntero;
}
.H_l_horizontal.H_slider {
  flotador izquierdo;
  altura: 4em;
  ancho: auto;
  acolchado: 0 1em;
}

.H_slider .H_slider_track {
  ancho: 0.4em;
  altura: 100%;
}

.H_l_horizontal.H_slider .H_slider_track {
  altura: 0.4em;
  ancho: 100%;
}

.H_l_horizontal.H_slider .H_slider_cont {
  altura: 100%;
}

.H_l_horizontal.H_slider .H_slider_knob_cont {
  margen superior: -0,4em;
}

.H_slider {
  color de fondo: #fff;
  acolchado: 1em 0em;
  ancho: 4em;
}


.H_slider .H_slider_cont {
  posición: relativa;
}

.H_slider .H_slider_knob_cont,
.H_slider .H_slider_knob_halo {
  ancho: 1.8em;
  altura: 1.8em;
  margen izquierdo: 0em;
  radio de borde: 9em;
}


.H_slider .H_slider_knob {
  ancho: 1.2em;
  altura: 1.2em;
  color de fondo: blanco;
  radio de borde: 9em;
  -webkit-transform: traducir (-50%, - 50%);
  -ms-transform: traducir (-50%, - 50%);
  transformar: traducir (-50%, - 50%);
  arriba: 50%;
  izquierda: 50%;
  sombra de caja: 0em 0 0.5em 0 rgba (15, 22, 33, 0.6);
  posición: absoluta;
}

.H_slider: desplazarse .H_slider_knob {
  sombra de caja: 0em 0 0.5em 0 rgba (15, 22, 33, 0.8);
}
.H_slider.H_disabled .H_slider_knob {
  caja de sombra: 0em 0 0.5em 0 rgba (15, 22, 33, 0.2);
}
.H_slider.H_slider_active .H_slider_knob {
  sombra de caja: 0em 0 0.5em 0 rgba (15, 22, 33, 1);
}

.H_slider: desplazarse .H_slider_track {
  color de fondo: rgba (15, 22, 33, 0.8);
}

.H_disabled .H_slider_track {
  color de fondo: rgba (15, 22, 33, 0.2);
}
.H_slider: hover .H_slider_track_active {
  color de fondo: rgba (0, 182, 178, 0.8);
}
.H_disabled .H_slider_track_active {
  color de fondo: rgba (0, 182, 178, 0.2);
}
.H_slider.H_slider_active .H_slider_track {
  color de fondo: rgba (15, 22, 33, 1.0);
}
.H_slider.H_slider_active .H_slider_track_active {
  color de fondo: rgba (0, 182, 178, 1.0);
}

.H_slider .H_slider_track,
.H_slider .H_slider_knob_cont {
  posición: relativa;
  arriba: 50%;
  izquierda: 50%;
  -webkit-transform: traducir (-50%, - 50%);
  -ms-transform: traducir (-50%, - 50%);
  transformar: traducir (-50%, - 50%);
}

.H_slider .H_slider_track {
  color de fondo: rgba (15, 22, 33, 0.6);
  desbordamiento: oculto;
}
.H_slider .H_slider_track_active {
  color de fondo: # 00B6B2;
  posición: absoluta;
  transformar: traducir (-50%, 0%);
}

.H_slider.H_disabled .H_slider_track {
  color de fondo: rgba (15, 22, 33, 0.2);
}
.H_slider.H_disabled .H_slider_track_active {
  color de fondo: # bae2e3;
}

.H_slider.H_l_horizontal .H_slider_track_active {
  transformar: traducir (-200%, -50%);
}

.H_slider.H_disabled {
  cursor: predeterminado;
}

/ * ############################################# CONTEXT MENÚ ############################################## #### * /
.H_context_menu {
  tamaño de fuente: 14px;
  ancho mínimo: 158px;
  ancho máximo: 40%;
  sombra de caja: 0em 0 0.4em 0 rgba (15, 22, 33, 0.6);
  posición: absoluta;
  izquierda: -100%;
  arriba: 0;
  color: rgba (15, 22, 33, 0,6);
  color de fondo: #fff;
  -moz-border-radius: 5px;
  -webkit-border-radius: 5px;
  -o-radio-borde: 5px;
  radio del borde: 5px;
  relleno: 16px 16px 4px 16px;
  -moz-user-select: inicial;
  -khtml-user-select: inicial;
  -webkit-user-select: inicial;
  -o-user-select: inicial;
  -ms-user-select: inicial;
  índice z: 200;
}

.H_context_menu_closed {
    pantalla: ninguna;
}

.H_context_menu_item {
  desbordamiento de texto: puntos suspensivos;
  desbordamiento: oculto;
  altura de línea: 24px;
  margen inferior: 16px;
  esquema: ninguno;
}

.H_context_menu_item.clickable: hover {
  color: rgba (15, 22, 33, 0.8);
  cursor: puntero;
}
.H_context_menu_item.disabled: hover,
.H_context_menu_item.disabled {
    fondo: transparente! importante;
    color: rgba (15, 22, 33, 0,2);
    cursor: predeterminado! importante;

    -moz-user-select: ninguno;
    -khtml-user-select: ninguno;
    -webkit-user-select: ninguno;
    -o-user-select: ninguno;
    -ms-user-select: ninguno;
}
.H_context_menu_item_separator {
    altura: 0;
    borde superior: rgba sólido de 1px (15, 22, 33, 0.1);
    fondo acolchado: 16px;
    altura de línea: 0;
    tamaño de fuente: 0;
}


/ * ############################################## # BARRA DE ESCALA ############################################# ####### * /
.H_scalebar {
  margen superior: 36px;
  sombra de caja: ninguna;
  pantalla: flex;
  alinear-elementos: centro;
  sombra de texto:
    -1px -1px 0 rgba (255, 255, 255, 0.7),
    1 px -1 px 0 rgba (255, 255, 255, 0.7),
    -1px 1px 0 rgba (255, 255, 255, 0.7),
    1 px 1 px 0 rgba (255, 255, 255, 0.7);
}


/ * ################################# MEDICIÓN DE DISTANCIA E INCIDENTES DE TRÁFICO ######## ########################### * /

.H_tib_content {
  ancho: 25em;
  posición: relativa;
  margen: -16px -28px -20px -16px;
}
.H_tib .H_tib_desc {relleno: 0px 16px 20px 16px}
.H_tib .H_tib_time {color: rgba (15,22,33, .4); margin-top: 0.8em;}
.H_tib_right {float: right; }

.H_tib .H_btn> svg.H_icon {
  relleno: rgba (255,255,255, .6);
}

.H_tib .H_btn: hover> svg.H_icon {
  relleno: blanco;
}

.H_dm_label {
  fuente: 12px "Lucida Grande", Arial, Helvetica, sans-serif;
  de color negro;
  sombra de texto: 1px 1px .5px #FFF, 1px -1px .5px #FFF, -1px 1px .5px #FFF, -1px -1px .5px #FFF;
  espacio en blanco: nowrap;
  margen izquierdo: 12px;
  margen superior: -7px;
  / * Esto no funcionará en IE9, ¡pero se acepta! * /
  eventos de puntero: ninguno;
}


/ * ############################################## ### ICON ############################################# ########### * /
svg.H_icon {
  bloqueo de pantalla;
  posición: relativa;
  arriba: 50%;
  transformar: translateY (-50%);
  margen: automático;
  ancho: 24px;
  altura: 24px;
  relleno: rgba (15, 22, 33, 0,6);
}
svg.H_icon .H_icon_stroke {
  accidente cerebrovascular: rgba (15, 22, 33, 0,6);
  llenar: ninguno;
}
.H_btn: hover> svg.H_icon {
  relleno: rgba (15, 22, 33, 0.8);
}
.H_btn: hover> svg.H_icon .H_icon_stroke {
  accidente cerebrovascular: rgba (15, 22, 33, 0,8);
}
.H_btn.H_active {
  color de fondo: # CFD0D3;
}
.H_rdo .H_btn.H_active {
  fondo: ninguno;
}

.H_active> svg.H_icon,
.H_active: hover> svg.H_icon {
  llenar: # 0F1621! importante;
}
.H_active> svg.H_icon .H_icon_stroke,
.H_active: hover> svg.H_icon .H_icon_stroke {
  trazo: # 0F1621;
}

.H_disabled svg.H_icon,
.H_disabled: hover svg.H_icon {
  relleno: rgba (15, 22, 33, 0,2);
  cursor: predeterminado;
}
.H_disabled svg.H_icon .H_icon_stroke,
.H_disabled: hover svg.H_icon .H_icon_stroke {
  accidente cerebrovascular: rgba (15, 22, 33, 0,2);
}

/*############################################### VISIÓN DE CONJUNTO MAPA ################################################# #### * /
.H_overview {
  transición: ancho 0,2 s, altura 0,2 s, margen superior 0,2 s, relleno 0,2 s;
  ancho: 0em;
  altura: 0em;
  desbordamiento: oculto;
  cursor: predeterminado;
  posición: absoluta;
  margen: automático;
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  tamaño de caja: caja de borde;
  sombra de caja: 0em 0 0.4em 0 rgba (15, 22, 33, 0.6);
}

.H_l_vertical .H_overview_active {
    margen: automático 5px;
}

.H_l_horizontal .H_overview_active {
  margen: 5px automático;
}

.H_l_center .H_overview {
  izquierda: -9999px;
  derecha: -9999px;
}

.H_l_middle .H_overview {
  superior: -9999px;
  abajo: -9999px;
}

.H_l_right .H_overview {
  derecha: 100%;
}

.H_l_left .H_overview {
  izquierda: 100%;
}

.H_l_bottom .H_overview {
  abajo: 0;
}
.H_l_center.H_l_bottom .H_overview {
  abajo: 100%;
}

.H_l_top .H_overview {
  arriba: 0;
}
.H_l_center.H_l_top .H_overview {
  Top 100%;
}

.H_overview .H_overview_map {
  color de fondo: rgba (256,256,256,0.6);
  -webkit-box-sizing: border-box;
  -moz-box-sizing: border-box;
  tamaño de caja: caja de borde;
  sombra de caja: 0em 0 0.4em 0 rgba (15, 22, 33, 0.6);
}


.H_overview_map .H_ui {
  pantalla: ninguna;
}

.H_zoom_lasso {
  posición: absoluta;
  pantalla: ninguna;
  sombra de caja: 0em 0 0.4em 0 rgba (15, 22, 33, 0.6);
  índice z: 100000;
  color de fondo: rgba (15, 22, 33, 0.2);
}