<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MajorsList.ascx.cs" Inherits="Book_MajorsList" %>
<link rel="stylesheet" href="../HomeLib/css/bootstrap.min.css"/>
<link href="../Content/Majors.css" rel="stylesheet" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
<script>
	/**
 * Clearable text inputs
 */
	function tog(v) { return v ? "addClass" : "removeClass"; }
	$(document).on("input", ".clearable", function () {
		$(this)[tog(this.value)]("x");
	}).on("mousemove", ".x", function (e) {
		$(this)[tog(this.offsetWidth - 18 < e.clientX - this.getBoundingClientRect().left)]("onX");
	}).on("touchstart click", ".onX", function (ev) {
		ev.preventDefault();
		$(this).removeClass("x onX").val("").change();
	});

// $('.clearable').trigger("input");
// Uncomment the line above if you pre-fill values from LS or server
</script>
<style>
    /*
    Clearable text inputs
*/
    .clearable {
        background: #fff url(http://i.stack.imgur.com/mJotv.gif) no-repeat right -10px center;
        border: 1px solid #999;
        padding: 3px 18px 3px 4px; /* Use the same right padding (18) in jQ! */
        border-radius: 3px;
        transition: background 0.4s;
    }

        .clearable.x {
            background-position: right 5px center;
        }
        /* (jQ) Show icon */
        .clearable.onX {
            cursor: pointer;
        }
        /* (jQ) hover cursor style */
        .clearable::-ms-clear {
            display: none;
            width: 0;
            height: 0;
        }
    /* Remove IE default X */
</style>


<div class="container-fluid">
    <div class="mt-1 col-md-12" id="div_info" runat="server">
        <div class="row text-black text-left mb-3">
            <div>
                <p>
                    <asp:Label ID="Lab_tenmon" runat="server" CssClass="pb-1 font-weight-bold" Font-Size="1.2em"></asp:Label>
                </p>
                <asp:Label ID="Lab_mota" runat="server"></asp:Label>
            </div>
        </div>
    </div>
    <div class="form-group" id="div_info_search" runat="server">
        <div class="row col-md-12">
            <div class="col-md-4">
                <asp:CheckBox ID="CheckBox_GTC" CssClass=" form-check" Text="&nbsp;Giáo trình chính" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_GTC_CheckedChanged1" ToolTip="Giáo trình chính theo đề cương chi tiết" />
            </div>
            <div class="col-md-4">
                <asp:CheckBox ID="CheckBox_TLTK" CssClass="form-check" Text="&nbsp;Tham khảo theo ĐCCT" runat="server" AutoPostBack="True" OnCheckedChanged="CheckBox_TLTK_CheckedChanged" ToolTip="Tài liệu tham khảo theo đề cương chi tiết" />
            </div>
            <div class="col-md-4 mb-1">
                <div class="input-group md-form form-sm form-2 pl-0">
                    <asp:TextBox ID="Txt_search" CssClass="clearable form-control my-0 py-1" placeholder="Tìm trong môn học này" aria-label="Search" runat="server" OnTextChanged="Txt_search_TextChanged"></asp:TextBox>                   
                    <div class="input-group-append">
                        <button class="input-group-text red lighten-3" onserverclick="Txt_search_TextChanged" runat="server"><i class="fa fa-search text-grey"
                            aria-hidden="true"></i></button>
                    </div>					
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <asp:DataList ID="DataList_Document" runat="server" Font-Names="Arial" Width="100%" OnItemDataBound="DataList_Document_ItemDataBound">
                <ItemTemplate>
                    <div style="border-top: 1px dotted #c1650f; margin-top: -3px; padding-top: 2px">
                        <table id="table_result" class="table">
                            <tr>
                                <td colspan="2">                                
                                    <a class="link" href='<%#"/chi-tiet?id="+ Eval("RecordID")%>'  runat="server" target="_blank"><%# LimitLength(Eval("Nhan đề").ToString(),100,"...") %></a>
                                                                    
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="4">
                                    <div class="table_td_thumbnail">
                                        <asp:HyperLink ID="hplImage" runat="server" NavigateUrl='<%#"/chi-tiet?id="+ Eval("RecordID")%>' Target="_blank">
                                            <asp:Image ID="ImageBook" runat="server" Width="85"  ToolTip='<%# Eval("Nhan đề")%>' />
                                        </asp:HyperLink>
                                    </div>
                                </td>
                                <td class="col-sm-12 col-md-12">
                                    <span style="color: blueviolet">Tác giả:</span>
                                    <asp:Repeater ID="repLinks" runat="server" ViewStateMode="Enabled">
                                        <ItemTemplate>
                                            <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("/fts?q=au:{0}&ck=false", HttpUtility.UrlEncode(Container.DataItem.ToString().Replace("(GVHD)",""))) %>' Text='<%# Container.DataItem.ToString() + ", " %>' ID="HL" ForeColor="#0066CC" />
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Thông tin xuất bản: </strong>
                                    <asp:Label ID="lbNamXB" runat="server" Text='<%# Eval("Nơi XB").ToString() +" "+Eval("Nhà XB").ToString() +" "+ Eval("Năm XB").ToString() %>' />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Ký hiệu phân loại: </strong>
                                    <asp:HyperLink ID="HyperDewey" runat="server" NavigateUrl='<%# Eval("[Dewey]", "/fts?q=ddc:{0}&ck=false") %>'><%# Eval("Dewey").ToString()%></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>Bộ sưu tập: </strong>                                    
                                    <asp:HyperLink ForeColor="#336699" Font-Bold="true" ID="HyperGroupName" NavigateUrl='<%# string.Format("fts?q=grid:{0}{1}", Eval("RecordGroupID"), "&ck=false") %>' runat="server"><%#Eval("RecordGroupName") %></asp:HyperLink>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-3957443453467964"
                                        crossorigin="anonymous"></script>
                                    <ins class="adsbygoogle"
                                        style="display: block"
                                        data-ad-format="fluid"
                                        data-ad-layout-key="-gm-f-1y-a5+y1"
                                        data-ad-client="ca-pub-3957443453467964"
                                        data-ad-slot="3484625228"></ins>
                                    <script>
                                        (adsbygoogle = window.adsbygoogle || []).push({});
                                    </script>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>

    <div class="col-md-12 mt-3 mb-5 form-group " id="div_page" runat="server">
        <div role="group" class="form-inline justify-content-center ">
            <asp:ImageButton ID="lbtnPrev" CssClass="mr-2" runat="server" ImageUrl="~/Logo/Prev.png" OnClick="lbtnPrev_Click" />
            <asp:DropDownList ID="DropDownList_page" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList_page_SelectedIndexChanged">
            </asp:DropDownList>
            <asp:ImageButton ID="lbtnNext" CssClass=" ml-2" runat="server" ImageUrl="~/Logo/next.png" OnClick="lbtnNext_Click" />
        </div>
    </div>
    <div class="row">
       
    </div>
</div>

