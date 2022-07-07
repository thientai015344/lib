<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BookResult.ascx.cs" Inherits="Book_BookResult" %>
<%@ Register TagPrefix="LDown" TagName="LDownload" Src="~/Book/Download.ascx" %>
<%@ Register TagPrefix="DS" TagName="MenuOB" Src="~/Book/MenuObject.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="ShowSlider" TagName="BookShowSlider" Src="~/Book/ShowSlider.ascx" %>
<%@ Register TagPrefix="ShowSliderBanner" TagName="BookShowSliderBanber" Src="~/Book/SliderShowBanner.ascx" %>

<link href="Content/SearchBox.css" rel="stylesheet" />
<link href="Content/Result.css" rel="stylesheet" />
<link href="../Content/PageNumber.css" rel="stylesheet" />
<link href="../Content/Autocomplete.css" rel="stylesheet" />

<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
<script src='https://www.google.com/recaptcha/api.js'></script>

<style type="text/css">
    .table_td_thumbnail {
        width: 12%;
    }

    #table_result tr > td {
        padding: 0;
        margin-top: 20px;
        border: 0;
        height: 22px;
    }

    strong {
        color: #727070;
    }

    .container-fluid {
        font-family: Arial;
    }

    .auto-style2 {
        position: relative;
        min-height: 1px;
        float: left;
        width: 100%;
        margin-bottom: 0px;
        padding-left: 15px;
        padding-right: 15px;
        left: 0px;
        top: 0px;
    }
