/*
 dhtmlxScheduler.Net v.3.2.0 Professional Evaluation

This software is covered by DHTMLX Evaluation License. Contact sales@dhtmlx.com to get Commercial or Enterprise license. Usage without proper license is prohibited.

(c) Dinamenta, UAB.
*/
Scheduler.plugin(function (t)
{
    t.initCustomLightbox = function (e, n, i) {
        function o()
        {
            if (!n._lightbox)
            { var t = n._getLightbox(), i = t.childNodes[1]; t.className.indexOf("dhx_cal_light_wide") >= 0 ? i.lastChild.firstChild.style.display = "none" : i.firstChild.style.display = "none", i.style.height = i.style.height.replace("px", "") - 25 + "px", t.style.height = t.style.height.replace("px", "") - 50 + "px", t.style.width = +e.width + 10 + "px", i.style.width = e.width + "px" } return n._lightbox
        }
        var a = { initial: 0, load_data: 1, save_data: 2 }, d = i + "_here_iframe_";
n.config.buttons_left=[],n.config.buttons_right=[],n._getLightbox=n.getLightbox,n.config.lightbox.sections=[{type:"frame",name:"box"}],n._cust_string_to_date=function(t){return n.templates.xml_date(t)},n._cust_date_to_str=function(t){return n.templates.xml_format(t)},n._deep_copy=function(e){if("object"==typeof e){if("[object Date]"==Object.prototype.toString.call(e))var n=new Date(e);else if("[object Array]"==Object.prototype.toString.call(e))var n=new Array;else var n=new Object;for(var i in e)n[i]=t._deep_copy(e[i])
}
else var n = e; return n
}, "external" == e.type ? (n.attachEvent("onBeforeLightbox", function () { return n._custom_box_stage = 0, !0 }), n.getLightbox = o, n._setLightboxValues = function (t, i) {
    var t = document.getElementById(d); try {
        switch (n._custom_box_stage) {
            case a.initial:
                if (!n.dataProcessor)
                { var o = n.dataProcessor = new DataProcessor; o.init(n) }
                var o = n.dataProcessor || o, s = o._getRowData(i), r = -1 === (e.view || "").indexOf("?") ? "?" : "&", c = "<form action='/" + e.view + r + "id=" + encodeURIComponent(i) + "' method='POST'>"; for (var l in s) c += "<input type='hidden' name='" + l + "'/>";
                if (c += "</form>",
                    t.Document) var h = t.Document.body; else var h = t.contentDocument.body; h.innerHTML = c; var _ = 0; for (var l in s) h.firstChild.childNodes[_++].value = s[l]; h.firstChild.submit(); break; case a.load_data: if (!t.contentWindow.lightbox) { { t.contentWindow } t.contentWindow.lightbox = { close: function () { n._remove_customBox() } } } n.callEvent("onLightbox", [i]); break; case a.save_data: if (!t) return; var u = t.contentWindow; if (!u || !u.response_data) return; n._doLAction(u.response_data), n._remove_customBox()
        }
    } catch (f) {
        n._remove_customBox(), window.console && console.log(f)
    }
    n._custom_box_stage++
}, n._remove_customBox = function () { n._lightbox ? n.endLightbox(!1, n._lightbox) : n.endLightbox(!1), n.callEvent("onAfterLightbox", []) }, n._doLAction = function (t) {
    try {
        if (!t) return; var e = t.data; e.start_date && e.end_date && (e.start_date = n._cust_string_to_date(t.data.start_date), e.end_date = n._cust_string_to_date(t.data.end_date)); var i = t.action; switch (t.action) {
            case "insert": n._loading = !0, n.addEvent(e), n._loading = !1, i = "inserted"; break; case "update": var o = n.getEvent(t.sid); for (var a in e) o[a] = e[a];
                n.event_updated(o),
                    n.updateEvent(t.sid), i = "updated"; break; case "delete": n.deleteEvent(t.sid, !0), i = "deleted"
            } n.dataProcessor.callEvent("onAfterUpdate", [t.sid, i, t.id, t])
        } catch (d) { }
}, n.form_blocks.frame = {
    onload: function (t, e, n) { n._setLightboxValues(t, e) }, render: function () { return "<div style='display:inline-block; height:" + e.height + "px'></div>" }, set_value: function (t, o, a) {
        n._last_id = a.id; var s = '<iframe id="' + d + '" frameborder="0" onload="' + i + ".form_blocks.frame.onload(this, " + i + "._last_id, " + i + ');" src=""';

        return (e.width || e.height) && (s += " style='"), e.width && (s += "width:" + e.width + "px;", t.style.width = e.width + "px"), e.height && (s += "height:" + e.height + "px;", t.style.height = e.height + "px"), (e.width || e.height) && (s += " '"), s += "><html></html></iframe>", t.innerHTML = s, e.className && (t.className = e.className), !0
    }, get_value: function () { return !0 }, focus: function () { return !0 }
}) : (n.form_blocks.frame = {
    render: function () {
        var t = '<iframe  id="' + i + "_here_iframe_\" onload='" + i + "._addLightboxInterface(this)' frameborder='0' src='" + e.view + "'";

        return (e.width || e.height) && (t += " style='"), e.width && (t += "width:" + e.width + "px;"), e.height && (t += "height:" + e.height + "px;"), (e.width || e.height) && (t += " '"), t += " ></iframe>"
    }, set_value: function (t, e, n) { if (t.contentWindow && t.contentWindow.setValues) { if (1 == t.contentWindow.document.getElementsByTagName("form").length) t.contentWindow.document.getElementsByTagName("form")[0].reset(); else { var n = t.contentWindow.getValues(); for (var i in n) n[i] = ""; t.contentWindow.setValues(n) } t.contentWindow.setValues(n) } }, get_value: function (t) {
        return n._deep_copy(t.contentWindow.getValues())
    },
    focus: function () { return !0 }
}, n.getLightbox = o, n._addLightboxInterface = function (t) {
    if (t.contentWindow.lightbox || (t.contentWindow.lightbox = {}), t.contentWindow.lightbox.save = function () { var e = n.getEvent(n.getState().lightbox_id), i = t.contentWindow.getValues(); for (var o in i) e[o] = i[o]; n.endLightbox(!0, n._lightbox) }, t.contentWindow.lightbox.close = function () { n.endLightbox(!1, n._lightbox) }, t.contentWindow.lightbox.remove = function () {
var t = n.locale.labels.confirm_deleting; (!t || confirm(t)) && (n.deleteEvent(n._lightbox_id), n._new_event = null), n.endLightbox(!0, n._lightbox)
    },
        1 == t.contentWindow.document.getElementsByTagName("form").length) t.contentWindow.document.getElementsByTagName("form")[0].reset(); else if (t.contentWindow.getValues && t.contentWindow.setValues) { var e = t.contentWindow.getValues(); for (var i in e) e[i] = ""; t.contentWindow.setValues(e) } t.contentWindow.setValues && t.contentWindow.setValues(n.getEvent(n._lightbox_id)), n.callEvent("onLightbox", [n._lightbox_id])
})
    }
});