<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Detail.ascx.cs" Inherits="Book_Detailt" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="../Content/PageNumber.css" rel="stylesheet" />
<link href="../Content/DetailLocaltion.css" rel="stylesheet" />
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>
<script src='https://www.google.com/recaptcha/api.js'></script>
<style type="text/css">
    .main {
        margin-top: 60px;
        font-family: 'Helvetica';
    }

    strong {
        color: #716f6f;
    }

    .font {
        font-size: 1em;
        height: 1.8em;
        display: inline-block;
    }

    ul.list-group:after {
        clear: both;
        display: block;
        content: "|";
    }

    .list-group-item {
        float: left;
    }

    .circleDBP {
        width: 32px;
        height: 32px;
        border-radius: 50%;
        font-size: 14px;
        font-family: 'Noto Serif';
        color: #fff;
        line-height: 32px;
        text-align: center;
        background: #0e798d;
        font-weight: 600;
        margin-right: 5px
    }

    .circleQ9 {
        width: 32px;
        height: 32px;
        border-radius: 50%;
        font-size: 13px;
        color: #fff;
        line-height: 32px;
        text-align: center;
        background: #456ed0;
    }

    .tablehand {
        line-height: 40px;
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

    .custom {
        width: 120px !important;
    }
    .down {
        padding:0 10px 0 10px
    }
    .mt {
        margin-top:20px;     
    }
</style>
<div class="main">
    <div class="container-fluid">
        <div class="row">
            <div class=" col-lg-9 col-md-8">
                <div id="bookcover" style="float: left; margin: 0px; padding: 0px;">
                    <div>
                        <asp:Image ID="ImageBook" runat="server" Height="180px" Width="130px" Style="margin: 0px 1em 1em 0px;" />
                    </div>
                </div>
                <div>
                    <asp:Label ID="Title" runat="server" Style="font-weight: bold; font-size: 1.2em; font-family: Arial, Helvetica, sans-serif;"></asp:Label>
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
                                <asp:HyperLink runat="server" NavigateUrl='<%# string.Format("/fts?q=au:{0}{1}", HttpUtility.UrlEncode(Container.DataItem.ToString().Replace("(GVHD)","")),"&ck=false") %>' Text='<%# Container.DataItem.ToString() + ", " %>' ID="HL" ForeColor="#0066FF" />
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <br />
                    <div class="font">
                        <strong>Bộ sưu tập: </strong>
                        <asp:Label ID="Lab_bst" runat="server"></asp:Label>
                        <asp:Image ID="Img_open_access" ImageUrl="../Logo/open-access.png" Height="28" runat="server" Visible="false" />
                    </div>
                    <br />
                    <div class="font"><strong>ISBN: </strong>&nbsp;<asp:Label ID="isbn" runat="server"></asp:Label></div>
                    <br />
                    <div class="font">
                        <strong>Ký hiệu phân loại: </strong>
                        <asp:HyperLink ID="Dewey" runat="server"></asp:HyperLink>
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        <asp:HyperLink ID="DeweyName" runat="server"></asp:HyperLink>
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
                    <div class="font">
                        <strong>Ngôn ngữ: </strong>
                        <asp:Label ID="Languages" runat="server"></asp:Label>
                    </div>
                    <br />
                    <div class="row">
                        <div class="font col-lg-5">
                            <strong>ID: </strong>
                            <asp:Label ID="BookID" runat="server"></asp:Label>
                        </div>
                        <div class=" col-lg-5 text-right">
                            <asp:LinkButton runat="server" CssClass="btn btn-danger" CommandName="AddProducts" OnClick="ButtonAddtoCart" CommandArgument='<%# Eval("RecordID") %>' ID="AddCart"><span class="glyphicon glyphicon-star" aria-hidden="true"></span>&nbsp;Lưu vào danh sách</asp:LinkButton>
                        </div>
                    </div>

                    <br />
                    <div class="font">
                        <asp:HyperLink ID="Link_outsite" runat="server" Font-Bold="False" Font-Italic="True" Font-Underline="True" Visible="false" Target="_blank">Sách online</asp:HyperLink>
                    </div>

                </div>
                <div class="row">
                    <div class=" col-lg-12 col-md-12 col-sm-12 col-xs-12 ">
                        <!-- Nav tabs -->
                        <div class="card">
                            <ul class="nav nav-tabs" role="tablist">
                                <li role="presentation"><a href="#mota" aria-controls="mota" role="tab" data-toggle="tab">Tóm tắt</a></li>
                                <li role="presentation"><a href="#chude" aria-controls="chude" role="tab" data-toggle="tab">Chủ đề</a></li>
                                <li role="presentation" id="film" runat="server"><a href="#video" aria-controls="video" role="tab" data-toggle="tab">Video</a></li>
                                <li role="presentation" id="tabdownload" runat="server" class="active"><a href="#down" aria-controls="down" role="tab" data-toggle="tab">Tải về</a></li>
                            </ul>
                            <!-- Tab panes -->
                            <div class="tab-content">
                                <div role="tabpanel" class="tab-pane " id="mota">
                                    <asp:Label ID="Tomtat" runat="server"></asp:Label>
                                </div>
                                <div role="tabpanel" class="tab-pane" id="chude">
                                    <p id="subject" runat="server"></p>
                                </div>
                                <div role="tabpanel" class="tab-pane col-lg-8 col-md-8" id="video">
                                    <asp:PlaceHolder ID="LVideo" runat="server">
                                        <iframe id="ifvideo" runat="server" height="315" width="100%" allowfullscreen="" frameborder="0"></iframe>
                                    </asp:PlaceHolder>
                                    <asp:Label ID="Mesvideo" runat="server" Text="Chưa có video"></asp:Label>
                                </div>
                                <div role="tabpanel" class="tab-pane active" id="down">
                                    <asp:DataList ID="DataListDownload" runat="server" OnItemDataBound="DataListDownload_ItemDataBound" Width="100%">
                                        <ItemTemplate>
                                            <div class="row form-group mt">
                                                <div class="col-md-12 col-xs-12">
                                                    <%# DataListDownload.Items.Count +1 %>)&ensp;
                                                    <asp:HyperLink ID="HyperView" runat="server" Target="_blank" Font-Underline="true">Xem mục lục</asp:HyperLink>&ensp;
                                                    <asp:HyperLink ID="HpDown" runat="server" class="btn btn-sm btn-info text-center">
                                                        <span class="glyphicon glyphicon-save "></span>Tải sách
                                                            <asp:Label ID="Lenght" runat="server"></asp:Label>
                                                    </asp:HyperLink>
                                                    <asp:Label ID="LaFilename" runat="server"></asp:Label>
                                                </div>
                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>

            </div>

            <!--ben trai-->
            <div class=" col-lg-3 col-md-3 col-sm-4 ">
                <div class="row" id="sachmuon" runat="server" visible="false">
                    <div class="bg-primary text-center" style="padding: 8px">Thông tin sách đang mượn và vị trí </div>
                    <div style="padding: 5px 0 5px 10px">
                        <asp:Label ID="La_soban" runat="server"></asp:Label><br />
                        <asp:Label ID="La_dangmuon" runat="server"></asp:Label><br />
                        <asp:Label ID="La_khongchomuon" runat="server" ForeColor="Red" Font-Italic="true"></asp:Label>
                    </div>
                    <div id="divtable" runat="server">
                        <table id="tbvitri" class="tablehand" runat="server">
                            <tr>
                                <td>
                                    <div>
                                        <span class="lngbtn">DÃY KỆ</span>
                                    </div>
                                </td>
                                <td>
                                    <div>
                                        <asp:Repeater ID="Repeater_Pos" runat="server" ViewStateMode="Enabled" OnItemDataBound="Repeater_Pos_ItemDataBound">
                                            <ItemTemplate>
                                                <div class="row">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <div class="circleDBP">
                                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Dayke").ToString() %> '></asp:Label>
                                                                </div>
                                                            </td>
                                                            <td>
                                                                <div>
                                                                    <asp:Label ID="LaPosition" runat="server" Text='<%# Eval("InventoryLocation").ToString()  %> '></asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" class=" text-right">
                                    <div class="text-left">
                                        <span class="glyphicon glyphicon-hand-right"></span>
                                        <asp:Label ID="Lab_dewey090" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="row" id="DivBookShow" runat="server" visible="false">
                    <div class="bg-primary " style="padding: 5px 0px 5px 30px; font-weight: bold; font-family: 'Times New Roman', Times, serif">Cuốn này đang trưng bày tại: </div>

                    <div style="padding: 5px 0 5px 10px" class="text-center">
                        <img src="../Logo/new.gif" width="40">
                        <asp:Label ID="La_subbook" runat="server" ForeColor="#006666" Font-Bold="true" Font-Size="13pt" Font-Names="arial"></asp:Label><br />
                        <asp:Label ID="La_infosubbook" runat="server"></asp:Label><br />
                        <asp:Label ID="Label3" runat="server" ForeColor="Violet"></asp:Label>
                    </div>

                </div>
                <div class="row" id="qrcodeimg" style="background: rgb(243, 243, 243); margin-top: 5px; border: 1px solid rgb(208, 208, 208); vertical-align: middle">

                    <h1 id="h1cd" style="margin: 0px 0px 10px; font-family: inherit; font-weight: bold; line-height: 19.5px; color: rgb(0, 102, 153); text-rendering: optimizeLegibility; font-size: 13px; background: rgb(217, 217, 217); border-bottom: 1px solid rgb(208, 208, 208); padding-top: 10px; padding-bottom: 10px; padding-left: 25px;"></h1>
                    <div class="text-center " style="margin-bottom: 10px">
                        <asp:Image ID="ImageQrCode" runat="server" TabIndex="1" />
                    </div>
                   
                </div>
                <div class="row">
                    <script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client=ca-pub-3957443453467964"
                        crossorigin="anonymous"></script>
                    <!-- Khánh My -->
                    <ins class="adsbygoogle"
                        style="display: block"
                        data-ad-client="ca-pub-3957443453467964"
                        data-ad-slot="3093371858"
                        data-ad-format="auto"
                        data-full-width-responsive="true"></ins>
                    <script>
                        (adsbygoogle = window.adsbygoogle || []).push({});
                    </script>
                </div>
            </div>
        </div>
         <div class="row">
            <div class="col-md-12 col-xs-12" >
                <div class="fb-comments" data-href="<%=Request.Url %>" data-width="" data-numposts="5"></div>
                <div id="fb-root"></div>
                
            </div>             
        </div>   
        <div class="row">

        </div>
    </div>
</div>












