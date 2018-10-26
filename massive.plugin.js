/*
    Author: Joe Garrett, Jordan Taylor
    Date: 24SEP18
    Descr: Massive Plugin to enable dynamic generated content based on massive data query using html direct.
    by providing data-attributes we launch a corresponding set of actions, methods, and html output directly from massive.  
    For the most part using sql-views to drive the data to our application configured to display the way we want

*/
var __config = [];
var errors = [];
conf = {
    onElementValidate: function (valid, $el, $form, errorMess) {
        if (!valid) { errors.push({ el: $el, error: errorMess }); }
    }
}
$(document).ready(function () {
    OnLoad();
    $(".sumbit").on("keypress", function (event) {
        if (event.which == 13) {
            event.preventDefault();
            $(".submit").click();
            ValidateRights();
            Validate($(this));
        };
    });
    $(".clear").on("click", function (event) {
        event.preventDefault();
        $('.pfc-massive').each(function () {
            if ($(this).is('[readonly]')) {
                $(this).val("");
            };
        })
    });
    $(".cancel").on("click", function (event) {
        event.preventDefault();
        location.href("/");
    });

    $(".submit").on("click", function (event) {
        $(".config").each(function () {
            event.preventDefault();
            CheckInput($(this));
            Validate($(this));
        });
    });

    $(".config").on("change", function (event) {
        $(this).each(function () {
            CheckInput($(this));
            Validate($(this));
        });
    });

    //onLoad binding
    function OnLoad() {
        $(".loadcontrol").each(function () {
            CheckInput($(this));
            Validate($(this));
        });
    };

    //default pass by ref values
    function ByRef() {
        $("#Latitude").val($("#main_lat").text());
        $("#Longitude").val($("#main_long").text());
    };
    //Checks input variables
    function CheckInput(config) {
        var field = $("#" + config.data("field")).val();
        var placement = $("#" + config.data("results"));
        if (config.data("formfill")) MassiveAjax(field, config).done(function (data) { FillInputs(data, config); });
        else if (config.data("formreader")) MassiveAjax(field, config).done(function (data) { FillFromForm(data, config); });
        else if (placement.is("table")) MassiveAjax(field, config).done(function (data) { DataToTable(data, config); });
        else if (placement.is("select")) MassiveAjax(field, config).done(function (data) { DataToDropDown(data, config); });
        else if (placement.is("div")) MassiveAjax(field, config).done(function (data) { DataToDiv(data, config); });
        else if (placement.is("fieldset")) MassiveAjax(field, config).done(function (data) { DataToFieldset(data, config); });
        else if (placement.is("ul")) MassiveAjax(field, config).done(function (data) { DataToList(data, config); });
        else MassiveAjax(field, placement).done(DataToTable);
        $("#loadingModal").hide();
        //promis functions - functions that are built into the app pages that we want to run after our data is populated.
        placement.promise().done(function () {
            if (typeof PromiseFunction !== 'undefined' && $.isFunction(PromiseFunction)) {
                PromiseFunction();
            };
            if (typeof InsertFunction !== 'undefined' && $.isFunction(InsertFunction)) InsertFunction();
        });
    };
    //to use this you should use the massive query/or optionally a view to represent your value.
    function FillInputs(jdata, config) {
        var data = $.parseJSON(jdata.d);
        var placement = $("#" + config.data("results"));
        for (property in data) {
            for (prop in data[property]) {
                var input = $("#" + prop)
                if (input.is("input")) {
                    if (prop.toLowerCase().indexOf("date") > 0) input.val(moment(data[property][prop]).format("l"));
                    else if (prop.toLowerCase().indexOf("phone") > 0) input.val(data[property][prop]).mask('(000) 000-0000');
                    else input.val(data[property][prop]);
                } else {
                    if (prop.toLowerCase().indexOf("date") > 0) input.html(moment(data[property][prop]).format("l"));
                    if (prop.toLowerCase().indexOf("phone") > 0) input.html(data[property][prop]).mask('(000) 000-0000');
                    else input.val(data[property][prop]);
                }
            };
        };
        CreateHiddenControls(data, config);
    };
    //the fillfromform grabs the current active form, and uses the data-table tag to pull all inputs from the list of columns that match to insert them back into the table.
    function FillFromForm(jdata, config) {
        var data = $.parseJSON(jdata.d);
        var placement = $("#" + config.data("results"));
        for (property in data) {
            for (prop in data[property]) {
                var input = $("#" + prop)
                if (input.is("input")) input.val(data[property][prop]);
                else {
                    if (prop.toLowerCase().indexOf("date") > 0) input.html(moment(data[property][prop]).format("l"));
                    if (prop.toLowerCase().indexOf("phone") > 0) input.html(Inputmask({ "mask": "(999) 999-9999" }).mask(data[property][prop]));
                    else input.html(data[property][prop]);
                }
            };
        };
        CreateHiddenControls(data, config);
    };
    //to use this you should use the massive query/or optionally a view to represent your id,name values.
    function DataToDropDown(jdata, config) {
        var data = $.parseJSON(jdata.d);
        var placement = $("#" + config.data("results"));
        var value = config.data("value");
        var text = (config.data("text") !== undefined && config.data("text").indexOf(',') ? config.data("text").split(',') : config.data("text"));
        if (config.data("text") == undefined || config.data("value") == undefined) alert("Dropdown Binding requires data-text, data-value fields to be set.");
        else {
            $.each(data, function (i, o) {
                //console.log(o[text], o[value]);
                if (text.indexOf(',') > 0) placement.append($('<option/>').attr("value", o[value]).text(o[text[0].replace(" ", "")] + " : " + o[text[1].replace(" ", "")]));
                else placement.append($('<option/>').attr("value", o[value]).text(o[text]));
            });
        }
    };
    //to use this setup a view and where your column is a li (i.e. SELECT '<a href='/pages/somewhere.html'><i class='fa fa-note'></li>&nbsp;</a>&nbsp;'+ColumnName AS li FROM TestTable WHERE 1=1)
    //result set needs to be pkey,value so, we can assign a data-key='' on each for ref by MassiveArr.control.
    function DataToList(jdata, config) {
        var placement = $("#" + config.data("results"));
        var data = $.parseJSON(jdata.d);
        var html = "";
        $.each(data, function (idx, item) {
            html += "<li class='list-group-item'>";
            $.each(item, function (index, key) {
                html += "" + key + "";
            })
            html += "</li>";
        });
        placement.append(html);
    };
    //output values to a fieldset with labels direct essentially kvp 
    function DataToFieldset(jdata, config) {
        var data = $.parseJSON(jdata.d);
        var placement = $("#" + config.data("results"));
        $.each(data, function (index, key) {
            $.each(key, function (idx, item) {
                placement.append("<legend class='primary'>" + idx + "</legend>" + item + "<br/>");
            });
        });
        $(placement).append(html);
    };
    //output values to a div direct essentially kvp 
    function DataToDiv(jdata, config) {
        var placement = $("#" + config.data("results"));
        var data = $.parseJSON(jdata.d);
        $.each(data, function (idx, item) {
            $.each(item, function (index, key) {
                placement.append("<span class='primary'>" + index + "</span>:" + key + "<br/>");
            })
        });
    };
    //throw to a table
    function DataToTable(data, config) {
        __config = config;
        var data = $.parseJSON(data.d);
        var placement = $("#" + config.data("results"));
        if (data.length > 0) {
            if (config.data("hidden-cols") !== undefined) {
                placement.attr("data-hidden-cols", config.data("hidden-cols"));
            };
            placement.createTable(data);
            CreateDataTable(config);
            GenerateLinks(data, config);
        } else {
            placement.addClass("table table-striped table-hover table-condensed col-xs-12").html("<div class='bs bs-callout bs-callout-danger col-xs-12'><b>No Results</b></div>");
        }
    };
    function CreateHiddenControls(data, config) {
        var placement = $("#" + config.data("results"));
        $.each(data, function (idx, item) {
            $.each(item, function (index, key) {
                placement.append("<input type='hidden' id='hdn" + index + "' value='" + key + "'/>");
            })
        });
    };
    //datatables generator
    function CreateDataTable(config) {
        var placement = $("#" + config.data("results"));
        $(placement).addClass("table table-striped table-hover table-condensed table-responsive");
        if (config.data("datatables")) {
            $.fn.dataTable.moment('MMDDYYYY');
            $.fn.dataTable.moment('MMDDYYYY hh:mm a');
            if ($.fn.DataTable.isDataTable(placement)) $(placement).dataTable().fnDestroy();
            var dt = $(placement).DataTable({
                "oLanguage": { "sSearch": "Filter:&nbsp;", "emptyTable": "" },
                dom: 'Bfrtip',
                ordering: true,
                processing: true,
                buttons:
                [
                    { extend: 'copy', text: '<i class=\'fa fa-clipboard\'></i>', className: 'btn btn-sm btn-primary text-white', titleAttr: 'Copy to Clipboard' },
                    { extend: 'excel', text: '<i class=\'fa fa-table\'></i>', className: 'btn btn-sm btn-primary', titleAttr: 'Download as Spreadsheet' },
                    { extend: 'pdf', text: '<i class=\'fa fa-file\'></i>', className: 'btn btn-sm btn-primary', titleAttr: 'Download as PDF' },
                    { extend: 'print', text: '<i class=\'fa fa-print\'></i>', className: 'btn btn-sm btn-primary', titleAttr: 'Print this Page' },
                ]
            });
            dt
            .order(config.data("sort"))
            .draw();

            var cols = config.data("hidecolumns").toString().split(',');
            console.log(cols.length);
            if (cols.length > 1) {
                columns.forEach(function (idx, obj) {
                    dt.column(idx).visible(false);
                });
            };
            if (cols.length == 1) dt.column(config.data("hidecolumns")).visible(false);
        };
    };
    //for our actions links
    function GenerateLinks(json_data, config) {
        ///looop each row and then based on the links in the collecton we need to pull that column ref so we can generate an action column
        var placement = $("#" + config.data("results"));
        var html = ActionProcessor(json_data, placement, "");
        var placement_id = placement.data("id");
        var tr = $("#" + placement_id + " tbody tr")
        $(tr).each(function (i, row) {
            //console.log("row-->",row);
            var td = $(row).children("td");
            $.each(td, function (i, col) {
                //console.log("col-->", $(this).attr("data-name"));
                var tda = td.find("action_column");
                var col_name = $(this).data("name");
                var col_value = $(this).data("value");
                if (placement.data("formattemplate")) {
                    $($(placement.data("formattemplate")).attr("id") + " a").each(function () {
                        var selectable_cols = ($(this).data("name").indexOf(',') ? $(this).data("name").split(',') : $(this).data("name"));
                        for (var i = 0; i <= selectable_cols.length; i++) {
                            if (selectable_cols[i] !== undefined && col_value !== undefined) {
                                if (col_name == selectable_cols[i]) {
                                    var tag = "@" + selectable_cols[i];
                                    if (col_value !== null) $(".delete").addClass("disabled"); $(".insert").removeClass("disabled");
                                    if (col_value === null) $(".delete").removeClass("disabled"); $(".insert").addClass("disabled");
                                    $(this).attr("onclick").replace(tag, (col_value !== null ? col_value : ""));
                                    $(this).html().replace(/@icon-/g, "fa fa-");
                                    tda.append($(this));
                                    $(".insert").removeClass("disabled");
                                    $(".delete").removeClass("disabled");
                                }
                            }
                        }
                    });
                };
            });
        });
    };
    // Our Data Gathering Library Functions
    //dynamic function for page html level json data parsed by HTML data-attributes
    function MassiveAjax(fieldstr, config, callback) {
        var placement = $("#" + config.data("results"));

        /*You can set up your own static variables to pull from*/
        var CurrentUserId = $("#CurrentUserId").val();
        var CurrentUser = $("#CurrentUser").val();
        var CreatedBy = $("#CreatedBy").val();
        var LastModifiedBy = $("#LastModifiedBy").val();
        var DivisionId = $("#DivisionId").val();
        var CurrentDate = $("#CurrentDate").val();
        var FutureDate = $("#FutureDate").val();

        var where = config.data("where");
        var query = config.data("query");
        var columns = config.data("columns");
        var links = config.data("links");
        var orderby = (config.data("orderby") !== undefined ? config.data("orderby") : "1");
        var method = config.data("method");
        var table = config.data("table");
        var arguments = config.data("arguments");
        var condition = $("#" + config.data("input-fields")).val();

        var msg = "Missing data-attributes for: ";
        var massive_required = [];
        if (method !== "MassiveInsert" && method != "MassiveForm") {
            if (where === undefined) massive_required += "data-where";
            if (table === undefined) massive_required += "data-table";
            if (massive_required.length > 0) alert("You are missing the data-attributes [" + massive_required + "] for " + placement.attr("id") + ".");
        }
        if (method === "MassiveInsert") {
            //insert requires form element values and names {Column_1=#Column_1.val}
            if (table === undefined) massive_required += "data-table";
            if (arguments === undefined) massive_required += "data-arguments";
            if (massive_required.length > 0) alert("You are missing the data-attributes [" + massive_required + "] for " + placement.attr("id") + ".");
            arguments.split(',').forEach(function (objName, objIndex) {
                console.log(objName, objIndex);
            });
        }
        if (where !== undefined) {
            where = where.replace("@CurrentUserId", "'" + CurrentUserId + "'");
            where = where.replace("@CurrentUser", "'" + CurrentUser + "'");
            where = where.replace("@CreatedBy", "'" + CreatedBy + "'");
            where = where.replace("@LastModifiedBy", "'" + LastModifiedBy + "'");
            where = where.replace("@DivisionId", "'" + DivisionId + "'");
            where = where.replace("@FutureDate", "'" + FutureDate + "'");
            where = where.replace(/(@0)/g, condition);
        } else where = "";
        $("#loadingModal").show();
        if (method !== "MassiveForm") {
            if (arguments === undefined) var json_data = JSON.stringify({ "table": table, "where": where, "arguments": "", "columns": columns, "links": JSON.stringify(links), "orderby": orderby });
            else if (query === undefined) var json_data = JSON.stringify({ "table": table, "where": where, "arguments": "[" + JSON.stringify(fieldstr) + "]", "columns": columns, "links": JSON.stringify(links), "orderby": orderby });
            else if (query !== undefined) var json_data = JSON.stringify({ "table": table, "query": query });
        } else {
            var json_data = JSON.stringify({ "rform": "[{" + JSON.stringify($("form").serialize()) + "}]", "table": table });
        }
        return $.ajax({
            url: "/wsWebServices.asmx/" + method,
            contentType: "application/json; charset=utf-8",
            async: true,
            dataType: "json",
            type: "POST",
            data: json_data
        });
    };
    //callback return functions for implied execution @ page requestor
    function MassiveHandler(obj) {
        ParseDataAttrOfRef(obj);
        MassiveAjax(parseddatafields);
    };
    //process our actions
    function ActionProcessor(object, config, html) {
        var ft = $("#" + config.data("formattemplate"));
        var tmpl = ft.html();
        if (tmpl) {
            var result = [];
            $.each(object, function (key, value) {
                $.each(value, function (idx, v) {
                    if (tmpl.indexOf('@') <= 0) {
                        html += tmpl;
                        tmpl = ft.html();
                    }
                    html = tmpl.replace("@" + idx, v).replace(/@Action_Idx/g, "action_btn_" + key).replace(/@icon-/g, "fa fa-") + " ";
                });
            });
            return html;
        };
    }
    function Validate(config) {
        if (config.data("validate")) $.validate();
    };
    function DisplayData(data) {
        $("#primaryModal_Hdr").html(method.toUpperCase());
        $("#primary_message").html(data);
        $("#primaryModal").show();
    };
});
//choosing the method to run based on data attr
function MassiveCall(data) {
    switch (method) {
        case "MassiveInsert": json_data = JSON.stringify({ "table": table, "arguments": data }); break;
        case "MassiveUpdate": json_data = JSON.stringify({ "table": table, "where": where, "arguments": data }); break;
        case "MassiveDelete": json_data = JSON.stringify({ "table": table, "where": data }); break;
        case "MassiveArchive": json_data = JSON.stringify({ "table": table, "where": where }); break;
    }
    return $.ajax({
        url: "/wsWebServices.asmx/" + method,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        data: json_data
    });
};
function Action(method, table, where, arguments) {
    var CurrentUserId = $("#CurrentUserId").val();
    var CurrentUser = $("#CurrentUser").val();
    var CreatedBy = $("#CreatedBy").val();
    var LastModifiedBy = $("#LastModifiedBy").val();
    var DivisionId = $("#DivisionId").val();
    var CurrentDate = $("#CurrentDate").val();
    var FutureDate = $("#FutureDate").val();
    if (where && where !== "" || where !== undefined) {
        where = where.replace("@CurrentUserId", "'" + CurrentUserId + "'");
        where = where.replace("@CurrentUser", "'" + CurrentUser + "'");
        where = where.replace("@CreatedBy", "'" + CreatedBy + "'");
        where = where.replace("@LastModifiedBy", "'" + LastModifiedBy + "'");
        where = where.replace("@DivisionId", "'" + DivisionId + "'");
        where = where.replace("@FutureDate", "'" + FutureDate + "'");
    };
    var sa = (arguments === undefined ? "" : (arguments.indexOf(",") ? arguments.split(",") : arguments));
    var args = "";
    if (sa !== "") {
        $.each(sa, function (k, v) {
            var item = v.split("=");
            console.log(item);
            args += '"' + item[0] + '":' + (item[1] != "" ? "'" + item[1] + "'" : "") + ',';
            args = args.replace("@CurrentUserId", CurrentUserId);
            args = args.replace("@CurrentUser", CurrentUser);
            args = args.replace("@CreatedBy", CreatedBy);
            args = args.replace("@LastModifiedBy", LastModifiedBy);
            args = args.replace("@DivisionId", DivisionId);
            args = args.replace("@CurrentDate", CurrentDate);
            args = args.replace("@NULL", "");
        });
        args = '{' + (sa.length > 0 ? args.substr(0, args.length - 1) : args) + '}';

        if (method == "MassiveInsertForm" || method == "MassiveUpdateForm") {
            var form_items = arguments.split(",");
            form_items.forEach(function (data) {
                var kvp = data.split('_');
                if (args.indexOf("#" + kvp[0] + "_CreatedBy") > 0) args = args.replace("#", "").replace("\"", "'").replace("undefined", $("#CreatedBy").val());
                args = args.replace("#", "").replace("\"", "'").replace("undefined", $(data).val());
            })
        };
        //console.log(args);
    };
    switch (method) {
        case "MassiveInsert":
        case "MassiveInsertForm":
            method = "MassiveInsert";
            json_data = JSON.stringify({ "table": table, "arguments": args });
            break;
        case "MassiveUpdate":
        case "MassiveUpdateForm":
            method = "MassiveUpdate";
            json_data = JSON.stringify({ "table": table, "where": where, "arguments": args });
            break;
        case "MassiveDelete": json_data = JSON.stringify({ "table": table, "where": where }); break;
        case "MassiveArchive": json_data = JSON.stringify({ "table": table, "where": where, "arguments": args }); break;
    }
    $.ajax({
        url: "/wsWebServices.asmx/" + method,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        type: "POST",
        data: json_data
    });
    window.location.reload();
};
//function GetJSONAttrib (wow this is crazy--who built this --- ahahahahah jwg)
function ParseDataAttrOfRef(obj) {
    var isValid = $.fn.isValid(null, null, false);
    if (isValid) {
        var ref = $("#" + obj);
        var kvp = {};
        table = ($(ref).data("table") ? $(ref).data("table") : table);
        if ($(ref).data("fields")) {
            fields = $(ref).data("fields").indexOf(',') > 0 ? $(ref).data("fields").split(",") : $(ref).data("fields");
            $.each(fields, function (idx, obj) {
                //log the kvp
                kvp[obj] = $("#" + obj).val();
            });
        }
        where = ($(ref).data("where") ? $(ref).data("where") : where);
        method = ($(ref).data("method") ? $(ref).data("method") : method);

        //setup key value pair for parsing with a defined method (ws->massive)
        parseddatafields = JSON.stringify(kvp);
        MassiveCall(parseddatafields).done(RefreshPage());
    };
};
function RefreshPage() {
    location.reload();
};
function ActionHandler(obj_id) {
    var obj = $("#" + obj_id);
    ParseDataAttrOfRef(obj_id);
};

