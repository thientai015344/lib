<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Guestdownload.ascx.cs" Inherits="Book_Guestdownload" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../Content/PageNumber.css" rel="stylesheet" />
<link href="../Content/DetailPos.css" rel="stylesheet" />
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
<script src='https://www.google.com/recaptcha/api.js'></script>
<style type="text/css">
    .auto-style1 {
        float: left;
    }

    .main {
        font-family: Arial,sans-serif;
        margin-top: 65px;
    }

    .font {
        font-size: 11.9px;
        height: 1.8em;
        display: inline-block;
    }
    .Star {
        background-image: url(logo/Star.gif);
        height: 17px;
        width: 17px;
    }

    .WaitingStar {
        background-image: url(logo/WaitingStar.gif);
        height: 17px;
        width: 17px;
    }

    .FilledStar {
        background-image: url(logo/FilledStar.gif);
        height: 17px;
        width: 17px;
    }
</style>
<div class="main">
    <div class="container-fluid">
        <div class="row">
            <div class=" text-center">
                <asp:Label ID="Label_info" runat="server" class="p-3 mb-2 bg-danger text-white" Visible="false" Text="Tập tin này không tồn tại"></asp:Label>
            </div>
            <div id="panel1" runat="server">
                <div class="row">
                    <div class=" col-lg-9 col-md-8">
                        <div id="bookcover" style="margin: 0px; padding: 0px;" class="auto-style1">
                            <div>
                                <asp:Image ID="ImageBook" runat="server" Height="180px" Width="130px" Style="margin: 0px 1em 1em 0px;" />
                            </div>
                        </div>
                        <div>
                            <h1 style="font-weight: bold; font-size: 19.6px;">
                                <asp:Label ID="Title" runat="server"></asp:Label></h1>
                            <div>
                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                                <cc1:Rating ID="Rating1" AutoPostBack="true" OnChanged="OnRatingChanged" runat="server"
                                    StarCssClass="Star" WaitingStarCssClass="WaitingStar" EmptyStarCssClass="Star"
                                    FilledStarCssClass="FilledStar">
                                </cc1:Rating>
                                &nbsp<asp:Label ID="lblRatingStatus" runat="server" Font-Size="10pt"></asp:Label>
                            </div>
                            <div class="font">
                                <strong>Tác giả: </strong>
                                <asp:Repeater ID="repLinks" runat="server" ViewStateMode="Enabled">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("/fts?q=au:{0}&ck=false", HttpUtility.UrlEncode(Container.DataItem.ToString().Replace("(GVHD)",""))) %>' Text='<%# Container.DataItem.ToString() + ", " %>' ID="HL" ForeColor="#0066FF" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                            <br />
                            <div class="font">
                                <strong>Bộ sưu tập: </strong>
                                <asp:Label ID="bst" runat="server"></asp:Label>
                            </div>
                            <br />
                            <div class="font">
                                <strong>Thông tin xuất bản: </strong>
                                <asp:Label ID="InfoPublisher" runat="server"></asp:Label>
                            </div>
                            <br />
                            <div class="font">
                                <strong>Mô tả vật lý: </strong>
                                <asp:Label ID="Description" runat="server"></asp:Label>
                            </div>
                            <br />
                        </div>

                    </div>
                </div>
            </div>
            <div id="panel2" runat="server">
                <h4>Tải sách</h4>
                <table class="table" style="color: #076381">
                    <tr>
                        <td class="col-lg-3 col-sm-4  col-xs-5">
                            <div class="p-3 mb-2 bg-info text-white">
                                <asp:Label ID="Labelpage1" runat="server"></asp:Label>
                            </div>
                        </td>
                        <td>
                            <asp:LinkButton ID="LinkButton_load" ForeColor="#0099CC" runat="server" OnClick="LinkButton_load_Click">PDF <span class="glyphicon glyphicon glyphicon-save"></span></asp:LinkButton>

                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="p-3 mb-2 bg-info text-white">
                                <asp:Label ID="Labelpage2" runat="server"></asp:Label>
                            </div>

                        </td>
                        <td>Liên hệ Thư viện HUTECH (tt.thuvien@hutech.edu.vn)</td>
                    </tr>
                </table>
            </div>
            <div class="fb-comments" data-href="" data-numposts="5"></div>
            <div id="fb-root"></div>
            <script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = 'https://connect.facebook.net/vi_VN/sdk.js#xfbml=1&version=v3.2&appId=1603828266537773&autoLogAppEvents=1';
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
        </div>
    </div>
</div>

