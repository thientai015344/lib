<%@ Page Language="C#" AutoEventWireup="true" CodeFile="~/Student/Listdown.aspx.cs" Inherits="StudentDown" MasterPageFile="~/Site.master" %>

<asp:Content ID="CartContent" ContentPlaceHolderID="MainContent" runat="server">
 <div class="container-fluid" style="font-family: Arial">
    <div class="row">
        <div style="margin-top: 80px">            
                <div class="container">
                    <div class="row ">                         
                        <div class="col-md-4 text-white text-center " style="background:#00ff21">                             
                            <div class=" ">
                                <div class="card-body">
                                    <img src="../Logo/arrows.png" style="width: 20%">
                                    <h4 class="py-3">Danh sách tài liệu học tập</h4>
                                    <p>
                                        
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="text-right">
                                <asp:Button ID="logout" runat="server" class="btn btn-danger" Text="Thoát" OnClick="logout_Click" />
                            </div>
                        <div class="col-md-8 py-5 border">                           
                                <h4 class="pb-4">Danh sách File</h4>                                                       
                            <div class="row">                                 
                                <div>
                                    <table class="table">
                                        <tr>
                                            <td>
                                                <asp:DataList ID="DataListDownload" runat="server" OnItemDataBound="DataListDownload_ItemDataBound">
                                                    <ItemTemplate>
                                                        <div>
                                                            <tr>
                                                                <td class="col-lg-1 col-md-1 col-sm-1">
                                                                    <%# DataListDownload.Items.Count +1 %>
                                                                </td>
                                                                <td class="col-lg-1 col-md-1 col-sm-1">
                                                                    <asp:Label ID="La_mamon" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="col-lg-5 col-md-5 col-sm-5">
                                                                    <asp:Label ID="La_tenmon" runat="server"></asp:Label>
                                                                </td>
                                                                <td class="col-lg-2 col-md-2 col-sm-2">
                                                                    <asp:HyperLink ID="Hyper_down" runat="server">pdf <span class="glyphicon glyphicon glyphicon-save"></span></asp:HyperLink><asp:Label ID="Lab_dungluong" runat="server"></asp:Label>

                                                                </td>
                                                            </tr>

                                                        </div>
                                                    </ItemTemplate>
                                                </asp:DataList>
                                            </td>
                                        </tr>
                                    </table>
                                </div>                           
                                                                                           
                            </div>                           
                        </div>
                    </div>
                </div>
        </div>       
    </div>
</div>
</asp:Content>