//automated extesion function to build json to table
!function (t) {
    var hc = $("#hidden-columns").data("columns");
    var ce = $("#editable").data("columns");
    t.fn.createTable = function (e, a) {
        var r, o = this, n = t.extend({}, t.fn.createTable.defaults, a);
        void 0 !== o[0].id && (r = "#" + o[0].id + " ");
        var l = '<table>';
        return l += '<thead class="primary">', l += t.fn.createTable.parseTableData(e, !0), l += "</thead>", l += "<tbody>", l += t.fn.createTable.parseTableData(e, !1), l += "</tbody>", l += "</table>", o.html(l),
            function () {
                t(r + ""),
                t(r + ""),
                t(r + "thead th:not(:first-child), tbody td:not(:first-child)"),
                t(r + "thead th, tbody td"),
                t(r + "thead th"),
                t(r + "tbody td")
            }()
    }, t.fn.createTable.getHighestColumnCount = function (e) {
        for (var a = 0, r = 0, o = { max: 0, when: 0 }, n = 0; n < e.length; n++) a = t.fn.getObjectLength(e[n]), a >= r && (r = a, o.max = a, o.when = n);
        return o
    }, t.fn.createTable.parseTableData = function (e, a) {
        for (var r = "", o = 0; o < e.length; o++) a === !1 && (r += '<tr>'), t.each(e[o], function (n, i) {
            //console.log(n, i, hiddencols);
            a === !0 ? o === t.fn.createTable.getHighestColumnCount(e).when && (r += "<th class='" + (hc !== undefined && hc.indexOf(n) >= 0 ? "hidden" : "") + "' data-name='" + n + "'>" + t.fn.humanize(n) + "</th>") : a === !1 &&
            (r += "<td class='" + (hc !== undefined && hc.indexOf(n) >= 0 ? "hidden" : "normal") + "'>" + (n !== null && n.toLowerCase().indexOf("date") >= 0 && i !== null ? moment(i).format('l') : (i !== null ? i : "")) + "</td>")
        }), a === !1 && (r += "</tr>");
        return r
    }, t.fn.getObjectLength = function (t) {
        var e = 0;
        for (var a in t) t.hasOwnProperty(a) && ++e;
        return e
    }, t.fn.humanize = function (t) {
        var e = t.split("_");
        for (i = 0; i < e.length; i++) e[i] = e[i].charAt(0).toUpperCase() + e[i].slice(1);
        return e.join(" ")
    }, t.fn.createTable.defaults = {
        thTextTransform: "capitalize",
    }
}(jQuery);
