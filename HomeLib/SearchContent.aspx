<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchContent.aspx.cs" Inherits="HomeLib_Search" MasterPageFile="~/HomeLib.master" %>
<%@ Register Src="~/Book/FilterSearch.ascx" TagPrefix="uc1" TagName="FilterSearch" %>
<asp:Content ID="Search" ContentPlaceHolderID="ContentPlaceHolderHomeLib" runat="server">
    <link href="../Content/SearchFullText.css?c=2" rel="stylesheet" />
    <link href="../Content/PageNumber.css" rel="stylesheet" />
    <link href="../Content/Collection.css?c=2" rel="stylesheet" />
    <link href="../Content/Autocomplete.css" rel="stylesheet" />

    <script> 
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
            document.getElementById('<%=ButSearch.ClientID%>').click();
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
    

    <style>
        input[type="search"]::-webkit-search-cancel-button {
            -webkit-appearance: searchfield-cancel-button;
        }

        .selcls {
            padding: 4px;
            border: solid 0px #517B97;
            border-bottom: groove 0px #517B97;
            outline: 0;
            background: -webkit-gradient(linear, left top, left 25, from(#FFFFFF), color-stop(4%, #FFFFFF), to(#FFFFFF));
            background: -moz-linear-gradient(top, #FFFFFF, #CAD9E3 1px, #FFFFFF 25px);
            box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;
            -moz-box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;
            -webkit-box-shadow: rgba(0,0,0, 0.1) 0px 0px 8px;
        }
       
        @media screen and (min-width: 991px) {
        #Filter {
            display: block;

                }        
        } 
       </style>  
    
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="main container ">
        <div class="row justify-content-center">
            <div class="mb-5">
                <a href="/fts"><img src="../Logo/iconsearch.png" width ="350" /></a>               
            </div>
        </div>
        <div class=" row justify-content-center mb-3">
            <div class=" align-items-center col-lg-8 col-md-10 col-sm-10">               
                 <div class="input-group">
                   
                     <asp:TextBox ID="TxtSearch" runat="server" class="form-control" placeholder="Bạn đang cần tìm?" ></asp:TextBox>                    
                     <cc1:AutoCompleteExtender ID="TxtSearch_AutoCompleteExtender" 
                       runat="server" Enabled="True"
                        ServiceMethod="GetCompletionList"
                        TargetControlID="TxtSearch"
                        CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        CompletionListItemCssClass="autocomplete_listItem "
                        CompletionSetCount="20" CompletionInterval="20"
                        CompletionListCssClass="autocomplete_completionListElement "
                        MinimumPrefixLength="2" OnClientPopulated="BoldChildren"
                        OnClientItemSelected="aceSelected" ServicePath="~/Search.aspx"
                        OnClientShown=""
                        ShowOnlyCurrentWordInCompletionListItem="True">
                     </cc1:AutoCompleteExtender>
                    
                    <div class="input-group-append">
                        <button id="ButSearch" class="btn btn-danger wrn-btn" type="submit" onserverclick="ButtonSearch_Click" runat="server"><i class="fa fa-search text-white"></i></button>
                    </div>
                </div>
                <div class="row mt-2 form-inline">
                    <div class="col-6 col-lg-6 input-group">
                        <strong class="mr-2">Tìm toàn văn</strong>
                        <label class="switch">
                            <asp:CheckBox ID="CheckFullText" runat="server" AutoPostBack="true" OnCheckedChanged="CheckFullText_CheckedChanged" />
                            <span class="slider round"></span>
                        </label>
                    </div>
                    <div class="col-6 col-lg-6 text-right">
                        <a href="/AdvancedSearch" class="fontcolor" hidden="hidden">Tìm kiếm nâng cao</a>
                    </div>                    
                </div> 
                <div class="row mt-4 ml-0">
                    <div id="div_file" class=" form-inline mr-sm-3" runat="server">
                        <span class="filter d-none d-md-block">Loại tài liệu:&nbsp;</span>
                        <asp:DropDownList ID="ddlSearchFieldsFile" Font-Size="12pt" CssClass="selcls" runat="server"></asp:DropDownList>
                    </div>
                    <div class="form-inline">
                        <span class="filter d-none d-md-block">Chỉ tìm trong:&nbsp;</span>
                        <asp:DropDownList ID="ddlSearchFields" Font-Size="12pt" CssClass="selcls" runat="server"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div> 
        <div class="text-right">
            <asp:Label ID="Lb_SelectFields" runat="server"></asp:Label>
        </div>
        <div class="row mt-2">
            <div class=" col-md-12 summary">
                <asp:Label ID="LabelSummary" runat="server" Text="<%# Summary %>"></asp:Label>
            </div>
        </div>       
        <div class="row mt-3 ">
            <div class="col-md-3">
                <div id="divFilter" class="dropdown mb-3" runat="server"> 
                    <div class="mb-2">
						 <asp:LinkButton ID="link_filter" runat="server"  OnClick="link_filter_Click" ForeColor="#990000"></asp:LinkButton> 

                    </div>                    
                     <button type="button" id="dropdownMenuLink" class="btn btn-info btn-sm dropdown-toggle" data-toggle="dropdown" data-flip="false" aria-haspopup="true" aria-expanded="false" >
                        Bộ lọc
                    </button>
                    <div class="dropdown-menu" id="Filter" aria-labelledby="dropdownMenuLink">                        
                        <uc1:FilterSearch  runat="server" ID="MenuSearch" keyword='<%# SearchKeyword %>' CheckFullText='<%# CheckBoxFullText %>' field='<%# getSearchField %>' fieldgroup='<%# getSearchFieldFile %>' />
                    </div>
                </div>
            </div>
            <div class="col-md-9 col-sm-8">
                <div class="row" hidden="hidden">
                    <div class=" col-md-12 summaryResutl">
                        <asp:Label ID="LabelSummary2" runat="server"></asp:Label>
                    </div>
                </div>
                <asp:Repeater ID="Repeater1" runat="server" DataSource="<%# Results %>" OnItemDataBound="Repeater1_ItemDataBound" OnItemCommand="Repeater1_ItemCommand">
                    <ItemTemplate>                       
                        <table  class="tablttom mb-2">
                            <tr>
                                <td colspan="4">
                                    <a href='<%# Eval("path")  %>' class="link" target="_blank" /><%# Eval("Title") %></a>
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="6" class="table_td_thumbnail">
                                    <a href='<%# Eval("path")  %>' class="link" target="_blank">
                                        <asp:Image ID="ImageBook" ImageAlign="Middle" Width="90" Height="125" CssClass="thumbnail mr-2 mb-3 mb-3 rounded shadow" runat="server" /></a>
                                </td>
                                <td>
                                    <div id="author_div" runat="server">
                                        <strong class="author">Tác giả:</strong>
                                        <asp:Repeater ID="repLinks_au" runat="server" ViewStateMode="Enabled">
                                            <ItemTemplate>
                                                <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("/fts?q=au:{0}{1}", HttpUtility.UrlEncode(Container.DataItem.ToString().Replace("(GVHD)","")),"&ck=false") %>' Text='<%# Container.DataItem.ToString() + ", " %>' ID="HL" ForeColor="#0066FF" />
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="sample">
                                        <strong>Thông tin xuất bản:</strong>
                                        <asp:Label ID="Lb_info" runat="server" Text='<%# string.Format("{0}, {1}", Eval("PublishPlace").ToString().Trim(),Eval("Publisher")) %>'></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="sample">
                                        <strong>Ký hiệu phân loại:</strong>
                                        <asp:HyperLink ID="HyperLink_dewey" NavigateUrl='<%# string.Format("fts?q=ddc:{0}{1}", Eval("Dewey"), "&ck=" + CheckBoxFullText) %>' runat="server"><span class="badge badge-danger py-1"><%#Eval("Dewey") %></span></asp:HyperLink>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="sample">
                                        <asp:Label ID="Lb_isbn_issn" runat="server"></asp:Label>
                                    </div>
                                    <div class="sample list-inline-item">
                                        <strong>Bộ sưu tập:</strong>
                                        <asp:HyperLink ForeColor="#336699" Font-Bold="true" ID="HyperGroupName" NavigateUrl='<%# string.Format("fts?q=grid:{0}{1}", Eval("RecordGroupID"), "&ck=" + CheckBoxFullText) %>' runat="server"><%#Eval("RecordGroupName") %></asp:HyperLink>
                                        <asp:Image ID="Img_open_access" ImageUrl="../Logo/open-access.png" Height="28" runat="server" Visible="false" />                                        
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="row form-inline">                                       
                                        <div class="id col-lg-8 col-md-8 col-sm-8">
                                            <strong>ID:</strong> <%# Eval("RecordID")  %>
                                            <span class=" ml-2"><strong>Năm XB:</strong> </span><a href='<%# string.Format("fts?q=py:{0}{1}", Eval("Year"), "&ck=" + CheckBoxFullText)%>' style="color: darkgoldenrod; font-weight: 600"><%# Eval("Year") %></a>
                                            <asp:Label ID="spanfile" runat="server" class=" ml-2">Định dạng: <%# Eval("Filetype")  %></asp:Label>                                           
                                        </div>
                                        <div class="text-right col-lg-4 col-md-4 col-sm-4">  
                                            <asp:LinkButton ID="LinkCart"  CommandName="AddProducts" CommandArgument='<%# Eval("RecordID") %>' CssClass="sampleCart" runat="server" ><i class="fa fa-star"></i>&nbsp;Lưu vào danh sách</asp:LinkButton>                                            
                                        </div>                                         
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="sample">
                                        <asp:Label ID="Lab_Content" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                       
                    </ItemTemplate>
                </asp:Repeater>
                <div class="text-center mt-5">
                    <asp:LinkButton ID="btnPrev" CssClass="btnFont" runat="server" Text="Trước" OnClick="btnPrev_Click" Visible="false" />
                    <asp:Repeater ID="rptPages" runat="server" DataSource="<%# Paging %>">
                        <ItemTemplate>
                            <span class="paging"><%# Eval("html").ToString() %></span>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:LinkButton ID="btnNext" CssClass="btnFont" runat="server" Text="Tiếp" OnClick="btnNext_Click" Visible="false" />
                </div>
            </div>
        </div>        

        <h4 class="mb-3 mt-5 text-center">Truy cập nhanh danh mục</h4>
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:1&Field=&start=0&ck="><i class="fa fa-book fa-3x" aria-hidden="true"></i>
                        <h6>Sách tham khảo</h6>
                    </a>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:2&Field=&start=0&ck="><i class="fa fa-leanpub fa-3x" aria-hidden="true"></i>
                        <h6>Đồ án TN ĐH-CĐ</h6>
                    </a>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=filetype:pdf OR grid:4&Field=&start=0&ck=false"><i class="fa fa-address-book fa-3x" aria-hidden="true"></i>
                        <h6>Tài liệu điện tử</h6>
                    </a>

                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:4&Field=&start=0&ck=false"><i class="fa  fa-cc fa-3x" aria-hidden="true"></i>
                        <h6>Truy cập mở</h6>
                    </a>

                </div>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:5&Field=&start=0&ck="><i class="fa fa-gears fa-3x" aria-hidden="true"></i>
                        <h6>Nghiên cứu khoa học</h6>
                    </a>

                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:6&Field=&start=0&ck="><i class="fa fa-pencil-square-o fa-3x" aria-hidden="true"></i>
                        <h6>Sách giáo trình</h6>
                    </a>

                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:7&Field=&start=0&ck="><i class="fa fa-newspaper-o fa-3x" aria-hidden="true"></i>
                        <h6>Báo-Tạp chí</h6>
                    </a>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:8&Field=&start=0&ck="><i class="fa fa-paperclip fa-3x" aria-hidden="true"></i>
                        <h6>Bài trích</h6>
                    </a>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:9&Field=&start=0&ck="><i class="fa fa-won fa-3x" aria-hidden="true"></i>
                        <h6>Sách tra cứu</h6>
                    </a>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:10&Field=&start=0&ck="><i class="fa fa-graduation-cap fa-3x" aria-hidden="true"></i>
                        <h6>Luận văn Thạc sĩ</h6>
                    </a>
                </div>
            </div>

            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:11&Field=&start=0&ck="><i class="fa fa-graduation-cap fa-3x" aria-hidden="true"></i>
                        <h6>Luận án Tiến sĩ</h6>
                    </a>

                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-4">
                <div class="box-part text-center">
                    <a href="/fts?q=grid:12&Field=&start=0&ck="><i class="fa fa-leanpub fa-3x" aria-hidden="true"></i>
                        <h6>Đồ án môn học</h6>
                    </a>

                </div>
            </div>
        </div>
       
    </div>
</asp:Content>
