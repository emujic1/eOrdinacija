/*
 dhtmlxScheduler.Net v.3.2.0 Professional Evaluation

This software is covered by DHTMLX Evaluation License. Contact sales@dhtmlx.com to get Commercial or Enterprise license. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
scheduler.__recurring_template='<div class="dhx_form_repeat"> <form> <div class="dhx_repeat_left"> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="day" />Diariamente</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="week"/>Semanalment</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="month" checked />Mensualmente</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="repeat" value="year" />Anualmente</label> </div> <div class="dhx_repeat_divider"></div> <div class="dhx_repeat_center"> <div style="display:none;" id="dhx_repeat_day"> <label><input class="dhx_repeat_radio" type="radio" name="day_type" value="d"/>Cada</label><input class="dhx_repeat_text" type="text" name="day_count" value="1" />dia<br /> <label><input class="dhx_repeat_radio" type="radio" name="day_type" checked value="w"/>Cada jornada de trabajo</label> </div> <div style="display:none;" id="dhx_repeat_week"> Repetir cada<input class="dhx_repeat_text" type="text" name="week_count" value="1" />semana:<br /> <table class="dhx_repeat_days"> <tr> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="1" />Lunes</label><br /> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="4" />Jeuves</label> </td> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="2" />Martes</label><br /> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="5" />Viernes</label> </td> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="3" />Miércoles</label><br /> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="6" />Sabado</label> </td> <td> <label><input class="dhx_repeat_checkbox" type="checkbox" name="week_day" value="0" />Domingo</label><br /><br /> </td> </tr> </table> </div> <div id="dhx_repeat_month"> <label><input class="dhx_repeat_radio" type="radio" name="month_type" value="d"/>Repita</label><input class="dhx_repeat_text" type="text" name="month_day" value="1" />dia cada <input class="dhx_repeat_text" type="text" name="month_count" value="1" />mes<br /> <label><input class="dhx_repeat_radio" type="radio" name="month_type" checked value="w"/>El</label><input class="dhx_repeat_text" type="text" name="month_week2" value="1" /><select name="month_day2"><option value="1" selected >Lunes<option value="2">Martes<option value="3">Miércoles<option value="4">Jeuves<option value="5">Viernes<option value="6">Sabado<option value="0">Domingo</select>cada<input class="dhx_repeat_text" type="text" name="month_count2" value="1" />mes<br /> </div> <div style="display:none;" id="dhx_repeat_year"> <label><input class="dhx_repeat_radio" type="radio" name="year_type" value="d"/>Cada</label><input class="dhx_repeat_text" type="text" name="year_day" value="1" />dia<select name="year_month"><option value="0" selected >Enero<option value="1">Febrero<option value="2">Маrzo<option value="3">Аbril<option value="4">Mayo<option value="5">Junio<option value="6">Julio<option value="7">Аgosto<option value="8">Setiembre<option value="9">Octubre<option value="10">Noviembre<option value="11">Diciembre</select>mes<br /> <label><input class="dhx_repeat_radio" type="radio" name="year_type" checked value="w"/>El</label><input class="dhx_repeat_text" type="text" name="year_week2" value="1" /><select name="year_day2"><option value="1" selected >Lunes<option value="2">Martes<option value="3">Miércoles<option value="4">Jeuves<option value="5">Viernes<option value="6">Sabado<option value="0">Domingo</select>del<select name="year_month2"><option value="0" selected >Enero<option value="1">Febrero<option value="2">Маrzo<option value="3">Аbril<option value="4">Mayo<option value="5">Junio<option value="6">Julio<option value="7">Аgosto<option value="8">Setiembre<option value="9">Octubre<option value="10">Noviembre<option value="11">Diciembre</select><br /> </div> </div> <div class="dhx_repeat_divider"></div> <div class="dhx_repeat_right"> <label><input class="dhx_repeat_radio" type="radio" name="end" checked/>Sin fecha de finalización</label><br /> <label><input class="dhx_repeat_radio" type="radio" name="end" />Después de</label><input class="dhx_repeat_text" type="text" name="occurences_count" value="1" />occurencias<br /> <label><input class="dhx_repeat_radio" type="radio" name="end" />Fin</label><input class="dhx_repeat_date" type="text" name="date_of_end" value="'+scheduler.config.repeat_date_of_end+'" /><br /> </div> </form> </div> <div style="clear:both"> </div>';

