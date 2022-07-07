<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuObject.ascx.cs" Inherits="Book_MenuObject" %>
<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet">
<link href='https://fonts.googleapis.com/css?family=Lato:300,400,700' rel='stylesheet' type='text/css'>
<script src="https://code.jquery.com/jquery-1.12.0.min.js"></script>       
<script src='https://www.google.com/recaptcha/api.js'></script>  
<style type="text/css">
    .container-fluid
    {
        font-family: Arial,sans-serif;
        font-size:13px  
    }
    .Locketqua
     {  
        color:#fff;         
        text-align:center;       
        background: #0078ae;
        line-height:20px;
        height:22px;
        border-top-right-radius: 10px 10px;
	    border-top-left-radius: 10px 10px;
    }
</style>
<div class="container-fluid">
    <div class=" Locketqua ">
        <p><b>Lọc kết quả </b></p>
    </div>   
    <div class="row col-md-offset-1">
        <asp:DataList ID="DataList_Author100" runat="server" Style="text-align: left"
            OnItemDataBound="DataList1_ItemDataBound" CaptionAlign="Left"
            HorizontalAlign="Left">
            <FooterTemplate>
                <div >
                    <asp:HyperLink ID="Hyper_viewmoreAuthor" runat="server" Font-Italic="True"
                        NavigateUrl='<%# "~/Viewmoreauthor.aspx?q=" + HttpUtility.UrlEncode(GetKeyword) +"&rag="+ GetRag %>'
                        Style="text-decoration: underline" Visible="False">Xem thêm</asp:HyperLink>
                </div>
            </FooterTemplate>
            <HeaderTemplate>
                <strong>Tác giả</strong>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <asp:HyperLink ID="HPAuthor100" runat="server" Text='<%# Eval("Tacgia100") %>' ForeColor="#004B91"></asp:HyperLink>
                </li>
            </ItemTemplate>
        </asp:DataList>
    </div>      
    <div class="row col-md-offset-1">
        <div>
            <asp:DataList ID="DL_Chude" runat="server"
                OnItemDataBound="DataList_Chude_ItemDataBound" CaptionAlign="Left"
                HorizontalAlign="Left">
                <FooterTemplate>
                    <div>
                        <asp:HyperLink ID="Hyper_viewmoreSubject" runat="server" CssClass="viewmore"
                            Font-Italic="True"
                            NavigateUrl='<%# "~/Viewmoresubject.aspx?q=" + HttpUtility.UrlEncode(GetKeyword) +"&rag="+ GetRag %>'
                            Visible="False">Xem thêm</asp:HyperLink>
                    </div>
                </FooterTemplate>
                <HeaderTemplate>
                    <strong>Chủ đề</strong>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="HPSubject" runat="server" ForeColor="#004B91"
                            Text='<%# Eval("Chủ đề").ToString() %>'></asp:HyperLink>
                    </li>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>       
    <div class="row col-md-offset-1">
        <div>
            <asp:DataList ID="DataList_Year" runat="server" CaptionAlign="Left"
                HorizontalAlign="Left" Visible="False">
                <FooterTemplate>
                </FooterTemplate>
                <HeaderTemplate>
                    <strong>Năm xuất bản</strong>
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:HyperLink ID="HPYear" runat="server" ForeColor="#004B91" Text='<%# Eval("Năm XB").ToString() %>'></asp:HyperLink>
                    </li>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>        
</div>