<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchBox.ascx.cs" Inherits="Book_SearchBox" %>

<link rel="stylesheet" href="../HomeLib/css/bootstrap.min.css">
<link rel="stylesheet" href="../Content/SearchBoxHome.css">
 <link rel="stylesheet" href="../Content/font-awesome.min.css">
<link href="../Content/Autocomplete.css" rel="stylesheet" />
<script>
    $(document).ready(function () {
        $('#fr_panel').click(function (e) {
            //alert("Asd");
            e.stopPropagation();
        });
    });

    function aceSelected(sourse, e) {
        var value = e.get_value();
        if (!value) {
            if (e._item.parentElement && e._item.parentElement.tagName == "LI")
                value = e._item.parentElement.attributes["_value"].value;
            else if (e._item.parentElement && e._item.parentElement.parentElement.tagName == "LI")
                value = e._item.parentElement.parentElement.attributes["_value"].value;
            else if (e._item.parentNode && e._item.parentNode.tagName == "LI")
                value = e._item.parentNode._value;
            else if (e._item.parentNode && e._item.parentNode.parentNode.tagName == "LI")
                value = e._item.parentNode.parentNode._value;
            else
                value = "";
        }
        sourse.get_element().value = value;
        document.getElementById('<%=Search.ClientID%>').click();
    }

    function resetPosition(object, args) {
        var tb = object._element;
        var tbposition = findPositionWithScrolling(tb);
        var xposition = tbposition[0];
        var yposition = tbposition[1] + 28; // 28 textbox height 
        var ex = object._completionListElement;
        if (ex)
            $common.setLocation(ex, new Sys.UI.Point(xposition, yposition));
    }

    function BoldChildren(behavior) {

        var target = behavior.get_completionList();
        var prefix = behavior._currentPrefix.toLowerCase();
        var i;
        for (i = 0; i < target.childNodes.length; i++) {
            var sValue = target.childNodes[i].innerHTML.toLowerCase();
            if (sValue.indexOf(prefix) != -1) {
                var fstr = target.childNodes[i].innerHTML.substring(0, sValue.indexOf(prefix));
                var estr = target.childNodes[i].innerHTML.substring(fstr.length + prefix.length, target.childNodes[i].innerHTML.length);
                target.childNodes[i].innerHTML = fstr + '<B style="color:#0099CC">' + prefix + '</B>' + estr;
            }
        }

    }

    function findPositionWithScrolling(oElement) {
        if (typeof (oElement.offsetParent) != 'undefined') {
            var originalElement = oElement;
            for (var posX = 0, posY = 0; oElement; oElement = oElement.offsetParent) {
                posX += oElement.offsetLeft;
                posY += oElement.offsetTop;
                if (oElement != originalElement && oElement != document.body && oElement != document.documentElement) {
                    posX -= oElement.scrollLeft;
                    posY -= oElement.scrollTop;
                }
            }
            return [posX, posY];
        } else {
            return [oElement.x, oElement.y];
        }
    }
    function CheckCheckboxes() {

        if (document.getElementById('CheckBox_title').checked == true) {
            document.getElementById('CheckBox_author').checked = false;
        }
        else if (document.getElementById('CheckBox_author').checked == true) {
            document.getElementById('CheckBox_title').checked = false;
        }

    }
    $('#your-custom-id-material').mdbDropSearch();
</script>

<style>
    input[type="search"]::-webkit-search-cancel-button {
        -webkit-appearance: searchfield-cancel-button;
    }
</style> 

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="row">
    <div class="col-md-12 mt-3">
        <div class="input-group col-md-12" id="adv-search">
            <div class="input-group">
                <input id="TextBox_search" type="search" class="form-control" placeholder="Bạn đang cần tìm?" aria-describedby="basic-addon2" runat="server" />
                <div class="input-group-append">
                    <button id="Search" class="btn btn-danger wrn-btn" type="submit" onserverclick="Search_Click1" runat="server"><i class="fa fa-search text-white"></i></button>
                </div>
            </div>
        </div>
    </div>    
</div>