</style>
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
        document.getElementById('<%=ImageButtonSearch.ClientID%>').click();
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
</script>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<div class="ct" style="font-family: Arial">
    <div class=" container-fluid">
        <div class="row" style="margin: 0px 0 15px 0">
            <div class="text-center">
                <asp:Image ID="ImageLogo" ImageUrl="~/Logo/Search_Icon2.png" Width="250" runat="server" />
            </div>
        </div>
        <div class="row">
            <div class="auto-style2">
                <div class="input-group" id="adv-search">
                    <asp:TextBox ID="TextBox_search" runat="server" class="form-control speech " placeholder="Bạn đang cần tìm?"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="TextBox_search_AutoCompleteExtender"
                        runat="server" Enabled="True"
                        ServiceMethod="GetCompletionList"
                        TargetControlID="TextBox_search"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem "
                        CompletionSetCount="20" CompletionInterval="20"
                        CompletionListCssClass="autocomplete_completionListElement "
                        MinimumPrefixLength="2" OnClientPopulated="BoldChildren"
                        OnClientItemSelected="aceSelected" ServicePath="~/Search.aspx"
                        OnClientShown=""
                        ShowOnlyCurrentWordInCompletionListItem="True">
                    </cc1:AutoCompleteExtender>

                    <div class="input-group-btn">
                        <div class="btn-group" role="group">
                            <div class="dropdown dropdown-lg">
                                <button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-expanded="false"><span class="caret"></span></button>
                                <div class="dropdown-menu dropdown-menu-right" role="menu" id="fr_panel">
                                    <div class="form-group">
                                        <label for="filter">Tìm kiếm nâng cao</label>
                                    </div>
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label for="filter">Bộ sưu tập:</label>
                                            <asp:DropDownList ID="DDLListDocument" CssClass="form-control" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DDLListDocument_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <label for="contain">Chỉ tìm trong:</label>
                                            <asp:DropDownList ID="DropDown_Filter" CssClass="form-control" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="DropDown_Filter_SelectedIndexChanged">
                                                <asp:ListItem Value="0">--Tất cả--</asp:ListItem>
                                                <asp:ListItem Value="1">Nhan đề</asp:ListItem>
                                                <asp:ListItem Value="2">Tác giả</asp:ListItem>
                                                <asp:ListItem Value="3">Chủ đề</asp:ListItem>
                                                <asp:ListItem Value="4">Năm xuất bản</asp:ListItem>
                                                <asp:ListItem Value="5">Nhà xuất bản</asp:ListItem>
                                                <asp:ListItem Value="6">Nơi xuất bản</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <!-- <button type="submit" class="btn btn-primary"><span class="glyphicon glyphicon-search" aria-hidden="true"></span></button>-->
                                    </div>
                                </div>
                            </div>
                            <asp:ImageButton ID="ImageButtonSearch" CssClass="btn btn-primary" runat="server" ImageAlign="Middle" ImageUrl="~/Logo/search-3-16.ico" OnClick="ImageButton1_Click" />
                        </div>

                    </div>
                </div>
                <div class="text-right" style="margin: 5px 75px 0 0">
                    <a href="../Help.html"><span class="glyphicon glyphicon-question-sign"></span>Hướng dẫn</a>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:Panel ID="Result" runat="server">
                <div class="row" style="padding-top: 10px">
                    <div class="col-xs-10 col-sm-6 col-md-8 col-sm-offset-2 ">
                        <!--info result-->
                        <asp:Label ID="Label_thongtin" runat="server"></asp:Label>
                    </div>
                    <div class="col-xs-8 col-sm-6 col-md-4 col-xs-offset-4 col-sm-offset-7 col-md-offset-8 " style="padding-bottom: 10px">
                        <!--Sort-->
                        <asp:Label ID="Labelsapxep" runat="server" Text="Sắp xếp theo" Visible="false"></asp:Label>
                        <asp:DropDownList ID="DropDownSorting" runat="server" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="DropDownSorting_SelectedIndexChanged" Visible="false">
                            <asp:ListItem Value="0">Bình thường</asp:ListItem>
                            <asp:ListItem Value="1">Mới nhất</asp:ListItem>
                            <asp:ListItem Value="2">Nhan đề</asp:ListItem>
                            <asp:ListItem Value="3">Tác giả</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div id="DivControl" class="hidden-xs row col-sm-8 col-sm-offset-3" runat="server" style="padding-bottom: 10px">
                        <strong>
                            <asp:Label ID="Label_loctheo" runat="server" Text="Lọc theo: " Font-Size="10pt"></asp:Label></strong>
                        <asp:LinkButton ID="LalObject" runat="server" OnClick="LalObject_Click" OnClientClick="#" Font-Underline="true" ToolTip="Xóa chủ đề"></asp:LinkButton>
                    </div>
                </div>
                <div class="row form-inline">
                    <div class="col-md-3 col-sm-3 hidden-xs ">
                        <asp:Panel ID="Panel_Menu" runat="server">
                            <DS:MenuOB ID="MOJ" runat="server" />
                        </asp:Panel>
                    </div>
                    <div class="col-md-9 col-sm-9">
                        <!--result-->
                        <asp:Repeater ID="rptBooks" runat="server" OnItemDataBound="rptBooks_ItemDataBound" OnItemCommand="rptBooks_ItemCommand">
                            <ItemTemplate>
                                <div style="border-top: 1px dotted #c1650f; margin-top: -5px; padding-top: 2px">
                                    <table id="table_result" class="table" style="margin-bottom: 5px">
                                        <tr>
                                            <td colspan="2">
                                                <asp:HyperLink ID="hplBookName" Font-Bold="true" Font-Size="1.1em" ForeColor="#0066C0" EnableTheming="True" NavigateUrl='<%#"/chi-tiet?id="+ Eval("RecordID")%>' Text='<%# HighlightText(LimitLength(Eval("Nhan đề").ToString(),100,"")) %>' ToolTip='<%# DataBinder.Eval(Container.DataItem, "Nhan đề")%>' runat="server" Target="_blank">
                                                </asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td rowspan="7" class="table_td_thumbnail">
                                                <asp:HyperLink ID="hplImage" runat="server" NavigateUrl='<%#"/chi-tiet?id="+ Eval("RecordID")%>' Target="_blank">
                                                    <asp:Image ID="ImageBook" runat="server" ImageAlign="Middle" Width="90" Height="125" CssClass="thumbnail" ToolTip='<%# DataBinder.Eval(Container.DataItem, "Nhan đề")%>' />
                                                </asp:HyperLink>
                                            </td>
                                            <td class="col-sm-7 col-md-8">
                                                <span style="font-size: 13px; color: blueviolet">Tác giả:</span>
                                                <asp:Repeater ID="repLinks" runat="server" ViewStateMode="Enabled">
                                                    <ItemTemplate>
                                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("~/tim-kiem?q={0}&rag=0&filter=0", HttpUtility.UrlEncode(Container.DataItem.ToString().Replace("(GVHD)",""))) %>' Text='<%# Container.DataItem.ToString() + ", " %>' ID="HL" ForeColor="#0066CC" />
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                        <tr class="hidden-xs">
                                            <td>
                                                <div>
                                                    <strong>Thông tin xuất bản: </strong>
                                                    <asp:Label ID="lbNamXB" runat="server" Text='<%# HighlightText(Eval("Nơi XB").ToString()) + HighlightText(Eval("Nhà XB").ToString()) +" "+ HighlightText(LimitLength(Eval("Năm XB").ToString(),50,"")) %>' />

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Mô tả vật lý:</strong>
                                                <asp:Label ID="MTvatly" runat="server" Text='<%#HighlightText(Eval("Mô tả vật lý").ToString()) %>'></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong>Ký hiệu phân loại:</strong>
                                                <asp:HyperLink ID="HyperLinkDewey" runat="server" Text='<%#HighlightText(Eval("Phân loại").ToString()) %>' NavigateUrl='<%# Eval("[Phân loại]", "~/tim-kiem?q={0}&rag=0&filter=0") %>'></asp:HyperLink>
                                            </td>
                                        </tr>                                                                                                                                   
                                            <asp:Label ID="LabelISBN" runat="server" ></asp:Label>                                       
                                        <tr>
                                            <td>
                                                <div class="col-md-12">
                                                    <div class="col-md-6">
                                                        <strong><span class="glyphicon glyphicon-book"></span> Bộ sưu tập:</strong>
                                                        <asp:Label ID="LabelloaiTL" runat="server" ForeColor="#6600ff" Font-Bold="true" Text='<%#HighlightText(Eval("loại tài liệu").ToString())%>'></asp:Label>
                                                    </div>
                                                    <div style="margin-left: 50%" class="col-md-6">
                                                        <a href='<%# String.Format("DocumentCart.aspx?id={0}",DataBinder.Eval(Container.DataItem, "RecordID")) %>'></a>
                                                        <asp:LinkButton runat="server" CommandName="AddProducts" CommandArgument='<%# Eval("RecordID") %>' ID="AddCart" Visible="true"><span class="glyphicon glyphicon-shopping-cart" aria-hidden="true" ></span> Thêm vào giỏ</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <asp:HyperLink ID="HyperLinkSachtang" runat="server" Text='<%# Eval("Specialization").ToString() %>' NavigateUrl='<%#Eval("Specialization", "~/tim-kiem?q={0}&rag=0&filter=0") %>' Visible="false"></asp:HyperLink>
                                                <asp:Label ID="La_chau_a" runat="server" Text='<%# Eval("Specialization").ToString().Contains("Sách quỹ châu Á tặng")?Eval("Specialization").ToString():"" %>' ></asp:Label>
                                                <LDown:LDownload ID="LDown" runat="server" RecordID='<%#Eval("RecordID") %>' />

                                            </td>
                                            <td rowspan="2">
                                                <div class="hidden-xs">
                                                    <asp:Label ID="Labe_trongkho" runat="server"></asp:Label><br />
                                                    <asp:Label ID="Labe_dangmuon" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr class="hidden-md hidden-lg hidden-sm">
                                            <td rowspan="2">
                                                <div class="col-xs-offset-1">
                                                    <asp:Label ID="Labe_trongkho_xs" runat="server"></asp:Label><br />
                                                    <asp:Label ID="Labe_dangmuon_xs" runat="server"></asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <div class="text-center mtpage">
                            <asp:Repeater ID="rptPager" runat="server">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkPage" runat="server" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'
                                        CssClass='<%# Convert.ToBoolean(Eval("Enabled")) ? "page_enabled" : "page_disabled" %>'
                                        OnClick="Page_Changed" OnClientClick='<%# !Convert.ToBoolean(Eval("Enabled")) ? "return false;" : "" %>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
    </div>
</div>


